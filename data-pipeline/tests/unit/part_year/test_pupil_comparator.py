import numpy as np
import pandas as pd

from pipeline import part_year


def test_special_school_all_data_present():
    df = pd.DataFrame(
        {
            "SchoolPhaseType": ["Special"],
            "Number of pupils": [100],
            "Percentage Free school meals": [15.0],
            "Percentage Primary Need SPLD": [10.0],
            "Percentage Primary Need MLD": [5.0],
            "Percentage Primary Need PMLD": [2.0],
            "Percentage Primary Need SEMH": [3.0],
            "Percentage Primary Need SLCN": [4.0],
            "Percentage Primary Need HI": [1.0],
            "Percentage Primary Need MSI": [0.5],
            "Percentage Primary Need PD": [6.0],
            "Percentage Primary Need ASD": [8.0],
            "Percentage Primary Need OTH": [2.5],
            "Percentage SEN": [np.nan],
        },
        index=[0],
    )

    result = part_year.common.map_has_pupil_comparator_data(df)

    assert result["Pupil Comparator Data Present"].tolist() == [True]


def test_special_school_some_data_missing():
    df = pd.DataFrame(
        {
            "SchoolPhaseType": ["Special", "Special"],
            "Number of pupils": [100, 120],
            "Percentage Free school meals": [15.0, 20.0],
            "Percentage Primary Need SPLD": [10.0, np.nan],
            "Percentage Primary Need MLD": [5.0, 6.0],
            "Percentage Primary Need PMLD": [2.0, 2.5],
            "Percentage Primary Need SEMH": [3.0, 3.5],
            "Percentage Primary Need SLCN": [4.0, 4.5],
            "Percentage Primary Need HI": [1.0, 1.5],
            "Percentage Primary Need MSI": [0.5, 0.7],
            "Percentage Primary Need PD": [6.0, 6.5],
            "Percentage Primary Need ASD": [8.0, 8.5],
            "Percentage Primary Need OTH": [2.5, np.nan],
        },
        index=[0, 1],
    )

    result = part_year.common.map_has_pupil_comparator_data(df)

    assert result["Pupil Comparator Data Present"].tolist() == [True, False]


def test_special_school_all_required_data_missing():
    df = pd.DataFrame(
        {
            "SchoolPhaseType": ["Special"],
            "Number of pupils": [np.nan],
            "Percentage Free school meals": [np.nan],
            "Percentage Primary Need SPLD": [np.nan],
            "Percentage Primary Need MLD": [np.nan],
            "Percentage Primary Need PMLD": [np.nan],
            "Percentage Primary Need SEMH": [np.nan],
            "Percentage Primary Need SLCN": [np.nan],
            "Percentage Primary Need HI": [np.nan],
            "Percentage Primary Need MSI": [np.nan],
            "Percentage Primary Need PD": [np.nan],
            "Percentage Primary Need ASD": [np.nan],
            "Percentage Primary Need OTH": [np.nan],
        },
        index=[0],
    )

    result = part_year.common.map_has_pupil_comparator_data(df)

    assert result["Pupil Comparator Data Present"].tolist() == [False]


def test_non_special_school_all_data_present():
    df = pd.DataFrame(
        {
            "SchoolPhaseType": ["Primary", "Secondary"],
            "Number of pupils": [50, 200],
            "Percentage Free school meals": [10.0, 25.0],
            "Percentage SEN": [3.0, 7.0],
            "Percentage Primary Need SPLD": [np.nan, 1.0],
            "Percentage Primary Need MLD": [2.0, np.nan],
        },
        index=[0, 1],
    )

    result = part_year.common.map_has_pupil_comparator_data(df)

    assert result["Pupil Comparator Data Present"].tolist() == [True, True]


def test_non_special_school_some_data_missing():
    df = pd.DataFrame(
        {
            "SchoolPhaseType": ["Primary", "Secondary"],
            "Number of pupils": [50, np.nan],
            "Percentage Free school meals": [10.0, 25.0],
            "Percentage SEN": [np.nan, 7.0],
        },
        index=[0, 1],
    )

    result = part_year.common.map_has_pupil_comparator_data(df)

    assert result["Pupil Comparator Data Present"].tolist() == [False, False]
