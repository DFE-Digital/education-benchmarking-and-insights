import io
import random

import pandas as pd
import pytest


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
        "1.9.3 Dedicated Schools Grant carried forward to next year",
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
def la_statistical_neighbours() -> io.StringIO:
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

    test_data_df = pd.DataFrame(data, columns=columns)

    buffer = io.BytesIO()
    test_data_df.to_excel(buffer, sheet_name="SNsWithNewDorsetBCP")
    buffer.seek(0)

    return buffer


@pytest.fixture
def la_ons() -> pd.DataFrame:
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
def la_sen2() -> pd.DataFrame:
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


def _la_dsg_block() -> pd.DataFrame:
    data = {
        "Dedicated schools grant: 2023 to 2024 provisional high needs block allocations": list(
            map(str, range(100, 111))
        ),
        "Total high needs block before deductions (£s)": list(
            map(float, range(100, 111))
        ),
    }

    return pd.DataFrame(data)


def _la_dsg_deductions() -> pd.DataFrame:
    data = {
        "Dedicated schools grant: 2023 to 2024 high needs block deductions": list(
            map(str, range(100, 111))
        ),
        "Total deduction to high needs block for direct funding of places (£s)": list(
            map(float, range(100, 111))
        ),
    }

    return pd.DataFrame(data)


@pytest.fixture
def la_dsg() -> io.BytesIO:
    data = io.BytesIO()

    with pd.ExcelWriter(data, engine="odf") as writer:
        _la_dsg_block().to_excel(writer, sheet_name="High_needs_block", index=False)
        _la_dsg_deductions().to_excel(
            writer, sheet_name="High_needs_deductions", index=False
        )

    data.seek(0)
    return data
