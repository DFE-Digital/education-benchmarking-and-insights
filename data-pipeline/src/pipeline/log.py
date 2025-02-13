import logging
import os

from pythonjsonlogger.json import JsonFormatter


def setup_logger(name: str):
    logger = logging.getLogger(name)

    logger.setLevel(os.getenv("LOG_LEVEL", logging.DEBUG))

    handler = logging.StreamHandler()
    formatter = JsonFormatter(fmt="%(asctime)s %(levelname)s %(module)s %(message)s")
    handler.setFormatter(formatter)
    logger.addHandler(handler)

    return logger
