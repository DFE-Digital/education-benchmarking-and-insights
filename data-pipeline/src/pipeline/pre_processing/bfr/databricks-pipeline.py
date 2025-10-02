from contextlib import contextmanager

import findspark

from pipeline.pre_processing.bfr.sparkcode.pipeline import BFRPipeline

findspark.init()

import os

from pyspark.sql import SparkSession


@contextmanager
def local_spark_session():
    spark = (
        SparkSession.builder.master("local[*]")
        .appName("LocalSparkTesting")
        .config("spark.driver.memory", "4g")
        .config("spark.driver.host", "127.0.0.1")
        .config("spark.driver.bindAddress", "127.0.0.1")
        .getOrCreate()
    )
    try:
        yield spark
    finally:
        spark.stop()


def is_running_in_databricks():
    return "DATABRICKS_RUNTIME_VERSION" in os.environ


if __name__ == "__main__":
    if not is_running_in_databricks():
        with local_spark_session() as spark:
            bfr_pipeline = BFRPipeline(2025, spark=spark)
            bfr_pipeline.run()
    else:
        spark = SparkSession.builder.getOrCreate()
        bfr_pipeline = BFRPipeline(2025, spark=spark)
        bfr_pipeline.run()
