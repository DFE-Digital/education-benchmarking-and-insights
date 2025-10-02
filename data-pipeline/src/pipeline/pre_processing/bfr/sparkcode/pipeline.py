import config
from pyspark.sql import SparkSession

from .base import DatabricksFBITPipeline
from .forecast_and_risk import BFRForecastAndRiskCalculator
from .it_spend import BFRITSpendCalculator
from .loader import BFRLoader
from .logging import setup_logger
from .preprocessor import BFRPreprocessor

logger = setup_logger(__name__)


class BFRPipeline(DatabricksFBITPipeline):
    def __init__(self, year: int, spark: SparkSession):
        self.year = year
        self.config = config
        self.spark = spark
        self.bfr_loader = BFRLoader(year, spark, config)
        self.bfr_preprocessor = BFRPreprocessor(year, spark, config, self)
        self.bfr_forecast_and_risk_calculator = BFRForecastAndRiskCalculator(
            year, spark, config, self
        )
        self.bfr_it_spend_calculator = BFRITSpendCalculator(year, spark, config)

    def run(self):
        bfr_sofa_mv, bfr_three_year_mv = self.bfr_loader.load_data()
        (
            bfr_sofa_year_minus_one,
            bfr_sofa_year_minus_two,
            academies,
            academies_y1,
            academies_y2,
        ) = self.bfr_loader.load_ancillary_data()
        merged_bfr_with_crn = self.bfr_preprocessor.preprocess_data(
            bfr_sofa_mv, bfr_three_year_mv, academies
        )
        bfr_forecast_and_risk_rows, bfr_forecast_and_risk_metrics = (
            self.bfr_forecast_and_risk_calculator.get_bfr_forecast_and_risk_data(
                merged_bfr_with_crn,
                bfr_sofa_year_minus_two,
                bfr_sofa_year_minus_one,
                self.year,
                academies,
                academies_y1,
                academies_y2,
            )
        )
        # IT spend breakdown was introduced from the 2025 return
        if self.year > 2024:
            bfr_it_spend_rows = self.bfr_it_spend_calculator.get_bfr_it_spend_rows(
                merged_bfr_with_crn, self.year
            )
            bfr_final_long = bfr_forecast_and_risk_rows.unionByName(bfr_it_spend_rows)
            return bfr_final_long, bfr_forecast_and_risk_metrics
        return bfr_forecast_and_risk_rows, bfr_forecast_and_risk_metrics

    def save_data(self):
        pass


if __name__ == "__main__":
    spark = SparkSession.builder.appName("BFR Pipeline").getOrCreate()
    bfr_pipeline = BFRPipeline(2023, spark)
    bfr_final_long, bfr_forecast_and_risk_metrics = bfr_pipeline.run()
