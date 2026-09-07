from typing import List, Optional, Tuple

import numpy as np
import pandas as pd

from .config import (
    get_laa_column_eval,
    get_yearly_grading_thresholds,
    get_yearly_risk_config,
)
from .metrics import BaseRiskMetric, GradeThreshold, RiskFlag, RiskGroup

GRADING_EPSILON = 1e-9


def derive_laa_risk_scores(
    cfr_with_all_extra_data: pd.DataFrame, run_year: int, run_id: str
) -> Tuple[pd.DataFrame, pd.DataFrame, pd.DataFrame]:
    """Coordinating dynamic metric execution, scoring, grading, and db melting."""
    df = cfr_with_all_extra_data.copy()

    column_eval = get_laa_column_eval(run_year)
    for col, expr in column_eval.items():
        df[col] = df.eval(expr)

    evaluators = get_yearly_risk_config(run_year)
    for metric in evaluators:
        metric.execute(df)

    grading_thresholds = get_yearly_grading_thresholds(run_year)
    df_graded = roll_up_laa_risk_scores_to_headlines(df, evaluators, grading_thresholds)
    indicators, headers = melt_laa_risk_scores(df_graded, evaluators, run_id=run_id)

    return df_graded, indicators, headers


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
    total_risks = (
        df[risk_cols].isin([RiskFlag.MINOR.value, RiskFlag.MAJOR.value]).sum(axis=1)
    )
    total_major_risks = df[risk_cols].isin([RiskFlag.MAJOR.value]).sum(axis=1)

    parental_pref = df["proportion_1stprefs_v_totaloffers"]
    attainment_col = "PerformanceTablesAchievementScore"
    attainment = (
        df[attainment_col]
        if attainment_col in df.columns
        else pd.Series(np.nan, index=df.index)
    )
    conditions = []
    choices = []

    for gt in grading_thresholds:
        cond = (
            (total_score - GRADING_EPSILON <= gt.max_score)
            & (total_risks <= gt.max_risks)
            & (total_major_risks <= gt.max_major_risks)
        )
        if gt.min_parental_pref is not None:
            cond = cond & (parental_pref + GRADING_EPSILON >= gt.min_parental_pref)
        if gt.min_attainment is not None:
            cond = cond & (attainment + GRADING_EPSILON >= gt.min_attainment)

        conditions.append(cond)
        choices.append(gt.grade)

    df["Total_Risk_Score"] = total_score
    df["Total_Risks"] = total_risks
    df["Total_Major_Risks"] = total_major_risks
    # The first grade which has a score meeting the threshold is chosen
    df["LAA_Risk_Grade"] = np.select(conditions, choices, default="G")

    return df


def melt_laa_risk_scores(
    df_headlines: pd.DataFrame, evaluators: List[BaseRiskMetric], run_id: str
) -> Tuple[pd.DataFrame, pd.DataFrame]:
    """Melts the risk scores headlines dataframe into two long tables for database storage
    using metadata properties belonging to each metric object.
    """
    df = df_headlines.copy()

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

    financial_cols = [
        metric.score_column
        for metric in evaluators
        if metric.risk_group == RiskGroup.FINANCIAL
    ]
    financial_max = sum(
        metric.risk_score_maximum
        for metric in evaluators
        if metric.risk_group == RiskGroup.FINANCIAL
    )
    school_and_pupil_cols = [
        metric.score_column
        for metric in evaluators
        if metric.risk_group == RiskGroup.SCHOOL_CHARACTERISTICS
    ]
    school_and_pupil_max = sum(
        metric.risk_score_maximum
        for metric in evaluators
        if metric.risk_group == RiskGroup.SCHOOL_CHARACTERISTICS
    )

    educational_perf_cols = [
        metric.score_column
        for metric in evaluators
        if metric.risk_group == RiskGroup.EDUCATIONAL_PERFORMANCE
    ]
    educational_perf_max = sum(
        metric.risk_score_maximum
        for metric in evaluators
        if metric.risk_group == RiskGroup.EDUCATIONAL_PERFORMANCE
    )

    la_school_risk_indicators_headers = pd.DataFrame()
    la_school_risk_indicators_headers["URN"] = df["URN"]
    la_school_risk_indicators_headers["RunId"] = run_id
    la_school_risk_indicators_headers["EducationalPerformance"] = df[
        educational_perf_cols
    ].sum(axis=1)
    la_school_risk_indicators_headers["EducationalPerformanceMax"] = (
        educational_perf_max
    )
    la_school_risk_indicators_headers["Financial"] = df[financial_cols].sum(axis=1)
    la_school_risk_indicators_headers["FinancialMax"] = financial_max
    la_school_risk_indicators_headers["SchoolAndPupil"] = df[school_and_pupil_cols].sum(
        axis=1
    )
    la_school_risk_indicators_headers["SchoolAndPupilMax"] = school_and_pupil_max
    la_school_risk_indicators_headers["Overall"] = df["Total_Risk_Score"]
    la_school_risk_indicators_headers["OverallMax"] = sum(
        metric.risk_score_maximum for metric in evaluators
    )
    la_school_risk_indicators_headers["OverallGrade"] = df["LAA_Risk_Grade"]

    return la_school_risk_indicators, la_school_risk_indicators_headers
