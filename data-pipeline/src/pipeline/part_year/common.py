import pandas as pd

from src.pipeline import config


def map_has_pupil_comparator_data(df: pd.DataFrame) -> pd.DataFrame:
    """
    Whether the data is sufficiently populated to create a pupil
    comparator group.

    Specifically, this is the FSM and SEN data.

    :param df: academy/school data
    :return: updated DataFrame
    """
    pupil_comparator_columns = list(
        set(list(config.census_column_map.values()) + config.sen_generated_columns)
    )

    df["Pupil Comparator Data Present"] = (
        ~df[pupil_comparator_columns].isna().all(axis=1)
    )

    return df


def map_has_building_comparator_data(
    maintained_schools: pd.DataFrame,
) -> pd.DataFrame:
    """
    Whether the maintained school data has all the necessary data to
    create a building comparator group.

    Specifically, this is the CDC data.

    :param maintained_schools: maintained schools data
    :return: updated DataFrame
    """
    building_comparator_columns = config.cdc_generated_columns

    maintained_schools["Building Comparator Data Present"] = (
        ~maintained_schools[building_comparator_columns].isna().all(axis=1)
    )

    return maintained_schools
