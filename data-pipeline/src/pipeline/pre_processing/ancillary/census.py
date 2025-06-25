import pandas as pd

import pipeline.config as config
import pipeline.input_schemas as input_schemas
from pipeline.log import setup_logger

logger = setup_logger("fbit-data-pipeline")

# noinspection PyTypeChecker
def prepare_census_data(
    workforce_census_path,
    pupil_census_path,
    year: int,
):
    """
    Prepare workforce- and pupil-census data.

    Note: either source may have orgs. present which the other lacks.
    In either case, all rows must be retained in the resulting, merged
    data.

    :param workforce_census_path: readable source for workforce census
    :param pupil_census_path: readable source for pupil census
    :param year: financial year in question
    """
    school_workforce_census = (
        pd.read_excel(
            workforce_census_path,
            header=input_schemas.workforce_census_header_row.get(
                year, input_schemas.workforce_census_header_row["default"]
            ),
            usecols=input_schemas.workforce_census.get(
                year, input_schemas.workforce_census["default"]
            ).keys(),
            dtype=input_schemas.workforce_census.get(
                year, input_schemas.workforce_census["default"]
            ),
            na_values=["x", "u", "c", "z", ":"],
            keep_default_na=True,
            engine="calamine",
        )
        .rename(
            columns=input_schemas.workforce_census_column_mappings.get(
                year, input_schemas.workforce_census_column_mappings["default"]
            ),
        )
    )
    logger.info(f"School workforce census raw {year=} shape: {school_workforce_census.shape}")
    school_workforce_census = (
        school_workforce_census
        .dropna(subset=[input_schemas.workforce_census_index_col])
        .drop_duplicates()
        .set_index(input_schemas.workforce_census_index_col)
    )

    for column, eval_ in input_schemas.workforce_census_column_eval.get(
        year, input_schemas.workforce_census_column_eval["default"]
    ).items():
        school_workforce_census[column] = school_workforce_census.eval(eval_)

    school_pupil_census = (
        pd.read_csv(
            pupil_census_path,
            encoding="cp1252",
            usecols=input_schemas.pupil_census.get(
                year, input_schemas.pupil_census["default"]
            ).keys(),
            dtype=input_schemas.pupil_census.get(
                year, input_schemas.pupil_census["default"]
            ),
            na_values=["x", "u", "c", "z"],
            keep_default_na=True,
        )
        .rename(
            columns=input_schemas.pupil_census_column_mappings.get(
                year, input_schemas.pupil_census_column_mappings["default"]
            ),
        )
    )
    logger.info(f"School pupil census raw {year=} shape: {school_pupil_census.shape}")
    school_pupil_census = (
        school_pupil_census
        .dropna(subset=[input_schemas.pupil_census_index_col])
        .drop_duplicates()
        .set_index(input_schemas.pupil_census_index_col)
    )

    school_pupil_census["Pupil Dual Registrations"] = school_pupil_census.get(
        "Pupil Dual Registrations", pd.Series(0, index=school_pupil_census.index)
    ).fillna(0)

    census = school_pupil_census.join(
        school_workforce_census,
        how="outer",
        rsuffix="_pupil",
        lsuffix="_workforce",
    ).rename(columns=config.census_column_map)

    census["Number of pupils"] = (
        census["Number of pupils"] + census["Pupil Dual Registrations"]
    )

    census["TotalPupilsNursery"] = (
        census["Number of early year pupils (years E1 and E2)"]
        + census["Number of nursery pupils (years N1 and N2)"]
    )

    census["TotalPupilsSixthForm"] = (
        census["Full time boys Year group 12"]
        + census["Full time boys Year group 13"]
        + census["Full time girls Year group 12"]
        + census["Full time girls Year group 13"]
    )

    return census
