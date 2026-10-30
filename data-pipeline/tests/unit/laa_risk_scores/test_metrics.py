import numpy as np
import pandas as pd
import pytest

from pipeline.laa_risk_scores.config import DEFAULT_RISK_CONFIG
from pipeline.laa_risk_scores.engine import melt_laa_risk_scores
from pipeline.laa_risk_scores.metrics import (
    BaseRiskMetric,
    BinaryRiskMetric,
    ChangeInExpenditureOver4YearsMetric,
    CurrentLongTermSurplusABoveThresholdFor5YearsMetric,
    CurrentYearRevenueReserveMetric,
    DeficitInLast4YearsFlagMetric,
    ExcludePhases,
    InterestOnLoanFlagMetric,
    LargeDecreaseInBalanceMetric,
    MetricRule,
    OverspendMetric,
    ParentalPreferenceMetric,
    PercentExpenditureOnPremisesMetric,
    PercentExpenditureOnStaffMetric,
    PerformanceTablesAchievementScoreMetric,
    PerformanceTablesProgressScoreMetric,
    PreviousYearRevenueReserveMetric,
    PupilAbsenceMetric,
    PupilChangeOver1YearMetric,
    PupilChangeOver4YearsMetric,
    PupilNumberVarianceFromCapacityMetric,
    PupilsSixthFormMetric,
    RangeRiskMetric,
    RiskFlag,
    RiskGroup,
    RiskIndicatorValueFormatting,
)


def _get_metric(metric_cls: type[BaseRiskMetric]) -> BaseRiskMetric:
    return next(m for m in DEFAULT_RISK_CONFIG if isinstance(m, metric_cls))


@pytest.mark.parametrize(
    "reserve_ratio,expected_score,expected_flag",
    [
        (0.10, 0.0, RiskFlag.NO_FLAG.value),
        (0.0, 0.0, RiskFlag.NO_FLAG.value),
        (-0.005, 0.25, RiskFlag.MINOR.value),
        (-0.01, 0.5, RiskFlag.MINOR.value),
        (-0.02, 0.5, RiskFlag.MINOR.value),
        (-0.025, 1.0, RiskFlag.MINOR.value),
        (-0.03, 1.0, RiskFlag.MINOR.value),
        (-0.04, 1.25, RiskFlag.MINOR.value),
        (-0.045, 1.25, RiskFlag.MINOR.value),
        (-0.05, 1.75, RiskFlag.MAJOR.value),
        (-0.055, 1.75, RiskFlag.MAJOR.value),
        (-0.06, 2.0, RiskFlag.MAJOR.value),
        (-0.07, 2.0, RiskFlag.MAJOR.value),
        (-0.075, 2.5, RiskFlag.MAJOR.value),
        (-0.08, 2.5, RiskFlag.MAJOR.value),
        (-0.09, 3.0, RiskFlag.MAJOR.value),
        (-0.12, 3.0, RiskFlag.MAJOR.value),
    ],
)
def test_current_year_revenue_reserve_metric_scores_threshold_bands_and_boundaries(
    reserve_ratio: float, expected_score: float, expected_flag: str
):
    metric = _get_metric(CurrentYearRevenueReserveMetric)
    df = pd.DataFrame(
        {
            "Revenue reserve": [reserve_ratio * 1_000_000.0],
            "Total Income": [1_000_000.0],
        }
    )

    metric.execute(df)

    assert df.loc[0, metric.value_column] == pytest.approx(reserve_ratio)
    assert df.loc[0, metric.score_column] == expected_score
    assert df.loc[0, metric.flag_column] == expected_flag


@pytest.mark.parametrize(
    "reserve_ratio,expected_score,expected_flag",
    [
        (0.05, 0.0, RiskFlag.NO_FLAG.value),
        (0.0, 0.0, RiskFlag.NO_FLAG.value),
        (-0.005, 0.125, RiskFlag.MINOR.value),
        (-0.01, 0.25, RiskFlag.MINOR.value),
        (-0.02, 0.25, RiskFlag.MINOR.value),
        (-0.025, 0.5, RiskFlag.MINOR.value),
        (-0.03, 0.5, RiskFlag.MINOR.value),
        (-0.04, 0.625, RiskFlag.MINOR.value),
        (-0.045, 0.625, RiskFlag.MINOR.value),
        (-0.05, 0.875, RiskFlag.MAJOR.value),
        (-0.055, 0.875, RiskFlag.MAJOR.value),
        (-0.06, 1.0, RiskFlag.MAJOR.value),
        (-0.07, 1.0, RiskFlag.MAJOR.value),
        (-0.075, 1.25, RiskFlag.MAJOR.value),
        (-0.08, 1.25, RiskFlag.MAJOR.value),
        (-0.09, 1.5, RiskFlag.MAJOR.value),
        (-0.15, 1.5, RiskFlag.MAJOR.value),
    ],
)
def test_previous_year_revenue_reserve_metric_scores_threshold_bands_and_boundaries(
    reserve_ratio: float, expected_score: float, expected_flag: str
):
    metric = _get_metric(PreviousYearRevenueReserveMetric)
    df = pd.DataFrame(
        {
            "Revenue reserve_y_minus_one": [reserve_ratio * 1_000_000.0],
            "Total Income_y_minus_one": [1_000_000.0],
        }
    )

    metric.execute(df)

    assert df.loc[0, metric.value_column] == pytest.approx(reserve_ratio)
    assert df.loc[0, metric.score_column] == expected_score
    assert df.loc[0, metric.flag_column] == expected_flag


@pytest.mark.parametrize(
    "reserve,income",
    [
        (np.nan, np.nan),
        (np.nan, 1_000_000.0),
        (-10_000.0, 0.0),
        (5_000.0, 0.0),
    ],
)
def test_revenue_reserve_metrics_penalise_missing_or_zero_income_with_maximum_risk(
    reserve: float, income: float
):
    metric_curr = _get_metric(CurrentYearRevenueReserveMetric)
    metric_prev = _get_metric(PreviousYearRevenueReserveMetric)
    df = pd.DataFrame(
        {
            "Revenue reserve": [reserve],
            "Total Income": [income],
            "Revenue reserve_y_minus_one": [reserve],
            "Total Income_y_minus_one": [income],
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


@pytest.mark.parametrize(
    "interest_charges,expected_value,expected_score,expected_flag",
    [
        (0.0, False, 0.0, RiskFlag.NO_FLAG.value),
        (-50.0, False, 0.0, RiskFlag.NO_FLAG.value),
        (np.nan, False, 0.0, RiskFlag.NO_FLAG.value),
        (1250.0, True, 0.25, RiskFlag.MINOR.value),
    ],
)
def test_interest_on_loan_flag_metric_scores_positive_interest_charges(
    interest_charges: float,
    expected_value: bool,
    expected_score: float,
    expected_flag: str,
):
    metric = _get_metric(InterestOnLoanFlagMetric)
    df = pd.DataFrame(
        {"Other costs_Interest charges for loan and bank": [interest_charges]}
    )

    metric.execute(df)

    assert bool(df.loc[0, metric.value_column]) is expected_value
    assert df.loc[0, metric.score_column] == expected_score
    assert df.loc[0, metric.flag_column] == expected_flag


@pytest.mark.parametrize(
    "premises_spend,net_expenditure,expected_score,expected_flag",
    [
        (50_000.0, 1_000_000.0, 0.0, RiskFlag.NO_FLAG.value),
        (100_000.0, 1_000_000.0, 0.25, RiskFlag.MINOR.value),
        (120_000.0, 1_000_000.0, 0.25, RiskFlag.MINOR.value),
        (150_000.0, 1_000_000.0, 0.5, RiskFlag.MINOR.value),
        (250_000.0, 1_000_000.0, 0.5, RiskFlag.MINOR.value),
        (np.nan, 1_000_000.0, 0.5, RiskFlag.MINOR.value),
        (50_000.0, 0.0, 0.5, RiskFlag.MINOR.value),
    ],
)
def test_percent_expenditure_on_premises_metric_scores_bands_and_missing_data(
    premises_spend: float,
    net_expenditure: float,
    expected_score: float,
    expected_flag: str,
):
    metric = _get_metric(PercentExpenditureOnPremisesMetric)
    df = pd.DataFrame(
        {
            "LAAPremisesExpenditureRollup": [premises_spend],
            "NetExpenditure": [net_expenditure],
        }
    )

    metric.execute(df)

    assert df.loc[0, metric.score_column] == expected_score
    assert df.loc[0, metric.flag_column] == expected_flag


@pytest.mark.parametrize(
    "staff_spend,net_expenditure,expected_score,expected_flag",
    [
        (750_000.0, 1_000_000.0, 0.0, RiskFlag.NO_FLAG.value),
        (800_000.0, 1_000_000.0, 0.75, RiskFlag.MINOR.value),
        (850_000.0, 1_000_000.0, 1.0, RiskFlag.MINOR.value),
        (900_000.0, 1_000_000.0, 1.25, RiskFlag.MINOR.value),
        (950_000.0, 1_000_000.0, 1.5, RiskFlag.MINOR.value),
        (np.nan, 1_000_000.0, 1.5, RiskFlag.MINOR.value),
    ],
)
def test_percent_expenditure_on_staff_metric_scores_bands_and_missing_data(
    staff_spend: float,
    net_expenditure: float,
    expected_score: float,
    expected_flag: str,
):
    metric = _get_metric(PercentExpenditureOnStaffMetric)
    df = pd.DataFrame(
        {
            "LAAStaffExpenditureRollup": [staff_spend],
            "NetExpenditure": [net_expenditure],
        }
    )

    metric.execute(df)

    assert df.loc[0, metric.score_column] == expected_score
    assert df.loc[0, metric.flag_column] == expected_flag


@pytest.mark.parametrize(
    "exp_this_year,exp_four_years_ago,income,expected_score,expected_flag",
    [
        (1_100_000.0, 1_000_000.0, 1_000_000.0, 0.0, RiskFlag.NO_FLAG.value),
        (1_150_000.0, 1_000_000.0, 1_000_000.0, 0.5, RiskFlag.MINOR.value),
        (1_200_000.0, 1_000_000.0, 1_000_000.0, 1.0, RiskFlag.MINOR.value),
        (1_300_000.0, 1_000_000.0, 1_000_000.0, 1.5, RiskFlag.MAJOR.value),
        (1_100_000.0, np.nan, 1_000_000.0, 1.5, RiskFlag.MAJOR.value),
        (1_100_000.0, 1_000_000.0, 0.0, 1.5, RiskFlag.MAJOR.value),
    ],
)
def test_change_in_expenditure_over_4_years_metric_scores_bands_and_missing_data(
    exp_this_year: float,
    exp_four_years_ago: float,
    income: float,
    expected_score: float,
    expected_flag: str,
):
    metric = _get_metric(ChangeInExpenditureOver4YearsMetric)
    df = pd.DataFrame(
        {
            "Total Expenditure": [exp_this_year],
            "Total Expenditure_y_minus_four": [exp_four_years_ago],
            "Total Income": [income],
        }
    )

    metric.execute(df)

    assert df.loc[0, metric.score_column] == expected_score
    assert df.loc[0, metric.flag_column] == expected_flag


@pytest.mark.parametrize(
    "reserves_five_years,expected_value,expected_score,expected_flag",
    [
        (
            [10_000.0, 5_000.0, 0.0, 2_000.0, 1_000.0],
            False,
            0.0,
            RiskFlag.NO_FLAG.value,
        ),
        ([-1.0, 5_000.0, 5_000.0, 5_000.0, 5_000.0], True, 3.0, RiskFlag.MAJOR.value),
        (
            [10_000.0, 5_000.0, 5_000.0, 5_000.0, -500.0],
            True,
            3.0,
            RiskFlag.MAJOR.value,
        ),
    ],
)
def test_deficit_in_last_4_years_flag_metric_detects_any_negative_reserve(
    reserves_five_years: list[float],
    expected_value: bool,
    expected_score: float,
    expected_flag: str,
):
    metric = _get_metric(DeficitInLast4YearsFlagMetric)
    df = pd.DataFrame(
        {
            "Revenue reserve": [reserves_five_years[0]],
            "Revenue reserve_y_minus_one": [reserves_five_years[1]],
            "Revenue reserve_y_minus_two": [reserves_five_years[2]],
            "Revenue reserve_y_minus_three": [reserves_five_years[3]],
            "Revenue reserve_y_minus_four": [reserves_five_years[4]],
        }
    )

    metric.execute(df)

    assert bool(df.loc[0, metric.value_column]) is expected_value
    assert df.loc[0, metric.score_column] == expected_score
    assert df.loc[0, metric.flag_column] == expected_flag


@pytest.mark.parametrize(
    "ratios_five_years,expected_value,expected_score,expected_flag",
    [
        ([0.15, 0.16, 0.20, 0.15, 0.18], True, 1.0, RiskFlag.MAJOR.value),
        ([0.15, 0.16, 0.149, 0.15, 0.18], False, 0.0, RiskFlag.NO_FLAG.value),
        ([0.10, 0.10, 0.10, 0.10, 0.10], False, 0.0, RiskFlag.NO_FLAG.value),
    ],
)
def test_long_term_surplus_above_threshold_for_5_years_metric_requires_all_years_above_15_percent(
    ratios_five_years: list[float],
    expected_value: bool,
    expected_score: float,
    expected_flag: str,
):
    metric = _get_metric(CurrentLongTermSurplusABoveThresholdFor5YearsMetric)
    df = pd.DataFrame(
        {
            "Revenue reserve": [ratios_five_years[0] * 100_000.0],
            "Total Income": [100_000.0],
            "Revenue reserve_y_minus_one": [ratios_five_years[1] * 100_000.0],
            "Total Income_y_minus_one": [100_000.0],
            "Revenue reserve_y_minus_two": [ratios_five_years[2] * 100_000.0],
            "Total Income_y_minus_two": [100_000.0],
            "Revenue reserve_y_minus_three": [ratios_five_years[3] * 100_000.0],
            "Total Income_y_minus_three": [100_000.0],
            "Revenue reserve_y_minus_four": [ratios_five_years[4] * 100_000.0],
            "Total Income_y_minus_four": [100_000.0],
        }
    )

    metric.execute(df)

    assert bool(df.loc[0, metric.value_column]) is expected_value
    assert df.loc[0, metric.score_column] == expected_score
    assert df.loc[0, metric.flag_column] == expected_flag


@pytest.mark.parametrize(
    "income,expenditure,expected_score,expected_flag",
    [
        (1_000_000.0, 950_000.0, 0.0, RiskFlag.NO_FLAG.value),
        (1_000_000.0, 1_000_000.0, 0.0, RiskFlag.NO_FLAG.value),
        (1_000_000.0, 1_005_000.0, 0.25, RiskFlag.MINOR.value),
        (1_000_000.0, 1_020_000.0, 0.5, RiskFlag.MINOR.value),
        (1_000_000.0, 1_030_000.0, 1.0, RiskFlag.MINOR.value),
        (1_000_000.0, 1_045_000.0, 1.25, RiskFlag.MINOR.value),
        (1_000_000.0, 1_055_000.0, 1.75, RiskFlag.MAJOR.value),
        (1_000_000.0, 1_070_000.0, 2.0, RiskFlag.MAJOR.value),
        (1_000_000.0, 1_080_000.0, 2.5, RiskFlag.MAJOR.value),
        (1_000_000.0, 1_100_000.0, 3.0, RiskFlag.MAJOR.value),
        (0.0, 1_000_000.0, 3.0, RiskFlag.MAJOR.value),
    ],
)
def test_overspend_metric_scores_in_year_balance_percentage_bands(
    income: float,
    expenditure: float,
    expected_score: float,
    expected_flag: str,
):
    metric = _get_metric(OverspendMetric)
    df = pd.DataFrame(
        {
            "Total Income": [income],
            "Total Expenditure": [expenditure],
        }
    )

    metric.execute(df)

    assert df.loc[0, metric.score_column] == expected_score
    assert df.loc[0, metric.flag_column] == expected_flag


@pytest.mark.parametrize(
    "curr_ratio,prev_ratio,expected_value,expected_score,expected_flag",
    [
        (0.04, 0.15, True, 1.5, RiskFlag.MAJOR.value),
        (0.025, 0.125, True, 1.5, RiskFlag.MAJOR.value),
        (0.16, 0.30, False, 0.0, RiskFlag.NO_FLAG.value),
        (0.08, 0.15, False, 0.0, RiskFlag.NO_FLAG.value),
    ],
)
def test_large_decrease_in_balance_metric_flags_ten_percent_drop_when_reserve_below_fifteen_percent(
    curr_ratio: float,
    prev_ratio: float,
    expected_value: bool,
    expected_score: float,
    expected_flag: str,
):
    metric = _get_metric(LargeDecreaseInBalanceMetric)
    df = pd.DataFrame(
        {
            "Revenue reserve": [curr_ratio * 1_000_000.0],
            "Total Income": [1_000_000.0],
            "Revenue reserve_y_minus_one": [prev_ratio * 1_000_000.0],
            "Total Income_y_minus_one": [1_000_000.0],
        }
    )

    metric.execute(df)

    assert bool(df.loc[0, metric.value_column]) is expected_value
    assert df.loc[0, metric.score_column] == expected_score
    assert df.loc[0, metric.flag_column] == expected_flag


@pytest.mark.parametrize(
    "phase,pupils,places,expected_score,expected_flag",
    [
        ("Primary", 35.0, 100.0, 1.5, RiskFlag.MAJOR.value),
        ("Primary", 42.0, 100.0, 1.25, RiskFlag.MAJOR.value),
        ("Primary", 48.0, 100.0, 1.0, RiskFlag.MAJOR.value),
        ("Primary", 52.0, 100.0, 0.75, RiskFlag.MINOR.value),
        ("Primary", 58.0, 100.0, 0.6, RiskFlag.MINOR.value),
        ("Primary", 62.0, 100.0, 0.45, RiskFlag.MINOR.value),
        ("Primary", 68.0, 100.0, 0.3, RiskFlag.MINOR.value),
        ("Primary", 72.0, 100.0, 0.15, RiskFlag.MINOR.value),
        ("Primary", 80.0, 100.0, 0.0, RiskFlag.NO_FLAG.value),
        ("Primary", 50.0, 0.0, 1.5, RiskFlag.MAJOR.value),
        ("Special", 35.0, 100.0, 0.0, RiskFlag.NO_FLAG.value),
        ("Special", np.nan, np.nan, 0.0, RiskFlag.NO_FLAG.value),
    ],
)
def test_pupil_number_variance_from_capacity_metric_scores_bands_and_exempts_special_phase(
    phase: str,
    pupils: float,
    places: float,
    expected_score: float,
    expected_flag: str,
):
    metric = _get_metric(PupilNumberVarianceFromCapacityMetric)
    df = pd.DataFrame(
        {
            "Overall Phase": [phase],
            "Number of pupils": [pupils],
            "school_places": [places],
        }
    )

    metric.execute(df)

    assert df.loc[0, metric.score_column] == expected_score
    assert df.loc[0, metric.flag_column] == expected_flag


@pytest.mark.parametrize(
    "pupils_now,pupils_historical,expected_score,expected_1yr_flag,expected_4yr_flag",
    [
        (80.0, 100.0, 1.5, RiskFlag.MAJOR.value, RiskFlag.MAJOR.value),
        (86.0, 100.0, 1.25, RiskFlag.MAJOR.value, RiskFlag.MINOR.value),
        (88.0, 100.0, 1.0, RiskFlag.MAJOR.value, RiskFlag.MINOR.value),
        (91.0, 100.0, 0.8, RiskFlag.MINOR.value, RiskFlag.MINOR.value),
        (93.0, 100.0, 0.6, RiskFlag.MINOR.value, RiskFlag.MINOR.value),
        (95.0, 100.0, 0.4, RiskFlag.MINOR.value, RiskFlag.MINOR.value),
        (97.0, 100.0, 0.2, RiskFlag.MINOR.value, RiskFlag.MINOR.value),
        (105.0, 100.0, 0.0, RiskFlag.NO_FLAG.value, RiskFlag.NO_FLAG.value),
        (50.0, 0.0, 1.5, RiskFlag.MAJOR.value, RiskFlag.MAJOR.value),
        (0.0, 0.0, 1.5, RiskFlag.MAJOR.value, RiskFlag.MAJOR.value),
    ],
)
def test_pupil_change_metrics_score_reductions_and_handle_zero_historical_pupils(
    pupils_now: float,
    pupils_historical: float,
    expected_score: float,
    expected_1yr_flag: str,
    expected_4yr_flag: str,
):
    metric_1yr = _get_metric(PupilChangeOver1YearMetric)
    metric_4yr = _get_metric(PupilChangeOver4YearsMetric)
    df = pd.DataFrame(
        {
            "Number of pupils": [pupils_now],
            "Number of pupils_y_minus_one": [pupils_historical],
            "Number of pupils_y_minus_four": [pupils_historical],
        }
    )

    metric_1yr.execute(df)
    metric_4yr.execute(df)

    assert not np.isinf(df[metric_1yr.value_column]).any()
    assert not np.isinf(df[metric_4yr.value_column]).any()
    assert df.loc[0, metric_1yr.score_column] == expected_score
    assert df.loc[0, metric_1yr.flag_column] == expected_1yr_flag
    assert df.loc[0, metric_4yr.score_column] == expected_score
    assert df.loc[0, metric_4yr.flag_column] == expected_4yr_flag


@pytest.mark.parametrize(
    "phase,sixth_form_pupils,expected_score,expected_flag",
    [
        ("Secondary", 0.0, 0.0, RiskFlag.NO_FLAG.value),
        ("Secondary", 25.0, 0.5, RiskFlag.MINOR.value),
        ("Secondary", 60.0, 0.4, RiskFlag.MINOR.value),
        ("Secondary", 80.0, 0.3, RiskFlag.MINOR.value),
        ("Secondary", 110.0, 0.2, RiskFlag.MINOR.value),
        ("Secondary", 130.0, 0.1, RiskFlag.MINOR.value),
        ("Secondary", 160.0, 0.0, RiskFlag.NO_FLAG.value),
        ("Secondary", np.nan, 0.5, RiskFlag.MINOR.value),
        ("Primary", 25.0, 0.0, RiskFlag.NO_FLAG.value),
        ("Nursery", np.nan, 0.0, RiskFlag.NO_FLAG.value),
    ],
)
def test_pupils_sixth_form_metric_scores_secondary_and_exempts_primary_and_nursery(
    phase: str,
    sixth_form_pupils: float,
    expected_score: float,
    expected_flag: str,
):
    metric = _get_metric(PupilsSixthFormMetric)
    df = pd.DataFrame(
        {
            "Overall Phase": [phase],
            "TotalPupilsSixthForm": [sixth_form_pupils],
        }
    )

    metric.execute(df)

    assert df.loc[0, metric.score_column] == expected_score
    assert df.loc[0, metric.flag_column] == expected_flag


@pytest.mark.parametrize(
    "establishment_code,absence_percent,expected_scaled_value,expected_score,expected_flag",
    [
        (1, 4.0, 0.04, 0.0, RiskFlag.NO_FLAG.value),
        (1, 5.5, 0.055, 0.25, RiskFlag.MINOR.value),
        (1, 7.0, 0.07, 0.5, RiskFlag.MINOR.value),
        (1, np.nan, np.nan, 0.5, RiskFlag.MINOR.value),
        (7, 10.0, 0.10, 0.0, RiskFlag.NO_FLAG.value),
        (12, 20.0, 0.20, 0.25, RiskFlag.MINOR.value),
        (44, 30.0, 0.30, 0.5, RiskFlag.MINOR.value),
    ],
)
def test_pupil_absence_metric_scales_percentage_and_applies_establishment_specific_rules(
    establishment_code: int,
    absence_percent: float,
    expected_scaled_value: float,
    expected_score: float,
    expected_flag: str,
):
    metric = _get_metric(PupilAbsenceMetric)
    df = pd.DataFrame(
        {
            "TypeOfEstablishment (code)": [establishment_code],
            "sess_overall_percent": [absence_percent],
        }
    )

    metric.execute(df)

    if pd.isna(expected_scaled_value):
        assert pd.isna(df.loc[0, metric.value_column])
    else:
        assert df.loc[0, metric.value_column] == pytest.approx(expected_scaled_value)
    assert df.loc[0, metric.score_column] == expected_score
    assert df.loc[0, metric.flag_column] == expected_flag


@pytest.mark.parametrize(
    "phase,pref_ratio,expected_score,expected_flag,expected_formatted",
    [
        ("Primary", 0.50, 1.5, RiskFlag.MAJOR.value, "Low"),
        ("Primary", 0.62, 1.2, RiskFlag.MAJOR.value, "Low"),
        ("Primary", 0.675, 1.0, RiskFlag.MAJOR.value, "Low"),
        ("Primary", 0.72, 0.8, RiskFlag.MINOR.value, "Medium"),
        ("Primary", 0.78, 0.6, RiskFlag.MINOR.value, "Medium"),
        ("Primary", 0.82, 0.4, RiskFlag.MINOR.value, "Medium"),
        ("Primary", 0.88, 0.2, RiskFlag.MINOR.value, "Medium"),
        ("Primary", 0.90, 0.0, RiskFlag.NO_FLAG.value, "Medium"),
        ("Primary", 0.95, 0.0, RiskFlag.NO_FLAG.value, "High"),
        ("Primary", np.nan, 1.5, RiskFlag.MAJOR.value, None),
        ("Special", 0.50, 0.0, RiskFlag.NO_FLAG.value, "N/A"),
        ("Special", np.nan, 0.0, RiskFlag.NO_FLAG.value, "N/A"),
        ("Nursery", 0.50, 0.0, RiskFlag.NO_FLAG.value, "N/A"),
        ("Pupil referral unit", 1.20, 0.0, RiskFlag.NO_FLAG.value, "N/A"),
    ],
)
def test_parental_preference_metric_scores_formats_and_exempts_excluded_phases(
    phase: str,
    pref_ratio: float,
    expected_score: float,
    expected_flag: str,
    expected_formatted: str | None,
):
    metric = _get_metric(ParentalPreferenceMetric)
    df = pd.DataFrame(
        {
            "Overall Phase": [phase],
            "proportion_1stprefs_v_totaloffers": [pref_ratio],
        }
    )

    metric.execute(df)
    formatted = metric.format_value(df[metric.value_column], df=df)

    assert df.loc[0, metric.score_column] == expected_score
    assert df.loc[0, metric.flag_column] == expected_flag
    assert formatted.iloc[0] == expected_formatted


@pytest.mark.parametrize(
    "phase,ptrwm_exp,average_attainment,expected_val,expected_score,expected_flag,expected_formatted",
    [
        ("Primary", np.nan, np.nan, np.nan, 0.0, RiskFlag.NO_FLAG.value, None),
        ("Secondary", np.nan, np.nan, np.nan, 0.0, RiskFlag.NO_FLAG.value, None),
        ("Primary", 40.0, np.nan, 40.0, 0.25, RiskFlag.MINOR.value, "40"),
        ("Primary", 46.0, np.nan, 46.0, 0.0, RiskFlag.NO_FLAG.value, "46"),
        ("Secondary", np.nan, 42.5, 42.5, 0.25, RiskFlag.MINOR.value, "42.5"),
        ("Secondary", np.nan, 52.0, 52.0, 0.0, RiskFlag.NO_FLAG.value, "52"),
        ("Special", 30.0, 30.0, np.nan, 0.0, RiskFlag.NO_FLAG.value, "N/A"),
    ],
)
def test_performance_tables_achievement_score_metric_scores_by_phase_without_penalising_missing(
    phase: str,
    ptrwm_exp: float,
    average_attainment: float,
    expected_val: float,
    expected_score: float,
    expected_flag: str,
    expected_formatted: str | None,
):
    metric = _get_metric(PerformanceTablesAchievementScoreMetric)
    df = pd.DataFrame(
        {
            "Overall Phase": [phase],
            "PTRWM_EXP": [ptrwm_exp],
            "AverageAttainment": [average_attainment],
        }
    )

    metric.execute(df)
    formatted = metric.format_value(df[metric.value_column], df=df)

    if pd.isna(expected_val):
        assert pd.isna(df.loc[0, metric.value_column])
    else:
        assert df.loc[0, metric.value_column] == pytest.approx(expected_val)
    assert df.loc[0, metric.score_column] == expected_score
    assert df.loc[0, metric.flag_column] == expected_flag
    assert formatted.iloc[0] == expected_formatted


def test_performance_tables_progress_score_metric_derives_ks2_and_p8_and_exempts_excluded_phases():
    default_metric = _get_metric(PerformanceTablesProgressScoreMetric)
    df_default = pd.DataFrame(
        {
            "Overall Phase": ["Primary", "Secondary"],
            "Ks2Progress": [-0.6, np.nan],
            "Progress8Measure": [np.nan, -0.5],
        }
    )

    default_metric.execute(df_default)

    assert df_default.loc[0, default_metric.value_column] == pytest.approx(-0.2)
    assert df_default.loc[0, default_metric.score_column] == 0.0
    assert df_default.loc[0, default_metric.flag_column] == RiskFlag.NO_FLAG.value
    assert df_default.loc[1, default_metric.value_column] == pytest.approx(-0.5)
    assert df_default.loc[1, default_metric.score_column] == 0.0
    assert df_default.loc[1, default_metric.flag_column] == RiskFlag.NO_FLAG.value


@pytest.mark.parametrize(
    "formatting,raw_value,expected_string",
    [
        (RiskIndicatorValueFormatting.BOOLEAN, True, "Yes"),
        (RiskIndicatorValueFormatting.BOOLEAN, False, "No"),
        (RiskIndicatorValueFormatting.BOOLEAN, 1, "Yes"),
        (RiskIndicatorValueFormatting.BOOLEAN, 0, "No"),
        (RiskIndicatorValueFormatting.BOOLEAN, "True", "Yes"),
        (RiskIndicatorValueFormatting.BOOLEAN, "False", "No"),
        (RiskIndicatorValueFormatting.BOOLEAN, 1.0, "Yes"),
        (RiskIndicatorValueFormatting.BOOLEAN, 0.0, "No"),
        (RiskIndicatorValueFormatting.BOOLEAN, np.nan, None),
        (RiskIndicatorValueFormatting.DECIMAL, 120, "120"),
        (RiskIndicatorValueFormatting.DECIMAL, 120.0, "120"),
        (RiskIndicatorValueFormatting.DECIMAL, 4.5, "4.5"),
        (RiskIndicatorValueFormatting.DECIMAL, 0.0, "0"),
        (RiskIndicatorValueFormatting.DECIMAL, -1.25, "-1.25"),
        (RiskIndicatorValueFormatting.DECIMAL, np.inf, None),
        (RiskIndicatorValueFormatting.DECIMAL, np.nan, None),
        (RiskIndicatorValueFormatting.PERCENTAGE, 0.15, "0.15"),
        (RiskIndicatorValueFormatting.PERCENTAGE, -0.024, "-0.024"),
        (RiskIndicatorValueFormatting.PERCENTAGE, 0.0, "0.0"),
        (RiskIndicatorValueFormatting.PERCENTAGE, np.inf, None),
        (RiskIndicatorValueFormatting.STRING, "High", "High"),
        (RiskIndicatorValueFormatting.STRING, None, None),
    ],
)
def test_base_risk_metric_format_value_serialises_scalars_by_formatting_type(
    formatting: RiskIndicatorValueFormatting,
    raw_value: object,
    expected_string: str | None,
):
    metric = BaseRiskMetric(
        name="TestMetric",
        risk_group=RiskGroup.FINANCIAL,
        risk_score_maximum=1.0,
        value_formatting=formatting,
    )

    formatted = metric.format_value(pd.Series([raw_value], dtype=object))

    assert formatted.iloc[0] == expected_string


def test_exclude_phases_treats_all_rows_as_applicable_when_phase_column_missing():
    rule = ExcludePhases(["Nursery", "Special"])
    df = pd.DataFrame({"URN": [1001, 1002]})

    assert rule.is_applicable(df).tolist() == [True, True]


def test_melt_laa_risk_scores_produces_long_indicators_and_aggregated_group_headers():
    metric_bool = BinaryRiskMetric(
        name="LoanFlag",
        risk_group=RiskGroup.FINANCIAL,
        risk_score_maximum=0.25,
        score_when_1=0.25,
        risk_when_1=RiskFlag.MINOR.value,
        value_formatting=RiskIndicatorValueFormatting.BOOLEAN,
    )
    metric_pct = BaseRiskMetric(
        name="SpendPct",
        risk_group=RiskGroup.FINANCIAL,
        risk_score_maximum=1.0,
        value_formatting=RiskIndicatorValueFormatting.PERCENTAGE,
    )
    metric_sixth_form = BaseRiskMetric(
        name="SixthFormCount",
        risk_group=RiskGroup.SCHOOL_CHARACTERISTICS,
        risk_score_maximum=0.5,
        value_formatting=RiskIndicatorValueFormatting.DECIMAL,
        applicability=ExcludePhases(["Primary"]),
    )
    metric_attainment = BaseRiskMetric(
        name="AttainmentScore",
        risk_group=RiskGroup.EDUCATIONAL_PERFORMANCE,
        risk_score_maximum=0.25,
        value_formatting=RiskIndicatorValueFormatting.DECIMAL,
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
            "SixthFormCount": [50.0, 50.0],
            "SixthFormCount_Score": [0.0, 0.0],
            "SixthFormCount_Risk": ["No flag", "No flag"],
            "AttainmentScore": [60.0, 40.0],
            "AttainmentScore_Score": [0.0, 0.25],
            "AttainmentScore_Risk": ["No flag", "Minor"],
            "Total_Risk_Score": [0.75, 1.25],
            "LAA_Risk_Grade": ["B", "C"],
        }
    )

    evaluators = [metric_bool, metric_pct, metric_sixth_form, metric_attainment]
    indicators_df, headers_df = melt_laa_risk_scores(
        df_headlines, evaluators, run_id="2026"
    )

    sixth_form_rows = indicators_df[
        indicators_df["RiskIndicator"] == "SixthFormCount"
    ].reset_index(drop=True)
    assert sixth_form_rows.loc[0, "RiskIndicatorValue"] == "50"
    assert sixth_form_rows.loc[0, "RiskIndicatorValueFormatting"] == "Decimal"
    assert sixth_form_rows.loc[1, "RiskIndicatorValue"] == "N/A"
    assert sixth_form_rows.loc[1, "RiskIndicatorValueFormatting"] == "String"

    assert headers_df.loc[0, "Financial"] == pytest.approx(0.75)
    assert headers_df.loc[0, "FinancialMax"] == pytest.approx(1.25)
    assert headers_df.loc[0, "SchoolAndPupil"] == pytest.approx(0.0)
    assert headers_df.loc[0, "SchoolAndPupilMax"] == pytest.approx(0.5)
    assert headers_df.loc[0, "EducationalPerformance"] == pytest.approx(0.0)
    assert headers_df.loc[0, "EducationalPerformanceMax"] == pytest.approx(0.25)
    assert headers_df.loc[0, "Overall"] == pytest.approx(0.75)
    assert headers_df.loc[0, "OverallMax"] == pytest.approx(2.0)
    assert headers_df.loc[0, "OverallGrade"] == "B"
