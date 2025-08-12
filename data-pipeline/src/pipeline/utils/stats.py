import pandas as pd

from pipeline.utils.log import setup_logger

stats_logger = setup_logger(__name__)


class StatsCollector:
    def __init__(self):
        self.stats = {}

    def start_pipeline_run(self):
        self.reset()

    def reset(self):
        self.stats = {
            "financial_data": {
                "academies": {"AAR": {}, "ancillary_data": {}},
                "la_maintained_schools": {"CFR": {}, "ancillary_data": {}},
                "combined_schools": {},
            },
        }

    def _generate_school_counts(
        self, school_df: pd.DataFrame, phase_col: str = "Overall Phase"
    ) -> dict:
        unique_school_count: int = school_df.shape[0]
        unique_schools_per_phase: dict[str, int] = (
            school_df[phase_col].value_counts().to_dict()
        )

        return {"total": unique_school_count, "by_phase": unique_schools_per_phase}

    def collect_aar_academy_counts(
        self, aar_academies_data_preprocessed, aar_year
    ) -> None:
        self.stats["financial_data"]["academies"]["AAR"] = self._generate_school_counts(
            aar_academies_data_preprocessed
        )
        self.stats["financial_data"]["academies"]["AAR"]["year"] = aar_year

    def collect_cfr_la_maintained_school_counts(
        self, la_maintained_schools_data_preprocessed, cfr_year
    ) -> None:
        self.stats["financial_data"]["la_maintained_schools"]["CFR"] = (
            self._generate_school_counts(la_maintained_schools_data_preprocessed)
        )
        self.stats["financial_data"]["la_maintained_schools"]["CFR"]["year"] = cfr_year

    def collect_combined_school_counts(
        self, combined_schools_data_preprocessed
    ) -> None:
        self.stats["financial_data"]["combined_schools"] = self._generate_school_counts(
            combined_schools_data_preprocessed
        )

    def _collect_preprocessed_ancillary_data_shapes(
        self, school_category: str, ancillary_data_dict: dict, ancillary_data_year
    ) -> None:
        self.stats["financial_data"][school_category]["ancillary_data"][
            "year"
        ] = ancillary_data_year
        for ancillary_data_name, ancillary_data_df in ancillary_data_dict.items():
            if ancillary_data_df is not None:
                row_count = ancillary_data_df.shape[0]
                self.stats["financial_data"][school_category]["ancillary_data"][
                    ancillary_data_name
                ] = {"unique_schools": row_count}

    def collect_aar_ancillary_data_shapes(self, aar_ancillary_data, aar_year) -> None:
        self._collect_preprocessed_ancillary_data_shapes(
            "academies", aar_ancillary_data, aar_year
        )

    def collect_cfr_ancillary_data_shapes(self, cfr_ancillary_data, cfr_year) -> None:
        self._collect_preprocessed_ancillary_data_shapes(
            "la_maintained_schools", cfr_ancillary_data, cfr_year
        )

    def get_stats(self) -> dict:
        if not self.stats["financial_data"]["academies"]["AAR"]:
            stats_logger.info("No AAR data counts have been logged")
        if not self.stats["financial_data"]["la_maintained_schools"]["CFR"]:
            stats_logger.info("No CFR data counts have been logged")

        return self.stats


stats_collector = StatsCollector()
