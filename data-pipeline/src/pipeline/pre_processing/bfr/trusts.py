import logging
from warnings import simplefilter

import pandas as pd

import pipeline.input_schemas as input_schemas
import pipeline.pre_processing.bfr.calculations as BFR

simplefilter(action="ignore", category=pd.errors.PerformanceWarning)
simplefilter(action="ignore", category=FutureWarning)

logger = logging.getLogger("fbit-data-pipeline")


def build_bfr_historical_data(
    academies_historical: pd.DataFrame | None,
    bfr_sofa_historical: pd.DataFrame | None,
) -> pd.DataFrame | None:
    """
    Derive historical data from BFR SOFA data.

    `academies_historical` must have:

    - Trust UPIN
    - Company Registration Number

    `bfr_sofa_historical` must have:

    - Trust UPIN
    - EFALineNo (containing 430 and 999)
    - Y1P2
    - Y2P2

    The return value will be of the same form as `academies_historical`,
    with an additional colums:

    - "Trust Revenue reserve"
    - "Total pupils in trust"

    :param academies_historical: academy data from a previous year
    :param bfr_sofa_historical: BFR SOFA data from a previous year
    :return: updated, historical data
    """
    if academies_historical is not None:
        academies_historical["Trust Revenue reserve"] = 0.0
        academies_historical["Total pupils in trust"] = 0.0

        if bfr_sofa_historical is not None:
            academies_historical = academies_historical.drop(
                columns=["Trust Revenue reserve", "Total pupils in trust"],
            )

            academies_historical = academies_historical.merge(
                bfr_sofa_historical[bfr_sofa_historical["EFALineNo"] == 430].rename(
                    {"Y2P2": "Trust Revenue reserve"},
                    axis=1,
                )[["Trust UPIN", "Trust Revenue reserve"]],
                on="Trust UPIN",
                how="left",
            )
            academies_historical["Trust Revenue reserve"] *= 1_000
            academies_historical = academies_historical.merge(
                bfr_sofa_historical[bfr_sofa_historical["EFALineNo"] == 999].rename(
                    {"Y1P2": "Total pupils in trust"},
                    axis=1,
                )[["Trust UPIN", "Total pupils in trust"]],
                on="Trust UPIN",
                how="left",
            )

    return academies_historical


def build_bfr_data(
    current_year,
    bfr_sofa_data_path,
    bfr_3y_data_path,
    academies,
    academies_y1=None,
    academies_y2=None,
):
    bfr_sofa = pd.read_csv(
        bfr_sofa_data_path,
        encoding="unicode-escape",
        dtype=input_schemas.bfr_sofa_cols,
        usecols=input_schemas.bfr_sofa_cols.keys(),
    ).rename(columns={"TrustUPIN": "Trust UPIN", "Title": "Category"})
    logger.info(f"BFR sofa raw shape: {bfr_sofa.shape}")

    bfr_sofa = bfr_sofa[
        bfr_sofa["EFALineNo"].isin(
            [298, 430, 335, 380, 211, 220, 199, 200, 205, 210, 999]
        )
    ]
    bfr_sofa.drop_duplicates(inplace=True)
    bfr_sofa[["Y1P1", "Y1P2", "Y2P1", "Y2P2"]] = bfr_sofa[
        ["Y1P1", "Y1P2", "Y2P1", "Y2P2"]
    ].apply(lambda x: x * 1000, axis=1)

    self_gen_income = (
        bfr_sofa[bfr_sofa["EFALineNo"].isin([211, 220])]
        .groupby(["Trust UPIN"])[["Y1P1", "Y1P2", "Y2P1", "Y2P2"]]
        .sum()
        .reset_index()
    )
    self_gen_income["Category"] = "Self-generated income"

    grant_funding = (
        bfr_sofa[bfr_sofa["EFALineNo"].isin([199, 200, 205, 210])]
        .groupby(["Trust UPIN"])[["Y1P1", "Y1P2", "Y2P1", "Y2P2"]]
        .sum()
        .reset_index()
    )
    grant_funding["Category"] = "Grant funding"

    bfr_sofa = pd.concat([bfr_sofa, self_gen_income, grant_funding]).drop_duplicates()

    bfr_3y = pd.read_csv(
        bfr_3y_data_path,
        encoding="unicode-escape",
        dtype=input_schemas.bfr_3y_cols,
        usecols=input_schemas.bfr_3y_cols.keys(),
    ).rename(columns={"TrustUPIN": "Trust UPIN"})
    logger.info(f"BFR 3y raw shape: {bfr_3y.shape}")

    bfr_3y[["Y2", "Y3", "Y4"]] = bfr_3y[["Y2", "Y3", "Y4"]].apply(
        lambda x: x * 1000, axis=1
    )

    bfr_3y["EFALineNo"].replace(
        {2980: 298, 4300: 430, 3800: 380, 9000: 999}, inplace=True
    )
    bfr_3y = bfr_3y[bfr_3y["EFALineNo"].isin([298, 430, 335, 380, 999])]
    bfr_3y.drop_duplicates(inplace=True)

    merged_bfr = bfr_sofa.merge(bfr_3y, how="left", on=("Trust UPIN", "EFALineNo"))

    # Compare input and output Trust counts (by Trust UPIN) and warn on mismatch
    input_trust_count = academies["Trust UPIN"].nunique()

    bfr = (
        academies.groupby("Trust UPIN")
        .first()
        .reset_index()
        .merge(merged_bfr, on="Trust UPIN")
    )

    output_trust_count = bfr["Trust UPIN"].nunique()
    if input_trust_count != output_trust_count:
        logger.warning(
            f"BFR preprocessing Trust count mismatch: input trusts={input_trust_count}, output trusts={output_trust_count}"
        )

    bfr["Category"].replace(
        {
            "Balance c/f to next period ": "Revenue reserve",
            "Pupil numbers (actual and estimated)": "Pupil numbers",
            "Total revenue expenditure": "Total expenditure",
            "Total revenue income": "Total income",
            "Total staff costs": "Staff costs",
        },
        inplace=True,
    )

    if academies_y2 is not None:
        bfr = bfr.merge(
            academies_y2[
                ["Trust UPIN", "Trust Revenue reserve", "Total pupils in trust"]
            ]
            .groupby("Trust UPIN")
            .first()
            .reset_index()
            .rename(
                columns={
                    "Trust Revenue reserve": "Y-2",
                    "Total pupils in trust": "Pupils Y-2",
                }
            ),
            how="left",
            on="Trust UPIN",
        )
    else:
        bfr["Y-2"] = 0.0
        bfr["Pupils Y-2"] = 0.0

    if academies_y1 is not None:
        bfr = bfr.merge(
            academies_y1[
                ["Trust UPIN", "Trust Revenue reserve", "Total pupils in trust"]
            ]
            .groupby("Trust UPIN")
            .first()
            .reset_index()
            .rename(
                columns={
                    "Trust Revenue reserve": "Y-1",
                    "Total pupils in trust": "Pupils Y-1",
                }
            ),
            how="left",
            on="Trust UPIN",
        )
    else:
        bfr["Y-1"] = 0.0
        bfr["Pupils Y-1"] = 0.0

    bfr["Y1"] = bfr["Y2P2"]
    bfr = bfr.drop_duplicates()
    bfr_metrics = BFR.calculate_metrics(bfr.reset_index())

    bfr_pupils = bfr[(bfr["Category"] == "Pupil numbers")][
        ["Trust UPIN", "Y2", "Y3", "Y4"]
    ]
    bfr = bfr[(bfr["Category"] == "Revenue reserve")]

    bfr_metrics = pd.concat([bfr_metrics, BFR.slope_analysis(bfr)])

    bfr_pupils[["Y2", "Y3", "Y4"]] = bfr_pupils[["Y2", "Y3", "Y4"]].apply(
        lambda x: x / 1000, axis=1
    )
    bfr_pupils.rename(
        columns={"Y2": "Pupils Y2", "Y3": "Pupils Y3", "Y4": "Pupils Y4"}, inplace=True
    )

    bfr_pupils = bfr_pupils.merge(
        academies[["Trust UPIN", "Total pupils in trust"]]
        .groupby("Trust UPIN")
        .first()
        .rename(columns={"Total pupils in trust": "Pupils Y1"}),
        how="left",
        on="Trust UPIN",
    )

    bfr = bfr.merge(bfr_pupils, how="left", on="Trust UPIN").drop(
        labels=["Y1P1", "Y1P2", "Y2P1", "Y2P2", "EFALineNo", "Trust Revenue reserve"],
        axis=1,
    )

    pupil_cols = [
        "Company Registration Number",
        "Category",
        "Pupils Y-2",
        "Pupils Y-1",
        "Pupils Y1",
        "Pupils Y2",
        "Pupils Y3",
        "Pupils Y4",
    ]

    bfr_pupils = (
        bfr[pupil_cols]
        .melt(
            id_vars=["Company Registration Number", "Category"],
            value_vars=[
                "Pupils Y-2",
                "Pupils Y-1",
                "Pupils Y1",
                "Pupils Y2",
                "Pupils Y3",
                "Pupils Y4",
            ],
            var_name="Year",
            value_name="Pupils",
        )
        .replace(
            {
                "Pupils Y-2": current_year - 2,
                "Pupils Y-1": current_year - 1,
                "Pupils Y1": current_year,
                "Pupils Y2": current_year + 1,
                "Pupils Y3": current_year + 2,
                "Pupils Y4": current_year + 3,
            }
        )
        .set_index("Company Registration Number")
        .sort_values(by=["Company Registration Number", "Year"])
    )

    cols = [
        "Company Registration Number",
        "Category",
        "Y-2",
        "Y-1",
        "Y1",
        "Y2",
        "Y3",
        "Y4",
    ]
    bfr_revenue_reserve = (
        bfr[cols]
        .melt(
            id_vars=["Company Registration Number", "Category"],
            value_vars=["Y-2", "Y-1", "Y1", "Y2", "Y3", "Y4"],
            var_name="Year",
            value_name="Value",
        )
        .replace(
            {
                "Y-2": current_year - 2,
                "Y-1": current_year - 1,
                "Y1": current_year,
                "Y2": current_year + 1,
                "Y3": current_year + 2,
                "Y4": current_year + 3,
            }
        )
        .set_index("Company Registration Number")
        .sort_values(by=["Company Registration Number", "Year"])
    )

    bfr = bfr_revenue_reserve.merge(
        bfr_pupils, how="left", on=("Company Registration Number", "Category", "Year")
    )

    return bfr, bfr_metrics
