export type SchoolExpenditure = {
  urn: string;
  schoolName: string;
  schoolType: string;
  laName: string;
  totalPupils: bigint;
  totalInternalFloorArea: number;
  periodCoveredByReturn: number;
};

type AdministrativeSuppliesExpenditureBase = {
  administrativeSuppliesNonEducationalCosts: number;
};

export type AdministrativeSuppliesExpenditure = SchoolExpenditure &
  AdministrativeSuppliesExpenditureBase;

type CateringStaffServicesExpenditureBase = {
  totalGrossCateringCosts: number;
  totalNetCateringCosts: number;
  cateringStaffCosts: number;
  cateringSuppliesCosts: number;
};

export type CateringStaffServicesExpenditure = SchoolExpenditure &
  CateringStaffServicesExpenditureBase;

type EducationalIctExpenditureBase = {
  learningResourcesIctCosts: number;
};

export type EducationalIctExpenditure = SchoolExpenditure &
  EducationalIctExpenditureBase;

type EducationalSuppliesExpenditureBase = {
  totalEducationalSuppliesCosts: number;
  examinationFeesCosts: number;
  learningResourcesNonIctCosts: number;
};

export type EducationalSuppliesExpenditure = SchoolExpenditure &
  EducationalSuppliesExpenditureBase;

type NonEducationalSupportStaffExpenditureBase = {
  totalNonEducationalSupportStaffCosts: number;
  administrativeClericalStaffCosts: number;
  auditorsCosts: number;
  otherStaffCosts: number;
  professionalServicesNonCurriculumCosts: number;
};

export type NonEducationalSupportStaffExpenditure = SchoolExpenditure &
  NonEducationalSupportStaffExpenditureBase;

type OtherCostsDataExpenditureBase = {
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

export type OtherCostsDataExpenditure = SchoolExpenditure &
  OtherCostsDataExpenditureBase;

type PremisesStaffServicesExpenditureBase = {
  totalPremisesStaffServiceCosts: number;
  cleaningCaretakingCosts: number;
  maintenancePremisesCosts: number;
  otherOccupationCosts: number;
  premisesStaffCosts: number;
};

export type PremisesStaffServicesExpenditure = SchoolExpenditure &
  PremisesStaffServicesExpenditureBase;

type TeachingSupportStaffExpenditureBase = {
  totalTeachingSupportStaffCosts: number;
  teachingStaffCosts: number;
  supplyTeachingStaffCosts: number;
  educationalConsultancyCosts: number;
  educationSupportStaffCosts: number;
  agencySupplyTeachingStaffCosts: number;
};

export type TeachingSupportStaffExpenditure = SchoolExpenditure &
  TeachingSupportStaffExpenditureBase;

type TotalExpenditureExpenditureBase = {
  totalExpenditure: number;
};

export type TotalExpenditureExpenditure = SchoolExpenditure &
  TotalExpenditureExpenditureBase;

type UtilitiesExpenditureBase = {
  totalUtilitiesCosts: number;
  energyCosts: number;
  waterSewerageCosts: number;
};

export type UtilitiesExpenditure = SchoolExpenditure & UtilitiesExpenditureBase;

export type TrustExpenditure = {
  companyNumber: string;
  trustName: string;
};

type AdministrativeSuppliesTrustExpenditureBase =
  AdministrativeSuppliesExpenditureBase & {
    schoolAdministrativeSuppliesNonEducationalCosts: number;
    centralAdministrativeSuppliesNonEducationalCosts: number;
  };

export type AdministrativeSuppliesTrustExpenditure = TrustExpenditure &
  AdministrativeSuppliesTrustExpenditureBase;

type CateringStaffServicesTrustExpenditureBase =
  CateringStaffServicesExpenditureBase & {
    schoolTotalGrossCateringCosts: number;
    schoolTotalNetCateringCosts: number;
    schoolCateringStaffCosts: number;
    schoolCateringSuppliesCosts: number;

    centralTotalGrossCateringCosts: number;
    centralTotalNetCateringCosts: number;
    centralCateringStaffCosts: number;
    centralCateringSuppliesCosts: number;
  };

export type CateringStaffServicesTrustExpenditure = TrustExpenditure &
  CateringStaffServicesTrustExpenditureBase;

type EducationalIctTrustExpenditureBase = EducationalIctExpenditureBase & {
  schoolLearningResourcesIctCosts: number;
  centralLearningResourcesIctCosts: number;
};

export type EducationalIctTrustExpenditure = TrustExpenditure &
  EducationalIctTrustExpenditureBase;

type EducationalSuppliesTrustExpenditureBase =
  EducationalSuppliesExpenditureBase & {
    schoolTotalEducationalSuppliesCosts: number;
    schoolExaminationFeesCosts: number;
    schoolLearningResourcesNonIctCosts: number;

    centralTotalEducationalSuppliesCosts: number;
    centralExaminationFeesCosts: number;
    centralLearningResourcesNonIctCosts: number;
  };

export type EducationalSuppliesTrustExpenditure = TrustExpenditure &
  EducationalSuppliesTrustExpenditureBase;

type NonEducationalSupportStaffTrustExpenditureBase =
  NonEducationalSupportStaffExpenditureBase & {
    schoolTotalNonEducationalSupportStaffCosts: number;
    schoolAdministrativeClericalStaffCosts: number;
    schoolAuditorsCosts: number;
    schoolOtherStaffCosts: number;
    schoolProfessionalServicesNonCurriculumCosts: number;

    centralTotalNonEducationalSupportStaffCosts: number;
    centralAdministrativeClericalStaffCosts: number;
    centralAuditorsCosts: number;
    centralOtherStaffCosts: number;
    centralProfessionalServicesNonCurriculumCosts: number;
  };

export type NonEducationalSupportStaffTrustExpenditure = TrustExpenditure &
  NonEducationalSupportStaffTrustExpenditureBase;

type OtherCostsDataTrustExpenditureBase = OtherCostsDataExpenditureBase & {
  schoolTotalOtherCosts: number;
  schoolDirectRevenueFinancingCosts: number;
  schoolGroundsMaintenanceCosts: number;
  schoolIndirectEmployeeExpenses: number;
  schoolInterestChargesLoanBank: number;
  schoolOtherInsurancePremiumsCosts: number;
  schoolPrivateFinanceInitiativeCharges: number;
  schoolRentRatesCosts: number;
  schoolSpecialFacilitiesCosts: number;
  schoolStaffDevelopmentTrainingCosts: number;
  schoolStaffRelatedInsuranceCosts: number;
  schoolSupplyTeacherInsurableCosts: number;
  schoolCommunityFocusedSchoolStaff: number;
  schoolCommunityFocusedSchoolCosts: number;

  centralTotalOtherCosts: number;
  centralDirectRevenueFinancingCosts: number;
  centralGroundsMaintenanceCosts: number;
  centralIndirectEmployeeExpenses: number;
  centralInterestChargesLoanBank: number;
  centralOtherInsurancePremiumsCosts: number;
  centralPrivateFinanceInitiativeCharges: number;
  centralRentRatesCosts: number;
  centralSpecialFacilitiesCosts: number;
  centralStaffDevelopmentTrainingCosts: number;
  centralStaffRelatedInsuranceCosts: number;
  centralSupplyTeacherInsurableCosts: number;
  centralCommunityFocusedSchoolStaff: number;
  centralCommunityFocusedSchoolCosts: number;
};

export type OtherCostsDataTrustExpenditure = TrustExpenditure &
  OtherCostsDataTrustExpenditureBase;

type PremisesStaffServicesTrustExpenditureBase =
  PremisesStaffServicesExpenditureBase & {
    schoolTotalPremisesStaffServiceCosts: number;
    schoolCleaningCaretakingCosts: number;
    schoolMaintenancePremisesCosts: number;
    schoolOtherOccupationCosts: number;
    schoolPremisesStaffCosts: number;

    centralTotalPremisesStaffServiceCosts: number;
    centralCleaningCaretakingCosts: number;
    centralMaintenancePremisesCosts: number;
    centralOtherOccupationCosts: number;
    centralPremisesStaffCosts: number;
  };

export type PremisesStaffServicesTrustExpenditure = TrustExpenditure &
  PremisesStaffServicesTrustExpenditureBase;

type TeachingSupportStaffTrustExpenditureBase =
  TeachingSupportStaffExpenditureBase & {
    schoolTotalTeachingSupportStaffCosts: number;
    schoolTeachingStaffCosts: number;
    schoolSupplyTeachingStaffCosts: number;
    schoolEducationalConsultancyCosts: number;
    schoolEducationSupportStaffCosts: number;
    schoolAgencySupplyTeachingStaffCosts: number;

    centralTotalTeachingSupportStaffCosts: number;
    centralTeachingStaffCosts: number;
    centralSupplyTeachingStaffCosts: number;
    centralEducationalConsultancyCosts: number;
    centralEducationSupportStaffCosts: number;
    centralAgencySupplyTeachingStaffCosts: number;
  };

export type TeachingSupportStaffTrustExpenditure = TrustExpenditure &
  TeachingSupportStaffTrustExpenditureBase;

type TotalExpenditureTrustExpenditureBase = TotalExpenditureExpenditureBase & {
  schoolTotalExpenditure: number;
  centralTotalExpenditure: number;
};

export type TotalExpenditureTrustExpenditure = TrustExpenditure &
  TotalExpenditureTrustExpenditureBase;

type UtilitiesTrustExpenditureBase = UtilitiesExpenditureBase & {
  schoolTotalUtilitiesCosts: number;
  schoolEnergyCosts: number;
  schoolWaterSewerageCosts: number;

  centralTotalUtilitiesCosts: number;
  centralEnergyCosts: number;
  centralWaterSewerageCosts: number;
};

export type UtilitiesTrustExpenditure = TrustExpenditure &
  UtilitiesTrustExpenditureBase;

export type SchoolExpenditureHistory = AdministrativeSuppliesExpenditureBase &
  CateringStaffServicesExpenditureBase &
  EducationalIctExpenditureBase &
  EducationalSuppliesExpenditureBase &
  NonEducationalSupportStaffExpenditureBase &
  OtherCostsDataExpenditureBase &
  PremisesStaffServicesExpenditureBase &
  TeachingSupportStaffExpenditureBase &
  TotalExpenditureExpenditureBase &
  UtilitiesExpenditureBase & {
    totalCateringCostsField: number;
  } & HistoryBase;

export type SchoolHistoryComparison<T extends HistoryRow> = {
  startYear?: number;
  endYear?: number;
  school?: Array<T>;
  comparatorSetAverage?: Array<T>;
  nationalAverage?: Array<T>;
};

export type HistoryBase = {
  year: number;
  term: string;
};

type BalanceBase = {
  inYearBalance: number;
  revenueReserve: number;
};

type TrustBalanceBase = BalanceBase & {
  schoolInYearBalance: number;
  centralInYearBalance: number;
};

export type TrustBalance = TrustBalanceBase & {
  companyNumber: string;
  trustName: string;
};

export type HistoryRow = {
  year: number;
};

export type HistoryRows<T extends HistoryRow> = {
  startYear: number;
  endYear: number;
  rows: Array<T>;
};

export type BalanceHistoryItem = Partial<BalanceBase> & HistoryBase;
export type BalanceHistoryRow = Partial<BalanceBase> & HistoryRow;
export type BalanceHistoryRows = HistoryRows<BalanceHistoryRow>;

type CensusBase = {
  urn: string;
  totalPupils: bigint;
  workforce: number;
  workforceHeadcount: number;
  teachers: number;
  seniorLeadership: number;
  teachingAssistant: number;
  nonClassroomSupportStaff: number;
  auxiliaryStaff: number;
  percentTeacherWithQualifiedStatus: number;
};

export type Census = CensusBase & {
  schoolName: string;
  schoolType: string;
  laName: string;
};

export type CensusHistoryItem = Partial<CensusBase> & HistoryBase;
export type CensusHistoryRow = Partial<CensusBase> & HistoryRow;
export type CensusHistoryRows = HistoryRows<CensusHistoryRow>;

type IncomeBase = {
  totalIncome: number;
  totalGrantFunding: number;
  totalSelfGeneratedFunding: number;
  directRevenueFinancing: number;
  directGrants: number;
  prePost16Funding: number;
  otherDfeGrants: number;
  otherIncomeGrants: number;
  governmentSource: number;
  communityGrants: number;
  academies: number;
  incomeFacilitiesServices: number;
  incomeCatering: number;
  donationsVoluntaryFunds: number;
  receiptsSupplyTeacherInsuranceClaims: number;
  investmentIncome: number;
  otherSelfGeneratedIncome: number;
};

export type IncomeHistoryItem = Partial<IncomeBase> & HistoryBase;
export type IncomeHistoryRow = Partial<IncomeBase> & HistoryRow;
export type IncomeHistoryRows = HistoryRows<IncomeHistoryRow>;

export type BudgetForecastReturn = {
  year: number;
  forecast?: number;
  actual?: number;
  forecastTotalPupils?: number;
  actualTotalPupils?: number;
  variance?: number;
  percentVariance?: number;
  varianceStatus?: string;
};

export type TotalCateringCostsField = keyof Pick<
  CateringStaffServicesExpenditureBase,
  "totalGrossCateringCosts" | "totalNetCateringCosts"
>;
