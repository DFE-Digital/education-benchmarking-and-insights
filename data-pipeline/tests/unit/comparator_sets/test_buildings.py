import numpy as np

from src.pipeline.comparator_sets import buildings_calc


def test_varied_input():
    gifa = np.array([150, 300, 450])
    average_age = np.array([5, 10, 15])

    result = buildings_calc(gifa, average_age)

    expected_output = np.array([[0.0, 0.5, 1.0], [0.5, 0.0, 0.5], [1.0, 0.5, 0.0]])
    np.testing.assert_array_equal(result, expected_output)
