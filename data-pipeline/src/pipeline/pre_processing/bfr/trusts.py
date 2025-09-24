from json import load
import logging
from warnings import simplefilter

import pandas as pd

from .loader import load_bfr_sofa, load_bfr_3y
from .calculations import calculate_metrics, slope_analysis

simplefilter(action="ignore", category=pd.errors.PerformanceWarning)
simplefilter(action="ignore", category=FutureWarning)

logger = logging.getLogger(__name__)

# BFR SOFA
SOFA_SELF_GENERATED_INCOME_EFALINES = [211, 220]
SOFA_PUPIL_NUMBER_EFALINE = 999
SOFA_GRANT_FUNDING_EFALINES = [199, 200, 205, 210]
SOFA_TRUST_REVENUE_RESERVE_EFALINE = 430
SOFA_SUBTOTAL_INCOME_EFALINE = 980
SOFA_OTHER_COSTS_EFALINE = 335
SOFA_TOTAL_REVENUE_INCOME = 298
SOFA_TOTAL_REVENUE_EXPENDITURE = 380
SOFA_IT_SPEND_LINES = [336, 337, 338, 339, 340, 341, 342]
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

BFR_CATEGORY_MAPPINGS = {
    "Balance c/f to next period ": "Revenue reserve",
    "Pupil numbers (actual and estimated)": "Pupil numbers",
    "Total revenue expenditure": "Total expenditure",
    "Total revenue income": "Total income",
    "Total staff costs": "Staff costs",
}


def build_bfr_historical_data(
    academies_historical: pd.DataFrame | None,
    bfr_sofa_historical: pd.DataFrame | None,
) -> pd.DataFrame | None:
    """
    Derive historical pupil numbers and revenue reserves from BFR SOFA data.
    Also add the historic company reference number from the historic academies file.
    """
    # If there is no BFR SOFA, we can't get historic revenue/pupils
    if academies_historical is not None and bfr_sofa_historical is None:
        academies_historical["Trust Revenue reserve"] = 0.0
        academies_historical["Total pupils in trust"] = 0
        return academies_historical
    if academies_historical is not None and bfr_sofa_historical is not None:
        historic_bfr_with_crn = academies_historical.merge(
            bfr_sofa_historical[
                bfr_sofa_historical["EFALineNo"] == SOFA_TRUST_REVENUE_RESERVE_EFALINE
            ].rename(
                {"Y2P2": "Trust Revenue reserve"},
                axis=1,
            )[
                ["Trust UPIN", "Trust Revenue reserve"]
            ],
            on="Trust UPIN",
            how="left",
        )
        historic_bfr_with_crn["Trust Revenue reserve"] *= 1_000
        historic_bfr_with_crn = historic_bfr_with_crn.merge(
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
        return historic_bfr_with_crn
    return None


def aggregate_custom_sofa_categories(
    bfr_sofa_filtered,
) -> tuple[pd.DataFrame, pd.DataFrame]:
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


def merge_historic_bfr(bfr, historic_bfr, year: str) -> pd.DataFrame:
    if historic_bfr is not None:
        bfr = bfr.merge(
            historic_bfr[
                ["Trust UPIN", "Trust Revenue reserve", "Total pupils in trust"]
            ]
            .groupby("Trust UPIN")
            .first()
            .reset_index()
            .rename(
                columns={
                    "Trust Revenue reserve": year,
                    "Total pupils in trust": f"Pupils {year}",
                }
            ),
            how="left",
            on="Trust UPIN",
        )
    if historic_bfr is None:
        bfr[year] = 0.0
        bfr[f"Pupils {year}"] = 0.0
    return bfr


def preprocess_bfr_sofa(bfr_sofa_raw):
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
                *SOFA_IT_SPEND_LINES,
            ]
        )
    ]
    bfr_sofa_filtered[SOFA_YEAR_COLS] = bfr_sofa_filtered[SOFA_YEAR_COLS].apply(
        lambda x: x * 1000, axis=1
    )
    sofa_self_generated_income, sofa_grant_funding = aggregate_custom_sofa_categories(
        bfr_sofa_filtered
    )
    bfr_sofa_with_aggregated_categories = pd.concat(
        [bfr_sofa_filtered, sofa_self_generated_income, sofa_grant_funding]
    ).drop_duplicates()

    return bfr_sofa_with_aggregated_categories


def preprocess_bfr_3y(bfr_3y_raw):
    # Normalise line numbers between SOFA/3Y and filter 3Y
    bfr_3y_raw["EFALineNo"].replace(BFR_3Y_TO_SOFA_MAPPINGS, inplace=True)
    bfr_3y_filtered = bfr_3y_raw[
        bfr_3y_raw["EFALineNo"].isin(
            [*BFR_3Y_TO_SOFA_MAPPINGS.values(), OTHER_COSTS_EFALINE]
        )
    ]
    bfr_3y_filtered[THREE_YEAR_PROJECTION_COLS] = bfr_3y_filtered[
        THREE_YEAR_PROJECTION_COLS
    ].apply(lambda x: x * 1000, axis=1)

    return bfr_3y_filtered


def melt_it_spend_rows_from_bfr(bfr, current_year):
    return (
        bfr[bfr["EFALineNo"].isin(SOFA_IT_SPEND_LINES)]
        .assign(
            Y1P_Total=lambda x: x["Y1P1"] + x["Y1P2"],
            Y2P_Total=lambda x: x["Y2P1"] + x["Y2P2"],
            Y3P_Total=lambda x: x["Y3P1"] + x["Y3P2"],
        )
        .melt(
            id_vars=["Category", "Company Registration Number"],
            value_vars=["Y1P_Total", "Y2P_Total", "Y3P_Total"],
            var_name="Year",
            value_name="Value",
        )
        .replace(
            {
                "Y1P_Total": current_year,
                "Y2P_Total": current_year + 1,
                "Y3P_Total": current_year + 2,
            }
        )
        .set_index("Company Registration Number")
        .sort_values(by=["Company Registration Number", "Year"])
    )


def melt_pupil_numbers_from_bfr(bfr, current_year):
    return (
        bfr.melt(
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


def melt_revenue_reserve_numbers_from_bfr(bfr, current_year):
    return (
        bfr[bfr["EFALineNo"].isin([SOFA_TRUST_REVENUE_RESERVE_EFALINE])]
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


def prepare_current_and_future_pupils(bfr_data, academies):
    """
    The current year BFR_SOFA (Y2P2) doesn't have current year pupil numbers as
    it is released halfway through the year, so we get them from the academies 
    data (aka the academy year census). Future years come from BFR_3Y.
    """
    bfr_pupils = bfr_data[(bfr_data["Category"] == "Pupil numbers")][
        ["Trust UPIN", "Y2", "Y3", "Y4"]
    ]
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
    return bfr_pupils


def build_bfr_data(
    current_year,
    bfr_sofa_data_path,
    bfr_3y_data_path,
    academies,
    historic_bfr_y1=None,
    historic_bfr_y2=None,
):
    bfr_sofa_raw = load_bfr_sofa(bfr_sofa_data_path)
    bfr_sofa_preprocessed = preprocess_bfr_sofa(bfr_sofa_raw)

    bfr_3y_raw = load_bfr_3y(bfr_3y_data_path)
    bfr_3y_preprocessed = preprocess_bfr_3y(bfr_3y_raw)

    merged_bfr = bfr_sofa_preprocessed.merge(
        bfr_3y_preprocessed, how="left", on=("Trust UPIN", "EFALineNo")
    )
    # Normalise Category strings between SOFA/3Y
    merged_bfr["Category"].replace(BFR_CATEGORY_MAPPINGS, inplace=True)
    # Add CRN from academies
    merged_bfr_with_crn = (
        academies[["Company Registration Number", "Trust UPIN"]]
        .drop_duplicates(subset=["Trust UPIN"])
        .merge(merged_bfr, on="Trust UPIN")
    )

    # Pupil numbers and revenue reserve are sourced from BFR_SOFA for the previous 2 years.
    merged_bfr_with_1y_historic_data = merge_historic_bfr(
        merged_bfr_with_crn, historic_bfr_y2, "Y-2"
    )
    merged_bfr_with_2y_historic_data = merge_historic_bfr(
        merged_bfr_with_1y_historic_data, historic_bfr_y1, "Y-1"
    )

    # Y1 is taken to be BFR_SOFA Y2P2 for finance metrics
    merged_bfr_with_2y_historic_data["Y1"] = merged_bfr_with_2y_historic_data["Y2P2"]
    bfr_rows_for_normalised_finance_metrics = merged_bfr_with_2y_historic_data[
        merged_bfr_with_2y_historic_data["Category"].isin(
            [
                "Total income",
                "Revenue reserve",
                "Staff costs",
                "Total expenditure",
                "Self-generated income"
            ]
        )
    ]
    bfr_metrics = calculate_metrics(bfr_rows_for_normalised_finance_metrics.reset_index())
    bfr_rows_for_slope_analysis = merged_bfr_with_2y_historic_data[
        merged_bfr_with_2y_historic_data["Category"] == "Revenue reserve"
    ]
    bfr_slope_analysis = slope_analysis(bfr_rows_for_slope_analysis)
    bfr_metrics_and_slope_analysis = pd.concat([bfr_metrics, bfr_slope_analysis])

    # Current pupil numbers from academies (that year's census), future from bfr_3y
    bfr_pupils = prepare_current_and_future_pupils(
        bfr_data=merged_bfr_with_2y_historic_data, academies=academies
    )
    bfr_final_wide = merged_bfr_with_2y_historic_data.merge(
        bfr_pupils, how="left", on="Trust UPIN"
    )

    # The BFR table is long/melted
    it_spend_melted_rows = melt_it_spend_rows_from_bfr(
        bfr_final_wide, current_year
    )
    revenue_reserve_melted_rows = melt_revenue_reserve_numbers_from_bfr(
        bfr_final_wide, current_year
    )
    pupil_numbers_melted_rows = melt_pupil_numbers_from_bfr(
        bfr_final_wide, current_year
    )
    it_spend_and_revenue_reserve_melted_records = pd.concat(
        [revenue_reserve_melted_rows, it_spend_melted_rows]
    )

    # Add pupil numbers to final melted rows
    bfr_final_long = it_spend_and_revenue_reserve_melted_records.merge(
        pupil_numbers_melted_rows,
        how="left",
        on=("Company Registration Number", "Category", "Year"),
    )

    return bfr_final_long, bfr_metrics_and_slope_analysis
