import pandas as pd

from pipeline.utils.log import setup_logger

from .config import get_laa_ancillary_period

logger = setup_logger(__name__)


def preprocess_laa_data(
    cfr_data_this_year,
    cfr_data_year_minus_one,
    cfr_data_year_minus_two,
    cfr_data_year_minus_three,
    cfr_data_year_minus_four,
    absences,
    capacity,
    parental_preference,
    run_year: int,
):
    logger.info(f"Merging {run_year} LAA risk score data with ancillary data...")
    cfr_with_one_historic_year = pd.merge(
        cfr_data_this_year,
        cfr_data_year_minus_one,
        how="left",
        left_index=True,
        right_index=True,
        suffixes=["", "_y_minus_one"],
    )
    cfr_with_two_historic_years = pd.merge(
        cfr_with_one_historic_year,
        cfr_data_year_minus_two,
        how="left",
        left_index=True,
        right_index=True,
        suffixes=["", "_y_minus_two"],
    )
    cfr_with_three_historic_years = pd.merge(
        cfr_with_two_historic_years,
        cfr_data_year_minus_three,
        how="left",
        left_index=True,
        right_index=True,
        suffixes=["", "_y_minus_three"],
    )
    cfr_with_four_historic_years = pd.merge(
        cfr_with_three_historic_years,
        cfr_data_year_minus_four,
        how="left",
        left_index=True,
        right_index=True,
        suffixes=["", "_y_minus_four"],
    )

    # Reset index so 'URN' is a column for joining ancillary data
    cfr_flat = cfr_with_four_historic_years.reset_index()

    cfr_with_absences = pd.merge(
        cfr_flat,
        absences,
        how="left",
        left_on="URN",
        right_on="school_urn",
    )
    cfr_with_capacity = pd.merge(
        cfr_with_absences,
        capacity,
        how="left",
        left_on="URN",
        right_on="school_urn",
    )
    cfr_with_all_extra_data = pd.merge(
        cfr_with_capacity,
        parental_preference,
        how="left",
        left_on="URN",
        right_on="school_urn",
    )

    unfederated_schools = cfr_with_all_extra_data["Lead school in federation"] == "0"
    cfr_with_all_extra_data = cfr_with_all_extra_data[unfederated_schools]

    return cfr_with_all_extra_data


def preprocess_laa_extra_ancillary_data(
    absences_raw,
    capacity_raw,
    capacity_special_raw,
    parental_preference_raw,
    run_year: int,
):
    absences_period = get_laa_ancillary_period("absences", run_year)
    capacity_period = get_laa_ancillary_period("capacity", run_year)
    capacity_special_period = get_laa_ancillary_period("capacity_special", run_year)
    parental_preference_period = get_laa_ancillary_period(
        "parental_preference", run_year
    )

    preprocessed_absences = absences_raw[absences_raw["time_period"] == absences_period]

    capacity_filtered = capacity_raw[capacity_raw["time_period"] == capacity_period]
    capacity_special_filtered = capacity_special_raw[
        capacity_special_raw["time_period"] == capacity_special_period
    ]
    all_capacity = pd.concat([capacity_filtered, capacity_special_filtered])

    # Some all-through schools from CFR are split into primary/secondary
    preprocessed_capacity = all_capacity.groupby("school_urn", as_index=False)[
        "school_places"
    ].sum()

    parental_preference_df_filtered = parental_preference_raw[
        parental_preference_raw["time_period"] == parental_preference_period
    ]

    # Some all-through schools from CFR are split into primary/secondary
    preprocessed_parental_preference = parental_preference_df_filtered.groupby(
        "school_urn", as_index=False
    )[["times_put_as_1st_preference", "total_number_places_offered"]].sum()

    places_offered = preprocessed_parental_preference["total_number_places_offered"]
    times_1st_pref = preprocessed_parental_preference["times_put_as_1st_preference"]
    preprocessed_parental_preference["proportion_1stprefs_v_totaloffers"] = (
        (times_1st_pref.div(places_offered.replace(0, pd.NA)))
        .fillna(0.0)
        .astype("float")
    )

    return (
        preprocessed_absences,
        preprocessed_capacity,
        preprocessed_parental_preference,
    )
