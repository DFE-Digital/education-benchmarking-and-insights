from unittest.mock import patch

import pandas as pd
import pytest

from pipeline.stats_collector import StatsCollector


class TestStatsCollector:
    """Test suite for StatsCollector class."""

    @pytest.fixture
    def stats_collector(self):
        return StatsCollector()

    @pytest.fixture
    def sample_school_df(self):
        return pd.DataFrame(
            {
                "School Name": ["School A", "School B", "School C", "School D"],
                "Overall Phase": ["Primary", "Secondary", "Primary", "All-through"],
            }
        )

    @pytest.fixture
    def empty_school_df(self):
        return pd.DataFrame(columns=["School Name", "Overall Phase"])

    @pytest.fixture
    def not_school_df(self):
        return pd.DataFrame(columns=["test", "test"])

    @pytest.fixture
    def sample_ancillary_data(self):
        return {
            "pupil_data": pd.DataFrame({"col1": [1, 2, 3], "col2": [4, 5, 6]}),
            "staff_data": pd.DataFrame({"col1": [1, 2], "col2": [3, 4]}),
            "empty_data": None,
        }

    def test_init(self, stats_collector):
        assert stats_collector.stats == {}

    def test_start_pipeline_run(self, stats_collector):
        stats_collector.stats = {"test": "data"}
        stats_collector.start_pipeline_run()

        expected_stats = {
            "financial_data": {
                "academies": {"AAR": {}, "ancillary_data": {}},
                "la_maintained_schools": {"CFR": {}, "ancillary_data": {}},
                "combined_schools": {}
            },
        }
        assert stats_collector.stats == expected_stats

    def test_reset(self, stats_collector):
        stats_collector.stats = {"test": "data"}
        stats_collector.reset()

        expected_stats = {
            "financial_data": {
                "academies": {"AAR": {}, "ancillary_data": {}},
                "la_maintained_schools": {"CFR": {}, "ancillary_data": {}},
                "combined_schools": {}
            },
        }
        assert stats_collector.stats == expected_stats

    def test_generate_school_counts_normal(self, stats_collector, sample_school_df):
        result = stats_collector._generate_school_counts(sample_school_df)

        expected = {
            "total": 4,
            "by_phase": {"Primary": 2, "Secondary": 1, "All-through": 1},
        }
        assert result == expected

    def test_generate_school_counts_empty_df(self, stats_collector, empty_school_df):
        result = stats_collector._generate_school_counts(empty_school_df)

        expected = {"total": 0, "by_phase": {}}
        assert result == expected

    def test_generate_school_counts_custom_phase_col(self, stats_collector):
        df = pd.DataFrame(
            {
                "School Name": ["School A", "School B"],
                "Custom Phase": ["Type1", "Type2"],
            }
        )

        result = stats_collector._generate_school_counts(df, phase_col="Custom Phase")

        expected = {"total": 2, "by_phase": {"Type1": 1, "Type2": 1}}
        assert result == expected

    def test_generate_school_counts_missing_phase_col(
        self, stats_collector, sample_school_df
    ):
        with pytest.raises(KeyError):
            stats_collector._generate_school_counts(
                sample_school_df, phase_col="NonExistent"
            )

    def test_collect_aar_academy_counts(self, stats_collector, sample_school_df):
        stats_collector.reset()
        aar_year = 2024
        
        stats_collector.collect_aar_academy_counts(sample_school_df, aar_year)

        expected = {
            "total": 4,
            "by_phase": {"Primary": 2, "Secondary": 1, "All-through": 1},
            "year": 2024
        }
        assert stats_collector.stats["financial_data"]["academies"]["AAR"] == expected

    def test_collect_aar_academy_counts_keyerror(self, stats_collector, not_school_df):
        """Test collecting AAR academy counts when phase column doesn't exist."""
        stats_collector.reset()
        
        with pytest.raises(KeyError):
            stats_collector.collect_aar_academy_counts(not_school_df, 2024)

    def test_collect_cfr_la_maintained_school_counts(
        self, stats_collector, sample_school_df
    ):
        """Test collecting CFR LA maintained school counts."""
        stats_collector.reset()
        cfr_year = 2024

        stats_collector.collect_cfr_la_maintained_school_counts(sample_school_df, cfr_year)

        expected = {
            "total": 4,
            "by_phase": {"Primary": 2, "Secondary": 1, "All-through": 1},
            "year": 2024
        }
        assert stats_collector.stats["financial_data"]["la_maintained_schools"]["CFR"] == expected

    def test_collect_combined_school_counts(self, stats_collector, sample_school_df):
        stats_collector.reset()
        stats_collector.collect_combined_school_counts(sample_school_df)

        expected = {
            "total": 4,
            "by_phase": {"Primary": 2, "Secondary": 1, "All-through": 1},
        }
        assert stats_collector.stats["financial_data"]["combined_schools"] == expected

    def test_collect_preprocessed_ancillary_data_shapes_private_method(
        self, stats_collector, sample_ancillary_data
    ):
        """Test the private method _collect_preprocessed_ancillary_data_shapes."""
        stats_collector.reset()
        
        stats_collector._collect_preprocessed_ancillary_data_shapes("academies", sample_ancillary_data, 2024)

        expected = {
            "year": 2024,
            "pupil_data": 3,
            "staff_data": 2,
        }
        assert stats_collector.stats["financial_data"]["academies"]["ancillary_data"] == expected

    def test_collect_aar_ancillary_data_shapes(
        self, stats_collector, sample_ancillary_data
    ):
        """Test collecting AAR ancillary data shapes."""
        stats_collector.reset()
        
        stats_collector.collect_aar_ancillary_data_shapes(sample_ancillary_data, 2024)

        expected = {
            "year": 2024,
            "pupil_data": 3,
            "staff_data": 2,
        }
        assert stats_collector.stats["financial_data"]["academies"]["ancillary_data"] == expected

    def test_collect_cfr_ancillary_data_shapes(
        self, stats_collector, sample_ancillary_data
    ):
        """Test collecting CFR ancillary data shapes."""
        stats_collector.reset()
        
        stats_collector.collect_cfr_ancillary_data_shapes(sample_ancillary_data, 2024)

        expected = {
            "year": 2024,
            "pupil_data": 3,
            "staff_data": 2,
        }
        assert stats_collector.stats["financial_data"]["la_maintained_schools"]["ancillary_data"] == expected

    def test_collect_ancillary_data_shapes_with_none_values(self, stats_collector):
        """Test ancillary data collection handles None values correctly."""
        stats_collector.reset()
        
        ancillary_data_with_none = {
            "valid_data": pd.DataFrame({"col1": [1, 2]}),
            "none_data": None,
        }
        
        stats_collector.collect_aar_ancillary_data_shapes(ancillary_data_with_none, 2023)
        
        expected = {"year": 2023, "valid_data": 2}
        assert stats_collector.stats["financial_data"]["academies"]["ancillary_data"] == expected

    @patch("pipeline.stats_collector.stats_logger")
    def test_get_stats_with_complete_data(self, mock_logger, stats_collector):
        """Test get_stats when all data exists."""
        stats_collector.reset()
        stats_collector.stats["financial_data"]["academies"]["AAR"] = {"total": 10}
        stats_collector.stats["financial_data"]["la_maintained_schools"]["CFR"] = {"total": 5}

        result = stats_collector.get_stats()

        assert result == stats_collector.stats
        mock_logger.info.assert_not_called()

    @patch("pipeline.stats_collector.stats_logger")
    def test_get_stats_missing_aar_data(self, mock_logger, stats_collector):
        """Test get_stats when AAR data is missing."""
        stats_collector.reset()
        stats_collector.stats["financial_data"]["la_maintained_schools"]["CFR"] = {"total": 5}

        result = stats_collector.get_stats()

        assert result == stats_collector.stats
        mock_logger.info.assert_called_with("No AAR data counts have been logged")

    @patch("pipeline.stats_collector.stats_logger")
    def test_get_stats_missing_cfr_data(self, mock_logger, stats_collector):
        """Test get_stats when CFR data is missing."""
        stats_collector.reset()
        stats_collector.stats["financial_data"]["academies"]["AAR"] = {"total": 10}

        result = stats_collector.get_stats()

        assert result == stats_collector.stats
        mock_logger.info.assert_called_with("No CFR data counts have been logged")

    @patch("pipeline.stats_collector.stats_logger")
    def test_get_stats_missing_both_data_types(self, mock_logger, stats_collector):
        """Test get_stats when both AAR and CFR data are missing."""
        stats_collector.reset()

        result = stats_collector.get_stats()

        assert result == stats_collector.stats
        # Should be called twice - once for AAR, once for CFR
        assert mock_logger.info.call_count == 2
        mock_logger.info.assert_any_call("No AAR data counts have been logged")
        mock_logger.info.assert_any_call("No CFR data counts have been logged")

    def test_phase_column_with_nan_values(self, stats_collector):
        """Test handling of NaN values in phase column."""
        df_with_nan = pd.DataFrame(
            {
                "School Name": ["School A", "School B", "School C"],
                "Overall Phase": ["Primary", None, "Secondary"],
            }
        )

        result = stats_collector._generate_school_counts(df_with_nan)

        # pandas value_counts() excludes NaN by default
        expected = {"total": 3, "by_phase": {"Primary": 1, "Secondary": 1}}
        assert result == expected

    def test_integration_workflow(self, stats_collector):
        """Test a complete workflow integration."""
        stats_collector.start_pipeline_run()

        # Collect AAR academy data
        academy_df = pd.DataFrame(
            {
                "School Name": ["Academy A", "Academy B"],
                "Overall Phase": ["Primary", "Secondary"],
            }
        )
        stats_collector.collect_aar_academy_counts(academy_df, 2024)

        # Collect CFR LA maintained school data
        la_df = pd.DataFrame(
            {"School Name": ["LA School A"], "Overall Phase": ["Primary"]}
        )
        stats_collector.collect_cfr_la_maintained_school_counts(la_df, 2024)

        # Collect combined school data
        combined_df = pd.DataFrame(
            {
                "School Name": ["All School A", "All School B", "All School C"],
                "Overall Phase": ["Primary", "Primary", "Secondary"],
            }
        )
        stats_collector.collect_combined_school_counts(combined_df)

        # Collect ancillary data
        aar_ancillary = {"pupil_data": pd.DataFrame({"col1": [1, 2, 3]})}
        cfr_ancillary = {"staff_data": pd.DataFrame({"col1": [1, 2]})}
        
        stats_collector.collect_aar_ancillary_data_shapes(aar_ancillary, 2024)
        stats_collector.collect_cfr_ancillary_data_shapes(cfr_ancillary, 2024)

        # Get final stats
        with patch("pipeline.stats_collector.stats_logger"):
            final_stats = stats_collector.get_stats()

        expected_stats = {
            "financial_data": {
                "academies": {
                    "AAR": {
                        "total": 2, 
                        "by_phase": {"Primary": 1, "Secondary": 1},
                        "year": 2024
                    },
                    "ancillary_data": {"year": 2024, "pupil_data": 3}
                },
                "la_maintained_schools": {
                    "CFR": {
                        "total": 1, 
                        "by_phase": {"Primary": 1},
                        "year": 2024
                    },
                    "ancillary_data": {"year": 2024, "staff_data": 2}
                },
                "combined_schools": {
                    "total": 3,
                    "by_phase": {"Primary": 2, "Secondary": 1}
                }
            },
        }
        assert final_stats == expected_stats