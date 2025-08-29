import string

import numpy as np
import pandas as pd
import pytest

from pipeline.comparator_sets.calculations import ComparatorCalculator, prepare_data
from pipeline.comparator_sets.config import ColumnNames

sample_data_length = 26
half_sample_data_length = int(sample_data_length / 2)


@pytest.fixture
def sample_data() -> pd.DataFrame:
    """Provides a base DataFrame for testing."""
    data = {
        ColumnNames.URN: [f"URN{i}" for i in range(sample_data_length)],
        ColumnNames.PUPILS: [100 + i * 10 for i in range(sample_data_length)],
        ColumnNames.FSM: [5 + i * 0.5 for i in range(sample_data_length)],
        ColumnNames.SEN: [2 + i * 0.2 for i in range(sample_data_length)],
        ColumnNames.BOARDERS: ["Boarding", "Not Boarding"] * half_sample_data_length,
        ColumnNames.PFI: [True, False] * half_sample_data_length,
        ColumnNames.REGION: ["Region A"] * half_sample_data_length
        + ["Region B"] * half_sample_data_length,
        ColumnNames.GIFA: [1000 + i * 50 for i in range(sample_data_length)],
        ColumnNames.AGE_SCORE: [20 + i for i in range(sample_data_length)],
        ColumnNames.PHASE: ["Primary"] * sample_data_length,
        ColumnNames.DID_NOT_SUBMIT: [False] * sample_data_length,
        ColumnNames.PARTIAL_YEARS: [False] * sample_data_length,
        ColumnNames.FINANCIAL_DATA: [True] * sample_data_length,
        ColumnNames.PUPIL_DATA: [True] * sample_data_length,
        ColumnNames.BUILDING_DATA: [True] * sample_data_length,
    }
    for col in ColumnNames.SEN_NEEDS:
        data[col] = [1 + i * 0.1 for i in range(sample_data_length)]

    df = pd.DataFrame(data)
    df = df.set_index(ColumnNames.URN)
    return df


@pytest.fixture
def calculator(sample_data: pd.DataFrame) -> ComparatorCalculator:
    """Provides an initialized ComparatorCalculator instance."""
    prepared_df = prepare_data(sample_data)
    return ComparatorCalculator(prepared_data=prepared_df)


@pytest.fixture
def top_urns_phase_arrays() -> dict:
    """
    Default phase_arrays dictionary for testing `_select_top_urns`.
    Corresponds to an array of 26 schools, URNs "A" through "Z".
    """
    return {
        ColumnNames.URN: np.array(list(string.ascii_uppercase)),
        ColumnNames.PFI: np.array([True] * 26),
        ColumnNames.BOARDERS: np.array(["Boarding"] * 26),
        ColumnNames.REGION: np.array(["A"] * 26),
        "include_mask": np.array([True] * 26),
    }
