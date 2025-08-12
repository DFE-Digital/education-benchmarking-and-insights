import io

import pandas as pd

from pipeline.pre_processing.aar.central_services import prepare_central_services_data

_expected_output_columns = [
    "Company Registration Number",
    "Company_Name",
    "BNCH11110T (EFA Revenue Grants)",
    "BNCH11131 (DfE Family Revenue Grants)",
    "Income_Targeted grants",
    "Income_Other DFE grants",
    "Income_Other grants",
    "Income_Government source",
    "Income_Academies",
    "BNCH11163 (Non- Government)",
    "BNCH11123-BTI011-A (MAT Central services - Income)",
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
    "Non-educational support staff and services_Audit cost",
    "Non-educational support staff and services_Other staff",
    "Revenue reserve",
    "Non-educational support staff and services_Professional services "
    "(non-curriculum)",
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
    "Financial Position",
]


def test_central_services_data_has_correct_output_columns(
    prepared_central_services_data: pd.DataFrame,
):
    assert list(prepared_central_services_data.columns) == _expected_output_columns


def test_central_services_new_columns():
    """
    Input column headings changed significantly in 2024.

    Validate the input data with the newer headings results in the same
    output data.
    """
    cs_data = pd.DataFrame(
        {
            "Lead_UPIN": 100,
            "Company_Number": "Company_Number",
            "Company_Name": "Company_Name",
            "BTI050": [100.0],
            "BTI061": [100.0],
            "BTI030": [100.0],
            "BTI040": [100.0],
            "BTI060": [100.0],
            "BTI070": [100.0],
            "BTI080": [100.0],
            "BTI090": [100.0],
            "BTI100": [100.0],
            "BTI110": [100.0],
            "BTI120": [100.0],
            "BTI130": [100.0],
            "BTI140": [100.0],
            "BTI150": [100.0],
            "BTE010": [100.0],
            "BTE020": [100.0],
            "BTE030": [100.0],
            "BTE040": [100.0],
            "BTE050": [100.0],
            "BTE060": [100.0],
            "BTE070": [100.0],
            "BTE080": [100.0],
            "BTE090": [100.0],
            "BTE110": [100.0],
            "BTE100": [100.0],
            "BTE120": [100.0],
            "BTE130": [100.0],
            "BTE140": [100.0],
            "BTE150": [100.0],
            "BTE160": [100.0],
            "BTE170": [100.0],
            "BTE180": [100.0],
            "BTE190": [100.0],
            "BTE200": [100.0],
            "BTE210": [100.0],
            "BTE220": [100.0],
            "BTE230": [100.0],
            "BTE240": [100.0],
            "BTE250": [100.0],
            "BTE300": [100.0],
            "BTE260": [100.0],
            "BTE270": [100.0],
            "BTE280": [100.0],
            "BTE290": [100.0],
            "BTE320": [100.0],
            "BTE310": [100.0],
            "BTI170": [100.0],
            "BTB030": [100.0],
            "BAB030-T": [100.0],
            "BTI101": [100.0],
            "BTI011-A": [100.0],
        }
    )
    cs_data = io.StringIO(cs_data.to_csv())

    result = prepare_central_services_data(cs_data, year=2024)

    assert len(result.index) == 1
    # note: output may have _additional_ columns due to derived values.
    assert all(column in result.columns for column in _expected_output_columns)
