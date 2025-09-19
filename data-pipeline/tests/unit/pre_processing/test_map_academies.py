import numpy as np
import pandas as pd

from pipeline import config
from pipeline.pre_processing.aar import part_year


def test_map_is_early_transfer():
    df = pd.DataFrame(
        {
            "Date joined or opened if in period": [
                "01/01/1970",
                np.nan,
                "01/09/2202",
                "10/09/1963",
                "21/09/1976",
                np.nan,
            ]
        }
    )

    df = part_year.map_is_early_transfer(df)

    assert df["Is Early Transfer"].to_list() == [
        False,
        False,
        True,
        True,
        False,
        False,
    ]


def test_map_has_financial_data():
    financial_columns = list(
        (
            config.nonaggregated_cost_category_map["academies"]
            | config.nonaggregated_income_category_map["academies"]
        ).values()
    )
    df = pd.DataFrame(
        [
            {k: np.nan for k in financial_columns},
            {k: np.nan if i % 2 == 0 else 1.0 for i, k in enumerate(financial_columns)},
            {k: np.nan for k in financial_columns},
            {k: np.nan if i % 2 != 0 else 1.0 for i, k in enumerate(financial_columns)},
        ]
    )

    df = part_year.map_has_financial_data(df)

    assert df["Financial Data Present"].to_list() == [False, True, False, True]


def test_map_partial_year_present():
    df = pd.DataFrame(
        {
            "Period covered by return": [
                1,
                2,
                3,
                4,
                5,
                6,
                7,
                8,
                9,
                10,
                11,
                12,
            ]
        }
    )

    df = part_year.map_partial_year_present(df)

    assert df["Partial Years Present"].to_list() == [
        True,
        True,
        True,
        True,
        True,
        True,
        True,
        True,
        True,
        True,
        True,
        False,
    ]
