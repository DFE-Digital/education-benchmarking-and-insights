import os


class Config():

    def __init__(self):
        self.base_data_path = os.path.join(os.path.dirname(os.getcwd()), "data/").replace('\\','/')

        # constants
        self.time_period = 202223

        self.sen_breakdown_list = ['Primary_need_spld','Primary_need_mld','Primary_need_sld',
                                    'Primary_need_pmld','Primary_need_semh','Primary_need_slcn',
                                    'Primary_need_hi','Primary_need_vi','Primary_need_msi',
                                    'Primary_need_pd','Primary_need_asd','Primary_need_oth']
        

        self.school_parameter_list = ['NumberOfPupils','PercentageFSM','Percent_SEN_all']
        self.school_parameter_weightings = [0.5,0.4,0.1]


        self.special_school_parameter_list = ['NumberOfPupils','PercentageFSM','Primary_need_spld',
                                               'Primary_need_mld','Primary_need_sld','Primary_need_pmld',
                                               'Primary_need_semh','Primary_need_slcn','Primary_need_hi',
                                               'Primary_need_vi','Primary_need_msi','Primary_need_pd',
                                               'Primary_need_asd','Primary_need_oth']
        self.special_school_parameter_weightings = [0.6,0.4,1,1,1,1,1,1,1,1,1,1,1,1]

        self.school_area_parameter_list = ['Sum_Area','Age_avg_score']
        self.school_area_parameter_weightings = [0.8,0.2]