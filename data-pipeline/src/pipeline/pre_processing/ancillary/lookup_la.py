import pandas as pd

def prepare_lookup_la_data(lookup_la_blob):
    return pd.read_csv(lookup_la_blob)