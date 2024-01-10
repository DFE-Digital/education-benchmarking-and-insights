"""
Controller used to load data.
"""

import pandas as pd
from config.config import Config

class DataLoadController():

    def __init__(self):
        self.config = Config()
        None
    
    def load_data(self, data_source):
        """
        data_source:
            - academies_data
            - academy_groups_mapping
            - edubase
            - finance_data_release_id_10
            - master_list
            - maintained_schools_list
            - SEN
        """
        try: 
            return pd.read_csv(''.join(
                                        [self.config.base_data_path,
                                        data_source,
                                        '.csv']
                                        ))
        except FileNotFoundError:
            print("File with name '{data_source}' not found. Please check data\
                  sources in data directory and check the data_source input against\
                   valid options. Use data_load? to list valid options.") 
        except UnicodeDecodeError:
            return pd.read_csv(''.join(
                                        [self.config.base_data_path,
                                        data_source,
                                        '.csv']
                                        ), encoding='unicode_escape')

