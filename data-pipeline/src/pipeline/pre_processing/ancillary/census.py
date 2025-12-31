import logging

import pandas as pd

import pipeline.config as config
import pipeline.input_schemas as input_schemas

logger = logging.getLogger("fbit-data-pipeline")


# noinspection PyTypeChecker
def prepare_census_data(
    workforce_census_path,
    head_teacher_breakdowns_path,
    pupil_census_path,
    year: int,
) -> pd.DataFrame:
    """
    Prepare workforce- and pupil-census data.

    Note: either source may have orgs. present which the other lacks.
    In either case, all rows must be retained in the resulting, merged
    data.

    :param workforce_census_path: readable source for workforce census
    :param pupil_census_path: readable source for pupil census
    :param year: financial year in question
    """
    school_workforce_census = pd.read_excel(
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
    ).rename(
        columns=input_schemas.workforce_census_column_mappings.get(
            year, input_schemas.workforce_census_column_mappings["default"]
        ),
    )
    logger.info(
        f"School workforce census raw {year=} shape: {school_workforce_census.shape}"
    )
    school_workforce_census = (
        school_workforce_census.dropna(
            subset=[input_schemas.workforce_census_index_col]
        )
        .drop_duplicates()
        .set_index(input_schemas.workforce_census_index_col)
    )

    for column, eval_ in input_schemas.workforce_census_column_eval.get(
        year, input_schemas.workforce_census_column_eval["default"]
    ).items():
        school_workforce_census[column] = school_workforce_census.eval(eval_)

    school_pupil_census = pd.read_csv(
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
    ).rename(
        columns=input_schemas.pupil_census_column_mappings.get(
            year, input_schemas.pupil_census_column_mappings["default"]
        ),
    )
    logger.info(f"School pupil census raw {year=} shape: {school_pupil_census.shape}")
    school_pupil_census = (
        school_pupil_census.dropna(subset=[input_schemas.pupil_census_index_col])
        .drop_duplicates()
        .set_index(input_schemas.pupil_census_index_col)
    )

    school_pupil_census["Pupil Dual Registrations"] = school_pupil_census.get(
        "Pupil Dual Registrations", pd.Series(0, index=school_pupil_census.index)
    ).fillna(0)

    head_teacher_breakdowns = get_census_head_teacher_breakdowns(
        head_teacher_breakdowns_path, year=year
    )

    census = (
        school_pupil_census.join(
            school_workforce_census,
            how="outer",
            rsuffix="_pupil",
            lsuffix="_workforce",
        )
        .join(head_teacher_breakdowns, how="left")
        .rename(columns=config.census_column_map)
    )

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


def get_census_head_teacher_breakdowns(
    head_teacher_breakdowns_path,
    year: int,
) -> pd.DataFrame:
    head_teacher_breakdowns = pd.read_csv(
        head_teacher_breakdowns_path,
        usecols=input_schemas.head_teacher_breakdowns["default"].keys(),
        dtype=input_schemas.head_teacher_breakdowns["default"],
        encoding="latin-1",
        na_values=["x"],
    )

    academic_year_code = ((year - 1) * 100) + year % 100
    head_teacher_breakdowns_filtered = head_teacher_breakdowns[
        head_teacher_breakdowns["time_period"] == academic_year_code
    ]

    head_teacher_breakdowns_preprocessed = (
        head_teacher_breakdowns_filtered.drop(columns=["time_period"])
        .rename(columns={"school_urn": "URN"})
        .set_index("URN")
    )

    return head_teacher_breakdowns_preprocessed
