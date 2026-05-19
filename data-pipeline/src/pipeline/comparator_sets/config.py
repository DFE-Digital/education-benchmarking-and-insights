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

#TO DO: change weight scheme names to be more transparent
#this will affect write up of analysis and temporary storage of files etc.

#Weight scheme/features
WEIGHT_SCHEME = "baseline_geog"
#baseline - calculated with the features and weights used by the current FBIT service - should be used with the "legacy" selection method to replicate this
#geog_dist - as the crow flies distance as the only feature, use with with "distance_only" or "distance_boarding_pfi" selection method
#ew - as per baseline, but the weights have been set equally between features
#afe - all of the original and all of the additional features, weighted equally, but not including geographic distance
#afe_with_geog - as per afe, but using as the crow flies distance - use with the "distance_boarding_pfi" or "distance_only" selection methods
#ew_with_geog - baseline features, but with as the crow flies distance and each feature is equally weighted - use with the "distance_boarding_pfi" or "distance_only" selection methods
#

SAVE_SIMILARITY_DISTANCES = False

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
    SPLITSITE = 0
    EASTING_WEIGHT = 0
    NORTHING_WEIGHT = 0

if WEIGHT_SCHEME == "baseline_geog":
    #pupil weights (non-special)
    PUPILS_WEIGHT = 1/4 * 0.5
    FSM_WEIGHT = 1/4 * 0.4
    SEN_WEIGHT = 1/4 * 0.1
    OVERCAPACITY_WEIGHT = 0
    UNDERCAPACITY_WEIGHT = 0
    RURALSCORE_WEIGHT = 0
    EASTING_WEIGHT = 1/8
    NORTHING_WEIGHT = 1/8

    #pupil weights (special)
    SPECIAL_PUPILS_WEIGHT = 1/3 *0.6
    SPECIAL_FSM_WEIGHT = 1/3 * 0.4
    EASTING_WEIGHT = 1/6
    NORTHING_WEIGHT = 1/6

    #building weights
    GIFA_WEIGHT = 1/3 * 0.8
    AGE_WEIGHT = 1/3 * 0.2
    OLDESTBUILDINGAGE_WEIGHT = 0
    NEWESTBUILDINGAGE_WEIGHT = 0
    BUILDINGCOUNT_WEIGHT = 0
    SPLITSITE = 0
    EASTING_WEIGHT = 1/6
    NORTHING_WEIGHT = 1/6

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
    SPLITSITE = 0
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
    SPLITSITE = 0
    EASTING_WEIGHT = 1/2
    NORTHING_WEIGHT = 1/2

elif WEIGHT_SCHEME == "ew_with_geog":
    #pupil weights (non-special)
    PUPILS_WEIGHT = 2/8 #1/5 #replace with 2/8
    FSM_WEIGHT = 2/8 #1/5
    SEN_WEIGHT = 2/8 #1/5
    OVERCAPACITY_WEIGHT = 0
    UNDERCAPACITY_WEIGHT = 0
    RURALSCORE_WEIGHT = 0
    EASTING_WEIGHT = 1/8 #1/5 #replace with 1/8
    NORTHING_WEIGHT = 1/8 #1/5

    #pupil weights (special)
    SPECIAL_PUPILS_WEIGHT = 2/6 #1/4 #replace with 2/6
    SPECIAL_FSM_WEIGHT = 2/6 #1/4
    EASTING_WEIGHT = 1/6 #1/4 #replace with 1/6
    NORTHING_WEIGHT = 1/6 #1/4

    #building weights
    GIFA_WEIGHT = 2/6 #1/4 #replace with 2/6
    AGE_WEIGHT = 2/6 #1/4
    OLDESTBUILDINGAGE_WEIGHT = 0
    NEWESTBUILDINGAGE_WEIGHT = 0
    BUILDINGCOUNT_WEIGHT = 0
    SPLITSITE = 0
    EASTING_WEIGHT = 1/6 #1/4 #replace with 1/6
    NORTHING_WEIGHT = 1/6 #1/4

elif WEIGHT_SCHEME == "afe_with_geog":
    #pupil weights (non-special)
    PUPILS_WEIGHT = 2/14 #1/8 #replace with 2/14
    FSM_WEIGHT = 2/14 #1/8
    SEN_WEIGHT = 2/14 #1/8
    OVERCAPACITY_WEIGHT = 2/14 #1/8
    UNDERCAPACITY_WEIGHT = 2/14 #1/8
    RURALSCORE_WEIGHT = 2/14 #1/8
    EASTING_WEIGHT = 1/14 #1/8 # replace with 1/14
    NORTHING_WEIGHT = 1/14 #1/8

    #pupil weights (special)
    SPECIAL_PUPILS_WEIGHT = 2/6 #1/4 #replace with 2/6
    SPECIAL_FSM_WEIGHT = 2/6 #1/4
    EASTING_WEIGHT = 1/6 #1/4 #replace with 1/6
    NORTHING_WEIGHT = 1/6 #1/4

    #building weights
    GIFA_WEIGHT = 2/14#2/12 #1/7 #replace with 2/12
    AGE_WEIGHT = 2/14 #2/12 #1/7
    OLDESTBUILDINGAGE_WEIGHT = 2/14 #2/12 #1/7
    NEWESTBUILDINGAGE_WEIGHT = 2/14 #2/12 #1/7
    BUILDINGCOUNT_WEIGHT = 2/14 #2/12 #1/7
    SPLITSITE = 2/24
    EASTING_WEIGHT = 1/14 #1/12 #1/7 #replace with 1/12
    NORTHING_WEIGHT = 1/14 #1/12 #1/7

elif WEIGHT_SCHEME == "pupils_gifa_geog":
    #pupil weights (non-special)
    PUPILS_WEIGHT = 2/4 #1/3 #replace with 2/4
    FSM_WEIGHT = 0
    SEN_WEIGHT = 0
    OVERCAPACITY_WEIGHT = 0
    UNDERCAPACITY_WEIGHT = 0
    RURALSCORE_WEIGHT = 0
    EASTING_WEIGHT = 1/4 #1/3 #replace with 1/4
    NORTHING_WEIGHT = 1/4 #1/3

    #pupil weights (special)
    SPECIAL_PUPILS_WEIGHT = 2/4 #1/3 #replace with 2/4
    SPECIAL_FSM_WEIGHT = 0
    EASTING_WEIGHT = 1/4 #1/3 #replace with 1/4
    NORTHING_WEIGHT = 1/4 #1/3

    #building weights
    GIFA_WEIGHT = 2/4 #1/3 #replace with 2/4
    AGE_WEIGHT = 0
    OLDESTBUILDINGAGE_WEIGHT = 0
    NEWESTBUILDINGAGE_WEIGHT = 0
    BUILDINGCOUNT_WEIGHT = 0
    SPLITSITE = 0
    EASTING_WEIGHT = 1/4 #1/3 #replace with 1/4
    NORTHING_WEIGHT = 1/4 #1/3

elif WEIGHT_SCHEME == "fsm_age_geog":
    #pupil weights (non-special)
    PUPILS_WEIGHT = 0
    FSM_WEIGHT = 2/4 #1/3 #replace with 2/4
    SEN_WEIGHT = 0
    OVERCAPACITY_WEIGHT = 0
    UNDERCAPACITY_WEIGHT = 0
    RURALSCORE_WEIGHT = 0
    EASTING_WEIGHT = 1/4 #1/3 #replace with 1/4
    NORTHING_WEIGHT = 1/4 #1/3

    #pupil weights (special)
    SPECIAL_PUPILS_WEIGHT = 0
    SPECIAL_FSM_WEIGHT = 2/4 #1/3 #replace with 2/4
    EASTING_WEIGHT = 1/4 #1/3 #replace with 1/4
    NORTHING_WEIGHT = 1/4 #1/3

    #building weights
    GIFA_WEIGHT = 0
    AGE_WEIGHT = 2/4 #1/3 #replace with 2/4
    OLDESTBUILDINGAGE_WEIGHT = 0
    NEWESTBUILDINGAGE_WEIGHT = 0
    BUILDINGCOUNT_WEIGHT = 0
    SPLITSITE = 0
    EASTING_WEIGHT = 1/4 #1/3 #replace with 1/4
    NORTHING_WEIGHT = 1/4 #1/3

elif WEIGHT_SCHEME == "sen_oldest_geog":
    #pupil weights (non-special)
    PUPILS_WEIGHT = 0
    FSM_WEIGHT = 0
    SEN_WEIGHT = 2/4 #1/3 #replace with 2/4
    OVERCAPACITY_WEIGHT = 0
    UNDERCAPACITY_WEIGHT = 0
    RURALSCORE_WEIGHT = 0
    EASTING_WEIGHT = 1/4 #1/3 #replace with 1/4
    NORTHING_WEIGHT = 1/4 #1/3

    #pupil weights (special)
    SPECIAL_PUPILS_WEIGHT = 0
    SPECIAL_FSM_WEIGHT = 0
    EASTING_WEIGHT = 1/2
    NORTHING_WEIGHT = 1/2

    #building weights
    GIFA_WEIGHT = 0
    AGE_WEIGHT = 0
    OLDESTBUILDINGAGE_WEIGHT = 2/4 #1/3 #replace with 2/4
    NEWESTBUILDINGAGE_WEIGHT = 0
    BUILDINGCOUNT_WEIGHT = 0
    SPLITSITE = 0
    EASTING_WEIGHT = 1/4 #1/3 #replace with 1/4
    NORTHING_WEIGHT = 1/4 #1/3

elif WEIGHT_SCHEME == "overcapacity_newest_geog":
    #pupil weights (non-special)
    PUPILS_WEIGHT = 0
    FSM_WEIGHT = 0
    SEN_WEIGHT = 0
    OVERCAPACITY_WEIGHT = 2/4 #1/3 #replace with 2/4
    UNDERCAPACITY_WEIGHT = 0
    RURALSCORE_WEIGHT = 0
    EASTING_WEIGHT = 1/4 #1/3 #replace with 1/4
    NORTHING_WEIGHT = 1/4 #1/3

    #pupil weights (special)
    SPECIAL_PUPILS_WEIGHT = 0
    SPECIAL_FSM_WEIGHT = 0
    EASTING_WEIGHT = 1/3
    NORTHING_WEIGHT = 1/3

    #building weights
    GIFA_WEIGHT = 0
    AGE_WEIGHT = 0
    OLDESTBUILDINGAGE_WEIGHT = 0
    NEWESTBUILDINGAGE_WEIGHT = 2/4 #1/3 #replace with 2/4
    BUILDINGCOUNT_WEIGHT = 0
    SPLITSITE = 0
    EASTING_WEIGHT = 1/4 #1/3 #replace with 1/4
    NORTHING_WEIGHT = 1/4 #1/3

elif WEIGHT_SCHEME == "undercapacity_buildingcount_geog":
    #pupil weights (non-special)
    PUPILS_WEIGHT = 0
    FSM_WEIGHT = 0
    SEN_WEIGHT = 0
    OVERCAPACITY_WEIGHT = 0
    UNDERCAPACITY_WEIGHT = 2/4 #1/3 #replace with 2/4
    RURALSCORE_WEIGHT = 0
    EASTING_WEIGHT = 1/4 #1/3 #replace with 1/4
    NORTHING_WEIGHT = 1/4 #1/3

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
    BUILDINGCOUNT_WEIGHT = 2/4 #1/3 #replace with 2/4
    SPLITSITE = 0
    EASTING_WEIGHT = 1/4 #1/3 #replace with 1/4
    NORTHING_WEIGHT = 1/4 #1/3

elif WEIGHT_SCHEME == "ruralscore_split-site_geog":
    #pupil weights (non-special)
    PUPILS_WEIGHT = 0
    FSM_WEIGHT = 0
    SEN_WEIGHT = 0
    OVERCAPACITY_WEIGHT = 0
    UNDERCAPACITY_WEIGHT = 0
    RURALSCORE_WEIGHT = 2/4 #1/3 #replace with 2/4
    EASTING_WEIGHT = 1/4 #1/3 #replace with 1/4
    NORTHING_WEIGHT = 1/4 #1/3

    #pupil weights (special)
    SPECIAL_PUPILS_WEIGHT = 0
    SPECIAL_FSM_WEIGHT = 0
    EASTING_WEIGHT = 1/2
    NORTHING_WEIGHT = 1/2

    #building weights
    GIFA_WEIGHT = 0 #1/5 #replace with 2/8
    AGE_WEIGHT = 0 #1/5
    OLDESTBUILDINGAGE_WEIGHT = 0
    NEWESTBUILDINGAGE_WEIGHT = 0
    BUILDINGCOUNT_WEIGHT = 0 #1/5
    SPLITSITE = 2/4
    EASTING_WEIGHT = 1/4 #1/5 # replace with 1/8
    NORTHING_WEIGHT = 1/4 #1/5

#need to add in weight schemes which include sparsity, once that data is avaialble

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
    OVERCAPACITY = "pupils_over_capacity_count" #"OverCapacity"
    UNDERCAPACITY = "unfilled_places_count" #"UnderCapacity"
    OLDESTBUILDINGAGE = "OldestBuildingAge"
    NEWESTBUILDINGAGE = "NewestBuildingAge"
    BUILDINGCOUNT = "BuildingCount"
    RURALSCORE = "RuralScore"
    EASTING = "Easting"
    NORTHING = "Northing"
    SPLITSITE = "SplitSiteScore"


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
    "pupils_over_capacity_count",
    "unfilled_places_count",
    "OldestBuildingAge",
    "NewestBuildingAge",
    "BuildingCount",
    "RuralScore",
    "Easting",
    "Northing",
    "SplitSiteScore"
]
