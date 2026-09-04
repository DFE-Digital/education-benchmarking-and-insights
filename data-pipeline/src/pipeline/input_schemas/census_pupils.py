pupil_census_index_col = "URN"

_base_pupils_cols = {
    "% of pupils known to be eligible for free school meals": "float",
    "headcount of pupils": "float",
    "fte pupils": "float",
    "Number of early year pupils (years E1 and E2)": "float",
    "Number of nursery pupils (years N1 and N2)": "float",
}

_pupil_census_default = {
    "URN": "Int64",
    "Full time boys Year group 12": "float",
    "Full time girls Year group 12": "float",
    "Full time boys Year group 13": "float",
    "Full time girls Year group 13": "float",
    **_base_pupils_cols,
}

_pupil_census_2023 = {
    **_pupil_census_default,
    "number_of_dual_subsidiary_registrations": "float",
}

_pupil_census_2024 = {
    "urn": "Int64",
    "% of pupils whose first language is known or believed to be other than English": "float",
    "total boarders": "float",
    "full-time male Year group 12": "float",
    "full-time female Year group 12": "float",
    "full-time male Year group 13": "float",
    "full-time female Year group 13": "float",
    "number_of_dual_subsidiary_registrations": "float",
    **_base_pupils_cols,
}

_pupil_census_2025 = {
    "number of pupils known to be eligible for free school meals": "float",
    "number of pupils whose first language is known or believed to be other than English": "float",
    "% of pupils whose first language is known or believed to be other than English": "float",
    **_pupil_census_2024,
}

pupil_census = {
    "default": _pupil_census_default,
    2023: _pupil_census_2023,
    2024: _pupil_census_2024,
    2025: _pupil_census_2025,
    2026: _pupil_census_2025,
}

_pupil_census_mappings_change_2024 = {
    "urn": "URN",
    "full-time male Year group 12": "Full time boys Year group 12",
    "full-time female Year group 12": "Full time girls Year group 12",
    "full-time male Year group 13": "Full time boys Year group 13",
    "full-time female Year group 13": "Full time girls Year group 13",
    "number_of_dual_subsidiary_registrations": "Pupil Dual Registrations",
}
pupil_census_column_mappings = {
    "default": {},
    2023: {
        "number_of_dual_subsidiary_registrations": "Pupil Dual Registrations",
    },
    2024: _pupil_census_mappings_change_2024,
    2025: _pupil_census_mappings_change_2024,
    2026: _pupil_census_mappings_change_2024,
}
