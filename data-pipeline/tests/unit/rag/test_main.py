from unittest.mock import patch

import pandas as pd
import pytest

from pipeline.rag.calculations import RAG_RESULT_COLUMNS
from pipeline.rag.main import (
    compute_rag,
    compute_rag_for_school_type,
    compute_user_defined_rag_analysis,
    create_empty_rag_dataframe,
    load_school_data_and_comparators,
)

# --- Fixtures ---


@pytest.fixture
def sample_rag_df():
    """Creates a sample RAG results DataFrame."""
    data = {
        "URN": [101, 102],
        "Category": ["CatA", "CatB"],
        "SubCategory": ["Sub1", "Sub2"],
        "Value": [100, 200],
        "Median": [110, 190],
        "DiffMedian": [-10, 10],
        "Key": ["other", "outstanding_10"],
        "PercentDiff": [-9.09, 5.26],
        "Percentile": [45, 85],
        "Decile": [4, 8],
        "RAG": ["green", "red"],
    }
    return pd.DataFrame(data).set_index("URN")


# --- Tests for Helper Functions ---


def test_create_empty_rag_dataframe():
    df = create_empty_rag_dataframe()
    assert df.empty
    assert df.index.name == "URN"
    assert all(col in df.columns for col in RAG_RESULT_COLUMNS if col != "URN")


# --- Tests for Main Logic Functions ---


@patch("pipeline.rag.main.write_blob")
@patch("pipeline.rag.main.calculate_rag")
def test_compute_rag_for_school_type_single_urn(
    mock_calculate_rag, mock_write_blob, sample_rag_df
):
    """Tests the serial (single URN) execution path."""
    mock_calculate_rag.return_value = iter(
        sample_rag_df.reset_index().to_dict("records")
    )
    school_data = pd.DataFrame(index=[101, 102])

    result_df = compute_rag_for_school_type(
        school_type="academies",
        run_type="default",
        run_id="123",
        school_data=school_data,
        comparator_data={},
        target_urn=101,
    )
    mock_calculate_rag.assert_called_once_with(school_data, {}, target_urn=101)
    mock_write_blob.assert_called_once()
    assert not result_df.empty
    assert result_df.shape == (2, 10)  # 2 rows from sample_rag_df


def test_compute_rag_for_school_type_target_urn_not_found():
    """Tests that an empty DataFrame is returned if the target URN doesn't exist."""
    school_data = pd.DataFrame(index=[102, 103])  # Does not contain 101
    result_df = compute_rag_for_school_type(
        school_type="academies",
        run_type="default",
        run_id="123",
        school_data=school_data,
        comparator_data={},
        target_urn=101,
    )
    assert result_df.empty


@patch("pipeline.rag.main.get_blob")
def test_load_school_data_and_comparators_positive(mock_get_blob):
    """Tests successful data loading."""
    mock_get_blob.return_value = b"some_parquet_data"  # Placeholder for blob data

    with patch("pandas.read_parquet") as mock_read_parquet:
        mock_read_parquet.return_value = pd.DataFrame({"col1": [1, 2]})

        school_data, comparator_data = load_school_data_and_comparators(
            "default", "123", "maintained_schools"
        )
        assert mock_read_parquet.call_count == 2
        assert not school_data.empty
        assert not comparator_data.empty


def test_load_school_data_and_comparators_negative_unknown_type():
    """Tests that a ValueError is raised for an unknown school type."""
    with pytest.raises(ValueError, match="Unknown school type: invalid_type"):
        load_school_data_and_comparators("default", "123", "invalid_type")


@patch("pipeline.rag.main.insert_metric_rag")
@patch("pipeline.rag.main.compute_rag_for_school_type")
@patch("pipeline.rag.main.load_school_data_and_comparators")
def test_compute_rag_positive(
    mock_load_data, mock_compute_type, mock_insert, sample_rag_df
):
    """Tests the end-to-end compute_rag flow."""
    # Mock data loading to return dummy data
    mock_load_data.return_value = (pd.DataFrame(), pd.DataFrame())
    # Mock type-specific computation to return sample results
    mock_compute_type.return_value = sample_rag_df

    compute_rag(run_type="default", run_id="123")

    assert mock_load_data.call_count == 2  # Once for each school type
    assert mock_compute_type.call_count == 2
    mock_insert.assert_called_once()

    # Check the combined DataFrame passed to insert_metric_rag
    args, _ = mock_insert.call_args
    combined_df = args[2]
    assert combined_df.shape == (4, 10)  # sample_rag_df (2 rows) concatenated twice


@patch("pipeline.rag.main.insert_metric_rag")
@patch("pipeline.rag.main.compute_rag_for_school_type")
@patch("pipeline.rag.main.load_school_data_and_comparators")
def test_compute_rag_negative_load_failure(
    mock_load_data, mock_compute_type, mock_insert
):
    """Tests that an exception during data loading is propagated."""
    mock_load_data.side_effect = FileNotFoundError("File not found")

    with pytest.raises(FileNotFoundError):
        compute_rag(run_type="default", run_id="123")

    mock_compute_type.assert_not_called()
    mock_insert.assert_not_called()


@patch("pipeline.rag.main.insert_metric_rag")
@patch("pipeline.rag.main.write_blob")
@patch("pipeline.rag.main.compute_user_defined_rag")
@patch("pipeline.rag.main.prepare_data")
@patch("pandas.read_parquet")
@patch("pipeline.rag.main.get_blob")
def test_compute_user_defined_rag_analysis_positive(
    mock_get_blob,
    mock_read_parquet,
    mock_prepare,
    mock_compute_rag,
    mock_write_blob,
    mock_insert,
    sample_rag_df,
):
    """Tests the positive path for user-defined RAG analysis."""
    # Setup mocks
    mock_read_parquet.return_value = pd.DataFrame(index=[101, 102, 103])
    mock_prepare.side_effect = lambda x: x  # Pass through
    mock_compute_rag.return_value = iter(sample_rag_df.reset_index().to_dict("records"))

    compute_user_defined_rag_analysis(
        year=2023, run_id="user123", target_urn=101, comparator_set=[102, 103]
    )

    mock_get_blob.assert_called_once()
    mock_read_parquet.assert_called_once()
    mock_compute_rag.assert_called_once()
    mock_write_blob.assert_called_once()
    mock_insert.assert_called_once()


@patch("pandas.read_parquet")
@patch("pipeline.rag.main.get_blob")
def test_compute_user_defined_rag_analysis_target_not_found(
    mock_get_blob, mock_read_parquet
):
    """Tests ValueError when the target URN is not in the loaded data."""
    required_columns = [
        "Boarders (name)",
        "Number of pupils",
        "Percentage Free school meals",
        "Percentage SEN",
        "Percentage Primary Need SPLD",
        "Percentage Primary Need MLD",
        "Percentage Primary Need PMLD",
        "Percentage Primary Need SEMH",
        "Percentage Primary Need SLCN",
        "Percentage Primary Need HI",
        "Percentage Primary Need MSI",
        "Percentage Primary Need PD",
        "Percentage Primary Need ASD",
        "Percentage Primary Need OTH",
        "Total Internal Floor Area",
        "Age Average Score",
    ]
    mock_read_parquet.return_value = pd.DataFrame(
        index=[102, 103], columns=required_columns
    )  # Target 101 is missing

    with pytest.raises(ValueError, match="Target URN 101 not found in data"):
        compute_user_defined_rag_analysis(
            year=2023, run_id="user123", target_urn=101, comparator_set=[102, 103]
        )
