from unittest.mock import patch

import pandas as pd

from pipeline.rag.loader import load_school_data_and_comparators


@patch("pipeline.rag.loader.get_blob")
def test_load_school_data_and_comparators_positive(mock_get_blob):
    """Tests successful data loading."""
    mock_get_blob.return_value = b"some_parquet_data"

    with patch("pandas.read_parquet") as mock_read_parquet:
        mock_read_parquet.return_value = pd.DataFrame({"col1": [1, 2]})
        school_data, _ = load_school_data_and_comparators(
            "default", "123", "maintained_schools"
        )
        assert mock_read_parquet.call_count == 2
        assert not school_data.empty