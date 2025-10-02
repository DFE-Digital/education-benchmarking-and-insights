import pytest
from pyspark.sql import SparkSession
from pyspark.sql.types import (
    StructType,
    StructField,
    StringType,
    DecimalType,
    LongType,
)
from decimal import Decimal

from pipeline.pre_processing.bfr.sparkcode.it_spend import BFRITSpendCalculator


class MockPipelineConfig:
    SOFA_IT_SPEND_LINES = ["IT001", "IT002"]
    SOFA_PUPIL_NUMBER_EFALINE = "PUPIL001"


@pytest.fixture
def bfr_it_spend_calculator(spark_session: SparkSession):
    year = 2024
    config = MockPipelineConfig()
    return BFRITSpendCalculator(year, spark_session, config)


def test_melt_it_spend_rows_from_bfr(bfr_it_spend_calculator: BFRITSpendCalculator, spark_session: SparkSession):
    # Given
    schema = StructType(
        [
            StructField("Category", StringType(), True),
            StructField("Company Registration Number", StringType(), True),
            StructField("EFALineNo", StringType(), True),
            StructField("Y1P1", DecimalType(18, 2), True),
            StructField("Y1P2", DecimalType(18, 2), True),
            StructField("Y2P1", DecimalType(18, 2), True),
            StructField("Y2P2", DecimalType(18, 2), True),
            StructField("Y3P1", DecimalType(18, 2), True),
            StructField("Y3P2", DecimalType(18, 2), True),
        ]
    )
    bfr_data = [
        ("IT Spend 1", "CRN001", "IT001", Decimal("100.00"), Decimal("200.00"), Decimal("300.00"), Decimal("400.00"), Decimal("500.00"), Decimal("600.00")),
        ("IT Spend 2", "CRN001", "IT002", Decimal("50.00"), Decimal("150.00"), Decimal("250.00"), Decimal("350.00"), Decimal("450.00"), Decimal("550.00")),
        ("Other Spend", "CRN001", "OTH001", Decimal("10.00"), Decimal("20.00"), Decimal("30.00"), Decimal("40.00"), Decimal("50.00"), Decimal("60.00")),
    ]
    bfr_df = spark_session.createDataFrame(bfr_data, schema)

    # When
    result_df = bfr_it_spend_calculator._melt_it_spend_rows_from_bfr(bfr_df)

    # Then
    expected_schema = StructType(
        [
            StructField("Category", StringType(), True),
            StructField("Company Registration Number", StringType(), True),
            StructField("Year", LongType(), True),
            StructField("Value", DecimalType(19, 2), True), # Changed to DecimalType(19, 2)
        ]
    )
    expected_data = [
        ("IT Spend 1", "CRN001", 2023, Decimal("300.00")),
        ("IT Spend 1", "CRN001", 2024, Decimal("700.00")),
        ("IT Spend 1", "CRN001", 2025, Decimal("1100.00")),
        ("IT Spend 2", "CRN001", 2023, Decimal("200.00")),
        ("IT Spend 2", "CRN001", 2024, Decimal("600.00")),
        ("IT Spend 2", "CRN001", 2025, Decimal("1000.00")),
    ]
    expected_df = spark_session.createDataFrame(expected_data, expected_schema)

    # Ensure column order is consistent before sorting and collecting
    expected_cols = [field.name for field in expected_schema.fields]
    result_df_ordered = result_df.select(expected_cols)

    # Sort both DataFrames before collecting for comparison
    sorted_result_df = result_df_ordered.orderBy("Company Registration Number", "Year", "Category")
    sorted_expected_df = expected_df.orderBy("Company Registration Number", "Year", "Category")

    assert sorted_result_df.collect() == sorted_expected_df.collect()
    assert sorted_result_df.schema == sorted_expected_df.schema


def test_melt_it_spend_pupil_numbers_from_bfr(bfr_it_spend_calculator: BFRITSpendCalculator, spark_session: SparkSession):
    # Given
    schema = StructType(
        [
            StructField("Company Registration Number", StringType(), True),
            StructField("EFALineNo", StringType(), True),
            StructField("Y1P1", DecimalType(18, 2), True),
            StructField("Y1P2", DecimalType(18, 2), True),
            StructField("Y2P1", DecimalType(18, 2), True),
            StructField("Y3P1", DecimalType(18, 2), True),
        ]
    )
    bfr_data = [
        ("CRN001", "PUPIL001", Decimal("1000.00"), Decimal("1100.00"), Decimal("1200.00"), Decimal("1300.00")),
        ("CRN002", "PUPIL001", Decimal("500.00"), Decimal("550.00"), Decimal("600.00"), Decimal("650.00")),
        ("CRN001", "OTH002", Decimal("10.00"), Decimal("20.00"), Decimal("30.00"), Decimal("40.00")),
    ]
    bfr_df = spark_session.createDataFrame(bfr_data, schema)

    # When
    result_df = bfr_it_spend_calculator._melt_it_spend_pupil_numbers_from_bfr(bfr_df)

    # Then
    expected_schema = StructType(
        [
            StructField("Company Registration Number", StringType(), True),
            StructField("Year", LongType(), True),
            StructField("Pupils", DecimalType(18, 2), True),
        ]
    )
    expected_data = [
        ("CRN001", 2023, Decimal("1000.00")),
        ("CRN001", 2024, Decimal("1100.00")),
        ("CRN001", 2025, Decimal("1200.00")),
        ("CRN002", 2023, Decimal("500.00")),
        ("CRN002", 2024, Decimal("550.00")),
        ("CRN002", 2025, Decimal("600.00")),
    ]
    expected_df = spark_session.createDataFrame(expected_data, expected_schema)

    # Ensure column order is consistent before sorting and collecting
    expected_cols = [field.name for field in expected_schema.fields]
    result_df_ordered = result_df.select(expected_cols)

    # Sort both DataFrames before collecting for comparison
    sorted_result_df = result_df_ordered.orderBy("Company Registration Number", "Year")
    sorted_expected_df = expected_df.orderBy("Company Registration Number", "Year")

    assert sorted_result_df.collect() == sorted_expected_df.collect()
    assert sorted_result_df.schema == sorted_expected_df.schema


def test_get_bfr_it_spend_rows(bfr_it_spend_calculator: BFRITSpendCalculator, spark_session: SparkSession):
    # Given
    bfr_schema = StructType(
        [
            StructField("Category", StringType(), True),
            StructField("Company Registration Number", StringType(), True),
            StructField("EFALineNo", StringType(), True),
            StructField("Y1P1", DecimalType(18, 2), True),
            StructField("Y1P2", DecimalType(18, 2), True),
            StructField("Y2P1", DecimalType(18, 2), True),
            StructField("Y2P2", DecimalType(18, 2), True),
            StructField("Y3P1", DecimalType(18, 2), True),
            StructField("Y3P2", DecimalType(18, 2), True),
        ]
    )
    bfr_data = [
        ("IT Spend 1", "CRN001", "IT001", Decimal("100.00"), Decimal("200.00"), Decimal("300.00"), Decimal("400.00"), Decimal("500.00"), Decimal("600.00")),
        ("IT Spend 2", "CRN001", "IT002", Decimal("50.00"), Decimal("150.00"), Decimal("250.00"), Decimal("350.00"), Decimal("450.00"), Decimal("550.00")),
        ("Other Spend", "CRN001", "OTH001", Decimal("10.00"), Decimal("20.00"), Decimal("30.00"), Decimal("40.00"), Decimal("50.00"), Decimal("60.00")),
        ("Pupil Num", "CRN001", "PUPIL001", Decimal("1000.00"), Decimal("1100.00"), Decimal("1200.00"), Decimal("1300.00"), Decimal("1400.00"), Decimal("1500.00")),
        ("IT Spend 1", "CRN002", "IT001", Decimal("10.00"), Decimal("20.00"), Decimal("30.00"), Decimal("40.00"), Decimal("50.00"), Decimal("60.00")),
        ("Pupil Num", "CRN002", "PUPIL001", Decimal("100.00"), Decimal("110.00"), Decimal("120.00"), Decimal("130.00"), Decimal("140.00"), Decimal("150.00")),
    ]
    bfr_df = spark_session.createDataFrame(bfr_data, bfr_schema)

    # When
    result_df = bfr_it_spend_calculator.get_bfr_it_spend_rows(bfr_df)

    # Then
    expected_schema = StructType(
        [
            StructField("Category", StringType(), True),
            StructField("Company Registration Number", StringType(), True),
            StructField("Year", LongType(), True),
            StructField("Value", DecimalType(19, 2), True),
            StructField("Pupils", DecimalType(18, 2), True),
        ]
    )
    expected_data = [
        ("IT Spend 1", "CRN001", 2023, Decimal("300.00"), Decimal("1000.00")),
        ("IT Spend 1", "CRN001", 2024, Decimal("700.00"), Decimal("1100.00")),
        ("IT Spend 1", "CRN001", 2025, Decimal("1100.00"), Decimal("1200.00")),
        ("IT Spend 2", "CRN001", 2023, Decimal("200.00"), Decimal("1000.00")),
        ("IT Spend 2", "CRN001", 2024, Decimal("600.00"), Decimal("1100.00")),
        ("IT Spend 2", "CRN001", 2025, Decimal("1000.00"), Decimal("1200.00")),
        ("IT Spend 1", "CRN002", 2023, Decimal("30.00"), Decimal("100.00")),
        ("IT Spend 1", "CRN002", 2024, Decimal("70.00"), Decimal("110.00")),
        ("IT Spend 1", "CRN002", 2025, Decimal("110.00"), Decimal("120.00")),
    ]
    expected_df = spark_session.createDataFrame(expected_data, expected_schema)

    # Ensure column order is consistent before sorting and collecting
    expected_cols = [field.name for field in expected_schema.fields]
    result_df_ordered = result_df.select(expected_cols)

    # Sort both DataFrames before collecting for comparison
    sorted_result_df = result_df_ordered.orderBy("Company Registration Number", "Year", "Category")
    sorted_expected_df = expected_df.orderBy("Company Registration Number", "Year", "Category")

    assert sorted_result_df.collect() == sorted_expected_df.collect()
    assert sorted_result_df.schema == sorted_expected_df.schema