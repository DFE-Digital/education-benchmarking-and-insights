ALTER VIEW SchoolBalanceHistoric AS
SELECT s.URN,
       s.Year,
       s.TrustCompanyNumber,
       f.TotalPupils,
       f.TotalIncome,
       f.TotalExpenditure,
       f.InYearBalance,
       f.RevenueReserve,
       f.TotalIncomeCS,
       f.TotalExpenditureCS,
       f.InYearBalanceCS,
       f.RevenueReserveCS
FROM (SELECT *
      FROM School,
           AARHistoryYears) s
         LEFT OUTER JOIN Financial f ON s.Year = f.RunId AND f.URN = s.URN AND f.RunType = 'default'
WHERE s.FinanceType = 'Academy'
UNION
SELECT s.URN,
       s.Year,
       s.TrustCompanyNumber,
       f.TotalPupils,
       f.TotalIncome,
       f.TotalExpenditure,
       f.InYearBalance,
       f.RevenueReserve,
       f.TotalIncomeCS,
       f.TotalExpenditureCS,
       f.InYearBalanceCS,
       f.RevenueReserveCS
FROM (SELECT *
      FROM School,
           CFRHistoryYears) s
         LEFT OUTER JOIN Financial f ON s.Year = f.RunId AND f.URN = s.URN AND f.RunType = 'default'
WHERE s.FinanceType = 'Maintained'

GO

ALTER VIEW SchoolCensusHistoric AS
SELECT s.URN,
       s.Year,
       nf.TotalPupils,
       nf.WorkforceFTE,
       nf.WorkforceHeadcount,
       nf.TeachersFTE,
       nf.SeniorLeadershipFTE,
       nf.TeachingAssistantFTE,
       nf.NonClassroomSupportStaffFTE,
       nf.AuxiliaryStaffFTE,
       nf.PercentTeacherWithQualifiedStatus
FROM (SELECT *
      FROM School,
           AARHistoryYears) s
         LEFT OUTER JOIN NonFinancial nf ON s.Year = nf.RunId AND nf.URN = s.URN AND nf.RunType = 'default'
WHERE s.FinanceType = 'Academy'
UNION
SELECT s.URN,
       s.Year,
       nf.TotalPupils,
       nf.WorkforceFTE,
       nf.WorkforceHeadcount,
       nf.TeachersFTE,
       nf.SeniorLeadershipFTE,
       nf.TeachingAssistantFTE,
       nf.NonClassroomSupportStaffFTE,
       nf.AuxiliaryStaffFTE,
       nf.PercentTeacherWithQualifiedStatus
FROM (SELECT *
      FROM School,
           CFRHistoryYears) s
         LEFT OUTER JOIN NonFinancial nf ON s.Year = nf.RunId AND nf.URN = s.URN AND nf.RunType = 'default'
WHERE s.FinanceType = 'Maintained'

GO

ALTER VIEW SchoolExpenditureHistoric AS
SELECT s.URN,
       s.Year,
       s.TrustCompanyNumber,
       f.TotalPupils,
       f.TotalInternalFloorArea,
       f.TotalIncome,
       f.TotalExpenditure,
       f.TotalTeachingSupportStaffCosts,
       f.TeachingStaffCosts,
       f.SupplyTeachingStaffCosts,
       f.EducationalConsultancyCosts,
       f.EducationSupportStaffCosts,
       f.AgencySupplyTeachingStaffCosts,
       f.TotalNonEducationalSupportStaffCosts,
       f.AdministrativeClericalStaffCosts,
       f.AuditorsCosts,
       f.OtherStaffCosts,
       f.ProfessionalServicesNonCurriculumCosts,
       f.TotalEducationalSuppliesCosts,
       f.ExaminationFeesCosts,
       f.LearningResourcesNonIctCosts,
       f.LearningResourcesIctCosts,
       f.TotalPremisesStaffServiceCosts,
       f.CleaningCaretakingCosts,
       f.MaintenancePremisesCosts,
       f.OtherOccupationCosts,
       f.PremisesStaffCosts,
       f.TotalUtilitiesCosts,
       f.EnergyCosts,
       f.WaterSewerageCosts,
       f.AdministrativeSuppliesNonEducationalCosts,
       f.TotalGrossCateringCosts,
       f.CateringStaffCosts,
       f.CateringSuppliesCosts,
       f.TotalOtherCosts,
       f.DirectRevenueFinancingCosts,
       f.GroundsMaintenanceCosts,
       f.IndirectEmployeeExpenses,
       f.InterestChargesLoanBank,
       f.OtherInsurancePremiumsCosts,
       f.PrivateFinanceInitiativeCharges,
       f.RentRatesCosts,
       f.SpecialFacilitiesCosts,
       f.StaffDevelopmentTrainingCosts,
       f.StaffRelatedInsuranceCosts,
       f.SupplyTeacherInsurableCosts,
       f.CommunityFocusedSchoolStaff,
       f.CommunityFocusedSchoolCosts,
       f.TotalIncomeCS,
       f.TotalExpenditureCS,
       f.TotalTeachingSupportStaffCostsCS,
       f.TeachingStaffCostsCS,
       f.SupplyTeachingStaffCostsCS,
       f.EducationalConsultancyCostsCS,
       f.EducationSupportStaffCostsCS,
       f.AgencySupplyTeachingStaffCostsCS,
       f.TotalNonEducationalSupportStaffCostsCS,
       f.AdministrativeClericalStaffCostsCS,
       f.AuditorsCostsCS,
       f.OtherStaffCostsCS,
       f.ProfessionalServicesNonCurriculumCostsCS,
       f.TotalEducationalSuppliesCostsCS,
       f.ExaminationFeesCostsCS,
       f.LearningResourcesNonIctCostsCS,
       f.LearningResourcesIctCostsCS,
       f.TotalPremisesStaffServiceCostsCS,
       f.CleaningCaretakingCostsCS,
       f.MaintenancePremisesCostsCS,
       f.OtherOccupationCostsCS,
       f.PremisesStaffCostsCS,
       f.TotalUtilitiesCostsCS,
       f.EnergyCostsCS,
       f.WaterSewerageCostsCS,
       f.AdministrativeSuppliesNonEducationalCostsCS,
       f.TotalGrossCateringCostsCS,
       f.CateringStaffCostsCS,
       f.CateringSuppliesCostsCS,
       f.TotalOtherCostsCS,
       f.DirectRevenueFinancingCostsCS,
       f.GroundsMaintenanceCostsCS,
       f.IndirectEmployeeExpensesCS,
       f.InterestChargesLoanBankCS,
       f.OtherInsurancePremiumsCostsCS,
       f.PrivateFinanceInitiativeChargesCS,
       f.RentRatesCostsCS,
       f.SpecialFacilitiesCostsCS,
       f.StaffDevelopmentTrainingCostsCS,
       f.StaffRelatedInsuranceCostsCS,
       f.SupplyTeacherInsurableCostsCS
FROM (SELECT *
      FROM School,
           AARHistoryYears) s
         LEFT OUTER JOIN Financial f ON s.Year = f.RunId AND f.URN = s.URN AND f.RunType = 'default'
WHERE s.FinanceType = 'Academy'
UNION
SELECT s.URN,
       s.Year,
       s.TrustCompanyNumber,
       f.TotalPupils,
       f.TotalInternalFloorArea,
       f.TotalIncome,
       f.TotalExpenditure,
       f.TotalTeachingSupportStaffCosts,
       f.TeachingStaffCosts,
       f.SupplyTeachingStaffCosts,
       f.EducationalConsultancyCosts,
       f.EducationSupportStaffCosts,
       f.AgencySupplyTeachingStaffCosts,
       f.TotalNonEducationalSupportStaffCosts,
       f.AdministrativeClericalStaffCosts,
       f.AuditorsCosts,
       f.OtherStaffCosts,
       f.ProfessionalServicesNonCurriculumCosts,
       f.TotalEducationalSuppliesCosts,
       f.ExaminationFeesCosts,
       f.LearningResourcesNonIctCosts,
       f.LearningResourcesIctCosts,
       f.TotalPremisesStaffServiceCosts,
       f.CleaningCaretakingCosts,
       f.MaintenancePremisesCosts,
       f.OtherOccupationCosts,
       f.PremisesStaffCosts,
       f.TotalUtilitiesCosts,
       f.EnergyCosts,
       f.WaterSewerageCosts,
       f.AdministrativeSuppliesNonEducationalCosts,
       f.TotalGrossCateringCosts,
       f.CateringStaffCosts,
       f.CateringSuppliesCosts,
       f.TotalOtherCosts,
       f.DirectRevenueFinancingCosts,
       f.GroundsMaintenanceCosts,
       f.IndirectEmployeeExpenses,
       f.InterestChargesLoanBank,
       f.OtherInsurancePremiumsCosts,
       f.PrivateFinanceInitiativeCharges,
       f.RentRatesCosts,
       f.SpecialFacilitiesCosts,
       f.StaffDevelopmentTrainingCosts,
       f.StaffRelatedInsuranceCosts,
       f.SupplyTeacherInsurableCosts,
       f.CommunityFocusedSchoolStaff,
       f.CommunityFocusedSchoolCosts,
       f.TotalIncomeCS,
       f.TotalExpenditureCS,
       f.TotalTeachingSupportStaffCostsCS,
       f.TeachingStaffCostsCS,
       f.SupplyTeachingStaffCostsCS,
       f.EducationalConsultancyCostsCS,
       f.EducationSupportStaffCostsCS,
       f.AgencySupplyTeachingStaffCostsCS,
       f.TotalNonEducationalSupportStaffCostsCS,
       f.AdministrativeClericalStaffCostsCS,
       f.AuditorsCostsCS,
       f.OtherStaffCostsCS,
       f.ProfessionalServicesNonCurriculumCostsCS,
       f.TotalEducationalSuppliesCostsCS,
       f.ExaminationFeesCostsCS,
       f.LearningResourcesNonIctCostsCS,
       f.LearningResourcesIctCostsCS,
       f.TotalPremisesStaffServiceCostsCS,
       f.CleaningCaretakingCostsCS,
       f.MaintenancePremisesCostsCS,
       f.OtherOccupationCostsCS,
       f.PremisesStaffCostsCS,
       f.TotalUtilitiesCostsCS,
       f.EnergyCostsCS,
       f.WaterSewerageCostsCS,
       f.AdministrativeSuppliesNonEducationalCostsCS,
       f.TotalGrossCateringCostsCS,
       f.CateringStaffCostsCS,
       f.CateringSuppliesCostsCS,
       f.TotalOtherCostsCS,
       f.DirectRevenueFinancingCostsCS,
       f.GroundsMaintenanceCostsCS,
       f.IndirectEmployeeExpensesCS,
       f.InterestChargesLoanBankCS,
       f.OtherInsurancePremiumsCostsCS,
       f.PrivateFinanceInitiativeChargesCS,
       f.RentRatesCostsCS,
       f.SpecialFacilitiesCostsCS,
       f.StaffDevelopmentTrainingCostsCS,
       f.StaffRelatedInsuranceCostsCS,
       f.SupplyTeacherInsurableCostsCS
FROM (SELECT *
      FROM School,
           CFRHistoryYears) s
         LEFT OUTER JOIN Financial f ON s.Year = f.RunId AND f.URN = s.URN AND f.RunType = 'default'
WHERE s.FinanceType = 'Maintained'

GO

ALTER VIEW SchoolIncomeHistoric AS
SELECT s.URN,
       s.Year,
       s.TrustCompanyNumber,
       f.TotalPupils,
       f.TotalIncome,
       f.TotalExpenditure,
       f.TotalGrantFunding,
       f.TotalSelfGeneratedFunding,
       f.DirectRevenueFinancing,
       f.DirectGrants,
       f.PrePost16Funding,
       f.OtherDfeGrants,
       f.OtherIncomeGrants,
       f.GovernmentSource,
       f.CommunityGrants,
       f.Academies,
       f.IncomeFacilitiesServices,
       f.IncomeCateringServices,
       f.DonationsVoluntaryFunds,
       f.ReceiptsSupplyTeacherInsuranceClaims,
       f.InvestmentIncome,
       f.OtherSelfGeneratedIncome,
       f.TotalIncomeCS,
       f.TotalExpenditureCS,
       f.TotalGrantFundingCS,
       f.TotalSelfGeneratedFundingCS,
       f.DirectRevenueFinancingCS,
       f.DirectGrantsCS,
       f.PrePost16FundingCS,
       f.OtherDfeGrantsCS,
       f.OtherIncomeGrantsCS,
       f.GovernmentSourceCS,
       f.CommunityGrantsCS,
       f.AcademiesCS,
       f.IncomeFacilitiesServicesCS,
       f.IncomeCateringServicesCS,
       f.DonationsVoluntaryFundsCS,
       f.ReceiptsSupplyTeacherInsuranceClaimsCS,
       f.InvestmentIncomeCS,
       f.OtherSelfGeneratedIncomeCS
FROM (SELECT *
      FROM School,
           AARHistoryYears) s
         LEFT OUTER JOIN Financial f ON s.Year = f.RunId AND f.URN = s.URN AND f.RunType = 'default'
WHERE s.FinanceType = 'Academy'
UNION
SELECT s.URN,
       s.Year,
       s.TrustCompanyNumber,
       f.TotalPupils,
       f.TotalIncome,
       f.TotalExpenditure,
       f.TotalGrantFunding,
       f.TotalSelfGeneratedFunding,
       f.DirectRevenueFinancing,
       f.DirectGrants,
       f.PrePost16Funding,
       f.OtherDfeGrants,
       f.OtherIncomeGrants,
       f.GovernmentSource,
       f.CommunityGrants,
       f.Academies,
       f.IncomeFacilitiesServices,
       f.IncomeCateringServices,
       f.DonationsVoluntaryFunds,
       f.ReceiptsSupplyTeacherInsuranceClaims,
       f.InvestmentIncome,
       f.OtherSelfGeneratedIncome,
       f.TotalIncomeCS,
       f.TotalExpenditureCS,
       f.TotalGrantFundingCS,
       f.TotalSelfGeneratedFundingCS,
       f.DirectRevenueFinancingCS,
       f.DirectGrantsCS,
       f.PrePost16FundingCS,
       f.OtherDfeGrantsCS,
       f.OtherIncomeGrantsCS,
       f.GovernmentSourceCS,
       f.CommunityGrantsCS,
       f.AcademiesCS,
       f.IncomeFacilitiesServicesCS,
       f.IncomeCateringServicesCS,
       f.DonationsVoluntaryFundsCS,
       f.ReceiptsSupplyTeacherInsuranceClaimsCS,
       f.InvestmentIncomeCS,
       f.OtherSelfGeneratedIncomeCS
FROM (SELECT *
      FROM School,
           CFRHistoryYears) s
         LEFT OUTER JOIN Financial f ON s.Year = f.RunId AND f.URN = s.URN AND f.RunType = 'default'
WHERE s.FinanceType = 'Maintained'

GO