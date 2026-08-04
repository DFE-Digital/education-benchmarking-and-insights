from typing import Mapping


laa_ancillary_files = {
    "default": {
        "absences": "Absence_2term_school.csv",
        "capacity": "capacity_school_200910-202425.csv",
        "capacity_special": "specialist-provision_school_202223-202425.csv",
        "parental_preference": "AppsandOffers_2025_SchoolLevel07102025.csv",
    }
}


def get_laa_ancillary_filenames(year: int) -> Mapping:
    # Optionally route years to configs
    return laa_ancillary_files["default"]


laa_ancillary_columns = {
    "default": {
        "absences": {
            "time_period": "Int64",
            "school_urn": "Int64",
            "sess_overall_percent": "float",
        },
        "capacity": {
            "time_period": "Int64",
            "school_urn": "Int64",
            "school_places": "float",
        },
        "capacity_special": {
            "time_period": "Int64",
            "school_urn": "Int64",
            "school_places": "float",
        },
        "parental_preference": {
            "time_period": "Int64",
            "school_urn": "Int64",
            "times_put_as_1st_preference": "float",
            "total_number_places_offered": "float",
        },
    }
}


def get_laa_ancillary_columns(year: int) -> Mapping:
    # Optionally route years to configs
    return laa_ancillary_columns["default"]
