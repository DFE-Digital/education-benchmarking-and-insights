import json
import logging
import os
import textwrap
import time
from dataclasses import dataclass

import numpy as np
import pandas as pd
import sqlalchemy
from sqlalchemy import URL, create_engine, event

from pipeline import config

logger = logging.getLogger("fbit-data-pipeline")


def get_engine() -> sqlalchemy.engine.Engine:
    """
    Creates a SQLAlchemy Engine.

    By default, configuration is derived from environment variables.

    :return: SQLAlchemy Engine
    """
    db_args = os.getenv("DB_ARGS", "")

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

    engine = create_engine(connection_url, fast_executemany=True)

    @event.listens_for(engine, "before_cursor_execute")
    def receive_before_cursor_execute(
        conn, cursor, statement, params, context, executemany
    ):
        logger.debug(str(statement))

    return engine


@dataclass
class TempTable:
    name: str
    target: str
    columns: list[str]

    def __str__(self):
        return self.name


def _get_table_columns(
    table: str,
    engine: sqlalchemy.engine.Engine,
) -> list[str]:
    """
    Retrieve the columns for a given table.

    The source-of-truth for the table formats should remain the DB
    itself: here, we retrieve the list of expected columns directly
    from the table's schema.

    :param table: DB table for which to retrieve columns
    :return: column names
    """
    sql = textwrap.dedent(
        """
    SELECT COLUMN_NAME
      FROM INFORMATION_SCHEMA.COLUMNS
     WHERE TABLE_NAME = :table
    ;
    """
    ).strip()

    with engine.begin() as cnx:
        results = (
            cnx.execute(
                sqlalchemy.text(sql),
                parameters={"table": table},
            )
            .mappings()
            .all()
        )

    return [result["COLUMN_NAME"] for result in results]


def _get_temp_table_name(table: str, run_id: str) -> str:
    """
    Generate a unique temp. table name.

    The name will be comprised of:

    - a `_` prefix
    - target table name (to which data will be copied)
    - the `RunId` of the current job
      - `-` will be removed to ensure validity of the table name
    - the current epoch time
    - a `temp` suffix

    :param table: the table to which temp. table relates
    :param run_id: the current RunId
    :return: unique temp. table name
    """
    return f"_{table}_{run_id}_{int(time.time())}_temp".replace("-", "_")


def _get_temp_table(
    table: str,
    run_id: str,
    engine: sqlalchemy.engine.Engine,
) -> TempTable:
    """
    Create a temp. table for data ingest.

    Data will be stored in a temp. table before data are copied into
    the target table. Here, we create the temp. table and appropriate
    metadata.

    Note: the `SELECT…INTO…` pattern assures that the temp. table will
    be structurally identical to the target table.

    :param table: target table to which data will be moved
    :param run_id: unique identifier for processing
    :return: temp. table details
    """
    temp_table_name = _get_temp_table_name(table, run_id)

    logger.info(f"Creating temp. table: {temp_table_name}.")
    sql = textwrap.dedent(
        f"""
    SELECT *
      INTO {temp_table_name}
      FROM {table}
     WHERE 1=0
    ;
    """
    ).strip()
    with engine.begin() as cnx:
        cnx.execute(sqlalchemy.text(sql))

    columns = _get_table_columns(temp_table_name, engine)

    return TempTable(
        name=temp_table_name,
        target=table,
        columns=columns,
    )


def _write_data(
    df: pd.DataFrame,
    table: str,
    run_id: str,
    engine: sqlalchemy.engine.Engine | None = None,
):
    """
    Write data to a target table.

    The workflow is as follows:

    1. Create a temp. table
    2. Copy the data to the temp. table
    3. In a single transaction:
       1. Delete the corresponding data in the target table
       2. Copy the data from the temp. table to the target table
    4. Delete the temp. table

    :param df: DataFrame holding the data to be written
    :param table: the target table
    :param run_id: unique identifier for processing
    :param engine: SQLAlchemy Engine
    """
    if engine is None:
        engine = get_engine()

    temp_table = _get_temp_table(
        table=table,
        run_id=run_id,
        engine=engine,
    )

    logger.info(f"Writing to temp. table: {temp_table}.")
    start = time.time()
    df.to_sql(
        name=temp_table.name,
        con=engine,
        if_exists="append",
        index=df.index.name is not None,
        chunksize=int(os.getenv("SQL_CHUNKSIZE", 10_000)),
    )
    logger.info(
        f"Wrote {len(df.index):,} rows to {temp_table} in {int(time.time() - start):,} seconds."
    )

    sql = textwrap.dedent(
        f"""
    BEGIN TRANSACTION;

    DELETE
      FROM {table}
     WHERE RunId = :run_id
    ;

    INSERT INTO {table} (
        {'\n      , '.join(temp_table.columns)}
    )
    SELECT {'\n         , '.join(temp_table.columns)}
      FROM {temp_table}
    ;

    COMMIT;
    """
    ).strip()

    logger.info(f"Writing to {table} ({run_id}).")
    start = time.time()
    with engine.begin() as cnx:
        cnx.execute(sqlalchemy.text(sql), parameters={"run_id": run_id})
        cnx.execute(sqlalchemy.text(f"DROP TABLE {temp_table};"))

    logger.info(
        f"Wrote {len(df):,} rows to {table}:{run_id} in {int(time.time() - start):,} seconds."
    )


def upsert(
    df: pd.DataFrame,
    table_name: str,
    run_id: str,
    keys: list[str],
    dtype: dict[str, any] | None = None,
    engine: sqlalchemy.engine.Engine | None = None,
):
    if engine is None:
        engine = get_engine()

    logger.info(f"Connecting to database {engine.url}")

    # Drop duplicates, including the index if specifically set.
    if _index := df.index.name:
        df.reset_index(inplace=True)
    df.drop_duplicates(inplace=True)
    if _index:
        df.set_index(_index, inplace=True)

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

    temp_table = _get_temp_table_name(table_name, run_id)
    df.to_sql(temp_table, engine, if_exists="fail", index=True, dtype=dtype)

    update_stmt = f'MERGE {table_name} as dest USING {temp_table} as src ON {" AND ".join(match_keys)}  WHEN MATCHED THEN UPDATE SET {", ".join(update_cols)} WHEN NOT MATCHED BY TARGET THEN INSERT ({", ".join(insert_cols)}) VALUES ({", ".join(insert_vals)});'
    with engine.begin() as cnx:
        cnx.execute(sqlalchemy.text(update_stmt))
        cnx.execute(sqlalchemy.text(f"DROP TABLE {temp_table};"))


def insert_schools_and_local_authorities(
    run_type: str,
    run_id: str,
    df: pd.DataFrame,
    engine: sqlalchemy.engine.Engine | None = None,
):
    projections = {
        "URN": "URN",
        "EstablishmentName": "SchoolName",
        "Company Registration Number": "TrustCompanyNumber",
        "Company_Name": "TrustName",
        "Federation Lead School URN": "FederationLeadURN",
        "Federation Name": "FederationLeadName",
        "LA Code": "LACode",
        "LA Name": "LAName",
        "London Weighting": "LondonWeighting",
        "Finance Type": "FinanceType",
        "SchoolPhaseType": "OverallPhase",
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

    upsert(write_frame, "School", run_id, keys=["URN"], engine=engine)
    logger.info(f"Wrote {len(write_frame)} rows to school {run_type} - {run_id}")

    la_projections = {"LA Code": "Code", "LA Name": "Name"}

    las = (
        df.reset_index()[["LA Code", "LA Name"]]
        .rename(columns=la_projections)[[*la_projections.values()]]
        .drop_duplicates()
    )

    las.set_index("Code", inplace=True)

    upsert(las, "LocalAuthority", run_id, keys=["Code"], engine=engine)
    logger.info(f"Wrote {len(las)} rows to LAs {run_type} - {run_id}")


def insert_trusts(
    run_type: str,
    run_id: str,
    df: pd.DataFrame,
    engine: sqlalchemy.engine.Engine | None = None,
):
    """
    Store Trust non-financial information.

    Academy-level information is rolled up to Trust level.

    :param run_type: "default" or "custom"
    :param run_id: unique identifier for processing
    :param df: Academy financial information
    :param engine: SQLAlchemy Engine
    """
    trust_projections = {
        "Company_Name": "TrustName",
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

    upsert(trusts, "Trust", run_id, keys=["CompanyNumber"], engine=engine)
    logger.info(f"Wrote {len(trusts)} rows to trust {run_type} - {run_id}")


def insert_non_financial_data(
    run_type: str,
    run_id: str,
    df: pd.DataFrame,
    engine: sqlalchemy.engine.Engine | None = None,
):
    projections = {
        "URN": "URN",
        "TypeOfEstablishment (name)": "EstablishmentType",
        "Total Internal Floor Area": "TotalInternalFloorArea",
        "Building Age": "BuildingAverageAge",
        "Number of pupils": "TotalPupils",
        "Finance Type": "FinanceType",
        "SchoolPhaseType": "OverallPhase",
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
    write_frame["RunId"] = str(run_id)
    write_frame.set_index("URN", inplace=True)
    write_frame.replace({np.inf: np.nan, -np.inf: np.nan}, inplace=True)

    _write_data(
        df=write_frame,
        table="NonFinancial",
        run_id=run_id,
        engine=engine,
    )


def insert_financial_data(
    run_type: str,
    run_id: str,
    df: pd.DataFrame,
    engine: sqlalchemy.engine.Engine | None = None,
):
    projections = {
        "URN": "URN",
        "TypeOfEstablishment (name)": "EstablishmentType",
        "Period covered by return": "PeriodCoveredByReturn",
        "Financial Position": "FinancialPosition",
        "Trust Financial Position": "TrustPosition",
        "Total Income": "TotalIncome",
        "Total Expenditure": "TotalExpenditure",
        "Total Internal Floor Area": "TotalInternalFloorArea",
        "Number of pupils": "TotalPupils",
        "In year balance": "InYearBalance",
        "Revenue reserve": "RevenueReserve",
        "Share Revenue reserve": "ShareRevenueReserve",
        "Finance Type": "FinanceType",
        "SchoolPhaseType": "OverallPhase",
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
        "Total Income_CS": "TotalIncomeCS",
        "Total Expenditure_CS": "TotalExpenditureCS",
        "In year balance_CS": "InYearBalanceCS",
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
        "Income_Direct revenue finance_CS": "DirectRevenueFinancingCS",
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
        "Other costs_Grounds maintenance_CS": "GroundsMaintenanceCostsCS",
        "E20A  Connectivity": "ConnectivityCosts",
        "E20B  Onsite servers": "OnsiteServersCosts",
        "E20C  IT learning resources": "ItLearningResourcesCosts",
        "E20D  Administration software and systems": "AdministrationSoftwareAndSystemsCosts",
        "E20E  Laptops, desktops and tablets": "LaptopsDesktopsAndTabletsCosts",
        "E20F  Other hardware": "OtherHardwareCosts",
        "E20G  IT support": "ItSupportCosts",
    }

    write_frame = df.reset_index().rename(columns=projections)[[*projections.values()]]

    write_frame["RunType"] = run_type
    write_frame["RunId"] = str(run_id)
    write_frame.set_index("URN", inplace=True)
    write_frame.replace({np.inf: np.nan, -np.inf: np.nan}, inplace=True)

    _write_data(
        df=write_frame,
        table="Financial",
        run_id=run_id,
        engine=engine,
    )


def insert_trust_financial_data(
    run_type: str,
    run_id: str,
    df: pd.DataFrame,
    engine: sqlalchemy.engine.Engine | None = None,
):
    """
    Write Trust financial info. to the TrustFinancial table.

    - DataFrame columns will be aligned with DB columns
    - erroneous numeric values will be replaced with NULL

    :param run_type: should only be "default"
    :param run_id: unique identifier for processing
    :param df: Trust financial info.
    :param engine: SQLAlchemy Engine
    """
    write_frame = (
        df.reset_index()[config.trust_db_projections.keys()]
        .rename(columns=config.trust_db_projections)
        .replace({np.inf: np.nan, -np.inf: np.nan})
    )

    write_frame["RunType"] = run_type
    write_frame["RunId"] = str(run_id)

    _write_data(
        df=write_frame,
        table="TrustFinancial",
        run_id=run_id,
        engine=engine,
    )


def insert_bfr_metrics(
    run_id: str,
    year: int,
    df: pd.DataFrame,
    engine: sqlalchemy.engine.Engine | None = None,
):
    """
    Persist BFR metric data to the database.

    Note: `RunType` will _always_ be "default".

    :param run_id: unique identifier for processing
    :param year: BFR year in question
    :param df: BFR metric data
    :param engine: SQLAlchemy Engine
    """
    projections = {
        "Company Registration Number": "CompanyNumber",
        "Category": "Metric",
        "Value": "Value",
    }

    write_frame = df.reset_index().rename(columns=projections)[[*projections.values()]]

    write_frame["RunType"] = "default"
    write_frame["RunId"] = str(run_id)
    write_frame["Year"] = int(year)
    write_frame.set_index("CompanyNumber", inplace=True)

    _write_data(
        df=write_frame,
        table="BudgetForecastReturnMetric",
        run_id=run_id,
        engine=engine,
    )


def insert_bfr(
    run_id: str,
    df: pd.DataFrame,
    engine: sqlalchemy.engine.Engine | None = None,
):
    """
    Persist BFR data to the database.

    Note: `RunType` will _always_ be "default".

    :param run_id: unique identifier for processing
    :param df: BFR data
    :param engine: SQLAlchemy Engine
    """
    projections = {
        "Company Registration Number": "CompanyNumber",
        "Year": "Year",
        "Category": "Category",
        "Value": "Value",
        "Pupils": "TotalPupils",
    }

    write_frame = df.reset_index().rename(columns=projections)[[*projections.values()]]
    write_frame["CompanyNumber"] = write_frame["CompanyNumber"].astype(str)
    write_frame["RunType"] = "default"
    write_frame["RunId"] = str(run_id)

    _write_data(
        df=write_frame,
        table="BudgetForecastReturn",
        run_id=run_id,
        engine=engine,
    )


def insert_comparator_set(
    run_type: str,
    run_id: str,
    df: pd.DataFrame,
    engine: sqlalchemy.engine.Engine | None = None,
):
    write_frame = df[["Pupil", "Building"]].copy()
    write_frame["RunType"] = run_type
    write_frame["RunId"] = str(run_id)
    write_frame["Pupil"] = write_frame["Pupil"].map(
        lambda array: json.dumps(array.tolist())
    )
    write_frame["Building"] = write_frame["Building"].map(
        lambda array: json.dumps(array.tolist())
    )

    _write_data(
        df=write_frame,
        table="ComparatorSet",
        run_id=run_id,
        engine=engine,
    )


def insert_metric_rag(
    run_type: str,
    run_id: str,
    df: pd.DataFrame,
    engine: sqlalchemy.engine.Engine | None = None,
):
    write_frame = df[
        [
            "Category",
            "SubCategory",
            "Value",
            "Median",
            "DiffMedian",
            "PercentDiff",
            "Percentile",
            "Decile",
            "RAG",
        ]
    ].copy()
    write_frame["RunType"] = run_type
    write_frame["RunId"] = str(run_id)

    _write_data(
        df=write_frame,
        table="MetricRAG",
        run_id=run_id,
        engine=engine,
    )


def insert_la_financial(
    run_type: str,
    run_id: str,
    df: pd.DataFrame,
    engine: sqlalchemy.engine.Engine | None = None,
):
    """
    Persist Local Authority Section 251 data to the database.

    :param run_type: "default" or "custom"
    :param run_id: unique identifier for processing
    :param df: Local Authority financial information
    :param engine: (optional) SQLAlchemy Engine
    """
    write_frame = df.reset_index().rename(columns=config.la_db_financial_projections)
    write_frame["RunType"] = run_type
    write_frame["RunId"] = run_id
    write_frame = write_frame[config.la_db_financial_projections.values()]

    _write_data(
        df=write_frame,
        table="LocalAuthorityFinancial",
        run_id=run_id,
        engine=engine,
    )


def insert_la_non_financial(
    run_type: str,
    run_id: str,
    df: pd.DataFrame,
    engine: sqlalchemy.engine.Engine | None = None,
):
    """
    Persist Local Authority SEN2 data to the database.

    :param run_type: "default" or "custom"
    :param run_id: unique identifier for processing
    :param df: Local Authority non-financial information
    :param engine: (optional) SQLAlchemy Engine
    """
    write_frame = df.reset_index().rename(
        columns=config.la_db_non_financial_projections
    )
    write_frame["RunType"] = run_type
    write_frame["RunId"] = run_id
    write_frame = write_frame[config.la_db_non_financial_projections.values()]

    _write_data(
        df=write_frame,
        table="LocalAuthorityNonFinancial",
        run_id=run_id,
        engine=engine,
    )


def _unpivot_statistical_neighbour_column(
    df: pd.DataFrame,
    value_suffix: str,
    value_column: str,
):
    """
    Unpivot Local Authority Statistical Neighbour data.

    :param df: Local Authority Statistical Neighbour information
    :param value_suffix: columns relevant to data
    :param value_column: column name for unpivoted data
    """
    unpivoted = df.melt(
        id_vars=["LaCode"],
        value_vars=[c for c in df.columns if c.endswith(value_suffix)],
        value_name=value_column,
        var_name="NeighbourPosition",
    )[["LaCode", "NeighbourPosition", value_column]]

    unpivoted["NeighbourPosition"] = unpivoted["NeighbourPosition"].map(
        lambda v: int("".join(c for c in v if c.isdigit()))
    )

    return unpivoted


def insert_la_statistical_neighbours(
    run_type: str,
    run_id: str,
    df: pd.DataFrame,
    engine: sqlalchemy.engine.Engine | None = None,
):
    """
    Persist Local Authority Statistical Neighbour data to the database.

    Note: the data must be un-pivoted to match the database schema.

    :param run_type: "default" or "custom"
    :param run_id: unique identifier for processing
    :param df: Local Authority Statistical Neighbour information
    :param engine: (optional) SQLAlchemy Engine
    """
    write_frame = df.reset_index().rename(
        columns=config.la_statistical_neighbours_projections
    )[["LaCode"] + [c for c in df.columns if c.startswith("SN")]]

    _desc = _unpivot_statistical_neighbour_column(
        write_frame, "Prox", "NeighbourDescription"
    )
    _code = _unpivot_statistical_neighbour_column(
        write_frame, "Code", "NeighbourLaCode"
    )

    write_frame = _code.merge(
        _desc,
        how="inner",
        on=["LaCode", "NeighbourPosition"],
    ).sort_values(["LaCode", "NeighbourPosition"])

    write_frame["RunType"] = run_type
    write_frame["RunId"] = run_id
    write_frame = write_frame[config.la_statistical_neighbours_projections.values()]

    _write_data(
        df=write_frame,
        table="LocalAuthorityStatisticalNeighbour",
        run_id=run_id,
        engine=engine,
    )
