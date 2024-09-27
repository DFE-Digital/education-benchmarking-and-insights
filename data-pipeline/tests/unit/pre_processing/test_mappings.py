import datetime

import pandas as pd
import pytest

import src.pipeline.mappings as mappings


@pytest.mark.parametrize(
    "rating,expected",
    [
        ("Good", "Good"),
        ("Adequate", "Adequate"),
        ("Serious Weaknesses", "Serious weaknesses"),
        ("Special Measures", "Special measures"),
    ],
)
def test_ofsted_rating(rating, expected):
    assert mappings.map_ofsted_rating(rating) == expected


@pytest.mark.parametrize(
    "phase_code,establishment_code,expected",
    [
        (0, 6, "University Technical College"),
        (0, 7, "Special"),
        (0, 12, "Special"),
        (0, 14, "Pupil Referral Unit"),
        (0, 33, "Special"),
        (0, 36, "Special"),
        (0, 38, "Alternative Provision"),
        (0, 42, "Alternative Provision"),
        (0, 43, "Alternative Provision"),
        (0, 44, "Special"),
        (0, 1_000, "Unknown"),
        (1, 1_000, "Nursery"),
        (2, 1_000, "Primary"),
        (3, 1_000, "Primary"),
        (4, 40, "University Technical College"),
        (4, 1_000, "Secondary"),
        (5, 40, "University Technical College"),
        (5, 1_000, "Secondary"),
        (6, 1_000, "Post-16"),
        (7, 1_000, "All-through"),
        (8, 1_000, "Unknown"),
    ],
)
def test_map_phase_type(phase_code, establishment_code, expected):
    assert mappings.map_phase_type(establishment_code, phase_code) == expected


@pytest.mark.parametrize(
    "block_age,expected",
    [("", None), ("pre 1900", 1880), ("1990-2000", 1995), ("2000-2010", 2005)],
)
def test_map_block_age(block_age, expected):
    assert mappings.map_block_age(block_age) == expected


@pytest.mark.parametrize(
    "boarder,expected",
    [
        ("Boarding school", "Boarding"),
        ("Children's Home (Boarding school)", "Boarding"),
        ("College / FE residential accommodation", "Boarding"),
        ("No Boarders", "Not Boarding"),
        ("Not Applicable", "Not Boarding"),
        ("Other", "Unknown"),
    ],
)
def test_map_boarders(boarder, expected):
    assert mappings.map_boarders(boarder) == expected


@pytest.mark.parametrize(
    "sixth_form,expected",
    [
        ("Has a sixth form", "Has a sixth form"),
        ("Does not have a sixth form", "No sixth form"),
        ("Other", "Unknown"),
    ],
)
def test_map_sixth_form(sixth_form, expected):
    assert mappings.map_sixth_form(sixth_form) == expected


@pytest.mark.parametrize(
    "sixth_form,expected",
    [("Has a sixth form", True), ("No sixth form", False), ("Other", False)],
)
def test_map_sixth_form(sixth_form, expected):
    assert mappings.map_has_sixth_form(sixth_form) == expected


@pytest.mark.parametrize(
    "sixth_form,expected",
    [
        ("Has a sixth form", "Has a sixth form"),
        ("Does not have a sixth form", "No sixth form"),
        ("Other", "Unknown"),
    ],
)
def test_map_has_sixth_form(sixth_form, expected):
    assert mappings.map_sixth_form(sixth_form) == expected


@pytest.mark.parametrize(
    "nursery,provision,expected",
    [
        ("Has nursery classes", "Primary", "Has Nursery classes"),
        (" ", "Primary", "Unknown"),
        ("Other", "Secondary", "No Nursery classes"),
    ],
)
def test_map_nursery(nursery, provision, expected):
    assert mappings.map_nursery(nursery, provision) == expected


@pytest.mark.parametrize(
    "nursery,expected",
    [("Has nursery classes", True), ("No nursery classes", False), ("Other", False)],
)
def test_map_has_nursery(nursery, expected):
    assert mappings.map_has_nursery(nursery) == expected


@pytest.mark.parametrize(
    "admission_policy,expected",
    [
        ("Selective", "Selective"),
        ("Non-Selective", "HI selective"),
        ("Not applicable", "HI selective"),
        ("Other", "Unknown"),
    ],
)
def test_map_admission_policy(admission_policy, expected):
    assert mappings.map_admission_policy(admission_policy) == expected


@pytest.mark.parametrize(
    "opened_in_period,closed_in_period,valid_to,start_date,closed_date,period_start,year_start_date,year_end_date,expected",
    [
        (
            pd.NA,
            pd.NA,
            pd.NA,
            pd.NA,
            pd.NA,
            "10/09/2019",
            "01/09/2019",
            "30/08/2020",
            None,
        ),
        (
            pd.NA,
            pd.NA,
            pd.NA,
            "01/06/2018",
            "01/08/2019",
            "10/09/2019",
            "01/09/2019",
            "30/08/2020",
            "Closed",
        ),
        (
            pd.NA,
            pd.NA,
            pd.NA,
            "01/06/2018",
            "30/08/2020",
            "10/09/2019",
            "01/09/2019",
            "30/08/2020",
            "Closed in period",
        ),
        (
            pd.NA,
            pd.NA,
            pd.NA,
            "01/06/2018",
            "01/11/2019",
            "10/09/2019",
            "01/09/2019",
            "30/08/2020",
            "Closed in period",
        ),
        (
            pd.NA,
            pd.NA,
            pd.NA,
            "20/09/2019",
            pd.NA,
            "10/09/2019",
            "01/09/2019",
            "30/08/2020",
            "(Re)opened in period",
        ),
        (
            "25/09/2019",
            pd.NA,
            pd.NA,
            "01/06/2018",
            pd.NA,
            "10/09/2019",
            "01/09/2019",
            "30/08/2020",
            "(Re)opened in period",
        ),
        (
            pd.NA,
            pd.NA,
            pd.NA,
            "01/06/2018",
            pd.NA,
            "10/09/2019",
            "01/09/2019",
            "30/08/2020",
            "Open",
        ),
        (
            pd.NA,
            pd.NA,
            "05/09/2019",
            "01/06/2018",
            pd.NA,
            "10/09/2019",
            "01/09/2019",
            "30/08/2020",
            "Invalid",
        ),
    ],
)
def test_map_academy_status(
    opened_in_period,
    closed_in_period,
    valid_to,
    start_date,
    closed_date,
    period_start,
    year_start_date,
    year_end_date,
    expected,
):
    assert (
        mappings.map_academy_status(
            pd.to_datetime(opened_in_period, dayfirst=True),
            pd.to_datetime(closed_in_period, dayfirst=True),
            pd.to_datetime(valid_to, dayfirst=True),
            pd.to_datetime(start_date, dayfirst=True),
            pd.to_datetime(closed_date, dayfirst=True),
            pd.to_datetime(period_start, dayfirst=True),
            pd.to_datetime(year_start_date, dayfirst=True),
            pd.to_datetime(year_end_date, dayfirst=True),
        )
        == expected
    )


@pytest.mark.parametrize(
    "start_date,closed_date,return_period_length,year_start_date,year_end_date,expected",
    [
        (pd.NA, pd.NA, pd.NA, "01/04/2019", "31/03/2020", None),
        ("01/05/2018", pd.NA, 12, "01/04/2019", "31/03/2020", "Open"),
        ("01/05/2018", "01/11/2018", 12, "01/04/2019", "31/03/2020", "Closed"),
        ("01/05/2018", "01/05/2019", 1, "01/04/2019", "31/03/2020", "Closed in period"),
        ("01/05/2018", pd.NA, 1, "01/04/2019", "31/03/2020", "Closed in period"),
        ("01/05/2019", pd.NA, 12, "01/04/2019", "31/03/2020", "Open in period"),
    ],
)
def test_map_maintained_school_status(
    start_date,
    closed_date,
    return_period_length: int,
    year_start_date,
    year_end_date,
    expected,
):
    assert (
        mappings.map_maintained_school_status(
            pd.to_datetime(start_date),
            pd.to_datetime(closed_date),
            return_period_length,
            pd.to_datetime(year_start_date),
            pd.to_datetime(year_end_date),
        )
        == expected
    )


@pytest.mark.parametrize(
    "url,expected",
    [
        (None, None),
        ("", None),
        ("https://www.school.org.uk", "https://www.school.org.uk"),
        ("http://www.school.org.uk", "http://www.school.org.uk"),
        ("www.school.org.uk", "https://www.school.org.uk"),
    ],
)
def test_map_school_website(url, expected):
    assert mappings.map_school_website(url) == expected


@pytest.mark.parametrize(
    "pfi,expected",
    [
        (1, "PFI School"),
        (0, "Non-PFI school"),
        (-1, "PFI School"),
        (None, "Non-PFI school"),
    ],
)
def test_map_pfi(pfi, expected):
    assert mappings.map_is_pfi_school(pfi) == expected


@pytest.mark.parametrize(
    "value,expected",
    [(pd.NA, "Unknown"), (-0.1, "Deficit"), (0.0, "Surplus"), (1.0, "Surplus")],
)
def test_map_is_surplus_deficit(value, expected):
    assert mappings.map_is_surplus_deficit(value) == expected


@pytest.mark.parametrize(
    "company_number,expected",
    [("8146633", "08146633"), ("6633", "00006633"), ("18168237", "18168237")],
)
def test_map_company_number(company_number: str, expected: str):
    assert mappings.map_company_number(company_number) == expected
