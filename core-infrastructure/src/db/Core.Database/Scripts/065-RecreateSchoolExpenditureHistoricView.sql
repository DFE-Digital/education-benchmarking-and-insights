DROP VIEW IF EXISTS SchoolExpenditureHistoric
GO

CREATE VIEW SchoolExpenditureHistoric AS
  SELECT s.URN
       , s.Year
       , s.FinanceType
       , s.OverallPhase
       , s.TrustCompanyNumber
       , f.TotalPupils
       , f.TotalInternalFloorArea
       , f.TotalIncome
       , f.TotalExpenditure
       , f.TotalTeachingSupportStaffCosts
       , f.TeachingStaffCosts
       , f.SupplyTeachingStaffCosts
       , f.EducationalConsultancyCosts
       , f.EducationSupportStaffCosts
       , f.AgencySupplyTeachingStaffCosts
       , f.TotalNonEducationalSupportStaffCosts
       , f.AdministrativeClericalStaffCosts
       , f.AuditorsCosts
       , f.OtherStaffCosts
       , f.ProfessionalServicesNonCurriculumCosts
       , f.TotalEducationalSuppliesCosts
       , f.ExaminationFeesCosts
       , f.LearningResourcesNonIctCosts
       , f.LearningResourcesIctCosts
       , f.TotalPremisesStaffServiceCosts
       , f.CleaningCaretakingCosts
       , f.MaintenancePremisesCosts
       , f.OtherOccupationCosts
       , f.PremisesStaffCosts
       , f.TotalUtilitiesCosts
       , f.EnergyCosts
       , f.WaterSewerageCosts
       , f.AdministrativeSuppliesNonEducationalCosts
       , f.TotalGrossCateringCosts
       , f.TotalNetCateringCostsCosts AS TotalNetCateringCosts
       , f.CateringStaffCosts
       , f.CateringSuppliesCosts
       , f.TotalOtherCosts
       , f.GroundsMaintenanceCosts
       , f.IndirectEmployeeExpenses
       , f.InterestChargesLoanBank
       , f.OtherInsurancePremiumsCosts
       , f.PrivateFinanceInitiativeCharges
       , f.RentRatesCosts
       , f.SpecialFacilitiesCosts
       , f.StaffDevelopmentTrainingCosts
       , f.StaffRelatedInsuranceCosts
       , f.SupplyTeacherInsurableCosts
       , f.CommunityFocusedSchoolStaff
       , f.CommunityFocusedSchoolCosts
       , f.TotalIncomeCS
       , f.TotalExpenditureCS
       , f.TotalTeachingSupportStaffCostsCS
       , f.TeachingStaffCostsCS
       , f.SupplyTeachingStaffCostsCS
       , f.EducationalConsultancyCostsCS
       , f.EducationSupportStaffCostsCS
       , f.AgencySupplyTeachingStaffCostsCS
       , f.TotalNonEducationalSupportStaffCostsCS
       , f.AdministrativeClericalStaffCostsCS
       , f.AuditorsCostsCS
       , f.OtherStaffCostsCS
       , f.ProfessionalServicesNonCurriculumCostsCS
       , f.TotalEducationalSuppliesCostsCS
       , f.ExaminationFeesCostsCS
       , f.LearningResourcesNonIctCostsCS
       , f.LearningResourcesIctCostsCS
       , f.TotalPremisesStaffServiceCostsCS
       , f.CleaningCaretakingCostsCS
       , f.MaintenancePremisesCostsCS
       , f.OtherOccupationCostsCS
       , f.PremisesStaffCostsCS
       , f.TotalUtilitiesCostsCS
       , f.EnergyCostsCS
       , f.WaterSewerageCostsCS
       , f.AdministrativeSuppliesNonEducationalCostsCS
       , f.TotalGrossCateringCostsCS
       , f.TotalNetCateringCostsCostsCS AS TotalNetCateringCostsCS
       , f.CateringStaffCostsCS
       , f.CateringSuppliesCostsCS
       , f.TotalOtherCostsCS
       , f.GroundsMaintenanceCostsCS
       , f.IndirectEmployeeExpensesCS
       , f.InterestChargesLoanBankCS
       , f.OtherInsurancePremiumsCostsCS
       , f.PrivateFinanceInitiativeChargesCS
       , f.RentRatesCostsCS
       , f.SpecialFacilitiesCostsCS
       , f.StaffDevelopmentTrainingCostsCS
       , f.StaffRelatedInsuranceCostsCS
       , f.SupplyTeacherInsurableCostsCS
    FROM (
           SELECT *
             FROM School
                , AARHistoryYears
         ) s
    LEFT
    OUTER
    JOIN Financial f ON s.Year = f.RunId
     AND f.URN = s.URN
     AND f.RunType = 'default'
   WHERE s.FinanceType = 'Academy'

  UNION

  SELECT s.URN
       , s.Year
       , s.FinanceType
       , s.OverallPhase
       , s.TrustCompanyNumber
       , f.TotalPupils
       , f.TotalInternalFloorArea
       , f.TotalIncome
       , f.TotalExpenditure
       , f.TotalTeachingSupportStaffCosts
       , f.TeachingStaffCosts
       , f.SupplyTeachingStaffCosts
       , f.EducationalConsultancyCosts
       , f.EducationSupportStaffCosts
       , f.AgencySupplyTeachingStaffCosts
       , f.TotalNonEducationalSupportStaffCosts
       , f.AdministrativeClericalStaffCosts
       , f.AuditorsCosts
       , f.OtherStaffCosts
       , f.ProfessionalServicesNonCurriculumCosts
       , f.TotalEducationalSuppliesCosts
       , f.ExaminationFeesCosts
       , f.LearningResourcesNonIctCosts
       , f.LearningResourcesIctCosts
       , f.TotalPremisesStaffServiceCosts
       , f.CleaningCaretakingCosts
       , f.MaintenancePremisesCosts
       , f.OtherOccupationCosts
       , f.PremisesStaffCosts
       , f.TotalUtilitiesCosts
       , f.EnergyCosts
       , f.WaterSewerageCosts
       , f.AdministrativeSuppliesNonEducationalCosts
       , f.TotalGrossCateringCosts
       , f.TotalNetCateringCostsCosts AS TotalNetCateringCosts
       , f.CateringStaffCosts
       , f.CateringSuppliesCosts
       , f.TotalOtherCosts
       , f.GroundsMaintenanceCosts
       , f.IndirectEmployeeExpenses
       , f.InterestChargesLoanBank
       , f.OtherInsurancePremiumsCosts
       , f.PrivateFinanceInitiativeCharges
       , f.RentRatesCosts
       , f.SpecialFacilitiesCosts
       , f.StaffDevelopmentTrainingCosts
       , f.StaffRelatedInsuranceCosts
       , f.SupplyTeacherInsurableCosts
       , f.CommunityFocusedSchoolStaff
       , f.CommunityFocusedSchoolCosts
       , f.TotalIncomeCS
       , f.TotalExpenditureCS
       , f.TotalTeachingSupportStaffCostsCS
       , f.TeachingStaffCostsCS
       , f.SupplyTeachingStaffCostsCS
       , f.EducationalConsultancyCostsCS
       , f.EducationSupportStaffCostsCS
       , f.AgencySupplyTeachingStaffCostsCS
       , f.TotalNonEducationalSupportStaffCostsCS
       , f.AdministrativeClericalStaffCostsCS
       , f.AuditorsCostsCS
       , f.OtherStaffCostsCS
       , f.ProfessionalServicesNonCurriculumCostsCS
       , f.TotalEducationalSuppliesCostsCS
       , f.ExaminationFeesCostsCS
       , f.LearningResourcesNonIctCostsCS
       , f.LearningResourcesIctCostsCS
       , f.TotalPremisesStaffServiceCostsCS
       , f.CleaningCaretakingCostsCS
       , f.MaintenancePremisesCostsCS
       , f.OtherOccupationCostsCS
       , f.PremisesStaffCostsCS
       , f.TotalUtilitiesCostsCS
       , f.EnergyCostsCS
       , f.WaterSewerageCostsCS
       , f.AdministrativeSuppliesNonEducationalCostsCS
       , f.TotalGrossCateringCostsCS
       , f.TotalNetCateringCostsCostsCS AS TotalNetCateringCostsCS
       , f.CateringStaffCostsCS
       , f.CateringSuppliesCostsCS
       , f.TotalOtherCostsCS
       , f.GroundsMaintenanceCostsCS
       , f.IndirectEmployeeExpensesCS
       , f.InterestChargesLoanBankCS
       , f.OtherInsurancePremiumsCostsCS
       , f.PrivateFinanceInitiativeChargesCS
       , f.RentRatesCostsCS
       , f.SpecialFacilitiesCostsCS
       , f.StaffDevelopmentTrainingCostsCS
       , f.StaffRelatedInsuranceCostsCS
       , f.SupplyTeacherInsurableCostsCS
    FROM (
           SELECT *
             FROM School
                , CFRHistoryYears
         ) s
    LEFT
   OUTER
    JOIN Financial f ON s.Year = f.RunId
     AND f.URN = s.URN
     AND f.RunType = 'default'
   WHERE s.FinanceType = 'Maintained'
GO
