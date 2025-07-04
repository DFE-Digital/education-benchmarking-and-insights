import logging

import pandas as pd

import pipeline.input_schemas as input_schemas
from pipeline.stats_collector import stats_collector

logger = logging.getLogger("fbit-data-pipeline")


def prepare_ks2_data(ks2_path):
    if ks2_path is not None:
        ks2 = pd.read_excel(
            ks2_path,
            usecols=input_schemas.ks2.keys(),
            dtype=input_schemas.ks2,
            engine="calamine",
        )
        logger.info(f"KS2 Data raw shape: {ks2.shape}")
        ks2["READPROG"] = ks2["READPROG"].replace({"SUPP": "0", "LOWCOV": "0"})
        ks2["MATPROG"] = ks2["MATPROG"].replace({"SUPP": "0", "LOWCOV": "0"})
        ks2["WRITPROG"] = ks2["WRITPROG"].replace({"SUPP": "0", "LOWCOV": "0"})

        ks2["Ks2Progress"] = (
            ks2["READPROG"].astype(float)
            + ks2["MATPROG"].astype(float)
            + ks2["WRITPROG"].astype(float)
        )
        ks2 = ks2[["URN", "Ks2Progress"]].dropna().drop_duplicates()
    else:
        ks2 = pd.DataFrame(
            {"URN": pd.Series(dtype="Int64"), "Ks2Progress": pd.Series(dtype="float")}
        )

    ks2_with_index = ks2.set_index("URN")
    stats_collector.collect_preprocessed_ancillary_data_shape(
        "ks2", ks2_with_index.shape
    )

    return ks2_with_index
