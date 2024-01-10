"""
The euclidean distancing calculator used to determine the benchmarking sets for given schools.
"""

from controllers.data_load_controller import DataLoadController
from config.config import Config
import pandas as pd
import numpy as np


class PupilBenchmarkingSetCalculatorHelper():

    def __init__(self):
        self.config = Config()
        self.data_load_controller = DataLoadController()
        self.sen = self.data_load_controller.load_data('SEN')
        self.sen = self.sen.loc[self.sen['time_period'] == self.config.time_period]
    
        self.benchmarking_groups = {}


    def _get_percentage_sen_breakdowns(self, df: pd.DataFrame, sen_breakdown_list:list) -> pd.DataFrame:
        for item in sen_breakdown_list:
            df[item] = df[item]/df['NumberOfPupils']
        return df
    
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
        if special:
            distance_matrix = np.sqrt(np.sum([self.parameter_matrix[item] for item in self.config.special_school_parameter_list[0:2]], axis=0)) + \
                np.sqrt(np.sum([self.parameter_matrix[item] for item in self.config.special_school_parameter_list[2:]], axis=0))
        else:
            distance_matrix = np.sqrt(np.sum([self.parameter_matrix[item] for item in self.config.school_parameter_list], axis=0))
        return distance_matrix
    

    def get_benchmark_sets(self, grouped_data) -> dict:
        for i in range(len(grouped_data)):
            
            series = grouped_data.iloc[i]
            
            if 'Special' in grouped_data.iloc[i].name:
                
                series['URN'] = [int(item[-6:]) for item in series['UKPRN_URN']]

                # convert df (series) to temp dict, then to dataframe
                tmp_df_dict = {}
                for ii in range(len(series)):
                    tmp_df_dict[series.index[ii]] = series[ii]

                df2 = pd.DataFrame(tmp_df_dict)

                # merge 
                df2 = pd.merge(df2, self.sen, on='URN', how='left')
                # convert back to series
                tmp_df_dict = {}
                for ii in range(len(df2.columns)):
                    tmp_df_dict[df2.columns[ii]] = df2[df2.columns[ii]].values
                series = pd.Series(tmp_df_dict)
                series.name = grouped_data.iloc[i].name

                series = self._get_percentage_sen_breakdowns(series, self.config.sen_breakdown_list)
            
                print('\ncomputing special set: ', grouped_data.iloc[i].name)
                print("number of schools in set: ", len(series['UKPRN_URN']))
                
                self.parameter_range = self._get_parameter_range(series, self.config.special_school_parameter_list)

                # apply non-sen algorithm for each school
                block_size = 5000
                if len(series['NumberOfPupils']) > block_size:
                    # incrementors to track progress through blocking
                    i1, i2, counter = -block_size, 0, 0
                    # dictionaries to track the closest_blocks and the index (for entire matrix)
                    
                    while counter != len(series['NumberOfPupils']):
                        i1 += block_size
                        # catch to deal with last iteration which will be < block size
                        if counter + block_size < len(series['NumberOfPupils']):
                            i2 += block_size
                            counter += block_size
                        else:
                            i2 += (len(series['NumberOfPupils']) - counter)
                            counter += (len(series['NumberOfPupils']) - counter)
                            
                        # compute euclidean distance metrics
                        
                        self.parameter_delta_matrix = self._get_parameter_delta_matrix(series,self.config.special_school_parameter_list,i1=i1,i2=i2)

                        # euclidean distance coefficients with weightings

                        self.parameter_matrix = self._compute_parameter_matrix(series, self.config.special_school_parameter_list, self.config.special_school_parameter_weightings)
                        
                        # how do we want to deal with nans? currently replace with the median
                        # value from that school's matrix row

                        self.parameter_matrix = self._replace_matrix_nans()

                        # euclidean distance matrix
                        distance_matrix = self._compute_distance_matrix(True)
   
                        for j in range(len(distance_matrix)):
                            distance_matrix[j][i1+j] += 1e8
                        
                        if counter == block_size:
                            closest = np.argsort(distance_matrix)[:,0:60]
                        else:
                            closest = np.concatenate((closest, np.argsort(distance_matrix)[:,0:60]), axis=0)
                        
                else:
                
                # compute euclidean distance metrics
                    self.parameter_delta_matrix = self._get_parameter_delta_matrix(series,self.config.special_school_parameter_list)

                    # euclidean distance coefficients with weightings
                    self.parameter_matrix = self._compute_parameter_matrix(series, self.config.special_school_parameter_list, self.config.special_school_parameter_weightings)
                    
                    # how do we want to deal with nans? currently replace with the median
                    # value from that school's matrix row
                    self.parameter_matrix = self._replace_matrix_nans()

                    distance_matrix = self._compute_distance_matrix(True)
                    '''distance_matrix = np.sqrt(np.sum([p1,p2], axis=0)) + np.sqrt(np.sum([p301,p302,p303,p304,p305,p306,p307,p308,p309,p310,p311,p312], axis=0))'''
                    closest = np.argsort(distance_matrix + np.identity(len(distance_matrix)) * 1e8)[:,0:60]
            
            else:

                print('\ncomputing set: ', grouped_data.iloc[i].name)
                print("number of schools in set: ", len(series['UKPRN_URN']))
                
                # get parameter ranges
                self.parameter_range = self._get_parameter_range(series, self.config.school_parameter_list)

                block_size = 5000
                if len(series['NumberOfPupils']) > block_size:
                    # incrementors to track progress through blocking
                    i1, i2, counter = -block_size, 0, 0
                    # dictionaries to track the closest_blocks and the index (for entire matrix)
                    
                    while counter != len(series['NumberOfPupils']):
                        i1 += block_size
                        # catch to deal with last iteration which will be < block size
                        if counter + block_size < len(series['NumberOfPupils']):
                            i2 += block_size
                            counter += block_size
                        else:
                            i2 += (len(series['NumberOfPupils']) - counter)
                            counter += (len(series['NumberOfPupils']) - counter)
                            
                        # compute euclidean distance metrics
                        self.parameter_delta_matrix = self._get_parameter_delta_matrix(series,self.config.school_parameter_list,i1=i1,i2=i2)
         
                        # euclidean distance coefficients with weightings
                        self.parameter_matrix = self._compute_parameter_matrix(series, self.config.school_parameter_list, self.config.school_parameter_weightings)

                        # how do we want to deal with nans? currently replace with the median
                        # value from that school's matrix row
                        self.parameter_matrix = self._replace_matrix_nans()

                        # euclidean distance matrix
                        distance_matrix = self._compute_distance_matrix(False)

                        for j in range(len(distance_matrix)):
                            distance_matrix[j][i1+j] += 1e8
                        
                        if counter == block_size:
                            closest = np.argsort(distance_matrix)[:,0:60]
                        else:
                            closest = np.concatenate((closest, np.argsort(distance_matrix)[:,0:60]), axis=0)
                        
                else:
                    self.parameter_delta_matrix = self._get_parameter_delta_matrix(series,self.config.school_parameter_list)
                    
                    self.parameter_matrix = self._compute_parameter_matrix(series, self.config.school_parameter_list, self.config.school_parameter_weightings)
                    
                    self.parameter_matrix = self._replace_matrix_nans()
                
                    distance_matrix = self._compute_distance_matrix(False)

                    closest = np.argsort(distance_matrix + np.identity(len(distance_matrix)) * 1e8)[:,0:60]
            self.benchmarking_groups[grouped_data.iloc[i].name] = closest
                

        return self.benchmarking_groups
