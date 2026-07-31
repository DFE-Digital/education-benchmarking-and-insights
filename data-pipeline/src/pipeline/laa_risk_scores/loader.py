from azure.core.exceptions import ResourceNotFoundError
import pandas as pd

from pipeline.utils.log import setup_logger
from pipeline.utils.storage import get_blob

logger = setup_logger(__name__)

def load_preprocessed_cfr_parquet(run_year):
    blob_path = f"default/{run_year}/maintained_schools.parquet"
    try:
        blob = get_blob("pre-processed", blob_path)
        cfr_df = pd.read_parquet(blob, columns=None)
        return cfr_df
    except ResourceNotFoundError:
        slug = "Cannot compute historical financial risk metrics without historic CFR data going back 4 years"
        logger.error(slug)
        raise ValueError(slug)


def load_laa_extra_ancillary_data(run_year: int):
    time_period = int(f"{run_year - 1}{str(run_year)[-2:]}")

    absences_data_path = f"default/{run_year}/Absence_2term_school.csv"
    absences_cols = ["time_period", "school_urn", "sess_overall_percent"]
    absences_df = pd.read_csv(get_blob("raw", absences_data_path), usecols=absences_cols)
    absences_df_filtered = absences_df[absences_df["time_period"]==time_period]

    capacity_data_path = f"default/{run_year}/capacity_school_200910-202425.csv"
    capacity_cols = ["time_period", "school_urn", "school_places"]
    capacity_df = pd.read_csv(get_blob("raw", capacity_data_path), usecols=capacity_cols)
    capacity_df_filtered = capacity_df[capacity_df["time_period"]==time_period]

    parental_preference_data_path = f"default/{run_year}/AppsandOffers_2025_SchoolLevel07102025.csv"
    parental_preference_cols = ["time_period", "school_urn", "proportion_1stprefs_v_totaloffers"]
    parental_preference_df = pd.read_csv(get_blob("raw", parental_preference_data_path), usecols=parental_preference_cols)
    parental_preference_df_filtered = parental_preference_df[parental_preference_df["time_period"]==time_period]

    return (
        absences_df_filtered,
        capacity_df_filtered,
        parental_preference_df_filtered
    )


def load_laa_risk_score_data(run_year: int) -> pd.DataFrame:
    logger.info(f"Loading {run_year} LAA risk score data...")

    cfr_data_this_year = load_preprocessed_cfr_parquet(run_year)
    cfr_data_year_minus_one = load_preprocessed_cfr_parquet(run_year-1)
    cfr_data_year_minus_two = load_preprocessed_cfr_parquet(run_year-2)
    cfr_data_year_minus_three = load_preprocessed_cfr_parquet(run_year-3)
    cfr_data_year_minus_four = load_preprocessed_cfr_parquet(run_year-4)
    absences, capacity, parental_preference = load_laa_extra_ancillary_data(run_year)
    logger.info(f"Loaded {run_year} LAA risk score data.")

    cfr_with_one_historic_year = pd.merge(cfr_data_this_year, cfr_data_year_minus_one, how="left", left_index=True, right_index=True, suffixes=["", "_y_minus_one"])
    cfr_with_two_historic_years = pd.merge(cfr_with_one_historic_year, cfr_data_year_minus_two, how="left", left_index=True, right_index=True, suffixes=["", "_y_minus_two"])
    cfr_with_three_historic_years = pd.merge(cfr_with_two_historic_years, cfr_data_year_minus_three, how="left", left_index=True, right_index=True, suffixes=["", "_y_minus_three"])
    cfr_with_four_historic_years = pd.merge(cfr_with_three_historic_years, cfr_data_year_minus_four, how="left", left_index=True, right_index=True, suffixes=["", "_y_minus_four"])
    cfr_with_absences = pd.merge(cfr_with_four_historic_years.reset_index(), absences, left_on="URN", right_on="school_urn")
    cfr_with_capacity = pd.merge(cfr_with_absences, capacity, left_on="URN", right_on="school_urn")
    cfr_with_all_extra_data = pd.merge(cfr_with_capacity, parental_preference, left_on="URN", right_on="school_urn")
    logger.info(f"Merged {run_year} LAA risk score data with ancillary data.")

    unfederated_schools = cfr_with_all_extra_data["Lead school in federation"] == "0"
    cfr_with_all_extra_data = cfr_with_all_extra_data[unfederated_schools]

    return cfr_with_all_extra_data