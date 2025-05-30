import pandas as pd

from pipeline import config


def _has_pupil_comparator_data(row: pd.Series) -> bool:
    """
    Whether the row is sufficiently populated.

    This follows the logic from
    `pipeline.comparator_sets.compute_pupils_comparator`. _All_
    required columns must be non-null.

    :param row: single academy/school data
    :return: whether the row is sufficiently populated
    """
    columns = [
        "Number of pupils",
        "Percentage Free school meals",
    ]

    if row["SchoolPhaseType"].lower() == "special":
        columns += [
            "Percentage Primary Need SPLD",
            "Percentage Primary Need MLD",
            "Percentage Primary Need PMLD",
            "Percentage Primary Need SEMH",
            "Percentage Primary Need SLCN",
            "Percentage Primary Need HI",
            "Percentage Primary Need MSI",
            "Percentage Primary Need PD",
            "Percentage Primary Need ASD",
            "Percentage Primary Need OTH",
        ]
    else:
        columns += ["Percentage SEN"]

    return ~row[columns].isna().any()


def map_has_pupil_comparator_data(df: pd.DataFrame) -> pd.DataFrame:
    """
    Whether the data is sufficiently populated to create a pupil
    comparator group.

    Specifically, this is the FSM and SEN data.

    :param df: academy/school data
    :return: updated DataFrame
    """
    df["Pupil Comparator Data Present"] = df.apply(_has_pupil_comparator_data, axis=1)

    return df


def map_has_building_comparator_data(
    maintained_schools: pd.DataFrame,
) -> pd.DataFrame:
    """
    Whether the maintained school data has all the necessary data to
    create a building comparator group.

    :param maintained_schools: maintained schools data
    :return: updated DataFrame
    """
    building_comparator_columns = ["Total Internal Floor Area", "Age Average Score"]

    maintained_schools["Building Comparator Data Present"] = (
        ~maintained_schools[building_comparator_columns].isna().any(axis=1)
    )

    return maintained_schools
