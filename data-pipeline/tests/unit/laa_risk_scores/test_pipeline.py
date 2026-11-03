import numpy as np
import pandas as pd
import pytest

from pipeline.laa_risk_scores.config import (
    get_yearly_grading_thresholds,
    get_yearly_risk_config,
)
from pipeline.laa_risk_scores.engine import (
    derive_laa_risk_scores,
    roll_up_laa_risk_scores_to_headlines,
)
from pipeline.laa_risk_scores.metrics import (
    BaseRiskMetric,
    PerformanceTablesAchievementScoreMetric,
    RiskFlag,
    RiskGroup,
)
from pipeline.laa_risk_scores.preprocessing import (
    preprocess_laa_data,
    preprocess_laa_extra_ancillary_data,
)


def _build_graded_school_input(
    minor_scores: list[float],
    major_scores: list[float],
    parental_pref: float,
    attainment: float,
) -> tuple[pd.DataFrame, list[BaseRiskMetric]]:
    evaluators: list[BaseRiskMetric] = [
        PerformanceTablesAchievementScoreMetric(
            name="Attainment",
            risk_group=RiskGroup.EDUCATIONAL_PERFORMANCE,
            risk_score_maximum=0.25,
        )
    ]
    row_data: dict[str, list[object]] = {
        "URN": [1001],
        "proportion_1stprefs_v_totaloffers": [parental_pref],
        "Attainment": [attainment],
        "Attainment_Score": [0.0],
        "Attainment_Risk": [RiskFlag.NO_FLAG.value],
    }

    for idx in range(12):
        metric_name = f"Metric_{idx}"
        evaluators.append(
            BaseRiskMetric(
                name=metric_name,
                risk_group=RiskGroup.FINANCIAL,
                risk_score_maximum=3.0,
            )
        )
        if idx < len(major_scores):
            score = major_scores[idx]
            flag = RiskFlag.MAJOR.value
        elif idx - len(major_scores) < len(minor_scores):
            score = minor_scores[idx - len(major_scores)]
            flag = RiskFlag.MINOR.value
        else:
            score = 0.0
            flag = RiskFlag.NO_FLAG.value

        row_data[metric_name] = [0.0]
        row_data[f"{metric_name}_Score"] = [score]
        row_data[f"{metric_name}_Risk"] = [flag]

    return pd.DataFrame(row_data), evaluators


def test_preprocess_laa_extra_ancillary_data_filters_period_and_aggregates_split_schools():
    run_year = 2025
    time_period = 202425

    absences_raw = pd.DataFrame(
        {
            "time_period": [time_period, 202324, time_period],
            "school_urn": [1001, 1001, 1002],
            "sess_overall_percent": [4.5, 3.2, 5.1],
        }
    )
    capacity_raw = pd.DataFrame(
        {
            "time_period": [time_period, time_period],
            "school_urn": [1001, 1002],
            "school_places": [100.0, 150.0],
        }
    )
    capacity_special_raw = pd.DataFrame(
        {
            "time_period": [time_period],
            "school_urn": [1001],
            "school_places": [20.0],
        }
    )
    parental_preference_raw = pd.DataFrame(
        {
            "time_period": [time_period, time_period, time_period],
            "school_urn": [1001, 1002, 1003],
            "times_put_as_1st_preference": [50.0, 100.0, 0.0],
            "total_number_places_offered": [40.0, 100.0, 0.0],
        }
    )

    absences, capacity, parental_pref = preprocess_laa_extra_ancillary_data(
        absences_raw=absences_raw,
        capacity_raw=capacity_raw,
        capacity_special_raw=capacity_special_raw,
        parental_preference_raw=parental_preference_raw,
        cfr_year=run_year,
    )

    assert len(absences) == 2
    assert set(absences["school_urn"]) == {1001, 1002}

    assert len(capacity) == 2
    school_1001_capacity = capacity.loc[
        capacity["school_urn"] == 1001, "school_places"
    ].iloc[0]
    assert school_1001_capacity == 120.0

    assert len(parental_pref) == 3
    school_1001_pref = parental_pref.loc[
        parental_pref["school_urn"] == 1001, "proportion_1stprefs_v_totaloffers"
    ].iloc[0]
    assert school_1001_pref == pytest.approx(1.25)

    school_1003_pref = parental_pref.loc[
        parental_pref["school_urn"] == 1003, "proportion_1stprefs_v_totaloffers"
    ].iloc[0]
    assert school_1003_pref == 0.0


def test_preprocess_laa_extra_ancillary_data_uses_lagged_period_for_2026_capacity_and_absences():
    absences_raw = pd.DataFrame(
        {
            "time_period": [202526, 202425],
            "school_urn": [1001, 1002],
            "sess_overall_percent": [5.0, 6.0],
        }
    )
    capacity_raw = pd.DataFrame(
        {
            "time_period": [202425, 202324],
            "school_urn": [1001, 1001],
            "school_places": [150.0, 140.0],
        }
    )
    capacity_special_raw = pd.DataFrame(
        {
            "time_period": [202425],
            "school_urn": [1001],
            "school_places": [25.0],
        }
    )
    parental_preference_raw = pd.DataFrame(
        {
            "time_period": [202526, 202425],
            "school_urn": [1001, 1001],
            "times_put_as_1st_preference": [60.0, 30.0],
            "total_number_places_offered": [50.0, 50.0],
        }
    )

    absences, capacity, parental_pref = preprocess_laa_extra_ancillary_data(
        absences_raw=absences_raw,
        capacity_raw=capacity_raw,
        capacity_special_raw=capacity_special_raw,
        parental_preference_raw=parental_preference_raw,
        cfr_year=2026,
    )

    assert absences["school_urn"].tolist() == [1002]
    assert capacity["school_places"].tolist() == [175.0]
    assert parental_pref["proportion_1stprefs_v_totaloffers"].tolist() == [1.2]


def test_preprocess_laa_data_merges_and_aligns_urn_index_while_excluding_federation_leads():
    cfr_this = pd.DataFrame(
        {"Lead school in federation": ["0", "1", "0"], "Value": [100.0, 200.0, 300.0]},
        index=pd.Index([1001, 1002, 1003], name="URN"),
    )
    cfr_minus_one = pd.DataFrame(
        {"Value": [3000.0, 1000.0, 2000.0]},
        index=pd.Index([1003, 1001, 1002], name="URN"),
    )
    cfr_minus_two = pd.DataFrame(
        {"Value": [10.0, 20.0, 30.0]},
        index=pd.Index([1001, 1002, 1003], name="URN"),
    )
    cfr_minus_three = pd.DataFrame(
        {"Value": [10.0, 20.0, 30.0]},
        index=pd.Index([1001, 1002, 1003], name="URN"),
    )
    cfr_minus_four = pd.DataFrame(
        {"Value": [10.0, 20.0, 30.0]},
        index=pd.Index([1001, 1002, 1003], name="URN"),
    )

    absences = pd.DataFrame(
        {"school_urn": [1001, 1003], "sess_overall_percent": [4.0, 5.0]}
    )
    capacity = pd.DataFrame(
        {"school_urn": [1001, 1003], "school_places": [120.0, 180.0]}
    )
    parental_pref = pd.DataFrame(
        {"school_urn": [1001, 1003], "proportion_1stprefs_v_totaloffers": [1.25, 0.5]}
    )

    result = preprocess_laa_data(
        cfr_data_this_year=cfr_this,
        cfr_data_year_minus_one=cfr_minus_one,
        cfr_data_year_minus_two=cfr_minus_two,
        cfr_data_year_minus_three=cfr_minus_three,
        cfr_data_year_minus_four=cfr_minus_four,
        absences=absences,
        capacity=capacity,
        parental_preference=parental_pref,
        cfr_year=2025,
    )

    assert set(result["URN"]) == {1001, 1003}
    assert result.loc[result["URN"] == 1001, "Value_y_minus_one"].iloc[0] == 1000.0
    assert result.loc[result["URN"] == 1003, "Value_y_minus_one"].iloc[0] == 3000.0


@pytest.mark.parametrize(
    "raw_ptrwm_values,expected_ptrwm_values",
    [
        ([0.78, 0.45, None], [78.0, 45.0, np.nan]),
        ([78.0, 45.0, None], [78.0, 45.0, np.nan]),
    ],
)
def test_preprocess_laa_data_rescales_ptrwm_exp_only_when_on_proportion_scale(
    raw_ptrwm_values: list[float | None],
    expected_ptrwm_values: list[float],
):
    index = pd.Index([1001, 1002, 1003], name="URN")
    cfr_this = pd.DataFrame(
        {
            "Lead school in federation": ["0", "0", "0"],
            "PTRWM_EXP": raw_ptrwm_values,
        },
        index=index,
    )
    empty_history = pd.DataFrame(index=index)
    absences = pd.DataFrame({"school_urn": [], "sess_overall_percent": []})
    capacity = pd.DataFrame({"school_urn": [], "school_places": []})
    parental_pref = pd.DataFrame(
        {"school_urn": [], "proportion_1stprefs_v_totaloffers": []}
    )

    result = preprocess_laa_data(
        cfr_data_this_year=cfr_this,
        cfr_data_year_minus_one=empty_history,
        cfr_data_year_minus_two=empty_history,
        cfr_data_year_minus_three=empty_history,
        cfr_data_year_minus_four=empty_history,
        absences=absences,
        capacity=capacity,
        parental_preference=parental_pref,
        cfr_year=2026,
    )

    assert result.loc[result["URN"] == 1001, "PTRWM_EXP"].iloc[0] == pytest.approx(
        expected_ptrwm_values[0]
    )
    assert result.loc[result["URN"] == 1002, "PTRWM_EXP"].iloc[0] == pytest.approx(
        expected_ptrwm_values[1]
    )
    assert pd.isna(result.loc[result["URN"] == 1003, "PTRWM_EXP"].iloc[0])


@pytest.mark.parametrize(
    "minor_scores,major_scores,parental_pref,attainment,expected_grade",
    [
        ([], [], 1.0, 69.0, "A*"),
        ([], [], 1.2, np.nan, "A*"),
        ([0.25], [], 0.95, 60.0, "A"),
        ([], [], 0.95, 69.0, "A"),
        ([0.25], [], 0.95, 50.0, "B"),
        ([0.5, 0.5], [], 0.5, 40.0, "B"),
        ([0.5], [1.5], 0.5, 40.0, "C"),
        ([0.5], [1.5, 1.5], 0.5, 40.0, "D"),
        ([0.5], [1.5, 1.5, 1.5], 0.5, 40.0, "E"),
        ([0.5], [1.5, 1.5, 1.5, 1.5], 0.5, 40.0, "F"),
        ([1.0], [2.0, 2.0, 2.0, 2.0], 0.5, 40.0, "G"),
    ],
)
def test_roll_up_laa_risk_scores_to_headlines_assigns_expected_grade(
    minor_scores: list[float],
    major_scores: list[float],
    parental_pref: float,
    attainment: float,
    expected_grade: str,
):
    df, evaluators = _build_graded_school_input(
        minor_scores=minor_scores,
        major_scores=major_scores,
        parental_pref=parental_pref,
        attainment=attainment,
    )
    grading_thresholds = get_yearly_grading_thresholds(2026)

    result = roll_up_laa_risk_scores_to_headlines(df, evaluators, grading_thresholds)

    assert result.loc[0, "LAA_Risk_Grade"] == expected_grade


def test_roll_up_laa_risk_scores_to_headlines_without_thresholds_raises_value_error():
    df, evaluators = _build_graded_school_input(
        minor_scores=[], major_scores=[], parental_pref=1.0, attainment=70.0
    )

    with pytest.raises(ValueError, match="Missing grading thresholds"):
        roll_up_laa_risk_scores_to_headlines(df, evaluators, grading_thresholds=None)


def test_derive_laa_risk_scores_evaluates_rollups_scores_grades_and_melts_outputs():
    df = pd.DataFrame(
        {
            "URN": [1001],
            "TypeOfEstablishment (code)": [1],
            "Overall Phase": ["Primary"],
            "Revenue reserve": [80_000.0],
            "Revenue reserve_y_minus_one": [70_000.0],
            "Revenue reserve_y_minus_two": [60_000.0],
            "Revenue reserve_y_minus_three": [50_000.0],
            "Revenue reserve_y_minus_four": [40_000.0],
            "Total Income": [1_000_000.0],
            "Total Income_y_minus_one": [950_000.0],
            "Total Income_y_minus_two": [900_000.0],
            "Total Income_y_minus_three": [880_000.0],
            "Total Income_y_minus_four": [850_000.0],
            "Total Expenditure": [950_000.0],
            "Total Expenditure_y_minus_four": [860_000.0],
            "Number of pupils": [200.0],
            "Number of pupils_y_minus_one": [200.0],
            "Number of pupils_y_minus_four": [200.0],
            "Other costs_Interest charges for loan and bank": [0.0],
            "TotalPupilsSixthForm": [0.0],
            "Ks2Progress": [1.5],
            "Progress8Measure": [np.nan],
            "PTRWM_EXP": [75.0],
            "AverageAttainment": [np.nan],
            "Teaching and Teaching support staff_Teaching staff": [500_000.0],
            "Teaching and Teaching support staff_Supply teaching staff": [10_000.0],
            "Teaching and Teaching support staff_Education support staff": [100_000.0],
            "Non-educational support staff and services_Administrative and clerical staff": [
                40_000.0
            ],
            "Non-educational support staff and services_Other staff": [20_000.0],
            "Other costs_Indirect employee expenses": [5_000.0],
            "Other costs_Staff development and training": [5_000.0],
            "Other costs_Supply teacher insurance": [2_000.0],
            "Other costs_Staff-related insurance": [2_000.0],
            "Administrative supplies_Administrative supplies (non educational)": [
                6_000.0
            ],
            "Teaching and Teaching support staff_Agency supply teaching staff": [
                10_000.0
            ],
            "Non-educational support staff and services_Professional services (non-curriculum)": [
                10_000.0
            ],
            "Income_Receipts supply teacher insurance": [10_000.0],
            "Income_Catering services": [30_000.0],
            "Income_Other Revenue Income": [10_000.0],
            "Premises staff and services_Premises staff": [20_000.0],
            "Premises staff and services_Maintenance of premises": [15_000.0],
            "Other costs_Grounds maintenance": [5_000.0],
            "Premises staff and services_Cleaning and caretaking": [10_000.0],
            "Utilities_Water and sewerage": [5_000.0],
            "Premises staff and services_Other occupation costs": [5_000.0],
            "school_places": [220.0],
            "sess_overall_percent": [4.0],
            "proportion_1stprefs_v_totaloffers": [1.1],
        }
    )

    df_graded, indicators_df, headers_df = derive_laa_risk_scores(
        df, cfr_year=2026, run_id="2026"
    )

    assert df_graded.loc[0, "LAAStaffExpenditureRollup"] == pytest.approx(700_000.0)
    assert df_graded.loc[0, "NetExpenditure"] == pytest.approx(900_000.0)
    assert df_graded.loc[0, "LAAPremisesExpenditureRollup"] == pytest.approx(60_000.0)
    assert df_graded.loc[0, "Total_Risk_Score"] == 0.0
    assert df_graded.loc[0, "LAA_Risk_Grade"] == "A*"

    expected_metric_names = {m.name for m in get_yearly_risk_config(2026)}
    assert set(indicators_df["RiskIndicator"]) == expected_metric_names
    assert headers_df.loc[0, "Overall"] == 0.0
    assert headers_df.loc[0, "OverallGrade"] == "A*"
