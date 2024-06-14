from enum import Enum, auto


class MessageType(Enum):
    """
    Various types of incoming message:

    - `default` data.
    - `default` data with user-defined comparator-set.
    - `custom` data.
    """

    Default = auto()
    DefaultUserDefined = auto()
    Custom = auto()


def get_message_type(message: dict) -> MessageType:
    """
    Determine the type of an incoming message.

    TODO: `default` message format?

    User-defined comparator set message will be of the form:

    ```json
    {
        "jobId": "24463424-9642-4314-bb55-45424af6e812",
        "type": "comparator-set",
        "runId": "c321ef6a-3b1c-4ce2-8e32-0d0167bf2fa7",
        "year": 2022,
        "urn": "106057",
        "payload": {
            "kind": "ComparatorSetPayload",
            "set": [
                "145799",
                "142875"
            ]
        }
    }
    ```

    Custom-data message will be of the form:

    ```json
    {
        "jobId": "79ca4d18-d065-4180-9e0d-10c9b3fdd1a8",
        "type": "custom-data",
        "runType": "custom",
        "runId": "123",
        "year": 2023,
        "urn": "123",
        "payload": {
            "kind": "CustomDataPayload",
            "administrativeSuppliesNonEducationalCosts": 0.0,
            "cateringStaffCosts": 0.0,
            "cateringSuppliesCosts": 0.0,
            "incomeCateringServices": 0.0,
            "examinationFeesCosts": 0.0,
            "learningResourcesNonIctCosts": 0.0,
            "learningResourcesIctCosts": 0.0,
            "administrativeClericalStaffCosts": 0.0,
            "auditorsCosts": 0.0,
            "otherStaffCosts": 0.0,
            "professionalServicesNonCurriculumCosts": 0.0,
            "cleaningCaretakingCosts": 0.0,
            "maintenancePremisesCosts": 0.0,
            "otherOccupationCosts": 0.0,
            "premisesStaffCosts": 0.0,
            "agencySupplyTeachingStaffCosts": 0.0,
            "educationSupportStaffCosts": 0.0,
            "educationalConsultancyCosts": 0.0,
            "supplyTeachingStaffCosts": 0.0,
            "teachingStaffCosts": 0.0,
            "energyCosts": 0.0,
            "waterSewerageCosts": 0.0,
            "directRevenueFinancingCosts": 0.0,
            "groundsMaintenanceCosts": 0.0,
            "indirectEmployeeExpenses": 0.0,
            "interestChargesLoanBank": 0.0,
            "otherInsurancePremiumsCosts": 0.0,
            "privateFinanceInitiativeCharges": 0.0,
            "rentRatesCosts": 0.0,
            "specialFacilitiesCosts": 0.0,
            "staffDevelopmentTrainingCosts": 0.0,
            "staffRelatedInsuranceCosts": 0.0,
            "supplyTeacherInsurableCosts": 0.0,
            "totalIncome": 0.0,
            "totalExpenditure": 0.0,
            "revenueReserve": 0.0,
            "totalPupils": 0.0,
            "percentFreeSchoolMeals": 0.0,
            "percentSpecialEducationNeeds": 0.0,
            "totalInternalFloorArea": 0.0,
            "workforceFTE": 0.0,
            "teachersFTE": 0.0,
            "seniorLeadershipFTE": 0.0
        }
    }
    ```

    :param message: incoming message
    :return: type of incoming message
    """
    match message.get("payload", {}).get("kind"):
        case "ComparatorSetPayload":
            return MessageType.DefaultUserDefined
        case "CustomDataPayload":
            return MessageType.Custom
        case _:
            return MessageType.Default
