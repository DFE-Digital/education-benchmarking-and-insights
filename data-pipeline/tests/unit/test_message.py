import pytest

from src.pipeline.message import MessageType, get_message_type


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
