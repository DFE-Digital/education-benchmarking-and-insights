import io

import pandas as pd

from pipeline import input_schemas
from pipeline.pre_processing.ancillary import local_authority


def test_la_budget(la_budget: pd.DataFrame):
    """
    The pivoted data must result in reduced rows.
    """
    year = 2024

    result = local_authority._prepare_la_section_251_data(
        io.StringIO(la_budget.to_csv()),
        year,
        usecols=input_schemas.la_budget.get(
            year, input_schemas.la_budget["default"]
        ).keys(),
        dtype=input_schemas.la_budget.get(year, input_schemas.la_budget["default"]),
        category_column="category_of_planned_expenditure",
        column_mappings=input_schemas.la_budget_column_mappings.get(
            year,
            input_schemas.la_budget_column_mappings["default"],
        ),
        column_eval=input_schemas.la_budget_column_eval.get(
            year, input_schemas.la_budget_column_eval["default"]
        ),
        column_pivot=input_schemas.la_budget_pivot.get(
            year, input_schemas.la_budget_pivot["default"]
        ),
    )

    assert len(la_budget) == 13
    assert len(result.index) == 1


def test_la_budget_year(la_budget: pd.DataFrame):
    """
    Rows with a different `time_period` must be discarded.
    """
    la_expenditure_old = la_budget.copy()
    la_expenditure_old["time_period"] = "202122"
    combined = pd.concat([la_budget, la_expenditure_old])
    year = 2024

    result = local_authority._prepare_la_section_251_data(
        io.StringIO(combined.to_csv()),
        year,
        usecols=input_schemas.la_budget.get(
            year, input_schemas.la_budget["default"]
        ).keys(),
        dtype=input_schemas.la_budget.get(year, input_schemas.la_budget["default"]),
        category_column="category_of_planned_expenditure",
        column_mappings=input_schemas.la_budget_column_mappings.get(
            year,
            input_schemas.la_budget_column_mappings["default"],
        ),
        column_eval=input_schemas.la_budget_column_eval.get(
            year, input_schemas.la_budget_column_eval["default"]
        ),
        column_pivot=input_schemas.la_budget_pivot.get(
            year, input_schemas.la_budget_pivot["default"]
        ),
    )

    assert len(combined) == 26
    assert len(result.index) == 1
