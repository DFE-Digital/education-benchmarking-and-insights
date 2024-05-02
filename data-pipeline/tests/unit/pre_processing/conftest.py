from io import StringIO

import pandas as pd
import pytest

from pipeline.pre_processing import prepare_cdc_data


@pytest.fixture
def cdc_data() -> pd.DataFrame:
    return pd.DataFrame(
        {
            "Site": ["Site A", "Site A", "Site A", "Site A", "Site B"],
            "URN": [100150, 100150, 100150, 100150, 100162],
            "SurveySectionSectionType": [
                "ANCILLARY",
                "BLOCK",
                "BLOCK",
                "BLOCK",
                "ANCILLARY",
            ],
            "GIFA": [89, 2221, 436, 57, 131],
            "Block Age": ["NULL", "1961-1970", "2001-2010", "1991-2000", "NULL"],
        }
    )


@pytest.fixture
def prepared_cdc_data(cdc_data: pd.DataFrame) -> dict:
    return (
        prepare_cdc_data(StringIO(cdc_data.to_csv()), 2022)
        .reset_index()
        .to_dict("records")[0]
    )
