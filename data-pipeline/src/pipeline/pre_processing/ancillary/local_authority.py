import pandas as pd
from pandas._typing import FilePath, ReadCsvBuffer

from pipeline import input_schemas, log

logger = log.setup_logger(__name__)


def build_local_authorities(
    ons_filepath_or_buffer: FilePath | ReadCsvBuffer[bytes] | ReadCsvBuffer[str],
    year: int,
) -> pd.DataFrame:
    """
    Build Local Authority data from various sources.

    :param ons_filepath_or_buffer: source for ONS LA population data
    :param year: financial year in question
    :return: Local Authority data
    """
    ons_la_population_data = _build_ons_la_population_data(
        ons_filepath_or_buffer,
        year,
    )

    return ons_la_population_data


def _build_ons_la_population_data(
    ons_filepath_or_buffer: FilePath | ReadCsvBuffer[bytes] | ReadCsvBuffer[str],
    year: int,
) -> pd.DataFrame:
    """
    Parse ONS Local Authority population data.

    The rows of interest will be those where the `AGE_GROUP` is `2` to
    `18` (inclusive).

    The value in the rows for the each LA (identified by the
    `AREA_CODE`) will be summed to provide a single figure, to be
    rounded to a whole integer.

    :param ons_filepath_or_buffer: source for ONS LA population data
    :param year: financial year in question
    :return: ONS Local Authority population data
    """
    dtypes = input_schemas.la_ons_population.get(
        year, input_schemas.la_ons_population["default"]
    )
    dtypes[str(year)] = "float"

    df = pd.read_csv(
        ons_filepath_or_buffer,
        usecols=dtypes.keys(),
        dtype=dtypes,
    )

    df = (
        df[df["AGE_GROUP"].isin(set(map(str, range(2, 19))))]
        .drop(columns=["AGE_GROUP"])
        .groupby(["AREA_CODE"])
        .agg(sum)
        .rename(columns={str(year): "Population2To18"})
        .reset_index()
        .set_index(input_schemas.la_ons_population_index_column)
    )

    df["Population2To18"] = df["Population2To18"].astype(int)

    return df
