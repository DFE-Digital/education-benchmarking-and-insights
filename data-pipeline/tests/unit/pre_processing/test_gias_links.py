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
    """
    - 3 and 4 are present in `df` and `linkable`, 6 is missing
    - GIAS links 3 and 6
    - `linkable` should be extended to include 6 with the data from 3
    """
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

    assert list(result.columns) == ["A", "B", "C"]
    assert result.index.name == "URN"
    assert list(result.index) == [3, 4, 5, 6]
    assert result.loc[6].to_dict() == {"A": "3", "B": "3", "C": "3"}


def test_original_data_prioritised():
    """
    - 4 and 5 are present in `df` and `linkable`, 6 is missing
    - GIAS links 3 and 4, 4 and 5, 5 and 6
    - `linkable` should be extended to include 6 with the data from 5
    - the data for 4 and 5, despite the GIAS links, must not be changed
    """
    df = pd.DataFrame({"URN": [4, 5, 6]})
    linkable = pd.DataFrame(
        {
            "URN": [3, 4, 5],
            "A": ["3", "4", "5"],
            "B": ["3", "4", "5"],
            "C": ["3", "4", "5"],
        }
    ).set_index("URN")
    gias_links = pd.DataFrame({"URN": [4, 5, 6], "LinkURN": [3, 4, 5]})

    result = gias.link_data(df, linkable, gias_links)

    assert result.index.name == "URN"
    assert list(result.columns) == ["A", "B", "C"]
    assert list(result.index) == [3, 4, 5, 6]
    assert result.loc[4].to_dict() == {"A": "4", "B": "4", "C": "4"}
    assert result.loc[5].to_dict() == {"A": "5", "B": "5", "C": "5"}
    assert result.loc[6].to_dict() == {"A": "5", "B": "5", "C": "5"}
