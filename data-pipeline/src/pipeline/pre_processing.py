import datetime
import logging
from warnings import simplefilter

import numpy as np
import pandas as pd

import src.pipeline.bfr as BFR
import src.pipeline.config as config
import src.pipeline.input_schemas as input_schemas
import src.pipeline.maintained_schools as maintained_pipeline
import src.pipeline.mappings as mappings
from src.pipeline import part_year

simplefilter(action="ignore", category=pd.errors.PerformanceWarning)
simplefilter(action="ignore", category=FutureWarning)

logger = logging.getLogger("fbit-data-pipeline")


def prepare_cdc_data(cdc_file_path, current_year):
    cdc = pd.read_csv(
        cdc_file_path,
        encoding="utf8",
        index_col=input_schemas.cdc_index_col,
        usecols=input_schemas.cdc.keys(),
        dtype=input_schemas.cdc,
    )

    cdc["Total Internal Floor Area"] = cdc.groupby(by=["URN"])["GIFA"].sum()
    cdc["Proportion Area"] = cdc["GIFA"] / cdc["Total Internal Floor Area"]
    cdc["Indicative Age"] = (
        cdc["Block Age"].fillna("").map(mappings.map_block_age).astype("Int64")
    )
    cdc["Age Score"] = cdc["Proportion Area"] * (current_year - cdc["Indicative Age"])
    cdc["Age Average Score"] = cdc.groupby(by=["URN"])["Age Score"].sum()
    cdc["Building Age"] = (
        cdc.groupby(by=["URN"])["Indicative Age"].mean().astype("Int64")
    )
    result = cdc[config.cdc_generated_columns]

    return result[~result.index.duplicated(keep="first")]


# noinspection PyTypeChecker
def prepare_census_data(workforce_census_path, pupil_census_path):
    school_workforce_census = pd.read_excel(
        workforce_census_path,
        header=5,
        usecols=input_schemas.workforce_census.keys(),
        na_values=["x", "u", "c", "z", ":"],
        keep_default_na=True,
    ).drop_duplicates()

    school_workforce_census = school_workforce_census.astype(
        input_schemas.workforce_census
    ).set_index(input_schemas.workforce_census_index_col)

    school_pupil_census = pd.read_csv(
        pupil_census_path,
        encoding="cp1252",
        index_col=input_schemas.pupil_census_index_col,
        usecols=lambda x: x in input_schemas.pupil_census.keys(),
        dtype=input_schemas.pupil_census,
        na_values=["x", "u", "c", "z"],
        keep_default_na=True,
    ).drop_duplicates()

    if "number_of_dual_subsidiary_registrations" in school_pupil_census.columns:
        school_pupil_census.rename(
            columns={
                "number_of_dual_subsidiary_registrations": "Pupil Dual Registrations"
            },
            inplace=True,
        )
        school_pupil_census["Pupil Dual Registrations"] = school_pupil_census[
            "Pupil Dual Registrations"
        ].fillna(0)
    else:
        school_pupil_census["Pupil Dual Registrations"] = 0

    census = school_pupil_census.join(
        school_workforce_census,
        on="URN",
        how="inner",
        rsuffix="_pupil",
        lsuffix="_workforce",
    ).rename(columns=config.census_column_map)

    census["Number of pupils"] = (
        census["Number of pupils"] + census["Pupil Dual Registrations"]
    )

    census["TotalPupilsNursery"] = (
        census["Number of early year pupils (years E1 and E2)"]
        + census["Number of nursery pupils (years N1 and N2)"]
    )

    census["TotalPupilsSixthForm"] = (
        census["Full time boys Year group 12"]
        + census["Full time boys Year group 13"]
        + census["Full time girls Year group 12"]
        + census["Full time girls Year group 13"]
    )

    return census


def prepare_sen_data(sen_path):
    sen = pd.read_csv(
        sen_path,
        encoding="cp1252",
        index_col=input_schemas.sen_index_col,
        dtype=input_schemas.sen,
        usecols=input_schemas.sen.keys(),
    )
    sen["Percentage SEN"] = (
        ((sen["EHC plan"] + sen["SEN support"]) / sen["Total pupils"]) * 100.0
    ).fillna(0)
    sen["Percentage with EHC"] = (
        (sen["EHC plan"] / sen["Total pupils"]) * 100.0
    ).fillna(0)
    sen["Percentage without EHC"] = sen["Percentage SEN"] - sen["Percentage with EHC"]

    sen["Primary Need SPLD"] = (
        sen["EHC_Primary_need_spld"] + sen["SUP_Primary_need_spld"]
    )
    sen["Primary Need MLD"] = sen["EHC_Primary_need_mld"] + sen["SUP_Primary_need_mld"]
    sen["Primary Need SLD"] = sen["EHC_Primary_need_sld"] + sen["SUP_Primary_need_sld"]
    sen["Primary Need PMLD"] = (
        sen["EHC_Primary_need_pmld"] + sen["SUP_Primary_need_pmld"]
    )
    sen["Primary Need SEMH"] = (
        sen["EHC_Primary_need_semh"] + sen["SUP_Primary_need_semh"]
    )
    sen["Primary Need SLCN"] = (
        sen["EHC_Primary_need_slcn"] + sen["SUP_Primary_need_slcn"]
    )
    sen["Primary Need HI"] = sen["EHC_Primary_need_hi"] + sen["SUP_Primary_need_hi"]
    sen["Primary Need VI"] = sen["EHC_Primary_need_vi"] + sen["SUP_Primary_need_vi"]
    sen["Primary Need MSI"] = sen["EHC_Primary_need_msi"] + sen["SUP_Primary_need_msi"]
    sen["Primary Need PD"] = sen["EHC_Primary_need_pd"] + sen["SUP_Primary_need_pd"]
    sen["Primary Need ASD"] = sen["EHC_Primary_need_asd"] + sen["SUP_Primary_need_asd"]
    sen["Primary Need OTH"] = sen["EHC_Primary_need_oth"] + sen["SUP_Primary_need_oth"]

    sen["Percentage Primary Need SPLD"] = (
        (sen["Primary Need SPLD"] / sen["Total pupils"]) * 100.0
    ).fillna(0)
    sen["Percentage Primary Need MLD"] = (
        (sen["Primary Need MLD"] / sen["Total pupils"]) * 100.0
    ).fillna(0)
    sen["Percentage Primary Need SLD"] = (
        (sen["Primary Need SLD"] / sen["Total pupils"]) * 100.0
    ).fillna(0)
    sen["Percentage Primary Need PMLD"] = (
        (sen["Primary Need PMLD"] / sen["Total pupils"]) * 100.0
    ).fillna(0)
    sen["Percentage Primary Need SEMH"] = (
        (sen["Primary Need SEMH"] / sen["Total pupils"]) * 100.0
    ).fillna(0)
    sen["Percentage Primary Need SLCN"] = (
        (sen["Primary Need SLCN"] / sen["Total pupils"]) * 100.0
    ).fillna(0)
    sen["Percentage Primary Need HI"] = (
        (sen["Primary Need HI"] / sen["Total pupils"]) * 100.0
    ).fillna(0)
    sen["Percentage Primary Need VI"] = (
        (sen["Primary Need VI"] / sen["Total pupils"]) * 100.0
    ).fillna(0)
    sen["Percentage Primary Need MSI"] = (
        (sen["Primary Need MSI"] / sen["Total pupils"]) * 100.0
    ).fillna(0)
    sen["Percentage Primary Need PD"] = (
        (sen["Primary Need PD"] / sen["Total pupils"]) * 100.0
    ).fillna(0)
    sen["Percentage Primary Need ASD"] = (
        (sen["Primary Need ASD"] / sen["Total pupils"]) * 100.0
    ).fillna(0)
    sen["Percentage Primary Need OTH"] = (
        (sen["Primary Need OTH"] / sen["Total pupils"]) * 100.0
    ).fillna(0)

    return sen[config.sen_generated_columns]


def prepare_ks2_data(ks2_path):
    if ks2_path is not None:
        ks2 = pd.read_excel(
            ks2_path,
            usecols=input_schemas.ks2.keys(),
            dtype=input_schemas.ks2,
        )
        ks2["READPROG"] = ks2["READPROG"].replace({"SUPP": "0", "LOWCOV": "0"})
        ks2["MATPROG"] = ks2["MATPROG"].replace({"SUPP": "0", "LOWCOV": "0"})
        ks2["WRITPROG"] = ks2["WRITPROG"].replace({"SUPP": "0", "LOWCOV": "0"})

        ks2["Ks2Progress"] = (
            ks2["READPROG"].astype(float)
            + ks2["MATPROG"].astype(float)
            + ks2["WRITPROG"].astype(float)
        )
        ks2 = ks2[["URN", "Ks2Progress"]].dropna().drop_duplicates()
    else:
        ks2 = pd.DataFrame(
            {"URN": pd.Series(dtype="Int64"), "Ks2Progress": pd.Series(dtype="float")}
        )
    return ks2.set_index("URN")


def prepare_ks4_data(ks4_path):
    if ks4_path is not None:
        ks4 = pd.read_excel(
            ks4_path,
            dtype=input_schemas.ks4,
            usecols=input_schemas.ks4.keys(),
            na_values=["NP", "NE", "SUPP", "LOWCOV"],
        )
        ks4["ATT8SCR"] = ks4["ATT8SCR"].astype(float).fillna(0)
        ks4["P8MEA"] = ks4["P8MEA"].astype(float).fillna(0)

        ks4.rename(
            columns={
                "ATT8SCR": "AverageAttainment",
                "P8MEA": "Progress8Measure",
                "P8_BANDING": "Progress8Banding",
            },
            inplace=True,
        )

        ks4 = (
            ks4[["URN", "AverageAttainment", "Progress8Measure", "Progress8Banding"]]
            .dropna()
            .drop_duplicates()
        )
    else:
        ks4 = pd.DataFrame(
            {
                "URN": pd.Series(dtype="Int64"),
                "AverageAttainment": pd.Series(dtype="float"),
                "Progress8Measure": pd.Series(dtype="float"),
                "Progress8Banding": pd.Series(dtype="string"),
            }
        )

    return ks4.set_index("URN")


def prepare_central_services_data(cs_path, current_year: int):
    central_services_financial = pd.read_csv(
        cs_path,
        encoding="utf-8",
        usecols=lambda x: x in input_schemas.aar_central_services.keys(),
        dtype=input_schemas.aar_central_services,
    )

    if (current_year < 2023) or (
        "BNCH11123-BTI011-A (MAT Central services - Income)"
        not in central_services_financial.columns
    ):
        central_services_financial[
            "BNCH11123-BTI011-A (MAT Central services - Income)"
        ] = 0.0

        if (current_year <= 2022) and (
            "BNCHBAI061 (Coronavirus Govt Funding)"
            in central_services_financial.columns
        ):
            central_services_financial[
                "BNCH11123-BTI011-A (MAT Central services - Income)"
            ] = central_services_financial["BNCHBAI061 (Coronavirus Govt Funding)"]

    central_services_financial["In year balance"] = (
        central_services_financial["BNCH11000T (Revenue Income)"]
        - central_services_financial["BNCH20000T (Total Costs)"]
    )

    central_services_financial["Income_Total grant funding"] = (
        central_services_financial["BNCH11110T (EFA Revenue Grants)"]
        + central_services_financial["BNCH11131 (DfE Family Revenue Grants)"]
        + central_services_financial["BNCH11141 (SEN)"]
        + central_services_financial["BNCH11142 (Other Revenue)"]
        + central_services_financial["BNCH11151 (Other Government Revenue Grants)"]
        + central_services_financial["BNCH11161 (Government source (non-grant))"]
        + central_services_financial["BNCH11162 (Academies)"]
        + central_services_financial["BNCH11163 (Non- Government)"]
        + central_services_financial[
            "BNCH11123-BTI011-A (MAT Central services - Income)"
        ]
    )

    central_services_financial["Income_Total self generated funding"] = (
        central_services_financial["BNCH11201 (Income from facilities and services)"]
        + central_services_financial["BNCH11202 (Income from catering)"]
        + central_services_financial[
            "BNCH11203 (Receipts from supply teacher insurance claims)"
        ]
        + central_services_financial["BNCH11300T (Voluntary income)"]
        + central_services_financial["BNCH11204 (Other income - revenue)"]
        + central_services_financial[
            "BNCH11205 (Other Income from facilities and services)"
        ]
        + central_services_financial["BNCH11400T (Investment income)"]
    )

    central_services_financial["Income_Direct grants"] = (
        central_services_financial["BNCH11110T (EFA Revenue Grants)"]
        + central_services_financial["BNCH11131 (DfE Family Revenue Grants)"]
        + central_services_financial["BNCH11142 (Other Revenue)"]
        + central_services_financial["BNCH11151 (Other Government Revenue Grants)"]
        + central_services_financial[
            "BNCH11123-BTI011-A (MAT Central services - Income)"
        ]
    )

    central_services_financial["Income_Other DFE grants"] = (
        central_services_financial["BNCH11110T (EFA Revenue Grants)"]
        + central_services_financial["BNCH11131 (DfE Family Revenue Grants)"]
        + central_services_financial[
            "BNCH11123-BTI011-A (MAT Central services - Income)"
        ]
    )

    central_services_financial["Income_Other Revenue Income"] = (
        central_services_financial["BNCH11162 (Academies)"]
        + central_services_financial["BNCH11163 (Non- Government)"]
    )

    central_services_financial["Income_Facilities and services"] = (
        central_services_financial["BNCH11201 (Income from facilities and services)"]
        + central_services_financial[
            "BNCH11205 (Other Income from facilities and services)"
        ]
    )

    central_services_financial["Total Expenditure"] = (
        central_services_financial["BNCH21101 (Teaching staff)"]
        + central_services_financial[
            "BNCH21102 (Supply teaching staff - extra note in guidance)"
        ]
        + central_services_financial["BNCH21103 (Education support staff)"]
        + central_services_financial["BNCH21104 (Administrative and clerical staff)"]
        + central_services_financial["BNCH21105 (Premises staff)"]
        + central_services_financial["BNCH21106 (Catering staff)"]
        + central_services_financial["BNCH21107 (Other staff)"]
        + central_services_financial["BNCH21201 (Indirect employee expenses)"]
        + central_services_financial["BNCH21202 (Staff development and training)"]
        + central_services_financial["BNCH21203 (Staff-related insurance)"]
        + central_services_financial["BNCH21204 (Supply teacher insurance)"]
        + central_services_financial["BNCH21301 (Maintenance of premises)"]
        + central_services_financial["BNCH21405 (Grounds maintenance)"]
        + central_services_financial["BNCH21401 (Cleaning and caretaking)"]
        + central_services_financial["BNCH21402 (Water and sewerage)"]
        + central_services_financial["BNCH21403 (Energy)"]
        + central_services_financial["BNCH21404 (Rent and rates)"]
        + central_services_financial["BNCH21406 (Other occupation costs)"]
        + central_services_financial["BNCH21501 (Special facilities)"]
        + central_services_financial[
            "BNCH21601 (Learning resources (not ICT equipment))"
        ]
        + central_services_financial["BNCH21602 (ICT learning resources)"]
        + central_services_financial["BNCH21603 (Examination fees)"]
        + central_services_financial["BNCH21604 (Educational Consultancy)"]
        + central_services_financial[
            "BNCH21706 (Administrative supplies - non educational)"
        ]
        + central_services_financial["BNCH21606 (Agency supply teaching staff)"]
        + central_services_financial["BNCH21701 (Catering supplies)"]
        + central_services_financial["BNCH21705 (Other insurance premiums)"]
        + central_services_financial[
            "BNCH21702 (Professional Services - non-curriculum)"
        ]
        + central_services_financial["BNCH21703 (Auditor costs)"]
        + central_services_financial["BNCH21801 (Interest charges for Loan and bank)"]
        + central_services_financial["BNCH21802 (PFI Charges)"]
        - central_services_financial[
            "BNCH21707 (Direct revenue financing (Revenue contributions to capital))"
        ]
    )

    central_services_financial["Total Income"] = (
        central_services_financial["Income_Total grant funding"]
        + central_services_financial["Income_Total self generated funding"]
        - central_services_financial[
            "BNCH21707 (Direct revenue financing (Revenue contributions to capital))"
        ]
    )

    central_services_financial.rename(
        columns={
            "Lead_UPIN": "Trust UPIN",
            "Company_Number": "Company Registration Number",
        }
        | config.cost_category_map["central_services"]
        | config.income_category_map["central_services"],
        inplace=True,
    )

    central_services_financial["Financial Position"] = central_services_financial[
        "In year balance"
    ].map(mappings.map_is_surplus_deficit)

    return central_services_financial.set_index("Trust UPIN")


def prepare_aar_data(aar_path, current_year: int):
    aar = pd.read_csv(
        aar_path,
        encoding="utf-8",
        usecols=lambda x: x in input_schemas.aar_academies.keys(),
        dtype=input_schemas.aar_academies,
    )

    aar = aar[~aar["URN"].isna()]

    if (current_year < 2023) or (
        "BNCH11123-BAI011-A (Academies - Income)" not in aar.columns
    ):
        aar["BNCH11123-BAI011-A (Academies - Income)"] = 0.0

    aar["In year balance"] = (
        aar["BNCH11000T (Revenue Income)"] - aar["BNCH20000T (Total Costs)"]
    )

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

    aar["Income_Other DFE grants"] = (
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
        - aar["BNCH21707 (Direct revenue financing (Revenue contributions to capital))"]
    )

    aar["Total Income"] = (
        aar["Income_Total grant funding"]
        + aar["Income_Total self generated funding"]
        - aar["BNCH21707 (Direct revenue financing (Revenue contributions to capital))"]
    )

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


def prepare_schools_data(base_data_path, links_data_path):
    gias = pd.read_csv(
        base_data_path,
        encoding="cp1252",
        index_col=input_schemas.gias_index_col,
        usecols=input_schemas.gias.keys(),
        dtype=input_schemas.gias,
    )

    gias_links = pd.read_csv(
        links_data_path,
        encoding="cp1252",
        index_col=input_schemas.gias_links_index_col,
        usecols=input_schemas.gias_links.keys(),
        dtype=input_schemas.gias_links,
    )

    # GIAS transformations
    gias["LA Establishment Number"] = (
        gias["LA (code)"].astype("string")
        + "-"
        + gias["EstablishmentNumber"].astype("string")
    )
    gias["LA Establishment Number"] = gias["LA Establishment Number"].astype("string")

    gias["OpenDate"] = pd.to_datetime(gias["OpenDate"], dayfirst=True, format="mixed")
    gias["CloseDate"] = pd.to_datetime(gias["CloseDate"], dayfirst=True, format="mixed")
    gias["SchoolWebsite"] = (
        gias["SchoolWebsite"].fillna("").map(mappings.map_school_website)
    )

    gias["Boarders (name)"] = (
        gias["Boarders (name)"].fillna("").map(mappings.map_boarders)
    )

    gias["OfstedRating (name)"] = (
        gias["OfstedRating (name)"].fillna("").map(mappings.map_ofsted_rating)
    )

    gias["TypeOfEstablishment (name)"] = (
        gias["TypeOfEstablishment (name)"].fillna("").map(lambda x: x.strip())
    )

    gias["NurseryProvision (name)"] = gias["NurseryProvision (name)"].fillna("")

    gias["NurseryProvision (name)"] = gias.apply(
        lambda df: mappings.map_nursery(
            df["NurseryProvision (name)"], df["PhaseOfEducation (name)"]
        ),
        axis=1,
    )

    gias["Has Nursery"] = gias["NurseryProvision (name)"].map(mappings.map_has_nursery)

    gias["OfficialSixthForm (name)"] = (
        gias["OfficialSixthForm (name)"].fillna("").map(mappings.map_sixth_form)
    )

    gias["Has Sixth Form"] = gias["OfficialSixthForm (name)"].map(
        mappings.map_has_sixth_form
    )

    gias["AdmissionsPolicy (name)"] = (
        gias["AdmissionsPolicy (name)"].fillna("").map(mappings.map_admission_policy)
    )

    gias_links = gias_links[
        gias_links["LinkType"].isin(
            [
                "Predecessor",
                "Predecessor - amalgamated",
                "Predecessor - Split School",
                "Predecessor - merged",
                "Merged - expansion of school capacity",
                "Merged - change in age range",
            ]
        )
    ].sort_values(by="LinkEstablishedDate", ascending=False)

    gias_links["Rank"] = gias_links.groupby("URN").cumcount() + 1
    gias_links["Rank"] = gias_links["Rank"].astype("Int64")

    schools = gias.join(
        gias_links, on="URN", how="left", rsuffix="_links", lsuffix="_school"
    ).sort_values(by="URN")

    return schools[(schools["Rank"] == 1) | (schools["Rank"].isna())].drop(
        columns=["LinkURN", "LinkName", "LinkType", "LinkEstablishedDate", "Rank"]
    )


def build_cfo_data(cfo_data_path) -> pd.DataFrame:
    """
    Read Chief Financial Officer (CFO) details.

    Note: CFO details are at Trust level.

    :param cfo_data_path: from which to read data
    :return: cfo DataFrame
    """
    cfo_data = pd.read_excel(
        cfo_data_path,
        usecols=[
            "Companies House Number",
            "Title",
            "Forename 1",
            "Surname",
            "Direct email address",
        ],
        dtype=str,
    ).rename(
        columns={
            "Direct email address": "CFO email",
        },
    )

    cfo_data["CFO name"] = (
        cfo_data["Title"] + " " + cfo_data["Forename 1"] + " " + cfo_data["Surname"]
    )

    return cfo_data[["Companies House Number", "CFO name", "CFO email"]]


def build_academy_data(
    year,
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
    accounts_return_period_start_date = datetime.date(year - 1, 9, 10)
    academy_year_start_date = datetime.date(year - 1, 9, 1)
    academy_year_end_date = datetime.date(year, 8, 30)

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

    academies["Status"] = academies.apply(
        lambda df: mappings.map_academy_status(
            pd.to_datetime(df["Date joined or opened if in period"], dayfirst=True),
            pd.to_datetime(df["Date left or closed if in period"], dayfirst=True),
            pd.to_datetime(df["Valid To"], dayfirst=True),
            pd.to_datetime(df["OpenDate"]),
            pd.to_datetime(df["CloseDate"]),
            pd.to_datetime(accounts_return_period_start_date),
            pd.to_datetime(academy_year_start_date),
            pd.to_datetime(academy_year_end_date),
        ),
        axis=1,
    )

    # TODO: should factor into apportionment.
    academies["Period covered by return"] = academies.apply(
        lambda df: mappings.map_academy_period_return(
            pd.to_datetime(df["Date joined or opened if in period"], dayfirst=True),
            pd.to_datetime(df["Date left or closed if in period"], dayfirst=True),
            pd.to_datetime(academy_year_start_date),
            pd.to_datetime(academy_year_end_date),
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

    # TODO: pro-rata apportionment dividends.
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
            ) * apportionment.astype(float)

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
            academies["Number of pupils"].astype(float)
            / academies["Total pupils in trust"].astype(float)
        )

        # Target income category from academy base data
        target_income_col = f"{comps[0]}_{comps[1]}"
        academies[target_income_col] = (
            academies[target_income_col] + academies[income_col]
        )

    academies["In year balance_CS"] = academies["In year balance_CS"] * (
        academies["Number of pupils"].astype(float)
        / academies["Total pupils in trust"].astype(float)
    )

    academies["In year balance"] = (
        academies["In year balance"] + academies["In year balance_CS"]
    )

    academies["Revenue reserve_CS"] = academies["Revenue reserve_CS"] * (
        academies["Number of pupils"].astype(float)
        / academies["Total pupils in trust"].astype(float)
    )

    academies["Revenue reserve"] = (
        academies["Revenue reserve"] + academies["Revenue reserve_CS"]
    )

    academies["Total Income_CS"] = academies["Total Income_CS"] * (
        academies["Number of pupils"].astype(float)
        / academies["Total pupils in trust"].astype(float)
    )

    academies["Total Income"] = academies["Total Income"] + academies["Total Income_CS"]

    academies["Total Expenditure_CS"] = academies["Total Expenditure_CS"] * (
        academies["Number of pupils"].astype(float)
        / academies["Total pupils in trust"].astype(float)
    )

    academies["Total Expenditure"] = (
        academies["Total Expenditure"] + academies["Total Expenditure_CS"]
    )

    # net catering cost, not net catering income
    academies["Catering staff and supplies_Net Costs"] = (
        academies["Catering staff and supplies_Total"]
        - academies["Income_Catering services"]
    )

    academies["Catering staff and supplies_Net Costs_CS"] = (
        academies["Catering staff and supplies_Total_CS"]
        - academies["Income_Catering services_CS"]
    )

    trust_revenue_reserve = (
        academies[["Trust UPIN", "Revenue reserve"]]
        .groupby(["Trust UPIN"])
        .sum()
        .rename(
            columns={
                "Revenue reserve": "Trust Revenue reserve",
            }
        )
    )

    academies = academies.merge(trust_revenue_reserve, on="Trust UPIN", how="left")

    academies["Company Registration Number"] = academies[
        "Company Registration Number"
    ].map(mappings.map_company_number)

    return academies.set_index("URN")


def build_trust_data(academies: pd.DataFrame) -> pd.DataFrame:
    """
    Build Trust financial information.

    Academy financial information are rolled up to Trust level.

    :param academies: Academy financial information
    :return: Trust-level financial information
    """
    df = (
        academies.reset_index()[
            [
                c
                for c in config.trust_db_projections.keys()
                if c != "Trust Financial Position"
            ]
        ]
        .groupby("Company Registration Number")
        .sum()
    )

    df["Trust Financial Position"] = df["In year balance"].map(
        mappings.map_is_surplus_deficit
    )

    return df


def map_academy_data(df: pd.DataFrame) -> pd.DataFrame:
    """
    Derive additional columns from academy data.

    Currently, this largely pertains to part-year information.

    :param df: academy data
    :return: updated data
    """
    df = part_year.academies.map_is_day_one_return(df)
    df = part_year.academies.map_is_early_transfer(df)
    df = part_year.academies.map_has_financial_data(df)
    df = part_year.academies.map_partial_year_present(df)
    df = part_year.common.map_has_pupil_comparator_data(df)
    df = part_year.common.map_has_building_comparator_data(df)

    return df


def build_maintained_school_data(
    maintained_schools_data_path,
    links_data_path,
    year,
    schools,
    census,
    sen,
    cdc,
    ks2,
    ks4,
):

    maintained_schools_list = pd.read_csv(
        maintained_schools_data_path,
        encoding="unicode-escape",
        usecols=input_schemas.maintained_schools_master_list.keys(),
    )

    maintained_schools = maintained_pipeline.create_master_list(
        maintained_schools_list, schools, sen, census, cdc, ks2, ks4
    )

    maintained_schools = maintained_pipeline.map_status(maintained_schools, year)
    maintained_schools = maintained_pipeline.map_pfi(maintained_schools)
    maintained_schools = maintained_pipeline.map_submission_attrs(maintained_schools)
    maintained_schools = maintained_pipeline.map_school_attrs(maintained_schools)
    maintained_schools = maintained_pipeline.map_school_type_attrs(maintained_schools)
    maintained_schools = maintained_pipeline.calc_base_financials(maintained_schools)
    maintained_schools = maintained_pipeline.map_cost_income_categories(
        maintained_schools,
        config.cost_category_map["maintained_schools"],
        config.income_category_map["maintained_schools"],
    )

    maintained_schools = maintained_pipeline.calc_rag_cost_series(
        maintained_schools, config.rag_category_settings
    )
    maintained_schools = maintained_pipeline.calc_catering_net_costs(maintained_schools)
    maintained_schools = maintained_schools[maintained_schools.index.notnull()]

    (hard_federations, soft_federations) = build_federations_data(
        links_data_path, maintained_schools
    )

    # partial-year checks…
    maintained_schools = part_year.maintained_schools.map_has_financial_data(
        maintained_schools
    )
    maintained_schools = part_year.common.map_has_pupil_comparator_data(
        maintained_schools
    )
    maintained_schools = part_year.common.map_has_building_comparator_data(
        maintained_schools
    )

    # Applying federation mappings
    maintained_schools = maintained_pipeline.apply_federation_mapping(
        maintained_schools, hard_federations, soft_federations
    )

    return maintained_schools.set_index("URN")


def build_federations_data(links_data_path, maintained_schools):
    group_links = pd.read_csv(
        links_data_path,
        encoding="unicode-escape",
        index_col=input_schemas.groups_index_col,
        usecols=input_schemas.groups.keys(),
        dtype=input_schemas.groups,
    )

    federations = maintained_schools[["URN", "LAEstab"]].copy()

    # join
    federations = federations.join(
        group_links[["Group Name", "Group UID", "Closed Date"]], on="URN"
    )

    # remove federations with an associated closed date
    federations = federations.loc[federations["Closed Date"].isna()]

    # federations with a UID listed in the GIAS groups data are referred to as "Hard" federations
    # while federations not listed in GIAS are referred to as "Soft" federations.
    # Soft federation UIDs are a combination of their URN and LAEstab codes.

    # create mask for soft federations
    mask = federations["Group UID"].isna()

    hard_federations = federations.loc[~mask].copy()
    soft_federations = federations.loc[mask].copy()

    # define members list for hard federations
    group_links["Members"] = group_links.index
    hard_members = group_links[["Members", "Group UID"]].groupby("Group UID").agg(list)

    hard_federations = hard_federations.join(hard_members, on="Group UID")

    # Rename columns
    hard_federations.rename(
        columns={
            "Group Name": "FederationName",
            "Group UID": "FederationUid",
        },
        inplace=True,
    )

    # for the soft federations
    soft_federations["Group UID"] = soft_federations.index.astype(
        str
    ) + soft_federations["LAEstab"].astype(str)

    # Rename columns
    soft_federations.rename(
        columns={
            "Group Name": "FederationName",
            "Group UID": "FederationUid",
        },
        inplace=True,
    )

    return hard_federations, soft_federations


def build_bfr_historical_data(
    academies_historical: pd.DataFrame | None,
    bfr_sofa_historical: pd.DataFrame | None,
) -> pd.DataFrame | None:
    """
    Derive historical data from BFR SOFA data.

    `academies_historical` must have:

    - Trust UPIN
    - Company Registration Number

    `bfr_sofa_historical` must have:

    - Trust UPIN
    - EFALineNo (containing 430 and 999)
    - Y1P2
    - Y2P2

    The return value will be of the same form as `academies_historical`,
    with an additional colums:

    - "Trust Revenue reserve"
    - "Total pupils in trust"

    :param academies_historical: academy data from a previous year
    :param bfr_sofa_historical: BFR SOFA data from a previous year
    :return: updated, historical data
    """
    if academies_historical is not None:
        academies_historical["Trust Revenue reserve"] = 0.0
        academies_historical["Total pupils in trust"] = 0.0

        if bfr_sofa_historical is not None:
            academies_historical = academies_historical.drop(
                columns=["Trust Revenue reserve", "Total pupils in trust"],
            )

            academies_historical = academies_historical.merge(
                bfr_sofa_historical[bfr_sofa_historical["EFALineNo"] == 430].rename(
                    {"Y2P2": "Trust Revenue reserve"},
                    axis=1,
                )[["Trust UPIN", "Trust Revenue reserve"]],
                on="Trust UPIN",
                how="left",
            )
            academies_historical["Trust Revenue reserve"] *= 1_000
            academies_historical = academies_historical.merge(
                bfr_sofa_historical[bfr_sofa_historical["EFALineNo"] == 999].rename(
                    {"Y1P2": "Total pupils in trust"},
                    axis=1,
                )[["Trust UPIN", "Total pupils in trust"]],
                on="Trust UPIN",
                how="left",
            )

    return academies_historical


def build_bfr_data(
    current_year,
    bfr_sofa_data_path,
    bfr_3y_data_path,
    academies,
    academies_y1=None,
    academies_y2=None,
):
    bfr_sofa = pd.read_csv(
        bfr_sofa_data_path,
        encoding="unicode-escape",
        dtype=input_schemas.bfr_sofa_cols,
        usecols=input_schemas.bfr_sofa_cols.keys(),
    ).rename(columns={"TrustUPIN": "Trust UPIN", "Title": "Category"})
    bfr_sofa = bfr_sofa[
        bfr_sofa["EFALineNo"].isin(
            [298, 430, 335, 380, 211, 220, 199, 200, 205, 210, 999]
        )
    ]
    bfr_sofa.drop_duplicates(inplace=True)
    bfr_sofa[["Y1P1", "Y1P2", "Y2P1", "Y2P2"]] = bfr_sofa[
        ["Y1P1", "Y1P2", "Y2P1", "Y2P2"]
    ].apply(lambda x: x * 1000, axis=1)

    self_gen_income = (
        bfr_sofa[bfr_sofa["EFALineNo"].isin([211, 220])]
        .groupby(["Trust UPIN"])[["Y1P1", "Y1P2", "Y2P1", "Y2P2"]]
        .sum()
        .reset_index()
    )
    self_gen_income["Category"] = "Self-generated income"

    grant_funding = (
        bfr_sofa[bfr_sofa["EFALineNo"].isin([199, 200, 205, 210])]
        .groupby(["Trust UPIN"])[["Y1P1", "Y1P2", "Y2P1", "Y2P2"]]
        .sum()
        .reset_index()
    )
    grant_funding["Category"] = "Grant funding"

    bfr_sofa = pd.concat([bfr_sofa, self_gen_income, grant_funding]).drop_duplicates()

    bfr_3y = pd.read_csv(
        bfr_3y_data_path,
        encoding="unicode-escape",
        dtype=input_schemas.bfr_3y_cols,
        usecols=input_schemas.bfr_3y_cols.keys(),
    ).rename(columns={"TrustUPIN": "Trust UPIN"})

    bfr_3y[["Y2", "Y3", "Y4"]] = bfr_3y[["Y2", "Y3", "Y4"]].apply(
        lambda x: x * 1000, axis=1
    )

    bfr_3y["EFALineNo"].replace(
        {2980: 298, 4300: 430, 3800: 380, 9000: 999}, inplace=True
    )
    bfr_3y = bfr_3y[bfr_3y["EFALineNo"].isin([298, 430, 335, 380, 999])]
    bfr_3y.drop_duplicates(inplace=True)

    merged_bfr = bfr_sofa.merge(bfr_3y, how="left", on=("Trust UPIN", "EFALineNo"))

    bfr = (
        academies.groupby("Trust UPIN")
        .first()
        .reset_index()
        .merge(merged_bfr, on="Trust UPIN")
    )

    bfr["Category"].replace(
        {
            "Balance c/f to next period ": "Revenue reserve",
            "Pupil numbers (actual and estimated)": "Pupil numbers",
            "Total revenue expenditure": "Total expenditure",
            "Total revenue income": "Total income",
            "Total staff costs": "Staff costs",
        },
        inplace=True,
    )

    if academies_y2 is not None:
        bfr = bfr.merge(
            academies_y2[
                ["Trust UPIN", "Trust Revenue reserve", "Total pupils in trust"]
            ]
            .groupby("Trust UPIN")
            .first()
            .reset_index()
            .rename(
                columns={
                    "Trust Revenue reserve": "Y-2",
                    "Total pupils in trust": "Pupils Y-2",
                }
            ),
            how="left",
            on="Trust UPIN",
        )
    else:
        bfr["Y-2"] = 0.0
        bfr["Pupils Y-2"] = 0.0

    if academies_y1 is not None:
        bfr = bfr.merge(
            academies_y1[
                ["Trust UPIN", "Trust Revenue reserve", "Total pupils in trust"]
            ]
            .groupby("Trust UPIN")
            .first()
            .reset_index()
            .rename(
                columns={
                    "Trust Revenue reserve": "Y-1",
                    "Total pupils in trust": "Pupils Y-1",
                }
            ),
            how="left",
            on="Trust UPIN",
        )
    else:
        bfr["Y-1"] = 0.0
        bfr["Pupils Y-1"] = 0.0

    bfr["Y1"] = bfr["Y2P2"]
    bfr = bfr.drop_duplicates()
    bfr_metrics = BFR.calculate_metrics(bfr.reset_index())

    bfr_pupils = bfr[(bfr["Category"] == "Pupil numbers")][
        ["Trust UPIN", "Y2", "Y3", "Y4"]
    ]
    bfr = bfr[(bfr["Category"] == "Revenue reserve")]

    bfr_metrics = pd.concat([bfr_metrics, BFR.slope_analysis(bfr)])

    bfr_pupils[["Y2", "Y3", "Y4"]] = bfr_pupils[["Y2", "Y3", "Y4"]].apply(
        lambda x: x / 1000, axis=1
    )
    bfr_pupils.rename(
        columns={"Y2": "Pupils Y2", "Y3": "Pupils Y3", "Y4": "Pupils Y4"}, inplace=True
    )

    bfr_pupils = bfr_pupils.merge(
        academies[["Trust UPIN", "Total pupils in trust"]]
        .groupby("Trust UPIN")
        .first()
        .rename(columns={"Total pupils in trust": "Pupils Y1"}),
        how="left",
        on="Trust UPIN",
    )

    bfr = bfr.merge(bfr_pupils, how="left", on="Trust UPIN").drop(
        labels=["Y1P1", "Y1P2", "Y2P1", "Y2P2", "EFALineNo", "Trust Revenue reserve"],
        axis=1,
    )

    pupil_cols = [
        "Company Registration Number",
        "Category",
        "Pupils Y-2",
        "Pupils Y-1",
        "Pupils Y1",
        "Pupils Y2",
        "Pupils Y3",
        "Pupils Y4",
    ]

    bfr_pupils = (
        bfr[pupil_cols]
        .melt(
            id_vars=["Company Registration Number", "Category"],
            value_vars=[
                "Pupils Y-2",
                "Pupils Y-1",
                "Pupils Y1",
                "Pupils Y2",
                "Pupils Y3",
                "Pupils Y4",
            ],
            var_name="Year",
            value_name="Pupils",
        )
        .replace(
            {
                "Pupils Y-2": current_year - 2,
                "Pupils Y-1": current_year - 1,
                "Pupils Y1": current_year,
                "Pupils Y2": current_year + 1,
                "Pupils Y3": current_year + 2,
                "Pupils Y4": current_year + 3,
            }
        )
        .set_index("Company Registration Number")
        .sort_values(by=["Company Registration Number", "Year"])
    )

    cols = [
        "Company Registration Number",
        "Category",
        "Y-2",
        "Y-1",
        "Y1",
        "Y2",
        "Y3",
        "Y4",
    ]
    bfr_revenue_reserve = (
        bfr[cols]
        .melt(
            id_vars=["Company Registration Number", "Category"],
            value_vars=["Y-2", "Y-1", "Y1", "Y2", "Y3", "Y4"],
            var_name="Year",
            value_name="Value",
        )
        .replace(
            {
                "Y-2": current_year - 2,
                "Y-1": current_year - 1,
                "Y1": current_year,
                "Y2": current_year + 1,
                "Y3": current_year + 2,
                "Y4": current_year + 3,
            }
        )
        .set_index("Company Registration Number")
        .sort_values(by=["Company Registration Number", "Year"])
    )

    bfr = bfr_revenue_reserve.merge(
        bfr_pupils, how="left", on=("Company Registration Number", "Category", "Year")
    )

    return bfr, bfr_metrics


def update_custom_data(
    existing_data: pd.DataFrame,
    custom_data: dict,
    target_urn: int,
) -> pd.DataFrame:
    """
    Update existing financial data with custom data.

    This will overwrite financial information for a specific row with
    data provided; additionally, _all_ "central services" information
    (less the "Financial Position" as this is non-numeric) will be set
    to zero, again for that row only.

    Note: only a subset of the custom fields may be present in the
    inbound message; only a subset of mapped columns may be present in
    the existing data. Equally, the data will only be updated if the
    target is present in the existing data.

    :param existing_data: existing, pre-processed data
    :param custom_data: custom financial information
    :param target_urn: specific row to update
    :return: updated data
    """

    if target_urn not in existing_data.index:
        return existing_data

    custom_to_columns = {
        "administrativeSuppliesNonEducationalCosts": "Administrative supplies_Administrative supplies (non educational)",
        "cateringStaffCosts": "Catering staff and supplies_Catering staff",
        "cateringSuppliesCosts": "Catering staff and supplies_Catering supplies",
        "incomeCateringServices": "Income_Catering services",
        "examinationFeesCosts": "Educational supplies_Examination fees",
        "learningResourcesNonIctCosts": "Educational supplies_Learning resources (not ICT equipment)",
        "learningResourcesIctCosts": "Educational ICT_ICT learning resources",
        "administrativeClericalStaffCosts": "Non-educational support staff and services_Administrative and clerical staff",
        "auditorsCosts": "Non-educational support staff and services_Audit cost",
        "otherStaffCosts": "Non-educational support staff and services_Other staff",
        "professionalServicesNonCurriculumCosts": "Non-educational support staff and services_Professional services (non-curriculum)",
        "cleaningCaretakingCosts": "Premises staff and services_Cleaning and caretaking",
        "maintenancePremisesCosts": "Premises staff and services_Maintenance of premises",
        "otherOccupationCosts": "Premises staff and services_Other occupation costs",
        "premisesStaffCosts": "Premises staff and services_Premises staff",
        "agencySupplyTeachingStaffCosts": "Teaching and Teaching support staff_Agency supply teaching staff",
        "educationSupportStaffCosts": "Teaching and Teaching support staff_Education support staff",
        "educationalConsultancyCosts": "Teaching and Teaching support staff_Educational consultancy",
        "supplyTeachingStaffCosts": "Teaching and Teaching support staff_Supply teaching staff",
        "teachingStaffCosts": "Teaching and Teaching support staff_Teaching staff",
        "energyCosts": "Utilities_Energy",
        "waterSewerageCosts": "Utilities_Water and sewerage",
        "directRevenueFinancingCosts": "Other costs_Direct revenue financing",
        "groundsMaintenanceCosts": "Other costs_Grounds maintenance",
        "indirectEmployeeExpenses": "Other costs_Indirect employee expenses",
        "interestChargesLoanBank": "Other costs_Interest charges for loan and bank",
        "otherInsurancePremiumsCosts": "Other costs_Other insurance premiums",
        "privateFinanceInitiativeCharges": "Other costs_PFI charges",
        "rentRatesCosts": "Other costs_Rent and rates",
        "specialFacilitiesCosts": "Other costs_Special facilities",
        "staffDevelopmentTrainingCosts": "Other costs_Staff development and training",
        "staffRelatedInsuranceCosts": "Other costs_Staff-related insurance",
        "supplyTeacherInsurableCosts": "Other costs_Supply teacher insurance",
        "totalIncome": "Total Income",
        "totalExpenditure": "Total Expenditure",
        "revenueReserve": "Revenue reserve",
        "totalPupils": "Number of pupils",
        "percentFreeSchoolMeals": "Percentage Free school meals",
        "percentSpecialEducationNeeds": "Percentage SEN",
        "totalInternalFloorArea": "Total Internal Floor Area",
        "workforceFTE": "Total School Workforce (Full-Time Equivalent)",
        "teachersFTE": "Total Number of Teachers (Full-Time Equivalent)",
        "percentTeacherWithQualifiedStatus": "Teachers with Qualified Teacher Status (%) (Headcount)",
        "seniorLeadershipFTE": "Total Number of Teachers in the Leadership Group (Full-time Equivalent)",
        "teachingAssistantFTE": "Total Number of Teaching Assistants (Full-Time Equivalent)",
        "nonClassroomSupportStaffFTE": "NonClassroomSupportStaffFTE",
        "auxiliaryStaffFTE": "Total Number of Auxiliary Staff (Full-Time Equivalent)",
        "workforceHeadcount": "Total School Workforce (Headcount)",
    }

    custom_present = [
        custom
        for custom in custom_to_columns.keys()
        if custom in custom_data and custom_to_columns[custom] in existing_data.columns
    ]

    existing_columns = [custom_to_columns[custom] for custom in custom_present]
    custom_values = [custom_data[custom] for custom in custom_present]
    existing_data.loc[target_urn, existing_columns] = custom_values

    central_services_columns = [
        column
        for column in existing_data.columns
        if column.endswith("_CS") and column != "Financial Position_CS"
    ]
    central_services_values = [0.0] * len(central_services_columns)
    existing_data.loc[
        target_urn,
        central_services_columns,
    ] = central_services_values

    existing_data.loc[target_urn] = _post_process_custom(
        target_data=existing_data.loc[[target_urn]]
    ).loc[target_urn]

    return existing_data


def _post_process_custom(
    target_data: pd.DataFrame,
) -> pd.DataFrame:
    """
    Recalculate total and per-unit costs for custom data.

    Total and per-unit costs are set to zero before recalculating.

    Note: largely repeats some of the logic from
    :func:`build_maintained_school_data`; for this reason it accepts
    a :class:`DataFrame` but must only include the URN of interest.

    :param existing_data: existing, pre-processed data
    :return: updated data
    """
    zero_columns = [
        column
        for column in target_data.columns
        for category, _ in config.rag_category_settings.items()
        if column.startswith(category) and column.endswith(("_Per Unit", "_Total"))
    ]
    zero_column_indices = [target_data.columns.get_loc(c) for c in zero_columns]
    zero_column_values = [0.0] * len(zero_column_indices)
    target_data.iloc[0, zero_column_indices] = zero_column_values

    # TODO: `_Net Costs` need to be recalculated as per line 1152.
    catering_net_costs = target_data["Catering staff and supplies_Net Costs"].copy()
    target_data["Catering staff and supplies_Net Costs"] = 0.0

    for category, settings in config.rag_category_settings.items():
        basis_data = target_data[
            (
                "Number of pupils"
                if settings["type"] == "Pupil"
                else "Total Internal Floor Area"
            )
        ]
        target_data = mappings.map_cost_series(category, target_data, basis_data)

    target_data["Catering staff and supplies_Net Costs"] = catering_net_costs

    return target_data
