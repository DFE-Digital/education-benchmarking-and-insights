import pandas as pd

def prepare_hospital_schools_data(hospital_schools_blob):
    hospital_schools = pd.read_csv(hospital_schools_blob)
    return hospital_schools