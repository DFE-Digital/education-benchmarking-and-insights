import pandas as pd

from pipeline import input_schemas


def prepare_lookup_la_data(lookup_la_blob, year: int = 2025):
    schema = input_schemas.lookup_la_cols.get(
        year, input_schemas.lookup_la_cols["default"]
    )
    return pd.read_csv(lookup_la_blob, usecols=schema.keys(), dtype=schema)
