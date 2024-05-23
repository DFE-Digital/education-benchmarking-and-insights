import json
from urllib.parse import quote_plus
import pyodbc
import os
import logging
import pandas as pd
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
