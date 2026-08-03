from typing import List

import numpy as np

from .metrics import *

DEFAULT_RISK_CONFIG: list = [
    EndYearBalanceMetric(
        name="EndYearBalanceAsPercentageIncomePerPupil",
        risk_group=RiskGroup.FINANCIAL,
        risk_score_maximum=4.5,
        rules=[
            MetricRule(0, np.inf, 0.0, "NoRisk", "both"),
            MetricRule(-0.01, 0, 0.25, "Minor", "neither"),
            MetricRule(-0.025, -0.01, 0.5, "Minor", "right"),
            MetricRule(-0.04, -0.025, 1.0, "Minor", "right"),
            MetricRule(-0.05, -0.04, 1.25, "Minor", "right"),
            MetricRule(-0.06, -0.05, 1.75, "Major", "right"),
            MetricRule(-0.075, -0.06, 2.0, "Major", "right"),
            MetricRule(-0.09, -0.075, 2.5, "Major", "right"),
            MetricRule(-np.inf, -0.09, 3.0, "Major", "both"),
        ],
    ),
    InterestOnLoanFlagMetric(
        name="InterestOnLoanFlag",
        risk_group=RiskGroup.FINANCIAL,
        risk_score_maximum=0.25,
        score_when_1=0.25,
        risk_when_1="Minor",
    ),
    PercentExpenditureOnPremisesMetric(
        name="PercentExpenditureOnPremises",
        risk_group=RiskGroup.FINANCIAL,
        risk_score_maximum=0.5,
        rules=[
            MetricRule(0, 0.09999, 0.0, "No Risk", "both"),
            MetricRule(0.1, 0.149999, 0.25, "Minor", "both"),
            MetricRule(0.15, 1.0, 0.5, "Minor", "both"),
        ],
    ),
    PercentExpenditureOnStaffMetric(
        name="PercentExpenditureOnStaff",
        risk_group=RiskGroup.FINANCIAL,
        risk_score_maximum=1.5,
        rules=[
            MetricRule(0, 0.799999, 0.0, "No Risk", "both"),
            MetricRule(0.8, 0.8499, 0.75, "Minor", "both"),
            MetricRule(0.85, 0.899, 1.0, "Minor", "both"),
            MetricRule(0.9, 0.9499, 1.25, "Minor", "both"),
            MetricRule(0.95, 1000000, 1.5, "Minor", "both"),
        ],
    ),
    ChangeInExpenditureOver4YearsMetric(
        name="ChangeInExpenditureOver4YearsAsAPercentageOfIncome",
        risk_group=RiskGroup.FINANCIAL,
        risk_score_maximum=1.5,
        rules=[
            MetricRule(-1000000.0, 0.15, 0.0, "None", "both"),
            MetricRule(0.150000001, 0.20, 0.5, "Minor", "both"),
            MetricRule(0.200000001, 0.30, 1.0, "Minor", "both"),
            MetricRule(0.300000001, 1000000.0, 1.5, "Major", "both"),
        ],
    ),
    DeficitInLast4YearsFlagMetric(
        name="DeficitInLast4YearsFlag",
        risk_group=RiskGroup.FINANCIAL,
        risk_score_maximum=3.0,
        score_when_1=3.0,
        risk_when_1="Major",
    ),
    CurrentLongTermSurplusABoveThresholdFor5YearsMetric(
        name="CurrentLongTermSurplusABoveThresholdFor5Years",
        risk_group=RiskGroup.FINANCIAL,
        risk_score_maximum=1.0,
        threshold=0.15,
        score_when_1=1.0,
        risk_when_1="Major",
    ),
    PreviousLongTermBalanceDeficitMetric(
        name="PreviousLongTermBalanceDeficit",
        risk_group=RiskGroup.FINANCIAL,
        risk_score_maximum=2.0,
        threshold=0.04,
        score_when_1=2.0,
        risk_when_1="Major",
    ),
    OverspendMetric(
        name="Overspend",
        risk_group=RiskGroup.FINANCIAL,
        risk_score_maximum=3.0,
        rules=[
            MetricRule(-1000000.0, -0.09, 3.0, "Major", "both"),
            MetricRule(-0.08999999, -0.075, 2.5, "Major", "both"),
            MetricRule(-0.07499999, -0.06, 2.0, "Major", "both"),
            MetricRule(-0.05999999, -0.05, 1.75, "Major", "both"),
            MetricRule(-0.04999999, -0.04, 1.25, "Minor", "both"),
            MetricRule(-0.03999999, -0.025, 1.0, "Minor", "both"),
            MetricRule(-0.02499999, -0.01, 0.5, "Minor", "both"),
            MetricRule(-0.00999999, -0.00000001, 0.25, "Minor", "both"),
            MetricRule(0.0, 10000000.0, 0.0, "None", "both"),
        ],
    ),
    LargeDecreaseInBalanceMetric(
        name="LargeDecreaseInBalanceWithoutLargeSurplus",
        risk_group=RiskGroup.FINANCIAL,
        risk_score_maximum=1.5,
        score_when_1=1.5,
        risk_when_1="Major",
    ),
    PupilNumberVarianceFromCapacityMetric(
        name="PupilNumberVarianceFromCapacity",
        risk_group=RiskGroup.SCHOOL_CHARACTERISTICS,
        risk_score_maximum=1.5,
        rules=[
            MetricRule(0.0, 0.39999999, 1.5, "Major", "both"),
            MetricRule(0.40, 0.44999999, 1.25, "Major", "both"),
            MetricRule(0.45, 0.49999999, 1.0, "Major", "both"),
            MetricRule(0.50, 0.54999999, 0.75, "Minor", "both"),
            MetricRule(0.55, 0.59999999, 0.6, "Minor", "both"),
            MetricRule(0.60, 0.64999990, 0.45, "Minor", "both"),
            MetricRule(0.65, 0.69999999, 0.3, "Minor", "both"),
            MetricRule(0.70, 0.74999999, 0.15, "Minor", "both"),
            MetricRule(0.75, 1000.0, 0.0, "None", "both"),
        ],
    ),
    PupilChangeOver1YearMetric(
        name="PupilChangeOver1Year",
        risk_group=RiskGroup.SCHOOL_CHARACTERISTICS,
        risk_score_maximum=1.5,
        rules=[
            MetricRule(-10000.0, -0.15, 1.5, "Major", "both"),
            MetricRule(-0.14999999, -0.125, 1.25, "Major", "both"),
            MetricRule(-0.12499990, -0.10, 1.0, "Major", "both"),
            MetricRule(-0.09999999, -0.08, 0.8, "Minor", "both"),
            MetricRule(-0.07999999, -0.06, 0.6, "Minor", "both"),
            MetricRule(-0.05999999, -0.04, 0.4, "Minor", "both"),
            MetricRule(-0.03999999, -0.02, 0.2, "Minor", "both"),
            MetricRule(-0.01999999, 100000.0, 0.0, "None", "both"),
        ],
    ),
    PupilChangeOver4YearsMetric(
        name="PupilChangeOver4Years",
        risk_group=RiskGroup.SCHOOL_CHARACTERISTICS,
        risk_score_maximum=1.5,
        rules=[
            MetricRule(-10000.0, -0.15, 1.5, "Major", "both"),
            MetricRule(-0.14999999, -0.125, 1.25, "Minor", "both"),
            MetricRule(-0.12499990, -0.10, 1.0, "Minor", "both"),
            MetricRule(-0.09999999, -0.08, 0.8, "Minor", "both"),
            MetricRule(-0.07999999, -0.06, 0.6, "Minor", "both"),
            MetricRule(-0.05999999, -0.04, 0.4, "Minor", "both"),
            MetricRule(-0.03999999, -0.02, 0.2, "Minor", "both"),
            MetricRule(-0.01999999, 100000.0, 0.0, "None", "both"),
        ],
    ),
    TotalPupilsSixthFormMetric(
        name="TotalPupilsSixthForm",
        risk_group=RiskGroup.SCHOOL_CHARACTERISTICS,
        risk_score_maximum=0.5,
        rules=[
            MetricRule(0, 0, 0.0, "None", "both"),
            MetricRule(1.0, 49.99999999, 0.5, "Minor", "both"),
            MetricRule(50.0, 74.99999999, 0.4, "Minor", "both"),
            MetricRule(75.0, 99.99999999, 0.3, "Minor", "both"),
            MetricRule(100.0, 124.99999999, 0.2, "Minor", "both"),
            MetricRule(125.0, 149.99999999, 0.1, "Minor", "both"),
            MetricRule(150.0, 100000000.0, 0.0, "None", "both"),
        ],
    ),
    PupilAbsenceMetric(
        name="PupilAbsence",
        risk_group=RiskGroup.EDUCATIONAL_PERFORMANCE,
        risk_score_maximum=0.5,
        condition_column="TypeOfEstablishment (code)",
        special_values=[7, 8, 12, 42, 44],
        standard_rules=[
            MetricRule(0, 0.05, 0.0, "No Risk", "left"),
            MetricRule(0.05, 0.06, 0.25, "Minor Risk", "left"),
            MetricRule(0.06, np.inf, 0.5, "Minor Risk", "both"),
        ],
        special_rules=[
            MetricRule(0, 0.15, 0.0, "No Risk", "left"),
            MetricRule(0.15, 0.25, 0.25, "Minor Risk", "left"),
            MetricRule(0.25, np.inf, 0.5, "Minor Risk", "both"),
        ],
    ),
    ParentalPreferenceMetric(
        name="ParentalPreference",
        risk_group=RiskGroup.SCHOOL_CHARACTERISTICS,
        risk_score_maximum=1.5,
        rules=[
            MetricRule(0.0, 0.59999999, 1.5, "Major", "both"),
            MetricRule(0.60, 0.64999999, 1.2, "Major", "both"),
            MetricRule(0.65, 0.69999999, 1.0, "Major", "both"),
            MetricRule(0.70, 0.74999999, 0.8, "Minor", "both"),
            MetricRule(0.75, 0.79999999, 0.6, "Minor", "both"),
            MetricRule(0.80, 0.84999990, 0.4, "Minor", "both"),
            MetricRule(0.85, 0.89999999, 0.2, "Minor", "both"),
            MetricRule(0.90, 10000.0, 0.0, "None", "both"),
        ],
    ),
    PerformanceTablesProgressScoreMetric(
        name="PerformanceTablesProgressScore",
        risk_group=RiskGroup.EDUCATIONAL_PERFORMANCE,
        risk_score_maximum=0.25,
        rules=[
            MetricRule(-100.0, -0.11000001, 0.25, "Minor", "both"),
            MetricRule(-0.11000000, 100.0, 0.0, "None", "both"),
        ],
    ),
    PerformanceTablesAchievementScoreMetric(
        name="PerformanceTablesAchievementScore",
        risk_group=RiskGroup.EDUCATIONAL_PERFORMANCE,
        risk_score_maximum=0.25,
        rules=[
            MetricRule(0.0, 0.45999990, 0.25, "Minor", "both"),
            MetricRule(0.46000000, 1.0, 0.0, "None", "both"),
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
