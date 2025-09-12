DROP VIEW IF EXISTS SchoolExpenditurePercentageOfIncomeHistoric
GO

CREATE VIEW SchoolExpenditurePercentageOfIncomeHistoric AS
  SELECT Year
       , URN
       , FinanceType
       , OverallPhase
       , (TotalExpenditure / TotalIncome) * 100                          AS TotalExpenditure
       , (TotalIncome / TotalIncome) * 100                               AS TotalIncome
       , (TotalTeachingSupportStaffCosts / TotalIncome) * 100            AS TotalTeachingSupportStaffCosts
       , (TeachingStaffCosts / TotalIncome) * 100                        AS TeachingStaffCosts
       , (SupplyTeachingStaffCosts / TotalIncome) * 100                  AS SupplyTeachingStaffCosts
       , (EducationalConsultancyCosts / TotalIncome) * 100               AS EducationalConsultancyCosts
       , (EducationSupportStaffCosts / TotalIncome) * 100                AS EducationSupportStaffCosts
       , (AgencySupplyTeachingStaffCosts / TotalIncome) * 100            AS AgencySupplyTeachingStaffCosts
       , (TotalNonEducationalSupportStaffCosts / TotalIncome) * 100      AS TotalNonEducationalSupportStaffCosts
       , (AdministrativeClericalStaffCosts / TotalIncome) * 100          AS AdministrativeClericalStaffCosts
       , (AuditorsCosts / TotalIncome) * 100                             AS AuditorsCosts
       , (OtherStaffCosts / TotalIncome) * 100                           AS OtherStaffCosts
       , (ProfessionalServicesNonCurriculumCosts / TotalIncome) * 100    AS ProfessionalServicesNonCurriculumCosts
       , (TotalEducationalSuppliesCosts / TotalIncome) * 100             AS TotalEducationalSuppliesCosts
       , (ExaminationFeesCosts / TotalIncome) * 100                      AS ExaminationFeesCosts
       , (LearningResourcesNonIctCosts / TotalIncome) * 100              AS LearningResourcesNonIctCosts
       , (LearningResourcesIctCosts / TotalIncome) * 100                 AS LearningResourcesIctCosts
       , (TotalGrossCateringCosts / TotalIncome) * 100                   AS TotalGrossCateringCosts
       , (TotalNetCateringCosts / TotalIncome) * 100                     AS TotalNetCateringCosts
       , (CateringStaffCosts / TotalIncome) * 100                        AS CateringStaffCosts
       , (CateringSuppliesCosts / TotalIncome) * 100                     AS CateringSuppliesCosts
       , (TotalOtherCosts / TotalIncome) * 100                           AS TotalOtherCosts
       , (DirectRevenueFinancingCosts / TotalIncome) * 100               AS DirectRevenueFinancingCosts
       , (GroundsMaintenanceCosts / TotalIncome) * 100                   AS GroundsMaintenanceCosts
       , (IndirectEmployeeExpenses / TotalIncome) * 100                  AS IndirectEmployeeExpenses
       , (InterestChargesLoanBank / TotalIncome) * 100                   AS InterestChargesLoanBank
       , (OtherInsurancePremiumsCosts / TotalIncome) * 100               AS OtherInsurancePremiumsCosts
       , (PrivateFinanceInitiativeCharges / TotalIncome) * 100           AS PrivateFinanceInitiativeCharges
       , (RentRatesCosts / TotalIncome) * 100                            AS RentRatesCosts
       , (SpecialFacilitiesCosts / TotalIncome) * 100                    AS SpecialFacilitiesCosts
       , (StaffDevelopmentTrainingCosts / TotalIncome) * 100             AS StaffDevelopmentTrainingCosts
       , (StaffRelatedInsuranceCosts / TotalIncome) * 100                AS StaffRelatedInsuranceCosts
       , (SupplyTeacherInsurableCosts / TotalIncome) * 100               AS SupplyTeacherInsurableCosts
       , (CommunityFocusedSchoolStaff / TotalIncome) * 100               AS CommunityFocusedSchoolStaff
       , (CommunityFocusedSchoolCosts / TotalIncome) * 100               AS CommunityFocusedSchoolCosts
       , (AdministrativeSuppliesNonEducationalCosts / TotalIncome) * 100 AS AdministrativeSuppliesNonEducationalCosts
       , (TotalPremisesStaffServiceCosts / TotalIncome) * 100            AS TotalPremisesStaffServiceCosts
       , (CleaningCaretakingCosts / TotalIncome) * 100                   AS CleaningCaretakingCosts
       , (MaintenancePremisesCosts / TotalIncome) * 100                  AS MaintenancePremisesCosts
       , (OtherOccupationCosts / TotalIncome) * 100                      AS OtherOccupationCosts
       , (PremisesStaffCosts / TotalIncome) * 100                        AS PremisesStaffCosts
       , (TotalUtilitiesCosts / TotalIncome) * 100                       AS TotalUtilitiesCosts
       , (EnergyCosts / TotalIncome) * 100                               AS EnergyCosts
       , (WaterSewerageCosts / TotalIncome) * 100                        AS WaterSewerageCosts
    FROM SchoolExpenditureHistoricWithNulls
GO
