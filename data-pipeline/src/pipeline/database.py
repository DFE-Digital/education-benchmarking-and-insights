import json
import os
import logging
import pandas as pd
import sqlalchemy
from sqlalchemy import create_engine, event, URL
from src.pipeline.storage import (write_blob)

azure_logger = logging.getLogger("azure")
azure_logger.setLevel(logging.WARNING)

logger = logging.getLogger("fbit-data-pipeline")

db_args = os.getenv("DB_ARGS")

args = []
for sub in db_args.split(';'):
    if '=' in sub:
        args.append(map(str.strip, sub.split('=', 1)))
args = dict(args)

connection_url = URL.create(
    "mssql+pyodbc",
    username=os.getenv("DB_USER"),
    password=os.getenv("DB_PWD"),
    host=os.getenv("DB_HOST"),
    database=os.getenv("DB_NAME"),
    port=os.getenv("DB_PORT"),
    query={"driver": "ODBC Driver 18 for SQL Server"} | args
)

engine = create_engine(connection_url)


@event.listens_for(engine, "before_cursor_execute")
def receive_before_cursor_execute(
        conn, cursor, statement, params, context, executemany
):
    if executemany:
        cursor.fast_executemany = True


def upsert(df, table_name, keys: list[str]):
    logger.info(f"Connecting to database {engine.url}")

    update_cols = []
    insert_cols = [*keys]
    insert_vals = [f'src.{key_name}' for key_name in keys]
    match_keys = [f'dest.{key_name}=src.{key_name}' for key_name in keys]

    for col in df.columns:
        if col in keys:
            continue
        update_cols.append("{col}=src.{col}".format(col=col))
        insert_cols.append(col)
        insert_vals.append("src.{col}".format(col=col))
    temp_table = f'{table_name}_temp'
    df.to_sql(temp_table, engine, if_exists='replace', index=True)
    update_stmt = f'MERGE {table_name} as dest USING {temp_table} as src ON {" AND ".join(match_keys)}  WHEN MATCHED THEN UPDATE SET {", ".join(update_cols)} WHEN NOT MATCHED BY TARGET THEN INSERT ({", ".join(insert_cols)}) VALUES ({", ".join(insert_vals)});'
    with engine.begin() as cnx:
        cnx.execute(sqlalchemy.text(update_stmt))
        cnx.execute(sqlalchemy.text(f'DROP TABLE IF EXISTS {temp_table}'))


def insert_comparator_set(run_type: str, set_type: str, year: str, df: pd.DataFrame):
    write_frame = df[["Pupil", "Building"]].copy()
    write_frame["RunType"] = run_type
    write_frame["SetType"] = set_type
    write_frame["RunId"] = year
    write_frame["Pupil"] = write_frame["Pupil"].map(lambda x: json.dumps(x.tolist()))
    write_frame["Building"] = write_frame["Building"].map(
        lambda x: json.dumps(x.tolist())
    )

    upsert(write_frame, "ComparatorSet", keys=["RunType", "RunId", "URN", "SetType"])
    logger.info(
        f"Wrote {len(write_frame)} rows to comparator set {run_type} - {set_type} - {year}"
    )


def insert_metric_rag(run_type: str, set_type: str, year: str, df: pd.DataFrame):
    write_frame = df[
        ["Category", "SubCategory", "Value", "Mean", "DiffMean", "PercentDiff", "Percentile", "Decile", "RAG"]
    ].copy()
    write_frame["RunType"] = run_type
    write_frame["RunId"] = year
    write_frame["SetType"] = set_type

    upsert(write_frame, "MetricRAG", keys=["RunType", "RunId", "SetType", "URN", "Category", "SubCategory"])
    logger.info(f"Wrote {len(write_frame)} rows to metric rag {run_type} - {set_type} - {year}")


def insert_schools_and_trusts_and_local_authorities(run_type: str, year: str, df: pd.DataFrame):
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
        "Postcode": "AddressPostcode"
    }

    write_frame = df.reset_index().rename(columns=projections)[[*projections.values()]]

    upsert(write_frame, "School", keys=["URN"])
    logger.info(f"Wrote {len(write_frame)} rows to school {run_type} - {year}")

    trust_projections = {
        "Trust Name": "TrustName",
        "Group UID": "UID",
        "CFO Name": "CFOName",
        "CFO Email": "CFOEmail",
        "OpenDate": "OpenDate",
        "Company Registration Number": "CompanyNumber"
    }

    trusts = (
        df[~df["Trust UKPRN"].isna()]
        .reset_index()
        .sort_values(by=["Trust UKPRN", "OpenDate"], ascending=False)
        .groupby(["Trust UKPRN"])
        .first()
        .rename(columns=trust_projections)[[*trust_projections.values()]]
        .reset_index()
        .drop(columns=["Trust UKPRN"])
    )

    upsert(trusts, "Trust", keys=["CompanyNumber"])
    logger.info(f"Wrote {len(trusts)} rows to trust {run_type} - {year}")

    la_projections = {
        "LA Code": "Code",
        "LA Name": "Name"
    }

    las = (df.reset_index()[["LA Code", "LA Name"]]
           .rename(columns=la_projections)[[*la_projections.values()]]
           .drop_duplicates())

    las.set_index("Code", inplace=True)

    upsert(las, "LocalAuthority", keys=["Code"])
    logger.info(f"Wrote {len(las)} rows to LAs {run_type} - {year}")
