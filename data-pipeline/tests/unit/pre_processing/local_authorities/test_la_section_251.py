import io

import pandas as pd

from pipeline.pre_processing.s251 import local_authority


def test_la_section_251(la_budget: pd.DataFrame, la_outturn: pd.DataFrame):
    """
    The combined data must result in reduced rows.
    """
    budget_buffer = io.BytesIO()
    la_budget.to_csv(budget_buffer)
    budget_buffer.seek(0)
    outturn_buffer = io.BytesIO()
    la_outturn.to_csv(outturn_buffer, encoding="cp1252")
    outturn_buffer.seek(0)
    year = 2024

    result = local_authority._build_section_251_data(
        budget_buffer,
        outturn_buffer,
        year,
    )

    assert len(result.index) == 1
