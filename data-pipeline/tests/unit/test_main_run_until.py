import json
from unittest.mock import MagicMock, patch

import pytest
from azure.storage.queue import QueueMessage

from pipeline.main import handle_msg


def test_handle_msg_invalid_run_until():
    # Arrange
    msg_payload = {
        "runId": 2023,
        "year": {"aar": 2022, "cfr": 2023, "bfr": 2022, "s251": 2021},
        "runUntil": "invalid-stage",
    }

    mock_msg = MagicMock(spec=QueueMessage)
    mock_msg.content = json.dumps(msg_payload)

    mock_worker_queue = MagicMock()
    mock_complete_queue = MagicMock()

    # Act
    result = handle_msg(mock_msg, mock_worker_queue, mock_complete_queue)

    # Assert
    assert result["success"] is False
    assert "Invalid runUntil value: 'invalid-stage'" in result["error"]
    # Verify that worker queue message is still deleted
    mock_worker_queue.delete_message.assert_called_once_with(mock_msg)
    # Since any error inside handle_msg goes through the standard error path, a failure message is sent
    mock_complete_queue.send_message.assert_called_once()


@patch("pipeline.main.pre_process_data")
@patch("pipeline.main.run_comparator_sets_pipeline")
@patch("pipeline.main.run_rag_pipeline")
def test_handle_msg_run_until_preprocessing(
    mock_rag, mock_comparators, mock_pre_process
):
    # Arrange
    msg_payload = {
        "runId": 2023,
        "year": {"aar": 2022, "cfr": 2023, "bfr": 2022, "s251": 2021},
        "runUntil": "pre-processing",
    }

    mock_msg = MagicMock(spec=QueueMessage)
    mock_msg.content = json.dumps(msg_payload)

    mock_worker_queue = MagicMock()
    mock_complete_queue = MagicMock()

    mock_pre_process.return_value = 10.0

    # Act
    result = handle_msg(mock_msg, mock_worker_queue, mock_complete_queue)

    # Assert
    assert result["success"] is True
    assert "error" not in result

    mock_pre_process.assert_called_once_with(
        run_id="2023",
        aar_year=2022,
        cfr_year=2023,
        bfr_year=2022,
        s251_year=2021,
        run_until="pre-processing",
        generate_cfr_transparency_file=False,
    )
    # RAG and Comparators should NOT be called
    mock_comparators.assert_not_called()
    mock_rag.assert_not_called()

    # Message should be deleted from worker queue
    mock_worker_queue.delete_message.assert_called_once_with(mock_msg)
    # Complete queue failure message should be sent
    mock_complete_queue.send_message.assert_called_once()


@patch("pipeline.main.pre_process_data")
@patch("pipeline.main.run_comparator_sets_pipeline")
@patch("pipeline.main.run_rag_pipeline")
def test_handle_msg_generate_cfr_transparency_true(
    mock_rag, mock_comparators, mock_pre_process
):
    # Arrange
    msg_payload = {
        "runId": 2023,
        "year": {"aar": 2022, "cfr": 2023, "bfr": 2022, "s251": 2021},
        "generateCFRTransparencyFile": True,
    }

    mock_msg = MagicMock(spec=QueueMessage)
    mock_msg.content = json.dumps(msg_payload)

    mock_worker_queue = MagicMock()
    mock_complete_queue = MagicMock()

    mock_pre_process.return_value = 10.0
    mock_comparators.return_value = 5.0
    mock_rag.return_value = 8.0

    # Act
    handle_msg(mock_msg, mock_worker_queue, mock_complete_queue)

    # Assert
    mock_pre_process.assert_called_once_with(
        run_id="2023",
        aar_year=2022,
        cfr_year=2023,
        bfr_year=2022,
        s251_year=2021,
        run_until=None,
        generate_cfr_transparency_file=True,
    )


@patch("pipeline.main.pre_process_data")
@patch("pipeline.main.run_comparator_sets_pipeline")
@patch("pipeline.main.run_rag_pipeline")
def test_handle_msg_run_until_comparators(mock_rag, mock_comparators, mock_pre_process):
    # Arrange
    msg_payload = {
        "runId": 2023,
        "year": {"aar": 2022, "cfr": 2023, "bfr": 2022, "s251": 2021},
        "runUntil": "comparators",
    }

    mock_msg = MagicMock(spec=QueueMessage)
    mock_msg.content = json.dumps(msg_payload)

    mock_worker_queue = MagicMock()
    mock_complete_queue = MagicMock()

    mock_pre_process.return_value = 10.0
    mock_comparators.return_value = 5.0

    # Act
    result = handle_msg(mock_msg, mock_worker_queue, mock_complete_queue)

    # Assert
    assert result["success"] is True
    assert "error" not in result

    mock_pre_process.assert_called_once()
    mock_comparators.assert_called_once()
    # RAG should NOT be called
    mock_rag.assert_not_called()

    # Message should be deleted from worker queue
    mock_worker_queue.delete_message.assert_called_once_with(mock_msg)
    # Complete queue failure message should be sent
    mock_complete_queue.send_message.assert_called_once()


@patch("pipeline.main.pre_process_data")
@patch("pipeline.main.run_comparator_sets_pipeline")
@patch("pipeline.main.run_rag_pipeline")
def test_handle_msg_normal_execution(mock_rag, mock_comparators, mock_pre_process):
    # Arrange
    msg_payload = {
        "runId": 2023,
        "year": {"aar": 2022, "cfr": 2023, "bfr": 2022, "s251": 2021},
    }

    mock_msg = MagicMock(spec=QueueMessage)
    mock_msg.content = json.dumps(msg_payload)

    mock_worker_queue = MagicMock()
    mock_complete_queue = MagicMock()

    mock_pre_process.return_value = 10.0
    mock_comparators.return_value = 5.0
    mock_rag.return_value = 8.0

    # Act
    result = handle_msg(mock_msg, mock_worker_queue, mock_complete_queue)

    # Assert
    assert result["success"] is True
    assert "error" not in result

    mock_pre_process.assert_called_once()
    mock_comparators.assert_called_once()
    mock_rag.assert_called_once()

    # Message should be deleted from worker queue
    mock_worker_queue.delete_message.assert_called_once_with(mock_msg)
    # Complete queue message SHOULD be sent because runUntil is NOT in the payload!
    mock_complete_queue.send_message.assert_called_once()
