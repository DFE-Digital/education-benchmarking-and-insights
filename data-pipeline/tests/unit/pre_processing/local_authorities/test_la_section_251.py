import io

import pandas as pd
import pytest

from pipeline.pre_processing.s251 import local_authority

from .conftest import write_budget_2026_ods_bytes, write_outturn_2026_xlsx_bytes


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


def test_la_section_251_2026_joins_ods_and_xlsx_preserving_multiindex(
    la_budget_2026: pd.DataFrame,
    la_outturn_2026: pd.DataFrame,
):
    budget_buffer = write_budget_2026_ods_bytes(la_budget_2026)
    outturn_buffer = write_outturn_2026_xlsx_bytes(la_outturn_2026)

    result = local_authority._build_section_251_data(
        budget_buffer,
        outturn_buffer,
        year=2026,
    )

    assert list(result.index) == [("E10000000", 101)]
    assert result.index.names == ["new_la_code", "old_la_code"]
    assert result.shape == (1, 66)
    assert result.loc[("E10000000", 101), "BudgetTotalHighNeeds"] == pytest.approx(
        2600.0
    )
    assert result.loc[("E10000000", 101), "OutturnTotalHighNeeds"] == pytest.approx(
        3900.0
    )
