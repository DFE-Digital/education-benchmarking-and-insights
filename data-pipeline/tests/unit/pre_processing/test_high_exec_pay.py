from io import StringIO

import pandas as pd
import pytest

from pipeline.pre_processing import build_high_exec_pay_data


@pytest.fixture
def test_output() -> pd.DataFrame:
    test_data = (
        """EntityHierarchy,EMLBand\nC07682294,EMLBANDS200\nC08619729,EMLBANDS130"""
    )
    return build_high_exec_pay_data(StringIO(test_data), 2024)


def test_high_exec_pay_column_names(test_output):
    expected_columns = ["Company Registration Number", "EMLBand"]
    assert list(test_output.columns) == expected_columns


def test_high_exec_pay_company_number_has_c_stripped(test_output):
    assert not test_output["Company Registration Number"].str.contains("C").any()
