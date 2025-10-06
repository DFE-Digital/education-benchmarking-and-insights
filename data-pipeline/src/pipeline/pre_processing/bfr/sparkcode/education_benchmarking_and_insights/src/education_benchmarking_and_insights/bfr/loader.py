from pyspark.sql import DataFrame, SparkSession
from pyspark.sql.functions import col

from . import bfr_pyspark_mocks as mocks
from .base import DatabricksDataLoader
from .logging import setup_logger

logger = setup_logger(__name__)


class BFRLoader(DatabricksDataLoader):
    BRONZE_BFR_CATALOG_PATH = "catalog_30_bronze.bfr"
    COPPER_FBIT_CATALOG_PATH = "catalog_40_copper_financial_benchmarking_insights.financial_benchmarking_insights"
    BRONZE_AAR_CATALOG_PATH = "catalog_30_bronze.aar"

    def __init__(self, year: int, spark: SparkSession, pipeline_config):
        self.year = year
        self.spark = spark
        self.config = pipeline_config

    def _get_mock_dataframes(self, table_id: str) -> tuple[DataFrame, DataFrame]:
        """
        Internal helper to get mock materialized and source DataFrames.
        This uses _get_mock_table to fetch individual DataFrames.
        """
        logger.info(f"Using mock data for {table_id}")
        materialized_view = None

        if table_id == "bfr_sofa":
            materialized_view = mocks.get_mock_bfr_sofa_mv(self.spark, self.year)
        elif table_id == "bfr_three_year":
            materialized_view = mocks.get_mock_bfr_three_year_mv(self.spark)
        elif table_id.startswith("bfr_sofa_"):
            historical_year = int(table_id.split("_")[-1])
            materialized_view = mocks.get_mock_bfr_sofa_mv(self.spark, historical_year)
        elif table_id == "academies":
            materialized_view = mocks.get_mock_academies_df(self.spark, self.year)
        elif table_id.startswith("academies_y"):
            year_offset = int(table_id.split("_")[-1].replace("y", ""))
            acad_year = self.year - year_offset
            materialized_view = mocks.get_mock_academies_df(self.spark, acad_year)
        else:
            raise ValueError(f"Unknown mock table_id: {table_id}")

        return materialized_view

    def _read_materialized_view(
        self,
        materialized_view_full_name: str,
        source_view_full_name: str,
        table_id: str,
    ) -> DataFrame:
        """
        Reads a materialized view and checks it against its source view for updates.
        If not in Databricks, it returns mock data.
        """
        if not self._is_databricks():
            materialized_view = self._get_mock_dataframes(table_id)
        else:
            try:
                materialized_view = self.spark.table(
                    self._get_table_name(materialized_view_full_name)
                )
                source_view = self.spark.table(
                    self._get_table_name(source_view_full_name)
                )
            except Exception as e:
                logger.error(
                    f"Failed to load Spark table '{table_id}' from Databricks: {e}"
                )
                raise
            # Only check for updates when running in Databricks (not with mocks)
            self._check_for_updates_in_materialized_views(
                table_id, source_view, materialized_view
            )
        return materialized_view

    def load_data(self) -> tuple[DataFrame, DataFrame]:
        """Load raw materialized views for BFR data"""
        bfr_sofa_mv = self._read_materialized_view(
            f"{self.COPPER_FBIT_CATALOG_PATH}.mv_BFR_Sofa_{self.year}",
            f"{self.BRONZE_BFR_CATALOG_PATH}.vw_BFR_Sofa_{self.year - 1}",
            "bfr_sofa",
        )
        bfr_three_year_mv = self._read_materialized_view(
            f"{self.COPPER_FBIT_CATALOG_PATH}.mv_BFR_Three_Year_Forecast_{self.year}",
            f"{self.BRONZE_BFR_CATALOG_PATH}.vw_BFR_Three_Year_Forecast_{self.year - 1}",
            "bfr_three_year",
        )
        return bfr_sofa_mv, bfr_three_year_mv

    def load_ancillary_data(
        self,
    ) -> tuple[DataFrame, DataFrame, DataFrame, DataFrame, DataFrame]:
        bfr_sofa_minus_one, bfr_sofa_minus_two = self._get_historical_bfr_data()
        academies = self._get_academies_data()
        academies_y1, academies_y2 = self._get_historical_academies_data()

        return (
            bfr_sofa_minus_one,
            bfr_sofa_minus_two,
            academies,
            academies_y1,
            academies_y2,
        )

    def _get_historical_bfr_data(self) -> tuple[DataFrame, DataFrame]:
        last_year = self.year - 1
        year_before_last = self.year - 2
        historical_bfr_columns = ["TrustUPIN", "EFALineNo", "Y1P2", "Y2P2"]
        desired_efa_lines = [
            self.config.SOFA_TRUST_REVENUE_RESERVE_EFALINE,
            self.config.SOFA_PUPIL_NUMBER_EFALINE,
        ]
        bfr_sofa_year_minus_one = (
            self._read_materialized_view(
                f"{self.BRONZE_BFR_CATALOG_PATH}.vw_BFR_Sofa_{last_year}",
                f"{self.BRONZE_BFR_CATALOG_PATH}.vw_BFR_Sofa_{last_year}",
                f"bfr_sofa_{last_year}",
            )
            .select(historical_bfr_columns)
            .withColumnRenamed("TrustUPIN", "Trust UPIN")
            .filter(col("EFALineNo").isin(desired_efa_lines))
        )
        bfr_sofa_year_minus_two = (
            self._read_materialized_view(
                f"{self.BRONZE_BFR_CATALOG_PATH}.vw_BFR_Sofa_{year_before_last}",
                f"{self.BRONZE_BFR_CATALOG_PATH}.vw_BFR_Sofa_{year_before_last}",
                f"bfr_sofa_{year_before_last}",
            )
            .select(historical_bfr_columns)
            .withColumnRenamed("TrustUPIN", "Trust UPIN")
            .filter(col("EFALineNo").isin(desired_efa_lines))
        )

        return bfr_sofa_year_minus_one, bfr_sofa_year_minus_two

    def _get_academies_data(self) -> DataFrame:
        """
        Reads current year academies data.
        If not in Databricks, it returns mock data.
        """
        if not self._is_databricks():
            logger.info("Using mock academies data")
            academies = self._get_mock_dataframes("academies")
        else:
            academies = self._read_materialized_view(
                f"{self.BRONZE_AAR_CATALOG_PATH}.vw_academies_{self.year}",
                f"{self.BRONZE_AAR_CATALOG_PATH}.vw_academies_{self.year}",
                "academies",
            )

        logger.info(
            f"Academies preprocessed {self.year=} shape: {academies.count()} rows"
        )
        return academies

    def _get_historical_academies_data(
        self,
    ) -> tuple[DataFrame, DataFrame]:
        academies_y1 = None
        academies_y2 = None

        if not self._is_databricks():
            logger.info("Using mock historical academies data")
            academies_y1 = self._get_mock_dataframes(f"academies_y1")
            academies_y2 = self._get_mock_dataframes(f"academies_y2")
        else:
            academies_y1 = self._read_materialized_view(
                f"{self.BRONZE_AAR_CATALOG_PATH}.vw_academies_{self.year - 1}",
                f"{self.BRONZE_AAR_CATALOG_PATH}.vw_academies_{self.year - 1}",
                "academies_y1",
            )
            academies_y2 = self._read_materialized_view(
                f"{self.BRONZE_AAR_CATALOG_PATH}.vw_academies_{self.year - 2}",
                f"{self.BRONZE_AAR_CATALOG_PATH}.vw_academies_{self.year - 2}",
                "academies_y2",
            )
        return academies_y1, academies_y2
