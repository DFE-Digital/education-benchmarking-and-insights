import pandas as pd


def build_cfo_data(cfo_data_path) -> pd.DataFrame:
    """
    Read Chief Financial Officer (CFO) details.

    Note: CFO details are at Trust level.

    :param cfo_data_path: from which to read data
    :return: cfo DataFrame
    """
    cfo_data = pd.read_excel(
        cfo_data_path,
        usecols=[
            "Companies House Number",
            "Title",
            "Forename 1",
            "Surname",
            "Direct email address",
        ],
        dtype=str,
    ).rename(
        columns={
            "Direct email address": "CFO email",
        },
    )

    cfo_data["CFO name"] = (
        cfo_data["Title"] + " " + cfo_data["Forename 1"] + " " + cfo_data["Surname"]
    )

    return cfo_data[["Companies House Number", "CFO name", "CFO email"]]
