import multiprocessing as mp
from multiprocessing.pool import Pool

import numpy as np
import pandas as pd


def compute_range(data):
    return data.max() - data.min()


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


def pupils_calc(pupils, fsm, sen):
    pupil_range = compute_range(pupils)
    fsm_range = compute_range(fsm)
    sen_range = compute_range(sen)

    print(pupil_range, fsm_range, sen_range)
    pupil = 0.5 * np.power(np.abs(pupils[:, None] - pupils[None, :]) / pupil_range, 2)
    meal = 0.4 * np.power(np.abs(fsm[:, None] - fsm[None, :]) / fsm_range, 2)
    sen = 0.1 * np.power(np.abs(sen[:, None] - sen[None, :]) / sen_range, 2)
    return np.power(pupil + meal + sen, 0.5)


def special_pupils_calc(
    pupils, fsm, splds, mlds, pmlds, semhs, slcns, his, msis, pds, asds, oths
):
    pupil_range = compute_range(pupils)
    fsm_range = compute_range(fsm)
    spld_range = compute_range(splds)
    mld_range = compute_range(mlds)
    pmld_range = compute_range(pmlds)
    semh_range = compute_range(semhs)
    slcn_range = compute_range(slcns)
    hi_range = compute_range(his)
    msi_range = compute_range(msis)
    pd_range = compute_range(pds)
    asd_range = compute_range(asds)
    oth_range = compute_range(oths)

    pupil = 0.6 * np.power(np.abs(pupils[:, None] - pupils[None, :]) / pupil_range, 2)
    meal = 0.4 * np.power(np.abs(fsm[:, None] - fsm[None, :]) / fsm_range, 2)
    base1 = np.power(pupil + meal, 0.5)

    spld = np.power(np.abs(splds[:, None] - splds[None, :]) / spld_range, 2)
    mld = np.power(np.abs(mlds[:, None] - mlds[None, :]) / mld_range, 2)
    pmld = np.power(np.abs(pmlds[:, None] - pmlds[None, :]) / pmld_range, 2)
    semh = np.power(np.abs(semhs[:, None] - semhs[None, :]) / semh_range, 2)
    slcn = np.power(np.abs(slcns[:, None] - slcns[None, :]) / slcn_range, 2)
    hi = np.power(np.abs(his[:, None] - his[None, :]) / hi_range, 2)
    msi = np.power(np.abs(msis[:, None] - msis[None, :]) / msi_range, 2)
    pd = np.power(np.abs(pds[:, None] - pds[None, :]) / pd_range, 2)
    asd = np.power(np.abs(asds[:, None] - asds[None, :]) / asd_range, 2)
    oth = np.power(np.abs(oths[:, None] - oths[None, :]) / oth_range, 2)

    base2 = np.power(spld + mld + pmld + semh + slcn + hi + msi + pd + asd + oth, 0.5)

    return base1 + base2


def buildings_calc(gifa, average_age) -> np.array:
    gifa_range = compute_range(gifa)
    average_age_range = compute_range(average_age)

    gifa = 0.8 * np.power(np.abs(gifa[:, None] - gifa[None, :]) / gifa_range, 2)
    age = 0.2 * np.power(
        np.abs(average_age[:, None] - average_age[None, :]) / average_age_range, 2
    )

    return np.power(gifa + age, 0.5)


def compute_pupils_comparator(arg) -> (str, np.array, np.array):
    idx, row = arg
    phase = idx
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
    else:
        sen = np.array(row["Percentage SEN"])
        return phase, urns, pupils_calc(pupils, fsm, sen)


def compute_buildings_comparator(arg):
    idx, row = arg
    phase = idx
    urns = np.array(row["URN"])
    gifa = np.array(row["Total Internal Floor Area"])
    age = np.array(row["Age Average Score"])

    return phase, urns, buildings_calc(gifa, age)


def run_comparator(data, f):
    distance_classes = {}

    with Pool(mp.cpu_count()) as pool:
        for idx, urns, distance in pool.map(f, data.iterrows()):
            distance_classes[idx] = {"urns": urns, "distances": distance}

    return distance_classes


def compute_comparator_matrix(data, f, comparator_key="SchoolPhaseType"):
    copy = data.reset_index()
    classes = copy.groupby([comparator_key]).agg(list)
    return run_comparator(classes, f)


def compute_custom_comparator(name, data, f):
    copy = data.reset_index()
    copy["Custom"] = name
    classes = copy.groupby(["Custom"]).agg(list)
    return run_comparator(classes, f)


def get_comparator_set_by(
    school_selector,
    schools,
    comparators,
    is_custom=False,
    comparator_key="SchoolPhaseType",
):
    school_no_index = schools.reset_index()
    school = (
        schools[school_selector(schools)].reset_index().to_dict(orient="records")[0]
    )

    phase_data = (
        comparators[comparator_key]
        if is_custom
        else comparators[school[comparator_key]]
    )

    col_index = np.argwhere(phase_data["urns"] == school["URN"])[0][0]
    data = phase_data["distances"][col_index]

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
