import pandas as pd

from pipeline import input_schemas


def prepare_pru_data(pru_blob, year: int = 2025):
    schema = input_schemas.pru_cols.get(year, input_schemas.pru_cols["default"])
    pru = pd.read_csv(pru_blob, usecols=schema.keys(), dtype=schema).rename(
        columns={"Headcount": "PRU_Headcount"}
    )
    return pru
