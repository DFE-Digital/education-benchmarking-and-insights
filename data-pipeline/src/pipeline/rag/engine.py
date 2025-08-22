import itertools
import os
from multiprocessing import Pool
from typing import Dict, Optional

import pandas as pd

from pipeline.utils.log import setup_logger

from .calculations import RAG_RESULT_COLUMNS, CategoryColumnCache, process_single_urn

logger = setup_logger(__name__)


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


def _run_rag_computation_engine(
    processed_data: pd.DataFrame,
    comparator_map: dict[str, dict[str, list[int]]],
    target_urn: Optional[str] = None,
) -> pd.DataFrame:
    """
    Core RAG computation engine. Orchestrates serial or parallel execution.
    This function is pure: it takes data in and returns a DataFrame, with no side effects like I/O.
    """
    all_results = []
    empty_rag_df = pd.DataFrame(columns=RAG_RESULT_COLUMNS).set_index("URN")
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
