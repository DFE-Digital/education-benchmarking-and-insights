import json
import logging
import os
import time
from contextlib import suppress

import pandas as pd
import tornado.iostream
from azure.core.exceptions import ResourceNotFoundError
from azure.storage.queue import QueueClient, QueueMessage
from dask.distributed import Client
from dotenv import load_dotenv

load_dotenv()

from pipeline.comparator_sets import compute_comparator_set, prepare_data
from pipeline.database import (
    insert_bfr,
    insert_bfr_metrics,
    insert_comparator_set,
    insert_financial_data,
    insert_la_financial,
    insert_la_non_financial,
    insert_la_statistical_neighbours,
    insert_metric_rag,
    insert_non_financial_data,
    insert_schools_and_local_authorities,
    insert_trust_financial_data,
    insert_trusts,
)
from pipeline.log import setup_logger
from pipeline.message import MessageType, get_message_type
from pipeline.pre_processing import (
    build_academy_data,
    build_bfr_data,
    build_bfr_historical_data,
    build_cfo_data,
    build_ilr_data,
    build_local_authorities,
    build_maintained_school_data,
    build_trust_data,
    map_academy_data,
    patch_missing_sixth_form_data,
    predecessor_links,
    prepare_aar_data,
    prepare_cdc_data,
    prepare_census_data,
    prepare_central_services_data,
    prepare_ks2_data,
    prepare_ks4_data,
    prepare_schools_data,
    prepare_sen_data,
    update_custom_data,
)
from pipeline.rag import compute_rag, compute_user_defined_rag
from pipeline.storage import (
    blob_service_client,
    complete_queue_name,
    connect_to_queue,
    dead_letter_dequeue_max,
    dead_letter_queue_name,
    get_blob,
    queue_service_client,
    raw_container,
    try_get_blob,
    worker_queue_name,
    write_blob,
)

logger = setup_logger("fbit-data-pipeline")

ds_logger = logging.getLogger("distributed.utils_perf")
ds_logger.setLevel(logging.ERROR)


def pre_process_cdc(run_type: str, year: int, run_id: str) -> pd.DataFrame:
    logger.info(f"Processing CDC Data: {run_type}/{year}/cdc.csv")

    cdc_data = get_blob(raw_container, f"{run_type}/{year}/cdc.csv", encoding="utf-8")

    cdc = prepare_cdc_data(cdc_data, year)

    write_blob("pre-processed", f"{run_type}/{run_id}/cdc.parquet", cdc.to_parquet())

    return cdc


def pre_process_census(run_type: str, year: int, run_id: str) -> pd.DataFrame:
    logger.info(f"Processing Census Data: {run_type}/{year}/census_workforce.xlsx")

    workforce_census_data = get_blob(
        raw_container, f"{run_type}/{year}/census_workforce.xlsx"
    )
    pupil_census_data = get_blob(
        raw_container, f"{run_type}/{year}/census_pupils.csv", encoding="cp1252"
    )

    census = prepare_census_data(
        workforce_census_data,
        pupil_census_data,
        year,
    )

    write_blob(
        "pre-processed",
        f"{run_type}/{run_id}/census.parquet",
        census.to_parquet(),
    )

    return census


def pre_process_sen(run_type: str, year: int, run_id: str) -> pd.DataFrame:
    logger.info(f"Processing SEN Data: {run_type}/{year}/sen.csv")

    sen_data = get_blob(raw_container, f"{run_type}/{year}/sen.csv", encoding="cp1252")

    sen = prepare_sen_data(sen_data)

    write_blob("pre-processed", f"{run_type}/{run_id}/sen.parquet", sen.to_parquet())

    return sen


def pre_process_ks2(run_type: str, year: int, run_id: str) -> pd.DataFrame:
    logger.info(f"Processing KS2 Data: {run_type}/{year}/ks2.xlsx")

    ks2_data = try_get_blob(raw_container, f"{run_type}/{year}/ks2.xlsx")

    ks2 = prepare_ks2_data(ks2_data)

    write_blob("pre-processed", f"{run_type}/{run_id}/ks2.parquet", ks2.to_parquet())

    return ks2


def pre_process_ks4(run_type: str, year: int, run_id: str) -> pd.DataFrame:
    logger.info(f"Processing KS4 Data: {run_type}/{year}/ks4.xlsx")

    ks4_data = try_get_blob(raw_container, f"{run_type}/{year}/ks4.xlsx")

    ks4 = prepare_ks4_data(ks4_data)

    write_blob("pre-processed", f"{run_type}/{run_id}/ks4.parquet", ks4.to_parquet())

    return ks4


def pre_process_academy_ar(
    run_type: str, year: int, run_id: str
) -> pd.DataFrame | None:
    """
    Process AAR data.

    Note: depending on the timing, the AAR data may not be present for
    the `year`.

    :param run_type: "default" or "custom"
    :param year: financial year in question
    :param run_id: unique identifer for data-writes
    :return: processed AAR data, if present
    """
    if academy_ar_data := try_get_blob(
        raw_container, f"{run_type}/{year}/aar.csv", encoding="utf-8"
    ):
        logger.info(f"Processing Academy AR Data: {run_type}/{year}/aar.csv")

        aar = prepare_aar_data(academy_ar_data, year)

        write_blob(
            "pre-processed",
            f"{run_type}/{run_id}/aar.parquet",
            aar.to_parquet(),
        )

        return aar

    return None


def pre_process_schools(run_type: str, year: int, run_id: str) -> pd.DataFrame:
    logger.info(f"Processing GIAS Data: {run_type}/{year}/gias.csv")
    gias_data = get_blob(
        raw_container, f"{run_type}/{year}/gias.csv", encoding="cp1252"
    )
    logger.info(f"Processing GIAS-links Data: {run_type}/{year}/gias_links.csv")
    gias_links_data = get_blob(
        raw_container, f"{run_type}/{year}/gias_links.csv", encoding="cp1252"
    )

    schools = prepare_schools_data(gias_data, gias_links_data, year)

    write_blob(
        "pre-processed",
        f"{run_type}/{run_id}/schools.parquet",
        schools.to_parquet(),
    )

    return schools


def pre_process_gias_links(run_type: str, year: int, run_id: str) -> pd.DataFrame:
    """
    Generate the GIAS-links dataset.

    Note: this is distinct from the "schools" data insofar as this only
    contains GIAS-links and retains _all_ rows.

    :param run_type: "default" or "custom"
    :param year: financial year in question
    :param run_id: unique identifer for data-writes
    :return: GIAS-links data
    """
    logger.info(f"Processing GIAS-links data: {run_type}/{year}/gias_links.csv")
    gias_links_data = get_blob(
        raw_container, f"{run_type}/{year}/gias_links.csv", encoding="cp1252"
    )

    gias_links = predecessor_links(gias_links_data)

    write_blob(
        "pre-processed",
        f"{run_type}/{run_id}/gias_links.parquet",
        gias_links.to_parquet(),
    )

    return gias_links


def pre_process_ilr_data(
    run_type: str,
    year: int,
    run_id: str,
    schools: pd.DataFrame,
) -> pd.DataFrame | None:
    """
    Process ILR data.

    :param run_type: should only be "default"
    :param run_id: unique identifer for data-writes
    :param year: financial year in question
    :param schools: data containing URN/UKPRN mappings
    :return: ILR data, if present for the year in question
    """
    if ilr_data := try_get_blob(
        raw_container,
        f"{run_type}/{year}/ILR R06 cut with FSM and EHCP.xlsx",
    ):
        return build_ilr_data(
            ilr_data,
            schools,
            year,
        )

    return None


def pre_process_cfo(run_type: str, year: int, run_id: str) -> pd.DataFrame:
    logger.info(f"Processing CFO Data: {run_type}/{year}/cfo.xlsx")

    cfo_data = get_blob(raw_container, f"{run_type}/{year}/cfo.xlsx")

    cfo = build_cfo_data(cfo_data, year)

    write_blob(
        "pre-processed",
        f"{run_type}/{run_id}/cfo.parquet",
        cfo.to_parquet(),
    )

    return cfo


def pre_process_central_services(
    run_type: str, year: int, run_id: str
) -> pd.DataFrame | None:
    logger.info(f"Building Central Services Data: {run_type}/{year}/aar_cs.csv")

    if academies_data := try_get_blob(
        raw_container, f"{run_type}/{year}/aar_cs.csv", encoding="utf-8"
    ):
        central_services = prepare_central_services_data(academies_data, year)

        write_blob(
            "pre-processed",
            f"{run_type}/{run_id}/central_services.parquet",
            central_services.to_parquet(),
        )

        return central_services

    return None


def pre_process_academies_data(
    run_type: str,
    run_id: str,
    year: int,
    data_ref: tuple,
) -> pd.DataFrame:
    logger.info("Building Academy Set")
    (
        schools,
        census,
        sen,
        cdc,
        aar,
        ks2,
        ks4,
        cfo,
        central_services,
        gias_links,
    ) = data_ref

    logger.info(f"Processing AAR data - {run_id} - {year}.")

    academies = build_academy_data(
        schools,
        census,
        sen,
        cdc,
        aar,
        ks2,
        ks4,
        cfo,
        central_services,
        gias_links,
    )

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
    data_ref: tuple,
) -> pd.DataFrame:
    logger.info("Building Maintained School Set")
    (
        schools,
        census,
        sen,
        cdc,
        aar,
        ks2,
        ks4,
        cfo,
        central_services,
        gias_links,
    ) = data_ref

    logger.info(f"Processing CFR data - {run_id} - {year}.")

    maintained_schools_data = get_blob(
        raw_container,
        f"{run_type}/{year}/maintained_schools_master_list.csv",
        encoding="cp1252",
    )

    maintained_schools = build_maintained_school_data(
        maintained_schools_data, schools, census, sen, cdc, ks2, ks4
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
) -> pd.DataFrame:
    """
    Build and store Trust financial information.

    :param run_type: "default" or "custom"
    :param run_id: unique identifer for data-writes
    :param academies: Academy financial information
    :return: Trust-level financial information
    """
    logger.info("Building Trust data.")

    trusts = build_trust_data(academies)

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

    # Process BFR data…
    academies_y2 = build_bfr_historical_data(
        academies_historical=academies_y2,
        bfr_sofa_historical=bfr_sofa_year_minus_two,
    )

    academies_y1 = build_bfr_historical_data(
        academies_historical=academies_y1,
        bfr_sofa_historical=bfr_sofa_year_minus_one,
    )
    bfr, bfr_metrics = build_bfr_data(
        year, bfr_sofa, bfr_3y, academies, academies_y1, academies_y2
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


def _get_ancillary_data(
    worker_client: Client,
    run_id: str,
    year: int,
) -> tuple:
    """
    Retrieve and process supporting data files.

    Note: `run_type` is _always_ "default".

    :param worker_client: Dask client
    :param run_id: unique identifier (used for write target)
    :param year: financial year in question
    :return: tuple of supporting datasets
    """
    run_type = "default"

    (
        cdc,
        census,
        sen,
        ks2,
        ks4,
        aar,
        schools,
        cfo,
        central_services,
        gias_links,
    ) = worker_client.gather(
        [
            worker_client.submit(pre_process_cdc, run_type, year, run_id),
            worker_client.submit(pre_process_census, run_type, year, run_id),
            worker_client.submit(pre_process_sen, run_type, year, run_id),
            worker_client.submit(pre_process_ks2, run_type, year, run_id),
            worker_client.submit(pre_process_ks4, run_type, year, run_id),
            worker_client.submit(pre_process_academy_ar, run_type, year, run_id),
            worker_client.submit(pre_process_schools, run_type, year, run_id),
            worker_client.submit(pre_process_cfo, run_type, year, run_id),
            worker_client.submit(pre_process_central_services, run_type, year, run_id),
            worker_client.submit(pre_process_gias_links, run_type, year, run_id),
        ]
    )

    return worker_client.scatter(
        (
            schools,
            census,
            sen,
            cdc,
            aar,
            ks2,
            ks4,
            cfo,
            central_services,
            gias_links,
        )
    )


def pre_process_data(
    worker_client: Client,
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

    :param worker_client: Dask client
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

    academies_data_ref = maintained_data_ref = _get_ancillary_data(
        worker_client,
        run_id,
        aar_year,
    )
    if cfr_year != aar_year:
        maintained_data_ref = _get_ancillary_data(worker_client, run_id, cfr_year)

    academies, maintained_schools = worker_client.gather(
        [
            worker_client.submit(
                pre_process_academies_data,
                run_type,
                run_id,
                aar_year,
                academies_data_ref,
            ),
            worker_client.submit(
                pre_process_maintained_schools_data,
                run_type,
                run_id,
                cfr_year,
                maintained_data_ref,
            ),
        ]
    )

    if (
        academies_ilr_data := worker_client.submit(
            pre_process_ilr_data,
            run_type,
            run_id,
            aar_year,
            academies_data_ref[0],
        ).result()
    ) is not None:
        academies = worker_client.submit(
            patch_missing_sixth_form_data,
            academies,
            academies_ilr_data,
            academies_data_ref[9],
        ).result()

    if (
        maintained_ilr_data := worker_client.submit(
            pre_process_ilr_data,
            run_type,
            run_id,
            cfr_year,
            maintained_data_ref[0],
        ).result()
    ) is not None:
        maintained_schools = worker_client.submit(
            patch_missing_sixth_form_data,
            maintained_schools,
            maintained_ilr_data,
            maintained_data_ref[9],
        ).result()

    trusts = pre_process_trust_data(run_type, run_id, academies)

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


def compute_comparator_set_for(
    data_type: str,
    run_type: str,
    data: pd.DataFrame,
    run_id: str,
    target_urn: str | None = None,
) -> pd.DataFrame:
    """
    Perform comparator-set calculation and persist the result.

    Results are persisted in both blob-storage and, only if there are
    data to be written, the database.

    :param data_type: type (e.g. academy) of the data
    :param run_type: "default" or "custom"
    :param data: used to determine comparator set
    :param run_id: job identifier for custom data
    :param target_urn: optional data identifier for custom data
    :return: generated comparator sets
    """
    st = time.time()
    logger.info(f"Computing {data_type} set")
    result = compute_comparator_set(data, target_urn=target_urn)
    logger.info(f"Computing {data_type} set. Done in {time.time() - st:.2f} seconds")

    st = time.time()
    write_blob(
        "comparator-sets",
        f"{run_type}/{run_id}/{data_type}.parquet",
        result.to_parquet(),
    )

    return result


def compute_comparator_sets(
    run_type: str,
    run_id: str,
    target_urn: str | None = None,
) -> float:
    """
    Determine Comparator Sets.

    :param run_type: "default" or "custom" data type
    :param run_id: optional job identifier for custom data
    :param target_urn: optional data identifier for custom data
    :return: duration of calculation
    """
    start_time = time.time()
    logger.info("Computing comparator sets")

    academies = prepare_data(
        pd.read_parquet(
            get_blob("pre-processed", f"{run_type}/{run_id}/academies.parquet")
        )
    )
    maintained = prepare_data(
        pd.read_parquet(
            get_blob(
                "pre-processed",
                f"{run_type}/{run_id}/maintained_schools.parquet",
            )
        )
    )

    academies_comparators = compute_comparator_set_for(
        data_type="academy_comparators",
        run_type=run_type,
        data=academies,
        run_id=run_id,
        target_urn=target_urn,
    )
    maintained_comparators = compute_comparator_set_for(
        data_type="maintained_schools_comparators",
        run_type=run_type,
        data=maintained,
        run_id=run_id,
        target_urn=target_urn,
    )

    write_blob(
        "comparator-sets",
        f"{run_type}/{run_id}/academies.parquet",
        academies.to_parquet(),
    )
    write_blob(
        "comparator-sets",
        f"{run_type}/{run_id}/maintained_schools.parquet",
        maintained.to_parquet(),
    )

    comparators = pd.concat(
        [
            academies_comparators,
            maintained_comparators,
        ],
        axis=0,
    )
    insert_comparator_set(
        run_type=run_type,
        run_id=run_id,
        df=comparators,
    )

    time_taken = time.time() - start_time
    logger.info(f"Computing comparators sets done in {time_taken:,.2f} seconds")

    return time_taken


def compute_rag_for(
    data_type: str,
    run_type: str,
    run_id: str,
    data: pd.DataFrame,
    comparators: pd.DataFrame,
    target_urn: str | None = None,
) -> pd.DataFrame:
    st = time.time()
    logger.info(f"Computing {data_type} RAG")

    # TODO: move logic to `rag` rather than hard-coding columns.
    if target_urn and target_urn not in data.index:
        df = pd.DataFrame(
            columns=[
                "URN",
                "Category",
                "SubCategory",
                "Value",
                "Median",
                "DiffMedian",
                "Key",
                "PercentDiff",
                "Percentile",
                "Decile",
                "RAG",
            ]
        ).set_index("URN")
    else:
        df = pd.DataFrame(
            compute_rag(data, comparators, target_urn=target_urn)
        ).set_index("URN")

    logger.info(f"Computing {data_type} RAG. Done in {time.time() - st:.2f} seconds")

    write_blob(
        "metric-rag",
        f"{run_type}/{run_id}/{data_type}.parquet",
        df.to_parquet(),
    )

    return df


def run_compute_rag(
    run_type: str,
    run_id: str,
    target_urn: str | None = None,
):
    """
    Perform RAG calculations.

    :param run_type: "default" or "custom" data type
    :param run_id: optional job identifier for custom data
    :param target_urn: optional data identifier for custom data
    :return: duration of RAG calculations
    """
    start_time = time.time()

    ms_data = pd.read_parquet(
        get_blob("comparator-sets", f"{run_type}/{run_id}/maintained_schools.parquet")
    )
    ms_comparators = pd.read_parquet(
        get_blob(
            "comparator-sets",
            f"{run_type}/{run_id}/maintained_schools_comparators.parquet",
        )
    )
    maintained_rag = compute_rag_for(
        "maintained_schools",
        run_type,
        run_id,
        ms_data,
        ms_comparators,
        target_urn=target_urn,
    )

    academy_data = pd.read_parquet(
        get_blob("comparator-sets", f"{run_type}/{run_id}/academies.parquet")
    )
    academy_comparators = pd.read_parquet(
        get_blob("comparator-sets", f"{run_type}/{run_id}/academy_comparators.parquet")
    )
    academies_rag = compute_rag_for(
        "academies",
        run_type,
        run_id,
        academy_data,
        academy_comparators,
        target_urn=target_urn,
    )

    rag = pd.concat(
        [
            academies_rag,
            maintained_rag,
        ],
        axis=0,
    )
    insert_metric_rag(run_type, run_id, rag)

    time_taken = time.time() - start_time
    logger.info(f"Computing RAG done in {time_taken:,.2f} seconds")

    return time_taken


def run_user_defined_rag(
    year: int,
    run_id: str,
    target_urn: int,
    comparator_set: list[int],
):
    """
    Perform user-defined RAG calculations.

    Use the pre-processed "all-schools" data to guarantee coverage of
    the user-defined comparator-set.

    Note: `run_type` is _always_ "default" for user-defined
    comparator-sets.

    :param year: financial year in question
    :param run_id: unique run identifier
    :param target_urn: URN of the "target" org.
    :param comparator_set: user-defined comparator-set
    :return: duration of RAG calculations
    """
    run_type = "default"

    all_schools = prepare_data(
        pd.read_parquet(
            get_blob("pre-processed", f"{run_type}/{year}/all_schools.parquet")
        )
    )

    st = time.time()
    logger.info(f"Computing user-defined RAG ({run_id}).")
    df = pd.DataFrame(
        compute_user_defined_rag(
            data=all_schools,
            target_urn=target_urn,
            set_urns=comparator_set,
        )
    ).set_index("URN")
    logger.info(
        f"Computing user-defined ({run_id}) RAG. Done in {time.time() - st:.2f} seconds."
    )

    write_blob(
        "metric-rag",
        f"{run_type}/{run_id}/user_defined.parquet",
        df.to_parquet(),
    )

    insert_metric_rag(
        run_type=run_type,
        run_id=run_id,
        df=df,
    )


def handle_msg(
    worker_client: Client,
    msg: QueueMessage,
    worker_queue: QueueClient,
    complete_queue: QueueClient,
):
    """
    Process an incoming message.

    Note: user-defined comparator sets will assume pre-processing has
    taken place for the year in question, failing if that does not hold
    true.

    :param worker_client: Dask client
    :param msg: incoming message, triggering this process
    :param worker_queue: incoming message queue (for deletion)
    :param complete_queue: outcoming message queue (for completion)
    :return: updated message payload
    """
    msg_payload = json.loads(msg.content)
    run_type = msg_payload.get("runType", "default")

    try:
        match get_message_type(message=msg_payload):
            case MessageType.Default:
                msg_payload["pre_process_duration"] = pre_process_data(
                    worker_client=worker_client,
                    run_id=str(msg_payload["runId"]),
                    aar_year=msg_payload["year"]["aar"],
                    cfr_year=msg_payload["year"]["cfr"],
                    bfr_year=msg_payload["year"]["bfr"],
                    s251_year=msg_payload["year"]["s251"],
                )
                msg_payload["comparator_set_duration"] = compute_comparator_sets(
                    run_type=run_type,
                    run_id=str(msg_payload["runId"]),
                )
                msg_payload["rag_duration"] = run_compute_rag(
                    run_type=run_type,
                    run_id=str(msg_payload["runId"]),
                )

            case MessageType.DefaultUserDefined:
                msg_payload["rag_duration"] = run_user_defined_rag(
                    year=msg_payload["year"],
                    run_id=msg_payload["runId"],
                    target_urn=int(msg_payload["urn"]),
                    comparator_set=list(map(int, msg_payload["payload"]["set"])),
                )

            case MessageType.Custom:
                msg_payload["pre_process_duration"] = pre_process_custom_data(
                    run_id=msg_payload["runId"],
                    year=msg_payload["year"],
                    target_urn=int(msg_payload["urn"]),
                    custom_data={
                        k: v for k, v in msg_payload["payload"].items() if k != "kind"
                    },
                )
                msg_payload["comparator_set_duration"] = compute_comparator_sets(
                    run_type=run_type,
                    run_id=msg_payload["runId"],
                    target_urn=int(msg_payload["urn"]),
                )
                msg_payload["rag_duration"] = run_compute_rag(
                    run_type=run_type,
                    run_id=msg_payload["runId"],
                    target_urn=int(msg_payload["urn"]),
                )

        msg_payload["success"] = True
    except Exception as error:
        logger.exception(
            f"An exception occurred: {type(error).__name__}", exc_info=error
        )
        msg_payload["success"] = False
        msg_payload["error"] = str(error)
    finally:
        with suppress(ResourceNotFoundError):
            worker_queue.delete_message(msg)

        complete_queue.send_message(json.dumps(msg_payload), time_to_live=300)

    return msg_payload


def _check_msg_dequeue(
    message: QueueMessage,
    dead_letter_queue: QueueClient,
    origin_queue: QueueClient,
    dequeue_limit: int,
) -> None:
    """
    Determine whether to redirect to the dead-letter queue.

    If the message's `dequeue_count` exceeds the `dequeue_limit`, it is:

    1. added to the dead-letter queue.
    2. removed from the originating queue.

    The entire message (not just the payload) is serialised when passed
    to the dead-letter queue, for additional context.

    :param message: incoming message to check
    :param dead_letter_queue: target for messages exceeding the limit
    :param worker_queue: source for incoming messages
    :param dequeue_limit: retry limit for re-queued messages
    :raises Exception: when the retry limit is exceeded
    """
    if message.dequeue_count <= dequeue_limit:
        return

    logger.error(
        f"Message {message.id} has exceeded the retry limit of {dequeue_limit}."
    )
    dead_letter_queue.send_message(json.dumps(message, default=str))

    with suppress(ResourceNotFoundError):
        origin_queue.delete_message(message)

    raise Exception(
        f"Message {message.id} has exceeded the retry limit of {dequeue_limit}."
    )


def receive_one_message(worker_client):
    try:
        with blob_service_client, queue_service_client:
            worker_queue = connect_to_queue(worker_queue_name)
            complete_queue = connect_to_queue(complete_queue_name)
            dead_letter_queue = connect_to_queue(dead_letter_queue_name)

            with worker_queue, complete_queue, dead_letter_queue:
                if msg := worker_queue.receive_message(visibility_timeout=300):
                    _check_msg_dequeue(
                        message=msg,
                        dead_letter_queue=dead_letter_queue,
                        origin_queue=worker_queue,
                        dequeue_limit=dead_letter_dequeue_max,
                    )

                    logger.info(f"received message {msg.content}")
                    msg = handle_msg(worker_client, msg, worker_queue, complete_queue)
                    exit(0) if msg["success"] else exit(1)
                else:
                    logger.info("no messages received")
                    exit(0)
    except Exception as error:
        logger.exception(
            f"An exception occurred: {type(error).__name__}", exc_info=error
        )
        exit(-1)


def receive_messages(worker_client):
    try:
        with blob_service_client, queue_service_client:
            worker_queue = connect_to_queue(worker_queue_name)
            complete_queue = connect_to_queue(complete_queue_name)
            dead_letter_queue = connect_to_queue(dead_letter_queue_name)
            with worker_queue, complete_queue:
                while True:
                    if msg := worker_queue.receive_message(visibility_timeout=300):
                        _check_msg_dequeue(
                            message=msg,
                            dead_letter_queue=dead_letter_queue,
                            origin_queue=worker_queue,
                            dequeue_limit=dead_letter_dequeue_max,
                        )

                        logger.info(f"received message {msg.content}")
                        msg = handle_msg(
                            worker_client, msg, worker_queue, complete_queue
                        )
                        logger.info(f"processed msg response: {msg}")
                    else:
                        time.sleep(1)
    except Exception as error:
        logger.exception(
            f"An exception occurred: {type(error).__name__}", exc_info=error
        )
        exit(-1)


if __name__ == "__main__":
    with suppress(tornado.iostream.StreamClosedError):
        with Client(memory_limit="16GB", heartbeat_interval=None) as client:
            try:
                if os.getenv("ENV") == "dev":
                    receive_messages(client)
                else:
                    receive_one_message(client)
            finally:
                client.close()
