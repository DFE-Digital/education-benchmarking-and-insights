import uuid

from pipeline import database


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
