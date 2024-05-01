import json
import sys
import asyncio
from contextlib import suppress
from azure.core.exceptions import ResourceExistsError
from azure.storage.queue.aio import QueueServiceClient


async def run_pipeline(run_type,
    year,
    conn_str="DefaultEndpointsProtocol=http;AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1;QueueEndpoint=http://127.0.0.1:10001/devstoreaccount1;TableEndpoint=http://127.0.0.1:10002/devstoreaccount1;",
    queue_name="data-pipeline-job-start"):

    print(f'Running pipeline {run_type} for {year}')

    queue_service_client = QueueServiceClient.from_connection_string(conn_str=conn_str)

    if not conn_str:
        raise Exception("Queue connection string not provided!")

    if not queue_name:
        raise Exception("Queue name not provided!")

    queue = queue_service_client.get_queue_client(queue_name)
    with suppress(ResourceExistsError):
        await queue.create_queue()

    async with queue_service_client:
        await queue.send_message(json.dumps({"type": run_type, "year": int(year)}))

if __name__ == "__main__":
    if len(sys.argv) < 3:
        print("Usage: trigger_pipeline_run.py <type> <year> [<conn_str>] [<queue_name>]")
        sys.exit(-1)

    if len(sys.argv) == 3:
        asyncio.run(run_pipeline(sys.argv[1], sys.argv[2]))

    if len(sys.argv) == 4:
        asyncio.run(run_pipeline(sys.argv[1], sys.argv[2], sys.argv[3]))

    if len(sys.argv) == 5:
        asyncio.run(run_pipeline(sys.argv[1], sys.argv[2], sys.argv[3], sys.argv[4]))

    print('Done')
