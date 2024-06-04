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
  totalPupils: bigint;
  schoolName: string;
  laName: string;
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
  totalPupils: bigint;
  schoolName: string;
  laName: string;
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
  totalPupils: bigint;
  schoolName: string;
  laName: string;
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
  totalPupils: bigint;
  schoolName: string;
  laName: string;
};

export type NonEducationalSupportStaffProps = {
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
  totalPupils: bigint;
  schoolName: string;
  laName: string;
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
  totalPupils: bigint;
  schoolName: string;
  laName: string;
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
  totalPupils: bigint;
  schoolName: string;
  laName: string;
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
  totalPupils: bigint;
  schoolName: string;
  laName: string;
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
  totalPupils: bigint;
  schoolName: string;
  laName: string;
};
