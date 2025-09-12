DROP VIEW IF EXISTS SchoolExpenditureAvgHistoric
GO

CREATE VIEW SchoolExpenditureAvgHistoric AS
  SELECT Year
       , FinanceType
       , OverallPhase
       , Avg(TotalPupils)                               AS TotalPupils
       , Avg(TotalInternalFloorArea)                    AS TotalInternalFloorArea
       , Avg(TotalExpenditure)                          AS TotalExpenditure
       , Avg(TotalIncome)                               AS TotalIncome
       , Avg(TotalTeachingSupportStaffCosts)            AS TotalTeachingSupportStaffCosts
       , Avg(TeachingStaffCosts)                        AS TeachingStaffCosts
       , Avg(SupplyTeachingStaffCosts)                  AS SupplyTeachingStaffCosts
       , Avg(EducationalConsultancyCosts)               AS EducationalConsultancyCosts
       , Avg(EducationSupportStaffCosts)                AS EducationSupportStaffCosts
       , Avg(AgencySupplyTeachingStaffCosts)            AS AgencySupplyTeachingStaffCosts
       , Avg(TotalNonEducationalSupportStaffCosts)      AS TotalNonEducationalSupportStaffCosts
       , Avg(AdministrativeClericalStaffCosts)          AS AdministrativeClericalStaffCosts
       , Avg(AuditorsCosts)                             AS AuditorsCosts
       , Avg(OtherStaffCosts)                           AS OtherStaffCosts
       , Avg(ProfessionalServicesNonCurriculumCosts)    AS ProfessionalServicesNonCurriculumCosts
       , Avg(TotalEducationalSuppliesCosts)             AS TotalEducationalSuppliesCosts
       , Avg(ExaminationFeesCosts)                      AS ExaminationFeesCosts
       , Avg(LearningResourcesNonIctCosts)              AS LearningResourcesNonIctCosts
       , Avg(LearningResourcesIctCosts)                 AS LearningResourcesIctCosts
       , Avg(TotalGrossCateringCosts)                   AS TotalGrossCateringCosts
       , Avg(TotalNetCateringCosts)                     AS TotalNetCateringCosts
       , Avg(CateringStaffCosts)                        AS CateringStaffCosts
       , Avg(CateringSuppliesCosts)                     AS CateringSuppliesCosts
       , Avg(TotalOtherCosts)                           AS TotalOtherCosts
       , Avg(DirectRevenueFinancingCosts)               AS DirectRevenueFinancingCosts
       , Avg(GroundsMaintenanceCosts)                   AS GroundsMaintenanceCosts
       , Avg(IndirectEmployeeExpenses)                  AS IndirectEmployeeExpenses
       , Avg(InterestChargesLoanBank)                   AS InterestChargesLoanBank
       , Avg(OtherInsurancePremiumsCosts)               AS OtherInsurancePremiumsCosts
       , Avg(PrivateFinanceInitiativeCharges)           AS PrivateFinanceInitiativeCharges
       , Avg(RentRatesCosts)                            AS RentRatesCosts
       , Avg(SpecialFacilitiesCosts)                    AS SpecialFacilitiesCosts
       , Avg(StaffDevelopmentTrainingCosts)             AS StaffDevelopmentTrainingCosts
       , Avg(StaffRelatedInsuranceCosts)                AS StaffRelatedInsuranceCosts
       , Avg(SupplyTeacherInsurableCosts)               AS SupplyTeacherInsurableCosts
       , Avg(CommunityFocusedSchoolStaff)               AS CommunityFocusedSchoolStaff
       , Avg(CommunityFocusedSchoolCosts)               AS CommunityFocusedSchoolCosts
       , Avg(AdministrativeSuppliesNonEducationalCosts) AS AdministrativeSuppliesNonEducationalCosts
       , Avg(TotalPremisesStaffServiceCosts)            AS TotalPremisesStaffServiceCosts
       , Avg(CleaningCaretakingCosts)                   AS CleaningCaretakingCosts
       , Avg(MaintenancePremisesCosts)                  AS MaintenancePremisesCosts
       , Avg(OtherOccupationCosts)                      AS OtherOccupationCosts
       , Avg(PremisesStaffCosts)                        AS PremisesStaffCosts
       , Avg(TotalUtilitiesCosts)                       AS TotalUtilitiesCosts
       , Avg(EnergyCosts)                               AS EnergyCosts
       , Avg(WaterSewerageCosts)                        AS WaterSewerageCosts
    FROM SchoolExpenditureHistoricWithNulls
   GROUP
      BY Year
       , FinanceType
       , OverallPhase
GO
