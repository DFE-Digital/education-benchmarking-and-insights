import numpy as np
import pandas as pd
from pyspark.sql import DataFrame, SparkSession
from pyspark.sql.functions import (
    array,
    coalesce,
    col,
    expr,
    first,
    lit,
    nanvl,
    pandas_udf,
    when,
)
from pyspark.sql.types import DoubleType, IntegerType, StructField, StructType
from pyspark import pipelines as dp
from education_benchmarking_and_insights.bfr.config import (
    SOFA_TRUST_REVENUE_RESERVE_EFALINE, SOFA_PUPIL_NUMBER_EFALINE
)

from education_benchmarking_and_insights.logger import setup_logger

logger = setup_logger(__name__)


def _build_bfr_historical_data(
    academies_historical: DataFrame, bfr_sofa_historical: DataFrame
) -> DataFrame:
    """
    Derive historical pupil numbers and revenue reserves from BFR SOFA data using Spark.
    Also add the historic company reference number from the historic academies file.
    """
    if academies_historical is None or bfr_sofa_historical is None:
        if academies_historical is not None:
            # If there is no BFR SOFA, we can't get historic revenue/pupils
            # Add columns with default values if bfr_sofa_historical is None
            academies_historical = academies_historical.withColumn(
                "TrustRevenueReserve", lit(0.0).cast(DoubleType())
            ).withColumn("TotalPupilsInTrust", lit(0).cast(IntegerType()))
            return academies_historical
        return None

    # Filter bfr_sofa_historical for revenue reserve and rename Y2P2
    sofa_revenue_reserve = bfr_sofa_historical.filter(
        col("EFALineNo") == SOFA_TRUST_REVENUE_RESERVE_EFALINE
    ).select("TrustUPIN", col("Y2P2").alias("TrustRevenueReserve"))

    # Merge with academies_historical
    historic_bfr_with_crn = academies_historical.join(
        sofa_revenue_reserve, on="TrustUPIN", how="left_outer"
    ).withColumn(
        "TrustRevenueReserve",
        coalesce(col("TrustRevenueReserve"), lit(0.0)) * 1_000,
    )

    # Filter bfr_sofa_historical for pupil number and rename Y1P2
    sofa_pupil_number = bfr_sofa_historical.filter(
        col("EFALineNo") == SOFA_PUPIL_NUMBER_EFALINE
    ).select("TrustUPIN", col("Y1P2").alias("SofaPupilsInTrust"))

    # Merge again for pupil numbers, using SOFA data if academies data is missing
    historic_bfr_with_crn = historic_bfr_with_crn.join(
        sofa_pupil_number, on="TrustUPIN", how="left_outer"
    ).withColumn(
        "TotalPupilsInTrust",
        coalesce(
            col("TotalPupilsInTrust"), 
            col("SofaPupilsInTrust"),
            lit(0).cast(IntegerType())
        )
    ).drop("SofaPupilsInTrust")
    return historic_bfr_with_crn


def _merge_historic_bfr(
    bfr: DataFrame, historic_bfr_processed: DataFrame, year_prefix: str
) -> DataFrame:
    """Merges historic BFR processed data into the main BFR DataFrame using Spark."""
    if historic_bfr_processed is not None and historic_bfr_processed.count() > 0:
        historic_bfr_selected = historic_bfr_processed.select(
            "TrustUPIN",
            col("TrustRevenueReserve").alias(year_prefix),
            col("TotalPupilsInTrust").alias(f"Pupils{year_prefix}"),
        )

        bfr = bfr.join(historic_bfr_selected, on="TrustUPIN", how="left_outer")

        # Fill nulls after join with 0.0
        bfr = bfr.withColumn(year_prefix, coalesce(col(year_prefix), lit(0.0)))
        bfr = bfr.withColumn(
            f"Pupils {year_prefix}",
            coalesce(col(f"Pupils{year_prefix}"), lit(0.0)),
        )
    else:
        # If historic_bfr_processed is None or empty, add columns with 0.0
        bfr = bfr.withColumn(year_prefix, lit(0.0).cast(DoubleType()))
        bfr = bfr.withColumn(f"Pupils{year_prefix}", lit(0.0).cast(DoubleType()))
    return bfr


def _prepare_merged_bfr_for_forecast_and_risk(
    merged_bfr_with_crn: DataFrame,
    historic_academies_processed_y2: DataFrame,
    historic_academies_processed_y1: DataFrame,
) -> DataFrame:
    """
    Prepares the merged BFR data by adding historic data for forecast and risk calculations.
    """
    merged_bfr_with_1y_historic_data = _merge_historic_bfr(
        merged_bfr_with_crn, historic_academies_processed_y2, "Y-2"
    )
    merged_bfr_with_2y_historic_data = _merge_historic_bfr(
        merged_bfr_with_1y_historic_data, historic_academies_processed_y1, "Y-1"
    )
    merged_bfr_with_2y_historic_data = merged_bfr_with_2y_historic_data.withColumn(
        "Y1", col("Y2P2")
    )
    return merged_bfr_with_2y_historic_data


def _calculate_metrics(bfr: DataFrame) -> DataFrame:
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
    ).select("Company_Number", "Category", "metric")

    # Pivot the DataFrame
    pivot_df = (
        bfr_with_metric.groupBy("Company_Number")
        .pivot(
            "Category",
            [
                "Total income",
                "Revenue reserve",
                "Staff costs",
                "Total expenditure",
                "Self-generated income",
            ],
        )
        .agg(first("metric"))
    )

    # Calculate metrics
    df_metrics = (
        pivot_df.withColumn(
            "Revenue reserve as percentage of income",
            (col("Revenue reserve") / col("Total income")) * 100,
        )
        .withColumn(
            "Staff costs as percentage of income",
            (col("Staff costs") / col("Total income")) * 100,
        )
        .withColumn(
            "Expenditure as percentage of income",
            (col("Total expenditure") / col("Total income")) * 100,
        )
        .withColumn(
            "Self generated income as percentage of income",
            (col("Self-generated income") / col("Total income")) * 100,
        )
        .withColumn(
            "Grant funding as percentage of income",
            lit(100) - col("Self generated income as percentage of income"),
        )
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
        col("Company_Number"),
        expr(
            f"stack({len(metric_cols)}, "
            + ", ".join([f"'{c}', CAST(`{c}` AS DECIMAL(38,8))" for c in metric_cols])
            + ") as (Category, Value)"
        ),
    )

    # Replace inf, -inf, nan with 0.0
    final_df = melted_df.withColumn(
        "Value",
        when(col("Value").isin([float("inf"), float("-inf")]), lit(0.0)).otherwise(
            nanvl(col("Value"), lit(0.0))
        ),
    )

    return final_df


def _calculate_slopes(bfr: DataFrame, year_cols: list[str]) -> DataFrame:
    """Calculates slopes for revenue reserves using a Pandas UDF in Spark."""

    @pandas_udf(DoubleType())
    def calculate_slopes_udf(year_values: pd.Series) -> pd.Series:
        x = np.array([1, 2, 3, 4, 5, 6])
        x_bar = 3.5
        x_x_bar = x - x_bar
        # Ensure year_values is treated as a 2D array for consistent operations
        matrix = np.array(year_values.tolist()).astype(float)
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


def _assign_slope_flag(bfr_with_slopes: DataFrame) -> DataFrame:
    """Assigns slope flags based on percentiles using a Pandas UDF in Spark."""
    # Define the schema for the output of the UDF, including the new 'Slope flag' column
    output_schema = StructType(
        bfr_with_slopes.schema.fields
        + [StructField("Slope flag", IntegerType(), True)]
    )

    def assign_slope_flag_func(pdf: pd.DataFrame) -> pd.DataFrame:
        percentile_10 = np.nanpercentile(pdf["Slope"].values, 10)
        percentile_90 = np.nanpercentile(pdf["Slope"].values, 90)
        pdf["Slope flag"] = 0
        pdf.loc[pdf["Slope"] < percentile_10, "Slope flag"] = -1
        pdf.loc[pdf["Slope"] > percentile_90, "Slope flag"] = 1
        return pdf

    # Apply the grouped map UDF
    bfr_with_slope_flag = bfr_with_slopes.groupBy(
        "Company_Number"
    ).applyInPandas(assign_slope_flag_func, schema=output_schema)
    return bfr_with_slope_flag


def _slope_analysis(bfr: DataFrame) -> DataFrame:
    """Performs slope analysis on BFR data using Spark."""
    year_columns = ["Y-2", "Y-1", "Y1", "Y2", "Y3", "Y4"]

    # Calculate slopes
    bfr_with_slopes = _calculate_slopes(bfr, year_columns)

    # Assign slope flags
    bfr_with_slope_flag = _assign_slope_flag(bfr_with_slopes)

    # Melt the DataFrame
    melted_df = bfr_with_slope_flag.select(
        "Company_Number",
        expr(
            "stack(2, 'Slope', Slope, 'Slope flag', CAST(`Slope flag` AS DOUBLE)) as (Category, Value)"
        ),
    ).withColumnRenamed("Category", "Category")

    return melted_df.orderBy("Company_Number")


def _calculate_forecast_and_risk_metrics(
    merged_bfr_with_2y_historic_data: DataFrame
) -> DataFrame:
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
    bfr_forecast_and_risk_metrics = _calculate_metrics(
        bfr_rows_for_normalised_finance_metrics
    )
    bfr_rows_for_slope_analysis = merged_bfr_with_2y_historic_data.filter(
        col("Category") == "Revenue reserve"
    )
    bfr_forecast_and_risk_slope_analysis = _slope_analysis(
        bfr_rows_for_slope_analysis
    )
    bfr_forecast_and_risk_metrics = bfr_forecast_and_risk_metrics.unionByName(
        bfr_forecast_and_risk_slope_analysis
    )
    return bfr_forecast_and_risk_metrics


def _prepare_current_and_future_pupils(
    bfr_data: DataFrame, academies: DataFrame
) -> DataFrame:
    """
    Prepares current and future pupil numbers using Spark.
    The current year BFR_SOFA (Y2) doesn't have current year pupil numbers as
    it is released halfway through the year, so we get them from the academies
    data (aka the academy year census). Future years come from BFR_3Y.
    """
    bfr_pupils = bfr_data.filter(col("Category") == "Pupil numbers").select(
        "TrustUPIN",
        col("Y2").alias("PupilsY2"),
        col("Y3").alias("PupilsY3"),
        col("Y4").alias("PupilsY4"),
    )

    academies_pupils = (
        # TODO: this pupil number should come from census data
        academies.withColumn("TotalPupilsInTrust", lit(0.0))
        .select(
            "TrustUPIN", col("TotalPupilsInTrust").alias("PupilsY1")
        )
        .groupBy("TrustUPIN")
        .agg(first("PupilsY1").alias("PupilsY1"))
    )

    bfr_pupils = bfr_pupils.join(academies_pupils, on="TrustUPIN", how="left_outer")
    return bfr_pupils


def _melt_forecast_and_risk_pupil_numbers_from_bfr(
    bfr: DataFrame, year: int
) -> DataFrame:
    """Melt pupil numbers for forecast and risk calculations using Spark."""
    id_vars = ["Company_Number", "Category"]
    value_vars = [
        "PupilsY-2",
        "PupilsY-1",
        "PupilsY1",
        "PupilsY2",
        "PupilsY3",
        "PupilsY4",
    ]
    # Using stack to melt the DataFrame
    forecast_and_risk_pupil_numbers_melted_rows = bfr.select(
        *id_vars,
        expr(
            f"stack({len(value_vars)}, "
            + ", ".join([f"'{col_name}', CAST(`{col_name}` AS DOUBLE)"  for col_name in value_vars])
            + ") as (Year, Pupils)"
        ),
    ).withColumn(
        "Year",
        when(col("Year") == "PupilsY-2", year - 2)
        .when(col("Year") == "PupilsY-1", year - 1)
        .when(col("Year") == "PupilsY1", year)
        .when(col("Year") == "PupilsY2", year + 1)
        .when(col("Year") == "PupilsY3", year + 2)
        .when(col("Year") == "PupilsY4", year + 3)
        .otherwise(col("Year")),
    )
    return forecast_and_risk_pupil_numbers_melted_rows.orderBy(
        "Company_Number", "Year"
    )


def _melt_forecast_and_risk_revenue_reserves_from_bfr(
    bfr: DataFrame, year: int
) -> DataFrame:
    """Melt revenue reserves for forecast and risk calculations using Spark."""
    id_vars = ["Company_Number", "Category"]
    value_vars = ["Y-2", "Y-1", "Y1", "Y2", "Y3", "Y4"]
    # Filter and then melt the DataFrame
    forecast_and_risk_revenue_reserves_melted_rows = (
        bfr.filter(col("EFALineNo").isin([SOFA_TRUST_REVENUE_RESERVE_EFALINE]))
        .select(
            *id_vars,
            expr(
                f"stack({len(value_vars)}, "
                + ", ".join([f"'{col_name}', CAST(`{col_name}` AS DOUBLE)" for col_name in value_vars])
                + ") as (Year, Value)"
            ),
        )
        .withColumn(
            "Year",
            when(col("Year") == "Y-2", year - 2)
            .when(col("Year") == "Y-1", year - 1)
            .when(col("Year") == "Y1", year)
            .when(col("Year") == "Y2", year + 1)
            .when(col("Year") == "Y3", year + 2)
            .when(col("Year") == "Y4", year + 3)
            .otherwise(col("Year")),
        )
    )
    return forecast_and_risk_revenue_reserves_melted_rows.orderBy(
        "Company_Number", "Year"
    )


@dp.table(name="academies_historical_y1_processed")
def academies_historical_y1_processed():
    return _build_bfr_historical_data(
        dp.read("academies_y1").withColumnRenamed("Lead_UPIN", "TrustUPIN"), 
        dp.read("bfr_sofa_y1_processed")
    )

@dp.table(name="academies_historical_y2_processed")
def academies_historical_y2_processed():
    return _build_bfr_historical_data(
        dp.read("academies_y2").withColumnRenamed("Lead_UPIN", "TrustUPIN"), 
        dp.read("bfr_sofa_y2_processed")
    )

@dp.table(name="historic_bfr_y1_with_historical_data")
def historic_bfr_y1_with_historical_data():
    academies_historical_y1_processed = dp.read("academies_historical_y1_processed")
    historic_bfr_y1_initial = dp.read("bfr_sofa_y1_processed") # Assumed upstream DLT table

    historic_bfr_y1_joined = historic_bfr_y1_initial.join(
        academies_historical_y1_processed.select(
            "TrustUPIN",
            "Company_Number",
            "TrustRevenueReserve",
            "TotalPupilsInTrust",
        ),
        on="TrustUPIN",
        how="left_outer",
    ).drop(
        "Y1P2", "Y2P2"  # Drop old columns if they exist
    )
    return historic_bfr_y1_joined


@dp.table(name="historic_bfr_y2_with_historical_data")
def historic_bfr_y2_with_historical_data():
    academies_historical_y2_processed = dp.read("academies_historical_y2_processed")
    historic_bfr_y2_initial = dp.read("bfr_sofa_y2_processed") # Assumed upstream DLT table

    historic_bfr_y2_joined = historic_bfr_y2_initial.join(
        academies_historical_y2_processed.select(
            "TrustUPIN",
            "Company_Number",
            "TrustRevenueReserve",
            "TotalPupilsInTrust",
        ),
        on="TrustUPIN",
        how="left_outer",
    ).drop(
        "Y1P2", "Y2P2"  # Drop old columns if they exist
    )
    return historic_bfr_y2_joined


@dp.table(name="merged_bfr_with_2y_historic_data")
def merged_bfr_with_2y_historic_data():
    return _prepare_merged_bfr_for_forecast_and_risk(
        dp.read("merged_bfr_with_crn"),
        dp.read("academies_y2"),
        dp.read("academies_y1"),
    )


@dp.table(name="bfr_pupils")
def bfr_pupils():
    return _prepare_current_and_future_pupils(
        dp.read("merged_bfr_with_2y_historic_data"),
        dp.read("academies_current_year").withColumnRenamed("Lead_UPIN", "TrustUPIN")
    )


@dp.table(name="bfr_final_wide")
def bfr_final_wide():
    merged_bfr_with_2y_historic_data = dp.read("merged_bfr_with_2y_historic_data")
    bfr_pupils = dp.read("bfr_pupils")
    return merged_bfr_with_2y_historic_data.join(
        bfr_pupils, on="TrustUPIN", how="left_outer"
    )


@dp.table(name="forecast_and_risk_revenue_reserve_rows")
def forecast_and_risk_revenue_reserve_rows():
    pipeline_year = dp.read("bfr_final_wide").sparkSession.conf.get("pipeline.bfr_year", "2025") # Defaulting to 2025 for safety
    return _melt_forecast_and_risk_revenue_reserves_from_bfr(dp.read("bfr_final_wide"), int(pipeline_year))


@dp.table(name="forecast_and_risk_pupil_numbers_melted_rows")
def forecast_and_risk_pupil_numbers_melted_rows():
    pipeline_year = dp.read("bfr_final_wide").sparkSession.conf.get("pipeline.bfr_year", "2025") # Defaulting to 2025 for safety
    return _melt_forecast_and_risk_pupil_numbers_from_bfr(dp.read("bfr_final_wide"), int(pipeline_year))


@dp.table(name="bfr_forecast_and_risk_rows")
def bfr_forecast_and_risk_rows():
    forecast_and_risk_revenue_reserve_rows = dp.read("forecast_and_risk_revenue_reserve_rows")
    forecast_and_risk_pupil_numbers_melted_rows = dp.read("forecast_and_risk_pupil_numbers_melted_rows")
    return forecast_and_risk_revenue_reserve_rows.join(
        forecast_and_risk_pupil_numbers_melted_rows,
        on=["Company_Number", "Category", "Year"],
        how="left_outer",
    )


@dp.table(name="bfr_forecast_and_risk_metrics")
def bfr_forecast_and_risk_metrics():
    return _calculate_forecast_and_risk_metrics(dp.read("merged_bfr_with_2y_historic_data"))