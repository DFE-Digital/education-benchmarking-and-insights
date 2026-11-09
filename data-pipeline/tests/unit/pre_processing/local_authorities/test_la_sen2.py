import io
from unittest.mock import patch

import numpy as np
import pandas as pd
import pytest

from pipeline.pre_processing.ancillary.main import pre_process_sen2
from pipeline.pre_processing.ancillary.sen2 import (
    pivot_sen2_caseload_data,
    prepare_sen2_data,
)
from pipeline.pre_processing.s251 import local_authority


def test_sen2(la_sen2_raw: pd.DataFrame):
    """
    The 58 rows for a single LA should be pivoted to a single row.
    """
    result = prepare_sen2_data(io.StringIO(la_sen2_raw.to_csv()), 2024)

    assert len(la_sen2_raw.index) == 58
    assert len(result.index) == 1


def test_sen2_2025_caseload(la_sen2_caseload_2025_raw: pd.DataFrame):
    result = prepare_sen2_data(
        io.StringIO(la_sen2_caseload_2025_raw.to_csv(index=False)), 2025
    )

    assert len(result.index) == 1
    row = result.loc[("E10000000", 101)]
    assert row["EHCPTotal"] == 100
    assert row["EHCPMainstream"] == 17
    assert row["EHCPResourced"] == 6
    assert row["EHCPSpecial"] == 35
    assert row["EHCPIndependent"] == 9
    assert row["EHCPHospital"] == 6
    assert row["EHCPPost16"] == 12
    assert row["EHCPOther"] == 15


def test_sen2_2026_caseload_combines_split_special_academy_and_free_school(
    la_sen2_caseload_2026_raw: pd.DataFrame,
):
    result = prepare_sen2_data(
        io.StringIO(la_sen2_caseload_2026_raw.to_csv(index=False)), 2026
    )

    assert len(result.index) == 1
    row = result.loc[("E10000000", 101)]
    assert row["EHCPTotal"] == 100
    assert row["EHCPSpecial"] == 35
    assert row["EHCPHospital"] == 6
    assert row["EHCPPost16"] == 12
    assert row["EHCPOther"] == 15


@pytest.mark.parametrize(
    "special_academy,special_free_school,expected_ehcp_special",
    [
        (10, "x", 30),
        ("x", 5, 25),
        ("x", "x", pd.NA),
    ],
)
def test_sen2_2026_caseload_handles_suppressed_values_in_split_columns(
    la_sen2_caseload_2026_raw: pd.DataFrame,
    special_academy,
    special_free_school,
    expected_ehcp_special,
):
    df = la_sen2_caseload_2026_raw.copy()
    df["special_academy"] = [special_academy, 100, 10]
    df["special_free_school"] = [special_free_school, 50, 5]

    result = prepare_sen2_data(io.StringIO(df.to_csv(index=False)), 2026)

    val = result.loc[("E10000000", 101)]["EHCPSpecial"]
    if pd.isna(expected_ehcp_special):
        assert pd.isna(val)
    else:
        assert val == expected_ehcp_special


@pytest.mark.parametrize(
    "year,missing_col,fixture_name",
    [
        (2025, "special_academy_free", "la_sen2_caseload_2025_raw"),
        (2026, "special_academy", "la_sen2_caseload_2026_raw"),
        (2026, "special_free_school", "la_sen2_caseload_2026_raw"),
    ],
)
def test_pivot_sen2_caseload_missing_expected_column_raises_value_error(
    request, year: int, missing_col: str, fixture_name: str
):
    df = request.getfixturevalue(fixture_name).drop(columns=[missing_col])

    with pytest.raises(ValueError, match=missing_col):
        pivot_sen2_caseload_data(io.StringIO(df.to_csv(index=False)), year)


def test_pivot_sen2_caseload_normalises_groups_and_time_period(
    la_sen2_caseload_2026_raw: pd.DataFrame,
):
    pivoted = pivot_sen2_caseload_data(
        io.StringIO(la_sen2_caseload_2026_raw.to_csv(index=False)), 2026
    )

    assert pivoted["time_period"].dtype == "string"
    assert "2026" in pivoted["time_period"].values
    groups = dict(zip(pivoted["establishment_type"], pivoted["establishment_group"]))
    assert groups["Sixth form college"] == "Further education"
    assert groups["Hospital School"] == "Special school"
    assert "mainstream_total" not in groups


@pytest.mark.parametrize(
    "year,expected_blob_path,fixture_name",
    [
        (2024, "default/2024/sen2_estab_caseload.csv", "la_sen2_raw"),
        (2025, "default/2025/caseload.csv", "la_sen2_caseload_2025_raw"),
        (2026, "default/2026/caseload.csv", "la_sen2_caseload_2026_raw"),
    ],
)
def test_pre_process_sen2_uses_configured_filename_per_year(
    request, year: int, expected_blob_path: str, fixture_name: str
):
    raw_df = request.getfixturevalue(fixture_name)
    csv_buffer = io.StringIO(raw_df.to_csv(index=False))

    with patch(
        "pipeline.pre_processing.ancillary.main.get_blob", return_value=csv_buffer
    ) as mock_get_blob:
        result = pre_process_sen2(year, run_id=str(year))

    mock_get_blob.assert_called_once_with("raw", expected_blob_path)
    assert len(result.index) == 1


@pytest.fixture
def la_data():
    """Fixture for the local authority data with a MultiIndex."""
    data = {
        "new_la_code": ["E10000006", "E0600006", "E10000009", "E1000096", "E0600009"],
        "old_la_code": ["915", "320", "855", "815", "113"],
        "OtherCol1": [10, 20, 30, 40, 50],
    }
    df = pd.DataFrame(data).set_index(["new_la_code", "old_la_code"])
    return df


@pytest.fixture
def sen_data_perfect_match():
    """Fixture for SEN data that matches perfectly on the full index."""
    data = {
        "new_la_code": ["E10000006", "E0600006", "E10000009", "E0600009"],
        "old_la_code": ["915", "320", "855", "113"],
        "EHCPTotal": [100, 200, 300, 500],
        "OtherCol2": [1, 2, 3, 5],
    }
    df = pd.DataFrame(data).set_index(["new_la_code", "old_la_code"])
    return df


@pytest.fixture
def sen_data_old_la_match():
    """
    Fixture for SEN data that ONLY matches on 'old_la_code' for some records.
    E1000096/815 in la_data fails first join (index-based) but succeeds second join (on old_la_code 815).
    """
    data = {
        # This will match E10000006/915
        ("E10000006", "915"): {"EHCPTotal": 100, "OtherCol2": 1},
        # This will fail the index match for E1000096/815 because it's keyed as D99/815,
        # but the second stage will match on '815'
        ("D99", "815"): {"EHCPTotal": 400, "OtherCol2": 4},
        # This will match E0600009/113
        ("E0600009", "113"): {"EHCPTotal": 500, "OtherCol2": 5},
    }

    # Converting to DataFrame with MultiIndex
    df = pd.DataFrame.from_dict(
        data, orient="index", columns=["EHCPTotal", "OtherCol2"]
    )
    df.index.names = ["new_la_code", "old_la_code"]
    return df


def test_first_stage_join_success(la_data, sen_data_perfect_match):
    """Test that records successfully join in the first, index-based stage."""
    # E10000009/855 from la_data is NOT in sen_data_perfect_match, so it should still be NaN
    expected_index = la_data.index

    result = local_authority._join_sen_to_local_authority_data(
        la_data, sen_data_perfect_match
    )

    assert result.index.names == la_data.index.names
    assert len(result) == len(la_data)

    # Perfect matches
    assert result.loc[("E10000006", "915")]["EHCPTotal"] == 100
    assert result.loc[("E0600006", "320")]["EHCPTotal"] == 200
    assert result.loc[("E0600009", "113")]["EHCPTotal"] == 500

    # Check that E1000096 (no match) remains NaN (using index match)
    assert np.isnan(result.loc[("E1000096", "815")]["EHCPTotal"])


def test_two_stage_join_success(la_data, sen_data_old_la_match):
    """A record failing stage 1 successfully joins in stage 2."""

    result = local_authority._join_sen_to_local_authority_data(
        la_data, sen_data_old_la_match
    )

    assert len(result) == len(la_data)

    assert result.loc[("E10000006", "915")]["EHCPTotal"] == 100

    # This only succeeds at join 2
    assert result.loc[("E1000096", "815")]["EHCPTotal"] == 400

    # Fails both
    assert np.isnan(result.loc[("E10000009", "855")]["EHCPTotal"])

    # 4. Check the index of the E1000096 record to ensure it maintained the original la_data index
    # (i.e., new_la_code is 'E1000096', not 'D99' from sen_data)
    assert "E1000096" in result.index.get_level_values("new_la_code")
    assert "D99" not in result.index.get_level_values("new_la_code")


def test_total_join_failure(la_data):
    """Test a scenario where the SEN data has no corresponding LA codes, failing both stages."""
    # Create an empty SEN data frame with the expected MultiIndex
    sen_data_empty = pd.DataFrame(
        columns=["EHCPTotal", "OtherCol2"],
        index=pd.MultiIndex.from_tuples([], names=["new_la_code", "old_la_code"]),
    )

    result = local_authority._join_sen_to_local_authority_data(la_data, sen_data_empty)

    assert len(result) == len(la_data)
    assert result["EHCPTotal"].isnull().all()
    assert "OtherCol2" in result.columns
