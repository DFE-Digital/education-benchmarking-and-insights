import pytest

from education_benchmarking_and_insights.bfr import config
from education_benchmarking_and_insights.bfr.bfr_pyspark_mocks import (
    get_mock_academies_df,
    get_mock_bfr_sofa_mv,
    get_mock_bfr_three_year_mv,
)
from education_benchmarking_and_insights.bfr.preprocessor import BFRPreprocessor

BFR_3Y_TO_SOFA_MAPPINGS_REVERSE = {
    v: k for k, v in config.BFR_3Y_TO_SOFA_MAPPINGS.items()
}


@pytest.fixture
def preprocessor(spark):
    return BFRPreprocessor(year=2024, spark=spark, config=config)


def test_preprocess_bfr_sofa_scaling(preprocessor, spark):
    mock_bfr_sofa_mv_data = get_mock_bfr_sofa_mv(spark, year=2024)
    processed_df = preprocessor.preprocess_bfr_sofa(mock_bfr_sofa_mv_data)

    # Trust UPIN is IntegerType in mock, so filter by Integer
    # Pupil Number (EFALine 999) should not be scaled
    pupil_number_row = (
        processed_df.filter(processed_df["Trust UPIN"] == 100001)
        .filter(processed_df["EFALineNo"] == config.SOFA_PUPIL_NUMBER_EFALINE)
        .first()
    )
    assert pupil_number_row is not None
    assert pupil_number_row["Y1P1"] == 1000.0
    assert pupil_number_row["Y1P2"] == 1050.0

    # Test scaling for an IT Spend line (e.g., 336) which is scaled but not aggregated into a custom category
    it_spend_row = (
        processed_df.filter(processed_df["Trust UPIN"] == 100001)
        .filter(processed_df["EFALineNo"] == 336)
        .first()
    )
    assert it_spend_row is not None
    assert it_spend_row["Y1P1"] == 10.0 * 1000
    assert it_spend_row["Y1P2"] == 11.0 * 1000


def test_preprocess_bfr_sofa_aggregation(preprocessor, spark):
    mock_bfr_sofa_mv_data = get_mock_bfr_sofa_mv(spark, year=2024)
    processed_df = preprocessor.preprocess_bfr_sofa(mock_bfr_sofa_mv_data)
    upin_mask = processed_df["Trust UPIN"] == 100001

    # Check aggregated 'Self-generated income'
    self_gen_income_row = (
        processed_df.filter(upin_mask)
        .filter(processed_df["Category"] == "Self-generated income")
        .first()
    )
    assert self_gen_income_row is not None
    # Original data for 211 (Y1P1=100.0, Y1P2=110.0), 220 (Y1P1=50.0, Y1P2=55.0)
    assert self_gen_income_row["Y1P1"] == (100.0 + 50.0) * 1000
    assert self_gen_income_row["Y1P2"] == (110.0 + 55.0) * 1000

    # Check aggregated 'Grant funding'
    # For year 2024 mock data, only EFALine 199 exists in SOFA_GRANT_FUNDING_EFALINES from the mock data.
    # So, the aggregation should essentially just reflect the scaled value of 199.
    grant_funding_agg_row = (
        processed_df.filter(upin_mask)
        .filter(processed_df["Category"] == "Grant funding")
        .first()
    )
    assert grant_funding_agg_row is not None
    assert grant_funding_agg_row["Y1P1"] == 200.0 * 1000
    assert grant_funding_agg_row["Y1P2"] == 210.0 * 1000


def test_preprocess_bfr_sofa_category_rename(preprocessor, spark):
    mock_bfr_sofa_mv_data = get_mock_bfr_sofa_mv(spark, year=2024)
    processed_df = preprocessor.preprocess_bfr_sofa(mock_bfr_sofa_mv_data)

    # "Balance c/f to next period " should be renamed to "Revenue reserve"
    revenue_reserve_row = (
        processed_df.filter(processed_df["Trust UPIN"] == 100001)
        .filter(processed_df["EFALineNo"] == config.SOFA_TRUST_REVENUE_RESERVE_EFALINE)
        .first()
    )
    assert revenue_reserve_row is not None
    assert revenue_reserve_row["Category"] == "Revenue reserve"

    # "Pupil numbers (actual and estimated)" should be renamed to "Pupil numbers"
    pupil_numbers_row = (
        processed_df.filter(processed_df["Trust UPIN"] == 100001)
        .filter(processed_df["EFALineNo"] == config.SOFA_PUPIL_NUMBER_EFALINE)
        .first()
    )
    assert pupil_numbers_row is not None
    assert pupil_numbers_row["Category"] == "Pupil numbers"


def test_preprocess_bfr_3y_normalization_and_scaling(preprocessor, spark):
    mock_bfr_three_year_mv_data = get_mock_bfr_three_year_mv(spark)
    processed_df = preprocessor.preprocess_bfr_3y(mock_bfr_three_year_mv_data)

    # Check normalization of EFALineNo and scaling
    # Original 3Y EFALine 2980 maps to SOFA_TOTAL_REVENUE_INCOME (298)
    normalized_income_row = (
        processed_df.filter(processed_df["Trust UPIN"] == 100001)
        .filter(processed_df["EFALineNo"] == config.SOFA_TOTAL_REVENUE_INCOME)
        .first()
    )
    assert normalized_income_row is not None
    assert (
        normalized_income_row["Y2"] == 100.0 * 1000
    )  # Value from mock_bfr_three_year_mv_data for 2980 (Y2)

    # Original 3Y EFALine 4300 maps to SOFA_TRUST_REVENUE_RESERVE_EFALINE (430)
    normalized_reserve_row = (
        processed_df.filter(processed_df["Trust UPIN"] == 100001)
        .filter(processed_df["EFALineNo"] == config.SOFA_TRUST_REVENUE_RESERVE_EFALINE)
        .first()
    )
    assert normalized_reserve_row is not None
    assert (
        normalized_reserve_row["Y2"] == 200.0 * 1000
    )  # Value from mock_bfr_three_year_mv_data for 4300 (Y2)

    # Check a line that doesn't need mapping (OTHER_COSTS_EFALINE = 335) and is scaled
    other_costs_row = (
        processed_df.filter(processed_df["Trust UPIN"] == 100001)
        .filter(processed_df["EFALineNo"] == config.OTHER_COSTS_EFALINE)
        .first()
    )
    assert other_costs_row is not None
    assert (
        other_costs_row["Y2"] == 50.0 * 1000
    )  # Value from mock_bfr_three_year_mv_data for 335 (Y2)


def test_preprocess_data_merge_and_crn(preprocessor, spark):
    mock_academies_data = get_mock_academies_df(spark, year=2024)
    mock_bfr_three_year_mv_data = get_mock_bfr_three_year_mv(spark)
    mock_bfr_sofa_mv_data = get_mock_bfr_sofa_mv(spark, year=2024)
    merged_df = preprocessor.preprocess_data(
        mock_bfr_sofa_mv_data, mock_bfr_three_year_mv_data, mock_academies_data
    )

    # Check schema and number of rows
    expected_sofa_year_cols = config.get_sofa_year_cols(preprocessor.year)
    expected_3y_cols = config.THREE_YEAR_PROJECTION_COLS
    expected_columns = [
        "Company Registration Number",
        "Trust UPIN",
        "EFALineNo",
        "Category",
        *expected_sofa_year_cols,
        *expected_3y_cols,
    ]
    assert set(merged_df.columns) == set(expected_columns)
    assert merged_df.count() > 0

    # Check CRN merge for Trust UPIN 100001
    crn_row = merged_df.filter(merged_df["Trust UPIN"] == 100001).first()
    assert crn_row is not None
    assert crn_row["Company Registration Number"] == "CRN001"

    # Check data from both SOFA and 3Y are present for Trust UPIN 100001
    # EFALineNo for Total revenue income (298)
    sofa_and_3y_row = merged_df.filter(
        (merged_df["Trust UPIN"] == 100001)
        & (merged_df["EFALineNo"] == config.SOFA_TOTAL_REVENUE_INCOME)
    ).first()
    assert sofa_and_3y_row is not None
    assert sofa_and_3y_row["Y1P1"] == 1000.0 * 1000  # SOFA value scaled
    assert (
        sofa_and_3y_row["Y2"] == 100.0 * 1000
    )  # 3Y value scaled and normalized (Y2 in 3Y maps to first year of projection)

    # Check that rows with SOFA_PUPIL_NUMBER_EFALINE (999) have both SOFA and 3Y data
    # as 999 is present in both mock SOFA and 3Y (via mapping from 9000)
    pupil_row = merged_df.filter(
        (merged_df["Trust UPIN"] == 100001)
        & (merged_df["EFALineNo"] == config.SOFA_PUPIL_NUMBER_EFALINE)
    ).first()
    assert pupil_row is not None
    assert pupil_row["Y1P1"] == 1000.0  # Not scaled (from SOFA mock)
    assert (
        pupil_row["Y2"] == 1000.0
    )  # Not scaled (from 3Y mock, originally 9000, mapped to 999)

    # Verify a row that exists only in SOFA (not covered by 3Y mappings, and not one of the aggregated categories).
    # For example, EFALine 336 (IT backend) if it's not mapped from 3Y and not part of aggregation
    # Checking if 336 is in SOFA_IT_SPEND_LINES, it should be filtered and scaled.
    # It should not have 3Y projection columns filled.
    it_spend_sofa_only_row = merged_df.filter(
        (merged_df["Trust UPIN"] == 100001) & (merged_df["EFALineNo"] == 336)
    ).first()
    assert it_spend_sofa_only_row is not None
    assert it_spend_sofa_only_row["Y1P1"] == 10.0 * 1000  # Scaled SOFA value
    assert it_spend_sofa_only_row["Y2"] is None  # Should be None as it's not in 3Y data
