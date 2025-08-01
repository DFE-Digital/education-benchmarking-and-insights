import logging

import pandas as pd

import pipeline.input_schemas as input_schemas
from pipeline.stats_collector import stats_collector

logger = logging.getLogger("fbit-data-pipeline")


def prepare_ks4_data(ks4_path):
    if ks4_path is not None:
        ks4 = pd.read_excel(
            ks4_path,
            dtype=input_schemas.ks4,
            usecols=input_schemas.ks4.keys(),
            na_values=["NP", "NE", "SUPP", "LOWCOV"],
            engine="calamine",
        )
        logger.info(f"KS4 Data raw shape: {ks4.shape}")
        ks4["ATT8SCR"] = ks4["ATT8SCR"].astype(float).fillna(0)
        ks4["P8MEA"] = ks4["P8MEA"].astype(float).fillna(0)

        ks4.rename(
            columns={
                "ATT8SCR": "AverageAttainment",
                "P8MEA": "Progress8Measure",
                "P8_BANDING": "Progress8Banding",
            },
            inplace=True,
        )

        ks4 = (
            ks4[["URN", "AverageAttainment", "Progress8Measure", "Progress8Banding"]]
            .dropna()
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
    stats_collector.collect_preprocessed_ancillary_data_shape(
        "ks4", ks4_with_index.shape
    )

    return ks4_with_index
