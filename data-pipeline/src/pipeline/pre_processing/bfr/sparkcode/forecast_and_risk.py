from pyspark.sql import DataFrame, SparkSession
from pyspark.sql.functions import (
    col,
    first,
    lit,
    nanvl,
    coalesce,
    expr,
    when,
    array,
    pandas_udf,
)
import numpy as np
import pandas as pd
from pyspark.sql.types import (
    DoubleType,
    IntegerType,
    StructField,
    StructType,
)

from .base_pipeline import DatabricksFBITPipeline
from .logging import setup_logger

logger = setup_logger(__name__)


class BFRForecastAndRiskCalculator:
    def __init__(self, year: int, spark: SparkSession, pipeline_config, base_pipeline_helpers: DatabricksFBITPipeline):
        self.year = year
        self.spark = spark
        self.config = pipeline_config
        self.base_pipeline_helpers = base_pipeline_helpers

    def get_bfr_forecast_and_risk_data(
        self,
        merged_bfr_with_crn: DataFrame,
        historic_bfr_y2: DataFrame,
        historic_bfr_y1: DataFrame,
        current_year: int,
        academies: DataFrame,
    ) -> tuple[DataFrame, DataFrame]:
        """
        Orchestrates the preparation of data and calculation of forecast and risk metrics and rows.
        """
        merged_bfr_with_2y_historic_data = self._prepare_merged_bfr_for_forecast_and_risk(
            merged_bfr_with_crn, historic_bfr_y2, historic_bfr_y1
        )

        bfr_forecast_and_risk_metrics = self._calculate_forecast_and_risk_metrics(
            merged_bfr_with_2y_historic_data
        )

        bfr_pupils = self._prepare_current_and_future_pupils(
            bfr_data=merged_bfr_with_2y_historic_data, academies=academies
        )
        bfr_final_wide = merged_bfr_with_2y_historic_data.join(
            bfr_pupils, on="Trust UPIN", how="left_outer"
        )

        forecast_and_risk_revenue_reserve_rows = (
            self._melt_forecast_and_risk_revenue_reserves_from_bfr(
                bfr_final_wide, current_year
            )
        )
        forecast_and_risk_pupil_numbers_melted_rows = (
            self._melt_forecast_and_risk_pupil_numbers_from_bfr(
                bfr_final_wide, current_year
            )
        )
        bfr_forecast_and_risk_rows = forecast_and_risk_revenue_reserve_rows.join(
            forecast_and_risk_pupil_numbers_melted_rows,
            on=["Company Registration Number", "Category", "Year"],
            how="left_outer",
        )

        return bfr_forecast_and_risk_rows, bfr_forecast_and_risk_metrics

    def _calculate_forecast_and_risk_metrics(self, merged_bfr_with_2y_historic_data: DataFrame) -> DataFrame:
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
        bfr_forecast_and_risk_metrics = self._calculate_metrics(
            bfr_rows_for_normalised_finance_metrics
        )
        bfr_rows_for_slope_analysis = merged_bfr_with_2y_historic_data.filter(
            col("Category") == "Revenue reserve"
        )
        bfr_forecast_and_risk_slope_analysis = self._slope_analysis(
            bfr_rows_for_slope_analysis
        )
        bfr_forecast_and_risk_metrics = bfr_forecast_and_risk_metrics.unionByName(
            bfr_forecast_and_risk_slope_analysis
        )
        return bfr_forecast_and_risk_metrics

    def _prepare_current_and_future_pupils(
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

    def _melt_forecast_and_risk_pupil_numbers_from_bfr(
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

    def _melt_forecast_and_risk_revenue_reserves_from_bfr(
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

    def _merge_historic_bfr(
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

    def _prepare_merged_bfr_for_forecast_and_risk(
        self, merged_bfr_with_crn: DataFrame, historic_bfr_y2: DataFrame, historic_bfr_y1: DataFrame
    ) -> DataFrame:
        """
        Prepares the merged BFR data by adding historic data for forecast and risk calculations.
        """
        merged_bfr_with_1y_historic_data = self._merge_historic_bfr(
            merged_bfr_with_crn, historic_bfr_y2, "Y-2"
        )
        merged_bfr_with_2y_historic_data = self._merge_historic_bfr(
            merged_bfr_with_1y_historic_data, historic_bfr_y1, "Y-1"
        )
        merged_bfr_with_2y_historic_data = merged_bfr_with_2y_historic_data.withColumn(
            "Y1", col("Y2P2")
        )
        return merged_bfr_with_2y_historic_data

    def _calculate_metrics(self, bfr: DataFrame) -> DataFrame:
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

    def _calculate_slopes(self, bfr: DataFrame, year_cols: list[str]) -> DataFrame:
        """Calculates slopes for revenue reserves using a Pandas UDF in Spark."""
        
        @pandas_udf(DoubleType())
        def calculate_slopes_udf(year_values: pd.Series) -> pd.Series:
            x = np.array([1, 2, 3, 4, 5, 6])
            x_bar = 3.5
            x_x_bar = x - x_bar
            # Ensure year_values is treated as a 2D array for consistent operations
            matrix = year_values.values.reshape(-1, len(year_cols)).astype(float)
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

    def _assign_slope_flag(self, bfr_with_slopes: DataFrame) -> DataFrame:
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

    def _slope_analysis(self, bfr: DataFrame) -> DataFrame:
        """Performs slope analysis on BFR data using Spark."""
        year_columns = ["Y-2", "Y-1", "Y1", "Y2", "Y3", "Y4"]

        # Calculate slopes
        bfr_with_slopes = self._calculate_slopes(bfr, year_columns)

        # Assign slope flags
        bfr_with_slope_flag = self._assign_slope_flag(bfr_with_slopes)

        # Melt the DataFrame
        melted_df = bfr_with_slope_flag.select(
            "Company Registration Number",
            expr(
                "stack(2, 'Slope', Slope, 'Slope flag', CAST(`Slope flag` AS DOUBLE)) as (Category, Value)"
            ),
        ).withColumnRenamed("Category", "Category")

        return melted_df.orderBy("Company Registration Number")
