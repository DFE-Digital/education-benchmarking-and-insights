import pandas as pd

import pipeline.input_schemas as input_schemas
from pipeline.log import setup_logger

logger = setup_logger("fbit-data-pipeline")


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
    return ks2.set_index("URN")
