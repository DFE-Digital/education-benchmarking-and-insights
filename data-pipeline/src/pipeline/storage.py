import logging
import os
from contextlib import suppress
from io import BytesIO, StringIO

from azure.core.exceptions import ResourceExistsError, ResourceNotFoundError
from azure.storage.blob import BlobServiceClient
from azure.storage.queue import QueueClient, QueueServiceClient

azure_logger = logging.getLogger("azure")
logger = logging.getLogger("fbit-data-pipeline")
azure_logger.setLevel(logging.WARNING)


conn_str = os.getenv("STORAGE_CONNECTION_STRING")
worker_queue_name = os.getenv("WORKER_QUEUE_NAME", "data-pipeline-job-default-start")
complete_queue_name = os.getenv("COMPLETE_QUEUE_NAME", "data-pipeline-job-finished")
dead_letter_queue_name = os.getenv("DEAD_LETTER_QUEUE_NAME", "data-pipeline-job-dlq")
dead_letter_dequeue_max = int(os.getenv("DEAD_LETTER_QUEUE_DEQUEUE_MAX", "5"))
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
        logger.info(f"Created blob container: {container_name}")

    return blob_service_client.get_container_client(container_name)


def get_blob(container_name: str, blob_name: str, encoding=None) -> BytesIO | StringIO:
    container_client = blob_service_client.get_container_client(container_name)
    blob_client = container_client.get_blob_client(blob_name)

    content = blob_client.download_blob(encoding=encoding).readall()
    properties = blob_client.get_blob_properties()
    size = properties.size
    name = properties.name
    content_md5 = properties.content_settings.content_md5
    md5_hash = content_md5.hex() if content_md5 else "None"
    logger.info(f"Downloaded blob: {name=} {size=} {md5_hash=}")

    return BytesIO(content) if encoding is None else StringIO(content)


def try_get_blob(
    container_name: str, blob_name: str, encoding=None
) -> None | BytesIO | StringIO:
    """For some blobs, we want to return None if it doesn't exist."""
    try:
        io_blob_content = get_blob(container_name, blob_name, encoding)
        return io_blob_content
    except ResourceNotFoundError:
        logger.info(f"'{blob_name=}' does not exist, skipping download")
        return None


def write_blob(container_name, blob_name, data):
    container_client = create_container(container_name)
    with container_client:
        blob_client = container_client.get_blob_client(blob_name)
        with blob_client as blob:
            blob.upload_blob(data, encoding="utf-8", overwrite=True)
        logger.info(f"Written to blob {container_name}/{blob_name}")
