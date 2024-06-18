import datetime

import numpy as np
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
    if not (pd.isna(code) or pd.isna(provision)):
        if provision.lower() == "special" or code == 33 or code == 36 or code == 44:
            return "Special"

        if provision.lower() == "all through":
            return "All-through"

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


def map_has_sixth_form(has_sixth_form: str):
    return has_sixth_form.lower() == "has a sixth form"


def map_nursery(nursery: str, provision: str):
    match nursery.lower():
        case "has nursery classes":
            return "Has Nursery classes"
        case _:
            if not nursery.strip() and provision.lower() == "primary":
                return "Unknown"
            else:
                return "No Nursery classes"


def map_has_nursery(has_nursery: str):
    return has_nursery.lower() == "has nursery classes"


def map_admission_policy(admission_policy: str):
    match admission_policy.lower():
        case "selective":
            return "Selective"
        case "non-selective" | "not applicable":
            return "HI selective"
        case _:
            return "Unknown"


def map_academy_status(
    opened_in_period: datetime,
    closed_in_period: datetime,
    valid_to: datetime,
    start_date: datetime,
    closed_date: datetime,
    period_start_date: datetime,
    year_start_date: datetime,
    year_end_date: datetime,
):
    if not (
        pd.isnull(closed_in_period)
        and pd.isnull(opened_in_period)
        and pd.isnull(valid_to)
        and pd.isnull(start_date)
        and pd.isnull(closed_date)
    ):
        if closed_date < year_start_date:
            return "Closed"

        if (
            year_start_date < closed_in_period < year_end_date
        ) or closed_date <= year_end_date:
            return "Closed in period"

        if (
            start_date > period_start_date
        ) or period_start_date < opened_in_period < year_end_date:
            return "(Re)opened in period"

        if year_start_date < valid_to < year_end_date:
            return "Invalid"

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
        pd.isnull(return_period_length)
        and pd.isnull(start_date)
        and pd.isnull(closed_date)
    ):
        if closed_date < year_start_date:
            return "Closed"

        if closed_date < year_end_date or return_period_length < 12:
            return "Closed in period"

        if start_date > year_start_date:
            return "Open in period"

        return "Open"
    else:
        return None


def map_school_website(url: str):
    if url is None:
        return None
    if url.startswith("https://") or url.startswith("http://"):
        return url
    return "https://" + url


def map_is_pfi_school(pfi: str):
    return (
        "PFI School"
        if pfi is not None and pfi.lower() == "part of pfi"
        else "Non-PFI school"
    )


def map_is_surplus_deficit(closing_balance: float):
    if pd.isnull(closing_balance):
        return "Unknown"
    if closing_balance >= 0:
        return "Surplus"
    else:
        return "Deficit"


def map_school_phase_type(establishment_code: int, phase_type: str):
    if pd.isnull(phase_type):
        return None

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


def _diff_month(d1, d2):
    return (d1.year - d2.year) * 12 + d1.month - d2.month


def map_academy_period_return(
    opened_in_period: datetime,
    closed_in_period: datetime,
    year_start_date: datetime,
    year_end_date: datetime,
):
    if not pd.isnull(closed_in_period):
        return _diff_month(closed_in_period, year_start_date)
    elif not pd.isnull(opened_in_period):
        return _diff_month(year_end_date, opened_in_period)
    else:
        return 12


def map_cost_series(category_name, df, basis):
    # Create total column
    df[category_name + "_Total"] = (
        df[df.columns[pd.Series(df.columns).str.startswith(category_name)]]
        .fillna(0)
        .sum(axis=1)
    )

    sub_categories = df.columns[
        df.columns.str.startswith(category_name)
    ].values.tolist()

    for sub_category in sub_categories:
        df[sub_category + "_Per Unit"] = df[sub_category].fillna(0) / basis
        df[sub_category + "_Per Unit"].replace(
            [np.inf, -np.inf, np.nan], 0, inplace=True
        )

    return df