from io import StringIO

import pytest

from src.pipeline.pre_processing import prepare_sen_data


def test_prepare_sen_data_has_correct_output_columns(sen_data):
    result = prepare_sen_data(StringIO(sen_data.to_csv()))

    assert list(result.columns) == [
        "Total pupils",
        "EHC plan",
        "Percentage SEN",
        "Primary Need SPLD",
        "Primary Need MLD",
        "Primary Need SLD",
        "Primary Need PMLD",
        "Primary Need SEMH",
        "Primary Need SLCN",
        "Primary Need HI",
        "Primary Need VI",
        "Primary Need MSI",
        "Primary Need PD",
        "Primary Need ASD",
        "Primary Need OTH",
        "Percentage Primary Need SPLD",
        "Percentage Primary Need MLD",
        "Percentage Primary Need SLD",
        "Percentage Primary Need PMLD",
        "Percentage Primary Need SEMH",
        "Percentage Primary Need SLCN",
        "Percentage Primary Need HI",
        "Percentage Primary Need VI",
        "Percentage Primary Need MSI",
        "Percentage Primary Need PD",
        "Percentage Primary Need ASD",
        "Percentage Primary Need OTH"
    ]


def test_sen_has_correct_index(prepared_sen_data: dict):
    assert prepared_sen_data["URN"] == 100150


def test_percentage_sen_computed_correctly(prepared_sen_data: dict):
    assert prepared_sen_data["Percentage SEN"] == 50


def test_primary_need_spld_computed_correctly(prepared_sen_data: dict):
    assert prepared_sen_data["Primary Need SPLD"] == 2


def test_primary_need_mld_computed_correctly(prepared_sen_data: dict):
    assert prepared_sen_data["Primary Need MLD"] == 3


def test_primary_need_sld_computed_correctly(prepared_sen_data: dict):
    assert prepared_sen_data["Primary Need SLD"] == 4


def test_primary_need_pmld_computed_correctly(prepared_sen_data: dict):
    assert prepared_sen_data["Primary Need PMLD"] == 5


def test_primary_need_semh_computed_correctly(prepared_sen_data: dict):
    assert prepared_sen_data["Primary Need SEMH"] == 6


def test_primary_need_slnc_computed_correctly(prepared_sen_data: dict):
    assert prepared_sen_data["Primary Need SLCN"] == 7


def test_primary_need_hi_computed_correctly(prepared_sen_data: dict):
    assert prepared_sen_data["Primary Need HI"] == 8


def test_primary_need_vi_computed_correctly(prepared_sen_data: dict):
    assert prepared_sen_data["Primary Need VI"] == 9


def test_primary_need_msi_computed_correctly(prepared_sen_data: dict):
    assert prepared_sen_data["Primary Need MSI"] == 10


def test_primary_need_pd_computed_correctly(prepared_sen_data: dict):
    assert prepared_sen_data["Primary Need PD"] == 11


def test_primary_need_asd_computed_correctly(prepared_sen_data: dict):
    assert prepared_sen_data["Primary Need ASD"] == 12


def test_primary_need_oth_computed_correctly(prepared_sen_data: dict):
    assert prepared_sen_data["Primary Need OTH"] == 13


def test_percentage_primary_need_spld_computed_correctly(prepared_sen_data: dict):
    assert prepared_sen_data["Percentage Primary Need SPLD"] == 2


def test_percentage_primary_need_mld_computed_correctly(prepared_sen_data: dict):
    assert prepared_sen_data["Percentage Primary Need MLD"] == 3


def test_percentage_primary_need_sld_computed_correctly(prepared_sen_data: dict):
    assert prepared_sen_data["Percentage Primary Need SLD"] == 4


def test_percentage_primary_need_pmld_computed_correctly(prepared_sen_data: dict):
    assert prepared_sen_data["Percentage Primary Need PMLD"] == 5


def test_percentage_primary_need_semh_computed_correctly(prepared_sen_data: dict):
    assert prepared_sen_data["Percentage Primary Need SEMH"] == 6


def test_percentage_primary_need_slnc_computed_correctly(prepared_sen_data: dict):
    assert pytest.approx(prepared_sen_data["Percentage Primary Need SLCN"], 0.001) == 7


def test_percentage_primary_need_hi_computed_correctly(prepared_sen_data: dict):
    assert prepared_sen_data["Percentage Primary Need HI"] == 8


def test_percentage_primary_need_vi_computed_correctly(prepared_sen_data: dict):
    assert prepared_sen_data["Percentage Primary Need VI"] == 9


def test_percentage_primary_need_msi_computed_correctly(prepared_sen_data: dict):
    assert prepared_sen_data["Percentage Primary Need MSI"] == 10


def test_percentage_primary_need_pd_computed_correctly(prepared_sen_data: dict):
    assert prepared_sen_data["Percentage Primary Need PD"] == 11


def test_percentage_primary_need_asd_computed_correctly(prepared_sen_data: dict):
    assert prepared_sen_data["Percentage Primary Need ASD"] == 12


def test_percentage_primary_need_oth_computed_correctly(prepared_sen_data: dict):
    assert prepared_sen_data["Percentage Primary Need OTH"] == 13


def test_sen_nan_has_correct_index(prepared_sen_data_with_nans: dict):
    assert prepared_sen_data_with_nans["URN"] == 100151


def test_sen_replaces_nan_with_zero(prepared_sen_data_with_nans: dict):
    assert prepared_sen_data_with_nans["Percentage Primary Need MLD"] == 0
