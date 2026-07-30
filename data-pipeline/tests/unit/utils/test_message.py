import pytest

from pipeline.utils.message import (
    MessageType,
    get_message_type,
    PipelineEarlyExit,
    check_run_until_gate,
)


@pytest.mark.parametrize(
    "body,expected",
    [
        ({}, MessageType.Default),
        ({"payload": {"kind": "ComparatorSetPayload"}}, MessageType.DefaultUserDefined),
        ({"payload": {"kind": "CustomDataPayload"}}, MessageType.Custom),
    ],
)
def test_message(body: dict, expected: MessageType):
    result = get_message_type(message=body)

    assert result == expected


def test_check_run_until_gate_matches():
    with pytest.raises(PipelineEarlyExit) as exc_info:
        check_run_until_gate("pre-processing", "pre-processing")
    assert "Pipeline stopped after pre-processing as requested by runUntil." in str(exc_info.value)


def test_check_run_until_gate_different():
    # Should not raise any exception
    check_run_until_gate("pre-processing", "comparators")


def test_check_run_until_gate_none():
    # Should not raise any exception
    check_run_until_gate("pre-processing", None)
