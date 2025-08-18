import time
import warnings
from typing import Any, Dict, Generator, List, Optional

import numpy as np
import pandas as pd

from pipeline.config import rag_category_settings
from pipeline.utils.log import setup_logger

logger = setup_logger(__name__)

# --- Constants for Configuration ---
BATCH_LOG_INTERVAL = 1000
MAX_DECILE_INDEX = 9
PERCENTILE_TO_DECILE_DIVISOR = 10
FLOOR_AREA_PERCENTAGE_TOLERANCE = 0.1
BUILDING_AGE_YEAR_TOLERANCE = 20.0
PUPIL_COUNT_PERCENTAGE_TOLERANCE = 0.25
FSM_PERCENTAGE_TOLERANCE = 0.05
SEN_PERCENTAGE_TOLERANCE = 0.1

# --- Constants for Column Names ---
BASE_COLUMNS = [
    "URN",
    "Total Internal Floor Area",
    "Age Average Score",
    "OfstedRating (name)",
    "Percentage SEN",
    "Percentage Free school meals",
    "Number of pupils",
]

RAG_RESULT_COLUMNS = [
    "URN",
    "Category",
    "SubCategory",
    "Value",
    "Median",
    "DiffMedian",
    "Key",
    "PercentDiff",
    "Percentile",
    "Decile",
    "RAG",
]

class CategoryColumnCache:
    """Caches column masks and subcategory lists for faster access."""

    def __init__(self, columns: pd.Index):
        self.base_columns_mask = columns.isin(BASE_COLUMNS)
        self.category_mappings = {}
        for category_name in rag_category_settings:
            category_mask = columns.str.startswith(
                category_name
            ) & columns.str.endswith("_Per Unit")
            self.category_mappings[category_name] = {
                "column_mask": self.base_columns_mask | category_mask,
                "subcategories": columns[category_mask].tolist(),
            }


def find_area_close_comparators(
    target_school: pd.Series, comparators: pd.DataFrame
) -> pd.Series:
    """
    Vectorized function to find comparators with similar building characteristics.
    - Gross internal floor area is within 10% (relative).
    - Average age of buildings is within 20 years (absolute).
    """
    floor_area_is_close = np.isclose(
        comparators["Total Internal Floor Area"],
        target_school["Total Internal Floor Area"],
        rtol=FLOOR_AREA_PERCENTAGE_TOLERANCE,
    )
    age_score_is_close = np.isclose(
        comparators["Age Average Score"],
        target_school["Age Average Score"],
        atol=BUILDING_AGE_YEAR_TOLERANCE,  # Absolute tolerance
    )
    return floor_area_is_close & age_score_is_close


def find_pupil_close_comparators(
    target_school: pd.Series, comparators: pd.DataFrame
) -> pd.Series:
    """
    Vectorized function to find comparators with similar pupil demographics.
    - Number of pupils is within 25% (relative).
    - FSM percentage is within 5 percentage points (absolute).
    - SEN percentage is within 10 percentage points (absolute).
    """
    pupils_is_close = np.isclose(
        comparators["Number of pupils"],
        target_school["Number of pupils"],
        rtol=PUPIL_COUNT_PERCENTAGE_TOLERANCE,
    )
    fsm_is_close = np.isclose(
        comparators["Percentage Free school meals"],
        target_school["Percentage Free school meals"],
        rtol=FSM_PERCENTAGE_TOLERANCE,
    )
    sen_is_close = np.isclose(
        comparators["Percentage SEN"],
        target_school["Percentage SEN"],
        rtol=SEN_PERCENTAGE_TOLERANCE,
    )
    return pupils_is_close & fsm_is_close & sen_is_close


# --- Core Calculation Logic ---


def calculate_percentile_rank(data_values: np.ndarray, target_value: float) -> float:
    """Calculates the percentile of a value within a numpy array."""
    if len(data_values) == 0:
        return 0.0
    sorted_series = np.sort(data_values, kind="stable")
    rank = np.searchsorted(sorted_series, target_value, side="right")
    return (rank / len(data_values)) * 100


def get_positive_values_series(data: pd.DataFrame, column: str, urn: str) -> pd.Series:
    """Filters a series for positive values, always including the target URN."""
    mask = (data[column] > 0) | (data.index == urn)
    return data.loc[mask, column]


def calculate_category_statistics(
    school_urn: str,
    category_name: str,
    subcategory_name: str,
    category_data: pd.DataFrame,
    ofsted_rating: str,
    rag_settings: Dict[str, Any],
    similar_schools_count: int,
) -> Dict[str, Any]:
    """Calculates all statistical metrics for a single school and category."""
    rating_key = "outstanding" if ofsted_rating.lower() == "outstanding" else "other"
    rating_key += "_10" if similar_schools_count > 10 else ""

    category_series = get_positive_values_series(
        category_data, subcategory_name, school_urn
    )
    school_value = category_series.loc[school_urn]

    percentile_score = calculate_percentile_rank(category_series.values, school_value)
    decile = int(percentile_score / PERCENTILE_TO_DECILE_DIVISOR)
    median_value = np.median(category_series)
    median_difference = school_value - median_value

    percent_difference = 0.0
    if median_value and np.isfinite(median_value):
        percent_difference = (median_difference / median_value) * 100

    subcategory_components = subcategory_name.split("_")

    return {
        "URN": school_urn,
        "Category": category_name,
        "SubCategory": subcategory_components[1],
        "Value": school_value,
        "Median": median_value,
        "DiffMedian": median_difference,
        "Key": rating_key,
        "PercentDiff": percent_difference,
        "Percentile": percentile_score,
        "Decile": decile,
        "RAG": rag_settings[rating_key][min(decile, MAX_DECILE_INDEX)],
    }


def compute_category_rag_statistics(
    school_urn: str,
    category_name: str,
    rag_settings: Dict[str, Any],
    target_school: pd.Series,
    comparator_set: pd.DataFrame,
    column_cache: CategoryColumnCache,
) -> Generator[Dict[str, Any], None, None]:
    """Computes RAG statistics for all subcategories within a main category."""
    ofsted_rating = target_school["OfstedRating (name)"]
    if rag_settings["type"] == "Building":
        similar_mask = find_area_close_comparators(target_school, comparator_set)
    else:
        similar_mask = find_pupil_close_comparators(target_school, comparator_set)
    similar_schools_count = similar_mask.sum()

    category_mapping = column_cache.category_mappings[category_name]
    category_data = comparator_set.loc[:, category_mapping["column_mask"]]

    for subcategory_name in category_mapping["subcategories"]:
        try:
            yield calculate_category_statistics(
                school_urn,
                category_name,
                subcategory_name,
                category_data,
                ofsted_rating,
                rag_settings,
                similar_schools_count,
            )
        except (KeyError, ValueError) as e:
            logger.warning(
                f"Could not calculate stats for {school_urn}, {subcategory_name}: {e}"
            )
            continue


# --- Main Entry Points ---


def prepare_data_for_rag(data: pd.DataFrame) -> pd.DataFrame:
    """Prepares and cleans data for RAG calculations."""
    required_cols_mask = (
        data.columns.isin(BASE_COLUMNS)
        | data.columns.str.endswith("_Per Unit")
        | (data.columns == "Partial Years Present")
    )
    return data.loc[:, required_cols_mask].fillna(0.0)


def calculate_rag(
    data: pd.DataFrame, comparators: Dict, target_urn: Optional[str] = None
) -> Generator[Dict[str, Any], None, None]:
    """
    Performs RAG calculations for a dataset using a high-performance vectorized approach.
    """
    processed_data = prepare_data_for_rag(data)
    column_cache = CategoryColumnCache(processed_data.columns)

    schools_to_process = (
        [target_urn]
        if target_urn and target_urn in processed_data.index
        else processed_data.index
    )
    start_time = time.time()

    with warnings.catch_warnings(), np.errstate(invalid="ignore"):
        warnings.simplefilter("ignore", category=RuntimeWarning)

        for i, school_urn in enumerate(schools_to_process):
            target_school = processed_data.loc[school_urn]
            if target_school.get("Partial Years Present", False):
                continue

            try:
                pupil_urns = comparators.get("Pupil", {}).get(school_urn)
                building_urns = comparators.get("Building", {}).get(school_urn)

                for category_name, rag_settings in rag_category_settings.items():
                    set_urns = (
                        pupil_urns if rag_settings["type"] == "Pupil" else building_urns
                    )

                    if set_urns is not None and len(set_urns) > 0:
                        # Filter comparator set just-in-time (fast and memory-efficient)
                        comparator_set = processed_data[
                            processed_data.index.isin(set_urns)
                        ]
                        yield from compute_category_rag_statistics(
                            school_urn,
                            category_name,
                            rag_settings,
                            target_school,
                            comparator_set,
                            column_cache,
                        )

            except Exception as e:
                logger.exception(
                    f"Unexpected error processing school {school_urn}: {e}"
                )
                continue

            if i > 0 and i % BATCH_LOG_INTERVAL == 0:
                elapsed = time.time() - start_time
                logger.debug(f"Processed {i} schools in {elapsed:.2f} seconds.")
                start_time = time.time()


def compute_user_defined_rag(
    data: pd.DataFrame, target_urn: int, comparator_urns: List[int]
) -> Generator[Dict[str, Any], None, None]:
    """Performs user-defined RAG calculation against a specific list of comparators."""
    processed_data = prepare_data_for_rag(data)
    column_cache = CategoryColumnCache(processed_data.columns)

    if target_urn not in processed_data.index:
        logger.error(f"Target URN {target_urn} not found in data for user-defined RAG.")
        return

    target_school = processed_data.loc[target_urn]

    # Filter to only valid comparators that exist in the dataset
    valid_urns = [urn for urn in comparator_urns if urn in processed_data.index]
    if not valid_urns:
        logger.warning(
            f"No valid comparators found for user-defined RAG for {target_urn}."
        )
        return

    comparator_set = processed_data.loc[valid_urns]

    with warnings.catch_warnings(), np.errstate(invalid="ignore"):
        warnings.simplefilter("ignore", category=RuntimeWarning)
        for category_name, rag_settings in rag_category_settings.items():
            try:
                yield from compute_category_rag_statistics(
                    target_urn,
                    category_name,
                    rag_settings,
                    target_school,
                    comparator_set,
                    column_cache,
                )
            except Exception as e:
                logger.exception(
                    f"Error in user-defined RAG for {target_urn}, category {category_name}: {e}"
                )
                continue
