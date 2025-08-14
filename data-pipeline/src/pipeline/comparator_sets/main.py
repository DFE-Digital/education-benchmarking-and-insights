import time

import pandas as pd

from pipeline.utils.database import insert_comparator_set
from pipeline.utils.log import setup_logger
from pipeline.utils.storage import get_blob, write_blob

from .calculations import compute_comparator_set, prepare_data

logger = setup_logger("comparator-sets")


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
    logger.info(
        f"Computing {data_type} set shape={result.shape}. Done in {time.time() - st:.2f} seconds"
    )

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
    logger.info(f"Academies Data preprocessed shape: {academies.shape}")
    maintained = prepare_data(
        pd.read_parquet(
            get_blob(
                "pre-processed",
                f"{run_type}/{run_id}/maintained_schools.parquet",
            )
        )
    )
    logger.info(f"Maintained Data preprocessed shape: {maintained.shape}")

    academies_comparators = compute_comparator_set_for(
        data_type="academy_comparators",
        run_type=run_type,
        data=academies,
        run_id=run_id,
        target_urn=target_urn,
    )
    logger.info(
        f"Academies Comparators preprocessed shape: {academies_comparators.shape}"
    )
    maintained_comparators = compute_comparator_set_for(
        data_type="maintained_schools_comparators",
        run_type=run_type,
        data=maintained,
        run_id=run_id,
        target_urn=target_urn,
    )
    logger.info(
        f"Maintained Comparators preprocessed shape: {maintained_comparators.shape}"
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
