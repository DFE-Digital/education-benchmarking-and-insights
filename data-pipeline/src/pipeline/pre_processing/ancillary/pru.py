import pandas as pd

def prepare_pru_data(pru_blob):
    pru = pd.read_csv(pru_blob).rename(
        columns={"Headcount": "PRU_Headcount"}
    ).set_index("LAEstab")
    return pru