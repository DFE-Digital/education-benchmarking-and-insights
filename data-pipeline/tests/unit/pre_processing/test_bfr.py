import numpy as np
import pandas as pd
import pytest

from pipeline.pre_processing.ancillary.bfr import build_bfr_historical_data
from pipeline.pre_processing.bfr import calculations

def test_bfr_metric_data_has_correct_output_columns(prepared_bfr_data: pd.DataFrame):
    assert list(prepared_bfr_data[1].columns) == [
        "Category",
        "Value",
        "Trust UPIN",
        "value",
    ]


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
