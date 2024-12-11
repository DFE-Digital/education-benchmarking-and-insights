DROP VIEW IF EXISTS SchoolExpenditurePercentageOfExpenditureHistoric
GO

CREATE VIEW SchoolExpenditurePercentageOfExpenditureHistoric AS
  SELECT Year
       , URN
       , FinanceType
       , OverallPhase
       , (TotalExpenditure / TotalExpenditure) * 100                          AS TotalExpenditure
       , (TotalIncome / TotalExpenditure) * 100                               AS TotalIncome
       , (TotalTeachingSupportStaffCosts / TotalExpenditure) * 100            AS TotalTeachingSupportStaffCosts
       , (TeachingStaffCosts / TotalExpenditure) * 100                        AS TeachingStaffCosts
       , (SupplyTeachingStaffCosts / TotalExpenditure) * 100                  AS SupplyTeachingStaffCosts
       , (EducationalConsultancyCosts / TotalExpenditure) * 100               AS EducationalConsultancyCosts
       , (EducationSupportStaffCosts / TotalExpenditure) * 100                AS EducationSupportStaffCosts
       , (AgencySupplyTeachingStaffCosts / TotalExpenditure) * 100            AS AgencySupplyTeachingStaffCosts
       , (TotalNonEducationalSupportStaffCosts / TotalExpenditure) * 100      AS TotalNonEducationalSupportStaffCosts
       , (AdministrativeClericalStaffCosts / TotalExpenditure) * 100          AS AdministrativeClericalStaffCosts
       , (AuditorsCosts / TotalExpenditure) * 100                             AS AuditorsCosts
       , (OtherStaffCosts / TotalExpenditure) * 100                           AS OtherStaffCosts
       , (ProfessionalServicesNonCurriculumCosts / TotalExpenditure) * 100    AS ProfessionalServicesNonCurriculumCosts
       , (TotalEducationalSuppliesCosts / TotalExpenditure) * 100             AS TotalEducationalSuppliesCosts
       , (ExaminationFeesCosts / TotalExpenditure) * 100                      AS ExaminationFeesCosts
       , (LearningResourcesNonIctCosts / TotalExpenditure) * 100              AS LearningResourcesNonIctCosts
       , (LearningResourcesIctCosts / TotalExpenditure) * 100                 AS LearningResourcesIctCosts
       , (TotalGrossCateringCosts / TotalExpenditure) * 100                   AS TotalGrossCateringCosts
       , (TotalNetCateringCosts / TotalExpenditure) * 100                     AS TotalNetCateringCosts
       , (CateringStaffCosts / TotalExpenditure) * 100                        AS CateringStaffCosts
       , (CateringSuppliesCosts / TotalExpenditure) * 100                     AS CateringSuppliesCosts
       , (TotalOtherCosts / TotalExpenditure) * 100                           AS TotalOtherCosts
       , (DirectRevenueFinancingCosts / TotalExpenditure) * 100               AS DirectRevenueFinancingCosts
       , (GroundsMaintenanceCosts / TotalExpenditure) * 100                   AS GroundsMaintenanceCosts
       , (IndirectEmployeeExpenses / TotalExpenditure) * 100                  AS IndirectEmployeeExpenses
       , (InterestChargesLoanBank / TotalExpenditure) * 100                   AS InterestChargesLoanBank
       , (OtherInsurancePremiumsCosts / TotalExpenditure) * 100               AS OtherInsurancePremiumsCosts
       , (PrivateFinanceInitiativeCharges / TotalExpenditure) * 100           AS PrivateFinanceInitiativeCharges
       , (RentRatesCosts / TotalExpenditure) * 100                            AS RentRatesCosts
       , (SpecialFacilitiesCosts / TotalExpenditure) * 100                    AS SpecialFacilitiesCosts
       , (StaffDevelopmentTrainingCosts / TotalExpenditure) * 100             AS StaffDevelopmentTrainingCosts
       , (StaffRelatedInsuranceCosts / TotalExpenditure) * 100                AS StaffRelatedInsuranceCosts
       , (SupplyTeacherInsurableCosts / TotalExpenditure) * 100               AS SupplyTeacherInsurableCosts
       , (CommunityFocusedSchoolStaff / TotalExpenditure) * 100               AS CommunityFocusedSchoolStaff
       , (CommunityFocusedSchoolCosts / TotalExpenditure) * 100               AS CommunityFocusedSchoolCosts
       , (AdministrativeSuppliesNonEducationalCosts / TotalExpenditure) * 100 AS AdministrativeSuppliesNonEducationalCosts
       , (TotalPremisesStaffServiceCosts / TotalExpenditure) * 100            AS TotalPremisesStaffServiceCosts
       , (CleaningCaretakingCosts / TotalExpenditure) * 100                   AS CleaningCaretakingCosts
       , (MaintenancePremisesCosts / TotalExpenditure) * 100                  AS MaintenancePremisesCosts
       , (OtherOccupationCosts / TotalExpenditure) * 100                      AS OtherOccupationCosts
       , (PremisesStaffCosts / TotalExpenditure) * 100                        AS PremisesStaffCosts
       , (TotalUtilitiesCosts / TotalExpenditure) * 100                       AS TotalUtilitiesCosts
       , (EnergyCosts / TotalExpenditure) * 100                               AS EnergyCosts
       , (WaterSewerageCosts / TotalExpenditure) * 100                        AS WaterSewerageCosts
    FROM SchoolExpenditureHistoricWithNulls
GO
