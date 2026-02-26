import io

import pandas as pd

from pipeline.pre_processing.ancillary.ons_population_estimates import (
    prepare_ons_population_estimates,
)


def test_la_ons(la_ons_raw: pd.DataFrame):
    """
    Initial dataset will be reduced to a single row.

    The `Population2To18` figure will equal 1 for every row in the age
    range 2—18.
    """
    year = 2024

    result = prepare_ons_population_estimates(
        io.StringIO(la_ons_raw.to_csv()),
        year,
    )

    assert len(la_ons_raw.index) == 42
    assert len(result.index) == 1
    assert result.iloc[0]["Population2To18"] == 17
