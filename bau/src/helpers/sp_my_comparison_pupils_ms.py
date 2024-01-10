"""
Copied from the sp_my_comparison_pupils_ms stored procedure as part of the VMFI 
data drop. (for maintained schools)
"""
from controllers.data_load_controller import DataLoadController
from helpers.pupil_benchmark_set_calculator_helper import PupilBenchmarkingSetCalculatorHelper
import numpy as np


class SpMyComparisonPupilsMsHelper():

    def __init__(self):
        self.data_load_controller = DataLoadController()
        self.benchmark_set_calculator = PupilBenchmarkingSetCalculatorHelper()

        # load in data
        
        self.academy_groups_mapping = self.data_load_controller.load_data(
                                        'academy_groups_mapping')
        self.ms_data = self.data_load_controller.load_data('ms_data')



    def run_procedure(self):
        print('------------')
        print('Running maintained schools pupil comparator set creation')
        print('------------')
        # filter down data

        self.ms_data = self.ms_data.loc[
                                (self.ms_data['DataReleaseId'] == 12).values]
            
        self.ms_data = self.ms_data.loc[
                                (~self.ms_data['Region_added'].isna()).values |\
                                (self.ms_data['NumberOfPupils'] > 0).values]
            
        self.ms_data = self.ms_data.fillna(0)

        self.all_schools_data = self.ms_data[['UKPRN_URN',
                                                       'Region_added',
                                                       'Percent_SEN_all',
                                                       'NumberOfPupils',
                                                       'PercentageFSM',
                                                       'SchoolPhaseType',
                                                       'Boarding',
                                                       'Year',
                                                       'DataReleaseId']]
        
        self.all_schools_data = self.all_schools_data.loc[~(self.all_schools_data['UKPRN_URN']==0)]
        # adjust the boarding and school phase type to remain inline with 
        self.all_schools_data.loc[
            (self.all_schools_data['SchoolPhaseType']=='Pupil referral unit'),'Boarding'
            ] = 'Any'
        self.all_schools_data.loc[
            (self.all_schools_data['SchoolPhaseType']=='Primary'),'Boarding'
            ] = 'Any'
        self.all_schools_data.loc[
            (self.all_schools_data['SchoolPhaseType']=='Secondary'),'Boarding'
            ] = 'Any'
        self.all_schools_data.loc[
            (self.all_schools_data['SchoolPhaseType']=='Nursery'),'Boarding'
            ] = 'Any'
        self.all_schools_data.loc[
            (self.all_schools_data['SchoolPhaseType']=='Special') & (self.all_schools_data['Boarding']=='Unknown'),
            'Boarding'
            ] = 'Not Boarding'
        self.all_schools_data.loc[
            self.all_schools_data['Boarding']=='Unknown','Boarding'
            ] = 'Not Boarding'


        self.all_schools_data = self.all_schools_data.loc[self.all_schools_data['DataReleaseId']==12]
        
        self.all_schools_data['phase_boarding'] = self.all_schools_data.apply(lambda row: f"{row['SchoolPhaseType']}-{row['Boarding']}", axis=1)
        
        self.all_schools_data_grouped = self.all_schools_data.groupby(['phase_boarding']).agg(list)

        # compute benchmarking sets
        benchmarking_groups = self.benchmark_set_calculator.get_benchmark_sets(self.all_schools_data_grouped)

        self.academy_groups_mapping['phase_boarding'] = self.academy_groups_mapping['SchoolPhaseType'] + '-' + self.academy_groups_mapping['Boarding']
        
        ms_mappings = self.academy_groups_mapping.loc[
            (self.academy_groups_mapping['Sector']=='Maintained') & (self.academy_groups_mapping['CompGroup']=='Pupil')
            ]

        print('\n')
        for i in range(len(ms_mappings)):
            # take table name from the filtered academy_groups_mapping table
            file = open('C:/Users/clorch/OneDrive - Department for Education/Documents/LAMS/custom_scripts/MetricRAG_output_tables/' + \
                        ms_mappings['Table_name'].iloc[i].replace('comparison.','') + '.csv', 'w+')
            file.write('Year,UKPRN_URN,UKPRN_URN2,Region_added,Region_added2,RANK1,DataReleaseId\n')
            try:
                tmp_ms_data = self.all_schools_data_grouped.loc[ms_mappings['phase_boarding'].iloc[i]]
                regions = np.array(tmp_ms_data['Region_added']) # regions used in region filtering
                for school_being_compared_index in range(len(benchmarking_groups[ms_mappings['phase_boarding'].iloc[i]])):
                    
                    
                    # items used in region filter
                    parent_region = regions[school_being_compared_index] #determine parent region
                    group_indices = benchmarking_groups[ms_mappings['phase_boarding'].iloc[i]][school_being_compared_index] # get indices of schools used in current comparator set (60)
                    if len(group_indices) < 30:
                        schools_30 = np.array(group_indices)
                    else:
                        region_filtered = group_indices[np.where(regions[group_indices]==parent_region)[0]] # take indices where the regions are the same as the school being compared
                        if len(region_filtered) > 30:
                            schools_30 = np.array(region_filtered[:30])
                        else:
                            subset = [x for x in group_indices if x not in region_filtered] # remove those indices from the standing list
                            schools_30 = np.append(region_filtered, subset[:(30-len(region_filtered))]) # make up the rest of the 30 schools with the nearest euclidean distanced schools
                    rank = [list(group_indices).index(item)+1 for item in schools_30]
                    c = 0
                    for k in schools_30:
                        
                        if c == 0:
                            comparator_school_index = int(school_being_compared_index)
                        else:
                            comparator_school_index = int(k)
                        
                        file.write(','.join([str(tmp_ms_data['Year'][school_being_compared_index]),
                                            str(tmp_ms_data['UKPRN_URN'][school_being_compared_index]),
                                            str(tmp_ms_data['UKPRN_URN'][comparator_school_index]),
                                            str(tmp_ms_data['Region_added'][school_being_compared_index]),
                                            str(tmp_ms_data['Region_added'][comparator_school_index]),
                                            str(rank[c]),
                                            str(12),
                                            '\n']))
                        c +=1
                        
                file.close()
            except KeyError: 
                print(str(ms_mappings['phase_boarding'].iloc[i]) + ' is not a key in the benchmarking groups')
                
        return None