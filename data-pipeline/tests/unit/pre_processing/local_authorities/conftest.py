import io
import random

import numpy as np
import pandas as pd
import pytest

from pipeline import input_schemas
from pipeline.pre_processing.ancillary.dsg import prepare_dsg_data
from pipeline.pre_processing.ancillary.ons_population_estimates import (
    prepare_ons_population_estimates,
)
from pipeline.pre_processing.ancillary.sen2 import prepare_sen2_data


@pytest.fixture
def la_all_schools() -> pd.DataFrame:
    return pd.DataFrame(
        data={
            "LA Code": [101, 101, 102, 102, 102, 103],
            "LA": [101, 101, 102, 102, 102, 103],
            "URN": [1, 2, 3, 4, 5, 6],
            "Number of pupils": [200.02, 1.345, 54.0, 2005.7, 3007.9, 12],
            "Finance Type": [
                "Maintained",
                "Maintained",
                "Academy",
                "Academy",
                "Academy",
                "Academy",
            ],
            "Lead school in federation": [
                "1011234",
                "1016736",
                "1026671",
                "1021111",
                "1021253",
                "1032813",
            ],
            "EstablishmentNumber": ["1234", "7456", "5522", "1993", "5623", "9978"],
            "Overall Phase": [
                input_schemas.primary,
                input_schemas.secondary,
                input_schemas.primary,
                input_schemas.secondary,
                input_schemas.primary,
                "AP",
            ],
        }
    )


@pytest.fixture
def la_budget() -> pd.DataFrame:
    """
    Local Authority budget data.
    """
    category_of_planned_expenditure = [
        "1.0.2 High needs place funding within Individual Schools Budget, including all pre- and post-16 place funding for maintained schools and academies",
        "1.2.1 Top-up funding – maintained schools",
        "1.2.2 Top-up funding – academies, free schools and colleges",
        "1.2.3 Top-up and other funding – non-maintained and independent providers",
        "1.2.4 Additional high needs targeted funding for mainstream schools and academies",
        "1.2.5 SEN support service",
        "1.2.6 Hospital education services",
        "1.2.7 Other alternative provision services",
        "1.2.8 Support for inclusion",
        "1.2.9 Special schools and PRUs in financial difficulty",
        "1.2.10 PFI/ BSF costs at special schools, AP/ PRUs and Post 16 institutions only",
        "1.2.11 Direct payments (SEN and disability)",
        "1.2.13 Therapies and other health related services",
    ]

    def _get_str(value: str) -> list[str]:
        return [value] * len(category_of_planned_expenditure)

    def _get_float() -> list[float]:
        return [
            random.uniform(0.0, 1000.0)
            for _ in range(len(category_of_planned_expenditure))
        ]

    la_ex = {
        "time_period": _get_str("202324"),
        "time_identifier": _get_str("Financial year"),
        "geographic_level": _get_str("Local authority"),
        "country_name": _get_str("England"),
        "country_code": _get_str("E92000001"),
        "region_name": _get_str("Yorkshire and The Humber"),
        "region_code": _get_str("E13000001"),
        "la_name": _get_str("West Yorkshire"),
        "old_la_code": _get_str("101"),
        "new_la_code": _get_str("E10000000"),
        "main_category_planned_expenditure": _get_str("Section A: Schools"),
        "category_of_planned_expenditure": category_of_planned_expenditure,
        "early_years_establishments": _get_float(),
        "primary_schools": _get_float(),
        "secondary_schools": _get_float(),
        "sen_and_special_schools": _get_float(),
        "pupil_referral_units_and_alt_provision": _get_float(),
        "post_16": _get_float(),
        "gross_planned_expenditure": _get_float(),
        "income": _get_float(),
        "net_planned_expenditure": _get_float(),
        "net_per_capita_planned_expenditure": _get_float(),
    }

    return pd.DataFrame(la_ex)


@pytest.fixture
def la_outturn() -> pd.DataFrame:
    """
    Local Authority outturn data.
    """
    category_of_expenditure = [
        "1.0.2 High needs place funding within Individual Schools Budget",
        "1.2.1 Top-up funding – maintained schools",
        "1.2.2 Top-up funding – academies, free schools and colleges",
        "1.2.3 Top-up and other funding – non-maintained and independent providers",
        "1.2.4 Additional high needs targeted funding for mainstream schools and academies",
        "1.2.5 SEN support service",
        "1.2.6 Hospital education services",
        "1.2.7 Other alternative provision services",
        "1.2.8 Support for inclusion",
        "1.2.9 Special schools and PRUs in financial difficulty",
        "1.2.10 PFI/ BSF costs at special schools, AP/ PRUs and Post 16 institutions only",
        "1.2.11 Direct payments (SEN and disability)",
        "1.2.13 Therapies and other health related services",
    ]

    def _get_str(value: str) -> list[str]:
        return [value] * len(category_of_expenditure)

    def _get_float() -> list[float]:
        return [
            random.uniform(0.0, 1000.0) for _ in range(len(category_of_expenditure))
        ]

    outturn = {
        "time_period": _get_str("202324"),
        "time_identifier": _get_str("Financial year"),
        "geographic_level": _get_str("Local authority"),
        "country_name": _get_str("England"),
        "country_code": _get_str("E92000001"),
        "region_name": _get_str("Yorkshire and The Humber"),
        "region_code": _get_str("E13000001"),
        "la_name": _get_str("West Yorkshire"),
        "old_la_code": _get_str("101"),
        "new_la_code": _get_str("E10000000"),
        "main_category": _get_str("Section A: Schools"),
        "category_of_expenditure": category_of_expenditure,
        "early_years_establishments": _get_float(),
        "primary_schools": _get_float(),
        "secondary_schools": _get_float(),
        "sen_and_special_schools": _get_float(),
        "pupil_referral_units_and_alt_provision": _get_float(),
        "post_16": _get_float(),
        "gross_expenditure": _get_float(),
        "income": _get_float(),
        "net_expenditure": _get_float(),
        "net_per_capita_expenditure": _get_float(),
        "EES_order": list(range(1, len(category_of_expenditure) + 1)),
    }

    return pd.DataFrame(outturn)


@pytest.fixture
def la_statistical_neighbours_df() -> pd.DataFrame:
    columns = [
        "LA number",
        "SN1",
        "SN1",
        "SN2",
        "SN2",
        "SN3",
        "SN3",
        "SN4",
        "SN4",
        "SN5",
        "SN5",
        "SN6",
        "SN6",
        "SN7",
        "SN7",
        "SN8",
        "SN8",
        "SN9",
        "SN9",
        "SN10",
        "SN10",
        "SN1",
        "SN2",
        "SN3",
        "SN4",
        "SN5",
        "SN6",
        "SN7",
        "SN8",
        "SN9",
        "SN10",
        "GOInd",
        "GOReg",
    ]

    data = [
        (
            101,
            "LA 2",
            "Close",
            "LA 3",
            "Close",
            "LA 4",
            "Somewhat close",
            "LA 5",
            "Somewhat close",
            "LA 6",
            "Not Close",
            "LA 7",
            "Not Close",
            "LA 8",
            "Not Close",
            "LA 9",
            "Not Close",
            "LA 10",
            "Not Close",
            "LA 11",
            "Not Close",
            102,
            103,
            104,
            105,
            106,
            107,
            108,
            109,
            110,
            111,
            3,
            "London (Inner)",
        ),
        (
            102,
            "LA 3",
            "Close",
            "LA 4",
            "Somewhat close",
            "LA 5",
            "Somewhat close",
            "LA 6",
            "Not Close",
            "LA 7",
            "Not Close",
            "LA 8",
            "Not Close",
            "LA 9",
            "Not Close",
            "LA 10",
            "Not Close",
            "LA 11",
            "Not Close",
            "LA 12",
            "Not Close",
            103,
            104,
            105,
            106,
            107,
            108,
            109,
            110,
            111,
            112,
            8,
            "West Midlands",
        ),
    ]

    return pd.DataFrame(data, columns=columns).set_index("LA number")


@pytest.fixture
def la_statistical_neighbours_io(la_statistical_neighbours_df) -> io.BytesIO:
    buffer = io.BytesIO()
    la_statistical_neighbours_df.to_excel(buffer, sheet_name="SNsWithNewDorsetBCP")
    buffer.seek(0)
    return buffer


@pytest.fixture
def la_ons_raw() -> pd.DataFrame:
    year = 2024
    years = list(map(str, range(year - 5, year + 5)))
    ages = list(map(str, range(1, 43)))

    data = {
        "AREA_CODE": ["E10000000"] * len(ages),
        "AREA_NAME": ["West Yorkshire"] * len(ages),
        "COMPONENT": ["Population"] * len(ages),
        "SEX": ["persons"] * len(ages),
        "AGE_GROUP": ages,
    } | {y: 1.0 for y in years}

    return pd.DataFrame(data)


@pytest.fixture
def la_ons_preprocessed(la_ons_raw):
    return prepare_ons_population_estimates(io.StringIO(la_ons_raw.to_csv()), year=2024)


@pytest.fixture
def la_sen2_raw() -> pd.DataFrame:
    establishment_types = [
        "AP/PRU - Academy",
        "AP/PRU - Free school",
        "AP/PRU - LA maintained",
        "Total",
        "Awaiting provision - above compulsory school age and in education",
        "Awaiting provision - above compulsory school age and not in education",
        "Awaiting provision - below compulsory school age and in education",
        "Awaiting provision - below compulsory school age and not in education",
        "Awaiting provision - compulsory school age and in education",
        "Awaiting provision - compulsory school age and not in education",
        "Awaiting provision - total",
        "Elective home education",
        "Not in education or training - notice to cease issued",
        "Not in education or training - other",
        "Not in education or training - other - compulsory school age",
        "Other arrangements by local authority",
        "Other arrangements by parents",
        "Permanently excluded",
        "Total",
        "General FE and tertiary colleges/HE",
        "Independent specialist providers",
        "Other FE",
        "Sixth form college",
        "Specialist post-16 institutions",
        "Total",
        "UKRLP provider",
        "Mainstream - Academy",
        "Mainstream - Academy - Resourced Provision",
        "Mainstream - Academy - SEN unit",
        "Mainstream - Free school",
        "Mainstream - Free school - Resourced provision",
        "Mainstream - Free school - SEN unit",
        "Mainstream - Independent",
        "Mainstream - LA maintained",
        "Mainstream - LA maintained - Resourced provision",
        "Mainstream - LA maintained - SEN unit",
        "Total",
        "NEET",
        "Total",
        "Non-maintained early years",
        "Total",
        "Other",
        "Total",
        "Hospital School",
        "Special - Academy",
        "Special - Academy/free",
        "Special - Free school",
        "Special - Independent",
        "Special - LA maintained",
        "Special - Non-maintained",
        "Total",
        "Total",
        "Apprenticeship",
        "Supported Internship",
        "Total",
        "Traineeship",
        "Unknown",
        "Work-based learning",
    ]
    establishment_groups = [
        "Alternative provision/Pupil referral unit",
        "Alternative provision/Pupil referral unit",
        "Alternative provision/Pupil referral unit",
        "Alternative provision/Pupil referral unit",
        "Educated elsewhere",
        "Educated elsewhere",
        "Educated elsewhere",
        "Educated elsewhere",
        "Educated elsewhere",
        "Educated elsewhere",
        "Educated elsewhere",
        "Educated elsewhere",
        "Educated elsewhere",
        "Educated elsewhere",
        "Educated elsewhere",
        "Educated elsewhere",
        "Educated elsewhere",
        "Educated elsewhere",
        "Educated elsewhere",
        "Further education",
        "Further education",
        "Further education",
        "Further education",
        "Further education",
        "Further education",
        "Further education",
        "Mainstream school",
        "Mainstream school",
        "Mainstream school",
        "Mainstream school",
        "Mainstream school",
        "Mainstream school",
        "Mainstream school",
        "Mainstream school",
        "Mainstream school",
        "Mainstream school",
        "Mainstream school",
        "NEET",
        "NEET",
        "Non-maintained early years",
        "Non-maintained early years",
        "Other",
        "Other",
        "Special school",
        "Special school",
        "Special school",
        "Special school",
        "Special school",
        "Special school",
        "Special school",
        "Special school",
        "Total",
        "Unknown",
        "Unknown",
        "Unknown",
        "Unknown",
        "Unknown",
        "Unknown",
    ]
    sen2 = {
        "time_period": "2024",
        "time_identifier": ["Financial year"] * len(establishment_types),
        "geographic_level": ["Local authority"] * len(establishment_types),
        "country_code": ["E92000001"] * len(establishment_types),
        "country_name": ["England"] * len(establishment_types),
        "region_code": ["E13000001"] * len(establishment_types),
        "region_name": ["Yorkshire and The Humber"] * len(establishment_types),
        "la_name": ["West Yorkshire"] * len(establishment_types),
        "new_la_code": ["E10000000"] * len(establishment_types),
        "old_la_code": [101] * len(establishment_types),
        "establishment_group": establishment_groups,
        "establishment_type": establishment_types,
        "ehcp_or_statement": ["Total"] * len(establishment_types),
        "num_caseload": [1.0] * len(establishment_types),
        "pc_caseload": [1.0] * len(establishment_types),
    }
    return pd.DataFrame(sen2)


@pytest.fixture
def la_sen2_preprocessed(la_sen2_raw) -> pd.DataFrame:
    return prepare_sen2_data(io.StringIO(la_sen2_raw.to_csv()), year=2024)


@pytest.fixture
def la_place_numbers() -> pd.DataFrame:
    """
    Minimal high needs place-numbers frame keyed by URN, with just the
    6k/10k columns that are used in the recoupment calculation.
    """
    year = 2024
    six_k_col, ten_k_col = input_schemas.get_six_and_ten_k_cols(year)

    data = {
        "URN": [3, 4],
        six_k_col: [10, 5],
        ten_k_col: [0, 3],
    }

    return pd.DataFrame(data)


@pytest.fixture
def la_dsg_raw() -> io.BytesIO:
    """
    Mimics the DSG 'High_needs_deductions' sheet after
    pd.read_excel(..., header=[0, 1, 2]) with a 3-level column MultiIndex.
    """
    columns = [
        ("LA", "", ""),
        # --- DSGSENAcademyPlaceFunding ---
        ("Special academies", "Pre-16 SEN Places", "SEN places deduction (£s)"),
        ("Special academies", "Post-16 SEN Places", "SEN places deduction (£s)"),
        ("Special academies", "Pre-16 AP Places", "AP places deduction (£s)"),
        ("Special free schools", "Pre-16 SEN places", "SEN places deduction (£s)"),
        ("Special free schools", "Post-16 SEN places", "SEN places deduction (£s)"),
        ("Special free schools", "Pre-16 AP Places", "AP places deduction (£s) "),
        # --- DSGAPAcademyPlaceFunding ---
        (
            "Alternative provision (AP) academies and free schools ",
            "Pre-16 SEN places",
            "SEN places deduction (£s)",
        ),
        (
            "Alternative provision (AP) academies and free schools ",
            "Post-16 SEN places",
            "SEN places deduction (£s)",
        ),
        (
            "Alternative provision (AP) academies and free schools ",
            "Pre-16 AP Places",
            "AP places deduction (£s) ",
        ),
        # --- DSGHospitalPlaceFunding ---
        (
            "Hospital Academies",
            "Hospital Academies funding",
            "Total hospital education deduction (£s)",
        ),
        # --- Total Mainstream DSG deduction ---
        (
            "Mainstream academies (special educational needs (SEN) units and resourced provision)",
            "Pre-16 SEN places funded at £6,000",
            "SEN places deduction (£s)",
        ),
        (
            "Mainstream academies (special educational needs (SEN) units and resourced provision)",
            "Pre-16 SEN places funded at £10,000",
            "SEN places deduction (£s)",
        ),
        (
            "Mainstream academies (special educational needs (SEN) units and resourced provision)",
            "Post-16 SEN Places",
            "SEN places deduction (£s)",
        ),
        (
            "Mainstream academies (special educational needs (SEN) units and resourced provision)",
            "Pre-16 alternative provision (AP) places",
            "AP places deduction (£s)",
        ),
    ]

    index = pd.MultiIndex.from_tuples(columns, names=["level_0", "level_1", "level_2"])

    la_code = 101
    values = np.array(
        [
            [
                la_code,    # "LA"
                10_000.0,   # Special academies pre-16 SEN
                2_000.0,    # Special academies post-16 SEN
                1_000.0,    # Special academies pre-16 AP
                5_000.0,    # Special free schools pre-16 SEN
                1_000.0,    # Special free schools post-16 SEN
                500.0,      # Special free schools pre-16 AP
                # DSGSENAcademyPlaceFunding = 10k+2k+1k+5k+1k+500 = 19_500
                1_500.0,    # AP academies pre-16 SEN
                500.0,      # AP academies post-16 SEN
                1_500.0,    # AP academies pre-16 AP
                # DSGAPAcademyPlaceFunding = 1.5k+500+1.5k = 3_500
                750.0,      # Hospital
                # DSGHospitalPlaceFunding = 750
                6_000.0,    # Mainstream pre-16 SEN @£6k
                4_000.0,    # Mainstream pre-16 SEN @£10k
                3_000.0,    # Mainstream post-16 SEN
                2_000.0,    # Mainstream pre-16 AP
                # Total Mainstream DSG deduction = 6k+4k+3k+2k = 15_000
            ]
        ]
    )

    df = pd.DataFrame(values, columns=index)
    buffer = io.BytesIO()
    df.to_excel(buffer, engine="odf", sheet_name="High_needs_deductions")
    buffer.seek(0)
    return buffer


@pytest.fixture
def la_dsg_preprocessed(la_dsg_raw):
    return prepare_dsg_data(la_dsg_raw)
