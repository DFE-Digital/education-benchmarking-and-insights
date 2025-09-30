import findspark

findspark.init()

from pyspark.sql import SparkSession

# Create a local SparkSession
spark = (
    SparkSession.builder.master("local[*]")
    .appName("LocalSparkTesting")
    .config("spark.driver.memory", "4g")
    .config("spark.driver.host", "127.0.0.1")
    .config("spark.driver.bindAddress", "127.0.0.1")
    .getOrCreate()
)

# Your Spark code goes here
df = spark.createDataFrame([(1, "A"), (2, "B")], ["id", "value"])
df.show()

spark.stop()
