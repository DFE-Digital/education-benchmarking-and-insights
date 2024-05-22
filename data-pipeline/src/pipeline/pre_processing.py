import datetime

import numpy as np

import src.pipeline.input_schemas as input_schemas
import src.pipeline.mappings as mappings
import src.pipeline.config as config
import pandas as pd
import logging

from warnings import simplefilter
simplefilter(action="ignore", category=pd.errors.PerformanceWarning)


logger = logging.getLogger("fbit-data-pipeline:pre-processing")
logger.setLevel(logging.INFO)


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

    return cdc[["Total Internal Floor Area", "Age Average Score"]].drop_duplicates()


def prepare_census_data(workforce_census_path, pupil_census_path):
    school_workforce_census = pd.read_excel(
        workforce_census_path,
        header=5,
        index_col=input_schemas.workforce_census_index_col,
        usecols=input_schemas.workforce_census.keys(),
        dtype=input_schemas.workforce_census,
        na_values=["x", "u", "c"],
        keep_default_na=True,
    ).drop_duplicates()

    school_pupil_census = pd.read_csv(
        pupil_census_path,
        encoding="utf8",
        index_col=input_schemas.pupil_census_index_col,
        usecols=input_schemas.pupil_census.keys(),
        dtype=input_schemas.pupil_census,
    ).drop_duplicates()

    census = school_pupil_census.join(
        school_workforce_census,
        on="URN",
        how="inner",
        rsuffix="_pupil",
        lsuffix="_workforce",
    )

    census.rename(
        columns={
            # TODO: Are the top to mappings here seem to be named badly
            "Total Number of Non-Classroom-based School Support Staff, (Other school support staff plus Administrative staff plus Technicians and excluding Auxiliary staff (Full-Time Equivalent)": "FullTimeOther",
            "Total Number of Non Classroom-based School Support Staff, Excluding Auxiliary Staff (Headcount)": "FullTimeOtherHeadCount",
            "% of pupils known to be eligible for free school meals (Performa": "Percentage Free school meals",
            "% of pupils known to be eligible for and claiming free school me": "Percentage claiming Free school meals",
        },
        inplace=True,
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
    sen["Percentage SEN"] = ((sen["EHC plan"] / sen["Total pupils"]) * 100.0).fillna(0)
    sen["Primary Need SPLD"] = sen["EHC_Primary_need_spld"] + sen["SUP_Primary_need_spld"]
    sen["Primary Need MLD"] = sen["EHC_Primary_need_mld"] + sen["SUP_Primary_need_mld"]
    sen["Primary Need SLD"] = sen["EHC_Primary_need_sld"] + sen["SUP_Primary_need_sld"]
    sen["Primary Need PMLD"] = sen["EHC_Primary_need_pmld"] + sen["SUP_Primary_need_pmld"]
    sen["Primary Need SEMH"] = sen["EHC_Primary_need_semh"] + sen["SUP_Primary_need_semh"]
    sen["Primary Need SLCN"] = sen["EHC_Primary_need_slcn"] + sen["SUP_Primary_need_slcn"]
    sen["Primary Need HI"] = sen["EHC_Primary_need_hi"] + sen["SUP_Primary_need_hi"]
    sen["Primary Need VI"] = sen["EHC_Primary_need_vi"] + sen["SUP_Primary_need_vi"]
    sen["Primary Need MSI"] = sen["EHC_Primary_need_msi"] + sen["SUP_Primary_need_msi"]
    sen["Primary Need PD"] = sen["EHC_Primary_need_pd"] + sen["SUP_Primary_need_pd"]
    sen["Primary Need ASD"] = sen["EHC_Primary_need_asd"] + sen["SUP_Primary_need_asd"]
    sen["Primary Need OTH"] = sen["EHC_Primary_need_oth"] + sen["SUP_Primary_need_oth"]

    sen["Percentage Primary Need SPLD"] = ((sen["Primary Need SPLD"] / sen["Total pupils"]) * 100.0).fillna(0)
    sen["Percentage Primary Need MLD"] = ((sen["Primary Need MLD"] / sen["Total pupils"]) * 100.0).fillna(0)
    sen["Percentage Primary Need SLD"] = ((sen["Primary Need SLD"] / sen["Total pupils"]) * 100.0).fillna(0)
    sen["Percentage Primary Need PMLD"] = ((sen["Primary Need PMLD"] / sen["Total pupils"]) * 100.0).fillna(0)
    sen["Percentage Primary Need SEMH"] = ((sen["Primary Need SEMH"] / sen["Total pupils"]) * 100.0).fillna(0)
    sen["Percentage Primary Need SLCN"] = ((sen["Primary Need SLCN"] / sen["Total pupils"]) * 100.0).fillna(0)
    sen["Percentage Primary Need HI"] = ((sen["Primary Need HI"] / sen["Total pupils"]) * 100.0).fillna(0)
    sen["Percentage Primary Need VI"] = ((sen["Primary Need VI"] / sen["Total pupils"]) * 100.0).fillna(0)
    sen["Percentage Primary Need MSI"] = ((sen["Primary Need MSI"] / sen["Total pupils"]) * 100.0).fillna(0)
    sen["Percentage Primary Need PD"] = ((sen["Primary Need PD"] / sen["Total pupils"]) * 100.0).fillna(0)
    sen["Percentage Primary Need ASD"] = ((sen["Primary Need ASD"] / sen["Total pupils"]) * 100.0).fillna(0)
    sen["Percentage Primary Need OTH"] = ((sen["Primary Need OTH"] / sen["Total pupils"]) * 100.0).fillna(0)

    return sen[
        [
            "Total pupils",
            "EHC plan",
            "Percentage SEN",
            "Primary Need SPLD",
            "Primary Need MLD",
            "Primary Need SLD",
            "Primary Need PMLD",
            "Primary Need SEMH",
            "Primary Need SLCN",
            "Primary Need HI",
            "Primary Need VI",
            "Primary Need MSI",
            "Primary Need PD",
            "Primary Need ASD",
            "Primary Need OTH",
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
            "Percentage Primary Need OTH"
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
    )

    ks4.rename(
        columns={
            "ATT8SCR": "AverageAttainment",
            "P8MEA": "Progress8Measure",
            "P8_BANDING": "Progress8Banding",
        },
        inplace=True,
    )

    return ks4[["AverageAttainment", "Progress8Measure", "Progress8Banding"]].dropna()


def build_cost_series(category_name, df, basis):
    basis_data = df[
        "Number of pupils" if basis == "Pupil" else "Total Internal Floor Area"
    ]

    # Create total column
    df[category_name + "_Total"] = df[df.columns[pd.Series(df.columns).str.startswith(category_name)]].fillna(0).sum(axis=1)

    sub_categories = df.columns[
        df.columns.str.startswith(category_name)
    ].values.tolist()

    for sub_category in sub_categories:
        df[sub_category + "_Per Unit"] = df[sub_category].fillna(0) / basis_data

    return df


def prepare_aar_data(aar_path):
    aar = pd.read_excel(
        aar_path,
        sheet_name="Academies",
        usecols=input_schemas.aar_academies.keys(),
        dtype=input_schemas.aar_academies,
    )

    # removing pre-transition academies
    transitioned_academy_urns = aar['URN'][aar['URN'].duplicated()].values
    mask = ~(aar['URN'].isin(transitioned_academy_urns) & aar['Date joined or opened if in period'].isna())
    aar = aar[mask]
    aar.drop(columns=['URN'], inplace=True)

    central_services_financial = pd.read_excel(
        aar_path,
        sheet_name="CentralServices",
        usecols=input_schemas.aar_central_services.keys(),
        dtype=input_schemas.aar_central_services,
    )

    aar.rename(
        columns={
                    "In year balance": "Academy Balance",
                    "PFI": "PFI School",
                    "Lead UPIN": "Trust UPIN",
                } | config.cost_category_map["academies"],
        inplace=True,
    )

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
        aar
        .merge(trust_balance, on="Trust UPIN", how="left")
        .merge(trust_income, on="Trust UPIN", how="left")
        .merge(central_services_balance, on="Trust UPIN", how="left")
    )

    aar["Central Services Financial Position"] = aar["Central Services Balance"].map(mappings.map_is_surplus_deficit)
    aar["Academy Financial Position"] = aar["Academy Balance"].map(mappings.map_is_surplus_deficit)
    aar["Trust Financial Position"] = aar["Trust Balance"].map(mappings.map_is_surplus_deficit)

    aar["PFI School"] = aar["PFI School"].map(mappings.map_is_pfi_school)
    
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
            gias["LA (code)"] + "-" + gias["EstablishmentNumber"].astype("string")
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
    gias["NurseryProvision (name)"] = gias["NurseryProvision (name)"].fillna("")
    gias["OfficialSixthForm (name)"] = (
        gias["OfficialSixthForm (name)"].fillna("").map(mappings.map_sixth_form)
    )
    gias["AdmissionsPolicy (name)"] = (
        gias["AdmissionsPolicy (name)"].fillna("").map(mappings.map_admission_policy)
    )
    gias["HeadName"] = (
            gias["HeadTitle (name)"]
            + " "
            + gias["HeadFirstName"]
            + " "
            + gias["HeadLastName"]
    )

    # In the following cell, we find all the predecessor and merged links.
    # The links are then Ranked by URN and order by 'Link Established Date'.
    # The linked GAIS data in then joined to the base GIAS data.
    # This creates the overall school data set.
    # This dataset is then filtered for schools that are open (CloseDate is null) and the schools with
    # nested links that are Ranked 1.
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


def build_academy_data(
        academy_data_path, year, schools, census, sen, cdc, aar, ks2, ks4
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

    academies_base = academies_list.merge(
        schools.reset_index(), left_index=True, right_on="LA Establishment Number"
    ).set_index("URN")

    academies = (
        academies_base.merge(census, on="URN", how="left")
        .merge(sen, on="URN", how="left")
        .merge(cdc, on="URN", how="left")
        .merge(aar, left_on="Academy UPIN", right_index=True, how="left")
        .merge(ks2, on="URN", how="left")
        .merge(ks4, on="URN", how="left")
    )

    # TODO: Check what to do here as CDC data doesn't seem to contain all of the academy data URN=148853 is an example
    academies["Total Internal Floor Area"] = academies["Total Internal Floor Area"].fillna(
        academies["Total Internal Floor Area"].median())

    academies["Type of Provision - Phase"] = academies.apply(
        lambda df: mappings.map_academy_phase_type(
            df["TypeOfEstablishment (code)"], df["Type of Provision - Phase"]
        ),
        axis=1,
    )

    # Bizarre I shouldn't need this as this is coming from the original GIAS dataset, but I seem to have to do this twice.
    academies["NurseryProvision (name)"] = academies["NurseryProvision (name)"].fillna(
        ""
    )
    academies["NurseryProvision (name)"] = academies.apply(
        lambda df: mappings.map_nursery(
            df["NurseryProvision (name)"], df["Type of Provision - Phase"]
        ),
        axis=1,
    )

    academies["Status"] = academies.apply(
        lambda df: mappings.map_academy_status(
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

    academies['Finance Type'] = "Academy"

    academies.rename(
        columns={
            "UKPRN_x": "UKPRN",
            "LA (code)": "LA Code",
            "LA (name)": "LA Name",
            "Number of Pupils": "Number of pupils",
            "Academy Trust Name":"Trust Name",
            "Academy UKPRN":"Trust UKPRN",
        },
        inplace=True,
    )

    for category in config.rag_category_settings.keys():
        academies = build_cost_series(category, academies, config.rag_category_settings[category]["type"])

    academies.set_index("UKPRN", inplace=True)
    return academies


def build_maintained_school_data(
        maintained_schools_data_path, year, schools, census, sen, cdc, ks2, ks4
):
    maintained_schools_year_start_date = datetime.date(year, 4, 1)
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
        axis=1)

    maintained_schools["Partial Years Present"] = maintained_schools[
        "Period covered by return (months)"
    ].map(lambda x: x != 12)

    maintained_schools["Did Not Submit"] = maintained_schools[
        "Did Not Supply flag"
    ].map(lambda x: x == 1)

    maintained_schools['Finance Type'] = "Maintained School"

    maintained_schools.rename(
        columns={
                    "No Pupils": "Number of pupils",
                } | config.cost_category_map["maintained_schools"],
        inplace=True,
    )

    for category in config.rag_category_settings.keys():
        maintained_schools = build_cost_series(category, maintained_schools,
                                               config.rag_category_settings[category]["type"])

    return maintained_schools.reset_index().set_index("UKPRN")


def build_federations_data(links_data_path, maintained_schools):
    group_links = pd.read_csv(
        links_data_path,
        encoding="unicode-escape",
        index_col=input_schemas.groups_index_col,
        usecols=input_schemas.groups.keys(),
        dtype=input_schemas.groups,
    )

    federations = maintained_schools[["URN","LAEstab"]][maintained_schools["Federation"] == "Lead school"].copy()

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

    # TODO - add in soft federation members and names (currently no mapping available)
    # soft_federations.join(maintained_schools, on="URN")
    # hard_federations.join(maintained_schools, on="URN")
    #
    # soft_federations.set_index("UKPRN", inplace=True)
    # hard_federations.set_index("UKPRN", inplace=True)
    return hard_federations, soft_federations
