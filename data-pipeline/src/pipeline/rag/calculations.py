import logging
import math
import time
import warnings
from typing import Any, Dict, Generator, List, Optional, Tuple

import numpy as np
import pandas as pd

from pipeline.config import rag_category_settings

pd.options.mode.chained_assignment = None

logger = logging.getLogger("rag")

# Constants for similarity thresholds
FLOOR_AREA_TOLERANCE = 0.1
BUILDING_AGE_THRESHOLD = 20.0
PUPIL_COUNT_TOLERANCE = 0.25
FSM_PERCENTAGE_TOLERANCE = 0.05
SEN_PERCENTAGE_TOLERANCE = 0.1
BATCH_LOG_INTERVAL = 1000
MAX_DECILE_INDEX = 9
PERCENTILE_TO_DECILE_DIVISOR = 10

# Base columns required for RAG calculations
BASE_COLUMNS = [
    "URN",
    "Total Internal Floor Area",
    "Age Average Score",
    "OfstedRating (name)",
    "Percentage SEN",
    "Percentage Free school meals",
    "Number of pupils",
]

# Standard RAG result columns
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


class ComputationError(Exception):
    """Custom exception for RAG computation errors"""

    pass


class CategoryColumnCache:
    """Pre-computed column indices and category mappings for efficient data access"""

    def __init__(self, columns: pd.Index):
        self.base_columns_mask = columns.isin(BASE_COLUMNS)
        self.category_mappings = {}

        for category_name in rag_category_settings.keys():
            category_mask = columns.str.startswith(
                category_name
            ) & columns.str.endswith("_Per Unit")
            combined_mask = self.base_columns_mask | category_mask
            subcategories = columns[category_mask].tolist()

            self.category_mappings[category_name] = {
                "column_mask": combined_mask,
                "subcategories": subcategories,
            }


class ComparatorSetCache:
    """Pre-filtered comparator sets to avoid repeated DataFrame filtering"""

    def __init__(self, data: pd.DataFrame, comparators: Dict):
        self.data = data
        self._cache = {}
        self._build_cache(comparators)

    def _build_cache(self, comparators: Dict) -> None:
        """Pre-filter all comparator sets once"""
        for school_urn, comparator_sets in comparators.items():
            if school_urn in self.data.index:
                pupil_urns = comparator_sets.get("Pupil", [])
                building_urns = comparator_sets.get("Building", [])

                self._cache[school_urn] = {
                    "Pupil": (
                        self.data[self.data.index.isin(pupil_urns)]
                        if pupil_urns
                        else None
                    ),
                    "Building": (
                        self.data[self.data.index.isin(building_urns)]
                        if building_urns
                        else None
                    ),
                }

    def get_comparator_set(
        self, school_urn: str, comparator_type: str
    ) -> Optional[pd.DataFrame]:
        """Retrieve pre-filtered comparator set"""
        return self._cache.get(school_urn, {}).get(comparator_type)


def are_building_characteristics_similar(
    target_school: pd.Series, comparison_school: pd.Series
) -> bool:
    """
    Determine whether two schools have similar building characteristics.

    Buildings are considered similar if both:
    - Gross internal floor area is within 10%
    - Average age of buildings is within 20 years

    Args:
        target_school: Reference school data
        comparison_school: School to compare against

    Returns:
        True if buildings are similar according to criteria
    """
    floor_area_similar = math.isclose(
        target_school["Total Internal Floor Area"],
        comparison_school["Total Internal Floor Area"],
        rel_tol=FLOOR_AREA_TOLERANCE,
    )
    building_age_similar = math.isclose(
        target_school["Age Average Score"],
        comparison_school["Age Average Score"],
        abs_tol=BUILDING_AGE_THRESHOLD,
    )

    return floor_area_similar and building_age_similar


def are_pupil_demographics_similar(
    target_school: pd.Series, comparison_school: pd.Series
) -> bool:
    """
    Determine if two schools have similar pupil demographics.

    Demographics are considered similar if all of:
    - Number of pupils is within 25%
    - Percentage of FSM (Free School Meals) is within 5%
    - Percentage of SEN (Special Educational Needs) is within 10%

    Args:
        target_school: Reference school data
        comparison_school: School to compare against

    Returns:
        True if demographics are similar according to criteria
    """
    pupil_count_similar = math.isclose(
        target_school["Number of pupils"],
        comparison_school["Number of pupils"],
        rel_tol=PUPIL_COUNT_TOLERANCE,
    )
    fsm_percentage_similar = math.isclose(
        target_school["Percentage Free school meals"],
        comparison_school["Percentage Free school meals"],
        rel_tol=FSM_PERCENTAGE_TOLERANCE,
    )
    sen_percentage_similar = math.isclose(
        target_school["Percentage SEN"],
        comparison_school["Percentage SEN"],
        rel_tol=SEN_PERCENTAGE_TOLERANCE,
    )

    return all(
        [
            pupil_count_similar,
            fsm_percentage_similar,
            sen_percentage_similar,
        ]
    )


def determine_similarity_comparator(
    category_type: str, target_school: pd.Series, comparison_school: pd.Series
) -> bool:
    """
    Determine which similarity criteria to apply based on category type.

    For building cost categories (premises, staff, services, utilities),
    building characteristics are used. For all other categories,
    pupil demographics are used.

    Args:
        category_type: Type of cost category ("Building" or other)
        target_school: Reference school data
        comparison_school: School to compare against

    Returns:
        True if schools are similar according to appropriate criteria
    """
    if category_type == "Building":
        return are_building_characteristics_similar(target_school, comparison_school)

    return are_pupil_demographics_similar(target_school, comparison_school)


def calculate_percentile_rank(data_values: pd.Series, target_value: float) -> float:
    """
    Calculate percentile rank for a value within a dataset.

    Uses pandas rank method for O(n log n) performance instead of
    repeated sorting operations.

    Args:
        data_values: Series of values for comparison
        target_value: Value to find percentile rank for

    Returns:
        Percentile rank (0-100)
    """
    if len(data_values) == 0:
        return 0.0

    # Create temporary series with target value included
    temp_series = pd.concat([data_values, pd.Series([target_value])])
    ranks = temp_series.rank(method="max", pct=True)

    # Return percentile for the target value (last element)
    return ranks.iloc[-1] * 100


def calculate_category_statistics(
    school_urn: str,
    category_name: str,
    subcategory_name: str,
    category_data: pd.DataFrame,
    ofsted_rating: str,
    rag_settings: Dict[str, Any],
    similar_schools_count: int,
) -> Dict[str, Any]:
    """
    Calculate RAG statistics for a single category and subcategory.

    Args:
        school_urn: URN of the school
        category_name: Main category name
        subcategory_name: Specific subcategory name
        category_data: DataFrame containing category data
        ofsted_rating: Ofsted rating of the school
        rag_settings: RAG configuration settings
        similar_schools_count: Number of similar schools found

    Returns:
        Dictionary containing calculated statistics
    """
    # Determine rating key for RAG mapping
    rating_key = "outstanding" if ofsted_rating.lower() == "outstanding" else "other"
    rating_key += "_10" if similar_schools_count > 10 else ""

    # Get series for this subcategory with positive values only
    category_series = get_positive_values_series(
        category_data, subcategory_name, school_urn
    )
    school_value = category_series.loc[school_urn]

    # Calculate statistics
    percentile_score = calculate_percentile_rank(category_series, school_value)
    decile = int(percentile_score / PERCENTILE_TO_DECILE_DIVISOR)
    median_value = category_series.median()

    # Calculate difference from median
    median_difference = school_value - median_value
    if median_value != 0 and not (pd.isna(median_value) or np.isinf(median_value)):
        percent_difference = (median_difference / median_value) * 100
    else:
        percent_difference = 0.0

    # Parse category components
    category_components = category_name.split("_")

    return {
        "URN": school_urn,
        "Category": category_components[0],
        "SubCategory": category_components[1],
        "Value": school_value,
        "Median": median_value,
        "DiffMedian": median_difference,
        "Key": rating_key,
        "PercentDiff": percent_difference,
        "Percentile": percentile_score,
        "Decile": decile,
        "RAG": rag_settings[rating_key][min(decile, MAX_DECILE_INDEX)],
    }


def count_similar_schools(
    category_type: str, target_school: pd.Series, comparator_schools: pd.DataFrame
) -> int:
    """
    Count how many schools in the comparator set are similar to the target school.

    Args:
        category_type: Type of category for determining similarity criteria
        target_school: Reference school data
        comparator_schools: DataFrame of schools to compare against

    Returns:
        Count of similar schools
    """
    similar_count = 0
    for _, comparison_school in comparator_schools.iterrows():
        if determine_similarity_comparator(
            category_type, target_school, comparison_school
        ):
            similar_count += 1
    return similar_count


def compute_category_rag_statistics(
    school_urn: str,
    category_name: str,
    rag_settings: Dict[str, Any],
    target_school: pd.Series,
    comparator_set: pd.DataFrame,
    column_cache: CategoryColumnCache,
) -> Generator[Dict[str, Any], None, None]:
    """
    Compute RAG statistics for all subcategories within a category.

    Args:
        school_urn: URN of the school
        category_name: Name of the cost category
        rag_settings: RAG configuration for this category
        target_school: Data for the target school
        comparator_set: Set of schools for comparison
        column_cache: Pre-computed column mappings

    Yields:
        RAG statistics for each subcategory
    """
    ofsted_rating = target_school["OfstedRating (name)"]

    # Count similar schools using appropriate criteria
    similar_schools_count = count_similar_schools(
        rag_settings["type"], target_school, comparator_set
    )

    # Get category data using cached column mappings
    category_mapping = column_cache.category_mappings[category_name]
    category_data = comparator_set.loc[:, category_mapping["column_mask"]]

    # Generate statistics for each subcategory
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
        except (KeyError, ValueError, ZeroDivisionError) as e:
            logger.warning(
                f"Failed to calculate statistics for {school_urn} {subcategory_name}: {e}"
            )
            continue


def prepare_data_for_rag_calculation(data: pd.DataFrame) -> pd.DataFrame:
    """
    Prepare and clean data for RAG calculations.

    Args:
        data: Raw school data

    Returns:
        Cleaned DataFrame ready for RAG computation
    """
    # Select only needed columns
    required_columns = (
        data.columns.isin(BASE_COLUMNS)
        | data.columns.str.endswith("_Per Unit")
        | (data.columns == "Partial Years Present")
    )
    cleaned_data = data.loc[:, required_columns].fillna(0.0)

    return cleaned_data


def should_skip_school_processing(school_data: pd.Series) -> bool:
    """
    Determine if a school should be skipped during RAG processing.

    Args:
        school_data: Data for a single school

    Returns:
        True if school should be skipped (e.g., part-year data)
    """
    # Skip if part-year data present
    return bool(school_data.get("Partial Years Present", False))


def process_single_school_rag(
    school_urn: str,
    processed_data: pd.DataFrame,
    comparator_cache: ComparatorSetCache,
    column_cache: CategoryColumnCache,
) -> Generator[Dict[str, Any], None, None]:
    """
    Process RAG calculations for a single school across all categories.

    Args:
        school_urn: URN of the school to process
        processed_data: Prepared DataFrame with school data
        comparator_cache: Pre-built cache of comparator sets
        column_cache: Pre-computed column mappings

    Yields:
        RAG statistics for each category/subcategory combination
    """
    target_school = processed_data.loc[school_urn]

    for category_name, rag_settings in rag_category_settings.items():
        try:
            # Get appropriate comparator set
            comparator_set = comparator_cache.get_comparator_set(
                school_urn, rag_settings["type"]
            )

            if comparator_set is not None and len(comparator_set) > 0:
                yield from compute_category_rag_statistics(
                    school_urn,
                    category_name,
                    rag_settings,
                    target_school,
                    comparator_set,
                    column_cache,
                )
            else:
                if logger.isEnabledFor(logging.DEBUG):
                    logger.debug(
                        f'Unable to compute RAG for {category_name} - {rag_settings["type"]} - {school_urn}'
                    )
        except (KeyError, ValueError) as e:
            logger.error(
                f"Error processing category {category_name} for school {school_urn}: {e}"
            )
            continue


def calculate_rag(
    data: pd.DataFrame,
    comparators: Dict,
    target_urn: Optional[str] = None,
) -> Generator[Dict[str, Any], None, None]:
    """
    Perform RAG calculations for a dataset.

    Note: RAG will not be calculated for part-year data.

    Args:
        data: Dataset for RAG calculation
        comparators: Supplementary comparator-set data
        target_urn: If generating RAG for a single organization

    Yields:
        RAG information for each (sub-)category
    """
    # Prepare data efficiently
    processed_data = prepare_data_for_rag_calculation(data)

    # Build caches for efficient access
    column_cache = CategoryColumnCache(processed_data.columns)
    comparator_cache = ComparatorSetCache(processed_data, comparators)

    # Determine schools to process
    schools_to_process = [target_urn] if target_urn else processed_data.index

    start_time = time.time()
    processed_count = 0

    with warnings.catch_warnings():
        warnings.simplefilter("ignore", category=RuntimeWarning)
        with np.errstate(invalid="ignore"):
            for school_urn in schools_to_process:
                if school_urn not in processed_data.index:
                    continue

                target_school = processed_data.loc[school_urn]

                # Skip part-year data
                if should_skip_school_processing(target_school):
                    continue

                try:
                    yield from process_single_school_rag(
                        school_urn, processed_data, comparator_cache, column_cache
                    )

                    processed_count += 1

                    # Conditional logging for performance
                    if (
                        processed_count > 1
                        and processed_count % BATCH_LOG_INTERVAL == 0
                        and logger.isEnabledFor(logging.DEBUG)
                    ):
                        elapsed_time = time.time() - start_time
                        logger.debug(
                            f"Processed {processed_count} schools in {elapsed_time:.2f} seconds"
                        )
                        start_time = time.time()

                except ComputationError as e:
                    logger.error(f"RAG computation failed for school {school_urn}: {e}")
                    continue
                except Exception as e:
                    logger.exception(
                        f"Unexpected error processing school {school_urn}: {e}"
                    )
                    continue


def compute_user_defined_rag(
    data: pd.DataFrame,
    target_urn: int,
    comparator_urns: List[int],
) -> Generator[Dict[str, Any], None, None]:
    """
    Perform user-defined RAG calculation.

    Similar to calculate_rag() but uses a user-specified comparator set
    instead of pre-computed comparator sets.

    Args:
        data: Organization data for RAG computation
        target_urn: URN of the reference organization
        comparator_urns: URNs to use as the comparator set

    Yields:
        RAG statistics for each category/subcategory
    """
    # Prepare data
    processed_data = prepare_data_for_rag_calculation(data)
    column_cache = CategoryColumnCache(processed_data.columns)

    start_time = time.time()

    with warnings.catch_warnings():
        warnings.simplefilter("ignore", category=RuntimeWarning)
        with np.errstate(invalid="ignore"):
            if target_urn not in processed_data.index:
                logger.error(f"Target URN {target_urn} not found in data")
                return

            target_school = processed_data.loc[target_urn]

            try:
                # Create comparator set from user-defined URNs
                available_comparator_urns = [
                    urn for urn in comparator_urns if urn in processed_data.index
                ]

                if not available_comparator_urns:
                    logger.warning(f"No valid comparator URNs found for {target_urn}")
                    return

                comparator_set = processed_data.loc[available_comparator_urns]

                for category_name, rag_settings in rag_category_settings.items():
                    yield from compute_category_rag_statistics(
                        target_urn,
                        category_name,
                        rag_settings,
                        target_school,
                        comparator_set,
                        column_cache,
                    )

                if logger.isEnabledFor(logging.DEBUG):
                    elapsed_time = time.time() - start_time
                    logger.debug(
                        f"Completed user-defined RAGs in {elapsed_time:.2f} seconds"
                    )

            except ComputationError as e:
                logger.error(
                    f"User-defined RAG computation failed for {target_urn}: {e}"
                )
                return
            except Exception as e:
                logger.exception(
                    f"Unexpected error in user-defined RAG for {target_urn}: {e}"
                )
                return


def get_positive_values_series(
    data: pd.DataFrame, category_column: str, school_urn: str
) -> pd.Series:
    """
    Filter comparator set data for specified category, retaining only
    positive values or the value for the specified organization.

    This ensures only positive expenditure values are considered for
    RAG calculations while always including the target school's value.

    Args:
        data: DataFrame containing comparator data
        category_column: Column name to filter on
        school_urn: URN of the organization whose value should always be included

    Returns:
        Series containing filtered values
    """
    mask = (data[category_column] > 0) | (data.index == school_urn)
    return data.loc[mask, category_column]
