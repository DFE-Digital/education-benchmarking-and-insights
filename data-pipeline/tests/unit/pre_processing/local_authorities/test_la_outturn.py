import io

import pandas as pd

from pipeline import input_schemas
from pipeline.pre_processing.ancillary import local_authority


def test_la_outturn(la_outturn: pd.DataFrame):
    """
    The pivoted data must result in reduced rows.
    """
    year = 2024

    result = local_authority._prepare_la_section_251_data(
        io.StringIO(la_outturn.to_csv(encoding="cp1252")),
        year,
        usecols=input_schemas.la_outturn.get(
            year, input_schemas.la_outturn["default"]
        ).keys(),
        dtype=input_schemas.la_outturn.get(year, input_schemas.la_outturn["default"]),
        category_column="category_of_expenditure",
        column_mappings=input_schemas.la_outturn_column_mappings.get(
            year,
            input_schemas.la_outturn_column_mappings["default"],
        ),
        column_eval=input_schemas.la_outturn_column_eval.get(
            year, input_schemas.la_outturn_column_eval["default"]
        ),
        column_pivot=input_schemas.la_outturn_pivot.get(
            year, input_schemas.la_outturn_pivot["default"]
        ),
        encoding="cp1252",
    )

    assert len(la_outturn) == 13
    assert len(result.index) == 1


def test_la_outturn_year(la_outturn: pd.DataFrame):
    """
    Rows with a different `time_period` must be discarded.
    """
    la_outturn_old = la_outturn.copy()
    la_outturn_old["time_period"] = "202122"
    combined = pd.concat([la_outturn, la_outturn_old])
    year = 2024

    result = local_authority._prepare_la_section_251_data(
        io.StringIO(combined.to_csv(encoding="cp1252")),
        year,
        usecols=input_schemas.la_outturn.get(
            year, input_schemas.la_outturn["default"]
        ).keys(),
        dtype=input_schemas.la_outturn.get(year, input_schemas.la_outturn["default"]),
        category_column="category_of_expenditure",
        column_mappings=input_schemas.la_outturn_column_mappings.get(
            year,
            input_schemas.la_outturn_column_mappings["default"],
        ),
        column_eval=input_schemas.la_outturn_column_eval.get(
            year, input_schemas.la_outturn_column_eval["default"]
        ),
        column_pivot=input_schemas.la_outturn_pivot.get(
            year, input_schemas.la_outturn_pivot["default"]
        ),
        encoding="cp1252",
    )

    assert len(combined) == 26
    assert len(result.index) == 1
