from io import StringIO

import pandas as pd
import pytest

from src.pipeline.pre_processing import prepare_cdc_data

data = pd.DataFrame({
        'Site': ['Site A', 'Site A', 'Site A', 'Site A', 'Site B'],
        'URN': [100150, 100150, 100150, 100150, 100162],
        'SurveySectionSectionType': ['ANCILLARY', 'BLOCK', 'BLOCK', 'BLOCK', 'ANCILLARY'],
        'GIFA': [89, 2221, 436, 57, 131],
        'Block Age': ['NULL', '1961-1970', '2001-2010', '1991-2000', 'NULL']
    })

actual = prepare_cdc_data(StringIO(data.to_csv()), 2022).reset_index().to_dict('records')[0]


def test_cdc_has_correct_index():
    assert actual['URN'] == 100150


def test_cdc_has_correct_total_internal_floor_area():
    assert actual['Total Internal Floor Area'] == 2803.0


def test_cdc_has_correct_age_average_score():
    assert pytest.approx(actual['Age Average Score'], 0.001) == 48.358
