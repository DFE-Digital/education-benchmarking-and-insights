DROP VIEW IF EXISTS SchoolExpenditurePerUnitHistoric
GO

CREATE VIEW SchoolExpenditurePerUnitHistoric AS
  SELECT URN
       , Year
       , FinanceType
       , OverallPhase
       , TotalExpenditure / TotalPupils                          AS TotalExpenditure
       , TotalIncome / TotalPupils                               AS TotalIncome
       , TotalTeachingSupportStaffCosts / TotalPupils            AS TotalTeachingSupportStaffCosts
       , TeachingStaffCosts / TotalPupils                        AS TeachingStaffCosts
       , SupplyTeachingStaffCosts / TotalPupils                  AS SupplyTeachingStaffCosts
       , EducationalConsultancyCosts / TotalPupils               AS EducationalConsultancyCosts
       , EducationSupportStaffCosts / TotalPupils                AS EducationSupportStaffCosts
       , AgencySupplyTeachingStaffCosts / TotalPupils            AS AgencySupplyTeachingStaffCosts
       , TotalNonEducationalSupportStaffCosts / TotalPupils      AS TotalNonEducationalSupportStaffCosts
       , AdministrativeClericalStaffCosts / TotalPupils          AS AdministrativeClericalStaffCosts
       , AuditorsCosts / TotalPupils                             AS AuditorsCosts
       , OtherStaffCosts / TotalPupils                           AS OtherStaffCosts
       , ProfessionalServicesNonCurriculumCosts / TotalPupils    AS ProfessionalServicesNonCurriculumCosts
       , TotalEducationalSuppliesCosts / TotalPupils             AS TotalEducationalSuppliesCosts
       , ExaminationFeesCosts / TotalPupils                      AS ExaminationFeesCosts
       , LearningResourcesNonIctCosts / TotalPupils              AS LearningResourcesNonIctCosts
       , LearningResourcesIctCosts / TotalPupils                 AS LearningResourcesIctCosts
       , TotalGrossCateringCosts / TotalPupils                   AS TotalGrossCateringCosts
       , TotalNetCateringCosts / TotalPupils                     AS TotalNetCateringCosts
       , CateringStaffCosts / TotalPupils                        AS CateringStaffCosts
       , CateringSuppliesCosts / TotalPupils                     AS CateringSuppliesCosts
       , TotalOtherCosts / TotalPupils                           AS TotalOtherCosts
       , DirectRevenueFinancingCosts / TotalPupils               AS DirectRevenueFinancingCosts
       , GroundsMaintenanceCosts / TotalPupils                   AS GroundsMaintenanceCosts
       , IndirectEmployeeExpenses / TotalPupils                  AS IndirectEmployeeExpenses
       , InterestChargesLoanBank / TotalPupils                   AS InterestChargesLoanBank
       , OtherInsurancePremiumsCosts / TotalPupils               AS OtherInsurancePremiumsCosts
       , PrivateFinanceInitiativeCharges / TotalPupils           AS PrivateFinanceInitiativeCharges
       , RentRatesCosts / TotalPupils                            AS RentRatesCosts
       , SpecialFacilitiesCosts / TotalPupils                    AS SpecialFacilitiesCosts
       , StaffDevelopmentTrainingCosts / TotalPupils             AS StaffDevelopmentTrainingCosts
       , StaffRelatedInsuranceCosts / TotalPupils                AS StaffRelatedInsuranceCosts
       , SupplyTeacherInsurableCosts / TotalPupils               AS SupplyTeacherInsurableCosts
       , CommunityFocusedSchoolStaff / TotalPupils               AS CommunityFocusedSchoolStaff
       , CommunityFocusedSchoolCosts / TotalPupils               AS CommunityFocusedSchoolCosts
       , AdministrativeSuppliesNonEducationalCosts / TotalPupils AS AdministrativeSuppliesNonEducationalCosts
       , TotalPremisesStaffServiceCosts / TotalInternalFloorArea AS TotalPremisesStaffServiceCosts
       , CleaningCaretakingCosts / TotalInternalFloorArea        AS CleaningCaretakingCosts
       , MaintenancePremisesCosts / TotalInternalFloorArea       AS MaintenancePremisesCosts
       , OtherOccupationCosts / TotalInternalFloorArea           AS OtherOccupationCosts
       , PremisesStaffCosts / TotalInternalFloorArea             AS PremisesStaffCosts
       , TotalUtilitiesCosts / TotalInternalFloorArea            AS TotalUtilitiesCosts
       , EnergyCosts / TotalInternalFloorArea                    AS EnergyCosts
       , WaterSewerageCosts / TotalInternalFloorArea             AS WaterSewerageCosts
    FROM SchoolExpenditureHistoricWithNulls
GO

DROP VIEW IF EXISTS SchoolExpenditureAvgPerUnitHistoric
GO

CREATE VIEW SchoolExpenditureAvgPerUnitHistoric AS
  SELECT Year
       , FinanceType
       , OverallPhase
       , Avg(TotalExpenditure)                           AS TotalExpenditure
       , Avg(TotalIncome)                                AS TotalIncome
       , Avg(TotalTeachingSupportStaffCosts)             AS TotalTeachingSupportStaffCosts
       , Avg(TeachingStaffCosts)                         AS TeachingStaffCosts
       , Avg(SupplyTeachingStaffCosts)                   AS SupplyTeachingStaffCosts
       , Avg(EducationalConsultancyCosts)                AS EducationalConsultancyCosts
       , Avg(EducationSupportStaffCosts)                 AS EducationSupportStaffCosts
       , Avg(AgencySupplyTeachingStaffCosts)             AS AgencySupplyTeachingStaffCosts
       , Avg(TotalNonEducationalSupportStaffCosts)       AS TotalNonEducationalSupportStaffCosts
       , Avg(AdministrativeClericalStaffCosts)           AS AdministrativeClericalStaffCosts
       , Avg(AuditorsCosts)                              AS AuditorsCosts
       , Avg(OtherStaffCosts)                            AS OtherStaffCosts
       , Avg(ProfessionalServicesNonCurriculumCosts)     AS ProfessionalServicesNonCurriculumCosts
       , Avg(TotalEducationalSuppliesCosts)              AS TotalEducationalSuppliesCosts
       , Avg(ExaminationFeesCosts)                       AS ExaminationFeesCosts
       , Avg(LearningResourcesNonIctCosts)               AS LearningResourcesNonIctCosts
       , Avg(LearningResourcesIctCosts)                  AS LearningResourcesIctCosts
       , Avg(TotalGrossCateringCosts)                    AS TotalGrossCateringCosts
       , Avg(TotalNetCateringCosts)                      AS TotalNetCateringCosts
       , Avg(CateringStaffCosts)                         AS CateringStaffCosts
       , Avg(CateringSuppliesCosts)                      AS CateringSuppliesCosts
       , Avg(TotalOtherCosts)                            AS TotalOtherCosts
       , Avg(DirectRevenueFinancingCosts)                AS DirectRevenueFinancingCosts
       , Avg(GroundsMaintenanceCosts)                    AS GroundsMaintenanceCosts
       , Avg(IndirectEmployeeExpenses)                   AS IndirectEmployeeExpenses
       , Avg(InterestChargesLoanBank)                    AS InterestChargesLoanBank
       , Avg(OtherInsurancePremiumsCosts)                AS OtherInsurancePremiumsCosts
       , Avg(PrivateFinanceInitiativeCharges)            AS PrivateFinanceInitiativeCharges
       , Avg(RentRatesCosts)                             AS RentRatesCosts
       , Avg(SpecialFacilitiesCosts)                     AS SpecialFacilitiesCosts
       , Avg(StaffDevelopmentTrainingCosts)              AS StaffDevelopmentTrainingCosts
       , Avg(StaffRelatedInsuranceCosts)                 AS StaffRelatedInsuranceCosts
       , Avg(SupplyTeacherInsurableCosts)                AS SupplyTeacherInsurableCosts
       , Avg(CommunityFocusedSchoolStaff)                AS CommunityFocusedSchoolStaff
       , Avg(CommunityFocusedSchoolCosts)                AS CommunityFocusedSchoolCosts
       , Avg(AdministrativeSuppliesNonEducationalCosts)  AS AdministrativeSuppliesNonEducationalCosts
       , Avg(TotalPremisesStaffServiceCosts)             AS TotalPremisesStaffServiceCosts
       , Avg(CleaningCaretakingCosts)                    AS CleaningCaretakingCosts
       , Avg(MaintenancePremisesCosts)                   AS MaintenancePremisesCosts
       , Avg(OtherOccupationCosts)                       AS OtherOccupationCosts
       , Avg(PremisesStaffCosts)                         AS PremisesStaffCosts
       , Avg(TotalUtilitiesCosts)                        AS TotalUtilitiesCosts
       , Avg(EnergyCosts)                                AS EnergyCosts
       , Avg(WaterSewerageCosts)                         AS WaterSewerageCosts
    FROM SchoolExpenditurePerUnitHistoric
   GROUP
      BY Year
       , FinanceType
       , OverallPhase
GO
