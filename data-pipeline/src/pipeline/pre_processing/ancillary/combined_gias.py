import logging

import pandas as pd

import pipeline.input_schemas as input_schemas
import pipeline.pre_processing.common.mappings as mappings

logger = logging.getLogger("fbit-data-pipeline")


def prepare_combined_gias_data(base_data_path, links_data_path, year: int):
    """
    Prepare school data derived from GIAS and GIAS links.

    :param base_data_path: readable source for GIAS
    :param links_data_path: readable source for GIAS links
    :param year: financial year in question
    """
    gias = pd.read_csv(
        base_data_path,
        encoding="cp1252",
        index_col=input_schemas.gias_index_col,
        usecols=input_schemas.gias.get(year, input_schemas.gias["default"]).keys(),
        dtype=input_schemas.gias.get(year, input_schemas.gias["default"]),
    )
    logger.info(f"GIAS Data raw {year=} shape: {gias.shape}")

    gias_links = pd.read_csv(
        links_data_path,
        encoding="cp1252",
        index_col=input_schemas.gias_links_index_col,
        usecols=input_schemas.gias_links.keys(),
        dtype=input_schemas.gias_links,
    )
    logger.info(f"GIAS links Data raw {year=} shape: {gias_links.shape}")

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

    gias = _optional_ofsted_cols(gias)

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


def _optional_ofsted_cols(df: pd.DataFrame) -> pd.DataFrame:
    """
    Ensure that "OfstedRating (name)" and "OfstedLastInsp" columns are present in the DataFrame,
    even if they are missing from the original submission. Missing columns are created with defaults
    to ensure compatibility with downstream processing.

    These columns are required to write to the db and "OfstedRating (name)" is required for rag
    calculations.

    If the columns exist, they are either preserved or mapped as necessary.
    If they do not exist, new columns are created: an empty string column for
    "OfstedRating (name)" and a `None` column for "OfstedLastInsp".

    :param df: The GIAS DataFrame to process.

    :return: The DataFrame with the "OfstedRating (name)" and "OfstedLastInsp" columns added or modified.
    """
    df["OfstedRating (name)"] = (
        df.get("OfstedRating (name)", pd.Series([""] * len(df), index=df.index)).fillna(
            ""
        )
    ).map(mappings.map_ofsted_rating)

    df["OfstedLastInsp"] = df.get(
        "OfstedLastInsp", pd.Series([None] * len(df), index=df.index)
    )

    return df
