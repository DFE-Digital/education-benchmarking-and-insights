import numpy as np
import pandas as pd

from pipeline.pre_processing.aar import _trust_revenue_reserve


def test_revenue_reserve():
    """
    Academy `Revenue reserve` plus apportioned Central Services
    `Revenue reserve`.
    """
    academies = pd.DataFrame(
        [
            {
                "URN": 0,
                "Trust UPIN": 0,
                "Valid To": np.nan,
                "Revenue reserve": 1.0,
                "Number of pupils_pro_rata": 5.0,
                "Total pupils in trust_pro_rata": 5.0,
            },
        ],
        index=[0],
    )
    central_services = pd.DataFrame(
        [
            {
                "Trust UPIN": 0,
                "Revenue reserve": 10.0,
            },
        ]
    )

    result = _trust_revenue_reserve(
        academies,
        central_services,
    )

    assert result.iloc[0]["Revenue reserve"] == 1.0 + (10.0 * (5.0 / 5.0))


def test_share_revenue_reserve():
    """
    Sum of all Trust's Academies' `Revenue reserve` value, plus Central
    Services `Revenue reserve`, apportioned back.
    """
    academies = pd.DataFrame(
        [
            {
                "URN": 0,
                "Trust UPIN": 0,
                "Valid To": np.nan,
                "Revenue reserve": 1.0,
                "Number of pupils_pro_rata": 5.0,
                "Total pupils in trust_pro_rata": 5.0,
            },
        ],
        index=[0],
    )
    central_services = pd.DataFrame(
        [
            {
                "Trust UPIN": 0,
                "Revenue reserve": 10.0,
            },
        ]
    )

    result = _trust_revenue_reserve(
        academies,
        central_services,
    )

    assert result.iloc[0]["Share Revenue reserve"] == ((1.0 + 10.0) * (5.0 / 5.0))


def test_revenue_reserve_part_year_academy():
    """
    `Revenue reserve` will be the sum of:

    - the Academy's `Revenue reserve`
    - the apportioned (pro rata) Central Services `Revenue reserve` of
      the Academy's _final_ Trust

    `Shared Revenue reserve` will be the (pro rata) apportionment of:

    - the sum of `Revenue reserve` for all Academies in the Trust
      - or zero, in the case of an Academy moving _from_ a Trust
    - the Central Services `Revenue reserve` for the Academy's _final_
      Trust
    """
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

    result = _trust_revenue_reserve(
        academies,
        central_services,
    )

    assert result[(result["URN"] == 0) & (result["Trust UPIN"] == 0)].at[
        0, "Revenue reserve"
    ] == 1.0 + (10.0 * (5.0 / 5.0))
    assert (
        result[(result["URN"] == 0) & (result["Trust UPIN"] == 1)].at[
            1, "Revenue reserve"
        ]
        == 0.0  # MUST be zero as leaving this Trust.
    )
    assert result[(result["URN"] == 1) & (result["Trust UPIN"] == 1)].at[
        2, "Revenue reserve"
    ] == 1.0 + (20.0 * (10.0 / 15.0))

    assert result[(result["URN"] == 0) & (result["Trust UPIN"] == 0)].at[
        0, "Share Revenue reserve"
    ] == 11.0 * (5.0 / 5.0)
    assert (
        result[(result["URN"] == 0) & (result["Trust UPIN"] == 1)].at[
            1, "Share Revenue reserve"
        ]
        == 0.0  # MUST be zero as leaving this Trust.
    )
    assert result[(result["URN"] == 1) & (result["Trust UPIN"] == 1)].at[
        2, "Share Revenue reserve"
    ] == 21.0 * (10.0 / 15.0)
