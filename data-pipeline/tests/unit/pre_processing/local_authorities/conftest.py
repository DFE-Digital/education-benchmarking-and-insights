import pandas as pd
import pytest


@pytest.fixture
def la_ons() -> pd.DataFrame:
    year = 2024
    years = list(map(str, range(year - 5, year + 5)))
    ages = list(map(str, range(1, 43)))

    data = {
        "AREA_CODE": ["C"] * len(ages),
        "AREA_NAME": ["N"] * len(ages),
        "COMPONENT": ["O"] * len(ages),
        "SEX": ["P"] * len(ages),
        "AGE_GROUP": ages,
    } | {y: 1.0 for y in years}

    return pd.DataFrame(data)
