import time
from typing import List, Optional

import pandas as pd

from pipeline.comparator_sets.calculations import prepare_data
from pipeline.utils.database import insert_metric_rag
from pipeline.utils.log import setup_logger
from pipeline.utils.storage import get_blob, write_blob

from .engine import _run_rag_computation_engine
from .loader import load_school_data_and_comparators

logger = setup_logger(__name__)


def run_rag_pipeline(
    run_type: str, run_id: str, target_urn: Optional[str] = None
) -> float:
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


def run_user_defined_rag_pipeline(
    year: int, run_id: str, target_urn: str, comparator_set: List[int]
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
        comparator_map = {target_urn: {"Pupil": valid_urns, "Building": valid_urns}}

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
