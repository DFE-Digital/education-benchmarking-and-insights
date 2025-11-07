import { SchoolExpenditureCommon } from "../../types";

export type TeachingSupportStaffData = SchoolExpenditureCommon & {
  totalTeachingSupportStaffCosts: number;
  teachingStaffCosts: number;
  supplyTeachingStaffCosts: number;
  educationalConsultancyCosts: number;
  educationSupportStaffCosts: number;
  agencySupplyTeachingStaffCosts: number;
};

export type NonEducationalSupportStaffData = SchoolExpenditureCommon & {
  totalNonEducationalSupportStaffCosts: number;
  administrativeClericalStaffCosts: number;
  auditorsCosts: number;
  otherStaffCosts: number;
  professionalServicesNonCurriculumCosts: number;
};

export type EducationalSuppliesData = SchoolExpenditureCommon & {
  totalEducationalSuppliesCosts: number;
  examinationFeesCosts: number;
  learningResourcesNonIctCosts: number;
};

export type EducationalIctData = SchoolExpenditureCommon & {
  learningResourcesIctCosts: number;
};

export type PremisesStaffServicesData = SchoolExpenditureCommon & {
  totalPremisesStaffServiceCosts: number;
  cleaningCaretakingCosts: number;
  maintenancePremisesCosts: number;
  otherOccupationCosts: number;
  premisesStaffCosts: number;
};

export type UtilitiesData = SchoolExpenditureCommon & {
  totalUtilitiesCosts: number;
  energyCosts: number;
  waterSewerageCosts: number;
};

export type AdministrativeSuppliesData = SchoolExpenditureCommon & {
  administrativeSuppliesNonEducationalCosts: number;
};

export type CateringStaffServicesData = SchoolExpenditureCommon & {
  totalGrossCateringCosts: number;
  totalNetCateringCosts: number;
  cateringStaffCosts: number;
  cateringSuppliesCosts: number;
};

export type OtherCostsData = SchoolExpenditureCommon & {
  totalOtherCosts: number;
  otherInsurancePremiumsCosts: number;
  groundsMaintenanceCosts: number;
  indirectEmployeeExpenses: number;
  interestChargesLoanBank: number;
  privateFinanceInitiativeCharges: number;
  rentRatesCosts: number;
  specialFacilitiesCosts: number;
  staffDevelopmentTrainingCosts: number;
  staffRelatedInsuranceCosts: number;
  supplyTeacherInsurableCosts: number;
  communityFocusedSchoolStaff: number;
  communityFocusedSchoolCosts: number;
};

export type CompareYourCosts2Props = {
  id: string;
  onFetching?: (fetching: boolean) => void;
  type: "school" | "trust";
};
