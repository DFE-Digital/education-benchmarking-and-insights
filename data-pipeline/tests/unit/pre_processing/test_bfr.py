import pandas as pd
import pytest


def test_bfr_metric_data_has_correct_output_columns(prepared_bfr_data: pd.DataFrame):
    assert list(prepared_bfr_data[0].columns) == [
        "Revenue reserve as percentage of income",
        "Staff costs as percentage of income",
        "Expenditure as percentage of income",
        "percent self-generated income",
        "percent grant funding",
        "revenue_reserves_year_-2",
        "revenue_reserves_year_-1",
        "revenue_reserves_year_0",
        "revenue_reserves_year_1",
        "revenue_reserves_year_2",
        "revenue_reserves_slope",
        "revenue_reserves_slope_flag",
        "revenue_reserves_year_-2_per_pupil",
        "revenue_reserves_year_-1_per_pupil",
        "revenue_reserves_year_0_per_pupil",
        "revenue_reserves_year_1_per_pupil",
        "revenue_reserves_year_2_per_pupil",
        "revenue_reserves_per_pupil_slope",
        "revenue_reserves_per_pupil_slope_flag",
    ]


def test_bfr_output_data_has_correct_output_columns(
    prepared_bfr_data: pd.DataFrame,
):
    assert list(prepared_bfr_data[1].columns) == [
        "TrustUPIN",
        "CreatedBy",
        "Category",
        "Title",
        "EFALineNo",
        "Y1P1",
        "Y1P2",
        "Y2P1",
        "Y2P2",
        "Y1",
        "Y2",
        "Y3",
        "Y4",
        "Trust Balance",
        "volatility",
        "volatility_status",
    ]
