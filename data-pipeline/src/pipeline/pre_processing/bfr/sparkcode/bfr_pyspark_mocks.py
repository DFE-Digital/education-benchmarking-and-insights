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
        ]
    else:
        data = [
            (100001, "Trust A", 298, 1000.0, 1100.0, 1200.0, 1300.0),
            (100002, "Trust B", 430, 2000.0, 2100.0, 2200.0, 2300.0),
        ]

    return spark.createDataFrame(data, schema=schema)


def get_mock_bfr_three_year_mv(spark: SparkSession) -> DataFrame:
    """
    Generates a mock DataFrame for BFR Three Year Forecast materialized view.
    """
    schema = _get_schema_from_dict(bfr_3y_cols)

    data = [
        (100001, 298, 100.0, 110.0, 120.0),
        (100002, 430, 200.0, 210.0, 220.0),
    ]
    return spark.createDataFrame(data, schema=schema)
