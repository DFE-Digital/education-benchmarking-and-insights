from pyspark.sql.functions import col, create_map, lit
from pyspark.sql.functions import sum as spark_sum
from pyspark.sql.functions import when
from pyspark.sql.types import IntegerType

from . import config
from pyspark import pipelines as dp
from .raw_tables import *


def aggregate_efalines_over_years(bfr, efa_lines: list[int], year_cols: list[str], aggregated_category_name: str):
    filtered_bfr_for_aggregation = bfr.filter(col("EFALineNo").isin(efa_lines))
    bfr_aggregated_category_rows = (
        filtered_bfr_for_aggregation.groupBy("TrustUPIN")
        .agg(*[spark_sum(col(c)).alias(c) for c in year_cols])
        .withColumn("Category", lit(aggregated_category_name))
        .withColumn("EFALineNo", lit(None).cast(IntegerType()))
    )
    return bfr_aggregated_category_rows


def preprocess_bfr_sofa(bfr_sofa_mv, year):
    sofa_year_cols = config.get_sofa_year_cols(year)
    bfr_sofa_mv = bfr_sofa_mv.drop("Category") \
        .withColumnRenamed("Title", "Category") \
        .select("TrustUPIN", "EFALineNo", "Category", *sofa_year_cols)
    sofa_efa_lines_to_filter = [
        *config.SOFA_SELF_GENERATED_INCOME_EFALINES,
        config.SOFA_PUPIL_NUMBER_EFALINE,
        *config.SOFA_GRANT_FUNDING_EFALINES,
        config.SOFA_TRUST_REVENUE_RESERVE_EFALINE,
        config.SOFA_SUBTOTAL_INCOME_EFALINE,
        config.SOFA_OTHER_COSTS_EFALINE,
        config.SOFA_TOTAL_REVENUE_INCOME,
        config.SOFA_TOTAL_REVENUE_EXPENDITURE,
        *config.SOFA_IT_SPEND_LINES,
    ]

    bfr_sofa_filtered = bfr_sofa_mv.filter(
        col("EFALineNo").isin(sofa_efa_lines_to_filter)
    )

    sofa_year_cols = config.get_sofa_year_cols(year)
    for col_name in sofa_year_cols:
        bfr_sofa_filtered = bfr_sofa_filtered.withColumn(
            col_name,
            when(
                col("EFALineNo") != config.SOFA_PUPIL_NUMBER_EFALINE,
                col(col_name) * 1000,
            ).otherwise(col(col_name)),
        )

    self_gen_income = aggregate_efalines_over_years(
        bfr_sofa_filtered,
        config.SOFA_SELF_GENERATED_INCOME_EFALINES,
        sofa_year_cols,
        "Self-generated income",
    )
    grant_funding = aggregate_efalines_over_years(
        bfr_sofa_filtered,
        config.SOFA_GRANT_FUNDING_EFALINES,
        sofa_year_cols,
        "Grant funding",
    )

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
        .dropDuplicates(["TrustUPIN", "EFALineNo", "Category"])
    )

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


def preprocess_bfr_3y(bfr_3y_mv, year):
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

    bfr_3y_filtered = bfr_3y_normalized.filter(
        col("EFALineNo").isin(
            [*config.BFR_3Y_TO_SOFA_MAPPINGS.values(), config.OTHER_COSTS_EFALINE]
        )
    )

    for col_name in config.THREE_YEAR_PROJECTION_COLS:
        bfr_3y_filtered = bfr_3y_filtered.withColumn(
            col_name,
            when(
                col("EFALineNo") != config.SOFA_PUPIL_NUMBER_EFALINE,
                col(col_name) * 1000,
            ).otherwise(col(col_name)),
        )

    return bfr_3y_filtered.select(
        "TrustUPIN", "EFALineNo", "Category", *config.THREE_YEAR_PROJECTION_COLS
    )


@dp.table()
def bfr_sofa_preprocessed():
    spark = SparkSession.builder.getOrCreate()
    year = int(spark.conf.get("pipeline.year"))
    bfr_sofa = dp.read("bfr_sofa_current_year")
    return preprocess_bfr_sofa(bfr_sofa, year)


@dp.table()
def bfr_3y_preprocessed():
    spark = SparkSession.builder.getOrCreate()
    year = int(spark.conf.get("pipeline.year"))
    bfr_3y = dp.read("bfr_three_year_forecast_current_year")
    return preprocess_bfr_3y(bfr_3y, year)


@dp.table(name="merged_bfr")
def merged_bfr():
    bfr_sofa_preprocessed = dp.read("bfr_sofa_preprocessed")
    bfr_3y_preprocessed = dp.read("bfr_3y_preprocessed").drop("Category")
    joined = bfr_sofa_preprocessed.join(
        bfr_3y_preprocessed,
        on=["TrustUPIN", "EFALineNo"],
        how="left_outer"
    )

    return joined

# @dp.table(name="merged_bfr_with_crn")
# def merged_bfr_with_crn():
#     academies = spark.read.table("academies")
#     return (
#         academies.select("Company Registration Number", "Trust UPIN")
#         .dropDuplicates(subset=["TrustUPIN"])
#         .join(merged_bfr(), on="TrustUPIN", how="inner")
#     )