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
