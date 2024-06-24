import logging

import numpy as np
import pandas as pd

logger = logging.getLogger("fbit-data-pipeline")


# TODO: This should be moved to pre-processing really
def fillna_median(data):
    return data.fillna(data.median())


def prepare_data(data):
    data["Boarders (name)"] = data["Boarders (name)"].map(
        lambda x: "Not Boarding" if x == "Unknown" else x
    )
    data["Number of pupils"] = fillna_median(data["Number of pupils"])
    data["Percentage Free school meals"] = fillna_median(
        data["Percentage Free school meals"]
    )
    data["Percentage SEN"] = fillna_median(data["Percentage SEN"])
    data["Percentage Primary Need SPLD"] = fillna_median(
        data["Percentage Primary Need SPLD"]
    )
    data["Percentage Primary Need MLD"] = fillna_median(
        data["Percentage Primary Need MLD"]
    )
    data["Percentage Primary Need PMLD"] = fillna_median(
        data["Percentage Primary Need PMLD"]
    )
    data["Percentage Primary Need SEMH"] = fillna_median(
        data["Percentage Primary Need SEMH"]
    )
    data["Percentage Primary Need SLCN"] = fillna_median(
        data["Percentage Primary Need SLCN"]
    )
    data["Percentage Primary Need HI"] = fillna_median(
        data["Percentage Primary Need HI"]
    )
    data["Percentage Primary Need MSI"] = fillna_median(
        data["Percentage Primary Need MSI"]
    )
    data["Percentage Primary Need PD"] = fillna_median(
        data["Percentage Primary Need PD"]
    )
    data["Percentage Primary Need ASD"] = fillna_median(
        data["Percentage Primary Need ASD"]
    )
    data["Percentage Primary Need OTH"] = fillna_median(
        data["Percentage Primary Need OTH"]
    )
    data["Total Internal Floor Area"] = fillna_median(data["Total Internal Floor Area"])
    data["Age Average Score"] = fillna_median(data["Age Average Score"])

    return data.sort_index()


def _delta_range_ratio(input: np.array) -> np.array:
    """
    Calculate the ratio of input delta to its range.

    Determine the matrix of absolute differences between all
    combinations in the input, divided by the range of the input.

    :param input: array of data
    :return: the ratio of the delta to the data range
    """
    input_range = np.ptp(input)

    input_column_vector = input[:, None]
    input_row_vector = input[None, :]
    input_diff_matrix = input_column_vector - input_row_vector
    input_delta_matrix = np.abs(input_diff_matrix)

    return input_delta_matrix / input_range


def _delta_range_ratio_squared(input: np.array) -> np.array:
    """
    Calculate the ratio of input delta to its range, squared.

    Determine the square of the matrix of absolute differences between
    all combinations in the input, divided by the range of the input.

    :param input: array of data
    :return: the ratio of the delta to the data range, squared
    """
    return np.power(_delta_range_ratio(input), 2)


def pupils_calc(pupils: np.array, fsm: np.array, sen: np.array) -> np.ndarray:
    """
    Perform pupil calculation (non-special).

    :param pupils: number of pupils
    :param fsm: percentage FSM (Free School Meals)
    :param sen: percentage SEN (Special Education Needs)
    :return: pupil calculation
    """
    pupil = 0.5 * _delta_range_ratio_squared(pupils)
    meal = 0.4 * _delta_range_ratio_squared(fsm)
    sen_ = 0.1 * _delta_range_ratio_squared(sen)

    return np.sqrt(pupil + meal + sen_)


def special_pupils_calc(
    pupils: np.array,
    fsm: np.array,
    splds: np.array,
    mlds: np.array,
    pmlds: np.array,
    semhs: np.array,
    slcns: np.array,
    his: np.array,
    msis: np.array,
    pds: np.array,
    asds: np.array,
    oths: np.array,
) -> np.ndarray:
    """
    Perform pupil calculation (special).

    :param pupils: number of pupils
    :param fsm: percentage FSM (Free School Meals)
    :param splds: percentage SPLD (Specific Learning Difficulty)
    :param mlds: percentage MLD (Moderate Learning Difficulty)
    :param pmlds: percentage PMLD (Profound and Multiple Learning Difficulty)
    :param semhs: percentage SEMH (Social, Emotional and Mental Health Difficulties)
    :param slcns: percentage SLCN (Speech, Language and Communication Needs)
    :param his: percentage HI (Hearing Impairment)
    :param msis: percentage MSI (Multi-Sensory Impairment)
    :param pds: percentage PD (Physical Disability)
    :param asds: percentage ASD (Autistic Spectrum Disorder)
    :param oths: percentage Other
    :return: pupil calculation (special)
    """
    pupil = 0.6 * _delta_range_ratio_squared(pupils)
    meal = 0.4 * _delta_range_ratio_squared(fsm)

    pupils_ = pupil + meal

    spld = _delta_range_ratio_squared(splds)
    mld = _delta_range_ratio_squared(mlds)
    pmld = _delta_range_ratio_squared(pmlds)
    semh = _delta_range_ratio_squared(semhs)
    slcn = _delta_range_ratio_squared(slcns)
    hi = _delta_range_ratio_squared(his)
    msi = _delta_range_ratio_squared(msis)
    pd = _delta_range_ratio_squared(pds)
    asd = _delta_range_ratio_squared(asds)
    oth = _delta_range_ratio_squared(oths)

    sen = spld + mld + pmld + semh + slcn + hi + msi + pd + asd + oth

    return np.sqrt(pupils_) + np.sqrt(sen)


def buildings_calc(gifa: np.array, average_age: np.array) -> np.ndarray:
    """
    Compute area metrics.

    :param gifa: sum of the internal floor areas of the school buildings
    :param average_age: product of age of the school and area
    :return: area metric
    """
    gifa_ = 0.8 * _delta_range_ratio_squared(gifa)
    age = 0.2 * _delta_range_ratio_squared(average_age)

    return np.sqrt(gifa_ + age)


def compute_buildings_comparator(arg) -> np.ndarray:
    """
    Computing area metrics, consuming attributes from the input data.

    :param arg: phase/data
    :return: phase, URNs and calculations
    """
    _, row = arg
    gifa = np.array(row["Total Internal Floor Area"])
    age = np.array(row["Age Average Score"])

    return buildings_calc(gifa, age)


def compute_pupils_comparator(arg) -> np.ndarray:
    """
    Computing pupil metrics, consuming attributes from the input data.

    Calculations will be either "pupil" or "special", using a different
    set of attributes in each case.

    :param arg: phase/data
    :return: phase, URNs and calculations
    """
    phase, row = arg
    pupils = np.array(row["Number of pupils"])
    fsm = np.array(row["Percentage Free school meals"])

    if phase.lower() == "special":
        return special_pupils_calc(
            pupils,
            fsm,
            np.array(row["Percentage Primary Need SPLD"]),
            np.array(row["Percentage Primary Need MLD"]),
            np.array(row["Percentage Primary Need PMLD"]),
            np.array(row["Percentage Primary Need SEMH"]),
            np.array(row["Percentage Primary Need SLCN"]),
            np.array(row["Percentage Primary Need HI"]),
            np.array(row["Percentage Primary Need MSI"]),
            np.array(row["Percentage Primary Need PD"]),
            np.array(row["Percentage Primary Need ASD"]),
            np.array(row["Percentage Primary Need OTH"]),
        )

    sen = np.array(row["Percentage SEN"])
    return pupils_calc(pupils, fsm, sen)


# def select_top_set_urns(
#     urns: np.array,
#     pfi: np.array,
#     boarding: np.array,
#     regions: np.array,
#     distances: np.ndarray,
#     base_set_size=60,
#     final_set_size=30,
# ) -> np.ndarray:
#     """
#     Determine the URNs of the closest orgs.
#
#     1. take the top (by default) 60 orgs. closest by distance metrics,
#        with the same PFI and Boarding status.
#     2. reduce this to those in the same region.
#     3. if fewer than (by default) 30 results, supplement this with the
#        closest by distance metrics from the original set.
#
#     :param urns: URN of each org. in this grouping.
#     :param pfi: PFI status of each org. in this grouping.
#     :param boarding: boarding status of each org. in this grouping.
#     :param regions: regional location of each org. in this grouping.
#     :param distances: computed distances of each org. from the first.
#     :return: URNs of "top" orgs. meeting criteria, orderd by distance.
#     """
#     index_by_distance = np.argsort(distances, axis=0, kind="stable")
#     urns_by_distance = urns[index_by_distance]
#
#     pfi_by_distance = pfi[index_by_distance]
#     boarding_by_distance = boarding[index_by_distance]
#     same_pfi = pfi_by_distance == pfi_by_distance[0]
#     same_boarding = boarding_by_distance == boarding_by_distance[0]
#     same_criteria = np.logical_and(
#         same_pfi,
#         same_boarding,
#     )
#     top_criteria_index = np.argwhere(same_criteria).flatten()[:base_set_size]
#
#     same_region_index = np.argwhere(regions == regions[0]).flatten()
#     top_regions_index = np.intersect1d(
#         top_criteria_index,
#         same_region_index,
#     )
#     same_region_urns = urns_by_distance[top_regions_index][:final_set_size]
#
#     urns = np.append(
#         same_region_urns,
#         np.delete(
#             urns_by_distance,
#             same_region_index,
#         )[: final_set_size - len(same_region_urns)],
#     )
#
#     return urns


def select_top_set_urns(
    index: int,
    urns: np.array,
    pfi: np.array,
    boarding: np.array,
    regions: np.array,
    distances: np.ndarray,
    base_set_size=60,
    final_set_size=30,
) -> np.ndarray:
    """
    Determine the URNs of the closest orgs.

    1. take the top (by default) 60 orgs. closest by distance metrics,
       with the same PFI and Boarding status.
    2. reduce this to those in the same region.
    3. if fewer than (by default) 30 results, supplement this with the
       closest by distance metrics from the original set.

    :param index: The index of the target URN in this grouping.
    :param urns: URN of each org. in this grouping.
    :param pfi: PFI status of each org. in this grouping.
    :param boarding: boarding status of each org. in this grouping.
    :param regions: regional location of each org. in this grouping.
    :param distances: computed distances of each org. from the first.
    :param base_set_size: The size of the initial sorted set for filtering.
    :param final_set_size: The final desired size of the comparator set.
    :return: URNs of "top" orgs. meeting criteria, ordered by distance.
    """
    target_region = regions[index]
    target_pfi = pfi[index]
    target_boarding = boarding[index]

    distance_without_urn = np.delete(distances, index)
    urns_without_urn = np.delete(urns, index)
    regions_without_urn = np.delete(regions, index)
    pfi_without_urn = np.delete(pfi, index)
    boarding_without_urn = np.delete(boarding, index)

    index_by_distance = np.argsort(distance_without_urn, axis=0, kind="stable")[
        :base_set_size
    ]
    urns_by_distance = urns_without_urn[index_by_distance]

    # URNS is the result array we will build up
    urns = np.array(urns[index])

    if target_pfi or target_boarding == "Boarding":
        if target_pfi:
            top_pfi = pfi_without_urn[index_by_distance]
            same_pfi = np.argwhere(top_pfi).flatten()
            urns = np.append(urns, urns_by_distance[same_pfi])

        if target_boarding == "Boarding":
            top_boarding = boarding_without_urn[index_by_distance]
            same_boarding = np.argwhere(top_boarding == target_boarding).flatten()
            urns = np.append(urns, urns_by_distance[same_boarding])

    top_regions = regions_without_urn[index_by_distance]
    same_region = np.argwhere(top_regions == target_region).flatten()
    urns = np.append(urns, urns_by_distance[same_region])

    if len(urns) >= final_set_size:
        return urns[:final_set_size]

    # Fill up with the rest of the URNs using the distance metric
    urns = np.append(
        urns,
        np.delete(urns_by_distance, same_region)[: final_set_size - len(urns)],
    )

    return urns


# TODO: rename, this does more than distances.
def compute_distances(
    orig_data: pd.DataFrame,
    grouped_data: pd.DataFrame,
    target_urn: int | None = None,
):
    # TODO: docstring needed to explain this.
    pupils = pd.Series(index=orig_data.index, dtype=object)
    buildings = pd.Series(index=orig_data.index, dtype=object)

    for phase, row in grouped_data.iterrows():
        if target_urn and target_urn not in row["URN"]:
            continue

        # compute distances for all orgs. in this phase.
        pupil_distances = compute_pupils_comparator((phase, row))
        building_distances = compute_buildings_comparator((phase, row))
        phase_urns = np.array(row["URN"])
        phase_pfi = np.array(row["Is PFI"])
        phase_boarding = np.array(row["Boarders (name)"])
        phase_regions = np.array(row["GOR (name)"])

        # TODO: compares ab/ba and aa.
        # compute best-set for each org. individually.
        for idx, urn in enumerate(phase_urns):
            if target_urn and urn != target_urn:
                continue

            try:
                top_pupil_set_urns = select_top_set_urns(
                    idx,
                    phase_urns,
                    phase_pfi,
                    phase_boarding,
                    phase_regions,
                    pupil_distances[idx],
                )
                top_building_set_urns = select_top_set_urns(
                    idx,
                    phase_urns,
                    phase_pfi,
                    phase_boarding,
                    phase_regions,
                    building_distances[idx],
                )

                pupils.loc[urn] = top_pupil_set_urns
                buildings.loc[urn] = top_building_set_urns

                break
            except Exception as error:
                logger.exception(
                    f"An exception occurred {type(error).__name__} processing {urn}:",
                    exc_info=error,
                )
                return

    orig_data["Pupil"] = pupils
    orig_data["Building"] = buildings

    if target_urn:
        return orig_data.loc[[target_urn]]

    return orig_data


def compute_comparator_set(
    data: pd.DataFrame,
    target_urn: int | None = None,
) -> pd.DataFrame:
    """
    Determine the comparator-sets for the input data.

    TODO: explain the below columns.

    Perform comparator-set derivation, restricted by "phase".

    If `data` is "custom" data, the resulting output will be restricted
    to just the `target_urn`.

    :param data: data for which to determine the comparator-sets
    :param target_urn: optional identifier for custom data
    :return: data, updated with comparator-sets
    """
    copy = data[~data.index.isna()][
        [
            "OfstedRating (name)",
            "Percentage SEN",
            "Percentage Free school meals",
            "Number of pupils",
            "Total Internal Floor Area",
            "Age Average Score",
            "Is PFI",
            "Boarders (name)",
            "GOR (name)",
            "SchoolPhaseType",
            "Percentage Primary Need SPLD",
            "Percentage Primary Need MLD",
            "Percentage Primary Need PMLD",
            "Percentage Primary Need SEMH",
            "Percentage Primary Need SLCN",
            "Percentage Primary Need HI",
            "Percentage Primary Need MSI",
            "Percentage Primary Need PD",
            "Percentage Primary Need ASD",
            "Percentage Primary Need OTH",
        ]
    ].copy()

    classes = copy.reset_index().groupby(["SchoolPhaseType"]).agg(list)

    return compute_distances(copy, classes, target_urn=target_urn)
