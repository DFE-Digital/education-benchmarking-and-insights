import numpy as np
import pandas as pd

from src.pipeline import config, part_year


def test_map_is_day_one_return():
    df = pd.DataFrame(
        {
            "ACADEMYTRUSTSTATUS": [
                "Existing",
                "Existing",
                "1 day",
                "Closed",
                "Existing",
            ],
        }
    )

    df = part_year.academies.map_is_day_one_return(df)

    assert df["Is Day One Return"].to_list() == [False, False, True, False, False]


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

    df = part_year.academies.map_is_early_transfer(df)

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
            config.cost_category_map["academies"]
            | config.income_category_map["academies"]
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

    df = part_year.academies.map_has_financial_data(df)

    assert df["Financial Data Present"].to_list() == [False, True, False, True]


def test_map_partial_year_present():
    df = pd.DataFrame(
        {
            "Academy Status": [
                "Member for whole period",
                "Member for whole period",
                "in period transfer",
                "Member for whole period",
                "in period transfer",
            ]
        }
    )

    df = part_year.academies.map_partial_year_present(df)

    assert df["Partial Years Present"].to_list() == [False, False, True, False, True]
