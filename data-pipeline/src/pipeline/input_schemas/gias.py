gias_index_col = "URN"

_gias_base_cols = {
    "URN": "Int64",
    "UKPRN": "Int64",
    "LA (code)": "Int64",
    "LA (name)": "string",
    "EstablishmentNumber": "Int64",
    "EstablishmentName": "string",
    "TypeOfEstablishment (code)": "Int64",
    "TypeOfEstablishment (name)": "string",
    "OpenDate": "string",
    "CloseDate": "string",
    "PhaseOfEducation (code)": "Int64",
    "PhaseOfEducation (name)": "string",
    "Boarders (code)": "Int64",
    "Boarders (name)": "string",
    "NurseryProvision (name)": "string",
    "OfficialSixthForm (code)": "Int64",
    "OfficialSixthForm (name)": "string",
    "AdmissionsPolicy (code)": "Int64",
    "AdmissionsPolicy (name)": "string",
    "Postcode": "string",
    "SchoolWebsite": "string",
    "TelephoneNum": "string",
    "GOR (name)": "string",
    "MSOA (code)": "string",
    "LSOA (code)": "string",
    "StatutoryLowAge": "Int64",
    "StatutoryHighAge": "Int64",
    "Street": "string",
    "Locality": "string",
    "Address3": "string",
    "Town": "string",
    "County (name)": "string",
    "SpecialClasses (name)": "string",
}

_gias_default = {
    **_gias_base_cols,
    "OfstedLastInsp": "string",
    "OfstedRating (name)": "string",
}

_gias_2024 = _gias_base_cols

_gias_2025 = {
    **_gias_2024,
    "EstablishmentStatus (name)": "string",
    "EstablishmentTypeGroup (name)": "string",
    "UrbanRural (name)": "string",
    "Easting": "Int64",
    "Northing": "Int64",
    "Gender (name)": "string",
    "Federations (code)": "string",
    "Federations (name)": "string",
}

gias = {
    "default": _gias_default,
    2024: _gias_2024,
    2025: _gias_2025,
    2026: _gias_2025,
}
