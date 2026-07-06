import numpy as np
import pandas as pd

from pipeline.pre_processing.common import mappings
from pipeline.utils.log import setup_logger
from .rollups import calculate_transparency_file_rollups, calculate_cs_transparency_file_rollups

logger = setup_logger(__name__)


def build_aar_transparency_file(
        academies_with_apportionments: pd.DataFrame, 
        academies_preprocessed: pd.DataFrame,
    ) -> pd.DataFrame:
    """
    Generates the AAR transparency file from the pre-processed academies DataFrame.
    Should work 2025 onwards.
    """
    df_apportioned = academies_with_apportionments.copy().reset_index()
    df_raw = academies_preprocessed.copy()

    # Calculate additional/extra fields specified in SQL
    df_apportioned["LAEstab"] = (df_apportioned["LA"].astype(str) + df_apportioned["Estab"].astype(str)).astype(int)
    df_apportioned["% of pupils who are Boarders"] = np.where(
        df_apportioned["Total pupils"] > 1,
        df_apportioned["total boarders"] / df_apportioned["Number of pupils (headcount)"] * 100,
        0,
    )
    borough_conditions = [
        df_apportioned["LA"].isin(
            [201, 202, 204, 205, 206, 207, 208, 209, 210, 211, 212, 213, 309, 316]
        ),
        df_apportioned["LA"].isin(
            [
                203,
                301,
                302,
                303,
                304,
                305,
                306,
                307,
                308,
                310,
                311,
                312,
                313,
                314,
                315,
                317,
                318,
                319,
                320,
            ]
        ),
    ]
    borough_choices = ["Inner", "Outer"]
    df_apportioned["London Borough"] = np.select(
        borough_conditions, borough_choices, default="Neither"
    )
    df_apportioned["Is PFI"] = df_apportioned["Is PFI"].map(mappings.map_is_pfi_school)
    mats_and_sats = df_apportioned["Company Registration Number"].value_counts()
    mats = mats_and_sats[mats_and_sats > 1].index
    df_apportioned["MAT SAT or Central Services"] = np.where(
        df_apportioned["Company Registration Number"].isin(mats),
        "Multi Academy Trust (MAT)",
        "Single Academy Trust (SAT)",
    )

    # Copy a few financial FBIT datapoints
    df_raw["Trust Revenue reserve_CS"] = df_apportioned["Trust Revenue reserve_CS"]
    df_raw["Academy Revenue Reserve"] = df_apportioned["Academy Revenue Reserve"]

    df_raw = calculate_transparency_file_rollups(df_raw)

    nonfinancial_cols = {
        "LAEstab": "LAEstab",
        "LA Code": "LA",
        "Estab": "Estab",
        "URN": "URN",
        "Academy UPIN": "Academy UPIN",
        "EstablishmentName": "School Name",
        "Period covered by return": "Period covered by return",
        "MAT SAT or Central Services": "MAT SAT or Central Services",
        "Group Identifier": "UID",
        "Company Registration Number": "Company Number",
        "Trust Name": "Trust or Company Name",
        "OpenDate": "Date opened",
        "CloseDate": "Date closed",
        "Date left or closed if in period": "Date left or closed if in period",
        "Date joined or opened if in period": "Date joined or opened if in period",
        "Number of pupils": "Number of pupils in academy (FTE) plus dual subsidiary registrations",
        "Total Number of Teachers (Full-Time Equivalent)": "Number of teachers in academy (FTE)",
        "Gender (name)": "Gender",
        "Overall Phase": "Overall Phase",
        "PhaseOfEducation (name)": "Phase",
        "TypeOfEstablishment (name)": "Type",
        "UrbanRural (name)": "Urban/Rural",
        "GOR (name)": "Region",
        "London Borough": "London Borough",
        "London Weighting": "London Weighting",
        "AdmissionsPolicy (name)": "Admissions policy",
        "Is PFI": "PFI",
        "Percentage Free school meals": "% of pupils eligible for FSM",
        "Percentage with EHC": "% of pupils with EHCP",
        "Percentage SEN": "% of pupil with SEN support",
        "% of pupils whose first language is known or believed to be other than English": "% of pupils with English as an additional language",
        "% of pupils who are Boarders": "% of pupils who are Boarders",
        "Has Sixth Form": "Has a 6th form",
        "TotalPupilsSixthForm": "No of pupils in 6th form",
        "StatutoryLowAge": "Lowest age of pupils",
        "StatutoryHighAge": "Highest age of pupils",
    }
    financial_cols = {
        "DFE/EFA Revenue grants": "DFE Revenue grants",
        "BNCH11123-BAI011-A (Academies - Income)": "16 to 19 allocations",
        "BNCH11131 (DfE Family Revenue Grants)": "Other DfE Group grants (revenue)",
        "Income_Targeted grants": "SEN Funding",
        "Income_Other DFE grants": "Other DfE Revenue Grants",
        "BNCH11101 (Start-up grants)": "Grants for trust activity",
        "Income_Other grants": "Other income - LA & other Government grants",
        "Income_Government source": "Government source (non-grant)",
        "Income_Academies": "Academies",
        "BNCH11163 (Non- Government)": "Non-Government",
        "BNCH11201 (Income from facilities and services)": "Income from facilities and services - rents & letting",
        "BNCH11205 (Other Income from facilities and services)": "Income from facilities and services - other",
        "Income_Catering services": "Income from catering",
        "Income_Receipts supply teacher insurance": "Receipts from supply teacher insurance claims",
        "Income_Other self-generated income": "Other self-generated income",
        "Income_Donations and voluntary funds": "Donations and/or voluntary funds",
        "Income_Investment income": "Investment income",
        "Teaching and Teaching support staff_Teaching staff": "Teaching staff",
        "Teaching and Teaching support staff_Supply teaching staff": "Supply teaching staff",
        "Teaching and Teaching support staff_Education support staff": "Education support staff",
        "Non-educational support staff and services_Administrative and clerical staff": "Administrative and clerical staff",
        "Premises staff and services_Premises staff": "Premises staff",
        "Catering staff and supplies_Catering staff": "Catering staff",
        "Non-educational support staff and services_Other staff": "Other staff",
        "Other costs_Indirect employee expenses": "Indirect employee expenses",
        "Other costs_Staff development and training": "Staff development and training",
        "Other costs_Supply teacher insurance": "Supply teacher insurance",
        "Other costs_Staff-related insurance": "Staff-related insurance",
        "Premises staff and services_Maintenance of premises": "Maintenance of premises",
        "Premises staff and services_Cleaning and caretaking": "Cleaning and caretaking",
        "Utilities_Water and sewerage": "Water and sewerage",
        "Utilities_Energy": "Energy",
        "Other costs_Rent and rates": "Rent and Rates",
        "Other costs_Grounds maintenance": "Grounds maintenance",
        "Premises staff and services_Other occupation costs": "Other occupation costs",
        "Other costs_Special facilities": "Special facilities",
        "Educational supplies_Learning resources (not ICT equipment)": "Learning resources (not ICT equipment)",
        "Educational ICT_ICT learning resources": "ICT learning resources",
        "Educational supplies_Examination fees": "Examination fees",
        "Teaching and Teaching support staff_Educational consultancy": "Educational Consultancy",
        "Teaching and Teaching support staff_Agency supply teaching staff": "Agency supply teaching staff",
        "Catering staff and supplies_Catering supplies": "Catering supplies",
        "Non-educational support staff and services_Audit cost": "Auditor costs",
        "Other costs_Other insurance premiums": "Other insurance premiums",
        "Administrative supplies_Administrative supplies (non educational)": "Administrative supplies - non educational",
        "Income_Direct revenue finance": "Direct revenue financing (Revenue contributions to capital)",
        "Non-educational support staff and services_Professional services (non-curriculum)": "Legal & Professional services",
        "Other costs_PFI charges": "PFI Charges",
        "Other costs_Interest charges for loan and bank": "Interest charges for Loan and Bank",
        "Revenue reserve": "Revenue Reserve",
        "Trust Revenue reserve_CS": "Share of Central Services Revenue Reserve, distributed on per pupil basis, at end of period based on time spent in trust",
        "In year balance": "In year balance",
        "Income_Total grant funding": "Total Grant Funding",
        "BNCH11122 (Other)": "DFE/EFA Revenue grants",
        "Income_Direct grants": "Direct Grants",
        "Community Grants": "Community Grants",
        "Income_Total self generated funding": "Total Self Generated Funding",
        "Total Income": "Total Income",
        "Supply Staff Costs": "Supply Staff Costs",
        "Other Staff Costs": "Other Staff Costs",
        "Total Staff Costs": "Total Staff Costs",
        "Maintenance & Improvement Costs": "Maintenance & Improvement Costs",
        "Premises Costs": "Premises Costs",
        "Catering Expenses": "Catering Expenses",
        "Occupation Costs": "Occupation Costs",
        "Total Costs of Supplies and Services": "Total Costs of Supplies and Services",
        "Total Costs of Educational Supplies": "Total Costs of Educational Supplies",
        "Costs of Brought in Professional Services": "Costs of Brought in Professional Services",
        "Total Expenditure": "Total Expenditure",
        "Academy Revenue Reserve": "RRpropperpupil",
    }

    # Handle any missing gracefully by creating them as empty or null
    for col in nonfinancial_cols.keys():
        if col not in df_apportioned.columns:
            logger.info(
                f"Column {col} not found for AAR transparency file, setting to nan"
            )
            df_apportioned[col] = pd.NA

    for col in financial_cols.keys():
        if col not in df_raw.columns:
            logger.info(
                f"Column {col} not found for AAR transparency file, setting to nan"
            )
            df_raw[col] = pd.NA
    
    financial_transparency_df = df_raw[list(financial_cols.keys())].rename(
        columns=financial_cols
    )
    nonfinancial_transparency_df = df_apportioned[list(nonfinancial_cols.keys())].rename(
        columns=nonfinancial_cols
    )

    transparency_df = pd.merge(financial_transparency_df, nonfinancial_transparency_df, 
                               left_index=True, right_on="URN", how="left")
    transparency_df = transparency_df.set_index("URN").sort_index()

    return transparency_df


def build_aar_central_services_transparency_file(
    trusts: pd.DataFrame, academies: pd.DataFrame, central_services_raw: pd.DataFrame
) -> pd.DataFrame:
    """
    Generates the AAR Central Services (Trust) transparency file from the pre-processed trusts and academies DataFrames.
    """
    trusts_apportioned = trusts.copy()
    academies_copy = academies.copy()
    central_services_copy = central_services_raw.copy()

    central_services_copy["Company Registration Number"] = central_services_copy[
        "Company Registration Number"
    ].map(mappings.map_company_number)
    central_services_copy = calculate_cs_transparency_file_rollups(central_services_copy)

    grouped = academies_copy.groupby("Company Registration Number")
    trust_pupil_counts = grouped.agg(
        total_pupils=("Number of pupils", "sum"),
        total_fsm=(
            "number of pupils known to be eligible for free school meals",
            "sum",
        ),
        total_ehcp=("EHC plan", "sum"),
        total_sen=("SEN support", "sum"),
        total_eal=(
            "number of pupils whose first language is known or believed to be other than English",
            "sum",
        ),
        total_boarders=("total boarders", "sum"),
        total_sixth_form=("TotalPupilsSixthForm", "sum"),
        total_teachers=("Total Number of Teachers (Headcount)", "sum"),
        total_prop_pupils=("Number of pupils_pro_rata_end_of_period", "sum"),
        sum_of_academy_rr=("Academy Revenue Reserve", "sum"),
        trust_name=("Trust Name", "first")
    ).reset_index()

    trusts_apportioned = trusts_apportioned.merge(trust_pupil_counts, on="Company Registration Number", how="left")

    trusts_apportioned["%_pupils_FSM"] = (trusts_apportioned["total_fsm"] / trusts_apportioned["total_pupils"].replace(0, pd.NA)) * 100
    trusts_apportioned["%_pupils_EHCP"] = (
        trusts_apportioned["total_ehcp"] / trusts_apportioned["total_pupils"].replace(0, pd.NA)
    ) * 100
    trusts_apportioned["%_pupils_SEN"] = (trusts_apportioned["total_sen"] / trusts_apportioned["total_pupils"].replace(0, pd.NA)) * 100
    trusts_apportioned["%_pupils_EAL"] = (trusts_apportioned["total_eal"] / trusts_apportioned["total_pupils"].replace(0, pd.NA)) * 100
    trusts_apportioned["%_pupils_boarders"] = (
        trusts_apportioned["total_boarders"] / trusts_apportioned["total_pupils"].replace(0, pd.NA)
    ) * 100

    trusts_apportioned[
        "Central Services Revenue Reserve per pupil at end of period based on time in trust"
    ] = (trusts_apportioned["Revenue reserve"] / trusts_apportioned["total_prop_pupils"])
    trusts_apportioned["Sum of Trust + Academy Revenue Reserve"] = trusts_apportioned["Revenue reserve"]
    trusts_apportioned["trust_type"] = "Central services"

    cs_transparency_file_df = pd.merge(
        central_services_copy, 
        trusts_apportioned,
        on="Company Registration Number",
        how="left",
        suffixes=["", "_app"]
    )
    cs_transparency_file_cols = {
        "trust_type": "MAT SAT or Central Services",
        "Company Registration Number": "Company Number",
        "trust_name": "Trust or Company Name",
        "%_pupils_FSM": "Percentage of pupils in trust eligible for FSM",
        "%_pupils_EHCP": "Percentage of pupils in trust with EHCP",
        "%_pupils_SEN": "Percentage of pupils in trust with SEN support",
        "%_pupils_EAL": "Percentage of pupils in trust with EAL",
        "%_pupils_boarders": "Percentage of pupils in trust who are boarders",
        "total_sixth_form": "Total number of sixth form pupils in trust",
        "total_teachers": "Total teachers in trust",
        "Income_Targeted grants": "SEN",
        "Income_Other DFE grants": "Other DfE/EFA Revenue Grants",
        "BNCH11101 (Start-up grants)": "Start-up grants",
        "Income_Other grants": "Other income (LA & other Government grants)",
        "Income_Government source": "Government source, non-grant",
        "Income_Academies": "Academies",
        "BNCH11163 (Non- Government)": "Non-Government",
        "BNCH11201 (Income from facilities and services)": "Income from Facilities and Services",
        "BNCH11205 (Other Income from facilities and services)": "Other Income from Facilities and Services",
        "Income_Catering services": "Income from catering",
        "Income_Receipts supply teacher insurance": "Receipts from supply teacher insurance claims",
        "Income_Other self-generated income": "Other self-generated income",
        "Income_Donations and voluntary funds": "Donations and/or voluntary funds",
        "Income_Investment income": "Investment income",
        "Teaching and Teaching support staff_Teaching staff": "Teaching staff",
        "Teaching and Teaching support staff_Supply teaching staff": "Supply teaching staff",
        "Teaching and Teaching support staff_Education support staff": "Education support staff",
        "Non-educational support staff and services_Administrative and clerical staff": "Administrative and clerical staff",
        "Premises staff and services_Premises staff": "Premises staff",
        "Catering staff and supplies_Catering staff": "Catering staff",
        "Non-educational support staff and services_Other staff": "Other staff",
        "Other costs_Indirect employee expenses": "Indirect employee expenses",
        "Other costs_Staff development and training": "Staff development and training",
        "Other costs_Supply teacher insurance": "Supply teacher insurance",
        "Other costs_Staff-related insurance": "Staff-related insurance",
        "Premises staff and services_Maintenance of premises": "Maintainance of premises",
        "Premises staff and services_Cleaning and caretaking": "Cleaning and caretaking",
        "Utilities_Water and sewerage": "Water and sewerage",
        "Utilities_Energy": "Energy",
        "Other costs_Rent and rates": "Rent and Rates",
        "Other costs_Grounds maintenance": "Grounds Maintenance",
        "Premises staff and services_Other occupation costs": "Other occupation costs",
        "Other costs_Special facilities": "Special facilities",
        "Educational supplies_Learning resources (not ICT equipment)": "Learning resources (not ICT equipment)",
        "Educational ICT_ICT learning resources": "ICT learning resources",
        "Educational supplies_Examination fees": "Examination fees",
        "Teaching and Teaching support staff_Educational consultancy": "Educational Consultancy",
        "Teaching and Teaching support staff_Agency supply teaching staff": "Agency supply teaching staff",
        "Catering staff and supplies_Catering supplies": "Catering supplies",
        "Non-educational support staff and services_Audit cost": "Auditor costs",
        "Other costs_Other insurance premiums": "Other insurance premiums",
        "Administrative supplies_Administrative supplies (non educational)": "Administrative supplies - non educational",
        "Income_Direct revenue finance": "Direct revenue financing (Revenue contributions to capital)",
        "Non-educational support staff and services_Professional services (non-curriculum)": "Legal & Professional",
        "Other costs_PFI charges": "PFI Charges",
        "Other costs_Interest charges for loan and bank": "Interest charges for Loan and Bank",
        "Revenue reserve": "Revenue Reserve",
        "In year balance": "In Year Balance",
        "total_pupils": "Total number of Pupils",
        "total_prop_pupils": "Number of pupils at end of period based on time in trust",
        "Central Services Revenue Reserve per pupil at end of period based on time in trust": "Central Services Revenue Reserve per pupil at end of period based on time in trust",
        "sum_of_academy_rr": "Sum of Academy RR",
        "Sum of Trust + Academy Revenue Reserve": "Sum of Trust + Academy Revenue Reserve",
        "Income_Total grant funding": "Grant Funding",
        "Income_Direct grants": "Direct Grant",
        "Income_Other Revenue Income": "Community Grants",
        "Income_Total self generated funding": "Self Generated Funding",
        "Total Income": "Total Income",
        "Teaching and Teaching support staff_Supply teaching staff": "Supply Staff",
        "Non-educational support staff and services_Other staff": "Other Staff Costs",
        "Staff Total": "Staff Total",
        "Maintenance & Improvement": "Maintenance & Improvement",
        "Premises": "Premises",
        "Catering Exp": "Catering Exp",
        "Occupation": "Occupation",
        "Supplies and Services": "Supplies and Services",
        "Educational Supplies": "Educational Supplies",
        "Brought in Professional Services": "Bought in Professional Services",
        "Total Expenditure": "Total Expenditure",
    }

    formatted_cs_transparency_file_df = cs_transparency_file_df[
        cs_transparency_file_cols.keys()].rename(cs_transparency_file_cols)
    
    return formatted_cs_transparency_file_df


