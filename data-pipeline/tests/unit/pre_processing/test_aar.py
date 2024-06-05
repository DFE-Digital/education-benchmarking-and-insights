import pandas as pd
import pytest

from src.pipeline.config import income_category_map


def test_aar_data_has_correct_output_columns(prepared_aar_data: pd.DataFrame):
    assert list(prepared_aar_data.columns) == [
        "Academy Balance",
        "PFI School",
        "Trust UPIN",
        "DFE/EFA Revenue grants (includes Coronavirus Government Funding",
        "of which: Coronavirus Government Funding",
        "SEN funding",
        "Other DfE/EFA Revenue Grants",
        "Other income - LA & other Government grants",
        "Government source, non-grant",
        "Academies",
        "Non-Government",
        "All income from facilities and services",
        "Income from catering",
        "Receipts from supply teacher insurance claims",
        "Donations and/or voluntary funds",
        "Other self-generated income",
        "Investment income",
        "Teaching and Teaching support staff_Teaching staff",
        "Teaching and Teaching support staff_Supply teaching staff",
        "Teaching and Teaching support staff_Education support staff",
        "Non-educational support staff and services_Administrative and clerical staff",
        "Premises staff and services_Premises staff",
        "Catering staff and supplies_Catering staff",
        "Non-educational support staff and services_Other staff",
        "Other costs_Indirect employee expenses",
        "Other costs_Staff development and training",
        "Other costs_Staff-related insurance",
        "Other costs_Supply teacher insurance",
        "Premises staff and services_Maintenance of premises",
        "Premises staff and services_Cleaning and caretaking",
        "Utilities_Water and sewerage",
        "Utilities_Energy",
        "Other costs_Rent and rates",
        "Premises staff and services_Other occupation costs",
        "Other costs_Special facilities",
        "Educational supplies_Learning resources (not ICT equipment)",
        "Educational ICT_ICT learning resources",
        "Educational supplies_Examination fees",
        "Teaching and Teaching support staff_Educational consultancy",
        "Administrative supplies_Administrative supplies (non educational)",
        "Teaching and Teaching support staff_Agency supply teaching staff",
        "Catering staff and supplies_Catering supplies",
        "Other costs_Other insurance premiums",
        "Non-educational support staff and services_Professional services (non-curriculum)",
        "Non-educational support staff and services_Audit cost",
        "Other costs_Interest charges for loan and bank",
        "Other costs_Direct revenue financing",
        "Other costs_PFI charges",
        "Revenue reserve",
        "Total Grant Funding",
        "Direct Grants",
        "Community Grants",
        "Targeted Grants",
        "Total Self Generated Funding",
        "Total Income",
        "Supply Staff Costs",
        "Other Staff Costs",
        "Total Staff Costs",
        "Maintenance & Improvement Costs",
        "Premises Costs",
        "Catering Expenses",
        "Occupation Costs",
        "Total Costs of Supplies and Services",
        "Total Costs of Educational Supplies",
        "Costs of Brought in Professional Services",
        "Total Expenditure",
        "Share of Revenue Reserve, distributed on per pupil basis\n",
        "London Weighting",
        "Date joined or opened if in period",
        "Date left or closed if in period",
        "Trust Balance",
        "Trust_DFE/EFA Revenue grants (includes Coronavirus Government Funding",
        "Trust_of which: Coronavirus Government Funding",
        "Trust_SEN funding",
        "Trust_Other DfE/EFA Revenue Grants",
        "Trust_Other income - LA & other Government grants",
        "Trust_Government source, non-grant",
        "Trust_Academies",
        "Trust_Non-Government",
        "Trust_All income from facilities and services",
        "Trust_Income from catering",
        "Trust_Receipts from supply teacher insurance claims",
        "Trust_Donations and/or voluntary funds",
        "Trust_Other self-generated income",
        "Trust_Investment income",
        "Central Services Balance",
        "Central Services Financial Position",
        "Academy Financial Position",
        "Trust Financial Position",
        "Is PFI",
    ]


def test_aar_central_services_balance_aggregated_at_trust_level(
    prepared_aar_data: pd.DataFrame,
):
    assert (
        prepared_aar_data["Central Services Balance"][
            prepared_aar_data["Trust UPIN"] == 137157
        ].iloc[0]
        == 2001
    )


@pytest.mark.parametrize("col_name", income_category_map["academies"])
def test_aar_income_aggregated_at_trust_level(
    col_name: str, prepared_aar_data: pd.DataFrame
):
    assert (
        prepared_aar_data["Trust_" + col_name][
            prepared_aar_data["Trust UPIN"] == 137157
        ].iloc[0]
        == 2001
    )


def test_aar_balance_aggregated_at_trust_level(prepared_aar_data: pd.DataFrame):
    assert (
        prepared_aar_data["Trust Balance"][
            prepared_aar_data["Trust UPIN"] == 137157
        ].iloc[0]
        == 2001
    )


def test_aar_trust_financial_position(prepared_aar_data: pd.DataFrame):
    assert (
        prepared_aar_data["Trust Financial Position"][
            prepared_aar_data["Trust UPIN"] == 137157
        ].iloc[0]
        == "Surplus"
    )


def test_aar_central_services_financial_position(prepared_aar_data: pd.DataFrame):
    assert (
        prepared_aar_data["Central Services Financial Position"][
            prepared_aar_data["Trust UPIN"] == 137157
        ].iloc[0]
        == "Surplus"
    )


def test_aar_academy_financial_position(prepared_aar_data: pd.DataFrame):
    assert (
        prepared_aar_data["Academy Financial Position"][
            prepared_aar_data["Trust UPIN"] == 137157
        ].iloc[0]
        == "Surplus"
    )


def test_aar_trust_financial_position_deficit(prepared_aar_data: pd.DataFrame):
    assert (
        prepared_aar_data["Trust Financial Position"][
            prepared_aar_data["Trust UPIN"] == 135112
        ].iloc[0]
        == "Deficit"
    )


def test_aar_central_services_financial_position_deficit(
    prepared_aar_data: pd.DataFrame,
):
    assert (
        prepared_aar_data["Central Services Financial Position"][
            prepared_aar_data["Trust UPIN"] == 135112
        ].iloc[0]
        == "Deficit"
    )


def test_aar_academy_financial_position_deficit(prepared_aar_data: pd.DataFrame):
    assert (
        prepared_aar_data["Academy Financial Position"][
            prepared_aar_data["Trust UPIN"] == 135112
        ].iloc[0]
        == "Deficit"
    )
