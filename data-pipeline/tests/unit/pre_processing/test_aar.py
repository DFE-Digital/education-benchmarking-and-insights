import io

import pandas as pd

from pipeline.pre_processing.aar.academies import prepare_aar_data

_expected_output_columns = [
    "LA",
    "Estab",
    "Academy UPIN",
    "ACADEMYTRUSTSTATUS",
    "Company Registration Number",
    "Date joined or opened if in period",
    "Date left or closed if in period",
    "BNCH11110T (EFA Revenue Grants)",
    "BNCH11131 (DfE Family Revenue Grants)",
    "Income_Targeted grants",
    "Income_Other DFE grants",
    "Income_Other grants",
    "Income_Government source",
    "Income_Academies",
    "BNCH11163 (Non- Government)",
    "BNCH11123-BAI011-A (Academies - Income)",
    "BNCH11201 (Income from facilities and services)",
    "Income_Catering services",
    "Income_Receipts supply teacher insurance",
    "Income_Donations and voluntary funds",
    "Income_Other self-generated income",
    "BNCH11205 (Other Income from facilities and services)",
    "Income_Investment income",
    "Administrative supplies_Administrative supplies (non educational)",
    "Catering staff and supplies_Catering staff",
    "Catering staff and supplies_Catering supplies",
    "Other costs_Direct revenue financing",
    "Educational ICT_ICT learning resources",
    "Educational supplies_Examination fees",
    "Educational supplies_Learning resources (not ICT equipment)",
    "Non-educational support staff and services_Administrative and clerical staff",
    "Non-educational support staff and services_Other staff",
    "Revenue reserve",
    "Non-educational support staff and services_Professional services (non-curriculum)",
    "Non-educational support staff and services_Audit cost",
    "Premises staff and services_Maintenance of premises",
    "Other costs_Grounds maintenance",
    "Other costs_Indirect employee expenses",
    "Other costs_Interest charges for loan and bank",
    "Other costs_Other insurance premiums",
    "Other costs_PFI charges",
    "Other costs_Rent and rates",
    "Other costs_Special facilities",
    "Other costs_Staff development and training",
    "Other costs_Staff-related insurance",
    "Other costs_Supply teacher insurance",
    "Premises staff and services_Cleaning and caretaking",
    "Premises staff and services_Other occupation costs",
    "Premises staff and services_Premises staff",
    "Teaching and Teaching support staff_Teaching staff",
    "Teaching and Teaching support staff_Supply teaching staff",
    "Teaching and Teaching support staff_Education support staff",
    "Teaching and Teaching support staff_Educational consultancy",
    "Teaching and Teaching support staff_Agency supply teaching staff",
    "Utilities_Energy",
    "Utilities_Water and sewerage",
    "Valid To",
    "Income_Direct revenue finance",
    "Income_Total grant funding",
    "Income_Total self generated funding",
    "Income_Direct grants",
    "Income_Pre Post 16",
    "Income_Other Revenue Income",
    "Income_Facilities and services",
    "Total Expenditure",
    "Total Income",
    "In year balance",
    "Trust Balance",
    "Financial Position",
    "Trust Financial Position",
    "London Weighting",
    "PFI School",
    "Is PFI",
]


def test_aar_data_has_correct_output_columns(prepared_aar_data: pd.DataFrame):
    assert list(prepared_aar_data.columns) == _expected_output_columns


def test_aar_balance_aggregated_at_trust_level(prepared_aar_data: pd.DataFrame):
    assert prepared_aar_data["Trust Balance"].loc[100150] == -48080.0


def test_aar_academy_financial_position(prepared_aar_data: pd.DataFrame):
    assert prepared_aar_data["Financial Position"].loc[100150] == "Deficit"


def test_aar_academy_financial_in_year_balance(prepared_aar_data: pd.DataFrame):
    assert prepared_aar_data["In year balance"].loc[100153] == -16032.0


def test_aar_academy_financial_position_deficit(prepared_aar_data: pd.DataFrame):
    assert prepared_aar_data["Financial Position"].loc[100153] == "Deficit"


def test_empty_lines_stripped(aar_data: pd.DataFrame):
    empty_line = ",".join("" for _ in range(len(aar_data.columns)))
    empty_lines = "\n".join(empty_line for _ in range(1_000))
    csv_with_empty_lines = aar_data.to_csv() + empty_lines

    result = prepare_aar_data(
        aar_path=io.StringIO(csv_with_empty_lines),
        year=2022,
    )

    assert len(csv_with_empty_lines.splitlines()) > len(aar_data.index)
    assert len(result.index) == len(aar_data.index)


def test_aar_transitioned_academies_not_removed(
    aar_data: pd.DataFrame,
    prepared_aar_data: pd.DataFrame,
):
    assert sorted(aar_data["URN"]) == [100150, 100152, 100152, 100153]
    assert sorted(prepared_aar_data.index) == [100150, 100152, 100152, 100153]


def test_aar_new_codes():
    """
    Input column headings changed significantly in 2024.

    Validate the input data with the newer headings results in the same
    output data.
    """
    aar_data = pd.DataFrame(
        {
            "LA": [100],
            "Estab": [100],
            "URN": [100],
            "ACADEMYUPIN": [100],
            "Company_Number": ["Company_Number"],
            "Company_Name": ["Company_Name"],
            "ACADEMYTRUSTSTATUS": [100.00],
            "Date left or closed if in period:": [100.00],
            "Date joined or opened if in period:": [100.00],
            "Valid To": [100.00],
            "BAI050-T": [100.00],
            "BAI010-T": [100.00],
            "BAI020-T": [100.00],
            "BAI030-T": [100.00],
            "BAI040-T": [100.00],
            "BAI060-T": [100.00],
            "BAI070-T": [100.00],
            "BAI080-T": [100.00],
            "BAI090-T": [100.00],
            "BAI100-T": [100.00],
            "BAI110-T": [100.00],
            "BAI120-T": [100.00],
            "BAI130-T": [100.00],
            "BAI140-T": [100.00],
            "BAI150-T": [100.00],
            "BAE010-T": [100.00],
            "BAE020-T": [100.00],
            "BAE030-T": [100.00],
            "BAE040-T": [100.00],
            "BAE050-T": [100.00],
            "BAE060-T": [100.00],
            "BAE070-T": [100.00],
            "BAE080-T": [100.00],
            "BAE090-T": [100.00],
            "BAE110-T": [100.00],
            "BAE100-T": [100.00],
            "BAE120-T": [100.00],
            "BAE130-T": [100.00],
            "BAE140-T": [100.00],
            "BAE150-T": [100.00],
            "BAE160-T": [100.00],
            "BAE170-T": [100.00],
            "BAE180-T": [100.00],
            "BAE190-T": [100.00],
            "BAE200-T": [100.00],
            "BAE210-T": [100.00],
            "BAE220-T": [100.00],
            "BAE230-T": [100.00],
            "BAE240-T": [100.00],
            "BAE250-T": [100.00],
            "BAE300-T": [100.00],
            "BAE260-T": [100.00],
            "BAE270-T": [100.00],
            "BAE280-T": [100.00],
            "BAE290-T": [100.00],
            "BAE320-T": [100.00],
            "BAE310-T": [100.00],
            "BAI160-T": [100.00],
            "BAI170-T": [100.00],
            "BAB030-T": [100.00],
            "BAI061-T": [100.00],
            "BAI101-T": [100.00],
            "BAI011-A": [100.00],
        }
    )
    aar_data = io.StringIO(aar_data.to_csv())

    result = prepare_aar_data(aar_path=aar_data, year=2024)

    assert len(result.index) == 1
    # note: output may have _additional_ columns due to derived values.
    assert all(column in result.columns for column in _expected_output_columns)
