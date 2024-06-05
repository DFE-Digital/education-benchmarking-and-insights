import random
import string

import numpy as np
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


@pytest.fixture
def select_top_set_urns_defaults() -> dict:
    """
    Some default values for the `select_top_set_urns` function.

    :return: function defaults
    """
    return {
        "urns": np.array(list(string.ascii_uppercase)),  # ["A"…"Z"]
        "pfi": np.array([True] * 26),  # [True, True…]
        "boarding": np.array([True] * 26),  # [True, True…]
        "regions": np.array(["A"] * 26),  # ["A"…"A"]
        "distances": np.array([0.01 * i for i in range(26)]),  # [0.0, 0.01…0.25]
        "base_set_size": 6,
        "final_set_size": 3,
    }
