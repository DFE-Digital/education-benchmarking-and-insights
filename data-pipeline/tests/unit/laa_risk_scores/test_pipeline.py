from unittest.mock import MagicMock, patch

import pandas as pd
import pytest

from pipeline.laa_risk_scores.orchestrator import run_laa_risk_scores_pipeline
from pipeline.laa_risk_scores.preprocessing import (
    preprocess_laa_data,
    preprocess_laa_extra_ancillary_data,
)


def test_preprocess_laa_extra_ancillary_data():
    run_year = 2025
    time_period = 202425

    # Mock raw absences
    absences_raw = pd.DataFrame(
        {
            "time_period": [time_period, 202324, time_period],
            "school_urn": [1001, 1001, 1002],
            "sess_overall_percent": [4.5, 3.2, 5.1],
        }
    )

    # Mock raw capacity
    capacity_raw = pd.DataFrame(
        {
            "time_period": [time_period, time_period],
            "school_urn": [1001, 1002],
            "school_places": [100.0, 150.0],
        }
    )

    # Mock raw capacity special
    capacity_special_raw = pd.DataFrame(
        {
            "time_period": [time_period],
            "school_urn": [1001],  # Splitting provision for school 1001
            "school_places": [20.0],
        }
    )

    # Mock raw parental preference
    parental_preference_raw = pd.DataFrame(
        {
            "time_period": [time_period, time_period, time_period],
            "school_urn": [1001, 1002, 1003],
            "times_put_as_1st_preference": [50.0, 100.0, 0.0],
            "total_number_places_offered": [
                40.0,
                100.0,
                0.0,
            ],  # One with zero offers to test division-by-zero
        }
    )

    absences, capacity, parental_pref = preprocess_laa_extra_ancillary_data(
        absences_raw=absences_raw,
        capacity_raw=capacity_raw,
        capacity_special_raw=capacity_special_raw,
        parental_preference_raw=parental_preference_raw,
        run_year=run_year,
    )

    # 1. Assert absences filtered correctly
    assert len(absences) == 2
    assert set(absences["school_urn"]) == {1001, 1002}

    # 2. Assert capacity concatenated and summed correctly
    assert len(capacity) == 2
    school_1001_capacity = capacity[capacity["school_urn"] == 1001][
        "school_places"
    ].values[0]
    # 100.0 (regular) + 20.0 (special) = 120.0
    assert school_1001_capacity == 120.0

    # 3. Assert parental preferences grouped and safely divided
    assert len(parental_pref) == 3
    school_1001_pref = parental_pref[parental_pref["school_urn"] == 1001][
        "proportion_1stprefs_v_totaloffers"
    ].values[0]
    assert school_1001_pref == 1.25  # 50.0 / 40.0

    # Check that division by zero was handled safely and returned 0.0
    school_1003_pref = parental_pref[parental_pref["school_urn"] == 1003][
        "proportion_1stprefs_v_totaloffers"
    ].values[0]
    assert school_1003_pref == 0.0


def test_preprocess_laa_data_merges_and_aligns_urn_index():
    # We set up 5 years of historic CFR data frames indexed on URN.
    # To test the RangeIndex scrambling fix, we use different order of schools in year-minus-one.
    cfr_this = pd.DataFrame(
        {"Lead school in federation": ["0", "1", "0"], "Value": [100.0, 200.0, 300.0]},
        index=[1001, 1002, 1003],
    )
    cfr_this.index.name = "URN"

    cfr_minus_one = pd.DataFrame(
        {"Value": [3000.0, 1000.0, 2000.0]}, index=[1003, 1001, 1002]
    )  # different order
    cfr_minus_one.index.name = "URN"

    cfr_minus_two = pd.DataFrame(
        {"Value": [10.0, 20.0, 30.0]}, index=[1001, 1002, 1003]
    )
    cfr_minus_two.index.name = "URN"

    cfr_minus_three = pd.DataFrame(
        {"Value": [10.0, 20.0, 30.0]}, index=[1001, 1002, 1003]
    )
    cfr_minus_three.index.name = "URN"

    cfr_minus_four = pd.DataFrame(
        {"Value": [10.0, 20.0, 30.0]}, index=[1001, 1002, 1003]
    )
    cfr_minus_four.index.name = "URN"

    # Mock preprocessed ancillary data
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
        run_year=2025,
    )

    # 1. Assert school 1002 is filtered out (as its Lead school in federation is "1")
    assert len(result) == 2
    assert set(result["URN"]) == {1001, 1003}

    # 2. Assert index alignment worked and is not scrambled by different sorting in cfr_minus_one
    school_1001 = result[result["URN"] == 1001]
    assert school_1001["Value"].values[0] == 100.0
    # Should align with school_urn 1001 from cfr_minus_one, which is 1000.0, NOT 3000.0
    assert school_1001["Value_y_minus_one"].values[0] == 1000.0

    school_1003 = result[result["URN"] == 1003]
    assert school_1003["Value"].values[0] == 300.0
    # Should align with school_urn 1003 from cfr_minus_one, which is 3000.0
    assert school_1003["Value_y_minus_one"].values[0] == 3000.0


@patch("pipeline.laa_risk_scores.orchestrator.load_laa_risk_score_data")
@patch("pipeline.laa_risk_scores.orchestrator.load_laa_extra_ancillary_data")
@patch("pipeline.laa_risk_scores.orchestrator.derive_laa_risk_scores")
@patch("pipeline.laa_risk_scores.orchestrator.insert_laa_risk_scores")
@patch("pipeline.laa_risk_scores.orchestrator.create_laa_risk_scores_download_file")
def test_orchestrator_pipeline(
    mock_create_download,
    mock_insert_db,
    mock_derive,
    mock_load_ancillary,
    mock_load_cfr,
):
    # Setup mocks
    cfr_df = pd.DataFrame(
        {"Lead school in federation": ["0"], "Value": [100.0]}, index=[1001]
    )
    cfr_df.index.name = "URN"

    mock_load_cfr.return_value = (cfr_df, cfr_df, cfr_df, cfr_df, cfr_df)
    mock_derive.return_value = (pd.DataFrame(), pd.DataFrame(), pd.DataFrame())

    mock_load_ancillary.return_value = {
        "absences_raw": pd.DataFrame(
            {
                "time_period": [202425],
                "school_urn": [1001],
                "sess_overall_percent": [4.0],
            }
        ),
        "capacity_raw": pd.DataFrame(
            {"time_period": [202425], "school_urn": [1001], "school_places": [120.0]}
        ),
        "capacity_special_raw": pd.DataFrame(
            {"time_period": [202425], "school_urn": [1001], "school_places": [10.0]}
        ),
        "parental_preference_raw": pd.DataFrame(
            {
                "time_period": [202425],
                "school_urn": [1001],
                "times_put_as_1st_preference": [1.0],
                "total_number_places_offered": [1.0],
            }
        ),
        "run_year": 2025,
    }

    result = run_laa_risk_scores_pipeline(run_year=2025, run_id="test-run")

    assert result is not None
    assert len(result) == 1
    assert result["URN"].values[0] == 1001
    assert result["school_places"].values[0] == 130.0  # 120 + 10
