from io import StringIO

import pytest

from src.pipeline.pre_processing import prepare_cdc_data


def test_prepare_cdc_data(cdc_data):
    prepared_cdc_data = prepare_cdc_data(StringIO(cdc_data.to_csv()), 2022)

    assert list(prepared_cdc_data.columns) == [
        "Total Internal Floor Area",
        "Age Average Score",
    ]


def test_cdc_has_correct_index(prepared_cdc_data: dict):
    assert prepared_cdc_data["URN"] == 100150


def test_cdc_has_correct_total_internal_floor_area(prepared_cdc_data: dict):
    assert prepared_cdc_data["Total Internal Floor Area"] == 2803.0


def test_cdc_has_correct_age_average_score(prepared_cdc_data: dict):
    assert pytest.approx(prepared_cdc_data["Age Average Score"], 0.001) == 48.358
