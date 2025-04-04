import pytest

from pipeline.utils import input


@pytest.mark.parametrize(
    "dict_,expected",
    [
        ({}, 0),
        ({("a",): {}}, 1),
        (
            {
                (
                    "a",
                    "b",
                ): {}
            },
            2,
        ),
    ],
)
def test_multikeydict_len(dict_: dict, expected: int):
    result = input.MultiKeyDefaultDict(dict_, default={})

    assert len(result) == expected


@pytest.mark.parametrize(
    "default",
    [
        ({},),
        ({("a",): []},),
    ],
)
def test_multikeydict_initialised_default(default: dict):
    result = input.MultiKeyDefaultDict({}, default=default)

    assert result[1] == default
    assert result.get(1) == default


@pytest.mark.parametrize(
    "default",
    [
        ({},),
        ({("a",): []},),
    ],
)
def test_multikeydict_default(default: dict):
    result = input.MultiKeyDefaultDict({}, default=default)

    with pytest.raises(ValueError):
        result.get(1, {"n": "n"})


def test_multikeydict_multi():
    value = {"c": "c"}
    result = input.MultiKeyDefaultDict(
        {
            (
                1,
                2,
                3,
            ): value,
        },
        default={},
    )

    assert result[1] == value
    assert result[2] == value
    assert result[3] == value
    assert result.get(1) == value
    assert result.get(2) == value
    assert result.get(3) == value
