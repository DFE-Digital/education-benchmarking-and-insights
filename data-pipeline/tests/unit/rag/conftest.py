import pandas as pd
import pytest


@pytest.fixture
def sample_school_data():
    """Creates a sample DataFrame of school data for testing."""
    data = {
        "URN": [1, 2, 3, 4, 5],
        "Boarders (name)": ["No", "Unknown", "Yes", "No", "No"],
        "Total Internal Floor Area": [1000, 1050, 1200, 950, 800],
        "Age Average Score": [1980, 1985, 2010, 1975, 1950],
        "OfstedRating (name)": [
            "Good",
            "Outstanding",
            "Requires Improvement",
            "Good",
            "Outstanding",
        ],
        "Percentage SEN": [0.1, 0.11, 0.091, 0.25, 0.15],
        "Percentage Free school meals": [0.2, 0.205, 0.199, 0.26, 0.25],
        "Number of pupils": [500, 510, 480, 650, 550],
        "CategoryA_Sub1_Per Unit": [10, 12, 8, 15, 9],
        "CategoryB_Sub1_Per Unit": [100, 110, 90, 120, 95],
        "Extra Column": ["foo", "bar", "baz", "qux", "quux"],  # To be dropped
    }
    return pd.DataFrame(data).set_index("URN")


@pytest.fixture
def target_school(sample_school_data):
    """Provides a single target school for comparison."""
    return sample_school_data.loc[1]
