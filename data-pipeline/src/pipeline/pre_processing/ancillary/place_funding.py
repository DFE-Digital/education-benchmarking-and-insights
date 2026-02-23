import pandas as pd


def prepare_place_funding_data(dsg_data, place_numbers_data):
    place_cols = [
        "URN",
        "2024 to 2025 free schools and academies - pre-16 AP places",
        "2024 to 2025  special schools and academies - pre-16 SEN places",
        "2024 to 2025 mainstream academies and free schools - pre-16 SEN Unit/RP Places (@£6k)",
        "2024 to 2025 mainstream academies and free schools - pre-16 SEN Unit/RP Places (@£10k)",
    ]
    # SEN place numbers per school
    place_numbers = pd.read_excel(
        place_numbers_data,
        sheet_name="High_Needs_Places",
        engine="odf",
        usecols=place_cols,
        header=3,
        skiprows=None,
    )
    # Funding for SEN through the DSG per LA
    dsg = pd.read_excel(
        dsg_data,
        sheet_name="High_needs_deductions",
        skiprows=[3],
        header=[0, 1, 2],
        engine="odf",
    )
    dsg["SENAcademyPlaceFunding"] = (
        dsg[("Special academies", "Pre-16 SEN Places", "SEN places deduction (£s)")]
        + dsg[("Special free schools", "Pre-16 SEN places", "SEN places deduction (£s)")]
    )
    dsg["APAcademyPlaceFunding"] = (
        dsg[
            (
                "Mainstream academies (special educational needs (SEN) units and resourced provision)",
                "Pre-16 alternative provision (AP) places",
                "AP places deduction (£s)",
            )
        ]
        + dsg[("Special academies", "Pre-16 AP Places", "AP places deduction (£s)")]
        + dsg[("Special free schools", "Pre-16 AP Places", "AP places deduction (£s) ")]
        + dsg[
            (
                "Alternative provision (AP) academies and free schools ",
                "Pre-16 AP Places",
                "AP places deduction (£s) ",
            )
        ]
    )
    dsg["Post16PlaceFunding"] = (
        dsg[
            (
                "Mainstream academies (special educational needs (SEN) units and resourced provision)",
                "Post-16 SEN Places",
                "SEN places deduction (£s)",
            )
        ]
        + dsg[("Special academies", "Post-16 SEN Places", "SEN places deduction (£s)")]
        + dsg[("Special free schools", "Post-16 SEN places", "SEN places deduction (£s)")]
        + dsg[
            (
                "Alternative provision (AP) academies and free schools ",
                "Post-16 SEN places",
                "SEN places deduction (£s)",
            )
        ]
        + dsg[
            (
                "16-19 academies and free schools",
                "Post-16 SEN places",
                "SEN places deduction (£s)",
            )
        ]
        + dsg[
            (
                "Further education (FE) and independent learning provider (ILP)",
                "FE and ILP",
                "Total FE and ILP deduction (£s)",
            )
        ]
    )
    dsg["HospitalPlaceFunding"] = dsg[
        ("Hospital Academies", "Hospital Academies funding", "Total hospital education deduction (£s)")
    ]
    dsg["Total Mainstream Pre-16 SEN places deduction"] = (
        dsg[
            (
            "Mainstream academies (special educational needs (SEN) units and resourced provision)",
            "Pre-16 SEN places funded at £6,000",
            "SEN places deduction (£s)"
            )
        ]
        + dsg[
            (
            "Mainstream academies (special educational needs (SEN) units and resourced provision)",
            "Pre-16 SEN places funded at £10,000",
            "SEN places deduction (£s)"
            )
        ]
    )

    # Flatten the multindex in the columns
    dsg.columns = [' '.join(col).strip() for col in dsg.columns.values]
    dsg_preprocessed = dsg.rename({dsg.columns[0]: "LA"}, axis=1).set_index("LA")
    dsg_preprocessed = dsg_preprocessed[[
        "SENAcademyPlaceFunding",
        "APAcademyPlaceFunding",
        "Post16PlaceFunding",
        "HospitalPlaceFunding",
        "Total Mainstream Pre-16 SEN places deduction",
    ]]
    return place_numbers, dsg_preprocessed
