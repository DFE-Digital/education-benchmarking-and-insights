from enum import Enum, auto

from .log import setup_logger

logger = setup_logger(__name__)


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

    `default` messages will be of the form (including varying sources
    for each data type):

    ```json
    {
        "jobId": "24463424-9642-4314-bb55-45424af6e812",
        "type": "default",
        "runId": 2023,
        "year": {
            "aar": 2022,
            "cfr": 2023,
            "bfr": 2022,
            "s251": 2021
        }
    }
    ```

    User-defined comparator set message will be of the form (including
    only a single source from which to retrieve data):

    ```json
    {
        "runId": "a6db019c-d45f-44f3-8af0-71d82c1c6257",
        "year": 2025,
        "urn": "100449",
        "payload": {
            "_type": "ComparatorSetPipelinePayload",
            "kind": "ComparatorSetPayload",
            "set": [
                "100218",
                "100240",
                "102272",
                "132266",
                "100022",
                "101258",
                "100585",
                "131871",
                "102890",
                "102830",
                "102084",
                "100852",
                "100163",
                "101526",
                "102324",
                "103072",
                "101893",
                "100023",
                "101277",
                "130302",
                "102131",
                "100449",
            ],
        },
        "jobId": "7af1abc5-88a9-4447-bdf7-92f4f95dc772",
        "type": "comparator-set",
        "runType": "default",
    }
    ```

    Custom-data message will be of the form:

    ```json
    {
        "jobId": "79ca4d18-d065-4180-9e0d-10c9b3fdd1a8",
        "type": "custom-data",
        "runType": "custom",
        "runId": "c321ef6a-3b1c-4ce2-8e32-0d0167bf2fa7",
        "year": 2022,
        "urn": "142875",
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
            "revenueReserve": 0.0,
            "totalPupils": 0.0,
            "percentFreeSchoolMeals": 0.0,
            "percentSpecialEducationNeeds": 0.0,
            "totalInternalFloorArea": 0.0,
            "workforceFTE": 0.0,
            "teachersFTE": 0.0,
            "percentTeacherWithQualifiedStatus": 0.0,
            "seniorLeadershipFTE": 0.0,
            "teachingAssistantFTE": 0.0,
            "nonClassroomSupportStaffFTE": 0.0,
            "auxiliaryStaffFTE": 0.0,
            "workforceHeadcount": 0.0,
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
