from pyspark import pipelines as dp
from pyspark.sql import SparkSession
from pyspark.sql.functions import col
from .bfr_pyspark_mocks import get_mock_academies_df

bronze_catalog = "catalog_30_bronze"
bfr_schema = 'bfr'
aar_schema = 'accountsreturn'
COPPER_FBIT_CATALOG_PATH = "catalog_40_copper_financial_benchmarking_insights.financial_benchmarking_insights"
BRONZE_AAR_CATALOG_PATH = f"{bronze_catalog}.aar"


@dp.view()
def bfr_sofa_current_year():
    """
    Loads the current year BFR Sofa materialized view.
    """
    spark = SparkSession.builder.getOrCreate()
    current_year = int(spark.conf.get("pipeline.bfr_year"))
    return dp.read(f"{bronze_catalog}.{bfr_schema}.vw_bfr_sofa_{current_year}")


@dp.view()
def bfr_three_year_forecast_current_year():
    """
    Loads the current year BFR Three Year Forecast materialized view.
    """
    spark = SparkSession.builder.getOrCreate()
    current_year = int(spark.conf.get("pipeline.bfr_year"))
    return dp.read(f"{bronze_catalog}.{bfr_schema}.vw_bfr_three_year_forecast_{current_year}")


@dp.view()
def academies_current_year():
    """
    Loads the current year academies data. (TODO: get rid of the mock)
    """
    spark = SparkSession.builder.getOrCreate()
    current_year = int(spark.conf.get("pipeline.aar_year"))
    academies_view_name = get_ar_cs_vw_name(current_year, year_offset=0)
    return dp.read(f"{bronze_catalog}.{aar_schema}.{academies_view_name}")


@dp.view(
  comment="Last year's BFR SOFA",
)
def bfr_sofa_historical_y1():
    """
    Loads historical BFR Sofa data for a given year offset.
    """
    spark = SparkSession.builder.getOrCreate()
    current_year = int(spark.conf.get("pipeline.bfr_year"))
    historical_year = current_year - 1
    return dp.read(f"{bronze_catalog}.{bfr_schema}.vw_bfr_sofa_{historical_year}") \
                .select("TrustUPIN", "EFALineNo", "Y1P2", "Y2P2")


@dp.view(
  comment="The year before last's BFR SOFA"
)
def bfr_sofa_historical_y2():
    """
    Loads historical BFR Sofa data for a given year offset.
    """
    spark = SparkSession.builder.getOrCreate()
    current_year = int(spark.conf.get("pipeline.bfr_year"))
    historical_year = current_year - 2
    return dp.read(f"{bronze_catalog}.{bfr_schema}.vw_bfr_sofa_{historical_year}") \
                .select("TrustUPIN", "EFALineNo", "Y1P2", "Y2P2")


def get_ar_cs_vw_name(current_year, year_offset):
    historical_year = current_year - year_offset
    ar_number = historical_year - 2015 # This works as far back as 2018
    return f"vw_ar{ar_number}_cs_benchmarkreport_{historical_year}"


@dp.table()
def academies_y2():
    spark = SparkSession.builder.getOrCreate()
    current_year = int(spark.conf.get("pipeline.aar_year"))
    academies_view_name = get_ar_cs_vw_name(current_year, year_offset=2)
    return dp.read(f"{bronze_catalog}.{aar_schema}.{academies_view_name}")


@dp.table()
def academies_y1():
    spark = SparkSession.builder.getOrCreate()
    current_year = int(spark.conf.get("pipeline.aar_year"))
    academies_view_name = get_ar_cs_vw_name(current_year, year_offset=1)
    return dp.read(f"{bronze_catalog}.{aar_schema}.{academies_view_name}")


# Example of how you would use the historical views in other dp tables
@dp.table()
def bfr_sofa_y1_processed():
    desired_efa_lines = [
        # For demonstration, using dummy values
        "100",
        "200",
    ]
    return dp.read("bfr_sofa_historical_y1") \
                .filter(col("EFALineNo").isin(desired_efa_lines))


@dp.table()
def bfr_sofa_y2_processed():
    desired_efa_lines = [
        # For demonstration, using dummy values
        "100",
        "200",
    ]
    return dp.read("bfr_sofa_historical_y2") \
                .filter(col("EFALineNo").isin(desired_efa_lines))
