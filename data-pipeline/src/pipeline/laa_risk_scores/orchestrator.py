import pandas as pd

from pipeline.utils.database import insert_laa_risk_scores
from pipeline.utils.log import setup_logger
from pipeline.utils.storage import write_blob

from .config import get_download_file_schema
from .engine import derive_laa_risk_scores
from .loader import load_laa_risk_score_data

logger = setup_logger(__name__)


def create_laa_risk_scores_download_file(df: pd.DataFrame, run_year: int):
    logger.info(f"Creating LAA risk scores download file for {run_year}...")

    columns_to_export = get_download_file_schema(run_year)

    export_cols = [col for col in columns_to_export if col in df.columns]
    export_df = df[export_cols]

    blob_path = f"default/{run_year}/laa_risk_scores_download.csv"
    write_blob(
        "pre-processed",
        blob_path,
        export_df.to_csv(index=False),
    )
    logger.info(
        f"Successfully generated and wrote LAA risk scores download file to {blob_path}"
    )


def run_laa_risk_scores_pipeline(run_year: int, run_id: str):
    logger.info(
        f"Starting LAA risk scores pipeline for year {run_year} (RunId: {run_id})..."
    )
    cfr_with_all_extra_data = load_laa_risk_score_data(run_year)
    risk_metrics_with_raw_data, melted_indicators_df, melted_headers_df = (
        derive_laa_risk_scores(
            cfr_with_all_extra_data, run_year=run_year, run_id=run_id
        )
    )
    insert_laa_risk_scores(run_id, melted_indicators_df, melted_headers_df)
    create_laa_risk_scores_download_file(risk_metrics_with_raw_data, run_year)
    logger.info(
        f"Completed LAA risk scores pipeline for year {run_year} (RunId: {run_id})."
    )
