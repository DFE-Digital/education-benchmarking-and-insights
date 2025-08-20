# Set Sizes
BASE_SET_SIZE = 60
FINAL_SET_SIZE = 30

# Pupil Calculation Weights (Non-Special)
PUPILS_WEIGHT = 0.5
FSM_WEIGHT = 0.4
SEN_WEIGHT = 0.1

# Pupil Calculation Weights (Special)
SPECIAL_PUPILS_WEIGHT = 0.6
SPECIAL_FSM_WEIGHT = 0.4

# Building Calculation Weights
GIFA_WEIGHT = 0.8
AGE_WEIGHT = 0.2


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