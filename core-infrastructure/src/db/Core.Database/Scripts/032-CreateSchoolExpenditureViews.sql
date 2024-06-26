IF EXISTS(SELECT 1
          FROM sys.views
          WHERE name = 'SchoolExpenditureHistoric')
    BEGIN
        DROP VIEW SchoolExpenditureHistoric
    END
GO

CREATE VIEW SchoolExpenditureHistoric
AS
SELECT s.URN,
       f.RunId 'Year',
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
       f.SupplyTeacherInsurableCostsCS,
       f.CommunityFocusedSchoolStaffCS,
       f.CommunityFocusedSchoolCostsCS
FROM School s,
     (SELECT Value FROM Parameters p WHERE p.Name = 'LatestAARYear') y,
     Financial f
WHERE s.URN = f.URN
  AND f.RunType = 'default'
  AND s.FinanceType = 'Academy'
  AND f.RunId BETWEEN y.Value - 5 AND y.Value
UNION
SELECT s.URN,
       f.RunId 'Year',
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
       f.SupplyTeacherInsurableCostsCS,
       f.CommunityFocusedSchoolStaffCS,
       f.CommunityFocusedSchoolCostsCS
FROM School s,
     (SELECT Value FROM Parameters p WHERE p.Name = 'LatestCFRYear') y,
     Financial f
WHERE s.URN = f.URN
  AND f.RunType = 'default'
  AND s.FinanceType = 'Maintained'
  AND f.RunId BETWEEN y.Value - 5 AND y.Value

GO


IF EXISTS(SELECT 1
          FROM sys.views
          WHERE name = 'SchoolExpenditure')
    BEGIN
        DROP VIEW SchoolExpenditure
    END
GO

CREATE VIEW SchoolExpenditure
AS
SELECT s.URN,
       s.SchoolName,
       s.SchoolType,
       s.LAName,
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
       f.SupplyTeacherInsurableCostsCS,
       f.CommunityFocusedSchoolStaffCS,
       f.CommunityFocusedSchoolCostsCS
FROM School s
         LEFT JOIN CurrentDefaultFinancial f on f.URN = s.URN

GO