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
        self,
        target_index: int,
        phase_arrays: dict,
        distances: np.ndarray,
        include_mask: np.ndarray,
    ) -> np.ndarray:
        """
        Determines the URNs of the most similar schools based on a
        hierarchy of criteria.
        """
        valid_mask = (
            np.arange(len(phase_arrays[ColumnNames.URN])) != target_index
        ) & include_mask

        other_distances = distances[valid_mask]
        urns_without_target = phase_arrays[ColumnNames.URN][valid_mask]
        regions_without_target = phase_arrays[ColumnNames.REGION][valid_mask]
        pfi_without_target = phase_arrays[ColumnNames.PFI][valid_mask]
        boarding_without_target = phase_arrays[ColumnNames.BOARDERS][valid_mask]

        sorted_indices = np.argsort(other_distances, kind="stable")[:BASE_SET_SIZE]
        urns_by_distance = urns_without_target[sorted_indices]

        target_urn = phase_arrays[ColumnNames.URN][target_index]
        target_pfi = phase_arrays[ColumnNames.PFI][target_index]
        target_boarding = phase_arrays[ColumnNames.BOARDERS][target_index]
        target_region = phase_arrays[ColumnNames.REGION][target_index]

        urns = np.array([target_urn])

        # POTENTIAL ERROR: If a school meets multiple criteria (e.g. same PFI and
        # same region), its URN will be appended multiple times, creating duplicates
        # in the final comparator set.
        if target_pfi:
            top_pfi = pfi_without_target[sorted_indices]
            same_pfi = np.argwhere(top_pfi).flatten()
            urns = np.append(urns, urns_by_distance[same_pfi])

        if target_boarding == "Boarding":
            top_boarding = boarding_without_target[sorted_indices]
            same_boarding = np.argwhere(top_boarding == target_boarding).flatten()
            urns = np.append(urns, urns_by_distance[same_boarding])

        top_regions = regions_without_target[sorted_indices]
        same_region_indices = np.argwhere(top_regions == target_region).flatten()
        urns = np.append(urns, urns_by_distance[same_region_indices])

        if len(urns) >= FINAL_SET_SIZE:
            return urns[:FINAL_SET_SIZE]

        # POTENTIAL ERROR: This logic only excludes schools already added for being
        # in the `same_region`. It does NOT exclude schools added for PFI or Boarding
        # status, so those schools could be added a second time here.
        fill_count = FINAL_SET_SIZE - len(urns)
        urns_to_add = np.delete(urns_by_distance, same_region_indices)[:fill_count]
        urns = np.append(urns, urns_to_add)

        return urns

    def _process_phase_group(self, phase: str, group: pd.DataFrame) -> pd.DataFrame:
        """
        Processes a single school phase group to generate all its comparator sets.
        """
        pupil_distances = self._compute_pupils_distance(phase, group)
        building_distances = self._compute_buildings_distance(group)

        pupil_include_mask = (
            ~np.array(group[ColumnNames.PARTIAL_YEARS])
            & ~np.array(group[ColumnNames.DID_NOT_SUBMIT])
            & np.array(group[ColumnNames.PUPIL_DATA])
        )
        building_include_mask = (
            ~np.array(group[ColumnNames.PARTIAL_YEARS])
            & ~np.array(group[ColumnNames.DID_NOT_SUBMIT])
            & np.array(group[ColumnNames.BUILDING_DATA])
        )

        base_arrays = {
            ColumnNames.URN: np.array(group.index),
            ColumnNames.PFI: np.array(group[ColumnNames.PFI]),
            ColumnNames.BOARDERS: np.array(group[ColumnNames.BOARDERS]),
            ColumnNames.REGION: np.array(group[ColumnNames.REGION]),
        }

        pupil_sets = []
        building_sets = []

        for i, (urn, row) in enumerate(group.iterrows()):
            if row["_GeneratePupilSet"]:
                # Pass the specific pupil mask
                pupil_urns = self._select_top_urns(
                    i, base_arrays, pupil_distances[i], pupil_include_mask
                )
                pupil_sets.append(pupil_urns if len(pupil_urns) > 1 else np.array([]))
            else:
                pupil_sets.append(np.array([]))

            if row["_GenerateBuildingSet"]:
                # Pass the specific building mask
                building_urns = self._select_top_urns(
                    i, base_arrays, building_distances[i], building_include_mask
                )
                building_sets.append(
                    building_urns if len(building_urns) > 1 else np.array([])
                )
            else:
                building_sets.append(np.array([]))

        return pd.DataFrame(
            {"Pupil": pupil_sets, "Building": building_sets}, index=group.index
        )

    def calculate_comparator_sets(self, target_urn: int | None = None) -> pd.DataFrame:
        """
        Orchestrates the derivation of comparator sets for all schools.
        """
        df = self.prepared_data
        if target_urn:
            full_df = self.prepared_data
            if target_urn not in full_df.index:
                return pd.DataFrame()
        else:
            full_df = df

        can_generate = (
            full_df[ColumnNames.FINANCIAL_DATA] & ~full_df[ColumnNames.DID_NOT_SUBMIT]
        )
        full_df["_GeneratePupilSet"] = can_generate & full_df[ColumnNames.PUPIL_DATA]
        full_df["_GenerateBuildingSet"] = (
            full_df["_GeneratePupilSet"] & full_df[ColumnNames.BUILDING_DATA]
        )

        grouped = full_df.groupby(ColumnNames.PHASE)
        all_results = [
            self._process_phase_group(phase, group) for phase, group in grouped
        ]

        if not all_results:
            result_df = full_df.copy()
            result_df["Pupil"] = [[] for _ in range(len(full_df))]
            result_df["Building"] = [[] for _ in range(len(full_df))]
        else:
            final_sets = pd.concat(all_results)
            result_df = full_df.join(final_sets)

        result_df = result_df.drop(
            columns=["_GeneratePupilSet", "_GenerateBuildingSet"]
        )

        if target_urn:
            return result_df.loc[[target_urn]]

        return result_df
