from io import StringIO, BytesIO

import numpy as np
import pandas as pd
import pytest

from src.pipeline.pre_processing import (prepare_cdc_data, prepare_sen_data, prepare_ks2_data, prepare_ks4_data)


@pytest.fixture
def cdc_data() -> pd.DataFrame:
    return pd.DataFrame(
        {
            "Site": ["Site A", "Site A", "Site A", "Site A", "Site B"],
            "URN": [100150, 100150, 100150, 100150, 100162],
            "SurveySectionSectionType": [
                "ANCILLARY",
                "BLOCK",
                "BLOCK",
                "BLOCK",
                "ANCILLARY",
            ],
            "GIFA": [89, 2221, 436, 57, 131],
            "Block Age": ["NULL", "1961-1970", "2001-2010", "1991-2000", "NULL"],
        }
    )


@pytest.fixture
def prepared_cdc_data(cdc_data: pd.DataFrame) -> dict:
    return (
        prepare_cdc_data(StringIO(cdc_data.to_csv()), 2022)
        .reset_index()
        .to_dict("records")[0]
    )


@pytest.fixture
def sen_data() -> pd.DataFrame:
    return pd.DataFrame({
        "URN": [100150, 100151],
        "EHC plan": [50, 100],
        "Total pupils": [100, 100],
        "EHC_Primary_need_spld": [1, 50],
        "SUP_Primary_need_spld": [1, 50],
        "EHC_Primary_need_mld": [1, np.nan],
        "SUP_Primary_need_mld": [2, np.nan],
        "EHC_Primary_need_sld": [1, np.nan],
        "SUP_Primary_need_sld": [3, np.nan],
        "EHC_Primary_need_pmld": [1, np.nan],
        "SUP_Primary_need_pmld": [4, np.nan],
        "EHC_Primary_need_semh": [1, np.nan],
        "SUP_Primary_need_semh": [5, np.nan],
        "EHC_Primary_need_slcn": [1, np.nan],
        "SUP_Primary_need_slcn": [6, np.nan],
        "EHC_Primary_need_hi":  [1, np.nan],
        "SUP_Primary_need_hi": [7, np.nan],
        "EHC_Primary_need_vi": [1, np.nan],
        "SUP_Primary_need_vi": [8, np.nan],
        "EHC_Primary_need_msi": [1, np.nan],
        "SUP_Primary_need_msi": [9, np.nan],
        "EHC_Primary_need_pd": [1, np.nan],
        "SUP_Primary_need_pd": [10, np.nan],
        "EHC_Primary_need_asd": [1, np.nan],
        "SUP_Primary_need_asd": [11, np.nan],
        "EHC_Primary_need_oth": [1, np.nan],
        "SUP_Primary_need_oth": [12, np.nan],
    })


@pytest.fixture
def prepared_sen_data(sen_data: pd.DataFrame) -> dict:
    return (
        prepare_sen_data(StringIO(sen_data.to_csv()))
        .reset_index()
        .to_dict("records")[0]
    )


@pytest.fixture
def prepared_sen_data_with_nans(sen_data: pd.DataFrame) -> dict:
    return (
        prepare_sen_data(StringIO(sen_data.to_csv()))
        .reset_index()
        .to_dict("records")[1]
    )


@pytest.fixture
def ks2_data() -> pd.DataFrame:
    return pd.DataFrame({
        "URN": [100150, 100152, 100153],
        "READPROG": ["0.1", "SUPP", "LOWCOV"],
        "WRITPROG": ["0.1", "SUPP", "LOWCOV"],
        "MATPROG": ["0.1", "SUPP", "LOWCOV"],
    })


@pytest.fixture
def prepared_ks2_data(ks2_data: pd.DataFrame):
    output = BytesIO()
    writer = pd.ExcelWriter(output)
    ks2_data.to_excel(writer, index=False)
    writer.close()
    output.seek(0)

    return prepare_ks2_data(output)


@pytest.fixture
def ks4_data() -> pd.DataFrame:
    return pd.DataFrame({
        "URN": [100150, 100152, 100153],
        "ATT8SCR": ["0.1", "SUPP", "NE"],
        "P8MEA": ["0.1", "SUPP", "NE"],
        "P8_BANDING": ["0.1", "SUPP", "NE"],
    })


@pytest.fixture
def prepared_ks4_data(ks4_data: pd.DataFrame):
    output = BytesIO()
    writer = pd.ExcelWriter(output)
    ks4_data.to_excel(writer, index=False)
    writer.close()
    output.seek(0)

    return prepare_ks4_data(output)
