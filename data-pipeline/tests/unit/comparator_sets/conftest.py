import random
import string

import pytest


@pytest.fixture
def random_row() -> dict:
    return {
        "URN": "".join(random.choice(string.ascii_lowercase) for _ in range(64)),
        "Number of pupils": random.randrange(1, 10_000),
        "Percentage Free school meals": random.uniform(0, 100),
        "Prov_SPLD": random.uniform(0, 100),
        "Prov_MLD": random.uniform(0, 100),
        "Prov_PMLD": random.uniform(0, 100),
        "Prov_SEMH": random.uniform(0, 100),
        "Prov_SLCN": random.uniform(0, 100),
        "Prov_HI": random.uniform(0, 100),
        "Prov_MSI": random.uniform(0, 100),
        "Prov_PD": random.uniform(0, 100),
        "Prov_ASD": random.uniform(0, 100),
        "Prov_OTH": random.uniform(0, 100),
        "Percentage SEN": random.uniform(0, 100),
    }
