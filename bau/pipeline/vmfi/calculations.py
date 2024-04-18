import numpy as np
import multiprocessing as mp
from multiprocessing.pool import ThreadPool as Pool

def compute_range(data):
    return data.max() - data.min()


def fillna_mean(data):
    return data.fillna(data.mean())


def prepare_data(data):
    data['Boarders (name)'] = data['Boarders (name)'].map(lambda x: 'Not Boarding' if x == 'Unknown' else x)
    data['Number of pupils'] = fillna_mean(data['Number of pupils'])
    data['Percentage Free school meals'] = fillna_mean(data['Percentage Free school meals'])
    data['Percentage SEN'] = fillna_mean(data['Percentage SEN'])
    data['Prov_SPLD'] = fillna_mean(data['Prov_SPLD'])
    data['Prov_MLD'] = fillna_mean(data['Prov_MLD'])
    data['Prov_PMLD'] = fillna_mean(data['Prov_PMLD'])
    data['Prov_SEMH'] = fillna_mean(data['Prov_SEMH'])
    data['Prov_SLCN'] = fillna_mean(data['Prov_SLCN'])
    data['Prov_HI'] = fillna_mean(data['Prov_HI'])
    data['Prov_MSI'] = fillna_mean(data['Prov_MSI'])
    data['Prov_PD'] = fillna_mean(data['Prov_PD'])
    data['Prov_ASD'] = fillna_mean(data['Prov_ASD'])
    data['Prov_OTH'] = fillna_mean(data['Prov_OTH'])
    return data.set_index('URN').sort_index()


def pupils_calc(pupils, fsm, sen):
    pupil_range = compute_range(pupils)
    fsm_range = compute_range(fsm)
    sen_range = compute_range(sen)

    pupil = 0.5 * np.power(np.abs(pupils[:, None] - pupils[None, :]) / pupil_range, 2)
    meal = 0.4 * np.power(np.abs(fsm[:, None] - fsm[None, :]) / fsm_range, 2)
    sen = 0.1 * np.power(np.abs(sen[:, None] - sen[None, :]) / sen_range, 2)
    return np.power(pupil + meal + sen, 0.5)


def special_pupils_calc(pupils, fsm, splds, mlds, pmlds, semhs, slcns, his, msis, pds, asds, oths):
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


def compute_pupils_comparator(arg):
    idx, row = arg
    phase = idx
    pupils = np.array(row['Number of pupils'])
    fsm = np.array(row['Percentage Free school meals'])

    if phase.lower() == 'special':
        Prov_SPLD = np.array(row['Prov_SPLD'])
        Prov_MLD = np.array(row['Prov_MLD'])
        Prov_PMLD = np.array(row['Prov_PMLD'])
        Prov_SEMH = np.array(row['Prov_SEMH'])
        Prov_SLCN = np.array(row['Prov_SLCN'])
        Prov_HI = np.array(row['Prov_HI'])
        Prov_MSI = np.array(row['Prov_MSI'])
        Prov_PD = np.array(row['Prov_PD'])
        Prov_ASD = np.array(row['Prov_ASD'])
        Prov_OTH = np.array(row['Prov_OTH'])
        return idx, special_pupils_calc(pupils, fsm, Prov_SPLD, Prov_MLD, Prov_PMLD, Prov_SEMH,
                                                               Prov_SLCN, Prov_HI, Prov_MSI, Prov_PD, Prov_ASD,
                                                               Prov_OTH)
    else:
        sen = np.array(row['Percentage SEN'])
        return idx, pupils_calc(pupils, fsm, sen)


def compute_pupils_comparator_matrix(data):
    # TODO: This needs to be grouped correctly not really sure how the
    #  region / phase / boarding / non-boarding are all
    # related, it feels like we could just compute the School Phase Type groups
    # everything else inside that can just be filtered after the fact
    classes = data.groupby(['SchoolPhaseType']).agg(list)

    distance_classes = {
        'urns': data.index.values
    }

    with Pool(mp.cpu_count()) as pool:
        for (idx, distance) in pool.map(compute_pupils_comparator, classes.iterrows()):
            distance_classes[idx] = distance

    return distance_classes


def compute_custom_comparator(name, data):
    copy = data.copy()
    copy['Grouper'] = name
    classes = copy.groupby(['Grouper']).agg(list)

    distance_classes = {
        'urns': copy.index.values
    }
    with Pool(mp.cpu_count()) as pool:
        for (idx, distance) in pool.map(compute_pupils_comparator, classes.iterrows()):
            distance_classes[idx] = distance

    return distance_classes
