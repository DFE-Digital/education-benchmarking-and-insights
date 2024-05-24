import pandas as pd
import pytest


def test_prepare_ks2_data_has_correct_output_columns(prepared_ks2_data: pd.DataFrame):
    assert list(prepared_ks2_data.columns) == [
        "Ks2Progress"
    ]


def test_ks2_computes_ks2_progress(prepared_ks2_data: pd.DataFrame):
    assert pytest.approx(prepared_ks2_data["Ks2Progress"].iloc[0], 0.001) == 0.3


def test_ks2_replaces_supp_with_zero(prepared_ks2_data: pd.DataFrame):
    assert pytest.approx(prepared_ks2_data["Ks2Progress"].iloc[1], 0.001) == 0


def test_ks2_replaces_lowcov_with_zero(prepared_ks2_data: pd.DataFrame):
    assert pytest.approx(prepared_ks2_data["Ks2Progress"].iloc[1], 0.001) == 0
