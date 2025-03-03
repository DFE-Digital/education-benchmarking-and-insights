import io

import pandas as pd

from pipeline.pre_processing.ancillary import local_authority


def test_la_expenditure(la_expenditure: pd.DataFrame):
    """
    The pivoted data must result in reduced rows.
    """
    buffer = io.BytesIO()
    la_expenditure.to_csv(buffer)
    buffer.seek(0)

    result = local_authority._prepare_la_expenditure_data(buffer, 2024)

    assert len(la_expenditure) == 13
    assert len(result.index) == 1


def test_la_expenditure_year(la_expenditure: pd.DataFrame):
    """
    Rows with a different `time_period` must be discarded.
    """
    la_expenditure_old = la_expenditure.copy()
    la_expenditure_old["time_period"] = "202122"
    combined = pd.concat([la_expenditure, la_expenditure_old])
    buffer = io.BytesIO()
    combined.to_csv(buffer)
    buffer.seek(0)

    result = local_authority._prepare_la_expenditure_data(buffer, 2024)

    assert len(combined) == 26
    assert len(result.index) == 1
