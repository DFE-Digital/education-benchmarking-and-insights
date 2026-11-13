import io
import random

import numpy as np
import pandas as pd
import pytest

from pipeline import input_schemas
from pipeline.input_schemas.dsg import flat_high_needs_block_cols
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
            "SEN support": [90, 80, 1, 0, 34, 220],
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
        "1.4.11 SEN transport",
        "1.9.3 Dedicated Schools Grant carried forward to next year",
        "2.1.4 Home to school transport (pre 16): SEN transport expenditure",
        "2.1.6 Home to post-16 provision: SEN/ LLDD transport expenditure (aged 16-18)",
        "2.1.7 Home to post-16 provision: SEN/ LLDD transport expenditure (aged 19-25)",
        "2.1.1 Educational psychology service",
        "2.1.2 SEN administration, assessment and coordination and monitoring",
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
        "1.4.11 SEN transport",
        "1.9.3 Dedicated Schools Grant carried forward to next year",
        "2.1.4 Home to school transport (pre 16): SEN transport expenditure",
        "2.1.6 Home to post-16 provision: SEN/ LLDD transport expenditure (aged 16-18)",
        "2.1.7 Home to post-16 provision: SEN/ LLDD transport expenditure (aged 19-25)",
        "2.1.1 Educational psychology service",
        "2.1.2 SEN administration, assessment and coordination and monitoring",
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
def la_sen2_caseload_2025_raw() -> pd.DataFrame:
    return pd.DataFrame(
        {
            "time_period": [202425, 202425, 202324],
            "time_identifier": ["Academic year"] * 3,
            "geographic_level": ["Local authority", "National", "Local authority"],
            "country_code": ["E92000001"] * 3,
            "country_name": ["England"] * 3,
            "region_code": ["E13000001", "", "E13000001"],
            "region_name": ["Yorkshire and The Humber", "", "Yorkshire and The Humber"],
            "new_la_code": ["E10000000", "z", "E10000000"],
            "old_la_code": [101, np.nan, 101],
            "la_name": ["West Yorkshire", "", "West Yorkshire"],
            "breakdown_topic": ["All EHC plans"] * 3,
            "breakdown": ["All EHC plans", "All EHC plans", "All EHC plans"],
            "ehcplans": [100, 1000, 90],
            "mainstream_la_maintained": [10, 100, 10],
            "mainstream_academy": [5, 50, 5],
            "mainstream_free_school": [2, 20, 2],
            "mainstream_la_maintained_resourced_provision": [1, 10, 1],
            "mainstream_la_maintained_senunit": [1, 10, 1],
            "mainstream_academy_resourced_provision": [1, 10, 1],
            "mainstream_academy_senunit": [1, 10, 1],
            "mainstream_free_school_resourced_provision": [1, 10, 1],
            "mainstream_free_school_senunit": [1, 10, 1],
            "mainstream_independent": [3, 30, 3],
            "mainstream_total": [26, 260, 26],
            "mainstream_total_pc": [26.0, 26.0, 26.0],
            "special_la_maintained": [20, 200, 20],
            "special_academy_free": [15, 150, 15],
            "special_independent": [4, 40, 4],
            "special_non_maintained": [2, 20, 2],
            "special_total": [41, 410, 41],
            "special_total_pc": [41.0, 41.0, 41.0],
            "ap_pru_la_maintained": [3, 30, 3],
            "ap_pru_academy": [2, 20, 2],
            "ap_pru_free_school": [1, 10, 1],
            "ap_pru_total": [6, 60, 6],
            "AP_PRU_total_pc": [6.0, 6.0, 6.0],
            "general_fe_tertiary_colleges": [8, 80, 8],
            "specialist_post_16_institutions": [4, 40, 4],
            "ukrlp_provider": [1, 10, 1],
            "fe_total": [13, 130, 13],
            "fe_total_pc": [13.0, 13.0, 13.0],
            "ed_elsewhere_pc": [1.0, 1.0, 1.0],
            "nm_early_years_pc": [1.0, 1.0, 1.0],
            "placement_unknown_pc": [1.0, 1.0, 1.0],
        }
    )


@pytest.fixture
def la_sen2_caseload_2026_raw(la_sen2_caseload_2025_raw: pd.DataFrame) -> pd.DataFrame:
    df = la_sen2_caseload_2025_raw.copy()
    df["time_period"] = [202526, 202526, 202425]
    df.rename(columns={"AP_PRU_total_pc": "ap_pru_total_pc"}, inplace=True)
    df.drop(columns=["special_academy_free"], inplace=True)
    df["special_academy"] = [10, 100, 10]
    df["special_free_school"] = [5, 50, 5]
    return df


@pytest.fixture
def la_place_numbers() -> pd.DataFrame:
    """
    Minimal high needs place-numbers frame keyed by URN, with just the
    6k/10k columns that are used in the recoupment calculation.
    """
    year = 2024
    six_k_col, ten_k_col, post_16_col = input_schemas.get_six_and_ten_k_cols(year)

    data = {"URN": [3, 4], six_k_col: [10, 5], ten_k_col: [0, 3], post_16_col: [1, 1]}

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
                la_code,  # "LA"
                10_000.0,  # Special academies pre-16 SEN
                2_000.0,  # Special academies post-16 SEN
                1_000.0,  # Special academies pre-16 AP
                5_000.0,  # Special free schools pre-16 SEN
                1_000.0,  # Special free schools post-16 SEN
                500.0,  # Special free schools pre-16 AP
                # DSGSENAcademyPlaceFunding = 10k+2k+1k+5k+1k+500 = 19_500
                1_500.0,  # AP academies pre-16 SEN
                500.0,  # AP academies post-16 SEN
                1_500.0,  # AP academies pre-16 AP
                # DSGAPAcademyPlaceFunding = 1.5k+500+1.5k = 3_500
                750.0,  # Hospital
                # DSGHospitalPlaceFunding = 750
                6_000.0,  # Mainstream pre-16 SEN @£6k
                4_000.0,  # Mainstream pre-16 SEN @£10k
                3_000.0,  # Mainstream post-16 SEN
                2_000.0,  # Mainstream pre-16 AP
                # Total Mainstream DSG deduction = 6k+4k+3k+2k = 15_000
            ]
        ]
    )

    df = pd.DataFrame(values, columns=index)

    # Build a minimal High_needs_block sheet
    high_needs_cols = [("", "", ""), ("", "", flat_high_needs_block_cols[2024])]
    high_needs_index = pd.MultiIndex.from_tuples(high_needs_cols)
    high_needs_values = np.array(
        [
            [
                la_code,  # LA
                100_000.0,  # Total high needs block before deductions (arbitrary test value)
            ],
            [
                "NORTH EAST",
                1_000_000,  # This will be filtered out
            ],
        ]
    )
    high_needs_df = pd.DataFrame(high_needs_values, columns=high_needs_index)
    high_needs_df = high_needs_df.set_index(high_needs_df.columns[0])

    buffer = io.BytesIO()
    with pd.ExcelWriter(buffer, engine="odf") as writer:
        df.to_excel(writer, sheet_name="High_needs_deductions")
        high_needs_df.to_excel(writer, sheet_name="High_needs_block")
    buffer.seek(0)
    return buffer


@pytest.fixture
def la_dsg_preprocessed(la_dsg_raw, monkeypatch):
    monkeypatch.setitem(
        flat_high_needs_block_cols,
        9999,
        (
            "Unnamed: 1_level_0 Unnamed: 1_level_1 "
            "Total high needs block before deductions (£s) [I]  = [A] + ( [B] * [C] ) + [D] + [E] + [F] + [H]"
        ),
    )
    return prepare_dsg_data(la_dsg_raw, 9999)


S251_2026_LINES = [
    "1.0.2",
    "1.2.1",
    "1.2.2",
    "1.2.3",
    "1.2.4",
    "1.2.5",
    "1.2.6",
    "1.2.7",
    "1.2.8",
    "1.2.9",
    "1.2.10",
    "1.2.11",
    "1.2.13",
    "1.4.11",
    "1.9.3",
    "2.1.1",
    "2.1.2",
    "2.1.4",
    "2.1.6",
    "2.1.7",
]

S251_BUDGET_2026_DESCRIPTIONS = [
    "1.0.2 High needs place funding within Individual Schools Budget (before academy recoupment), excluding funding for 16-19 academies and free schools and FE colleges and independent learning providers",
    "1.2.1 Top-up funding – maintained schools",
    "1.2.2 Top-up funding – academies, free schools and colleges",
    "1.2.3 Top-up and other funding – non-maintained and independent providers",
    "1.2.4 Additional high needs targeted funding for mainstream schools and academies",
    "1.2.5 SEN support services",
    "1.2.6 Hospital education services",
    "1.2.7 Other alternative provision services",
    "1.2.8 Support for inclusion",
    "1.2.9 Special schools and PRUs in financial difficulty",
    "1.2.10 PFI/ BSF costs at special schools, AP/ PRUs and Post 16 institutions only",
    "1.2.11 Direct payments (SEN and disability)",
    "1.2.13 Therapies and other health related services",
    "1.4.11 SEN transport",
    "1.9.3 Dedicated Schools Grant carry forward to 2026-27",
    "2.1.1 Educational psychology service",
    "2.1.2 SEN administration, assessment and coordination and monitoring",
    "2.1.4 Home to school transport (pre 16): SEN transport expenditure",
    "2.1.6 Home to post-16 provision: SEN/ LLDD transport expenditure (aged 16-18)",
    "2.1.7 Home to post-16 provision: SEN/ LLDD transport expenditure (aged 19-25)",
]

S251_OUTTURN_2026_DESCRIPTIONS = [
    "1.0.2 High needs place funding within Individual Schools Budget (after academies recoupment), including all pre- and post-16 place funding for maintained schools",
    "1.2.1 Top up funding - maintained schools",
    "1.2.2 Top-up funding – academies, free schools and colleges",
    "1.2.3 Top-up and other funding – non-maintained and independent providers",
    "1.2.4 Additional high needs targeted funding for mainstream schools and academies",
    "1.2.5 SEN support services",
    "1.2.6 Hospital education services",
    "1.2.7 Other alternative provision services",
    "1.2.8 Support for inclusion",
    "1.2.9 Special schools and PRUs in financial difficulty",
    "1.2.10 PFI/ BSF costs at special schools, AP/ PRUs and Post 16 institutions only",
    "1.2.11 Direct payments (SEN and disability)",
    "1.2.13 Therapies and other health related services",
    "1.4.11 SEN transport",
    "1.9.3 Dedicated Schools Grant carry forward to 2025-26",
    "2.1.1 Educational psychology service",
    "2.1.2 SEN administration, assessment and coordination and monitoring",
    "2.1.4 Home to school transport (pre 16): SEN transport expenditure",
    "2.1.6 Home to post-16 provision: SEN/LLDD transport expenditure (aged 16-18)",
    "2.1.7 Home to post-16 provision: SEN/LLDD transport expenditure (aged 19-25)",
]


def write_budget_2026_ods_bytes(df: pd.DataFrame) -> io.BytesIO:
    """Write a Planned_expenditure sheet with Row 0 title banner and Row 1 headers."""
    banner_row = ["Local authority planned expenditure 2015-16 to 2025-26"] + [""] * (
        len(df.columns) - 1
    )
    header_row = list(df.columns)
    sheet_df = pd.DataFrame([banner_row, header_row] + df.values.tolist())
    buffer = io.BytesIO()
    with pd.ExcelWriter(buffer, engine="odf") as writer:
        sheet_df.to_excel(
            writer, sheet_name="Planned_expenditure", index=False, header=False
        )
    buffer.seek(0)
    return buffer


def write_outturn_2026_xlsx_bytes(df: pd.DataFrame) -> io.BytesIO:
    """Write an LA_expenditure_by_year sheet with Row 0 headers."""
    buffer = io.BytesIO()
    with pd.ExcelWriter(buffer, engine="openpyxl") as writer:
        df.to_excel(writer, sheet_name="LA_expenditure_by_year", index=False)
    buffer.seek(0)
    return buffer


@pytest.fixture
def la_budget_2026() -> pd.DataFrame:
    n = len(S251_BUDGET_2026_DESCRIPTIONS)
    return pd.DataFrame(
        {
            "Financial year": [2526] * n,
            "Table name": ["SchoolsBudget"] * n,
            "Local authority number": [101] * n,
            "Local authority name": ["West Yorkshire"] * n,
            "Order line": list(range(1, n + 1)),
            "Description": S251_BUDGET_2026_DESCRIPTIONS,
            "Section 251 line": S251_2026_LINES,
            "Early years": [10.0] * n,
            "Primaries": [20.0] * n,
            "Secondary": [30.0] * n,
            "Special educational needs (SEN) special": [40.0] * n,
            "Alternative Provision(AP)/ Pupil referral unit (PRU)": [50.0] * n,
            "Post school": [60.0] * n,
            "Gross": [250.0] * n,
            "Income": [50.0] * n,
            "Net": [200.0] * n,
        }
    )


@pytest.fixture
def la_outturn_2026() -> pd.DataFrame:
    n = len(S251_OUTTURN_2026_DESCRIPTIONS)
    return pd.DataFrame(
        {
            "time_period": [202526] * n,
            "time_identifier": ["Financial year"] * n,
            "geographic_level": ["Local authority"] * n,
            "country_name": ["England"] * n,
            "country_code": ["E92000001"] * n,
            "region_name": ["Yorkshire and The Humber"] * n,
            "region_code": ["E13000001"] * n,
            "la_name": ["West Yorkshire"] * n,
            "old_la_code": [101] * n,
            "new_la_code": ["E10000000"] * n,
            "RowOrder": list(range(1, n + 1)),
            "Description": S251_OUTTURN_2026_DESCRIPTIONS,
            "S251Line": S251_2026_LINES,
            "EarlyYears": [15.0] * n,
            "Primary": [25.0] * n,
            "Secondary": [35.0] * n,
            "SenSpecial": [45.0] * n,
            "APPru": [55.0] * n,
            "PostSchool": [65.0] * n,
            "Gross": [350.0] * n,
            "Income": [50.0] * n,
            "Net": [300.0] * n,
        }
    )


@pytest.fixture
def la_place_numbers_2026() -> pd.DataFrame:
    year = 2026
    six_k_col, ten_k_col, post_16_col = input_schemas.get_six_and_ten_k_cols(year)
    return pd.DataFrame(
        {"URN": [3, 4], six_k_col: [10, 5], ten_k_col: [0, 3], post_16_col: [1, 1]}
    )
