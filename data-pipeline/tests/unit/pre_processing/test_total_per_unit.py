import pandas as pd

from pipeline import config
from pipeline.pre_processing import total_per_unit

unit_data = {
    "Administrative supplies_Administrative supplies (non educational)": [
        0.0,
        1.0,
        1.0,
        1.0,
    ],
    "Administrative supplies_Administrative supplies (non educational)_CS": [
        0.0,
        1.0,
        1.0,
        1.0,
    ],
    "Administrative supplies_Administrative supplies (non educational)_Per Unit": [
        0.0,
        1.0,
        1.0,
        1.0,
    ],
    "Administrative supplies_Total": [0.0, 1.0, 1.0, 1.0],
    "Administrative supplies_Total_CS": [0.0, 1.0, 1.0, 1.0],
    "Administrative supplies_Total_Per Unit": [0.0, 1.0, 1.0, 1.0],
    "Catering staff and supplies_Catering staff": [0.0, 1.0, 1.0, 1.0],
    "Catering staff and supplies_Catering staff_CS": [0.0, 1.0, 1.0, 1.0],
    "Catering staff and supplies_Catering staff_Per Unit": [0.0, 1.0, 1.0, 1.0],
    "Catering staff and supplies_Catering supplies": [0.0, 1.0, 1.0, 1.0],
    "Catering staff and supplies_Catering supplies_CS": [0.0, 1.0, 1.0, 1.0],
    "Catering staff and supplies_Catering supplies_Per Unit": [0.0, 1.0, 1.0, 1.0],
    "Catering staff and supplies_Net Costs": [0.0, 1.0, 1.0, 1.0],
    "Catering staff and supplies_Net Costs_CS": [0.0, 1.0, 1.0, 1.0],
    "Catering staff and supplies_Total": [0.0, 2.0, 2.0, 2.0],
    "Catering staff and supplies_Total_CS": [0.0, 1.0, 1.0, 1.0],
    "Catering staff and supplies_Total_Per Unit": [0.0, 2.0, 2.0, 2.0],
    "Educational ICT_ICT learning resources": [0.0, 1.0, 1.0, 1.0],
    "Educational ICT_ICT learning resources_CS": [0.0, 1.0, 1.0, 1.0],
    "Educational ICT_ICT learning resources_Per Unit": [0.0, 1.0, 1.0, 1.0],
    "Educational ICT_Total": [0.0, 1.0, 1.0, 1.0],
    "Educational ICT_Total_CS": [0.0, 1.0, 1.0, 1.0],
    "Educational ICT_Total_Per Unit": [0.0, 1.0, 1.0, 1.0],
    "Educational supplies_Examination fees": [0.0, 1.0, 1.0, 1.0],
    "Educational supplies_Examination fees_CS": [0.0, 1.0, 1.0, 1.0],
    "Educational supplies_Examination fees_Per Unit": [0.0, 1.0, 1.0, 1.0],
    "Educational supplies_Learning resources (not ICT equipment)": [0.0, 1.0, 1.0, 1.0],
    "Educational supplies_Learning resources (not ICT equipment)_CS": [
        0.0,
        1.0,
        1.0,
        1.0,
    ],
    "Educational supplies_Learning resources (not ICT equipment)_Per Unit": [
        0.0,
        1.0,
        1.0,
        1.0,
    ],
    "Educational supplies_Total": [0.0, 2.0, 2.0, 2.0],
    "Educational supplies_Total_CS": [0.0, 1.0, 1.0, 1.0],
    "Educational supplies_Total_Per Unit": [0.0, 2.0, 2.0, 2.0],
    "Non-educational support staff and services_Administrative and clerical staff": [
        0.0,
        1.0,
        1.0,
        1.0,
    ],
    "Non-educational support staff and services_Administrative and clerical staff_CS": [
        0.0,
        1.0,
        1.0,
        1.0,
    ],
    "Non-educational support staff and services_Administrative and clerical staff_Per Unit": [
        0.0,
        1.0,
        1.0,
        1.0,
    ],
    "Non-educational support staff and services_Audit cost": [0.0, 1.0, 1.0, 1.0],
    "Non-educational support staff and services_Audit cost_CS": [0.0, 1.0, 1.0, 1.0],
    "Non-educational support staff and services_Audit cost_Per Unit": [
        0.0,
        1.0,
        1.0,
        1.0,
    ],
    "Non-educational support staff and services_Other staff": [0.0, 1.0, 1.0, 1.0],
    "Non-educational support staff and services_Other staff_CS": [0.0, 1.0, 1.0, 1.0],
    "Non-educational support staff and services_Other staff_Per Unit": [
        0.0,
        1.0,
        1.0,
        1.0,
    ],
    "Non-educational support staff and services_Professional services (non-curriculum)": [
        0.0,
        1.0,
        1.0,
        1.0,
    ],
    "Non-educational support staff and services_Professional services (non-curriculum)_CS": [
        0.0,
        1.0,
        1.0,
        1.0,
    ],
    "Non-educational support staff and services_Professional services (non-curriculum)_Per Unit": [
        0.0,
        1.0,
        1.0,
        1.0,
    ],
    "Non-educational support staff and services_Total": [0.0, 4.0, 4.0, 4.0],
    "Non-educational support staff and services_Total_CS": [0.0, 1.0, 1.0, 1.0],
    "Non-educational support staff and services_Total_Per Unit": [0.0, 4.0, 4.0, 4.0],
    "Other costs_Direct revenue financing": [0.0, 1.0, 1.0, 1.0],
    "Other costs_Direct revenue financing_CS": [0.0, 1.0, 1.0, 1.0],
    "Other costs_Direct revenue financing_Per Unit": [0.0, 1.0, 1.0, 1.0],
    "Other costs_Grounds maintenance": [0.0, 1.0, 1.0, 1.0],
    "Other costs_Grounds maintenance_CS": [0.0, 1.0, 1.0, 1.0],
    "Other costs_Grounds maintenance_Per Unit": [0.0, 1.0, 1.0, 1.0],
    "Other costs_Indirect employee expenses": [0.0, 1.0, 1.0, 1.0],
    "Other costs_Indirect employee expenses_CS": [0.0, 1.0, 1.0, 1.0],
    "Other costs_Indirect employee expenses_Per Unit": [0.0, 1.0, 1.0, 1.0],
    "Other costs_Interest charges for loan and bank": [0.0, 1.0, 1.0, 1.0],
    "Other costs_Interest charges for loan and bank_CS": [0.0, 1.0, 1.0, 1.0],
    "Other costs_Interest charges for loan and bank_Per Unit": [0.0, 1.0, 1.0, 1.0],
    "Other costs_Other insurance premiums": [0.0, 1.0, 1.0, 1.0],
    "Other costs_Other insurance premiums_CS": [0.0, 1.0, 1.0, 1.0],
    "Other costs_Other insurance premiums_Per Unit": [0.0, 1.0, 1.0, 1.0],
    "Other costs_PFI charges": [0.0, 1.0, 1.0, 1.0],
    "Other costs_PFI charges_CS": [0.0, 1.0, 1.0, 1.0],
    "Other costs_PFI charges_Per Unit": [0.0, 1.0, 1.0, 1.0],
    "Other costs_Rent and rates": [0.0, 1.0, 1.0, 1.0],
    "Other costs_Rent and rates_CS": [0.0, 1.0, 1.0, 1.0],
    "Other costs_Rent and rates_Per Unit": [0.0, 1.0, 1.0, 1.0],
    "Other costs_School costs": [0.0, 1.0, 1.0, 1.0],
    "Other costs_School costs_Per Unit": [0.0, 1.0, 1.0, 1.0],
    "Other costs_School staff": [0.0, 1.0, 1.0, 1.0],
    "Other costs_School staff_Per Unit": [0.0, 1.0, 1.0, 1.0],
    "Other costs_Special facilities": [0.0, 1.0, 1.0, 1.0],
    "Other costs_Special facilities_CS": [0.0, 1.0, 1.0, 1.0],
    "Other costs_Special facilities_Per Unit": [0.0, 1.0, 1.0, 1.0],
    "Other costs_Staff development and training": [0.0, 1.0, 1.0, 1.0],
    "Other costs_Staff development and training_CS": [0.0, 1.0, 1.0, 1.0],
    "Other costs_Staff development and training_Per Unit": [0.0, 1.0, 1.0, 1.0],
    "Other costs_Staff-related insurance": [0.0, 1.0, 1.0, 1.0],
    "Other costs_Staff-related insurance_CS": [0.0, 1.0, 1.0, 1.0],
    "Other costs_Staff-related insurance_Per Unit": [0.0, 1.0, 1.0, 1.0],
    "Other costs_Supply teacher insurance": [0.0, 1.0, 1.0, 1.0],
    "Other costs_Supply teacher insurance_CS": [0.0, 1.0, 1.0, 1.0],
    "Other costs_Supply teacher insurance_Per Unit": [0.0, 1.0, 1.0, 1.0],
    "Other costs_Total": [0.0, 13.0, 13.0, 13.0],
    "Other costs_Total_CS": [0.0, 1.0, 1.0, 1.0],
    "Other costs_Total_Per Unit": [0.0, 13.0, 13.0, 13.0],
    "Premises staff and services_Cleaning and caretaking": [0.0, 1.0, 1.0, 1.0],
    "Premises staff and services_Cleaning and caretaking_CS": [0.0, 1.0, 1.0, 1.0],
    "Premises staff and services_Cleaning and caretaking_Per Unit": [
        0.0,
        1.0,
        1.0,
        1.0,
    ],
    "Premises staff and services_Maintenance of premises": [0.0, 1.0, 1.0, 1.0],
    "Premises staff and services_Maintenance of premises_CS": [0.0, 1.0, 1.0, 1.0],
    "Premises staff and services_Maintenance of premises_Per Unit": [
        0.0,
        1.0,
        1.0,
        1.0,
    ],
    "Premises staff and services_Other occupation costs": [0.0, 1.0, 1.0, 1.0],
    "Premises staff and services_Other occupation costs_CS": [0.0, 1.0, 1.0, 1.0],
    "Premises staff and services_Other occupation costs_Per Unit": [0.0, 1.0, 1.0, 1.0],
    "Premises staff and services_Premises staff": [0.0, 1.0, 1.0, 1.0],
    "Premises staff and services_Premises staff_CS": [0.0, 1.0, 1.0, 1.0],
    "Premises staff and services_Premises staff_Per Unit": [0.0, 1.0, 1.0, 1.0],
    "Premises staff and services_Total": [0.0, 4.0, 4.0, 4.0],
    "Premises staff and services_Total_CS": [0.0, 1.0, 1.0, 1.0],
    "Premises staff and services_Total_Per Unit": [0.0, 4.0, 4.0, 4.0],
    "Teaching and Teaching support staff_Agency supply teaching staff": [
        0.0,
        1.0,
        1.0,
        1.0,
    ],
    "Teaching and Teaching support staff_Agency supply teaching staff_CS": [
        0.0,
        1.0,
        1.0,
        1.0,
    ],
    "Teaching and Teaching support staff_Agency supply teaching staff_Per Unit": [
        0.0,
        1.0,
        1.0,
        1.0,
    ],
    "Teaching and Teaching support staff_Education support staff": [0.0, 1.0, 1.0, 1.0],
    "Teaching and Teaching support staff_Education support staff_CS": [
        0.0,
        1.0,
        1.0,
        1.0,
    ],
    "Teaching and Teaching support staff_Education support staff_Per Unit": [
        0.0,
        1.0,
        1.0,
        1.0,
    ],
    "Teaching and Teaching support staff_Educational consultancy": [0.0, 1.0, 1.0, 1.0],
    "Teaching and Teaching support staff_Educational consultancy_CS": [
        0.0,
        1.0,
        1.0,
        1.0,
    ],
    "Teaching and Teaching support staff_Educational consultancy_Per Unit": [
        0.0,
        1.0,
        1.0,
        1.0,
    ],
    "Teaching and Teaching support staff_Supply teaching staff": [0.0, 1.0, 1.0, 1.0],
    "Teaching and Teaching support staff_Supply teaching staff_CS": [
        0.0,
        1.0,
        1.0,
        1.0,
    ],
    "Teaching and Teaching support staff_Supply teaching staff_Per Unit": [
        0.0,
        1.0,
        1.0,
        1.0,
    ],
    "Teaching and Teaching support staff_Teaching staff": [0.0, 1.0, 1.0, 1.0],
    "Teaching and Teaching support staff_Teaching staff_CS": [0.0, 1.0, 1.0, 1.0],
    "Teaching and Teaching support staff_Teaching staff_Per Unit": [0.0, 1.0, 1.0, 1.0],
    "Teaching and Teaching support staff_Total": [0.0, 5.0, 5.0, 5.0],
    "Teaching and Teaching support staff_Total_CS": [0.0, 1.0, 1.0, 1.0],
    "Teaching and Teaching support staff_Total_Per Unit": [0.0, 5.0, 5.0, 5.0],
    "Utilities_Energy": [0.0, 1.0, 1.0, 1.0],
    "Utilities_Energy_CS": [0.0, 1.0, 1.0, 1.0],
    "Utilities_Energy_Per Unit": [0.0, 1.0, 1.0, 1.0],
    "Utilities_Total": [0.0, 2.0, 2.0, 2.0],
    "Utilities_Total_CS": [0.0, 1.0, 1.0, 1.0],
    "Utilities_Total_Per Unit": [0.0, 2.0, 2.0, 2.0],
    "Utilities_Water and sewerage": [0.0, 1.0, 1.0, 1.0],
    "Utilities_Water and sewerage_CS": [0.0, 1.0, 1.0, 1.0],
    "Utilities_Water and sewerage_Per Unit": [0.0, 1.0, 1.0, 1.0],
    "Number of pupils": [0.0, 1.0, 1.0, 1.0],
    "Total Internal Floor Area": [0.0, 1.0, 1.0, 1.0],
}


def test_calculate_total_per_unit_costs_recreation():
    """
    Calling `calculate_total_per_unit_costs` should result in the
    creation of a _new_ DataFrame with all relevant columns recreated.
    """
    df = pd.DataFrame(unit_data, index=[0, 1, 2, 3])

    result = total_per_unit.calculate_total_per_unit_costs(df)

    assert df is not result
    pd.testing.assert_frame_equal(df, result)
