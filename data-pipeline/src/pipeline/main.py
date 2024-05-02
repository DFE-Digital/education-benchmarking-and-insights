import asyncio
import gc
import json
import logging
import pickle
import sys
import time
from contextlib import suppress

import pandas as pd
import psutil
from azure.core.exceptions import ResourceNotFoundError

from dotenv import load_dotenv

load_dotenv()


import comparator_sets as comparators
import pre_processing as pre_processing
from storage import (
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

logging.basicConfig(stream=sys.stdout, level=logging.INFO)


async def pre_process_cdc(set_type, year):
    logger.info("Processing CDC Data")
    cdc_data = await get_blob(
        raw_container, f"{set_type}/{year}/cdc.csv", encoding="utf-8"
    )
    cdc = pre_processing.prepare_cdc_data(cdc_data, year)
    await write_blob(
        "pre-processed", f"{set_type}/{year}/cdc.csv", cdc.to_csv(encoding="utf-8")
    )
    del cdc_data
    return cdc


async def pre_process_census(set_type, year):
    logger.info("Processing Census Data")
    workforce_census_data = await get_blob(
        raw_container, f"{set_type}/{year}/census_workforce.xlsx"
    )
    pupil_census_data = await get_blob(
        raw_container, f"{set_type}/{year}/census_pupils.csv", encoding="utf-8"
    )
    census = pre_processing.prepare_census_data(
        workforce_census_data, pupil_census_data
    )
    await write_blob(
        "pre-processed",
        f"{set_type}/{year}/census.csv",
        census.to_csv(encoding="utf-8"),
    )

    del workforce_census_data
    del pupil_census_data
    return census


async def pre_process_sen(set_type, year):
    logger.info("Processing SEN Data")
    sen_data = await get_blob(
        raw_container, f"{set_type}/{year}/sen.csv", encoding="cp1252"
    )
    sen = pre_processing.prepare_sen_data(sen_data)
    await write_blob(
        "pre-processed", f"{set_type}/{year}/sen.csv", sen.to_csv(encoding="utf-8")
    )
    del sen_data
    return sen


async def pre_process_ks2(set_type, year):
    logger.info("Processing KS2 Data")
    ks2_data = await get_blob(raw_container, f"{set_type}/{year}/ks2.xlsx")
    ks2 = pre_processing.prepare_ks2_data(ks2_data)
    await write_blob(
        "pre-processed", f"{set_type}/{year}/ks2.csv", ks2.to_csv(encoding="utf-8")
    )
    del ks2_data
    return ks2


async def pre_process_ks4(set_type, year):
    logger.info("Processing KS4 Data")
    ks4_data = await get_blob(raw_container, f"{set_type}/{year}/ks4.xlsx")
    ks4 = pre_processing.prepare_ks4_data(ks4_data)
    await write_blob(
        "pre-processed", f"{set_type}/{year}/ks4.csv", ks4.to_csv(encoding="utf-8")
    )
    del ks4_data
    return ks4


async def pre_process_academy_ar(set_type, year):
    logger.info("Processing Academy AR Data")
    academy_ar_data = await get_blob(
        raw_container, f"{set_type}/{year}/academy_ar.xlsx"
    )
    (trust_ar, academy_ar) = pre_processing.prepare_aar_data(academy_ar_data)
    await write_blob(
        "pre-processed",
        f"{set_type}/{year}/trust_ar.csv",
        trust_ar.to_csv(encoding="utf-8"),
    )
    await write_blob(
        "pre-processed",
        f"{set_type}/{year}/academy_ar.csv",
        academy_ar.to_csv(encoding="utf-8"),
    )

    del academy_ar_data
    return trust_ar, academy_ar


async def pre_process_schools(set_type, year):
    logger.info("Processing Schools Data")
    gias_data = await get_blob(
        raw_container, f"{set_type}/{year}/gias.csv", encoding="cp1252"
    )
    gias_links_data = await get_blob(
        raw_container, f"{set_type}/{year}/gias_links.csv", encoding="cp1252"
    )
    schools = pre_processing.prepare_schools_data(gias_data, gias_links_data)
    await write_blob(
        "pre-processed",
        f"{set_type}/{year}/schools.csv",
        schools.to_csv(encoding="utf-8"),
    )

    del gias_data
    del gias_links_data
    return schools


async def pre_process_data(set_type, year):
    start_time = time.time()
    logger.info("Pre-processing data")

    cdc = await pre_process_cdc(set_type, year)
    census = await pre_process_census(set_type, year)
    sen = await pre_process_sen(set_type, year)
    ks2 = await pre_process_ks2(set_type, year)
    ks4 = await pre_process_ks4(set_type, year)
    (trust_ar, academy_ar) = await pre_process_academy_ar(set_type, year)
    schools = await pre_process_schools(set_type, year)

    logger.info("Building Academy Set")
    academies_data = await get_blob(
        raw_container, f"{set_type}/{year}/academy_master_list.csv", encoding="utf-8"
    )
    academies = pre_processing.build_academy_data(
        academies_data, year, schools, census, sen, cdc, academy_ar, trust_ar, ks2, ks4
    )
    await write_blob(
        "pre-processed",
        f"{set_type}/{year}/academies.csv",
        academies.to_csv(encoding="utf-8"),
    )

    del academies_data
    del academies

    logger.info("Building Maintained School Set")
    maintained_schools_data = await get_blob(
        raw_container,
        f"{set_type}/{year}/maintained_schools_master_list.csv",
        encoding="utf-8",
    )
    maintained_schools = pre_processing.build_maintained_school_data(
        maintained_schools_data, year, schools, census, sen, cdc, ks2, ks4
    )
    await write_blob(
        "pre-processed",
        f"{set_type}/{year}/maintained_schools.csv",
        maintained_schools.to_csv(encoding="utf-8"),
    )

    del maintained_schools_data

    logger.info("Building Federations Set")
    gias_all_links_data = await get_blob(
        raw_container,
        f"{set_type}/{year}/gias_all_links.csv",
        encoding="unicode-escape",
    )
    (hard_federations, soft_federations) = pre_processing.build_federations_data(
        gias_all_links_data, maintained_schools
    )
    await write_blob(
        "pre-processed",
        f"{set_type}/{year}/hard_federations.csv",
        hard_federations.to_csv(encoding="utf-8"),
    )
    await write_blob(
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


async def ms_pupils_comparator(set_type, year, data):
    logger.info("Computing maintained schools pupil comparator set")
    ms_pupil_comparators = comparators.compute_comparator_matrix(
        data, comparators.compute_pupils_comparator
    )
    await write_blob(
        "comparator-sets",
        f"{set_type}/{year}/ms_pupil.pkl",
        pickle.dumps(ms_pupil_comparators, protocol=pickle.HIGHEST_PROTOCOL),
    )

    del ms_pupil_comparators
    logger.info("Done. Computing maintained schools pupil comparator set")


async def ms_buildings_comparator(set_type, year, data):
    logger.info("Computing maintained schools building comparator set")
    ms_building_comparators = comparators.compute_comparator_matrix(
        data, comparators.compute_buildings_comparator
    )
    await write_blob(
        "comparator-sets",
        f"{set_type}/{year}/ms_building.pkl",
        pickle.dumps(ms_building_comparators, protocol=pickle.HIGHEST_PROTOCOL),
    )

    del ms_building_comparators
    logger.info("Done. Computing maintained schools building comparator set")


async def academies_pupils_comparator(set_type, year, data):
    logger.info("Computing academy pupil comparator set")
    academy_pupil_comparators = comparators.compute_comparator_matrix(
        data, comparators.compute_pupils_comparator
    )
    await write_blob(
        "comparator-sets",
        f"{set_type}/{year}/academy_pupil.pkl",
        pickle.dumps(academy_pupil_comparators, protocol=pickle.HIGHEST_PROTOCOL),
    )
    del academy_pupil_comparators
    logger.info("Done. Computing academy pupil comparator set")


async def academies_buildings_comparator(set_type, year, data):
    logger.info("Computing academy building comparator set")
    academy_building_comparators = comparators.compute_comparator_matrix(
        data, comparators.compute_buildings_comparator
    )
    await write_blob(
        "comparator-sets",
        f"{set_type}/{year}/academy_building.pkl",
        pickle.dumps(academy_building_comparators, protocol=pickle.HIGHEST_PROTOCOL),
    )

    del academy_building_comparators
    logger.info("Done. Computing academy building comparator set")


async def all_pupils_comparator(set_type, year, data):
    logger.info("Computing all pupil comparator set")
    pupil_comparators = comparators.compute_comparator_matrix(
        data, comparators.compute_pupils_comparator
    )
    await write_blob(
        "comparator-sets",
        f"{set_type}/{year}/all_pupil.pkl",
        pickle.dumps(pupil_comparators, protocol=pickle.HIGHEST_PROTOCOL),
    )

    del pupil_comparators
    logger.info("Done. Computing all pupil comparator set")


async def all_buildings_comparator(set_type, year, data):
    logger.info("Computing all building comparator set")
    building_comparators = comparators.compute_comparator_matrix(
        data, comparators.compute_buildings_comparator
    )
    await write_blob(
        "comparator-sets",
        f"{set_type}/{year}/all_building.pkl",
        pickle.dumps(building_comparators, protocol=pickle.HIGHEST_PROTOCOL),
    )

    del building_comparators
    logger.info("Done. Computing all building comparator set")


async def maintained_schools_comparators(set_type, year):
    pp_maintained_schools_data = await get_blob(
        "pre-processed", f"{set_type}/{year}/maintained_schools.csv", encoding="utf-8"
    )
    ms_data = comparators.prepare_data(
        pd.read_csv(pp_maintained_schools_data, low_memory=False)
    )
    await ms_pupils_comparator(set_type, year, ms_data)
    await ms_buildings_comparator(set_type, year, ms_data)

    return ms_data


async def academies_comparators(set_type, year):
    pp_academy_data = await get_blob(
        "pre-processed", f"{set_type}/{year}/academies.csv", encoding="utf-8"
    )

    academy_data = comparators.prepare_data(pd.read_csv(pp_academy_data))

    await academies_pupils_comparator(set_type, year, academy_data),
    await academies_buildings_comparator(set_type, year, academy_data)
    return academy_data


async def compute_comparator_sets(set_type, year):
    start_time = time.time()
    logger.info("Computing comparator sets")

    ms_data = await maintained_schools_comparators(set_type, year)
    academy_data = await academies_comparators(set_type, year)

    logger.info("Writing all schools data")
    all_schools = pd.concat([ms_data, academy_data], copy=False)
    await write_blob(
        "comparator-sets",
        f"{set_type}/{year}/schools.pkl",
        pickle.dumps(all_schools, protocol=pickle.HIGHEST_PROTOCOL),
    )

    await all_pupils_comparator(set_type, year, all_schools)
    await all_buildings_comparator(set_type, year, all_schools)

    time_taken = time.time() - start_time
    logger.info(f"Computing comparators sets done in {time_taken} seconds")
    return time_taken


async def handle_msg(msg, worker_queue, complete_queue):
    msg_payload = json.loads(msg.content)
    try:
        msg_payload["pre_process_duration"] = await pre_process_data(
            msg_payload["type"], msg_payload["year"]
        )

        # normally bad practice but let's clean up as much as poss
        gc.collect()

        msg_payload["comparator_set_duration"] = await compute_comparator_sets(
            msg_payload["type"], msg_payload["year"]
        )
        msg_payload["success"] = True
    except Exception as error:
        logger.exception("An exception occurred:", type(error).__name__, "–", error)
        msg_payload["success"] = False
        msg_payload["error"] = str(error)
    finally:
        with suppress(ResourceNotFoundError):
            await worker_queue.delete_message(msg)

        await complete_queue.send_message(msg_payload)

    return msg


async def receive_one_message():
    try:
        async with blob_service_client, queue_service_client:
            worker_queue = await connect_to_queue(worker_queue_name)
            complete_queue = await connect_to_queue(complete_queue_name)
            msg = await worker_queue.receive_message(visibility_timeout=300)
            if msg is not None:
                logger.info(f"received message {msg.content}")
                msg_payload = json.loads(msg.content)
                msg = await handle_msg(msg_payload, worker_queue, complete_queue)
                exit(0) if msg["success"] else exit(1)
            else:
                logger.info("no messages received")
                exit(0)
    except Exception:
        logger.exception("An exception occurred")
        exit(-1)


async def receive_messages():
    try:
        async with blob_service_client, queue_service_client:
            worker_queue = await connect_to_queue(worker_queue_name)
            complete_queue = await connect_to_queue(complete_queue_name)

            while True:
                msg = await worker_queue.receive_message(visibility_timeout=300)
                if msg is not None:
                    logger.info(f"received message {msg.content}")
                    msg = await handle_msg(msg, worker_queue, complete_queue)
                    logger.info(f"processed msg response: {msg}")
                else:
                    time.sleep(1)
    except Exception:
        logger.exception("An exception occurred")
        exit(-1)


if __name__ == "__main__":
    if len(sys.argv) > 1 and sys.argv[1] == "test":
        asyncio.run(receive_messages())
    else:
        asyncio.run(receive_one_message())
