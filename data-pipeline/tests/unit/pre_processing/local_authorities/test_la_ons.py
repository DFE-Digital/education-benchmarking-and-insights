import io

import pandas as pd

from pipeline.pre_processing.ancillary import local_authority


def test_la_ons(la_ons: pd.DataFrame):
    """
    Initial dataset will be reduced to a single row.

    The `Population2To18` figure will equal 1 for every row in the age
    range 2—18.
    """
    buffer = io.BytesIO()
    la_ons.to_csv(buffer)
    buffer.seek(0)
    year = 2024

    result = local_authority._build_ons_la_population_data(
        buffer,
        year,
    )

    assert len(la_ons.index) == 42
    assert len(result.index) == 1
    assert result.iloc[0]["Population2To18"] == 17
