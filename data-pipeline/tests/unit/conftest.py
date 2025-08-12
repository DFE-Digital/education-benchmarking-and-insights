import os
import pytest

@pytest.fixture(scope="session", autouse=True)
def setup_test_env():
    """Set up environment variables for all tests."""
    os.environ.update({
        "STORAGE_CONNECTION_STRING": "test"
    })
    yield