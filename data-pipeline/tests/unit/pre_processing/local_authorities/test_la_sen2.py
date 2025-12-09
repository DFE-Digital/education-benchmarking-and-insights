import io

import pandas as pd
import pytest
import numpy as np

from pipeline.pre_processing.s251 import local_authority


def test_sen2(la_sen2: pd.DataFrame):
    """
    The 58 rows for a single LA should be pivoted to a single row.
    """
    result = local_authority._prepare_sen2_la_data(
        io.StringIO(la_sen2.to_csv()),
        2024,
    )

    assert len(la_sen2.index) == 58
    assert len(result.index) == 1


@pytest.fixture
def la_data():
    """Fixture for the local authority data with a MultiIndex."""
    data = {
        'new_la_code': ['E10000006', 'E0600006', 'E10000009', 'E1000096', 'E0600009'],
        'old_la_code': ['915', '320', '855', '815', '113'],
        'OtherCol1': [10, 20, 30, 40, 50]
    }
    df = pd.DataFrame(data).set_index(['new_la_code', 'old_la_code'])
    return df

@pytest.fixture
def sen_data_perfect_match():
    """Fixture for SEN data that matches perfectly on the full index."""
    data = {
        'new_la_code': ['E10000006', 'E0600006', 'E10000009', 'E0600009'],
        'old_la_code': ['915', '320', '855', '113'],
        'EHCPTotal': [100, 200, 300, 500],
        'OtherCol2': [1, 2, 3, 5]
    }
    df = pd.DataFrame(data).set_index(['new_la_code', 'old_la_code'])
    return df

@pytest.fixture
def sen_data_old_la_match():
    """
    Fixture for SEN data that ONLY matches on 'old_la_code' for some records.
    E1000096/815 in la_data fails first join (index-based) but succeeds second join (on old_la_code 815).
    """
    data = {
        # This will match E10000006/915
        ('E10000006', '915'): {'EHCPTotal': 100, 'OtherCol2': 1}, 
        # This will fail the index match for E1000096/815 because it's keyed as D99/815, 
        # but the second stage will match on '815'
        ('D99', '815'): {'EHCPTotal': 400, 'OtherCol2': 4}, 
        # This will match E0600009/113
        ('E0600009', '113'): {'EHCPTotal': 500, 'OtherCol2': 5}
    }
    
    # Converting to DataFrame with MultiIndex
    df = pd.DataFrame.from_dict(data, orient='index', columns=['EHCPTotal', 'OtherCol2'])
    df.index.names = ['new_la_code', 'old_la_code']
    return df


def test_first_stage_join_success(la_data, sen_data_perfect_match):
    """Test that records successfully join in the first, index-based stage."""
    # E10000009/855 from la_data is NOT in sen_data_perfect_match, so it should still be NaN
    expected_index = la_data.index
    
    result = local_authority._join_sen_to_local_authority_data(la_data, sen_data_perfect_match)
    
    assert result.index.names == la_data.index.names
    assert len(result) == len(la_data)
    
    # Perfect matches
    assert result.loc[('E10000006', '915')]['EHCPTotal'] == 100
    assert result.loc[('E0600006', '320')]['EHCPTotal'] == 200
    assert result.loc[('E0600009', '113')]['EHCPTotal'] == 500
    
    # Check that E1000096 (no match) remains NaN (using index match)
    assert np.isnan(result.loc[('E1000096', '815')]['EHCPTotal'])


def test_two_stage_join_success(la_data, sen_data_old_la_match):
    """A record failing stage 1 successfully joins in stage 2."""
    
    result = local_authority._join_sen_to_local_authority_data(la_data, sen_data_old_la_match)

    assert len(result) == len(la_data)

    assert result.loc[('E10000006', '915')]['EHCPTotal'] == 100
    
    # This only succeeds at join 2
    assert result.loc[('E1000096', '815')]['EHCPTotal'] == 400
    
    # Fails both
    assert np.isnan(result.loc[('E10000009', '855')]['EHCPTotal'])
    
    # 4. Check the index of the E1000096 record to ensure it maintained the original la_data index
    # (i.e., new_la_code is 'E1000096', not 'D99' from sen_data)
    assert 'E1000096' in result.index.get_level_values('new_la_code')
    assert 'D99' not in result.index.get_level_values('new_la_code')


def test_total_join_failure(la_data):
    """Test a scenario where the SEN data has no corresponding LA codes, failing both stages."""
    # Create an empty SEN data frame with the expected MultiIndex
    sen_data_empty = pd.DataFrame(
        columns=['EHCPTotal', 'OtherCol2'],
        index=pd.MultiIndex.from_tuples([], names=['new_la_code', 'old_la_code'])
    )
    
    result = local_authority._join_sen_to_local_authority_data(la_data, sen_data_empty)
    
    assert len(result) == len(la_data)
    assert result['EHCPTotal'].isnull().all()
    assert 'OtherCol2' in result.columns