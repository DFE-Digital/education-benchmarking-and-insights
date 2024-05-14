import numpy as np
from typing import Callable

import pandas as pd
import dask.distributed as dask


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
    data["Prov_SPLD"] = fillna_median(data["Prov_SPLD"])
    data["Prov_MLD"] = fillna_median(data["Prov_MLD"])
    data["Prov_PMLD"] = fillna_median(data["Prov_PMLD"])
    data["Prov_SEMH"] = fillna_median(data["Prov_SEMH"])
    data["Prov_SLCN"] = fillna_median(data["Prov_SLCN"])
    data["Prov_HI"] = fillna_median(data["Prov_HI"])
    data["Prov_MSI"] = fillna_median(data["Prov_MSI"])
    data["Prov_PD"] = fillna_median(data["Prov_PD"])
    data["Prov_ASD"] = fillna_median(data["Prov_ASD"])
    data["Prov_OTH"] = fillna_median(data["Prov_OTH"])
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


def compute_pupils_comparator(arg) -> tuple[str, np.array, np.array]:
    """
    Computing pupil metrics, consuming attributes from the input data.

    Calculations will be either "pupil" or "special", using a different
    set of attributes in each case.

    :param arg: phase/data
    :return: phase, URNs and calculations
    """
    phase, row = arg
    urns = np.array(row["URN"])
    pupils = np.array(row["Number of pupils"])
    fsm = np.array(row["Percentage Free school meals"])

    if phase.lower() == "special":
        Prov_SPLD = np.array(row["Prov_SPLD"])
        Prov_MLD = np.array(row["Prov_MLD"])
        Prov_PMLD = np.array(row["Prov_PMLD"])
        Prov_SEMH = np.array(row["Prov_SEMH"])
        Prov_SLCN = np.array(row["Prov_SLCN"])
        Prov_HI = np.array(row["Prov_HI"])
        Prov_MSI = np.array(row["Prov_MSI"])
        Prov_PD = np.array(row["Prov_PD"])
        Prov_ASD = np.array(row["Prov_ASD"])
        Prov_OTH = np.array(row["Prov_OTH"])
        return (
            phase,
            urns,
            special_pupils_calc(
                pupils,
                fsm,
                Prov_SPLD,
                Prov_MLD,
                Prov_PMLD,
                Prov_SEMH,
                Prov_SLCN,
                Prov_HI,
                Prov_MSI,
                Prov_PD,
                Prov_ASD,
                Prov_OTH,
            ),
        )

    sen = np.array(row["Percentage SEN"])
    return phase, urns, pupils_calc(pupils, fsm, sen)


def compute_buildings_comparator(arg):
    """
    Computing area metrics, consuming attributes from the input data.

    :param arg: phase/data
    :return: phase, URNs and calculations
    """
    phase, row = arg
    urns = np.array(row["URN"])
    gifa = np.array(row["Total Internal Floor Area"])
    age = np.array(row["Age Average Score"])

    return phase, urns, buildings_calc(gifa, age)


def compute_distances(data, f) -> pd.DataFrame:
    distance_classes = {}

    for idx, row in data.iterrows():
        idx, urns, distance = f((idx, row))
        distance_classes[idx] = {"shape": np.shape(distance), "urns": urns, "distances": np.ravel(distance)}

    return pd.DataFrame.from_dict(distance_classes)


def compute_comparator_matrix(
    data: pd.DataFrame,
    comparator_function: Callable,
    comparator_key="SchoolPhaseType",
):
    copy = data.reset_index()
    classes = copy.groupby([comparator_key]).agg(list)
    return compute_distances(classes, comparator_function)


def compute_custom_comparator(name, data, f):
    copy = data.reset_index()
    copy["Custom"] = name
    classes = copy.groupby(["Custom"]).agg(list)
    return compute_distances(classes, f)


def get_comparator_set_by(
    school_selector,
    schools,
    comparators,
    is_custom=False,
    comparator_key="SchoolPhaseType",
):
    school_no_index = schools.reset_index()
    school = (
        school_no_index[school_selector(schools)].to_dict(orient="records")[0]
    )

    phase_data = (
        comparators[comparator_key]
        if is_custom
        else comparators[school[comparator_key]]
    )

    col_index = np.argwhere(phase_data["urns"] == school["URN"])[0][0]
    shape = phase_data["shape"][0].astype(int), phase_data["shape"][1].astype(int)
    data = np.reshape(phase_data["distances"], tuple(shape))[col_index]

    top_60_index = np.argsort(data)[:60]
    distances = data[top_60_index]
    urns = phase_data["urns"][top_60_index]

    d = []
    idx = 0
    for urn in urns:
        row = school_no_index[school_no_index["URN"] == urn].to_dict(orient="records")[
            0
        ]
        row["Distance"] = distances[idx]
        d.append(row)
        idx += 1

    all_comparators = pd.DataFrame(d)
    same_region = all_comparators[
        all_comparators["GOR (name)"] == school["GOR (name)"]
    ].head()
    out_of_region = (
        all_comparators[all_comparators["GOR (name)"] != school["GOR (name)"]]
        .sort_values(by="Distance", ascending=True)
        .head(30 - len(same_region))
    )
    return pd.concat([same_region, out_of_region])
