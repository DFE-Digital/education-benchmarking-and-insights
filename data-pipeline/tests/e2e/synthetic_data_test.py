import os
from dotenv import load_dotenv

load_dotenv()


def test_synthetic_data_env_is_set():
    actual = os.getenv("STORAGE_CONNECTION_STRING")
    assert actual != "__data-storage-connection-string__"
