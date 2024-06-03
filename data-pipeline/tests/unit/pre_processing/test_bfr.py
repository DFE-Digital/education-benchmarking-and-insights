import pandas as pd
import pytest

def test_bfr_metric_data_has_correct_output_columns(prepared_bfr_data: pd.DataFrame):
    assert list(prepared_bfr_data[0].columns) == [
        'Revenue reserve as percentage of income',
        'Staff costs as percentage of income',
        'Expenditure as percentage of income',
        'percent self-generated income',
        'percent grant funding'
    ]


def test_bfr_revenue_reserves_data_has_correct_output_columns(prepared_bfr_data: pd.DataFrame):
    assert list(prepared_bfr_data[1].columns) == [ 
        'CreatedBy', 
        'Category',
        'Title',
        'EFALineNo',
        'Y1P1',
        'Y1P2',
        'Y2P1',
        'Y2P2',
        'Y1',
        'Y2',
        'Y3',
        'Y4',
        'Y-1',
        'Y-2',
        'slope',
        'slope_flag'
    ]

def test_bfr_revenue_reserves_per_pupil_data_has_correct_output_columns(prepared_bfr_data: pd.DataFrame):
    assert list(prepared_bfr_data[2].columns) == [ 
        'CreatedBy',
        'Category',
        'Title',
        'EFALineNo',
        'slope',
        'Y-2',
        'Y-1',
        'Y1',
        'Y2',
        'Y3',
        'Y4',
        'slope_flag'
    ]