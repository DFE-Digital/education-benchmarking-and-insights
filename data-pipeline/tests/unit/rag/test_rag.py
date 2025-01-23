import numpy as np
import pandas as pd
import pytest

import pipeline.config as config
import pipeline.rag as rag


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
    is_close = rag.is_area_close_comparator(
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
    is_close = rag.is_pupil_close_comparator(
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


@pytest.mark.parametrize(
    "type,a_pupils,a_meals,a_sen,a_floor,a_age,b_pupils,b_meals,b_sen,b_floor,b_age,expected",
    [
        ("Pupil", 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, True),
        ("Pupil", 1.0, 1.0, 1.0, 1.0, 1.0, 2.0, 1.0, 1.0, 1.0, 1.0, False),
        ("Building", 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, True),
        ("Building", 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 2.0, 1.0, False),
    ],
)
def test_is_close_compartor(
    type,
    a_pupils,
    a_meals,
    a_sen,
    a_floor,
    a_age,
    b_pupils,
    b_meals,
    b_sen,
    b_floor,
    b_age,
    expected,
):
    is_close = rag.is_close_comparator(
        type,
        {
            "Number of pupils": a_pupils,
            "Percentage Free school meals": a_meals,
            "Percentage SEN": a_sen,
            "Total Internal Floor Area": a_floor,
            "Age Average Score": a_age,
        },
        {
            "Number of pupils": b_pupils,
            "Percentage Free school meals": b_meals,
            "Percentage SEN": b_sen,
            "Total Internal Floor Area": b_floor,
            "Age Average Score": b_age,
        },
    )

    assert is_close == expected


def test_find_percentile():
    assert (
        rag.find_percentile(np.array([20, 5, 6, 15, 16, 19, 22, 25, 76, 150]), 20) == 60
    )


@pytest.mark.parametrize(
    "value,data,diff_median,percent_diff,percentile,decile,expected_rag,ofsted,key",
    [
        (
            20,
            [20, 5, 6, 15, 16, 19, 22, 25, 76, 150],
            0.5,
            2.564102564102564,
            60.0,
            6,
            "amber",
            "outstanding",
            "outstanding",
        ),
        (
            150,
            [150, 5, 6, 15, 16, 19, 22, 25, 76, 20],
            130.5,
            669.2307692307693,
            100.0,
            10,
            "red",
            "outstanding",
            "outstanding",
        ),
        (
            15,
            [15, 5, 6, 150, 16, 19, 22, 25, 76, 20],
            -4.5,
            -23.076923076923077,
            30.0,
            3,
            "green",
            "outstanding",
            "outstanding",
        ),
        (
            15,
            [15, 5, 6, 150, 16, 19, 22, 25, 76, 20],
            -4.5,
            -23.076923076923077,
            30.0,
            3,
            "green",
            "",
            "other",
        ),
    ],
)
def test_category_stats(
    value,
    data,
    diff_median,
    percent_diff,
    percentile,
    decile,
    expected_rag,
    ofsted,
    key,
):
    category = "Teaching and Teaching support staff_Sub Cat"
    data = pd.DataFrame(
        {category: data},
        index=[item for item in range(100000, 100000 + len(data))],
    )

    expected = {
        "URN": 100000,
        "Category": "Teaching and Teaching support staff",
        "SubCategory": "Sub Cat",
        "Value": value,
        "Median": 19.5,
        "DiffMedian": diff_median,
        "Key": key,
        "PercentDiff": percent_diff,
        "Percentile": percentile,
        "Decile": decile,
        "RAG": expected_rag,
    }

    rag_settings = config.rag_category_settings["Teaching and Teaching support staff"]
    assert (
        rag.category_stats(100000, category, data, ofsted, rag_settings, 10) == expected
    )
