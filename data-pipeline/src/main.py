import logging
import os
import sys
import time
from contextlib import suppress

from azure.core.exceptions import ResourceExistsError
from azure.storage.queue import QueueClient, QueueServiceClient
from dotenv import load_dotenv

load_dotenv()

logger = logging.getLogger("fbit-data-pipeline")
logging.basicConfig(stream=sys.stdout, level=logging.INFO)


def connect_to_queue() -> QueueClient:
    conn_str = os.getenv("QUEUE_CONNECTION_STRING")
    queue_name = os.getenv("WORKER_QUEUE_NAME")

    if not conn_str:
        raise Exception("Queue connection string not provided!")

    if not queue_name:
        raise Exception("Queue name not provided!")

    logger.info(f"Connecting to queue {conn_str} - {queue_name}")
    service_client = QueueServiceClient.from_connection_string(conn_str=conn_str)
    queue = service_client.get_queue_client(queue_name)
    with suppress(ResourceExistsError):
        queue.create_queue()

    return queue


def receive_messages():
    try:
        queue = connect_to_queue()
        msg = queue.receive_message()
        if msg is not None:
            logger.info(f"received message {msg}")
            time.sleep(45)
            logger.info(f"processing complete")
            exit(0)
        else:
            logger.info("no messages received")
            exit(0)
    except Exception as error:
        logger.exception("An exception occurred:", type(error).__name__, "–", error)
        exit(-1)


if __name__ == "__main__":
    receive_messages()
