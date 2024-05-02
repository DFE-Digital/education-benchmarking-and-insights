import datetime

import pandas as pd

import src.pipeline.input_schemas as input_schemas
import src.pipeline.mappings as mappings


def prepare_cdc_data(cdc_file_path, current_year) -> dict:
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

    census.drop(
        labels=["full time pupils", "headcount of pupils"], axis=1, inplace=True
    )

    census.rename(
        columns={
            "Total Number of Non-Classroom-based School Support Staff, (Other school support staff plus Administrative staff plus Technicians and excluding Auxiliary staff (Full-Time Equivalent)": "FullTimeOther",
            "Total Number of Non Classroom-based School Support Staff, Excluding Auxiliary Staff (Headcount)": "FullTimeOtherHeadCount",
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
    sen["Percentage SEN"] = (sen["EHC plan"] / sen["Total pupils"]) * 100.0
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
    sen.rename(
        columns={
            "prov_slcn": "Prov_SLCN",
            "prov_hi": "Prov_HI",
            "prov_vi": "Prov_VI",
            "prov_msi": "Prov_MSI",
            "prov_pd": "Prov_PD",
            "prov_asd": "Prov_ASD",
            "prov_oth": "Prov_OTH",
        },
        inplace=True,
    )

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
            "Prov_SPLD",
            "Prov_MLD",
            "Prov_SLD",
            "Prov_PMLD",
            "Prov_SEMH",
            "Prov_SLCN",
            "Prov_HI",
            "Prov_VI",
            "Prov_MSI",
            "Prov_PD",
            "Prov_ASD",
            "Prov_OTH",
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


def prepare_aar_data(aar_path):
    aar = pd.read_excel(
        aar_path,
        sheet_name="Academies",
        usecols=input_schemas.aar_academies.keys(),
        dtype=input_schemas.aar_academies,
    )

    central_services_financial = pd.read_excel(
        aar_path,
        sheet_name="CentralServices",
        usecols=input_schemas.aar_central_services.keys(),
        dtype=input_schemas.aar_central_services,
    )

    aar.rename(
        columns={
            "Academy UPIN": "academyupin",
            "In year balance": "Academy Balance",
            "PFI": "PFI School",
            "Lead UPIN": "trustupin",
        },
        inplace=True,
    )

    academies_financial = aar[
        aar["MAT SAT or Central Services"] == "Single Academy Trust (SAT)"
    ].copy()

    academy_financial_position = academies_financial[["academyupin", "Academy Balance"]]

    academy_agg = (
        academies_financial[input_schemas.aar_aggregation_columns]
        .groupby("academyupin")
        .sum()
    )

    academy_agg = academy_agg.drop(columns=["trustupin"])

    trust_financial = aar[
        aar["MAT SAT or Central Services"] == "Multi Academy Trust (MAT)"
    ].copy()

    trust_financial_position = (
        trust_financial[["trustupin", "Academy Balance"]]
        .groupby("trustupin")
        .sum()
        .rename(columns={"Academy Balance": "Trust Balance"})
    )

    central_services_financial.rename(
        columns={
            "In Year Balance": "Central Services Balance",
            "Lead UPIN": "trustupin",
        },
        inplace=True,
    )

    central_services_financial_position = (
        central_services_financial[["trustupin", "Central Services Balance"]]
        .groupby("trustupin")
        .sum()
    )

    aar = aar.drop(columns=["Academy Balance"])

    ar = (
        aar.merge(academy_financial_position, on="academyupin", how="left")
        .merge(trust_financial_position, on="trustupin", how="left")
        .merge(central_services_financial_position, on="trustupin", how="left")
    )

    trust_ar = (
        trust_financial[input_schemas.aar_aggregation_columns]
        .groupby("trustupin")
        .sum()
    )

    trust_ar = trust_ar.drop(columns=["academyupin"])

    academy_ar = (
        ar.reset_index()
        .drop_duplicates(subset=["academyupin"], ignore_index=True)[
            [
                "academyupin",
                "Academy Balance",
                "Trust Balance",
                "Central Services Balance",
                "PFI School",
            ]
        ]
        .set_index("academyupin")
    )

    academy_ar["Central Services Financial Position"] = academy_ar[
        "Central Services Balance"
    ].map(mappings.map_is_surplus_deficit)

    academy_ar["Academy Financial Position"] = academy_ar["Academy Balance"].map(
        mappings.map_is_surplus_deficit
    )

    academy_ar["Trust Financial Position"] = academy_ar["Trust Balance"].map(
        mappings.map_is_surplus_deficit
    )

    academy_ar.merge(academy_agg, left_on="academyupin", right_index=True, how="left")

    return trust_ar, academy_ar


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
    academy_data_path, year, schools, census, sen, cdc, academy_ar, trust_agg, ks2, ks4
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
        .merge(academy_ar, left_on="Academy UPIN", right_index=True, how="left")
        .merge(trust_agg, left_on="Academy Trust UPIN", right_index=True, how="left")
        .merge(ks2, on="URN", how="left")
        .merge(ks4, on="URN", how="left")
    )

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

    academies.rename(
        columns={
            "UKPRN_x": "UKPRN",
            "Number of Pupils": "Number of pupils",
            "% of pupils known to be eligible for free school meals (Performa": "Percentage Free school meals",
        },
        inplace=True,
    )

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
        axis=1,
    )
    maintained_schools["Partial Years Present"] = maintained_schools[
        "Period covered by return (months)"
    ].map(lambda x: x != 12)
    maintained_schools["Did Not Submit"] = maintained_schools[
        "Did Not Supply flag"
    ].map(lambda x: x == 1)

    maintained_schools.rename(
        columns={
            "E22 Administrative supplies": "Administrative supplies_Administrative supplies (non educational)",
            "E06 Catering staff": "Catering_Catering staff",
            "E25  Catering supplies": "Catering_Catering supplies",
            "I09  Income from catering": "Catering_Income from catering",
            "E21  Exam fees": "Educational supplies_Examination fees",
            "E19  Learning resources (not ICT equipment)": "Educational supplies_Learning resources (not ICT equipment)",
            "E20  ICT learning resources": "IT_ICT learning resources",
            "E05 Administrative and clerical staff": "Non-educational support staff_Administrative and clerical staff",
            # '':'Non-educational support staff_Auditor costs',
            "E07  Cost of other staff": "Non-educational support staff_Other staff",
            "E28a  Bought in professional services - other (except PFI)": "Non-educational support staff_Professional services (non-curriculum)",
            "E30 Direct revenue financing (revenue contributions to capital)": "Other costs_Direct revenue financing",
            "E13  Grounds maintenance and improvement": "Other costs_Grounds maintenance",
            "E08  Indirect employee expenses": "Other costs_Indirect employee expenses",
            "E29  Loan interest": "Other costs_Interest charges for loan and bank",
            "E23  Other insurance premiums": "Other costs_Other insurance premiums",
            "E28b Bought in professional services - other (PFI)": "Other costs_PFI charges",
            "E17  Rates": "Other costs_Rent and rates",
            "E24  Special facilities ": "Other costs_Special facilities",
            "E09  Development and training": "Other costs_Staff development and training",
            "E11  Staff related insurance": "Other costs_Staff-related insurance",
            "E10  Supply teacher insurance": "Other costs_Supply teacher insurance",
            "E14  Cleaning and caretaking": "Premises_Cleaning and caretaking",
            "E12  Building maintenance and improvement": "Premises_Maintenance of premises",
            "E18  Other occupation costs": "Premises_Other occupation costs",
            "E04  Premises staff": "Premises_Premises staff",
            "E26 Agency supply teaching staff": "Teaching and Teaching support staff_Agency supply teaching staff",
            "E03 Education support staff": "Teaching and Teaching support staff_Education support staff",
            "E27  Bought in professional services - curriculum": "Teaching and Teaching support staff_Educational consultancy",
            "E02  Supply teaching staff": "Teaching and Teaching support staff_Supply teaching staff",
            "E01  Teaching Staff": "Teaching and Teaching support staff_Teaching staff",
            "E16  Energy": "Utilities_Energy",
            "E15  Water and sewerage": "Utilities_Water and sewerage: ",
            "PFI": "PFI School",
            "% of pupils eligible for FSM": "Percentage Free school meals",
            "No Pupils": "Number of pupils",
            "I07  Other grants and payments": "Other grants and payments",
        },
        inplace=True,
    )

    maintained_schools.set_index("URN", inplace=True)
    return maintained_schools


def build_federations_data(links_data_path, maintained_schools):
    group_links = pd.read_csv(
        links_data_path,
        encoding="unicode-escape",
        index_col=input_schemas.groups_index_col,
        usecols=input_schemas.groups.keys(),
        dtype=input_schemas.groups,
    )

    federations = maintained_schools[["LAEstab"]][
        maintained_schools["Federation"] == "Lead school"
    ].copy()
    # join
    federations = federations.join(
        group_links[["Group Name", "Group UID", "Closed Date"]]
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
    soft_federations["FederationUid"] = soft_federations.index.astype(
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
    return hard_federations, soft_federations
