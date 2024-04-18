import numpy as np


def non_special_distance_calc(pupils, fsm, sen):
    pupil_range = pupils.max() - pupils.min()
    fsm_range = fsm.max() - fsm.min()
    sen_range = sen.max() - sen.min()

    pupil = 0.5 * np.power(np.abs(pupils[:, None] - pupils[None, :]) / pupil_range, 2)
    meal = 0.4 * np.power(np.abs(fsm[:, None] - fsm[None, :]) / fsm_range, 2)
    sen = 0.1 * np.power(np.abs(sen[:, None] - sen[None, :]) / sen_range, 2)
    return np.power(pupil + meal + sen, 0.5)
