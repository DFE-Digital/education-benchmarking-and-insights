import os
from unittest.mock import MagicMock, patch

from pyspark.sql.types import IntegerType, StringType, StructField, StructType

from education_benchmarking_and_insights.base import (
    DatabricksDataLoader,
    hash_compare_view_dfs,
)


# Tests for hash_compare_view_dfs
def test_hash_compare_view_dfs_match(spark, mock_logger):
    schema = StructType(
        [
            StructField("col1", StringType(), True),
            StructField("col2", IntegerType(), True),
        ]
    )
    data = [("A", 1), ("B", 2)]
    df1 = spark.createDataFrame(data, schema)
    df2 = spark.createDataFrame(data, schema)

    result = hash_compare_view_dfs("test_table", df1, df2)
    assert result is True
    mock_logger.info.assert_called_with(
        "Upstream data in test_table matches materialized_view"
    )
    mock_logger.warning.assert_not_called()


def test_hash_compare_view_dfs_no_match(spark, mock_logger):
    schema = StructType(
        [
            StructField("col1", StringType(), True),
            StructField("col2", IntegerType(), True),
        ]
    )
    data1 = [("A", 1), ("B", 2)]
    data2 = [("A", 1), ("C", 3)]
    df1 = spark.createDataFrame(data1, schema)
    df2 = spark.createDataFrame(data2, schema)

    result = hash_compare_view_dfs("test_table", df1, df2)
    assert result is False
    mock_logger.warning.assert_called_with(
        "Upstream data in test_table differs from materialized_view"
    )
    mock_logger.info.assert_not_called()


def test_hash_compare_view_dfs_empty_dfs(spark, mock_logger):
    schema = StructType(
        [
            StructField("col1", StringType(), True),
            StructField("col2", IntegerType(), True),
        ]
    )
    df_empty1 = spark.createDataFrame([], schema)
    df_empty2 = spark.createDataFrame([], schema)

    result = hash_compare_view_dfs("empty_table", df_empty1, df_empty2)
    assert result is True
    mock_logger.info.assert_called_with(
        "Upstream data in empty_table matches materialized_view"
    )
    mock_logger.warning.assert_not_called()


def test_hash_compare_view_dfs_different_order_same_content(spark, mock_logger):
    schema = StructType(
        [
            StructField("col1", StringType(), True),
            StructField("col2", IntegerType(), True),
        ]
    )
    data1 = [("A", 1), ("B", 2)]
    data2 = [("B", 2), ("A", 1)]  # Different order
    df1 = spark.createDataFrame(data1, schema)
    df2 = spark.createDataFrame(data2, schema)

    result = hash_compare_view_dfs("ordered_table", df1, df2)
    assert result is False
    mock_logger.warning.assert_called_once()


# Tests for DatabricksDataLoader
class TestDatabricksDataLoader:
    def test_is_databricks_env(self):
        with patch.dict(os.environ, {"DATABRICKS_RUNTIME_VERSION": "10.4"}):
            loader = DatabricksDataLoader(MagicMock())
            assert loader._is_databricks() is True

    def test_is_not_databricks_env(self):
        with patch.dict(os.environ, {}, clear=True):
            loader = DatabricksDataLoader(MagicMock())
            assert loader._is_databricks() is False

    def test_get_table_name_databricks(self):
        with patch.dict(os.environ, {"DATABRICKS_RUNTIME_VERSION": "10.4"}):
            loader = DatabricksDataLoader(MagicMock())
            full_table_name = "catalog.schema.table_name"
            assert loader._get_table_name(full_table_name) == full_table_name

    def test_get_table_name_local(self):
        with patch.dict(os.environ, {}, clear=True):
            loader = DatabricksDataLoader(MagicMock())
            full_table_name = "catalog.schema.table_name"
            assert loader._get_table_name(full_table_name) == "table_name"

    def test_get_table_name_local_simple(self):
        with patch.dict(os.environ, {}, clear=True):
            loader = DatabricksDataLoader(MagicMock())
            full_table_name = "table_name"
            assert loader._get_table_name(full_table_name) == "table_name"

    def test_check_for_updates_in_materialized_views_match(
        self, spark, mock_logger
    ):
        loader = DatabricksDataLoader(spark)
        schema = StructType(
            [
                StructField("col1", StringType(), True),
                StructField("col2", IntegerType(), True),
            ]
        )
        data = [("A", 1), ("B", 2)]
        df1 = spark.createDataFrame(data, schema)
        df2 = spark.createDataFrame(data, schema)

        loader._check_for_updates_in_materialized_views("test_table_check", df1, df2)
        mock_logger.info.assert_called_with(
            "Upstream data in test_table_check matches materialized_view"
        )
        mock_logger.error.assert_not_called()

    def test_check_for_updates_in_materialized_views_no_match(
        self, spark, mock_logger
    ):
        loader = DatabricksDataLoader(spark)
        schema = StructType(
            [
                StructField("col1", StringType(), True),
                StructField("col2", IntegerType(), True),
            ]
        )
        data1 = [("A", 1), ("B", 2)]
        data2 = [("A", 1), ("C", 3)]
        df1 = spark.createDataFrame(data1, schema)
        df2 = spark.createDataFrame(data2, schema)

        loader._check_for_updates_in_materialized_views("test_table_check", df1, df2)
        mock_logger.warning.assert_called_with(
            "Upstream data in test_table_check differs from materialized_view"
        )
        mock_logger.error.assert_not_called()

    def test_check_for_updates_in_materialized_views_exception(
        self, spark, mock_logger
    ):
        loader = DatabricksDataLoader(spark)
        # Pass an object that will cause an error when trying to call .agg()
        faulty_df = None
        df2 = spark.createDataFrame([], StructType([]))

        loader._check_for_updates_in_materialized_views("faulty_table", faulty_df, df2)
        mock_logger.error.assert_called_once()
        assert (
            "Error comparing hashes for faulty_table"
            in mock_logger.error.call_args[0][0]
        )
