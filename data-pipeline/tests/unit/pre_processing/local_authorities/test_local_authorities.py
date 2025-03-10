import io

import pandas as pd

from pipeline.pre_processing.ancillary import local_authority


def test_local_authorities(
    la_budget: pd.DataFrame,
    la_outturn: pd.DataFrame,
    la_statistical_neighbours: io.StringIO,
    la_ons: pd.DataFrame,
):
    year = 2024

    result = local_authority.build_local_authorities(
        io.StringIO(la_budget.to_csv()),
        io.StringIO(la_outturn.to_csv(encoding="cp1252")),
        la_statistical_neighbours,
        io.StringIO(la_ons.to_csv()),
        year,
    )

    assert len(result.index) > 0
