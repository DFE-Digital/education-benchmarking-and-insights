import pandas as pd


def test_census_data_has_correct_output_columns(prepared_census_data: pd.DataFrame):
    assert list(prepared_census_data.columns) == [
        "Percentage claiming Free school meals",
        "Percentage Free school meals",
        "Number of pupils",
        "Number of Pupils (FTE)",
        "ward_name",
        "district_administrative_name",
        "region_name",
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
    assert prepared_census_data.loc[100152]["Number of pupils"] == 628


def test_dual_pupils_handled_when_z(prepared_census_data: pd.DataFrame):
    assert prepared_census_data.loc[100150]["Number of pupils"] == 325


def test_total_nursery_computed_correctly(prepared_census_data: pd.DataFrame):
    assert prepared_census_data.loc[100150]["TotalPupilsNursery"] == 20


def test_total_sixth_form_computed_correctly(prepared_census_data: pd.DataFrame):
    assert prepared_census_data.loc[100150]["TotalPupilsSixthForm"] == 40
