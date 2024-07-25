import pandas
import pytest

from src.pipeline.rag import find_percentile


@pytest.mark.parametrize(
    "score,expected",
    [
        (2, 9),
        (4, 18),
        (6, 27),
        (8, 36),
        (13, 45),
        (16, 54),
        (22, 63),
        (35, 72),
        (40, 81),
        (42, 90),
        (48, 100),
    ],
)
def test_simple_range(score: int, expected: int):
    """
    Note: expected values derived from
    `scipy.stats.percentileofscore(kind="rank")`.

    :param score: to determine percentile of
    :param expected: expected percentile
    """
    scores = pandas.Series([2, 4, 6, 8, 13, 16, 22, 35, 40, 42, 48])

    assert int(find_percentile(scores, score)) == expected
