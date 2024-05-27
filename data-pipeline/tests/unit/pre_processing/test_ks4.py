import pandas as pd
import pytest


def test_prepare_ks4_data_has_correct_output_columns(prepared_ks4_data: pd.DataFrame):
    assert list(prepared_ks4_data.columns) == [
        "AverageAttainment",
        "Progress8Measure",
        "Progress8Banding"
    ]


def test_ks4_computes_average_attainment(prepared_ks4_data: pd.DataFrame):
    assert pytest.approx(prepared_ks4_data["AverageAttainment"].iloc[0], 0.001) == 0.1


def test_ks4_progress_eight_measure(prepared_ks4_data: pd.DataFrame):
    assert pytest.approx(prepared_ks4_data["Progress8Measure"].iloc[0], 0.001) == 0.1


def test_ks4_progress_eight_banding(prepared_ks4_data: pd.DataFrame):
    assert pytest.approx(prepared_ks4_data["Progress8Banding"].iloc[0], 0.001) == 0.1


def test_ks4_replaces_average_attainment_supp_with_zero(prepared_ks4_data: pd.DataFrame):
    assert pytest.approx(prepared_ks4_data["AverageAttainment"].iloc[1], 0.001) == 0


def test_ks4_replaces_average_attainment_ne_with_zero(prepared_ks4_data: pd.DataFrame):
    assert pytest.approx(prepared_ks4_data["AverageAttainment"].iloc[2], 0.001) == 0


