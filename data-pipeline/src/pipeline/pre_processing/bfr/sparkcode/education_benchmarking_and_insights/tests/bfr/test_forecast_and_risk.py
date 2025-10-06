import numpy as np
import pandas as pd
import pytest
from pyspark.sql.types import (
    DoubleType,
    IntegerType,
    StringType,
    StructField,
    StructType,
)

from education_benchmarking_and_insights.bfr.forecast_and_risk import (
    BFRForecastAndRiskCalculator,
)


@pytest.fixture
def mock_config():
    class MockPipelineConfig:
        SOFA_TRUST_REVENUE_RESERVE_EFALINE = 430
        SOFA_PUPIL_NUMBER_EFALINE = 1

    return MockPipelineConfig()


@pytest.fixture
def bfr_forecast_and_risk_calculator(spark_session, mock_config):
    return BFRForecastAndRiskCalculator(2025, spark_session, mock_config)


def test_calculate_slopes(bfr_forecast_and_risk_calculator, spark_session):
    # Prepare input data for _calculate_slopes
    data = [
        {
            "Company Registration Number": "1",
            "Y-2": 2,
            "Y-1": 4,
            "Y1": 6,
            "Y2": 8,
            "Y3": 10,
            "Y4": 12,
        },
        {
            "Company Registration Number": "2",
            "Y-2": 1,
            "Y-1": 2,
            "Y1": 3,
            "Y2": 4,
            "Y3": 5,
            "Y4": 6,
        },
    ]
    bfr_df = spark_session.createDataFrame(data)
    year_columns = ["Y-2", "Y-1", "Y1", "Y2", "Y3", "Y4"]

    # Call the method under test
    result_df = bfr_forecast_and_risk_calculator._calculate_slopes(bfr_df, year_columns)
    result_pdf = result_df.toPandas()

    # Assertions
    expected_slopes = np.array([2.0, 1.0])
    assert "Slope" in result_pdf.columns
    pd.testing.assert_series_equal(
        pd.Series(result_pdf["Slope"].values, name="Slope"),
        pd.Series(expected_slopes, name="Slope"),
        check_exact=False,
        rtol=1e-9,
    )


def test_assign_slope_flag(bfr_forecast_and_risk_calculator, spark_session):
    # Prepare input data for _assign_slope_flag with multiple slopes per company
    data = [
        {"Company Registration Number": "C1", "Slope": 0.1},
        {"Company Registration Number": "C1", "Slope": -0.5},
        {"Company Registration Number": "C1", "Slope": 1.0},
        {"Company Registration Number": "C1", "Slope": -2.0},
        {"Company Registration Number": "C1", "Slope": 0.0},
        {"Company Registration Number": "C1", "Slope": 0.5},
        {"Company Registration Number": "C1", "Slope": -0.1},
        {"Company Registration Number": "C1", "Slope": 0.2},
        {"Company Registration Number": "C1", "Slope": -0.8},
        {"Company Registration Number": "C1", "Slope": 1.5},
        {"Company Registration Number": "C2", "Slope": 10.0},
        {"Company Registration Number": "C2", "Slope": 20.0},
        {"Company Registration Number": "C2", "Slope": 30.0},
        {"Company Registration Number": "C2", "Slope": 40.0},
        {"Company Registration Number": "C2", "Slope": 50.0},
    ]
    bfr_with_slopes_df = spark_session.createDataFrame(data)

    # Call the method under test
    result_df = bfr_forecast_and_risk_calculator._assign_slope_flag(bfr_with_slopes_df)
    result_pdf = result_df.toPandas()

    expected_flags_c1 = [0, 0, 0, -1, 0, 0, 0, 0, 0, 1]
    expected_flags_c2 = [-1, 0, 0, 0, 1]

    # Combine expected flags in the order of the original 'data' list
    expected_flags_raw = expected_flags_c1 + expected_flags_c2

    expected_df = pd.DataFrame(data)
    expected_df["Slope flag"] = expected_flags_raw

    result_pdf_sorted = result_pdf.sort_values(
        by=["Company Registration Number", "Slope"]
    ).reset_index(drop=True)
    expected_df_sorted = expected_df.sort_values(
        by=["Company Registration Number", "Slope"]
    ).reset_index(drop=True)

    pd.testing.assert_series_equal(
        pd.Series(
            result_pdf_sorted["Slope flag"].values, name="Slope flag", dtype="int32"
        ),
        pd.Series(
            expected_df_sorted["Slope flag"].values, name="Slope flag", dtype="int32"
        ),
        check_exact=True,
    )


def test_build_bfr_historical_data_academies_none(
    bfr_forecast_and_risk_calculator, spark_session
):
    # Test case where academies_historical is None
    bfr_sofa_historical_data = [
        {"Trust UPIN": "0", "EFALineNo": 430, "Y1P2": 2048.0, "Y2P2": 1024.0}
    ]
    bfr_sofa_historical_df = spark_session.createDataFrame(bfr_sofa_historical_data)

    result = bfr_forecast_and_risk_calculator._build_bfr_historical_data(
        academies_historical=None,
        bfr_sofa_historical=bfr_sofa_historical_df,
    )
    assert result is None


def test_build_bfr_historical_data_bfr_sofa_none(
    bfr_forecast_and_risk_calculator, spark_session
):
    # Test case where bfr_sofa_historical is None
    academies_historical_data = [
        {
            "Trust UPIN": "0",
            "Company Registration Number": "0",
            "Total pupils in trust": 100,
        }
    ]
    academies_historical_df = spark_session.createDataFrame(academies_historical_data)

    result_df = bfr_forecast_and_risk_calculator._build_bfr_historical_data(
        academies_historical=academies_historical_df,
        bfr_sofa_historical=None,
    )
    result_pdf = result_df.toPandas()

    assert "Trust Revenue reserve" in result_pdf.columns
    assert list(result_pdf["Trust Revenue reserve"]) == [0.0]
    assert "Total pupils in trust" in result_pdf.columns
    assert list(result_pdf["Total pupils in trust"]) == [0]


def test_build_bfr_historical_data_both_none(bfr_forecast_and_risk_calculator):
    # Test case where both inputs are None
    result = bfr_forecast_and_risk_calculator._build_bfr_historical_data(
        academies_historical=None,
        bfr_sofa_historical=None,
    )
    assert result is None


def test_build_bfr_historical_data_empty_bfr_sofa(
    bfr_forecast_and_risk_calculator, spark_session
):
    # Test case where bfr_sofa_historical is an empty DataFrame
    academies_historical_data = [
        {
            "Trust UPIN": "0",
            "Company Registration Number": "0",
            "Total pupils in trust": 100,
        }
    ]
    academies_historical_df = spark_session.createDataFrame(academies_historical_data)

    # Correct schema definition using StructType and StructField
    empty_bfr_sofa_schema = StructType(
        [
            StructField("Trust UPIN", StringType(), True),
            StructField("EFALineNo", IntegerType(), True),
            StructField("Y1P2", DoubleType(), True),
            StructField("Y2P2", DoubleType(), True),
        ]
    )
    empty_bfr_sofa_df = spark_session.createDataFrame([], schema=empty_bfr_sofa_schema)

    result_df = bfr_forecast_and_risk_calculator._build_bfr_historical_data(
        academies_historical=academies_historical_df,
        bfr_sofa_historical=empty_bfr_sofa_df,
    )
    result_pdf = result_df.toPandas()

    assert "Trust Revenue reserve" in result_pdf.columns
    assert list(result_pdf["Trust Revenue reserve"]) == [0.0]
    assert "Total pupils in trust" in result_pdf.columns
    assert list(result_pdf["Total pupils in trust"]) == [100]


def test_build_bfr_historical_data_full_data(
    bfr_forecast_and_risk_calculator, spark_session
):
    # Test case with full data
    academies_historical_data = [
        {
            "Trust UPIN": "0",
            "Company Registration Number": "0",
            "Total pupils in trust": 100,
        }
    ]
    academies_historical_df = spark_session.createDataFrame(academies_historical_data)

    bfr_sofa_historical_data = [
        {
            "Trust UPIN": "0",
            "EFALineNo": 430,
            "Y1P2": 2048.0,
            "Y2P2": 1024.0,
        },  # Revenue reserve
        {"Trust UPIN": "0", "EFALineNo": 1, "Y1P2": 500.0, "Y2P2": 0.0},  # Pupil number
    ]
    bfr_sofa_historical_df = spark_session.createDataFrame(bfr_sofa_historical_data)

    result_df = bfr_forecast_and_risk_calculator._build_bfr_historical_data(
        academies_historical=academies_historical_df,
        bfr_sofa_historical=bfr_sofa_historical_df,
    )
    result_pdf = result_df.toPandas()

    assert "Trust Revenue reserve" in result_pdf.columns
    # 1024.0 * 1000 = 1024000.0
    assert result_pdf["Trust Revenue reserve"].iloc[0] == 1_024_000.0
    assert "Total pupils in trust" in result_pdf.columns
    assert result_pdf["Total pupils in trust"].iloc[0] == 100
