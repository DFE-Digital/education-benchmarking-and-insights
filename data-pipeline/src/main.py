import logging
import os
import sys
import time
from contextlib import suppress

from azure.core.exceptions import ResourceExistsError
from azure.storage.queue import QueueClient, QueueServiceClient

logger = logging.getLogger('fbit-data-pipeline')
logging.basicConfig(stream=sys.stdout, level=logging.INFO)

def connect_to_queue():
    conn_str = os.getenv('QUEUE_CONNECTION_STRING')
    queue_name = os.getenv('QUEUE_NAME')

    if not conn_str:
        logger.error("Queue connection string not provided!")
        return

    if not queue_name:
        logger.error("Queue name not provided!")
        return

    print(f"Connecting to queue {conn_str} - {queue_name}")
    service_client = QueueServiceClient.from_connection_string(conn_str=conn_str)
    queue = service_client.get_queue_client(queue_name)
    with suppress(ResourceExistsError):
        queue.create_queue()

    return queue


def receive_messages():
    queue = connect_to_queue()

    msg = queue.receive_message()
    if msg is not None:
        logger.info(f'received message {msg}')
        time.sleep(45)
        logger.info(f'processing complete')
    else:
        logger.info('no messages received')


if __name__ == '__main__':
    receive_messages()
