import numpy as np
import pandas as pd

from pipeline.pre_processing.common import mappings
from pipeline.utils.log import setup_logger

logger = setup_logger(__name__)


def build_aar_transparency_file(academies: pd.DataFrame, year: int) -> pd.DataFrame:
    """
    Generates the AAR transparency file from the pre-processed academies DataFrame.
    Should work 2025 onwards.
    """
    df = academies.copy().reset_index()

    # Calculate additional/extra fields specified in SQL
    df["LAEstab"] = (df["LA"].astype(str) + df["Estab"].astype(str)).astype(int)
    df["In year balance"] = df["Total Income"] - df["Total Expenditure"]
    df["% of pupils who are Boarders"] = np.where(
        df["Total pupils"] > 1,
        df["total boarders"] / df["Number of pupils (headcount)"] * 100,
        0,
    )
    borough_conditions = [
        df["LA"].isin(
            [201, 202, 204, 205, 206, 207, 208, 209, 210, 211, 212, 213, 309, 316]
        ),
        df["LA"].isin(
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
    df["London Borough"] = np.select(
        borough_conditions, borough_choices, default="Neither"
    )

    mats_and_sats = df["Company Registration Number"].value_counts()
    mats = mats_and_sats[mats_and_sats > 1].index
    df["MAT SAT or Central Services"] = np.where(
        df["Company Registration Number"].isin(mats),
        "Multi Academy Trust (MAT)",
        "Single Academy Trust (SAT)",
    )

    # Transparency file rollups
    df["Community Grants"] = df[
        ["Income_Academies", "BNCH11163 (Non- Government)"]
    ].sum(axis=1)
    df["Supply Staff Costs"] = df[
        [
            "Teaching and Teaching support staff_Supply teaching staff",
            "Other costs_Supply teacher insurance",
            "Teaching and Teaching support staff_Agency supply teaching staff",
        ]
    ].sum(axis=1)
    df["Other Staff Costs"] = df[
        [
            "Non-educational support staff and services_Other staff",
            "Other costs_Indirect employee expenses",
            "Other costs_Staff development and training",
            "Other costs_Staff-related insurance",
        ]
    ].sum(axis=1)
    df["Maintenance & Improvement Costs"] = df[
        [
            "Premises staff and services_Maintenance of premises",
            "Other costs_Grounds maintenance",
        ]
    ].sum(axis=1)
    df["Premises Costs"] = df[
        [
            "Premises staff and services_Maintenance of premises",
            "Other costs_Grounds maintenance",
            "Premises staff and services_Premises staff",
            "Premises staff and services_Cleaning and caretaking",
            "Other costs_PFI charges",
        ]
    ].sum(axis=1)
    df["Catering Expenses"] = df[
        [
            "Catering staff and supplies_Catering staff",  # BNCH21106
            "Catering staff and supplies_Catering supplies",  # BNCH21701
        ]
    ].sum(axis=1)
    df["Occupation Costs"] = df[
        [
            "Catering staff and supplies_Catering staff",  # BNCH21106
            "Catering staff and supplies_Catering supplies",  # BNCH21701
            "Utilities_Water and sewerage",  # BNCH21402
            "Utilities_Energy",  # BNCH21403
            "Other costs_Rent and rates",  # BNCH21404
            "Premises staff and services_Other occupation costs",  # BNCH21406
            "Other costs_Other insurance premiums",  # BNCH21705
        ]
    ].sum(axis=1)

    df["Total Costs of Supplies and Services"] = df[
        [
            "Educational supplies_Learning resources (not ICT equipment)",  # BNCH21601
            "Educational ICT_ICT learning resources",  # BNCH21602
            "Educational supplies_Examination fees",  # BNCH21603
            "Teaching and Teaching support staff_Educational consultancy",  # BNCH21604
            "Administrative supplies_Administrative supplies (non educational)",  # BNCH21706
            "Non-educational support staff and services_Professional services (non-curriculum)",  # BNCH21702
            "Non-educational support staff and services_Audit cost",  # BNCH21703
        ]
    ].sum(axis=1)

    df["Total Costs of Educational Supplies"] = df[
        [
            "Educational supplies_Learning resources (not ICT equipment)",  # BNCH21601
            "Educational ICT_ICT learning resources",  # BNCH21602
            "Educational supplies_Examination fees",  # BNCH21603
        ]
    ].sum(axis=1)

    df["Costs of Brought in Professional Services"] = df[
        [
            "Teaching and Teaching support staff_Educational consultancy",  # BNCH21604
            "Non-educational support staff and services_Professional services (non-curriculum)",  # BNCH21702
            "Non-educational support staff and services_Audit cost",  # BNCH21703
        ]
    ].sum(axis=1)

    # Map the columns exactly as requested in the SQL file, and handle missing ones
    transparency_cols = {
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
        "Total School Workforce (Full-Time Equivalent)": "Number of teachers in academy (FTE)",
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
        "Income_Other DFE grants": "DfE revenue grants - other",
        "Income_Pre Post 16": "16 to 19 allocations",
        "Income_Other DFE grants": "Other DfE Group grants (revenue)",
        "BNCH11141 (SEN)": "SEN funding",
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
        "BNCH11122 (Other)": "DFE Revenue grants",
        "Income_Direct grants": "Direct Grants",
        "Community Grants": "Community Grants",
        "Income_Total self generated funding": "Total Self Generated Funding",
        "Total Income": "Total Income",
        "Supply Staff Costs": "Supply Staff Costs",
        "Other Staff Costs": "Other Staff Costs",
        "Teaching and Teaching support staff_Total": "Total Staff Costs",
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
    for col in transparency_cols.keys():
        if col not in df.columns:
            logger.info(
                f"Column {col} not found for AAR transparency file, setting to nan"
            )
            df[col] = pd.NA

    transparency_df = df[list(transparency_cols.keys())].rename(
        columns=transparency_cols
    )

    return transparency_df


def build_aar_central_services_transparency_file(
    trusts: pd.DataFrame, academies: pd.DataFrame, central_services: pd.DataFrame | None
) -> pd.DataFrame:
    """
    Generates the AAR Central Services (Trust) transparency file from the pre-processed trusts and academies DataFrames.
    """
    df = trusts.copy()
    academies_copy = academies.copy()
    central_services_copy = central_services.copy()
    central_services_copy["Company Registration Number"] = central_services_copy[
        "Company Registration Number"
    ].map(mappings.map_company_number)

    # Get MAT level variables from academies (distinct for trust level mapping)
    trust_meta = (
        academies_copy.groupby("Company Registration Number")
        .agg(
            # uid=("UID", "first"),
            trust_name=("Trust Name", "first"),
        )
        .reset_index()
    )

    df = df.merge(trust_meta, on="Company Registration Number", how="left")

    # Replicate logic for CCAdditionalData percentages from academies dataframe
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
    ).reset_index()

    df = df.merge(trust_pupil_counts, on="Company Registration Number", how="left")

    df["%_pupils_FSM"] = (df["total_fsm"] / df["total_pupils"].replace(0, pd.NA)) * 100
    df["%_pupils_EHCP"] = (
        df["total_ehcp"] / df["total_pupils"].replace(0, pd.NA)
    ) * 100
    df["%_pupils_SEN"] = (df["total_sen"] / df["total_pupils"].replace(0, pd.NA)) * 100
    df["%_pupils_EAL"] = (df["total_eal"] / df["total_pupils"].replace(0, pd.NA)) * 100
    df["%_pupils_boarders"] = (
        df["total_boarders"] / df["total_pupils"].replace(0, pd.NA)
    ) * 100

    # Calculate Central Services RR per pupil based on prop_pupils
    df[
        "Central Services Revenue Reserve per pupil at end of period based on time in trust"
    ] = (df["Revenue reserve"] / df["total_prop_pupils"])

    df["Sum of Trust + Academy Revenue Reserve"] = (
        df["sum_of_academy_rr"] + df["Revenue reserve"]
    )
    df["trust_type"] = "Central services"

    # Grab some items from the CS return not used in FBIT eg the BNCH codes here
    df = df.merge(
        central_services_copy,
        on="Company Registration Number",
        how="left",
        suffixes=["", "_cs"],
    )
    cs_cols = {
        "trust_type": "MAT SAT or Central Services",
        "uid": "UID",
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
        "Teaching and Teaching support staff_Total": "Staff Total",
        "Maintenance & Improvement": "Maintenance & Improvement",
        "Premises": "Premises",
        "Catering staff and supplies_Total": "Catering Exp",
        "Occupation": "Occupation",
        "Supplies and Services": "Supplies and Services",
        "Educational supplies_Total": "Educational Supplies",
        "Brought in Professional Services": "Bought in Professional Services",
        "Total Expenditure": "Total Expenditure",
    }

    # Transparency file rollups
    df["Maintenance & Improvement"] = df[
        "Premises staff and services_Maintenance of premises"
    ].fillna(0) + df["Other costs_Grounds maintenance"].fillna(0)
    df["Premises"] = (
        df["Premises staff and services_Cleaning and caretaking"].fillna(0)
        + df["Utilities_Water and sewerage"].fillna(0)
        + df["Utilities_Energy"].fillna(0)
        + df["Other costs_Rent and rates"].fillna(0)
    )
    df["Occupation"] = df["Premises staff and services_Other occupation costs"].fillna(
        0
    )
    df["Supplies and Services"] = df[
        "Administrative supplies_Administrative supplies (non educational)"
    ].fillna(0)
    df["Brought in Professional Services"] = df[
        "Non-educational support staff and services_Professional services (non-curriculum)"
    ].fillna(0) + df["Non-educational support staff and services_Audit cost"].fillna(0)

    # Keep only the matched columns, handling any missing gracefully
    for col in cs_cols.keys():
        if col not in df.columns:
            logger.info(
                f"Column {col} not found for AAR CS transparency file, setting to nan"
            )
            df[col] = pd.NA

    transparency_df = df[list(cs_cols.keys())].rename(columns=cs_cols)

    return transparency_df
