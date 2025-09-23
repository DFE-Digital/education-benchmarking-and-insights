import time
from typing import Mapping

import pandas as pd

from pipeline.utils.database import (
    insert_bfr,
    insert_bfr_metrics,
    insert_financial_data,
    insert_la_financial,
    insert_la_non_financial,
    insert_la_statistical_neighbours,
    insert_non_financial_data,
    insert_schools_and_local_authorities,
    insert_trust_financial_data,
    insert_trusts,
)
from pipeline.utils.log import setup_logger
from pipeline.utils.stats import stats_collector
from pipeline.utils.storage import get_blob, raw_container, try_get_blob, write_blob

from .aar.academies import build_academy_data, map_academy_data
from .aar.trusts import build_trust_data
from .bfr.trusts import build_bfr_data, build_bfr_historical_data
from .ancillary.custom_data import update_custom_data
from .ancillary.ilr import patch_missing_sixth_form_data
from .ancillary.main import (
    pre_process_academy_ar,
    pre_process_cdc,
    pre_process_census,
    pre_process_central_services,
    pre_process_cfo,
    pre_process_combined_gias,
    pre_process_gias_links,
    pre_process_high_exec_pay,
    pre_process_ilr_data,
    pre_process_ks2,
    pre_process_ks4,
    pre_process_sen,
)
from .cfr.maintained_schools import build_maintained_school_data
from .common import total_per_unit
from .s251.local_authority import build_local_authorities

logger = setup_logger(__name__)


def pre_process_data(
    run_id: str,
    aar_year: int,
    cfr_year: int,
    bfr_year: int,
    s251_year: int,
):
    """
    Process data necessary for Academies, Maintained Schools BFR and Trusts.

    The expected "drop" of data is:

    - BFR data arrive first
    - CFR data arrive next
    - AAR data arrive last

    As a result, `cfr_year` is always expected to be the same or later
    than `aar_year`.

    Note: `run_type` is _always_ "default".

    :param run_id: unique identifier (used for write target)
    :param aar_year: Academy financial year/source
    :param cfr_year: Maintained School financial year/source
    :param bfr_year: BFR year/source
    :param s251_year: Section 251 year/source
    :return: duration of processing
    """
    run_type = "default"

    start_time = time.time()
    logger.info(f"Pre-processing data {run_type} - {run_id}")

    academies_data_ref = get_aar_ancillary_data(run_id, aar_year)
    maintained_data_ref = get_cfr_ancillary_data(run_id, cfr_year)

    academies = pre_process_academies_data(
        run_type,
        run_id,
        aar_year,
        academies_data_ref,
    )
    maintained_schools = pre_process_maintained_schools_data(
        run_type,
        run_id,
        cfr_year,
        maintained_data_ref,
    )
    stats_collector.collect_aar_academy_counts(academies, aar_year)
    stats_collector.collect_cfr_la_maintained_school_counts(
        maintained_schools, cfr_year
    )

    if academies_data_ref["ilr"] is not None:
        # The assert here is to satisfy type checking - gias_links should never be None
        assert academies_data_ref["gias_links"] is not None
        academies = patch_missing_sixth_form_data(
            academies,
            academies_data_ref["ilr"],
            academies_data_ref["gias_links"],
        )
        academies = total_per_unit.calculate_total_per_unit_costs(academies)

    if maintained_data_ref["ilr"] is not None:
        maintained_schools = patch_missing_sixth_form_data(
            maintained_schools,
            maintained_data_ref["ilr"],
            maintained_data_ref["gias_links"],
        )
        maintained_schools = total_per_unit.calculate_total_per_unit_costs(
            maintained_schools
        )

    trusts = pre_process_trust_data(
        run_type,
        run_id,
        academies,
        academies_data_ref["high_exec_pay"],
    )

    pre_process_all_schools(
        run_type,
        run_id,
        (academies, maintained_schools, trusts),
    )

    pre_process_bfr(run_id, bfr_year)

    pre_process_local_authorities(s251_year, run_id)

    time_taken = time.time() - start_time
    logger.info(f"Pre-processing data done in {time_taken:,.2f} seconds")

    return time_taken


def pre_process_custom_data(
    run_id: str,
    year: int,
    target_urn: int,
    custom_data: dict,
) -> float:
    """
    Perform pre-processing for custom financial data.

    Note: `custom_data` **must** contain only financial data and no
    extraneous keys.

    This leverages the existing pre-processed data:

    - reads existing pre-processed data for `year`;
    - updates as per `custom_data` for `target_urn`;
    - persists the updated, pre-processed data in blob storage;
    - persists the updated financial information in the database.

    Note: `run_type` is _always_ `custom`.

    :param run_id: unique run identifier
    :param year: financial year in question
    :param target_urn: identifier of the org. in question
    :param custom_data: custom financial data
    :return: processing duration
    """
    run_type = "custom"

    start_time = time.time()
    logger.info(f"Pre-processing data {run_type} - {run_id}")

    academies = pd.read_parquet(
        get_blob("pre-processed", f"default/{year}/academies.parquet")
    )
    maintained = pd.read_parquet(
        get_blob("pre-processed", f"default/{year}/maintained_schools.parquet")
    )
    all_schools = pd.read_parquet(
        get_blob("pre-processed", f"default/{year}/all_schools.parquet")
    )

    academies = update_custom_data(
        existing_data=academies,
        custom_data=custom_data,
        target_urn=target_urn,
    )
    maintained = update_custom_data(
        existing_data=maintained,
        custom_data=custom_data,
        target_urn=target_urn,
    )
    all_schools = update_custom_data(
        existing_data=all_schools,
        custom_data=custom_data,
        target_urn=target_urn,
    )
    stats_collector.collect_aar_academy_counts(academies, year)
    stats_collector.collect_cfr_la_maintained_school_counts(maintained, year)
    stats_collector.collect_combined_school_counts(all_schools)

    write_blob(
        "pre-processed",
        f"{run_type}/{run_id}/academies.parquet",
        academies.to_parquet(),
    )
    write_blob(
        "pre-processed",
        f"{run_type}/{run_id}/maintained_schools.parquet",
        maintained.to_parquet(),
    )
    write_blob(
        "pre-processed",
        f"{run_type}/{run_id}/all_schools.parquet",
        all_schools.to_parquet(),
    )

    insert_non_financial_data(
        run_type,
        run_id,
        df=all_schools.loc[[target_urn]],
    )
    insert_financial_data(
        run_type,
        run_id,
        df=all_schools.loc[[target_urn]],
    )

    time_taken = time.time() - start_time
    logger.info(f"Pre-processing data done in {time_taken:,.2f} seconds")

    return time_taken


def pre_process_academies_data(
    run_type: str,
    run_id: str,
    year: int,
    aar_ancillary_data: Mapping,
) -> pd.DataFrame:
    logger.info("Building Academy Set")
    logger.info(f"Processing AAR data - {run_id} - {year}.")

    academies = build_academy_data(**aar_ancillary_data)
    academies = map_academy_data(academies)

    write_blob(
        "pre-processed",
        f"{run_type}/{run_id}/academies.parquet",
        academies.to_parquet(),
    )

    return academies


def pre_process_maintained_schools_data(
    run_type: str,
    run_id: str,
    year: int,
    cfr_ancillary_data: Mapping,
) -> pd.DataFrame:
    logger.info("Building Maintained School Set")
    logger.info(f"Processing CFR data - {run_id} - {year}.")

    maintained_schools_data = get_blob(
        raw_container,
        f"{run_type}/{year}/maintained_schools_master_list.csv",
        encoding="cp1252",
    )

    maintained_schools = build_maintained_school_data(
        maintained_schools_data, year, **cfr_ancillary_data
    )

    write_blob(
        "pre-processed",
        f"{run_type}/{run_id}/maintained_schools.parquet",
        maintained_schools.to_parquet(),
    )

    return maintained_schools


def pre_process_trust_data(
    run_type: str,
    run_id: str,
    academies: pd.DataFrame,
    high_exec_pay_per_trust: pd.DataFrame | None,
) -> pd.DataFrame:
    """
    Build and store Trust financial information.

    :param run_type: "default" or "custom"
    :param run_id: unique identifer for data-writes
    :param academies: Academy financial information
    :return: Trust-level financial information
    """
    logger.info("Building Trust data.")

    trusts = build_trust_data(academies, high_exec_pay_per_trust)

    write_blob(
        "pre-processed",
        f"{run_type}/{run_id}/trusts.parquet",
        trusts.to_parquet(),
    )

    return trusts


def pre_process_all_schools(run_type, run_id, data_ref):
    """
    Store various org. information.

    - store Trust information, derived from Academy-level details
    - remove transitioning Academies
    - store combined Maintained-School/Academy information
    - store Trust financial information

    :param run_type: should only be "default"
    :param run_id: unique identifer for data-writes
    :param data_ref: Academy, Maintained and Trust info.
    """
    logger.info("Building All schools Set")
    academies, maintained_schools, trusts = data_ref

    insert_trusts(run_type, run_id, academies)
    mask = academies.index.duplicated(keep=False) & ~academies["Valid To"].isna()
    academies = academies[~mask]
    # TODO: this overwrites the previous one inc. transitioning academies.
    write_blob(
        "pre-processed",
        f"{run_type}/{run_id}/academies.parquet",
        academies.to_parquet(),
    )

    all_schools = pd.concat([academies, maintained_schools], axis=0)
    # TODO: Shouldn't need to filter this out
    all_schools = all_schools[~all_schools["Financial Position"].isna()]
    stats_collector.collect_combined_school_counts(all_schools)

    write_blob(
        "pre-processed",
        f"{run_type}/{run_id}/all_schools.parquet",
        all_schools.to_parquet(),
    )

    insert_schools_and_local_authorities(run_type, run_id, all_schools)
    insert_non_financial_data(run_type, run_id, all_schools)
    insert_financial_data(run_type, run_id, all_schools)
    insert_trust_financial_data(run_type, run_id, trusts)


def pre_process_bfr(run_id: str, year: int):
    """
    Aggregate academies budget forecast return (BFR) from
    statement of financial returns (SOFA) and three-year forecast
    (3Y) files.

    Forecast data for previous-and-current years are derived from BFR
    SOFA data; forecast data for future years are derived from BFR 3Y
    data.

    :param run_id: unique identifier for processing
    :param year: financial year in question
    """
    logger.info(f"Processing BFR SOFA data: default/{year}/BFR_SOFA_raw.csv")
    bfr_sofa = get_blob(
        raw_container, f"default/{year}/BFR_SOFA_raw.csv", encoding="unicode-escape"
    )
    logger.info(f"Processing BFR 3Y data: default/{year}/BFR_3Y_raw.csv")
    bfr_3y = get_blob(
        raw_container, f"default/{year}/BFR_3Y_raw.csv", encoding="unicode-escape"
    )

    # TODO: handle condition where this doesn't exist.
    logger.info(f"Processing BFR academies data: default/{year}/academies.parquet")
    academies = pd.read_parquet(
        get_blob("pre-processed", f"default/{year}/academies.parquet"),
        columns=[
            "Trust UPIN",
            "Company Registration Number",
            "Trust Revenue reserve",
            "Total pupils in trust",
        ],
    )
    logger.info(f"Academies preprocessed {year=} shape: {academies.shape}")

    # Conditionally read in academy/SOFA files, skipping unnecessary data…
    # TODO: "Company Reg…" isn't referenced for historic data; we can drop the
    # join to historic academies data and just use the historic SOFA data.
    academies_y2 = None
    bfr_sofa_year_minus_two = None
    if academies_y2_file := try_get_blob(
        "pre-processed", f"default/{year - 2}/academies.parquet"
    ):
        academies_y2 = pd.read_parquet(
            academies_y2_file,
            columns=[
                "Trust UPIN",
                "Company Registration Number",
                # "Trust Revenue reserve",  # SOFA, EFALineNo == 430 (Y2P2)
                # "Total pupils in trust",  # SOFA, EFALineNo == 999 (Y1P2)
            ],
        )
        logger.info(f"Academies Y2 preprocessed {year=} shape: {academies_y2.shape}")

        if bfr_sofa_year_minus_two_file := try_get_blob(
            raw_container,
            f"default/{year - 2}/BFR_SOFA_raw.csv",
            encoding="unicode-escape",
        ):
            bfr_sofa_year_minus_two = (
                pd.read_csv(
                    bfr_sofa_year_minus_two_file,
                    usecols=[
                        "TrustUPIN",
                        "EFALineNo",
                        "Y1P2",
                        "Y2P2",
                    ],
                )
                .rename(
                    {"TrustUPIN": "Trust UPIN"},
                    axis=1,
                )
                .query("EFALineNo in (430, 999,)")
            )
            logger.info(
                f"BFR sofa year minus two preprocessed {year=} shape: {bfr_sofa_year_minus_two.shape}"
            )

    academies_y1 = None
    bfr_sofa_year_minus_one = None
    if academies_y1_file := try_get_blob(
        "pre-processed", f"default/{year - 1}/academies.parquet"
    ):
        academies_y1 = pd.read_parquet(
            academies_y1_file,
            columns=[
                "Trust UPIN",
                "Company Registration Number",
                # "Trust Revenue reserve",  # SOFA, EFALineNo == 430 (Y2P2)
                # "Total pupils in trust",  # SOFA, EFALineNo == 999 (Y1P2)
            ],
        )
        logger.info(f"Academies Y1 preprocessed {year=} shape: {academies_y1.shape}")

        if bfr_sofa_year_minus_one_file := try_get_blob(
            raw_container,
            f"default/{year - 1}/BFR_SOFA_raw.csv",
            encoding="unicode-escape",
        ):
            bfr_sofa_year_minus_one = (
                pd.read_csv(
                    bfr_sofa_year_minus_one_file,
                    usecols=[
                        "TrustUPIN",
                        "EFALineNo",
                        "Y1P2",
                        "Y2P2",
                    ],
                )
                .rename(
                    {"TrustUPIN": "Trust UPIN"},
                    axis=1,
                )
                .query("EFALineNo in (430, 999,)")
            )
            logger.info(
                f"BFR sofa year minus one preprocessed {year=} shape: {bfr_sofa_year_minus_one.shape}"
            )

    # Process BFR data…
    historic_bfr_y2 = build_bfr_historical_data(
        academies_historical=academies_y2,
        bfr_sofa_historical=bfr_sofa_year_minus_two,
    )

    historic_bfr_y1 = build_bfr_historical_data(
        academies_historical=academies_y1,
        bfr_sofa_historical=bfr_sofa_year_minus_one,
    )
    bfr, bfr_metrics = build_bfr_data(
        year, bfr_sofa, bfr_3y, academies, historic_bfr_y1, historic_bfr_y2
    )

    # Store results…
    write_blob(
        "pre-processed",
        f"default/{run_id}/bfr_metrics.parquet",
        bfr_metrics.to_parquet(),
    )

    write_blob(
        "pre-processed",
        f"default/{run_id}/bfr.parquet",
        bfr.to_parquet(),
    )

    insert_bfr(run_id, bfr)
    insert_bfr_metrics(run_id, year, bfr_metrics)


def pre_process_local_authorities(
    year: int,
    run_id: str,
):
    """
    Process Local Authority data.

    - Section 251 budget data
    - Section 251 outturn data
    - Local Authority statistical neighbours data
    - ONS Local Authority population data

    :param run_id: unique identifier for processing
    :param year: financial year in question
    """
    logger.info(
        f"Reading LA Section 251 budget data: default/{year}/plannedexpenditure_schools_other_education_la_unrounded_data.csv"
    )
    la_expenditure_data = get_blob(
        raw_container,
        f"default/{year}/plannedexpenditure_schools_other_education_la_unrounded_data.csv",
    )

    logger.info(
        f"Reading LA Section 251 outturn data: default/{year}/s251_alleducation_la_regional_national.csv"
    )
    la_outturn_data = get_blob(
        raw_container,
        f"default/{year}/s251_alleducation_la_regional_national.csv",
    )

    logger.info(
        f"Reading LA statistical neighbours: default/{year}/High-needs-local-authority-benchmarking-tool.xlsm"
    )
    la_statistical_neighbours_data = get_blob(
        raw_container,
        f"default/{year}/High-needs-local-authority-benchmarking-tool.xlsm",
    )

    logger.info(
        f"Reading ONS LA population data: default/{year}/2018 SNPP Population persons.csv"
    )
    la_ons_data = get_blob(
        raw_container,
        f"default/{year}/2018 SNPP Population persons.csv",
    )

    logger.info(
        f"Reading LA SEN2 ECHP plan data: default/{year}/sen2_estab_caseload.csv"
    )
    la_sen2_data = get_blob(
        raw_container,
        f"default/{year}/sen2_estab_caseload.csv",
    )

    logger.info("Processing Local Authority data.")
    local_authorities = build_local_authorities(
        la_expenditure_data,
        la_outturn_data,
        la_statistical_neighbours_data,
        la_ons_data,
        la_sen2_data,
        year,
    )
    logger.info(
        f"Local Authorities preprocessed' {year=} shape: {local_authorities.shape}"
    )

    write_blob(
        "pre-processed",
        f"default/{run_id}/local_authorities.parquet",
        local_authorities.to_parquet(),
    )

    insert_la_financial(
        run_type="default",
        run_id=run_id,
        df=local_authorities,
    )
    insert_la_non_financial(
        run_type="default",
        run_id=run_id,
        df=local_authorities,
    )
    insert_la_statistical_neighbours(
        run_type="default",
        run_id=run_id,
        df=local_authorities,
    )


def get_aar_ancillary_data(
    run_id: str,
    aar_year: int,
) -> dict[str, pd.DataFrame | None]:
    """
    Retrieve and process supporting data files.

    Note: `run_type` is _always_ "default".

    :param run_id: unique identifier (used for write target)
    :param year: financial year in question
    :return: tuple of supporting datasets
    """
    run_type = "default"

    gias = pre_process_combined_gias(run_type, aar_year, run_id)
    aar_ancillary_data = {
        "gias": gias,
        "census": pre_process_census(run_type, aar_year, run_id),
        "sen": pre_process_sen(run_type, aar_year, run_id),
        "cdc": pre_process_cdc(run_type, aar_year, run_id),
        "aar": pre_process_academy_ar(run_type, aar_year, run_id),
        "ks2": pre_process_ks2(run_type, aar_year, run_id),
        "ks4": pre_process_ks4(run_type, aar_year, run_id),
        "cfo": pre_process_cfo(run_type, aar_year, run_id),
        "central_services": pre_process_central_services(run_type, aar_year, run_id),
        "gias_links": pre_process_gias_links(run_type, aar_year, run_id),
        "high_exec_pay": pre_process_high_exec_pay(run_type, aar_year, run_id),
        "ilr": pre_process_ilr_data(run_type, aar_year, run_id, gias),
    }
    stats_collector.collect_aar_ancillary_data_shapes(aar_ancillary_data, aar_year)

    return aar_ancillary_data


def get_cfr_ancillary_data(
    run_id: str,
    cfr_year: int,
) -> dict[str, pd.DataFrame]:
    """
    Retrieve and process supporting data files.

    Note: `run_type` is _always_ "default".

    :param run_id: unique identifier (used for write target)
    :param year: financial year in question
    :return: tuple of supporting datasets
    """
    run_type = "default"

    gias = pre_process_combined_gias(run_type, cfr_year, run_id)
    cfr_ancillary_data = {
        "gias": gias,
        "census": pre_process_census(run_type, cfr_year, run_id),
        "sen": pre_process_sen(run_type, cfr_year, run_id),
        "cdc": pre_process_cdc(run_type, cfr_year, run_id),
        "ks2": pre_process_ks2(run_type, cfr_year, run_id),
        "ks4": pre_process_ks4(run_type, cfr_year, run_id),
        "gias_links": pre_process_gias_links(run_type, cfr_year, run_id),
        "ilr": pre_process_ilr_data(run_type, cfr_year, run_id, gias),
    }
    stats_collector.collect_cfr_ancillary_data_shapes(cfr_ancillary_data, cfr_year)

    return cfr_ancillary_data
