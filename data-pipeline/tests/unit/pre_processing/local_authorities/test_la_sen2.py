import io

import pandas as pd

from pipeline.pre_processing.s251 import local_authority


def test_sen2(la_sen2: pd.DataFrame):
    """
    The 58 rows for a single LA should be pivoted to a single row.
    """
    result = local_authority._prepare_sen2_la_data(
        io.StringIO(la_sen2.to_csv()),
        2024,
    )

    assert len(la_sen2.index) == 58
    assert len(result.index) == 1
