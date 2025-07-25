import logging

import pandas as pd

import pipeline.config as config
import pipeline.input_schemas as input_schemas
import pipeline.mappings as mappings
from pipeline.stats_collector import stats_collector

logger = logging.getLogger("fbit-data-pipeline")


def prepare_cdc_data(cdc_file_path, current_year):
    cdc = pd.read_csv(
        cdc_file_path,
        encoding="utf8",
        index_col=input_schemas.cdc_index_col,
        usecols=input_schemas.cdc.keys(),
        dtype=input_schemas.cdc,
    )
    logger.info(f"CDC Data raw {current_year=} shape: {cdc.shape}")

    cdc["Total Internal Floor Area"] = cdc.groupby(by=["URN"])["GIFA"].sum()
    cdc["Proportion Area"] = cdc["GIFA"] / cdc["Total Internal Floor Area"]
    cdc["Indicative Age"] = (
        cdc["Block Age"].fillna("").map(mappings.map_block_age).astype("Int64")
    )
    cdc["Age Score"] = cdc["Proportion Area"] * (current_year - cdc["Indicative Age"])
    cdc["Age Average Score"] = cdc.groupby(by=["URN"])["Age Score"].sum()
    cdc["Building Age"] = (
        cdc.groupby(by=["URN"])["Indicative Age"].mean().astype("Int64")
    )
    cdc_generated = cdc[config.cdc_generated_columns]
    cdc_generated_no_dupes = cdc_generated[
        ~cdc_generated.index.duplicated(keep="first")
    ]

    stats_collector.collect_preprocessed_ancillary_data_shape(
        "cdc", cdc_generated_no_dupes.shape
    )

    return cdc_generated_no_dupes
