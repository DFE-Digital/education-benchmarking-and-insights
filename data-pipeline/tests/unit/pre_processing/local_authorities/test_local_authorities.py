import io

import numpy as np
import pandas as pd
import pytest

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
