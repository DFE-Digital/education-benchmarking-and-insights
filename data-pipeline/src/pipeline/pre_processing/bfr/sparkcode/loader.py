from pyspark.sql import DataFrame, SparkSession
from pyspark.sql.functions import col

from . import bfr_pyspark_mocks as mocks
from .base_pipeline import DatabricksFBITPipeline
from .logging import setup_logger

logger = setup_logger(__name__)


class BFRLoader:
    def __init__(
        self,
        year: int,
        spark: SparkSession,
        pipeline_config,
        base_pipeline_helpers: DatabricksFBITPipeline,
    ):
        self.year = year
        self.spark = spark
        self.config = pipeline_config
        self.base_pipeline_helpers = base_pipeline_helpers  # Instance to access _is_databricks and _get_table_name

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
        if not self.base_pipeline_helpers._is_databricks():
            if table_id == "bfr_sofa":
                materialized_view = mocks.get_mock_bfr_sofa_mv(self.spark, self.year)
                source_view = mocks.get_mock_bfr_sofa_mv(
                    self.spark, self.year - 1
                )  # Mock source for comparison
            elif table_id == "bfr_three_year":
                materialized_view = mocks.get_mock_bfr_three_year_mv(self.spark)
                source_view = mocks.get_mock_bfr_three_year_mv(
                    self.spark
                )  # Mock source for comparison
            elif table_id.startswith("bfr_sofa_"):  # Historical BFR SOFA
                year = int(table_id.split("_")[-1])
                materialized_view = mocks.get_mock_bfr_sofa_mv(self.spark, year)
                source_view = mocks.get_mock_bfr_sofa_mv(
                    self.spark, year - 1
                )  # Mock source for comparison
            elif table_id == "academies":
                materialized_view = mocks.get_mock_academies_df(self.spark, self.year)
                source_view = mocks.get_mock_academies_df(self.spark, self.year)
            elif table_id.startswith("academies_y"):  # Historical Academies
                year_offset = int(table_id.split("_")[-1].replace("y", ""))
                acad_year = self.year - year_offset
                materialized_view = mocks.get_mock_academies_df(self.spark, acad_year)
                source_view = mocks.get_mock_academies_df(self.spark, acad_year)
            else:
                raise ValueError(f"Unknown mock table_id: {table_id}")
            logger.info(f"Using mock data for {table_id}")
        else:
            materialized_view = self.spark.table(
                self.base_pipeline_helpers._get_table_name(materialized_view_full_name)
            )
            source_view = self.spark.table(
                self.base_pipeline_helpers._get_table_name(source_view_full_name)
            )

        self.base_pipeline_helpers._check_for_updates_in_materialized_views(
            table_id, source_view, materialized_view
        )
        return materialized_view

    def load_data(self) -> tuple[DataFrame, DataFrame]:
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
                f"catalog_30_bronze.bfr.vw_BFR_Sofa_{last_year}",
                f"catalog_30_bronze.bfr.vw_BFR_Sofa_{last_year}",  # Mock source is same as materialized for historical
                f"bfr_sofa_{last_year}",
            )
            .select(historical_bfr_columns)
            .withColumnRenamed("TrustUPIN", "Trust UPIN")
            .filter(col("EFALineNo").isin(desired_efa_lines))
        )
        bfr_sofa_year_minus_two = (
            self._read_materialized_view(
                f"catalog_30_bronze.bfr.vw_BFR_Sofa_{year_before_last}",
                f"catalog_30_bronze.bfr.vw_BFR_Sofa_{year_before_last}",  # Mock source is same as materialized for historical
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
        if not self.base_pipeline_helpers._is_databricks():
            logger.info("Using mock academies data")
            academies = mocks.get_mock_academies_df(self.spark, self.year)
        else:
            academies = self._read_materialized_view(
                f"catalog_30_bronze.aar.vw_academies_{self.year}",
                f"catalog_30_bronze.aar.vw_academies_{self.year}",
                "academies",
            )

        logger.info(
            f"Academies preprocessed {self.year=} shape: {academies.count()} rows"
        )
        return academies

    def _get_historical_academies_data(
        self,
    ) -> tuple[DataFrame | None, DataFrame | None]:
        # Conditionally read historical data (year - 1)
        academies_y1 = None
        academies_y2 = None

        if not self.base_pipeline_helpers._is_databricks():
            logger.info("Using mock historical academies data")
            academies_y1 = mocks.get_mock_academies_df(self.spark, self.year - 1)
            academies_y2 = mocks.get_mock_academies_df(self.spark, self.year - 2)
        else:
            academies_y1 = self._read_materialized_view(
                f"catalog_30_bronze.aar.vw_academies_{self.year - 1}",
                f"catalog_30_bronze.aar.vw_academies_{self.year - 1}",
                "academies_y1",
            )
            academies_y2 = self._read_materialized_view(
                f"catalog_30_bronze.aar.vw_academies_{self.year - 2}",
                f"catalog_30_bronze.aar.vw_academies_{self.year - 2}",
                "academies_y2",
            )
        return academies_y1, academies_y2
