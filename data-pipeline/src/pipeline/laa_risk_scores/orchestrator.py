from pipeline.utils.log import setup_logger

from .loader import load_laa_extra_ancillary_data, load_laa_risk_score_data
from .preprocessing import preprocess_laa_data, preprocess_laa_extra_ancillary_data

logger = setup_logger(__name__)


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
        run_year=run_year
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
        run_year=run_year
    )
    
    logger.info(
        f"LAA risk scores data-loading pipeline completed for year {run_year} (RunId: {run_id})."
    )
    
    return cfr_with_all_extra_data
