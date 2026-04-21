# Set Sizes
BASE_SET_SIZE = 60
#BASE_SET_SIZE = 120
FINAL_SET_SIZE = 30

#Selection order/method
#SELECTION_METHOD = "legacy" # Existing method - pick smallest distance in same region first, then top up to 30 with non-region smallest distance
#SELECTION_METHOD = "top_up_local" # Pick the ten with the absolute smallest distances, then top up to 30 with the remaining 20 from same region in order of distance
#SELECTION_METHOD = "distance_only" # Pick the 30 with the absolute smallest distances, regardless of region - any locality should be included in the weight scheme with this option, if needed
SELECTION_METHOD = "distance_boarding_pfi" # Pick the 30 with the absolute smallest distances, but choose schools with the same boarding/PFI status first (as per orginal method) - no selection by region, any geographic distance should be included in the weight scheme with this option
#SELECTION_METHOD = "local_only" # Pick schools from the same region as the target, up to the final set size but less if there aren't enough

#Weight scheme/features
WEIGHT_SCHEME = "geog_dist"
#"geog_dist"
#"baseline"
#swc
#ew
#afe
#geog_dist

SAVE_SIMILARITY_DISTANCES = False

# Pupil Calculation Weights (Non-Special)
if WEIGHT_SCHEME == "baseline":
    #pupil weights (non-special)
    PUPILS_WEIGHT = 0.5
    FSM_WEIGHT = 0.4
    SEN_WEIGHT = 0.1
    OVERCAPACITY_WEIGHT = 0
    UNDERCAPACITY_WEIGHT = 0
    RURALSCORE_WEIGHT = 0
    EASTING_WEIGHT = 0
    NORTHING_WEIGHT = 0

    #pupil weights (special)
    SPECIAL_PUPILS_WEIGHT = 0.6
    SPECIAL_FSM_WEIGHT = 0.4
    EASTING_WEIGHT = 0
    NORTHING_WEIGHT = 0

    #building weights
    GIFA_WEIGHT = 0.8
    AGE_WEIGHT = 0.2
    OLDESTBUILDINGAGE_WEIGHT = 0
    NEWESTBUILDINGAGE_WEIGHT = 0
    BUILDINGCOUNT_WEIGHT = 0
    EASTING_WEIGHT = 0
    NORTHING_WEIGHT = 0

elif WEIGHT_SCHEME == "ew":
    #pupil weights (non-special)
    PUPILS_WEIGHT = 1/3
    FSM_WEIGHT = 1/3
    SEN_WEIGHT = 1/3
    OVERCAPACITY_WEIGHT = 0
    UNDERCAPACITY_WEIGHT = 0
    RURALSCORE_WEIGHT = 0
    EASTING_WEIGHT = 0
    NORTHING_WEIGHT = 0

    #pupil weights (special)
    SPECIAL_PUPILS_WEIGHT = 1/2
    SPECIAL_FSM_WEIGHT = 1/2
    EASTING_WEIGHT = 0
    NORTHING_WEIGHT = 0

    #building weights
    GIFA_WEIGHT = 1/2
    AGE_WEIGHT = 1/2
    OLDESTBUILDINGAGE_WEIGHT = 0
    NEWESTBUILDINGAGE_WEIGHT = 0
    BUILDINGCOUNT_WEIGHT = 0
    EASTING_WEIGHT = 0
    NORTHING_WEIGHT = 0

elif WEIGHT_SCHEME == "afe":
    #pupil weights (non-special)
    PUPILS_WEIGHT = 1/6
    FSM_WEIGHT = 1/6
    SEN_WEIGHT = 1/6
    OVERCAPACITY_WEIGHT = 1/6
    UNDERCAPACITY_WEIGHT = 1/6
    RURALSCORE_WEIGHT = 1/6
    EASTING_WEIGHT = 0
    NORTHING_WEIGHT = 0

    #pupil weights (special)
    SPECIAL_PUPILS_WEIGHT = 1/2
    SPECIAL_FSM_WEIGHT = 1/2
    EASTING_WEIGHT = 0
    NORTHING_WEIGHT = 0

    #building weights
    GIFA_WEIGHT = 1/5
    AGE_WEIGHT = 1/5
    OLDESTBUILDINGAGE_WEIGHT = 1/5
    NEWESTBUILDINGAGE_WEIGHT = 1/5
    BUILDINGCOUNT_WEIGHT = 1/5
    EASTING_WEIGHT = 0
    NORTHING_WEIGHT = 0

elif WEIGHT_SCHEME == "geog_dist":
    #pupil weights (non-special)
    PUPILS_WEIGHT = 0
    FSM_WEIGHT = 0
    SEN_WEIGHT = 0
    OVERCAPACITY_WEIGHT = 0
    UNDERCAPACITY_WEIGHT = 0
    RURALSCORE_WEIGHT = 0
    EASTING_WEIGHT = 1/2
    NORTHING_WEIGHT = 1/2

    #pupil weights (special)
    SPECIAL_PUPILS_WEIGHT = 0
    SPECIAL_FSM_WEIGHT = 0
    EASTING_WEIGHT = 1/2
    NORTHING_WEIGHT = 1/2

    #building weights
    GIFA_WEIGHT = 0
    AGE_WEIGHT = 0
    OLDESTBUILDINGAGE_WEIGHT = 0
    NEWESTBUILDINGAGE_WEIGHT = 0
    BUILDINGCOUNT_WEIGHT = 0
    EASTING_WEIGHT = 1/2
    NORTHING_WEIGHT = 1/2

elif WEIGHT_SCHEME == "ew_with_geog":
    #pupil weights (non-special)
    PUPILS_WEIGHT = 1/5
    FSM_WEIGHT = 1/5
    SEN_WEIGHT = 1/5
    OVERCAPACITY_WEIGHT = 0
    UNDERCAPACITY_WEIGHT = 0
    RURALSCORE_WEIGHT = 0
    EASTING_WEIGHT = 1/5
    NORTHING_WEIGHT = 1/5

    #pupil weights (special)
    SPECIAL_PUPILS_WEIGHT = 1/4
    SPECIAL_FSM_WEIGHT = 1/4
    EASTING_WEIGHT = 1/4
    NORTHING_WEIGHT = 1/4

    #building weights
    GIFA_WEIGHT = 1/4
    AGE_WEIGHT = 1/4
    OLDESTBUILDINGAGE_WEIGHT = 0
    NEWESTBUILDINGAGE_WEIGHT = 0
    BUILDINGCOUNT_WEIGHT = 0
    EASTING_WEIGHT = 1/4
    NORTHING_WEIGHT = 1/4

elif WEIGHT_SCHEME == "afe_with_geog":
    #pupil weights (non-special)
    PUPILS_WEIGHT = 1/8
    FSM_WEIGHT = 1/8
    SEN_WEIGHT = 1/8
    OVERCAPACITY_WEIGHT = 1/8
    UNDERCAPACITY_WEIGHT = 1/8
    RURALSCORE_WEIGHT = 1/8
    EASTING_WEIGHT = 1/8
    NORTHING_WEIGHT = 1/8

    #pupil weights (special)
    SPECIAL_PUPILS_WEIGHT = 1/4
    SPECIAL_FSM_WEIGHT = 1/4
    EASTING_WEIGHT = 1/4
    NORTHING_WEIGHT = 1/4

    #building weights
    GIFA_WEIGHT = 1/7
    AGE_WEIGHT = 1/7
    OLDESTBUILDINGAGE_WEIGHT = 1/7
    NEWESTBUILDINGAGE_WEIGHT = 1/7
    BUILDINGCOUNT_WEIGHT = 1/7
    EASTING_WEIGHT = 1/7
    NORTHING_WEIGHT = 1/7

else:
   raise ValueError("Weight scheme not recognised") 


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
    EASTING = "Easting"
    NORTHING = "Northing"


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
    "Building",
    "OverCapacity",
    "UnderCapacity",
    "OldestBuildingAge",
    "NewestBuildingAge",
    "BuildingCount",
    "RuralScore",
    "Easting",
    "Northing"
]
