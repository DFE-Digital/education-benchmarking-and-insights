import json
from urllib.parse import quote_plus
import pyodbc
import os
import logging
import pandas as pd
import sqlalchemy
from sqlalchemy import create_engine, event

azure_logger = logging.getLogger("azure")
azure_logger.setLevel(logging.WARNING)

logger = logging.getLogger("fbit-data-pipeline:db")
logger.setLevel(logging.INFO)

conn_str = os.getenv("DATABASE_CONNECTION_STRING")
quoted = quote_plus(conn_str)
engine = create_engine("mssql+pyodbc:///?odbc_connect={}".format(quoted))


@event.listens_for(engine, "before_cursor_execute")
def receive_before_cursor_execute(
        conn, cursor, statement, params, context, executemany
):
    if executemany:
        cursor.fast_executemany = True


def upsert(df, table_name, key_name):
    update_cols = []
    insert_cols = [key_name]
    insert_vals = [f'src.{key_name}']
    for col in df.columns:
        if col == key_name:
            continue
        update_cols.append("{col}=src.{col}".format(col=col))
        insert_cols.append(col)
        insert_vals.append("src.{col}".format(col=col))
    temp_table = f'{table_name}_temp'
    df.to_sql(temp_table, engine, if_exists='replace', index=True)
    update_stmt = f'MERGE {table_name} as dest USING {temp_table} as src ON dest.{key_name}=src.{key_name} WHEN MATCHED THEN UPDATE SET {", ".join(update_cols)} WHEN NOT MATCHED BY TARGET THEN INSERT ({", ".join(insert_cols)}) VALUES ({", ".join(insert_vals)});'
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

    write_frame.to_sql("ComparatorSet", con=engine, if_exists="append", schema="dbo")
    logger.info(
        f"Wrote {len(df)} rows to comparator set {run_type} - {set_type} - {year}"
    )


def insert_metric_rag(run_type: str, year: str, df: pd.DataFrame):
    write_frame = df[
        ["Value", "Mean", "DiffMean", "PercentDiff", "Percentile", "Decile", "RAG"]
    ].copy()
    write_frame["RunType"] = run_type
    write_frame["RunId"] = year

    write_frame.to_sql("MetricRAG", con=engine, if_exists="append", schema="dbo")
    logger.info(f"Wrote {len(df)} rows to metric rag {run_type} - {year}")


def insert_school(run_type: str, year: str, df: pd.DataFrame):
    projections = {
        "URN": "URN",
        "EstablishmentName": "SchoolName",
        "Trust UKPRN": "TrustUKPRN",
        "Trust Name": "TrustName",
        "Federation Lead School UKPRN": "FederationLeadUKPRN",
        "Federation Name": "FederationLeadName",
        "LA Code": "LACode",
        "LA Name": "LAName",
        "London Weighting": "LondonWeighting",
        "Finance Type": "FinanceType",
        "Overall Phase": "OverallPhase",
        "SchoolPhaseType": "SchoolType",
        "Has Sixth Form": "HasSixthForm",
        "Has Nursery": "HasNursery",
        "Is PFI": "IsPFISchool",
        "OfstedLastInsp": "OfstedDate",
        "OfstedRating (name)": "OfstedDescription",
        "TelephoneNum": "Telephone",
        "SchoolWebsite": "Website",
        "Email": "ContactEmail",
        "HeadName": "HeadTeacherName",
        "HeadEmail": "HeadTeacherEmail",
    }

    write_frame = df.rename(columns=projections)[[*projections.values()]]

    upsert(write_frame, "School", "UKPRN")
    logger.info(f"Wrote {len(df)} rows to school {run_type} - {year}")
