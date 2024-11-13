import pandas as pd

from pipeline import config


def _is_early_transfer(row: pd.Series) -> bool:
    """
    Whether an row's "Date joined or opened if in period" value is
    within the first 10 days of September (inclusive).

    :param row: representing a single academy
    :return: whether this is an early transfer
    """
    if not (
        date_ := pd.to_datetime(
            row["Date joined or opened if in period"], dayfirst=True
        )
    ):
        return False

    if date_.month == 9 and 1 <= date_.day <= 10:
        return True

    return False


def map_is_early_transfer(academies: pd.DataFrame) -> pd.DataFrame:
    """
    Whether an academy is an "early transfer".

    Specifically, whether the academy has transferred within the first
    10 days of the academic year.

    :param academies: academy data
    :return: updated data
    """
    academies["Is Early Transfer"] = academies.apply(_is_early_transfer, axis=1)

    return academies


def map_has_financial_data(
    academies: pd.DataFrame,
) -> pd.DataFrame:
    """
    Whether the academy data contains financial data.

    If all "financial" data columns are _not_ null—i.e. there are at
    least _some_ financial data—the data are considered to contain
    financial information.

    Note: colums must be derive as per `map_cost_income_categories()`.

    :param academies: academy data
    :return: updated DataFrame
    """
    financial_columns = list(
        (
            config.cost_category_map["academies"]
            | config.income_category_map["academies"]
        ).values()
    )

    academies["Financial Data Present"] = (
        ~academies[financial_columns].isna().all(axis=1)
    )

    return academies


def map_partial_year_present(academies: pd.DataFrame) -> pd.DataFrame:
    """
    Whether the academy data contains a part-year submission.

    This is based on the period covered by the return.

    :param academies: academy data
    :return: updated DataFrame
    """
    academies["Partial Years Present"] = (
        academies["Period covered by return"] != 12
    )

    return academies
