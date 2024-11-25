import numpy as np
import pandas as pd

from pipeline import pre_processing


def test_revenue_reservce_part_year_academy():
    academies = pd.DataFrame(
        [
            {
                "URN": 0,
                "Trust UPIN": 0,  # Joining this Trust…
                "Valid To": np.nan,
                "Revenue reserve": 1.0,
                "Number of pupils_pro_rata": 5.0,
                "Total pupils in trust_pro_rata": 5.0,
            },
            {
                "URN": 0,
                "Trust UPIN": 1,  # …and leaving this.
                "Valid To": True,
                "Revenue reserve": 1.0,
                "Number of pupils_pro_rata": 5.0,
                "Total pupils in trust_pro_rata": 15.0,
            },
            {
                "URN": 1,
                "Trust UPIN": 1,
                "Valid To": np.nan,
                "Revenue reserve": 1.0,
                "Number of pupils_pro_rata": 10.0,
                "Total pupils in trust_pro_rata": 15.0,
            },
        ],
        index=[0, 0, 1],
    )
    central_services = pd.DataFrame(
        [
            {
                "Trust UPIN": 0,
                "Revenue reserve": 10.0,
            },
            {
                "Trust UPIN": 1,
                "Revenue reserve": 20.0,
            },
        ]
    )

    result = pre_processing._trust_revenue_reserve(
        academies,
        central_services,
    )

    assert result[(result["URN"] == 0) & (result["Trust UPIN"] == 0)].at[
        0, "Revenue reserve"
    ] == 11.0 * (5.0 / 5.0)
    assert (
        result[(result["URN"] == 0) & (result["Trust UPIN"] == 1)].at[
            1, "Revenue reserve"
        ]
        == 0.0
    )
    assert result[(result["URN"] == 1) & (result["Trust UPIN"] == 1)].at[
        2, "Revenue reserve"
    ] == 21.0 * (10.0 / 15.0)
