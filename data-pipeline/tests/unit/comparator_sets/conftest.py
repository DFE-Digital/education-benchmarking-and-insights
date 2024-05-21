import random
import string

import pytest


@pytest.fixture
def random_row() -> dict:
    return {
        "URN": "".join(random.choice(string.ascii_lowercase) for _ in range(64)),
        "Number of pupils": random.randrange(1, 10_000),
        "Percentage Free school meals": random.uniform(0, 100),
        "Percentage Primary Need SPLD": random.uniform(0, 100),
        "Percentage Primary Need MLD": random.uniform(0, 100),
        "Percentage Primary Need PMLD": random.uniform(0, 100),
        "Percentage Primary Need SEMH": random.uniform(0, 100),
        "Percentage Primary Need SLCN": random.uniform(0, 100),
        "Percentage Primary Need HI": random.uniform(0, 100),
        "Percentage Primary Need MSI": random.uniform(0, 100),
        "Percentage Primary Need PD": random.uniform(0, 100),
        "Percentage Primary Need ASD": random.uniform(0, 100),
        "Percentage Primary Need OTH": random.uniform(0, 100),
        "Percentage SEN": random.uniform(0, 100),
    }
