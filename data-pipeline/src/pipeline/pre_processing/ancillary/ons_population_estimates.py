import pandas as pd
from pipeline.utils import log
from pandas._typing import FilePath, ReadCsvBuffer
from pipeline import input_schemas

logger = log.setup_logger(__name__)

def prepare_ons_population_estimates(
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

    Population data is published for July of each calendar year.
    The data we ingest for the given year needs to fall within
    April to March financial year, here we ingest the data for
    the year prior to the input year.

    For example, if the year is 2024, the relevant data is for the 2023/2024 financial year,
    and thus data from 2023 should be ingested.

    :param ons_filepath_or_buffer: source for ONS LA population data
    :param year: financial year in question
    :return: ONS Local Authority population data
    """
    logger.info("Processing ONS Local Authority population data.")
    dtypes = input_schemas.la_ons_population.get(
        year, input_schemas.la_ons_population["default"]
    )
    dtypes[str(year - 1)] = "float"

    df = pd.read_csv(
        ons_filepath_or_buffer,
        usecols=dtypes.keys(),
        dtype=dtypes,
    )

    df = (
        df[df["AGE_GROUP"].isin(set(map(str, range(2, 19))))]
        .drop(columns=["AGE_GROUP"])
        .groupby(["AREA_CODE"])
        .agg("sum")
        .rename(columns={str(year - 1): "Population2To18"})
        .reset_index()
        .set_index(input_schemas.la_ons_population_index_column)
    )

    df["Population2To18"] = df["Population2To18"].astype(int)

    return df
