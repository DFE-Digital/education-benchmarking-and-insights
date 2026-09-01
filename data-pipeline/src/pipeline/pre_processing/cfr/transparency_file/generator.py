import io

import pandas as pd

from pipeline import input_schemas
from pipeline.utils.log import setup_logger

from .ancillary_data import build_federation_context
from .financials import add_financials
from .formatting import (
    build_maintained_schools_download_file,
    build_maintained_schools_master_list,
    build_sfb_maintained,
)

logger = setup_logger(__name__)


def build_transparency_files(
    cfr_raw_blob: io.StringIO | io.BytesIO,
    gias: pd.DataFrame,
    pru: pd.DataFrame,
    hospital_schools: pd.DataFrame,
    sen: pd.DataFrame,
    census: pd.DataFrame,
    lookup_la: pd.DataFrame,
    census_last_year: pd.DataFrame,
    sen_last_year: pd.DataFrame,
    pru_last_year: pd.DataFrame,
    hospital_schools_last_year: pd.DataFrame,
    year: int,
) -> tuple[pd.DataFrame, pd.DataFrame]:
    """
    Coordinates the generation of the CFR transparency files (Master List and Download File).
    Replaces the legacy My_Step1.sql through My_Step5.sql execution.
    """
    logger.info(f"Generating CFR transparency files for year {year}...")

    # 1. Read Raw CFR Data
    schema = input_schemas.cfr_raw_cols.get(year, input_schemas.cfr_raw_cols["default"])
    cfr_raw = pd.read_csv(cfr_raw_blob, usecols=schema.keys(), dtype=schema)
    logger.info(f"Loaded raw CFR data. Shape: {cfr_raw.shape}")

    # 2. Add DNS rows for federations and join ancillary data
    logger.info("Building federation context...")
    cfr_federations_with_context = build_federation_context(
        cfr_raw=cfr_raw,
        gias=gias,
        pru=pru,
        hospital_schools=hospital_schools,
        sen=sen,
        census=census,
        lookup_la=lookup_la,
        census_last_year=census_last_year,
        sen_last_year=sen_last_year,
        pru_last_year=pru_last_year,
        hospital_schools_last_year=hospital_schools_last_year,
        year=year,
    )
    logger.info(
        f"Built federation context. Shape: {cfr_federations_with_context.shape}"
    )

    # 3. Create additive columns
    logger.info("Merging CFR raw data and adding financials...")
    cfr_raw_fin = cfr_raw.copy().rename(columns={"LEAEstab": "LAEstab"})
    merged = cfr_federations_with_context.merge(
        cfr_raw_fin, on="LAEstab", how="left", suffixes=("", "_raw")
    )
    mergedworking = add_financials(merged)
    logger.info(
        f"Financials added. Merged working dataframe shape: {mergedworking.shape}"
    )

    # 4. Format final dataframe for the transparency file and data pipeline
    logger.info(
        "Formatting final dataframes for SFB Maintained, Master List, and Download File..."
    )
    sfb_maintained = build_sfb_maintained(mergedworking, lookup_la)
    master_list = build_maintained_schools_master_list(sfb_maintained, year=year)
    download_file = build_maintained_schools_download_file(sfb_maintained, year=year)
    logger.info(f"Completed generating CFR transparency files for year {year}. ")

    return master_list, download_file
