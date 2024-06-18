export type TeachingSupportStaffData = {
  companyNumber: string;
  totalTeachingSupportStaffCosts: number;
  teachingStaffCosts: number;
  supplyTeachingStaffCosts: number;
  educationalConsultancyCosts: number;
  educationSupportStaffCosts: number;
  agencySupplyTeachingStaffCosts: number;
  trustName: string;
};

export type NonEducationalSupportStaffData = {
  companyNumber: string;
  totalNonEducationalSupportStaffCosts: number;
  administrativeClericalStaffCosts: number;
  auditorsCosts: number;
  otherStaffCosts: number;
  professionalServicesNonCurriculumCosts: number;
  trustName: string;
};

export type EducationalSuppliesData = {
  companyNumber: string;
  totalEducationalSuppliesCosts: number;
  examinationFeesCosts: number;
  learningResourcesNonIctCosts: number;
  trustName: string;
};

export type EducationalIctData = {
  companyNumber: string;
  learningResourcesIctCosts: number;
  trustName: string;
};

export type PremisesStaffServicesData = {
  companyNumber: string;
  totalPremisesStaffServiceCosts: number;
  cleaningCaretakingCosts: number;
  maintenancePremisesCosts: number;
  otherOccupationCosts: number;
  premisesStaffCosts: number;
  trustName: string;
};

export type UtilitiesData = {
  companyNumber: string;
  totalUtilitiesCosts: number;
  energyCosts: number;
  waterSewerageCosts: number;
  trustName: string;
};

export type AdministrativeSuppliesData = {
  companyNumber: string;
  administrativeSuppliesCosts: number;
  trustName: string;
};

export type CateringStaffServicesData = {
  companyNumber: string;
  totalGrossCateringCosts: number;
  cateringStaffCosts: number;
  cateringSuppliesCosts: number;
  trustName: string;
};

export type OtherCostsData = {
  companyNumber: string;
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
  trustName: string;
};
