import pandas as pd
import pytest

from pipeline.pre_processing.ancillary import ilr

# TODO: these do not factor into comparator-sets and should be removed.
_extraneous_columns = [
    "NonClassroomSupportStaffFTE",
    "NonClassroomSupportStaffHeadcount",
    "Percentage Primary Need SLD",
    "Percentage Primary Need SPLD",
    "Percentage Primary Need VI",
    "Percentage Primary Need PD",
    "EHC plan",
    "Percentage Primary Need SLCN",
    "Percentage Primary Need OTH",
    "Percentage with EHC",
    "Percentage Primary Need ASD",
    "Percentage Primary Need SEMH",
    "Percentage without EHC",
    "Percentage Primary Need PMLD",
    "Percentage Primary Need HI",
    "Percentage Primary Need MLD",
    "Number of pupils (headcount)",
    "Percentage Primary Need MSI",
    "SEN support",
]


def _add_extraneous_columns(count: int) -> dict:
    return {column: [pd.NA] * count for column in _extraneous_columns}


@pytest.fixture
def df_with_missing():
    return pd.DataFrame(
        {
            "SchoolPhaseType": [
                "Primary",
                "Post-16",
                "Secondary",
                "University Technical College",
                "Post-16",
                "Primary",
            ],
            "Number of pupils": [100, pd.NA, 500, pd.NA, 200, 300],
            "Percentage Free school meals": [10.5, pd.NA, 15.0, 20.0, pd.NA, 5.0],
            "Percentage SEN": [2.0, 3.5, pd.NA, pd.NA, 4.0, 1.0],
        }
        | _add_extraneous_columns(6),
        index=pd.Index([101, 102, 103, 104, 105, 106], name="URN"),
    )


@pytest.fixture
def ilr_data():
    return pd.DataFrame(
        {
            "URN": [102, 104, 105, 999],
            "SchoolPhaseType": [
                "Post-16",
                "University Technical College",
                "Post-16",
                "Post-16",
            ],
            "Number of pupils": [150, 250, 210, 180],
            "Percentage Free school meals": [12.0, 18.0, 22.0, 10.0],
            "Percentage SEN": [2.5, 3.0, 4.5, 5.0],
        }
        | _add_extraneous_columns(4)
    )


@pytest.fixture
def gias_links_empty():
    return pd.DataFrame(columns=["URN", "LinkURN"])


def test_no_missing_sixth_form_data(df_with_missing, ilr_data, gias_links_empty):
    df_no_missing = df_with_missing.copy()
    df_no_missing.loc[
        102, ["Number of pupils", "Percentage Free school meals", "Percentage SEN"]
    ] = [150, 12.0, 2.5]
    df_no_missing.loc[
        104, ["Number of pupils", "Percentage Free school meals", "Percentage SEN"]
    ] = [250, 18.0, 3.0]
    df_no_missing.loc[
        105, ["Number of pupils", "Percentage Free school meals", "Percentage SEN"]
    ] = [210, 22.0, 4.5]

    initial = df_no_missing.copy()
    result = ilr.patch_missing_sixth_form_data(initial, ilr_data, gias_links_empty)

    pd.testing.assert_frame_equal(result[initial.columns], initial)


def test_patching_missing_number_of_pupils(df_with_missing, ilr_data, gias_links_empty):
    df = df_with_missing.copy()

    expected = df_with_missing.copy()
    expected.loc[102, "Number of pupils"] = 150
    expected.loc[102, "Percentage Free school meals"] = 12.0
    expected.loc[104, "Number of pupils"] = 250
    expected.loc[104, "Percentage SEN"] = 3.0
    expected.loc[105, "Percentage Free school meals"] = 22.0

    result = ilr.patch_missing_sixth_form_data(df, ilr_data, gias_links_empty)

    assert result.loc[102, "Number of pupils"] == 150
    assert result.loc[104, "Number of pupils"] == 250
    assert result.loc[104, "Percentage SEN"] == 3.0
    assert result.loc[105, "Percentage Free school meals"] == 22.0

    assert result.loc[101, "Number of pupils"] == 100
    assert pd.isna(result.loc[103, "Percentage SEN"])

    pd.testing.assert_frame_equal(result[expected.columns], expected)


def test_ilr_data_not_affecting_non_sixth_form_schools(
    df_with_missing, ilr_data, gias_links_empty
):
    df = df_with_missing.copy()
    initial = df_with_missing.copy()

    result = ilr.patch_missing_sixth_form_data(df, ilr_data, gias_links_empty)

    pd.testing.assert_series_equal(result.loc[101][initial.columns], initial.loc[101])
    pd.testing.assert_series_equal(result.loc[106][initial.columns], initial.loc[106])
    pd.testing.assert_series_equal(result.loc[103][initial.columns], initial.loc[103])


def test_no_ilr_match_for_missing_data(df_with_missing, ilr_data, gias_links_empty):
    df = df_with_missing.copy()
    empty_ilr = pd.DataFrame(columns=ilr_data.columns)  # Use fixture to get columns
    initial = df.copy()

    result = ilr.patch_missing_sixth_form_data(df, empty_ilr, gias_links_empty)

    assert pd.isna(result.loc[102, "Number of pupils"])
    assert pd.isna(result.loc[104, "Number of pupils"])
    assert pd.isna(result.loc[105, "Percentage Free school meals"])

    pd.testing.assert_frame_equal(result[initial.columns], initial)
