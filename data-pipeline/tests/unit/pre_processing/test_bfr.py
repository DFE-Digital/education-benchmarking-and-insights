import numpy as np
import pandas as pd
import pytest
import src.pipeline.pre_processing as pre_processing
import src.pipeline.bfr as BFR


def test_bfr_metric_data_has_correct_output_columns(prepared_bfr_data: pd.DataFrame):
    assert list(prepared_bfr_data[1].columns) == [
        'Category', 'Value', 'Trust UPIN', 'value'
    ]


def test_bfr_output_data_has_correct_output_columns(
    prepared_bfr_data: pd.DataFrame,
):
    assert list(prepared_bfr_data[0].columns) == [
        'Category', 'Year', 'Value', 'Pupils'
    ]


def test_bfr_slop_calc():
    actual = BFR.calculate_slopes(np.array([[2, 4, 6, 8, 10, 12]]))
    assert [2] == actual