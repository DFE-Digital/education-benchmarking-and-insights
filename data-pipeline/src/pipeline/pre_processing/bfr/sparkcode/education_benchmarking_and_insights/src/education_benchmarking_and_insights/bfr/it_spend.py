from pyspark.sql import DataFrame, SparkSession
from pyspark.sql.functions import col, expr, when

from education_benchmarking_and_insights.logger import setup_logger

logger = setup_logger(__name__)


class BFRITSpendCalculator:
    def __init__(self, year: int, spark: SparkSession, pipeline_config):
        self.year = year
        self.spark = spark
        self.config = pipeline_config

    def _melt_it_spend_rows_from_bfr(self, bfr: DataFrame) -> DataFrame:
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
                when(col("Year") == "Y1P_Total", self.year - 1)
                .when(col("Year") == "Y2P_Total", self.year)
                .when(col("Year") == "Y3P_Total", self.year + 1)
                .otherwise(col("Year")),
            )
        )
        return it_spend_melted_rows.orderBy("Company Registration Number", "Year")

    def _melt_it_spend_pupil_numbers_from_bfr(self, bfr: DataFrame) -> DataFrame:
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
                when(col("Year") == "Y1P1", self.year - 1)
                .when(col("Year") == "Y1P2", self.year)
                .when(col("Year") == "Y2P1", self.year + 1)
                .otherwise(col("Year")),
            )
        )
        return it_spend_pupil_numbers_melted_rows.orderBy(
            "Company Registration Number", "Year"
        )

    def get_bfr_it_spend_rows(self, bfr_final_wide: DataFrame) -> DataFrame:
        """Gets BFR IT spend rows using Spark."""
        bfr_it_spend_melted_rows = self._melt_it_spend_rows_from_bfr(bfr_final_wide)
        bfr_it_spend_pupil_numbers = self._melt_it_spend_pupil_numbers_from_bfr(
            bfr_final_wide
        )
        bfr_it_spend_final = bfr_it_spend_melted_rows.join(
            bfr_it_spend_pupil_numbers,
            on=["Company Registration Number", "Year"],
            how="left_outer",
        )
        return bfr_it_spend_final
