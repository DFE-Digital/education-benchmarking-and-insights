import pandas as pd

from pipeline.pre_processing.ancillary import gias


def test_empty():
    df = pd.DataFrame({"URN": []})
    linkable = pd.DataFrame({"URN": []}).set_index("URN")
    gias_links = pd.DataFrame({"URN": [], "LinkURN": []})

    result = gias.link_data(df, linkable, gias_links)

    assert result.index.name == "URN"
    assert len(result.index) == 0


def test_no_match():
    df = pd.DataFrame({"URN": [4, 5, 6]})
    linkable = pd.DataFrame({"URN": [1, 2, 3]}).set_index("URN")
    gias_links = pd.DataFrame({"URN": [1, 2, 3], "LinkURN": [1, 2, 3]})

    result = gias.link_data(df, linkable, gias_links)

    assert result.index.name == "URN"
    assert result.index.equals(linkable.index)


def test_complete_match():
    df = pd.DataFrame({"URN": [4, 5, 6]})
    linkable = pd.DataFrame({"URN": [1, 2, 3]}).set_index("URN")
    gias_links = pd.DataFrame({"URN": [4, 5, 6], "LinkURN": [1, 2, 3]})

    result = gias.link_data(df, linkable, gias_links)

    assert result.index.name == "URN"
    assert list(result.index) == [1, 2, 3, 4, 5, 6]


def test_partial_match():
    df = pd.DataFrame({"URN": [4, 5, 6]})
    linkable = pd.DataFrame({"URN": [3, 4, 5]}).set_index("URN")
    gias_links = pd.DataFrame({"URN": [4, 5, 6], "LinkURN": [1, 2, 3]})

    result = gias.link_data(df, linkable, gias_links)

    assert result.index.name == "URN"
    assert list(result.index) == [3, 4, 5, 6]


def test_partial_match_data():
    df = pd.DataFrame({"URN": [4, 5, 6]})
    linkable = pd.DataFrame(
        {
            "URN": [3, 4, 5],
            "A": ["3", "4", "5"],
            "B": ["3", "4", "5"],
            "C": ["3", "4", "5"],
        }
    ).set_index("URN")
    gias_links = pd.DataFrame({"URN": [4, 5, 6], "LinkURN": [1, 2, 3]})

    result = gias.link_data(df, linkable, gias_links)

    assert result.index.name == "URN"
    assert list(result.index) == [3, 4, 5, 6]
    assert result.loc[6, "A"] == "3"
    assert result.loc[6, "B"] == "3"
    assert result.loc[6, "C"] == "3"
