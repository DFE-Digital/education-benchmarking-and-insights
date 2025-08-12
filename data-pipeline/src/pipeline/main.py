import json
import os
import time
from contextlib import suppress

from azure.core.exceptions import ResourceNotFoundError
from azure.storage.queue import QueueClient, QueueMessage
from dotenv import load_dotenv

from pipeline.utils.stats import stats_collector

load_dotenv()

from pipeline.comparator_sets import compute_comparator_sets
from pipeline.pre_processing import pre_process_custom_data, pre_process_data
from pipeline.rag import compute_rag, compute_user_defined_rag
from pipeline.utils.log import setup_logger
from pipeline.utils.message import MessageType, get_message_type
from pipeline.utils.storage import (
    get_blob_service_client,
    get_queue_service_client,
    complete_queue_name,
    connect_to_queue,
    dead_letter_dequeue_max,
    dead_letter_queue_name,
    worker_queue_name,
)

logger = setup_logger("fbit-data-pipeline")


def handle_msg(
    msg: QueueMessage,
    worker_queue: QueueClient,
    complete_queue: QueueClient,
):
    """
    Process an incoming message.

    Note: user-defined comparator sets will assume pre-processing has
    taken place for the year in question, failing if that does not hold
    true.

    :param msg: incoming message, triggering this process
    :param worker_queue: incoming message queue (for deletion)
    :param complete_queue: outcoming message queue (for completion)
    :return: updated message payload
    """
    msg_payload = json.loads(msg.content)
    run_type = msg_payload.get("runType", "default")

    try:
        match get_message_type(message=msg_payload):
            case MessageType.Default:
                logger.info("Starting default pipeline run...")
                stats_collector.start_pipeline_run()
                msg_payload["pre_process_duration"] = pre_process_data(
                    run_id=str(msg_payload["runId"]),
                    aar_year=msg_payload["year"]["aar"],
                    cfr_year=msg_payload["year"]["cfr"],
                    bfr_year=msg_payload["year"]["bfr"],
                    s251_year=msg_payload["year"]["s251"],
                )
                msg_payload["comparator_set_duration"] = compute_comparator_sets(
                    run_type=run_type,
                    run_id=str(msg_payload["runId"]),
                )
                msg_payload["rag_duration"] = compute_rag(
                    run_type=run_type,
                    run_id=str(msg_payload["runId"]),
                )
                msg_payload["stats"] = stats_collector.get_stats()
                logger.info("Default pipeline run completed!")

            case MessageType.DefaultUserDefined:
                logger.info("Starting user defined RAG pipeline run...")
                msg_payload["rag_duration"] = compute_user_defined_rag(
                    year=msg_payload["year"],
                    run_id=msg_payload["runId"],
                    target_urn=int(msg_payload["urn"]),
                    comparator_set=list(map(int, msg_payload["payload"]["set"])),
                )
                logger.info("User defined RAG pipeline run completed!")

            case MessageType.Custom:
                logger.info("Starting custom pipeline run...")
                stats_collector.start_pipeline_run()
                msg_payload["pre_process_duration"] = pre_process_custom_data(
                    run_id=msg_payload["runId"],
                    year=msg_payload["year"],
                    target_urn=int(msg_payload["urn"]),
                    custom_data={
                        k: v for k, v in msg_payload["payload"].items() if k != "kind"
                    },
                )
                msg_payload["comparator_set_duration"] = compute_comparator_sets(
                    run_type=run_type,
                    run_id=msg_payload["runId"],
                    target_urn=int(msg_payload["urn"]),
                )
                msg_payload["rag_duration"] = compute_rag(
                    run_type=run_type,
                    run_id=msg_payload["runId"],
                    target_urn=int(msg_payload["urn"]),
                )
                msg_payload["stats"] = stats_collector.get_stats()
                logger.info("Custom pipeline run completed!")

        msg_payload["success"] = True
    except Exception as error:
        logger.exception(
            f"An exception occurred: {type(error).__name__}", exc_info=error
        )
        msg_payload["success"] = False
        msg_payload["error"] = str(error)
    finally:
        with suppress(ResourceNotFoundError):
            worker_queue.delete_message(msg)

        complete_queue.send_message(json.dumps(msg_payload), time_to_live=300)

    return msg_payload


def _check_msg_dequeue(
    message: QueueMessage,
    dead_letter_queue: QueueClient,
    origin_queue: QueueClient,
    dequeue_limit: int,
) -> None:
    """
    Determine whether to redirect to the dead-letter queue.

    If the message's `dequeue_count` exceeds the `dequeue_limit`, it is:

    1. added to the dead-letter queue.
    2. removed from the originating queue.

    The entire message (not just the payload) is serialised when passed
    to the dead-letter queue, for additional context.

    :param message: incoming message to check
    :param dead_letter_queue: target for messages exceeding the limit
    :param worker_queue: source for incoming messages
    :param dequeue_limit: retry limit for re-queued messages
    :raises Exception: when the retry limit is exceeded
    """
    if message.dequeue_count <= dequeue_limit:
        return

    logger.error(
        f"Message {message.id} has exceeded the retry limit of {dequeue_limit}."
    )
    dead_letter_queue.send_message(json.dumps(message, default=str))

    with suppress(ResourceNotFoundError):
        origin_queue.delete_message(message)

    raise Exception(
        f"Message {message.id} has exceeded the retry limit of {dequeue_limit}."
    )


def receive_one_message():
    try:
        blob_service_client = get_blob_service_client()
        queue_service_client = get_queue_service_client()
        with blob_service_client, queue_service_client:
            worker_queue = connect_to_queue(worker_queue_name)
            complete_queue = connect_to_queue(complete_queue_name)
            dead_letter_queue = connect_to_queue(dead_letter_queue_name)

            with worker_queue, complete_queue, dead_letter_queue:
                if msg := worker_queue.receive_message(visibility_timeout=300):
                    _check_msg_dequeue(
                        message=msg,
                        dead_letter_queue=dead_letter_queue,
                        origin_queue=worker_queue,
                        dequeue_limit=dead_letter_dequeue_max,
                    )

                    logger.info(f"received message {msg.content}")
                    msg = handle_msg(msg, worker_queue, complete_queue)
                    exit(0) if msg["success"] else exit(1)
                else:
                    logger.info("no messages received")
                    exit(0)
    except Exception as error:
        logger.exception(
            f"An exception occurred: {type(error).__name__}", exc_info=error
        )
        exit(-1)


def receive_messages():
    try:
        blob_service_client = get_blob_service_client()
        queue_service_client = get_queue_service_client()
        with blob_service_client, queue_service_client:
            worker_queue = connect_to_queue(worker_queue_name)
            complete_queue = connect_to_queue(complete_queue_name)
            dead_letter_queue = connect_to_queue(dead_letter_queue_name)
            with worker_queue, complete_queue:
                while True:
                    if msg := worker_queue.receive_message(visibility_timeout=300):
                        _check_msg_dequeue(
                            message=msg,
                            dead_letter_queue=dead_letter_queue,
                            origin_queue=worker_queue,
                            dequeue_limit=dead_letter_dequeue_max,
                        )

                        logger.info(f"received message {msg.content}")
                        msg = handle_msg(msg, worker_queue, complete_queue)
                        logger.info(f"processed msg response: {msg}")
                    else:
                        time.sleep(1)
    except Exception as error:
        logger.exception(
            f"An exception occurred: {type(error).__name__}", exc_info=error
        )
        exit(-1)


if __name__ == "__main__":
    if os.getenv("ENV") == "dev":
        receive_messages()
    else:
        receive_one_message()
