import numpy as np
import pandas as pd
import pytest

from pipeline.pre_processing.bfr import calculations
from pipeline.pre_processing.bfr.trusts import build_bfr_historical_data


def test_bfr_metric_data_has_correct_output_columns(prepared_bfr_data: pd.DataFrame):
    assert list(prepared_bfr_data[1].columns) == [
        "Category",
        "Value",
        "Trust UPIN",
    ]


def test_bfr_slope_and_slope_flag_resolve_correctly(prepared_bfr_data: pd.DataFrame):
    # 1. BudgetForecastReturn table data (prepared_bfr_data[0])
    bfr_df = prepared_bfr_data[0]
    assert "Slope" not in bfr_df["Category"].values
    assert "Slope flag" not in bfr_df["Category"].values

    # 2. BudgetForecastReturnMetric table data (prepared_bfr_data[1])
    bfr_metrics_df = prepared_bfr_data[1]

    slope_rows = bfr_metrics_df[bfr_metrics_df["Category"] == "Slope"]
    slope_flag_rows = bfr_metrics_df[bfr_metrics_df["Category"] == "Slope flag"]

    assert not slope_rows.empty, "Slope category should be present in BFR metrics"
    assert not slope_flag_rows.empty, "Slope flag category should be present in BFR metrics"

    # Assert that the "Value" (uppercase) column resolves to correct non-null float values for these rows
    # (Since "Value" is the projected database column for metric values, mapping it correctly
    # ensures it writes to the database as correct non-null numbers).
    assert not slope_rows["Value"].isna().any(), "Slope value should not be null/NaN"
    assert not slope_flag_rows["Value"].isna().any(), "Slope flag value should not be null/NaN"

    # Check exact calculated values in the test fixture dataset
    assert slope_rows["Value"].iloc[0] == pytest.approx(41560.0)
    assert slope_flag_rows["Value"].iloc[0] == pytest.approx(0.0)


def test_bfr_output_data_has_correct_output_columns(
    prepared_bfr_data: pd.DataFrame,
):
    assert list(prepared_bfr_data[0].columns) == ["Category", "Year", "Value", "Pupils"]


def test_bfr_slop_calc():
    actual = calculations.calculate_slopes(np.array([[2, 4, 6, 8, 10, 12]]))
    assert [2] == actual


@pytest.mark.parametrize(
    "bfr_sofa",
    [
        (None,),
        (pd.DataFrame(),),
    ],
)
def test_historical_bfr_academy_none(bfr_sofa):
    result = build_bfr_historical_data(
        academies_historical=None,
        bfr_sofa_historical=bfr_sofa,
    )

    assert result is None


def test_historical_bfr_sofa_none():
    academies = pd.DataFrame(
        [
            {
                "Trust UPIN": "0",
                "Company Registration Number": "0",
            }
        ]
    )
    result = build_bfr_historical_data(
        academies_historical=academies,
        bfr_sofa_historical=None,
    )

    assert result is not None
    assert "Trust Revenue reserve" in result.columns
    assert list(result["Trust Revenue reserve"]) == [0.0]


def test_historical_bfr():
    academies = pd.DataFrame(
        [
            {
                "Trust UPIN": "0",
                "Company Registration Number": "0",
            }
        ]
    )
    bfr_sofa = pd.DataFrame(
        [
            {
                "Trust UPIN": "0",
                "EFALineNo": 430,
                "Y1P2": 2_048.0,
                "Y2P2": 1_024.0,
            }
        ]
    )
    result = build_bfr_historical_data(
        academies_historical=academies,
        bfr_sofa_historical=bfr_sofa,
    )

    assert result is not None
    assert "Trust Revenue reserve" in result.columns
    assert list(result["Trust Revenue reserve"]) == [1_024_000.0]
