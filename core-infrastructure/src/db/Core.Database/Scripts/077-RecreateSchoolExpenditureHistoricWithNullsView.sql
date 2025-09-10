DROP VIEW IF EXISTS SchoolExpenditureHistoricWithNulls
GO

CREATE VIEW SchoolExpenditureHistoricWithNulls AS
  SELECT URN
       , Year
       , FinanceType
       , OverallPhase
       , CASE
             WHEN TotalPupils IS NULL OR TotalPupils <= 0.0 THEN NULL
             ELSE TotalPupils
         END AS TotalPupils
       , CASE
             WHEN TotalInternalFloorArea IS NULL OR TotalInternalFloorArea <= 0.0 THEN NULL
             ELSE TotalInternalFloorArea
         END AS TotalInternalFloorArea
       , CASE
             WHEN TotalExpenditure IS NULL OR TotalExpenditure <= 0.0 THEN NULL
             ELSE TotalExpenditure
         END AS TotalExpenditure
       , CASE
             WHEN TotalIncome IS NULL OR TotalIncome <= 0.0 THEN NULL
             ELSE TotalIncome
         END AS TotalIncome
       , CASE
             WHEN TotalTeachingSupportStaffCosts IS NULL OR TotalTeachingSupportStaffCosts <= 0.0 THEN NULL
             ELSE TotalTeachingSupportStaffCosts
         END AS TotalTeachingSupportStaffCosts
       , CASE
             WHEN TeachingStaffCosts IS NULL OR TeachingStaffCosts <= 0.0 THEN NULL
             ELSE TeachingStaffCosts
         END AS TeachingStaffCosts
       , CASE
             WHEN SupplyTeachingStaffCosts IS NULL OR SupplyTeachingStaffCosts <= 0.0 THEN NULL
             ELSE SupplyTeachingStaffCosts
         END AS SupplyTeachingStaffCosts
       , CASE
             WHEN EducationalConsultancyCosts IS NULL OR EducationalConsultancyCosts <= 0.0 THEN NULL
             ELSE EducationalConsultancyCosts
         END AS EducationalConsultancyCosts
       , CASE
             WHEN EducationSupportStaffCosts IS NULL OR EducationSupportStaffCosts <= 0.0 THEN NULL
             ELSE EducationSupportStaffCosts
         END AS EducationSupportStaffCosts
       , CASE
             WHEN AgencySupplyTeachingStaffCosts IS NULL OR AgencySupplyTeachingStaffCosts <= 0.0 THEN NULL
             ELSE AgencySupplyTeachingStaffCosts
         END AS AgencySupplyTeachingStaffCosts
       , CASE
             WHEN TotalNonEducationalSupportStaffCosts IS NULL OR TotalNonEducationalSupportStaffCosts <= 0.0 THEN NULL
             ELSE TotalNonEducationalSupportStaffCosts
         END AS TotalNonEducationalSupportStaffCosts
       , CASE
             WHEN AdministrativeClericalStaffCosts IS NULL OR AdministrativeClericalStaffCosts <= 0.0 THEN NULL
             ELSE AdministrativeClericalStaffCosts
         END AS AdministrativeClericalStaffCosts
       , CASE
             WHEN AuditorsCosts IS NULL OR AuditorsCosts <= 0.0 THEN NULL
             ELSE AuditorsCosts
         END AS AuditorsCosts
       , CASE
             WHEN OtherStaffCosts IS NULL OR OtherStaffCosts <= 0.0 THEN NULL
             ELSE OtherStaffCosts
         END AS OtherStaffCosts
       , CASE
             WHEN ProfessionalServicesNonCurriculumCosts IS NULL OR ProfessionalServicesNonCurriculumCosts <= 0.0 THEN NULL
             ELSE ProfessionalServicesNonCurriculumCosts
         END AS ProfessionalServicesNonCurriculumCosts
       , CASE
             WHEN TotalEducationalSuppliesCosts IS NULL OR TotalEducationalSuppliesCosts <= 0.0 THEN NULL
             ELSE TotalEducationalSuppliesCosts
         END AS TotalEducationalSuppliesCosts
       , CASE
             WHEN ExaminationFeesCosts IS NULL OR ExaminationFeesCosts <= 0.0 THEN NULL
             ELSE ExaminationFeesCosts
         END AS ExaminationFeesCosts
       , CASE
             WHEN LearningResourcesNonIctCosts IS NULL OR LearningResourcesNonIctCosts <= 0.0 THEN NULL
             ELSE LearningResourcesNonIctCosts
         END AS LearningResourcesNonIctCosts
       , CASE
             WHEN LearningResourcesIctCosts IS NULL OR LearningResourcesIctCosts <= 0.0 THEN NULL
             ELSE LearningResourcesIctCosts
         END AS LearningResourcesIctCosts
       , CASE
             WHEN TotalGrossCateringCosts IS NULL OR TotalGrossCateringCosts <= 0.0 THEN NULL
             ELSE TotalGrossCateringCosts
         END AS TotalGrossCateringCosts
       , CASE
             WHEN TotalNetCateringCosts IS NULL OR TotalNetCateringCosts <= 0.0 THEN NULL
             ELSE TotalNetCateringCosts
         END AS TotalNetCateringCosts
       , CASE
             WHEN CateringStaffCosts IS NULL OR CateringStaffCosts <= 0.0 THEN NULL
             ELSE CateringStaffCosts
         END AS CateringStaffCosts
       , CASE
             WHEN CateringSuppliesCosts IS NULL OR CateringSuppliesCosts <= 0.0 THEN NULL
             ELSE CateringSuppliesCosts
         END AS CateringSuppliesCosts
       , CASE
             WHEN TotalOtherCosts IS NULL OR TotalOtherCosts <= 0.0 THEN NULL
             ELSE TotalOtherCosts
         END AS TotalOtherCosts
       , CASE
             WHEN GroundsMaintenanceCosts IS NULL OR GroundsMaintenanceCosts <= 0.0 THEN NULL
             ELSE GroundsMaintenanceCosts
         END AS GroundsMaintenanceCosts
       , CASE
             WHEN IndirectEmployeeExpenses IS NULL OR IndirectEmployeeExpenses <= 0.0 THEN NULL
             ELSE IndirectEmployeeExpenses
         END AS IndirectEmployeeExpenses
       , CASE
             WHEN InterestChargesLoanBank IS NULL OR InterestChargesLoanBank <= 0.0 THEN NULL
             ELSE InterestChargesLoanBank
         END AS InterestChargesLoanBank
       , CASE
             WHEN OtherInsurancePremiumsCosts IS NULL OR OtherInsurancePremiumsCosts <= 0.0 THEN NULL
             ELSE OtherInsurancePremiumsCosts
         END AS OtherInsurancePremiumsCosts
       , CASE
             WHEN PrivateFinanceInitiativeCharges IS NULL OR PrivateFinanceInitiativeCharges <= 0.0 THEN NULL
             ELSE PrivateFinanceInitiativeCharges
         END AS PrivateFinanceInitiativeCharges
       , CASE
             WHEN RentRatesCosts IS NULL OR RentRatesCosts <= 0.0 THEN NULL
             ELSE RentRatesCosts
         END AS RentRatesCosts
       , CASE
             WHEN SpecialFacilitiesCosts IS NULL OR SpecialFacilitiesCosts <= 0.0 THEN NULL
             ELSE SpecialFacilitiesCosts
         END AS SpecialFacilitiesCosts
       , CASE
             WHEN StaffDevelopmentTrainingCosts IS NULL OR StaffDevelopmentTrainingCosts <= 0.0 THEN NULL
             ELSE StaffDevelopmentTrainingCosts
         END AS StaffDevelopmentTrainingCosts
       , CASE
             WHEN StaffRelatedInsuranceCosts IS NULL OR StaffRelatedInsuranceCosts <= 0.0 THEN NULL
             ELSE StaffRelatedInsuranceCosts
         END AS StaffRelatedInsuranceCosts
       , CASE
             WHEN SupplyTeacherInsurableCosts IS NULL OR SupplyTeacherInsurableCosts <= 0.0 THEN NULL
             ELSE SupplyTeacherInsurableCosts
         END AS SupplyTeacherInsurableCosts
       , CASE
             WHEN CommunityFocusedSchoolStaff IS NULL OR CommunityFocusedSchoolStaff <= 0.0 THEN NULL
             ELSE CommunityFocusedSchoolStaff
         END AS CommunityFocusedSchoolStaff
       , CASE
             WHEN CommunityFocusedSchoolCosts IS NULL OR CommunityFocusedSchoolCosts <= 0.0 THEN NULL
             ELSE CommunityFocusedSchoolCosts
         END AS CommunityFocusedSchoolCosts
       , CASE
             WHEN AdministrativeSuppliesNonEducationalCosts IS NULL OR AdministrativeSuppliesNonEducationalCosts <= 0.0 THEN NULL
             ELSE AdministrativeSuppliesNonEducationalCosts
         END AS AdministrativeSuppliesNonEducationalCosts
       , CASE
             WHEN TotalPremisesStaffServiceCosts IS NULL OR TotalPremisesStaffServiceCosts <= 0.0 THEN NULL
             ELSE TotalPremisesStaffServiceCosts
         END AS TotalPremisesStaffServiceCosts
       , CASE
             WHEN CleaningCaretakingCosts IS NULL OR CleaningCaretakingCosts <= 0.0 THEN NULL
             ELSE CleaningCaretakingCosts
         END AS CleaningCaretakingCosts
       , CASE
             WHEN MaintenancePremisesCosts IS NULL OR MaintenancePremisesCosts <= 0.0 THEN NULL
             ELSE MaintenancePremisesCosts
         END AS MaintenancePremisesCosts
       , CASE
             WHEN OtherOccupationCosts IS NULL OR OtherOccupationCosts <= 0.0 THEN NULL
             ELSE OtherOccupationCosts
         END AS OtherOccupationCosts
       , CASE
             WHEN PremisesStaffCosts IS NULL OR PremisesStaffCosts <= 0.0 THEN NULL
             ELSE PremisesStaffCosts
         END AS PremisesStaffCosts
       , CASE
             WHEN TotalUtilitiesCosts IS NULL OR TotalUtilitiesCosts <= 0.0 THEN NULL
             ELSE TotalUtilitiesCosts
         END AS TotalUtilitiesCosts
       , CASE
             WHEN EnergyCosts IS NULL OR EnergyCosts <= 0.0 THEN NULL
             ELSE EnergyCosts
         END AS EnergyCosts
       , CASE
             WHEN WaterSewerageCosts IS NULL OR WaterSewerageCosts <= 0.0 THEN NULL
             ELSE WaterSewerageCosts
         END AS WaterSewerageCosts
    FROM SchoolExpenditureHistoric
GO
