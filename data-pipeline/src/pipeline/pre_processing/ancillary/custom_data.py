import pandas as pd

import pipeline.config as config
import pipeline.pre_processing.common.mappings as mappings


def update_custom_data(
    existing_data: pd.DataFrame,
    custom_data: dict,
    target_urn: int,
) -> pd.DataFrame:
    """
    Update existing financial data with custom data.

    This will overwrite financial information for a specific row with
    data provided; additionally, _all_ "central services" information
    (less the "Financial Position" as this is non-numeric) will be set
    to zero, again for that row only.

    It is assumed that the updated custom-data constitutes a full year
    of financial information and as a result, `Partial Years Present`
    will _always_ be set to _false_ for the row in question.

    Note: only a subset of the custom fields may be present in the
    inbound message; only a subset of mapped columns may be present in
    the existing data. Equally, the data will only be updated if the
    target is present in the existing data.

    :param existing_data: existing, pre-processed data
    :param custom_data: custom financial information
    :param target_urn: specific row to update
    :return: updated data
    """

    if target_urn not in existing_data.index:
        return existing_data

    custom_to_columns = {
        "administrativeSuppliesNonEducationalCosts": "Administrative supplies_Administrative supplies (non educational)",
        "cateringStaffCosts": "Catering staff and supplies_Catering staff",
        "cateringSuppliesCosts": "Catering staff and supplies_Catering supplies",
        "incomeCateringServices": "Income_Catering services",
        "examinationFeesCosts": "Educational supplies_Examination fees",
        "learningResourcesNonIctCosts": "Educational supplies_Learning resources (not ICT equipment)",
        "learningResourcesIctCosts": "Educational ICT_ICT learning resources",
        "administrativeClericalStaffCosts": "Non-educational support staff and services_Administrative and clerical staff",
        "auditorsCosts": "Non-educational support staff and services_Audit cost",
        "otherStaffCosts": "Non-educational support staff and services_Other staff",
        "professionalServicesNonCurriculumCosts": "Non-educational support staff and services_Professional services (non-curriculum)",
        "cleaningCaretakingCosts": "Premises staff and services_Cleaning and caretaking",
        "maintenancePremisesCosts": "Premises staff and services_Maintenance of premises",
        "otherOccupationCosts": "Premises staff and services_Other occupation costs",
        "premisesStaffCosts": "Premises staff and services_Premises staff",
        "agencySupplyTeachingStaffCosts": "Teaching and Teaching support staff_Agency supply teaching staff",
        "educationSupportStaffCosts": "Teaching and Teaching support staff_Education support staff",
        "educationalConsultancyCosts": "Teaching and Teaching support staff_Educational consultancy",
        "supplyTeachingStaffCosts": "Teaching and Teaching support staff_Supply teaching staff",
        "teachingStaffCosts": "Teaching and Teaching support staff_Teaching staff",
        "energyCosts": "Utilities_Energy",
        "waterSewerageCosts": "Utilities_Water and sewerage",
        "directRevenueFinancingCosts": "Other costs_Direct revenue financing",
        "groundsMaintenanceCosts": "Other costs_Grounds maintenance",
        "indirectEmployeeExpenses": "Other costs_Indirect employee expenses",
        "interestChargesLoanBank": "Other costs_Interest charges for loan and bank",
        "otherInsurancePremiumsCosts": "Other costs_Other insurance premiums",
        "privateFinanceInitiativeCharges": "Other costs_PFI charges",
        "rentRatesCosts": "Other costs_Rent and rates",
        "specialFacilitiesCosts": "Other costs_Special facilities",
        "staffDevelopmentTrainingCosts": "Other costs_Staff development and training",
        "staffRelatedInsuranceCosts": "Other costs_Staff-related insurance",
        "supplyTeacherInsurableCosts": "Other costs_Supply teacher insurance",
        "totalIncome": "Total Income",
        "totalExpenditure": "Total Expenditure",
        "revenueReserve": "Revenue reserve",
        "totalPupils": "Number of pupils",
        "percentFreeSchoolMeals": "Percentage Free school meals",
        "percentSpecialEducationNeeds": "Percentage SEN",
        "totalInternalFloorArea": "Total Internal Floor Area",
        "workforceFTE": "Total School Workforce (Full-Time Equivalent)",
        "teachersFTE": "Total Number of Teachers (Full-Time Equivalent)",
        "percentTeacherWithQualifiedStatus": "Teachers with Qualified Teacher Status (%) (Headcount)",
        "seniorLeadershipFTE": "Total Number of Teachers in the Leadership Group (Full-time Equivalent)",
        "teachingAssistantFTE": "Total Number of Teaching Assistants (Full-Time Equivalent)",
        "nonClassroomSupportStaffFTE": "NonClassroomSupportStaffFTE",
        "auxiliaryStaffFTE": "Total Number of Auxiliary Staff (Full-Time Equivalent)",
        "workforceHeadcount": "Total School Workforce (Headcount)",
    }

    custom_present = [
        custom
        for custom in custom_to_columns.keys()
        if custom in custom_data and custom_to_columns[custom] in existing_data.columns
    ]

    existing_columns = [custom_to_columns[custom] for custom in custom_present]
    custom_values = [custom_data[custom] for custom in custom_present]
    existing_data.loc[target_urn, existing_columns] = custom_values

    central_services_columns = [
        column
        for column in existing_data.columns
        if column.endswith("_CS") and column != "Financial Position_CS"
    ]
    central_services_values = [0.0] * len(central_services_columns)
    existing_data.loc[
        target_urn,
        central_services_columns,
    ] = central_services_values

    existing_data.loc[target_urn] = _post_process_custom(
        target_data=existing_data.loc[[target_urn]]
    ).loc[target_urn]

    existing_data.loc[target_urn, "Partial Years Present"] = False

    return existing_data


# TODO: this could be replaced by `pipeline.pre_processing.total_per_unit`.
def _post_process_custom(
    target_data: pd.DataFrame,
) -> pd.DataFrame:
    """
    Recalculate total and per-unit costs for custom data.

    Total and per-unit costs are set to zero before recalculating.

    Note: largely repeats some of the logic from
    :func:`build_maintained_school_data`; for this reason it accepts
    a :class:`DataFrame` but must only include the URN of interest.

    :param existing_data: existing, pre-processed data
    :return: updated data
    """
    zero_columns = [
        column
        for column in target_data.columns
        for category, _ in config.rag_category_settings.items()
        if column.startswith(category) and column.endswith(("_Per Unit", "_Total"))
    ]
    zero_column_indices = [target_data.columns.get_loc(c) for c in zero_columns]
    zero_column_values = [0.0] * len(zero_column_indices)
    target_data.iloc[0, zero_column_indices] = zero_column_values

    # TODO: `_Net Costs` need to be recalculated as per line 1152.
    catering_net_costs = target_data["Catering staff and supplies_Net Costs"].copy()
    target_data["Catering staff and supplies_Net Costs"] = 0.0

    for category, settings in config.rag_category_settings.items():
        basis_data = target_data[
            (
                "Number of pupils"
                if settings["type"] == "Pupil"
                else "Total Internal Floor Area"
            )
        ]
        target_data = mappings.map_cost_series(category, target_data, basis_data)

    target_data["Catering staff and supplies_Net Costs"] = catering_net_costs

    return target_data
