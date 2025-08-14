import uuid

import pandas as pd

from pipeline.utils import database


def test_unique_temp_table_names():
    random_run_id = str(uuid.uuid4())
    table_names = [
        "ComparatorSet",
        "MetricRAG",
        "School",
        "LocalAuthority",
        "Trust",
        "NonFinancial",
        "Financial",
        "TrustFinancial",
        "BudgetForecastReturnMetric",
        "BudgetForecastReturn",
    ]

    temp_tables = [
        temp_table
        for table in table_names
        for temp_table in [
            database._get_temp_table_name(table, "2021"),
            database._get_temp_table_name(table, random_run_id),
            database._get_temp_table_name(table, str(uuid.uuid4())),
        ]
    ]

    assert len(temp_tables) == len(set(temp_tables))


def test_unpivot_statistical_neighbour_column():
    df = pd.DataFrame(
        {
            "LaCode": ["100", "101", "102"],
            "1Suffix": ["a", "b", "c"],
            "2Suffix": ["c", "b", "a"],
        }
    )

    result = database._unpivot_statistical_neighbour_column(df, "Suffix", "NewColumn")

    pd.testing.assert_frame_equal(
        result,
        pd.DataFrame(
            {
                "LaCode": ["100", "101", "102", "100", "101", "102"],
                "NeighbourPosition": [1, 1, 1, 2, 2, 2],
                "NewColumn": ["a", "b", "c", "c", "b", "a"],
            }
        ),
    )
