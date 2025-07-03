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
        unique_schools_per_phase: dict[str, int] = (
            school_df[phase_col].value_counts().to_dict()
        )

        return {"total": unique_school_count, "by_phase": unique_schools_per_phase}

    def collect_academy_counts(self, academies_data_preprocessed):
        self.stats.setdefault("school_counts", {})
        self.stats["school_counts"]["academies"] = self._generate_school_counts(
            academies_data_preprocessed
        )

    def collect_la_maintained_school_counts(self, la_maintained_schools_data_preprocessed):
        self.stats.setdefault("school_counts", {})
        self.stats["school_counts"]["la_maintained_schools"] = (
            self._generate_school_counts(la_maintained_schools_data_preprocessed)
        )

    def collect_combined_school_counts(self, combined_schools_data_preprocessed):
        self.stats.setdefault("school_counts", {})
        self.stats["school_counts"]["combined_schools"] = self._generate_school_counts(
            combined_schools_data_preprocessed
        )

    def collect_preprocessed_ancillary_data_shape(self, name: str, shape: tuple[int, int]):
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