import pandas as pd
from pandas._typing import FilePath, ReadCsvBuffer

from pipeline import input_schemas, log

logger = log.setup_logger(__name__)


def build_local_authorities(
    budget_filepath_or_buffer: FilePath | ReadCsvBuffer[bytes] | ReadCsvBuffer[str],
    outturn_filepath_or_buffer: FilePath | ReadCsvBuffer[bytes] | ReadCsvBuffer[str],
    statistical_neighbours_filepath: (
        FilePath | ReadCsvBuffer[bytes] | ReadCsvBuffer[str]
    ),
    ons_filepath_or_buffer: FilePath | ReadCsvBuffer[bytes] | ReadCsvBuffer[str],
    sen2_filepath_or_buffer: FilePath | ReadCsvBuffer[bytes] | ReadCsvBuffer[str],
    year: int,
):
    """
    Build Local Authority data from various sources.

    :param budget_filepath_or_buffer: source for LA budget data
    :param outturn_filepath_or_buffer: source for LA outturn data
    :param statistical_neighbours_filepath: source for LA statistical neighbours data
    :param ons_filepath_or_buffer: source for ONS LA data
    :param sen2_filepath_or_buffer: source for LA SEN2 data
    :param year: financial year in question
    :return: Local Authority data
    """
    section_251_data = _build_section_251_data(
        budget_filepath_or_buffer,
        outturn_filepath_or_buffer,
        year,
    )
    ons_la_population_data = _prepare_ons_la_population_data(
        ons_filepath_or_buffer,
        year,
    )
    statistical_neighbours_data = _prepare_la_statistical_neighbours(
        statistical_neighbours_filepath, year
    )
    sen_2_data = _prepare_sen2_la_data(
        sen2_filepath_or_buffer,
        year,
    )

    logger.info("Processing Local Authority combined data.")

    local_authority_data = (
        section_251_data.merge(
            statistical_neighbours_data,
            left_on="old_la_code",
            right_index=True,
            how="left",
        )
        .merge(
            ons_la_population_data,
            left_on="new_la_code",
            right_index=True,
            how="left",
        )
        .merge(
            sen_2_data,
            left_index=True,
            right_index=True,
            how="left",
        )
    )

    logger.info(
        f"Processed {len(local_authority_data.index)} combined Local Authority rows."
    )

    return local_authority_data


def _build_section_251_data(
    budget_filepath_or_buffer: FilePath | ReadCsvBuffer[bytes] | ReadCsvBuffer[str],
    outturn_filepath_or_buffer: FilePath | ReadCsvBuffer[bytes] | ReadCsvBuffer[str],
    year: int,
):
    """
    Build Local Authority Section 251 data.

    This is comprised of:

    - planned budget/expenditure data
    - outturn data

    The returned data will the data from each, columns prefixed with
    `Budget` or `Outturn`, indexed on `old_la_code` and `new_la_code`.

    :param budget_filepath_or_buffer: source for LA budget data
    :param outturn_filepath_or_buffer: source for LA outturn data
    :param year: financial year in question
    :return: Section 251 data
    """
    logger.info("Processing Local Authority budget data.")
    la_budget_data = _prepare_la_section_251_data(
        budget_filepath_or_buffer,
        year,
        usecols=input_schemas.la_budget.get(
            year, input_schemas.la_budget["default"]
        ).keys(),
        dtype=input_schemas.la_budget.get(year, input_schemas.la_budget["default"]),
        category_column="category_of_planned_expenditure",
        column_mappings=input_schemas.la_budget_column_mappings.get(
            year,
            input_schemas.la_budget_column_mappings["default"],
        ),
        column_eval=input_schemas.la_budget_column_eval.get(
            year, input_schemas.la_budget_column_eval["default"]
        ),
        column_pivot=input_schemas.la_budget_pivot.get(
            year, input_schemas.la_budget_pivot["default"]
        ),
    ).rename(
        columns=input_schemas.la_section_251_column_mappings.get(
            year,
            input_schemas.la_section_251_column_mappings["default"],
        )
    )

    for column, eval_ in input_schemas.la_section_251_column_eval.get(
        year, input_schemas.la_section_251_column_eval["default"]
    ).items():
        la_budget_data[column] = la_budget_data.eval(eval_)

    la_budget_data = la_budget_data[
        input_schemas.la_section_251_columns.get(
            year, input_schemas.la_section_251_columns["default"]
        )
    ].add_prefix("Budget")

    logger.info(
        f"Processed {len(la_budget_data.index)} rows from Local Authority budget data."
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
    ).rename(
        columns=input_schemas.la_section_251_column_mappings.get(
            year,
            input_schemas.la_section_251_column_mappings["default"],
        )
    )

    for column, eval_ in input_schemas.la_section_251_column_eval.get(
        year, input_schemas.la_section_251_column_eval["default"]
    ).items():
        la_outturn_data[column] = la_outturn_data.eval(eval_)

    la_outturn_data = la_outturn_data[
        input_schemas.la_section_251_columns.get(
            year, input_schemas.la_section_251_columns["default"]
        )
    ].add_prefix("Outturn")

    logger.info(
        f"Processed {len(la_outturn_data.index)} rows from Local Authority outturn data."
    )

    return la_budget_data.join(
        la_outturn_data,
        how="inner",
    )


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
    :return: Local Authority Section 251 data
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


def _prepare_la_statistical_neighbours(
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


def _prepare_ons_la_population_data(
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


def _prepare_sen2_la_data(
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
