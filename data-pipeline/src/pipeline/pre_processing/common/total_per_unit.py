import numpy as np
import pandas as pd

from pipeline import config


def _category_total_per_unit_costs(
    df: pd.DataFrame,
    category_name: str,
    basis_data: pd.DataFrame,
) -> pd.DataFrame:
    """
    Recalculate total and per-unit costs for a single category.

    :param df: existing, pre-processed data
    :return: updated data
    """
    df[category_name + "_Total"] = (
        df[
            [
                c
                for c in df.columns
                if c.startswith(category_name) and not c.endswith("_CS")
            ]
        ]
        .fillna(0)
        .sum(axis=1)
    )

    sub_categories = [
        c for c in df.columns if c.startswith(category_name) and not c.endswith("_CS")
    ]

    for sub_category in sub_categories:
        df[sub_category + "_Per Unit"] = df[sub_category].fillna(0) / basis_data
        df[sub_category + "_Per Unit"] = df[sub_category + "_Per Unit"].replace(
            [np.inf, -np.inf, np.nan], 0
        )

    return df


def calculate_total_per_unit_costs(df: pd.DataFrame) -> pd.DataFrame:
    """
    Recalculate total and per-unit costs.

    Existing total and per-unit columns are removed before
    recalculating.

    :param df: existing, pre-processed data
    :return: recalculated data
    """
    target_data = df.copy(deep=True)

    total_per_unit_columns = [
        column
        for column in target_data.columns
        for category in config.rag_category_settings.keys()
        if column.startswith(category) and column.endswith(("_Per Unit", "_Total"))
    ]
    catering_net_costs = target_data["Catering staff and supplies_Net Costs"].copy()
    target_data.drop(
        columns=total_per_unit_columns + ["Catering staff and supplies_Net Costs"],
        inplace=True,
    )

    for category, settings in config.rag_category_settings.items():
        basis_data = target_data[
            (
                "Number of pupils"
                if settings["type"] == "Pupil"
                else "Total Internal Floor Area"
            )
        ]
        target_data = _category_total_per_unit_costs(target_data, category, basis_data)

    target_data["Catering staff and supplies_Net Costs"] = catering_net_costs
    target_data = target_data[df.columns]

    return target_data
