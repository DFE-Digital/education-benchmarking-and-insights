import numpy as np
import pandas as pd
import logging

logger = logging.getLogger("fbit-data-pipeline:comparator-set")
logger.setLevel(logging.INFO)


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


def pupils_calc(pupils: np.array, fsm: np.array, sen: np.array):
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
):
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


def buildings_calc(gifa: np.array, average_age: np.array) -> np.array:
    """
    Compute area metrics.

    :param gifa: sum of the internal floor areas of the school buildings
    :param average_age: product of age of the school and area
    :return: area metric
    """
    gifa_ = 0.8 * _delta_range_ratio_squared(gifa)
    age = 0.2 * _delta_range_ratio_squared(average_age)

    return np.sqrt(gifa_ + age)


def compute_buildings_comparator(arg):
    """
    Computing area metrics, consuming attributes from the input data.

    :param arg: phase/data
    :return: phase, URNs and calculations
    """
    phase, row = arg
    gifa = np.array(row["Total Internal Floor Area"])
    age = np.array(row["Age Average Score"])

    return buildings_calc(gifa, age)


def compute_pupils_comparator(arg) -> tuple[str, np.array, np.array]:
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


def select_top_set(all_urns, all_regions, data, base_set_size=60, final_set_size=30):
    top_index = np.argsort(data, axis=0, kind="stable")[:base_set_size]
    top_urns = all_urns[top_index]
    top_regions = all_regions[top_index]
    same_region = np.argwhere(top_regions == top_regions[0]).flatten()
    same_region_urns = top_urns[same_region]
    urns = np.append(
        same_region_urns,
        np.delete(top_urns, same_region)[: final_set_size - len(same_region_urns)],
    )
    return urns


def compute_distances(orig_data, grouped_data):
    pupils = pd.Series(index=orig_data.index, dtype=object)
    buildings = pd.Series(index=orig_data.index, dtype=object)
    for phase, row in grouped_data.iterrows():
        pupil_distance = compute_pupils_comparator((phase, row))
        building_distance = compute_buildings_comparator((phase, row))
        all_urns = np.array(row["UKPRN"])
        all_regions = np.array(row["GOR (name)"])

        for idx in range(len(all_urns)):
            ukprn = all_urns[idx]
            try:
                pupil_set = select_top_set(all_urns, all_regions, pupil_distance[idx])
                building_set = select_top_set(
                    all_urns, all_regions, building_distance[idx]
                )

                pupils.loc[ukprn] = pupil_set
                buildings.loc[ukprn] = building_set
            except Exception as error:
                logger.exception(
                    f"An exception occurred processing {ukprn}:",
                    type(error).__name__,
                    "–",
                    error,
                )
                return

    orig_data["Pupil"] = pupils
    orig_data["Building"] = buildings

    return orig_data


def compute_comparator_set(data: pd.DataFrame):
    # TODO: Drop_duplicates should not be needed here.
    # TODO: Need to add boarding and PFI groups into this logic
    copy = (
        data[~data.index.isna()][
            [
                "OfstedRating (name)",
                "Percentage SEN",
                "Percentage Free school meals",
                "Number of pupils",
                "Total Internal Floor Area",
                "Age Average Score",
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
        ]
        .drop_duplicates(ignore_index=False)
        .copy()
    )
    classes = copy.reset_index().groupby(["SchoolPhaseType"]).agg(list)
    return compute_distances(copy, classes)


def compute_custom_comparator_set(data: pd.DataFrame):
    copy = (
        data[
            [
                "OfstedRating (name)",
                "Percentage SEN",
                "Percentage Free school meals",
                "Number of pupils",
                "Total Internal Floor Area",
                "Age Average Score",
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
        ]
        .drop_duplicates(ignore_index=False)
        .copy()
    )
    copy["Custom"] = "Grouper"
    classes = copy.reset_index().groupby(["Custom"]).agg(list)
    return compute_distances(copy, classes)
