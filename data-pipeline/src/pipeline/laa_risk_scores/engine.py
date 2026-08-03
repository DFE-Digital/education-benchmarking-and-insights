from typing import List, Optional, Tuple
import numpy as np
import pandas as pd

from .config import get_yearly_risk_config, get_yearly_grading_thresholds
from .metrics import BaseRiskMetric, RiskGroup, GradeThreshold


def derive_laa_risk_scores(
    cfr_with_all_extra_data: pd.DataFrame,
    run_year: Optional[int] = None
) -> Tuple[pd.DataFrame, pd.DataFrame]:
    """Coordinating dynamic metric execution, scoring, grading, and db melting."""
    df = cfr_with_all_extra_data.copy()

    # Compute shared base columns upfront for metrics to use
    df["TotalIncomePerPupil"] = df["Total Income"] / df["Number of pupils"]
    df["TotalExpenditurePerPupil"] = df["Total Expenditure"] / df["Number of pupils"]
    df["LAAStaffExpenditureRollup"] = df[[
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
        "Other costs_PFI charges"
    ]].sum(axis=1) - df["Income_Receipts supply teacher insurance"]
    df["NetExpenditure"] = df["Total Expenditure"] - (
       df[[
            "Income_Receipts supply teacher insurance",
            "Income_Catering services",
            "Income_Other Revenue Income",
       ]].sum(axis=1)
    )

    risk_metrics = pd.DataFrame()
    risk_metrics["URN"] = df["URN"]
    risk_metrics["TypeOfEstablishment (code)"] = df["TypeOfEstablishment (code)"]
    risk_metrics["Overall Phase"] = df["Overall Phase"]
    risk_metrics["proportion_1stprefs_v_totaloffers"] = df["proportion_1stprefs_v_totaloffers"]

    year = run_year if run_year is not None else 2024
    evaluators = get_yearly_risk_config(year)

    for metric in evaluators:
        metric.execute(df)
        
        # Populate target outputs
        risk_metrics[metric.value_column] = df[metric.value_column]
        risk_metrics[metric.score_column] = df[metric.score_column]
        risk_metrics[metric.flag_column] = df[metric.flag_column]
        
        # Capture previous year results if applicable
        if hasattr(metric, "prev_year_column"):
            prev_col = metric.prev_year_column
            risk_metrics[prev_col] = df[prev_col]
            risk_metrics[f"{prev_col}_Score"] = df[f"{prev_col}_Score"]
            risk_metrics[f"{prev_col}_Risk"] = df[f"{prev_col}_Risk"]
            
    grading_thresholds = get_yearly_grading_thresholds(year)
    risk_scores_headlines = roll_up_laa_risk_scores_to_headlines(risk_metrics, evaluators, grading_thresholds)
    indicators, headers = melt_laa_risk_scores(risk_scores_headlines, evaluators, run_id=str(year))

    return indicators, headers


def roll_up_laa_risk_scores_to_headlines(
    risk_metrics: pd.DataFrame,
    evaluators: List[BaseRiskMetric],
    grading_thresholds: Optional[List[GradeThreshold]] = None,
) -> pd.DataFrame:
    """Grades each school based on its risk scores and flags according to policies."""
    if grading_thresholds is None:
        raise ValueError("Missing grading thresholds")

    df = risk_metrics.copy()

    score_cols = [metric.score_column for metric in evaluators]
    risk_cols = [metric.flag_column for metric in evaluators]

    total_score = df[score_cols].sum(axis=1)
    total_risks = df[risk_cols].isin(["Minor", "Minor Risk", "Major"]).sum(axis=1)
    total_major_risks = df[risk_cols].isin(["Major"]).sum(axis=1)

    parental_pref = df["proportion_1stprefs_v_totaloffers"]
    attainment_col = "PerformanceTablesAchievementScore"
    attainment = df[attainment_col] if attainment_col in df.columns else pd.Series(np.nan, index=df.index)
    conditions = []
    choices = []

    for gt in grading_thresholds:
        cond = (
            (total_score <= gt.max_score) &
            (total_risks <= gt.max_risks) &
            (total_major_risks <= gt.max_major_risks)
        )
        if gt.min_parental_pref is not None:
            cond = cond & (parental_pref >= gt.min_parental_pref)
        if gt.min_attainment is not None:
            cond = cond & (attainment >= gt.min_attainment)
            
        conditions.append(cond)
        choices.append(gt.grade)

    df["Total_Risk_Score"] = total_score
    df["Total_Risks"] = total_risks
    df["Total_Major_Risks"] = total_major_risks
    # The first grade which has a score meeting the threshold is chosen
    df["LAA_Risk_Grade"] = np.select(conditions, choices, default="G")

    return df


def melt_laa_risk_scores(
    df_headlines: pd.DataFrame, 
    evaluators: List[BaseRiskMetric], 
    run_id: str
) -> Tuple[pd.DataFrame, pd.DataFrame]:
    """Melts the risk scores headlines dataframe into two long tables for database storage
    using metadata properties belonging to each metric object.
    """
    df = df_headlines.copy()

    # 1. Melt Risk Indicators table dynamically from registered metrics
    melted_dfs = []
    for metric in evaluators:
        temp_df = pd.DataFrame()
        temp_df["URN"] = df["URN"]
        temp_df["RunId"] = run_id
        temp_df["RiskGroup"] = metric.risk_group.value
        temp_df["RiskIndicator"] = metric.name
        temp_df["RiskIndicatorValue"] = df[metric.value_column]
        temp_df["RiskIndicatorFlag"] = df[metric.flag_column]
        temp_df["RiskIndicatorContribution"] = df[metric.score_column]
        temp_df["RiskIndicatorContributionMax"] = metric.risk_score_maximum
        melted_dfs.append(temp_df)

    la_school_risk_indicators = pd.concat(melted_dfs, ignore_index=True)

    # 2. Group and sum headers table dynamically by inspecting categories of metric objects
    financial_cols = [metric.score_column for metric in evaluators if metric.risk_group == RiskGroup.FINANCIAL]
    financial_max = sum(metric.risk_score_maximum for metric in evaluators if metric.risk_group == RiskGroup.FINANCIAL)

    school_and_pupil_cols = [metric.score_column for metric in evaluators if metric.risk_group == RiskGroup.SCHOOL_CHARACTERISTICS]
    school_and_pupil_max = sum(metric.risk_score_maximum for metric in evaluators if metric.risk_group == RiskGroup.SCHOOL_CHARACTERISTICS)

    educational_perf_cols = [metric.score_column for metric in evaluators if metric.risk_group == RiskGroup.EDUCATIONAL_PERFORMANCE]
    educational_perf_max = sum(metric.risk_score_maximum for metric in evaluators if metric.risk_group == RiskGroup.EDUCATIONAL_PERFORMANCE)

    la_school_risk_indicators_headers = pd.DataFrame()
    la_school_risk_indicators_headers["URN"] = df["URN"]
    la_school_risk_indicators_headers["RunId"] = run_id
    la_school_risk_indicators_headers["EducationalPerformance"] = df[educational_perf_cols].sum(axis=1)
    la_school_risk_indicators_headers["EducationalPerformanceMax"] = educational_perf_max
    la_school_risk_indicators_headers["Financial"] = df[financial_cols].sum(axis=1)
    la_school_risk_indicators_headers["FinancialMax"] = financial_max
    la_school_risk_indicators_headers["SchoolAndPupil"] = df[school_and_pupil_cols].sum(axis=1)
    la_school_risk_indicators_headers["SchoolAndPupilMax"] = school_and_pupil_max
    la_school_risk_indicators_headers["Overall"] = df["Total_Risk_Score"]
    la_school_risk_indicators_headers["OverallMax"] = sum(metric.risk_score_maximum for metric in evaluators)
    la_school_risk_indicators_headers["OverallGrade"] = df["LAA_Risk_Grade"]

    return la_school_risk_indicators, la_school_risk_indicators_headers
