import pandas as pd
from pandas._typing import FilePath, ReadCsvBuffer

import pipeline.input_schemas as input_schemas
from pipeline import log

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


def link_data(
    df: pd.DataFrame,
    linkable: pd.DataFrame,
    gias_links: pd.DataFrame,
) -> pd.DataFrame:
    """
    Extend a dataset via GIAS-links.

    - `df`: point of reference, must have `URN`
    - `linkable`: must have `URN`, an extended version of this
      `DataFrame` will be returned
    - `gias_links`: must have `URN` and `LinkURN`, will be used to
      extend `linkable` with records missing from `df`.

    The data present in `linkable` may be missing URNs present in the
    `df` data. Where this is the case, we extend those data with
    additional records referencing the GIAS-link `LinkURN` (i.e. the
    data will then contain a record for both the GIAS-link `URN` and
    `LinkURN`).

    - determine which `df` records are missing from the `linkable`
    - of those missing records, determine which have GIAS-link records
    - of those GIAS-links, determine which have `linkable` records
    - supplement the `linkable` records, adding records with the
      mapped GIAS-links
    """
    _df = df.reset_index()[["URN"]]
    _linkable = linkable.reset_index()
    _gias_links = gias_links.reset_index()[["URN", "LinkURN"]]

    df_missing = _df[~_df["URN"].isin(_linkable["URN"])][["URN"]]

    link_urns = _gias_links[
        (_gias_links["URN"].isin(df_missing["URN"]))
        & (_gias_links["LinkURN"].isin(_linkable["URN"]))
    ][["URN", "LinkURN"]]

    linked = (
        _linkable[_linkable["URN"].isin(link_urns["LinkURN"])]
        .merge(
            link_urns,
            how="inner",
            left_on="URN",
            right_on="LinkURN",
            suffixes=("_census", "_link"),
        )
        .drop(columns=["URN_census", "LinkURN"])
        .rename(columns={"URN_link": "URN"})
        .set_index("URN")
    )

    return pd.concat([linkable, linked])
