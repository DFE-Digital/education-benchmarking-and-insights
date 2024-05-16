import math
import warnings
import time
import numpy as np
import pandas as pd
import logging
from src.pipeline.config import rag_category_settings

pd.options.mode.chained_assignment = None

logger = logging.getLogger("fbit-data-pipeline")
logger.setLevel(logging.INFO)

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
    if category_type == "area":
        return is_area_close_comparator(org_a, org_b)

    return is_pupil_close_comparator(org_a, org_b)


def get_category_series(category_name, data):
    category_cols = (
        data.columns.isin(base_cols)
        | data.columns.isin(["is_close"])
        | data.columns.str.startswith(category_name)
    )
    df = data[data.columns[category_cols]]

    sub_categories = df.columns[
        df.columns.str.startswith(category_name)
    ].values.tolist()

    return df, sub_categories


def category_stats(urn, category_name, data, ofsted_rating, rag_mapping):
    close_count = data["is_close"][data["is_close"]].count()
    key = "outstanding" if ofsted_rating.lower() == "outstanding" else "other"
    key += "_10" if close_count > 10 else ""

    series = data[category_name]
    # TODO: The next 2 lines account for ~60% of the execution time of this method
    percentiles = pd.qcut(series, 100, labels=False, duplicates="drop")
    percentile = int(np.nan_to_num(percentiles.iloc[0]))
    decile = int(percentile / 10)
    value = float(np.nan_to_num(series.iloc[0]))
    mean = float(np.nan_to_num(series.median()))
    diff = value - mean
    diff_percent = (diff / value) * 100 if value != 0 else 0
    cats = category_name.split('_')
    return {
        "Urn": urn,
        "Category": cats[0],
        "Sub category": cats[1],
        "Value": value,
        "Mean": mean,
        "Diff Mean": diff,
        "Key": key,
        "Percentage Diff": diff_percent,
        "Percentile": percentile,
        "Decile": decile,
        "Rag": rag_mapping[key][int(decile)]
    }


def compute_category_rag(category_name, settings, target, comparator_set):
    ofsted = target["OfstedRating (name)"]
    urn = target.name

    comparator_set["is_close"] = comparator_set.apply(
        lambda x: is_close_comparator(settings["type"], target, x), axis=1
    )

    series, sub_categories = get_category_series(category_name, comparator_set)

    for sub_category in sub_categories:
        yield category_stats(urn, sub_category, series, ofsted, settings)


def compute_rag(data, comparators):
    # TODO: This shouldn't be required
    with warnings.catch_warnings():
        warnings.simplefilter("ignore", category=RuntimeWarning)
        with np.errstate(invalid="ignore"):
            indices = range(len(data))
            keys = rag_category_settings.keys()
            st = time.time()
            for indx in indices:
                target = data.iloc[indx]
                urn = target.name
                pupil_urns = comparators["Pupil"][urn]
                building_urns = comparators["Building"][urn]
                for cat_name in keys:
                    rag_settings = rag_category_settings[cat_name]
                    set_urns = pupil_urns if rag_settings["type"] == "Pupil" else building_urns
                    if set_urns is not None:
                        comparator_set = data[data.index.isin(set_urns)]
                        for r in compute_category_rag(cat_name, rag_settings, target, comparator_set):
                            yield r
                    else:
                        logger.warning(f'Unable to compute rag for {cat_name} - {rag_settings["type"]} - {urn}')
                if indx > 1 and indx % 100 == 0:
                    logger.info(f'Completed {indx} RAGs in {time.time() - st:.2f} secs')
                    st = time.time()
