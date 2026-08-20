import pandas as pd
from azure.core.exceptions import ResourceNotFoundError

from pipeline.utils.log import setup_logger
from pipeline.utils.storage import get_blob

from .config import get_laa_ancillary_columns, get_laa_ancillary_filenames

logger = setup_logger(__name__)


def load_preprocessed_cfr_parquet_for_laa_risk_derivations(run_year):
    blob_path = f"default/{run_year}/maintained_schools.parquet"
    try:
        blob = get_blob("pre-processed", blob_path)
        cfr_df = pd.read_parquet(blob, columns=None)
        return cfr_df
    except ResourceNotFoundError:
        slug = "Cannot compute historical financial risk metrics without historic CFR data going back 4 years"
        logger.error(slug)
        raise ValueError(slug)


def load_laa_risk_score_data(run_year: int):
    logger.info(f"Loading {run_year} LAA risk score data...")

    cfr_data_this_year = load_preprocessed_cfr_parquet_for_laa_risk_derivations(
        run_year
    )
    cfr_data_year_minus_one = load_preprocessed_cfr_parquet_for_laa_risk_derivations(
        run_year - 1
    )
    cfr_data_year_minus_two = load_preprocessed_cfr_parquet_for_laa_risk_derivations(
        run_year - 2
    )
    cfr_data_year_minus_three = load_preprocessed_cfr_parquet_for_laa_risk_derivations(
        run_year - 3
    )
    cfr_data_year_minus_four = load_preprocessed_cfr_parquet_for_laa_risk_derivations(
        run_year - 4
    )

    return (
        cfr_data_this_year,
        cfr_data_year_minus_one,
        cfr_data_year_minus_two,
        cfr_data_year_minus_three,
        cfr_data_year_minus_four,
    )

def load_laa_extra_ancillary_data(run_year: int):
    file_config = get_laa_ancillary_filenames(run_year)
    columns_config = get_laa_ancillary_columns(run_year)

    absences_schema = columns_config["absences"]
    absences_data_path = f"default/{run_year}/{file_config['absences']}"
    absences_blob = get_blob("raw", absences_data_path)
    absences_df = pd.read_csv(
        absences_blob, usecols=absences_schema.keys(), dtype=absences_schema
    )

    capacity_schema = columns_config["capacity"]
    capacity_data_path = f"default/{run_year}/{file_config['capacity']}"
    capacity_blob = get_blob("raw", capacity_data_path)
    capacity_df = pd.read_csv(
        capacity_blob, usecols=capacity_schema.keys(), dtype=capacity_schema
    )

    capacity_special_schema = columns_config["capacity_special"]
    capacity_special_data_path = f"default/{run_year}/{file_config['capacity_special']}"
    capacity_special_blob = get_blob("raw", capacity_special_data_path)
    capacity_special_df = pd.read_csv(
        capacity_special_blob,
        usecols=capacity_special_schema.keys(),
        dtype=capacity_special_schema,
    )

    parental_preference_schema = columns_config["parental_preference"]
    parental_preference_data_path = (
        f"default/{run_year}/{file_config['parental_preference']}"
    )
    parental_preference_blob = get_blob("raw", parental_preference_data_path)
    parental_preference_df = pd.read_csv(
        parental_preference_blob,
        usecols=parental_preference_schema.keys(),
        dtype=parental_preference_schema,
    )

    return {
        "absences_raw": absences_df,
        "capacity_raw": capacity_df,
        "capacity_special_raw": capacity_special_df,
        "parental_preference_raw": parental_preference_df,
        "run_year": run_year,
    }
