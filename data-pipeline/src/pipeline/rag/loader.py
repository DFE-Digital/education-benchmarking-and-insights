from typing import Tuple

import pandas as pd

from pipeline.utils.log import setup_logger
from pipeline.utils.storage import get_blob

logger = setup_logger(__name__)


def load_school_data_and_comparators(
    run_type: str, run_id: str, school_type: str
) -> Tuple[pd.DataFrame, pd.DataFrame]:
    """Load school data and comparator mappings from storage."""
    if school_type == "maintained_schools":
        school_filename = "maintained_schools.parquet"
        comparator_filename = "maintained_schools_comparators.parquet"
    elif school_type == "academies":
        school_filename = "academies.parquet"
        comparator_filename = "academy_comparators.parquet"
    else:
        raise ValueError(f"Unknown school type: {school_type}")

    try:
        school_data = pd.read_parquet(
            get_blob("comparator-sets", f"{run_type}/{run_id}/{school_filename}")
        )
        comparator_data = pd.read_parquet(
            get_blob("comparator-sets", f"{run_type}/{run_id}/{comparator_filename}")
        )
        logger.debug(
            f"Loaded {school_type}: data_shape={school_data.shape}, comparators_shape={comparator_data.shape}"
        )
        return school_data, comparator_data
    except Exception as e:
        logger.error(f"Failed to load data for {school_type}: {e}")
        raise
