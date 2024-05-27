import pandas as pd
import pytest


def test_prepare_school_data_has_correct_output_columns(prepared_schools_data: pd.DataFrame):
    assert list(prepared_schools_data.columns) == [
         "UKPRN",
         "LA (code)",
         "LA (name)",
         "EstablishmentNumber",
         "EstablishmentName",
         "TypeOfEstablishment (code)",
         "TypeOfEstablishment (name)",
         "EstablishmentStatus (code)",
         "EstablishmentStatus (name)",
         "OpenDate",
         "CloseDate",
         "PhaseOfEducation (code)",
         "PhaseOfEducation (name)",
         "Boarders (code)",
         "Boarders (name)",
         "NurseryProvision (name)",
         "OfficialSixthForm (code)",
         "OfficialSixthForm (name)",
         "Gender (code)",
         "Gender (name)",
         "AdmissionsPolicy (code)",
         "AdmissionsPolicy (name)",
         "CensusDate",
         "SchoolCapacity",
         "NumberOfPupils",
         "NumberOfBoys",
         "NumberOfGirls",
         "OfstedLastInsp",
         "LastChangedDate",
         "Postcode",
         "SchoolWebsite",
         "TelephoneNum",
         "HeadTitle (name)",
         "HeadFirstName",
         "HeadLastName",
         "HeadPreferredJobTitle",
         "GOR (name)",
         "UrbanRural (name)",
         "BoardingEstablishment (name)",
         "PreviousLA (code)",
         "PreviousLA (name)",
         "PreviousEstablishmentNumber",
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
         "HeadName"
    ]


def test_la_establishment_number_computed(prepared_schools_data: pd.DataFrame):
    assert prepared_schools_data.loc[100150]["LA Establishment Number"] == "201-3614"


def test_school_website_is_mapped_correctly(prepared_schools_data: pd.DataFrame):
    assert prepared_schools_data.loc[100150]["SchoolWebsite"] == "https://www.schoola.co.uk"


@pytest.mark.parametrize("urn,expected", [
     (100150, "Not Boarding"),
     (100152, "Not Boarding"),
     (100153, "Boarding")
])
def test_boarders_is_mapped_correctly(urn, expected, prepared_schools_data: pd.DataFrame):
    assert prepared_schools_data.loc[urn]["Boarders (name)"] == expected


@pytest.mark.parametrize("urn,expected", [
     (100150, "Outstanding"),
     (100152, "Special measures"),
     (100153, "Serious weaknesses")
])
def test_boarders_is_mapped_correctly(urn, expected, prepared_schools_data: pd.DataFrame):
    assert prepared_schools_data.loc[urn]["OfstedRating (name)"] == expected


def test_nursery_provision_is_mapped_correctly(prepared_schools_data: pd.DataFrame):
    assert prepared_schools_data.loc[100150]["NurseryProvision (name)"] == "No Nursery classes"


@pytest.mark.parametrize("urn,expected", [
     (100150, "No sixth form"),
     (100152, "Has a sixth form"),
     (100153, "No sixth form")
])
def test_sixth_form_is_mapped_correctly(urn, expected, prepared_schools_data: pd.DataFrame):
    assert prepared_schools_data.loc[urn]["OfficialSixthForm (name)"] == expected


@pytest.mark.parametrize("urn,expected", [
     (100150, "HI selective"),
     (100152, "HI selective"),
     (100153, "Selective")
])
def test_admissions_policy_is_mapped_correctly(urn, expected, prepared_schools_data: pd.DataFrame):
    assert prepared_schools_data.loc[urn]["AdmissionsPolicy (name)"] == expected


@pytest.mark.parametrize("urn,expected", [
     (100150, "Miss A HeadA"),
     (100152, "Mr B HeadB"),
     (100153, "Mrs C HeadC")
])
def test_head_name_is_mapped_correctly(urn, expected, prepared_schools_data: pd.DataFrame):
    assert prepared_schools_data.loc[urn]["HeadName"] == expected

