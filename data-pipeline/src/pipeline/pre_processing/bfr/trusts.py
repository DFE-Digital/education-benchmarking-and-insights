import logging
from warnings import simplefilter

import pandas as pd

from .config import (
    BFR_3Y_TO_SOFA_MAPPINGS,
    BFR_CATEGORY_MAPPINGS,
    OTHER_COSTS_EFALINE,
    SOFA_GRANT_FUNDING_EFALINES,
    SOFA_IT_SPEND_LINES,
    SOFA_OTHER_COSTS_EFALINE,
    SOFA_PUPIL_NUMBER_EFALINE,
    SOFA_SELF_GENERATED_INCOME_EFALINES,
    SOFA_SUBTOTAL_INCOME_EFALINE,
    SOFA_TOTAL_REVENUE_EXPENDITURE,
    SOFA_TOTAL_REVENUE_INCOME,
    SOFA_TRUST_REVENUE_RESERVE_EFALINE,
    SOFA_YEAR_COLS,
    THREE_YEAR_PROJECTION_COLS,
)
from .forecast_and_risk import get_bfr_forecast_and_risk_data
from .it_spend import get_bfr_it_spend_rows
from .loader import load_bfr_3y, load_bfr_sofa

simplefilter(action="ignore", category=pd.errors.PerformanceWarning)
simplefilter(action="ignore", category=FutureWarning)

logger = logging.getLogger(__name__)


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
    # No Academies file means that we can't link the historic data on CRN
    return None


def aggregate_efalines_over_years(
    bfr, efa_lines: list[int], year_cols: list[str], aggregated_category_name: str
):
    bfr_aggregated_category_rows = (
        bfr[bfr["EFALineNo"].isin(efa_lines)]
        .groupby(["Trust UPIN"])[year_cols]
        .sum()
        .reset_index()
    )
    bfr_aggregated_category_rows["Category"] = aggregated_category_name
    return bfr_aggregated_category_rows


def aggregate_custom_sofa_categories(
    bfr_sofa_filtered,
) -> tuple[pd.DataFrame, pd.DataFrame]:
    """Create custom aggregate categories from BFR SOFA data
    Return just the new category rows, summed over year"""
    self_gen_income = aggregate_efalines_over_years(
        bfr_sofa_filtered,
        SOFA_SELF_GENERATED_INCOME_EFALINES,
        SOFA_YEAR_COLS,
        "Self-generated income",
    )
    grant_funding = aggregate_efalines_over_years(
        bfr_sofa_filtered, SOFA_GRANT_FUNDING_EFALINES, SOFA_YEAR_COLS, "Grant funding"
    )

    return self_gen_income, grant_funding


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
    # Rename categories for FBIT
    bfr_sofa_with_aggregated_categories["Category"] = (
        bfr_sofa_with_aggregated_categories["Category"].replace(BFR_CATEGORY_MAPPINGS)
    )

    return bfr_sofa_with_aggregated_categories


def preprocess_bfr_3y(bfr_3y_raw):
    # Normalise line numbers between SOFA/3Y and filter 3Y
    bfr_3y_raw["EFALineNo"] = bfr_3y_raw["EFALineNo"].replace(BFR_3Y_TO_SOFA_MAPPINGS)
    bfr_3y_filtered = bfr_3y_raw[
        bfr_3y_raw["EFALineNo"].isin(
            [*BFR_3Y_TO_SOFA_MAPPINGS.values(), OTHER_COSTS_EFALINE]
        )
    ]
    bfr_3y_filtered[THREE_YEAR_PROJECTION_COLS] = bfr_3y_filtered[
        THREE_YEAR_PROJECTION_COLS
    ].apply(lambda x: x * 1000, axis=1)

    return bfr_3y_filtered


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
    bfr_pupils = bfr_pupils.rename(
        columns={"Y2": "Pupils Y2", "Y3": "Pupils Y3", "Y4": "Pupils Y4"}
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
    # Add CRN from academies
    merged_bfr_with_crn = (
        academies[["Company Registration Number", "Trust UPIN"]]
        .drop_duplicates(subset=["Trust UPIN"])
        .merge(merged_bfr, on="Trust UPIN")
    )

    bfr_forecast_and_risk_rows, bfr_forecast_and_risk_metrics = (
        get_bfr_forecast_and_risk_data(
            merged_bfr_with_crn,
            historic_bfr_y2,
            historic_bfr_y1,
            current_year,
            academies,
        )
    )
    # IT spend breakdown was introduced from the 2025 return
    if current_year > 2024:
        bfr_it_spend_rows = get_bfr_it_spend_rows(merged_bfr_with_crn, current_year)
        bfr_final_long = pd.concat([bfr_forecast_and_risk_rows, bfr_it_spend_rows])
        return bfr_final_long, bfr_forecast_and_risk_metrics
    return bfr_forecast_and_risk_rows, bfr_forecast_and_risk_metrics
