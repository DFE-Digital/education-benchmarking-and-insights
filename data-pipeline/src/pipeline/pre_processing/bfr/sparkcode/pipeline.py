from pyspark.sql import DataFrame, SparkSession
from pyspark.sql.functions import (
    col,
    create_map,
    first,
    lit,
    nanvl,
    coalesce,
    expr,
    sum as spark_sum,
    when
)
import numpy as np
import pandas as pd
from pyspark.sql.types import (
    DoubleType,
    IntegerType,
    StructField,
    StructType,
)

from pipeline.pre_processing.bfr import config

from . import bfr_pyspark_mocks as mocks
import pandas as pd
from .base_pipeline import DatabricksFBITPipeline
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
        merged_bfr_with_crn = self.preprocess_data(
            bfr_sofa_mv, bfr_three_year_mv, academies
        )
        bfr_forecast_and_risk_rows, bfr_forecast_and_risk_metrics = (
            self.get_bfr_forecast_and_risk_data_spark(
                merged_bfr_with_crn,
                bfr_sofa_year_minus_two,
                bfr_sofa_year_minus_one,
                self.year,
                academies,
            )
        )
        # IT spend breakdown was introduced from the 2025 return
        if self.year > 2024:
            bfr_it_spend_rows = self.get_bfr_it_spend_rows_spark(merged_bfr_with_crn, self.year)
            bfr_final_long = bfr_forecast_and_risk_rows.unionByName(bfr_it_spend_rows)
            return bfr_final_long, bfr_forecast_and_risk_metrics
        return bfr_forecast_and_risk_rows, bfr_forecast_and_risk_metrics

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

    def preprocess_data(
        self, bfr_sofa_mv: DataFrame, bfr_three_year_mv: DataFrame, academies: DataFrame
    ):
        bfr_sofa_preprocessed = self.preprocess_bfr_sofa_spark(bfr_sofa_mv)
        bfr_3y_preprocessed = self.preprocess_bfr_3y_spark(bfr_three_year_mv)

        merged_bfr = bfr_sofa_preprocessed.join(
            bfr_3y_preprocessed, on=["Trust UPIN", "EFALineNo"], how="left_outer"
        )
        # Add CRN from academies
        merged_bfr_with_crn = (
            academies.select("Company Registration Number", "Trust UPIN")
            .dropDuplicates(subset=["Trust UPIN"])
            .join(merged_bfr, on="Trust UPIN", how="inner")
        )
        return merged_bfr_with_crn

    def aggregate_efalines_over_years_spark(
        self,
        bfr: DataFrame,
        efa_lines: list[int],
        year_cols: list[str],
        aggregated_category_name: str,
    ) -> DataFrame:
        """Aggregates specified EFA lines over given year."""
        bfr_aggregated_category_rows = (
            bfr.filter(col("EFALineNo").isin(efa_lines))
            .groupBy("Trust UPIN")
            .agg(*[spark_sum(col(c)).alias(c) for c in year_cols])
            .withColumn("Category", lit(aggregated_category_name))
            .withColumn("EFALineNo", lit(None).cast(IntegerType()))
        )
        return bfr_aggregated_category_rows

    def preprocess_bfr_sofa_spark(self, bfr_sofa_mv: DataFrame) -> DataFrame:
        bfr_sofa_mv = bfr_sofa_mv.withColumnRenamed("TrustUPIN", "Trust UPIN")
        bfr_sofa_mv = bfr_sofa_mv.withColumnRenamed("Title", "Category")
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

        bfr_sofa_filtered = bfr_sofa_mv.filter(
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

    def preprocess_bfr_3y_spark(self, bfr_3y_mv: DataFrame) -> DataFrame:
        """
        Preprocesses BFR 3-year data using Spark DataFrame operations.
        """
        bfr_3y_mv = bfr_3y_mv.withColumnRenamed("TrustUPIN", "Trust UPIN")
        # Normalise line numbers between SOFA/3Y
        efa_line_mapping_expr = create_map(
            [
                lit(x)
                for i, x in enumerate(sum(config.BFR_3Y_TO_SOFA_MAPPINGS.items(), ()))
            ]
        )[col("EFALineNo")]

        bfr_3y_normalized = bfr_3y_mv.withColumn(
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

    def get_bfr_forecast_and_risk_data_spark(
        self,
        merged_bfr_with_crn: DataFrame,
        historic_bfr_y2: DataFrame,
        historic_bfr_y1: DataFrame,
        current_year: int,
        academies: DataFrame,
    ):
        """
        Orchestrates the preparation of data and calculation of forecast and risk metrics and rows.
        """
        merged_bfr_with_2y_historic_data = self._prepare_merged_bfr_for_forecast_and_risk_spark(
            merged_bfr_with_crn, historic_bfr_y2, historic_bfr_y1
        )

        bfr_forecast_and_risk_metrics = self._calculate_forecast_and_risk_metrics_spark(
            merged_bfr_with_2y_historic_data
        )

        bfr_pupils = self._prepare_current_and_future_pupils_spark(
            bfr_data=merged_bfr_with_2y_historic_data, academies=academies
        )
        bfr_final_wide = merged_bfr_with_2y_historic_data.join(
            bfr_pupils, on="Trust UPIN", how="left_outer"
        )

        forecast_and_risk_revenue_reserve_rows = (
            self._melt_forecast_and_risk_revenue_reserves_from_bfr_spark(
                bfr_final_wide, current_year
            )
        )
        forecast_and_risk_pupil_numbers_melted_rows = (
            self._melt_forecast_and_risk_pupil_numbers_from_bfr_spark(
                bfr_final_wide, current_year
            )
        )
        bfr_forecast_and_risk_rows = forecast_and_risk_revenue_reserve_rows.join(
            forecast_and_risk_pupil_numbers_melted_rows,
            on=["Company Registration Number", "Category", "Year"],
            how="left_outer",
        )

        return bfr_forecast_and_risk_rows, bfr_forecast_and_risk_metrics

    def _calculate_forecast_and_risk_metrics_spark(self, merged_bfr_with_2y_historic_data: DataFrame) -> DataFrame:
        """
        Calculates the forecast and risk metrics from the merged BFR data.
        """
        bfr_rows_for_normalised_finance_metrics = merged_bfr_with_2y_historic_data.filter(
            col("Category").isin(
                [
                    "Total income",
                    "Revenue reserve",
                    "Staff costs",
                    "Total expenditure",
                    "Self-generated income",
                ]
            )
        )
        bfr_forecast_and_risk_metrics = self._calculate_metrics_spark(
            bfr_rows_for_normalised_finance_metrics
        )
        bfr_rows_for_slope_analysis = merged_bfr_with_2y_historic_data.filter(
            col("Category") == "Revenue reserve"
        )
        bfr_forecast_and_risk_slope_analysis = self._slope_analysis_spark(
            bfr_rows_for_slope_analysis
        )
        bfr_forecast_and_risk_metrics = bfr_forecast_and_risk_metrics.unionByName(
            bfr_forecast_and_risk_slope_analysis
        )
        return bfr_forecast_and_risk_metrics

    def _prepare_current_and_future_pupils_spark(
        self, bfr_data: DataFrame, academies: DataFrame
    ) -> DataFrame:
        """
        Prepares current and future pupil numbers using Spark.
        The current year BFR_SOFA (Y2) doesn't have current year pupil numbers as
        it is released halfway through the year, so we get them from the academies
        data (aka the academy year census). Future years come from BFR_3Y.
        """
        bfr_pupils = bfr_data.filter(col("Category") == "Pupil numbers").select(
            "Trust UPIN", col("Y2").alias("Pupils Y2"), col("Y3").alias("Pupils Y3"), col("Y4").alias("Pupils Y4")
        )

        academies_pupils = (
            academies.select("Trust UPIN", col("Total pupils in trust").alias("Pupils Y1"))
            .groupBy("Trust UPIN")
            .agg(first("Pupils Y1").alias("Pupils Y1"))
        )

        bfr_pupils = bfr_pupils.join(academies_pupils, on="Trust UPIN", how="left_outer")
        return bfr_pupils

    def _melt_forecast_and_risk_pupil_numbers_from_bfr_spark(
        self, bfr: DataFrame, current_year: int
    ) -> DataFrame:
        """Melt pupil numbers for forecast and risk calculations using Spark."""
        id_vars = ["Company Registration Number", "Category"]
        value_vars = [
            "Pupils Y-2",
            "Pupils Y-1",
            "Pupils Y1",
            "Pupils Y2",
            "Pupils Y3",
            "Pupils Y4",
        ]
        # Using stack to melt the DataFrame
        forecast_and_risk_pupil_numbers_melted_rows = bfr.select(
            *id_vars,
            expr(
                f"stack({len(value_vars)}, "
                + ", ".join([f"'{col_name}', `{col_name}`" for col_name in value_vars])
                + ") as (Year, Pupils)"
            ),
        ).withColumn(
            "Year",
            when(col("Year") == "Pupils Y-2", current_year - 2)
            .when(col("Year") == "Pupils Y-1", current_year - 1)
            .when(col("Year") == "Pupils Y1", current_year)
            .when(col("Year") == "Pupils Y2", current_year + 1)
            .when(col("Year") == "Pupils Y3", current_year + 2)
            .when(col("Year") == "Pupils Y4", current_year + 3)
            .otherwise(col("Year")),
        )
        return forecast_and_risk_pupil_numbers_melted_rows.orderBy(
            "Company Registration Number", "Year"
        )

    def _melt_forecast_and_risk_revenue_reserves_from_bfr_spark(
        self, bfr: DataFrame, current_year: int
    ) -> DataFrame:
        """Melt revenue reserves for forecast and risk calculations using Spark."""
        id_vars = ["Company Registration Number", "Category"]
        value_vars = ["Y-2", "Y-1", "Y1", "Y2", "Y3", "Y4"]
        # Filter and then melt the DataFrame
        forecast_and_risk_revenue_reserves_melted_rows = bfr.filter(
            col("EFALineNo").isin([self.config.SOFA_TRUST_REVENUE_RESERVE_EFALINE])
        ).select(
            *id_vars,
            expr(
                f"stack({len(value_vars)}, "
                + ", ".join([f"'{col_name}', `{col_name}`" for col_name in value_vars])
                + ") as (Year, Value)"
            ),
        ).withColumn(
            "Year",
            when(col("Year") == "Y-2", current_year - 2)
            .when(col("Year") == "Y-1", current_year - 1)
            .when(col("Year") == "Y1", current_year)
            .when(col("Year") == "Y2", current_year + 1)
            .when(col("Year") == "Y3", current_year + 2)
            .when(col("Year") == "Y4", current_year + 3)
            .otherwise(col("Year")),
        )
        return forecast_and_risk_revenue_reserves_melted_rows.orderBy(
            "Company Registration Number", "Year"
        )

    def _merge_historic_bfr_spark(
        self, bfr: DataFrame, historic_bfr: DataFrame, year: str
    ) -> DataFrame:
        """Merges historic BFR data into the main BFR DataFrame using Spark."""
        if historic_bfr is not None and historic_bfr.count() > 0:
            # Pivot historic_bfr to get 'Trust Revenue reserve' and 'Total pupils in trust' as columns
            # from Y2P2 based on EFALineNo
            pivot_df = historic_bfr.groupBy("Trust UPIN").pivot(
                "EFALineNo",
                [
                    self.config.SOFA_TRUST_REVENUE_RESERVE_EFALINE,
                    self.config.SOFA_PUPIL_NUMBER_EFALINE,
                ],
            ).agg(first("Y2P2"))

            # Rename columns to match expected names
            historic_bfr_selected = pivot_df.withColumnRenamed(
                str(self.config.SOFA_TRUST_REVENUE_RESERVE_EFALINE), year
            ).withColumnRenamed(
                str(self.config.SOFA_PUPIL_NUMBER_EFALINE), f"Pupils {year}"
            ).select("Trust UPIN", year, f"Pupils {year}")

            bfr = bfr.join(historic_bfr_selected, on="Trust UPIN", how="left_outer")

            # Fill nulls after join with 0.0 to replicate the 'historic_bfr is None' logic
            bfr = bfr.withColumn(year, coalesce(col(year), lit(0.0)))
            bfr = bfr.withColumn(f"Pupils {year}", coalesce(col(f"Pupils {year}"), lit(0.0)))
        else:
            # If historic_bfr is None or empty, add columns with 0.0
            bfr = bfr.withColumn(year, lit(0.0).cast(DoubleType()))
            bfr = bfr.withColumn(f"Pupils {year}", lit(0.0).cast(DoubleType()))
        return bfr

    def _prepare_merged_bfr_for_forecast_and_risk_spark(
        self, merged_bfr_with_crn: DataFrame, historic_bfr_y2: DataFrame, historic_bfr_y1: DataFrame
    ) -> DataFrame:
        """
        Prepares the merged BFR data by adding historic data for forecast and risk calculations.
        """
        merged_bfr_with_1y_historic_data = self._merge_historic_bfr_spark(
            merged_bfr_with_crn, historic_bfr_y2, "Y-2"
        )
        merged_bfr_with_2y_historic_data = self._merge_historic_bfr_spark(
            merged_bfr_with_1y_historic_data, historic_bfr_y1, "Y-1"
        )
        merged_bfr_with_2y_historic_data = merged_bfr_with_2y_historic_data.withColumn(
            "Y1", col("Y2P2")
        )
        return merged_bfr_with_2y_historic_data

    def _calculate_metrics_spark(self, bfr: DataFrame) -> DataFrame:
        """Calculates financial metrics using Spark DataFrames."""
        costs_list = [
            "Total income",
            "Staff costs",
            "Total expenditure",
            "Self-generated income",
        ]

        bfr_with_metric = bfr.withColumn(
            "metric",
            when(col("Category").isin(costs_list), col("Y2P1") + col("Y2P2")).otherwise(
                col("Y2P2")
            ),
        ).select("Company Registration Number", "Category", "metric")

        # Pivot the DataFrame
        pivot_df = bfr_with_metric.groupBy("Company Registration Number").pivot(
            "Category",
            [
                "Total income",
                "Revenue reserve",
                "Staff costs",
                "Total expenditure",
                "Self-generated income",
            ],
        ).agg(first("metric"))

        # Calculate metrics
        df_metrics = pivot_df.withColumn(
            "Revenue reserve as percentage of income",
            (col("Revenue reserve") / col("Total income")) * 100,
        ).withColumn(
            "Staff costs as percentage of income",
            (col("Staff costs") / col("Total income")) * 100,
        ).withColumn(
            "Expenditure as percentage of income",
            (col("Total expenditure") / col("Total income")) * 100,
        ).withColumn(
            "Self generated income as percentage of income",
            (col("Self-generated income") / col("Total income")) * 100,
        ).withColumn(
            "Grant funding as percentage of income",
            lit(100) - col("Self generated income as percentage of income"),
        )

        # Melt the DataFrame back
        metric_cols = [
            "Revenue reserve as percentage of income",
            "Staff costs as percentage of income",
            "Expenditure as percentage of income",
            "Self generated income as percentage of income",
            "Grant funding as percentage of income",
        ]

        melted_df = df_metrics.select(
            col("Company Registration Number"),
            expr(
                f"stack({len(metric_cols)}, "
                + ", ".join([f"'{c}', `{c}`" for c in metric_cols])
                + ") as (Category, Value)"
            ),
        )

        # Replace inf, -inf, nan with 0.0
        final_df = melted_df.withColumn(
            "Value",
            when(
                col("Value").isin([float("inf"), float("-inf")]), lit(0.0)
            ).otherwise(nanvl(col("Value"), lit(0.0))),
        )

        return final_df

    def _calculate_slopes_spark(self, bfr: DataFrame, year_cols: list[str]) -> DataFrame:
        """Calculates slopes for revenue reserves using a Pandas UDF in Spark."""
        import numpy as np
        import pandas as pd
        from pyspark.sql.functions import array, pandas_udf
        from pyspark.sql.types import DoubleType

        @pandas_udf(DoubleType())
        def calculate_slopes_udf(year_values: pd.Series) -> pd.Series:
            x = np.array([1, 2, 3, 4, 5, 6])
            x_bar = 3.5
            x_x_bar = x - x_bar
            # Ensure year_values is treated as a 2D array for consistent operations
            matrix = year_values.values.reshape(-1, len(year_cols))
            y_bar = np.nanmean(matrix, axis=1)
            y_y_bar = matrix - np.vstack(y_bar)
            # Perform element-wise multiplication and sum
            numerator = np.nansum(x_x_bar * y_y_bar, axis=1)
            denominator = np.nansum(x_x_bar**2)
            slope_array = numerator / denominator
            return pd.Series(slope_array)

        # Apply the UDF row-wise
        bfr_with_slopes = bfr.withColumn(
            "Slope", calculate_slopes_udf(array(*[col(c) for c in year_cols]))
        )
        return bfr_with_slopes

    def _assign_slope_flag_spark(self, bfr_with_slopes: DataFrame) -> DataFrame:
        """Assigns slope flags based on percentiles using a Pandas UDF in Spark."""
        # Define the schema for the output of the UDF, including the new 'Slope flag' column
        output_schema = StructType(bfr_with_slopes.schema.fields + [StructField("Slope flag", IntegerType(), True)])

        def assign_slope_flag_func(pdf: pd.DataFrame) -> pd.DataFrame:
            percentile_10 = np.nanpercentile(pdf["Slope"].values, 10)
            percentile_90 = np.nanpercentile(pdf["Slope"].values, 90)
            pdf["Slope flag"] = 0
            pdf.loc[pdf["Slope"] < percentile_10, "Slope flag"] = -1
            pdf.loc[pdf["Slope"] > percentile_90, "Slope flag"] = 1
            return pdf

        # Apply the grouped map UDF
        bfr_with_slope_flag = bfr_with_slopes.groupBy("Company Registration Number").applyInPandas(
            assign_slope_flag_func, schema=output_schema
        )
        return bfr_with_slope_flag

    def _slope_analysis_spark(self, bfr: DataFrame) -> DataFrame:
        """Performs slope analysis on BFR data using Spark."""
        year_columns = ["Y-2", "Y-1", "Y1", "Y2", "Y3", "Y4"]

        # Calculate slopes
        bfr_with_slopes = self._calculate_slopes_spark(bfr, year_columns)

        # Assign slope flags
        bfr_with_slope_flag = self._assign_slope_flag_spark(bfr_with_slopes)

        # Melt the DataFrame
        melted_df = bfr_with_slope_flag.select(
            "Company Registration Number",
            expr(
                "stack(2, 'Slope', Slope, 'Slope flag', CAST(`Slope flag` AS DOUBLE)) as (Category, Value)"
            ),
        ).withColumnRenamed("Category", "Category")

        return melted_df.orderBy("Company Registration Number")

    def save_data(self):
        pass

    def _melt_it_spend_rows_from_bfr_spark(self, bfr: DataFrame, current_year: int) -> DataFrame:
        """Melt IT spend rows from BFR data using Spark."""
        it_spend_melted_rows = (
            bfr.filter(col("EFALineNo").isin(self.config.SOFA_IT_SPEND_LINES))
            .withColumn("Y1P_Total", col("Y1P1") + col("Y1P2"))
            .withColumn("Y2P_Total", col("Y2P1") + col("Y2P2"))
            .withColumn("Y3P_Total", col("Y3P1") + col("Y3P2"))
            .select(
                "Category",
                "Company Registration Number",
                expr(
                    f"stack(3, 'Y1P_Total', `Y1P_Total`, 'Y2P_Total', `Y2P_Total`, 'Y3P_Total', `Y3P_Total`) as (Year, Value)"
                ),
            )
            .withColumn(
                "Year",
                when(col("Year") == "Y1P_Total", current_year - 1)
                .when(col("Year") == "Y2P_Total", current_year)
                .when(col("Year") == "Y3P_Total", current_year + 1)
                .otherwise(col("Year")),
            )
        )
        return it_spend_melted_rows.orderBy("Company Registration Number", "Year")

    def _melt_it_spend_pupil_numbers_from_bfr_spark(
        self, bfr: DataFrame, current_year: int
    ) -> DataFrame:
        """Melt IT spend pupil numbers from BFR data using Spark."""
        it_spend_pupil_numbers_melted_rows = (
            bfr.filter(col("EFALineNo").isin([self.config.SOFA_PUPIL_NUMBER_EFALINE]))
            .select(
                "Company Registration Number",
                expr(
                    f"stack(3, 'Y1P1', `Y1P1`, 'Y1P2', `Y1P2`, 'Y2P1', `Y2P1`) as (Year, Pupils)"
                ),
            )
            .withColumn(
                "Year",
                when(col("Year") == "Y1P1", current_year - 1)
                .when(col("Year") == "Y1P2", current_year)
                .when(col("Year") == "Y2P1", current_year + 1)
                .otherwise(col("Year")),
            )
        )
        return it_spend_pupil_numbers_melted_rows.orderBy(
            "Company Registration Number", "Year"
        )

    def get_bfr_it_spend_rows_spark(
        self, bfr_final_wide: DataFrame, current_year: int
    ) -> DataFrame:
        """Gets BFR IT spend rows using Spark."""
        bfr_it_spend_melted_rows = self._melt_it_spend_rows_from_bfr_spark(
            bfr_final_wide, current_year
        )
        bfr_it_spend_pupil_numbers = self._melt_it_spend_pupil_numbers_from_bfr_spark(
            bfr_final_wide, current_year
        )
        bfr_it_spend_final = bfr_it_spend_melted_rows.join(
            bfr_it_spend_pupil_numbers,
            on=["Company Registration Number", "Year"],
            how="left_outer",
        )
        return bfr_it_spend_final
