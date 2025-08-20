BATCH_LOG_INTERVAL = 1000
MAX_DECILE_INDEX = 9
PERCENTILE_TO_DECILE_DIVISOR = 10
FLOOR_AREA_PERCENTAGE_TOLERANCE = 0.1
BUILDING_AGE_YEAR_TOLERANCE = 20.0
PUPIL_COUNT_PERCENTAGE_TOLERANCE = 0.25
FSM_PERCENTAGE_TOLERANCE = 0.05
SEN_PERCENTAGE_TOLERANCE = 0.1

BASE_COLUMNS = [
    "URN",
    "Total Internal Floor Area",
    "Age Average Score",
    "OfstedRating (name)",
    "Percentage SEN",
    "Percentage Free school meals",
    "Number of pupils",
]

RAG_RESULT_COLUMNS = [
    "URN",
    "Category",
    "SubCategory",
    "Value",
    "Median",
    "DiffMedian",
    "Key",
    "PercentDiff",
    "Percentile",
    "Decile",
    "RAG",
]
