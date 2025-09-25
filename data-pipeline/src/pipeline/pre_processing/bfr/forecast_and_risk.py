from .config import SOFA_TRUST_REVENUE_RESERVE_EFALINE, THREE_YEAR_PROJECTION_COLS
from .calculations import calculate_metrics, slope_analysis
import pandas as pd


def melt_forecast_and_risk_pupil_numbers_from_bfr(bfr, current_year):
    forecast_and_risk_pupil_numbers_melted_rows = (
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
    return forecast_and_risk_pupil_numbers_melted_rows


def melt_forecast_and_risk_revenue_reserves_from_bfr(bfr, current_year):
    forecast_and_risk_revenue_reserves_melted_rows = (
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
    return forecast_and_risk_revenue_reserves_melted_rows


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


def prepare_current_and_future_pupils(bfr_data, academies):
    """
    The current year BFR_SOFA (Y2) doesn't have current year pupil numbers as
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


def _prepare_merged_bfr_for_forecast_and_risk(
    merged_bfr_with_crn, historic_bfr_y2, historic_bfr_y1
):
    """
    Prepares the merged BFR data by adding historic data for forecast and risk calculations.
    """
    # Pupil numbers and revenue reserve are sourced from BFR_SOFA for the previous 2 years.
    merged_bfr_with_1y_historic_data = merge_historic_bfr(
        merged_bfr_with_crn, historic_bfr_y2, "Y-2"
    )
    merged_bfr_with_2y_historic_data = merge_historic_bfr(
        merged_bfr_with_1y_historic_data, historic_bfr_y1, "Y-1"
    )
    # Y1 is taken to be BFR_SOFA Y2P2 for finance metrics
    merged_bfr_with_2y_historic_data["Y1"] = merged_bfr_with_2y_historic_data["Y2P2"]
    return merged_bfr_with_2y_historic_data


def _calculate_forecast_and_risk_metrics(merged_bfr_with_2y_historic_data):
    """
    Calculates the forecast and risk metrics from the merged BFR data.
    """
    bfr_rows_for_normalised_finance_metrics = merged_bfr_with_2y_historic_data[
        merged_bfr_with_2y_historic_data["Category"].isin(
            [
                "Total income",
                "Revenue reserve",
                "Staff costs",
                "Total expenditure",
                "Self-generated income",
            ]
        )
    ]
    bfr_forecast_and_risk_metrics = calculate_metrics(
        bfr_rows_for_normalised_finance_metrics.reset_index()
    )
    bfr_rows_for_slope_analysis = merged_bfr_with_2y_historic_data[
        merged_bfr_with_2y_historic_data["Category"] == "Revenue reserve"
    ]
    bfr_forecast_and_risk_slope_analysis = slope_analysis(bfr_rows_for_slope_analysis)
    bfr_forecast_and_risk_metrics = pd.concat(
        [bfr_forecast_and_risk_metrics, bfr_forecast_and_risk_slope_analysis]
    )
    return bfr_forecast_and_risk_metrics


def get_bfr_forecast_and_risk_data(
    merged_bfr_with_crn, historic_bfr_y2, historic_bfr_y1, current_year, academies
):
    """
    Orchestrates the preparation of data and calculation of forecast and risk metrics and rows.
    """
    merged_bfr_with_2y_historic_data = _prepare_merged_bfr_for_forecast_and_risk(
        merged_bfr_with_crn, historic_bfr_y2, historic_bfr_y1
    )

    bfr_forecast_and_risk_metrics = _calculate_forecast_and_risk_metrics(
        merged_bfr_with_2y_historic_data
    )

    # Current pupil numbers from academies (that year's census), future from bfr_3y
    bfr_pupils = prepare_current_and_future_pupils(
        bfr_data=merged_bfr_with_2y_historic_data, academies=academies
    )
    bfr_final_wide = merged_bfr_with_2y_historic_data.merge(
        bfr_pupils, how="left", on="Trust UPIN"
    )
    forecast_and_risk_revenue_reserve_rows = (
        melt_forecast_and_risk_revenue_reserves_from_bfr(bfr_final_wide, current_year)
    )
    forecast_and_risk_pupil_numbers_melted_rows = (
        melt_forecast_and_risk_pupil_numbers_from_bfr(bfr_final_wide, current_year)
    )
    bfr_forecast_and_risk_rows = forecast_and_risk_revenue_reserve_rows.merge(
        forecast_and_risk_pupil_numbers_melted_rows,
        how="left",
        on=("Company Registration Number", "Category", "Year"),
    )

    return bfr_forecast_and_risk_rows, bfr_forecast_and_risk_metrics