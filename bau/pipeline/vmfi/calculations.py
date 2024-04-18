import numpy as np

def range (data):
    data.max() - data.min()


def non_special_distance_calc(pupils, fsm, sen):
    pupil_range = range(pupils)
    fsm_range = range(fsm)
    sen_range = range(sen)

    pupil = 0.5 * np.power(np.abs(pupils[:, None] - pupils[None, :]) / pupil_range, 2)
    meal = 0.4 * np.power(np.abs(fsm[:, None] - fsm[None, :]) / fsm_range, 2)
    sen = 0.1 * np.power(np.abs(sen[:, None] - sen[None, :]) / sen_range, 2)
    return np.power(pupil + meal + sen, 0.5)


def special_distance_calc(pupils, fsm, splds, mlds, pmlds, semhs, slcns, his, msis, pds, asds, oths):
    pupil_range = range(pupils)
    fsm_range = range(fsm)
    spld_range = range(splds)
    mld_range = range(mlds)
    pmld_range = range(pmlds)
    semh_range = range(semhs)
    slcn_range = range(slcns)
    hi_range = range(his)
    msi_range = range(msis)
    pd_range = range(pds)
    asd_range = range(asds)
    oth_range = range(oths)

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

