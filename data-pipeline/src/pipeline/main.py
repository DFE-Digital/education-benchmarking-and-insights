import json
import logging
import os
import sys
import time
from contextlib import suppress
import pandas as pd
import tornado.iostream
from azure.core.exceptions import ResourceNotFoundError
from dotenv import load_dotenv
from dask.distributed import Client

load_dotenv()

from src.pipeline.database import (
    insert_comparator_set,
    insert_metric_rag,
    insert_school
)

from src.pipeline.rag import compute_rag
from src.pipeline.comparator_sets import (
    compute_comparator_set,
    prepare_data,
)
from src.pipeline.pre_processing import (
    build_academy_data,
    build_federations_data,
    build_maintained_school_data,
    prepare_aar_data,
    prepare_cdc_data,
    prepare_census_data,
    prepare_ks2_data,
    prepare_ks4_data,
    prepare_schools_data,
    prepare_sen_data,
)

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

logger = logging.getLogger("fbit-data-pipeline")
logger.setLevel(logging.INFO)

ds_logger = logging.getLogger("distributed.utils_perf")
ds_logger.setLevel(logging.ERROR)

logging.basicConfig(stream=sys.stdout, level=logging.INFO)


def pre_process_cdc(set_type: str, year: int) -> pd.DataFrame:
    logger.info("Processing CDC Data")
    cdc_data = get_blob(raw_container, f"{set_type}/{year}/cdc.csv", encoding="utf-8")
    cdc = prepare_cdc_data(cdc_data, year)
    write_blob("pre-processed", f"{set_type}/{year}/cdc.parquet", cdc.to_parquet())

    return cdc


def pre_process_census(set_type, year) -> pd.DataFrame:
    logger.info("Processing Census Data")
    workforce_census_data = get_blob(
        raw_container, f"{set_type}/{year}/census_workforce.xlsx"
    )
    pupil_census_data = get_blob(
        raw_container, f"{set_type}/{year}/census_pupils.csv", encoding="utf-8"
    )
    census = prepare_census_data(workforce_census_data, pupil_census_data)
    write_blob(
        "pre-processed",
        f"{set_type}/{year}/census.parquet",
        census.to_parquet(),
    )

    return census


def pre_process_sen(set_type, year) -> pd.DataFrame:
    logger.info("Processing SEN Data")
    sen_data = get_blob(raw_container, f"{set_type}/{year}/sen.csv", encoding="cp1252")
    sen = prepare_sen_data(sen_data)
    write_blob("pre-processed", f"{set_type}/{year}/sen.parquet", sen.to_parquet())

    return sen


def pre_process_ks2(set_type, year) -> pd.DataFrame:
    logger.info("Processing KS2 Data")
    ks2_data = get_blob(raw_container, f"{set_type}/{year}/ks2.xlsx")
    ks2 = prepare_ks2_data(ks2_data)
    write_blob("pre-processed", f"{set_type}/{year}/ks2.parquet", ks2.to_parquet())

    return ks2


def pre_process_ks4(set_type, year) -> pd.DataFrame:
    logger.info("Processing KS4 Data")
    ks4_data = get_blob(raw_container, f"{set_type}/{year}/ks4.xlsx")
    ks4 = prepare_ks4_data(ks4_data)
    write_blob("pre-processed", f"{set_type}/{year}/ks4.parquet", ks4.to_parquet())

    return ks4


def pre_process_academy_ar(set_type, year) -> (pd.DataFrame, pd.DataFrame):
    logger.info("Processing Academy AR Data")
    academy_ar_data = get_blob(raw_container, f"{set_type}/{year}/academy_ar.xlsx")
    aar = prepare_aar_data(academy_ar_data)

    write_blob(
        "pre-processed",
        f"{set_type}/{year}/aar.parquet",
        aar.to_parquet(),
    )

    return aar


def pre_process_schools(set_type, year) -> pd.DataFrame:
    logger.info("Processing Schools Data")
    gias_data = get_blob(
        raw_container, f"{set_type}/{year}/gias.csv", encoding="cp1252"
    )

    schools = prepare_schools_data(gias_data)

    write_blob(
        "pre-processed",
        f"{set_type}/{year}/schools.parquet",
        schools.to_parquet(),
    )

    return schools


def pre_process_academies_data(set_type, year, data_ref) -> pd.DataFrame:
    logger.info("Building Academy Set")
    schools, census, sen, cdc, aar, ks2, ks4 = data_ref

    academies_data = get_blob(
        raw_container, f"{set_type}/{year}/academy_master_list.csv", encoding="utf-8"
    )

    academies = build_academy_data(
        academies_data, year, schools, census, sen, cdc, aar, ks2, ks4
    )

    write_blob(
        "pre-processed",
        f"{set_type}/{year}/academies.parquet",
        academies.to_parquet(),
    )

    return academies


def pre_process_maintained_schools_data(set_type, year, data_ref) -> pd.DataFrame:
    logger.info("Building Maintained School Set")
    schools, census, sen, cdc, aar, ks2, ks4 = data_ref

    maintained_schools_data = get_blob(
        raw_container,
        f"{set_type}/{year}/maintained_schools_master_list.csv",
        encoding="utf-8",
    )

    links_data = get_blob(
        raw_container,
        f"{set_type}/{year}/gias_all_links.csv",
        encoding="utf-8"
    )

    maintained_schools = build_maintained_school_data(
        maintained_schools_data, links_data, year, schools, census, sen, cdc, ks2, ks4
    )

    write_blob(
        "pre-processed",
        f"{set_type}/{year}/maintained_schools.parquet",
        maintained_schools.to_parquet(),
    )

    return maintained_schools


def pre_process_federations(set_type, year, data_ref):
    logger.info("Building Federations Set")
    academies, maintained_schools = data_ref

    gias_all_links_data = get_blob(
        raw_container,
        f"{set_type}/{year}/gias_all_links.csv",
        encoding="unicode-escape",
    )

    (hard_federations, soft_federations) = build_federations_data(
        gias_all_links_data, maintained_schools
    )

    write_blob(
        "pre-processed",
        f"{set_type}/{year}/hard_federations.parquet",
        hard_federations.to_parquet(),
    )

    write_blob(
        "pre-processed",
        f"{set_type}/{year}/soft_federations.parquet",
        soft_federations.to_parquet(),
    )


def pre_process_all_schools(set_type, year, data_ref):
    logger.info("Building All schools Set")
    academies, maintained_schools = data_ref

    all_schools = pd.concat([academies, maintained_schools])
    write_blob(
        "pre-processed",
        f"{set_type}/{year}/all_schools.parquet",
        all_schools.to_parquet(),
    )

    insert_school(set_type, year, all_schools)


def pre_process_data(worker_client, set_type, year):
    start_time = time.time()
    logger.info("Pre-processing data")

    cdc, census, sen, ks2, ks4, aar, schools = worker_client.gather(
        [
            worker_client.submit(pre_process_cdc, set_type, year),
            worker_client.submit(pre_process_census, set_type, year),
            worker_client.submit(pre_process_sen, set_type, year),
            worker_client.submit(pre_process_ks2, set_type, year),
            worker_client.submit(pre_process_ks4, set_type, year),
            worker_client.submit(pre_process_academy_ar, set_type, year),
            worker_client.submit(pre_process_schools, set_type, year),
        ]
    )

    data_ref = worker_client.scatter((schools, census, sen, cdc, aar, ks2, ks4))

    academies, maintained_schools = worker_client.gather(
        [
            worker_client.submit(pre_process_academies_data, set_type, year, data_ref),
            worker_client.submit(
                pre_process_maintained_schools_data, set_type, year, data_ref
            ),
        ]
    )

    data2_ref = worker_client.scatter((academies, maintained_schools))

    worker_client.gather(
        [
            worker_client.submit(pre_process_federations, set_type, year, data2_ref),
            worker_client.submit(pre_process_all_schools, set_type, year, data2_ref),
        ]
    )

    time_taken = time.time() - start_time
    logger.info(f"Pre-processing data done in {time_taken} seconds")

    return time_taken


def compute_comparator_set_for(data_type, mix_type, set_type, year, data):
    st = time.time()
    logger.info(f"Computing {data_type} set")
    result = compute_comparator_set(data)
    logger.info(f"Computing {data_type} set. Done in {time.time() - st:.2f} seconds")

    write_blob(
        "comparator-sets",
        f"{set_type}/{year}/{data_type}.parquet",
        result.to_parquet(),
    )

    insert_comparator_set(set_type, mix_type, year, result)


def compute_comparator_sets(set_type, year):
    start_time = time.time()
    logger.info("Computing comparator sets")

    academies = prepare_data(
        pd.read_parquet(
            get_blob("pre-processed", f"{set_type}/{year}/academies.parquet")
        )
    )

    ms = prepare_data(
        pd.read_parquet(
            get_blob("pre-processed", f"{set_type}/{year}/maintained_schools.parquet")
        )
    )

    all_schools = prepare_data(
        pd.read_parquet(
            get_blob("pre-processed", f"{set_type}/{year}/all_schools.parquet")
        )
    )

    compute_comparator_set_for(
        "academy_comparators", "unmixed", set_type, year, academies
    )
    compute_comparator_set_for(
        "maintained_schools_comparators", "unmixed", set_type, year, ms
    )
    compute_comparator_set_for(
        "mixed_comparators", "mixed", set_type, year, all_schools
    )

    write_blob(
        "comparator-sets",
        f"{set_type}/{year}/academies.parquet",
        academies.to_parquet(),
    )

    write_blob(
        "comparator-sets",
        f"{set_type}/{year}/maintained_schools.parquet",
        ms.to_parquet(),
    )

    write_blob(
        "comparator-sets",
        f"{set_type}/{year}/all_schools.parquet",
        all_schools.to_parquet(),
    )

    time_taken = time.time() - start_time
    logger.info(f"Computing comparators sets done in {time_taken} seconds")

    return time_taken


def compute_rag_for(data_type, set_type, year, data, comparators):
    st = time.time()
    logger.info(f"Computing {data_type} RAG")
    df = pd.DataFrame(compute_rag(data, comparators))

    logger.info(f"Computing {data_type} RAG. Done in {time.time() - st:.2f} seconds")

    write_blob(
        "metric-rag",
        f"{set_type}/{year}/{data_type}.parquet",
        df.to_parquet(),
    )

    insert_metric_rag(set_type, year, df)


def run_compute_rag(set_type, year):
    start_time = time.time()

    ms_data = pd.read_parquet(
        get_blob("comparator-sets", f"{set_type}/{year}/maintained_schools.parquet")
    )
    ms_comparators = pd.read_parquet(
        get_blob(
            "comparator-sets",
            f"{set_type}/{year}/maintained_schools_comparators.parquet",
        )
    )
    compute_rag_for("maintained_schools", set_type, year, ms_data, ms_comparators)

    academy_data = pd.read_parquet(
        get_blob("comparator-sets", f"{set_type}/{year}/academies.parquet")
    )
    academy_comparators = pd.read_parquet(
        get_blob("comparator-sets", f"{set_type}/{year}/academy_comparators.parquet")
    )
    compute_rag_for("academies", set_type, year, academy_data, academy_comparators)

    mixed_data = pd.read_parquet(
        get_blob("comparator-sets", f"{set_type}/{year}/all_schools.parquet")
    )
    mixed_comparators = pd.read_parquet(
        get_blob("comparator-sets", f"{set_type}/{year}/mixed_comparators.parquet")
    )
    compute_rag_for("mixed", set_type, year, mixed_data, mixed_comparators)

    time_taken = time.time() - start_time
    logger.info(f"Computing RAG done in {time_taken} seconds")

    return time_taken


def handle_msg(worker_client, msg, worker_queue, complete_queue):
    msg_payload = json.loads(msg.content)
    try:
        msg_payload["pre_process_duration"] = pre_process_data(
            worker_client, msg_payload["type"], msg_payload["year"]
        )

        msg_payload["comparator_set_duration"] = compute_comparator_sets(
            msg_payload["type"], msg_payload["year"]
        )

        msg_payload["rag_duration"] = run_compute_rag(
            msg_payload["type"], msg_payload["year"]
        )

        msg_payload["success"] = True
    except Exception as error:
        logger.exception("An exception occurred:", type(error).__name__, "–", error)
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
        logger.exception("An exception occurred:", type(error).__name__, "–", error)
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
        logger.exception("An exception occurred:", type(error).__name__, "–", error)
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
