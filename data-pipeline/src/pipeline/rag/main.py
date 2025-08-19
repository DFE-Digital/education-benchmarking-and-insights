import itertools
import os
import time
from multiprocessing import Pool
from typing import Dict, List, Optional, Tuple

import pandas as pd

from pipeline.comparator_sets.calculations import prepare_data
from pipeline.utils.database import insert_metric_rag
from pipeline.utils.log import setup_logger
from pipeline.utils.storage import get_blob, write_blob

from .calculations import (
    RAG_RESULT_COLUMNS,
    CategoryColumnCache,
    process_single_urn,
)

logger = setup_logger(__name__)

# --- Worker functions for multiprocessing ---

_WORKER_GLOBALS = {}


def init_worker(processed_data: pd.DataFrame, comparator_map: Dict[str, dict]):
    """
    Initializer for each worker process. Caches data once per process.
    """
    global _WORKER_GLOBALS
    logger.info(f"Initializing worker process: {os.getpid()}")
    _WORKER_GLOBALS["processed_data"] = processed_data
    _WORKER_GLOBALS["column_cache"] = CategoryColumnCache(processed_data.columns)
    _WORKER_GLOBALS["comparator_map"] = comparator_map


def run_rag_for_urn_worker(school_urn: str) -> list:
    """
    The task for a single worker: wraps the core single-URN processing logic.
    """
    try:
        processed_data = _WORKER_GLOBALS["processed_data"]
        column_cache = _WORKER_GLOBALS["column_cache"]
        comparator_map = _WORKER_GLOBALS["comparator_map"]

        target_school = processed_data.loc[school_urn]
        # Get comparators specific to this URN from the map
        comparators = comparator_map.get(school_urn, {})

        results_generator = process_single_urn(
            school_urn, target_school, processed_data, comparators, column_cache
        )
        return list(results_generator)

    except Exception as e:
        logger.error(f"Error processing URN {school_urn} in worker {os.getpid()}: {e}")
        return []


def create_empty_rag_dataframe() -> pd.DataFrame:
    """Create an empty DataFrame with standard RAG result structure."""
    return pd.DataFrame(columns=RAG_RESULT_COLUMNS).set_index("URN")


def _run_rag_computation_engine(
    processed_data: pd.DataFrame,
    comparator_map: Dict[str, dict],
    target_urn: Optional[str] = None,
) -> pd.DataFrame:
    """
    Core RAG computation engine. Orchestrates serial or parallel execution.
    This function is pure: it takes data in and returns a DataFrame, with no side effects like I/O.
    """
    all_results = []
    empty_rag_df = create_empty_rag_dataframe()
    column_cache = CategoryColumnCache(processed_data.columns)

    if target_urn:
        # --- Unparallelised ---
        if target_urn not in processed_data.index:
            logger.warning(f"Target URN {target_urn} not found in data")
            return empty_rag_df

        logger.info(f"Processing single target URN {target_urn} serially.")
        try:
            target_school = processed_data.loc[target_urn]
            comparators = comparator_map.get(target_urn, {})
            rag_generator = process_single_urn(
                target_urn, target_school, processed_data, comparators, column_cache
            )
            all_results = list(rag_generator)
        except Exception as e:
            logger.error(f"Failed to calculate RAG for target URN {target_urn}: {e}")
            return empty_rag_df
    else:
        # --- Parallelised ---
        logger.info("Processing all schools in parallel.")
        schools_to_process = list(comparator_map.keys())

        if not schools_to_process:
            logger.warning("No schools to process.")
            return empty_rag_df

        try:
            num_processes = os.cpu_count() or 2
            logger.info(f"Utilizing {num_processes} worker processes.")
            pool_init_args = (processed_data, comparator_map)

            with Pool(
                processes=num_processes,
                initializer=init_worker,
                initargs=pool_init_args,
            ) as pool:
                results_list_of_lists = pool.map(
                    run_rag_for_urn_worker, schools_to_process
                )

            all_results = list(itertools.chain.from_iterable(results_list_of_lists))
        except Exception as e:
            logger.error("Parallel RAG calculation failed: {e}", exc_info=True)
            return empty_rag_df

    return pd.DataFrame(all_results).set_index("URN") if all_results else empty_rag_df


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
        logger.debug(
            f"Loaded {school_type}: data_shape={school_data.shape}, comparators_shape={comparator_data.shape}"
        )
        return school_data, comparator_data
    except Exception as e:
        logger.error(f"Failed to load data for {school_type}: {e}")
        raise

    
# --- Entrypoint Functions (Responsible for I/O and calling the engine) ---


def compute_rag(run_type: str, run_id: str, target_urn: Optional[str] = None) -> float:
    """
    Perform default RAG calculations for both maintained schools and academies.
    This function handles DATA LOADING and PERSISTENCE.
    """
    overall_start_time = time.time()
    logger.info(f"Starting RAG computation: run_type={run_type}, run_id={run_id}")
    rag_results = []

    try:
        for school_type in ["maintained_schools", "academies"]:
            logger.info(f"Processing {school_type}")
            # STEP 1: LOAD DATA (I/O)
            school_data, school_comparators = load_school_data_and_comparators(
                run_type, run_id, school_type
            )

            # Prepare data and comparator map for the engine
            processed_data = prepare_data(school_data)
            comparator_map = school_comparators.to_dict(orient="index")

            # STEP 2: COMPUTE (Call the pure engine)
            start_time = time.time()
            rag_df = _run_rag_computation_engine(
                processed_data, comparator_map, target_urn=target_urn
            )
            elapsed_time = time.time() - start_time
            logger.info(
                f"Completed {school_type} RAG calculations: shape={rag_df.shape}, duration={elapsed_time:.2f}s"
            )

            # STEP 3: PERSIST DATA (I/O)
            if not rag_df.empty:
                write_blob(
                    "metric-rag",
                    f"{run_type}/{run_id}/{school_type}.parquet",
                    rag_df.to_parquet(),
                )
                logger.debug(f"Saved {school_type} RAG results to storage")
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
    """
    Perform user-defined RAG calculations.
    This function handles DATA LOADING and PERSISTENCE for the user-defined case.
    """
    run_type = "default"
    start_time = time.time()
    logger.info(
        f"Starting user-defined RAG: year={year}, run_id={run_id}, target_urn={target_urn}"
    )

    try:
        # STEP 1: LOAD DATA (I/O)
        all_schools_data = pd.read_parquet(
            get_blob("pre-processed", f"{run_type}/{year}/all_schools.parquet")
        )
        processed_data = prepare_data(all_schools_data)
        if target_urn not in processed_data.index:
            raise ValueError(f"Target URN {target_urn} not found in data for {year}")

        # STEP 2: PREPARE INPUTS FOR THE ENGINE
        valid_urns = [urn for urn in comparator_set if urn in processed_data.index]
        if not valid_urns:
            raise ValueError("No valid comparator URNs found in the dataset")

        # Create the specific comparator map required by the engine
        # For user-defined, both Pupil and Building comparators are the same set
        comparator_map = {
            target_urn: {"Pupil": valid_urns, "Building": valid_urns}
        }

        # STEP 3: COMPUTE (Call the pure engine)
        rag_computation_start = time.time()
        user_defined_rag_results = _run_rag_computation_engine(
            processed_data, comparator_map, target_urn=target_urn
        )
        computation_duration = time.time() - rag_computation_start
        logger.info(
            f"User-defined RAG computation completed: shape={user_defined_rag_results.shape}, duration={computation_duration:.2f}s"
        )

        # STEP 4: PERSIST DATA (I/O)
        if not user_defined_rag_results.empty:
            write_blob(
                "metric-rag",
                f"{run_type}/{run_id}/user_defined.parquet",
                user_defined_rag_results.to_parquet(),
            )
            logger.debug("Saved user-defined RAG results to storage")
            insert_metric_rag(run_type, run_id, user_defined_rag_results)
            logger.info("Successfully inserted user-defined RAG results into database")

    except Exception as e:
        logger.error(
            f"Critical error during user-defined RAG computation: {e}", exc_info=True
        )
        raise

    total_duration = time.time() - start_time
    logger.info(f"User-defined RAG analysis completed in {total_duration:,.2f} seconds")
    return total_duration