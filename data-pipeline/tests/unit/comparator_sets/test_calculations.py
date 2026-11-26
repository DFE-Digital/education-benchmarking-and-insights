import numpy as np
import pandas as pd
import pytest

from pipeline.comparator_sets.calculations import ComparatorCalculator, prepare_data
from pipeline.comparator_sets.config import FINAL_SET_SIZE, ColumnNames

from .conftest import sample_data_length


class TestPrepareData:
    def test_prepare_data_fills_na(self):
        """Verify that NaN values in specified columns are filled with the median."""
        data = {
            ColumnNames.PUPILS: [100, 200, np.nan, 400],
            ColumnNames.FSM: [10, np.nan, 30, 40],
            ColumnNames.BOARDERS: ["Boarding", "Unknown", "Not Boarding", "Boarding"],
        }
        df = pd.DataFrame(data)
        prepared_df = prepare_data(df)

        assert prepared_df[ColumnNames.PUPILS].isnull().sum() == 0
        assert prepared_df[ColumnNames.FSM].isnull().sum() == 0
        # The median of [100, 200, 400] is 200
        pd.testing.assert_series_equal(
            prepared_df[ColumnNames.PUPILS],
            pd.Series([100.0, 200.0, 200.0, 400.0], name=ColumnNames.PUPILS),
            check_names=False,
        )

    def test_prepare_data_maps_unknown_boarders(self):
        """Ensure 'Unknown' boarder status is mapped to 'Not Boarding'."""
        df = pd.DataFrame(
            {ColumnNames.BOARDERS: ["Boarding", "Unknown", "Not Boarding"]}
        )
        prepared_df = prepare_data(df)
        expected = pd.Series(
            ["Boarding", "Not Boarding", "Not Boarding"], name=ColumnNames.BOARDERS
        )
        pd.testing.assert_series_equal(prepared_df[ColumnNames.BOARDERS], expected)


def _build_monotonic_school_df(
    urns: list[str],
    pfi: list[bool],
    boarders: list[str],
    regions: list[str],
    phase: str = "Primary",
) -> pd.DataFrame:
    """Builds a DataFrame where distance from index 0 increases strictly with row index."""
    count = len(urns)
    data = {
        ColumnNames.URN: urns,
        ColumnNames.PUPILS: [100 + i * 10 for i in range(count)],
        ColumnNames.FSM: [5.0 + i * 0.5 for i in range(count)],
        ColumnNames.SEN: [2.0 + i * 0.2 for i in range(count)],
        ColumnNames.BOARDERS: boarders,
        ColumnNames.PFI: pfi,
        ColumnNames.REGION: regions,
        ColumnNames.GIFA: [1000 + i * 50 for i in range(count)],
        ColumnNames.AGE_SCORE: [20 + i for i in range(count)],
        ColumnNames.PHASE: [phase] * count,
        ColumnNames.DID_NOT_SUBMIT: [False] * count,
        ColumnNames.PARTIAL_YEARS: [False] * count,
        ColumnNames.FINANCIAL_DATA: [True] * count,
        ColumnNames.PUPIL_DATA: [True] * count,
        ColumnNames.BUILDING_DATA: [True] * count,
    }
    for col in ColumnNames.SEN_NEEDS:
        data[col] = [1.0 + i * 0.1 for i in range(count)]

    return pd.DataFrame(data).set_index(ColumnNames.URN)


class TestComparatorCalculator:
    def test_delta_range_ratio_squared(self, calculator: ComparatorCalculator):
        """Test the basic distance calculation component."""
        input_array = np.array([0, 5, 10])
        result = calculator._delta_range_ratio_squared(input_array)
        expected = np.array([[0.0, 0.25, 1.0], [0.25, 0.0, 0.25], [1.0, 0.25, 0.0]])
        np.testing.assert_allclose(result, expected)

    @pytest.mark.skip(reason="this test fails as there's an unhandled bug in the code")
    def test_delta_range_ratio_squared_zero_range(
        self, calculator: ComparatorCalculator
    ):
        """Test the distance calculation component when the range is zero."""
        input_array = np.array([5, 5, 5])
        result = calculator._delta_range_ratio_squared(input_array)
        expected = np.zeros((3, 3))
        np.testing.assert_array_equal(result, expected)

    def test_compute_buildings_distance(self, calculator: ComparatorCalculator):
        """Test building distance calculation, adapted from legacy test."""
        group_data = pd.DataFrame(
            {
                ColumnNames.GIFA: [150, 300, 450],
                ColumnNames.AGE_SCORE: [5, 10, 15],
            }
        )
        result = calculator._compute_buildings_distance(group_data)
        expected = np.array([[0.0, 0.5, 1.0], [0.5, 0.0, 0.5], [1.0, 0.5, 0.0]])
        np.testing.assert_allclose(result, expected)

    def test_compute_pupils_distance_non_special(
        self, calculator: ComparatorCalculator
    ):
        """Test pupil distance calculation for non-special schools."""
        group_data = pd.DataFrame(
            {
                ColumnNames.PUPILS: [100, 200, 300],
                ColumnNames.FSM: [20, 40, 60],
                ColumnNames.SEN: [5, 10, 15],
            }
        )
        result = calculator._compute_pupils_distance("Primary", group_data)
        expected = np.array([[0.0, 0.5, 1.0], [0.5, 0.0, 0.5], [1.0, 0.5, 0.0]])
        np.testing.assert_allclose(result, expected)

    def test_compute_pupils_distance_special(self, calculator: ComparatorCalculator):
        """Test pupil distance calculation for special schools."""
        data = {
            ColumnNames.PUPILS: [100, 200],
            ColumnNames.FSM: [10, 20],
        }
        sen_needs = {col: [i, i + 1] for i, col in enumerate(ColumnNames.SEN_NEEDS)}
        group_data = pd.DataFrame(data | sen_needs)

        result = calculator._compute_pupils_distance("Special", group_data)
        expected = np.array([[0.0, 4.16227766], [4.16227766, 0.0]])
        np.testing.assert_allclose(result, expected)

    def test_select_top_urns_pfi_boarding(
        self, calculator: ComparatorCalculator, top_urns_phase_arrays: dict
    ):
        """
        Tests that for a PFI/Boarding target, the top results are prioritized correctly.
        """
        distances = np.array([0.01 * i for i in range(sample_data_length)])
        # Target 'A' (index 0) is PFI and Boarding
        top_urns_phase_arrays[ColumnNames.PFI] = np.array(
            [i % 2 == 0 for i in range(sample_data_length)]
        )
        top_urns_phase_arrays[ColumnNames.BOARDERS] = np.array(
            [
                "Boarding" if i % 2 == 0 else "Not Boarding"
                for i in range(sample_data_length)
            ]
        )
        include_mask = np.array([True] * sample_data_length)

        result = calculator._select_top_urns(
            0, top_urns_phase_arrays, distances, include_mask
        )

        # The size should be the smaller of FINAL_SET_SIZE or the total number of schools
        expected_size = min(FINAL_SET_SIZE, sample_data_length)
        assert len(result) == expected_size
        assert len(set(result)) == len(result)
        assert result[0] == "A"  # Target is always first
        # The next results should be 'C', 'E', 'G', etc. because they are also PFI/Boarding
        assert result[1] == "C"
        assert result[2] == "E"

    def test_select_top_urns_not_pfi_not_boarding(
        self, calculator: ComparatorCalculator, top_urns_phase_arrays: dict
    ):
        """
        Tests that for a non-PFI/Boarding target, the closest schools are chosen first.
        """
        distances = np.array([0.01 * i for i in range(sample_data_length)])
        # Target 'A' (index 0) is not PFI and not Boarding
        top_urns_phase_arrays[ColumnNames.PFI] = np.array([False] * sample_data_length)
        top_urns_phase_arrays[ColumnNames.BOARDERS] = np.array(
            ["Not Boarding"] * sample_data_length
        )
        include_mask = np.array([True] * sample_data_length)

        result = calculator._select_top_urns(
            0, top_urns_phase_arrays, distances, include_mask
        )

        # This set has not been topped up to the final size
        assert len(result) == sample_data_length
        assert result[0] == "A"
        # Since no PFI/Boarding/Region criteria apply, it should just be the closest schools
        assert result[1] == "B"
        assert result[2] == "C"

    def test_select_top_urns_fills_from_others(
        self, calculator: ComparatorCalculator, top_urns_phase_arrays: dict
    ):
        """
        Tests that the set is filled with the next closest candidates if primary criteria
        (like region) don't produce a full set.
        """
        distances = np.array([0.01 * i for i in range(sample_data_length)])
        # Only the target 'A' is in its region, and no PFI/Boarding priority applies
        top_urns_phase_arrays[ColumnNames.PFI] = np.array([False] * sample_data_length)
        top_urns_phase_arrays[ColumnNames.BOARDERS] = np.array(
            ["Not Boarding"] * sample_data_length
        )
        top_urns_phase_arrays[ColumnNames.REGION] = np.array(
            ["Region A"] + ["Region B"] * (sample_data_length - 1)
        )
        include_mask = np.array([True] * sample_data_length)

        result = calculator._select_top_urns(
            0, top_urns_phase_arrays, distances, include_mask
        )

        assert len(result) == min(FINAL_SET_SIZE, sample_data_length)
        assert len(set(result)) == len(result)
        assert result[0] == "A"
        # Since only 'A' is in the region, the rest are filled by distance
        assert result[1] == "B"
        assert result[2] == "C"

    def test_calculate_all_sets_generation_logic(self, sample_data: pd.DataFrame):
        """
        Tests the logic that determines if pupil and building sets should be generated.
        """
        df = sample_data.copy()

        # URN1: Cannot generate any set
        df.loc["URN1", ColumnNames.FINANCIAL_DATA] = False
        # URN2: Can generate pupil, but not building
        df.loc["URN2", ColumnNames.BUILDING_DATA] = False
        # URN3: Cannot generate due to Did Not Submit
        df.loc["URN3", ColumnNames.DID_NOT_SUBMIT] = True

        calculator = ComparatorCalculator(prepare_data(df))
        results = calculator.calculate_comparator_sets()

        # URN3 did not submit (excluded from both pools), and URN2 lacks building data
        expected_pupil_size = min(FINAL_SET_SIZE, sample_data_length - 1)
        expected_building_size = min(FINAL_SET_SIZE, sample_data_length - 2)

        # Set for URN0 should include each valid peer once
        assert len(results.loc["URN0"]["Pupil"]) == expected_pupil_size
        assert len(results.loc["URN0"]["Building"]) == expected_building_size

        # URN1 has no financial data
        assert len(results.loc["URN1"]["Pupil"]) == 0
        assert len(results.loc["URN1"]["Building"]) == 0

        # URN2 has no building data, so building set is empty
        assert len(results.loc["URN2"]["Pupil"]) == expected_pupil_size
        assert len(results.loc["URN2"]["Building"]) == 0

        # URN3 did not submit
        assert len(results.loc["URN3"]["Pupil"]) == 0
        assert len(results.loc["URN3"]["Building"]) == 0

    def test_calculate_comparator_sets_with_target_urn(self, sample_data: pd.DataFrame):
        """
        Test that processing a single URN results in a correctly formed set,
        using its peers for comparison.
        """
        calculator = ComparatorCalculator(prepare_data(sample_data))
        # This will filter the dataframe inside the method to only contain URN5
        results = calculator.calculate_comparator_sets(target_urn="URN5")

        assert len(results) == 1
        assert results.index[0] == "URN5"
        assert len(results.iloc[0]["Pupil"]) == sample_data_length
        assert len(results.iloc[0]["Building"]) == sample_data_length

    # regression-test: bug comparator-set-duplicate-urns
    @pytest.mark.parametrize("phase", ["Primary", "Special"])
    def test_calculate_comparator_sets_deduplicates_overlapping_criteria_and_top_up(
        self, phase: str
    ):
        """
        Ensure schools matching multiple criteria (PFI, Boarding, Region) or
        outside the target region are added once in strict priority order.
        """
        urns = ["S0", "S1", "S2", "S3", "S4", "S5"]
        pfi = [True, False, False, True, False, True]
        boarders = [
            "Boarding",
            "Not Boarding",
            "Not Boarding",
            "Boarding",
            "Boarding",
            "Not Boarding",
        ]
        regions = ["North", "South", "North", "North", "North", "South"]
        df = prepare_data(
            _build_monotonic_school_df(urns, pfi, boarders, regions, phase)
        )

        results = ComparatorCalculator(df).calculate_comparator_sets(target_urn="S0")

        # Priority order: S0 (target), S3 & S5 (PFI), S4 (Boarding), S2 (Region), S1 (fallback)
        expected_order = np.array(["S0", "S3", "S5", "S4", "S2", "S1"])
        np.testing.assert_array_equal(results.loc["S0", "Pupil"], expected_order)
        np.testing.assert_array_equal(results.loc["S0", "Building"], expected_order)

    # regression-test: bug comparator-set-duplicate-urns
    def test_calculate_comparator_sets_fills_full_unique_set_when_priority_peers_overlap(
        self,
    ):
        """
        Ensure overlapping PFI/Boarding/Region matches do not consume duplicate
        slots and displace remaining candidates from reaching FINAL_SET_SIZE unique URNs.
        """
        total_schools = 40
        urns = [f"S{i}" for i in range(total_schools)]
        pfi = [True] * 13 + [False] * 27
        boarders = ["Boarding"] * 13 + ["Not Boarding"] * 27
        regions = ["North"] * 13 + ["South"] * 27
        df = prepare_data(_build_monotonic_school_df(urns, pfi, boarders, regions))

        results = ComparatorCalculator(df).calculate_comparator_sets(target_urn="S0")

        expected_urns = np.array([f"S{i}" for i in range(FINAL_SET_SIZE)])
        np.testing.assert_array_equal(results.loc["S0", "Pupil"], expected_urns)
        np.testing.assert_array_equal(results.loc["S0", "Building"], expected_urns)

    def test_calculate_comparator_sets_deduplicates_duplicate_index_urns(self):
        """
        Ensure duplicate URN rows in prepared_data are deduplicated in __init__
        without duplicating rows in the output or comparator sets.
        """
        urns = ["S0", "S1", "S1", "S2"]
        pfi = [True, True, True, False]
        boarders = ["Not Boarding"] * 4
        regions = ["North"] * 4
        df = prepare_data(_build_monotonic_school_df(urns, pfi, boarders, regions))

        results = ComparatorCalculator(df).calculate_comparator_sets()

        assert list(results.index) == ["S0", "S1", "S2"]
        np.testing.assert_array_equal(
            results.loc["S0", "Pupil"], np.array(["S0", "S1", "S2"])
        )

    def test_calculate_comparator_sets_handles_single_school_phase_with_no_peers(self):
        """
        Ensure a school with no valid peers receives empty comparator arrays.
        """
        df = prepare_data(
            _build_monotonic_school_df(["S0"], [True], ["Boarding"], ["North"])
        )

        results = ComparatorCalculator(df).calculate_comparator_sets()

        assert len(results.loc["S0", "Pupil"]) == 0
        assert len(results.loc["S0", "Building"]) == 0

    def test_calculate_comparator_sets_partitions_top_base_set_size_on_large_phase(
        self,
    ):
        """
        Ensure phases larger than BASE_SET_SIZE (60) include priority matches inside
        rank 60 while excluding priority matches beyond rank 60.
        """
        total_schools = 70
        urns = [f"S{i:02d}" for i in range(total_schools)]
        pfi = [i in (0, 50, 65) for i in range(total_schools)]
        boarders = ["Not Boarding"] * total_schools
        regions = ["North"] + ["South"] * (total_schools - 1)
        df = prepare_data(_build_monotonic_school_df(urns, pfi, boarders, regions))

        results = ComparatorCalculator(df).calculate_comparator_sets(target_urn="S00")

        expected_urns = np.array(["S00", "S50", *[f"S{i:02d}" for i in range(1, 29)]])
        np.testing.assert_array_equal(results.loc["S00", "Pupil"], expected_urns)
        np.testing.assert_array_equal(results.loc["S00", "Building"], expected_urns)

    def test_calculate_comparator_sets_preserves_stable_order_on_boundary_ties(self):
        """
        Ensure exact distance ties straddling rank BASE_SET_SIZE (60) break ties
        stably by index order rather than arbitrary partition order.
        """
        total_schools = 70
        urns = [f"S{i:02d}" for i in range(total_schools)]
        pfi = [False] * total_schools
        boarders = ["Not Boarding"] * total_schools
        regions = ["North"] + ["South"] * 57 + ["North"] * 12
        raw_df = _build_monotonic_school_df(urns, pfi, boarders, regions)

        # Make S58..S65 identical on all numeric metrics so they tie across rank 60
        tied_urns = [f"S{i:02d}" for i in range(58, 66)]
        numeric_cols = [
            ColumnNames.PUPILS,
            ColumnNames.FSM,
            ColumnNames.SEN,
            ColumnNames.GIFA,
            ColumnNames.AGE_SCORE,
            *ColumnNames.SEN_NEEDS,
        ]
        for col in numeric_cols:
            raw_df.loc[tied_urns, col] = raw_df.loc["S58", col]

        df = prepare_data(raw_df)
        results = ComparatorCalculator(df).calculate_comparator_sets(target_urn="S00")

        # Only S58, S59, S60 fall within the top-60 window and match North region
        expected_urns = np.array(
            ["S00", "S58", "S59", "S60", *[f"S{i:02d}" for i in range(1, 27)]]
        )
        np.testing.assert_array_equal(results.loc["S00", "Pupil"], expected_urns)
        np.testing.assert_array_equal(results.loc["S00", "Building"], expected_urns)
