from io import BytesIO

import pandas as pd
import pytest

from pipeline.rag.calculations import BASE_COLUMNS


@pytest.fixture
def sample_school_data():
    """Fixture for sample school DataFrame."""
    data = {
        "URN": [1, 2, 3, 4, 5],
        "Total Internal Floor Area": [1000, 1050, 1200, 800, 950],
        "Age Average Score": [25, 30, 40, 50, 20],
        "OfstedRating (name)": [
            "Good",
            "Outstanding",
            "Requires Improvement",
            "Good",
            "Outstanding",
        ],
        "Utilities": [
            "Subcategory1",
            "Subcategory2",
            "Subcategory1",
            "Subcategory2",
            "Subcategory1",
        ],
        "Percentage SEN": [0.1, 0.12, 0.08, 0.15, 0.09],
        "Percentage Free school meals": [0.2, 0.21, 0.18, 0.25, 0.19],
        "Number of pupils": [500, 520, 480, 550, 490],
        "Partial Years Present": [False, False, True, False, False],
        "CategoryA_SubCategory1_Per Unit": [10.0, 12.0, 8.0, 15.0, 9.0],
        "CategoryB_SubCategory2_Per Unit": [20.0, 22.0, 18.0, 25.0, 19.0],
    }
    df = pd.DataFrame(data).set_index("URN")
    return df


@pytest.fixture(scope="module")
def sample_columns():
    """Provides a sample pandas Index for testing CategoryColumnCache."""
    return pd.Index(
        BASE_COLUMNS
        + [
            "Teaching and Teaching support staff_Per Unit",
            "Non-educational support staff and services_Per Unit",
            "Utilities_Per Unit",
            "Some other random column",
        ]
    )


@pytest.fixture
def sample_rag_results_df():
    """Fixture for a sample RAG results DataFrame."""
    data = {
        "URN": [1, 1, 2, 2],
        "Category": ["CategoryA", "CategoryB", "CategoryA", "CategoryB"],
        "SubCategory": ["Sub1", "Sub2", "Sub1", "Sub2"],
        "Value": [10.0, 20.0, 12.0, 22.0],
        "Median": [11.0, 21.0, 11.0, 21.0],
        "DiffMedian": [-1.0, -1.0, 1.0, 1.0],
        "Key": ["other", "other", "outstanding", "outstanding"],
        "PercentDiff": [-9.09, -4.76, 9.09, 4.76],
        "Percentile": [40.0, 35.0, 60.0, 65.0],
        "Decile": [4, 3, 6, 6],
        "RAG": ["A", "G", "R", "A"],
    }
    return pd.DataFrame(data).set_index("URN")


@pytest.fixture(scope="module")
def sample_data_for_comparators():
    """Provides a sample DataFrame and comparators dict for testing ComparatorSetCache."""
    data = {
        "URN": ["1", "2", "3", "4", "5", "6"],
        "SchoolName": ["A", "B", "C", "D", "E", "F"],
    }
    df = pd.DataFrame(data).set_index("URN")

    comparators = {
        # School with both pupil and building comparators
        "1": {"Pupil": ["2", "3"], "Building": ["4", "5"]},
        # School with an empty pupil list
        "2": {"Pupil": [], "Building": ["6"]},
        # School in comparators but not in the main dataframe index
        "999": {"Pupil": ["1"], "Building": ["2"]},
    }
    return df, comparators


@pytest.fixture
def mock_blob_storage(sample_school_data, sample_comparators_dict):
    """Fixture to mock the get_blob function."""

    def mock_get_blob(_, blob_name):
        if "comparators" in blob_name:
            buffer = BytesIO()
            sample_comparators_dict.to_parquet(buffer)
            buffer.seek(0)
            return buffer
        else:
            buffer = BytesIO()
            sample_school_data.to_parquet(buffer)
            buffer.seek(0)
            return buffer

    return mock_get_blob
