import json
import logging
import os

import numpy as np
import pandas as pd
import sqlalchemy
from sqlalchemy import URL, create_engine, event

logger = logging.getLogger("fbit-data-pipeline")

db_args = os.getenv("DB_ARGS")

args = []
for sub in db_args.split(";"):
    if "=" in sub:
        args.append(map(str.strip, sub.split("=", 1)))
args = dict(args)

connection_url = URL.create(
    "mssql+pyodbc",
    username=os.getenv("DB_USER"),
    password=os.getenv("DB_PWD"),
    host=os.getenv("DB_HOST"),
    database=os.getenv("DB_NAME"),
    port=os.getenv("DB_PORT"),
    query={"driver": "ODBC Driver 18 for SQL Server"} | args,
)

engine = create_engine(connection_url)


@event.listens_for(engine, "before_cursor_execute")
def receive_before_cursor_execute(
    conn, cursor, statement, params, context, executemany
):
    if executemany:
        cursor.fast_executemany = True


def upsert(df, table_name, keys: list[str], dtype: dict[str, any] = None):
    logger.info(f"Connecting to database {engine.url}")
    df.drop_duplicates(inplace=True)

    update_cols = []
    insert_cols = [*keys]
    insert_vals = [f"src.{key_name}" for key_name in keys]
    match_keys = [f"dest.{key_name}=src.{key_name}" for key_name in keys]

    for col in df.columns:
        if col in keys:
            continue
        update_cols.append("{col}=src.{col}".format(col=col))
        insert_cols.append(col)
        insert_vals.append("src.{col}".format(col=col))
    temp_table = f"{table_name}_temp"
    df.to_sql(temp_table, engine, if_exists="replace", index=True, dtype=dtype)
    update_stmt = f'MERGE {table_name} as dest USING {temp_table} as src ON {" AND ".join(match_keys)}  WHEN MATCHED THEN UPDATE SET {", ".join(update_cols)} WHEN NOT MATCHED BY TARGET THEN INSERT ({", ".join(insert_cols)}) VALUES ({", ".join(insert_vals)});'
    with engine.begin() as cnx:
        cnx.execute(sqlalchemy.text(update_stmt))
        cnx.execute(sqlalchemy.text(f"DROP TABLE IF EXISTS {temp_table}"))


def insert_comparator_set(run_type: str, set_type: str, run_id: str, df: pd.DataFrame):
    write_frame = df[["Pupil", "Building"]].copy()
    write_frame["RunType"] = run_type
    write_frame["SetType"] = set_type
    write_frame["RunId"] = str(run_id)
    write_frame["Pupil"] = write_frame["Pupil"].map(lambda x: json.dumps(x.tolist()))
    write_frame["Building"] = write_frame["Building"].map(
        lambda x: json.dumps(x.tolist())
    )

    upsert(write_frame, "ComparatorSet", keys=["RunType", "RunId", "URN", "SetType"])
    logger.info(
        f"Wrote {len(write_frame)} rows to comparator set {run_type} - {set_type} - {run_id}"
    )


def insert_metric_rag(run_type: str, set_type: str, run_id: str, df: pd.DataFrame):
    write_frame = df[
        [
            "Category",
            "SubCategory",
            "Value",
            "Mean",
            "DiffMean",
            "PercentDiff",
            "Percentile",
            "Decile",
            "RAG",
        ]
    ].copy()
    write_frame["RunType"] = run_type
    write_frame["RunId"] = str(run_id)
    write_frame["SetType"] = set_type

    upsert(
        write_frame,
        "MetricRAG",
        keys=["RunType", "RunId", "SetType", "URN", "Category", "SubCategory"],
        dtype={
            "RunType": sqlalchemy.types.VARCHAR(length=50),
            "RunId": sqlalchemy.types.VARCHAR(length=50),
            "URN": sqlalchemy.types.VARCHAR(length=6),
            "Category": sqlalchemy.types.VARCHAR(length=50),
            "SubCategory": sqlalchemy.types.VARCHAR(length=50),
            "SetType": sqlalchemy.types.VARCHAR(length=50),
            "Value": sqlalchemy.types.Numeric(16, 2),
            "Mean": sqlalchemy.types.Numeric(16, 2),
            "DiffMean": sqlalchemy.types.Numeric(16, 2),
            "PercentDiff": sqlalchemy.types.Numeric(16, 2),
            "Percentile": sqlalchemy.types.Numeric(16, 2),
            "Decile": sqlalchemy.types.Numeric(16, 2),
            "RAG": sqlalchemy.types.VARCHAR(length=10),
        },
    )
    logger.info(
        f"Wrote {len(write_frame)} rows to metric rag {run_type} - {set_type} - {run_id}"
    )


def insert_schools_and_trusts_and_local_authorities(
    run_type: str, year: str, df: pd.DataFrame
):
    projections = {
        "URN": "URN",
        "EstablishmentName": "SchoolName",
        "Company Registration Number": "TrustCompanyNumber",
        "Trust Name": "TrustName",
        "Federation Lead School URN": "FederationLeadURN",
        "Federation Name": "FederationLeadName",
        "LA Code": "LACode",
        "LA Name": "LAName",
        "London Weighting": "LondonWeighting",
        "Finance Type": "FinanceType",
        "Overall Phase": "OverallPhase",
        "TypeOfEstablishment (name)": "SchoolType",
        "Has Sixth Form": "HasSixthForm",
        "Has Nursery": "HasNursery",
        "Is PFI": "IsPFISchool",
        "OfstedLastInsp": "OfstedDate",
        "OfstedRating (name)": "OfstedDescription",
        "TelephoneNum": "Telephone",
        "SchoolWebsite": "Website",
        "Street": "AddressStreet",
        "Locality": "AddressLocality",
        "Address3": "AddressLine3",
        "Town": "AddressTown",
        "County (name)": "AddressCounty",
        "Postcode": "AddressPostcode",
    }

    write_frame = (
        df.reset_index()
        .rename(columns=projections)[[*projections.values()]]
        .drop_duplicates()
    )

    upsert(write_frame, "School", keys=["URN"])
    logger.info(f"Wrote {len(write_frame)} rows to school {run_type} - {year}")

    trust_projections = {
        "Trust Name": "TrustName",
        "Group UID": "UID",
        "CFO name": "CFOName",
        "CFO email": "CFOEmail",
        "OpenDate": "OpenDate",
        "Company Registration Number": "CompanyNumber",
    }

    trusts = (
        df[~df["Company Registration Number"].isna()]
        .reset_index()
        .sort_values(by=["Company Registration Number", "OpenDate"], ascending=False)
        .groupby(["Company Registration Number"])
        .first()
        .reset_index()
        .rename(columns=trust_projections)[[*trust_projections.values()]]
    )

    upsert(trusts, "Trust", keys=["CompanyNumber"])
    logger.info(f"Wrote {len(trusts)} rows to trust {run_type} - {year}")

    la_projections = {"LA Code": "Code", "LA Name": "Name"}

    las = (
        df.reset_index()[["LA Code", "LA Name"]]
        .rename(columns=la_projections)[[*la_projections.values()]]
        .drop_duplicates()
    )

    las.set_index("Code", inplace=True)

    upsert(las, "LocalAuthority", keys=["Code"])
    logger.info(f"Wrote {len(las)} rows to LAs {run_type} - {year}")


def insert_non_financial_data(run_type: str, year: str, df: pd.DataFrame):
    projections = {
        "URN": "URN",
        "TypeOfEstablishment (name)": "EstablishmentType",
        "Total Internal Floor Area": "TotalInternalFloorArea",
        "Building Age": "BuildingAverageAge",
        "Number of pupils": "TotalPupils",
        "TotalPupilsSixthForm": "TotalPupilsSixthForm",
        "TotalPupilsNursery": "TotalPupilsNursery",
        "Total School Workforce (Headcount)": "WorkforceHeadcount",
        "Total School Workforce (Full-Time Equivalent)": "WorkforceFTE",
        "Total Number of Teachers (Headcount)": "TeachersHeadcount",
        "Total Number of Teachers (Full-Time Equivalent)": "TeachersFTE",
        "Total Number of Teachers in the Leadership Group (Headcount)": "SeniorLeadershipHeadcount",
        "Total Number of Teachers in the Leadership Group (Full-time Equivalent)": "SeniorLeadershipFTE",
        "Total Number of Teaching Assistants (Headcount)": "TeachingAssistantHeadcount",
        "Total Number of Teaching Assistants (Full-Time Equivalent)": "TeachingAssistantFTE",
        "NonClassroomSupportStaffHeadcount": "NonClassroomSupportStaffHeadcount",
        "NonClassroomSupportStaffFTE": "NonClassroomSupportStaffFTE",
        "Total Number of Auxiliary Staff (Headcount)": "AuxiliaryStaffHeadcount",
        "Total Number of Auxiliary Staff (Full-Time Equivalent)": "AuxiliaryStaffFTE",
        "Teachers with Qualified Teacher Status (%) (Headcount)": "PercentTeacherWithQualifiedStatus",
        "Percentage Free school meals": "PercentFreeSchoolMeals",
        "Percentage SEN": "PercentSpecialEducationNeeds",
        "Percentage with EHC": "PercentWithEducationalHealthCarePlan",
        "Percentage without EHC": "PercentWithoutEducationalHealthCarePlan",
        "Ks2Progress": "KS2Progress",
        "Progress8Measure": "KS4Progress",
        "Percentage Primary Need VI": "PercentWithVI",
        "Percentage Primary Need SPLD": "PercentWithSPLD",
        "Percentage Primary Need SLD": "PercentWithSLD",
        "Percentage Primary Need SLCN": "PercentWithSLCN",
        "Percentage Primary Need SEMH": "PercentWithSEMH",
        "Percentage Primary Need PMLD": "PercentWithPMLD",
        "Percentage Primary Need PD": "PercentWithPD",
        "Percentage Primary Need OTH": "PercentWithOTH",
        "Percentage Primary Need MSI": "PercentWithMSI",
        "Percentage Primary Need MLD": "PercentWithMLD",
        "Percentage Primary Need HI": "PercentWithHI",
        "Percentage Primary Need ASD": "PercentWithASD",
    }

    write_frame = df.reset_index().rename(columns=projections)[[*projections.values()]]

    write_frame["RunType"] = run_type
    write_frame["RunId"] = str(year)
    write_frame.set_index("URN", inplace=True)
    write_frame.replace({np.inf: np.nan, -np.inf: np.nan}, inplace=True)

    upsert(write_frame, "NonFinancial", keys=["RunType", "RunId", "URN"])
    logger.info(
        f"Wrote {len(write_frame)} rows to non-financial data {run_type} - {year}"
    )


def insert_financial_data(run_type: str, year: str, df: pd.DataFrame):
    projections = {
        "URN": "URN",
        "TypeOfEstablishment (name)": "EstablishmentType",
        "Period covered by return": "PeriodCoveredByReturn",
        "Financial Position": "FinancialPosition",
        "Trust Financial Position": "TrustPosition",
        "Income_Total": "TotalIncome",
        "Total Expenditure": "TotalExpenditure",
        "Total Internal Floor Area": "TotalInternalFloorArea",
        "Number of pupils": "TotalPupils",
        "In year balance": "InYearBalance",
        "Revenue reserve": "RevenueReserve",
        "Income_Total grant funding": "TotalGrantFunding",
        "Income_Total self generated funding": "TotalSelfGeneratedFunding",
        "Income_Direct grants": "DirectGrants",
        "Income_Targeted grants": "TargetedGrants",
        "Income_Other DFE grants": "OtherDfeGrants",
        "Income_Other grants": "OtherIncomeGrants",
        "Income_Government source": "GovernmentSource",
        "Income_Other Revenue Income": "CommunityGrants",
        "Income_Academies": "Academies",
        "Income_Facilities and services": "IncomeFacilitiesServices",
        "Income_Catering services": "IncomeCateringServices",
        "Income_Donations and voluntary funds": "DonationsVoluntaryFunds",
        "Income_Receipts supply teacher insurance": "ReceiptsSupplyTeacherInsuranceClaims",
        "Income_Investment income": "InvestmentIncome",
        "Income_Other self-generated income": "OtherSelfGeneratedIncome",
        "Income_Direct revenue finance": "DirectRevenueFinancing",
        "Teaching and Teaching support staff_Total": "TotalTeachingSupportStaffCosts",
        "Teaching and Teaching support staff_Teaching staff": "TeachingStaffCosts",
        "Teaching and Teaching support staff_Supply teaching staff": "SupplyTeachingStaffCosts",
        "Teaching and Teaching support staff_Educational consultancy": "EducationalConsultancyCosts",
        "Teaching and Teaching support staff_Education support staff": "EducationSupportStaffCosts",
        "Teaching and Teaching support staff_Agency supply teaching staff": "AgencySupplyTeachingStaffCosts",
        "Non-educational support staff and services_Total": "TotalNonEducationalSupportStaffCosts",
        "Non-educational support staff and services_Administrative and clerical staff": "AdministrativeClericalStaffCosts",
        "Non-educational support staff and services_Audit cost": "AuditorsCosts",
        "Non-educational support staff and services_Other staff": "OtherStaffCosts",
        "Non-educational support staff and services_Professional services (non-curriculum)": "ProfessionalServicesNonCurriculumCosts",
        "Educational supplies_Total": "TotalEducationalSuppliesCosts",
        "Educational supplies_Examination fees": "ExaminationFeesCosts",
        "Educational supplies_Learning resources (not ICT equipment)": "LearningResourcesNonIctCosts",
        "Educational ICT_ICT learning resources": "LearningResourcesIctCosts",
        "Premises staff and services_Total": "TotalPremisesStaffServiceCosts",
        "Premises staff and services_Cleaning and caretaking": "CleaningCaretakingCosts",
        "Premises staff and services_Maintenance of premises": "MaintenancePremisesCosts",
        "Premises staff and services_Other occupation costs": "OtherOccupationCosts",
        "Premises staff and services_Premises staff": "PremisesStaffCosts",
        "Utilities_Total": "TotalUtilitiesCosts",
        "Utilities_Energy": "EnergyCosts",
        "Utilities_Water and sewerage": "WaterSewerageCosts",
        "Administrative supplies_Administrative supplies (non educational)": "AdministrativeSuppliesNonEducationalCosts",
        "Catering staff and supplies_Total": "TotalGrossCateringCosts",
        "Catering staff and supplies_Net Costs": "TotalNetCateringCostsCosts",
        "Catering staff and supplies_Catering staff": "CateringStaffCosts",
        "Catering staff and supplies_Catering supplies": "CateringSuppliesCosts",
        "Other costs_Total": "TotalOtherCosts",
        "Other costs_Direct revenue financing": "DirectRevenueFinancingCosts",
        "Other costs_Grounds maintenance": "GroundsMaintenanceCosts",
        "Other costs_Indirect employee expenses": "IndirectEmployeeExpenses",
        "Other costs_Interest charges for loan and bank": "InterestChargesLoanBank",
        "Other costs_Other insurance premiums": "OtherInsurancePremiumsCosts",
        "Other costs_PFI charges": "PrivateFinanceInitiativeCharges",
        "Other costs_Rent and rates": "RentRatesCosts",
        "Other costs_Special facilities": "SpecialFacilitiesCosts",
        "Other costs_Staff development and training": "StaffDevelopmentTrainingCosts",
        "Other costs_Staff-related insurance": "StaffRelatedInsuranceCosts",
        "Other costs_Supply teacher insurance": "SupplyTeacherInsurableCosts",
        "Other costs_School staff": "CommunityFocusedSchoolStaff",
        "Other costs_School costs": "CommunityFocusedSchoolCosts",
        "Income_Total_CS": "TotalIncomeCS",
        "Total Expenditure_CS": "TotalExpenditureCS",
        "In year balance_CS": "InYearBalanceCS",
        "Revenue reserve_CS": "RevenueReserveCS",
        "Income_Total grant funding_CS": "TotalGrantFundingCS",
        "Income_Total self generated funding_CS": "TotalSelfGeneratedFundingCS",
        "Income_Direct grants_CS": "DirectGrantsCS",
        "Income_Other DFE grants_CS": "OtherDfeGrantsCS",
        "Income_Other grants_CS": "OtherIncomeGrantsCS",
        "Income_Government source_CS": "GovernmentSourceCS",
        "Income_Other Revenue Income_CS": "CommunityGrantsCS",
        "Income_Academies_CS": "AcademiesCS",
        "Income_Facilities and services_CS": "IncomeFacilitiesServicesCS",
        "Income_Catering services_CS": "IncomeCateringServicesCS",
        "Income_Donations and voluntary funds_CS": "DonationsVoluntaryFundsCS",
        "Income_Receipts supply teacher insurance_CS": "ReceiptsSupplyTeacherInsuranceClaimsCS",
        "Income_Investment income_CS": "InvestmentIncomeCS",
        "Income_Other self-generated income_CS": "OtherSelfGeneratedIncomeCS",
        "Teaching and Teaching support staff_Total_CS": "TotalTeachingSupportStaffCostsCS",
        "Teaching and Teaching support staff_Teaching staff_CS": "TeachingStaffCostsCS",
        "Teaching and Teaching support staff_Supply teaching staff_CS": "SupplyTeachingStaffCostsCS",
        "Teaching and Teaching support staff_Educational consultancy_CS": "EducationalConsultancyCostsCS",
        "Teaching and Teaching support staff_Education support staff_CS": "EducationSupportStaffCostsCS",
        "Teaching and Teaching support staff_Agency supply teaching staff_CS": "AgencySupplyTeachingStaffCostsCS",
        "Non-educational support staff and services_Total_CS": "TotalNonEducationalSupportStaffCostsCS",
        "Non-educational support staff and services_Administrative and clerical staff_CS": "AdministrativeClericalStaffCostsCS",
        "Non-educational support staff and services_Audit cost_CS": "AuditorsCostsCS",
        "Non-educational support staff and services_Other staff_CS": "OtherStaffCostsCS",
        "Non-educational support staff and services_Professional services (non-curriculum)_CS": "ProfessionalServicesNonCurriculumCostsCS",
        "Educational supplies_Total_CS": "TotalEducationalSuppliesCostsCS",
        "Educational supplies_Examination fees_CS": "ExaminationFeesCostsCS",
        "Educational supplies_Learning resources (not ICT equipment)_CS": "LearningResourcesNonIctCostsCS",
        "Educational ICT_ICT learning resources_CS": "LearningResourcesIctCostsCS",
        "Premises staff and services_Total_CS": "TotalPremisesStaffServiceCostsCS",
        "Premises staff and services_Cleaning and caretaking_CS": "CleaningCaretakingCostsCS",
        "Premises staff and services_Maintenance of premises_CS": "MaintenancePremisesCostsCS",
        "Premises staff and services_Other occupation costs_CS": "OtherOccupationCostsCS",
        "Premises staff and services_Premises staff_CS": "PremisesStaffCostsCS",
        "Utilities_Total_CS": "TotalUtilitiesCostsCS",
        "Utilities_Energy_CS": "EnergyCostsCS",
        "Utilities_Water and sewerage_CS": "WaterSewerageCostsCS",
        "Administrative supplies_Administrative supplies (non educational)_CS": "AdministrativeSuppliesNonEducationalCostsCS",
        "Catering staff and supplies_Total_CS": "TotalGrossCateringCostsCS",
        "Catering staff and supplies_Net Costs_CS": "TotalNetCateringCostsCostsCS",
        "Catering staff and supplies_Catering staff_CS": "CateringStaffCostsCS",
        "Catering staff and supplies_Catering supplies_CS": "CateringSuppliesCostsCS",
        "Other costs_Total_CS": "TotalOtherCostsCS",
        "Other costs_Direct revenue financing_CS": "DirectRevenueFinancingCostsCS",
        "Other costs_Indirect employee expenses_CS": "IndirectEmployeeExpensesCS",
        "Other costs_Interest charges for loan and bank_CS": "InterestChargesLoanBankCS",
        "Other costs_Other insurance premiums_CS": "OtherInsurancePremiumsCostsCS",
        "Other costs_PFI charges_CS": "PrivateFinanceInitiativeChargesCS",
        "Other costs_Rent and rates_CS": "RentRatesCostsCS",
        "Other costs_Special facilities_CS": "SpecialFacilitiesCostsCS",
        "Other costs_Staff development and training_CS": "StaffDevelopmentTrainingCostsCS",
        "Other costs_Staff-related insurance_CS": "StaffRelatedInsuranceCostsCS",
        "Other costs_Supply teacher insurance_CS": "SupplyTeacherInsurableCostsCS",
        "Income_Pre Post 16_CS": "PrePost16FundingCS",
        "Income_Pre Post 16": "PrePost16Funding",
        # "Other costs_Grounds maintenance_CS": "GroundsMaintenanceCostsCS",
    }

    write_frame = df.reset_index().rename(columns=projections)[[*projections.values()]]

    write_frame["RunType"] = run_type
    write_frame["RunId"] = str(year)
    write_frame.set_index("URN", inplace=True)
    write_frame.replace({np.inf: np.nan, -np.inf: np.nan}, inplace=True)

    upsert(write_frame, "Financial", keys=["RunType", "RunId", "URN"])
    logger.info(f"Wrote {len(write_frame)} rows to financial data {run_type} - {year}")


def insert_bfr_metrics(run_type: str, year: str, df: pd.DataFrame):
    projections = {
        "Company Registration Number": "CompanyNumber",
        "Category": "Metric",
        "Value": "Value"
    }

    write_frame = df.reset_index().rename(columns=projections)[[*projections.values()]]

    write_frame["RunType"] = run_type
    write_frame["RunId"] = str(year)
    write_frame["Year"] = int(year)
    write_frame.set_index("CompanyNumber", inplace=True)

    upsert(
        write_frame,
        "BudgetForecastReturnMetric",
        keys=["RunType", "RunId", "Year", "CompanyNumber", "Metric"],
        dtype={
                "RunType": sqlalchemy.types.VARCHAR(length=50),
                "RunId": sqlalchemy.types.VARCHAR(length=50),
                "Year": sqlalchemy.types.Integer(),
                "Metric": sqlalchemy.types.VARCHAR(length=50),
                "CompanyNumber": sqlalchemy.types.VARCHAR(length=8),
                "Value": sqlalchemy.types.Numeric(16, 2)
        })
    logger.info(
        f"Wrote {len(write_frame)} rows to BFR metrics data {run_type} - {year}"
    )


def insert_bfr(run_type: str, year: str, df: pd.DataFrame):
    projections = {
        "Company Registration Number": "CompanyNumber",
        "Year": "Year",
        "Category": "Category",
        "Value": "Value",
        "Pupils": "TotalPupils"
    }

    write_frame = df.reset_index().rename(columns=projections)[[*projections.values()]]
    write_frame["CompanyNumber"] = write_frame["CompanyNumber"].astype(str)
    write_frame["RunType"] = run_type
    write_frame["RunId"] = str(year)

    upsert(write_frame, "BudgetForecastReturn", keys=["RunType", "RunId", "Year", "CompanyNumber", "Category"],
           dtype={
                "RunType": sqlalchemy.types.VARCHAR(length=50),
                "RunId": sqlalchemy.types.VARCHAR(length=50),
                "Year": sqlalchemy.types.Integer(),
                "Category": sqlalchemy.types.VARCHAR(length=50),
                "CompanyNumber": sqlalchemy.types.VARCHAR(length=8),
                "Value": sqlalchemy.types.Numeric(16, 2),
                "TotalPupils": sqlalchemy.types.Numeric(16, 2)
            })
    logger.info(f"Wrote {len(write_frame)} rows to BFR data {run_type} - {year}")
