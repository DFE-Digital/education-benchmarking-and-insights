import logging

import numpy as np
import pandas as pd

from .config import *

logger = logging.getLogger(__name__)


def prepare_data(data: pd.DataFrame) -> pd.DataFrame:
    """
    Cleans and prepares the data for comparator set calculations.
    This is a standalone utility function.

    :param data: The raw input DataFrame.
    :return: A prepared DataFrame with missing values filled.
    """
    df = data.copy()

    # Map 'Unknown' boarding status
    df[ColumnNames.BOARDERS] = df[ColumnNames.BOARDERS].map(
        lambda x: "Not Boarding" if x == "Unknown" else x
    )

    # Identify all numeric columns that require median filling
    cols_to_fill = [
        ColumnNames.PUPILS,
        ColumnNames.FSM,
        ColumnNames.SEN,
        ColumnNames.GIFA,
        ColumnNames.AGE_SCORE,
    ] + ColumnNames.SEN_NEEDS

    for col in cols_to_fill:
        if col in df.columns:
            df[col] = df[col].fillna(df[col].median())

    return df.sort_index()


class ComparatorCalculator:
    """
    Encapsulates the logic for calculating comparator sets on prepared data.
    """

    def __init__(self, prepared_data: pd.DataFrame):
        """
        Initializes the calculator.

        :param prepared_data: A DataFrame that has already been cleaned
                              by the `prepare_data` function.
        """
        self.prepared_data = prepared_data[~prepared_data.index.isna()].copy()

    def _delta_range_ratio_squared(self, input_array: np.array) -> np.array:
        """
        Calculates the squared ratio of the absolute difference between
        all combinations in the input, divided by the range of the input.
        """
        with np.errstate(divide="ignore", invalid="ignore"):
            input_range = np.ptp(input_array)
            if input_range == 0:
                return np.zeros((len(input_array), len(input_array)))
            diff_matrix = np.abs(input_array[:, None] - input_array[None, :])
            ratio = diff_matrix / input_range
            return np.power(ratio, 2)

    def _compute_weighted_distance(
        self, group_data: pd.DataFrame, metrics: dict
    ) -> np.ndarray:
        """
        A generic function to compute the square root of the sum of
        weighted, squared-delta-range-ratio calculations.
        """
        distance_components = [
            weight * self._delta_range_ratio_squared(np.array(group_data[col]))
            for col, weight in metrics.items()
        ]
        return np.sqrt(sum(distance_components))

    def _compute_pupils_distance(
        self, phase: str, group_data: pd.DataFrame
    ) -> np.ndarray:
        """Computes pupil distance, delegating to special or standard logic."""
        if phase.lower() == "special":
            base_metrics = {
                ColumnNames.PUPILS: SPECIAL_PUPILS_WEIGHT,
                ColumnNames.FSM: SPECIAL_FSM_WEIGHT,
            }
            pupil_dist = self._compute_weighted_distance(group_data, base_metrics)
            sen_metrics = {col: 1.0 for col in ColumnNames.SEN_NEEDS}
            sen_dist = self._compute_weighted_distance(group_data, sen_metrics)
            return pupil_dist + sen_dist
        else:
            standard_metrics = {
                ColumnNames.PUPILS: PUPILS_WEIGHT,
                ColumnNames.FSM: FSM_WEIGHT,
                ColumnNames.SEN: SEN_WEIGHT,
            }
            return self._compute_weighted_distance(group_data, standard_metrics)

    def _compute_buildings_distance(self, group_data: pd.DataFrame) -> np.ndarray:
        """Computes the building distance metric."""
        metrics = {
            ColumnNames.GIFA: GIFA_WEIGHT,
            ColumnNames.AGE_SCORE: AGE_WEIGHT,
        }
        return self._compute_weighted_distance(group_data, metrics)

    def _select_top_urns(
        self, target_index: int, phase_arrays: dict, distances: np.ndarray
    ) -> np.ndarray:
        """
        Determines the URNs of the most similar schools based on a
        hierarchy of criteria.
        """
        valid_mask = (np.arange(len(phase_arrays[ColumnNames.URN])) != target_index) & phase_arrays["include_mask"]
        other_distances = distances[valid_mask]
        
        # Use a stable sort to ensure deterministic output for tie-breaking
        sorted_indices = np.argsort(other_distances, kind='stable')[:BASE_SET_SIZE]

        top_candidates = pd.DataFrame(
            {
                col: arr[valid_mask][sorted_indices]
                for col, arr in phase_arrays.items() if col != "include_mask"
            }
        )
        
        target_urn = phase_arrays[ColumnNames.URN][target_index]
        final_set_urns = [target_urn]

        if phase_arrays[ColumnNames.PFI][target_index]:
            final_set_urns.extend(top_candidates[top_candidates[ColumnNames.PFI]]["URN"])
        if phase_arrays[ColumnNames.BOARDERS][target_index] == "Boarding":
            final_set_urns.extend(top_candidates[top_candidates[ColumnNames.BOARDERS] == "Boarding"]["URN"])

        target_region = phase_arrays[ColumnNames.REGION][target_index]
        final_set_urns.extend(top_candidates[top_candidates[ColumnNames.REGION] == target_region]["URN"])
        
        final_set_urns = list(dict.fromkeys(final_set_urns))

        if len(final_set_urns) < FINAL_SET_SIZE:
            remaining_candidates = top_candidates[~top_candidates["URN"].isin(final_set_urns)]
            fill_count = FINAL_SET_SIZE - len(final_set_urns)
            final_set_urns.extend(remaining_candidates["URN"].head(fill_count))

        return np.array(final_set_urns[:FINAL_SET_SIZE])

    def _process_phase_group(self, phase: str, group: pd.DataFrame) -> pd.DataFrame:
        """
        Processes a single school phase group to generate all its comparator sets.
        """
        pupil_distances = self._compute_pupils_distance(phase, group)
        building_distances = self._compute_buildings_distance(group)

        phase_arrays = {
            ColumnNames.URN: np.array(group.index),
            ColumnNames.PFI: np.array(group[ColumnNames.PFI]),
            ColumnNames.BOARDERS: np.array(group[ColumnNames.BOARDERS]),
            ColumnNames.REGION: np.array(group[ColumnNames.REGION]),
            "include_mask": (
                ~np.array(group[ColumnNames.PARTIAL_YEARS])
                & ~np.array(group[ColumnNames.DID_NOT_SUBMIT])
            ),
        }

        pupil_sets = []
        building_sets = []
        
        for i, (urn, row) in enumerate(group.iterrows()):
            if row["_GeneratePupilSet"]:
                pupil_urns = self._select_top_urns(i, phase_arrays, pupil_distances[i])
                pupil_sets.append(pupil_urns if len(pupil_urns) > 1 else np.array([]))
            else:
                pupil_sets.append(np.array([]))

            if row["_GenerateBuildingSet"]:
                building_urns = self._select_top_urns(i, phase_arrays, building_distances[i])
                building_sets.append(building_urns if len(building_urns) > 1 else np.array([]))
            else:
                building_sets.append(np.array([]))

        return pd.DataFrame(
            {"Pupil": pupil_sets, "Building": building_sets}, index=group.index
        )

    def calculate_all_sets(self, target_urn: int | None = None) -> pd.DataFrame:
        """
        Orchestrates the derivation of comparator sets for all schools.
        """
        df = self.prepared_data
        if target_urn:
            df = df.loc[[target_urn]] if target_urn in df.index else pd.DataFrame()
            if df.empty:
                return df

        can_generate = df[ColumnNames.FINANCIAL_DATA] & ~df[ColumnNames.DID_NOT_SUBMIT]
        df["_GeneratePupilSet"] = can_generate & df[ColumnNames.PUPIL_DATA]
        df["_GenerateBuildingSet"] = df["_GeneratePupilSet"] & df[ColumnNames.BUILDING_DATA]

        grouped = df.groupby(ColumnNames.PHASE)
        all_results = [self._process_phase_group(phase, group) for phase, group in grouped]

        if not all_results:
            df["Pupil"] = [[] for _ in range(len(df))]
            df["Building"] = [[] for _ in range(len(df))]
        else:
            final_sets = pd.concat(all_results)
            df = df.join(final_sets)

        return df.drop(columns=["_GeneratePupilSet", "_GenerateBuildingSet"])