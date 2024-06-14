import json
import logging
import os
import time
from contextlib import suppress

import pandas as pd
import tornado.iostream
from azure.core.exceptions import ResourceNotFoundError
from azure.storage.queue import QueueClient, QueueMessage
from dask.distributed import Client
from dotenv import load_dotenv

load_dotenv()

from src.pipeline.comparator_sets import compute_comparator_set, prepare_data
from src.pipeline.database import (
    insert_comparator_set,
    insert_financial_data,
    insert_metric_rag,
    insert_non_financial_data,
    insert_schools_and_trusts_and_local_authorities,
)
from src.pipeline.log import setup_logger
from src.pipeline.pre_processing import (
    build_academy_data,
    build_bfr_data,
    build_cfo_data,
    build_federations_data,
    build_maintained_school_data,
    prepare_aar_data,
    prepare_cdc_data,
    prepare_census_data,
    prepare_central_services_data,
    prepare_ks2_data,
    prepare_ks4_data,
    prepare_schools_data,
    prepare_sen_data,
)
from src.pipeline.rag import compute_rag, compute_user_defined_rag
from src.pipeline.storage import (
    blob_service_client,
    complete_queue_name,
    connect_to_queue,
    get_blob,
    queue_service_client,
    raw_container,
    worker_queue_name,
    write_blob,
)

logger = setup_logger("fbit-data-pipeline")

ds_logger = logging.getLogger("distributed.utils_perf")
ds_logger.setLevel(logging.ERROR)


def pre_process_cdc(run_type: str, year: int) -> pd.DataFrame:
    logger.info("Processing CDC Data")
    cdc_data = get_blob(raw_container, f"{run_type}/{year}/cdc.csv", encoding="utf-8")
    cdc = prepare_cdc_data(cdc_data, year)
    write_blob("pre-processed", f"{run_type}/{year}/cdc.parquet", cdc.to_parquet())

    return cdc


def pre_process_census(run_type, year) -> pd.DataFrame:
    logger.info("Processing Census Data")
    workforce_census_data = get_blob(
        raw_container, f"{run_type}/{year}/census_workforce.xlsx"
    )
    pupil_census_data = get_blob(
        raw_container, f"{run_type}/{year}/census_pupils.csv", encoding="utf-8"
    )
    census = prepare_census_data(workforce_census_data, pupil_census_data)
    write_blob(
        "pre-processed",
        f"{run_type}/{year}/census.parquet",
        census.to_parquet(),
    )

    return census


def pre_process_sen(run_type, year) -> pd.DataFrame:
    logger.info("Processing SEN Data")
    sen_data = get_blob(raw_container, f"{run_type}/{year}/sen.csv", encoding="cp1252")
    sen = prepare_sen_data(sen_data)
    write_blob("pre-processed", f"{run_type}/{year}/sen.parquet", sen.to_parquet())

    return sen


def pre_process_ks2(run_type, year) -> pd.DataFrame:
    logger.info("Processing KS2 Data")
    ks2_data = get_blob(raw_container, f"{run_type}/{year}/ks2.xlsx")
    ks2 = prepare_ks2_data(ks2_data)
    write_blob("pre-processed", f"{run_type}/{year}/ks2.parquet", ks2.to_parquet())

    return ks2


def pre_process_ks4(run_type, year) -> pd.DataFrame:
    logger.info("Processing KS4 Data")
    ks4_data = get_blob(raw_container, f"{run_type}/{year}/ks4.xlsx")
    ks4 = prepare_ks4_data(ks4_data)
    write_blob("pre-processed", f"{run_type}/{year}/ks4.parquet", ks4.to_parquet())

    return ks4


def pre_process_academy_ar(run_type, year) -> tuple[pd.DataFrame, pd.DataFrame]:
    logger.info("Processing Academy AR Data")
    academy_ar_data = get_blob(raw_container, f"{run_type}/{year}/academy_ar.xlsx")
    aar = prepare_aar_data(academy_ar_data)

    write_blob(
        "pre-processed",
        f"{run_type}/{year}/aar.parquet",
        aar.to_parquet(),
    )

    return aar


def pre_process_schools(run_type, year) -> pd.DataFrame:
    logger.info("Processing Schools Data")
    gias_data = get_blob(
        raw_container, f"{run_type}/{year}/gias.csv", encoding="cp1252"
    )
    gias_links_data = get_blob(
        raw_container, f"{run_type}/{year}/gias_links.csv", encoding="cp1252"
    )
    schools = prepare_schools_data(gias_data, gias_links_data)
    write_blob(
        "pre-processed",
        f"{run_type}/{year}/schools.parquet",
        schools.to_parquet(),
    )

    return schools


def pre_process_cfo(run_type, year) -> pd.DataFrame:
    logger.info("Processing CFO Data")
    cfo_data = get_blob(raw_container, f"{run_type}/{year}/cfo.xlsx")

    cfo = build_cfo_data(cfo_data)
    write_blob(
        "pre-processed",
        f"{run_type}/{year}/cfo.parquet",
        cfo.to_parquet(),
    )

    return cfo


def pre_process_central_services(run_type, year) -> pd.DataFrame:
    logger.info("Building Central Services Data")

    academies_data = get_blob(raw_container, f"{run_type}/{year}/academy_ar.xlsx")

    central_services = prepare_central_services_data(academies_data)

    write_blob(
        "pre-processed",
        f"{run_type}/{year}/central_services.parquet",
        central_services.to_parquet(),
    )

    return central_services


def pre_process_academies_data(run_type, year, data_ref) -> pd.DataFrame:
    logger.info("Building Academy Set")
    schools, census, sen, cdc, aar, ks2, ks4, cfo, central_services = data_ref

    academies_data = get_blob(
        raw_container, f"{run_type}/{year}/academy_master_list.csv", encoding="utf-8"
    )

    links_data = get_blob(
        raw_container, f"{run_type}/{year}/gias_all_links.csv", encoding="cp1252"
    )

    academies = build_academy_data(
        academies_data,
        links_data,
        year,
        schools,
        census,
        sen,
        cdc,
        aar,
        ks2,
        ks4,
        cfo,
        central_services,
    )

    write_blob(
        "pre-processed",
        f"{run_type}/{year}/academies.parquet",
        academies.to_parquet(),
    )

    return academies


def pre_process_maintained_schools_data(run_type, year, data_ref) -> pd.DataFrame:
    logger.info("Building Maintained School Set")
    schools, census, sen, cdc, aar, ks2, ks4, cfo, central_services = data_ref

    maintained_schools_data = get_blob(
        raw_container,
        f"{run_type}/{year}/maintained_schools_master_list.csv",
        encoding="utf-8",
    )

    links_data = get_blob(
        raw_container, f"{run_type}/{year}/gias_all_links.csv", encoding="cp1252"
    )

    maintained_schools = build_maintained_school_data(
        maintained_schools_data, links_data, year, schools, census, sen, cdc, ks2, ks4
    )

    write_blob(
        "pre-processed",
        f"{run_type}/{year}/maintained_schools.parquet",
        maintained_schools.to_parquet(),
    )

    return maintained_schools


def pre_process_federations(run_type, year, data_ref):
    logger.info("Building Federations Set")
    academies, maintained_schools = data_ref

    gias_all_links_data = get_blob(
        raw_container,
        f"{run_type}/{year}/gias_all_links.csv",
        encoding="unicode-escape",
    )

    (hard_federations, soft_federations) = build_federations_data(
        gias_all_links_data, maintained_schools
    )

    write_blob(
        "pre-processed",
        f"{run_type}/{year}/hard_federations.parquet",
        hard_federations.to_parquet(),
    )

    write_blob(
        "pre-processed",
        f"{run_type}/{year}/soft_federations.parquet",
        soft_federations.to_parquet(),
    )


def pre_process_all_schools(run_type, year, data_ref):
    logger.info("Building All schools Set")
    academies, maintained_schools = data_ref

    all_schools = pd.concat([academies, maintained_schools], axis=0)

    # TODO: Shouldn't need to filter this out
    all_schools = all_schools[~all_schools["Financial Position"].isna()]

    write_blob(
        "pre-processed",
        f"{run_type}/{year}/all_schools.parquet",
        all_schools.to_parquet(),
    )

    insert_schools_and_trusts_and_local_authorities(run_type, year, all_schools)
    insert_non_financial_data(run_type, year, all_schools)
    insert_financial_data(run_type, year, all_schools)


def pre_process_bfr(run_type, year):
    logger.info("Processing BFR Data")

    bfr_sofa = get_blob(
        raw_container, f"{run_type}/{year}/BFR_SOFA_raw.csv", encoding="unicode-escape"
    )

    bfr_3y = get_blob(
        raw_container, f"{run_type}/{year}/BFR_3Y_raw.csv", encoding="unicode-escape"
    )

    academies_y2 = prepare_data(
        pd.read_parquet(
            get_blob("pre-processed", f"{run_type}/{year-2}/academies.parquet")
        )
    )

    academies_y1 = prepare_data(
        pd.read_parquet(
            get_blob("pre-processed", f"{run_type}/{year-1}/academies.parquet")
        )
    )

    academies = prepare_data(
        pd.read_parquet(
            get_blob("pre-processed", f"{run_type}/{year}/academies.parquet")
        )
    )
    bfr_metrics, bfr = build_bfr_data(
        bfr_sofa, bfr_3y, academies_y2, academies_y1, academies
    )

    write_blob(
        "pre-processed",
        f"{run_type}/{year}/bfr_metrics.parquet",
        bfr_sofa.to_parquet(),
    )

    write_blob(
        "pre-processed",
        f"{run_type}/{year}/bfr.parquet",
        bfr_3y.to_parquet(),
    )

    return bfr_metrics, bfr


def pre_process_data(worker_client, run_type, year):
    start_time = time.time()
    logger.info(f"Pre-processing data {run_type} - {year}")

    cdc, census, sen, ks2, ks4, aar, schools, cfo, central_services = (
        worker_client.gather(
            [
                worker_client.submit(pre_process_cdc, run_type, year),
                worker_client.submit(pre_process_census, run_type, year),
                worker_client.submit(pre_process_sen, run_type, year),
                worker_client.submit(pre_process_ks2, run_type, year),
                worker_client.submit(pre_process_ks4, run_type, year),
                worker_client.submit(pre_process_academy_ar, run_type, year),
                worker_client.submit(pre_process_schools, run_type, year),
                worker_client.submit(pre_process_cfo, run_type, year),
                worker_client.submit(pre_process_central_services, run_type, year),
            ]
        )
    )

    data_ref = worker_client.scatter(
        (schools, census, sen, cdc, aar, ks2, ks4, cfo, central_services)
    )

    academies, maintained_schools = worker_client.gather(
        [
            worker_client.submit(pre_process_academies_data, run_type, year, data_ref),
            worker_client.submit(
                pre_process_maintained_schools_data, run_type, year, data_ref
            ),
        ]
    )

    pre_process_all_schools(run_type, year, (academies, maintained_schools))

    # pre_process_bfr(run_type, year)

    time_taken = time.time() - start_time
    logger.info(f"Pre-processing data done in {time_taken} seconds")

    return time_taken


def compute_comparator_set_for(data_type, set_type, run_type, year, data):
    st = time.time()
    logger.info(f"Computing {data_type} set")
    result = compute_comparator_set(data)
    logger.info(f"Computing {data_type} set. Done in {time.time() - st:.2f} seconds")

    write_blob(
        "comparator-sets",
        f"{run_type}/{year}/{data_type}.parquet",
        result.to_parquet(),
    )

    insert_comparator_set(run_type, set_type, year, result)


def compute_comparator_sets(run_type: str, year: int) -> float:
    """
    Determine Comparator Sets.

    :param run_type: "default" or "custom" data type
    :param year: financial year in question
    :return: duration of calculation
    """
    start_time = time.time()
    logger.info("Computing comparator sets")

    academies = prepare_data(
        pd.read_parquet(
            get_blob("pre-processed", f"{run_type}/{year}/academies.parquet")
        )
    )

    maintained = prepare_data(
        pd.read_parquet(
            get_blob("pre-processed", f"{run_type}/{year}/maintained_schools.parquet")
        )
    )

    all_schools = prepare_data(
        pd.read_parquet(
            get_blob("pre-processed", f"{run_type}/{year}/all_schools.parquet")
        )
    )

    compute_comparator_set_for(
        "academy_comparators", "unmixed", run_type, year, academies
    )
    compute_comparator_set_for(
        "maintained_schools_comparators", "unmixed", run_type, year, maintained
    )
    compute_comparator_set_for(
        "mixed_comparators", "mixed", run_type, year, all_schools
    )

    write_blob(
        "comparator-sets",
        f"{run_type}/{year}/academies.parquet",
        academies.to_parquet(),
    )

    write_blob(
        "comparator-sets",
        f"{run_type}/{year}/maintained_schools.parquet",
        maintained.to_parquet(),
    )

    write_blob(
        "comparator-sets",
        f"{run_type}/{year}/all_schools.parquet",
        all_schools.to_parquet(),
    )

    time_taken = time.time() - start_time
    logger.info(f"Computing comparators sets done in {time_taken} seconds")

    return time_taken


def compute_rag_for(
    data_type: str,
    set_type: str,
    run_type: str,
    year: int,
    data: pd.DataFrame,
    comparators: pd.DataFrame,
):
    st = time.time()
    logger.info(f"Computing {data_type} RAG")
    df = pd.DataFrame(compute_rag(data, comparators)).set_index("URN")

    logger.info(f"Computing {data_type} RAG. Done in {time.time() - st:.2f} seconds")

    write_blob(
        "metric-rag",
        f"{run_type}/{year}/{data_type}.parquet",
        df.to_parquet(),
    )

    insert_metric_rag(run_type, set_type, year, df)


def run_compute_rag(run_type: str, year: int):
    """
    Perform RAG calculations.

    :param run_type: "default" or "custom" data type
    :param year: financial year in question
    :return: duration of RAG calculations
    """
    start_time = time.time()

    ms_data = pd.read_parquet(
        get_blob("comparator-sets", f"{run_type}/{year}/maintained_schools.parquet")
    )
    ms_comparators = pd.read_parquet(
        get_blob(
            "comparator-sets",
            f"{run_type}/{year}/maintained_schools_comparators.parquet",
        )
    )
    compute_rag_for(
        "maintained_schools", "unmixed", run_type, year, ms_data, ms_comparators
    )

    academy_data = pd.read_parquet(
        get_blob("comparator-sets", f"{run_type}/{year}/academies.parquet")
    )
    academy_comparators = pd.read_parquet(
        get_blob("comparator-sets", f"{run_type}/{year}/academy_comparators.parquet")
    )
    compute_rag_for(
        "academies", "unmixed", run_type, year, academy_data, academy_comparators
    )

    mixed_data = pd.read_parquet(
        get_blob("comparator-sets", f"{run_type}/{year}/all_schools.parquet")
    )
    mixed_comparators = pd.read_parquet(
        get_blob("comparator-sets", f"{run_type}/{year}/mixed_comparators.parquet")
    )
    compute_rag_for("mixed", "mixed", run_type, year, mixed_data, mixed_comparators)

    time_taken = time.time() - start_time
    logger.info(f"Computing RAG done in {time_taken} seconds")

    return time_taken


def run_user_defined_rag(
    year: int,
    run_id: str,
    target_urn: int,
    comparator_set: list[int],
):
    """
    Perform user-defined RAG calculations.

    Use the pre-processed "all-schools" data to guarantee coverage of
    the user-defined comparator-set.

    Note: `SetType` is _always_ "mixed" when persisted, for
    user-defined comparator-sets; `run_type` is _always_ "default" for
    the same.

    :param year: financial year in question
    :param run_id: unique run identifier
    :param target_urn: URN of the "target" org.
    :param comparator_set: user-defined comparator-set
    :return: duration of RAG calculations
    """
    set_type = "mixed"
    run_type = "default"

    all_schools = prepare_data(
        pd.read_parquet(
            get_blob("pre-processed", f"{run_type}/{year}/all_schools.parquet")
        )
    )

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
        set_type=set_type,
        year=run_id,
        df=df,
    )


def handle_msg(
    worker_client: Client,
    msg: QueueMessage,
    worker_queue: QueueClient,
    complete_queue: QueueClient,
):
    """
    Process an incoming message.

    TODO: standard message format?

    User-defined comparator set message _content_ will be of the form:

    ```json
    {
        "jobId": "24463424-9642-4314-bb55-45424af6e812",
        "type": "comparator-set",
        "runId": "c321ef6a-3b1c-4ce2-8e32-0d0167bf2fa7",
        "year": 2022,
        "urn": "106057",
        "payload": {
            "kind": "ComparatorSetPayload",
            "set": [
                "145799",
                "142875"
            ]
        }
    }
    ```

    Note: user-defined comparator sets will assume pre-processing has
    taken place for the year in question, failing if that does not hold
    true.

    :param worker_client: Dask client
    :param msg: incoming message, triggering this process
    :param worker_queue: incoming message queue (for deletion)
    :param complete_queue: outcoming message queue (for completion)
    :return: updated message payload
    """
    msg_payload = json.loads(msg.content)
    run_type = msg_payload.get("runType", "default")

    try:
        payload = msg_payload.get("payload", {})
        if payload.get("kind") == "ComparatorSetPayload":
            msg_payload["rag_duration"] = run_user_defined_rag(
                year=msg_payload["year"],
                run_id=msg_payload["runId"],
                target_urn=int(msg_payload["urn"]),
                comparator_set=list(map(int, payload["set"])),
            )
        else:
            msg_payload["pre_process_duration"] = pre_process_data(
                worker_client, run_type, msg_payload["year"]
            )

            msg_payload["comparator_set_duration"] = compute_comparator_sets(
                run_type,
                msg_payload["year"],
            )

            msg_payload["rag_duration"] = run_compute_rag(run_type, msg_payload["year"])

        msg_payload["success"] = True
    except Exception as error:
        logger.exception(
            f"An exception occurred: {type(error).__name__}", exc_info=error
        )
        msg_payload["success"] = False
        msg_payload["error"] = str(error)
    finally:
        with suppress(ResourceNotFoundError):
            worker_queue.delete_message(msg)

        complete_queue.send_message(json.dumps(msg_payload), time_to_live=300)

    return msg_payload


def receive_one_message(worker_client):
    try:
        with blob_service_client, queue_service_client:
            worker_queue = connect_to_queue(worker_queue_name)
            complete_queue = connect_to_queue(complete_queue_name)
            with worker_queue, complete_queue:
                msg = worker_queue.receive_message(visibility_timeout=300)
                if msg is not None:
                    logger.info(f"received message {msg.content}")
                    msg = handle_msg(worker_client, msg, worker_queue, complete_queue)
                    exit(0) if msg["success"] else exit(1)
                else:
                    logger.info("no messages received")
                    exit(0)
    except Exception as error:
        logger.exception(
            f"An exception occurred: {type(error).__name__}", exc_info=error
        )
        exit(-1)


def receive_messages(worker_client):
    try:
        with blob_service_client, queue_service_client:
            worker_queue = connect_to_queue(worker_queue_name)
            complete_queue = connect_to_queue(complete_queue_name)
            with worker_queue, complete_queue:
                while True:
                    msg = worker_queue.receive_message(visibility_timeout=300)
                    if msg is not None:
                        logger.info(f"received message {msg.content}")
                        msg = handle_msg(
                            worker_client, msg, worker_queue, complete_queue
                        )
                        logger.info(f"processed msg response: {msg}")
                    else:
                        time.sleep(1)
    except Exception as error:
        logger.exception(
            f"An exception occurred: {type(error).__name__}", exc_info=error
        )
        exit(-1)


if __name__ == "__main__":
    with suppress(tornado.iostream.StreamClosedError):
        with Client(memory_limit="16GB", heartbeat_interval=None) as client:
            try:
                if os.getenv("ENV") == "dev":
                    receive_messages(client)
                else:
                    receive_one_message(client)
            finally:
                client.close()
