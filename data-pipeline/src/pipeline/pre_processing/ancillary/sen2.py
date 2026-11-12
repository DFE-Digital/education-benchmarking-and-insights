import pandas as pd
from pandas._typing import FilePath, ReadCsvBuffer

from pipeline import input_schemas
from pipeline.utils import log

logger = log.setup_logger(__name__)


def pivot_sen2_caseload_data(
    caseload_filepath_or_buffer: FilePath | ReadCsvBuffer[bytes] | ReadCsvBuffer[str],
    year: int,
) -> pd.DataFrame:
    """
    Transform raw SEN2 `caseload.csv` data into the `sen2_estab_caseload` format.

    :param caseload_filepath_or_buffer: source for raw SEN2 caseload data
    :param year: financial year in question
    :return: unpivoted Local Authority SEN2 caseload data
    """
    schema = input_schemas.la_sen2.get(year, input_schemas.la_sen2["default"])
    na_values = input_schemas.la_sen2_na_values.get(
        year, input_schemas.la_sen2_na_values["default"]
    )["num_caseload"]
    column_mappings = input_schemas.la_sen2_caseload_column_mappings.get(
        year, input_schemas.la_sen2_caseload_column_mappings[2026]
    )
    legacy_na_columns = input_schemas.la_sen2_caseload_na_columns.get(
        year, input_schemas.la_sen2_caseload_na_columns["default"]
    )
    id_vars = input_schemas.la_sen2_caseload_id_vars.get(
        year, input_schemas.la_sen2_caseload_id_vars["default"]
    )
    group_rules = input_schemas.la_sen2_caseload_group_rules.get(
        year, input_schemas.la_sen2_caseload_group_rules["default"]
    )
    usecols = id_vars + list(column_mappings.keys())

    df = pd.read_csv(
        caseload_filepath_or_buffer,
        usecols=usecols,
        na_values={col: na_values for col in [*column_mappings, "old_la_code"]},
    )

    academic_year = int(f"{year - 1}{year % 100:02d}")
    df = df[(df["time_period"] == academic_year) & (df["breakdown"] == "All EHC plans")]
    df["breakdown"] = "Total"
    df["time_period"] = year
    df[legacy_na_columns] = pd.NA
    df = df.rename(columns=column_mappings)

    # Combine split establishment columns that map to the same target name
    # (e.g. 2026 special academy and special free school columns).
    for col in df.columns[df.columns.duplicated()].unique():
        combined = df[col].sum(axis=1, min_count=1)
        df = df.drop(columns=[col])
        df[col] = combined

    value_vars = list(column_mappings.values()) + legacy_na_columns
    df = df.melt(
        id_vars=id_vars,
        value_vars=value_vars,
        var_name="establishment_type",
        value_name="num_caseload",
    )

    df["establishment_group"] = df["establishment_type"].map(group_rules).fillna("z")

    df = df.rename(columns={"breakdown": "ehcp_or_statement"})
    output_columns = [
        col for col in id_vars if col != "breakdown" and col in df.columns
    ] + [
        "establishment_group",
        "establishment_type",
        "ehcp_or_statement",
        "num_caseload",
    ]

    return df[output_columns].astype(schema)


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
    if year in input_schemas.la_sen2_caseload_column_mappings:
        df = pivot_sen2_caseload_data(sen2_filepath_or_buffer, year)
    else:
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
