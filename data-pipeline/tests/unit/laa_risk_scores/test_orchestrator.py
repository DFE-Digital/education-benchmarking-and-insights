from io import BytesIO, StringIO
from unittest.mock import patch

import numpy as np
import pandas as pd
import pytest
from azure.core.exceptions import ResourceNotFoundError

from pipeline.laa_risk_scores.orchestrator import (
    create_laa_risk_scores_download_file,
    run_laa_risk_scores_pipeline,
)


def _build_cfr_parquet_bytes(urn: int, reserve: float, income: float) -> BytesIO:
    df = pd.DataFrame(
        {
            "Lead school in federation": ["0"],
            "TypeOfEstablishment (code)": [1],
            "Overall Phase": ["Primary"],
            "Revenue reserve": [reserve],
            "Total Income": [income],
            "Total Expenditure": [950_000.0],
            "Number of pupils": [200.0],
            "Other costs_Interest charges for loan and bank": [0.0],
            "TotalPupilsSixthForm": [0.0],
            "Ks2Progress": [1.5],
            "Progress8Measure": [np.nan],
            "PTRWM_EXP": [0.75],
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
            "Other costs_PFI charges": [0.0],
            "Income_Receipts supply teacher insurance": [10_000.0],
            "Income_Catering services": [30_000.0],
            "Income_Other Revenue Income": [10_000.0],
            "Premises staff and services_Premises staff": [20_000.0],
            "Premises staff and services_Maintenance of premises": [15_000.0],
            "Other costs_Grounds maintenance": [5_000.0],
            "Premises staff and services_Cleaning and caretaking": [10_000.0],
            "Utilities_Water and sewerage": [5_000.0],
            "Premises staff and services_Other occupation costs": [5_000.0],
        },
        index=pd.Index([urn], name="URN"),
    )
    buf = BytesIO()
    df.to_parquet(buf)
    buf.seek(0)
    return buf


def _fake_get_blob_for_run(container_name: str, blob_name: str) -> BytesIO:
    if container_name == "pre-processed" and blob_name.endswith(
        "maintained_schools.parquet"
    ):
        return _build_cfr_parquet_bytes(urn=1001, reserve=80_000.0, income=1_000_000.0)

    if blob_name in (
        "default/2026/1a_Absence_3term_school.csv",
        "default/2025/1a_Absence_3term_school.csv",
    ):
        return BytesIO(
            b"time_period,school_urn,sess_overall_percent\n202425,1001,4.0\n"
        )
    if blob_name in (
        "default/2026/capacity_school_200910-202425.csv",
        "default/2025/capacity_school_200910-202425.csv",
    ):
        return BytesIO(b"time_period,school_urn,school_places\n202425,1001,200.0\n")
    if blob_name in (
        "default/2026/specialist-provision_school_202223-202425.csv",
        "default/2025/specialist-provision_school_202223-202425.csv",
    ):
        return BytesIO(b"time_period,school_urn,school_places\n202425,1001,20.0\n")
    if blob_name in (
        "default/2026/AppsandOffers_2026_SchoolLevel29062026.csv",
        "default/2025/AppsandOffers_2025_SchoolLevel07102025.csv",
    ):
        return BytesIO(
            b"time_period,school_urn,times_put_as_1st_preference,total_number_places_offered\n"
            b"202425,1001,110.0,100.0\n"
            b"202526,1001,110.0,100.0\n"
        )

    raise AssertionError(f"Unexpected blob requested: {container_name}/{blob_name}")


@pytest.mark.parametrize("cfr_year,academic_year", [(2025, 202425), (2026, 202526)])
@patch("pipeline.laa_risk_scores.loader.get_blob")
@patch("pipeline.laa_risk_scores.orchestrator.write_blob")
@patch("pipeline.utils.database._write_data")
def test_run_laa_risk_scores_pipeline_executes_end_to_end_with_isolated_io(
    mock_db_write_data,
    mock_write_blob,
    mock_get_blob,
    cfr_year: int,
    academic_year: int,
):
    mock_get_blob.side_effect = _fake_get_blob_for_run
    run_id = str(cfr_year)

    result = run_laa_risk_scores_pipeline(cfr_year=cfr_year, run_id=run_id)

    assert len(result) == 1
    assert result.loc[result["URN"] == 1001, "school_places"].iloc[0] == 220.0
    assert result.loc[result["URN"] == 1001, "PTRWM_EXP"].iloc[0] == 75.0

    assert mock_db_write_data.call_count == 2
    indicators_call = mock_db_write_data.call_args_list[0].kwargs
    headers_call = mock_db_write_data.call_args_list[1].kwargs

    assert indicators_call["table"] == "LASchoolRiskIndicators"
    assert indicators_call["run_id"] == run_id
    written_indicators = indicators_call["df"]
    assert set(written_indicators["URN"]) == {1001}
    assert (written_indicators["RiskIndicatorContribution"] == 0.0).all()

    assert headers_call["table"] == "LASchoolRiskIndicatorsHeaders"
    assert headers_call["run_id"] == run_id
    written_headers = headers_call["df"]
    assert written_headers.loc[0, "URN"] == 1001
    assert written_headers.loc[0, "Overall"] == 0.0
    assert written_headers.loc[0, "OverallGrade"] == "A*"

    mock_write_blob.assert_called_once()
    container, blob_path, csv_payload = mock_write_blob.call_args.args
    assert container == "artifacts"
    assert blob_path == f"default/{run_id}/laa_risk_scores_download.csv"
    exported_df = pd.read_csv(StringIO(csv_payload))
    assert exported_df.loc[0, "URN"] == 1001
    assert exported_df.loc[
        0, f"Current revenue reserve as % income ({academic_year})"
    ] == pytest.approx(0.08)


@patch("pipeline.laa_risk_scores.loader.get_blob")
def test_run_laa_risk_scores_pipeline_raises_value_error_when_historic_cfr_parquet_missing(
    mock_get_blob,
):
    def _fail_on_missing_historical_parquet(container_name: str, blob_name: str):
        if blob_name == "default/2022/maintained_schools.parquet":
            raise ResourceNotFoundError("Blob not found")
        return _fake_get_blob_for_run(container_name, blob_name)

    mock_get_blob.side_effect = _fail_on_missing_historical_parquet

    with pytest.raises(
        ValueError,
        match="Cannot compute historical financial risk metrics without historic CFR data going back 4 years",
    ):
        run_laa_risk_scores_pipeline(cfr_year=2026, run_id="2026")


@patch("pipeline.laa_risk_scores.orchestrator.write_blob")
def test_create_laa_risk_scores_download_file_exports_only_schema_columns_to_csv(
    mock_write_blob,
):
    df = pd.DataFrame(
        {
            "URN": [10001, 10002],
            "TypeOfEstablishment (code)": [1, 2],
            "Overall Phase": ["Primary", "Secondary"],
            "Revenue reserve": [150000.0, -12000.0],
            "Total Income": [1000000.0, 500000.0],
            "LAAStaffExpenditureRollup": [800000.0, 420000.0],
            "NetExpenditure": [950000.0, 480000.0],
            "Current revenue reserve as % income (202526)": [0.15, -0.024],
            "Current revenue reserve as % income (202526)_Score": [0.0, 0.5],
            "Current revenue reserve as % income (202526)_Risk": ["No flag", "Minor"],
            "Previous revenue reserve as % income (202425)": [0.10, -0.015],
            "Previous revenue reserve as % income (202425)_Score": [0.0, 0.25],
            "Previous revenue reserve as % income (202425)_Risk": ["No flag", "Minor"],
            "SomeUnrelatedColumn": ["unrelated1", "unrelated2"],
        }
    )

    create_laa_risk_scores_download_file(df, 2026, 2026)

    mock_write_blob.assert_called_once()
    container, blob_path, csv_str = mock_write_blob.call_args.args

    assert container == "artifacts"
    assert blob_path == "default/2026/laa_risk_scores_download.csv"

    written_df = pd.read_csv(StringIO(csv_str))
    expected_cols = [
        "URN",
        "TypeOfEstablishment (code)",
        "Overall Phase",
        "Revenue reserve",
        "Total Income",
        "LAAStaffExpenditureRollup",
        "NetExpenditure",
        "Current revenue reserve as % income (202526)",
        "Current revenue reserve as % income (202526)_Score",
        "Current revenue reserve as % income (202526)_Risk",
        "Previous revenue reserve as % income (202425)",
        "Previous revenue reserve as % income (202425)_Score",
        "Previous revenue reserve as % income (202425)_Risk",
    ]
    assert list(written_df.columns) == expected_cols
    assert "SomeUnrelatedColumn" not in written_df.columns
    assert len(written_df) == 2
