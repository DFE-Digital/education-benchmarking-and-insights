import pandas as pd
from pandas._typing import FilePath, ReadCsvBuffer

from pipeline import input_schemas
from pipeline.utils import log

logger = log.setup_logger(__name__)


def prepare_la_statistical_neighbours(
    filepath_or_buffer: FilePath | ReadCsvBuffer[bytes] | ReadCsvBuffer[str], year: int
):
    """
    Prepare the Local Authority statistical neighbours data.
    This is mostly processed as-is from the raw data however
    some mapping to deal with duplicate column names takes place using
    the mapping set out in the input schemas.
    Also removes rows where LA number is absent (due to extraneous
    rows in the input file).

    :param filepath_or_buffer: source for LA statistical neighbours data
    :param year: financial year in question
    :return: Local Authority statistical neighbours data
    """
    logger.info("Processing Local Authority statistical neighbours data.")
    df = pd.read_excel(
        filepath_or_buffer,
        engine="calamine",
        sheet_name="SNsWithNewDorsetBCP",
        index_col=input_schemas.la_statistical_neighbours_index_col,
        dtype=input_schemas.la_statistical_neighbours.get(
            year, input_schemas.la_statistical_neighbours["default"]
        ),
        usecols=input_schemas.la_statistical_neighbours.get(
            year, input_schemas.la_statistical_neighbours["default"]
        ).keys(),
    ).rename(
        columns=input_schemas.la_statistical_neighbours_column_mappings.get(
            year, input_schemas.la_statistical_neighbours_column_mappings["default"]
        ),
    )

    return df[~df.index.isna()]
