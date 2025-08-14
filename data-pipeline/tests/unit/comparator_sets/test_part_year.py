import pandas as pd
import pytest

from pipeline.comparator_sets import calculations as comparator_sets


@pytest.mark.parametrize(
    "series,expected",
    [
        (
            pd.Series(
                {
                    "Financial Data Present": True,
                    "Pupil Comparator Data Present": True,
                    "Did Not Submit": False,
                }
            ),
            True,
        ),
        (
            pd.Series(
                {
                    "Financial Data Present": False,
                    "Pupil Comparator Data Present": True,
                    "Did Not Submit": False,
                }
            ),
            False,
        ),
        (
            pd.Series(
                {
                    "Financial Data Present": True,
                    "Pupil Comparator Data Present": False,
                    "Did Not Submit": False,
                }
            ),
            False,
        ),
        (
            pd.Series(
                {
                    "Financial Data Present": False,
                    "Pupil Comparator Data Present": False,
                    "Did Not Submit": False,
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
                    "Financial Data Present": True,
                    "Pupil Comparator Data Present": True,
                    "Building Comparator Data Present": True,
                    "Did Not Submit": False,
                }
            ),
            True,
        ),
        (
            pd.Series(
                {
                    "Financial Data Present": True,
                    "Pupil Comparator Data Present": False,
                    "Building Comparator Data Present": False,
                    "Did Not Submit": False,
                }
            ),
            False,
        ),
        (
            pd.Series(
                {
                    "Financial Data Present": False,
                    "Pupil Comparator Data Present": True,
                    "Building Comparator Data Present": False,
                    "Did Not Submit": False,
                }
            ),
            False,
        ),
        (
            pd.Series(
                {
                    "Financial Data Present": False,
                    "Pupil Comparator Data Present": False,
                    "Building Comparator Data Present": True,
                    "Did Not Submit": False,
                }
            ),
            False,
        ),
        (
            pd.Series(
                {
                    "Financial Data Present": False,
                    "Pupil Comparator Data Present": False,
                    "Building Comparator Data Present": False,
                    "Did Not Submit": False,
                }
            ),
            False,
        ),
        (
            pd.Series(
                {
                    "Financial Data Present": True,
                    "Pupil Comparator Data Present": True,
                    "Building Comparator Data Present": False,
                    "Did Not Submit": False,
                }
            ),
            False,
        ),
        (
            pd.Series(
                {
                    "Financial Data Present": False,
                    "Pupil Comparator Data Present": True,
                    "Building Comparator Data Present": True,
                    "Did Not Submit": False,
                }
            ),
            False,
        ),
        (
            pd.Series(
                {
                    "Financial Data Present": True,
                    "Pupil Comparator Data Present": False,
                    "Building Comparator Data Present": True,
                    "Did Not Submit": False,
                }
            ),
            False,
        ),
    ],
)
def test_map_building_comparator_set(series: pd.Series, expected: bool):
    result = comparator_sets._map_building_comparator_set(series)

    assert result is expected
