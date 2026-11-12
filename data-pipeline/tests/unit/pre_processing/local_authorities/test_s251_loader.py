import io

import pandas as pd

from pipeline import input_schemas
from pipeline.pre_processing.s251.loader import load_s251_budget, load_s251_outturn

from .conftest import write_budget_2026_ods_bytes, write_outturn_2026_xlsx_bytes


def test_load_s251_budget_csv_default_year(la_budget: pd.DataFrame):
    buffer = io.BytesIO()
    la_budget.to_csv(buffer, index=False)
    buffer.seek(0)

    result = load_s251_budget(buffer, year=2024)

    assert list(result.columns) == list(input_schemas.la_budget["default"].keys())
    assert str(result["old_la_code"].dtype) == "Int64"
    assert (result["time_period"] == "202324").all()


def test_load_s251_budget_2026_ods_normalises_columns_and_year_digits(
    la_budget_2026: pd.DataFrame,
):
    raw = la_budget_2026.copy()
    raw["Early years"] = raw["Early years"].astype(object)
    raw.loc[0, "Early years"] = "z"
    buffer = write_budget_2026_ods_bytes(raw)

    result = load_s251_budget(buffer, year=2026)

    assert "category_of_planned_expenditure" in result.columns
    assert "old_la_code" in result.columns
    assert "Section 251 line" not in result.columns
    assert str(result["old_la_code"].dtype) == "Int64"
    assert (result["time_period"] == "202526").all()
    assert pd.isna(result.loc[0, "early_years_establishments"])


def test_load_s251_outturn_csv_cp1252_default_year(la_outturn: pd.DataFrame):
    buffer = io.BytesIO()
    la_outturn.to_csv(buffer, index=False, encoding="cp1252")
    buffer.seek(0)

    result = load_s251_outturn(buffer, year=2024)

    assert list(result.columns) == list(input_schemas.la_outturn["default"].keys())
    assert str(result["old_la_code"].dtype) == "Int64"
    assert (result["time_period"] == "202324").all()


def test_load_s251_outturn_2026_xlsx_normalises_columns(
    la_outturn_2026: pd.DataFrame,
):
    raw = la_outturn_2026.copy()
    raw["EarlyYears"] = raw["EarlyYears"].astype(object)
    raw.loc[0, "EarlyYears"] = "c"
    buffer = write_outturn_2026_xlsx_bytes(raw)

    result = load_s251_outturn(buffer, year=2026)

    assert "category_of_expenditure" in result.columns
    assert "S251Line" not in result.columns
    assert str(result["old_la_code"].dtype) == "Int64"
    assert (result["time_period"] == "202526").all()
    assert pd.isna(result.loc[0, "early_years_establishments"])
