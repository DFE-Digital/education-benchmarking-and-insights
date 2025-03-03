import pandas as pd
from pandas._typing import FilePath, ReadCsvBuffer

from pipeline import input_schemas


def build_local_authorities(
    filepath_or_buffer: FilePath | ReadCsvBuffer[bytes] | ReadCsvBuffer[str],
    year: int,
):
    """
    Build Local Authority data from various sources.

    :param filepath_or_buffer: source for LA expenditure data
    :param year: financial year in question
    :return: Local Authority data
    """
    la_expenditure_data = _prepare_la_expenditure_data(filepath_or_buffer, year)

    return la_expenditure_data


def _prepare_la_expenditure_data(
    filepath_or_buffer: FilePath | ReadCsvBuffer[bytes] | ReadCsvBuffer[str],
    year: int,
) -> pd.DataFrame:
    """
    Prepare the Section 251 expenditure (budget) data.

    `time_period` will be of the form, e.g., `202223`—these should be
    taken if the `year` is `2023`.

    Rows of interest are:

    - if, for example, `year` is `2023` then `time_period` equals `202223`
    - where `old_la_code` is not NULL
    - `category_of_planned_expenditure` starts with a configured value

    :param filepath_or_buffer: source for LA expenditure data
    :param year: financial year in question
    :return: Local Authority expenditure data
    """
    df = pd.read_csv(
        filepath_or_buffer,
        usecols=input_schemas.la_expenditure.get(
            year, input_schemas.la_expenditure["default"]
        ).keys(),
        dtype=input_schemas.la_expenditure.get(
            year, input_schemas.la_expenditure["default"]
        ),
        na_values=input_schemas.la_expenditure_na_values.get(
            year, input_schemas.la_expenditure_na_values["default"]
        ),
        keep_default_na=True,
    )

    df = df[~df["old_la_code"].isna()]

    df = df[df["time_period"] == f"{year // 100}{(year % 100) - 1}{year % 100}"]

    df = df[
        df["category_of_planned_expenditure"].str.startswith(
            input_schemas.la_expenditure_category_prefixes.get(
                year, input_schemas.la_expenditure_category_prefixes["default"]
            ),
        )
    ]

    df = (
        df.pivot(
            **input_schemas.la_expenditure_pivot.get(
                year, input_schemas.la_expenditure_pivot["default"]
            ),
        )
        .reset_index()
        .set_index(input_schemas.la_expenditure_index_column)
    )
    # note: converting columns to strings; tuple-columns resulting from
    # the above pivot cannot be easily renamed.
    df.columns = map(
        lambda column: f"{column[1]}__{column[0]}" if column[1] else column[0],
        df.columns,
    )

    df = df.rename(
        columns=input_schemas.la_expenditure_column_mappings.get(
            year,
            input_schemas.la_expenditure_column_mappings["default"],
        )
    )

    for column, eval_ in input_schemas.la_expenditure_column_eval.get(
        year, input_schemas.la_expenditure_column_eval["default"]
    ).items():
        df[column] = df.eval(eval_)

    return df
