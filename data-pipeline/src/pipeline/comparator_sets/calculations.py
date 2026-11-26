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
        valid_rows = prepared_data[~prepared_data.index.isna()]
        self.prepared_data = valid_rows[
            ~valid_rows.index.duplicated(keep="first")
        ].copy()

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
        hierarchy of criteria, ensuring each URN appears at most once.
        """
        phase_urns = phase_arrays[ColumnNames.URN]
        target_urn = phase_urns[target_index]
        target_pfi = phase_arrays[ColumnNames.PFI][target_index]
        target_boarding = phase_arrays[ColumnNames.BOARDERS][target_index]
        target_region = phase_arrays[ColumnNames.REGION][target_index]

        valid_mask = (
            (np.arange(len(phase_urns)) != target_index)
            & (phase_urns != target_urn)
            & include_mask
        )

        other_distances = distances[valid_mask]
        urns_without_target = phase_urns[valid_mask]
        regions_without_target = phase_arrays[ColumnNames.REGION][valid_mask]
        pfi_without_target = phase_arrays[ColumnNames.PFI][valid_mask]
        boarding_without_target = phase_arrays[ColumnNames.BOARDERS][valid_mask]

        sorted_indices = np.argsort(other_distances, kind="stable")[:BASE_SET_SIZE]
        urns_by_distance = urns_without_target[sorted_indices]

        # Track selected positions in urns_by_distance so each candidate is added once
        # across PFI, Boarding, Region, and fallback distance tiers.
        selected_mask = np.zeros(len(sorted_indices), dtype=bool)
        ordered_groups = [np.array([target_urn], dtype=phase_urns.dtype)]

        if pd.notna(target_pfi) and target_pfi:
            top_pfi = pfi_without_target[sorted_indices]
            pfi_mask = pd.notna(top_pfi) & np.asarray(top_pfi, dtype=bool)
            ordered_groups.append(urns_by_distance[pfi_mask])
            selected_mask |= pfi_mask

        if target_boarding == "Boarding":
            top_boarding = boarding_without_target[sorted_indices]
            boarding_mask = (top_boarding == target_boarding) & ~selected_mask
            ordered_groups.append(urns_by_distance[boarding_mask])
            selected_mask |= boarding_mask

        top_regions = regions_without_target[sorted_indices]
        region_mask = (top_regions == target_region) & ~selected_mask
        ordered_groups.append(urns_by_distance[region_mask])
        selected_mask |= region_mask

        remaining_mask = ~selected_mask
        ordered_groups.append(urns_by_distance[remaining_mask])

        return np.concatenate(ordered_groups)[:FINAL_SET_SIZE]

    def _build_sets_for_phase(
        self,
        base_arrays: dict[str, np.ndarray],
        distances: np.ndarray,
        include_mask: np.ndarray,
        generate_mask: np.ndarray,
    ) -> list[np.ndarray]:
        """
        Builds comparator URN sets for all qualifying rows in a phase group
        without DataFrame row iteration.
        """
        empty = np.array([], dtype=base_arrays[ColumnNames.URN].dtype)
        sets = [empty for _ in range(len(generate_mask))]
        for i in np.flatnonzero(generate_mask):
            urns = self._select_top_urns(i, base_arrays, distances[i], include_mask)
            if len(urns) > 1:
                sets[i] = urns
        return sets

    def _process_phase_group(self, phase: str, group: pd.DataFrame) -> pd.DataFrame:
        """
        Processes a single school phase group to generate all its comparator sets.
        """
        pupil_distances = self._compute_pupils_distance(phase, group)
        building_distances = self._compute_buildings_distance(group)

        not_excluded = ~np.asarray(
            group[ColumnNames.PARTIAL_YEARS], dtype=bool
        ) & ~np.asarray(group[ColumnNames.DID_NOT_SUBMIT], dtype=bool)
        pupil_include_mask = not_excluded & np.asarray(
            group[ColumnNames.PUPIL_DATA], dtype=bool
        )
        building_include_mask = not_excluded & np.asarray(
            group[ColumnNames.BUILDING_DATA], dtype=bool
        )

        base_arrays = {
            ColumnNames.URN: np.array(group.index),
            ColumnNames.PFI: np.array(group[ColumnNames.PFI]),
            ColumnNames.BOARDERS: np.array(group[ColumnNames.BOARDERS]),
            ColumnNames.REGION: np.array(group[ColumnNames.REGION]),
        }

        pupil_sets = self._build_sets_for_phase(
            base_arrays,
            pupil_distances,
            pupil_include_mask,
            np.asarray(group["_GeneratePupilSet"], dtype=bool),
        )
        building_sets = self._build_sets_for_phase(
            base_arrays,
            building_distances,
            building_include_mask,
            np.asarray(group["_GenerateBuildingSet"], dtype=bool),
        )

        return pd.DataFrame(
            {"Pupil": pupil_sets, "Building": building_sets}, index=group.index
        )

    def calculate_comparator_sets(self, target_urn: int | None = None) -> pd.DataFrame:
        """
        Orchestrates the derivation of comparator sets for all schools.
        """
        df = self.prepared_data
        if target_urn is not None:
            if target_urn not in df.index:
                return pd.DataFrame()
            target_phase = df.loc[target_urn, ColumnNames.PHASE]
            full_df = df[df[ColumnNames.PHASE] == target_phase].copy()
        else:
            full_df = df.copy()

        can_generate = (
            full_df[ColumnNames.FINANCIAL_DATA] & ~full_df[ColumnNames.DID_NOT_SUBMIT]
        )
        if target_urn is not None:
            can_generate = can_generate & (full_df.index == target_urn)

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

        if target_urn is not None:
            return result_df.loc[[target_urn]]

        return result_df
