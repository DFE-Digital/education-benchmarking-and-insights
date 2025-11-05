
from pyspark.sql import SparkSession
import pytest
from unittest.mock import patch
from mock_pipelines import mock_pipelines
import sys


def pytest_configure(config: pytest.Config):
    sys.modules['pyspark.pipelines'] = mock_pipelines


@pytest.fixture(scope="session")
def spark() -> SparkSession:
    """Provide a SparkSession fixture for tests."""
    return (
        SparkSession.builder
        .master("local[2]")
        .appName("pytest")
        .config("spark.sql.shuffle.partitions", "2")
        .getOrCreate()
    )


@pytest.fixture
def mock_logger():
    with patch("education_benchmarking_and_insights.base.logger") as mock:
        yield mock
