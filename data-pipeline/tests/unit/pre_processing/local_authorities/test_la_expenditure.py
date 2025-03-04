import io

import pandas as pd

from pipeline import input_schemas
from pipeline.pre_processing.ancillary import local_authority


def test_la_expenditure(la_expenditure: pd.DataFrame):
    """
    The pivoted data must result in reduced rows.
    """
    buffer = io.BytesIO()
    la_expenditure.to_csv(buffer)
    buffer.seek(0)
    year = 2024

    result = local_authority._prepare_la_section_251_data(
        buffer,
        year,
        usecols=input_schemas.la_expenditure.get(
            year, input_schemas.la_expenditure["default"]
        ).keys(),
        dtype=input_schemas.la_expenditure.get(
            year, input_schemas.la_expenditure["default"]
        ),
        category_column="category_of_planned_expenditure",
        column_mappings=input_schemas.la_expenditure_column_mappings.get(
            year,
            input_schemas.la_expenditure_column_mappings["default"],
        ),
        column_eval=input_schemas.la_expenditure_column_eval.get(
            year, input_schemas.la_expenditure_column_eval["default"]
        ),
        column_pivot=input_schemas.la_expenditure_pivot.get(
            year, input_schemas.la_expenditure_pivot["default"]
        ),
    )

    assert len(la_expenditure) == 13
    assert len(result.index) == 1


def test_la_expenditure_year(la_expenditure: pd.DataFrame):
    """
    Rows with a different `time_period` must be discarded.
    """
    la_expenditure_old = la_expenditure.copy()
    la_expenditure_old["time_period"] = "202122"
    combined = pd.concat([la_expenditure, la_expenditure_old])
    buffer = io.BytesIO()
    combined.to_csv(buffer)
    buffer.seek(0)
    year = 2024

    result = local_authority._prepare_la_section_251_data(
        buffer,
        year,
        usecols=input_schemas.la_expenditure.get(
            year, input_schemas.la_expenditure["default"]
        ).keys(),
        dtype=input_schemas.la_expenditure.get(
            year, input_schemas.la_expenditure["default"]
        ),
        category_column="category_of_planned_expenditure",
        column_mappings=input_schemas.la_expenditure_column_mappings.get(
            year,
            input_schemas.la_expenditure_column_mappings["default"],
        ),
        column_eval=input_schemas.la_expenditure_column_eval.get(
            year, input_schemas.la_expenditure_column_eval["default"]
        ),
        column_pivot=input_schemas.la_expenditure_pivot.get(
            year, input_schemas.la_expenditure_pivot["default"]
        ),
    )

    assert len(combined) == 26
    assert len(result.index) == 1
