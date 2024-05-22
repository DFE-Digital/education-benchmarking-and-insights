from io import StringIO, BytesIO

import numpy as np
import pandas as pd
import pytest

from src.pipeline.pre_processing import (
    prepare_cdc_data,
    prepare_sen_data,
    prepare_ks2_data,
    prepare_ks4_data,
    prepare_aar_data
)


@pytest.fixture
def cdc_data() -> pd.DataFrame:
    return pd.DataFrame(
        {
            "Site": ["Site A", "Site A", "Site A", "Site A", "Site B"],
            "URN": [100150, 100150, 100150, 100150, 100162],
            "SurveySectionSectionType": [
                "ANCILLARY",
                "BLOCK",
                "BLOCK",
                "BLOCK",
                "ANCILLARY",
            ],
            "GIFA": [89, 2221, 436, 57, 131],
            "Block Age": ["NULL", "1961-1970", "2001-2010", "1991-2000", "NULL"],
        }
    )


@pytest.fixture
def prepared_cdc_data(cdc_data: pd.DataFrame) -> dict:
    return (
        prepare_cdc_data(StringIO(cdc_data.to_csv()), 2022)
        .reset_index()
        .to_dict("records")[0]
    )


@pytest.fixture
def sen_data() -> pd.DataFrame:
    return pd.DataFrame({
        "URN": [100150, 100151],
        "EHC plan": [50, 100],
        "Total pupils": [100, 100],
        "EHC_Primary_need_spld": [1, 50],
        "SUP_Primary_need_spld": [1, 50],
        "EHC_Primary_need_mld": [1, np.nan],
        "SUP_Primary_need_mld": [2, np.nan],
        "EHC_Primary_need_sld": [1, np.nan],
        "SUP_Primary_need_sld": [3, np.nan],
        "EHC_Primary_need_pmld": [1, np.nan],
        "SUP_Primary_need_pmld": [4, np.nan],
        "EHC_Primary_need_semh": [1, np.nan],
        "SUP_Primary_need_semh": [5, np.nan],
        "EHC_Primary_need_slcn": [1, np.nan],
        "SUP_Primary_need_slcn": [6, np.nan],
        "EHC_Primary_need_hi":  [1, np.nan],
        "SUP_Primary_need_hi": [7, np.nan],
        "EHC_Primary_need_vi": [1, np.nan],
        "SUP_Primary_need_vi": [8, np.nan],
        "EHC_Primary_need_msi": [1, np.nan],
        "SUP_Primary_need_msi": [9, np.nan],
        "EHC_Primary_need_pd": [1, np.nan],
        "SUP_Primary_need_pd": [10, np.nan],
        "EHC_Primary_need_asd": [1, np.nan],
        "SUP_Primary_need_asd": [11, np.nan],
        "EHC_Primary_need_oth": [1, np.nan],
        "SUP_Primary_need_oth": [12, np.nan],
    })


@pytest.fixture
def prepared_sen_data(sen_data: pd.DataFrame) -> dict:
    return (
        prepare_sen_data(StringIO(sen_data.to_csv()))
        .reset_index()
        .to_dict("records")[0]
    )


@pytest.fixture
def prepared_sen_data_with_nans(sen_data: pd.DataFrame) -> dict:
    return (
        prepare_sen_data(StringIO(sen_data.to_csv()))
        .reset_index()
        .to_dict("records")[1]
    )


@pytest.fixture
def ks2_data() -> pd.DataFrame:
    return pd.DataFrame({
        "URN": [100150, 100152, 100153],
        "READPROG": ["0.1", "SUPP", "LOWCOV"],
        "WRITPROG": ["0.1", "SUPP", "LOWCOV"],
        "MATPROG": ["0.1", "SUPP", "LOWCOV"],
    })


@pytest.fixture
def prepared_ks2_data(ks2_data: pd.DataFrame):
    output = BytesIO()
    writer = pd.ExcelWriter(output)
    ks2_data.to_excel(writer, index=False)
    writer.close()
    output.seek(0)

    return prepare_ks2_data(output)


@pytest.fixture
def ks4_data() -> pd.DataFrame:
    return pd.DataFrame({
        "URN": [100150, 100152, 100153],
        "ATT8SCR": ["0.1", "SUPP", "NE"],
        "P8MEA": ["0.1", "SUPP", "NE"],
        "P8_BANDING": ["0.1", "SUPP", "NE"],
    })


@pytest.fixture
def prepared_ks4_data(ks4_data: pd.DataFrame):
    output = BytesIO()
    writer = pd.ExcelWriter(output)
    ks4_data.to_excel(writer, index=False)
    writer.close()
    output.seek(0)

    return prepare_ks4_data(output)


@pytest.fixture
def aar_data() -> pd.DataFrame:
    return pd.DataFrame({
        "URN": [100150, 100152, 100153],
        "Academy UPIN": [111443, 111451, 111453],
        "In year balance": [1000, 1001, -1002],
        "PFI": ["Not part of PFI", "Not part of PFI", "Part of PFI"],
        "Lead UPIN": [137157, 137157, 135112],
        "DFE/EFA Revenue grants (includes Coronavirus Government Funding": [1000, 1001, 1002],
        "of which: Coronavirus Government Funding": [1000, 1001, 1002],
        "SEN funding": [1000, 1001, 1002],
        "Other DfE/EFA Revenue Grants": [1000, 1001, 1002],
        "Other income - LA & other Government grants": [1000, 1001, 1002],
        "Government source, non-grant": [1000, 1001, 1002],
        "Academies": [1000, 1001, 1002],
        "Non-Government": [1000, 1001, 1002],
        "All income from facilities and services": [1000, 1001, 1002],
        "Income from catering": [1000, 1001, 1002],
        "Receipts from supply teacher insurance claims": [1000, 1001, 1002],
        "Donations and/or voluntary funds": [1000, 1001, 1002],
        "Other self-generated income": [1000, 1001, 1002],
        "Investment income": [1000, 1001, 1002],
        "Teaching staff": [1000, 1001, 1002],
        "Supply teaching staff": [1000, 1001, 1002],
        "Education support staff": [1000, 1001, 1002],
        "Administrative and clerical staff": [1000, 1001, 1002],
        "Premises staff": [1000, 1001, 1002],
        "Catering staff": [1000, 1001, 1002],
        "Other staff": [1000, 1001, 1002],
        "Indirect employee expenses": [1000, 1001, 1002],
        "Staff development and training": [1000, 1001, 1002],
        "Staff-related insurance": [1000, 1001, 1002],
        "Supply teacher insurance": [1000, 1001, 1002],
        "Building and Grounds maintenance and improvement": [1000, 1001, 1002],
        "Cleaning and caretaking": [1000, 1001, 1002],
        "Water and sewerage": [1000, 1001, 1002],
        "Energy": [1000, 1001, 1002],
        "Rent and Rates": [1000, 1001, 1002],
        "Other occupation costs": [1000, 1001, 1002],
        "Special facilities": [1000, 1001, 1002],
        "Learning resources (not ICT equipment)": [1000, 1001, 1002],
        "ICT learning resources": [1000, 1001, 1002],
        "Examination fees": [1000, 1001, 1002],
        "Educational Consultancy": [1000, 1001, 1002],
        "Administrative supplies - non educational": [1000, 1001, 1002],
        "Agency supply teaching staff": [1000, 1001, 1002],
        "Catering supplies": [1000, 1001, 1002],
        "Other insurance premiums": [1000, 1001, 1002],
        "Legal & Professionalservices": [1000, 1001, 1002],
        "Auditor costs": [1000, 1001, 1002],
        "Interest charges for Loan and Bank": [1000, 1001, 1002],
        "Direct revenue financing - Revenue contributions to capital": [1000, 1001, 1002],
        "PFI Charges": [1000, 1001, 1002],
        "Revenue reserve": [1000, 1001, 1002],
        "Total Grant Funding": [1000, 1001, 1002],
        "Direct Grants": [1000, 1001, 1002],
        "Community Grants": [1000, 1001, 1002],
        "Targeted Grants": [1000, 1001, 1002],
        "Total Self Generated Funding": [1000, 1001, 1002],
        "Total Income": [1000, 1001, 1002],
        "Supply Staff Costs": [1000, 1001, 1002],
        "Other Staff Costs": [1000, 1001, 1002],
        "Total Staff Costs": [1000, 1001, 1002],
        "Maintenance & Improvement Costs": [1000, 1001, 1002],
        "Premises Costs": [1000, 1001, 1002],
        "Catering Expenses": [1000, 1001, 1002],
        "Occupation Costs": [1000, 1001, 1002],
        "Total Costs of Supplies and Services": [1000, 1001, 1002],
        "Total Costs of Educational Supplies": [1000, 1001, 1002],
        "Costs of Brought in Professional Services": [1000, 1001, 1002],
        "Total Expenditure": [1000, 1001, 1002],
        "Share of Revenue Reserve, distributed on per pupil basis\n": [1000, 1001, 1002],
        "London Weighting": ["Neither", "Outer", "Inner"],
    })


@pytest.fixture
def aar_central_services_data() -> pd.DataFrame:
    return pd.DataFrame({
        "Lead UPIN": [137157, 137157, 135112],
        "In Year Balance": [1000, 1001, -1002],
    })




@pytest.fixture
def prepared_aar_data(aar_data: pd.DataFrame, aar_central_services_data: pd.DataFrame) -> pd.DataFrame:
    output = BytesIO()
    writer = pd.ExcelWriter(output)
    aar_data.to_excel(writer, sheet_name="Academies", index=False)
    aar_central_services_data.to_excel(writer, sheet_name="CentralServices", index=False)
    writer.close()
    output.seek(0)

    return prepare_aar_data(output)
