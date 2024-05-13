import numpy as np

from src.pipeline.comparator_sets import pupils_calc


def test_varied_input():
    pupils = np.array([100, 200, 300])
    fsm = np.array([20, 40, 60])
    sen = np.array([5, 10, 15])

    result = pupils_calc(pupils, fsm, sen)

    np.testing.assert_array_equal(
        result,
        np.array([[0.0, 0.5, 1.0], [0.5, 0.0, 0.5], [1.0, 0.5, 0.0]]),
    )
