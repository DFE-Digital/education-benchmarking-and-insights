import pandas as pd

from .engine import derive_laa_risk_scores
from .loader import load_laa_risk_score_data
from pipeline.utils.log import setup_logger

logger = setup_logger(__name__)


def write_laa_risk_scores_to_db(indicators_df: pd.DataFrame, headers_df: pd.DataFrame):
    logger.info("Writing LAA risk scores to database (stub)...")
    pass


def create_laa_risk_scores_download_file(headers_df: pd.DataFrame):
    logger.info("Creating LAA risk scores download file (stub)...")
    pass


def run_laa_risk_scores_pipeline(run_year: int):
    logger.info(f"Starting LAA risk scores pipeline for year {run_year}...")
    cfr_with_all_extra_data = load_laa_risk_score_data(run_year)
    indicators_df, headers_df = derive_laa_risk_scores(cfr_with_all_extra_data, run_year=run_year)
    write_laa_risk_scores_to_db(indicators_df, headers_df)
    create_laa_risk_scores_download_file(headers_df)
    logger.info(f"Completed LAA risk scores pipeline for year {run_year}.")