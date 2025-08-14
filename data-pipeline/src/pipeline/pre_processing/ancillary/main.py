import pandas as pd

from pipeline.pre_processing.aar.academies import prepare_aar_data
from pipeline.pre_processing.aar.central_services import prepare_central_services_data
from pipeline.utils.log import setup_logger
from pipeline.utils.storage import get_blob, raw_container, try_get_blob, write_blob

from .cdc import prepare_cdc_data
from .census import prepare_census_data
from .cfo import build_cfo_data
from .combined_gias import prepare_combined_gias_data
from .gias import predecessor_links
from .high_exec_pay import build_high_exec_pay_data
from .ilr import build_ilr_data
from .ks2 import prepare_ks2_data
from .ks4 import prepare_ks4_data
from .sen import prepare_sen_data

logger = setup_logger("preprocessing-ancillary")


def pre_process_cdc(run_type: str, year: int, run_id: str) -> pd.DataFrame:
    logger.info(f"Processing CDC Data: {run_type}/{year}/cdc.csv")

    cdc_data = get_blob(raw_container, f"{run_type}/{year}/cdc.csv", encoding="utf-8")

    cdc = prepare_cdc_data(cdc_data, year)
    logger.info(f"CDC Data preprocessed {year=} shape: {cdc.shape}")

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
    logger.info(f"Census Data preprocessed {year=} shape: {census.shape}")

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
    logger.info(f"SEN Data preprocessed {year=} shape: {sen.shape}")

    write_blob("pre-processed", f"{run_type}/{run_id}/sen.parquet", sen.to_parquet())

    return sen


def pre_process_ks2(run_type: str, year: int, run_id: str) -> pd.DataFrame:
    logger.info(f"Processing KS2 Data: {run_type}/{year}/ks2.xlsx")

    ks2_data = try_get_blob(raw_container, f"{run_type}/{year}/ks2.xlsx")

    ks2 = prepare_ks2_data(ks2_data)
    logger.info(f"KS2 Data preprocessed {year=} shape: {ks2.shape}")

    write_blob("pre-processed", f"{run_type}/{run_id}/ks2.parquet", ks2.to_parquet())

    return ks2


def pre_process_ks4(run_type: str, year: int, run_id: str) -> pd.DataFrame:
    logger.info(f"Processing KS4 Data: {run_type}/{year}/ks4.xlsx")

    ks4_data = try_get_blob(raw_container, f"{run_type}/{year}/ks4.xlsx")

    ks4 = prepare_ks4_data(ks4_data)
    logger.info(f"KS4 Data preprocessed {year=} shape: {ks4.shape}")

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
        logger.info(f"AAR Data preprocessed {year=} shape: {aar.shape}")

        write_blob(
            "pre-processed",
            f"{run_type}/{run_id}/aar.parquet",
            aar.to_parquet(),
        )

        return aar

    return None


def pre_process_combined_gias(run_type: str, year: int, run_id: str) -> pd.DataFrame:
    """Combined gias is called schools in the database"""
    logger.info(f"Processing GIAS Data: {run_type}/{year}/gias.csv")
    gias_data = get_blob(
        raw_container, f"{run_type}/{year}/gias.csv", encoding="cp1252"
    )
    logger.info(f"Processing GIAS-links Data: {run_type}/{year}/gias_links.csv")
    gias_links_data = get_blob(
        raw_container, f"{run_type}/{year}/gias_links.csv", encoding="cp1252"
    )

    schools = prepare_combined_gias_data(gias_data, gias_links_data, year)
    logger.info(f"Schools Data preprocessed {year=} shape: {schools.shape}")

    write_blob(
        "pre-processed",
        f"{run_type}/{run_id}/schools.parquet",
        schools.to_parquet(),
    )

    return schools


def pre_process_gias_links(run_type: str, year: int, run_id: str) -> pd.DataFrame:
    """
    Generate the GIAS-links dataset.

    Note: this is distinct from the "gias" data insofar as this only
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
    gias: pd.DataFrame,
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
            gias,
            year,
        )

    return None


def pre_process_high_exec_pay(
    run_type: str,
    year: int,
    run_id: str,
) -> pd.DataFrame | None:
    """
    Process high exec pay data.

    :param run_type: should only be "default"
    :param run_id: unique identifer for data-writes
    :param year: financial year in question
    :return: High exec pay data, if present for the year in question
    """
    if high_exec_pay_data := try_get_blob(
        raw_container,
        f"{run_type}/{year}/HExP.csv",
    ):
        return build_high_exec_pay_data(
            high_exec_pay_data,
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
        logger.info(
            f"Central Services Data preprocessed {year=} shape: {central_services.shape}"
        )

        write_blob(
            "pre-processed",
            f"{run_type}/{run_id}/central_services.parquet",
            central_services.to_parquet(),
        )

        return central_services

    return None
