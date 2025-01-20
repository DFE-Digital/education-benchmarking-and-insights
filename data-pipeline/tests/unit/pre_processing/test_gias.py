from io import StringIO

import pandas as pd
import pytest

from pipeline.pre_processing import prepare_schools_data


def test_prepare_school_data_has_correct_output_columns(
    prepared_schools_data: pd.DataFrame,
):
    assert list(prepared_schools_data.columns) == [
        "UKPRN",
        "LA (code)",
        "LA (name)",
        "EstablishmentNumber",
        "EstablishmentName",
        "TypeOfEstablishment (code)",
        "TypeOfEstablishment (name)",
        "OpenDate",
        "CloseDate",
        "PhaseOfEducation (code)",
        "PhaseOfEducation (name)",
        "Boarders (code)",
        "Boarders (name)",
        "NurseryProvision (name)",
        "OfficialSixthForm (code)",
        "OfficialSixthForm (name)",
        "AdmissionsPolicy (code)",
        "AdmissionsPolicy (name)",
        "OfstedLastInsp",
        "Postcode",
        "SchoolWebsite",
        "TelephoneNum",
        "GOR (name)",
        "OfstedRating (name)",
        "MSOA (code)",
        "LSOA (code)",
        "StatutoryLowAge",
        "StatutoryHighAge",
        "Street",
        "Locality",
        "Address3",
        "Town",
        "County (name)",
        "LA Establishment Number",
        "Has Nursery",
        "Has Sixth Form",
    ]


def test_prepare_school_data_has_correct_output_columns_without_ofsted_cols(
    gias_data, gias_links
):
    """
    For 2024 submissions GIAS data will not include "OfstedRating (name)" or "OfstedLastInsp"
    This test confirms these columns are created as they are required for downsteam processing.
    """
    gias_without_ofsted = gias_data.copy().drop(
        columns=["OfstedRating (name)", "OfstedLastInsp"]
    )

    actual = prepare_schools_data(
        StringIO(gias_without_ofsted.to_csv()), StringIO(gias_links.to_csv()), 2024
    )

    assert list(actual.columns) == [
        "UKPRN",
        "LA (code)",
        "LA (name)",
        "EstablishmentNumber",
        "EstablishmentName",
        "TypeOfEstablishment (code)",
        "TypeOfEstablishment (name)",
        "OpenDate",
        "CloseDate",
        "PhaseOfEducation (code)",
        "PhaseOfEducation (name)",
        "Boarders (code)",
        "Boarders (name)",
        "NurseryProvision (name)",
        "OfficialSixthForm (code)",
        "OfficialSixthForm (name)",
        "AdmissionsPolicy (code)",
        "AdmissionsPolicy (name)",
        "Postcode",
        "SchoolWebsite",
        "TelephoneNum",
        "GOR (name)",
        "MSOA (code)",
        "LSOA (code)",
        "StatutoryLowAge",
        "StatutoryHighAge",
        "Street",
        "Locality",
        "Address3",
        "Town",
        "County (name)",
        "LA Establishment Number",
        "OfstedRating (name)",
        "OfstedLastInsp",
        "Has Nursery",
        "Has Sixth Form",
    ]


def test_prepare_school_data_has_correct_output_ofsted_values_without_submission(
    gias_data, gias_links
):
    """
    For 2024 submissions GIAS data will not include "OfstedRating (name)" or
    "OfstedLastInsp". These columns should still be created as they are required for downstream processing.
    This test confirms these are set with default values.
    """
    gias_without_ofsted = gias_data.copy().drop(
        columns=["OfstedRating (name)", "OfstedLastInsp"]
    )

    actual = prepare_schools_data(
        StringIO(gias_without_ofsted.to_csv()), StringIO(gias_links.to_csv()), 2024
    )

    assert (actual["OfstedRating (name)"] == "").all()
    assert actual["OfstedLastInsp"].isna().all()


def test_la_establishment_number_computed(prepared_schools_data: pd.DataFrame):
    assert prepared_schools_data.loc[100150]["LA Establishment Number"] == "201-3614"


def test_school_website_is_mapped_correctly(prepared_schools_data: pd.DataFrame):
    assert (
        prepared_schools_data.loc[100150]["SchoolWebsite"]
        == "https://www.schoola.co.uk"
    )


@pytest.mark.parametrize(
    "urn,expected",
    [(100150, "Not Boarding"), (100152, "Not Boarding"), (100153, "Boarding")],
)
def test_boarders_is_mapped_correctly(
    urn, expected, prepared_schools_data: pd.DataFrame
):
    assert prepared_schools_data.loc[urn]["Boarders (name)"] == expected


@pytest.mark.parametrize(
    "urn,expected",
    [
        (100150, "Outstanding"),
        (100152, "Special measures"),
        (100153, "Serious weaknesses"),
    ],
)
def test_boarders_is_mapped_correctly(
    urn, expected, prepared_schools_data: pd.DataFrame
):
    assert prepared_schools_data.loc[urn]["OfstedRating (name)"] == expected


def test_nursery_provision_is_mapped_correctly(prepared_schools_data: pd.DataFrame):
    assert (
        prepared_schools_data.loc[100150]["NurseryProvision (name)"]
        == "No Nursery classes"
    )


@pytest.mark.parametrize(
    "urn,expected",
    [
        (100150, "No sixth form"),
        (100152, "Has a sixth form"),
        (100153, "No sixth form"),
    ],
)
def test_sixth_form_is_mapped_correctly(
    urn, expected, prepared_schools_data: pd.DataFrame
):
    assert prepared_schools_data.loc[urn]["OfficialSixthForm (name)"] == expected


@pytest.mark.parametrize(
    "urn,expected",
    [(100150, "HI selective"), (100152, "HI selective"), (100153, "Selective")],
)
def test_admissions_policy_is_mapped_correctly(
    urn, expected, prepared_schools_data: pd.DataFrame
):
    assert prepared_schools_data.loc[urn]["AdmissionsPolicy (name)"] == expected
