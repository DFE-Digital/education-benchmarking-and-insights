import pandas as pd
from pandas._typing import (
    FilePath,
    ReadCsvBuffer,
)

from pipeline import log
import pipeline.input_schemas as input_schemas


logger = log.setup_logger(__name__)


def predecessor_links(
    filepath_or_buffer: FilePath | ReadCsvBuffer[bytes] | ReadCsvBuffer[str],
) -> pd.DataFrame:
    """
    Read GIAS-links _Predecessor_ data.

    :param base_data_path: source for GIAS-links data
    :param year: financial year in question
    """
    gias_links = pd.read_csv(
        filepath_or_buffer,
        encoding="cp1252",
        index_col=input_schemas.gias_links_index_col,
        usecols=input_schemas.gias_links.keys(),
        dtype=input_schemas.gias_links,
    )

    logger.info(f"Read {len(gias_links.index):,} GIAS-links records.")

    predecessors = gias_links[gias_links["LinkType"] == "Predecessor"]

    logger.info(f"Read {len(predecessors.index):,} predecessor GIAS-links records.")

    return predecessors
