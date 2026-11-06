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
    df = pd.read_csv(caseload_filepath_or_buffer)
    df = df[df["breakdown"] == "All EHC plans"].copy()
    drop_cols = input_schemas.la_sen2_caseload_drop_columns.get(
        year, input_schemas.la_sen2_caseload_drop_columns["default"]
    )
    df.drop(columns=drop_cols, inplace=True, errors="ignore")
    df["breakdown"] = "Total"

    academic_time_period = int(f"{year - 1}{year % 100:02d}")
    time_period_map = input_schemas.la_sen2_caseload_time_period.get(
        year,
        {academic_time_period: str(year), str(academic_time_period): str(year)},
    )
    df["time_period"] = df["time_period"].replace(time_period_map).astype("string")

    na_cols = input_schemas.la_sen2_caseload_na_columns.get(
        year, input_schemas.la_sen2_caseload_na_columns["default"]
    )
    df[na_cols] = "x"

    rename_columns = input_schemas.la_sen2_caseload_column_mappings.get(
        year, input_schemas.la_sen2_caseload_column_mappings[2026]
    )
    missing_rename_cols = set(rename_columns.keys()) - set(df.columns)
    if missing_rename_cols:
        raise KeyError(f"Expected columns missing from input: {missing_rename_cols}")

    df.rename(columns=rename_columns, inplace=True)

    for col in df.columns[df.columns.duplicated()].unique():
        combined = (
            df[col]
            .apply(pd.to_numeric, errors="coerce")
            .sum(axis=1, min_count=1)
            .astype("Int64")
            .astype("object")
        )
        df.drop(columns=[col], inplace=True)
        df[col] = combined

    id_vars = input_schemas.la_sen2_caseload_id_vars.get(
        year, input_schemas.la_sen2_caseload_id_vars["default"]
    )
    value_vars = list(dict.fromkeys(list(rename_columns.values()) + list(na_cols)))
    df = df.melt(
        id_vars=id_vars,
        value_vars=value_vars,
        var_name="establishment_type",
        value_name="num_caseload",
    )

    df["num_caseload"] = df["num_caseload"].fillna("x")

    group_rules = input_schemas.la_sen2_caseload_group_rules.get(
        year, input_schemas.la_sen2_caseload_group_rules["default"]
    )

    def assign_group(name: str) -> str:
        for pattern, group in group_rules:
            if pattern in name:
                return group
        return "z"

    type_to_group = {t: assign_group(t) for t in df["establishment_type"].unique()}
    df["establishment_group"] = df["establishment_type"].map(type_to_group)

    df.rename(columns={"breakdown": "ehcp_or_statement"}, inplace=True)

    final_order = input_schemas.la_sen2_caseload_final_order.get(
        year, input_schemas.la_sen2_caseload_final_order["default"]
    )
    df = df[final_order].copy()

    schema = input_schemas.la_sen2.get(year, input_schemas.la_sen2["default"])
    na_values = input_schemas.la_sen2_na_values.get(
        year, input_schemas.la_sen2_na_values["default"]
    )
    for col, col_dtype in schema.items():
        if col_dtype == "Int64":
            df[col] = pd.to_numeric(df[col], errors="coerce").astype("Int64")
        else:
            if col in na_values:
                df[col] = df[col].replace(na_values[col], pd.NA)
            df[col] = df[col].astype(col_dtype)

    return df


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
