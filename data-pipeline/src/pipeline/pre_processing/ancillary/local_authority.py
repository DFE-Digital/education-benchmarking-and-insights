import pandas as pd
from pandas._typing import FilePath, ReadCsvBuffer

from pipeline import input_schemas, log

logger = log.setup_logger(__name__)


def build_local_authorities(
    budget_filepath_or_buffer: FilePath | ReadCsvBuffer[bytes] | ReadCsvBuffer[str],
    outturn_filepath_or_buffer: FilePath | ReadCsvBuffer[bytes] | ReadCsvBuffer[str],
    year: int,
):
    """
    Build Local Authority data from various sources.

    :param budget_filepath_or_buffer: source for LA expenditure data
    :param outturn_filepath_or_buffer: source for LA outturn data
    :param year: financial year in question
    :return: Local Authority data
    """
    logger.info("Processing Local Authority budget/expenditure data.")
    la_expenditure_data = _prepare_la_section_251_data(
        budget_filepath_or_buffer,
        year,
        usecols=input_schemas.la_expenditure.get(
            year, input_schemas.la_expenditure["default"]
        ).keys(),
        dtype=input_schemas.la_expenditure.get(
            year, input_schemas.la_expenditure["default"]
        ),
        category_column="category_of_planned_expenditure",
        column_mappings=input_schemas.la_expenditure_column_mappings.get(
            year,
            input_schemas.la_expenditure_column_mappings["default"],
        ),
        column_eval=input_schemas.la_expenditure_column_eval.get(
            year, input_schemas.la_expenditure_column_eval["default"]
        ),
        column_pivot=input_schemas.la_expenditure_pivot.get(
            year, input_schemas.la_expenditure_pivot["default"]
        ),
    )
    logger.info(
        f"Processed {len(la_expenditure_data.index)} rows from Local Authority budget/expenditure data."
    )

    logger.info("Preparing LA outturn data.")
    la_outturn_data = _prepare_la_section_251_data(
        outturn_filepath_or_buffer,
        year,
        usecols=input_schemas.la_outturn.get(
            year, input_schemas.la_outturn["default"]
        ).keys(),
        dtype=input_schemas.la_outturn.get(year, input_schemas.la_outturn["default"]),
        category_column="category_of_expenditure",
        column_mappings=input_schemas.la_outturn_column_mappings.get(
            year,
            input_schemas.la_outturn_column_mappings["default"],
        ),
        column_eval=input_schemas.la_outturn_column_eval.get(
            year, input_schemas.la_outturn_column_eval["default"]
        ),
        column_pivot=input_schemas.la_outturn_pivot.get(
            year, input_schemas.la_outturn_pivot["default"]
        ),
        encoding="cp1252",
    )
    logger.info(
        f"Processed {len(la_outturn_data.index)} rows from Local Authority outturn data."
    )

    # TODO: combine the above once projections finalised.
    return la_expenditure_data


def _prepare_la_section_251_data(
    filepath_or_buffer: FilePath | ReadCsvBuffer[bytes] | ReadCsvBuffer[str],
    year: int,
    usecols: list[str],
    dtype: dict[str, str],
    category_column: str,
    column_mappings: dict[str, str],
    column_eval: dict[str, str],
    column_pivot: dict[str, list[str]],
    encoding="utf-8",
) -> pd.DataFrame:
    """
    Prepare the Section 251 data ("budget" or "outturn").

    `time_period` will be of the form, e.g., `202223`—these should be
    taken if the `year` is `2023`.

    Rows of interest are:

    - if, for example, `year` is `2023` then `time_period` equals `202223`
    - where `old_la_code` is not NULL
    - `category_column` starts with a configured value

    :param filepath_or_buffer: source for LA data
    :param year: financial year in question
    :param usecols: subset of columns to select
    :param dtype: data-types for selected columns
    :param category_column: column containing categories of interest
    :param column_mappings: columns to be renamed
    :param column_eval: columns to be derived
    :param column_pivot: column-pivot configuration
    :return: Local Authority expenditure data
    """
    df = pd.read_csv(
        filepath_or_buffer,
        usecols=usecols,
        dtype=dtype,
        na_values=input_schemas.la_section_251_na_values.get(
            year, input_schemas.la_section_251_na_values["default"]
        ),
        keep_default_na=True,
        encoding=encoding,
    )

    df = df[~df["old_la_code"].isna()]

    df = df[df["time_period"] == f"{year // 100}{(year % 100) - 1}{year % 100}"]

    df = df[
        df[category_column].str.startswith(
            input_schemas.la_section_251_category_prefixes.get(
                year, input_schemas.la_section_251_category_prefixes["default"]
            ),
        )
    ]

    df = (
        df.pivot(**column_pivot)
        .reset_index()
        .set_index(input_schemas.la_section_251_index_column)
    )
    # note: converting columns to strings; tuple-columns resulting from
    # the above pivot cannot be easily renamed.
    df.columns = map(
        lambda column: f"{column[1]}__{column[0]}" if column[1] else column[0],
        df.columns,
    )

    df = df.rename(columns=column_mappings)

    for column, eval_ in column_eval.items():
        df[column] = df.eval(eval_)

    return df
