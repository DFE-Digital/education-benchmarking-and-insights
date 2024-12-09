import { SchoolExpenditureHistory } from "src/services";
import { HistoricData2Section } from "../types";

const spendingSections: HistoricData2Section<SchoolExpenditureHistory>[] = [
  {
    heading: "Teaching and teaching support staff",
    charts: [
      {
        name: "Total teaching and teaching support staff costs",
        field: "totalTeachingSupportStaffCosts",
      },
      {
        name: "Teaching staff costs",
        field: "teachingStaffCosts",
      },
      {
        name: "Supply teaching staff",
        field: "supplyTeachingStaffCosts",
      },
      {
        name: "Educational consultancy",
        field: "educationalConsultancyCosts",
      },
      {
        name: "Education support staff",
        field: "educationSupportStaffCosts",
      },
      {
        name: "Agency supply teaching staff",
        field: "agencySupplyTeachingStaffCosts",
      },
    ],
  },
  {
    heading: "Non-educational support staff",
    charts: [
      {
        name: "Total non-educational support staff costs",
        field: "totalNonEducationalSupportStaffCosts",
      },
      {
        name: "Administrative and clerical staff costs",
        field: "administrativeClericalStaffCosts",
      },
      {
        name: "Auditor costs",
        field: "auditorsCosts",
      },
      {
        name: "Other staff costs",
        field: "otherStaffCosts",
      },
      {
        name: "Professional services (non-curriculum) cost",
        field: "professionalServicesNonCurriculumCosts",
      },
    ],
  },
  {
    heading: "Educational supplies",
    charts: [
      {
        name: "Total educational supplies costs",
        field: "totalEducationalSuppliesCosts",
      },
      {
        name: "Examination fees costs",
        field: "examinationFeesCosts",
      },
      {
        name: "Learning resources (non ICT equipment) costs",
        field: "learningResourcesNonIctCosts",
      },
    ],
  },
  {
    heading: "Educational ICT",
    charts: [
      {
        name: "ICT learning resources costs",
        field: "learningResourcesIctCosts",
      },
    ],
  },
  {
    heading: "Premises staff and services",
    charts: [
      {
        name: "Total premises staff and services costs",
        field: "totalPremisesStaffServiceCosts",
      },
      {
        name: "Cleaning and caretaking costs",
        field: "cleaningCaretakingCosts",
      },
      {
        name: "Maintenance of premises costs",
        field: "maintenancePremisesCosts",
      },
      {
        name: "Other occupation costs",
        field: "otherOccupationCosts",
      },
      {
        name: "Premises staff costs",
        field: "premisesStaffCosts",
      },
    ],
  },
  {
    heading: "Utilities",
    charts: [
      {
        name: "Total utilities costs",
        field: "totalUtilitiesCosts",
      },
      {
        name: "Energy costs",
        field: "energyCosts",
      },
      {
        name: "Water and sewerage costs",
        field: "waterSewerageCosts",
      },
    ],
  },
  {
    heading: "Administrative supplies",
    charts: [
      {
        name: "Administration supplies (non educational) costs",
        field: "administrativeSuppliesCosts",
      },
    ],
  },
  {
    heading: "Catering staff and services",
    charts: [
      {
        name: "Total catering costs",
        field: "totalCateringCostsField",
      },
      {
        name: "Catering staff costs",
        field: "cateringStaffCosts",
      },
      {
        name: "Catering supplies costs",
        field: "cateringSuppliesCosts",
      },
    ],
  },
  {
    heading: "Other costs",
    charts: [
      {
        name: "Total other costs",
        field: "totalOtherCosts",
      },
      {
        name: "Direct revenue financing costs",
        field: "directRevenueFinancingCosts",
      },
      {
        name: "Grounds maintenance costs",
        field: "groundsMaintenanceCosts",
      },
      {
        name: "Indirect employee expenses costs",
        field: "indirectEmployeeExpenses",
      },
      {
        name: "Interest changes for loan and bank costs",
        field: "interestChargesLoanBank",
      },
      {
        name: "Other insurance premiums costs",
        field: "otherInsurancePremiumsCosts",
      },
      {
        name: "PFI charges costs",
        field: "privateFinanceInitiativeCharges",
      },
      {
        name: "Rents and rates costs",
        field: "rentRatesCosts",
      },
      {
        name: "Special facilities costs",
        field: "specialFacilitiesCosts",
      },
      {
        name: "Staff development and training costs",
        field: "staffDevelopmentTrainingCosts",
      },
      {
        name: "Staff-related insurance costs",
        field: "staffRelatedInsuranceCosts",
      },
      {
        name: "Supply teacher insurance costs",
        field: "supplyTeacherInsurableCosts",
      },
      {
        name: "Community focused school staff (maintained schools only)",
        field: "communityFocusedSchoolStaff",
        type: "school",
      },
      {
        name: "Community focused school costs (maintained schools only)",
        field: "communityFocusedSchoolCosts",
        type: "school",
      },
    ],
  },
];

export { spendingSections };
