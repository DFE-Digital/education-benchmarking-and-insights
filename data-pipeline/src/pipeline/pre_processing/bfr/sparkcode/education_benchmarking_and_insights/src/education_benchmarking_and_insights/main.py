from databricks.sdk.runtime import spark
from pyspark.sql import DataFrame
from bfr.pipeline import BFRPipeline


def run_bfr_pipeline() -> DataFrame:
    bfr_pipeline = BFRPipeline(2023, spark)
    bfr_final_long, bfr_forecast_and_risk_metrics = bfr_pipeline.run()
    bfr_pipeline.save()


def main():
    run_bfr_pipeline()


if __name__ == "__main__":
    main()
