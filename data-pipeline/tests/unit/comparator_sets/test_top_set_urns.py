import string

import numpy as np
import pytest

from src.pipeline.comparator_sets import select_top_set_urns


@pytest.mark.parametrize(
    "local",
    [
        {
            "pfi": np.array([i % 2 == 0 for i in range(26)]),
            "boarding": np.array(["Boarding" if i % 2 == 0 else "" for i in range(26)]),
        },
        {
            "pfi": np.array([i % 2 == 0 for i in range(26)]),
            "boarding": np.array(["Boarding" if i % 2 != 0 else "" for i in range(26)]),
        },
        {
            "pfi": np.array([i % 2 != 0 for i in range(26)]),
            "boarding": np.array(["Boarding" if i % 2 == 0 else "" for i in range(26)]),
        },
    ],
    ids=[
        "pfi_boarding",
        "pfi_not_boarding",
        "not_pfi_boarding",
    ],
)
def test_pfi_boarding(select_top_set_urns_defaults, local):
    """
    26 orgs. where every other org. is the same PFI/Boarding status as
    the first: this will reduce the base-set to ACEGIK.

    All are in the same region: the final set should be ACE.
    """
    result = select_top_set_urns(
        index=0,
        **(select_top_set_urns_defaults | local),
    )

    print(result)
    np.testing.assert_array_equal(
        result,
        np.array(["A", "C", "E"]),
    )


def test_not_pfi_not_boarding(select_top_set_urns_defaults):
    """
    If neither PFI nor boarding, the result should be ABC.
    """
    local = {
        "pfi": np.array([i % 2 != 0 for i in range(26)]),
        "boarding": np.array(["Boarding" if i % 2 != 0 else "" for i in range(26)]),
    }
    result = select_top_set_urns(
        index=0,
        **(select_top_set_urns_defaults | local),
    )

    print(result)
    np.testing.assert_array_equal(
        result,
        np.array(["A", "B", "C"]),
    )


def test_excludes_low_distance_region():
    """
    26 orgs. where every other org. is the same PFI/Boarding status:
    this will reduce the base-set to ACEGIK.

    None in the top `base_set_size` are in the same region, reducing
    the potential final set to just A.

    The final set should then be supplemented by the original set,
    ordered by distance-metric but prioritising PFI and boarding.
    """
    urns = np.array(list(string.ascii_uppercase))  # ["A"…"Z"]
    pfi = np.array([i % 2 == 0 for i in range(26)])  # [True, False, True…]
    boarding = np.array([i % 2 == 0 for i in range(26)])  # [True, False, True…]
    regions = np.array(["A"] + (["B"] * 24) + ["A"])  # ["A", "B"…"B", "A"]
    distances = np.array([0.01 * i for i in range(26)])  # [0.0, 0.01…0.25]
    include = np.array([True] * 26)

    result = select_top_set_urns(
        index=0,
        urns=urns,
        pfi=pfi,
        boarding=boarding,
        regions=regions,
        distances=distances,
        include=include,
        base_set_size=6,
        final_set_size=3,
    )

    np.testing.assert_array_equal(
        result,
        np.array(["A", "C", "E"]),
    )


def test_excludes_non_included(select_top_set_urns_defaults):
    """
    26 orgs. where every other org. is the same PFI/Boarding status:
    this will reduce the base-set to ACEGIK.

    None in the top `base_set_size` are in `include`; only the final
    URN should be included.
    """
    local = {
        "pfi": np.array([i % 2 == 0 for i in range(26)]),
        "boarding": np.array(["Boarding" if i % 2 == 0 else "" for i in range(26)]),
        "include": np.array([True] + [False] * 24 + [True]),
    }

    result = select_top_set_urns(
        index=0,
        **(select_top_set_urns_defaults | local),
    )

    np.testing.assert_array_equal(
        result,
        np.array(["A", "Z"]),
    )
