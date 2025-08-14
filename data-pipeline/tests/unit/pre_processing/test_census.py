import io

import pandas as pd

from pipeline.pre_processing.ancillary.census import prepare_census_data


def test_census_data_has_correct_output_columns(prepared_census_data: pd.DataFrame):
    assert list(prepared_census_data.columns) == [
        "Percentage Free school meals",
        "Number of pupils (headcount)",
        "Number of pupils",
        "Number of early year pupils (years E1 and E2)",
        "Number of nursery pupils (years N1 and N2)",
        "Full time boys Year group 12",
        "Full time girls Year group 12",
        "Full time boys Year group 13",
        "Full time girls Year group 13",
        "Pupil Dual Registrations",
        "NonClassroomSupportStaffFTE",
        "NonClassroomSupportStaffHeadcount",
        "Teachers with Qualified Teacher Status (%) (Headcount)",
        "Total Number of Teaching Assistants (Full-Time Equivalent)",
        "Total Number of Teaching Assistants (Headcount)",
        "Total School Workforce (Full-Time Equivalent)",
        "Total Number of Teachers (Full-Time Equivalent)",
        "Total Number of Teachers (Headcount)",
        "Total Number of Teachers in the Leadership Group (Headcount)",
        "Total Number of Teachers in the Leadership Group (Full-time Equivalent)",
        "Total Number of Auxiliary Staff (Full-Time Equivalent)",
        "Total Number of Auxiliary Staff (Headcount)",
        "Total School Workforce (Headcount)",
        "TotalPupilsNursery",
        "TotalPupilsSixthForm",
    ]


def test_dual_pupils_handled(prepared_census_data: pd.DataFrame):
    assert prepared_census_data.loc[100152]["Number of pupils"] == 619


def test_dual_pupils_handled_when_zero(prepared_census_data: pd.DataFrame):
    assert prepared_census_data.loc[100150]["Number of pupils"] == 320


def test_total_nursery_computed_correctly(prepared_census_data: pd.DataFrame):
    assert prepared_census_data.loc[100150]["TotalPupilsNursery"] == 20


def test_total_sixth_form_computed_correctly(prepared_census_data: pd.DataFrame):
    assert prepared_census_data.loc[100150]["TotalPupilsSixthForm"] == 40


def test_census_data_pupil_merge(
    workforce_census_data: pd.DataFrame,
    pupil_census_data: pd.DataFrame,
):
    """
    Missing rows from the pupil-census data should not result in
    missing rows from the final, merged dataset.
    """
    pupil_census_data = pupil_census_data[pupil_census_data["URN"] != 100153]
    pupil_csv = io.StringIO(pupil_census_data.to_csv())

    output = io.BytesIO()
    writer = pd.ExcelWriter(output)
    workforce_census_data.to_excel(
        writer, startrow=5, sheet_name="Schools 2022", index=False
    )
    writer.close()
    output.seek(0)
    workforce_xlsx = output

    census = prepare_census_data(workforce_xlsx, pupil_csv, 2023)

    assert sorted(list(pupil_census_data["URN"])) == [100150, 100152]
    assert sorted(list(workforce_census_data["URN"])) == [100150, 100152, 100153]
    assert sorted(list(census.index)) == [100150, 100152, 100153]


def test_census_data_workforce_merge(
    workforce_census_data: pd.DataFrame,
    pupil_census_data: pd.DataFrame,
):
    """
    Missing rows from the workforce-census data should not result in
    missing rows from the final, merged dataset.
    """
    pupil_csv = io.StringIO(pupil_census_data.to_csv())

    output = io.BytesIO()
    writer = pd.ExcelWriter(output)
    workforce_census_data = workforce_census_data[
        workforce_census_data["URN"] != 100153
    ]
    workforce_census_data.to_excel(
        writer, startrow=5, sheet_name="Schools 2022", index=False
    )
    writer.close()
    output.seek(0)
    workforce_xlsx = output

    census = prepare_census_data(workforce_xlsx, pupil_csv, 2023)

    assert sorted(list(pupil_census_data["URN"])) == [100150, 100152, 100153]
    assert sorted(list(workforce_census_data["URN"])) == [100150, 100152]
    assert sorted(list(census.index)) == [100150, 100152, 100153]


def test_census_data_merge(
    workforce_census_data: pd.DataFrame,
    pupil_census_data: pd.DataFrame,
):
    """
    Missing rows from the either census data should not result in
    missing rows from the final, merged dataset.
    """
    pupil_census_data = pupil_census_data[pupil_census_data["URN"] != 100153]
    pupil_csv = io.StringIO(pupil_census_data.to_csv())

    output = io.BytesIO()
    writer = pd.ExcelWriter(output)
    workforce_census_data = workforce_census_data[
        workforce_census_data["URN"] != 100152
    ]
    workforce_census_data.to_excel(
        writer, startrow=5, sheet_name="Schools 2022", index=False
    )
    writer.close()
    output.seek(0)
    workforce_xlsx = output

    census = prepare_census_data(workforce_xlsx, pupil_csv, 2023)

    print(census)
    assert sorted(list(pupil_census_data["URN"])) == [100150, 100152]
    assert sorted(list(workforce_census_data["URN"])) == [100150, 100153]
    assert sorted(list(census.index)) == [100150, 100152, 100153]
