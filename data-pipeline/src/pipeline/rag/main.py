import time
from typing import List, Optional, Tuple

import pandas as pd

from pipeline.comparator_sets.calculations import prepare_data
from pipeline.utils.database import insert_metric_rag
from pipeline.utils.log import setup_logger
from pipeline.utils.storage import get_blob, write_blob

from .calculations import RAG_RESULT_COLUMNS, calculate_rag, compute_user_defined_rag

logger = setup_logger(__name__)


def create_empty_rag_dataframe() -> pd.DataFrame:
    """
    Create an empty DataFrame with standard RAG result structure.

    Returns:
        Empty DataFrame with proper RAG columns and URN index
    """
    return pd.DataFrame(columns=RAG_RESULT_COLUMNS).set_index("URN")


def compute_rag_for_school_type(
    school_type: str,
    run_type: str,
    run_id: str,
    school_data: pd.DataFrame,
    comparator_data: pd.DataFrame,
    target_urn: Optional[str] = None,
) -> pd.DataFrame:
    """
    Compute RAG calculations for a specific school type.

    Args:
        school_type: Type of schools ("maintained_schools" or "academies")
        run_type: Type of run ("default" or "custom")
        run_id: Unique identifier for this run
        school_data: DataFrame containing school data
        comparator_data: DataFrame containing comparator mappings
        target_urn: Optional URN to compute RAG for specific school only

    Returns:
        DataFrame containing RAG results
    """
    start_time = time.time()
    logger.info(f"Computing {school_type} RAG calculations")

    # Check if target URN exists in data when specified
    if target_urn and target_urn not in school_data.index:
        logger.warning(f"Target URN {target_urn} not found in {school_type} data")
        rag_results = create_empty_rag_dataframe()
    else:
        # Perform RAG calculations
        try:
            rag_results = pd.DataFrame(
                calculate_rag(school_data, comparator_data, target_urn=target_urn)
            ).set_index("URN")
        except Exception as e:
            logger.error(f"Failed to calculate RAG for {school_type}: {e}")
            rag_results = create_empty_rag_dataframe()

    elapsed_time = time.time() - start_time
    logger.info(
        f"Completed {school_type} RAG calculations: "
        f"shape={rag_results.shape}, duration={elapsed_time:.2f}s"
    )

    # Save results to storage
    try:
        write_blob(
            "metric-rag",
            f"{run_type}/{run_id}/{school_type}.parquet",
            rag_results.to_parquet(),
        )
        logger.debug(f"Saved {school_type} RAG results to storage")
    except Exception as e:
        logger.error(f"Failed to save {school_type} RAG results: {e}")
        raise

    return rag_results


def load_school_data_and_comparators(
    run_type: str, run_id: str, school_type: str, comparator_suffix: str = ""
) -> Tuple[pd.DataFrame, pd.DataFrame]:
    """
    Load school data and comparator mappings from storage.

    Args:
        run_type: Type of run ("default" or "custom")
        run_id: Unique identifier for this run
        school_type: Type of schools to load
        comparator_suffix: Optional suffix for comparator file naming

    Returns:
        Tuple of (school_data, comparator_data)

    Raises:
        Exception: If data loading fails
    """
    try:
        school_data = pd.read_parquet(
            get_blob("comparator-sets", f"{run_type}/{run_id}/{school_type}.parquet")
        )

        comparator_filename = f"{school_type}{comparator_suffix}_comparators.parquet"
        comparator_data = pd.read_parquet(
            get_blob("comparator-sets", f"{run_type}/{run_id}/{comparator_filename}")
        )

        logger.debug(
            f"Loaded {school_type}: data_shape={school_data.shape}, "
            f"comparators_shape={comparator_data.shape}"
        )

        return school_data, comparator_data

    except Exception as e:
        logger.error(f"Failed to load data for {school_type}: {e}")
        raise


def compute_rag(
    run_type: str,
    run_id: str,
    target_urn: Optional[str] = None,
) -> float:
    """
    Perform RAG calculations for both maintained schools and academies.

    Args:
        run_type: "default" or "custom" data type
        run_id: Job identifier for data location
        target_urn: Optional URN for computing RAG for specific school only

    Returns:
        Total duration of RAG calculations in seconds

    Raises:
        Exception: If critical errors occur during computation
    """
    overall_start_time = time.time()
    logger.info(f"Starting RAG computation: run_type={run_type}, run_id={run_id}")

    rag_results = []

    try:
        # Process maintained schools
        logger.info("Processing maintained schools")
        maintained_schools_data, maintained_schools_comparators = (
            load_school_data_and_comparators(run_type, run_id, "maintained_schools")
        )

        maintained_schools_rag = compute_rag_for_school_type(
            "maintained_schools",
            run_type,
            run_id,
            maintained_schools_data,
            maintained_schools_comparators,
            target_urn=target_urn,
        )
        logger.info(
            f"Maintained schools RAG results: shape={maintained_schools_rag.shape}"
        )
        rag_results.append(maintained_schools_rag)

        # Process academies
        logger.info("Processing academies")
        academy_data, academy_comparators = load_school_data_and_comparators(
            run_type,
            run_id,
            "academies",
            "_comparators",  # Note: different naming pattern
        )

        academies_rag = compute_rag_for_school_type(
            "academies",
            run_type,
            run_id,
            academy_data,
            academy_comparators,
            target_urn=target_urn,
        )
        logger.info(f"Academies RAG results: shape={academies_rag.shape}")
        rag_results.append(academies_rag)

        # Combine results from both school types
        if rag_results:
            combined_rag_results = pd.concat(rag_results, axis=0)
            logger.info(f"Combined RAG results: shape={combined_rag_results.shape}")

            # Insert results into database
            try:
                insert_metric_rag(run_type, run_id, combined_rag_results)
                logger.info("Successfully inserted RAG results into database")
            except Exception as e:
                logger.error(f"Failed to insert RAG results into database: {e}")
                raise
        else:
            logger.warning("No RAG results to combine or insert")

    except Exception as e:
        logger.error(f"Critical error during RAG computation: {e}")
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
    Perform user-defined RAG calculations using custom comparator set.

    Uses pre-processed "all-schools" data to ensure coverage of
    the user-defined comparator set.

    Note: run_type is always "default" for user-defined comparator sets.

    Args:
        year: Financial year for the analysis
        run_id: Unique identifier for this analysis run
        target_urn: URN of the target organization
        comparator_set: List of URNs to use as comparators

    Returns:
        Duration of RAG calculations in seconds

    Raises:
        Exception: If critical errors occur during computation
    """
    run_type = "default"
    start_time = time.time()

    logger.info(
        f"Starting user-defined RAG computation: year={year}, run_id={run_id}, "
        f"target_urn={target_urn}, comparator_count={len(comparator_set)}"
    )

    try:
        # Load and prepare all schools data
        all_schools_data = pd.read_parquet(
            get_blob("pre-processed", f"{run_type}/{year}/all_schools.parquet")
        )

        prepared_data = prepare_data(all_schools_data)
        logger.info(
            f"Prepared all schools data for {year}: shape={prepared_data.shape}"
        )

        # Validate target URN and comparator set
        if target_urn not in prepared_data.index:
            raise ValueError(
                f"Target URN {target_urn} not found in data for year {year}"
            )

        valid_comparator_urns = [
            urn for urn in comparator_set if urn in prepared_data.index
        ]

        if not valid_comparator_urns:
            raise ValueError("No valid comparator URNs found in the dataset")

        logger.info(
            f"Validated comparators: {len(valid_comparator_urns)}/{len(comparator_set)} URNs found"
        )

        # Perform user-defined RAG calculations
        rag_computation_start = time.time()
        try:
            user_defined_rag_results = pd.DataFrame(
                compute_user_defined_rag(
                    data=prepared_data,
                    target_urn=target_urn,
                    comparator_urns=valid_comparator_urns,
                )
            ).set_index("URN")
        except Exception as e:
            logger.error(f"Failed to compute user-defined RAG: {e}")
            raise

        computation_duration = time.time() - rag_computation_start
        logger.info(
            f"User-defined RAG computation completed: "
            f"shape={user_defined_rag_results.shape}, duration={computation_duration:.2f}s"
        )

        # Save results to storage
        try:
            write_blob(
                "metric-rag",
                f"{run_type}/{run_id}/user_defined.parquet",
                user_defined_rag_results.to_parquet(),
            )
            logger.debug("Saved user-defined RAG results to storage")
        except Exception as e:
            logger.error(f"Failed to save user-defined RAG results: {e}")
            raise

        # Insert results into database
        try:
            insert_metric_rag(
                run_type=run_type,
                run_id=run_id,
                df=user_defined_rag_results,
            )
            logger.info("Successfully inserted user-defined RAG results into database")
        except Exception as e:
            logger.error(
                f"Failed to insert user-defined RAG results into database: {e}"
            )
            raise

    except ValueError as e:
        logger.error(f"Validation error in user-defined RAG: {e}")
        raise
    except Exception as e:
        logger.error(f"Critical error during user-defined RAG computation: {e}")
        raise

    total_duration = time.time() - start_time
    logger.info(f"User-defined RAG analysis completed in {total_duration:,.2f} seconds")

    return total_duration
