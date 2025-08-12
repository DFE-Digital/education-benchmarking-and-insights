import time

import pandas as pd
from pipeline.comparator_sets.calculations import prepare_data
from pipeline.utils.storage import get_blob, write_blob
from pipeline.utils.database import insert_metric_rag
from pipeline.utils.log import setup_logger

logger = setup_logger("rag")


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

    logger.info(
        f"Computing {data_type} RAG shape={df.shape}. Done in {time.time() - st:.2f} seconds"
    )

    write_blob(
        "metric-rag",
        f"{run_type}/{run_id}/{data_type}.parquet",
        df.to_parquet(),
    )

    return df


def compute_rag(
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
    logger.info(f"Maintained RAG shape {maintained_rag.shape}")

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
    logger.info(f"Academies RAG shape {academies_rag.shape}")

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


def compute_user_defined_rag(
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
    logger.info(f"All Schools Data preprocessed {year=} shape: {all_schools.shape}")

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
