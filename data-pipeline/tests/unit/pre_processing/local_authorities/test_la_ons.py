import io

import pandas as pd

from pipeline.pre_processing import local_authority


def test_la_ons(la_ons: pd.DataFrame):
    """
    Initial dataset will be reduced to a single row.

    The `Population2To18` figure will equal 1 for every row in the age
    range 2—18.
    """
    year = 2024

    result = local_authority._prepare_ons_la_population_data(
        io.StringIO(la_ons.to_csv()),
        year,
    )

    assert len(la_ons.index) == 42
    assert len(result.index) == 1
    assert result.iloc[0]["Population2To18"] == 17
