import pytest

from pipeline.rag import is_area_close_comparator, is_pupil_close_comparator


@pytest.mark.parametrize(
    "a_floor,a_age,b_floor,b_age,expected",
    [
        (1.0, 1.0, 1.0, 1.0, True),
        (1.0, 1.0, 2.0, 100.0, False),
        (1.0, 1.0, 2.0, 1.0, False),
        (1.0, 1.0, 1.0, 100.0, False),
        (2.0, 1.0, 1.0, 1.0, False),
        (1.0, 100.0, 1.0, 1.0, False),
    ],
)
def test_is_area_close_comparator(a_floor, a_age, b_floor, b_age, expected):
    is_close = is_area_close_comparator(
        {
            "Total Internal Floor Area": a_floor,
            "Age Average Score": a_age,
        },
        {
            "Total Internal Floor Area": b_floor,
            "Age Average Score": b_age,
        },
    )

    assert is_close == expected


@pytest.mark.parametrize(
    "a_pupils,a_meals,a_sen,b_pupils,b_meals,b_sen,expected",
    [
        (1.0, 1.0, 1.0, 1.0, 1.0, 1.0, True),
        (1.0, 1.0, 1.0, 1.0, 1.0, 2.0, False),
        (1.0, 1.0, 1.0, 1.0, 2.0, 1.0, False),
        (1.0, 1.0, 1.0, 2.0, 1.0, 1.0, False),
        (1.0, 1.0, 2.0, 1.0, 1.0, 1.0, False),
        (1.0, 2.0, 1.0, 1.0, 1.0, 1.0, False),
        (2.0, 1.0, 1.0, 1.0, 1.0, 1.0, False),
    ],
)
def test_is_pupil_close_comparator(
    a_pupils, a_meals, a_sen, b_pupils, b_meals, b_sen, expected
):
    is_close = is_pupil_close_comparator(
        {
            "Number of pupils": a_pupils,
            "Percentage Free school meals": a_meals,
            "Percentage SEN": a_sen,
        },
        {
            "Number of pupils": b_pupils,
            "Percentage Free school meals": b_meals,
            "Percentage SEN": b_sen,
        },
    )

    assert is_close == expected
