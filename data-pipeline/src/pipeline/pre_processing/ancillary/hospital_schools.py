import pandas as pd

from pipeline import input_schemas


def prepare_hospital_schools_data(hospital_schools_blob, year: int = 2025):
    schema = input_schemas.hospital_schools_cols.get(
        year, input_schemas.hospital_schools_cols["default"]
    )
    hospital_schools = pd.read_csv(
        hospital_schools_blob, usecols=schema.keys(),
        dtype=schema
    )
    if column_mappings:= input_schemas.hospital_schools_column_mappings.get(year, None):
        hospital_schools.rename(column_mappings, axis=1, inplace=True)
    return hospital_schools
