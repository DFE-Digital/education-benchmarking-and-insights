import logging
import os
from contextlib import suppress
from io import BytesIO, StringIO

from azure.core.exceptions import ResourceExistsError
from azure.storage.blob import BlobServiceClient
from azure.storage.queue import QueueClient, QueueServiceClient

azure_logger = logging.getLogger("azure")
azure_logger.setLevel(logging.WARNING)

conn_str = os.getenv("STORAGE_CONNECTION_STRING")
worker_queue_name = os.getenv("WORKER_QUEUE_NAME", "data-pipeline-job-start")
complete_queue_name = os.getenv("COMPLETE_QUEUE_NAME", "data-pipeline-job-finished")
raw_container = os.getenv("RAW_DATA_CONTAINER", "raw")
blob_service_client = BlobServiceClient.from_connection_string(conn_str=conn_str)
queue_service_client = QueueServiceClient.from_connection_string(conn_str=conn_str)


def connect_to_queue(queue_name) -> QueueClient:
    if not conn_str:
        raise Exception("Queue connection string not provided!")

    if not queue_name:
        raise Exception("Queue name not provided!")

    queue = queue_service_client.get_queue_client(queue_name)
    with suppress(ResourceExistsError):
        queue.create_queue()

    return queue


def create_container(container_name):
    with suppress(ResourceExistsError):
        blob_service_client.create_container(container_name)

    return blob_service_client.get_container_client(container_name)


def get_blob(container_name, blob_name, encoding=None):
    container_client = blob_service_client.get_container_client(container_name)
    blob_client = container_client.get_blob_client(blob_name)

    with blob_client as blob:
        if encoding is None:
            content = blob.download_blob(encoding=encoding).readall()
            return BytesIO(content)

        content = blob.download_blob(encoding=encoding).readall()
        return StringIO(content)


def write_blob(container_name, blob_name, data):
    container_client = create_container(container_name)
    with container_client:
        blob_client = container_client.get_blob_client(blob_name)
        with blob_client as blob:
            blob.upload_blob(data, encoding="utf-8", overwrite=True)
