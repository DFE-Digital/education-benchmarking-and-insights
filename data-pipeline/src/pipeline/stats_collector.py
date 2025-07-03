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


stats_collector = StatsCollector()
     """Get all collected statistics"""
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
