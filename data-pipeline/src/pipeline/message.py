from enum import Enum, auto


class MessageType(Enum):
    """
    Various types of incoming message:

    - `default` data.
    - `default` data with user-defined comparator-set.
    - `custom` data.
    """

    Default = auto()
    DefaultUserDefined = auto()
    Custom = auto()


class InvalidMessageTypeException(Exception):
    """
    If the type of an incoming message cannot be determined.
    """


def get_message_type(message: dict) -> MessageType:
    """
    Determine the type of an incoming message.

    TODO: `default` message format?

    User-defined comparator set message _content_ will be of the form:

    ```json
    {
        "jobId": "24463424-9642-4314-bb55-45424af6e812",
        "type": "comparator-set",
        "runId": "c321ef6a-3b1c-4ce2-8e32-0d0167bf2fa7",
        "year": 2022,
        "urn": "106057",
        "payload": {
            "kind": "ComparatorSetPayload",
            "set": [
                "145799",
                "142875"
            ]
        }
    }
    ```

    TODO: custom data message format?

    :param message: incoming message
    :return: type of incoming message
    """
    if message.get("payload", {}).get("kind") == "ComparatorSetPayload":
        return MessageType.DefaultUserDefined

    # TODO: custom data type.

    return MessageType.Default
