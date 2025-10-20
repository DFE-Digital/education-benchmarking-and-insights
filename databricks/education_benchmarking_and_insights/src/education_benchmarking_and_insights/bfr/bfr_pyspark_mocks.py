from pyspark.sql import DataFrame, SparkSession
from pyspark.sql.types import (
    DoubleType,
    IntegerType,
    StringType,
    StructField,
    StructType,
    FloatType,
)

from .config import bfr_3y_cols, bfr_sofa_cols


def _get_schema_from_dict(schema_dict: dict) -> StructType:
    fields = []
    for col_name, col_type_str in schema_dict.items():
        if col_type_str == "Int64":
            fields.append(StructField(col_name, IntegerType(), True))
        elif col_type_str == "float":
            fields.append(StructField(col_name, FloatType(), True))
        elif col_type_str == "string":
            fields.append(StructField(col_name, StringType(), True))
        else:
            raise ValueError(f"Unknown type: {col_type_str}")
    return StructType(fields)


def get_mock_bfr_sofa_mv(spark: SparkSession, year: int) -> DataFrame:
    """
    Generates a mock DataFrame for BFR SOFA materialized view.
    """
    schema_dict = bfr_sofa_cols.get(year, bfr_sofa_cols["default"])
    schema = _get_schema_from_dict(schema_dict)

    data = []
    if year == 2025:
        data = [
            (
                100001,
                "Total revenue income",
                298,
                1000.0,
                1100.0,
                1200.0,
                1300.0,
                1400.0,
                1500.0,
            ),
            (
                100001,
                "Balance c/f to next period ",
                430,
                200.0,
                210.0,
                220.0,
                230.0,
                240.0,
                250.0,
            ),
            (
                100001,
                "Self-gen 1",
                211,
                100.0,
                110.0,
                120.0,
                130.0,
                140.0,
                150.0,
            ),
            (100001, "Self-gen 2", 220, 50.0, 55.0, 60.0, 65.0, 70.0, 75.0),
            (
                100001,
                "Pupil numbers (actual and estimated)",
                999,
                1000.0,
                1050.0,
                1100.0,
                1150.0,
                1200.0,
                1250.0,
            ),
            (100001, "Grant funding", 199, 200.0, 210.0, 220.0, 230.0, 240.0, 250.0),
            (100001, "Grant funding", 200, 75.0, 80.0, 85.0, 90.0, 95.0, 100.0),
            (
                100001,
                "Subtotal income",
                980,
                3000.0,
                3100.0,
                3200.0,
                3300.0,
                3400.0,
                3500.0,
            ),
            (100001, "Other costs", 335, 400.0, 410.0, 420.0, 430.0, 440.0, 450.0),
            (
                100001,
                "Total revenue expenditure",
                380,
                500.0,
                510.0,
                520.0,
                530.0,
                540.0,
                550.0,
            ),
            (100001, "IT backend", 336, 10.0, 11.0, 12.0, 13.0, 14.0, 15.0),
            (100001, "Onsite Servers", 337, 5.0, 6.0, 7.0, 8.0, 9.0, 10.0),
            (
                100002,
                "Total revenue income",
                298,
                2000.0,
                2100.0,
                2200.0,
                2300.0,
                2400.0,
                2500.0,
            ),
            (
                100002,
                "Balance c/f to next period ",
                430,
                400.0,
                420.0,
                440.0,
                460.0,
                480.0,
                500.0,
            ),
            (
                100002,
                "Self-gen 1",
                211,
                150.0,
                160.0,
                170.0,
                180.0,
                190.0,
                200.0,
            ),
            (
                100002,
                "Pupil numbers (actual and estimated)",
                999,
                2000.0,
                2100.0,
                2200.0,
                2300.0,
                2400.0,
                2500.0,
            ),
            (100002, "IT backend", 336, 20.0, 22.0, 24.0, 26.0, 28.0, 30.0),
            (
                100003,
                "Total revenue income",
                298,
                1500.0,
                1600.0,
                1700.0,
                1800.0,
                1900.0,
                2000.0,
            ),
            (
                100003,
                "Balance c/f to next period ",
                430,
                300.0,
                310.0,
                320.0,
                330.0,
                340.0,
                350.0,
            ),
            (
                100003,
                "Pupil numbers (actual and estimated)",
                999,
                1500.0,
                1550.0,
                1600.0,
                1650.0,
                1700.0,
                1750.0,
            ),
            (100003, "Onsite Servers", 337, 15.0, 16.0, 17.0, 18.0, 19.0, 20.0),
        ]
    else:
        data = [
            (100001, "Total revenue income", 298, 1000.0, 1100.0, 1200.0, 1300.0),
            (100001, "Balance c/f to next period ", 430, 200.0, 210.0, 220.0, 230.0),
            (100001, "self-gen 1", 211, 100.0, 110.0, 120.0, 130.0),
            (100001, "Self-gen 2", 220, 50.0, 55.0, 60.0, 65.0),
            (100001, "IT backend", 336, 10.0, 11.0, 12.0, 13.0),
            (
                100001,
                "Pupil numbers (actual and estimated)",
                999,
                1000.0,
                1050.0,
                1100.0,
                1150.0,
            ),
            (100001, "Grant funding", 199, 200.0, 210.0, 220.0, 230.0),
            (100001, "Subtotal income", 980, 3000.0, 3100.0, 3200.0, 3300.0),
            (100001, "Other costs", 335, 400.0, 410.0, 420.0, 430.0),
            (100001, "Total revenue expenditure", 380, 500.0, 510.0, 520.0, 530.0),
            (100002, "Total revenue income", 298, 2000.0, 2100.0, 2200.0, 2300.0),
            (100002, "Balance c/f to next period ", 430, 400.0, 420.0, 440.0, 460.0),
            (100002, "Self-gen 1", 211, 150.0, 160.0, 170.0, 180.0),
            (
                100002,
                "Pupil numbers (actual and estimated)",
                999,
                2000.0,
                2100.0,
                2200.0,
                2300.0,
            ),
            (100003, "Total revenue income", 298, 1500.0, 1600.0, 1700.0, 1800.0),
            (100003, "Balance c/f to next period ", 430, 300.0, 310.0, 320.0, 330.0),
            (
                100003,
                "Pupil numbers (actual and estimated)",
                999,
                1500.0,
                1550.0,
                1600.0,
                1650.0,
            ),
        ]

    return spark.createDataFrame(data, schema=schema)


def get_mock_bfr_three_year_mv(spark: SparkSession) -> DataFrame:
    """
    Generates a mock DataFrame for BFR Three Year Forecast materialized view.
    """
    schema = _get_schema_from_dict(bfr_3y_cols)

    data = [
        (100001, 2980, 100.0, 110.0, 120.0),  # Maps to 298
        (100001, 4300, 200.0, 210.0, 220.0),  # Maps to 430
        (100001, 3800, 150.0, 160.0, 170.0),  # Maps to 380
        (100001, 9000, 1000.0, 1100.0, 1200.0),  # Maps to 999
        (100001, 335, 50.0, 55.0, 60.0),  # OTHER_COSTS_EFALINE
        (100002, 2980, 200.0, 210.0, 220.0),
        (100002, 4300, 400.0, 420.0, 440.0),
        (100002, 3800, 300.0, 310.0, 320.0),
        (100002, 9000, 2000.0, 2100.0, 2200.0),
        (100003, 2980, 150.0, 160.0, 170.0),
        (100003, 4300, 300.0, 310.0, 320.0),
        (100003, 9000, 1500.0, 1550.0, 1600.0),
    ]
    return spark.createDataFrame(data, schema=schema)


def get_mock_academies_df(spark: SparkSession, year: int) -> DataFrame:
    """
    Generates a mock DataFrame for academies data.
    """
    schema = StructType(
        [
            StructField("Lead_UPIN", IntegerType(), True),
            StructField("Company_Number", StringType(), True),
            StructField("TotalPupilsInTrust", FloatType(), True),
        ]
    )

    data = [
        (100001, "CRN001", 1000.0),
        (100002, "CRN002", 2000.0),
        (100003, "CRN003", 1500.0),
    ]
    return spark.createDataFrame(data, schema=schema)
