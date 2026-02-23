import pandas as pd
from pandas._typing import FilePath, ReadCsvBuffer

from pipeline import input_schemas
from pipeline.utils import log

logger = log.setup_logger(__name__)


def build_local_authorities(
    budget_filepath_or_buffer: FilePath | ReadCsvBuffer[bytes] | ReadCsvBuffer[str],
    outturn_filepath_or_buffer: FilePath | ReadCsvBuffer[bytes] | ReadCsvBuffer[str],
    la_statistical_neighbours: pd.DataFrame,
    ons_population_estimates: pd.DataFrame,
    sen2: pd.DataFrame,
    place_numbers: pd.DataFrame,
    dsg: pd.DataFrame,
    all_schools: pd.DataFrame,
    year: int,
):
    """
    Build Local Authority data from various sources.

    :param budget_filepath_or_buffer: source for LA budget data
    :param outturn_filepath_or_buffer: source for LA outturn data
    :param statistical_neighbours_filepath: source for LA statistical neighbours data
    :param ons_filepath_or_buffer: source for ONS LA data
    :param sen2_filepath_or_buffer: source for LA SEN2 data
    :param all_schools: FBIT schools data for pupil aggregations
    :param year: financial year in question
    :return: Local Authority data
    """
    section_251_data = _build_section_251_data(
        budget_filepath_or_buffer,
        outturn_filepath_or_buffer,
        year,
    )
    logger.info("Processing Local Authority combined data.")

    fbit_pupils_per_la = _aggregate_fbit_pupil_numbers_to_la_level(all_schools)

    local_authority_data = (
        section_251_data.merge(
            la_statistical_neighbours,
            left_on="old_la_code",
            right_index=True,
            how="left",
        )
        .merge(
            ons_population_estimates,
            left_on="new_la_code",
            right_index=True,
            how="left",
        )
        .merge(
            fbit_pupils_per_la,
            left_on="old_la_code",
            right_index=True,
            how="left",
        )
    )

    local_authority_data_with_sen = _join_sen_to_local_authority_data(
        local_authority_data, sen2
    )

    local_authority_data_with_dsg_recoupments = _calculate_dsg_recoupments(
        local_authority_data_with_sen, all_schools[["LA", "Overall Phase"]], place_numbers, dsg
    )

    logger.info(
        f"Processed {len(local_authority_data.index)} combined Local Authority rows."
    )

    return local_authority_data_with_dsg_recoupments


def _calculate_dsg_recoupments(
    local_authority_data, school_to_la_mapping, place_numbers, dsg
):
    place_numbers_with_la = place_numbers.set_index("URN").join(school_to_la_mapping, how="left")
    six_k_places_col = "2024 to 2025 mainstream academies and free schools - pre-16 SEN Unit/RP Places (@£6k)"
    ten_k_places_col = "2024 to 2025 mainstream academies and free schools - pre-16 SEN Unit/RP Places (@£10k)"
    primary = "Primary"
    secondary = "Secondary"
    PRIMARY_PLACES_6K = "PrimaryPlaces6000"
    PRIMARY_PLACES_10K = "PrimaryPlaces10000"
    SECONDARY_PLACES_6K = "SecondaryPlaces6000"
    SECONDARY_PLACES_10K = "SecondaryPlaces10000"
    place_numbers_per_phase_and_la = place_numbers_with_la \
        .groupby(["LA", "Overall Phase"]) \
        .agg(
            {
                "2024 to 2025 free schools and academies - pre-16 AP places": "sum",
                six_k_places_col: "sum",
                ten_k_places_col: "sum",
            }
        ).reset_index()
    place_numbers_pivoted = place_numbers_per_phase_and_la.pivot(index='LA', columns='Overall Phase')

    place_numbers_final = pd.DataFrame(index=place_numbers_pivoted.index)
    place_numbers_final[PRIMARY_PLACES_6K] = place_numbers_pivoted[(six_k_places_col, primary)]
    place_numbers_final[PRIMARY_PLACES_10K] = place_numbers_pivoted[(ten_k_places_col, primary)]
    place_numbers_final[SECONDARY_PLACES_6K] = place_numbers_pivoted[(six_k_places_col, secondary)]
    place_numbers_final[SECONDARY_PLACES_10K] = place_numbers_pivoted[(ten_k_places_col, secondary)]

    dsg_with_place_numbers = dsg.join(place_numbers_final, how="outer")
    dsg_with_place_numbers["TotalPlaceFunding"] = (
        dsg_with_place_numbers[PRIMARY_PLACES_6K] * 6000 
        + dsg_with_place_numbers[PRIMARY_PLACES_10K] * 10000
        + dsg_with_place_numbers[SECONDARY_PLACES_6K] * 6000 
        + dsg_with_place_numbers[SECONDARY_PLACES_10K] * 10000
    )
    dsg_with_place_numbers["PrimaryPlaceFundingRatio"] = (
        (
            dsg_with_place_numbers[PRIMARY_PLACES_6K] * 6000 
            + dsg_with_place_numbers[PRIMARY_PLACES_10K] * 10000
        ) / dsg_with_place_numbers["TotalPlaceFunding"]
    )
    dsg_with_place_numbers["SecondaryPlaceFundingRatio"] = (
        (
            dsg_with_place_numbers[SECONDARY_PLACES_6K] * 6000 
            + dsg_with_place_numbers[SECONDARY_PLACES_10K] * 10000
        ) / dsg_with_place_numbers["TotalPlaceFunding"]
    )

    dsg_with_place_numbers["NurseryPlaceFunding"] = 0
    dsg_with_place_numbers["PrimaryAcademyPlaceFunding"] = (
        dsg_with_place_numbers['Total Mainstream Pre-16 SEN places deduction']
        * dsg_with_place_numbers["PrimaryPlaceFundingRatio"]
    )
    dsg_with_place_numbers["SecondaryAcademyPlaceFunding"] = (
        dsg_with_place_numbers['Total Mainstream Pre-16 SEN places deduction']
        * dsg_with_place_numbers["SecondaryPlaceFundingRatio"]
    )

    las_with_recoupments = local_authority_data.merge(
        dsg_with_place_numbers,
        left_on='old_la_code',
        right_index=True,
        how='left'
    )

    # Overwritten in place
    las_with_recoupments["OutturnPlaceFundingPrimary"] += las_with_recoupments["PrimaryAcademyPlaceFunding"]
    las_with_recoupments["OutturnPlaceFundingSecondary"] += las_with_recoupments["SecondaryAcademyPlaceFunding"]
    las_with_recoupments["OutturnPlaceFundingSpecial"] += las_with_recoupments["SENAcademyPlaceFunding"]
    las_with_recoupments["OutturnPlaceFundingAlternativeProvision"] += las_with_recoupments["APAcademyPlaceFunding"]
    
    return las_with_recoupments


def _join_sen_to_local_authority_data(
    local_authority_data: pd.DataFrame, sen_2_data: pd.DataFrame
):
    """If EHCP data doesn't join on old and new LA code, try just joining on old LA code."""
    first_join_to_sen_data = local_authority_data.merge(
        sen_2_data,
        left_index=True,
        right_index=True,
        how="left",
    )
    la_data_which_succeeded_sen_join = first_join_to_sen_data[
        first_join_to_sen_data["EHCPTotal"].notna()
    ]

    la_data_which_failed_sen_join = local_authority_data.loc[
        first_join_to_sen_data[first_join_to_sen_data["EHCPTotal"].isna()].index
    ]
    # Resetting the index is needed to maintain the multiindex, preferring the new_la_code from s251
    second_join_to_sen_data = (
        la_data_which_failed_sen_join.reset_index()
        .merge(
            sen_2_data,
            left_on="old_la_code",
            right_on="old_la_code",
            how="left",
        )
        .set_index(["new_la_code", "old_la_code"])
    )

    # If there are any duplicates in `sen_2_data`'s `old_la_code`, try to keep the one with data.
    # This is a workaround for 2024, in which there are multiple new LA codes per old LA code.
    if second_join_to_sen_data.duplicated(subset=local_authority_data.columns).any():
        second_join_to_sen_data.dropna(subset=["EHCPTotal"], inplace=True)
        if second_join_to_sen_data.duplicated(
            subset=local_authority_data.columns
        ).any():
            logger.info(
                f"EHCP file linkage broken for LA codes {str(list(second_join_to_sen_data.index))}"
            )

    combined_la_join_to_sen = pd.concat(
        [la_data_which_succeeded_sen_join, second_join_to_sen_data]
    )

    return combined_la_join_to_sen


def _aggregate_fbit_pupil_numbers_to_la_level(
    all_schools: pd.DataFrame,
) -> pd.DataFrame:
    """Aggregate FBIT adjusted pupil numbers per LA"""
    # Lead federated schools have the pupil total for their federation already aggregated, so
    # non-lead schools shouldn't be counted or double counting will occur
    non_lead_federated_schools_mask = (
        (all_schools["Finance Type"] == "Maintained")
        & all_schools["Lead school in federation"].notna()
        & (all_schools["Lead school in federation"] != "0")
        & (
            all_schools["LA Code"].map(str)
            + all_schools["EstablishmentNumber"].map(str)
            != all_schools["Lead school in federation"]
        )
    )
    # Deselect these schools to aggregate pupil numbers per LA
    all_schools_without_non_lead_federated_schools = all_schools[
        ~non_lead_federated_schools_mask
    ]

    pupils_per_la = all_schools_without_non_lead_federated_schools.groupby(
        "LA Code"
    ).agg({"Number of pupils": "sum"})
    return pupils_per_la


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
        encoding=input_schemas.la_outturn_encoding.get(
            year, input_schemas.la_outturn_encoding["default"]
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