import pandas as pd

import pipeline.config as config
import pipeline.mappings as mappings


def build_trust_data(academies: pd.DataFrame) -> pd.DataFrame:
    """
    Build Trust financial information.

    Academy financial information are rolled up to Trust level.

    :param academies: Academy financial information
    :return: Trust-level financial information
    """
    df = (
        academies.reset_index()[
            [
                c
                for c in config.trust_db_projections.keys()
                if c != "Trust Financial Position"
            ]
        ]
        .groupby("Company Registration Number")
        .sum()
    )

    df["Trust Financial Position"] = df["In year balance"].map(
        mappings.map_is_surplus_deficit
    )

    return df
