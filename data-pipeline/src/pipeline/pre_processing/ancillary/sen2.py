import pandas as pd
from pipeline.utils import log
from pipeline import input_schemas
from pandas._typing import FilePath, ReadCsvBuffer

logger = log.setup_logger(__name__)

def prepare_sen2_data(
    sen2_filepath_or_buffer: FilePath | ReadCsvBuffer[bytes] | ReadCsvBuffer[str],
    year: int,
) -> pd.DataFrame:
    """
    Parse Local Authority SEN2 ECHP plan data.

    Rows of interest are:

    - where `old_la_code` is not NULL
    - where `time_period` equals `year`
    - where `ehcp_or_statement` is `Total`

    :param sen2_filepath_or_buffer: source for LA SEN2 data
    :param year: financial year in question
    :return: Local Authority SEN2 data
    """
    logger.info("Processing Local Authority SEN2 data.")
    df = pd.read_csv(
        sen2_filepath_or_buffer,
        usecols=input_schemas.la_sen2.get(
            year, input_schemas.la_sen2["default"]
        ).keys(),
        na_values=input_schemas.la_sen2_na_values.get(
            year, input_schemas.la_sen2_na_values["default"]
        ),
        dtype=input_schemas.la_sen2.get(year, input_schemas.la_sen2["default"]),
    )

    df = df[~df["old_la_code"].isna()]
    df = df[df["time_period"] == str(year)]
    df = df[df["ehcp_or_statement"] == "Total"]

    _pivot = input_schemas.la_sen2_pivot.get(
        year, input_schemas.la_sen2_pivot["default"]
    )
    # Drop any extraneous columns before pivoting.
    df = df[_pivot["index"] + _pivot["columns"] + _pivot["values"]]
    df = df.pivot(**_pivot).reset_index()

    # note: converting columns to strings; tuple-columns resulting from
    # the above pivot cannot be easily renamed.
    df.columns = map(
        lambda column: f"{column[1]}__{column[2]}" if column[1] else column[0],
        df.columns,
    )

    for column, eval_ in input_schemas.la_sen2_eval.get(
        year, input_schemas.la_sen2_eval["default"]
    ).items():
        df[column] = df.eval(eval_)

    return df.set_index(input_schemas.la_sen2_index_column)
