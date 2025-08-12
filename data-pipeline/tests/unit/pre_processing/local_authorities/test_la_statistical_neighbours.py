import io

import pytest

from pipeline.pre_processing.s251.local_authority import _prepare_la_statistical_neighbours


@pytest.fixture
def expected_columns():
    return [
        "SN1",
        "SN1Prox",
        "SN2",
        "SN2Prox",
        "SN3",
        "SN3Prox",
        "SN4",
        "SN4Prox",
        "SN5",
        "SN5Prox",
        "SN6",
        "SN6Prox",
        "SN7",
        "SN7Prox",
        "SN8",
        "SN8Prox",
        "SN9",
        "SN9Prox",
        "SN10",
        "SN10Prox",
        "SN1Code",
        "SN2Code",
        "SN3Code",
        "SN4Code",
        "SN5Code",
        "SN6Code",
        "SN7Code",
        "SN8Code",
        "SN9Code",
        "SN10Code",
        "GOInd",
        "GOReg",
    ]


@pytest.fixture
def expected_la_1_values():
    return [
        "LA 2",
        "Close",
        "LA 3",
        "Close",
        "LA 4",
        "Somewhat close",
        "LA 5",
        "Somewhat close",
        "LA 6",
        "Not Close",
        "LA 7",
        "Not Close",
        "LA 8",
        "Not Close",
        "LA 9",
        "Not Close",
        "LA 10",
        "Not Close",
        "LA 11",
        "Not Close",
        102,
        103,
        104,
        105,
        106,
        107,
        108,
        109,
        110,
        111,
        3,
        "London (Inner)",
    ]


@pytest.fixture
def expected_la_2_values():
    return [
        "LA 3",
        "Close",
        "LA 4",
        "Somewhat close",
        "LA 5",
        "Somewhat close",
        "LA 6",
        "Not Close",
        "LA 7",
        "Not Close",
        "LA 8",
        "Not Close",
        "LA 9",
        "Not Close",
        "LA 10",
        "Not Close",
        "LA 11",
        "Not Close",
        "LA 12",
        "Not Close",
        103,
        104,
        105,
        106,
        107,
        108,
        109,
        110,
        111,
        112,
        8,
        "West Midlands",
    ]


def test_prepare_la_statistical_neighbours(
    la_statistical_neighbours: io.StringIO, expected_columns: list
):
    """
    contains correct column names in output
    (column names ingested contain duplicates ensure these are mapped correctly)
    """
    result = _prepare_la_statistical_neighbours(la_statistical_neighbours, 2022)
    assert list(result.columns) == expected_columns


def test_prepare_la_statistical_neighbours_has_correct_index(
    la_statistical_neighbours: io.StringIO,
):
    """
    contains correct index
    """
    result = _prepare_la_statistical_neighbours(la_statistical_neighbours, 2022)
    assert result.index.name == "LA number"


def test_prepare_la_statistical_neighbours_has_correct_values(
    la_statistical_neighbours: io.StringIO,
    expected_columns: list,
    expected_la_1_values: list,
    expected_la_2_values: list,
):
    """
    contains correct values in output
    """
    result = _prepare_la_statistical_neighbours(la_statistical_neighbours, 2022)
    assert result.loc[101, expected_columns].tolist() == expected_la_1_values
    assert result.loc[102, expected_columns].tolist() == expected_la_2_values
