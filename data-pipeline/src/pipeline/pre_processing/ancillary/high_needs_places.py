import pandas as pd
from pandas._typing import FilePath, ReadCsvBuffer

from pipeline.input_schemas import high_needs_places_columns


def prepare_high_needs_places_data(
    place_numbers_data: FilePath | ReadCsvBuffer[bytes] | ReadCsvBuffer[str],
    s251_year: int,
):
    """SEN place numbers per school"""
    place_cols = high_needs_places_columns.get(s251_year)
    place_numbers = pd.read_excel(
        place_numbers_data,
        sheet_name="High_Needs_Places",
        engine="calamine",
        usecols=place_cols,
        header=3,
        skiprows=None,
    )
    place_numbers.columns = place_numbers.columns.astype(str).str.replace(
        r"[\r\n]+", "", regex=True
    )
    return place_numbers
