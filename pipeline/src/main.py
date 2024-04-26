import logging
import os
import sys
import time

from azure.storage.queue import QueueClient

logger = logging.getLogger('DocumentParser')
logging.basicConfig(stream=sys.stdout, level=logging.INFO)


def receive_messages():

    conn_str = os.getenv('QUEUE_CONNECTION_STRING')
    queue_name = os.getenv('QUEUE_NAME')
    if not conn_str:
        logger.error("queue connection string not provided!")
        return

    if not queue_name:
        logger.error("queue name not provided!")
        return

    queue_client = QueueClient.from_connection_string(conn_str=conn_str, queue_name=queue_name)
    msg = queue_client.receive_message()

    logger.info(f'received message {msg}')

    time.sleep(45)

    logger.info(f'processing complete')


if __name__ == '__main__':
    receive_messages()
