import string

import numpy as np
import pytest

from src.pipeline.comparator_sets import select_top_set_urns


# @pytest.mark.parametrize(
#     "local",
#     [
#         {
#             "pfi": np.array([i % 2 == 0 for i in range(26)]),
#             "boarding": np.array([i % 2 == 0 for i in range(26)]),
#         },
#         {
#             "pfi": np.array([i % 2 == 0 for i in range(26)]),
#             "boarding": np.array([i % 2 != 0 for i in range(26)]),
#         },
#         {
#             "pfi": np.array([i % 2 != 0 for i in range(26)]),
#             "boarding": np.array([i % 2 == 0 for i in range(26)]),
#         },
#         {
#             "pfi": np.array([i % 2 != 0 for i in range(26)]),
#             "boarding": np.array([i % 2 != 0 for i in range(26)]),
#         },
#     ],
# )
# def test_pfi_boarding(select_top_set_urns_defaults, local):
#     """
#     26 orgs. where every other org. is the same PFI/Boarding status as
#     the first: this will reduce the base-set to ACEGIK.
#
#     All are in the same region: the final set should be ACE.
#     """
#     result = select_top_set_urns(
#         index=1,
#         **(select_top_set_urns_defaults | local),
#     )
#
#     np.testing.assert_array_equal(
#         result,
#         np.array(["A", "C", "E"]),
#     )


# def test_excludes_low_distance_region():
#     """
#     26 orgs. where every other org. is the same PFI/Boarding status:
#     this will reduce the base-set to ACEGIK.
#
#     None in the top `base_set_size` are in the same region, reducing
#     the potential final set to just A.
#
#     The final set should then be supplemented by the original set,
#     ordered by distance-metric.
#     """
#     urns = np.array(list(string.ascii_uppercase))  # ["A"…"Z"]
#     pfi = np.array([i % 2 == 0 for i in range(26)])  # [True, False, True…]
#     boarding = np.array([i % 2 == 0 for i in range(26)])  # [True, False, True…]
#     regions = np.array(["A"] + (["B"] * 24) + ["A"])  # ["A", "B"…"B", "A"]
#     distances = np.array([0.01 * i for i in range(26)])  # [0.0, 0.01…0.25]
#
#     result = select_top_set_urns(
#         index=0,
#         urns=urns,
#         pfi=pfi,
#         boarding=boarding,
#         regions=regions,
#         distances=distances,
#         base_set_size=6,
#         final_set_size=3,
#     )
#
#     np.testing.assert_array_equal(
#         result,
#         np.array(["A", "B", "C"]),
#     )
#
#
# def test_two_calls_to_top_set_urns():
#     """
#     26 orgs. where every other org. is the same PFI/Boarding status:
#     this will reduce the base-set to ACEGIK.
#
#     None in the top `base_set_size` are in the same region, reducing
#     the potential final set to just A.
#
#     The final set should then be supplemented by the original set,
#     ordered by distance-metric.
#     """
#     urns = np.array(list(string.ascii_uppercase))  # ["A"…"Z"]
#     pfi = np.array([i % 2 == 0 for i in range(26)])  # [True, False, True…]
#     boarding = np.array([i % 2 == 0 for i in range(26)])  # [True, False, True…]
#     regions = np.array(["A"] + (["B"] * 24) + ["A"])  # ["A", "B"…"B", "A"]
#     distances = np.array([0.01 * i for i in range(26)])  # [0.0, 0.01…0.25]
#
#     resultA = select_top_set_urns(
#         index=0,
#         urns=urns,
#         pfi=pfi,
#         boarding=boarding,
#         regions=regions,
#         distances=distances,
#         base_set_size=6,
#         final_set_size=3,
#     )
#
#     resultB = select_top_set_urns(
#         index=0,
#         urns=urns,
#         pfi=pfi,
#         boarding=boarding,
#         regions=regions,
#         distances=distances,
#         base_set_size=6,
#         final_set_size=3,
#     )
#
#     np.testing.assert_array_equal(
#         resultA,
#         np.array(["A", "B", "C"]),
#     )
#
#     np.testing.assert_array_equal(
#         resultB,
#         np.array(["A", "B", "C"]),
#     )
