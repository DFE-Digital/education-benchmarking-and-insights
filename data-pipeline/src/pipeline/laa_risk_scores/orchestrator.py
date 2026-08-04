from pipeline.utils.log import setup_logger

from .loader import load_laa_risk_score_data

logger = setup_logger(__name__)


def run_laa_risk_scores_pipeline(run_year: int, run_id: str):
    logger.info(
        f"Starting LAA risk scores data-loading pipeline for year {run_year} (RunId: {run_id})..."
    )
    cfr_with_all_extra_data = load_laa_risk_score_data(run_year)
    logger.info(
        f"Loaded all preprocessed and ancillary LAA risk score data for year {run_year}."
    )
    logger.info(
        f"LAA risk scores data-loading pipeline completed for year {run_year} (RunId: {run_id})."
    )
