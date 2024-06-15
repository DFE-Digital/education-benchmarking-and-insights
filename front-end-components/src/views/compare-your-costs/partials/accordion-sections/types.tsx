export type TeachingSupportStaffData = {
  urn: string;
  schoolType: string;
  totalTeachingSupportStaffCosts: number;
  teachingStaffCosts: number;
  supplyTeachingStaffCosts: number;
  educationalConsultancyCosts: number;
  educationSupportStaffCosts: number;
  agencySupplyTeachingStaffCosts: number;
  totalPupils: bigint;
  schoolName: string;
  laName: string;
};

export type NonEducationalSupportStaffData = {
  urn: string;
  schoolType: string;
  totalNonEducationalSupportStaffCosts: number;
  administrativeClericalStaffCosts: number;
  auditorsCosts: number;
  otherStaffCosts: number;
  professionalServicesNonCurriculumCosts: number;
  totalPupils: bigint;
  schoolName: string;
  laName: string;
};

export type EducationalSuppliesData = {
  urn: string;
  schoolType: string;
  totalEducationalSuppliesCosts: number;
  examinationFeesCosts: number;
  learningResourcesNonIctCosts: number;
  totalPupils: bigint;
  schoolName: string;
  laName: string;
};

export type EducationalIctData = {
  urn: string;
  schoolType: string;
  learningResourcesIctCosts: number;
  totalPupils: bigint;
  schoolName: string;
  laName: string;
};

export type PremisesStaffServicesData = {
  urn: string;
  schoolType: string;
  totalPremisesStaffServiceCosts: number;
  cleaningCaretakingCosts: number;
  maintenancePremisesCosts: number;
  otherOccupationCosts: number;
  premisesStaffCosts: number;
  totalPupils: bigint;
  schoolName: string;
  laName: string;
};

export type UtilitiesData = {
  urn: string;
  schoolType: string;
  totalUtilitiesCosts: number;
  energyCosts: number;
  waterSewerageCosts: number;
  totalPupils: bigint;
  schoolName: string;
  laName: string;
};

export type AdministrativeSuppliesData = {
  urn: string;
  schoolType: string;
  administrativeSuppliesCosts: number;
  totalPupils: bigint;
  schoolName: string;
  laName: string;
};

export type CateringStaffServicesData = {
  urn: string;
  schoolType: string;
  totalGrossCateringCosts: number;
  cateringStaffCosts: number;
  cateringSuppliesCosts: number;
  totalPupils: bigint;
  schoolName: string;
  laName: string;
};

export type OtherCostsData = {
  urn: string;
  schoolType: string;
  totalOtherCosts: number;
  otherInsurancePremiumsCosts: number;
  directRevenueFinancingCosts: number;
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
  totalPupils: bigint;
  schoolName: string;
  laName: string;
};
