from pyspark import pipelines as dp
from pyspark.sql import DataFrame, SparkSession
from pyspark.sql.functions import col, when, expr
from education_benchmarking_and_insights.logger import setup_logger
from education_benchmarking_and_insights.bfr.config import (
    SOFA_IT_SPEND_LINES, SOFA_PUPIL_NUMBER_EFALINE
)

logger = setup_logger(__name__)


def melt_it_spend_rows_from_bfr(bfr: DataFrame, year: int, sofa_it_spend_efalines) -> DataFrame:
    """Melt IT spend rows from BFR data using Spark."""
    it_spend_melted_rows = (
        bfr.filter(col("EFALineNo").isin(sofa_it_spend_efalines))
        .withColumn("Y1P_Total", col("Y1P1") + col("Y1P2"))
        .withColumn("Y2P_Total", col("Y2P1") + col("Y2P2"))
        .withColumn("Y3P_Total", col("Y3P1") + col("Y3P2"))
        .select(
            "Category",
            "CompanyRegistrationNumber",
            expr(
                f"stack(3, 'Y1P_Total', `Y1P_Total`, 'Y2P_Total', `Y2P_Total`, 'Y3P_Total', `Y3P_Total`) as (Year, Value)"
            ),
        )
        .withColumn(
            "Year",
            when(col("Year") == "Y1P_Total", year - 1)
            .when(col("Year") == "Y2P_Total", year)
            .when(col("Year") == "Y3P_Total", year + 1)
            .otherwise(col("Year")),
        )
    )
    return it_spend_melted_rows.orderBy("CompanyRegistrationNumber", "Year")


def melt_it_spend_pupil_numbers_from_bfr(bfr: DataFrame, year: int, pupil_number_efaline: int) -> DataFrame:
    """Melt IT spend pupil numbers from BFR data using Spark."""
    it_spend_pupil_numbers_melted_rows = (
        bfr.filter(col("EFALineNo").isin([pupil_number_efaline]))
        .select(
            "CompanyRegistrationNumber",
            expr(
                f"stack(3, 'Y1P1', `Y1P1`, 'Y1P2', `Y1P2`, 'Y2P1', `Y2P1`) as (Year, Pupils)"
            ),
        )
        .withColumn(
            "Year",
            when(col("Year") == "Y1P1", year - 1)
            .when(col("Year") == "Y1P2", year)
            .when(col("Year") == "Y2P1", year + 1)
            .otherwise(col("Year")),
        )
    )
    return it_spend_pupil_numbers_melted_rows.orderBy(
        "CompanyRegistrationNumber", "Year"
    )


@dp.table
def bfr_it_spend_melted_rows():
    """DLT table for melted IT spend rows."""
    spark = SparkSession.builder.getOrCreate()
    year = int(spark.conf.get("pipeline.year"))
    return melt_it_spend_rows_from_bfr(dp.read("merged_bfr_with_crn"), year, SOFA_IT_SPEND_LINES)


@dp.table
def bfr_it_spend_pupil_numbers():
    """DLT table for melted IT spend pupil numbers."""
    spark = SparkSession.builder.getOrCreate()
    year = int(spark.conf.get("pipeline.year"))
    return melt_it_spend_pupil_numbers_from_bfr(dp.read("merged_bfr_with_crn"), year, SOFA_PUPIL_NUMBER_EFALINE)


@dp.table
def bfr_it_spend_final():
    """DLT table for the final joined IT spend data."""
    # Join the two intermediate DLT tables
    bfr_it_spend_melted_rows_df = dp.read("bfr_it_spend_melted_rows")
    bfr_it_spend_pupil_numbers_df = dp.read("bfr_it_spend_pupil_numbers")
    return bfr_it_spend_melted_rows_df.join(
        bfr_it_spend_pupil_numbers_df,
        on=["CompanyRegistrationNumber", "Year"],
        how="left_outer",
    )