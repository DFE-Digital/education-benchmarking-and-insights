import io

import pandas as pd

from pipeline.pre_processing.ancillary import local_authority


def test_la_expenditure(la_expenditure: pd.DataFrame):
    buffer = io.BytesIO()
    la_expenditure.to_csv(buffer)
    buffer.seek(0)

    result = local_authority._prepare_la_expenditure_data(buffer, 2024)

    assert len(result.index) == 1
