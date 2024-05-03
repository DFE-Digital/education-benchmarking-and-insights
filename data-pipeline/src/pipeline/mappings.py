import datetime

import pandas as pd


def map_ofsted_rating(rating: str):
    match rating.lower():
        case "serious weaknesses":
            return "Serious weaknesses"
        case "special measures":
            return "Special measures"
        case _:
            return rating


def map_academy_phase_type(code: int, provision: str):
    if not (pd.isna(code) and pd.isna(provision)):
        if provision.lower() == "special" or code == 33 or code == 36 or code == 44:
            return "Special"

        if code == 38 or code == 42 or code == 43:
            return "Alternative Provision"

        if code == 40:
            return "University technical college"

        if code == 39 or code == 45 or code == 46:
            return "Post-16"

        return provision
    else:
        return None


def map_block_age(block_age: str):
    if block_age == "":
        return None
    elif block_age.lower() == "pre 1900":
        return 1880
    else:
        comps = block_age.split("-")
        return int((int(comps[0]) + int(comps[1])) / 2)


def map_boarders(boarder: str):
    match boarder.lower():
        case (
            "boarding school"
            | "children's home (boarding school)"
            | "college / fe residential accommodation"
        ):
            return "Boarding"
        case "no boarders" | "not applicable":
            return "Not Boarding"
        case _:
            return "Unknown"


def map_sixth_form(sixth_form: str):
    match sixth_form.lower():
        case "has a sixth form":
            return "Has a sixth form"
        case "does not have a sixth form" | "not applicable":
            return "No sixth form"
        case _:
            return "Unknown"


def map_nursery(nursery: str, provision: str):
    match nursery.lower():
        case "has nursery classes":
            return "Has Nursery classes"
        case _:
            if not nursery.strip() and provision.lower() == "primary":
                return "Unknown"
            else:
                return "No Nursery classes"


def map_admission_policy(admission_policy: str):
    match admission_policy.lower():
        case "selective":
            return "Selective"
        case "non-selective" | "not applicable":
            return "HI selective"
        case _:
            return "Unknown"


def map_academy_status(
    closed_in_period: datetime,
    valid_to: datetime,
    start_date: datetime,
    closed_date: datetime,
    period_start_date: datetime,
    year_start_date: datetime,
    year_end_date: datetime,
):
    if not (
        pd.isna(closed_in_period)
        and pd.isna(valid_to)
        and pd.isna(start_date)
        and pd.isna(closed_date)
    ):
        if closed_date < year_start_date:
            return "Closed"
        elif (
            year_start_date < closed_in_period < year_end_date
        ) or closed_date <= year_end_date:
            return "Closed in period"
        elif (
            start_date > period_start_date
            or period_start_date < closed_in_period < year_end_date
        ):
            return "(Re)opened in period"
        elif year_start_date < valid_to < year_end_date:
            return "Invalid"
        else:
            return "Open"
    else:
        return None


def map_maintained_school_status(
    start_date: datetime,
    closed_date: datetime,
    return_period_length: int,
    year_start_date: datetime,
    year_end_date: datetime,
):
    if not (
        pd.isna(return_period_length) and pd.isna(start_date) and pd.isna(closed_date)
    ):
        if closed_date < year_start_date:
            return "Closed"
        elif closed_date < year_end_date or return_period_length < 12:
            return "Closed in period"
        elif start_date > year_start_date:
            return "Open in period"
        else:
            return "Open"
    else:
        return None


def map_school_website(url: str):
    if url.startswith("https://") or url.startswith("http://"):
        return url
    elif not url:
        return None
    else:
        return "https://" + url


def map_is_pfi_school(pfi: str):
    return "PFI School" if pfi.lower() == "part of PFI" else "Non-PFI school"


def map_is_surplus_deficit(closing_balance: float):
    if pd.isna(closing_balance):
        return "Unknown"
    if closing_balance > 0:
        return "Surplus"
    else:
        return "Deficit"


def map_school_phase_type(establishment_code: int, phase_type: str):
    if phase_type.lower() == "primary":
        match establishment_code:
            case 33 | 36 | 44:
                return "Special"
            case 38 | 42 | 43:
                return "Alternative Provision"
            case _:
                return phase_type
    elif phase_type.lower() == "secondary":
        match establishment_code:
            case 33 | 36 | 44:
                return "Special"
            case 38 | 42 | 43:
                return "Alternative Provision"
            case 40:
                return "UTC"
            case _:
                return phase_type
    elif phase_type.lower() == "all-through" or phase_type.lower() == "all through":
        match establishment_code:
            case 33 | 36 | 44:
                return "Special"
            case 38 | 42 | 43:
                return "Alternative Provision"
            case 40:
                return "UTC"
            case _:
                return "All-through"
    elif phase_type.lower() == "16 plus":
        return "Secondary"
    else:
        return phase_type
