import math

import pandas as pd

base_cols = [
    "URN",
    "Total Internal Floor Area",
    "Age Average Score",
    "OfstedRating (name)",
    "Percentage SEN",
    "Percentage Free school meals",
    "Number of pupils",
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
    "Non-educational support staff": {
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
    "IT": {
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
    "Premises": {
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


def map_rag(d, ofstead, rag_mapping):
    close_count = d["is_close"][d["is_close"]].count()
    key = "outstanding" if ofstead.lower() == "outstanding" else "other"
    key += "_10" if close_count > 10 else ""
    d["rag"] = d["decile"].fillna(0).map(lambda x: rag_mapping[key][int(x)])
    return d


def compute_rag(urn, comparator_set):
    result = {}
    for category in category_settings:
        deciles = category_settings[category]
        cols = comparator_set.columns.isin(
            base_cols
        ) | comparator_set.columns.str.startswith(category)

        df = comparator_set[comparator_set.columns[cols]].copy()
        target = comparator_set[comparator_set.index == urn][
            comparator_set.columns[cols]
        ].copy()

        if deciles["type"] == "area":
            df["Total"] = df[df.columns[6 : df.shape[1]]].sum(axis=1) / (
                df["Total Internal Floor Area"]
            )
        else:
            df["Total"] = df[df.columns[6 : df.shape[1]]].sum(axis=1) / (
                df["Number of pupils"]
            )

        df["is_close"] = df.apply(
            lambda x: is_close_comparator(deciles["type"], target, x), axis=1
        )
        df["decile"] = pd.qcut(df["Total"], 10, labels=False, duplicates="drop")

        result[category] = map_rag(
            df, target["OfstedRating (name)"].values[0], deciles
        ).sort_values(by="decile", ascending=True)
    return result
