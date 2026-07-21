import numpy as np
import pandas as pd

from pipeline.pre_processing.common import mappings
from pipeline.utils.log import setup_logger

from .output_cols import cs_transparency_file_cols, transparency_file_cols
from .rollups import (
    calculate_cs_transparency_file_rollups,
    calculate_transparency_file_rollups,
)

logger = setup_logger(__name__)


def build_aar_transparency_file(
    academies_with_apportionments: pd.DataFrame,
    academies_preprocessed: pd.DataFrame,
) -> pd.DataFrame:
    """
    Generates the AAR transparency file from the pre-processed academies DataFrame.
    Should work 2025 onwards.
    """
    df_apportioned = academies_with_apportionments.copy().reset_index()
    df_raw = academies_preprocessed.copy()
    df_raw = calculate_transparency_file_rollups(df_raw)
    df_raw["Is PFI"] = df_raw["Is PFI"].map(mappings.map_is_pfi_school)

    # Calculate additional/extra fields specified in SQL
    df_apportioned["LAEstab"] = (
        df_apportioned["LA"].astype(str) + df_apportioned["Estab"].astype(str)
    ).astype(int)
    df_apportioned["% of pupils who are Boarders"] = np.where(
        df_apportioned["Total pupils"] > 1,
        df_apportioned["total boarders"]
        / df_apportioned["Number of pupils (headcount)"]
        * 100,
        0,
    ).round(1)
    df_apportioned["% of pupil with SEN support"] = np.where(
        df_apportioned["Total pupils"] > 1,
        df_apportioned["SEN support"] / df_apportioned["Total pupils"] * 100,
        0,
    ).round(1)
    df_apportioned["Percentage with EHC"] = df_apportioned["Percentage with EHC"].round(
        1
    )
    borough_conditions = [
        df_apportioned["LA"].isin(
            [201, 202, 204, 205, 206, 207, 208, 209, 210, 211, 212, 213, 309, 316]
        ),
        df_apportioned["LA"].isin(
            [
                203, 301, 302, 303, 304, 305, 306, 307, 308, 310, 311, 312, 313, 
                314, 315, 317, 318, 319, 320
            ]
        ),
    ]
    borough_choices = ["Inner", "Outer"]
    df_apportioned["London Borough"] = np.select(
        borough_conditions, borough_choices, default="Neither"
    )

    df_raw["Company Registration Number"] = df_raw[
        "Company Registration Number"
    ].str.zfill(8)
    transparency_df_all_values = pd.merge(
        df_raw.reset_index(),
        df_apportioned,
        suffixes=["", "_app"],
        on=["URN", "Company Registration Number"],
        how="left",
    )
    transparency_df_all_values["RR apportionment from CS"] = (
        transparency_df_all_values["Revenue reserve_app"]
        - transparency_df_all_values["Revenue reserve"]
    ).round(0)
    transparency_df_all_values["Revenue reserve_app"] = transparency_df_all_values[
        "Revenue reserve_app"
    ].round(0)

    output_mappings = transparency_file_cols.get("default")
    transparency_df = (
        transparency_df_all_values[output_mappings.keys()]
        .rename(columns=output_mappings)
        .sort_values(by="URN")
    )

    logger.info("Generated Academy level AAR transparency file.")

    return transparency_df


def build_aar_central_services_transparency_file(
    trusts: pd.DataFrame, academies: pd.DataFrame, central_services_raw: pd.DataFrame
) -> pd.DataFrame:
    """
    Generates the AAR Central Services (Trust) transparency file from the pre-processed trusts and academies DataFrames.
    """
    trusts_apportioned = trusts.copy()
    academies_copy = academies.copy()
    central_services_copy = central_services_raw.copy()

    central_services_copy["Company Registration Number"] = central_services_copy[
        "Company Registration Number"
    ].map(mappings.map_company_number)
    central_services_copy = calculate_cs_transparency_file_rollups(
        central_services_copy
    )

    grouped = academies_copy.groupby("Company Registration Number")
    trust_pupil_counts = grouped.agg(
        total_pupils=("Number of pupils", "sum"),
        total_fsm=(
            "number of pupils known to be eligible for free school meals",
            "sum",
        ),
        total_ehcp=("EHC plan", "sum"),
        total_sen=("SEN support", "sum"),
        total_eal=(
            "number of pupils whose first language is known or believed to be other than English",
            "sum",
        ),
        total_boarders=("total boarders", "sum"),
        total_sixth_form=("TotalPupilsSixthForm", "sum"),
        total_teachers=("Total Number of Teachers (Full-Time Equivalent)", "sum"),
        total_prop_pupils=("Number of pupils_pro_rata_end_of_period", "sum"),
        sum_of_academy_rr=("Academy Revenue Reserve", "sum"),
        trust_name=("Trust Name", "first"),
    ).reset_index()

    trusts_apportioned = trusts_apportioned.merge(
        trust_pupil_counts, on="Company Registration Number", how="left"
    )

    def calculate_percentage(
        df: pd.DataFrame, target_col: str, total_col: str, round_places: int = 2
    ) -> pd.Series:
        safe_denominator = df[total_col].replace(0, np.nan)

        return ((df[target_col] / safe_denominator) * 100).round(round_places)

    trusts_apportioned["%_pupils_FSM"] = calculate_percentage(
        trusts_apportioned, "total_fsm", "total_pupils"
    )
    trusts_apportioned["%_pupils_EHCP"] = calculate_percentage(
        trusts_apportioned, "total_ehcp", "total_pupils"
    )
    trusts_apportioned["%_pupils_SEN"] = calculate_percentage(
        trusts_apportioned, "total_sen", "total_pupils"
    )
    trusts_apportioned["%_pupils_EAL"] = calculate_percentage(
        trusts_apportioned, "total_eal", "total_pupils"
    )
    trusts_apportioned["%_pupils_boarders"] = calculate_percentage(
        trusts_apportioned, "total_boarders", "total_pupils"
    )

    cs_transparency_file_df = pd.merge(
        trusts_apportioned,
        central_services_copy,
        on="Company Registration Number",
        how="left",
        suffixes=["_app", ""],
    )

    cs_transparency_file_df[
        "Central Services Revenue Reserve per pupil at end of period based on time in trust"
    ] = (
        cs_transparency_file_df["Revenue reserve"]
        / cs_transparency_file_df["total_prop_pupils"]
    ).round(
        0
    )

    output_mappings = cs_transparency_file_cols.get("default")
    formatted_cs_transparency_file_df = (
        cs_transparency_file_df[output_mappings.keys()]
        .sort_values(by="Company Registration Number")
        .rename(columns=output_mappings)
    )

    logger.info("Generated Trust level AAR transparency file.")

    return formatted_cs_transparency_file_df
