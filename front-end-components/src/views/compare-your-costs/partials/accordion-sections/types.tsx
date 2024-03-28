export type UtilitiesProps = {
  schools: UtilitiesData[];
};

export type UtilitiesData = {
  urn: string;
  name: string;
  schoolType: string;
  localAuthority: string;
  totalIncome: number;
  totalExpenditure: number;
  numberOfPupils: bigint;
  totalUtilitiesCosts: number;
  energyCosts: number;
  waterSewerageCosts: number;
};

export type TeachingSupportStaffProps = {
  schools: TeachingSupportStaffData[];
};

export type TeachingSupportStaffData = {
  urn: string;
  name: string;
  schoolType: string;
  localAuthority: string;
  totalIncome: number;
  totalExpenditure: number;
  numberOfPupils: bigint;
  totalTeachingSupportStaffCosts: number;
  teachingStaffCosts: number;
  supplyTeachingStaffCosts: number;
  educationalConsultancyCosts: number;
  educationSupportStaffCosts: number;
  agencySupplyTeachingStaffCosts: number;
};

export type OtherCostsProps = {
  schools: OtherCostsData[];
};

export type OtherCostsData = {
  urn: string;
  name: string;
  schoolType: string;
  localAuthority: string;
  totalIncome: number;
  totalExpenditure: number;
  numberOfPupils: bigint;
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
};

export type PremisesStaffServicesProps = {
  schools: PremisesStaffServicesData[];
};

export type PremisesStaffServicesData = {
  urn: string;
  name: string;
  schoolType: string;
  localAuthority: string;
  totalIncome: number;
  totalExpenditure: number;
  numberOfPupils: bigint;
  totalPremisesStaffServiceCosts: number;
  cleaningCaretakingCosts: number;
  maintenancePremisesCosts: number;
  otherOccupationCosts: number;
  premisesStaffCosts: number;
};

export type NonEducationalSupportStaffProps = {
  id: string; // move to shared props
  schools: NonEducationalSupportStaffData[];
};

export type NonEducationalSupportStaffData = {
  urn: string;
  name: string;
  schoolType: string;
  localAuthority: string;
  totalIncome: number;
  totalExpenditure: number;
  numberOfPupils: bigint;
  totalNonEducationalSupportStaffCosts: number;
  administrativeClericalStaffCosts: number;
  auditorsCosts: number;
  otherStaffCosts: number;
  professionalServicesNonCurriculumCosts: number;
};

export type EducationalSuppliesProps = {
  schools: EducationalSuppliesData[];
};

export type EducationalSuppliesData = {
  urn: string;
  name: string;
  schoolType: string;
  localAuthority: string;
  totalIncome: number;
  totalExpenditure: number;
  numberOfPupils: bigint;
  totalEducationalSuppliesCosts: number;
  examinationFeesCosts: number;
  breakdownEducationalSuppliesCosts: number;
  learningResourcesNonIctCosts: number;
};

export type EducationalIctProps = {
  schools: EducationalIctData[];
};

export type EducationalIctData = {
  urn: string;
  name: string;
  schoolType: string;
  localAuthority: string;
  totalIncome: number;
  totalExpenditure: number;
  numberOfPupils: bigint;
  learningResourcesIctCosts: number;
};

export type CateringStaffServicesProps = {
  schools: CateringStaffServicesData[];
};

export type CateringStaffServicesData = {
  urn: string;
  name: string;
  schoolType: string;
  localAuthority: string;
  totalIncome: number;
  totalExpenditure: number;
  numberOfPupils: bigint;
  netCateringCosts: number;
  cateringStaffCosts: number;
  cateringSuppliesCosts: number;
  incomeCatering: number;
};

export type AdministrativeSuppliesProps = {
  schools: AdministrativeSuppliesData[];
};

export type AdministrativeSuppliesData = {
  urn: string;
  name: string;
  schoolType: string;
  localAuthority: string;
  totalIncome: number;
  totalExpenditure: number;
  numberOfPupils: bigint;
  administrativeSuppliesCosts: number;
};
