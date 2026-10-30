import logging

import numpy as np
import pandas as pd

from pipeline import input_schemas

logger = logging.getLogger(__name__)


def prepare_ks4_data(ks4_path):
    if ks4_path is not None:
        ks4 = pd.read_excel(
            ks4_path,
            dtype=input_schemas.ks4["default"],
            usecols=input_schemas.ks4["default"].keys(),
            na_values=["NP", "NE", "SUPP", "LOWCOV"],
            engine="calamine",
        )
        logger.info(f"KS4 Data raw shape: {ks4.shape}")

        ks4.rename(
            columns=input_schemas.ks4_column_mappings["default"],
            inplace=True,
        )
        ks4["AverageAttainment"] = ks4["AverageAttainment"].astype(float).fillna(0)
        ks4["Progress8Measure"] = ks4["Progress8Measure"].astype(float).fillna(0)

        ks4 = (
            ks4[["URN", "AverageAttainment", "Progress8Measure", "Progress8Banding"]]
            .dropna(subset=["URN"])
            .drop_duplicates()
        )
    else:
        ks4 = pd.DataFrame(
            {
                "URN": pd.Series(dtype="Int64"),
                "AverageAttainment": pd.Series(dtype="float"),
                "Progress8Measure": pd.Series(dtype="float"),
                "Progress8Banding": pd.Series(dtype="string"),
            }
        )

    ks4_with_index = ks4.set_index("URN")

    return ks4_with_index


def prepare_la_ks4_data(ks4_path, year: int) -> tuple[pd.DataFrame, float]:
    if ks4_path is None:
        empty_la = pd.DataFrame(
            {
                "LEA": pd.Series(dtype="Int64"),
                "LocalAuthorityAvgAttainment8": pd.Series(dtype="float"),
            }
        ).set_index("LEA")
        return empty_la, np.nan

    schema = input_schemas.ks4.get(year, input_schemas.ks4["default"])
    column_mappings = input_schemas.ks4_column_mappings.get(
        year, input_schemas.ks4_column_mappings["default"]
    )

    ks4 = pd.read_excel(
        ks4_path,
        dtype=schema,
        usecols=schema.keys(),
        na_values=["NP", "NE", "SUPP", "LOWCOV"],
        engine="calamine",
    )
    logger.info(f"LA KS4 Data raw shape: {ks4.shape}")

    ks4.rename(columns=column_mappings, inplace=True)
    ks4["AverageAttainment"] = pd.to_numeric(
        ks4["AverageAttainment"], errors="coerce"
    ).fillna(0)

    la_avg_attainment = (
        ks4[ks4["RECTYPE"] == 4][["LEA", "AverageAttainment"]]
        .dropna(subset=["LEA"])
        .drop_duplicates(subset=["LEA"])
        .rename(columns={"AverageAttainment": "LocalAuthorityAvgAttainment8"})
        .set_index("LEA")
    )

    national_rows = ks4[ks4["RECTYPE"] == 5]["AverageAttainment"].dropna()
    national_avg_attainment = (
        float(national_rows.iloc[0]) if not national_rows.empty else np.nan
    )

    return la_avg_attainment, national_avg_attainment
