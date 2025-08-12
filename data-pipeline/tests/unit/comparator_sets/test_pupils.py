from unittest import mock

import numpy as np
import pytest

from pipeline.comparator_sets.calculations import (
    compute_pupils_comparator,
    pupils_calc,
    special_pupils_calc,
)


def test_varied_input_non_special():
    pupils = np.array([100, 200, 300])
    fsm = np.array([20, 40, 60])
    sen = np.array([5, 10, 15])

    result = pupils_calc(pupils, fsm, sen)

    np.testing.assert_array_equal(
        result,
        np.array([[0.0, 0.5, 1.0], [0.5, 0.0, 0.5], [1.0, 0.5, 0.0]]),
    )


def test_varied_input_special():
    pupils = np.array([100, 200])
    fsm = np.array([10, 20])
    splds = np.array([1, 2])
    mlds = np.array([3, 4])
    pmlds = np.array([5, 6])
    semhs = np.array([7, 8])
    slcns = np.array([9, 10])
    his = np.array([11, 12])
    msis = np.array([13, 14])
    pds = np.array([15, 16])
    asds = np.array([17, 18])
    oths = np.array([19, 20])

    result = special_pupils_calc(
        pupils, fsm, splds, mlds, pmlds, semhs, slcns, his, msis, pds, asds, oths
    )

    np.testing.assert_array_almost_equal(
        result, np.array([[0.0, 4.16227766], [4.16227766, 0.0]])
    )


@mock.patch("pipeline.comparator_sets.pupils_calc")
@mock.patch("pipeline.comparator_sets.special_pupils_calc")
@pytest.mark.parametrize(
    "phase,special_called,pupils_called",
    [
        ("special", True, False),
        ("non-special", False, True),
    ],
)
def test_compute_pupils_comparator_phase(
    mock_special_pupils_calc,
    mock_pupils_calc,
    phase: str,
    special_called: bool,
    pupils_called: bool,
    random_row: dict,
):
    result = compute_pupils_comparator((phase, random_row))

    assert mock_pupils_calc.called is pupils_called
    assert mock_special_pupils_calc.called is special_called
