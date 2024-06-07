import datetime
import struct

import src.pipeline.input_schemas as input_schemas
import src.pipeline.mappings as mappings
import src.pipeline.config as config
import pandas as pd
import logging
import numpy as np

from warnings import simplefilter

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
    cdc["Building Age"] = cdc.groupby(by=["URN"])["Indicative Age"].mean()
    return cdc[
        ["Total Internal Floor Area", "Age Average Score", "Building Age"]
    ].drop_duplicates()


def prepare_census_data(workforce_census_path, pupil_census_path):
    school_workforce_census = pd.read_excel(
        workforce_census_path,
        header=5,
        index_col=input_schemas.workforce_census_index_col,
        usecols=input_schemas.workforce_census.keys(),
        dtype=input_schemas.workforce_census,
        na_values=["x", "u", "c", "z"],
        keep_default_na=True,
    ).drop_duplicates()

    school_pupil_census = pd.read_csv(
        pupil_census_path,
        encoding="utf8",
        index_col=input_schemas.pupil_census_index_col,
        usecols=lambda x: x in input_schemas.pupil_census.keys(),
        dtype=input_schemas.pupil_census,
        na_values=["x", "u", "c", "z"],
        keep_default_na=True
    ).drop_duplicates()

    if 'number_of_dual_subsidiary_registrations' in school_pupil_census.columns:
        school_pupil_census.rename(columns={'number_of_dual_subsidiary_registrations': "Pupil Dual Registrations"}, inplace=True)
    else:
        school_pupil_census["Pupil Dual Registrations"] = 0

    census = school_pupil_census.join(
        school_workforce_census,
        on="URN",
        how="inner",
        rsuffix="_pupil",
        lsuffix="_workforce",
    ).rename(
        columns={
            "headcount of pupils": "Number of pupils",
            "fte pupils": "Number of Pupils (FTE)",
            "Total Number of Non-Classroom-based School Support Staff, (Other school support staff plus Administrative staff plus Technicians and excluding Auxiliary staff (Full-Time Equivalent)": "NonClassroomSupportStaffFTE",
            "Total Number of Non Classroom-based School Support Staff, Excluding Auxiliary Staff (Headcount)": "NonClassroomSupportStaffHeadcount",
            "% of pupils known to be eligible for free school meals (Performa": "Percentage Free school meals",
            "% of pupils known to be eligible for and claiming free school me": "Percentage claiming Free school meals",
        }
    )

    census["Number of pupils"] = census["Number of pupils"] + census["Pupil Dual Registrations"]

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

    return sen[
        [
            "EHC plan",
            "SEN support",
            "Percentage SEN",
            "Percentage with EHC",
            "Percentage without EHC",
            "Percentage Primary Need SPLD",
            "Percentage Primary Need MLD",
            "Percentage Primary Need SLD",
            "Percentage Primary Need PMLD",
            "Percentage Primary Need SEMH",
            "Percentage Primary Need SLCN",
            "Percentage Primary Need HI",
            "Percentage Primary Need VI",
            "Percentage Primary Need MSI",
            "Percentage Primary Need PD",
            "Percentage Primary Need ASD",
            "Percentage Primary Need OTH",
        ]
    ]


def prepare_ks2_data(ks2_path):
    ks2 = pd.read_excel(
        ks2_path,
        usecols=input_schemas.ks2.keys(),
        index_col=input_schemas.ks2_index_col,
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

    return ks2[["Ks2Progress"]].dropna()


def prepare_ks4_data(ks4_path):
    ks4 = pd.read_excel(
        ks4_path,
        index_col=input_schemas.ks4_index_col,
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

    return ks4[["AverageAttainment", "Progress8Measure", "Progress8Banding"]].dropna()


def prepare_aar_data(aar_path):
    aar = pd.read_excel(
        aar_path,
        sheet_name="Academies",
        usecols=input_schemas.aar_academies.keys()
    )

    # removing pre-transition academies
    transitioned_academy_urns = aar["URN"][aar["URN"].duplicated(keep=False)].values
    mask = ~(
            aar["URN"].isin(transitioned_academy_urns)
            & aar["Date joined or opened if in period"].isna()
    )
    aar = aar[mask]

    central_services_financial = pd.read_excel(
        aar_path,
        sheet_name="CentralServices",
        usecols=input_schemas.aar_central_services.keys()
    )

    aar.replace(to_replace={"DNS": np.nan, "n/a": np.nan}, inplace=True)
    aar = aar.astype(input_schemas.aar_academies)
    aar.drop(columns=['URN'], inplace=True)
    aar.rename(
        columns={
                    "In year balance": "Academy Balance",
                    "PFI": "PFI School",
                    "Lead UPIN": "Trust UPIN",
                }
                | config.cost_category_map["academies"],
        inplace=True,
    )

    central_services_financial.replace(to_replace={"DNS": np.nan, "n/a": np.nan}, inplace=True)
    central_services_financial = central_services_financial.astype(input_schemas.aar_central_services)
    central_services_financial.rename(
        columns={
            "In Year Balance": "Central Services Balance",
            "Lead UPIN": "Trust UPIN",
        },
        inplace=True,
    )
    trust_income = (
        aar[["Trust UPIN", *config.income_category_map["academies"]]]
        .groupby("Trust UPIN")
        .sum()
        .add_prefix("Trust_")
    )

    trust_balance = (
        aar[["Trust UPIN", "Academy Balance"]]
        .groupby("Trust UPIN")
        .sum()
        .rename(columns={"Academy Balance": "Trust Balance"})
    )

    central_services_balance = (
        central_services_financial[["Trust UPIN", "Central Services Balance"]]
        .groupby("Trust UPIN")
        .sum()
    )

    aar = (
        aar.merge(trust_balance, on="Trust UPIN", how="left")
        .merge(trust_income, on="Trust UPIN", how="left")
        .merge(central_services_balance, on="Trust UPIN", how="left")
    )

    aar["Central Services Financial Position"] = aar["Central Services Balance"].map(
        mappings.map_is_surplus_deficit
    )
    aar["Academy Financial Position"] = aar["Academy Balance"].map(
        mappings.map_is_surplus_deficit
    )
    aar["Trust Financial Position"] = aar["Trust Balance"].map(
        mappings.map_is_surplus_deficit
    )

    aar["PFI School"] = aar["PFI School"].map(mappings.map_is_pfi_school)

    aar["Is PFI"] = aar["PFI School"].map(lambda x: x == "PFI school")

    aar["London Weighting"] = aar["London Weighting"].fillna("Neither")

    return aar.set_index("Academy UPIN")


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

    return schools[
        schools["CloseDate"].isna()
        & ((schools["Rank"] == 1) | (schools["Rank"].isna()))
        ].drop(columns=["LinkURN", "LinkName", "LinkType", "LinkEstablishedDate", "Rank"])


def build_cost_series(category_name, df, basis):
    basis_data = df[
        "Number of pupils" if basis == "Pupil" else "Total Internal Floor Area"
    ]

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
        df[sub_category + "_Per Unit"] = df[sub_category].fillna(0) / basis_data
        df[sub_category + "_Per Unit"].replace(
            [np.inf, -np.inf, np.nan], 0, inplace=True
        )

    return df


def build_academy_data(
        academy_data_path, links_data_path, year, schools, census, sen, cdc, aar, ks2, ks4
):
    accounts_return_period_start_date = datetime.date(year - 1, 9, 10)
    academy_year_start_date = datetime.date(year - 1, 9, 1)
    academy_year_end_date = datetime.date(year, 8, 30)

    academies_list = pd.read_csv(
        academy_data_path,
        encoding="utf8",
        index_col=input_schemas.academy_master_list_index_col,
        dtype=input_schemas.academy_master_list,
        usecols=input_schemas.academy_master_list.keys(),
    ).rename(columns={"UKPRN": "Academy UKPRN"})

    group_links = pd.read_csv(
        links_data_path,
        encoding="cp1252",
        index_col=input_schemas.groups_index_col,
        usecols=input_schemas.groups.keys(),
        dtype=input_schemas.groups,
    )[["Group Type", "Group UID"]]

    group_links = group_links[
        group_links["Group Type"].isin(
            ["Single-academy trust", "Multi-academy trust", "Trust"]
        )
    ]

    # remove transitioned schools from academies_list
    mask = (
            academies_list.index.duplicated(keep=False) & ~academies_list["Valid to"].isna()
    )
    academies_list = academies_list[~mask]

    academies_base = academies_list.merge(
        schools.reset_index(), left_index=True, right_on="LA Establishment Number"
    )

    academies = (
        academies_base.merge(census, on="URN", how="left")
        .merge(sen, on="URN", how="left")
        .merge(cdc, on="URN", how="left")
        .merge(aar, left_on="Academy UPIN", right_index=True, how="left")
        .merge(ks2, on="URN", how="left")
        .merge(ks4, on="URN", how="left")
        .merge(group_links, on="URN", how="inner")
    )

    # TODO: Check what to do here as CDC data doesn't seem to contain all of the academy data URN=148853 is an example
    academies["Total Internal Floor Area"] = academies[
        "Total Internal Floor Area"
    ].fillna(academies["Total Internal Floor Area"].median())

    academies["Overall Phase"] = academies.apply(
        lambda df: mappings.map_academy_phase_type(
            df["TypeOfEstablishment (code)"], df["Type of Provision - Phase"]
        ),
        axis=1,
    )

    academies["Status"] = academies.apply(
        lambda df: mappings.map_academy_status(
            pd.to_datetime(df["Date joined or opened if in period"]),
            pd.to_datetime(df["Date left or closed if in period"]),
            pd.to_datetime(df["Valid to"]),
            pd.to_datetime(df["OpenDate"]),
            pd.to_datetime(df["CloseDate"]),
            pd.to_datetime(accounts_return_period_start_date),
            pd.to_datetime(academy_year_start_date),
            pd.to_datetime(academy_year_end_date),
        ),
        axis=1,
    )

    academies["SchoolPhaseType"] = academies.apply(
        lambda df: mappings.map_school_phase_type(
            df["TypeOfEstablishment (code)"], df["Type of Provision - Phase"]
        ),
        axis=1,
    )

    academies["Finance Type"] = "Academy"

    academies.rename(
        columns={
            "UKPRN_x": "UKPRN",
            "LA (code)": "LA Code",
            "LA (name)": "LA Name",
            "Academy Trust Name": "Trust Name",
            "Academy UKPRN": "Trust UKPRN",
        },
        inplace=True,
    )

    academies["OfstedLastInsp"] = pd.to_datetime(
        academies["OfstedLastInsp"], dayfirst=True
    )
    academies["London Weighting"] = academies["London Weighting"].fillna("Neither")
    academies["Email"] = ""
    academies["HeadEmail"] = ""
    academies["Is PFI"] = academies["Is PFI"].astype(bool).fillna(False)
    academies["CFO Email"] = None
    academies["CFO Name"] = None

    for category in config.rag_category_settings.keys():
        academies = build_cost_series(
            category, academies, config.rag_category_settings[category]["type"]
        )

    return academies.set_index("URN")


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
    maintained_schools_year_start_date = datetime.date(year - 1, 4, 1)
    maintained_schools_year_end_date = datetime.date(year, 3, 31)

    maintained_schools_list = pd.read_csv(
        maintained_schools_data_path,
        encoding="utf8",
        index_col=input_schemas.maintained_schools_master_list_index_col,
        usecols=input_schemas.maintained_schools_master_list.keys(),
        dtype=input_schemas.maintained_schools_master_list,
    )

    maintained_schools = maintained_schools_list.merge(
        schools.reset_index(), left_index=True, right_on="URN"
    )

    maintained_schools = (
        maintained_schools.merge(sen, on="URN", how="left")
        .merge(census, on="URN", how="left")
        .merge(cdc, on="URN", how="left")
        .merge(ks2, on="URN", how="left")
        .merge(ks4, on="URN", how="left")
    )

    maintained_schools["PFI"] = maintained_schools["PFI"].map(
        lambda x: "PFI school" if x == "Y" else "Non-PFI school"
    )

    maintained_schools["Is PFI"] = maintained_schools["PFI"].map(
        lambda x: x == "PFI school"
    )

    maintained_schools["Status"] = maintained_schools.apply(
        lambda df: mappings.map_maintained_school_status(
            df["OpenDate"],
            df["CloseDate"],
            df["Period covered by return (months)"],
            pd.to_datetime(maintained_schools_year_start_date),
            pd.to_datetime(maintained_schools_year_end_date),
        ),
        axis=1,
    )

    maintained_schools["School Balance"] = (
            maintained_schools["Total Income   I01 to I18"]
            - maintained_schools["Total Expenditure  E01 to E32"]
    )

    maintained_schools["School Financial Position"] = maintained_schools[
        "School Balance"
    ].map(mappings.map_is_surplus_deficit)

    maintained_schools["SchoolPhaseType"] = maintained_schools.apply(
        lambda df: mappings.map_school_phase_type(
            df["TypeOfEstablishment (code)"], df["Overall Phase"]
        ),
        axis=1,
    )

    maintained_schools["Partial Years Present"] = maintained_schools[
        "Period covered by return (months)"
    ].map(lambda x: x != 12)

    maintained_schools["Did Not Submit"] = maintained_schools[
        "Did Not Supply flag"
    ].map(lambda x: x == 1)

    maintained_schools["Finance Type"] = "Maintained"

    maintained_schools["Email"] = ""
    maintained_schools["HeadEmail"] = ""
    maintained_schools["Trust Name"] = None
    maintained_schools["OfstedLastInsp"] = pd.to_datetime(
        maintained_schools["OfstedLastInsp"], dayfirst=True
    )
    maintained_schools["London Weighting"] = maintained_schools[
        "London Weighting"
    ].fillna("Neither")

    maintained_schools.rename(
        columns=config.cost_category_map["maintained_schools"],
        inplace=True,
    )

    for category in config.rag_category_settings.keys():
        maintained_schools = build_cost_series(
            category, maintained_schools, config.rag_category_settings[category]["type"]
        )

    maintained_schools = maintained_schools[maintained_schools.index.notnull()]

    (hard_federations, soft_federations) = build_federations_data(
        links_data_path, maintained_schools
    )

    # Applying federation mappings
    list_of_laestabs = maintained_schools["LAEstab"][
        maintained_schools["Lead school in federation"] != "0"
        ]
    list_of_urns = maintained_schools.index[
        maintained_schools["Lead school in federation"] != "0"
        ]
    lae_ukprn = dict(zip(list_of_laestabs, list_of_urns))

    maintained_schools["Federation Lead School URN"] = maintained_schools[
        "Lead school in federation"
    ].map(lae_ukprn)
    maintained_schools = pd.merge(
        maintained_schools,
        hard_federations[["FederationName"]],
        how="left",
        left_index=True,
        right_index=True,
    )
    maintained_schools.rename(
        columns={"FederationName": "Federation Name"}, inplace=True
    )
    maintained_schools = maintained_schools[~maintained_schools.index.duplicated()]

    return maintained_schools.set_index("URN")


def build_federations_data(links_data_path, maintained_schools):
    group_links = pd.read_csv(
        links_data_path,
        encoding="unicode-escape",
        index_col=input_schemas.groups_index_col,
        usecols=input_schemas.groups.keys(),
        dtype=input_schemas.groups,
    )

    federations = maintained_schools[["URN", "LAEstab"]][
        maintained_schools["Federation"] == "Lead school"
        ].copy()

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


def _calculate_metrics(bfr):
    bfr_metrics = bfr[['TrustUPIN']].copy().set_index('TrustUPIN')
    bfr_metrics['Revenue reserve as percentage of income'] = \
        round(bfr[bfr['Title'] == 'Revenue reserves'].set_index('TrustUPIN')[['Y1']]
              / bfr[bfr['Title'] == 'Total income'].set_index('TrustUPIN')[['Y1']] * 100, 1)
    bfr_metrics['Staff costs as percentage of income'] = \
        round(bfr[bfr['Title'] == 'Staff costs'].set_index('TrustUPIN')[['Y1']]
              / bfr[bfr['Title'] == 'Total income'].set_index('TrustUPIN')[['Y1']] * 100, 1)
    bfr_metrics['Expenditure as percentage of income'] = \
        round(bfr[bfr['Title'] == 'Total expenditure'].set_index('TrustUPIN')[['Y1']]
              / bfr[bfr['Title'] == 'Total income'].set_index('TrustUPIN')[['Y1']] * 100, 1)
    bfr_metrics['percent self-generated income'] = \
        round(bfr[bfr['Title'] == 'Self-generated income'].set_index('TrustUPIN')[['Y1']] /
              (bfr[bfr['Title'] == 'Self-generated income'].set_index('TrustUPIN')[['Y1']] +
               bfr[bfr['Title'] == 'Grant funding'].set_index('TrustUPIN')[['Y1']]) * 100, 0)
    bfr_metrics['percent grant funding'] = 100 - bfr_metrics['percent self-generated income']
    return bfr_metrics


def _calculate_slopes(matrix):
    x = np.array([1, 2, 3, 4, 5, 6])
    x_bar = 3.5
    x_x_bar = x - x_bar
    y_bar = np.mean(matrix, axis=1)
    y_y_bar = matrix - np.vstack(y_bar)
    slope_array = np.sum(x_x_bar * y_y_bar, axis=1) / np.sum(x_x_bar ** 2)
    return slope_array


def _assign_slope_flag(df):
    percentile_10 = np.nanpercentile(df["slope"].values, 10)
    percentile_90 = np.nanpercentile(df["slope"].values, 90)
    df["slope_flag"] = 0
    df.loc[df["slope"] < percentile_10, "slope_flag"] = -1
    df.loc[df["slope"] > percentile_90, "slope_flag"] = 1
    return df


def _slope_analysis(bfr_dataframe, academies_y2, academies_y1):
    year_columns = ['Y-2', 'Y-1', 'Y1', 'Y2', 'Y3', 'Y4']
    bfr_revenue_reserves = bfr_dataframe[bfr_dataframe['Title'] == 'Revenue reserves']
    bfr_pupil_numbers = bfr_dataframe[bfr_dataframe['Title'] == 'Pupil numbers']

    # TODO need to add in historic data to this, filling in fake values for now
    bfr_revenue_reserves = pd.merge(
        bfr_revenue_reserves,
        academies_y2[['Trust UPIN', 'Trust Balance']].rename(columns={
            'Trust UPIN': 'TrustUPIN',
            'Trust Balance': 'Y-2'
        }).drop_duplicates(), how='left', on='TrustUPIN')

    bfr_revenue_reserves = pd.merge(
        bfr_revenue_reserves,
        academies_y1[['Trust UPIN', 'Trust Balance']].rename(columns={
            'Trust UPIN': 'TrustUPIN',
            'Trust Balance': 'Y-1'
        }).drop_duplicates(), how='left', on='TrustUPIN')

    bfr_pupil_numbers = pd.merge(
        bfr_pupil_numbers,
        academies_y2[['Trust UPIN', 'Number of pupils']].rename(columns={
            'Trust UPIN': 'TrustUPIN',
            'Number of pupils': 'Y-2'
        }).groupby('TrustUPIN').agg(sum), how='left', on='TrustUPIN')

    bfr_pupil_numbers = pd.merge(
        bfr_pupil_numbers,
        academies_y2[['Trust UPIN', 'Number of pupils']].rename(columns={
            'Trust UPIN': 'TrustUPIN',
            'Number of pupils': 'Y-1'
        }).groupby('TrustUPIN').agg(sum), how='left', on='TrustUPIN')

    # convert to matrix
    matrix_revenue_reserves = bfr_revenue_reserves[year_columns].values.astype(float)
    matrix_pupil_numbers = bfr_pupil_numbers[year_columns].values.astype(float)

    matrix_revenue_reserves_per_pupil = matrix_revenue_reserves / matrix_pupil_numbers

    # determine associated slopes
    bfr_revenue_reserves["slope"] = _calculate_slopes(matrix_revenue_reserves)

    bfr_revenue_reserves_per_pupil = bfr_revenue_reserves[
        ["CreatedBy", "Category", "Title", "EFALineNo"]
    ].copy()
    bfr_revenue_reserves_per_pupil["slope"] = _calculate_slopes(
        matrix_revenue_reserves_per_pupil
    )
    for i in range(len(year_columns)):
        bfr_revenue_reserves_per_pupil[year_columns[i]] = (
            matrix_revenue_reserves_per_pupil.T[i]
        )

    # flag top 10% and bottom 90% percent of slopes with -1 and 1 respectively
    bfr_revenue_reserves = _assign_slope_flag(bfr_revenue_reserves)
    bfr_revenue_reserves_per_pupil = _assign_slope_flag(bfr_revenue_reserves_per_pupil)

    return bfr_revenue_reserves, bfr_revenue_reserves_per_pupil


def _volatility_analysis(bfr):
    bfr["volatility"] = (bfr["Trust Balance"] - bfr["Y1P2"]) / abs(bfr["Trust Balance"])

    volatility_conditions = [
        (bfr["volatility"] <= -0.05),
        (bfr["volatility"] <= 0.05),
        (bfr["volatility"] <= 0.1),
        (bfr["volatility"] > 0.1),
    ]
    volatility_messages = [
        "AR below forecast",
        "stable forecast",
        "AR above forecast",
        "AR significantly above forecast",
    ]

    bfr["volatility_status"] = np.select(
        volatility_conditions, volatility_messages, default=""
    )
    return bfr


def build_bfr_data(bfr_sofa_data_path, bfr_3y_data_path, academies_y2, academies_y1, academies):
    bfr_sofa = pd.read_csv(
        bfr_sofa_data_path,
        encoding="unicode-escape",
        dtype=input_schemas.bfr_sofa_cols,
        usecols=input_schemas.bfr_sofa_cols.keys(),
    )

    bfr_3y = pd.read_csv(
        bfr_3y_data_path,
        encoding="unicode-escape",
        dtype=input_schemas.bfr_3y_cols,
        usecols=input_schemas.bfr_3y_cols.keys(),
    )

    # remove unused metrics
    bfr_sofa = bfr_sofa[bfr_sofa['EFALineNo'].isin([298, 430, 335, 380, 211, 220, 199, 200, 205, 210, 999])]

    self_gen_income = bfr_sofa[
        bfr_sofa['EFALineNo'].isin([211, 220])
    ].groupby('TrustUPIN')[['Y1P1', 'Y1P2', 'Y2P1', 'Y2P2']].sum().reset_index()
    self_gen_income['Title'] = 'Self-generated income'

    grant_funding = bfr_sofa[
        bfr_sofa['EFALineNo'].isin([199, 200, 205, 210])
    ].groupby('TrustUPIN')[['Y1P1', 'Y1P2', 'Y2P1', 'Y2P2']].sum().reset_index()
    grant_funding['Title'] = 'Grant funding'

    bfr_sofa = bfr_sofa[~bfr_sofa['EFALineNo'].isin([211, 220, 199, 200, 205, 210])]
    bfr_sofa = pd.concat([bfr_sofa, self_gen_income, grant_funding])
    bfr_sofa['Title'].replace({
        'Balance c/f to next period ': 'Revenue reserves',
        'Pupil numbers (actual and estimated)': 'Pupil numbers',
        'Total revenue expenditure': 'Total expenditure',
        'Total revenue income': 'Total income', 'Total staff costs': 'Staff costs'
    }, inplace=True)
    bfr_sofa['Y1'] = bfr_sofa['Y1P1'] + bfr_sofa['Y1P2']
    bfr_sofa.drop_duplicates(inplace=True)

    bfr_3y['EFALineNo'].replace({2980: 298, 4300: 430, 3800: 380, 9000: 999}, inplace=True)
    bfr_3y = bfr_3y[bfr_3y['EFALineNo'].isin([298, 430, 335, 380, 999])]
    bfr_3y.drop_duplicates(inplace=True)

    bfr = pd.merge(bfr_sofa, bfr_3y, how='left', on=('TrustUPIN', 'EFALineNo'))

    # get trust metrics
    bfr_metrics = _calculate_metrics(bfr)
    # Slope analysis
    bfr_revenue_reserves, bfr_revenue_reserves_per_pupil = _slope_analysis(bfr, academies_y2, academies_y1)

    # volatility analysis
    bfr = pd.merge(bfr, academies[['Trust UPIN', 'Trust Balance']].rename(
        columns={'Trust UPIN': 'TrustUPIN'}), how='left', on='TrustUPIN')
    bfr = _volatility_analysis(bfr)

    bfr_metrics.drop_duplicates(inplace=True)

    use_columns = ["Y-2", "Y-1", "Y1", "Y2", "Y3", "slope", "slope_flag"]

    bfr_revenue_reserves.drop_duplicates(inplace=True)
    bfr_revenue_reserves = bfr_revenue_reserves[use_columns]
    bfr_revenue_reserves.rename(columns={
        "Y-2": "revenue_reserves_year_-2",
        "Y-1": "revenue_reserves_year_-1",
        "Y1": "revenue_reserves_year_0",
        "Y2": "revenue_reserves_year_1",
        "Y3": "revenue_reserves_year_2",
        "slope": "revenue_reserves_slope",
        "slope_flag": "revenue_reserves_slope_flag"}, inplace=True)

    bfr_revenue_reserves_per_pupil.drop_duplicates(inplace=True)
    bfr_revenue_reserves_per_pupil = bfr_revenue_reserves_per_pupil[use_columns]
    bfr_revenue_reserves_per_pupil.rename(columns={
        "Y-2": "revenue_reserves_year_per_pupil_-2",
        "Y-1": "revenue_reserves_year_per_pupil_-1",
        "Y1": "revenue_reserves_year_per_pupil_0",
        "Y2": "revenue_reserves_year_per_pupil_1",
        "Y3": "revenue_reserves_year_per_pupil_2",
        "slope": "revenue_reserves_year_per_pupil_slope",
        "slope_flag": "revenue_reserves_year_per_pupil_slope_flag"}, inplace=True)

    bfr_metrics = pd.merge(bfr_metrics, bfr_revenue_reserves, left_index=True, right_index=True)
    bfr_metrics = pd.merge(bfr_metrics, bfr_revenue_reserves_per_pupil, left_index=True, right_index=True)
    return bfr_metrics, bfr
