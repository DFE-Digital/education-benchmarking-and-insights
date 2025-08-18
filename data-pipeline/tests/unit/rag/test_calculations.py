from unittest.mock import patch

import numpy as np
import pandas as pd
import pytest

from pipeline.config import rag_category_settings
from pipeline.rag.calculations import (
    BASE_COLUMNS,
    CategoryColumnCache,
    ComparatorSetCache,
    are_building_characteristics_similar,
    are_pupil_demographics_similar,
    calculate_percentile_rank,
    calculate_rag,
    compute_user_defined_rag,
    determine_similarity_comparator,
    get_positive_values_series,
    prepare_data_for_rag_calculation,
    should_skip_school_processing,
)


class TestCategoryColumnCache:
    """Test suite for the CategoryColumnCache class."""

    @pytest.fixture
    def sample_cache(self, sample_columns):
        return CategoryColumnCache(sample_columns)

    def test_initialization(self, sample_cache):
        """Test that the cache is initialized correctly."""
        assert sample_cache.base_columns_mask is not None
        assert isinstance(sample_cache.category_mappings, dict)
        # Ensures all categories from settings are present in the map
        assert len(sample_cache.category_mappings) == len(rag_category_settings)

    def test_base_columns_mask_correctness(self, sample_cache, sample_columns):
        """Test that the base columns mask is created correctly."""
        expected_mask = sample_columns.isin(BASE_COLUMNS)
        assert np.array_equal(sample_cache.base_columns_mask, expected_mask)
        assert sample_cache.base_columns_mask.sum() == len(BASE_COLUMNS)

    def test_category_mappings_structure(self, sample_cache):
        """Test the structure of the category_mappings dictionary."""
        # Check one entry for the correct keys
        a_category = list(rag_category_settings.keys())[0]
        assert a_category in sample_cache.category_mappings
        assert "column_mask" in sample_cache.category_mappings[a_category]
        assert "subcategories" in sample_cache.category_mappings[a_category]

    def test_column_mask_for_specific_category(self, sample_cache, sample_columns):
        """Test the combined column_mask for a specific category."""
        category_name = "Teaching and Teaching support staff"
        category_mask = sample_cache.category_mappings[category_name]["column_mask"]

        # The mask should select all base columns plus the specific category column
        expected_columns = sorted(BASE_COLUMNS + [f"{category_name}_Per Unit"])
        actual_columns = sorted(sample_columns[category_mask].tolist())

        assert actual_columns == expected_columns

    def test_subcategories_list_identification(self, sample_cache):
        """Test that subcategories are correctly identified for a present category."""
        category_name = "Utilities"
        subcategories = sample_cache.category_mappings[category_name]["subcategories"]
        assert subcategories == [f"{category_name}_Per Unit"]

    def test_subcategories_for_missing_category(self, sample_cache):
        """Test that subcategories list is empty for a category not in the columns."""
        # This category exists in settings but not in our sample_columns
        category_name = "Educational ICT"
        subcategories = sample_cache.category_mappings[category_name]["subcategories"]
        assert subcategories == []


class TestComparatorSetCache:
    """Test suite for the ComparatorSetCache class."""

    @pytest.fixture(autouse=True)
    def setup_cache(self, sample_data_for_comparators):
        """Initialize the cache for each test in this class."""
        df, comparators = sample_data_for_comparators
        self.cache = ComparatorSetCache(df, comparators)

    def test_cache_building_on_init(self):
        """Test that the internal cache is built correctly upon initialization."""
        # Schools in both dataframe and comparators should be cached
        assert "1" in self.cache._cache
        assert "2" in self.cache._cache
        # URN '999' is in comparators but not in the dataframe index, so it should be ignored
        assert "999" not in self.cache._cache
        # URN '3' is in the dataframe but not a primary key in comparators, so no entry
        assert "3" not in self.cache._cache

        # Check structure of a cached item
        assert "Pupil" in self.cache._cache["1"]
        assert "Building" in self.cache._cache["1"]

    def test_get_comparator_set_success(self):
        """Test retrieving a valid 'Pupil' and 'Building' comparator set."""
        pupil_set = self.cache.get_comparator_set("1", "Pupil")
        assert isinstance(pupil_set, pd.DataFrame)
        assert pupil_set.index.tolist() == ["2", "3"]

        building_set = self.cache.get_comparator_set("1", "Building")
        assert isinstance(building_set, pd.DataFrame)
        assert building_set.index.tolist() == ["4", "5"]

    def test_get_comparator_set_with_empty_list(self):
        """Test retrieving a set where the comparator URN list was empty."""
        # The cache should store None for this entry during the build
        assert self.cache._cache["2"]["Pupil"] is None
        # Therefore, the get method should return None
        pupil_set = self.cache.get_comparator_set("2", "Pupil")
        assert pupil_set is None

    def test_get_comparator_set_for_unknown_school(self):
        """Test requesting a comparator set for a URN that was never in comparators."""
        result = self.cache.get_comparator_set("3", "Pupil")
        assert result is None

    def test_get_comparator_set_for_unindexed_school(self):
        """Test requesting a set for a school that was not in the dataframe index."""
        result = self.cache.get_comparator_set("999", "Pupil")
        assert result is None

    def test_get_comparator_set_for_unknown_type(self):
        """Test requesting a comparator set with an invalid comparator type."""
        result = self.cache.get_comparator_set("1", "InvalidType")
        assert result is None


def test_are_building_characteristics_similar_true():
    school1 = pd.Series({"Total Internal Floor Area": 1000, "Age Average Score": 50})
    school2 = pd.Series({"Total Internal Floor Area": 1050, "Age Average Score": 60})
    assert are_building_characteristics_similar(school1, school2)


def test_are_building_characteristics_similar_false_due_to_area():
    school1 = pd.Series({"Total Internal Floor Area": 1000, "Age Average Score": 50})
    school2 = pd.Series({"Total Internal Floor Area": 1200, "Age Average Score": 60})
    assert not are_building_characteristics_similar(school1, school2)


def test_are_building_characteristics_similar_false_due_to_age():
    school1 = pd.Series({"Total Internal Floor Area": 1000, "Age Average Score": 50})
    school2 = pd.Series({"Total Internal Floor Area": 1050, "Age Average Score": 80})
    assert not are_building_characteristics_similar(school1, school2)


def test_are_pupil_demographics_similar_true():
    school1 = pd.Series(
        {
            "Number of pupils": 100,
            "Percentage Free school meals": 0.1,
            "Percentage SEN": 0.05,
        }
    )
    school2 = pd.Series(
        {
            "Number of pupils": 120,
            "Percentage Free school meals": 0.105,
            "Percentage SEN": 0.055,
        }
    )
    assert are_pupil_demographics_similar(school1, school2)


def test_are_pupil_demographics_similar_false_due_to_pupils():
    school1 = pd.Series(
        {
            "Number of pupils": 100,
            "Percentage Free school meals": 0.1,
            "Percentage SEN": 0.05,
        }
    )
    school2 = pd.Series(
        {
            "Number of pupils": 126,  # Over the 25% tolerance
            "Percentage Free school meals": 0.101,
            "Percentage SEN": 0.0501,
        }
    )
    assert are_pupil_demographics_similar(school1, school2) is False


def test_are_pupil_demographics_similar_false_due_to_fsm():
    school1 = pd.Series(
        {
            "Number of pupils": 100,
            "Percentage Free school meals": 0.1,
            "Percentage SEN": 0.05,
        }
    )
    school2 = pd.Series(
        {
            "Number of pupils": 101,
            "Percentage Free school meals": 0.094,  # 5% threshold
            "Percentage SEN": 0.0501,
        }
    )
    assert are_pupil_demographics_similar(school1, school2) is False


def test_are_pupil_demographics_similar_false_due_to_sen():
    school1 = pd.Series(
        {
            "Number of pupils": 100,
            "Percentage Free school meals": 0.1,
            "Percentage SEN": 0.05,
        }
    )
    school2 = pd.Series(
        {
            "Number of pupils": 101,
            "Percentage Free school meals": 0.1,
            "Percentage SEN": 0.01,
        }
    )
    assert are_pupil_demographics_similar(school1, school2) is False


@patch("pipeline.rag.calculations.are_building_characteristics_similar")
def test_determine_similarity_comparator_building(mock_building_similar):
    mock_building_similar.return_value = True
    school1 = pd.Series()
    school2 = pd.Series()
    assert determine_similarity_comparator("Building", school1, school2)
    mock_building_similar.assert_called_once_with(school1, school2)


@patch("pipeline.rag.calculations.are_pupil_demographics_similar")
def test_determine_similarity_comparator_pupil(mock_pupil_similar):
    mock_pupil_similar.return_value = True
    school1 = pd.Series()
    school2 = pd.Series()
    assert determine_similarity_comparator("Other", school1, school2)
    mock_pupil_similar.assert_called_once_with(school1, school2)


def test_calculate_percentile_rank():
    data = pd.Series([10, 20, 30, 40, 50])
    assert calculate_percentile_rank(data, 25) == 50.0


def test_calculate_percentile_rank_empty_series():
    assert calculate_percentile_rank(pd.Series([], dtype=float), 10) == 0.0


def test_get_positive_values_series(sample_school_data):
    series = get_positive_values_series(
        sample_school_data, "CategoryA_SubCategory1_Per Unit", 3
    )
    assert 3 in series.index
    assert all(series.drop(3) > 0)


def test_prepare_data_for_rag_calculation(sample_school_data):
    prepared_data = prepare_data_for_rag_calculation(sample_school_data)
    assert "Extra_Column" not in prepared_data.columns
    assert prepared_data.notna().all().all()


def test_should_skip_school_processing():
    assert should_skip_school_processing(pd.Series({"Partial Years Present": True}))
    assert not should_skip_school_processing(
        pd.Series({"Partial Years Present": False})
    )


def test_calculate_rag(sample_school_data, sample_data_for_comparators):
    _, comparators = sample_data_for_comparators
    results = list(calculate_rag(sample_school_data, comparators, target_urn=1))
    assert len(results) > 0
    assert results[0]["URN"] == 1
    assert "RAG" in results[0]


def test_calculate_rag_skip_partial_year(
    sample_school_data, sample_data_for_comparators
):
    _, comparators = sample_data_for_comparators
    results = list(calculate_rag(sample_school_data, comparators, target_urn=3))
    assert len(results) == 0


def test_compute_user_defined_rag(sample_school_data):
    results = list(
        compute_user_defined_rag(
            sample_school_data, target_urn=1, comparator_urns=[2, 4, 5]
        )
    )
    assert len(results) > 0
    assert results[0]["URN"] == 1
    assert "Percentile" in results[0]


def test_compute_user_defined_rag_target_not_in_data(sample_school_data):
    results = list(
        compute_user_defined_rag(
            sample_school_data, target_urn=99, comparator_urns=[2, 4, 5]
        )
    )
    assert len(results) == 0
