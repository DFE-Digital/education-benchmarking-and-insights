from io import StringIO

import pytest

from src.pipeline.pre_processing import prepare_cdc_data


def test_prepare_cdc_data_has_correct_output_columns(cdc_data):
    result = prepare_cdc_data(StringIO(cdc_data.to_csv()), 2022)

    assert list(result.columns) == [
        "Total Internal Floor Area",
        "Age Average Score",
        "Building Age",
    ]


def test_cdc_has_correct_building_age(prepared_cdc_data: dict):
    assert pytest.approx(prepared_cdc_data["Building Age"], 0.5) == 1988


def test_cdc_has_correct_total_internal_floor_area(prepared_cdc_data: dict):
    assert prepared_cdc_data["Total Internal Floor Area"] == 2803.0


def test_cdc_has_correct_age_average_score(prepared_cdc_data: dict):
    assert pytest.approx(prepared_cdc_data["Age Average Score"], 0.001) == 48.358


def test_cdc_has_correct_index(prepared_cdc_data: dict):
    assert prepared_cdc_data["URN"] == 100150
