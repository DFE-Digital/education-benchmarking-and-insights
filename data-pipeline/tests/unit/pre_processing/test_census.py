import io

import pandas as pd

from pipeline.pre_processing.ancillary.census import prepare_census_data


def test_census_data_has_correct_output_columns(prepared_census_data: pd.DataFrame):
    assert set(prepared_census_data.columns) == set(
        [
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
            "Total Number of Leadership Non-Teachers (Headcount)",
            "Total Number of Leadership Non-Teachers (FTE)",
            "Total School Workforce (Headcount)",
            "SeniorLeadershipHeadcount",
            "SeniorLeadershipFTE",
            "hc_head_teachers",
            "hc_deputy_head_teachers",
            "hc_assistant_head_teachers",
            "fte_head_teachers",
            "fte_deputy_head_teachers",
            "fte_assistant_head_teachers",
            "TotalPupilsNursery",
            "TotalPupilsSixthForm",
        ]
    )


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
    headteacher_workforce_census_data: pd.DataFrame,
    pupil_census_data: pd.DataFrame,
):
    """
    Missing rows from the pupil-census data should not result in
    missing rows from the final, merged dataset.
    """
    pupil_census_data = pupil_census_data[pupil_census_data["URN"] != 100153]
    pupil_csv = io.StringIO(pupil_census_data.to_csv())
    headteacher_csv = io.StringIO(headteacher_workforce_census_data.to_csv())

    output = io.BytesIO()
    writer = pd.ExcelWriter(output)
    workforce_census_data.to_excel(
        writer, startrow=5, sheet_name="Schools 2022", index=False
    )
    writer.close()
    output.seek(0)
    workforce_xlsx = output

    census = prepare_census_data(workforce_xlsx, headteacher_csv, pupil_csv, 2023)

    assert sorted(list(pupil_census_data["URN"])) == [100150, 100152]
    assert sorted(list(workforce_census_data["URN"])) == [100150, 100152, 100153]
    assert sorted(list(census.index)) == [100150, 100152, 100153]


def test_census_data_workforce_merge(
    workforce_census_data: pd.DataFrame,
    headteacher_workforce_census_data: pd.DataFrame,
    pupil_census_data: pd.DataFrame,
):
    """
    Missing rows from the workforce-census data should not result in
    missing rows from the final, merged dataset.
    """
    pupil_csv = io.StringIO(pupil_census_data.to_csv())
    headteacher_csv = io.StringIO(headteacher_workforce_census_data.to_csv())

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

    census = prepare_census_data(workforce_xlsx, headteacher_csv, pupil_csv, 2023)

    assert sorted(list(pupil_census_data["URN"])) == [100150, 100152, 100153]
    assert sorted(list(workforce_census_data["URN"])) == [100150, 100152]
    assert sorted(list(census.index)) == [100150, 100152, 100153]


def test_census_data_merge(
    workforce_census_data: pd.DataFrame,
    headteacher_workforce_census_data: pd.DataFrame,
    pupil_census_data: pd.DataFrame,
):
    """
    Missing rows from the either census data should not result in
    missing rows from the final, merged dataset.
    """
    pupil_census_data = pupil_census_data[pupil_census_data["URN"] != 100153]
    pupil_csv = io.StringIO(pupil_census_data.to_csv())
    headteacher_csv = io.StringIO(headteacher_workforce_census_data.to_csv())

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

    census = prepare_census_data(workforce_xlsx, headteacher_csv, pupil_csv, 2023)

    print(census)
    assert sorted(list(pupil_census_data["URN"])) == [100150, 100152]
    assert sorted(list(workforce_census_data["URN"])) == [100150, 100153]
    assert sorted(list(census.index)) == [100150, 100152, 100153]


def test_census_data_has_la_estab_for_2025(
    headteacher_workforce_census_data: pd.DataFrame,
):
    pupil_df = pd.DataFrame(
        {
            "urn": [100150, 100152, 100153],
            "laestab": [5, 6, 7],
            "number of pupils known to be eligible for free school meals": [10.0, 20.0, 30.0],
            "% of pupils known to be eligible for free school meals": [10.0, 20.0, 30.0],
            "number of pupils whose first language is known or believed to be other than English": [10.0, 20.0, 30.0],
            "% of pupils whose first language is known or believed to be other than English": [10.0, 20.0, 30.0],
            "headcount of pupils": [100, 200, 300],
            "total boarders": [0, 0, 0],
            "fte pupils": [100, 200, 300],
            "Number of early year pupils (years E1 and E2)": [0, 0, 0],
            "Number of nursery pupils (years N1 and N2)": [0, 0, 0],
            "full-time male Year group 12": [0, 0, 0],
            "full-time female Year group 12": [0, 0, 0],
            "full-time male Year group 13": [0, 0, 0],
            "full-time female Year group 13": [0, 0, 0],
            "number_of_dual_subsidiary_registrations": [0, 0, 0],
        }
    )

    workforce_df = pd.DataFrame(
        {
            "URN": [100150, 100152, 100158],
            "LAEstab": [5, 6, 99],
            "Total Number of Other School Support Staff (FTE)": [1.0, 1.0, 1.0],
            "Total Number of Other School Support Staff (Headcount)": [1, 1, 1],
            "Total Number of Technicians (FTE)": [1.0, 1.0, 1.0],
            "Total Number of Technicians (Headcount)": [1, 1, 1],
            "Total Number of Leadership Non-Teachers (FTE)": [1.0, 1.0, 1.0],
            "Total Number of Leadership Non-Teachers (Headcount)": [1, 1, 1],
            "Total Number of School Business Professionals (FTE)": [1.0, 1.0, 1.0],
            "Total Number of School Business Professionals (Headcount)": [1, 1, 1],
            "Total Number of Administrative Staff (FTE)": [1.0, 1.0, 1.0],
            "Total Number of Administrative Staff (Headcount)": [1, 1, 1],
            "Teachers with Qualified Teacher Status (%) (Headcount)": [100, 100, 100],
            "Total Number of Teaching Assistants (FTE)": [1.0, 1.0, 1.0],
            "Total Number of Teaching Assistants (Headcount)": [1, 1, 1],
            "Total School Workforce (FTE)": [10.0, 10.0, 10.0],
            "Total School Workforce (Headcount)": [10, 10, 10],
            "Total Number of Teachers (FTE)": [5.0, 5.0, 5.0],
            "Total Number of Teachers (Headcount)": [5, 5, 5],
            "Total Number of Teachers in the Leadership Group (FTE)": [1.0, 1.0, 1.0],
            "Total Number of Teachers in the Leadership Group (Headcount)": [1, 1, 1],
            "Total Number of Auxiliary Staff (FTE)": [1.0, 1.0, 1.0],
            "Total Number of Auxiliary Staff (Headcount)": [1, 1, 1],
        }
    )

    pupil_csv = io.StringIO(pupil_df.to_csv(index=False))
    headteacher_csv = io.StringIO(headteacher_workforce_census_data.to_csv(index=False))

    output = io.BytesIO()
    writer = pd.ExcelWriter(output)
    workforce_df.to_excel(
        writer, startrow=8, sheet_name="Schools 2025", index=False
    )
    writer.close()
    output.seek(0)
    workforce_xlsx = output

    census = prepare_census_data(workforce_xlsx, headteacher_csv, pupil_csv, 2025)

    assert "LAEstab" in census.columns
    assert census.loc[100150]["LAEstab"] == 5
    assert census.loc[100152]["LAEstab"] == 6
    assert census.loc[100153]["LAEstab"] == 7
    assert census.loc[100158]["LAEstab"] == 99

