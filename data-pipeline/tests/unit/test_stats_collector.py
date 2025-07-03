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

    def test_init(self, stats_collector):
        assert stats_collector.stats == {}
        assert not hasattr(stats_collector, "pipeline_start_time")

    def test_start_pipeline_run(self, stats_collector):
        stats_collector.stats = {"test": "data"}
        stats_collector.start_pipeline_run()

        assert stats_collector.stats == {}

    def test_reset(self, stats_collector):
        stats_collector.stats = {"test": "data"}
        stats_collector.reset()

        assert stats_collector.stats == {}

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

    def test_log_academy_counts(self, stats_collector, sample_school_df):
        stats_collector.stats["school_counts"] = {}
        stats_collector.log_academy_counts(sample_school_df)

        expected = {
            "total": 4,
            "by_phase": {"Primary": 2, "Secondary": 1, "All-through": 1},
        }
        assert stats_collector.stats["school_counts"]["academies"] == expected

    def test_log_academy_counts_keyerror(self, stats_collector, not_school_df):
        """Test logging academy counts when school_counts key doesn't exist."""
        with pytest.raises(KeyError):
            stats_collector.log_academy_counts(not_school_df)

    def test_log_la_maintained_school_counts(self, stats_collector, sample_school_df):
        """Test logging LA maintained school counts."""
        stats_collector.stats["school_counts"] = {}

        stats_collector.log_la_maintained_school_counts(sample_school_df)

        expected = {
            "total": 4,
            "by_phase": {"Primary": 2, "Secondary": 1, "All-through": 1},
        }
        assert (
            stats_collector.stats["school_counts"]["la_maintained_schools"] == expected
        )

    def test_log_combined_school_counts(self, stats_collector, sample_school_df):
        stats_collector.stats["school_counts"] = {}
        stats_collector.log_combined_school_counts(sample_school_df)

        expected = {
            "total": 4,
            "by_phase": {"Primary": 2, "Secondary": 1, "All-through": 1},
        }
        assert stats_collector.stats["school_counts"]["combined_schools"] == expected

    @patch("pipeline.stats_collector.stats_logger")
    def test_log_preprocessed_ancillary_data_shape(self, mock_logger, stats_collector):
        name, shape = "test_data", (100, 5)
        stats_collector.log_preprocessed_ancillary_data_shape(name, shape)

        # Check logging call
        mock_logger.info.assert_called_once_with(f"{name=} preprocessed with {shape=}")

        # Check stats structure
        assert "linked_data_school_counts" in stats_collector.stats
        assert name in stats_collector.stats["linked_data_school_counts"]
        assert stats_collector.stats["linked_data_school_counts"][name]["total"] == 100

    @patch("pipeline.stats_collector.stats_logger")
    def test_log_preprocessed_ancillary_data_shape_multiple_calls(
        self, mock_logger, stats_collector
    ):
        """Test multiple calls to log_preprocessed_ancillary_data_shape."""
        stats_collector.log_preprocessed_ancillary_data_shape("data1", (50, 3))
        stats_collector.log_preprocessed_ancillary_data_shape("data2", (75, 4))

        expected_stats = {
            "linked_data_school_counts": {
                "data1": {"total": 50},
                "data2": {"total": 75},
            }
        }
        assert stats_collector.stats == expected_stats
        assert mock_logger.info.call_count == 2

    @patch("pipeline.stats_collector.stats_logger")
    def test_get_stats_with_school_counts(self, mock_logger, stats_collector):
        """Test get_stats when school_counts exist."""
        stats_collector.stats = {
            "school_counts": {"academies": {"total": 10}},
            "linked_data_counts": {"test": 5},
        }

        result = stats_collector.get_stats()

        assert result == stats_collector.stats
        mock_logger.info.assert_not_called()

    @patch("pipeline.stats_collector.stats_logger")
    def test_get_stats_missing_school_counts(self, mock_logger, stats_collector):
        """Test get_stats when school_counts are missing."""
        stats_collector.stats = {"linked_data_counts": "value"}

        result = stats_collector.get_stats()

        assert result == stats_collector.stats
        mock_logger.info.assert_called_with("School counts have not been logged")

    @patch("pipeline.stats_collector.stats_logger")
    def test_get_stats_missing_linked_data_counts(self, mock_logger, stats_collector):
        """Test get_stats when linked_data_counts are missing."""
        stats_collector.stats = {"school_counts": {"test": 1}}

        result = stats_collector.get_stats()

        assert result == stats_collector.stats
        mock_logger.info.assert_called_with("Linked data counts have not been logged")

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
        stats_collector.start_pipeline_run()

        academy_df = pd.DataFrame(
            {
                "School Name": ["Academy A", "Academy B"],
                "Overall Phase": ["Primary", "Secondary"],
            }
        )
        stats_collector.log_academy_counts(academy_df)

        la_df = pd.DataFrame(
            {"School Name": ["LA School A"], "Overall Phase": ["Primary"]}
        )
        stats_collector.log_la_maintained_school_counts(la_df)

        with patch("pipeline.stats_collector.stats_logger"):
            stats_collector.log_preprocessed_ancillary_data_shape(
                "pupil_data", (1000, 10)
            )

        # Get final stats
        with patch("pipeline.stats_collector.stats_logger"):
            final_stats = stats_collector.get_stats()

        expected_stats = {
            "school_counts": {
                "academies": {"total": 2, "by_phase": {"Primary": 1, "Secondary": 1}},
                "la_maintained_schools": {"total": 1, "by_phase": {"Primary": 1}},
            },
            "linked_data_school_counts": {"pupil_data": {"total": 1000}},
        }
        assert final_stats == expected_stats
