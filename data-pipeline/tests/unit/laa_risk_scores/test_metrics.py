import numpy as np
import pandas as pd
import pytest

from pipeline.laa_risk_scores.config import (
    DEFAULT_RISK_CONFIG,
    get_laa_download_file_schema,
)
from pipeline.laa_risk_scores.engine import melt_laa_risk_scores
from pipeline.laa_risk_scores.metrics import (
    ApplicabilityRule,
    BaseRiskMetric,
    BinaryRiskMetric,
    CurrentYearRevenueReserveMetric,
    EndYearBalanceMetric,
    ExcludePhases,
    InterestOnLoanFlagMetric,
    MetricRule,
    ParentalPreferenceMetric,
    PercentExpenditureOnPremisesMetric,
    PercentExpenditureOnStaffMetric,
    PerformanceTablesAchievementScoreMetric,
    PerformanceTablesProgressScoreMetric,
    PreviousYearRevenueReserveMetric,
    PupilAbsenceMetric,
    PupilChangeOver1YearMetric,
    PupilChangeOver4YearsMetric,
    PupilsSixthFormMetric,
    RangeRiskMetric,
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
            "Overall Phase": [
                "Primary",
                "Primary",
                "Primary",
                "Special",
                "Special",
                "Primary",
            ],
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
            "Overall Phase": ["Secondary"],
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

    current_year_balance = next(
        m for m in DEFAULT_RISK_CONFIG if isinstance(m, CurrentYearRevenueReserveMetric)
    )
    previous_year_balance = next(
        m
        for m in DEFAULT_RISK_CONFIG
        if isinstance(m, PreviousYearRevenueReserveMetric)
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
        current_year_balance,
        previous_year_balance,
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

    # Balance metrics default to Major with 3.0 and 1.5
    assert df.loc[0, current_year_balance.flag_column] == "Major"
    assert df.loc[0, current_year_balance.score_column] == 3.0
    assert df.loc[0, previous_year_balance.flag_column] == "Major"
    assert df.loc[0, previous_year_balance.score_column] == 1.5


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


def test_format_value_parental_preference():
    metric = next(
        m for m in DEFAULT_RISK_CONFIG if isinstance(m, ParentalPreferenceMetric)
    )
    assert metric.value_formatting == RiskIndicatorValueFormatting.STRING
    assert metric.rating_low_threshold == 0.675
    assert metric.rating_high_threshold == 0.9

    series = pd.Series([np.nan, None, 0.5, 0.675, 0.72, 0.9, 0.95, 1.2])
    formatted = metric.format_value(series)
    assert formatted.tolist() == [
        "N/A",
        "N/A",
        "Low",
        "Low",
        "Medium",
        "Medium",
        "High",
        "High",
    ]

    # Test custom threshold override
    custom_metric = ParentalPreferenceMetric(
        name="CustomParentalPreference",
        risk_group=RiskGroup.EDUCATIONAL_PERFORMANCE,
        risk_score_maximum=1.5,
        rating_low_threshold=0.5,
        rating_high_threshold=0.8,
    )
    custom_series = pd.Series([np.nan, 0.5, 0.6, 0.8, 0.81])
    custom_formatted = custom_metric.format_value(custom_series)
    assert custom_formatted.tolist() == ["N/A", "Low", "Medium", "Medium", "High"]


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
    metric_with_applicability = BaseRiskMetric(
        name="SixthFormCount",
        risk_group=RiskGroup.SCHOOL_CHARACTERISTICS,
        risk_score_maximum=0.5,
        value_formatting=RiskIndicatorValueFormatting.DECIMAL,
        applicability=ExcludePhases(["Primary"]),
    )

    df_headlines = pd.DataFrame(
        {
            "URN": [1001, 1002],
            "Overall Phase": ["Secondary", "Primary"],
            "LoanFlag": [True, False],
            "LoanFlag_Score": [0.25, 0.0],
            "LoanFlag_Risk": ["Minor", "No flag"],
            "SpendPct": [0.15, np.nan],
            "SpendPct_Score": [0.5, 1.0],
            "SpendPct_Risk": ["Minor", "Major"],
            "PupilCount": [120.0, 45.5],
            "PupilCount_Score": [0.0, 0.5],
            "PupilCount_Risk": ["No flag", "Minor"],
            "SixthFormCount": [50.0, 50.0],
            "SixthFormCount_Score": [0.0, 0.0],
            "SixthFormCount_Risk": ["No flag", "No flag"],
            "Total_Risk_Score": [0.75, 1.5],
            "LAA_Risk_Grade": ["A", "B"],
        }
    )

    evaluators = [metric_bool, metric_pct, metric_dec, metric_with_applicability]
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

    # Verify applicability metric rows with N/A masking to String format
    app_rows = indicators_df[
        indicators_df["RiskIndicator"] == "SixthFormCount"
    ].reset_index(drop=True)
    assert app_rows.loc[0, "RiskIndicatorValue"] == "50"
    assert app_rows.loc[0, "RiskIndicatorValueFormatting"] == "Decimal"
    assert app_rows.loc[1, "RiskIndicatorValue"] == "N/A"
    assert app_rows.loc[1, "RiskIndicatorValueFormatting"] == "String"


def test_revenue_reserve_metrics_happy_path_scenarios():
    """Verifies TC-01 to TC-05: surplus, deficit combinations, and intermediate bands."""
    metric_curr = next(
        m for m in DEFAULT_RISK_CONFIG if isinstance(m, CurrentYearRevenueReserveMetric)
    )
    metric_prev = next(
        m
        for m in DEFAULT_RISK_CONFIG
        if isinstance(m, PreviousYearRevenueReserveMetric)
    )

    df = pd.DataFrame(
        {
            "URN": [1001, 1002, 1003, 1004, 1005],
            "Revenue reserve": [100000.0, -100000.0, 50000.0, -120000.0, -45000.0],
            "Total Income": [1000000.0, 1000000.0, 1000000.0, 1000000.0, 1000000.0],
            "Revenue reserve_y_minus_one": [
                50000.0,
                50000.0,
                -100000.0,
                -150000.0,
                -70000.0,
            ],
            "Total Income_y_minus_one": [
                1000000.0,
                1000000.0,
                1000000.0,
                1000000.0,
                1000000.0,
            ],
        }
    )

    metric_curr.execute(df)
    metric_prev.execute(df)

    combined_scores = df[metric_curr.score_column] + df[metric_prev.score_column]

    # TC-01: Both surplus (+10%, +5%)
    assert df.loc[0, metric_curr.value_column] == pytest.approx(0.10)
    assert df.loc[0, metric_curr.score_column] == 0.0
    assert df.loc[0, metric_curr.flag_column] == RiskFlag.NO_FLAG.value
    assert df.loc[0, metric_prev.value_column] == pytest.approx(0.05)
    assert df.loc[0, metric_prev.score_column] == 0.0
    assert df.loc[0, metric_prev.flag_column] == RiskFlag.NO_FLAG.value
    assert combined_scores.iloc[0] == 0.0

    # TC-02: Severe deficit current (-10%), surplus prev (+5%)
    assert df.loc[1, metric_curr.value_column] == pytest.approx(-0.10)
    assert df.loc[1, metric_curr.score_column] == 3.0
    assert df.loc[1, metric_curr.flag_column] == RiskFlag.MAJOR.value
    assert df.loc[1, metric_prev.value_column] == pytest.approx(0.05)
    assert df.loc[1, metric_prev.score_column] == 0.0
    assert df.loc[1, metric_prev.flag_column] == RiskFlag.NO_FLAG.value
    assert combined_scores.iloc[1] == 3.0

    # TC-03: Surplus current (+5%), severe deficit prev (-10%)
    assert df.loc[2, metric_curr.value_column] == pytest.approx(0.05)
    assert df.loc[2, metric_curr.score_column] == 0.0
    assert df.loc[2, metric_curr.flag_column] == RiskFlag.NO_FLAG.value
    assert df.loc[2, metric_prev.value_column] == pytest.approx(-0.10)
    assert df.loc[2, metric_prev.score_column] == 1.5
    assert df.loc[2, metric_prev.flag_column] == RiskFlag.MAJOR.value
    assert combined_scores.iloc[2] == 1.5

    # TC-04: Both severe deficits (-12%, -15%)
    assert df.loc[3, metric_curr.score_column] == 3.0
    assert df.loc[3, metric_curr.flag_column] == RiskFlag.MAJOR.value
    assert df.loc[3, metric_prev.score_column] == 1.5
    assert df.loc[3, metric_prev.flag_column] == RiskFlag.MAJOR.value
    assert combined_scores.iloc[3] == 4.5

    # TC-05: Intermediate bands (-4.5%, -7.0%)
    assert df.loc[4, metric_curr.score_column] == 1.25
    assert df.loc[4, metric_curr.flag_column] == RiskFlag.MINOR.value
    assert df.loc[4, metric_prev.score_column] == 1.0
    assert df.loc[4, metric_prev.flag_column] == RiskFlag.MAJOR.value
    assert combined_scores.iloc[4] == 2.25


def test_split_end_year_balance_mathematical_parity_across_all_bands():
    """Verifies TC-06: mathematical score parity across all 9 rule threshold bands
    comparing the sum of split metrics against the legacy formula.
    """
    metric_curr = next(
        m for m in DEFAULT_RISK_CONFIG if isinstance(m, CurrentYearRevenueReserveMetric)
    )
    metric_prev = next(
        m
        for m in DEFAULT_RISK_CONFIG
        if isinstance(m, PreviousYearRevenueReserveMetric)
    )

    test_ratios = [0.05, -0.005, -0.02, -0.03, -0.045, -0.055, -0.07, -0.08, -0.12]
    expected_curr_scores = [0.0, 0.25, 0.5, 1.0, 1.25, 1.75, 2.0, 2.5, 3.0]
    expected_prev_scores = [0.0, 0.125, 0.25, 0.5, 0.625, 0.875, 1.0, 1.25, 1.5]

    for curr_ratio, exp_curr in zip(test_ratios, expected_curr_scores):
        for prev_ratio, exp_prev in zip(test_ratios, expected_prev_scores):
            df = pd.DataFrame(
                {
                    "Revenue reserve": [curr_ratio * 100000.0],
                    "Total Income": [100000.0],
                    "Revenue reserve_y_minus_one": [prev_ratio * 100000.0],
                    "Total Income_y_minus_one": [100000.0],
                }
            )
            metric_curr.execute(df)
            metric_prev.execute(df)

            curr_score = df.loc[0, metric_curr.score_column]
            prev_score = df.loc[0, metric_prev.score_column]

            assert curr_score == exp_curr
            assert prev_score == exp_prev
            legacy_prev_raw = expected_curr_scores[test_ratios.index(prev_ratio)]
            legacy_blended = min(exp_curr + legacy_prev_raw / 2.0, 4.5)
            assert (curr_score + prev_score) == pytest.approx(legacy_blended)


def test_revenue_reserve_metrics_missing_data_fallbacks():
    """Verifies TC-07, TC-08, TC-09: missing data and NaN fallbacks to maximum penalties."""
    metric_curr = next(
        m for m in DEFAULT_RISK_CONFIG if isinstance(m, CurrentYearRevenueReserveMetric)
    )
    metric_prev = next(
        m
        for m in DEFAULT_RISK_CONFIG
        if isinstance(m, PreviousYearRevenueReserveMetric)
    )

    df = pd.DataFrame(
        {
            "Revenue reserve": [np.nan, 50000.0, np.nan],
            "Total Income": [np.nan, 1000000.0, np.nan],
            "Revenue reserve_y_minus_one": [np.nan, np.nan, 50000.0],
            "Total Income_y_minus_one": [np.nan, np.nan, 1000000.0],
        }
    )

    metric_curr.execute(df)
    metric_prev.execute(df)

    # TC-07: Both NaN -> Current 3.0 Major, Prev 1.5 Major, Total 4.5
    assert df.loc[0, metric_curr.score_column] == 3.0
    assert df.loc[0, metric_curr.flag_column] == RiskFlag.MAJOR.value
    assert df.loc[0, metric_prev.score_column] == 1.5
    assert df.loc[0, metric_prev.flag_column] == RiskFlag.MAJOR.value
    assert (
        df.loc[0, metric_curr.score_column] + df.loc[0, metric_prev.score_column]
    ) == 4.5

    # TC-08: Current 0.0 No flag, Prev 1.5 Major, Total 1.5
    assert df.loc[1, metric_curr.score_column] == 0.0
    assert df.loc[1, metric_curr.flag_column] == RiskFlag.NO_FLAG.value
    assert df.loc[1, metric_prev.score_column] == 1.5
    assert df.loc[1, metric_prev.flag_column] == RiskFlag.MAJOR.value
    assert (
        df.loc[1, metric_curr.score_column] + df.loc[1, metric_prev.score_column]
    ) == 1.5

    # TC-09: Current 3.0 Major, Prev 0.0 No flag, Total 3.0
    assert df.loc[2, metric_curr.score_column] == 3.0
    assert df.loc[2, metric_curr.flag_column] == RiskFlag.MAJOR.value
    assert df.loc[2, metric_prev.score_column] == 0.0
    assert df.loc[2, metric_prev.flag_column] == RiskFlag.NO_FLAG.value
    assert (
        df.loc[2, metric_curr.score_column] + df.loc[2, metric_prev.score_column]
    ) == 3.0


def test_revenue_reserve_metrics_zero_income_division():
    """Verifies TC-10: zero total income handles division by zero safely without errors."""
    metric_curr = next(
        m for m in DEFAULT_RISK_CONFIG if isinstance(m, CurrentYearRevenueReserveMetric)
    )
    metric_prev = next(
        m
        for m in DEFAULT_RISK_CONFIG
        if isinstance(m, PreviousYearRevenueReserveMetric)
    )

    df = pd.DataFrame(
        {
            "Revenue reserve": [-10000.0],
            "Total Income": [0.0],
            "Revenue reserve_y_minus_one": [5000.0],
            "Total Income_y_minus_one": [0.0],
        }
    )

    metric_curr.execute(df)
    metric_prev.execute(df)

    assert pd.isna(df.loc[0, metric_curr.value_column])
    assert df.loc[0, metric_curr.score_column] == 3.0
    assert df.loc[0, metric_curr.flag_column] == RiskFlag.MAJOR.value

    assert pd.isna(df.loc[0, metric_prev.value_column])
    assert df.loc[0, metric_prev.score_column] == 1.5
    assert df.loc[0, metric_prev.flag_column] == RiskFlag.MAJOR.value


def test_revenue_reserve_metrics_boundary_inclusivity():
    """Verifies TC-11: rule inclusivity behavior at exact boundary points."""
    metric_curr = next(
        m for m in DEFAULT_RISK_CONFIG if isinstance(m, CurrentYearRevenueReserveMetric)
    )
    metric_prev = next(
        m
        for m in DEFAULT_RISK_CONFIG
        if isinstance(m, PreviousYearRevenueReserveMetric)
    )

    boundary_values = [0.0, -0.01, -0.025, -0.04, -0.05, -0.06, -0.075, -0.09]
    expected_curr_scores = [0.0, 0.5, 1.0, 1.25, 1.75, 2.0, 2.5, 3.0]
    expected_curr_flags = [
        RiskFlag.NO_FLAG.value,
        RiskFlag.MINOR.value,
        RiskFlag.MINOR.value,
        RiskFlag.MINOR.value,
        RiskFlag.MAJOR.value,
        RiskFlag.MAJOR.value,
        RiskFlag.MAJOR.value,
        RiskFlag.MAJOR.value,
    ]
    expected_prev_scores = [0.0, 0.25, 0.5, 0.625, 0.875, 1.0, 1.25, 1.5]

    df = pd.DataFrame(
        {
            "Revenue reserve": [v * 100000.0 for v in boundary_values],
            "Total Income": [100000.0] * len(boundary_values),
            "Revenue reserve_y_minus_one": [v * 100000.0 for v in boundary_values],
            "Total Income_y_minus_one": [100000.0] * len(boundary_values),
        }
    )

    metric_curr.execute(df)
    metric_prev.execute(df)

    for i in range(len(boundary_values)):
        assert df.loc[i, metric_curr.score_column] == expected_curr_scores[i]
        assert df.loc[i, metric_curr.flag_column] == expected_curr_flags[i]
        assert df.loc[i, metric_prev.score_column] == expected_prev_scores[i]
        assert df.loc[i, metric_prev.flag_column] == expected_curr_flags[i]


def test_revenue_reserve_metrics_download_file_schema():
    """Verifies TC-12: download file schema column generation for both metrics."""
    schema = get_laa_download_file_schema(2026)

    # Both metrics must have their 3 standard columns in the download schema
    assert "Balance 24-25" in schema
    assert "Balance 24-25_Score" in schema
    assert "Balance 24-25_Risk" in schema

    assert "Balance 23-24" in schema
    assert "Balance 23-24_Score" in schema
    assert "Balance 23-24_Risk" in schema

    # Legacy side columns must not appear in the schema
    assert "EndYearBalanceAsPercentageIncome_y_minus_one" not in schema
    assert "EndYearBalanceAsPercentageIncome_y_minus_one_Score" not in schema
    assert "EndYearBalanceAsPercentageIncome_y_minus_one_Risk" not in schema


def test_exclude_phases_rule():
    rule = ExcludePhases(["Nursery", "Special"])
    df = pd.DataFrame({"Overall Phase": ["Primary", "Nursery", "Secondary", "Special"]})
    applicable = rule.is_applicable(df)
    assert applicable.tolist() == [True, False, True, False]

    # When column is missing, all should be applicable
    df_missing_col = pd.DataFrame({"URN": [1001, 1002]})
    assert rule.is_applicable(df_missing_col).all()


def test_metric_applicability_execution_and_scoring():
    metric = RangeRiskMetric(
        name="TestCapacity",
        risk_group=RiskGroup.SCHOOL_CHARACTERISTICS,
        risk_score_maximum=1.5,
        applicability=ExcludePhases(["Special"]),
        rules=[
            MetricRule(0.0, 0.5, 1.5, RiskFlag.MAJOR.value, "left"),
            MetricRule(0.5, 1.0, 0.0, RiskFlag.NO_FLAG.value, "both"),
        ],
    )

    df = pd.DataFrame(
        {
            "URN": [1001, 1002, 1003, 1004],
            "Overall Phase": ["Primary", "Special", "Nursery", "Special"],
            "TestCapacity": [0.2, 0.2, np.nan, np.nan],
        }
    )

    metric.execute(df)

    # 1. Applicable school with value 0.2 -> evaluated normally (1.5, Major)
    assert df.loc[0, metric.score_column] == 1.5
    assert df.loc[0, metric.flag_column] == RiskFlag.MAJOR.value

    # 2. Excluded school (code 7) with value 0.2 -> exempted (0.0, No flag)
    assert df.loc[1, metric.score_column] == 0.0
    assert df.loc[1, metric.flag_column] == RiskFlag.NO_FLAG.value

    # 3. Applicable school with NaN value -> penalised as missing (1.5, Major)
    assert df.loc[2, metric.score_column] == 1.5
    assert df.loc[2, metric.flag_column] == RiskFlag.MAJOR.value

    # 4. Excluded school (code 12) with NaN value -> exempted (0.0, No flag)
    assert df.loc[3, metric.score_column] == 0.0
    assert df.loc[3, metric.flag_column] == RiskFlag.NO_FLAG.value


def test_metric_applicability_combined_rules():
    metric = RangeRiskMetric(
        name="TestProgress",
        risk_group=RiskGroup.EDUCATIONAL_PERFORMANCE,
        risk_score_maximum=1.0,
        applicability=[
            ExcludePhases(["Nursery"])
        ],
        rules=[
            MetricRule(0.0, 10.0, 1.0, RiskFlag.MAJOR.value, "both"),
        ],
    )

    df = pd.DataFrame(
        {
            "URN": [1001, 1002, 1003, 1004],
            "Overall Phase": ["Primary", "Nursery", "Primary", "Nursery"],
            "TypeOfEstablishment (code)": [1, 1, 7, 7],
            "TestProgress": [5.0, 5.0, 5.0, 5.0],
        }
    )

    metric.execute(df)

    # Primary, code 1 -> Applicable -> Major, 1.0
    assert df.loc[0, metric.score_column] == 1.0
    assert df.loc[0, metric.flag_column] == RiskFlag.MAJOR.value

    # Nursery, code 1 -> Excluded by phase -> 0.0, No flag
    assert df.loc[1, metric.score_column] == 0.0
    assert df.loc[1, metric.flag_column] == RiskFlag.NO_FLAG.value

    # Nursery, code 7 -> Excluded by both -> 0.0, No flag
    assert df.loc[3, metric.score_column] == 0.0
    assert df.loc[3, metric.flag_column] == RiskFlag.NO_FLAG.value


def test_metric_applicability_cached_in_df_attrs():
    call_count = 0

    class CountingRule(ApplicabilityRule):
        def is_applicable(self, df: pd.DataFrame) -> pd.Series:
            nonlocal call_count
            call_count += 1
            return pd.Series([True, False], index=df.index)

    metric = BaseRiskMetric(
        name="TestCacheMetric",
        risk_group=RiskGroup.FINANCIAL,
        risk_score_maximum=1.0,
        applicability=CountingRule(),
    )

    df = pd.DataFrame({"URN": [1001, 1002]})
    first_res = metric.is_applicable(df)
    assert call_count == 1
    assert first_res.tolist() == [True, False]

    # Second call should read from df.attrs cache and not invoke rule again
    second_res = metric.is_applicable(df)
    assert call_count == 1
    assert second_res.tolist() == [True, False]

