import logging

import pandas as pd

from pipeline.input_schemas.bfr import bfr_3y_cols, bfr_sofa_cols

logger = logging.getLogger(__name__)


def load_bfr_sofa(bfr_sofa_data_path, current_year) -> pd.DataFrame:
    bfr_schema = bfr_sofa_cols.get(current_year, bfr_sofa_cols["default"])
    bfr_sofa = pd.read_csv(
        bfr_sofa_data_path,
        encoding="unicode-escape",
        dtype=bfr_schema,
        usecols=bfr_schema.keys(),
    ).rename(columns={"TrustUPIN": "Trust UPIN", "Title": "Category"})
    logger.info(f"BFR sofa raw shape: {bfr_sofa.shape}")

    return bfr_sofa


def load_bfr_3y(bfr_3y_data_path) -> pd.DataFrame:
    bfr_3y = pd.read_csv(
        bfr_3y_data_path,
        encoding="unicode-escape",
        dtype=bfr_3y_cols,
        usecols=bfr_3y_cols.keys(),
    ).rename(columns={"TrustUPIN": "Trust UPIN"})
    logger.info(f"BFR 3y raw shape: {bfr_3y.shape}")
    return bfr_3y
