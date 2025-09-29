import logging

import pandas as pd

import pipeline.input_schemas as input_schemas

logger = logging.getLogger(__name__)


def load_bfr_sofa(bfr_sofa_data_path) -> pd.DataFrame:
    bfr_sofa = pd.read_csv(
        bfr_sofa_data_path,
        encoding="unicode-escape",
        dtype=input_schemas.bfr_sofa_cols,
        usecols=input_schemas.bfr_sofa_cols.keys(),
    ).rename(columns={"TrustUPIN": "Trust UPIN", "Title": "Category"})
    logger.info(f"BFR sofa raw shape: {bfr_sofa.shape}")

    return bfr_sofa


def load_bfr_3y(bfr_3y_data_path) -> pd.DataFrame:
    bfr_3y = pd.read_csv(
        bfr_3y_data_path,
        encoding="unicode-escape",
        dtype=input_schemas.bfr_3y_cols,
        usecols=input_schemas.bfr_3y_cols.keys(),
    ).rename(columns={"TrustUPIN": "Trust UPIN"})
    logger.info(f"BFR 3y raw shape: {bfr_3y.shape}")
    return bfr_3y
