import pandas as pd
from pandas._typing import FilePath, ReadCsvBuffer


def prepare_dsg_data(dsg_data: FilePath | ReadCsvBuffer[bytes] | ReadCsvBuffer[str]):
    """
    Funding for SEN through the dedicated schools grant (DSG) per local authority (LA)
    """
    dsg = pd.read_excel(
        dsg_data,
        sheet_name="High_needs_deductions",
        skiprows=[3],
        header=[0, 1, 2],
        engine="odf",
    )
    dsg["SENAcademyPlaceFunding"] = (
        dsg[("Special academies", "Pre-16 SEN Places", "SEN places deduction (£s)")]
        + dsg[
            ("Special free schools", "Pre-16 SEN places", "SEN places deduction (£s)")
        ]
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
        + dsg[
            ("Special free schools", "Post-16 SEN places", "SEN places deduction (£s)")
        ]
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
        (
            "Hospital Academies",
            "Hospital Academies funding",
            "Total hospital education deduction (£s)",
        )
    ]
    dsg["Total Mainstream Pre-16 SEN places deduction"] = (
        dsg[
            (
                "Mainstream academies (special educational needs (SEN) units and resourced provision)",
                "Pre-16 SEN places funded at £6,000",
                "SEN places deduction (£s)",
            )
        ]
        + dsg[
            (
                "Mainstream academies (special educational needs (SEN) units and resourced provision)",
                "Pre-16 SEN places funded at £10,000",
                "SEN places deduction (£s)",
            )
        ]
    )

    # Flatten the multindex in the columns
    dsg.columns = [" ".join(col).strip() for col in dsg.columns.values]
    dsg_preprocessed = dsg.rename({dsg.columns[0]: "LA"}, axis=1).set_index("LA")
    dsg_preprocessed = dsg_preprocessed[
        [
            "SENAcademyPlaceFunding",
            "APAcademyPlaceFunding",
            "Post16PlaceFunding",
            "HospitalPlaceFunding",
            "Total Mainstream Pre-16 SEN places deduction",
        ]
    ]
    return dsg_preprocessed
