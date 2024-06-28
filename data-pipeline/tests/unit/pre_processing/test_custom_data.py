import pandas as pd

from src.pipeline.pre_processing import update_custom_data

_default_custom_data = {
    "administrativeSuppliesNonEducationalCosts": 1.0,
    "cateringStaffCosts": 1.0,
    "cateringSuppliesCosts": 1.0,
    "incomeCateringServices": 1.0,
    "examinationFeesCosts": 1.0,
    "learningResourcesNonIctCosts": 1.0,
    "learningResourcesIctCosts": 1.0,
    "administrativeClericalStaffCosts": 1.0,
    "auditorsCosts": 1.0,
    "otherStaffCosts": 1.0,
    "professionalServicesNonCurriculumCosts": 1.0,
    "cleaningCaretakingCosts": 1.0,
    "maintenancePremisesCosts": 1.0,
    "otherOccupationCosts": 1.0,
    "premisesStaffCosts": 1.0,
    "agencySupplyTeachingStaffCosts": 1.0,
    "educationSupportStaffCosts": 1.0,
    "educationalConsultancyCosts": 1.0,
    "supplyTeachingStaffCosts": 1.0,
    "teachingStaffCosts": 1.0,
    "energyCosts": 1.0,
    "waterSewerageCosts": 1.0,
    "directRevenueFinancingCosts": 1.0,
    "groundsMaintenanceCosts": 1.0,
    "indirectEmployeeExpenses": 1.0,
    "interestChargesLoanBank": 1.0,
    "otherInsurancePremiumsCosts": 1.0,
    "privateFinanceInitiativeCharges": 1.0,
    "rentRatesCosts": 1.0,
    "specialFacilitiesCosts": 1.0,
    "staffDevelopmentTrainingCosts": 1.0,
    "staffRelatedInsuranceCosts": 1.0,
    "supplyTeacherInsurableCosts": 1.0,
    "totalIncome": 1.0,
    "revenueReserve": 1.0,
    "totalPupils": 1.0,
    "percentFreeSchoolMeals": 1.0,
    "percentSpecialEducationNeeds": 1.0,
    "totalInternalFloorArea": 1.0,
    "workforceFTE": 1.0,
    "teachersFTE": 1.0,
    "percentTeacherWithQualifiedStatus": 1.0,
    "seniorLeadershipFTE": 1.0,
    "teachingAssistantFTE": 1.0,
    "nonClassroomSupportStaffFTE": 1.0,
    "auxiliaryStaffFTE": 1.0,
    "workforceHeadcount": 1.0,
}

_default_existing_data = {
    "Administrative supplies_Administrative supplies (non educational)": [
        0.0,
        0.0,
        0.0,
        0.0,
    ],
    "Catering staff and supplies_Catering staff": [0.0, 0.0, 0.0, 0.0],
    "Catering staff and supplies_Catering supplies": [0.0, 0.0, 0.0, 0.0],
    "Income_Catering services": [0.0, 0.0, 0.0, 0.0],
    "Educational supplies_Examination fees": [0.0, 0.0, 0.0, 0.0],
    "Educational supplies_Learning resources (not ICT equipment)": [0.0, 0.0, 0.0, 0.0],
    "Educational ICT_ICT learning resources": [0.0, 0.0, 0.0, 0.0],
    "Non-educational support staff and services_Administrative and clerical staff": [
        0.0,
        0.0,
        0.0,
        0.0,
    ],
    "Non-educational support staff and services_Audit cost": [0.0, 0.0, 0.0, 0.0],
    "Non-educational support staff and services_Other staff": [0.0, 0.0, 0.0, 0.0],
    "Non-educational support staff and services_Professional services (non-curriculum)": [
        0.0,
        0.0,
        0.0,
        0.0,
    ],
    "Premises staff and services_Cleaning and caretaking": [0.0, 0.0, 0.0, 0.0],
    "Premises staff and services_Maintenance of premises": [0.0, 0.0, 0.0, 0.0],
    "Premises staff and services_Other occupation costs": [0.0, 0.0, 0.0, 0.0],
    "Premises staff and services_Premises staff": [0.0, 0.0, 0.0, 0.0],
    "Teaching and Teaching support staff_Agency supply teaching staff": [
        0.0,
        0.0,
        0.0,
        0.0,
    ],
    "Teaching and Teaching support staff_Education support staff": [0.0, 0.0, 0.0, 0.0],
    "Teaching and Teaching support staff_Educational consultancy": [0.0, 0.0, 0.0, 0.0],
    "Teaching and Teaching support staff_Supply teaching staff": [0.0, 0.0, 0.0, 0.0],
    "Teaching and Teaching support staff_Teaching staff": [0.0, 0.0, 0.0, 0.0],
    "Utilities_Energy": [0.0, 0.0, 0.0, 0.0],
    "Utilities_Water and sewerage": [0.0, 0.0, 0.0, 0.0],
    "Other costs_Direct revenue financing": [0.0, 0.0, 0.0, 0.0],
    "Other costs_Grounds maintenance": [0.0, 0.0, 0.0, 0.0],
    "Other costs_Indirect employee expenses": [0.0, 0.0, 0.0, 0.0],
    "Other costs_Interest charges for loan and bank": [0.0, 0.0, 0.0, 0.0],
    "Other costs_Other insurance premiums": [0.0, 0.0, 0.0, 0.0],
    "Other costs_PFI charges": [0.0, 0.0, 0.0, 0.0],
    "Other costs_Rent and rates": [0.0, 0.0, 0.0, 0.0],
    "Other costs_Special facilities": [0.0, 0.0, 0.0, 0.0],
    "Other costs_Staff development and training": [0.0, 0.0, 0.0, 0.0],
    "Other costs_Staff-related insurance": [0.0, 0.0, 0.0, 0.0],
    "Other costs_Supply teacher insurance": [0.0, 0.0, 0.0, 0.0],
    "Income_Total": [0.0, 0.0, 0.0, 0.0],
    "Revenue reserve": [0.0, 0.0, 0.0, 0.0],
    "Total pupils in trust": [0.0, 0.0, 0.0, 0.0],
    "Percentage Free school meals": [0.0, 0.0, 0.0, 0.0],
    "Percentage SEN": [0.0, 0.0, 0.0, 0.0],
    "Total Internal Floor Area": [0.0, 0.0, 0.0, 0.0],
    "Total School Workforce (Full-Time Equivalent)": [0.0, 0.0, 0.0, 0.0],
    "Total Number of Teachers (Full-Time Equivalent)": [0.0, 0.0, 0.0, 0.0],
    "Teachers with Qualified Teacher Status (%) (Headcount)": [0.0, 0.0, 0.0, 0.0],
    "Total Number of Teachers in the Leadership Group (Full-time Equivalent)": [
        0.0,
        0.0,
        0.0,
        0.0,
    ],
    "Total Number of Teaching Assistants (Full-Time Equivalent)": [0.0, 0.0, 0.0, 0.0],
    "NonClassroomSupportStaffFTE": [0.0, 0.0, 0.0, 0.0],
    "Total Number of Auxiliary Staff (Full-Time Equivalent)": [0.0, 0.0, 0.0, 0.0],
    "Total School Workforce (Headcount)": [0.0, 0.0, 0.0, 0.0],
}


def test_update_custom_data():
    """
    Existing data will always have a value of `0.0`.

    Custom data will update this to `1.0`, except for 2 named columns.
    """
    df = pd.DataFrame(_default_existing_data, index=[0, 1, 2, 3])
    custom_data = _default_custom_data | {
        "totalInternalFloorArea": 2.0,
        "workforceFTE": 3.0,
    }

    result = update_custom_data(
        existing_data=df,
        custom_data=custom_data,
        target_urn=1,
    )

    assert result.loc[
        1,
        ["Total Internal Floor Area", "Total School Workforce (Full-Time Equivalent)"],
    ].to_list() == [2.0, 3.0]

    assert all(
        value == 1.0
        for column, value in result.loc[
            1,
            ~result.columns.isin(
                [
                    "Total Internal Floor Area",
                    "Total School Workforce (Full-Time Equivalent)",
                ]
            ),
        ].items()
    )


def test_update_partial_custom_data():
    """
    Existing data will always have a value of `0.0`.

    Custom data will update for 2 named columns, leaving the rest
    unchanged.
    """
    df = pd.DataFrame(_default_existing_data, index=[0, 1, 2, 3])
    custom_data = {
        "totalInternalFloorArea": 1.0,
        "workforceFTE": 2.0,
    }

    result = update_custom_data(
        existing_data=df,
        custom_data=custom_data,
        target_urn=1,
    )

    assert result.loc[
        1,
        ["Total Internal Floor Area", "Total School Workforce (Full-Time Equivalent)"],
    ].to_list() == [1.0, 2.0]

    assert all(
        value == 0.0
        for column, value in result.loc[
            1,
            ~result.columns.isin(
                [
                    "Total Internal Floor Area",
                    "Total School Workforce (Full-Time Equivalent)",
                ]
            ),
        ].items()
    )

def test_update_custom_data_missing_columns():
    """
    Existing data will always have a value of `0.0`.

    Existing data will be missing some mapped columns.
    """
    df = pd.DataFrame(_default_existing_data, index=[0, 1, 2, 3])
    df.drop("Total Internal Floor Area", axis="columns", inplace=True)
    custom_data = _default_custom_data | {
        "totalInternalFloorArea": 2.0,
        "workforceFTE": 3.0,
    }

    result = update_custom_data(
        existing_data=df,
        custom_data=custom_data,
        target_urn=1,
    )

    assert "Total Internal Floor Area" not in result.columns
