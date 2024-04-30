import gc
import json
import logging
import os
import pickle
import sys
import time
from contextlib import suppress
from io import BytesIO, StringIO

import pandas as pd
from azure.core.exceptions import ResourceExistsError, ResourceNotFoundError
from azure.storage.blob import BlobClient, BlobServiceClient
from azure.storage.queue import QueueClient, QueueServiceClient
from dotenv import load_dotenv

import comparator_sets as comparators
import pre_processing as pre_processing

# reset CPU affinity so that ALL cpus are used.
os.system("taskset -p 0xff %d" % os.getpid())

load_dotenv()

logger = logging.getLogger("fbit-data-pipeline")
logger.setLevel(logging.INFO)

azure_logger = logging.getLogger("azure")
azure_logger.setLevel(logging.WARNING)

logging.basicConfig(stream=sys.stdout, level=logging.INFO)


conn_str = os.getenv("STORAGE_CONNECTION_STRING")
worker_queue_name = os.getenv("WORKER_QUEUE_NAME")
complete_queue_name = os.getenv("COMPLETE_QUEUE_NAME")
raw_container = os.getenv("RAW_DATA_CONTAINER")


def connect_to_queue(queue_name) -> QueueClient:
    if not conn_str:
        raise Exception("Queue connection string not provided!")

    if not queue_name:
        raise Exception("Queue name not provided!")

    service_client = QueueServiceClient.from_connection_string(conn_str=conn_str)
    queue = service_client.get_queue_client(queue_name)
    with suppress(ResourceExistsError):
        queue.create_queue()

    return queue


def create_container(container_name):
    service_client = BlobServiceClient.from_connection_string(conn_str=conn_str)
    with suppress(ResourceExistsError):
        service_client.create_container(container_name)

    return service_client.get_container_client(container_name)


def get_blob(container_name, blob_name, encoding=None):
    service_client = BlobServiceClient.from_connection_string(conn_str=conn_str)
    container_client = service_client.get_container_client(container_name)
    blob = container_client.get_blob_client(blob_name)
    if encoding is None:
        return BytesIO(blob.download_blob(encoding=encoding).readall())

    return StringIO(blob.download_blob(encoding=encoding).readall())


def write_blob(container_name, blob_name, data):
    container_client = create_container(container_name)
    container_client.upload_blob(blob_name, data, encoding="utf-8", overwrite=True)


def pre_process_data(set_type, year):
    start_time = time.time()
    logger.info("Pre-processing data")

    logger.info("Processing CDC Data")
    cdc_data = get_blob(raw_container, f"{set_type}/{year}/cdc.csv", encoding="utf-8")
    cdc = pre_processing.prepare_cdc_data(cdc_data, year)
    write_blob(
        "pre-processed", f"{set_type}/{year}/cdc.csv", cdc.to_csv(encoding="utf-8")
    )

    logger.info("Processing Census Data")
    workforce_census_data = get_blob(
        raw_container, f"{set_type}/{year}/census_workforce.xlsx"
    )
    pupil_census_data = get_blob(
        raw_container, f"{set_type}/{year}/census_pupils.csv", encoding="utf-8"
    )
    census = pre_processing.prepare_census_data(
        workforce_census_data, pupil_census_data
    )
    write_blob(
        "pre-processed",
        f"{set_type}/{year}/census.csv",
        census.to_csv(encoding="utf-8"),
    )

    logger.info("Processing SEN Data")
    sen_data = get_blob(raw_container, f"{set_type}/{year}/sen.csv", encoding="cp1252")
    sen = pre_processing.prepare_sen_data(sen_data)
    write_blob(
        "pre-processed", f"{set_type}/{year}/sen.csv", sen.to_csv(encoding="utf-8")
    )

    logger.info("Processing KS2 Data")
    ks2_data = get_blob(raw_container, f"{set_type}/{year}/ks2.xlsx")
    ks2 = pre_processing.prepare_ks2_data(ks2_data)
    write_blob(
        "pre-processed", f"{set_type}/{year}/ks2.csv", ks2.to_csv(encoding="utf-8")
    )

    logger.info("Processing KS4 Data")
    ks4_data = get_blob(raw_container, f"{set_type}/{year}/ks4.xlsx")
    ks4 = pre_processing.prepare_ks4_data(ks4_data)
    write_blob(
        "pre-processed", f"{set_type}/{year}/ks4.csv", ks4.to_csv(encoding="utf-8")
    )

    logger.info("Processing Academy AR Data")
    academy_ar_data = get_blob(raw_container, f"{set_type}/{year}/academy_ar.xlsx")
    (trust_ar, academy_ar) = pre_processing.prepare_aar_data(academy_ar_data)
    write_blob(
        "pre-processed",
        f"{set_type}/{year}/trust_ar.csv",
        trust_ar.to_csv(encoding="utf-8"),
    )
    write_blob(
        "pre-processed",
        f"{set_type}/{year}/academy_ar.csv",
        academy_ar.to_csv(encoding="utf-8"),
    )

    logger.info("Processing Schools Data")
    gias_data = get_blob(
        raw_container, f"{set_type}/{year}/gias.csv", encoding="cp1252"
    )
    gias_links_data = get_blob(
        raw_container, f"{set_type}/{year}/gias_links.csv", encoding="cp1252"
    )
    schools = pre_processing.prepare_schools_data(gias_data, gias_links_data)
    write_blob(
        "pre-processed",
        f"{set_type}/{year}/schools.csv",
        schools.to_csv(encoding="utf-8"),
    )

    del gias_data
    del gias_links_data

    logger.info("Building Academy Set")
    academies_data = get_blob(
        raw_container, f"{set_type}/{year}/academy_master_list.csv", encoding="utf-8"
    )
    academies = pre_processing.build_academy_data(
        academies_data, year, schools, census, sen, cdc, academy_ar, trust_ar, ks2, ks4
    )
    write_blob(
        "pre-processed",
        f"{set_type}/{year}/academies.csv",
        academies.to_csv(encoding="utf-8"),
    )

    del academies_data
    del academies

    logger.info("Building Maintained School Set")
    maintained_schools_data = get_blob(
        raw_container,
        f"{set_type}/{year}/maintained_schools_master_list.csv",
        encoding="utf-8",
    )
    maintained_schools = pre_processing.build_maintained_school_data(
        maintained_schools_data, year, schools, census, sen, cdc, ks2, ks4
    )
    write_blob(
        "pre-processed",
        f"{set_type}/{year}/maintained_schools.csv",
        maintained_schools.to_csv(encoding="utf-8"),
    )

    del maintained_schools_data

    logger.info("Building Federations Set")
    gias_all_links_data = get_blob(
        raw_container,
        f"{set_type}/{year}/gias_all_links.csv",
        encoding="unicode-escape",
    )
    (hard_federations, soft_federations) = pre_processing.build_federations_data(
        gias_all_links_data, maintained_schools
    )
    write_blob(
        "pre-processed",
        f"{set_type}/{year}/hard_federations.csv",
        hard_federations.to_csv(encoding="utf-8"),
    )
    write_blob(
        "pre-processed",
        f"{set_type}/{year}/soft_federations.csv",
        soft_federations.to_csv(encoding="utf-8"),
    )

    del gias_all_links_data
    del hard_federations
    del soft_federations

    time_taken = time.time() - start_time
    logger.info(f"Pre-processing data done in {time_taken} seconds")
    gc.collect()
    return time_taken


def compute_comparator_sets(set_type, year):
    start_time = time.time()
    logger.info("Computing comparator sets")

    pp_academy_data = get_blob(
        "pre-processed", f"{set_type}/{year}/academies.csv", encoding="utf-8"
    )
    pp_maintained_schools_data = get_blob(
        "pre-processed", f"{set_type}/{year}/maintained_schools.csv", encoding="utf-8"
    )
    academy_data = comparators.prepare_data(pd.read_csv(pp_academy_data))
    ms_data = comparators.prepare_data(
        pd.read_csv(pp_maintained_schools_data, low_memory=False)
    )

    logger.info("Writing all schools data")
    all_schools = pd.concat([academy_data, ms_data])
    write_blob(
        "comparator-sets",
        f"{set_type}/{year}/schools.pkl",
        pickle.dumps(all_schools, protocol=pickle.HIGHEST_PROTOCOL),
    )

    logger.info("Computing maintained schools pupil comparator set")
    ms_pupil_comparators = comparators.compute_comparator_matrix(
        ms_data, comparators.compute_pupils_comparator
    )
    write_blob(
        "comparator-sets",
        f"{set_type}/{year}/ms_pupil.pkl",
        pickle.dumps(ms_pupil_comparators, protocol=pickle.HIGHEST_PROTOCOL),
    )

    del ms_pupil_comparators

    logger.info("Computing maintained schools building comparator set")
    ms_building_comparators = comparators.compute_comparator_matrix(
        ms_data, comparators.compute_buildings_comparator
    )
    write_blob(
        "comparator-sets",
        f"{set_type}/{year}/ms_building.pkl",
        pickle.dumps(ms_building_comparators, protocol=pickle.HIGHEST_PROTOCOL),
    )

    del ms_building_comparators

    logger.info("Computing academy pupil comparator set")
    academy_pupil_comparators = comparators.compute_comparator_matrix(
        academy_data, comparators.compute_pupils_comparator
    )
    write_blob(
        "comparator-sets",
        f"{set_type}/{year}/academy_pupil.pkl",
        pickle.dumps(academy_pupil_comparators, protocol=pickle.HIGHEST_PROTOCOL),
    )

    del academy_pupil_comparators

    logger.info("Computing academy building comparator set")
    academy_building_comparators = comparators.compute_comparator_matrix(
        academy_data, comparators.compute_buildings_comparator
    )
    write_blob(
        "comparator-sets",
        f"{set_type}/{year}/academy_building.pkl",
        pickle.dumps(academy_building_comparators, protocol=pickle.HIGHEST_PROTOCOL),
    )

    del academy_building_comparators

    logger.info("Computing all pupil comparator set")
    pupil_comparators = comparators.compute_comparator_matrix(
        all_schools, comparators.compute_pupils_comparator
    )
    write_blob(
        "comparator-sets",
        f"{set_type}/{year}/all_pupil.pkl",
        pickle.dumps(pupil_comparators, protocol=pickle.HIGHEST_PROTOCOL),
    )

    del pupil_comparators

    logger.info("Computing all building comparator set")
    building_comparators = comparators.compute_comparator_matrix(
        all_schools, comparators.compute_buildings_comparator
    )
    write_blob(
        "comparator-sets",
        f"{set_type}/{year}/all_building.pkl",
        pickle.dumps(building_comparators, protocol=pickle.HIGHEST_PROTOCOL),
    )

    del building_comparators

    time_taken = time.time() - start_time
    logger.info(f"Computing comparators sets done in {time_taken} seconds")
    return time_taken


def receive_messages():
    try:
        worker_queue = connect_to_queue(worker_queue_name)
        complete_queue = connect_to_queue(complete_queue_name)
        msg = worker_queue.receive_message(visibility_timeout=300)
        if msg is not None:
            logger.info(f"received message {msg.content}")
            msg_payload = json.loads(msg.content)

            try:
                msg_payload["pre_process_duration"] = pre_process_data(
                    msg_payload["type"], msg_payload["year"]
                )
                msg_payload["comparator_set_duration"] = compute_comparator_sets(
                    msg_payload["type"], msg_payload["year"]
                )
                msg_payload["success"] = True
            except Exception as error:
                logger.exception(
                    "An exception occurred:", type(error).__name__, "–", error
                )
                msg_payload["success"] = False
                msg_payload["error"] = str(error)
            finally:
                with suppress(ResourceNotFoundError):
                    worker_queue.delete_message(msg)

                complete_queue.send_message(json.dumps(msg_payload))

            exit(0) if msg_payload["success"] else exit(1)
        else:
            logger.info("no messages received")
            exit(0)
    except Exception:
        logger.exception("An exception occurred")
        exit(-1)


if __name__ == "__main__":
    receive_messages()
