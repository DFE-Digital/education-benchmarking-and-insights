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


def _map_secondary_phases(establishment_code: int) -> str:
    match establishment_code:
        case 40:
            return "University Technical College"
        case _:
            return "Secondary"


def _map_not_applicable_phases(establishment_code: int) -> str:
    match establishment_code:
        case 6:
            return "University Technical College"
        case 7 | 12 | 33 | 36 | 44:
            return "Special"
        case 14:
            return "Pupil Referral Unit"
        case 38 | 42 | 43:
            return "Alternative Provision"
        case _:
            return "Unknown"


def map_phase_type(
    establishment_code: int,
    phase_code: int,
) -> str:
    match phase_code:
        case 0:
            return _map_not_applicable_phases(establishment_code)
        case 1:
            return "Nursery"
        case 2 | 3:
            return "Primary"
        case 4 | 5:
            return _map_secondary_phases(establishment_code)
        case 6:
            return "Post-16"
        case 7:
            return "All-through"
        case _:
            return "Unknown"


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


def _period_from_start(start):
    match start.month:
        case 9:
            return 12 if start.day <= 10 else 11
        case 10:
            return 11
        case 11:
            return 10
        case 12:
            return 9
        case 1:
            return 8
        case 2:
            return 7
        case 3:
            return 6
        case 4:
            return 5
        case 5:
            return 4
        case 6:
            return 3
        case 7:
            return 2
        case 8:
            return 1


def _period_to_close(close):
    match close.month:
        case 9:
            return 0
        case 10:
            return 1
        case 11:
            return 2
        case 12:
            return 3
        case 1:
            return 4
        case 2:
            return 5
        case 3:
            return 6
        case 4:
            return 7
        case 5:
            return 8
        case 6:
            return 9
        case 7:
            return 10
        case 8:
            return 11


def map_academy_period_return(
    opened_in_period: datetime,
    closed_in_period: datetime,
):
    if not pd.isnull(closed_in_period):
        return _period_to_close(closed_in_period)
    elif not pd.isnull(opened_in_period):
        return _period_from_start(opened_in_period)
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
        df[sub_category + "_Per Unit"] = df[sub_category + "_Per Unit"].replace(
            [np.inf, -np.inf, np.nan], 0
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
