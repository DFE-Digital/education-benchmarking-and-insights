from unittest.mock import patch

import pandas as pd
import pytest

from pipeline.rag.main import (
    compute_rag,
    compute_user_defined_rag,
    load_school_data_and_comparators,
)

# --- Fixtures ---


@pytest.fixture
def sample_school_data():
    """Creates sample pre-processed school data."""
    return pd.DataFrame(index=[101, 102, 103])


@pytest.fixture
def sample_comparator_map():
    """Creates a sample comparator map dictionary."""
    return {
        101: {"Pupil": [102, 103], "Building": [102]},
        102: {"Pupil": [101, 103], "Building": [101, 103]},
        103: {"Pupil": [101, 102], "Building": [102]},
    }


@pytest.fixture
def sample_rag_df():
    """Creates a sample RAG results DataFrame."""
    data = {
        "URN": [101, 101],  # URN can be repeated for different categories
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


# --- Tests for I/O and Orchestration Functions ---


@patch("pipeline.rag.main.insert_metric_rag")
@patch("pipeline.rag.main.write_blob")
@patch("pipeline.rag.main._run_rag_computation_engine")
@patch("pipeline.rag.main.load_school_data_and_comparators")
def test_compute_rag_orchestration(
    mock_load_data,
    mock_engine,
    mock_write_blob,
    mock_insert,
    sample_rag_df,
    cols_for_prepare_data,
):
    mock_load_data.return_value = (
        pd.DataFrame(columns=cols_for_prepare_data),
        pd.DataFrame({}),
    )
    mock_engine.return_value = sample_rag_df

    compute_rag(run_type="default", run_id="123")

    assert mock_load_data.call_count == 2  # For maintained_schools and academies
    assert mock_engine.call_count == 2
    assert mock_write_blob.call_count == 2
    mock_insert.assert_called_once()

    args, _ = mock_insert.call_args
    combined_df = args[2]
    assert combined_df.shape == (4, 10)  # sample_rag_df concatenated twice


@patch("pipeline.rag.main.load_school_data_and_comparators")
def test_compute_rag_propagates_load_failure(mock_load_data):
    """Tests that an exception during data loading stops execution."""
    mock_load_data.side_effect = FileNotFoundError("File not found")

    with pytest.raises(FileNotFoundError):
        compute_rag(run_type="default", run_id="123")


@patch("pipeline.rag.main.insert_metric_rag")
@patch("pipeline.rag.main.write_blob")
@patch("pipeline.rag.main._run_rag_computation_engine")
@patch("pandas.read_parquet")
@patch("pipeline.rag.main.get_blob")
def test_compute_user_defined_rag_orchestration(
    mock_get_blob,
    mock_read_parquet,
    mock_engine,
    mock_write_blob,
    mock_insert,
    sample_rag_df,
    cols_for_prepare_data,
):
    mock_read_parquet.return_value = pd.DataFrame(
        index=[101, 102, 103], columns=cols_for_prepare_data
    )
    mock_engine.return_value = sample_rag_df

    compute_user_defined_rag(
        year=2023, run_id="user123", target_urn=101, comparator_set=[102, 103]
    )

    mock_get_blob.assert_called_once()
    mock_engine.assert_called_once()
    mock_write_blob.assert_called_once()
    mock_insert.assert_called_once()

    # Crucially, check that the comparator_set was correctly formatted for the engine
    args, _ = mock_engine.call_args
    comparator_map_arg = args[1]
    expected_map = {101: {"Pupil": [102, 103], "Building": [102, 103]}}
    assert comparator_map_arg == expected_map


@patch("pandas.read_parquet")
@patch("pipeline.rag.main.get_blob")
def test_compute_user_defined_rag_target_not_found(
    mock_get_blob, mock_read_parquet, cols_for_prepare_data
):
    """Tests ValueError when the target URN is not in the loaded data."""
    mock_read_parquet.return_value = pd.DataFrame(
        index=[102, 103], columns=cols_for_prepare_data
    )

    with pytest.raises(ValueError, match="Target URN 101 not found"):
        compute_user_defined_rag(
            year=2023, run_id="user123", target_urn=101, comparator_set=[102, 103]
        )


def test_load_school_data_and_comparators_negative_unknown_type():
    with pytest.raises(ValueError, match="Unknown school type: invalid_type"):
        load_school_data_and_comparators("default", "123", "invalid_type")
