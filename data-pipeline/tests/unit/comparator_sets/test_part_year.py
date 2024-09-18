import pandas as pd
import pytest

from src.pipeline import comparator_sets


@pytest.mark.parametrize(
    "series",
    [
        pd.Series({"Is Day One Return": True, "Is Early Transfer": False}),
        pd.Series({"Is Day One Return": False, "Is Early Transfer": True}),
        pd.Series({"Is Day One Return": True, "Is Early Transfer": True}),
    ],
)
def test_academy_map_pupil_comparator_set_early(series: pd.Series):
    result = comparator_sets._map_pupil_comparator_set(series)

    assert result is True


@pytest.mark.parametrize(
    "series",
    [
        pd.Series({"Is Day One Return": True, "Is Early Transfer": False}),
        pd.Series({"Is Day One Return": False, "Is Early Transfer": True}),
        pd.Series({"Is Day One Return": True, "Is Early Transfer": True}),
    ],
)
def test_academy_map_building_comparator_set_early(series: pd.Series):
    result = comparator_sets._map_building_comparator_set(series)

    assert result is True


@pytest.mark.parametrize(
    "series",
    [
        pd.Series({"Partial Years Present": False}),
        pd.Series(
            {
                "Is Day One Return": False,
                "Is Early Transfer": False,
                "Partial Years Present": False,
            }
        ),
    ],
)
def test_map_pupil_comparator_set_non_part_year(series: pd.Series):
    result = comparator_sets._map_pupil_comparator_set(series)

    assert result is True


@pytest.mark.parametrize(
    "series",
    [
        pd.Series({"Partial Years Present": False}),
        pd.Series(
            {
                "Is Day One Return": False,
                "Is Early Transfer": False,
                "Partial Years Present": False,
            }
        ),
    ],
)
def test_map_building_comparator_set_non_part_year(series: pd.Series):
    result = comparator_sets._map_building_comparator_set(series)

    assert result is True


@pytest.mark.parametrize(
    "series,expected",
    [
        (
            pd.Series(
                {
                    "Partial Years Present": True,
                    "Financial Data Present": True,
                    "Pupil Comparator Data Present": True,
                }
            ),
            True,
        ),
        (
            pd.Series(
                {
                    "Partial Years Present": True,
                    "Financial Data Present": False,
                    "Pupil Comparator Data Present": True,
                }
            ),
            False,
        ),
        (
            pd.Series(
                {
                    "Partial Years Present": True,
                    "Financial Data Present": True,
                    "Pupil Comparator Data Present": False,
                }
            ),
            False,
        ),
    ],
)
def test_map_pupil_comparator_set(series: pd.Series, expected: bool):
    result = comparator_sets._map_pupil_comparator_set(series)

    assert result is expected


@pytest.mark.parametrize(
    "series,expected",
    [
        (
            pd.Series(
                {
                    "Partial Years Present": True,
                    "Financial Data Present": True,
                    "Pupil Comparator Data Present": True,
                    "Building Comparator Data Present": True,
                }
            ),
            True,
        ),
        (
            pd.Series(
                {
                    "Partial Years Present": True,
                    "Financial Data Present": False,
                    "Pupil Comparator Data Present": True,
                    "Building Comparator Data Present": False,
                }
            ),
            False,
        ),
        (
            pd.Series(
                {
                    "Partial Years Present": True,
                    "Financial Data Present": True,
                    "Pupil Comparator Data Present": False,
                    "Building Comparator Data Present": False,
                }
            ),
            False,
        ),
        (
            pd.Series(
                {
                    "Partial Years Present": True,
                    "Financial Data Present": False,
                    "Pupil Comparator Data Present": False,
                    "Building Comparator Data Present": True,
                }
            ),
            False,
        ),
        (
            pd.Series(
                {
                    "Partial Years Present": True,
                    "Financial Data Present": False,
                    "Pupil Comparator Data Present": False,
                    "Building Comparator Data Present": False,
                }
            ),
            False,
        ),
    ],
)
def test_map_building_comparator_set(series: pd.Series, expected: bool):
    result = comparator_sets._map_building_comparator_set(series)

    assert result is expected
