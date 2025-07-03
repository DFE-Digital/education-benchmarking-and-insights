import logging
import threading
import uuid
from datetime import datetime
from typing import Any, Dict, Optional

import pandas as pd

stats_logger = logging.getLogger("stats")


class StatsCollector:
    def __init__(self):
        self.stats = {}

    def start_pipeline_run(self):
        self.reset()

    def reset(self):
        self.stats = {}

    def _generate_school_counts(
        self, school_df: pd.DataFrame, phase_col: str = "Overall Phase"
    ) -> dict:
        unique_school_count: int = school_df.shape[0]
        unique_schools_per_phase_dict: dict[str, int] = (
            school_df[phase_col].value_counts().to_dict()
        )

        return {"total": unique_school_count, "by_phase": unique_schools_per_phase_dict}

    def log_academy_counts(self, academies_data_preprocessed):
        self.stats.setdefault("school_counts", {})
        self.stats["school_counts"]["academies"] = self._generate_school_counts(
            academies_data_preprocessed
        )

    def log_la_maintained_school_counts(self, la_maintained_schools_data_preprocessed):
        self.stats.setdefault("school_counts", {})
        self.stats["school_counts"]["la_maintained_schools"] = (
            self._generate_school_counts(la_maintained_schools_data_preprocessed)
        )

    def log_combined_school_counts(self, combined_schools_data_preprocessed):
        self.stats.setdefault("school_counts", {})
        self.stats["school_counts"]["combined_schools"] = self._generate_school_counts(
            combined_schools_data_preprocessed
        )

    def log_preprocessed_ancillary_data_shape(self, name: str, shape: tuple[int, int]):
        stats_logger.info(f"{name=} preprocessed with {shape=}")
        self.stats.setdefault("linked_data_school_counts", {})
        self.stats["linked_data_school_counts"].setdefault(name, {})
        self.stats["linked_data_school_counts"][name]["total"] = shape[0]

    def get_stats(self):
        if "school_counts" not in self.stats.keys():
            stats_logger.info("School counts have not been logged")
        if "linked_data_counts" not in self.stats.keys():
            stats_logger.info("Linked data counts have not been logged")
        return self.stats


# class StatsCollector(logging.LoggerAdapter):
#     """Stats collector using LoggerAdapter for thread-safe operation"""

#     _instance = None

#     def __new__(cls):
#         if cls._instance is None:
#             # Create the base logger
#             logger = logging.getLogger('pipeline_stats')
#             logger.setLevel(logging.INFO)

#             # Create the adapter instance
#             cls._instance = super().__new__(cls)
#             cls._instance.__init__(logger, {})
#             cls._instance._initialized = True
#             cls._instance.reset()
#         return cls._instance

#     def __init__(self, logger, extra):
#         if not hasattr(self, '_initialized'):
#             super().__init__(logger, extra)

#     def reset(self):
#         """Reset stats for new pipeline run"""
#         self.stats = {
#             "school_counts": {},
#             "linked_data_school_counts": {},
#             "row_counts": {},
#         }
#         self.run_id = None
#         self.pipeline_start_time = None

#     def start_pipeline_run(self, run_id: Optional[str] = None) -> str:
#         """Start a new pipeline run"""
#         self.reset()
#         self.run_id = run_id or str(uuid.uuid4())
#         self.pipeline_start_time = datetime.now()
#         self.info(f"Started pipeline run: {self.run_id}")
#         return self.run_id

#     def log_academy_counts(self, academies_data_preprocessed):
#         """Log academy counts"""
#         count = self._generate_school_counts(academies_data_preprocessed)
#         self.stats["school_counts"]["academies"] = count
#         self.info(f"Academy counts logged: {count}")

#     def log_preprocessed_ancillary_data_shape(self, name: str, shape: tuple[int, int]):
#         """Log preprocessed ancillary data shape"""
#         if name not in self.stats["linked_data_school_counts"]:
#             self.stats["linked_data_school_counts"][name] = {}
#         self.stats["linked_data_school_counts"][name]["total"] = shape[0]
#         self.info(f"Ancillary data shape logged - {name}: {shape}")

#     def get_stats(self) -> Dict[str, Any]:
#         """Get all collected statistics"""
#         stats = {}
#         stats.update(self.stats)

#         if self.pipeline_start_time:
#             stats['_pipeline_duration'] = (datetime.now() - self.pipeline_start_time).total_seconds()
#             stats['_run_id'] = self.run_id
#             stats['_start_time'] = self.pipeline_start_time.isoformat()

#         self.info(f"Stats retrieved: {len(stats)} categories")
#         return stats

#     def _generate_school_counts(self, data):
#         """Generate school counts from data"""
#         return len(data) if hasattr(data, '__len__') else 0

#     def process(self, msg, kwargs):
#         """Process log messages (required by LoggerAdapter)"""
#         return f"[STATS] {msg}", kwargs


stats_collector = StatsCollector()
