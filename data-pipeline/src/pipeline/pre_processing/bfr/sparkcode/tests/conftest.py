import os
import sys
from unittest.mock import patch

import pytest
from pyspark.sql import SparkSession

sys.path.insert(0, os.path.abspath(os.path.dirname(__file__)))


@pytest.fixture(scope="module")
def spark_session():
    spark = SparkSession.builder.appName("test").master("local[*]").getOrCreate()
    yield spark
    spark.stop()


@pytest.fixture
def mock_logger():
    with patch("pipeline.pre_processing.bfr.sparkcode.base.logger") as mock:
        yield mock
