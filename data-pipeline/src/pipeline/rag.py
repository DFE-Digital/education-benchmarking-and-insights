import math
import numpy as np
import pandas as pd

base_cols = [
    "URN",
    "Total Internal Floor Area",
    "Age Average Score",
    "OfstedRating (name)",
    "Percentage SEN",
    "Percentage Free school meals",
    "Number of pupils"
]

category_settings = {
    "Teaching and Teaching support staff": {
        "type": "pupil",
        "outstanding_10": [
            "amber",
            "amber",
            "amber",
            "green",
            "green",
            "green",
            "amber",
            "amber",
            "red",
            "red",
        ],
        "outstanding": [
            "amber",
            "amber",
            "amber",
            "green",
            "green",
            "green",
            "amber",
            "amber",
            "amber",
            "red",
        ],
        "other_10": [
            "red",
            "red",
            "amber",
            "green",
            "green",
            "green",
            "amber",
            "amber",
            "red",
            "red",
        ],
        "other": [
            "red",
            "amber",
            "amber",
            "green",
            "green",
            "green",
            "amber",
            "amber",
            "amber",
            "red",
        ],
    },
    "Non-educational support staff and services": {
        "type": "pupil",
        "outstanding_10": [
            "green",
            "green",
            "green",
            "amber",
            "amber",
            "amber",
            "amber",
            "amber",
            "red",
            "red",
        ],
        "outstanding": [
            "green",
            "green",
            "green",
            "amber",
            "amber",
            "amber",
            "amber",
            "amber",
            "red",
            "red",
        ],
        "other_10": [
            "green",
            "green",
            "green",
            "amber",
            "amber",
            "amber",
            "amber",
            "amber",
            "red",
            "red",
        ],
        "other": [
            "green",
            "green",
            "green",
            "amber",
            "amber",
            "amber",
            "amber",
            "amber",
            "red",
            "red",
        ],
    },
    "Educational supplies": {
        "type": "pupil",
        "outstanding_10": [
            "amber",
            "amber",
            "amber",
            "green",
            "green",
            "green",
            "amber",
            "amber",
            "red",
            "red",
        ],
        "outstanding": [
            "amber",
            "amber",
            "amber",
            "green",
            "green",
            "green",
            "amber",
            "amber",
            "amber",
            "red",
        ],
        "other_10": [
            "red",
            "red",
            "amber",
            "green",
            "green",
            "green",
            "amber",
            "amber",
            "red",
            "red",
        ],
        "other": [
            "red",
            "amber",
            "amber",
            "green",
            "green",
            "green",
            "amber",
            "amber",
            "amber",
            "red",
        ],
    },
    "Educational ICT": {
        "type": "pupil",
        "outstanding_10": [
            "amber",
            "amber",
            "amber",
            "green",
            "green",
            "green",
            "amber",
            "amber",
            "red",
            "red",
        ],
        "outstanding": [
            "amber",
            "amber",
            "amber",
            "green",
            "green",
            "green",
            "amber",
            "amber",
            "amber",
            "red",
        ],
        "other_10": [
            "red",
            "red",
            "amber",
            "green",
            "green",
            "green",
            "amber",
            "amber",
            "red",
            "red",
        ],
        "other": [
            "red",
            "amber",
            "amber",
            "green",
            "green",
            "green",
            "amber",
            "amber",
            "amber",
            "red",
        ],
    },
    "Premises staff and services": {
        "type": "area",
        "outstanding_10": [
            "green",
            "green",
            "green",
            "amber",
            "amber",
            "amber",
            "amber",
            "amber",
            "red",
            "red",
        ],
        "outstanding": [
            "green",
            "green",
            "green",
            "amber",
            "amber",
            "amber",
            "amber",
            "amber",
            "red",
            "red",
        ],
        "other_10": [
            "green",
            "green",
            "green",
            "amber",
            "amber",
            "amber",
            "amber",
            "amber",
            "red",
            "red",
        ],
        "other": [
            "green",
            "green",
            "green",
            "amber",
            "amber",
            "amber",
            "amber",
            "amber",
            "red",
            "red",
        ],
    },
    "Utilities": {
        "type": "area",
        "outstanding_10": [
            "green",
            "green",
            "green",
            "amber",
            "amber",
            "amber",
            "amber",
            "amber",
            "red",
            "red",
        ],
        "outstanding": [
            "green",
            "green",
            "green",
            "amber",
            "amber",
            "amber",
            "amber",
            "amber",
            "red",
            "red",
        ],
        "other_10": [
            "green",
            "green",
            "green",
            "amber",
            "amber",
            "amber",
            "amber",
            "amber",
            "red",
            "red",
        ],
        "other": [
            "green",
            "green",
            "green",
            "amber",
            "amber",
            "amber",
            "amber",
            "amber",
            "red",
            "red",
        ],
    },
    "Administrative supplies": {
        "type": "pupil",
        "outstanding_10": [
            "green",
            "green",
            "green",
            "amber",
            "amber",
            "amber",
            "amber",
            "amber",
            "red",
            "red",
        ],
        "outstanding": [
            "green",
            "green",
            "green",
            "amber",
            "amber",
            "amber",
            "amber",
            "amber",
            "red",
            "red",
        ],
        "other_10": [
            "green",
            "green",
            "green",
            "amber",
            "amber",
            "amber",
            "amber",
            "amber",
            "red",
            "red",
        ],
        "other": [
            "green",
            "green",
            "green",
            "amber",
            "amber",
            "amber",
            "amber",
            "amber",
            "red",
            "red",
        ],
    },
    "Catering staff and supplies": {
        "type": "pupil",
        "outstanding_10": [
            "green",
            "green",
            "green",
            "amber",
            "amber",
            "amber",
            "amber",
            "amber",
            "red",
            "red",
        ],
        "outstanding": [
            "green",
            "green",
            "green",
            "amber",
            "amber",
            "amber",
            "amber",
            "amber",
            "red",
            "red",
        ],
        "other_10": [
            "green",
            "green",
            "green",
            "amber",
            "amber",
            "amber",
            "amber",
            "amber",
            "red",
            "red",
        ],
        "other": [
            "green",
            "green",
            "green",
            "amber",
            "amber",
            "amber",
            "amber",
            "amber",
            "red",
            "red",
        ],
    },
    "Other costs": {
        "type": "pupil",
        "outstanding_10": [
            "green",
            "green",
            "green",
            "amber",
            "amber",
            "amber",
            "amber",
            "amber",
            "red",
            "red",
        ],
        "outstanding": [
            "green",
            "green",
            "green",
            "amber",
            "amber",
            "amber",
            "amber",
            "amber",
            "red",
            "red",
        ],
        "other_10": [
            "green",
            "green",
            "green",
            "amber",
            "amber",
            "amber",
            "amber",
            "amber",
            "red",
            "red",
        ],
        "other": [
            "green",
            "green",
            "green",
            "amber",
            "amber",
            "amber",
            "amber",
            "amber",
            "red",
            "red",
        ],
    },
}


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


def get_category_series(category_name, data, basis):
    category_cols = data.columns.isin(base_cols) | data.columns.isin(["is_close"]) | data.columns.str.startswith(
        category_name)
    df = data[data.columns[category_cols]].copy()
    basis_data = data['Number of pupils' if basis == "pupil" else "Total Internal Floor Area"]

    # Create total column and divide be the basis data
    df[category_name + '_Total'] = df[df.columns[pd.Series(df.columns).str.startswith(category_name)]].sum(
        axis=1) / basis_data

    sub_categories = df.columns[df.columns.str.startswith(category_name)].values.tolist()

    for sub_category in sub_categories:
        df[sub_category] = df[sub_category] / basis_data

    return df, sub_categories


def category_stats(category_name, data, ofsted_rating, rag_mapping):
    close_count = data["is_close"][data["is_close"]].count()
    key = "outstanding" if ofsted_rating.lower() == "outstanding" else "other"
    key += "_10" if close_count > 10 else ""

    # TODO: This shouldn't be required
    with np.errstate(invalid="ignore"):
        series = data[category_name]
        percentiles = pd.qcut(series, 100, labels=False, duplicates="drop")
        deciles = pd.qcut(series, 10, labels=False, duplicates="drop")
        percentile = int(np.nan_to_num(percentiles.iloc[0]))
        decile = int(np.nan_to_num(deciles.iloc[0]))
        value = float(np.nan_to_num(series.iloc[0]))
        mean = float(np.nan_to_num(series.median()))
        diff = value - mean
        diff_percent = (diff / value) * 100 if value != 0 else 0

        return {
            'value': value,
            'mean': mean,
            'diff_mean': diff,
            'key': key,
            'percentage_diff': diff_percent,
            'percentile': percentile,
            'decile': decile,
            'rag': rag_mapping[key][int(decile)],
            'data': data.reset_index().to_dict(orient='records', index=True)
        }


def compute_category_rag(category_name, settings, comparator_set, stats):
    target = comparator_set.iloc[0]
    ofstead = target["OfstedRating (name)"]
    comparator_set["is_close"] = comparator_set.apply(
        lambda x: is_close_comparator(settings["type"], target, x), axis=1
    )

    series, sub_categories = get_category_series(category_name, comparator_set, settings['type'])

    for sub_category in sub_categories:
        stats[sub_category] = category_stats(sub_category, series, ofstead, settings)

    return stats


def compute_comparator_set_rag(comparator_set):
    stats = {}
    for cat in category_settings.keys():
        settings = category_settings[cat]
        stats = compute_category_rag(cat, settings, comparator_set, stats)

    return stats
