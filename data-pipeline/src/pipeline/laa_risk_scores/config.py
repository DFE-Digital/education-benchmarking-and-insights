from typing import List, Mapping

import numpy as np

from .metrics import *

DEFAULT_RISK_CONFIG: list = [
    EndYearBalanceMetric(
        name="EndYearBalanceAsPercentageIncome",
        risk_group=RiskGroup.FINANCIAL,
        risk_score_maximum=4.5,
        rules=[
            MetricRule(0, np.inf, 0.0, RiskFlag.NONE.value, "both"),
            MetricRule(-0.01, 0, 0.25, RiskFlag.MINOR.value, "neither"),
            MetricRule(-0.025, -0.01, 0.5, RiskFlag.MINOR.value, "right"),
            MetricRule(-0.04, -0.025, 1.0, RiskFlag.MINOR.value, "right"),
            MetricRule(-0.05, -0.04, 1.25, RiskFlag.MINOR.value, "right"),
            MetricRule(-0.06, -0.05, 1.75, RiskFlag.MAJOR.value, "right"),
            MetricRule(-0.075, -0.06, 2.0, RiskFlag.MAJOR.value, "right"),
            MetricRule(-0.09, -0.075, 2.5, RiskFlag.MAJOR.value, "right"),
            MetricRule(-np.inf, -0.09, 3.0, RiskFlag.MAJOR.value, "both"),
        ],
    ),
    InterestOnLoanFlagMetric(
        name="InterestOnLoanFlag",
        risk_group=RiskGroup.FINANCIAL,
        risk_score_maximum=0.25,
        score_when_1=0.25,
        risk_when_1=RiskFlag.MINOR.value,
    ),
    PercentExpenditureOnPremisesMetric(
        name="PercentExpenditureOnPremises",
        risk_group=RiskGroup.FINANCIAL,
        risk_score_maximum=0.5,
        rules=[
            MetricRule(0, 0.09999, 0.0, RiskFlag.NONE.value, "both"),
            MetricRule(0.1, 0.149999, 0.25, RiskFlag.MINOR.value, "both"),
            MetricRule(0.15, 1.0, 0.5, RiskFlag.MINOR.value, "both"),
        ],
    ),
    PercentExpenditureOnStaffMetric(
        name="PercentExpenditureOnStaff",
        risk_group=RiskGroup.FINANCIAL,
        risk_score_maximum=1.5,
        rules=[
            MetricRule(0, 0.799999, 0.0, RiskFlag.NONE.value, "both"),
            MetricRule(0.8, 0.8499, 0.75, RiskFlag.MINOR.value, "both"),
            MetricRule(0.85, 0.899, 1.0, RiskFlag.MINOR.value, "both"),
            MetricRule(0.9, 0.9499, 1.25, RiskFlag.MINOR.value, "both"),
            MetricRule(0.95, 1000000, 1.5, RiskFlag.MINOR.value, "both"),
        ],
    ),
    ChangeInExpenditureOver4YearsMetric(
        name="ChangeInExpenditureOver4YearsAsAPercentageOfIncome",
        risk_group=RiskGroup.FINANCIAL,
        risk_score_maximum=1.5,
        rules=[
            MetricRule(-1000000.0, 0.15, 0.0, RiskFlag.NONE.value, "both"),
            MetricRule(0.150000001, 0.20, 0.5, RiskFlag.MINOR.value, "both"),
            MetricRule(0.200000001, 0.30, 1.0, RiskFlag.MINOR.value, "both"),
            MetricRule(0.300000001, 1000000.0, 1.5, RiskFlag.MAJOR.value, "both"),
        ],
    ),
    DeficitInLast4YearsFlagMetric(
        name="DeficitInLast4YearsFlag",
        risk_group=RiskGroup.FINANCIAL,
        risk_score_maximum=3.0,
        score_when_1=3.0,
        risk_when_1=RiskFlag.MAJOR.value,
    ),
    CurrentLongTermSurplusABoveThresholdFor5YearsMetric(
        name="CurrentLongTermSurplusABoveThresholdFor5Years",
        risk_group=RiskGroup.FINANCIAL,
        risk_score_maximum=1.0,
        threshold=0.15,
        score_when_1=1.0,
        risk_when_1=RiskFlag.MAJOR.value,
    ),
    PreviousLongTermBalanceDeficitMetric(
        name="PreviousLongTermBalanceDeficit",
        risk_group=RiskGroup.FINANCIAL,
        risk_score_maximum=2.0,
        threshold=0.04,
        score_when_1=2.0,
        risk_when_1=RiskFlag.MAJOR.value,
    ),
    OverspendMetric(
        name="Overspend",
        risk_group=RiskGroup.FINANCIAL,
        risk_score_maximum=3.0,
        rules=[
            MetricRule(-1000000.0, -0.09, 3.0, RiskFlag.MAJOR.value, "both"),
            MetricRule(-0.08999999, -0.075, 2.5, RiskFlag.MAJOR.value, "both"),
            MetricRule(-0.07499999, -0.06, 2.0, RiskFlag.MAJOR.value, "both"),
            MetricRule(-0.05999999, -0.05, 1.75, RiskFlag.MAJOR.value, "both"),
            MetricRule(-0.04999999, -0.04, 1.25, RiskFlag.MINOR.value, "both"),
            MetricRule(-0.03999999, -0.025, 1.0, RiskFlag.MINOR.value, "both"),
            MetricRule(-0.02499999, -0.01, 0.5, RiskFlag.MINOR.value, "both"),
            MetricRule(-0.00999999, -0.00000001, 0.25, RiskFlag.MINOR.value, "both"),
            MetricRule(0.0, 10000000.0, 0.0, RiskFlag.NONE.value, "both"),
        ],
    ),
    LargeDecreaseInBalanceMetric(
        name="LargeDecreaseInBalanceWithoutLargeSurplus",
        risk_group=RiskGroup.FINANCIAL,
        risk_score_maximum=1.5,
        score_when_1=1.5,
        risk_when_1=RiskFlag.MAJOR.value,
    ),
    PupilNumberVarianceFromCapacityMetric(
        name="PupilNumberVarianceFromCapacity",
        risk_group=RiskGroup.SCHOOL_CHARACTERISTICS,
        risk_score_maximum=1.5,
        rules=[
            MetricRule(0.0, 0.39999999, 1.5, RiskFlag.MAJOR.value, "both"),
            MetricRule(0.40, 0.44999999, 1.25, RiskFlag.MAJOR.value, "both"),
            MetricRule(0.45, 0.49999999, 1.0, RiskFlag.MAJOR.value, "both"),
            MetricRule(0.50, 0.54999999, 0.75, RiskFlag.MINOR.value, "both"),
            MetricRule(0.55, 0.59999999, 0.6, RiskFlag.MINOR.value, "both"),
            MetricRule(0.60, 0.64999990, 0.45, RiskFlag.MINOR.value, "both"),
            MetricRule(0.65, 0.69999999, 0.3, RiskFlag.MINOR.value, "both"),
            MetricRule(0.70, 0.74999999, 0.15, RiskFlag.MINOR.value, "both"),
            MetricRule(0.75, 1000.0, 0.0, RiskFlag.NONE.value, "both"),
        ],
    ),
    PupilChangeOver1YearMetric(
        name="PupilChangeOver1Year",
        risk_group=RiskGroup.SCHOOL_CHARACTERISTICS,
        risk_score_maximum=1.5,
        rules=[
            MetricRule(-10000.0, -0.15, 1.5, RiskFlag.MAJOR.value, "both"),
            MetricRule(-0.14999999, -0.125, 1.25, RiskFlag.MAJOR.value, "both"),
            MetricRule(-0.12499990, -0.10, 1.0, RiskFlag.MAJOR.value, "both"),
            MetricRule(-0.09999999, -0.08, 0.8, RiskFlag.MINOR.value, "both"),
            MetricRule(-0.07999999, -0.06, 0.6, RiskFlag.MINOR.value, "both"),
            MetricRule(-0.05999999, -0.04, 0.4, RiskFlag.MINOR.value, "both"),
            MetricRule(-0.03999999, -0.02, 0.2, RiskFlag.MINOR.value, "both"),
            MetricRule(-0.01999999, 100000.0, 0.0, RiskFlag.NONE.value, "both"),
        ],
    ),
    PupilChangeOver4YearsMetric(
        name="PupilChangeOver4Years",
        risk_group=RiskGroup.SCHOOL_CHARACTERISTICS,
        risk_score_maximum=1.5,
        rules=[
            MetricRule(-10000.0, -0.15, 1.5, RiskFlag.MAJOR.value, "both"),
            MetricRule(-0.14999999, -0.125, 1.25, RiskFlag.MINOR.value, "both"),
            MetricRule(-0.12499990, -0.10, 1.0, RiskFlag.MINOR.value, "both"),
            MetricRule(-0.09999999, -0.08, 0.8, RiskFlag.MINOR.value, "both"),
            MetricRule(-0.07999999, -0.06, 0.6, RiskFlag.MINOR.value, "both"),
            MetricRule(-0.05999999, -0.04, 0.4, RiskFlag.MINOR.value, "both"),
            MetricRule(-0.03999999, -0.02, 0.2, RiskFlag.MINOR.value, "both"),
            MetricRule(-0.01999999, 100000.0, 0.0, RiskFlag.NONE.value, "both"),
        ],
    ),
    PupilsSixthFormMetric(
        name="PupilsSixthForm",
        risk_group=RiskGroup.SCHOOL_CHARACTERISTICS,
        risk_score_maximum=0.5,
        rules=[
            MetricRule(0, 0, 0.0, RiskFlag.NONE.value, "both"),
            MetricRule(1.0, 49.99999999, 0.5, RiskFlag.MINOR.value, "both"),
            MetricRule(50.0, 74.99999999, 0.4, RiskFlag.MINOR.value, "both"),
            MetricRule(75.0, 99.99999999, 0.3, RiskFlag.MINOR.value, "both"),
            MetricRule(100.0, 124.99999999, 0.2, RiskFlag.MINOR.value, "both"),
            MetricRule(125.0, 149.99999999, 0.1, RiskFlag.MINOR.value, "both"),
            MetricRule(150.0, 100000000.0, 0.0, RiskFlag.NONE.value, "both"),
        ],
    ),
    PupilAbsenceMetric(
        name="PupilAbsence",
        risk_group=RiskGroup.EDUCATIONAL_PERFORMANCE,
        risk_score_maximum=0.5,
        condition_column="TypeOfEstablishment (code)",
        special_values=[7, 8, 12, 42, 44],
        standard_rules=[
            MetricRule(0, 5, 0.0, RiskFlag.NONE.value, "left"),
            MetricRule(5, 6, 0.25, RiskFlag.MINOR_RISK.value, "left"),
            MetricRule(6, np.inf, 0.5, RiskFlag.MINOR_RISK.value, "both"),
        ],
        special_rules=[
            MetricRule(0, 15, 0.0, RiskFlag.NONE.value, "left"),
            MetricRule(15, 25, 0.25, RiskFlag.MINOR_RISK.value, "left"),
            MetricRule(25, np.inf, 0.5, RiskFlag.MINOR_RISK.value, "both"),
        ],
    ),
    ParentalPreferenceMetric(
        name="ParentalPreference",
        risk_group=RiskGroup.SCHOOL_CHARACTERISTICS,
        risk_score_maximum=1.5,
        rules=[
            MetricRule(0.0, 0.59999999, 1.5, RiskFlag.MAJOR.value, "both"),
            MetricRule(0.60, 0.64999999, 1.2, RiskFlag.MAJOR.value, "both"),
            MetricRule(0.65, 0.69999999, 1.0, RiskFlag.MAJOR.value, "both"),
            MetricRule(0.70, 0.74999999, 0.8, RiskFlag.MINOR.value, "both"),
            MetricRule(0.75, 0.79999999, 0.6, RiskFlag.MINOR.value, "both"),
            MetricRule(0.80, 0.84999990, 0.4, RiskFlag.MINOR.value, "both"),
            MetricRule(0.85, 0.89999999, 0.2, RiskFlag.MINOR.value, "both"),
            MetricRule(0.90, 10000.0, 0.0, RiskFlag.NONE.value, "both"),
        ],
    ),
    PerformanceTablesProgressScoreMetric(
        name="PerformanceTablesProgressScore",
        risk_group=RiskGroup.EDUCATIONAL_PERFORMANCE,
        risk_score_maximum=0.25,
        rules=[
            MetricRule(-100.0, -0.11000001, 0.25, RiskFlag.MINOR.value, "both"),
            MetricRule(-0.11000000, 100.0, 0.0, RiskFlag.NONE.value, "both"),
        ],
    ),
    PerformanceTablesAchievementScoreMetric(
        name="PerformanceTablesAchievementScore",
        risk_group=RiskGroup.EDUCATIONAL_PERFORMANCE,
        risk_score_maximum=0.25,
        rules=[
            MetricRule(0.0, 0.45999990, 0.25, RiskFlag.MINOR.value, "both"),
            MetricRule(0.46000000, 1.0, 0.0, RiskFlag.NONE.value, "both"),
        ],
    ),
]

# The grading engine uses np.select, which evaluates conditions sequentially (first-match-wins).
DEFAULT_GRADING_THRESHOLDS: List[GradeThreshold] = [
    GradeThreshold(
        grade="A*",
        max_score=0.0,
        max_risks=0,
        max_major_risks=0,
        min_parental_pref=1.0,
        min_attainment=0.69,
    ),
    GradeThreshold(
        grade="A",
        max_score=0.5,
        max_risks=1,
        max_major_risks=0,
        min_parental_pref=0.9,
        min_attainment=0.58,
    ),
    GradeThreshold(
        grade="B",
        max_score=1.0,
        max_risks=2,
        max_major_risks=0,
    ),
    GradeThreshold(
        grade="C",
        max_score=2.5,
        max_risks=4,
        max_major_risks=1,
    ),
    GradeThreshold(
        grade="D",
        max_score=3.5,
        max_risks=6,
        max_major_risks=2,
    ),
    GradeThreshold(
        grade="E",
        max_score=5.0,
        max_risks=8,
        max_major_risks=3,
    ),
    GradeThreshold(
        grade="F",
        max_score=7.0,
        max_risks=12,
        max_major_risks=4,
    ),
    GradeThreshold(
        grade="G",
        max_score=float("inf"),
        max_risks=999,
        max_major_risks=999,
    ),
]


def get_yearly_risk_config(year: int) -> List[BaseRiskMetric]:
    # Optionally route years to configs
    return DEFAULT_RISK_CONFIG


def get_yearly_grading_thresholds(year: int) -> List[GradeThreshold]:
    # Optionally route years to configs
    return DEFAULT_GRADING_THRESHOLDS


laa_ancillary_files = {
    "default": {
        "absences": "Absence_2term_school.csv",
        "capacity": "capacity_school_200910-202425.csv",
        "capacity_special": "specialist-provision_school_202223-202425.csv",
        "parental_preference": "AppsandOffers_2025_SchoolLevel07102025.csv",
    }
}


def get_laa_ancillary_filenames(year: int) -> Mapping:
    # Optionally route years to configs
    return laa_ancillary_files["default"]


laa_ancillary_columns = {
    "default": {
        "absences": {
            "time_period": "Int64",
            "school_urn": "Int64",
            "sess_overall_percent": "float",
        },
        "capacity": {
            "time_period": "Int64",
            "school_urn": "Int64",
            "school_places": "float",
        },
        "capacity_special": {
            "time_period": "Int64",
            "school_urn": "Int64",
            "school_places": "float",
        },
        "parental_preference": {
            "time_period": "Int64",
            "school_urn": "Int64",
            "times_put_as_1st_preference": "float",
            "total_number_places_offered": "float",
        },
    }
}


def get_laa_ancillary_columns(year: int) -> Mapping:
    # Optionally route years to configs
    return laa_ancillary_columns["default"]


laa_risk_scores_column_eval = {
    "default": {
        "TotalIncomePerPupil": "`Total Income` / `Number of pupils`",
        "TotalExpenditurePerPupil": "`Total Expenditure` / `Number of pupils`",
        "LAAStaffExpenditureRollup": (
            "`Teaching and Teaching support staff_Teaching staff` + "
            "`Teaching and Teaching support staff_Supply teaching staff` + "
            "`Teaching and Teaching support staff_Education support staff` + "
            "`Non-educational support staff and services_Administrative and clerical staff` + "
            "`Non-educational support staff and services_Other staff` + "
            "`Other costs_Indirect employee expenses` + "
            "`Other costs_Staff development and training` + "
            "`Other costs_Supply teacher insurance` + "
            "`Other costs_Staff-related insurance` + "
            "`Administrative supplies_Administrative supplies (non educational)` + "
            "`Teaching and Teaching support staff_Agency supply teaching staff` + "
            "`Non-educational support staff and services_Professional services (non-curriculum)` + "
            "`Other costs_PFI charges` - "
            "`Income_Receipts supply teacher insurance`"
        ),
        "NetExpenditure": (
            "`Total Expenditure` - ("
            "`Income_Receipts supply teacher insurance` + "
            "`Income_Catering services` + "
            "`Income_Other Revenue Income`"
            ")"
        ),
        "LAAPremisesExpenditureRollup": (
            "`Premises staff and services_Premises staff` + "
            "`Premises staff and services_Maintenance of premises` + "
            "`Other costs_Grounds maintenance` + "
            "`Premises staff and services_Cleaning and caretaking` + "
            "`Utilities_Water and sewerage` + "
            "`Premises staff and services_Other occupation costs`"
        ),
    }
}


def get_laa_column_eval(year: int) -> Mapping:
    # Optionally route years to configs
    return laa_risk_scores_column_eval["default"]


laa_download_schemas = {
    "default": [
        "URN",
        "TypeOfEstablishment (code)",
        "Overall Phase",
        "Revenue reserve",
        "Revenue reserve_y_minus_one",
        "Revenue reserve_y_minus_two",
        "Revenue reserve_y_minus_three",
        "Revenue reserve_y_minus_four",
        "Total Income",
        "Total Income_y_minus_one",
        "Total Income_y_minus_two",
        "Total Income_y_minus_three",
        "Total Income_y_minus_four",
        "Total Expenditure",
        "Total Expenditure_y_minus_four",
        "Number of pupils",
        "Number of pupils_y_minus_one",
        "Number of pupils_y_minus_four",
        "Other costs_Interest charges for loan and bank",
        "TotalPupilsSixthForm",
        "Ks2Progress",
        "Progress8Measure",
        "PTRWM_EXP",
        "AverageAttainment",
        "LAAStaffExpenditureRollup",
        "LAAPremisesExpenditureRollup",
        "NetExpenditure",
        "Teaching and Teaching support staff_Teaching staff",
        "Teaching and Teaching support staff_Supply teaching staff",
        "Teaching and Teaching support staff_Education support staff",
        "Non-educational support staff and services_Administrative and clerical staff",
        "Non-educational support staff and services_Other staff",
        "Other costs_Indirect employee expenses",
        "Other costs_Staff development and training",
        "Other costs_Supply teacher insurance",
        "Other costs_Staff-related insurance",
        "Administrative supplies_Administrative supplies (non educational)",
        "Teaching and Teaching support staff_Agency supply teaching staff",
        "Non-educational support staff and services_Professional services (non-curriculum)",
        "Other costs_PFI charges",
        "Income_Receipts supply teacher insurance",
        "Income_Catering services",
        "Income_Other Revenue Income",
        "Premises staff and services_Premises staff",
        "Premises staff and services_Maintenance of premises",
        "Other costs_Grounds maintenance",
        "Premises staff and services_Cleaning and caretaking",
        "Utilities_Water and sewerage",
        "Premises staff and services_Other occupation costs",
        "school_places",
        "sess_overall_percent",
        "proportion_1stprefs_v_totaloffers"
    ]
}


def get_download_file_schema(year: int) -> List[str]:
    # Optionally route years to configs
    columns = laa_download_schemas.get(year, laa_download_schemas["default"])

    evaluators = get_yearly_risk_config(year)
    metric_cols = []
    for metric in evaluators:
        metric_cols.extend(metric.get_all_cols())

    return columns + metric_cols
