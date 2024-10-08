import pandas as pd

from src.pipeline import config


def map_has_financial_data(
    maintained_schools: pd.DataFrame,
) -> pd.DataFrame:
    """
    Whether the maintained school data contains financial data.

    If all "financial" data columns are _not_ null—i.e. there are at
    least _some_ financial data—the data are considered to contain
    financial information.

    Note: colums must be derive as per `map_cost_income_categories()`.

    :param maintained_schools: maintained schools data
    :return: updated DataFrame
    """
    financial_columns = list(
        (
            config.cost_category_map["maintained_schools"]
            | config.income_category_map["maintained_schools"]
        ).values()
    )

    maintained_schools["Financial Data Present"] = (
        ~maintained_schools[financial_columns].isna().all(axis=1)
    )

    return maintained_schools
