import os
from abc import ABC, abstractmethod

from pyspark.sql import DataFrame
from pyspark.sql.functions import collect_list, hash, struct

from .logging import setup_logger

logger = setup_logger(__name__)


def hash_compare_view_dfs(
    table_id: str, view: DataFrame, materialized_view: DataFrame
) -> bool:
    """
    Compare the hash of the current view with the hash of the materialized view.
    If the hashes are different, the function will log a warning and return False.
    If the hashes are the same, the function will log an info message and return True.
    """
    current_hash = view.agg(
        hash(collect_list(hash(struct("*")))).alias("hash")
    ).collect()[0]["hash"]

    previous_hash = materialized_view.agg(
        hash(collect_list(hash(struct("*")))).alias("hash")
    ).collect()[0]["hash"]

    if current_hash != previous_hash:
        logger.warning(f"Upstream data in {table_id} differs from materialized_view")
        return False
    if current_hash == previous_hash:
        logger.info(f"Upstream data in {table_id} matches materialized_view")
        return True


class DatabricksFBITPipeline(ABC):
    @abstractmethod
    def run(self):
        pass

    @abstractmethod
    def load_data(self):
        pass

    @abstractmethod
    def load_ancillary_data(self):
        pass

    @abstractmethod
    def preprocess_data(self):
        pass

    @abstractmethod
    def save_data(self):
        pass

    @staticmethod
    def _check_for_updates_in_materialized_views(
        table_id: str, source_view: DataFrame, materialized_view: DataFrame
    ) -> None:
        """
        Check for updates in materialized views.
        If the hashes are different, the function will log a warning.
        If the hashes are the same, the function will log an info message.

        Args:
            table_id: The identifier for the table being checked.
            source_view: The DataFrame representing the source view.
            materialized_view: The DataFrame representing the materialized view.
        """
        try:
            hash_compare_view_dfs(table_id, source_view, materialized_view)
        except Exception as e:
            logger.error(f"Error comparing hashes for {table_id}: {e}")

    def _is_databricks(self):
        return "DATABRICKS_RUNTIME_VERSION" in os.environ

    def _get_table_name(self, full_table_name: str):
        if self._is_databricks():
            return full_table_name
        else:
            # For local execution, extract just the table name or adjust as needed
            # Assuming the format is catalog.schema.table
            parts = full_table_name.split(".")
            return parts[-1]
