import pandas as pd
from pandas._typing import FilePath, ReadCsvBuffer

from pipeline import input_schemas
from pipeline.utils import log

from .loader_config import la_budget_reader_config, la_outturn_reader_config

logger = log.setup_logger(__name__)


def _load_s251_source(
    filepath_or_buffer: FilePath | ReadCsvBuffer[bytes] | ReadCsvBuffer[str],
    year: int,
    schema_by_year: dict[int | str, dict[str, str]],
    reader_config_by_year: dict[int | str, dict],
    raw_column_mappings_by_year: dict[int | str, dict[str, str]],
) -> pd.DataFrame:
    schema = schema_by_year.get(year, schema_by_year["default"])
    reader_config = reader_config_by_year.get(year, reader_config_by_year["default"])
    raw_column_mappings = raw_column_mappings_by_year.get(
        year, raw_column_mappings_by_year["default"]
    )
    na_values = input_schemas.la_section_251_na_values.get(
        year, input_schemas.la_section_251_na_values["default"]
    )

    if reader_config["file_format"] == "excel":
        df = pd.read_excel(
            filepath_or_buffer,
            sheet_name=reader_config["sheet_name"],
            header=reader_config["header"],
            engine=reader_config["engine"],
            usecols=list(schema.keys()),
            dtype=schema,
            na_values=na_values,
            keep_default_na=True,
        )
    else:
        df = pd.read_csv(
            filepath_or_buffer,
            usecols=list(schema.keys()),
            dtype=schema,
            na_values=na_values,
            keep_default_na=True,
            encoding=reader_config["encoding"],
        )

    if raw_column_mappings:
        df = df.rename(columns=raw_column_mappings)

    if reader_config.get("year_digits") == 4:
        df["time_period"] = f"{year // 100}" + df["time_period"]

    return df


def load_s251_budget(
    filepath_or_buffer: FilePath | ReadCsvBuffer[bytes] | ReadCsvBuffer[str],
    year: int,
) -> pd.DataFrame:
    """Load and normalise raw Section 251 planned expenditure data into a DataFrame."""
    df = _load_s251_source(
        filepath_or_buffer,
        year,
        schema_by_year=input_schemas.la_budget,
        reader_config_by_year=la_budget_reader_config,
        raw_column_mappings_by_year=input_schemas.la_budget_raw_column_mappings,
    )
    logger.info(f"Loaded S251 budget raw shape: {df.shape}")
    return df


def load_s251_outturn(
    filepath_or_buffer: FilePath | ReadCsvBuffer[bytes] | ReadCsvBuffer[str],
    year: int,
) -> pd.DataFrame:
    """Load and normalise raw Section 251 outturn data into a DataFrame."""
    df = _load_s251_source(
        filepath_or_buffer,
        year,
        schema_by_year=input_schemas.la_outturn,
        reader_config_by_year=la_outturn_reader_config,
        raw_column_mappings_by_year=input_schemas.la_outturn_raw_column_mappings,
    )
    logger.info(f"Loaded S251 outturn raw shape: {df.shape}")
    return df
