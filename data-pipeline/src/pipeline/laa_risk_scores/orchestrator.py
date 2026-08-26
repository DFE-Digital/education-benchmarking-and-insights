import pandas as pd

from pipeline.utils.database import insert_laa_risk_scores
from pipeline.utils.log import setup_logger
from pipeline.utils.storage import write_blob

from .config import get_laa_download_file_schema
from .engine import derive_laa_risk_scores
from .loader import load_laa_extra_ancillary_data, load_laa_risk_score_data
from .preprocessing import preprocess_laa_data, preprocess_laa_extra_ancillary_data

logger = setup_logger(__name__)


def create_laa_risk_scores_download_file(df: pd.DataFrame, run_year: int, run_id: int):
    logger.info(f"Creating LAA risk scores download file for {run_id=}, {run_year=}...")

    columns_to_export = get_laa_download_file_schema(run_year)

    export_cols = [col for col in columns_to_export if col in df.columns]
    export_df = df[export_cols]

    blob_path = f"default/{run_id}/laa_risk_scores_download.csv"
    write_blob(
        "artifacts",
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

    raw_ancillary = load_laa_extra_ancillary_data(run_year)
    absences, capacity, parental_preference = preprocess_laa_extra_ancillary_data(
        absences_raw=raw_ancillary["absences_raw"],
        capacity_raw=raw_ancillary["capacity_raw"],
        capacity_special_raw=raw_ancillary["capacity_special_raw"],
        parental_preference_raw=raw_ancillary["parental_preference_raw"],
        run_year=run_year,
    )

    cfr_dfs = load_laa_risk_score_data(run_year)
    cfr_with_all_extra_data = preprocess_laa_data(
        cfr_data_this_year=cfr_dfs[0],
        cfr_data_year_minus_one=cfr_dfs[1],
        cfr_data_year_minus_two=cfr_dfs[2],
        cfr_data_year_minus_three=cfr_dfs[3],
        cfr_data_year_minus_four=cfr_dfs[4],
        absences=absences,
        capacity=capacity,
        parental_preference=parental_preference,
        run_year=run_year,
    )

    risk_metrics_with_raw_data, melted_indicators_df, melted_headers_df = (
        derive_laa_risk_scores(
            cfr_with_all_extra_data, run_year=run_year, run_id=run_id
        )
    )
    insert_laa_risk_scores(run_id, melted_indicators_df, melted_headers_df)
    create_laa_risk_scores_download_file(risk_metrics_with_raw_data, run_id)

    logger.info(
        f"LAA risk scores data-loading pipeline completed for year {run_year} (RunId: {run_id})."
    )

    return cfr_with_all_extra_data
