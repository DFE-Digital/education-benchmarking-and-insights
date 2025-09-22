from json import load
import logging
from warnings import simplefilter

import pandas as pd

import pipeline.input_schemas as input_schemas
import pipeline.pre_processing.bfr.calculations as BFR

simplefilter(action="ignore", category=pd.errors.PerformanceWarning)
simplefilter(action="ignore", category=FutureWarning)

logger = logging.getLogger("fbit-data-pipeline")

# BFR SOFA
SOFA_SELF_GENERATED_INCOME_EFALINES = [211, 220]
SOFA_PUPIL_NUMBER_EFALINE = 999
SOFA_GRANT_FUNDING_EFALINES = [199, 200, 205, 210]
SOFA_TRUST_REVENUE_RESERVE_EFALINE = 430
SOFA_SUBTOTAL_INCOME_EFALINE = 980
SOFA_OTHER_COSTS_EFALINE = 335
SOFA_TOTAL_REVENUE_INCOME = 298
SOFA_TOTAL_REVENUE_EXPENDITURE = 380
SOFA_YEAR_COLS = ["Y1P1", "Y1P2", "Y2P1", "Y2P2", "Y3P1", "Y3P2"]

# BFA 3Y
SUBTOTAL_INCOME_EFALINE = 2980
REVENUE_RESERVE_BALANCE_EFALINE = 4300
SUBTOTAL_COSTS_EFALINE = 3800
ESTIMASTED_PUPIL_NUMBERS_EFALINE = 9000
OTHER_COSTS_EFALINE = 335
BFR_3Y_TO_SOFA_MAPPINGS = {
    SUBTOTAL_INCOME_EFALINE: SOFA_TOTAL_REVENUE_INCOME,
    REVENUE_RESERVE_BALANCE_EFALINE: SOFA_TRUST_REVENUE_RESERVE_EFALINE,
    SUBTOTAL_COSTS_EFALINE: SOFA_TOTAL_REVENUE_EXPENDITURE,
    ESTIMASTED_PUPIL_NUMBERS_EFALINE: SOFA_PUPIL_NUMBER_EFALINE,
}
THREE_YEAR_PROJECTION_COLS = ["Y2", "Y3", "Y4"]


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
                bfr_sofa_historical[
                    bfr_sofa_historical["EFALineNo"]
                    == SOFA_TRUST_REVENUE_RESERVE_EFALINE
                ].rename(
                    {"Y2P2": "Trust Revenue reserve"},
                    axis=1,
                )[
                    ["Trust UPIN", "Trust Revenue reserve"]
                ],
                on="Trust UPIN",
                how="left",
            )
            academies_historical["Trust Revenue reserve"] *= 1_000
            academies_historical = academies_historical.merge(
                bfr_sofa_historical[
                    bfr_sofa_historical["EFALineNo"] == SOFA_PUPIL_NUMBER_EFALINE
                ].rename(
                    {"Y1P2": "Total pupils in trust"},
                    axis=1,
                )[
                    ["Trust UPIN", "Total pupils in trust"]
                ],
                on="Trust UPIN",
                how="left",
            )

    return academies_historical


def load_bfr_sofa(bfr_sofa_data_path) -> pd.DataFrame:
    bfr_sofa = pd.read_csv(
        bfr_sofa_data_path,
        encoding="unicode-escape",
        dtype=input_schemas.bfr_sofa_cols,
        usecols=input_schemas.bfr_sofa_cols.keys(),
    ).rename(columns={"TrustUPIN": "Trust UPIN", "Title": "Category"})
    logger.info(f"BFR sofa raw shape: {bfr_sofa.shape}")

    return bfr_sofa


def load_bfr_3y(bfr_3y_data_path) -> pd.DataFrame:
    bfr_3y = pd.read_csv(
        bfr_3y_data_path,
        encoding="unicode-escape",
        dtype=input_schemas.bfr_3y_cols,
        usecols=input_schemas.bfr_3y_cols.keys(),
    ).rename(columns={"TrustUPIN": "Trust UPIN"})
    logger.info(f"BFR 3y raw shape: {bfr_3y.shape}")
    return bfr_3y


def build_custom_sofa_categories(bfr_sofa_filtered) -> tuple[pd.DataFrame, pd.DataFrame]:
    """Create custom aggregate categories from BFR SOFA data"""
    self_gen_income = (
        bfr_sofa_filtered[
            bfr_sofa_filtered["EFALineNo"].isin(SOFA_SELF_GENERATED_INCOME_EFALINES)
        ]
        .groupby(["Trust UPIN"])[SOFA_YEAR_COLS]
        .sum()
        .reset_index()
    )
    self_gen_income["Category"] = "Self-generated income"

    grant_funding = (
        bfr_sofa_filtered[
            bfr_sofa_filtered["EFALineNo"].isin(SOFA_GRANT_FUNDING_EFALINES)
        ]
        .groupby(["Trust UPIN"])[SOFA_YEAR_COLS]
        .sum()
        .reset_index()
    )
    grant_funding["Category"] = "Grant funding"

    return self_gen_income, grant_funding


def build_bfr_data(
    current_year,
    bfr_sofa_data_path,
    bfr_3y_data_path,
    academies,
    academies_y1=None,
    academies_y2=None,
):
    bfr_sofa_raw = load_bfr_sofa(bfr_sofa_data_path)

    bfr_sofa_filtered = bfr_sofa_raw[
        bfr_sofa_raw["EFALineNo"].isin(
            [
                *SOFA_SELF_GENERATED_INCOME_EFALINES,
                SOFA_PUPIL_NUMBER_EFALINE,
                *SOFA_GRANT_FUNDING_EFALINES,
                SOFA_TRUST_REVENUE_RESERVE_EFALINE,
                SOFA_SUBTOTAL_INCOME_EFALINE,
                SOFA_OTHER_COSTS_EFALINE,
                SOFA_TOTAL_REVENUE_INCOME,
                SOFA_TOTAL_REVENUE_EXPENDITURE,
            ]
        )
    ]
    bfr_sofa_filtered[SOFA_YEAR_COLS] = bfr_sofa_filtered[SOFA_YEAR_COLS].apply(
        lambda x: x * 1000, axis=1
    )

    sofa_self_generated_income, sofa_grant_funding = build_custom_sofa_categories(
        bfr_sofa_filtered
    )

    bfr_sofa = pd.concat(
        [bfr_sofa_filtered, sofa_self_generated_income, sofa_grant_funding]
    ).drop_duplicates()

    bfr_3y_raw = load_bfr_3y(bfr_3y_data_path)

    # Scale year columns
    bfr_3y_raw[THREE_YEAR_PROJECTION_COLS] = bfr_3y_raw[THREE_YEAR_PROJECTION_COLS].apply(
        lambda x: x * 1000, axis=1
    )

    # Normalise line numbers between SOFA/3Y and filter
    bfr_3y_raw["EFALineNo"].replace(BFR_3Y_TO_SOFA_MAPPINGS, inplace=True)
    bfr_3y = bfr_3y_raw[
        bfr_3y_raw["EFALineNo"].isin(
            [*BFR_3Y_TO_SOFA_MAPPINGS.values(), OTHER_COSTS_EFALINE]
        )
    ]

    merged_bfr = bfr_sofa.merge(bfr_3y, how="left", on=("Trust UPIN", "EFALineNo"))

    bfr = (
        academies.groupby("Trust UPIN")
        .first()
        .reset_index()
        .merge(merged_bfr, on="Trust UPIN")
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

    bfr_pupils[THREE_YEAR_PROJECTION_COLS] = bfr_pupils[
        THREE_YEAR_PROJECTION_COLS
    ].apply(lambda x: x / 1000, axis=1)
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
