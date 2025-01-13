import pandas as pd

import pipeline.input_schemas as input_schemas
import pipeline.mappings as mappings


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
