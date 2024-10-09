import io

import pandas as pd

from src.pipeline import pre_processing


def test_aar_data_has_correct_output_columns(prepared_aar_data: pd.DataFrame):
    assert list(prepared_aar_data.columns) == [
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
        "Income_Pre Post 16",
        "Income_Other grants",
        "Income_Government source",
        "Income_Academies",
        "Income_Non government",
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
        "BNCH11000T (Revenue Income)",
        "BNCH20000T (Total Costs)",
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
        "Income_Total grant funding",
        "Income_Total self generated funding",
        "Income_Direct grants",
        "Income_Other DFE grants",
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

    result = pre_processing.prepare_aar_data(
        aar_path=io.StringIO(csv_with_empty_lines),
        current_year=2022,
    )

    assert len(csv_with_empty_lines.splitlines()) > len(aar_data.index)
    assert len(result.index) == len(aar_data.index)


def test_aar_transitioned_academies_not_removed(
    aar_data: pd.DataFrame,
    prepared_aar_data: pd.DataFrame,
):
    assert sorted(aar_data["URN"]) == [100150, 100152, 100152, 100153]
    assert sorted(prepared_aar_data.index) == [100150, 100152, 100152, 100153]
