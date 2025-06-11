import pandas as pd
import numpy as np

import pipeline.config as config
import pipeline.mappings as mappings


def build_trust_data(
    academies: pd.DataFrame, high_exec_pay_per_trust: pd.DataFrame | None
) -> pd.DataFrame:
    """
    Build Trust financial information.

    Academy financial information are rolled up to Trust level.
    High exec pay is added.

    :param academies: Academy financial information
    :return: Trust-level financial information
    """
    trust_data = (
        academies.reset_index()[
            [
                c
                for c in config.trust_db_projections.keys()
                if c != "Trust Financial Position" and c != "EMLBand"
            ]
        ]
        .groupby("Company Registration Number")
        .sum()
    )

    trust_data["Trust Financial Position"] = trust_data["In year balance"].map(
        mappings.map_is_surplus_deficit
    )

    if high_exec_pay_per_trust is not None:
        trust_data_with_high_exec_pay = trust_data.merge(
            high_exec_pay_per_trust, on="Company Registration Number", how="left"
        )
        return trust_data_with_high_exec_pay

    trust_data["EMLBand"] = np.nan
    return trust_data
