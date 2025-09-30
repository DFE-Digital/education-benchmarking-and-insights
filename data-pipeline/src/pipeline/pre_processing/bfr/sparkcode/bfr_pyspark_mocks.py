from pyspark.sql import DataFrame, SparkSession
from pyspark.sql.types import (
    DoubleType,
    IntegerType,
    StringType,
    StructField,
    StructType,
)

from pipeline.input_schemas.bfr import bfr_3y_cols, bfr_sofa_cols


def _get_schema_from_dict(schema_dict: dict) -> StructType:
    fields = []
    for col_name, col_type_str in schema_dict.items():
        if col_type_str == "Int64":
            fields.append(StructField(col_name, IntegerType(), True))
        elif col_type_str == "float":
            fields.append(StructField(col_name, DoubleType(), True))
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
            (100001, "Trust A", 298, 1000.0, 1100.0, 1200.0, 1300.0, 1400.0, 1500.0),
            (100002, "Trust B", 430, 2000.0, 2100.0, 2200.0, 2300.0, 2400.0, 2500.0),
            (100001, "Trust A", 211, 100.0, 110.0, 120.0, 130.0, 140.0, 150.0), # SOFA_SELF_GENERATED_INCOME_EFALINES
            (100001, "Trust A", 220, 50.0, 55.0, 60.0, 65.0, 70.0, 75.0), # SOFA_SELF_GENERATED_INCOME_EFALINES
            (100001, "Trust A", 999, 10000.0, 10500.0, 11000.0, 11500.0, 12000.0, 12500.0), # SOFA_PUPIL_NUMBER_EFALINE - not scaled
            (100001, "Trust A", 199, 200.0, 210.0, 220.0, 230.0, 240.0, 250.0), # SOFA_GRANT_FUNDING_EFALINES
            (100001, "Trust A", 200, 75.0, 80.0, 85.0, 90.0, 95.0, 100.0), # SOFA_GRANT_FUNDING_EFALINES
            (100001, "Trust A", 980, 3000.0, 3100.0, 3200.0, 3300.0, 3400.0, 3500.0), # SOFA_SUBTOTAL_INCOME_EFALINE
            (100001, "Trust A", 335, 400.0, 410.0, 420.0, 430.0, 440.0, 450.0), # SOFA_OTHER_COSTS_EFALINE
            (100001, "Trust A", 380, 500.0, 510.0, 520.0, 530.0, 540.0, 550.0), # SOFA_TOTAL_REVENUE_EXPENDITURE
            (100001, "Trust A", 336, 10.0, 11.0, 12.0, 13.0, 14.0, 15.0), # SOFA_IT_SPEND_LINES
            (100002, "Trust B", 211, 150.0, 160.0, 170.0, 180.0, 190.0, 200.0),
            (100002, "Trust B", 999, 20000.0, 21000.0, 22000.0, 23000.0, 24000.0, 25000.0),
        ]
    else:
        data = [
            (100001, "Trust A", 298, 1000.0, 1100.0, 1200.0, 1300.0),
            (100002, "Trust B", 430, 2000.0, 2100.0, 2200.0, 2300.0),
            (100001, "Trust A", 211, 100.0, 110.0, 120.0, 130.0),
            (100001, "Trust A", 999, 10000.0, 10500.0, 11000.0, 11500.0),
            (100001, "Trust A", 199, 200.0, 210.0, 220.0, 230.0),
            (100001, "Trust A", 980, 3000.0, 3100.0, 3200.0, 3300.0),
            (100001, "Trust A", 335, 400.0, 410.0, 420.0, 430.0),
            (100001, "Trust A", 380, 500.0, 510.0, 520.0, 530.0),
            (100002, "Trust B", 211, 150.0, 160.0, 170.0, 180.0),
            (100002, "Trust B", 999, 20000.0, 21000.0, 22000.0, 23000.0),
        ]

    return spark.createDataFrame(data, schema=schema)


def get_mock_bfr_three_year_mv(spark: SparkSession) -> DataFrame:
    """
    Generates a mock DataFrame for BFR Three Year Forecast materialized view.
    """
    schema = _get_schema_from_dict(bfr_3y_cols)

    data = [
        (100001, 2980, 100.0, 110.0, 120.0), # Maps to 298
        (100002, 4300, 200.0, 210.0, 220.0), # Maps to 430
        (100001, 3800, 150.0, 160.0, 170.0), # Maps to 380
        (100001, 9000, 1000.0, 1100.0, 1200.0), # Maps to 999
        (100001, 335, 50.0, 55.0, 60.0), # OTHER_COSTS_EFALINE
    ]
    return spark.createDataFrame(data, schema=schema)


def get_mock_academies_df(spark: SparkSession, year: int) -> DataFrame:
    """
    Generates a mock DataFrame for academies data.
    """
    schema = StructType(
        [
            StructField("Trust UPIN", IntegerType(), True),
            StructField("Company Registration Number", StringType(), True),
            StructField("Total pupils in trust", DoubleType(), True),
        ]
    )

    data = [
        (100001, "CRN001", 1000.0),
        (100002, "CRN002", 2000.0),
        (100003, "CRN003", 1500.0),
    ]
    return spark.createDataFrame(data, schema=schema)
