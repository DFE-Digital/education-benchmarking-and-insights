import pyspark.sql.functions as F
from pyspark.sql import DataFrame
from pyspark.sql.functions import (
    array,
    asc,
    avg,
    col,
    collect_list,
    desc,
    explode,
    isnan,
    isnull,
    lit,
    monotonically_increasing_id,
    percent_rank,
    regexp_replace,
    row_number,
    size,
)
from pyspark.sql.functions import sum as spark_sum
from pyspark.sql.functions import when
from pyspark.sql.types import DoubleType, IntegerType
from pyspark.sql.window import Window


def calculate_metrics(bfr: DataFrame) -> DataFrame:
    """
    Calculate financial metrics from balance sheet data.

    Args:
        bfr: PySpark DataFrame with columns including Y2P1, Y2P2, Category, Company Registration Number

    Returns:
        PySpark DataFrame with calculated percentage metrics in long format
    """
    # Define costs categories that need Y2P1 + Y2P2
    costs_list = [
        "Total income",
        "Staff costs",
        "Total expenditure",
        "Self-generated income",
    ]

    # Calculate metric column based on category
    bfr_with_metric = bfr.withColumn(
        "metric",
        when(col("Category").isin(costs_list), col("Y2P1") + col("Y2P2")).otherwise(
            col("Y2P2")
        ),
    )

    # Pivot the data to get categories as columns
    df_pivot = (
        bfr_with_metric.groupBy("Company Registration Number")
        .pivot("Category")
        .sum("metric")
    )

    # Calculate percentage metrics
    df_with_percentages = (
        df_pivot.withColumn(
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
            100 - col("Self generated income as percentage of income"),
        )
    )

    # Select only the percentage columns for melting
    percentage_columns = [
        "Revenue reserve as percentage of income",
        "Staff costs as percentage of income",
        "Expenditure as percentage of income",
        "Self generated income as percentage of income",
        "Grant funding as percentage of income",
    ]

    # Create array of column names and values for unpivoting
    df_melted = df_with_percentages.select(
        col("Company Registration Number"), *[col(c) for c in percentage_columns]
    )

    # Unpivot using stack function
    stack_expr = (
        f"stack({len(percentage_columns)}, "
        + ", ".join([f"'{c}', `{c}`" for c in percentage_columns])
        + ") as (variable, Value)"
    )

    df_unpivoted = df_melted.select(
        col("Company Registration Number"), F.expr(stack_expr)
    )

    # Replace infinite, null, and NaN values with 0.0 (matching original logic)
    df_final = df_unpivoted.withColumn(
        "Value",
        when(
            isnan(col("Value"))
            | isnull(col("Value"))
            | (col("Value") == float("inf"))
            | (col("Value") == float("-inf")),
            0.0,
        ).otherwise(col("Value")),
    )

    return df_final


def calculate_slopes_spark(df: DataFrame, year_columns: list) -> DataFrame:
    """
    Calculate slopes using PySpark operations.

    Args:
        df: PySpark DataFrame with year columns
        year_columns: List of column names representing years

    Returns:
        DataFrame with calculated slopes
    """
    # Create array of year values and x values (1,2,3,4,5,6)
    x_values = [1, 2, 3, 4, 5, 6]
    x_bar = 3.5

    # Create arrays for calculations - preserve nulls for proper nanmean behavior
    df_with_arrays = df.withColumn(
        "y_values", array([col(c) for c in year_columns])  # Keep original nulls/NaNs
    ).withColumn("x_values", array([lit(x) for x in x_values]))

    # Calculate y_bar (mean of non-null values, matching np.nanmean behavior)
    df_with_mean = df_with_arrays.withColumn(
        "y_bar",
        # Calculate mean excluding null/NaN values (matching np.nanmean)
        F.expr(
            """
        case 
            when size(filter(y_values, x -> x is not null and not isnan(x))) = 0 then 0.0
            else aggregate(filter(y_values, x -> x is not null and not isnan(x)), 0.0, (acc, x) -> acc + x) / 
                 size(filter(y_values, x -> x is not null and not isnan(x)))
        end
        """
        ),
    )

    # Calculate slope components using array operations
    df_with_slope = df_with_mean.withColumn(
        "slope",
        F.expr(
            f"""
        aggregate(
            zip_with(x_values, y_values, (x, y) -> (x - {x_bar}) * (y - y_bar)),
            0.0,
            (acc, val) -> acc + val
        ) / aggregate(
            transform(x_values, x -> pow(x - {x_bar}, 2)),
            0.0,
            (acc, val) -> acc + val
        )
        """
        ),
    )

    return df_with_slope.select(
        col("Company Registration Number"),
        col("Trust UPIN"),
        col("slope").alias("Slope"),
    )


def assign_slope_flag_spark(df: DataFrame) -> DataFrame:
    """
    Assign slope flags based on 10th and 90th percentiles.

    Args:
        df: DataFrame with Slope column

    Returns:
        DataFrame with Slope flag column added
    """
    # Calculate percentiles using window functions
    window_spec = Window.orderBy(col("Slope"))

    df_with_percentiles = df.withColumn(
        "percent_rank_col", percent_rank().over(window_spec)
    )

    # Assign flags based on percentile ranks
    df_with_flag = df_with_percentiles.withColumn(
        "Slope flag",
        when(col("percent_rank_col") <= 0.1, -1)
        .when(col("percent_rank_col") >= 0.9, 1)
        .otherwise(0),
    ).drop("percent_rank_col")

    return df_with_flag


def slope_analysis(bfr: DataFrame) -> DataFrame:
    """
    Perform slope analysis on time series data.

    Args:
        bfr: PySpark DataFrame with year columns and company information

    Returns:
        DataFrame with slope analysis results in long format
    """
    year_columns = ["Y-2", "Y-1", "Y1", "Y2", "Y3", "Y4"]

    # Fill null values - but preserve them for slope calculation initially
    df = bfr
    # Don't fill nulls here - let the slope calculation handle them properly

    # Calculate slopes
    df_with_slopes = calculate_slopes_spark(df, year_columns)

    # Assign slope flags
    df_with_flags = assign_slope_flag_spark(df_with_slopes)

    # Melt the slope and slope flag columns
    df_melted = df_with_flags.select(
        col("Company Registration Number"),
        col("Trust UPIN"),
        F.expr(
            "stack(2, 'Slope', Slope, 'Slope flag', `Slope flag`) as (Category, value)"
        ),
    )

    return df_melted
