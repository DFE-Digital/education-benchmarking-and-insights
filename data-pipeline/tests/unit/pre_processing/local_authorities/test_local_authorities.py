import io

import numpy as np
import pandas as pd
import pytest

from pipeline import input_schemas
from pipeline.pre_processing.s251 import local_authority


def test_local_authorities(
    la_budget: pd.DataFrame,
    la_outturn: pd.DataFrame,
    la_statistical_neighbours_df: pd.DataFrame,
    la_ons_preprocessed: pd.DataFrame,
    la_sen2_preprocessed: pd.DataFrame,
    la_place_numbers: pd.DataFrame,
    la_dsg_preprocessed: pd.DataFrame,
    la_all_schools: pd.DataFrame,
):
    year = 2024

    result = local_authority.build_local_authorities(
        io.StringIO(la_budget.to_csv()),
        io.StringIO(la_outturn.to_csv(encoding="cp1252")),
        la_statistical_neighbours_df,
        la_ons_preprocessed,
        la_sen2_preprocessed,
        la_place_numbers,
        la_dsg_preprocessed,
        la_all_schools,
        year,
    )

    assert len(result.index) > 0
    assert result["LocalAuthorityAvgAttainment8"].isna().all()
    assert result["NationalAvgAttainment8"].isna().all()


@pytest.mark.parametrize(
    "la_code,expected_la_attainment",
    [
        (101, 46.2),
        (999, np.nan),
    ],
)
def test_local_authorities_with_ks4_data_populates_attainment8(
    la_budget: pd.DataFrame,
    la_outturn: pd.DataFrame,
    la_statistical_neighbours_df: pd.DataFrame,
    la_ons_preprocessed: pd.DataFrame,
    la_sen2_preprocessed: pd.DataFrame,
    la_place_numbers: pd.DataFrame,
    la_dsg_preprocessed: pd.DataFrame,
    la_all_schools: pd.DataFrame,
    la_code: int,
    expected_la_attainment: float,
):
    year = 2024
    la_attainment = pd.DataFrame(
        {"LocalAuthorityAvgAttainment8": [46.2]},
        index=pd.Index([la_code], name="LEA", dtype="Int64"),
    )
    national_attainment = 45.9

    result = local_authority.build_local_authorities(
        io.StringIO(la_budget.to_csv()),
        io.StringIO(la_outturn.to_csv(encoding="cp1252")),
        la_statistical_neighbours_df,
        la_ons_preprocessed,
        la_sen2_preprocessed,
        la_place_numbers,
        la_dsg_preprocessed,
        la_all_schools,
        year,
        ks4_la=(la_attainment, national_attainment),
    )

    assert result["LocalAuthorityAvgAttainment8"].iloc[0] == pytest.approx(
        expected_la_attainment, nan_ok=True
    )
    assert result["NationalAvgAttainment8"].iloc[0] == pytest.approx(
        national_attainment
    )


@pytest.fixture
def mock_schools_data() -> pd.DataFrame:
    """
    Creates a mock DataFrame covering various school types and federation scenarios.

    Expected Logic Check:
    - Row 3 (ID '002-C'): Should be EXCLUDED because it is a 'Maintained'
      school, has a lead code ('002-A'), and its own ID ('002-C') doesn't match the lead code.
    - All other rows should be INCLUDED.
    """
    data = [
        # Non federated school
        {
            "LA Code": "111",
            "EstablishmentNumber": "1234",
            "Finance Type": "Maintained",
            "Lead school in federation": "0",
            "Number of pupils": 100,
        },
        # Lead Federated School
        {
            "LA Code": "222",
            "EstablishmentNumber": "3333",
            "Finance Type": "Maintained",
            "Lead school in federation": "2223333",  # Lead school has its own code here
            "Number of pupils": 200,
        },
        # Non lead federated school to ignore
        {
            "LA Code": "222",
            "EstablishmentNumber": "8888",
            "Finance Type": "Maintained",
            "Lead school in federation": "2223333",
            "Number of pupils": 50,
        },
        # Academy
        {
            "LA Code": "555",
            "EstablishmentNumber": np.nan,
            "Finance Type": "Academy",
            "Lead school in federation": np.nan,
            "Number of pupils": 40,
        },
    ]
    df = pd.DataFrame(data)

    return df


def test_aggregate_fbit_pupil_numbers_to_la_level(mock_schools_data):
    expected_data = {
        "LA Code": ["111", "222", "555"],
        "Number of pupils": [100, 200, 40],
    }
    expected_df = pd.DataFrame(expected_data).set_index("LA Code")

    result_df = local_authority._aggregate_fbit_pupil_numbers_to_la_level(
        mock_schools_data.copy()
    )

    assert isinstance(result_df, pd.DataFrame)
    assert "Number of pupils" in result_df.columns
    assert result_df.index.name == "LA Code"

    pd.testing.assert_frame_equal(result_df, expected_df)


def test_calculate_dsg_recoupments_single_phase_schools():
    """
    Ensure single-phase schools (only primary or only secondary in an LA)
    do not cause NaN values to cascade into DSG funding totals.
    """
    year = 2024
    six_k_col, ten_k_col, post_16_col = input_schemas.get_six_and_ten_k_cols(year)

    # LA 101 only has a Primary school; LA 102 only has a Secondary school
    school_to_la = pd.DataFrame(
        {
            "LA": [101, 102],
            "Overall Phase": [input_schemas.primary, input_schemas.secondary],
        },
        index=pd.Index([1, 2], name="URN"),
    )

    place_numbers = pd.DataFrame(
        {
            "URN": [1, 2],
            six_k_col: [10, 5],
            ten_k_col: [2, 3],
            post_16_col: [0, 1],
        }
    )

    dsg = pd.DataFrame(
        {
            "DSGSENAcademyPlaceFunding": [1000.0, 2000.0],
            "DSGAPAcademyPlaceFunding": [500.0, 600.0],
            "DSGHospitalPlaceFunding": [100.0, 200.0],
            "Total Mainstream DSG deduction": [15000.0, 25000.0],
            "DSGHighNeedsAllocation": [100000.0, 200000.0],
        },
        index=pd.Index([101, 102], name="LA"),
    )

    la_data = pd.DataFrame(
        {
            "old_la_code": [101, 102],
            "OutturnPlaceFundingPrimary": [0.0, 0.0],
            "OutturnPlaceFundingSecondary": [0.0, 0.0],
            "OutturnPlaceFundingSpecial": [0.0, 0.0],
            "OutturnPlaceFundingAlternativeProvision": [0.0, 0.0],
            "OutturnTotalHospitalServices": [0.0, 0.0],
            "OutturnTotalPlaceFunding": [0.0, 0.0],
            "OutturnTotalHighNeeds": [0.0, 0.0],
        }
    )

    result = local_authority._calculate_dsg_recoupments(
        la_data, school_to_la, place_numbers, dsg, year
    )

    # Guard against nulls cascading into DSG totals
    assert not result["HighNeedsTotalPlaceFunding"].isna().any()
    assert not result["PrimaryPlaceFundingRatio"].isna().any()
    assert not result["SecondaryPlaceFundingRatio"].isna().any()
    assert not result["DSGPrimaryAcademyPlaceFunding"].isna().any()
    assert not result["DSGSecondaryAcademyPlaceFunding"].isna().any()
    assert not result["DSGRecoupmentServiceTotal"].isna().any()

    # Primary-only LA allocates all mainstream place funding to Primary
    primary_la = result.loc[result["old_la_code"] == 101].iloc[0]
    assert primary_la["HighNeedsTotalPlaceFunding"] == pytest.approx(80000.0)
    assert primary_la["PrimaryPlaceFundingRatio"] == pytest.approx(1.0)
    assert primary_la["SecondaryPlaceFundingRatio"] == pytest.approx(0.0)
    assert primary_la["DSGPrimaryAcademyPlaceFunding"] == pytest.approx(15000.0)
    assert primary_la["DSGSecondaryAcademyPlaceFunding"] == pytest.approx(0.0)
    assert primary_la["DSGRecoupmentServiceTotal"] == pytest.approx(16600.0)

    # Secondary-only LA allocates all mainstream place funding to Secondary
    secondary_la = result.loc[result["old_la_code"] == 102].iloc[0]
    assert secondary_la["HighNeedsTotalPlaceFunding"] == pytest.approx(70000.0)
    assert secondary_la["PrimaryPlaceFundingRatio"] == pytest.approx(0.0)
    assert secondary_la["SecondaryPlaceFundingRatio"] == pytest.approx(1.0)
    assert secondary_la["DSGPrimaryAcademyPlaceFunding"] == pytest.approx(0.0)
    assert secondary_la["DSGSecondaryAcademyPlaceFunding"] == pytest.approx(25000.0)
    assert secondary_la["DSGRecoupmentServiceTotal"] == pytest.approx(27800.0)
