import numpy as np
import pandas as pd
import pytest

from pipeline.rag.calculations import (
    find_area_close_comparators,
    find_pupil_close_comparators,
    calculate_percentile_rank,
    get_positive_values_series,
    calculate_category_statistics,
    prepare_data_for_rag
)
from pipeline.config import rag_category_settings

# --- Fixtures for Test Data ---

@pytest.fixture
def sample_school_data():
    """Creates a sample DataFrame of school data for testing."""
    data = {
        "URN": [1, 2, 3, 4, 5],
        "Boarders (name)": ["No", "Unknown", "Yes", "No", "No"],
        "Total Internal Floor Area": [1000, 1050, 1200, 950, 800],
        "Age Average Score": [1980, 1985, 2010, 1975, 1950],
        "OfstedRating (name)": ["Good", "Outstanding", "Requires Improvement", "Good", "Outstanding"],
        "Percentage SEN": [0.1, 0.12, 0.08, 0.25, 0.15],
        "Percentage Free school meals": [0.2, 0.22, 0.18, 0.26, 0.25],
        "Number of pupils": [500, 510, 480, 650, 550],
        "CategoryA_Sub1_Per Unit": [10, 12, 8, 15, 9],
        "CategoryB_Sub1_Per Unit": [100, 110, 90, 120, 95],
        "Extra Column": ["foo", "bar", "baz", "qux", "quux"] # To be dropped
    }
    return pd.DataFrame(data).set_index("URN")

@pytest.fixture
def target_school(sample_school_data):
    """Provides a single target school for comparison."""
    return sample_school_data.loc[1]

# --- Tests for Comparator Functions ---

def test_find_area_close_comparators_positive(target_school, sample_school_data):
    """Tests that a school within tolerance is correctly identified."""
    comparators = sample_school_data.drop(1)
    # School 2: Floor area 1050 (within 10% of 1000), Age 1985 (within 20 years of 1980)
    # School 4: Floor area 950 (within 10% of 1000), Age 1975 (within 20 years of 1980)
    expected = pd.Series([True, False, True, False], index=[2, 3, 4, 5])
    result = find_area_close_comparators(target_school, comparators)
    pd.testing.assert_series_equal(result, expected)

def test_find_area_close_comparators_negative(target_school, sample_school_data):
    """Tests that schools outside of tolerance are not identified."""
    comparators = sample_school_data.drop(1)
    # School 3: Floor area 1200 (>10% diff), Age 2010 (>20 years diff)
    # School 5: Floor area 800 (>10% diff), Age 1950 (>20 years diff)
    result = find_area_close_comparators(target_school, comparators)
    assert not result[3]
    assert not result[5]

def test_find_pupil_close_comparators_positive(target_school, sample_school_data):
    """Tests finding comparators with similar pupil demographics."""
    comparators = sample_school_data.drop(1)
    # School 2: pupils 510 (close), fsm 0.22 (close), sen 0.12 (close)
    expected = pd.Series([True, False, False, False], index=[2, 3, 4, 5])
    result = find_pupil_close_comparators(target_school, comparators)
    pd.testing.assert_series_equal(result, expected)

def test_find_pupil_close_comparators_negative(target_school, sample_school_data):
    """Tests exclusion of schools with dissimilar pupil demographics."""
    comparators = sample_school_data.drop(1)
    # School 4: pupils 650 (>25% diff)
    # School 5: sen 0.15 (just outside 0.1 rtol of 0.1)
    result = find_pupil_close_comparators(target_school, comparators)
    assert not result[4]


# --- Tests for Calculation Logic ---

def test_calculate_percentile_rank():
    """Tests the percentile rank calculation."""
    data = np.array([10, 20, 30, 40, 50])
    assert calculate_percentile_rank(data, 35) == 80.0
    assert calculate_percentile_rank(data, 50) == 100.0
    assert calculate_percentile_rank(data, 5) == 0.0

def test_calculate_percentile_rank_empty_input():
    """Tests percentile rank with empty data, expecting 0."""
    assert calculate_percentile_rank(np.array([]), 100) == 0.0

def test_get_positive_values_series():
    """Tests filtering for positive values, always keeping the target URN."""
    data = pd.DataFrame({
        "Value": [-10, 0, 10, 20]
    }, index=[1, 2, 3, 4])
    result = get_positive_values_series(data, "Value", 2) # URN 2 has a value of 0
    expected = pd.Series([0, 10, 20], index=[2, 3, 4], name="Value")
    pd.testing.assert_series_equal(result, expected)

def test_calculate_category_statistics_positive():
    """Tests a standard calculation for a single category."""
    category_data = pd.DataFrame({
        "CategoryA_Sub1_Per Unit": [10, 20, 30, 40, 50]
    }, index=[1, 2, 3, 4, 5])

    result = calculate_category_statistics(
        school_urn=1,
        category_name="CategoryA",
        subcategory_name="CategoryA_Sub1_Per Unit",
        category_data=category_data,
        ofsted_rating="Outstanding",
        rag_settings=rag_category_settings["Teaching and Teaching support staff"],
        similar_schools_count=12
    )
    assert result["URN"] == 1
    assert result["Category"] == "CategoryA"
    assert result["SubCategory"] == "Sub1"
    assert result["Value"] == 10
    assert result["Median"] == 30.0
    assert np.isclose(result["PercentDiff"], -66.666666)
    assert result["Decile"] == 2
    assert result["Key"] == "outstanding_10"
    assert result["RAG"] == "amber"

def test_calculate_category_statistics_key_error():
    """Tests that a KeyError for a missing URN is handled gracefully (returns nothing)."""
    category_data = pd.DataFrame({
        "CategoryA_Sub1_Per Unit": [10, 20, 30, 40, 50]
    }, index=[1, 2, 3, 4, 5])

    # Expecting the function to raise KeyError because URN 99 is not in category_data
    with pytest.raises(KeyError):
        calculate_category_statistics(
            school_urn=99,
            category_name="CategoryA",
            subcategory_name="CategoryA_Sub1_Per Unit",
            category_data=category_data,
            ofsted_rating="Good",
            rag_settings=rag_category_settings["Teaching and Teaching support staff"],
            similar_schools_count=5
        )


# --- Tests for Data Preparation ---

def test_prepare_data_for_rag(sample_school_data):
    """Tests data cleaning and preparation."""
    # Add NaN to test filling
    sample_school_data.loc[1, "Total Internal Floor Area"] = np.nan
    prepared_data = prepare_data_for_rag(sample_school_data)

    # Check that NaN is filled
    assert prepared_data.loc[1, "Total Internal Floor Area"] == 0.0
    # Check that extra column is dropped
    assert "Extra Column" not in prepared_data.columns
    # Check that per unit columns are kept
    assert "CategoryA_Sub1_Per Unit" in prepared_data.columns

def test_np_isclose_rtol_behavior():
    """
    Explicitly test the relative tolerance (rtol) behavior of np.isclose,
    as it's central to the comparator logic.
    Formula: absolute(a - b) <= (atol + rtol * absolute(b))
    """
    # Positive case: 1050 is within 10% of 1000
    assert np.isclose(1050, 1000, rtol=0.1)
    # Negative case: 1101 is NOT within 10% of 1000
    assert not np.isclose(1101, 1000, rtol=0.1)
    # Edge case: Exactly at the 10% tolerance boundary
    assert np.isclose(1100, 1000, rtol=0.1)
    # Lower bound
    assert np.isclose(900, 1000, rtol=0.1)
    assert not np.isclose(899, 1000, rtol=0.1)