import json
import logging
import os
import pickle
import sys
import time
from contextlib import suppress

import pandas as pd
from azure.core.exceptions import ResourceNotFoundError
from dotenv import load_dotenv
from dask.distributed import Client, wait

load_dotenv()

from src.pipeline.comparator_sets import (
    compute_buildings_comparator,
    compute_comparator_matrix,
    compute_pupils_comparator,
    prepare_data,
    get_comparator_set_by,
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
from src.pipeline.rag import compute_comparator_set_rag
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
    cdc_data = get_blob(
        raw_container, f"{set_type}/{year}/cdc.csv", encoding="utf-8"
    )
    cdc = prepare_cdc_data(cdc_data, year)
    write_blob(
        "pre-processed", f"{set_type}/{year}/cdc.parquet", cdc.to_parquet()
    )

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
    sen_data = get_blob(
        raw_container, f"{set_type}/{year}/sen.csv", encoding="cp1252"
    )
    sen = prepare_sen_data(sen_data)
    write_blob(
        "pre-processed", f"{set_type}/{year}/sen.parquet", sen.to_parquet()
    )

    return sen


def pre_process_ks2(set_type, year) -> pd.DataFrame:
    logger.info("Processing KS2 Data")
    ks2_data = get_blob(raw_container, f"{set_type}/{year}/ks2.xlsx")
    ks2 = prepare_ks2_data(ks2_data)
    write_blob(
        "pre-processed", f"{set_type}/{year}/ks2.parquet", ks2.to_parquet()
    )

    return ks2


def pre_process_ks4(set_type, year) -> pd.DataFrame:
    logger.info("Processing KS4 Data")
    ks4_data = get_blob(raw_container, f"{set_type}/{year}/ks4.xlsx")
    ks4 = prepare_ks4_data(ks4_data)
    write_blob(
        "pre-processed", f"{set_type}/{year}/ks4.parquet", ks4.to_parquet()
    )

    return ks4


def pre_process_academy_ar(set_type, year) -> (pd.DataFrame, pd.DataFrame):
    logger.info("Processing Academy AR Data")
    academy_ar_data = get_blob(
        raw_container, f"{set_type}/{year}/academy_ar.xlsx"
    )
    (trust_ar, academy_ar) = prepare_aar_data(academy_ar_data)
    write_blob(
        "pre-processed",
        f"{set_type}/{year}/trust_ar.parquet",
        trust_ar.to_parquet(),
    )
    write_blob(
        "pre-processed",
        f"{set_type}/{year}/academy_ar.parquet",
        academy_ar.to_parquet(),
    )

    return trust_ar, academy_ar


def pre_process_schools(set_type, year) -> pd.DataFrame:
    logger.info("Processing Schools Data")
    gias_data = get_blob(
        raw_container, f"{set_type}/{year}/gias.csv", encoding="cp1252"
    )
    gias_links_data = get_blob(
        raw_container, f"{set_type}/{year}/gias_links.csv", encoding="cp1252"
    )
    schools = prepare_schools_data(gias_data, gias_links_data)
    write_blob(
        "pre-processed",
        f"{set_type}/{year}/schools.parquet",
        schools.to_parquet(),
    )

    return schools


def pre_process_academies_data(set_type, year, data_ref) -> pd.DataFrame:
    logger.info("Building Academy Set")
    schools, census, sen, cdc, academy_ar, trust_ar, ks2, ks4 = data_ref

    academies_data = get_blob(
        raw_container, f"{set_type}/{year}/academy_master_list.csv", encoding="utf-8"
    )

    academies = build_academy_data(
        academies_data, year, schools, census, sen, cdc, academy_ar, trust_ar, ks2, ks4
    )

    write_blob(
        "pre-processed",
        f"{set_type}/{year}/academies.parquet",
        academies.to_parquet(),
    )

    return academies


def pre_process_maintained_schools_data(set_type, year, data_ref) -> pd.DataFrame:
    logger.info("Building Maintained School Set")
    schools, census, sen, cdc, academy_ar, trust_ar, ks2, ks4 = data_ref

    maintained_schools_data = get_blob(
        raw_container,
        f"{set_type}/{year}/maintained_schools_master_list.csv",
        encoding="utf-8",
    )
    maintained_schools = build_maintained_school_data(
        maintained_schools_data, year, schools, census, sen, cdc, ks2, ks4
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


def pre_process_data(worker_client, set_type, year):
    start_time = time.time()
    logger.info("Pre-processing data")

    cdc, census, sen, ks2, ks4, ar, schools = worker_client.gather([
        worker_client.submit(pre_process_cdc, set_type, year),
        worker_client.submit(pre_process_census, set_type, year),
        worker_client.submit(pre_process_sen, set_type, year),
        worker_client.submit(pre_process_ks2, set_type, year),
        worker_client.submit(pre_process_ks4, set_type, year),
        worker_client.submit(pre_process_academy_ar, set_type, year),
        worker_client.submit(pre_process_schools, set_type, year)
    ])

    (academy_ar, trust_ar) = ar

    data_ref = worker_client.scatter((schools, census, sen, cdc, academy_ar, trust_ar, ks2, ks4))

    academies, maintained_schools = worker_client.gather([
        worker_client.submit(pre_process_academies_data, set_type, year, data_ref),
        worker_client.submit(pre_process_maintained_schools_data, set_type, year, data_ref)
    ])

    data2_ref = worker_client.scatter((academies, maintained_schools))

    worker_client.gather([
        worker_client.submit(pre_process_federations, set_type, year, data2_ref),
        worker_client.submit(pre_process_all_schools, set_type, year, data2_ref)
    ])

    time_taken = time.time() - start_time
    logger.info(f"Pre-processing data done in {time_taken} seconds")

    return time_taken


def compute_comparator(arg):
    set_type, year, data, name = arg
    logger.info(f"Computing {name} comparator set")

    func_map = {
        "academy_pupil": compute_pupils_comparator,
        "academy_building": compute_buildings_comparator,
        "maintained_school_pupil": compute_pupils_comparator,
        "maintained_school_building": compute_buildings_comparator,
        "all_pupil": compute_pupils_comparator,
        "all_building": compute_buildings_comparator
    }

    building_comparators = compute_comparator_matrix(data, func_map[name])
    write_blob(
        "comparator-sets",
        f"{set_type}/{year}/{name}.parquet",
        building_comparators.to_parquet(),
    )

    del building_comparators
    logger.info(f'Done. Computing {name} set')


def compute_comparator_sets(worker_client, set_type, year):
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

    academy_ref = worker_client.scatter(academies)
    ms_ref = worker_client.scatter(ms)
    all_ref = worker_client.scatter(all_schools)

    pupil = client.gather([
        client.submit(compute_comparator, (set_type, year, academy_ref, "academy_pupil")),
        client.submit(compute_comparator, (set_type, year, ms_ref, "maintained_school_pupil")),
        client.submit(compute_comparator, (set_type, year, all_ref, "all_pupil")),
    ])

    wait(pupil)

    building = client.gather([
        client.submit(compute_comparator, (set_type, year, academy_ref, "academy_building")),
        client.submit(compute_comparator, (set_type, year, ms_ref, "maintained_school_building")),
        client.submit(compute_comparator, (set_type, year, all_ref, "all_building"))
    ])

    wait(building)

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


def compute_rag(worker_client, set_type, year):
    start_time = time.time()
    logger.info("Computing RAG")

    rag_settings = {
        "academies": [("building", "academy_building"), ("pupil", "academies_pupil")],
        "maintained_schools": [
            ("building", "maintained_school_building"),
            ("pupil", "maintained_school_pupil"),
        ],
        "all_schools": [
            ("mixed_building", "all_building"),
            ("mixed_pupil", "all_pupil"),
        ],
    }

    for rag_file in rag_settings.keys():
        schools = pd.read_parquet(get_blob(
            "comparator-sets",
            f"{set_type}/{year}/{rag_file}.parquet",
            encoding="utf-8"
        )).reset_index()

        for comparator_type, comparator_file in rag_settings[rag_file]:
            st = time.time()
            logger.info(f"Computing {comparator_type} RAG")
            comparator_set = pd.read_parquet(get_blob(
                "comparator-sets",
                f"{set_type}/{year}/{comparator_file}.parquet",
                encoding="utf-8").read()
            ).to_dict()

            i = 0
            for index, row in schools.iterrows():
                s = time.time()
                urn = row["URN"]
                comparators = get_comparator_set_by(
                    lambda s: s["URN"] == urn, schools, comparator_set
                ).set_index("URN")
                result = compute_comparator_set_rag(comparators)
                write_blob("metric-rag",
                           f'{set_type}/{year}/{urn}/{comparator_type}.json',
                           json.dumps(result))

                if i % 10 == 0:
                    logger.info(f"Computing {comparator_type} RAG done in {time.time() - s} seconds")
                    s = time.time()
                i += 1


            logger.info(f"Computing {comparator_type} RAG done in {time.time() - st} seconds")

    time_taken = time.time() - start_time
    logger.info(f"Computing RAG done in {time_taken} seconds")
    return time_taken


def handle_msg(worker_client, msg, worker_queue, complete_queue):
    msg_payload = json.loads(msg.content)
    try:
        msg_payload["pre_process_duration"] = pre_process_data(
            worker_client,
            msg_payload["type"],
            msg_payload["year"]
        )

        msg_payload["comparator_set_duration"] = compute_comparator_sets(
            worker_client,
            msg_payload["type"],
            msg_payload["year"]
        )

        msg_payload["rag_duration"] = compute_rag(
            worker_client,
            msg_payload["type"],
            msg_payload["year"]
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
    except Exception:
        logger.exception("An exception occurred")
        exit(-1)


async def receive_messages(worker_client):
    try:
        with blob_service_client, queue_service_client:
            worker_queue = connect_to_queue(worker_queue_name)
            complete_queue = connect_to_queue(complete_queue_name)
            with worker_queue, complete_queue:
                while True:
                    msg = worker_queue.receive_message(visibility_timeout=300)
                    if msg is not None:
                        logger.info(f"received message {msg.content}")
                        msg = handle_msg(worker_client, msg, worker_queue, complete_queue)
                        logger.info(f"processed msg response: {msg}")
                    else:
                        time.sleep(1)
    except Exception:
        logger.exception("An exception occurred")
        exit(-1)


if __name__ == "__main__":
    with Client(n_workers=8, threads_per_worker=1, memory_limit='16GB') as client:
        if os.getenv("ENV") == "dev":
            receive_messages(client)
        else:
            receive_one_message(client)
