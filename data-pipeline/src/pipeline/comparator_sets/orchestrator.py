import time

import pandas as pd

from pipeline.utils.database import insert_comparator_set
from pipeline.utils.log import setup_logger
from pipeline.utils.storage import get_blob, write_blob

from .calculations import ComparatorCalculator, prepare_data

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

            # 1. Load Data
            blob_path = f"{run_type}/{run_id}/{school_type}.parquet"
            preprocessed_data = pd.read_parquet(get_blob("pre-processed", blob_path))
            logger.info(f"Loaded {school_type} data. Shape: {preprocessed_data.shape}")

            # 2. Instantiate calculator and run the process
            prepared_data = prepare_data(preprocessed_data)
            calculator = ComparatorCalculator(prepared_data=prepared_data)
            results_df = calculator.calculate_comparator_sets(target_urn=target_urn)
            logger.info(
                f"Computed {school_type} comparators. Shape: {results_df.shape}"
            )

            # 3. Persist the results and the prepared data
            # TODO get rid of this inconsistency
            comparators_parquet_filename_prefix = "academy" if school_type == "academies" else school_type
            write_blob(
                container_name="comparator-sets",
                blob_name=f"{run_type}/{run_id}/{comparators_parquet_filename_prefix}_comparators.parquet",
                data=results_df.to_parquet(),
            )

            # The prepared data (with filled NaNs) is a useful artifact
            if calculator.prepared_data is not None:
                write_blob(
                    container_name="comparator-sets",
                    blob_name=blob_path,
                    data=calculator.prepared_data.to_parquet(),
                )

            all_comparator_results.append(results_df)

        except Exception as e:
            logger.error(f"Failed to process {school_type}. Error: {e}", exc_info=True)
            continue

    # 4. Combine results and insert into the database
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
