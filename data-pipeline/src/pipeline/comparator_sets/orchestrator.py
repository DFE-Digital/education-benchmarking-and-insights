import time

import pandas as pd

from pipeline.utils.database import insert_comparator_set
from pipeline.utils.log import setup_logger
from pipeline.utils.storage import get_blob, write_blob

from .calculations import ComparatorCalculator, prepare_data
from .config import cols_for_comparators_parquet

logger = setup_logger(__name__)


def run_comparator_sets_pipeline(
    run_type: str, run_id: str, target_urn: int | None = None
) -> float:
    """
    Determines Comparator Sets for all specified school types, orchestrating
    the loading, processing, and saving of data.

    :param run_type: "default" or "custom" data type.
    :param run_id: Job identifier for the run.
    :param target_urn: Optional URN to process a single school.
    :return: Duration of the calculation in seconds.
    """
    start_time = time.time()
    logger.info("Starting comparator set computation.")

    school_types = ["academies", "maintained_schools"]
    all_comparator_results = []

    for school_type in school_types:
        try:
            logger.info(f"Processing {school_type}...")

            # 1. Load preprocessed data
            blob_path = f"{run_type}/{run_id}/{school_type}.parquet"
            preprocessed_data = pd.read_parquet(get_blob("pre-processed", blob_path))
            logger.info(f"Loaded {school_type} data. Shape: {preprocessed_data.shape}")

            # 2. Do more preprocessing, mostly filling NaNs. Persist this again.
            # TODO Move this to preprocessing
            prepared_data = prepare_data(preprocessed_data)
            if prepared_data is not None:
                write_blob(
                    container_name="comparator-sets",
                    blob_name=blob_path,
                    data=prepared_data.to_parquet(),
                )

            # 3. Instantiate comparator calculator and calculate comparator sets
            calculator = ComparatorCalculator(prepared_data=prepared_data)
            comparator_sets_df = calculator.calculate_comparator_sets(
                target_urn=target_urn
            )
            logger.info(
                f"Computed {school_type} comparators. Shape: {comparator_sets_df.shape}"
            )

            # 4. Persist the comparator sets
            # TODO get rid of naming inconsistency
            comparators_parquet_filename_prefix = (
                "academy" if school_type == "academies" else school_type
            )
            results_for_parquet_df = comparator_sets_df[cols_for_comparators_parquet]
            write_blob(
                container_name="comparator-sets",
                blob_name=f"{run_type}/{run_id}/{comparators_parquet_filename_prefix}_comparators.parquet",
                data=results_for_parquet_df.to_parquet(index=True),
            )

            all_comparator_results.append(comparator_sets_df)

        except Exception as e:
            logger.error(f"Failed to process {school_type}. Error: {e}", exc_info=True)
            continue

    # 5. Combine results and insert into the database
    if all_comparator_results:
        final_comparators = pd.concat(all_comparator_results, axis=0)
        if not final_comparators.empty:
            logger.info(
                f"Inserting {len(final_comparators)} total comparator sets into the database."
            )
            insert_comparator_set(
                run_type=run_type,
                run_id=run_id,
                df=final_comparators,
            )
    else:
        logger.warning("No comparator sets were generated.")

    time_taken = time.time() - start_time
    logger.info(f"Comparator set computation finished in {time_taken:,.2f} seconds.")

    return time_taken
