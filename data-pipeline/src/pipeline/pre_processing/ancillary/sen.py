import logging

import pandas as pd

import pipeline.config as config
import pipeline.input_schemas as input_schemas
from pipeline.stats_collector import stats_collector

logger = logging.getLogger("fbit-data-pipeline")


def prepare_sen_data(sen_path):
    sen = pd.read_csv(
        sen_path,
        encoding="cp1252",
        index_col=input_schemas.sen_index_col,
        dtype=input_schemas.sen,
        usecols=input_schemas.sen.keys(),
    )
    logger.info(f"SEN data raw shape: {sen.shape}")

    sen["Percentage SEN"] = (
        ((sen["EHC plan"] + sen["SEN support"]) / sen["Total pupils"]) * 100.0
    ).fillna(0)
    sen["Percentage with EHC"] = (
        (sen["EHC plan"] / sen["Total pupils"]) * 100.0
    ).fillna(0)
    sen["Percentage without EHC"] = sen["Percentage SEN"] - sen["Percentage with EHC"]

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

    sen["Percentage Primary Need SPLD"] = (
        (sen["Primary Need SPLD"] / sen["Total pupils"]) * 100.0
    ).fillna(0)
    sen["Percentage Primary Need MLD"] = (
        (sen["Primary Need MLD"] / sen["Total pupils"]) * 100.0
    ).fillna(0)
    sen["Percentage Primary Need SLD"] = (
        (sen["Primary Need SLD"] / sen["Total pupils"]) * 100.0
    ).fillna(0)
    sen["Percentage Primary Need PMLD"] = (
        (sen["Primary Need PMLD"] / sen["Total pupils"]) * 100.0
    ).fillna(0)
    sen["Percentage Primary Need SEMH"] = (
        (sen["Primary Need SEMH"] / sen["Total pupils"]) * 100.0
    ).fillna(0)
    sen["Percentage Primary Need SLCN"] = (
        (sen["Primary Need SLCN"] / sen["Total pupils"]) * 100.0
    ).fillna(0)
    sen["Percentage Primary Need HI"] = (
        (sen["Primary Need HI"] / sen["Total pupils"]) * 100.0
    ).fillna(0)
    sen["Percentage Primary Need VI"] = (
        (sen["Primary Need VI"] / sen["Total pupils"]) * 100.0
    ).fillna(0)
    sen["Percentage Primary Need MSI"] = (
        (sen["Primary Need MSI"] / sen["Total pupils"]) * 100.0
    ).fillna(0)
    sen["Percentage Primary Need PD"] = (
        (sen["Primary Need PD"] / sen["Total pupils"]) * 100.0
    ).fillna(0)
    sen["Percentage Primary Need ASD"] = (
        (sen["Primary Need ASD"] / sen["Total pupils"]) * 100.0
    ).fillna(0)
    sen["Percentage Primary Need OTH"] = (
        (sen["Primary Need OTH"] / sen["Total pupils"]) * 100.0
    ).fillna(0)

    return sen[config.sen_generated_columns]
