"""
The euclidean distancing calculator used to determine the benchmarking sets for given schools.
"""

from controllers.data_load_controller import DataLoadController
from config.config import Config
import pandas as pd
import numpy as np


class AreaBenchmarkingSetCalculatorHelper():

    def __init__(self):
        self.config = Config()
        self.data_load_controller = DataLoadController()
        self.sen = self.data_load_controller.load_data('SEN')
        self.sen = self.sen.loc[self.sen['time_period'] == self.config.time_period]
    
        self.benchmarking_groups = {}
    
    def _get_parameter_range(self, df: pd.DataFrame, parameter_list: list) -> dict:
        self.parameter_range = {}
        for item in parameter_list:
            self.parameter_range[item] = np.max(df[item]) - np.min(df[item])
        return self.parameter_range

    def _get_parameter_delta_matrix(self, df: pd.DataFrame, parameter_list: list, i1: int=None, i2: int=None) -> dict:
        self.parameter_delta_matrix = {}
        if i1 == None:
            for item in parameter_list:
                self.parameter_delta_matrix[item] = np.array(df[item])[:, np.newaxis] - np.array(df[item])
        else:
            for item in parameter_list:
                self.parameter_delta_matrix[item] = np.array(df[item][i1:i2])[:, np.newaxis] - np.array(df[item])
        return self.parameter_delta_matrix

    def _compute_parameter_matrix(self, df: pd.DataFrame, parameter_list: list, weighting_list: list) -> dict:
        self.parameter_matrix = {}
        for i,item in enumerate(parameter_list):
            self.parameter_matrix[item] = ((self.parameter_delta_matrix[item]/self.parameter_range[item])**2) * weighting_list[i]
        return self.parameter_matrix
    
    def _replace_matrix_nans(self) -> dict:
        for item in self.parameter_matrix:
            self.parameter_matrix[item] = np.nan_to_num(self.parameter_matrix[item].T, nan = np.nanmedian(self.parameter_matrix[item], axis=1)).T
        return self.parameter_matrix

    def _compute_distance_matrix(self, special: bool) -> np.ndarray:
        distance_matrix = np.sqrt(np.sum([self.parameter_matrix[item] for item in self.config.school_area_parameter_list], axis=0))
        return distance_matrix
    
    def get_benchmark_sets(self, grouped_data) -> dict:
        for i in range(len(grouped_data)):
            
            df = grouped_data.iloc[i]
            print('computing set: ', grouped_data.iloc[i].name)
            print("number of schools in set: ", len(df['UKPRN_URN']))
            
            # get parameter ranges
            self.parameter_range = self._get_parameter_range(df, self.config.school_area_parameter_list)

            block_size = 5000
            if len(df['UKPRN_URN']) > block_size:
                # incrementors to track progress through blocking
                i1, i2, counter = -block_size, 0, 0
                # dictionaries to track the closest_blocks and the index (for entire matrix)
                
                while counter != len(df['UKPRN_URN']):
                    i1 += block_size
                    # catch to deal with last iteration which will be < block size
                    if counter + block_size < len(df['UKPRN_URN']):
                        i2 += block_size
                        counter += block_size
                    else:
                        i2 += (len(df['UKPRN_URN']) - counter)
                        counter += (len(df['UKPRN_URN']) - counter)
                        
                    # compute euclidean distance metrics
                    self.parameter_delta_matrix = self._get_parameter_delta_matrix(df,self.config.school_area_parameter_list,i1=i1,i2=i2)
        
                    # euclidean distance coefficients with weightings
                    self.parameter_matrix = self._compute_parameter_matrix(df, self.config.school_area_parameter_list, self.config.school_area_parameter_weightings)

                    # how do we want to deal with nans? currently replace with the median
                    # value from that school's matrix row
                    self.parameter_matrix = self._replace_matrix_nans()

                    # euclidean distance matrix
                    distance_matrix = self._compute_distance_matrix(False)

                    for j in range(len(distance_matrix)):
                        distance_matrix[j][i1+j] += 1e8
                    
                    if counter == block_size:
                        closest = np.argsort(distance_matrix)[:,0:30]
                    else:
                        closest = np.concatenate((closest, np.argsort(distance_matrix)[:,0:30]), axis=0)
                    
            else:
                self.parameter_delta_matrix = self._get_parameter_delta_matrix(df,self.config.school_area_parameter_list)
                
                self.parameter_matrix = self._compute_parameter_matrix(df, self.config.school_area_parameter_list, self.config.school_area_parameter_weightings)
                
                self.parameter_matrix = self._replace_matrix_nans()
            
                distance_matrix = self._compute_distance_matrix(False)

                closest = np.argsort(distance_matrix + np.identity(len(distance_matrix)) * 1e8)[:,0:30]
            self.benchmarking_groups[grouped_data.iloc[i].name] = closest

        return self.benchmarking_groups
