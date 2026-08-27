import numpy as np
import pandas as pd

from pipeline.laa_risk_scores.config import DEFAULT_RISK_CONFIG


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
        m for m in DEFAULT_RISK_CONFIG if m.name == "PupilChangeOver4Years"
    )
    metric_1yr = next(
        m for m in DEFAULT_RISK_CONFIG if m.name == "PupilChangeOver1Year"
    )

    # Act
    metric_4yr.execute(df)
    metric_1yr.execute(df)

    # Assert
    # The derived values should not contain inf or -inf
    assert not np.isinf(df["PupilChangeOver4Years"]).any()
    assert not np.isinf(df["PupilChangeOver1Year"]).any()

    # The division-by-zero case should resolve to NaN and result in max score / Major risk
    assert pd.isna(df.loc[0, "PupilChangeOver4Years"])
    assert df.loc[0, "PupilChangeOver4Years_Risk"] == "Major"
    assert df.loc[0, "PupilChangeOver4Years_Score"] == 1.5

    assert pd.isna(df.loc[0, "PupilChangeOver1Year"])
    assert df.loc[0, "PupilChangeOver1Year_Risk"] == "Major"
    assert df.loc[0, "PupilChangeOver1Year_Score"] == 1.5


def test_parental_preference_metric_scoring():
    # Find ParentalPreference from DEFAULT_RISK_CONFIG
    metric = next(m for m in DEFAULT_RISK_CONFIG if m.name == "ParentalPreference")

    # Arrange:
    # 1. School with standard type and value 0.5 (should be RiskScore 1.5, Major)
    # 2. School with standard type and value 0.72 (should be RiskScore 0.8, Minor)
    # 3. School with standard type and NaN value (should be RiskScore 1.5, Major)
    # 4. School with TypeOfEstablishment = 7 and value 0.5 (should be RiskScore 0.0, None)
    # 5. School with TypeOfEstablishment = 12 and NaN value (should be RiskScore 0.0, None)
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
    assert df.loc[0, "ParentalPreference_Score"] == 1.5
    assert df.loc[0, "ParentalPreference_Risk"] == "Major"

    # 2. School with standard type and value 0.72 -> RiskScore 0.8, Minor
    assert df.loc[1, "ParentalPreference_Score"] == 0.8
    assert df.loc[1, "ParentalPreference_Risk"] == "Minor"

    # 3. School with standard type and NaN value -> RiskScore 1.5, Major
    assert df.loc[2, "ParentalPreference_Score"] == 1.5
    assert df.loc[2, "ParentalPreference_Risk"] == "Major"

    # 4. School with TypeOfEstablishment = 7 and value 0.5 -> RiskScore 0.0, None
    assert df.loc[3, "ParentalPreference_Score"] == 0.0
    assert df.loc[3, "ParentalPreference_Risk"] == "None"

    # 5. School with TypeOfEstablishment = 12 and NaN value -> RiskScore 0.0, None
    assert df.loc[4, "ParentalPreference_Score"] == 0.0
    assert df.loc[4, "ParentalPreference_Risk"] == "None"

    # 6. School with TypeOfEstablishment = 10 (not 7 or 12) and value 0.5 -> RiskScore 1.5, Major
    assert df.loc[5, "ParentalPreference_Score"] == 1.5
    assert df.loc[5, "ParentalPreference_Risk"] == "Major"


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
        m for m in DEFAULT_RISK_CONFIG if m.name == "InterestOnLoanFlag"
    )
    premises_exp = next(
        m for m in DEFAULT_RISK_CONFIG if m.name == "PercentExpenditureOnPremises"
    )
    staff_exp = next(
        m for m in DEFAULT_RISK_CONFIG if m.name == "PercentExpenditureOnStaff"
    )
    sixth_form = next(m for m in DEFAULT_RISK_CONFIG if m.name == "PupilsSixthForm")
    absence = next(m for m in DEFAULT_RISK_CONFIG if m.name == "PupilAbsence")
    progress_score = next(
        m for m in DEFAULT_RISK_CONFIG if m.name == "PerformanceTablesProgressScore"
    )
    achievement_score = next(
        m for m in DEFAULT_RISK_CONFIG if m.name == "PerformanceTablesAchievementScore"
    )

    # Also test one that should still default to Major
    end_year_balance = next(
        m for m in DEFAULT_RISK_CONFIG if m.name == "EndYearBalanceAsPercentageIncome"
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
    assert df.loc[0, "InterestOnLoanFlag_Risk"] == "None"
    assert df.loc[0, "InterestOnLoanFlag_Score"] == 0.0

    assert df.loc[0, "PercentExpenditureOnPremises_Risk"] == "Minor"
    assert df.loc[0, "PercentExpenditureOnPremises_Score"] == 0.5

    assert df.loc[0, "PercentExpenditureOnStaff_Risk"] == "Minor"
    assert df.loc[0, "PercentExpenditureOnStaff_Score"] == 1.5

    assert df.loc[0, "PupilsSixthForm_Risk"] == "Minor"
    assert df.loc[0, "PupilsSixthForm_Score"] == 0.5

    assert df.loc[0, "PupilAbsence_Risk"] == "Minor"
    assert df.loc[0, "PupilAbsence_Score"] == 0.5

    assert df.loc[0, "PerformanceTablesProgressScore_Risk"] == "Minor"
    assert df.loc[0, "PerformanceTablesProgressScore_Score"] == 0.25

    assert df.loc[0, "PerformanceTablesAchievementScore_Risk"] == "Minor"
    assert df.loc[0, "PerformanceTablesAchievementScore_Score"] == 0.25

    # This one has no risk_flag_maximum override, so it should default to Major
    assert df.loc[0, "EndYearBalanceAsPercentageIncome_Risk"] == "Major"
