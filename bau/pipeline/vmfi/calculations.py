import numpy as np
import pandas as pd


def compute_range(arr):
    return np.full((1, np.size(arr)), np.ptp(arr)).flatten()


def pairwise_index(x):
    return np.stack(np.triu_indices(len(x)), axis=-1)


def non_special_distance_calc(urns, pupils, fsm, sen, pupil_range, fsm_range, sen_range):
    combs = pairwise_index(urns)
    distances = np.empty((len(combs), 3))

    index = 0
    for comb in combs:
        x, y = comb[0], comb[1]
        pupil = 0.5 * pow(abs(pupils[x] - pupils[y]) / pupil_range[x], 2)
        meal = 0.4 * pow(abs(fsm[x] - fsm[y]) / fsm_range[x], 2)
        sens = 0.1 * pow(abs(sen[x] - sen[y]) / sen_range[x], 2)
        dist = pow(pupil + meal + sens, 0.5)
        distances[index] = [urns[x], urns[y], dist]
        index += 1

    return distances
