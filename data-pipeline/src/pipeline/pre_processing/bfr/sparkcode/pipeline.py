# Databricks notebook source
import os
from abc import ABC, abstractmethod

from pyspark.sql import DataFrame, SparkSession
from pyspark.sql.functions import col, collect_list, hash, struct
from pyspark.sql.types import ( # Added for schema definition
    DoubleType,
    IntegerType,
    StringType,
    StructField,
    StructType,
)

from .engine import build_bfr_data, build_bfr_historical_data
from .logging import setup_logger
from . import bfr_pyspark_mocks as mocks

logger = setup_logger("bfr_pipeline")

bfr_sofa_cols = {
    "TrustUPIN": "Int64",
    "Title": "string",
    "EFALineNo": "Int64",
    "Y1P1": "float",
    "Y1P2": "float",
    "Y2P1": "float",
    "Y2P2": "float",
}

bfr_3y_cols = {
    "TrustUPIN": "Int64",
    "EFALineNo": "Int64",
    "Y2": "float",
    "Y3": "float",
    "Y4": "float",
}


def hash_compare_view_dfs(
    table_id: str, view: DataFrame, materialized_view: DataFrame
) -> bool:
    """
    Compare the hash of the current view with the hash of the materialized view.
    If the hashes are different, the function will log a warning and return False.
    If the hashes are the same, the function will log an info message and return True.
    """
    current_hash = view.agg(
        hash(collect_list(hash(struct("*")))).alias("hash")
    ).collect()[0]["hash"]

    previous_hash = materialized_view.agg(
        hash(collect_list(hash(struct("*")))).alias("hash")
    ).collect()[0]["hash"]

    if current_hash != previous_hash:
        logger.warning(f"Upstream data in {table_id} differs from materialized_view")
        return False
    if current_hash == previous_hash:
        logger.info(f"Upstream data in {table_id} matches materialized_view")
        return True


class DatabricksFBITPipeline(ABC):
    @abstractmethod
    def run(self):
        pass

    @abstractmethod
    def load_data(self):
        pass

    @abstractmethod
    def load_ancillary_data(self):
        pass

    @abstractmethod
    def transform_data(self):
        pass

    @abstractmethod
    def save_data(self):
        pass

    @staticmethod
    def _check_for_updates_in_materialized_views(
        table_id: str, source_view: DataFrame, materialized_view: DataFrame
    ) -> None:
        """
        Check for updates in materialized views.
        If the hashes are different, the function will log a warning.
        If the hashes are the same, the function will log an info message.

        Args:
            table_id: The identifier for the table being checked.
            source_view: The DataFrame representing the source view.
            materialized_view: The DataFrame representing the materialized view.
        """
        try:
            hash_compare_view_dfs(table_id, source_view, materialized_view)
        except Exception as e:
            logger.error(f"Error comparing hashes for {table_id}: {e}")

    def _is_databricks(self):
        return "DATABRICKS_RUNTIME_VERSION" in os.environ

    def _get_table_name(self, full_table_name: str):
        if self._is_databricks():
            return full_table_name
        else:
            # For local execution, extract just the table name or adjust as needed
            # Assuming the format is catalog.schema.table
            parts = full_table_name.split(".")
            return parts[-1]


class BFRPipeline(DatabricksFBITPipeline):
    def __init__(self, year: int, spark: SparkSession):
        self.year = year
        self.config = {}
        self.spark = spark

    def _read_materialized_view(
        self, materialized_view_full_name: str, source_view_full_name: str, table_id: str
    ) -> DataFrame:
        """
        Reads a materialized view and checks it against its source view for updates.
        If not in Databricks, it returns mock data.
        """
        if not self._is_databricks():
            if table_id == "bfr_sofa":
                materialized_view = mocks.get_mock_bfr_sofa_mv(self.spark, self.year)
                source_view = mocks.get_mock_bfr_sofa_mv(self.spark, self.year - 1) # Mock source for comparison
            elif table_id == "bfr_three_year":
                materialized_view = mocks.get_mock_bfr_three_year_mv(self.spark)
                source_view = mocks.get_mock_bfr_three_year_mv(self.spark) # Mock source for comparison
            elif table_id.startswith("bfr_sofa_"): # Historical BFR SOFA
                year = int(table_id.split('_')[-1])
                materialized_view = mocks.get_mock_bfr_sofa_mv(self.spark, year)
                source_view = mocks.get_mock_bfr_sofa_mv(self.spark, year - 1) # Mock source for comparison
            else:
                raise ValueError(f"Unknown mock table_id: {table_id}")
            logger.info(f"Using mock data for {table_id}")
        else:
            materialized_view = self.spark.table(
                self._get_table_name(materialized_view_full_name)
            )
            source_view = self.spark.table(self._get_table_name(source_view_full_name))

        self._check_for_updates_in_materialized_views(
            table_id, source_view, materialized_view
        )
        return materialized_view

    def run(self):
        bfr_sofa_mv, bfr_three_year_mv = self.load_data()
        bfr_sofa_year_minus_one, bfr_sofa_year_minus_two = self.load_ancillary_data()
        bfr_final, bfr_metrics_final = self.transform_data(
            bfr_sofa_mv, bfr_three_year_mv
        )
        self.save_data(bfr_final, bfr_metrics_final)

    def load_data(self):
        """Load raw materialized views for BFR data"""
        bfr_sofa_mv = self._read_materialized_view(
            "catalog_40_copper_financial_benchmarking_insights.financial_benchmarking_insights.mv_BFR_Sofa_2025",
            "catalog_30_bronze.bfr.vw_BFR_Sofa_2024",
            "bfr_sofa",
        )
        bfr_three_year_mv = self._read_materialized_view(
            "catalog_40_copper_financial_benchmarking_insights.financial_benchmarking_insights.mv_BFR_Three_Year_Forecast_2025",
            "catalog_30_bronze.bfr.vw_BFR_Three_Year_Forecast_2024",
            "bfr_three_year",
        )

        return bfr_sofa_mv, bfr_three_year_mv

    def load_ancillary_data(self):
        # academies = self._get_academies_data()
        # academies_y1, academies_y2 = self._get_historical_academies_data()
        # Only get historical bfr data if there's the corresponding academies year
        self.bfr_sofa_year_minus_one, self.bfr_sofa_year_minus_two = (
            self._get_historical_bfr_data()
        )

        return (
            self.bfr_sofa_year_minus_one,
            self.bfr_sofa_year_minus_two,
            # academies, academies_y1, academies_y2
        )

    def _get_historical_bfr_data(self):
        last_year = self.year - 1
        year_before_last = self.year - 2
        historical_bfr_columns = ["TrustUPIN", "EFALineNo", "Y1P2", "Y2P2"]
        desired_efa_lines = [430, 999]
        self.bfr_sofa_year_minus_one = (
            self._read_materialized_view(
                f"catalog_30_bronze.bfr.vw_BFR_Sofa_{last_year}",
                f"catalog_30_bronze.bfr.vw_BFR_Sofa_{last_year}", # Mock source is same as materialized for historical
                f"bfr_sofa_{last_year}",
            )
            .select(historical_bfr_columns)
            .withColumnRenamed("TrustUPIN", "Trust UPIN")
            .filter(col("EFALineNo").isin(desired_efa_lines))
        )
        self.bfr_sofa_year_minus_two = (
            self._read_materialized_view(
                f"catalog_30_bronze.bfr.vw_BFR_Sofa_{year_before_last}",
                f"catalog_30_bronze.bfr.vw_BFR_Sofa_{year_before_last}", # Mock source is same as materialized for historical
                f"bfr_sofa_{year_before_last}",
            )
            .select(historical_bfr_columns)
            .withColumnRenamed("TrustUPIN", "Trust UPIN")
            .filter(col("EFALineNo").isin(desired_efa_lines))
        )

        return self.bfr_sofa_year_minus_one, self.bfr_sofa_year_minus_two

    def _get_academies_data(self):
        # Read current year academies data
        logger.info(
            f"Processing BFR academies data: default/{self.year}/academies.parquet"
        )
        academies_path = f"{preprocessed_path}/default/{self.year}/academies.parquet"
        academies = self.spark.read.parquet(academies_path).select(
            "Trust UPIN",
            "Company Registration Number",
            "Trust Revenue reserve",
            "Total pupils in trust",
        )

        logger.info(
            f"Academies preprocessed {self.year=} shape: {academies.count()} rows"
        )

    def _get_historical_academies_data(self):
        # Conditionally read historical data (year - 1)
        academies_y1 = None
        bfr_sofa_year_minus_one = None

        academies_y1_path = (
            f"{preprocessed_path}/default/{self.year - 1}/academies.parquet"
        )
        academies_y1 = try_read_parquet_dbfs(
            academies_y1_path,
            columns=[
                "Trust UPIN",
                "Company Registration Number",
                # Note: Trust Revenue reserve and Total pupils come from SOFA data
            ],
        )

        academies_y2 = None
        academies_y2_path = (
            f"{preprocessed_path}/default/{self.year - 2}/academies.parquet"
        )
        academies_y2 = try_read_parquet_dbfs(
            academies_y2_path,
            columns=[
                "Trust UPIN",
                "Company Registration Number",
                # Note: Trust Revenue reserve and Total pupils come from SOFA data
            ],
        )

    def transform_data(self, bfr_sofa_mv, bfr_three_year_mv):
        # Process BFR historical data
        # academies_y2 = build_bfr_historical_data(
        #     academies_historical=academies_y2,
        #     bfr_sofa_historical=self.bfr_sofa_year_minus_two,
        # )

        # academies_y1 = build_bfr_historical_data(
        #     academies_historical=academies_y1,
        #     bfr_sofa_historical=self.bfr_sofa_year_minus_one,
        # )

        # Build final BFR data
        self.bfr_final, self.bfr_metrics_final = build_bfr_data(
            self.year,
            bfr_sofa_mv,
            bfr_three_year_mv,
            academies,
            academies_y1=None,
            academies_y2=None,
        )

        return self.bfr_final, self.bfr_metrics_final

    def save_data(self, bfr_final: DataFrame, bfr_metrics_final: DataFrame):
        pass
