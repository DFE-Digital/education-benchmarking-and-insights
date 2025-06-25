import logging

import pandas as pd

from pipeline import input_schemas

logger = logging.getLogger("fbit-data-pipeline")


def build_cfo_data(cfo_data_path, year: int) -> pd.DataFrame:
    """
    Read Chief Financial Officer (CFO) details.

    Note: CFO details are at Trust level.

    :param cfo_data_path: from which to read data
    :param year: financial year in question
    :return: CFO DataFrame
    """
    cfo_data = pd.read_excel(
        cfo_data_path,
        usecols=input_schemas.cfo.get(year, input_schemas.cfo["default"]).keys(),
        dtype=input_schemas.cfo.get(year, input_schemas.cfo["default"]),
        engine="calamine",
    ).rename(
        columns=input_schemas.cfo_column_mappings.get(
            year, input_schemas.cfo_column_mappings["default"]
        ),
    )
    logger.info(f"CFO raw {year=} data shape: {cfo_data.shape}")

    for column, eval_ in input_schemas.cfo_column_eval.get(
        year, input_schemas.cfo_column_eval["default"]
    ).items():
        cfo_data[column] = cfo_data.eval(eval_)

    return cfo_data[["Companies House Number", "CFO name", "CFO email"]]
