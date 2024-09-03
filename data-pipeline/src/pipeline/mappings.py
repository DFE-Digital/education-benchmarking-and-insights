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

def map_phase_type(code: int, provision: str):
    if not (pd.isna(code) or pd.isna(provision)):
        if provision.lower() == "special" or code == 33 or code == 36 or code == 44:
            return 'Special'
        elif provision.lower() == 'all through' or provision.lower() == 'all-through' or code == 7:
            return 'All-through'
        elif code == 38 or code == 42 or code == 43:
            return 'Alternative Provision'
        elif code == 40:
            return 'Univeristy Technical College'
        elif provision.lower() == 'post-16' or provision.lower() == '16 plus' or code == 39 or code == 45 or code == 46 or code == 6:
            return 'Post-16'
        elif provision.lower() =='primary' or code == 2 or code == 3:
            return 'Primary'
        elif provision.lower() == 'secondary' or code == 4 or code == 5:
            return 'Secondary'
        elif provision.lower() == 'pupil referral unit':
            return 'Pupil Referral Unit'
        elif provision.lower() == 'nursery' or code == 1:
            return 'Nursery'
        else:
            return 'Unspecified'

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
    if url is None or url == "":
        return None
    if url.startswith("https://") or url.startswith("http://"):
        return url
    return "https://" + url


def map_is_pfi_school(pfi: float):
    return "PFI School" if pfi is not None and pfi != 0 else "Non-PFI school"


def map_is_surplus_deficit(closing_balance: float):
    if pd.isnull(closing_balance):
        return "Unknown"
    if closing_balance >= 0:
        return "Surplus"
    else:
        return "Deficit"


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


def map_company_number(company_number: str):
    if pd.isnull(company_number):
        return None
    if len(company_number) < 8:
        return company_number.zfill(8)

    return company_number


_inner_las = [201, 202, 204, 204, 205, 206, 207, 208, 209, 210, 211, 212, 213, 309, 316]
_outer_las = [
    203,
    301,
    302,
    303,
    304,
    305,
    306,
    307,
    308,
    310,
    311,
    312,
    313,
    314,
    315,
    317,
    318,
    319,
    320,
]


def map_london_weighting(la: int, estab: int):
    if pd.isnull(la):
        return "Neither"
    else:
        match f"{la}{estab}":
            case "3066900":
                return "Outer"
            case "9332064":
                return "Neither"
            case "8946900":
                return "Neither"
            case "3906900":
                return "Neither"
            case _:
                if la in _inner_las:
                    return "Inner"
                elif la in _outer_las:
                    return "Outer"
                else:
                    return "Neither"
