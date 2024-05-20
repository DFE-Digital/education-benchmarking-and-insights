import os
import json
import time

import pandas as pd
import pytest
from io import BytesIO
from dotenv import load_dotenv

dotenv_path = os.path.join('tests/e2e/.env.local' if os.getenv('ENV') == 'Local' else 'tests/e2e/.env')
load_dotenv(dotenv_path)

from src.pipeline.storage import write_blob, worker_queue_name, complete_queue_name, connect_to_queue, create_container

year = 2022


async def copy_raw_files_to_raw_blob_storage(scenario_name):
    root = f'tests/e2e/scenarios/{scenario_name}/raw'
    for f in os.listdir(root):
        path = os.path.join(root, f)
        with open(path, "rb") as file_bytes:
            await write_blob('raw', f'test/{scenario_name}/{year}/{f}', file_bytes)


async def trigger_pipeline(scenario_name):
    queue = await connect_to_queue(worker_queue_name)
    await queue.send_message(json.dumps({"type": f'test/{scenario_name}', "year": int(year)}), time_to_live=60)


async def wait_for_pipeline_complete(scenario_name, timeout_secs=180):
    queue = await connect_to_queue(complete_queue_name)
    timeout = time.time() + timeout_secs

    while True:
        if time.time() > timeout:
            raise TimeoutError

        msgs = queue.receive_messages()
        target_type = f'test/{scenario_name}'

        async for message in msgs:
            payload = json.loads(message.content)
            if payload['type'] == target_type and payload['year'] == int(year):
                await queue.delete_message(message)
                return payload


async def get_result_blobs(container, scenario_name):
    prefix = f'test/{scenario_name}/{year}'
    result = {}
    async for b in container.list_blobs(name_starts_with=prefix):
        blob = container.get_blob_client(b.name)
        name = b.name.replace(prefix + '/', '')
        content = await (await blob.download_blob()).readall()
        result[name] = pd.read_parquet(BytesIO(content))
    return result


def get_expected_files(scenario_name):
    root = f'tests/e2e/scenarios/{scenario_name}/expected'
    result = {}
    for f in os.listdir(root):
        path = os.path.join(root, f)
        with open(path, "rb") as file_bytes:
            result[f] = pd.read_parquet(file_bytes)
    return result


async def assert_results(scenario_name):
    container = await create_container('pre-processed')
    expected_files = get_expected_files(scenario_name)
    actual_blobs = await get_result_blobs(container, scenario_name)

    assert len(expected_files) == len(actual_blobs)

    for f in expected_files.keys():
        assert f in actual_blobs.keys(), f'{f} not found in actual blobs {actual_blobs.keys()}'
        actual = actual_blobs[f]
        expected = expected_files[f]
        assert actual.equals(expected), f'Data frames did not match for key: {f}'


def test_synthetic_data_env_is_set():
    actual = os.getenv("STORAGE_CONNECTION_STRING")
    assert actual != "__data-storage-connection-string__"


# To add a new scenario just add a folder with the name of the scenario
# and add the scenario to the list below.
@pytest.mark.parametrize("scenario_name", ['scenario1'])
@pytest.mark.asyncio
async def test_run_scenarios(scenario_name):
    await copy_raw_files_to_raw_blob_storage(scenario_name)
    await trigger_pipeline(scenario_name)
    complete_msg = await wait_for_pipeline_complete(scenario_name)
    if complete_msg["success"] is True:
        await assert_results(scenario_name)
    else:
        assert False, f'The pipeline failed during the run of the scenario {scenario_name}. Error: {complete_msg["error"]}'
