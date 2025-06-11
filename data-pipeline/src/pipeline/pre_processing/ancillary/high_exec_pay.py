import pandas as pd
from pandas._typing import FilePath, ReadCsvBuffer

from pipeline.input_schemas import high_exec_pay, high_exec_pay_column_mappings


def build_high_exec_pay_data(
    filepath_or_buffer: FilePath | ReadCsvBuffer[bytes] | ReadCsvBuffer[str],
    year: int,
) -> pd.DataFrame:
    """
    Build the high exec pay dataset.

    :param filepath_or_buffer: source for high exec pay data
    :param year: financial year in question
    :return: high exec pay data
    """
    high_exec_pay_data_raw = pd.read_csv(
        filepath_or_buffer,
        usecols=high_exec_pay["default"].keys(),
        dtype=high_exec_pay["default"],
    )

    high_exec_pay_data = high_exec_pay_data_raw.rename(columns=high_exec_pay_column_mappings["default"])

    # The raw file has a "C" preceding all CRNs which will intefere with joins
    high_exec_pay_data["Company Registration Number"] = high_exec_pay_data[
        "Company Registration Number"
    ].str[1:]

    return high_exec_pay_data
