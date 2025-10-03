from pyspark.sql import DataFrame, SparkSession
from pyspark.sql.functions import col, create_map, lit
from pyspark.sql.functions import sum as spark_sum
from pyspark.sql.functions import when
from pyspark.sql.types import IntegerType

from . import config


class BFRPreprocessor:
    def __init__(self, year: int, spark: SparkSession, config):
        self.year = year
        self.spark = spark
        self.config = config

    def preprocess_data(
        self, bfr_sofa_mv: DataFrame, bfr_three_year_mv: DataFrame, academies: DataFrame
    ):
        bfr_sofa_preprocessed = self.preprocess_bfr_sofa(bfr_sofa_mv)
        bfr_3y_preprocessed = self.preprocess_bfr_3y(bfr_three_year_mv)

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

    def aggregate_efalines_over_years(
        self,
        bfr: DataFrame,
        efa_lines: list[int],
        year_cols: list[str],
        aggregated_category_name: str,
    ) -> DataFrame:
        """Aggregates specified EFA lines over given year."""
        filtered_bfr_for_aggregation = bfr.filter(col("EFALineNo").isin(efa_lines))
        bfr_aggregated_category_rows = (
            filtered_bfr_for_aggregation.groupBy("Trust UPIN")
            .agg(*[spark_sum(col(c)).alias(c) for c in year_cols])
            .withColumn("Category", lit(aggregated_category_name))
            .withColumn("EFALineNo", lit(None).cast(IntegerType()))
        )

        return bfr_aggregated_category_rows

    def preprocess_bfr_sofa(self, bfr_sofa_mv: DataFrame) -> DataFrame:
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
        self_gen_income = self.aggregate_efalines_over_years(
            bfr_sofa_filtered,
            config.SOFA_SELF_GENERATED_INCOME_EFALINES,
            sofa_year_cols,
            "Self-generated income",
        )
        grant_funding = self.aggregate_efalines_over_years(
            bfr_sofa_filtered,
            config.SOFA_GRANT_FUNDING_EFALINES,
            sofa_year_cols,
            "Grant funding",
        )

        # Filter out the individual EFALines that have been aggregated
        lines_to_remove_after_aggregation = (
            config.SOFA_SELF_GENERATED_INCOME_EFALINES
            + config.SOFA_GRANT_FUNDING_EFALINES
        )
        bfr_sofa_filtered_without_original_aggregated_lines = bfr_sofa_filtered.filter(
            ~col("EFALineNo").isin(lines_to_remove_after_aggregation)
        )

        bfr_sofa_with_aggregated_categories = (
            bfr_sofa_filtered_without_original_aggregated_lines.unionByName(
                self_gen_income
            )
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

    def preprocess_bfr_3y(self, bfr_3y_mv: DataFrame) -> DataFrame:
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
