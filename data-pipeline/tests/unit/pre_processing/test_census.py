import pandas as pd


def test_census_data_has_correct_output_columns(prepared_census_data: pd.DataFrame):
    assert list(prepared_census_data.columns) == [
        'Percentage claiming Free school meals',
        'Percentage Free school meals',
        'number of pupils whose first language is known or believed to be other than '
        'English',
        'full time pupils',
        'ward_name',
        'district_administrative_name',
        'region_name',
        'Number of Vacant Teacher Posts',
        'Pupil: Teacher Ratio (Full-Time Equivalent of qualified and unqualified '
        'teachers)',
        'FullTimeOther',
        'FullTimeOtherHeadCount',
        'Teachers with Qualified Teacher Status (%) (Headcount)',
        'Total Number of Teaching Assistants (Full-Time Equivalent)',
        'Total Number of Teaching Assistants (Headcount)',
        'Total School Workforce (Full-Time Equivalent)',
        'Total Number of Teachers (Full-Time Equivalent)',
        'Total Number of Teachers (Headcount)',
        'Total Number of Teachers in the Leadership Group (Headcount)',
        'Total Number of Teachers in the Leadership Group (Full-time Equivalent)',
        'Total Number of Auxiliary Staff (Full-Time Equivalent)',
        'Total Number of Auxiliary Staff (Headcount)',
        'Total School Workforce (Headcount)'
    ]