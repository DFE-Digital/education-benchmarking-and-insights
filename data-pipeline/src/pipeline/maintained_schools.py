import datetime

import numpy as np
import pandas as pd

import src.pipeline.input_schemas as input_schemas
import src.pipeline.mappings as mappings


def create_master_list(
    maintained_schools_list: pd.DataFrame,
    schools: pd.DataFrame,
    sen: pd.DataFrame,
    census: pd.DataFrame,
    cdc: pd.DataFrame,
    ks2: pd.DataFrame,
    ks4: pd.DataFrame,
) -> pd.DataFrame:

    maintained_schools_list["Did Not Supply flag"] = maintained_schools_list[
        "Did Not Supply flag"
    ].map(lambda x: x if x == 0 else 1)

    maintained_schools_list["PFI"] = maintained_schools_list["PFI"].map(
        lambda x: x if x != "DNS" else "N"
    )

    maintained_schools_list.replace("DNS", np.nan, inplace=True)

    maintained_schools_list = maintained_schools_list.astype(
        input_schemas.maintained_schools_master_list
    ).set_index(input_schemas.maintained_schools_master_list_index_col)

    maintained_schools = maintained_schools_list.merge(
        schools.reset_index(), left_index=True, right_on="URN"
    )

    return (
        maintained_schools.merge(sen, on="URN", how="left")
        .merge(census, on="URN", how="left")
        .merge(cdc, on="URN", how="left")
        .merge(ks2, on="URN", how="left")
        .merge(ks4, on="URN", how="left")
    )


def map_pfi(maintained_schools: pd.DataFrame) -> pd.DataFrame:
    maintained_schools["PFI"] = maintained_schools["PFI"].map(
        lambda x: "PFI school" if x == "Y" else "Non-PFI school"
    )

    maintained_schools["Is PFI"] = maintained_schools["PFI"].map(
        lambda x: x == "PFI school"
    )

    return maintained_schools


def map_submission_attrs(maintained_schools: pd.DataFrame) -> pd.DataFrame:
    maintained_schools["Partial Years Present"] = maintained_schools.apply(
        lambda x: x["Did Not Supply flag"] != 0 | x["Period covered by return (months)"] != 12, axis=1)

    maintained_schools.rename(
        columns={"Period covered by return (months)": "Period covered by return"},
        inplace=True,
    )

    maintained_schools["Did Not Submit"] = maintained_schools[
        "Did Not Supply flag"
    ].map(lambda x: x == 1)

    return maintained_schools


def map_school_attrs(maintained_schools: pd.DataFrame) -> pd.DataFrame:

    maintained_schools["Email"] = ""
    maintained_schools["HeadEmail"] = ""
    maintained_schools["Trust Name"] = None
    maintained_schools["OfstedLastInsp"] = pd.to_datetime(
        maintained_schools["OfstedLastInsp"], dayfirst=True
    )
    maintained_schools["London Weighting"] = maintained_schools[
        "London Weighting"
    ].fillna("Neither")

    return maintained_schools


def map_school_type_attrs(maintained_schools: pd.DataFrame) -> pd.DataFrame:
    maintained_schools["Finance Type"] = "Maintained"
    maintained_schools["SchoolPhaseType"] = maintained_schools.apply(
        lambda df: mappings.map_phase_type(
            establishment_code=df["TypeOfEstablishment (code)"],
            phase_code=df["PhaseOfEducation (code)"],
        ),
        axis=1,
    )

    return maintained_schools


def calc_base_financials(maintained_schools: pd.DataFrame) -> pd.DataFrame:
    maintained_schools["Income_Total grant funding"] = (
        maintained_schools["Direct Grant"]
        + maintained_schools["Community Grants"]
        + maintained_schools["Targeted Grants"]
    )

    maintained_schools["Income_Pre Post 16"] = (
        maintained_schools["I01  Funds delegated by the LA"]
        + maintained_schools["I02  Funding for 6th form students"]
    )

    maintained_schools["In year balance"] = (
        maintained_schools["Total Income   I01 to I18"]
        - maintained_schools["Total Expenditure  E01 to E32"]
    )

    maintained_schools["Financial Position"] = maintained_schools[
        "In year balance"
    ].map(mappings.map_is_surplus_deficit)

    return maintained_schools


def map_cost_income_categories(
    maintained_schools: pd.DataFrame,
    cost_category_map: dict[str, str],
    income_category_map: dict[str, str],
) -> pd.DataFrame:
    maintained_schools.rename(
        columns=cost_category_map | income_category_map,
        inplace=True,
    )

    return maintained_schools


def calc_rag_cost_series(
    maintained_schools: pd.DataFrame, rag_category_settings: any
) -> pd.DataFrame:
    for category in rag_category_settings.keys():
        basis_data = maintained_schools[
            (
                "Number of pupils"
                if rag_category_settings[category]["type"] == "Pupil"
                else "Total Internal Floor Area"
            )
        ]
        maintained_schools = mappings.map_cost_series(
            category, maintained_schools, basis_data
        )

    return maintained_schools


# net catering cost, not net catering income
def calc_catering_net_costs(maintained_schools: pd.DataFrame) -> pd.DataFrame:
    maintained_schools["Catering staff and supplies_Net Costs"] = (
        maintained_schools["Catering staff and supplies_Total"]
        - maintained_schools["Income_Catering services"]
    )

    return maintained_schools


def join_federations(df: pd.DataFrame) -> pd.DataFrame:
    """
    Set Federation-related values.

    Each school in a Federation will have the `LAEstab` of the lead
    school in the `Lead school in federation` column.

    Note: lead schools will have their own `LAEstab` value in said
    column.

    :param df: Maintained School data
    :return: data updated with Federation data
    """
    lead_schools = df[df["Lead school in federation"] == df["LAEstab"]][
        ["URN", "School Name", "LAEstab"]
    ].rename(
        columns={
            "URN": "Federation Lead School URN",
            "School Name": "Federation Name",
            "LAEstab": "Federation LAEstab",
        }
    )

    return df.merge(
        lead_schools,
        left_on="Lead school in federation",
        right_on="Federation LAEstab",
        how="left",
    )
