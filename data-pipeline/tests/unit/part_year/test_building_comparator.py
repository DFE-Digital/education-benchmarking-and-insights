import pandas as pd

from pipeline.part_year.common import map_has_building_comparator_data


def test_both_columns_present():
    maintained_schools = pd.DataFrame(
        {
            "URN": [1, 2],
            "Total Internal Floor Area": [100.5, 200.0],
            "Age Average Score": [10, 15],
            "Other Column": ["A", "B"],
        }
    )

    result = map_has_building_comparator_data(maintained_schools)

    expected = pd.Series([True, True], name="Building Comparator Data Present")
    pd.testing.assert_series_equal(result["Building Comparator Data Present"], expected)


def test_one_column_null():
    maintained_schools = pd.DataFrame(
        {
            "URN": [1, 2],
            "Total Internal Floor Area": [100.5, None],
            "Age Average Score": [10, 15],
            "Other Column": ["A", "B"],
        }
    )
    result = map_has_building_comparator_data(maintained_schools)

    expected = pd.Series([True, False], name="Building Comparator Data Present")
    pd.testing.assert_series_equal(result["Building Comparator Data Present"], expected)


def test_other_column_null():
    maintained_schools = pd.DataFrame(
        {
            "URN": [1, 2],
            "Total Internal Floor Area": [100.5, 200.0],
            "Age Average Score": [None, 15],  # One null value
            "Other Column": ["A", "B"],
        }
    )

    result = map_has_building_comparator_data(maintained_schools)

    expected = pd.Series([False, True], name="Building Comparator Data Present")
    pd.testing.assert_series_equal(result["Building Comparator Data Present"], expected)


def test_both_columns_null():
    maintained_schools = pd.DataFrame(
        {
            "URN": [1, 2],
            "Total Internal Floor Area": [None, None],
            "Age Average Score": [None, None],
            "Other Column": ["A", "B"],
        }
    )

    result = map_has_building_comparator_data(maintained_schools)

    expected = pd.Series([False, False], name="Building Comparator Data Present")
    pd.testing.assert_series_equal(result["Building Comparator Data Present"], expected)


def test_dataframe_with_other_columns_null():
    maintained_schools = pd.DataFrame(
        {
            "URN": [1, 2],
            "Total Internal Floor Area": [100.5, 200.0],
            "Age Average Score": [10, 15],
            "Other Column": ["A", None],
        }
    )

    result = map_has_building_comparator_data(maintained_schools)

    expected = pd.Series([True, True], name="Building Comparator Data Present")
    pd.testing.assert_series_equal(result["Building Comparator Data Present"], expected)


def test_dataframe_with_all_non_null_but_zero():
    maintained_schools = pd.DataFrame(
        {
            "Total Internal Floor Area": [0.0, 150.0],
            "Age Average Score": [0, 20],
        }
    )

    result = map_has_building_comparator_data(maintained_schools)

    expected = pd.Series([True, True], name="Building Comparator Data Present")
    pd.testing.assert_series_equal(result["Building Comparator Data Present"], expected)
