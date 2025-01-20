import logging
from warnings import simplefilter

import numpy as np
import pandas as pd

import pipeline.config as config
import pipeline.input_schemas as input_schemas
import pipeline.mappings as mappings
from pipeline import part_year

simplefilter(action="ignore", category=pd.errors.PerformanceWarning)
simplefilter(action="ignore", category=FutureWarning)

logger = logging.getLogger("fbit-data-pipeline")


def prepare_aar_data(aar_path, current_year: int):
    """
    Process Academies Accounts Return (AAR) data.

    This processin includes:

    - removal of any rows where URN is absent (often due to extraneous
      rows in the input file)
    - removal of "1 day" Academies (which indicate a transitioning
      Academy for which there will be data elsewhere)
    - TODO

    :param aar_path: source from which data are to be read
    :param current_year: year in question
    :return: processed AAR data
    """
    aar = pd.read_csv(
        aar_path,
        encoding="utf-8",
        usecols=lambda x: x in input_schemas.aar_academies.keys(),
        dtype=input_schemas.aar_academies,
    )

    aar = aar[~aar["URN"].isna()]
    aar = aar[~(aar["ACADEMYTRUSTSTATUS"] == "1 day")]

    if "BNCH11123-BAI011-A (Academies - Income)" not in aar.columns:
        aar["BNCH11123-BAI011-A (Academies - Income)"] = 0.0

    aar["Income_Direct revenue finance"] = aar[
        "BNCH21707 (Direct revenue financing (Revenue contributions to capital))"
    ]

    aar["Income_Total grant funding"] = (
        aar["BNCH11110T (EFA Revenue Grants)"]
        + aar["BNCH11131 (DfE Family Revenue Grants)"]
        + aar["BNCH11141 (SEN)"]
        + aar["BNCH11142 (Other Revenue)"]
        + aar["BNCH11151 (Other Government Revenue Grants)"]
        + aar["BNCH11161 (Government source (non-grant))"]
        + aar["BNCH11162 (Academies)"]
        + aar["BNCH11163 (Non- Government)"]
        + aar["BNCH11123-BAI011-A (Academies - Income)"]
    )

    aar["Income_Total self generated funding"] = (
        aar["BNCH11201 (Income from facilities and services)"]
        + aar["BNCH11202 (Income from catering)"]
        + aar["BNCH11203 (Receipts from supply teacher insurance claims)"]
        + aar["BNCH11300T (Voluntary income)"]
        + aar["BNCH11204 (Other income - revenue)"]
        + aar["BNCH11205 (Other Income from facilities and services)"]
        + aar["BNCH11400T (Investment income)"]
    )

    aar["Income_Direct grants"] = (
        aar["BNCH11110T (EFA Revenue Grants)"]
        + aar["BNCH11131 (DfE Family Revenue Grants)"]
        + aar["BNCH11142 (Other Revenue)"]
        + aar["BNCH11151 (Other Government Revenue Grants)"]
        + aar["BNCH11123-BAI011-A (Academies - Income)"]
    )

    aar["Income_Pre Post 16"] = (
        aar["BNCH11110T (EFA Revenue Grants)"]
        + aar["BNCH11131 (DfE Family Revenue Grants)"]
        + aar["BNCH11123-BAI011-A (Academies - Income)"]
    )

    aar["Income_Other Revenue Income"] = (
        aar["BNCH11162 (Academies)"] + aar["BNCH11163 (Non- Government)"]
    )

    aar["Income_Facilities and services"] = (
        aar["BNCH11201 (Income from facilities and services)"]
        + aar["BNCH11205 (Other Income from facilities and services)"]
    )

    aar["Total Expenditure"] = (
        aar["BNCH21101 (Teaching staff)"]
        + aar["BNCH21102 (Supply teaching staff - extra note in guidance)"]
        + aar["BNCH21103 (Education support staff)"]
        + aar["BNCH21104 (Administrative and clerical staff)"]
        + aar["BNCH21105 (Premises staff)"]
        + aar["BNCH21106 (Catering staff)"]
        + aar["BNCH21107 (Other staff)"]
        + aar["BNCH21201 (Indirect employee expenses)"]
        + aar["BNCH21202 (Staff development and training)"]
        + aar["BNCH21203 (Staff-related insurance)"]
        + aar["BNCH21204 (Supply teacher insurance)"]
        + aar["BNCH21301 (Maintenance of premises)"]
        + aar["BNCH21405 (Grounds maintenance)"]
        + aar["BNCH21401 (Cleaning and caretaking)"]
        + aar["BNCH21402 (Water and sewerage)"]
        + aar["BNCH21403 (Energy)"]
        + aar["BNCH21404 (Rent and rates)"]
        + aar["BNCH21406 (Other occupation costs)"]
        + aar["BNCH21501 (Special facilities)"]
        + aar["BNCH21601 (Learning resources (not ICT equipment))"]
        + aar["BNCH21602 (ICT learning resources)"]
        + aar["BNCH21603 (Examination fees)"]
        + aar["BNCH21604 (Educational Consultancy)"]
        + aar["BNCH21706 (Administrative supplies - non educational)"]
        + aar["BNCH21606 (Agency supply teaching staff)"]
        + aar["BNCH21701 (Catering supplies)"]
        + aar["BNCH21705 (Other insurance premiums)"]
        + aar["BNCH21702 (Professional Services - non-curriculum)"]
        + aar["BNCH21703 (Auditor costs)"]
        + aar["BNCH21801 (Interest charges for Loan and bank)"]
        + aar["BNCH21802 (PFI Charges)"]
    )

    aar["Total Income"] = (
        aar["Income_Total grant funding"]
        + aar["Income_Total self generated funding"]
        - aar["BNCH21707 (Direct revenue financing (Revenue contributions to capital))"]
    )

    aar["In year balance"] = aar["Total Income"] - aar["Total Expenditure"]

    aar.rename(
        columns={
            "ACADEMYUPIN": "Academy UPIN",
            "Company_Number": "Company Registration Number",
            "Company_Name": "Trust Name",
            "Date joined or opened if in period:": "Date joined or opened if in period",
            "Date left or closed if in period:": "Date left or closed if in period",
        }
        | config.cost_category_map["academies"]
        | config.income_category_map["academies"],
        inplace=True,
    )

    trust_balance = (
        aar[["Company Registration Number", "In year balance"]]
        .groupby("Company Registration Number")
        .sum()
        .rename(columns={"In year balance": "Trust Balance"})
    )

    aar = aar.merge(trust_balance, on="Company Registration Number", how="left")

    aar["Financial Position"] = aar["In year balance"].map(
        mappings.map_is_surplus_deficit
    )

    aar["Trust Financial Position"] = aar["Trust Balance"].map(
        mappings.map_is_surplus_deficit
    )

    aar["London Weighting"] = aar.apply(
        lambda df: mappings.map_london_weighting(df["LA"], df["Estab"]), axis=1
    )

    aar["PFI School"] = aar["Other costs_PFI charges"].map(mappings.map_is_pfi_school)

    aar["Is PFI"] = aar["PFI School"].map(lambda x: x == "PFI School")

    return aar.set_index("URN")


def build_academy_data(
    schools,
    census,
    sen,
    cdc,
    aar,
    ks2,
    ks4,
    cfo,
    central_services,
):
    aar.rename(
        columns={
            "Date joined or opened if in period:": "Date joined or opened if in period",
            "Date left or closed if in period:": "Date left or closed if in period",
        },
        inplace=True,
    )

    academies = (
        aar.reset_index()
        .merge(schools, on="URN")
        .merge(census, on="URN", how="left")
        .merge(sen, on="URN", how="left")
        .merge(cdc, on="URN", how="left")
        .merge(
            cfo,
            left_on="Company Registration Number",
            right_on="Companies House Number",
            how="left",
        )
    )

    if ks2 is not None:
        academies = academies.merge(ks2, on="URN", how="left")

    if ks4 is not None:
        academies = academies.merge(ks4, on="URN", how="left")

    academies["Overall Phase"] = academies.apply(
        lambda df: mappings.map_phase_type(
            establishment_code=df["TypeOfEstablishment (code)"],
            phase_code=df["PhaseOfEducation (code)"],
        ),
        axis=1,
    )

    academies["Period covered by return"] = academies.apply(
        lambda df: mappings.map_academy_period_return(
            pd.to_datetime(df["Date joined or opened if in period"], dayfirst=True),
            pd.to_datetime(df["Date left or closed if in period"], dayfirst=True),
        ),
        axis=1,
    )

    # TODO: remove; duplicate of `Overall Phase`, above?
    academies["SchoolPhaseType"] = academies.apply(
        lambda df: mappings.map_phase_type(
            establishment_code=df["TypeOfEstablishment (code)"],
            phase_code=df["PhaseOfEducation (code)"],
        ),
        axis=1,
    )

    academies["Finance Type"] = "Academy"
    academies["Did Not Submit"] = False

    academies["OfstedLastInsp"] = pd.to_datetime(
        academies["OfstedLastInsp"], dayfirst=True
    )

    academies["Email"] = ""
    academies["HeadEmail"] = ""

    academies = academies.merge(
        central_services.reset_index(),
        on="Company Registration Number",
        how="left",
        suffixes=("", "_CS"),
    )

    academies["Number of pupils_pro_rata"] = academies["Number of pupils"] * (
        academies["Period covered by return"] / 12.0
    )
    academies["Total Internal Floor Area_pro_rata"] = academies[
        "Total Internal Floor Area"
    ] * (academies["Period covered by return"] / 12.0)
    trust_basis_data = (
        academies[
            [
                "Trust UPIN",
                "Number of pupils",
                "Number of pupils_pro_rata",
                "Total Internal Floor Area",
                "Total Internal Floor Area_pro_rata",
            ]
        ]
        .groupby(["Trust UPIN"])
        .sum()
        .rename(
            columns={
                "Number of pupils": "Total pupils in trust",
                "Number of pupils_pro_rata": "Total pupils in trust_pro_rata",
                "Total Internal Floor Area": "Total Internal Floor Area in trust",
                "Total Internal Floor Area_pro_rata": "Total Internal Floor Area in trust_pro_rata",
                "In year balance": "Trust Balance",
            }
        )
    )

    academies = academies.merge(trust_basis_data, on="Trust UPIN", how="left")

    academies.rename(
        columns={
            "LA (code)": "LA Code",
            "LA (name)": "LA Name",
        },
        inplace=True,
    )

    # TODO: avoid recalculating pupil/building apportionment.
    for category in config.rag_category_settings.keys():
        is_pupil_basis = (
            config.rag_category_settings[category]["type"] == "Pupil"
            if category in config.rag_category_settings
            else True
        )

        apportionment_divisor = academies[
            (
                "Total pupils in trust_pro_rata"
                if is_pupil_basis
                else "Total Internal Floor Area in trust_pro_rata"
            )
        ]

        apportionment_dividend = academies[
            (
                "Number of pupils_pro_rata"
                if is_pupil_basis
                else "Total Internal Floor Area_pro_rata"
            )
        ]

        basis_data = academies[
            (
                "Number of pupils_pro_rata"
                if config.rag_category_settings[category]["type"] == "Pupil"
                else "Total Internal Floor Area_pro_rata"
            )
        ]

        sub_categories = academies.columns[
            academies.columns.str.startswith(category)
            & ~academies.columns.str.endswith("_CS")
        ].values.tolist()

        apportionment = apportionment_dividend.astype(
            float
        ) / apportionment_divisor.astype(float)

        for sub_category in sub_categories:
            academies[sub_category + "_CS"] = academies[sub_category + "_CS"].astype(
                float
            ) * apportionment.astype(float).fillna(0.0)

            academies[sub_category] = (
                academies[sub_category] + academies[sub_category + "_CS"]
            )
            academies[sub_category + "_Per Unit"] = (
                academies[sub_category].fillna(0.0) / basis_data
            ).astype(float)

            # Here be dragons, well angry pandas: this looks like it can be replaced with `np.isclose` it can, but it doesn't seem to work in all cases, you will end up with
            # the odd -0.01 value or an untouched {rand-floating-point}e-16 which will cause an arithmetic overflow when writing to the DB.
            academies.loc[
                (academies[sub_category + "_Per Unit"] >= 0)
                & (academies[sub_category + "_Per Unit"] <= 0.00001),
                sub_category + "_Per Unit",
            ] = 0.0

            academies[sub_category + "_Per Unit"].replace(
                [np.inf, -np.inf, np.nan], 0.0, inplace=True
            )

        academies[category + "_Total_Per Unit"] = (
            academies[
                academies.columns[
                    academies.columns.str.startswith(category)
                    & ~academies.columns.str.endswith("_CS")
                    & academies.columns.str.endswith("_Per Unit")
                ]
            ]
            .fillna(0.0)
            .sum(axis=1)
        )

        academies.loc[
            (academies[category + "_Total_Per Unit"] >= 0)
            & (academies[category + "_Total_Per Unit"] <= 0.00001),
            category + "_Total_Per Unit",
        ] = 0.0

        academies[category + "_Total"] = (
            academies[
                academies.columns[
                    academies.columns.str.startswith(category)
                    & ~academies.columns.str.endswith("_CS")
                    & ~academies.columns.str.endswith("_Per Unit")
                ]
            ]
            .fillna(0.0)
            .sum(axis=1)
        )

        academies[category + "_Total_CS"] = (
            academies[
                academies.columns[
                    academies.columns.str.startswith(category)
                    & academies.columns.str.endswith("_CS")
                    & ~academies.columns.str.endswith("_Per Unit")
                ]
            ]
            .fillna(0)
            .sum(axis=1)
        )

    income_cols = academies.columns[
        academies.columns.str.startswith("Income_")
        & academies.columns.str.endswith("_CS")
        & ~academies.columns.str.startswith("Financial Position")
    ].values.tolist()

    for income_col in income_cols:
        # Income cols `Income_XXXX_CS` have the format.
        comps = income_col.split("_")
        academies[income_col] = academies[income_col] * (
            academies["Number of pupils_pro_rata"].astype(float)
            / academies["Total pupils in trust_pro_rata"].astype(float)
        ).fillna(0.0)

        # Target income category from academy base data
        target_income_col = f"{comps[0]}_{comps[1]}"
        academies[target_income_col] = (
            academies[target_income_col] + academies[income_col]
        )

    academies["In year balance_CS"] = academies["In year balance_CS"] * (
        academies["Number of pupils_pro_rata"].astype(float)
        / academies["Total pupils in trust_pro_rata"].astype(float)
    ).fillna(0.0)

    academies["In year balance"] = (
        academies["In year balance"] + academies["In year balance_CS"]
    )

    academies["Total Income_CS"] = academies["Total Income_CS"] * (
        academies["Number of pupils_pro_rata"].astype(float)
        / academies["Total pupils in trust_pro_rata"].astype(float)
    ).fillna(0.0)

    academies["Total Income"] = academies["Total Income"] + academies["Total Income_CS"]

    academies["Total Expenditure_CS"] = academies["Total Expenditure_CS"] * (
        academies["Number of pupils_pro_rata"].astype(float)
        / academies["Total pupils in trust_pro_rata"].astype(float)
    ).fillna(0.0)

    academies["Total Expenditure"] = (
        academies["Total Expenditure"] + academies["Total Expenditure_CS"]
    )

    # net catering cost, not net catering income
    academies["Catering staff and supplies_Net Costs"] = (
        academies["Catering staff and supplies_Total"]
        - academies["Income_Catering services"]
    )

    academies["Catering staff and supplies_Net Costs_CS"] = academies[
        "Catering staff and supplies_Total_CS"
    ] - academies["Income_Catering services_CS"].fillna(0.0)

    academies = _trust_revenue_reserve(academies, central_services)

    academies["Company Registration Number"] = academies[
        "Company Registration Number"
    ].map(mappings.map_company_number)

    return academies.set_index("URN")


def _trust_revenue_reserve(
    academies: pd.DataFrame,
    central_services: pd.DataFrame,
) -> pd.DataFrame:
    """
    Calculate the "revenue reserve" for each Academy.

    Revenue reserve is a balance that is legally "owned" by the Trust,
    not the Academies. As such, the _total_ revenue reserve is
    calculated for the whole Trust and then apportioned—pro rata based
    on time spent in the period—to Academies.

    Note: revenue reserve is a balance and as such, only pertains to
    schools in the Trust at the end of the period.

    :param academies: Academy data
    :param central_services: Central Services data
    :return: updated Academy data
    """
    mask = academies.index.duplicated(keep=False) & ~academies["Valid To"].isna()
    _academies = academies[~mask]

    trust_revenue_reserve = (
        _academies[["Trust UPIN", "Revenue reserve"]]
        .rename(columns={"Revenue reserve": "Sum Revenue reserve"})
        .groupby(["Trust UPIN"])
        .sum()
    )

    trust_revenue_reserve = trust_revenue_reserve.merge(
        central_services.reset_index()[["Trust UPIN", "Revenue reserve"]].rename(
            columns={"Revenue reserve": "Revenue reserve_CS"}
        ),
        on="Trust UPIN",
        how="inner",
    )

    trust_revenue_reserve["Trust Revenue reserve"] = (
        trust_revenue_reserve["Sum Revenue reserve"]
        + trust_revenue_reserve["Revenue reserve_CS"]
    )

    _academies = _academies.reset_index().merge(
        trust_revenue_reserve[["Trust UPIN", "Trust Revenue reserve"]],
        on="Trust UPIN",
    )

    academies = academies.merge(
        _academies[["URN", "Trust UPIN", "Trust Revenue reserve"]],
        on=["URN", "Trust UPIN"],
        how="left",
    )

    academies["Revenue reserve"] = (
        (
            academies["Trust Revenue reserve"]
            / academies["Total pupils in trust_pro_rata"]
        )
        * academies["Number of pupils_pro_rata"]
    ).fillna(0.0)

    return academies


def map_academy_data(df: pd.DataFrame) -> pd.DataFrame:
    """
    Derive additional columns from academy data.

    Currently, this largely pertains to part-year information.

    :param df: academy data
    :return: updated data
    """
    df = part_year.academies.map_is_early_transfer(df)
    df = part_year.academies.map_has_financial_data(df)
    df = part_year.academies.map_partial_year_present(df)
    df = part_year.common.map_has_pupil_comparator_data(df)
    df = part_year.common.map_has_building_comparator_data(df)

    return df
