from pyspark import pipelines as dp
from pyspark.sql import SparkSession
from pyspark.sql.functions import col

BRONZE_BFR_CATALOG_PATH = "catalog_30_bronze.bfr"
COPPER_FBIT_CATALOG_PATH = "catalog_40_copper_financial_benchmarking_insights.financial_benchmarking_insights"
BRONZE_AAR_CATALOG_PATH = "catalog_30_bronze.aar"

@dp.view()
def bfr_sofa_current_year():
    """
    Loads the current year BFR Sofa materialized view.
    """
    spark = SparkSession.builder.getOrCreate()
    current_year = int(spark.conf.get("pipeline.year"))
    return dp.read(f"{COPPER_FBIT_CATALOG_PATH}.mv_BFR_Sofa_{current_year}")

@dp.view()
def bfr_three_year_forecast_current_year():
    """
    Loads the current year BFR Three Year Forecast materialized view.
    """
    spark = SparkSession.builder.getOrCreate()
    current_year = int(spark.conf.get("pipeline.year"))
    return dp.read(f"{COPPER_FBIT_CATALOG_PATH}.mv_BFR_Three_Year_Forecast_{current_year}")

# @dp.view
# def academies_current_year():
#     """
#     Loads the current year academies data.
#     """
#     spark = SparkSession.builder.getOrCreate()
#     current_year = int(spark.conf.get("pipeline.year"))
#     return dp.read(f"{BRONZE_AAR_CATALOG_PATH}.vw_academies_{current_year}")

@dp.view(
  comment="Last year's BFR SOFA"
)
def bfr_sofa_historical_y1():
    """
    Loads historical BFR Sofa data for a given year offset.
    """
    spark = SparkSession.builder.getOrCreate()
    current_year = int(spark.conf.get("pipeline.year"))
    historical_year = current_year - 1
    return dp.read(f"{BRONZE_BFR_CATALOG_PATH}.vw_BFR_Sofa_{historical_year}") \
                .select("TrustUPIN", "EFALineNo", "Y1P2", "Y2P2")

@dp.view(
  comment="The year before last's BFR SOFA"
)
def bfr_sofa_historical_y2():
    """
    Loads historical BFR Sofa data for a given year offset.
    """
    spark = SparkSession.builder.getOrCreate()
    current_year = int(spark.conf.get("pipeline.year"))
    historical_year = current_year - 2
    return dp.read(f"{BRONZE_BFR_CATALOG_PATH}.vw_BFR_Sofa_{historical_year}") \
                .select("TrustUPIN", "EFALineNo", "Y1P2", "Y2P2")

# @dp.view
# def academies_historical():
#     """
#     Loads historical academies data for a given year offset.
#     """
#     spark = SparkSession.builder.getOrCreate()
#     current_year = int(spark.conf.get("pipeline.year"))
#     historical_year = current_year - 1
#     return dp.read(f"{BRONZE_AAR_CATALOG_PATH}.vw_academies_{historical_year}")

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

# @dp.table
# def academies_y1_processed():
#     return dp.read("academies_historical", year_offset=1)

# @dp.table
# def academies_y2_processed():
#     return dp.read("academies_historical", year_offset=2)