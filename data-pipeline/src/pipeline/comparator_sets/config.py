# Set Sizes
BASE_SET_SIZE = 60
#BASE_SET_SIZE = 120
FINAL_SET_SIZE = 30

# Pupil Calculation Weights (Non-Special)
PUPILS_WEIGHT = 0.5
FSM_WEIGHT = 0.4
SEN_WEIGHT = 0.1
#PUPILS_WEIGHT = 1/6
#FSM_WEIGHT = 1/6
#SEN_WEIGHT = 1/6
OVERCAPACITY_WEIGHT = 0
UNDERCAPACITY_WEIGHT = 0
RURALSCORE_WEIGHT = 0

# Pupil Calculation Weights (Special)
SPECIAL_PUPILS_WEIGHT = 0.6
SPECIAL_FSM_WEIGHT = 0.4
#SPECIAL_PUPILS_WEIGHT = 1/2
#SPECIAL_FSM_WEIGHT = 1/2

# Building Calculation Weights
GIFA_WEIGHT = 0.8
AGE_WEIGHT = 0.2
#GIFA_WEIGHT = 1/5
#AGE_WEIGHT = 1/5
OLDESTBUILDINGAGE_WEIGHT = 0
NEWESTBUILDINGAGE_WEIGHT = 0
BUILDINGCOUNT_WEIGHT = 0

class ColumnNames:
    URN = "URN"
    PUPILS = "Number of pupils"
    FSM = "Percentage Free school meals"
    SEN = "Percentage SEN"
    BOARDERS = "Boarders (name)"
    PFI = "Is PFI"
    REGION = "GOR (name)"
    GIFA = "Total Internal Floor Area"
    AGE_SCORE = "Age Average Score"
    PHASE = "SchoolPhaseType"
    DID_NOT_SUBMIT = "Did Not Submit"
    PARTIAL_YEARS = "Partial Years Present"
    FINANCIAL_DATA = "Financial Data Present"
    PUPIL_DATA = "Pupil Comparator Data Present"
    BUILDING_DATA = "Building Comparator Data Present"
    SEN_NEEDS = [
        "Percentage Primary Need SPLD",
        "Percentage Primary Need MLD",
        "Percentage Primary Need PMLD",
        "Percentage Primary Need SEMH",
        "Percentage Primary Need SLCN",
        "Percentage Primary Need HI",
        "Percentage Primary Need MSI",
        "Percentage Primary Need PD",
        "Percentage Primary Need ASD",
        "Percentage Primary Need OTH",
    ]
    OVERCAPACITY = "OverCapacity"
    UNDERCAPACITY = "UnderCapacity"
    OLDESTBUILDINGAGE = "OldestBuildingAge"
    NEWESTBUILDINGAGE = "NewestBuildingAge"
    BUILDINGCOUNT = "BuildingCount"
    RURALSCORE = "RuralScore"


cols_for_comparators_parquet = [
    "OfstedRating (name)",
    "Percentage SEN",
    "Percentage Free school meals",
    "Number of pupils",
    "Total Internal Floor Area",
    "Age Average Score",
    "Is PFI",
    "Boarders (name)",
    "GOR (name)",
    "SchoolPhaseType",
    "Percentage Primary Need SPLD",
    "Percentage Primary Need MLD",
    "Percentage Primary Need PMLD",
    "Percentage Primary Need SEMH",
    "Percentage Primary Need SLCN",
    "Percentage Primary Need HI",
    "Percentage Primary Need MSI",
    "Percentage Primary Need PD",
    "Percentage Primary Need ASD",
    "Percentage Primary Need OTH",
    "Partial Years Present",
    "Financial Data Present",
    "Pupil Comparator Data Present",
    "Building Comparator Data Present",
    "Did Not Submit",
    "Pupil",
    "Building"
    "OverCapacity",
    "UnderCapacity",
    "OldestBuildingAge",
    "NewestBuildingAge",
    "BuildingCount",
    "RuralScore"
]
