from pyspark.sql import DataFrame, SparkSession
from pyspark.sql.functions import (
    asc,
    broadcast,
    col,
    concat_ws,
    create_map,
    desc,
    first,
    isnan,
    isnull,
    lit,
    regexp_replace,
    sum as spark_sum,
    when,
)
from pyspark.sql.types import (
    DoubleType,
    IntegerType,
    StringType,
    StructField,
    StructType,
)

from pipeline.pre_processing.bfr import config

from . import bfr_pyspark_mocks as mocks
from .base_pipeline import DatabricksFBITPipeline
from .engine import build_bfr_data
from .logging import setup_logger

logger = setup_logger(__name__)


class BFRPipeline(DatabricksFBITPipeline):
    def __init__(self, year: int, spark: SparkSession):
        self.year = year
        self.config = config
        self.spark = spark

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
        (
            bfr_sofa_year_minus_one,
            bfr_sofa_year_minus_two,
            academies,
            academies_y1,
            academies_y2,
        ) = self.load_ancillary_data()
        bfr_final, bfr_metrics_final = self.transform_data(
            bfr_sofa_mv, bfr_three_year_mv, academies
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

        # Rename TrustUPIN to Trust UPIN for consistency with preprocessing functions
        bfr_sofa_mv = bfr_sofa_mv.withColumnRenamed("TrustUPIN", "Trust UPIN")
        bfr_three_year_mv = bfr_three_year_mv.withColumnRenamed("TrustUPIN", "Trust UPIN")

        # Rename 'Title' to 'Category' for bfr_sofa_mv as expected by preprocessing functions
        bfr_sofa_mv = bfr_sofa_mv.withColumnRenamed("Title", "Category")

        return bfr_sofa_mv, bfr_three_year_mv

    def load_ancillary_data(self):
        # academies = self._get_academies_data()
        # academies_y1, academies_y2 = self._get_historical_academies_data()
        # Only get historical bfr data if there's the corresponding academies year
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

    def _get_historical_bfr_data(self):
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
        if not self._is_databricks():
            logger.info("Using mock academies data")
            academies = mocks.get_mock_academies_df(self.spark, self.year)
        else:
            pass
            # academies = (
            #     self._read_materialized_view(
            #         f"catalog_30_bronze.aar.vw_academies_{year_before_last}",
            #         f"catalog_30_bronze.aar.vw_academies_{year_before_last}",  # Mock source is same as materialized for historical
            #         f"academies",
            #     )
            #     .select(historical_bfr_columns)
            #     .withColumnRenamed("TrustUPIN", "Trust UPIN")
            #     .filter(col("EFALineNo").isin(desired_efa_lines))
            # )

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

        if not self._is_databricks():
            logger.info("Using mock historical academies data")
            academies_y1 = mocks.get_mock_academies_df(self.spark, self.year - 1)
            academies_y2 = mocks.get_mock_academies_df(self.spark, self.year - 2)
        else:
            pass
            # academies = (
            #     self._read_materialized_view(
            #         f"catalog_30_bronze.aar.vw_academies_{year_before_last}",
            #         f"catalog_30_bronze.aar.vw_academies_{year_before_last}",  # Mock source is same as materialized for historical
            #         f"academies",
            #     )
            #     .select(historical_bfr_columns)
            #     .withColumnRenamed("TrustUPIN", "Trust UPIN")
            #     .filter(col("EFALineNo").isin(desired_efa_lines))
            # )
        return academies_y1, academies_y2

    def transform_data(
        self, bfr_sofa_mv: DataFrame, bfr_three_year_mv: DataFrame, academies: DataFrame
    ):
        bfr_sofa_preprocessed = self.preprocess_bfr_sofa_spark(bfr_sofa_mv)
        bfr_3y_preprocessed = self.preprocess_bfr_3y_spark(bfr_three_year_mv)
        return bfr_3y_preprocessed, bfr_sofa_preprocessed

    def aggregate_efalines_over_years_spark(
        self,
        bfr: DataFrame,
        efa_lines: list[int],
        year_cols: list[str],
        aggregated_category_name: str
    ) -> DataFrame:
        """
        Aggregates specified EFA lines over given year columns in a Spark DataFrame.
        """
        bfr_aggregated_category_rows = (
            bfr.filter(col("EFALineNo").isin(efa_lines))
            .groupBy("Trust UPIN")
            .agg(*[spark_sum(col(c)).alias(c) for c in year_cols])
            .withColumn("Category", lit(aggregated_category_name))
            .withColumn("EFALineNo", lit(None).cast(IntegerType()))
        )
        return bfr_aggregated_category_rows

    def preprocess_bfr_sofa_spark(
        self, bfr_sofa_raw: DataFrame
    ) -> DataFrame:
        """
        Preprocesses BFR SOFA data using Spark DataFrame operations.
        """
        sofa_efa_lines_to_filter = [
            *self.config.SOFA_SELF_GENERATED_INCOME_EFALINES,
            self.config.SOFA_PUPIL_NUMBER_EFALINE,
            *self.config.SOFA_GRANT_FUNDING_EFALINES,
            self.config.SOFA_TRUST_REVENUE_RESERVE_EFALINE,
            self.config.SOFA_SUBTOTAL_INCOME_EFALINE,
            self.config.SOFA_OTHER_COSTS_EFALINE,
            self.config.SOFA_TOTAL_REVENUE_INCOME,
            self.config.SOFA_TOTAL_REVENUE_EXPENDITURE,
            *self.config.SOFA_IT_SPEND_LINES,
        ]

        bfr_sofa_filtered = bfr_sofa_raw.filter(
            col("EFALineNo").isin(sofa_efa_lines_to_filter)
        )

        # Scale monetary values by 1000, excluding pupil numbers
        sofa_year_cols = config.get_sofa_year_cols(self.year)
        for col_name in sofa_year_cols:
            bfr_sofa_filtered = bfr_sofa_filtered.withColumn(
                col_name,
                when(
                    col("EFALineNo") != config.SOFA_PUPIL_NUMBER_EFALINE,
                    col(col_name) * 1000,
                ).otherwise(col(col_name)),
            )

        # Aggregate custom SOFA categories
        self_gen_income = self.aggregate_efalines_over_years_spark(
            bfr_sofa_filtered,
            config.SOFA_SELF_GENERATED_INCOME_EFALINES,
            sofa_year_cols,
            "Self-generated income",
        )
        grant_funding = self.aggregate_efalines_over_years_spark(
            bfr_sofa_filtered,
            config.SOFA_GRANT_FUNDING_EFALINES,
            sofa_year_cols,
            "Grant funding",
        )

        bfr_sofa_with_aggregated_categories = (
            bfr_sofa_filtered.unionByName(self_gen_income)
            .unionByName(grant_funding)
            .dropDuplicates(["Trust UPIN", "EFALineNo", "Category"])
        )

        # Rename categories for FBIT
        category_replacement_expr = create_map(
            [
                lit(x)
                for i, x in enumerate(sum(config.BFR_CATEGORY_MAPPINGS.items(), ()))
            ]
        )[col("Category")]

        bfr_sofa_with_aggregated_categories = (
            bfr_sofa_with_aggregated_categories.withColumn(
                "Category",
                when(
                    category_replacement_expr.isNotNull(), category_replacement_expr
                ).otherwise(col("Category")),
            )
        )

        return bfr_sofa_with_aggregated_categories

    def preprocess_bfr_3y_spark(self, bfr_3y_raw: DataFrame) -> DataFrame:
        """
        Preprocesses BFR 3-year data using Spark DataFrame operations.
        """
        # Normalise line numbers between SOFA/3Y
        efa_line_mapping_expr = create_map(
            [
                lit(x)
                for i, x in enumerate(sum(config.BFR_3Y_TO_SOFA_MAPPINGS.items(), ()))
            ]
        )[col("EFALineNo")]

        bfr_3y_normalized = bfr_3y_raw.withColumn(
            "EFALineNo",
            when(efa_line_mapping_expr.isNotNull(), efa_line_mapping_expr).otherwise(
                col("EFALineNo")
            ),
        )

        # Filter 3Y
        bfr_3y_filtered = bfr_3y_normalized.filter(
            col("EFALineNo").isin(
                [*config.BFR_3Y_TO_SOFA_MAPPINGS.values(), config.OTHER_COSTS_EFALINE]
            )
        )

        # Scale monetary values by 1000
        for col_name in config.THREE_YEAR_PROJECTION_COLS:
            bfr_3y_filtered = bfr_3y_filtered.withColumn(
                col_name,
                when(
                    col("EFALineNo") != config.SOFA_PUPIL_NUMBER_EFALINE,
                    col(col_name) * 1000,
                ).otherwise(col(col_name)),
            )

        return bfr_3y_filtered

    def save_data(self, bfr_final: DataFrame, bfr_metrics_final: DataFrame):
        pass
