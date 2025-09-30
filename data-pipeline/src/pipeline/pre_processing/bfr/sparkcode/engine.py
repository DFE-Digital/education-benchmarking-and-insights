import logging

import pyspark.sql.functions as F
from pyspark.sql import DataFrame, SparkSession
from pyspark.sql.functions import (
    asc,
    broadcast,
    col,
    concat_ws,
    desc,
    first,
    isnan,
    isnull,
    lit,
    regexp_replace,
)
from pyspark.sql.functions import sum as spark_sum
from pyspark.sql.functions import when
from pyspark.sql.types import DoubleType, IntegerType, StringType

from pipeline.pre_processing.bfr import config

from .calculations import calculate_metrics, slope_analysis


def build_bfr_historical_data(
    academies_historical: DataFrame | None,
    bfr_sofa_historical: DataFrame | None,
) -> DataFrame | None:
    """
    Derive historical data from BFR SOFA data.

    `academies_historical` must have:
    - Trust UPIN
    - Company Registration Number

    `bfr_sofa_historical` must have:
    - Trust UPIN
    - EFALineNo (containing 430 and 999)
    - Y1P2
    - Y2P2

    The return value will be of the same form as `academies_historical`,
    with additional columns:
    - "Trust Revenue reserve"
    - "Total pupils in trust"

    :param academies_historical: academy data from a previous year
    :param bfr_sofa_historical: BFR SOFA data from a previous year
    :return: updated, historical data
    """
    if academies_historical is not None:
        # Initialize columns with default values
        academies_historical = academies_historical.withColumn(
            "Trust Revenue reserve", lit(0.0)
        )
        academies_historical = academies_historical.withColumn(
            "Total pupils in trust", lit(0.0)
        )

        if bfr_sofa_historical is not None:
            # Drop the default columns to replace with actual data
            academies_historical = academies_historical.drop(
                "Trust Revenue reserve", "Total pupils in trust"
            )

            # Process revenue reserve data (EFALineNo == 430)
            revenue_reserve_data = bfr_sofa_historical.filter(
                col("EFALineNo") == 430
            ).select(
                col("Trust UPIN"), (col("Y2P2") * 1000).alias("Trust Revenue reserve")
            )

            # Join revenue reserve data
            academies_historical = academies_historical.join(
                broadcast(revenue_reserve_data), on="Trust UPIN", how="left"
            )

            # Process pupil data (EFALineNo == 999)
            pupil_data = bfr_sofa_historical.filter(col("EFALineNo") == 999).select(
                col("Trust UPIN"), col("Y1P2").alias("Total pupils in trust")
            )

            # Join pupil data
            academies_historical = academies_historical.join(
                broadcast(pupil_data), on="Trust UPIN", how="left"
            )

    return academies_historical


def build_bfr_data(
    current_year: int,
    bfr_sofa_preprocessed_df: DataFrame,
    bfr_3y_preprocessed_df: DataFrame,
    academies: DataFrame,
    academies_y1: DataFrame = None,
    academies_y2: DataFrame = None,
    spark: SparkSession = None # Add spark session to parameters
):
    """
    Build BFR data by processing SOFA and 3-year data frames.

    Returns tuple of (bfr_final, bfr_metrics) DataFrames
    """
    logger.info(f"BFR sofa preprocessed shape: {bfr_sofa_preprocessed_df.count()} rows")
    logger.info(f"BFR 3y preprocessed shape: {bfr_3y_preprocessed_df.count()} rows")

    # Merge BFR SOFA and 3-year data
    merged_bfr = bfr_sofa_preprocessed_df.join(bfr_3y_preprocessed_df, on=["Trust UPIN", "EFALineNo"], how="left")

    # Get first record per Trust UPIN from academies and merge with BFR data
    academies_grouped = academies.groupBy("Trust UPIN").agg(
        *[first(col(c)).alias(c) for c in academies.columns if c != "Trust UPIN"]
    )

    bfr = academies_grouped.join(merged_bfr, on="Trust UPIN")

    # Handle historical data (Y-2)
    if academies_y2 is not None:
        y2_data = (
            academies_y2.select(
                "Trust UPIN", "Trust Revenue reserve", "Total pupils in trust"
            )
            .groupBy("Trust UPIN")
            .agg(
                first("Trust Revenue reserve").alias("Y-2"),
                first("Total pupils in trust").alias("Pupils Y-2"),
            )
        )
        bfr = bfr.join(y2_data, on="Trust UPIN", how="left")
    else:
        bfr = bfr.withColumn("Y-2", lit(0.0)).withColumn("Pupils Y-2", lit(0.0))

    # Handle historical data (Y-1)
    if academies_y1 is not None:
        y1_data = (
            academies_y1.select(
                "Trust UPIN", "Trust Revenue reserve", "Total pupils in trust"
            )
            .groupBy("Trust UPIN")
            .agg(
                first("Trust Revenue reserve").alias("Y-1"),
                first("Total pupils in trust").alias("Pupils Y-1"),
            )
        )
        bfr = bfr.join(y1_data, on="Trust UPIN", how="left")
    else:
        bfr = bfr.withColumn("Y-1", lit(0.0)).withColumn("Pupils Y-1", lit(0.0))

    # Set Y1 from Y2P2
    bfr = bfr.withColumn("Y1", col("Y2P2")).dropDuplicates()

    # Calculate metrics using the refactored function
    bfr_for_metrics = bfr.withColumn("row_id", F.monotonically_increasing_id())
    bfr_metrics = calculate_metrics(bfr_for_metrics)

    # Extract pupil data
    bfr_pupils = bfr.filter(col("Category") == "Pupil numbers").select(
        "Trust UPIN", "Y2", "Y3", "Y4"
    )

    # Extract revenue reserve data for slope analysis
    bfr_revenue = bfr.filter(col("Category") == "Revenue reserve")

    # Perform slope analysis
    slope_results = slope_analysis(bfr_revenue)
    bfr_metrics = bfr_metrics.unionByName(slope_results)

    # Convert pupil numbers from thousands back to actual numbers
    for col_name in ["Y2", "Y3", "Y4"]:
        bfr_pupils = bfr_pupils.withColumn(col_name, col(col_name) / 1000)

    # Rename pupil columns
    bfr_pupils = (
        bfr_pupils.withColumnRenamed("Y2", "Pupils Y2")
        .withColumnRenamed("Y3", "Pupils Y3")
        .withColumnRenamed("Y4", "Pupils Y4")
    )

    # Add current year pupil data
    current_pupils = (
        academies.select("Trust UPIN", "Total pupils in trust")
        .groupBy("Trust UPIN")
        .agg(first("Total pupils in trust").alias("Pupils Y1"))
    )

    bfr_pupils = bfr_pupils.join(current_pupils, on="Trust UPIN", how="left")

    # Merge pupil data with main BFR data
    bfr = bfr.join(bfr_pupils, on="Trust UPIN", how="left")

    # Drop unnecessary columns
    columns_to_drop = [
        "Y1P1",
        "Y1P2",
        "Y2P1",
        "Y2P2",
        "EFALineNo",
        "Trust Revenue reserve",
    ]
    for col_name in columns_to_drop:
        if col_name in bfr.columns:
            bfr = bfr.drop(col_name)

    # Prepare pupil data for melting
    pupil_cols = [
        "Company Registration Number",
        "Category",
        "Pupils Y-2",
        "Pupils Y-1",
        "Pupils Y1",
        "Pupils Y2",
        "Pupils Y3",
        "Pupils Y4",
    ]

    bfr_pupils_melted = (
        bfr.select(*pupil_cols)
        .select(
            col("Company Registration Number"),
            col("Category"),
            F.expr(
                """
                stack(6, 
                    'Pupils Y-2', `Pupils Y-2`,
                    'Pupils Y-1', `Pupils Y-1`, 
                    'Pupils Y1', `Pupils Y1`,
                    'Pupils Y2', `Pupils Y2`,
                    'Pupils Y3', `Pupils Y3`,
                    'Pupils Y4', `Pupils Y4`
                ) as (Year, Pupils)
            """
            ),
        )
        .withColumn(
            "Year",
            when(col("Year") == "Pupils Y-2", current_year - 2)
            .when(col("Year") == "Pupils Y-1", current_year - 1)
            .when(col("Year") == "Pupils Y1", current_year)
            .when(col("Year") == "Pupils Y2", current_year + 1)
            .when(col("Year") == "Pupils Y3", current_year + 2)
            .when(col("Year") == "Pupils Y4", current_year + 3),
        )
        .orderBy("Company Registration Number", "Year")
    )

    # Prepare revenue reserve data for melting
    revenue_cols = [
        "Company Registration Number",
        "Category",
        "Y-2",
        "Y-1",
        "Y1",
        "Y2",
        "Y3",
        "Y4",
    ]

    bfr_revenue_melted = (
        bfr.select(*revenue_cols)
        .select(
            col("Company Registration Number"),
            col("Category"),
            F.expr(
                """
                stack(6,
                    'Y-2', `Y-2`,
                    'Y-1', `Y-1`,
                    'Y1', Y1,
                    'Y2', Y2,
                    'Y3', Y3,
                    'Y4', Y4
                ) as (Year, Value)
            """
            ),
        )
        .withColumn(
            "Year",
            when(col("Year") == "Y-2", current_year - 2)
            .when(col("Year") == "Y-1", current_year - 1)
            .when(col("Year") == "Y1", current_year)
            .when(col("Year") == "Y2", current_year + 1)
            .when(col("Year") == "Y3", current_year + 2)
            .when(col("Year") == "Y4", current_year + 3),
        )
        .orderBy("Company Registration Number", "Year")
    )

    # Final merge of revenue and pupil data
    bfr_final = bfr_revenue_melted.join(
        bfr_pupils_melted,
        on=["Company Registration Number", "Category", "Year"],
        how="left",
    )

    return bfr_final, bfr_metrics


# Example usage for Databricks:
"""
# Initialize Spark session (usually already available in Databricks)
# spark = SparkSession.builder.appName("BFR_Pipeline").getOrCreate()

# Read input data
academies_df = spark.read.table("your_database.academies_table")
academies_y1_df = spark.read.table("your_database.academies_y1_table")  # Optional
academies_y2_df = spark.read.table("your_database.academies_y2_table")  # Optional

# Example preprocessed dataframes (replace with actual preprocessed data)
mock_bfr_sofa_preprocessed = spark.createDataFrame([], schema=StructType([])) # Replace with actual
mock_bfr_3y_preprocessed = spark.createDataFrame([], schema=StructType([])) # Replace with actual

# Process BFR data
bfr_data, bfr_metrics = build_bfr_data(
    current_year=2024,
    bfr_sofa_preprocessed_df=mock_bfr_sofa_preprocessed,
    bfr_3y_preprocessed_df=mock_bfr_3y_preprocessed,
    academies=academies_df,
    academies_y1=academies_y1_df,
    academies_y2=academies_y2_df
)

# Display results
bfr_data.display()
bfr_metrics.display()

# Write results to tables
bfr_data.write.mode("overwrite").saveAsTable("your_database.bfr_processed")
bfr_metrics.write.mode("overwrite").saveAsTable("your_database.bfr_metrics")
"""
