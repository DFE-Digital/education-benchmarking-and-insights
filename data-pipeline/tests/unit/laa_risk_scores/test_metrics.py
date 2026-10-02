import numpy as np
import pandas as pd
import pytest

from pipeline.laa_risk_scores.config import DEFAULT_RISK_CONFIG
from pipeline.laa_risk_scores.engine import melt_laa_risk_scores
from pipeline.laa_risk_scores.metrics import (
    BaseRiskMetric,
    BinaryRiskMetric,
    EndYearBalanceMetric,
    InterestOnLoanFlagMetric,
    ParentalPreferenceMetric,
    PercentExpenditureOnPremisesMetric,
    PercentExpenditureOnStaffMetric,
    PerformanceTablesAchievementScoreMetric,
    PerformanceTablesProgressScoreMetric,
    PupilAbsenceMetric,
    PupilChangeOver1YearMetric,
    PupilChangeOver4YearsMetric,
    PupilsSixthFormMetric,
    RiskFlag,
    RiskGroup,
    RiskIndicatorValueFormatting,
)


def test_pupil_change_metrics_inf_handling():
    # Arrange: previous year/4-year pupil number is 0, which would cause division by zero
    df = pd.DataFrame(
        {
            "URN": [1001, 1002, 1003],
            "Number of pupils": [50, 50, 0],
            "Number of pupils_y_minus_one": [0, 10, 0],
            "Number of pupils_y_minus_four": [0, 10, 0],
        }
    )

    # Find the metrics from DEFAULT_RISK_CONFIG
    metric_4yr = next(
        m for m in DEFAULT_RISK_CONFIG if isinstance(m, PupilChangeOver4YearsMetric)
    )
    metric_1yr = next(
        m for m in DEFAULT_RISK_CONFIG if isinstance(m, PupilChangeOver1YearMetric)
    )

    # Act
    metric_4yr.execute(df)
    metric_1yr.execute(df)

    # Assert
    # The derived values should not contain inf or -inf
    assert not np.isinf(df[metric_4yr.value_column]).any()
    assert not np.isinf(df[metric_1yr.value_column]).any()

    # The division-by-zero case should resolve to NaN and result in max score / Major risk
    assert pd.isna(df.loc[0, metric_4yr.value_column])
    assert df.loc[0, metric_4yr.flag_column] == "Major"
    assert df.loc[0, metric_4yr.score_column] == 1.5

    assert pd.isna(df.loc[0, metric_1yr.value_column])
    assert df.loc[0, metric_1yr.flag_column] == "Major"
    assert df.loc[0, metric_1yr.score_column] == 1.5


def test_parental_preference_metric_scoring():
    # Find ParentalPreference from DEFAULT_RISK_CONFIG
    metric = next(
        m for m in DEFAULT_RISK_CONFIG if isinstance(m, ParentalPreferenceMetric)
    )

    # Arrange:
    # 1. School with standard type and value 0.5 (should be RiskScore 1.5, Major)
    # 2. School with standard type and value 0.72 (should be RiskScore 0.8, Minor)
    # 3. School with standard type and NaN value (should be RiskScore 1.5, Major)
    # 4. School with TypeOfEstablishment = 7 and value 0.5 (should be RiskScore 0.0, No flag)
    # 5. School with TypeOfEstablishment = 12 and NaN value (should be RiskScore 0.0, No flag)
    # 6. School with TypeOfEstablishment = 10 (not 7 or 12) and value 0.5 (should be RiskScore 1.5, Major)
    df = pd.DataFrame(
        {
            "URN": [1001, 1002, 1003, 1004, 1005, 1006],
            "TypeOfEstablishment (code)": [1, 1, 1, 7, 12, 10],
            "proportion_1stprefs_v_totaloffers": [0.5, 0.72, np.nan, 0.5, np.nan, 0.5],
        }
    )

    # Act
    metric.execute(df)

    # Assert
    # 1. School with standard type and value 0.5 -> RiskScore 1.5, Major
    assert df.loc[0, metric.score_column] == 1.5
    assert df.loc[0, metric.flag_column] == "Major"

    # 2. School with standard type and value 0.72 -> RiskScore 0.8, Minor
    assert df.loc[1, metric.score_column] == 0.8
    assert df.loc[1, metric.flag_column] == "Minor"

    # 3. School with standard type and NaN value -> RiskScore 1.5, Major
    assert df.loc[2, metric.score_column] == 1.5
    assert df.loc[2, metric.flag_column] == "Major"

    # 4. School with TypeOfEstablishment = 7 and value 0.5 -> RiskScore 0.0, No flag
    assert df.loc[3, metric.score_column] == 0.0
    assert df.loc[3, metric.flag_column] == RiskFlag.NO_FLAG.value

    # 5. School with TypeOfEstablishment = 12 and NaN value -> RiskScore 0.0, No flag
    assert df.loc[4, metric.score_column] == 0.0
    assert df.loc[4, metric.flag_column] == RiskFlag.NO_FLAG.value

    # 6. School with TypeOfEstablishment = 10 (not 7 or 12) and value 0.5 -> RiskScore 1.5, Major
    assert df.loc[5, metric.score_column] == 1.5
    assert df.loc[5, metric.flag_column] == "Major"


def test_metric_na_default_risk_flags():
    # Arrange: Create a DataFrame with NaN values for the metrics we want to test
    df = pd.DataFrame(
        {
            "URN": [1001],
            "Revenue reserve": [np.nan],
            "Total Income": [np.nan],
            "Revenue reserve_y_minus_one": [np.nan],
            "Total Income_y_minus_one": [np.nan],
            "Other costs_Interest charges for loan and bank": [np.nan],
            "LAAPremisesExpenditureRollup": [np.nan],
            "NetExpenditure": [np.nan],
            "LAAStaffExpenditureRollup": [np.nan],
            "TotalPupilsSixthForm": [np.nan],
            "sess_overall_percent": [np.nan],
            "TypeOfEstablishment (code)": [1],
            "Overall Phase": ["Primary"],
            "Ks2Progress": [np.nan],
            "Progress8Measure": [np.nan],
            "PTRWM_EXP": [np.nan],
            "AverageAttainment": [np.nan],
        }
    )

    # Find the metrics we want to test from DEFAULT_RISK_CONFIG
    interest_on_loan = next(
        m for m in DEFAULT_RISK_CONFIG if isinstance(m, InterestOnLoanFlagMetric)
    )
    premises_exp = next(
        m
        for m in DEFAULT_RISK_CONFIG
        if isinstance(m, PercentExpenditureOnPremisesMetric)
    )
    staff_exp = next(
        m for m in DEFAULT_RISK_CONFIG if isinstance(m, PercentExpenditureOnStaffMetric)
    )
    sixth_form = next(
        m for m in DEFAULT_RISK_CONFIG if isinstance(m, PupilsSixthFormMetric)
    )
    absence = next(m for m in DEFAULT_RISK_CONFIG if isinstance(m, PupilAbsenceMetric))
    progress_score = next(
        m
        for m in DEFAULT_RISK_CONFIG
        if isinstance(m, PerformanceTablesProgressScoreMetric)
    )
    achievement_score = next(
        m
        for m in DEFAULT_RISK_CONFIG
        if isinstance(m, PerformanceTablesAchievementScoreMetric)
    )

    # Also test one that should still default to Major
    end_year_balance = next(
        m for m in DEFAULT_RISK_CONFIG if isinstance(m, EndYearBalanceMetric)
    )

    # Act: execute each metric on our DataFrame
    for metric in [
        interest_on_loan,
        premises_exp,
        staff_exp,
        sixth_form,
        absence,
        progress_score,
        achievement_score,
        end_year_balance,
    ]:
        metric.execute(df)

    # Assert: verify they default to the correct metric-specific maximum score and flag
    assert df.loc[0, interest_on_loan.flag_column] == RiskFlag.NO_FLAG.value
    assert df.loc[0, interest_on_loan.score_column] == 0.0

    assert df.loc[0, premises_exp.flag_column] == "Minor"
    assert df.loc[0, premises_exp.score_column] == 0.5

    assert df.loc[0, staff_exp.flag_column] == "Minor"
    assert df.loc[0, staff_exp.score_column] == 1.5

    assert df.loc[0, sixth_form.flag_column] == "Minor"
    assert df.loc[0, sixth_form.score_column] == 0.5

    assert df.loc[0, absence.flag_column] == "Minor"
    assert df.loc[0, absence.score_column] == 0.5

    assert df.loc[0, progress_score.flag_column] == "Minor"
    assert df.loc[0, progress_score.score_column] == 0.25

    assert df.loc[0, achievement_score.flag_column] == "Minor"
    assert df.loc[0, achievement_score.score_column] == 0.25

    # This one has no risk_flag_maximum override, so it should default to Major
    assert df.loc[0, end_year_balance.flag_column] == "Major"


def test_format_value_boolean():
    metric = BinaryRiskMetric(
        name="TestBool",
        risk_group=RiskGroup.FINANCIAL,
        risk_score_maximum=1.0,
        score_when_1=1.0,
        risk_when_1="Major",
        value_formatting=RiskIndicatorValueFormatting.BOOLEAN,
    )
    series = pd.Series([True, False, 1, 0, "True", "False", np.nan, None])
    formatted = metric.format_value(series)
    assert formatted.tolist() == [
        "Yes",
        "No",
        "Yes",
        "No",
        "Yes",
        "No",
        None,
        None,
    ]


def test_format_value_decimal():
    metric = BaseRiskMetric(
        name="TestDecimal",
        risk_group=RiskGroup.FINANCIAL,
        risk_score_maximum=1.0,
        value_formatting=RiskIndicatorValueFormatting.DECIMAL,
    )
    series = pd.Series([120.0, 4.5, 0.0, -1.25, np.nan, None, np.inf, -np.inf])
    formatted = metric.format_value(series)
    assert formatted.tolist() == ["120", "4.5", "0", "-1.25", None, None, None, None]


def test_format_value_percentage():
    metric = BaseRiskMetric(
        name="TestPercentage",
        risk_group=RiskGroup.FINANCIAL,
        risk_score_maximum=1.0,
        value_formatting=RiskIndicatorValueFormatting.PERCENTAGE,
    )
    series = pd.Series([0.15, -0.024, 0.0, np.nan, None, np.inf])
    formatted = metric.format_value(series)
    assert formatted.tolist() == ["0.15", "-0.024", "0.0", None, None, None]


def test_format_value_string():
    metric = BaseRiskMetric(
        name="TestString",
        risk_group=RiskGroup.SCHOOL_CHARACTERISTICS,
        risk_score_maximum=1.0,
        value_formatting=RiskIndicatorValueFormatting.STRING,
    )
    series = pd.Series(["High", "Low", np.nan, None])
    formatted = metric.format_value(series)
    assert formatted.tolist() == ["High", "Low", None, None]


def test_all_config_metrics_have_valid_formatting():
    valid_formats = {"Percentage", "Boolean", "Decimal", "String"}
    for metric in DEFAULT_RISK_CONFIG:
        assert metric.value_formatting in RiskIndicatorValueFormatting
        assert metric.value_formatting.value in valid_formats


def test_melt_laa_risk_scores_includes_formatting_and_string_values():
    metric_bool = BinaryRiskMetric(
        name="LoanFlag",
        risk_group=RiskGroup.FINANCIAL,
        risk_score_maximum=0.25,
        score_when_1=0.25,
        risk_when_1="Minor",
        value_formatting=RiskIndicatorValueFormatting.BOOLEAN,
    )
    metric_pct = BaseRiskMetric(
        name="SpendPct",
        risk_group=RiskGroup.FINANCIAL,
        risk_score_maximum=1.0,
        value_formatting=RiskIndicatorValueFormatting.PERCENTAGE,
    )
    metric_dec = BaseRiskMetric(
        name="PupilCount",
        risk_group=RiskGroup.SCHOOL_CHARACTERISTICS,
        risk_score_maximum=1.0,
        value_formatting=RiskIndicatorValueFormatting.DECIMAL,
    )

    df_headlines = pd.DataFrame(
        {
            "URN": [1001, 1002],
            "LoanFlag": [True, False],
            "LoanFlag_Score": [0.25, 0.0],
            "LoanFlag_Risk": ["Minor", "No flag"],
            "SpendPct": [0.15, np.nan],
            "SpendPct_Score": [0.5, 1.0],
            "SpendPct_Risk": ["Minor", "Major"],
            "PupilCount": [120.0, 45.5],
            "PupilCount_Score": [0.0, 0.5],
            "PupilCount_Risk": ["No flag", "Minor"],
            "Total_Risk_Score": [0.75, 1.5],
            "LAA_Risk_Grade": ["A", "B"],
        }
    )

    evaluators = [metric_bool, metric_pct, metric_dec]
    indicators_df, _ = melt_laa_risk_scores(df_headlines, evaluators, run_id="2026")

    # Verify column existence
    assert "RiskIndicatorValue" in indicators_df.columns
    assert "RiskIndicatorValueFormatting" in indicators_df.columns

    # Verify boolean metric rows
    bool_rows = indicators_df[indicators_df["RiskIndicator"] == "LoanFlag"].reset_index(
        drop=True
    )
    assert bool_rows.loc[0, "RiskIndicatorValue"] == "Yes"
    assert bool_rows.loc[0, "RiskIndicatorValueFormatting"] == "Boolean"
    assert bool_rows.loc[1, "RiskIndicatorValue"] == "No"
    assert bool_rows.loc[1, "RiskIndicatorValueFormatting"] == "Boolean"

    # Verify percentage metric rows
    pct_rows = indicators_df[indicators_df["RiskIndicator"] == "SpendPct"].reset_index(
        drop=True
    )
    assert pct_rows.loc[0, "RiskIndicatorValue"] == "0.15"
    assert pct_rows.loc[0, "RiskIndicatorValueFormatting"] == "Percentage"
    assert pct_rows.loc[1, "RiskIndicatorValue"] is None
    assert pct_rows.loc[1, "RiskIndicatorValueFormatting"] == "Percentage"

    # Verify decimal metric rows
    dec_rows = indicators_df[
        indicators_df["RiskIndicator"] == "PupilCount"
    ].reset_index(drop=True)
    assert dec_rows.loc[0, "RiskIndicatorValue"] == "120"
    assert dec_rows.loc[0, "RiskIndicatorValueFormatting"] == "Decimal"
    assert dec_rows.loc[1, "RiskIndicatorValue"] == "45.5"
    assert dec_rows.loc[1, "RiskIndicatorValueFormatting"] == "Decimal"
