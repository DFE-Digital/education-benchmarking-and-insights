import os
import json
import time

import pandas as pd
import pytest
from io import BytesIO
from dotenv import load_dotenv

dotenv_path = os.path.join(
    "tests/e2e/.env.local" if os.getenv("ENV") == "Local" else "tests/e2e/.env"
)
load_dotenv(dotenv_path)

from src.pipeline.storage import (
    write_blob,
    worker_queue_name,
    complete_queue_name,
    connect_to_queue,
    create_container,
)

year = 2022


def copy_raw_files_to_raw_blob_storage(scenario_name):
    root = f"tests/e2e/scenarios/{scenario_name}/raw"
    for f in os.listdir(root):
        path = os.path.join(root, f)
        with open(path, "rb") as file_bytes:
            write_blob("raw", f"test/{scenario_name}/{year}/{f}", file_bytes)


def trigger_pipeline(scenario_name):
    queue = connect_to_queue(worker_queue_name)
    queue.send_message(
        json.dumps({"type": f"test/{scenario_name}", "year": int(year)}),
        time_to_live=60,
    )


def wait_for_pipeline_complete(scenario_name, timeout_secs=180):
    queue = connect_to_queue(complete_queue_name)
    timeout = time.time() + timeout_secs

    while True:
        if time.time() > timeout:
            raise TimeoutError

        msgs = queue.receive_messages()
        target_type = f"test/{scenario_name}"

        for message in msgs:
            payload = json.loads(message.content)
            if payload["type"] == target_type and payload["year"] == int(year):
                queue.delete_message(message)
                return payload


def get_result_blobs(container, scenario_name):
    prefix = f"test/{scenario_name}/{year}"
    result = {}
    for b in container.list_blobs(name_starts_with=prefix):
        blob = container.get_blob_client(b.name)
        name = b.name.replace(prefix + "/", "")
        content = blob.download_blob().readall()
        result[name] = pd.read_parquet(BytesIO(content))
    return result


def get_expected_files(scenario_name):
    root = f"tests/e2e/scenarios/{scenario_name}/expected"
    result = {}
    for f in os.listdir(root):
        path = os.path.join(root, f)
        with open(path, "rb") as file_bytes:
            result[f] = pd.read_parquet(file_bytes)
    return result


def assert_results(scenario_name):
    container = create_container("pre-processed")
    expected_files = get_expected_files(scenario_name)
    actual_blobs = get_result_blobs(container, scenario_name)

    assert len(expected_files) == len(actual_blobs)

    for f in expected_files.keys():
        assert (
            f in actual_blobs.keys()
        ), f"{f} not found in actual blobs {actual_blobs.keys()}"
        actual = actual_blobs[f]
        expected = expected_files[f]
        assert actual.equals(expected), f"Data frames did not match for key: {f}"


def test_synthetic_data_env_is_set():
    actual = os.getenv("STORAGE_CONNECTION_STRING")
    assert actual != "__data-storage-connection-string__"


# To add a new scenario just add a folder with the name of the scenario
# and add the scenario to the list below.
@pytest.mark.parametrize("scenario_name", ["WhenLoadingSingleAcademyTrusts"])
def test_run_scenarios(scenario_name):
    copy_raw_files_to_raw_blob_storage(scenario_name)
    trigger_pipeline(scenario_name)
    complete_msg = wait_for_pipeline_complete(scenario_name)
    if complete_msg["success"] is True:
        assert_results(scenario_name)
    else:
        assert (
            False
        ), f'The pipeline failed during the run of the scenario {scenario_name}. Error: {complete_msg["error"]}'
