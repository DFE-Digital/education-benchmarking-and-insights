import time
import os
import itertools
from multiprocessing import Pool
from typing import List, Optional, Tuple

import pandas as pd

from pipeline.comparator_sets.calculations import prepare_data
from pipeline.utils.database import insert_metric_rag
from pipeline.utils.log import setup_logger
from pipeline.utils.storage import get_blob, write_blob

# Import functions from calculations.py that are needed by both the main and worker processes
from .calculations import (
    RAG_RESULT_COLUMNS,
    calculate_rag,
    compute_user_defined_rag,
    prepare_data_for_rag,
    CategoryColumnCache,
    compute_category_rag_statistics,
)

logger = setup_logger(__name__)


# --- Worker functions for multiprocessing ---

# Global dict to hold data for worker processes, avoiding passing large objects repeatedly
_WORKER_GLOBALS = {}

def init_worker(school_data: pd.DataFrame, comparator_data: pd.DataFrame):
    """
    Initializer for each worker process. Prepares data and caches once per process.
    """
    global _WORKER_GLOBALS
    logger.info(f"Initializing worker process: {os.getpid()}")
    _WORKER_GLOBALS['processed_data'] = prepare_data_for_rag(school_data)
    _WORKER_GLOBALS['column_cache'] = CategoryColumnCache(_WORKER_GLOBALS['processed_data'].columns)
    _WORKER_GLOBALS['comparators'] = comparator_data

def run_rag_for_urn_worker(school_urn: str) -> list:
    """
    The task for a single worker: process one school URN.
    This function replicates the logic from the main loop of the serial `calculate_rag`.
    """
    try:
        # Retrieve data prepared by the worker initializer
        processed_data = _WORKER_GLOBALS['processed_data']
        column_cache = _WORKER_GLOBALS['column_cache']
        comparators = _WORKER_GLOBALS['comparators']
        
        target_school = processed_data.loc[school_urn]
        if target_school.get("Partial Years Present", False):
            return []

        results = []
        pupil_urns = comparators.get("Pupil", {}).get(school_urn)
        building_urns = comparators.get("Building", {}).get(school_urn)
        
        from .calculations import rag_category_settings # Local import for worker
        
        for category_name, rag_settings in rag_category_settings.items():
            set_urns = pupil_urns if rag_settings["type"] == "Pupil" else building_urns
            if set_urns is not None and len(set_urns) > 0:
                comparator_set = processed_data[processed_data.index.isin(set_urns)]
                # Use the generator and append its results to our list
                for result in compute_category_rag_statistics(
                    school_urn, category_name, rag_settings,
                    target_school, comparator_set, column_cache
                ):
                    results.append(result)
        return results
    except Exception as e:
        logger.error(f"Error processing URN {school_urn} in worker {os.getpid()}: {e}")
        return []


def create_empty_rag_dataframe() -> pd.DataFrame:
    """Create an empty DataFrame with standard RAG result structure."""
    return pd.DataFrame(columns=RAG_RESULT_COLUMNS).set_index("URN")


def compute_rag_for_school_type(
    school_type: str,
    run_type: str,
    run_id: str,
    school_data: pd.DataFrame,
    comparator_data: pd.DataFrame,
    target_urn: Optional[str] = None,
) -> pd.DataFrame:
    """Compute RAG calculations for a school type, using parallel processing."""
    start_time = time.time()
    logger.info(f"Computing {school_type} RAG calculations")

    all_results = []
    if target_urn:
        # --- Unparallelised ---
        if target_urn not in school_data.index:
            logger.warning(f"Target URN {target_urn} not found in {school_type} data")
            return create_empty_rag_dataframe()
        
        logger.info(f"Processing single target URN {target_urn} serially.")
        try:
            rag_generator = calculate_rag(school_data, comparator_data, target_urn=target_urn)
            all_results = list(rag_generator)
        except Exception as e:
            logger.error(f"Failed to calculate RAG for target URN {target_urn}: {e}")
            return create_empty_rag_dataframe()
    else:
        # --- Parallelised ---
        logger.info("Processing all schools in parallel.")
        schools_to_process = school_data.index.tolist()

        if not schools_to_process:
            logger.warning(f"No schools to process for {school_type}.")
            return create_empty_rag_dataframe()

        try:
            num_processes = os.cpu_count() or 2
            logger.info(f"Utilizing {num_processes} worker processes.")
            pool_init_args = (school_data, comparator_data)

            with Pool(processes=num_processes, initializer=init_worker, initargs=pool_init_args) as pool:
                results_list_of_lists = pool.map(run_rag_for_urn_worker, schools_to_process)
            
            all_results = list(itertools.chain.from_iterable(results_list_of_lists))
        except Exception as e:
            logger.error(f"Parallel RAG calculation failed for {school_type}: {e}", exc_info=True)
            return create_empty_rag_dataframe()

    # --- DataFrame Creation and Storage ---
    rag_df = create_empty_rag_dataframe()
    if all_results:
        rag_df = pd.DataFrame(all_results).set_index("URN")

    elapsed_time = time.time() - start_time
    logger.info(
        f"Completed {school_type} RAG calculations: shape={rag_df.shape}, duration={elapsed_time:.2f}s"
    )

    try:
        write_blob(
            "metric-rag", f"{run_type}/{run_id}/{school_type}.parquet", rag_df.to_parquet()
        )
        logger.debug(f"Saved {school_type} RAG results to storage")
    except Exception as e:
        logger.error(f"Failed to save {school_type} RAG results: {e}")
        raise

    return rag_df


def load_school_data_and_comparators(
    run_type: str, run_id: str, school_type: str
) -> Tuple[pd.DataFrame, pd.DataFrame]:
    """Load school data and comparator mappings from storage."""
    if school_type == "maintained_schools":
        school_filename = "maintained_schools.parquet"
        comparator_filename = "maintained_schools_comparators.parquet"
    elif school_type == "academies":
        school_filename = "academies.parquet"
        comparator_filename = "academy_comparators.parquet"
    else:
        raise ValueError(f"Unknown school type: {school_type}")
        
    try:
        school_data = pd.read_parquet(
            get_blob("comparator-sets", f"{run_type}/{run_id}/{school_filename}")
        )
        comparator_data = pd.read_parquet(
            get_blob("comparator-sets", f"{run_type}/{run_id}/{comparator_filename}")
        )
        logger.debug(f"Loaded {school_type}: data_shape={school_data.shape}, comparators_shape={comparator_data.shape}")
        return school_data, comparator_data
    except Exception as e:
        logger.error(f"Failed to load data for {school_type}: {e}")
        raise


def compute_rag(
    run_type: str,
    run_id: str,
    target_urn: Optional[str] = None,
) -> float:
    """Perform RAG calculations for both maintained schools and academies."""
    overall_start_time = time.time()
    logger.info(f"Starting RAG computation: run_type={run_type}, run_id={run_id}")
    rag_results = []
    
    try:
        for school_type in ["maintained_schools", "academies"]:
            logger.info(f"Processing {school_type}")
            school_data, school_comparators = load_school_data_and_comparators(
                run_type, run_id, school_type
            )
            rag_df = compute_rag_for_school_type(
                school_type, run_type, run_id, school_data, school_comparators, target_urn=target_urn,
            )
            logger.info(f"{school_type.capitalize()} RAG results: shape={rag_df.shape}")
            rag_results.append(rag_df)

        if rag_results:
            combined_rag_results = pd.concat(rag_results, axis=0)
            logger.info(f"Combined RAG results: shape={combined_rag_results.shape}")
            insert_metric_rag(run_type, run_id, combined_rag_results)
            logger.info("Successfully inserted RAG results into database")
        else:
            logger.warning("No RAG results to combine or insert")

    except Exception as e:
        logger.error(f"Critical error during RAG computation: {e}", exc_info=True)
        raise

    total_duration = time.time() - overall_start_time
    logger.info(f"RAG computation completed in {total_duration:,.2f} seconds")
    return total_duration


def run_user_defined_rag(
    year: int,
    run_id: str,
    target_urn: int,
    comparator_set: List[int],
) -> float:
    """Perform user-defined RAG calculations using custom comparator set."""
    run_type = "default"
    start_time = time.time()
    logger.info(f"Starting user-defined RAG: year={year}, run_id={run_id}, target_urn={target_urn}")

    try:
        all_schools_data = pd.read_parquet(
            get_blob("pre-processed", f"{run_type}/{year}/all_schools.parquet")
        )
        prepared_data = prepare_data(all_schools_data)
        logger.info(f"Prepared all schools data for {year}: shape={prepared_data.shape}")

        if target_urn not in prepared_data.index:
            raise ValueError(f"Target URN {target_urn} not found in data for year {year}")

        valid_comparator_urns = [urn for urn in comparator_set if urn in prepared_data.index]
        if not valid_comparator_urns:
            raise ValueError("No valid comparator URNs found in the dataset")

        logger.info(f"Validated comparators: {len(valid_comparator_urns)}/{len(comparator_set)} URNs found")

        rag_computation_start = time.time()
        user_defined_rag_results = pd.DataFrame(
            compute_user_defined_rag(
                data=prepared_data, target_urn=target_urn, comparator_urns=valid_comparator_urns,
            )
        ).set_index("URN")
        
        computation_duration = time.time() - rag_computation_start
        logger.info(f"User-defined RAG computation completed: shape={user_defined_rag_results.shape}, duration={computation_duration:.2f}s")

        write_blob(
            "metric-rag", f"{run_type}/{run_id}/user_defined.parquet", user_defined_rag_results.to_parquet(),
        )
        logger.debug("Saved user-defined RAG results to storage")

        insert_metric_rag(run_type=run_type, run_id=run_id, df=user_defined_rag_results)
        logger.info("Successfully inserted user-defined RAG results into database")

    except Exception as e:
        logger.error(f"Critical error during user-defined RAG computation: {e}", exc_info=True)
        raise

    total_duration = time.time() - start_time
    logger.info(f"User-defined RAG analysis completed in {total_duration:,.2f} seconds")
    return total_duration