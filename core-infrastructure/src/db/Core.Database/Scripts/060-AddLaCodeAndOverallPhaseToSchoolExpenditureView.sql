ALTER VIEW [dbo].[SchoolExpenditure] AS
SELECT
   s.URN,
   s.SchoolName,
   s.SchoolType,
   s.OverallPhase,
   s.LACode,
   s.LAName,
   s.TrustCompanyNumber,
   f.PeriodCoveredByReturn,
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
   f.TotalNetCateringCostsCosts AS TotalNetCateringCosts,
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
   f.TotalNetCateringCostsCostsCS AS TotalNetCateringCostsCS,
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
FROM
   School s
   LEFT JOIN CurrentDefaultFinancial f ON f.URN = s.URN