import logging
import math
import time
import warnings
from typing import Generator

import numpy as np
import pandas as pd

from src.pipeline.config import rag_category_settings

pd.options.mode.chained_assignment = None

logger = logging.getLogger("fbit-data-pipeline")

base_cols = [
    "URN",
    "Total Internal Floor Area",
    "Age Average Score",
    "OfstedRating (name)",
    "Percentage SEN",
    "Percentage Free school meals",
    "Number of pupils",
]


def is_area_close_comparator(org_a, org_b):
    """
    Determine whether two organisations are close assuming both:

    - gross internal floor area is within 10%;
    - average age of buildings is within 20 years.

    :param org_a: first organisation for comparison
    :param org_b: second organisation for comparison
    :return: whether organisations are close, as defined
    """
    floor_area_is_close = math.isclose(
        org_a["Total Internal Floor Area"],
        org_b["Total Internal Floor Area"],
        rel_tol=0.1,
    )
    average_age_score_is_close = math.isclose(
        org_a["Age Average Score"],
        org_b["Age Average Score"],
        abs_tol=20.0,
    )

    return floor_area_is_close and average_age_score_is_close


def is_pupil_close_comparator(org_a, org_b):
    """
    Determine if two organisations are close assuming all:

    - number of pupils is within 25%;
    - percentage of FSM (Free School Meals) is within 5%;
    - percentage of SEN (Special Educational Need) is within 10%;

    :param org_a: first organisation for comparison
    :param org_b: second organisation for comparison
    :return: whether organisations are close, as defined
    """
    number_of_pupils_is_close = math.isclose(
        org_a["Number of pupils"],
        org_b["Number of pupils"],
        rel_tol=0.25,
    )
    fsm_is_close = math.isclose(
        org_a["Percentage Free school meals"],
        org_b["Percentage Free school meals"],
        rel_tol=0.05,
    )
    sen_is_close = math.isclose(
        org_a["Percentage SEN"],
        org_b["Percentage SEN"],
        rel_tol=0.1,
    )

    return all(
        (
            number_of_pupils_is_close,
            fsm_is_close,
            sen_is_close,
        )
    )


def is_close_comparator(category_type, org_a, org_b):
    """
    Determine which "close range" is to be used.

    For building cost categories (i.e. "premises staff and services and
    utilities"), area calculations are used.

    For all other cost categories, pupil characteristics are used.

    :param org_a: first organisation for comparison
    :param org_b: second organisation for comparison
    :return: whether organisations are close, as defined
    """
    if category_type == "Building":
        return is_area_close_comparator(org_a, org_b)

    return is_pupil_close_comparator(org_a, org_b)


def find_percentile(d, value):
    sorted_series = np.sort(d, axis=0, kind="stable")
    rank = np.searchsorted(sorted_series, value, side="right")
    pc = rank / len(d) * 100
    return pc - 1


def category_stats(urn, category_name, data, ofsted_rating, rag_mapping, close_count):
    key = "outstanding" if ofsted_rating.lower() == "outstanding" else "other"
    key += "_10" if close_count > 10 else ""

    series = data[category_name]
    value = series.loc[urn]

    percentile = find_percentile(series, value)
    decile = int(percentile / 10)
    mean = np.median(series)
    diff = value - mean
    diff_percent = (
        (diff / mean) * 100
        if mean != 0 and mean != np.inf and mean != np.nan and not pd.isna(mean)
        else 0
    )
    cats = category_name.split("_")
    return {
        "URN": urn,
        "Category": cats[0],
        "SubCategory": cats[1],
        "Value": value,
        "Mean": mean,
        "DiffMean": diff,
        "Key": key,
        "PercentDiff": diff_percent,
        "Percentile": percentile,
        "Decile": decile,
        "RAG": rag_mapping[key][int(decile)],
    }


def compute_category_rag(
    urn, category_name, settings, target, comparator_set, col_cache
):
    ofsted = target["OfstedRating (name)"]

    close_count = sum(
        comparator_set.apply(
            lambda x: 1 if is_close_comparator(settings["type"], target, x) else 0,
            axis=1,
        )
    )

    # series, sub_categories = get_category_series(category_name, comparator_set)
    cols, sub_categories = col_cache[category_name]
    series = comparator_set[comparator_set.columns[cols]]

    for sub_category in sub_categories:
        yield category_stats(urn, sub_category, series, ofsted, settings, close_count)


def get_category_cols_predicates(category_name, data):
    category_cols = data.columns.isin(base_cols) | (
        data.columns.str.startswith(category_name)
        & (data.columns.str.endswith("_Per Unit"))
    )

    df = data[data.columns[category_cols]]
    dt = df.dtypes
    sub_categories = dt[dt.index.str.startswith(category_name)].index.values

    return category_cols, sub_categories


def compute_rag(data, comparators):
    keys = rag_category_settings.keys()

    # reduce to only used columns so that extraction routines are more efficient
    cols = data.columns.isin(base_cols) | data.columns.str.endswith("_Per Unit")
    df = data[data.columns[cols]].fillna(0.0)

    # Pre-computes the column accessors for each cost category
    column_cache = {}
    for cat_name in keys:
        column_cache[cat_name] = get_category_cols_predicates(cat_name, df)
    indices = range(len(df))
    st = time.time()

    with warnings.catch_warnings():
        warnings.simplefilter("ignore", category=RuntimeWarning)
        with np.errstate(invalid="ignore"):
            for indx in indices:
                target = df.iloc[indx]
                urn = target.name
                try:
                    pupil_urns = comparators["Pupil"][urn]
                    building_urns = comparators["Building"][urn]
                    for cat_name in keys:
                        rag_settings = rag_category_settings[cat_name]
                        set_urns = (
                            pupil_urns
                            if rag_settings["type"] == "Pupil"
                            else building_urns
                        )
                        if set_urns is not None:
                            comparator_set = df[df.index.isin(set_urns)]

                            for r in compute_category_rag(
                                urn,
                                cat_name,
                                rag_settings,
                                target,
                                comparator_set,
                                column_cache,
                            ):
                                yield r
                        else:
                            logger.warning(
                                f'Unable to compute rag for {cat_name} - {rag_settings["type"]} - {urn}'
                            )
                    if indx > 1 and indx % 1000 == 0:
                        logger.debug(
                            f"Completed {indx} RAGs in {time.time() - st:.2f} secs"
                        )
                        st = time.time()
                except Exception as error:
                    logger.exception(
                        f"An exception {type(error).__name__} occurred processing {urn}:",
                        exc_info=error,
                    )
                    return


def compute_user_defined_rag(
    data: pd.DataFrame,
    target_urn: int,
    set_urns: list[int],
) -> Generator[dict, None, None]:
    """
    Perform user-defined RAG calculation.

    TODO: largely as per `compute_rag()` save that `set_urns` defines
    the comparator set.

    :param data: org. data for RAG computation
    :param target_urn: URN of the reference org.
    :param set_urns: URNs for use as the comparator-set
    """
    # reduce to only used columns so that extraction routines are more efficient
    cols = data.columns.isin(base_cols) | data.columns.str.endswith("_Per Unit")
    df = data[data.columns[cols]].fillna(0.0)

    # Pre-computes the column accessors for each cost category
    column_cache = {}
    for category in rag_category_settings:
        column_cache[category] = get_category_cols_predicates(category, df)
    st = time.time()

    with warnings.catch_warnings():
        warnings.simplefilter("ignore", category=RuntimeWarning)
        with np.errstate(invalid="ignore"):
            target = df.loc[target_urn]
            urn = target.name
            try:
                comparator_set = df[df.index.isin(set_urns)]
                for category in rag_category_settings:
                    rag_settings = rag_category_settings[category]
                    for r in compute_category_rag(
                        urn,
                        category,
                        rag_settings,
                        target,
                        comparator_set,
                        column_cache,
                    ):
                        yield r
                logger.debug(
                    f"Completed user-defined RAGs in {time.time() - st:.2f} secs."
                )
                st = time.time()
            except Exception as error:
                logger.exception(
                    f"An exception {type(error).__name__} occurred processing {urn}:",
                    exc_info=error,
                )
                return
