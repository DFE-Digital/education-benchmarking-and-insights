"""Minimal mock module for pyspark.pipelines to enable local testing."""

from typing import Callable
from unittest.mock import MagicMock


class MockPipelinesModule:
    """Minimal mock implementation of the pyspark.pipelines module for testing."""
    
    def table(self, *args, **kwargs):
        """Mock decorator for @dp.table() - just returns the function unchanged"""
        def decorator(func: Callable) -> Callable:
            return func
        return decorator
    
    def view(self, *args, **kwargs):
        """Mock decorator for @dp.view() - just returns the function unchanged"""
        def decorator(func: Callable) -> Callable:
            return func
        return decorator
    
    def read(self, table_name: str):
        """Mock implementation of dp.read() - returns a basic mock"""
        mock_df = MagicMock()
        mock_df.sparkSession.conf.get.return_value = "2025"  # Default pipeline year
        return mock_df


# Create a global instance to be used as the mock
mock_pipelines = MockPipelinesModule()
