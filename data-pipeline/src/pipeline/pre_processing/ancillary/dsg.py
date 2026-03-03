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
    dsg_ap_academies = dsg["Alternative provision (AP) academies and free schools "]
    dsg_mainstream_academies = dsg[
        "Mainstream academies (special educational needs (SEN) units and resourced provision)"]
    dsg_special_academies = dsg["Special academies"]
    dsg_special_free_schools = dsg["Special free schools"]
    dsg_hospital_schools = dsg["Hospital Academies"]

    dsg["DSGSENAcademyPlaceFunding"] = (
          dsg_special_academies["Pre-16 SEN Places"]["SEN places deduction (£s)"]
        + dsg_special_academies["Post-16 SEN Places"]["SEN places deduction (£s)"]
        + dsg_special_academies["Pre-16 AP Places"]["AP places deduction (£s)"]
        + dsg_special_free_schools["Pre-16 SEN places"]["SEN places deduction (£s)"]
        + dsg_special_free_schools["Post-16 SEN places"]["SEN places deduction (£s)"]
        + dsg_special_free_schools["Pre-16 AP Places"]["AP places deduction (£s) "]
    )
    dsg["DSGAPAcademyPlaceFunding"] = (
          dsg_ap_academies["Pre-16 SEN places"]["SEN places deduction (£s)"]
        + dsg_ap_academies["Post-16 SEN places"]["SEN places deduction (£s)"]
        + dsg_ap_academies["Pre-16 AP Places"]["AP places deduction (£s) "]
    )
    dsg["DSGHospitalPlaceFunding"] = dsg_hospital_schools[
        "Hospital Academies funding"]["Total hospital education deduction (£s)"]
    # This gets split into Primary/Secondary later
    dsg["Total Mainstream DSG deduction"] = (
        dsg_mainstream_academies["Pre-16 SEN places funded at £6,000"]["SEN places deduction (£s)"]
        + dsg_mainstream_academies["Pre-16 SEN places funded at £10,000"]["SEN places deduction (£s)"]
        + dsg_mainstream_academies["Post-16 SEN Places"]["SEN places deduction (£s)"]
        + dsg_mainstream_academies["Pre-16 alternative provision (AP) places"]["AP places deduction (£s)"]
    )

    # Flatten the multindex in the columns
    dsg.columns = [" ".join(col).strip() for col in dsg.columns.values]
    dsg_preprocessed = dsg.rename({dsg.columns[0]: "LA"}, axis=1).set_index("LA")
    dsg_preprocessed = dsg_preprocessed[
        [
            "DSGSENAcademyPlaceFunding",
            "DSGAPAcademyPlaceFunding",
            "DSGHospitalPlaceFunding",
            "Total Mainstream DSG deduction",
        ]
    ]
    return dsg_preprocessed
