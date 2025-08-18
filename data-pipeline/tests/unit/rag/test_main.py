# from unittest.mock import ANY, call, patch

# import pandas as pd
# import pytest

# from pipeline.rag.main import (
#     RAG_RESULT_COLUMNS,
#     compute_rag,
#     compute_rag_for_school_type,
#     compute_user_defined_rag_analysis,
#     create_empty_rag_dataframe,
#     load_school_data_and_comparators,
# )


# # Test create_empty_rag_dataframe
# def test_create_empty_rag_dataframe():
#     df = create_empty_rag_dataframe()
#     assert isinstance(df, pd.DataFrame)
#     assert df.empty
#     assert df.index.name == "URN"
#     assert list(df.columns) == [c for c in RAG_RESULT_COLUMNS if c != "URN"]


# # Test compute_rag_for_school_type
# @patch("pipeline.rag.main.write_blob")
# @patch("pipeline.rag.main.calculate_rag")
# def test_compute_rag_for_school_type_success(
#     mock_calculate_rag, mock_write_blob, sample_school_data, sample_rag_results_df
# ):
#     mock_calculate_rag.return_value = iter(
#         sample_rag_results_df.reset_index().to_dict("records")
#     )

#     result_df = compute_rag_for_school_type(
#         school_type="maintained",
#         run_type="default",
#         run_id="123",
#         school_data=sample_school_data,
#         comparator_data={},
#         target_urn=None,
#     )

#     mock_calculate_rag.assert_called_once_with(sample_school_data, {}, target_urn=None)
#     mock_write_blob.assert_called_once_with(
#         "metric-rag", "default/123/maintained.parquet", ANY
#     )
#     pd.testing.assert_frame_equal(result_df, sample_rag_results_df)


# @patch("pipeline.rag.main.write_blob")
# @patch("pipeline.rag.main.calculate_rag")
# def test_compute_rag_for_school_type_urn_not_found(
#     mock_calculate_rag, mock_write_blob, sample_school_data
# ):
#     result_df = compute_rag_for_school_type(
#         school_type="maintained",
#         run_type="default",
#         run_id="123",
#         school_data=sample_school_data,
#         comparator_data={},
#         target_urn="999",  # This URN does not exist
#     )

#     mock_calculate_rag.assert_not_called()
#     assert result_df.empty


# @patch("pipeline.rag.main.write_blob")
# @patch("pipeline.rag.main.calculate_rag")
# def test_compute_rag_for_school_type_calculation_fails(
#     mock_calculate_rag, mock_write_blob, sample_school_data
# ):
#     mock_calculate_rag.side_effect = Exception("Calculation Error")

#     result_df = compute_rag_for_school_type(
#         school_type="maintained",
#         run_type="default",
#         run_id="123",
#         school_data=sample_school_data,
#         comparator_data={},
#     )

#     assert result_df.empty
#     mock_write_blob.assert_called_once()  # Should still try to write the empty result


# # Test load_school_data_and_comparators
# @patch("pipeline.rag.main.get_blob")
# def test_load_school_data_and_comparators_success(
#     mock_get_blob, mock_blob_storage, sample_school_data, comparators_fixture
# ):
#     mock_get_blob.side_effect = mock_blob_storage

#     school_data, comparator_data = load_school_data_and_comparators(
#         run_type="default",
#         run_id="123",
#         school_type="academies",
#         comparator_suffix="_comp",
#     )

#     assert mock_get_blob.call_count == 2
#     mock_get_blob.assert_has_calls(
#         [
#             call("comparator-sets", "default/123/academies.parquet"),
#             call("comparator-sets", "default/123/academies_comp_comparators.parquet"),
#         ]
#     )
#     assert not school_data.empty
#     assert not comparator_data.empty


# @patch("pipeline.rag.main.get_blob")
# def test_load_school_data_and_comparators_failure(mock_get_blob):
#     mock_get_blob.side_effect = FileNotFoundError("Blob not found")

#     with pytest.raises(FileNotFoundError):
#         load_school_data_and_comparators(
#             run_type="default", run_id="123", school_type="academies"
#         )


# # Test compute_rag
# @patch("pipeline.rag.main.insert_metric_rag")
# @patch("pipeline.rag.main.compute_rag_for_school_type")
# @patch("pipeline.rag.main.load_school_data_and_comparators")
# def test_compute_rag_success(
#     mock_load, mock_compute_type, mock_insert, sample_rag_results_df
# ):
#     # Simulate loading and computing for two school types
#     mock_load.side_effect = [
#         (pd.DataFrame({"URN": [1]}).set_index("URN"), {}),
#         (pd.DataFrame({"URN": [2]}).set_index("URN"), {}),
#     ]
#     mock_compute_type.side_effect = [
#         sample_rag_results_df.iloc[:2],  # Results for maintained
#         sample_rag_results_df.iloc[2:],  # Results for academies
#     ]

#     duration = compute_rag(run_type="default", run_id="123")

#     assert mock_load.call_count == 2
#     assert mock_compute_type.call_count == 2
#     # Check that the final combined DataFrame was passed to insert_metric_rag
#     mock_insert.assert_called_once()
#     final_df = mock_insert.call_args[0][2]
#     assert len(final_df) == 4
#     assert duration > 0


# # Test compute_user_defined_rag_analysis
# @patch("pipeline.rag.main.insert_metric_rag")
# @patch("pipeline.rag.main.write_blob")
# @patch("pipeline.rag.main.compute_user_defined_rag")
# @patch("pipeline.rag.main.prepare_data")
# @patch("pipeline.rag.main.get_blob")
# def test_compute_user_defined_rag_analysis_success(
#     mock_get,
#     mock_prepare,
#     mock_compute,
#     mock_write,
#     mock_insert,
#     mock_blob_storage,
#     sample_rag_results_df,
# ):
#     mock_get.side_effect = mock_blob_storage
#     mock_prepare.side_effect = lambda x: x  # Passthrough
#     mock_compute.return_value = iter(
#         sample_rag_results_df.reset_index().to_dict("records")
#     )

#     duration = compute_user_defined_rag_analysis(
#         year=2023, run_id="abc", target_urn=1, comparator_set=[2, 4, 5]
#     )

#     mock_get.assert_called_once_with(
#         "pre-processed", "default/2023/all_schools.parquet"
#     )
#     mock_prepare.assert_called_once()
#     mock_compute.assert_called_once()
#     mock_write.assert_called_once_with(
#         "metric-rag", "default/abc/user_defined.parquet", ANY
#     )
#     mock_insert.assert_called_once()
#     assert duration > 0


# # @patch("pipeline.rag.main.get_blob")
# # def test_compute_user_defined_rag_analysis_target_urn_not_found(
# #     mock_get, mock_blob_storage
# # ):
# #     mock_get.side_effect = mock_blob_storage

# #     with pytest.raises(ValueError, match="Target URN 999 not found"):
# #         compute_user_defined_rag_analysis(
# #             year=2023, run_id="abc", target_urn=999, comparator_set=[2, 4, 5]
# #         )
