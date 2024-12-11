import { SchoolExpenditureHistory } from "src/services";
import { HistoricData2Section } from "../types";
import { PoundsPerMetreSq, PoundsPerPupil } from "src/components";

/* eslint-disable react-refresh/only-export-components */
export * from "src/views/historic-data-2/partials/spending-section.tsx";

export const spendingSections: HistoricData2Section<SchoolExpenditureHistory>[] =
  [
    {
      heading: "Teaching and teaching support staff",
      charts: [
        {
          name: "Total teaching and teaching support staff costs",
          field: "totalTeachingSupportStaffCosts",
          perUnitDimension: PoundsPerPupil,
        },
        {
          name: "Teaching staff costs",
          field: "teachingStaffCosts",
          perUnitDimension: PoundsPerPupil,
        },
        {
          name: "Supply teaching staff",
          field: "supplyTeachingStaffCosts",
          perUnitDimension: PoundsPerPupil,
        },
        {
          name: "Educational consultancy",
          field: "educationalConsultancyCosts",
          perUnitDimension: PoundsPerPupil,
        },
        {
          name: "Education support staff",
          field: "educationSupportStaffCosts",
          perUnitDimension: PoundsPerPupil,
        },
        {
          name: "Agency supply teaching staff",
          field: "agencySupplyTeachingStaffCosts",
          perUnitDimension: PoundsPerPupil,
        },
      ],
    },
    {
      heading: "Non-educational support staff",
      charts: [
        {
          name: "Total non-educational support staff costs",
          field: "totalNonEducationalSupportStaffCosts",
          perUnitDimension: PoundsPerPupil,
        },
        {
          name: "Administrative and clerical staff costs",
          field: "administrativeClericalStaffCosts",
          perUnitDimension: PoundsPerPupil,
        },
        {
          name: "Auditor costs",
          field: "auditorsCosts",
          perUnitDimension: PoundsPerPupil,
        },
        {
          name: "Other staff costs",
          field: "otherStaffCosts",
          perUnitDimension: PoundsPerPupil,
        },
        {
          name: "Professional services (non-curriculum) cost",
          field: "professionalServicesNonCurriculumCosts",
          perUnitDimension: PoundsPerPupil,
        },
      ],
    },
    {
      heading: "Educational supplies",
      charts: [
        {
          name: "Total educational supplies costs",
          field: "totalEducationalSuppliesCosts",
          perUnitDimension: PoundsPerPupil,
        },
        {
          name: "Examination fees costs",
          field: "examinationFeesCosts",
          perUnitDimension: PoundsPerPupil,
        },
        {
          name: "Learning resources (non ICT equipment) costs",
          field: "learningResourcesNonIctCosts",
          perUnitDimension: PoundsPerPupil,
        },
      ],
    },
    {
      heading: "Educational ICT",
      charts: [
        {
          name: "ICT learning resources costs",
          field: "learningResourcesIctCosts",
          perUnitDimension: PoundsPerPupil,
        },
      ],
    },
    {
      heading: "Premises staff and services",
      charts: [
        {
          name: "Total premises staff and services costs",
          field: "totalPremisesStaffServiceCosts",
          perUnitDimension: PoundsPerMetreSq,
        },
        {
          name: "Cleaning and caretaking costs",
          field: "cleaningCaretakingCosts",
          perUnitDimension: PoundsPerMetreSq,
        },
        {
          name: "Maintenance of premises costs",
          field: "maintenancePremisesCosts",
          perUnitDimension: PoundsPerMetreSq,
        },
        {
          name: "Other occupation costs",
          field: "otherOccupationCosts",
          perUnitDimension: PoundsPerMetreSq,
        },
        {
          name: "Premises staff costs",
          field: "premisesStaffCosts",
          perUnitDimension: PoundsPerMetreSq,
        },
      ],
    },
    {
      heading: "Utilities",
      charts: [
        {
          name: "Total utilities costs",
          field: "totalUtilitiesCosts",
          perUnitDimension: PoundsPerMetreSq,
        },
        {
          name: "Energy costs",
          field: "energyCosts",
          perUnitDimension: PoundsPerMetreSq,
        },
        {
          name: "Water and sewerage costs",
          field: "waterSewerageCosts",
          perUnitDimension: PoundsPerMetreSq,
        },
      ],
    },
    {
      heading: "Administrative supplies",
      charts: [
        {
          name: "Administration supplies (non educational) costs",
          field: "administrativeSuppliesCosts",
          perUnitDimension: PoundsPerPupil,
        },
      ],
    },
    {
      heading: "Catering staff and services",
      charts: [
        {
          name: "Total catering costs",
          field: "totalCateringCostsField",
          perUnitDimension: PoundsPerPupil,
        },
        {
          name: "Catering staff costs",
          field: "cateringStaffCosts",
          perUnitDimension: PoundsPerPupil,
        },
        {
          name: "Catering supplies costs",
          field: "cateringSuppliesCosts",
          perUnitDimension: PoundsPerPupil,
        },
      ],
    },
    {
      heading: "Other costs",
      charts: [
        {
          name: "Total other costs",
          field: "totalOtherCosts",
          perUnitDimension: PoundsPerPupil,
        },
        {
          name: "Direct revenue financing costs",
          field: "directRevenueFinancingCosts",
          perUnitDimension: PoundsPerPupil,
        },
        {
          name: "Grounds maintenance costs",
          field: "groundsMaintenanceCosts",
          perUnitDimension: PoundsPerPupil,
        },
        {
          name: "Indirect employee expenses costs",
          field: "indirectEmployeeExpenses",
          perUnitDimension: PoundsPerPupil,
        },
        {
          name: "Interest changes for loan and bank costs",
          field: "interestChargesLoanBank",
          perUnitDimension: PoundsPerPupil,
        },
        {
          name: "Other insurance premiums costs",
          field: "otherInsurancePremiumsCosts",
          perUnitDimension: PoundsPerPupil,
        },
        {
          name: "PFI charges costs",
          field: "privateFinanceInitiativeCharges",
          perUnitDimension: PoundsPerPupil,
        },
        {
          name: "Rents and rates costs",
          field: "rentRatesCosts",
          perUnitDimension: PoundsPerPupil,
        },
        {
          name: "Special facilities costs",
          field: "specialFacilitiesCosts",
          perUnitDimension: PoundsPerPupil,
        },
        {
          name: "Staff development and training costs",
          field: "staffDevelopmentTrainingCosts",
          perUnitDimension: PoundsPerPupil,
        },
        {
          name: "Staff-related insurance costs",
          field: "staffRelatedInsuranceCosts",
          perUnitDimension: PoundsPerPupil,
        },
        {
          name: "Supply teacher insurance costs",
          field: "supplyTeacherInsurableCosts",
          perUnitDimension: PoundsPerPupil,
        },
        {
          name: "Community focused school staff (maintained schools only)",
          field: "communityFocusedSchoolStaff",
          type: "school",
          perUnitDimension: PoundsPerPupil,
        },
        {
          name: "Community focused school costs (maintained schools only)",
          field: "communityFocusedSchoolCosts",
          type: "school",
          perUnitDimension: PoundsPerPupil,
        },
      ],
    },
  ];
