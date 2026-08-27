from unittest.mock import patch

import pandas as pd
import pytest

from pipeline.utils.database import insert_laa_risk_scores


def test_insert_laa_risk_scores_mapping():
    indicators_df = pd.DataFrame(
        {
            "URN": [1001],
            "RunId": ["2026"],
            "RiskGroup": ["Financial"],
            "RiskIndicator": ["MetricA"],
            "RiskIndicatorValue": [10.5],
            "RiskIndicatorFlag": ["Minor"],
            "RiskIndicatorContribution": [2.0],
            "RiskIndicatorContributionMax": [5.0],
        }
    )
    headers_df = pd.DataFrame(
        {
            "URN": [1001],
            "RunId": ["2026"],
            "EducationalPerformance": [3.0],
            "EducationalPerformanceMax": [10.0],
            "Financial": [2.0],
            "FinancialMax": [10.0],
            "SchoolAndPupil": [1.0],
            "SchoolAndPupilMax": [10.0],
            "Overall": [6.0],
            "OverallMax": [30.0],
            "OverallGrade": ["Green"],
        }
    )

    with patch("pipeline.utils.database._write_data") as mock_write_data:
        insert_laa_risk_scores("2026", indicators_df, headers_df, engine="mock_engine")

        assert mock_write_data.call_count == 2

        # Check first write (indicators)
        first_call_args = mock_write_data.call_args_list[0][1]
        assert first_call_args["table"] == "LASchoolRiskIndicators"
        assert first_call_args["run_id"] == "2026"
        assert first_call_args["engine"] == "mock_engine"
        written_indicators_df = first_call_args["df"]

        expected_indicators_cols = [
            "URN",
            "RunId",
            "RiskGroup",
            "RiskIndicator",
            "RiskIndicatorValue",
            "RiskIndicatorFlag",
            "RiskIndicatorContribution",
            "RiskIndicatorContributionMax",
        ]
        assert list(written_indicators_df.columns) == expected_indicators_cols

        # Check second write (headers)
        second_call_args = mock_write_data.call_args_list[1][1]
        assert second_call_args["table"] == "LASchoolRiskIndicatorsHeaders"
        assert second_call_args["run_id"] == "2026"
        assert second_call_args["engine"] == "mock_engine"
        written_headers_df = second_call_args["df"]

        expected_headers_cols = [
            "URN",
            "RunId",
            "EducationalPerformance",
            "EducationalPerformanceMax",
            "Financial",
            "FinancialMax",
            "SchoolAndPupil",
            "SchoolAndPupilMax",
            "Overall",
            "OverallMax",
            "OverallGrade",
        ]
        assert list(written_headers_df.columns) == expected_headers_cols


from pipeline.laa_risk_scores.orchestrator import run_laa_risk_scores_pipeline


@patch("pipeline.laa_risk_scores.orchestrator.load_laa_risk_score_data")
@patch("pipeline.laa_risk_scores.orchestrator.load_laa_extra_ancillary_data")
@patch("pipeline.laa_risk_scores.orchestrator.preprocess_laa_extra_ancillary_data")
@patch("pipeline.laa_risk_scores.orchestrator.preprocess_laa_data")
@patch("pipeline.laa_risk_scores.orchestrator.derive_laa_risk_scores")
@patch("pipeline.laa_risk_scores.orchestrator.insert_laa_risk_scores")
@patch("pipeline.laa_risk_scores.orchestrator.create_laa_risk_scores_download_file")
def test_run_laa_risk_scores_pipeline_coordination(
    mock_create_download,
    mock_insert_db,
    mock_derive,
    mock_preprocess_laa_data,
    mock_preprocess_ancillary,
    mock_load_ancillary,
    mock_load_data,
):
    mock_load_ancillary.return_value = {
        "absences_raw": pd.DataFrame({"URN": [1]}),
        "capacity_raw": pd.DataFrame({"URN": [2]}),
        "capacity_special_raw": pd.DataFrame({"URN": [3]}),
        "parental_preference_raw": pd.DataFrame({"URN": [4]}),
    }
    mock_preprocess_ancillary.return_value = (
        pd.DataFrame({"abs": [1]}),
        pd.DataFrame({"cap": [2]}),
        pd.DataFrame({"pref": [3]}),
    )
    mock_load_data.return_value = (
        pd.DataFrame({"c1": [1]}),
        pd.DataFrame({"c2": [2]}),
        pd.DataFrame({"c3": [3]}),
        pd.DataFrame({"c4": [4]}),
        pd.DataFrame({"c5": [5]}),
    )
    mock_preprocess_laa_data.return_value = pd.DataFrame({"preprocessed": [1]})
    mock_derive.return_value = (
        pd.DataFrame({"raw_with_metrics": [0]}),
        pd.DataFrame({"indicators": [1]}),
        pd.DataFrame({"headers": [2]}),
    )

    run_laa_risk_scores_pipeline(2025, "2026")

    mock_load_ancillary.assert_called_once_with(2025)
    mock_preprocess_ancillary.assert_called_once_with(
        absences_raw=mock_load_ancillary.return_value["absences_raw"],
        capacity_raw=mock_load_ancillary.return_value["capacity_raw"],
        capacity_special_raw=mock_load_ancillary.return_value["capacity_special_raw"],
        parental_preference_raw=mock_load_ancillary.return_value[
            "parental_preference_raw"
        ],
        run_year=2025,
    )
    mock_load_data.assert_called_once_with(2025)
    mock_preprocess_laa_data.assert_called_once_with(
        cfr_data_this_year=mock_load_data.return_value[0],
        cfr_data_year_minus_one=mock_load_data.return_value[1],
        cfr_data_year_minus_two=mock_load_data.return_value[2],
        cfr_data_year_minus_three=mock_load_data.return_value[3],
        cfr_data_year_minus_four=mock_load_data.return_value[4],
        absences=mock_preprocess_ancillary.return_value[0],
        capacity=mock_preprocess_ancillary.return_value[1],
        parental_preference=mock_preprocess_ancillary.return_value[2],
        run_year=2025,
    )
    mock_derive.assert_called_once_with(
        mock_preprocess_laa_data.return_value, run_year=2025, run_id="2026"
    )
    mock_insert_db.assert_called_once()
    insert_args, _ = mock_insert_db.call_args
    pd.testing.assert_frame_equal(insert_args[0], mock_derive.return_value[1])
    pd.testing.assert_frame_equal(insert_args[1], mock_derive.return_value[2])
    assert insert_args[2] == 2026

    mock_create_download.assert_called_once()
    download_args, _ = mock_create_download.call_args
    pd.testing.assert_frame_equal(download_args[0], mock_derive.return_value[0])
    assert download_args[1] == 2025
    assert download_args[2] == 2026


from pipeline.laa_risk_scores.orchestrator import create_laa_risk_scores_download_file


@patch("pipeline.laa_risk_scores.orchestrator.write_blob")
def test_create_laa_risk_scores_download_file(mock_write_blob):
    # Setup some mock data including school metadata, raw columns, metric outputs, and overall risk indicators
    df = pd.DataFrame(
        {
            "URN": [10001, 10002],
            "TypeOfEstablishment (code)": [1, 2],
            "Overall Phase": ["Primary", "Secondary"],
            "Revenue reserve": [150000.0, -12000.0],
            "Total Income": [1000000.0, 500000.0],
            "LAAStaffExpenditureRollup": [800000.0, 420000.0],
            "NetExpenditure": [950000.0, 480000.0],
            "EndYearBalanceAsPercentageIncome": [0.15, -0.024],
            "EndYearBalanceAsPercentageIncome_Score": [0.0, 0.5],
            "EndYearBalanceAsPercentageIncome_Risk": ["No Risk", "Minor"],
            "SomeUnrelatedColumn": ["unrelated1", "unrelated2"],
        }
    )

    create_laa_risk_scores_download_file(df, 2026, 2026)

    # Verify write_blob coordination
    mock_write_blob.assert_called_once()
    call_args = mock_write_blob.call_args[0]

    assert call_args[0] == "artifacts"
    assert call_args[1] == "default/2026/laa_risk_scores_download.csv"

    # Let's convert the output string back to a DataFrame to verify columns and rows
    csv_str = call_args[2]
    written_df = (
        pd.read_csv(pd.compat.StringIO(csv_str))
        if hasattr(pd, "compat") and hasattr(pd.compat, "StringIO")
        else pd.read_csv(pd.io.common.StringIO(csv_str))
    )

    # Should contain all the LAA columns that were in original df
    expected_cols = [
        "URN",
        "TypeOfEstablishment (code)",
        "Overall Phase",
        "Revenue reserve",
        "Total Income",
        "LAAStaffExpenditureRollup",
        "NetExpenditure",
        "EndYearBalanceAsPercentageIncome",
        "EndYearBalanceAsPercentageIncome_Score",
        "EndYearBalanceAsPercentageIncome_Risk",
    ]
    assert list(written_df.columns) == expected_cols
    assert "SomeUnrelatedColumn" not in written_df.columns
    assert len(written_df) == 2
