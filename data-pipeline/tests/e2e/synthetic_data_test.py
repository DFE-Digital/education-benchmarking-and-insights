import os
from dotenv import load_dotenv
load_dotenv()


def test_synthetic_data():
    assert os.getenv("WORKER_QUEUE_NAME") == "test_queue_name"
