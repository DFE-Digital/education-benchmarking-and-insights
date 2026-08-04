from typing import Mapping

laa_ancillary_files = {
    "default": {
        "absences": "Absence_2term_school.csv",
        "capacity": "capacity_school_200910-202425.csv",
        "parental_preference": "AppsandOffers_2025_SchoolLevel07102025.csv",
    }
}


def get_laa_ancillary_files(year: int) -> Mapping:
    # Optionally route years to configs
    return laa_ancillary_files["default"]


laa_ancillary_columns = {
    "default": {
        "absences": ["time_period", "school_urn", "sess_overall_percent"],
        "capacity": ["time_period", "school_urn", "school_places"],
        "parental_preference": [
            "time_period",
            "school_urn",
            "proportion_1stprefs_v_totaloffers",
        ],
    }
}


def get_laa_ancillary_columns(year: int) -> Mapping:
    # Optionally route years to configs
    return laa_ancillary_columns["default"]
