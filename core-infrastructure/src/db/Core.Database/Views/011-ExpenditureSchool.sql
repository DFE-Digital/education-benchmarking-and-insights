DROP VIEW IF EXISTS VW_ExpenditureSchoolActual
GO

CREATE VIEW VW_ExpenditureSchoolActual AS
SELECT RunId,
       RunType,
       URN,
       OverallPhase,
       FinanceType,
       TotalPupils,
       TotalInternalFloorArea,
       PeriodCoveredByReturn,
       TotalExpenditure,
       TotalTeachingSupportStaffCosts,
       TeachingStaffCosts,
       SupplyTeachingStaffCosts,
       EducationalConsultancyCosts,
       EducationSupportStaffCosts,
       AgencySupplyTeachingStaffCosts,
       TotalNonEducationalSupportStaffCosts,
       AdministrativeClericalStaffCosts,
       AuditorsCosts,
       OtherStaffCosts,
       ProfessionalServicesNonCurriculumCosts,
       TotalEducationalSuppliesCosts,
       ExaminationFeesCosts,
       LearningResourcesNonIctCosts,
       LearningResourcesIctCosts,
       TotalGrossCateringCosts,
       TotalNetCateringCostsCosts AS 'TotalNetCateringCosts',
       CateringStaffCosts,
       CateringSuppliesCosts,
       TotalOtherCosts,
       DirectRevenueFinancingCosts,
       GroundsMaintenanceCosts,
       IndirectEmployeeExpenses,
       InterestChargesLoanBank,
       OtherInsurancePremiumsCosts,
       PrivateFinanceInitiativeCharges,
       RentRatesCosts,
       SpecialFacilitiesCosts,
       StaffDevelopmentTrainingCosts,
       StaffRelatedInsuranceCosts,
       SupplyTeacherInsurableCosts,
       CommunityFocusedSchoolStaff,
       CommunityFocusedSchoolCosts,
       AdministrativeSuppliesNonEducationalCosts,
       TotalPremisesStaffServiceCosts,
       CleaningCaretakingCosts,
       MaintenancePremisesCosts,
       OtherOccupationCosts,
       PremisesStaffCosts,
       TotalUtilitiesCosts,
       EnergyCosts,
       WaterSewerageCosts
FROM Financial
GO

DROP VIEW IF EXISTS VW_ExpenditureSchoolPercentExpenditure
GO

CREATE VIEW VW_ExpenditureSchoolPercentExpenditure AS
SELECT RunId,
       RunType,
       URN,
       OverallPhase,
       FinanceType,
       TotalPupils,
       TotalInternalFloorArea,
       PeriodCoveredByReturn,
       IIF(TotalExpenditure > 0.0, (TotalExpenditure / TotalExpenditure) * 100, NULL)                          AS TotalExpenditure,
       IIF(TotalExpenditure > 0.0, (TotalTeachingSupportStaffCosts / TotalExpenditure) * 100, NULL)            AS TotalTeachingSupportStaffCosts,
       IIF(TotalExpenditure > 0.0, (TeachingStaffCosts / TotalExpenditure) * 100, NULL)                        AS TeachingStaffCosts,
       IIF(TotalExpenditure > 0.0, (SupplyTeachingStaffCosts / TotalExpenditure) * 100, NULL)                  AS SupplyTeachingStaffCosts,
       IIF(TotalExpenditure > 0.0, (EducationalConsultancyCosts / TotalExpenditure) * 100, NULL)               AS EducationalConsultancyCosts,
       IIF(TotalExpenditure > 0.0, (EducationSupportStaffCosts / TotalExpenditure) * 100, NULL)                AS EducationSupportStaffCosts,
       IIF(TotalExpenditure > 0.0, (AgencySupplyTeachingStaffCosts / TotalExpenditure) * 100, NULL)            AS AgencySupplyTeachingStaffCosts,
       IIF(TotalExpenditure > 0.0, (TotalNonEducationalSupportStaffCosts / TotalExpenditure) * 100, NULL)      AS TotalNonEducationalSupportStaffCosts,
       IIF(TotalExpenditure > 0.0, (AdministrativeClericalStaffCosts / TotalExpenditure) * 100, NULL)          AS AdministrativeClericalStaffCosts,
       IIF(TotalExpenditure > 0.0, (AuditorsCosts / TotalExpenditure) * 100, NULL)                             AS AuditorsCosts,
       IIF(TotalExpenditure > 0.0, (OtherStaffCosts / TotalExpenditure) * 100, NULL)                           AS OtherStaffCosts,
       IIF(TotalExpenditure > 0.0, (ProfessionalServicesNonCurriculumCosts / TotalExpenditure) * 100, NULL)    AS ProfessionalServicesNonCurriculumCosts,
       IIF(TotalExpenditure > 0.0, (TotalEducationalSuppliesCosts / TotalExpenditure) * 100, NULL)             AS TotalEducationalSuppliesCosts,
       IIF(TotalExpenditure > 0.0, (ExaminationFeesCosts / TotalExpenditure) * 100, NULL)                      AS ExaminationFeesCosts,
       IIF(TotalExpenditure > 0.0, (LearningResourcesNonIctCosts / TotalExpenditure) * 100, NULL)              AS LearningResourcesNonIctCosts,
       IIF(TotalExpenditure > 0.0, (LearningResourcesIctCosts / TotalExpenditure) * 100, NULL)                 AS LearningResourcesIctCosts,
       IIF(TotalExpenditure > 0.0, (TotalGrossCateringCosts / TotalExpenditure) * 100, NULL)                   AS TotalGrossCateringCosts,
       IIF(TotalExpenditure > 0.0, (TotalNetCateringCostsCosts / TotalExpenditure) * 100, NULL)                AS TotalNetCateringCosts,
       IIF(TotalExpenditure > 0.0, (CateringStaffCosts / TotalExpenditure) * 100, NULL)                        AS CateringStaffCosts,
       IIF(TotalExpenditure > 0.0, (CateringSuppliesCosts / TotalExpenditure) * 100, NULL)                     AS CateringSuppliesCosts,
       IIF(TotalExpenditure > 0.0, (TotalOtherCosts / TotalExpenditure) * 100, NULL)                           AS TotalOtherCosts,
       IIF(TotalExpenditure > 0.0, (DirectRevenueFinancingCosts / TotalExpenditure) * 100, NULL)               AS DirectRevenueFinancingCosts,
       IIF(TotalExpenditure > 0.0, (GroundsMaintenanceCosts / TotalExpenditure) * 100, NULL)                   AS GroundsMaintenanceCosts,
       IIF(TotalExpenditure > 0.0, (IndirectEmployeeExpenses / TotalExpenditure) * 100, NULL)                  AS IndirectEmployeeExpenses,
       IIF(TotalExpenditure > 0.0, (InterestChargesLoanBank / TotalExpenditure) * 100, NULL)                   AS InterestChargesLoanBank,
       IIF(TotalExpenditure > 0.0, (OtherInsurancePremiumsCosts / TotalExpenditure) * 100, NULL)               AS OtherInsurancePremiumsCosts,
       IIF(TotalExpenditure > 0.0, (PrivateFinanceInitiativeCharges / TotalExpenditure) * 100, NULL)           AS PrivateFinanceInitiativeCharges,
       IIF(TotalExpenditure > 0.0, (RentRatesCosts / TotalExpenditure) * 100, NULL)                            AS RentRatesCosts,
       IIF(TotalExpenditure > 0.0, (SpecialFacilitiesCosts / TotalExpenditure) * 100, NULL)                    AS SpecialFacilitiesCosts,
       IIF(TotalExpenditure > 0.0, (StaffDevelopmentTrainingCosts / TotalExpenditure) * 100, NULL)             AS StaffDevelopmentTrainingCosts,
       IIF(TotalExpenditure > 0.0, (StaffRelatedInsuranceCosts / TotalExpenditure) * 100, NULL)                AS StaffRelatedInsuranceCosts,
       IIF(TotalExpenditure > 0.0, (SupplyTeacherInsurableCosts / TotalExpenditure) * 100, NULL)               AS SupplyTeacherInsurableCosts,
       IIF(TotalExpenditure > 0.0, (CommunityFocusedSchoolStaff / TotalExpenditure) * 100, NULL)               AS CommunityFocusedSchoolStaff,
       IIF(TotalExpenditure > 0.0, (CommunityFocusedSchoolCosts / TotalExpenditure) * 100, NULL)               AS CommunityFocusedSchoolCosts,
       IIF(TotalExpenditure > 0.0, (AdministrativeSuppliesNonEducationalCosts / TotalExpenditure) * 100, NULL) AS AdministrativeSuppliesNonEducationalCosts,
       IIF(TotalExpenditure > 0.0, (TotalPremisesStaffServiceCosts / TotalExpenditure) * 100, NULL)            AS TotalPremisesStaffServiceCosts,
       IIF(TotalExpenditure > 0.0, (CleaningCaretakingCosts / TotalExpenditure) * 100, NULL)                   AS CleaningCaretakingCosts,
       IIF(TotalExpenditure > 0.0, (MaintenancePremisesCosts / TotalExpenditure) * 100, NULL)                  AS MaintenancePremisesCosts,
       IIF(TotalExpenditure > 0.0, (OtherOccupationCosts / TotalExpenditure) * 100, NULL)                      AS OtherOccupationCosts,
       IIF(TotalExpenditure > 0.0, (PremisesStaffCosts / TotalExpenditure) * 100, NULL)                       AS PremisesStaffCosts,
       IIF(TotalExpenditure > 0.0, (TotalUtilitiesCosts / TotalExpenditure) * 100, NULL)                       AS TotalUtilitiesCosts,
       IIF(TotalExpenditure > 0.0, (EnergyCosts / TotalExpenditure) * 100, NULL)                               AS EnergyCosts,
       IIF(TotalExpenditure > 0.0, (WaterSewerageCosts / TotalExpenditure) * 100, NULL)                        AS WaterSewerageCosts
FROM Financial
GO

DROP VIEW IF EXISTS VW_ExpenditureSchoolPercentIncome
GO

CREATE VIEW VW_ExpenditureSchoolPercentIncome AS
SELECT RunId,
       RunType,
       URN,
       OverallPhase,
       FinanceType,
       TotalPupils,
       TotalInternalFloorArea,
       PeriodCoveredByReturn,
       IIF(TotalIncome > 0.0, (TotalExpenditure / TotalIncome) * 100, NULL)                          AS TotalExpenditure,
       IIF(TotalIncome > 0.0, (TotalTeachingSupportStaffCosts / TotalIncome) * 100, NULL)            AS TotalTeachingSupportStaffCosts,
       IIF(TotalIncome > 0.0, (TeachingStaffCosts / TotalIncome) * 100, NULL)                        AS TeachingStaffCosts,
       IIF(TotalIncome > 0.0, (SupplyTeachingStaffCosts / TotalIncome) * 100, NULL)                  AS SupplyTeachingStaffCosts,
       IIF(TotalIncome > 0.0, (EducationalConsultancyCosts / TotalIncome) * 100, NULL)               AS EducationalConsultancyCosts,
       IIF(TotalIncome > 0.0, (EducationSupportStaffCosts / TotalIncome) * 100, NULL)                AS EducationSupportStaffCosts,
       IIF(TotalIncome > 0.0, (AgencySupplyTeachingStaffCosts / TotalIncome) * 100, NULL)            AS AgencySupplyTeachingStaffCosts,
       IIF(TotalIncome > 0.0, (TotalNonEducationalSupportStaffCosts / TotalIncome) * 100, NULL)      AS TotalNonEducationalSupportStaffCosts,
       IIF(TotalIncome > 0.0, (AdministrativeClericalStaffCosts / TotalIncome) * 100, NULL)          AS AdministrativeClericalStaffCosts,
       IIF(TotalIncome > 0.0, (AuditorsCosts / TotalIncome) * 100, NULL)                             AS AuditorsCosts,
       IIF(TotalIncome > 0.0, (OtherStaffCosts / TotalIncome) * 100, NULL)                           AS OtherStaffCosts,
       IIF(TotalIncome > 0.0, (ProfessionalServicesNonCurriculumCosts / TotalIncome) * 100, NULL)    AS ProfessionalServicesNonCurriculumCosts,
       IIF(TotalIncome > 0.0, (TotalEducationalSuppliesCosts / TotalIncome) * 100, NULL)             AS TotalEducationalSuppliesCosts,
       IIF(TotalIncome > 0.0, (ExaminationFeesCosts / TotalIncome) * 100, NULL)                      AS ExaminationFeesCosts,
       IIF(TotalIncome > 0.0, (LearningResourcesNonIctCosts / TotalIncome) * 100, NULL)              AS LearningResourcesNonIctCosts,
       IIF(TotalIncome > 0.0, (LearningResourcesIctCosts / TotalIncome) * 100, NULL)                 AS LearningResourcesIctCosts,
       IIF(TotalIncome > 0.0, (TotalGrossCateringCosts / TotalIncome) * 100, NULL)                   AS TotalGrossCateringCosts,
       IIF(TotalIncome > 0.0, (TotalNetCateringCostsCosts / TotalIncome) * 100, NULL)                AS TotalNetCateringCosts,
       IIF(TotalIncome > 0.0, (CateringStaffCosts / TotalIncome) * 100, NULL)                        AS CateringStaffCosts,
       IIF(TotalIncome > 0.0, (CateringSuppliesCosts / TotalIncome) * 100, NULL)                     AS CateringSuppliesCosts,
       IIF(TotalIncome > 0.0, (TotalOtherCosts / TotalIncome) * 100, NULL)                           AS TotalOtherCosts,
       IIF(TotalIncome > 0.0, (DirectRevenueFinancingCosts / TotalIncome) * 100, NULL)               AS DirectRevenueFinancingCosts,
       IIF(TotalIncome > 0.0, (GroundsMaintenanceCosts / TotalIncome) * 100, NULL)                   AS GroundsMaintenanceCosts,
       IIF(TotalIncome > 0.0, (IndirectEmployeeExpenses / TotalIncome) * 100, NULL)                  AS IndirectEmployeeExpenses,
       IIF(TotalIncome > 0.0, (InterestChargesLoanBank / TotalIncome) * 100, NULL)                   AS InterestChargesLoanBank,
       IIF(TotalIncome > 0.0, (OtherInsurancePremiumsCosts / TotalIncome) * 100, NULL)               AS OtherInsurancePremiumsCosts,
       IIF(TotalIncome > 0.0, (PrivateFinanceInitiativeCharges / TotalIncome) * 100, NULL)           AS PrivateFinanceInitiativeCharges,
       IIF(TotalIncome > 0.0, (RentRatesCosts / TotalIncome) * 100, NULL)                            AS RentRatesCosts,
       IIF(TotalIncome > 0.0, (SpecialFacilitiesCosts / TotalIncome) * 100, NULL)                    AS SpecialFacilitiesCosts,
       IIF(TotalIncome > 0.0, (StaffDevelopmentTrainingCosts / TotalIncome) * 100, NULL)             AS StaffDevelopmentTrainingCosts,
       IIF(TotalIncome > 0.0, (StaffRelatedInsuranceCosts / TotalIncome) * 100, NULL)                AS StaffRelatedInsuranceCosts,
       IIF(TotalIncome > 0.0, (SupplyTeacherInsurableCosts / TotalIncome) * 100, NULL)               AS SupplyTeacherInsurableCosts,
       IIF(TotalIncome > 0.0, (CommunityFocusedSchoolStaff / TotalIncome) * 100, NULL)               AS CommunityFocusedSchoolStaff,
       IIF(TotalIncome > 0.0, (CommunityFocusedSchoolCosts / TotalIncome) * 100, NULL)               AS CommunityFocusedSchoolCosts,
       IIF(TotalIncome > 0.0, (AdministrativeSuppliesNonEducationalCosts / TotalIncome) * 100, NULL) AS AdministrativeSuppliesNonEducationalCosts,
       IIF(TotalIncome > 0.0, (TotalPremisesStaffServiceCosts / TotalIncome) * 100, NULL)            AS TotalPremisesStaffServiceCosts,
       IIF(TotalIncome > 0.0, (CleaningCaretakingCosts / TotalIncome) * 100, NULL)                   AS CleaningCaretakingCosts,
       IIF(TotalIncome > 0.0, (MaintenancePremisesCosts / TotalIncome) * 100, NULL)                  AS MaintenancePremisesCosts,
       IIF(TotalIncome > 0.0, (OtherOccupationCosts / TotalIncome) * 100, NULL)                      AS OtherOccupationCosts,
       IIF(TotalIncome > 0.0, (PremisesStaffCosts / TotalIncome) * 100, NULL)                        AS PremisesStaffCosts,
       IIF(TotalIncome > 0.0, (TotalUtilitiesCosts / TotalIncome) * 100, NULL)                       AS TotalUtilitiesCosts,
       IIF(TotalIncome > 0.0, (EnergyCosts / TotalIncome) * 100, NULL)                               AS EnergyCosts,
       IIF(TotalIncome > 0.0, (WaterSewerageCosts / TotalIncome) * 100, NULL)                        AS WaterSewerageCosts
FROM Financial
GO

DROP VIEW IF EXISTS VW_ExpenditureSchoolPerUnit
GO

CREATE VIEW VW_ExpenditureSchoolPerUnit AS
SELECT RunId,
       RunType,
       URN,
       OverallPhase,
       FinanceType,
       TotalPupils,
       TotalInternalFloorArea,
       PeriodCoveredByReturn,
       IIF(TotalPupils > 0.0, TotalExpenditure / TotalPupils, NULL)                          AS TotalExpenditure,
       IIF(TotalPupils > 0.0, TotalTeachingSupportStaffCosts / TotalPupils, NULL)            AS TotalTeachingSupportStaffCosts,
       IIF(TotalPupils > 0.0, TeachingStaffCosts / TotalPupils, NULL)                        AS TeachingStaffCosts,
       IIF(TotalPupils > 0.0, SupplyTeachingStaffCosts / TotalPupils, NULL)                  AS SupplyTeachingStaffCosts,
       IIF(TotalPupils > 0.0, EducationalConsultancyCosts / TotalPupils, NULL)               AS EducationalConsultancyCosts,
       IIF(TotalPupils > 0.0, EducationSupportStaffCosts / TotalPupils, NULL)                AS EducationSupportStaffCosts,
       IIF(TotalPupils > 0.0, AgencySupplyTeachingStaffCosts / TotalPupils, NULL)            AS AgencySupplyTeachingStaffCosts,
       IIF(TotalPupils > 0.0, TotalNonEducationalSupportStaffCosts / TotalPupils, NULL)      AS TotalNonEducationalSupportStaffCosts,
       IIF(TotalPupils > 0.0, AdministrativeClericalStaffCosts / TotalPupils, NULL)          AS AdministrativeClericalStaffCosts,
       IIF(TotalPupils > 0.0, AuditorsCosts / TotalPupils, NULL)                             AS AuditorsCosts,
       IIF(TotalPupils > 0.0, OtherStaffCosts / TotalPupils, NULL)                           AS OtherStaffCosts,
       IIF(TotalPupils > 0.0, ProfessionalServicesNonCurriculumCosts / TotalPupils, NULL)    AS ProfessionalServicesNonCurriculumCosts,
       IIF(TotalPupils > 0.0, TotalEducationalSuppliesCosts / TotalPupils, NULL)             AS TotalEducationalSuppliesCosts,
       IIF(TotalPupils > 0.0, ExaminationFeesCosts / TotalPupils, NULL)                      AS ExaminationFeesCosts,
       IIF(TotalPupils > 0.0, LearningResourcesNonIctCosts / TotalPupils, NULL)              AS LearningResourcesNonIctCosts,
       IIF(TotalPupils > 0.0, LearningResourcesIctCosts / TotalPupils, NULL)                 AS LearningResourcesIctCosts,
       IIF(TotalPupils > 0.0, TotalGrossCateringCosts / TotalPupils, NULL)                   AS TotalGrossCateringCosts,
       IIF(TotalPupils > 0.0, TotalNetCateringCostsCosts / TotalPupils, NULL)                AS TotalNetCateringCosts,
       IIF(TotalPupils > 0.0, CateringStaffCosts / TotalPupils, NULL)                        AS CateringStaffCosts,
       IIF(TotalPupils > 0.0, CateringSuppliesCosts / TotalPupils, NULL)                     AS CateringSuppliesCosts,
       IIF(TotalPupils > 0.0, TotalOtherCosts / TotalPupils, NULL)                           AS TotalOtherCosts,
       IIF(TotalPupils > 0.0, DirectRevenueFinancingCosts / TotalPupils, NULL)               AS DirectRevenueFinancingCosts,
       IIF(TotalPupils > 0.0, GroundsMaintenanceCosts / TotalPupils, NULL)                   AS GroundsMaintenanceCosts,
       IIF(TotalPupils > 0.0, IndirectEmployeeExpenses / TotalPupils, NULL)                  AS IndirectEmployeeExpenses,
       IIF(TotalPupils > 0.0, InterestChargesLoanBank / TotalPupils, NULL)                   AS InterestChargesLoanBank,
       IIF(TotalPupils > 0.0, OtherInsurancePremiumsCosts / TotalPupils, NULL)               AS OtherInsurancePremiumsCosts,
       IIF(TotalPupils > 0.0, PrivateFinanceInitiativeCharges / TotalPupils, NULL)           AS PrivateFinanceInitiativeCharges,
       IIF(TotalPupils > 0.0, RentRatesCosts / TotalPupils, NULL)                            AS RentRatesCosts,
       IIF(TotalPupils > 0.0, SpecialFacilitiesCosts / TotalPupils, NULL)                    AS SpecialFacilitiesCosts,
       IIF(TotalPupils > 0.0, StaffDevelopmentTrainingCosts / TotalPupils, NULL)             AS StaffDevelopmentTrainingCosts,
       IIF(TotalPupils > 0.0, StaffRelatedInsuranceCosts / TotalPupils, NULL)                AS StaffRelatedInsuranceCosts,
       IIF(TotalPupils > 0.0, SupplyTeacherInsurableCosts / TotalPupils, NULL)               AS SupplyTeacherInsurableCosts,
       IIF(TotalPupils > 0.0, CommunityFocusedSchoolStaff / TotalPupils, NULL)               AS CommunityFocusedSchoolStaff,
       IIF(TotalPupils > 0.0, CommunityFocusedSchoolCosts / TotalPupils, NULL)               AS CommunityFocusedSchoolCosts,
       IIF(TotalPupils > 0.0, AdministrativeSuppliesNonEducationalCosts / TotalPupils, NULL) AS AdministrativeSuppliesNonEducationalCosts,
       IIF(TotalInternalFloorArea > 0.0, TotalPremisesStaffServiceCosts / TotalInternalFloorArea, NULL)            AS TotalPremisesStaffServiceCosts,
       IIF(TotalInternalFloorArea > 0.0, CleaningCaretakingCosts / TotalInternalFloorArea, NULL)                   AS CleaningCaretakingCosts,
       IIF(TotalInternalFloorArea > 0.0, MaintenancePremisesCosts / TotalInternalFloorArea, NULL)                  AS MaintenancePremisesCosts,
       IIF(TotalInternalFloorArea > 0.0, OtherOccupationCosts / TotalInternalFloorArea, NULL)                      AS OtherOccupationCosts,
       IIF(TotalInternalFloorArea > 0.0, PremisesStaffCosts / TotalInternalFloorArea, NULL)                        AS PremisesStaffCosts,
       IIF(TotalInternalFloorArea > 0.0, TotalUtilitiesCosts / TotalInternalFloorArea, NULL)                       AS TotalUtilitiesCosts,
       IIF(TotalInternalFloorArea > 0.0, EnergyCosts / TotalInternalFloorArea, NULL)                               AS EnergyCosts,
       IIF(TotalInternalFloorArea > 0.0, WaterSewerageCosts / TotalInternalFloorArea, NULL)                        AS WaterSewerageCosts
FROM Financial
GO

DROP VIEW IF EXISTS VW_ExpenditureSchoolDefaultNormalisedActual
GO

CREATE VIEW VW_ExpenditureSchoolDefaultNormalisedActual AS
SELECT RunId,
       RunType,
       URN,
       OverallPhase,
       FinanceType,
       TotalPupils,
       TotalInternalFloorArea,
       PeriodCoveredByReturn,
       IIF(TotalExpenditure IS NULL OR TotalExpenditure <= 0.0, NULL, TotalExpenditure) AS TotalExpenditure,
       IIF(TotalTeachingSupportStaffCosts IS NULL OR TotalTeachingSupportStaffCosts <= 0.0, NULL, TotalTeachingSupportStaffCosts) AS TotalTeachingSupportStaffCosts,
       IIF(TeachingStaffCosts IS NULL OR TeachingStaffCosts <= 0.0, NULL, TeachingStaffCosts) AS TeachingStaffCosts,
       IIF(SupplyTeachingStaffCosts IS NULL OR SupplyTeachingStaffCosts <= 0.0, NULL, SupplyTeachingStaffCosts) AS SupplyTeachingStaffCosts,
       IIF(EducationalConsultancyCosts IS NULL OR EducationalConsultancyCosts <= 0.0, NULL, EducationalConsultancyCosts) AS EducationalConsultancyCosts,
       IIF(EducationSupportStaffCosts IS NULL OR EducationSupportStaffCosts <= 0.0, NULL, EducationSupportStaffCosts) AS EducationSupportStaffCosts,
       IIF(AgencySupplyTeachingStaffCosts IS NULL OR AgencySupplyTeachingStaffCosts <= 0.0, NULL, AgencySupplyTeachingStaffCosts) AS AgencySupplyTeachingStaffCosts,
       IIF(TotalNonEducationalSupportStaffCosts IS NULL OR TotalNonEducationalSupportStaffCosts <= 0.0, NULL, TotalNonEducationalSupportStaffCosts) AS TotalNonEducationalSupportStaffCosts,
       IIF(AdministrativeClericalStaffCosts IS NULL OR AdministrativeClericalStaffCosts <= 0.0, NULL, AdministrativeClericalStaffCosts) AS AdministrativeClericalStaffCosts,
       IIF(AuditorsCosts IS NULL OR AuditorsCosts <= 0.0, NULL, AuditorsCosts) AS AuditorsCosts,
       IIF(OtherStaffCosts IS NULL OR OtherStaffCosts <= 0.0, NULL, OtherStaffCosts) AS OtherStaffCosts,
       IIF(ProfessionalServicesNonCurriculumCosts IS NULL OR ProfessionalServicesNonCurriculumCosts <= 0.0, NULL, ProfessionalServicesNonCurriculumCosts) AS ProfessionalServicesNonCurriculumCosts,
       IIF(TotalEducationalSuppliesCosts IS NULL OR TotalEducationalSuppliesCosts <= 0.0, NULL, TotalEducationalSuppliesCosts) AS TotalEducationalSuppliesCosts,
       IIF(ExaminationFeesCosts IS NULL OR ExaminationFeesCosts <= 0.0, NULL, ExaminationFeesCosts) AS ExaminationFeesCosts,
       IIF(LearningResourcesNonIctCosts IS NULL OR LearningResourcesNonIctCosts <= 0.0, NULL, LearningResourcesNonIctCosts) AS LearningResourcesNonIctCosts,
       IIF(LearningResourcesIctCosts IS NULL OR LearningResourcesIctCosts <= 0.0, NULL, LearningResourcesIctCosts ) AS LearningResourcesIctCosts,
       IIF(TotalGrossCateringCosts IS NULL OR TotalGrossCateringCosts <= 0.0, NULL, TotalGrossCateringCosts) AS TotalGrossCateringCosts,
       IIF(TotalNetCateringCosts IS NULL OR TotalNetCateringCosts <= 0.0, NULL, TotalNetCateringCosts) AS TotalNetCateringCosts,
       IIF(CateringStaffCosts IS NULL OR CateringStaffCosts <= 0.0, NULL, CateringStaffCosts) AS CateringStaffCosts,
       IIF(CateringSuppliesCosts IS NULL OR CateringSuppliesCosts <= 0.0, NULL, CateringSuppliesCosts) AS CateringSuppliesCosts,
       IIF(TotalOtherCosts IS NULL OR TotalOtherCosts <= 0.0, NULL, TotalOtherCosts) AS TotalOtherCosts,
       IIF(DirectRevenueFinancingCosts IS NULL OR DirectRevenueFinancingCosts <= 0.0, NULL, DirectRevenueFinancingCosts) AS DirectRevenueFinancingCosts,
       IIF(GroundsMaintenanceCosts IS NULL OR GroundsMaintenanceCosts <= 0.0, NULL,GroundsMaintenanceCosts) AS GroundsMaintenanceCosts,
       IIF(IndirectEmployeeExpenses IS NULL OR IndirectEmployeeExpenses <= 0.0, NULL,IndirectEmployeeExpenses) AS IndirectEmployeeExpenses,
       IIF(InterestChargesLoanBank IS NULL OR InterestChargesLoanBank <= 0.0,NULL,InterestChargesLoanBank) AS InterestChargesLoanBank,
       IIF(OtherInsurancePremiumsCosts IS NULL OR OtherInsurancePremiumsCosts <= 0.0,NULL,OtherInsurancePremiumsCosts) AS OtherInsurancePremiumsCosts,
       IIF(PrivateFinanceInitiativeCharges IS NULL OR PrivateFinanceInitiativeCharges <= 0.0,NULL,PrivateFinanceInitiativeCharges) AS PrivateFinanceInitiativeCharges,
       IIF(RentRatesCosts IS NULL OR RentRatesCosts <= 0.0,NULL,RentRatesCosts) AS RentRatesCosts,
       IIF(SpecialFacilitiesCosts IS NULL OR SpecialFacilitiesCosts <= 0.0,NULL,SpecialFacilitiesCosts) AS SpecialFacilitiesCosts,
       IIF(StaffDevelopmentTrainingCosts IS NULL OR StaffDevelopmentTrainingCosts <= 0.0,NULL,StaffDevelopmentTrainingCosts) AS StaffDevelopmentTrainingCosts,
       IIF(StaffRelatedInsuranceCosts IS NULL OR StaffRelatedInsuranceCosts <= 0.0,NULL,StaffRelatedInsuranceCosts) AS StaffRelatedInsuranceCosts,
       IIF(SupplyTeacherInsurableCosts IS NULL OR SupplyTeacherInsurableCosts <= 0.0,NULL,SupplyTeacherInsurableCosts) AS SupplyTeacherInsurableCosts,
       IIF(CommunityFocusedSchoolStaff IS NULL OR CommunityFocusedSchoolStaff <= 0.0,NULL,CommunityFocusedSchoolStaff) AS CommunityFocusedSchoolStaff,
       IIF(CommunityFocusedSchoolCosts IS NULL OR CommunityFocusedSchoolCosts <= 0.0,NULL,CommunityFocusedSchoolCosts) AS CommunityFocusedSchoolCosts,
       IIF(AdministrativeSuppliesNonEducationalCosts IS NULL OR AdministrativeSuppliesNonEducationalCosts <= 0.0,NULL,AdministrativeSuppliesNonEducationalCosts) AS AdministrativeSuppliesNonEducationalCosts,
       IIF(TotalPremisesStaffServiceCosts IS NULL OR TotalPremisesStaffServiceCosts <= 0.0,NULL,TotalPremisesStaffServiceCosts) AS TotalPremisesStaffServiceCosts,
       IIF(CleaningCaretakingCosts IS NULL OR CleaningCaretakingCosts <= 0.0,NULL,CleaningCaretakingCosts) AS CleaningCaretakingCosts,
       IIF(MaintenancePremisesCosts IS NULL OR MaintenancePremisesCosts <= 0.0,NULL,MaintenancePremisesCosts) AS MaintenancePremisesCosts,
       IIF(OtherOccupationCosts IS NULL OR OtherOccupationCosts <= 0.0,NULL,OtherOccupationCosts) AS OtherOccupationCosts,
       IIF(PremisesStaffCosts IS NULL OR PremisesStaffCosts <= 0.0,NULL,PremisesStaffCosts) AS PremisesStaffCosts,
       IIF(TotalUtilitiesCosts IS NULL OR TotalUtilitiesCosts <= 0.0,NULL,TotalUtilitiesCosts) AS TotalUtilitiesCosts,
       IIF(EnergyCosts IS NULL OR EnergyCosts <= 0.0,NULL,EnergyCosts) AS EnergyCosts,
       IIF(WaterSewerageCosts IS NULL OR WaterSewerageCosts <= 0.0,NULL,WaterSewerageCosts) AS WaterSewerageCosts
FROM VW_ExpenditureSchoolActual
WHERE RunType = 'default'
GO

DROP VIEW IF EXISTS VW_ExpenditureSchoolDefaultNormalisedPercentExpenditure
GO

CREATE VIEW VW_ExpenditureSchoolDefaultNormalisedPercentExpenditure AS
SELECT RunId,
       RunType,
       URN,
       OverallPhase,
       FinanceType,
       TotalPupils,
       TotalInternalFloorArea,
       PeriodCoveredByReturn,
       IIF(TotalExpenditure IS NULL OR TotalExpenditure <= 0.0, NULL, TotalExpenditure) AS TotalExpenditure,
       IIF(TotalTeachingSupportStaffCosts IS NULL OR TotalTeachingSupportStaffCosts <= 0.0, NULL, TotalTeachingSupportStaffCosts) AS TotalTeachingSupportStaffCosts,
       IIF(TeachingStaffCosts IS NULL OR TeachingStaffCosts <= 0.0, NULL, TeachingStaffCosts) AS TeachingStaffCosts,
       IIF(SupplyTeachingStaffCosts IS NULL OR SupplyTeachingStaffCosts <= 0.0, NULL, SupplyTeachingStaffCosts) AS SupplyTeachingStaffCosts,
       IIF(EducationalConsultancyCosts IS NULL OR EducationalConsultancyCosts <= 0.0, NULL, EducationalConsultancyCosts) AS EducationalConsultancyCosts,
       IIF(EducationSupportStaffCosts IS NULL OR EducationSupportStaffCosts <= 0.0, NULL, EducationSupportStaffCosts) AS EducationSupportStaffCosts,
       IIF(AgencySupplyTeachingStaffCosts IS NULL OR AgencySupplyTeachingStaffCosts <= 0.0, NULL, AgencySupplyTeachingStaffCosts) AS AgencySupplyTeachingStaffCosts,
       IIF(TotalNonEducationalSupportStaffCosts IS NULL OR TotalNonEducationalSupportStaffCosts <= 0.0, NULL, TotalNonEducationalSupportStaffCosts) AS TotalNonEducationalSupportStaffCosts,
       IIF(AdministrativeClericalStaffCosts IS NULL OR AdministrativeClericalStaffCosts <= 0.0, NULL, AdministrativeClericalStaffCosts) AS AdministrativeClericalStaffCosts,
       IIF(AuditorsCosts IS NULL OR AuditorsCosts <= 0.0, NULL, AuditorsCosts) AS AuditorsCosts,
       IIF(OtherStaffCosts IS NULL OR OtherStaffCosts <= 0.0, NULL, OtherStaffCosts) AS OtherStaffCosts,
       IIF(ProfessionalServicesNonCurriculumCosts IS NULL OR ProfessionalServicesNonCurriculumCosts <= 0.0, NULL, ProfessionalServicesNonCurriculumCosts) AS ProfessionalServicesNonCurriculumCosts,
       IIF(TotalEducationalSuppliesCosts IS NULL OR TotalEducationalSuppliesCosts <= 0.0, NULL, TotalEducationalSuppliesCosts) AS TotalEducationalSuppliesCosts,
       IIF(ExaminationFeesCosts IS NULL OR ExaminationFeesCosts <= 0.0, NULL, ExaminationFeesCosts) AS ExaminationFeesCosts,
       IIF(LearningResourcesNonIctCosts IS NULL OR LearningResourcesNonIctCosts <= 0.0, NULL, LearningResourcesNonIctCosts) AS LearningResourcesNonIctCosts,
       IIF(LearningResourcesIctCosts IS NULL OR LearningResourcesIctCosts <= 0.0, NULL, LearningResourcesIctCosts ) AS LearningResourcesIctCosts,
       IIF(TotalGrossCateringCosts IS NULL OR TotalGrossCateringCosts <= 0.0, NULL, TotalGrossCateringCosts) AS TotalGrossCateringCosts,
       IIF(TotalNetCateringCosts IS NULL OR TotalNetCateringCosts <= 0.0, NULL, TotalNetCateringCosts) AS TotalNetCateringCosts,
       IIF(CateringStaffCosts IS NULL OR CateringStaffCosts <= 0.0, NULL, CateringStaffCosts) AS CateringStaffCosts,
       IIF(CateringSuppliesCosts IS NULL OR CateringSuppliesCosts <= 0.0, NULL, CateringSuppliesCosts) AS CateringSuppliesCosts,
       IIF(TotalOtherCosts IS NULL OR TotalOtherCosts <= 0.0, NULL, TotalOtherCosts) AS TotalOtherCosts,
       IIF(DirectRevenueFinancingCosts IS NULL OR DirectRevenueFinancingCosts <= 0.0, NULL, DirectRevenueFinancingCosts) AS DirectRevenueFinancingCosts,
       IIF(GroundsMaintenanceCosts IS NULL OR GroundsMaintenanceCosts <= 0.0, NULL,GroundsMaintenanceCosts) AS GroundsMaintenanceCosts,
       IIF(IndirectEmployeeExpenses IS NULL OR IndirectEmployeeExpenses <= 0.0, NULL,IndirectEmployeeExpenses) AS IndirectEmployeeExpenses,
       IIF(InterestChargesLoanBank IS NULL OR InterestChargesLoanBank <= 0.0,NULL,InterestChargesLoanBank) AS InterestChargesLoanBank,
       IIF(OtherInsurancePremiumsCosts IS NULL OR OtherInsurancePremiumsCosts <= 0.0,NULL,OtherInsurancePremiumsCosts) AS OtherInsurancePremiumsCosts,
       IIF(PrivateFinanceInitiativeCharges IS NULL OR PrivateFinanceInitiativeCharges <= 0.0,NULL,PrivateFinanceInitiativeCharges) AS PrivateFinanceInitiativeCharges,
       IIF(RentRatesCosts IS NULL OR RentRatesCosts <= 0.0,NULL,RentRatesCosts) AS RentRatesCosts,
       IIF(SpecialFacilitiesCosts IS NULL OR SpecialFacilitiesCosts <= 0.0,NULL,SpecialFacilitiesCosts) AS SpecialFacilitiesCosts,
       IIF(StaffDevelopmentTrainingCosts IS NULL OR StaffDevelopmentTrainingCosts <= 0.0,NULL,StaffDevelopmentTrainingCosts) AS StaffDevelopmentTrainingCosts,
       IIF(StaffRelatedInsuranceCosts IS NULL OR StaffRelatedInsuranceCosts <= 0.0,NULL,StaffRelatedInsuranceCosts) AS StaffRelatedInsuranceCosts,
       IIF(SupplyTeacherInsurableCosts IS NULL OR SupplyTeacherInsurableCosts <= 0.0,NULL,SupplyTeacherInsurableCosts) AS SupplyTeacherInsurableCosts,
       IIF(CommunityFocusedSchoolStaff IS NULL OR CommunityFocusedSchoolStaff <= 0.0,NULL,CommunityFocusedSchoolStaff) AS CommunityFocusedSchoolStaff,
       IIF(CommunityFocusedSchoolCosts IS NULL OR CommunityFocusedSchoolCosts <= 0.0,NULL,CommunityFocusedSchoolCosts) AS CommunityFocusedSchoolCosts,
       IIF(AdministrativeSuppliesNonEducationalCosts IS NULL OR AdministrativeSuppliesNonEducationalCosts <= 0.0,NULL,AdministrativeSuppliesNonEducationalCosts) AS AdministrativeSuppliesNonEducationalCosts,
       IIF(TotalPremisesStaffServiceCosts IS NULL OR TotalPremisesStaffServiceCosts <= 0.0,NULL,TotalPremisesStaffServiceCosts) AS TotalPremisesStaffServiceCosts,
       IIF(CleaningCaretakingCosts IS NULL OR CleaningCaretakingCosts <= 0.0,NULL,CleaningCaretakingCosts) AS CleaningCaretakingCosts,
       IIF(MaintenancePremisesCosts IS NULL OR MaintenancePremisesCosts <= 0.0,NULL,MaintenancePremisesCosts) AS MaintenancePremisesCosts,
       IIF(OtherOccupationCosts IS NULL OR OtherOccupationCosts <= 0.0,NULL,OtherOccupationCosts) AS OtherOccupationCosts,
       IIF(PremisesStaffCosts IS NULL OR PremisesStaffCosts <= 0.0,NULL,PremisesStaffCosts) AS PremisesStaffCosts,
       IIF(TotalUtilitiesCosts IS NULL OR TotalUtilitiesCosts <= 0.0,NULL,TotalUtilitiesCosts) AS TotalUtilitiesCosts,
       IIF(EnergyCosts IS NULL OR EnergyCosts <= 0.0,NULL,EnergyCosts) AS EnergyCosts,
       IIF(WaterSewerageCosts IS NULL OR WaterSewerageCosts <= 0.0,NULL,WaterSewerageCosts) AS WaterSewerageCosts
FROM VW_ExpenditureSchoolPercentExpenditure
WHERE RunType = 'default'
GO

DROP VIEW IF EXISTS VW_ExpenditureSchoolDefaultNormalisedPercentIncome
GO

CREATE VIEW VW_ExpenditureSchoolDefaultNormalisedPercentIncome AS
SELECT RunId,
       RunType,
       URN,
       OverallPhase,
       FinanceType,
       TotalPupils,
       TotalInternalFloorArea,
       PeriodCoveredByReturn,
       IIF(TotalExpenditure IS NULL OR TotalExpenditure <= 0.0, NULL, TotalExpenditure) AS TotalExpenditure,
       IIF(TotalTeachingSupportStaffCosts IS NULL OR TotalTeachingSupportStaffCosts <= 0.0, NULL, TotalTeachingSupportStaffCosts) AS TotalTeachingSupportStaffCosts,
       IIF(TeachingStaffCosts IS NULL OR TeachingStaffCosts <= 0.0, NULL, TeachingStaffCosts) AS TeachingStaffCosts,
       IIF(SupplyTeachingStaffCosts IS NULL OR SupplyTeachingStaffCosts <= 0.0, NULL, SupplyTeachingStaffCosts) AS SupplyTeachingStaffCosts,
       IIF(EducationalConsultancyCosts IS NULL OR EducationalConsultancyCosts <= 0.0, NULL, EducationalConsultancyCosts) AS EducationalConsultancyCosts,
       IIF(EducationSupportStaffCosts IS NULL OR EducationSupportStaffCosts <= 0.0, NULL, EducationSupportStaffCosts) AS EducationSupportStaffCosts,
       IIF(AgencySupplyTeachingStaffCosts IS NULL OR AgencySupplyTeachingStaffCosts <= 0.0, NULL, AgencySupplyTeachingStaffCosts) AS AgencySupplyTeachingStaffCosts,
       IIF(TotalNonEducationalSupportStaffCosts IS NULL OR TotalNonEducationalSupportStaffCosts <= 0.0, NULL, TotalNonEducationalSupportStaffCosts) AS TotalNonEducationalSupportStaffCosts,
       IIF(AdministrativeClericalStaffCosts IS NULL OR AdministrativeClericalStaffCosts <= 0.0, NULL, AdministrativeClericalStaffCosts) AS AdministrativeClericalStaffCosts,
       IIF(AuditorsCosts IS NULL OR AuditorsCosts <= 0.0, NULL, AuditorsCosts) AS AuditorsCosts,
       IIF(OtherStaffCosts IS NULL OR OtherStaffCosts <= 0.0, NULL, OtherStaffCosts) AS OtherStaffCosts,
       IIF(ProfessionalServicesNonCurriculumCosts IS NULL OR ProfessionalServicesNonCurriculumCosts <= 0.0, NULL, ProfessionalServicesNonCurriculumCosts) AS ProfessionalServicesNonCurriculumCosts,
       IIF(TotalEducationalSuppliesCosts IS NULL OR TotalEducationalSuppliesCosts <= 0.0, NULL, TotalEducationalSuppliesCosts) AS TotalEducationalSuppliesCosts,
       IIF(ExaminationFeesCosts IS NULL OR ExaminationFeesCosts <= 0.0, NULL, ExaminationFeesCosts) AS ExaminationFeesCosts,
       IIF(LearningResourcesNonIctCosts IS NULL OR LearningResourcesNonIctCosts <= 0.0, NULL, LearningResourcesNonIctCosts) AS LearningResourcesNonIctCosts,
       IIF(LearningResourcesIctCosts IS NULL OR LearningResourcesIctCosts <= 0.0, NULL, LearningResourcesIctCosts ) AS LearningResourcesIctCosts,
       IIF(TotalGrossCateringCosts IS NULL OR TotalGrossCateringCosts <= 0.0, NULL, TotalGrossCateringCosts) AS TotalGrossCateringCosts,
       IIF(TotalNetCateringCosts IS NULL OR TotalNetCateringCosts <= 0.0, NULL, TotalNetCateringCosts) AS TotalNetCateringCosts,
       IIF(CateringStaffCosts IS NULL OR CateringStaffCosts <= 0.0, NULL, CateringStaffCosts) AS CateringStaffCosts,
       IIF(CateringSuppliesCosts IS NULL OR CateringSuppliesCosts <= 0.0, NULL, CateringSuppliesCosts) AS CateringSuppliesCosts,
       IIF(TotalOtherCosts IS NULL OR TotalOtherCosts <= 0.0, NULL, TotalOtherCosts) AS TotalOtherCosts,
       IIF(DirectRevenueFinancingCosts IS NULL OR DirectRevenueFinancingCosts <= 0.0, NULL, DirectRevenueFinancingCosts) AS DirectRevenueFinancingCosts,
       IIF(GroundsMaintenanceCosts IS NULL OR GroundsMaintenanceCosts <= 0.0, NULL,GroundsMaintenanceCosts) AS GroundsMaintenanceCosts,
       IIF(IndirectEmployeeExpenses IS NULL OR IndirectEmployeeExpenses <= 0.0, NULL,IndirectEmployeeExpenses) AS IndirectEmployeeExpenses,
       IIF(InterestChargesLoanBank IS NULL OR InterestChargesLoanBank <= 0.0,NULL,InterestChargesLoanBank) AS InterestChargesLoanBank,
       IIF(OtherInsurancePremiumsCosts IS NULL OR OtherInsurancePremiumsCosts <= 0.0,NULL,OtherInsurancePremiumsCosts) AS OtherInsurancePremiumsCosts,
       IIF(PrivateFinanceInitiativeCharges IS NULL OR PrivateFinanceInitiativeCharges <= 0.0,NULL,PrivateFinanceInitiativeCharges) AS PrivateFinanceInitiativeCharges,
       IIF(RentRatesCosts IS NULL OR RentRatesCosts <= 0.0,NULL,RentRatesCosts) AS RentRatesCosts,
       IIF(SpecialFacilitiesCosts IS NULL OR SpecialFacilitiesCosts <= 0.0,NULL,SpecialFacilitiesCosts) AS SpecialFacilitiesCosts,
       IIF(StaffDevelopmentTrainingCosts IS NULL OR StaffDevelopmentTrainingCosts <= 0.0,NULL,StaffDevelopmentTrainingCosts) AS StaffDevelopmentTrainingCosts,
       IIF(StaffRelatedInsuranceCosts IS NULL OR StaffRelatedInsuranceCosts <= 0.0,NULL,StaffRelatedInsuranceCosts) AS StaffRelatedInsuranceCosts,
       IIF(SupplyTeacherInsurableCosts IS NULL OR SupplyTeacherInsurableCosts <= 0.0,NULL,SupplyTeacherInsurableCosts) AS SupplyTeacherInsurableCosts,
       IIF(CommunityFocusedSchoolStaff IS NULL OR CommunityFocusedSchoolStaff <= 0.0,NULL,CommunityFocusedSchoolStaff) AS CommunityFocusedSchoolStaff,
       IIF(CommunityFocusedSchoolCosts IS NULL OR CommunityFocusedSchoolCosts <= 0.0,NULL,CommunityFocusedSchoolCosts) AS CommunityFocusedSchoolCosts,
       IIF(AdministrativeSuppliesNonEducationalCosts IS NULL OR AdministrativeSuppliesNonEducationalCosts <= 0.0,NULL,AdministrativeSuppliesNonEducationalCosts) AS AdministrativeSuppliesNonEducationalCosts,
       IIF(TotalPremisesStaffServiceCosts IS NULL OR TotalPremisesStaffServiceCosts <= 0.0,NULL,TotalPremisesStaffServiceCosts) AS TotalPremisesStaffServiceCosts,
       IIF(CleaningCaretakingCosts IS NULL OR CleaningCaretakingCosts <= 0.0,NULL,CleaningCaretakingCosts) AS CleaningCaretakingCosts,
       IIF(MaintenancePremisesCosts IS NULL OR MaintenancePremisesCosts <= 0.0,NULL,MaintenancePremisesCosts) AS MaintenancePremisesCosts,
       IIF(OtherOccupationCosts IS NULL OR OtherOccupationCosts <= 0.0,NULL,OtherOccupationCosts) AS OtherOccupationCosts,
       IIF(PremisesStaffCosts IS NULL OR PremisesStaffCosts <= 0.0,NULL,PremisesStaffCosts) AS PremisesStaffCosts,
       IIF(TotalUtilitiesCosts IS NULL OR TotalUtilitiesCosts <= 0.0,NULL,TotalUtilitiesCosts) AS TotalUtilitiesCosts,
       IIF(EnergyCosts IS NULL OR EnergyCosts <= 0.0,NULL,EnergyCosts) AS EnergyCosts,
       IIF(WaterSewerageCosts IS NULL OR WaterSewerageCosts <= 0.0,NULL,WaterSewerageCosts) AS WaterSewerageCosts
FROM VW_ExpenditureSchoolPercentIncome
WHERE RunType = 'default'
GO

DROP VIEW IF EXISTS VW_ExpenditureSchoolDefaultNormalisedPerUnit
GO

CREATE VIEW VW_ExpenditureSchoolDefaultNormalisedPerUnit AS
SELECT RunId,
       RunType,
       URN,
       OverallPhase,
       FinanceType,
       TotalPupils,
       TotalInternalFloorArea,
       PeriodCoveredByReturn,
       IIF(TotalExpenditure IS NULL OR TotalExpenditure <= 0.0, NULL, TotalExpenditure) AS TotalExpenditure,
       IIF(TotalTeachingSupportStaffCosts IS NULL OR TotalTeachingSupportStaffCosts <= 0.0, NULL, TotalTeachingSupportStaffCosts) AS TotalTeachingSupportStaffCosts,
       IIF(TeachingStaffCosts IS NULL OR TeachingStaffCosts <= 0.0, NULL, TeachingStaffCosts) AS TeachingStaffCosts,
       IIF(SupplyTeachingStaffCosts IS NULL OR SupplyTeachingStaffCosts <= 0.0, NULL, SupplyTeachingStaffCosts) AS SupplyTeachingStaffCosts,
       IIF(EducationalConsultancyCosts IS NULL OR EducationalConsultancyCosts <= 0.0, NULL, EducationalConsultancyCosts) AS EducationalConsultancyCosts,
       IIF(EducationSupportStaffCosts IS NULL OR EducationSupportStaffCosts <= 0.0, NULL, EducationSupportStaffCosts) AS EducationSupportStaffCosts,
       IIF(AgencySupplyTeachingStaffCosts IS NULL OR AgencySupplyTeachingStaffCosts <= 0.0, NULL, AgencySupplyTeachingStaffCosts) AS AgencySupplyTeachingStaffCosts,
       IIF(TotalNonEducationalSupportStaffCosts IS NULL OR TotalNonEducationalSupportStaffCosts <= 0.0, NULL, TotalNonEducationalSupportStaffCosts) AS TotalNonEducationalSupportStaffCosts,
       IIF(AdministrativeClericalStaffCosts IS NULL OR AdministrativeClericalStaffCosts <= 0.0, NULL, AdministrativeClericalStaffCosts) AS AdministrativeClericalStaffCosts,
       IIF(AuditorsCosts IS NULL OR AuditorsCosts <= 0.0, NULL, AuditorsCosts) AS AuditorsCosts,
       IIF(OtherStaffCosts IS NULL OR OtherStaffCosts <= 0.0, NULL, OtherStaffCosts) AS OtherStaffCosts,
       IIF(ProfessionalServicesNonCurriculumCosts IS NULL OR ProfessionalServicesNonCurriculumCosts <= 0.0, NULL, ProfessionalServicesNonCurriculumCosts) AS ProfessionalServicesNonCurriculumCosts,
       IIF(TotalEducationalSuppliesCosts IS NULL OR TotalEducationalSuppliesCosts <= 0.0, NULL, TotalEducationalSuppliesCosts) AS TotalEducationalSuppliesCosts,
       IIF(ExaminationFeesCosts IS NULL OR ExaminationFeesCosts <= 0.0, NULL, ExaminationFeesCosts) AS ExaminationFeesCosts,
       IIF(LearningResourcesNonIctCosts IS NULL OR LearningResourcesNonIctCosts <= 0.0, NULL, LearningResourcesNonIctCosts) AS LearningResourcesNonIctCosts,
       IIF(LearningResourcesIctCosts IS NULL OR LearningResourcesIctCosts <= 0.0, NULL, LearningResourcesIctCosts ) AS LearningResourcesIctCosts,
       IIF(TotalGrossCateringCosts IS NULL OR TotalGrossCateringCosts <= 0.0, NULL, TotalGrossCateringCosts) AS TotalGrossCateringCosts,
       IIF(TotalNetCateringCosts IS NULL OR TotalNetCateringCosts <= 0.0, NULL, TotalNetCateringCosts) AS TotalNetCateringCosts,
       IIF(CateringStaffCosts IS NULL OR CateringStaffCosts <= 0.0, NULL, CateringStaffCosts) AS CateringStaffCosts,
       IIF(CateringSuppliesCosts IS NULL OR CateringSuppliesCosts <= 0.0, NULL, CateringSuppliesCosts) AS CateringSuppliesCosts,
       IIF(TotalOtherCosts IS NULL OR TotalOtherCosts <= 0.0, NULL, TotalOtherCosts) AS TotalOtherCosts,
       IIF(DirectRevenueFinancingCosts IS NULL OR DirectRevenueFinancingCosts <= 0.0, NULL, DirectRevenueFinancingCosts) AS DirectRevenueFinancingCosts,
       IIF(GroundsMaintenanceCosts IS NULL OR GroundsMaintenanceCosts <= 0.0, NULL,GroundsMaintenanceCosts) AS GroundsMaintenanceCosts,
       IIF(IndirectEmployeeExpenses IS NULL OR IndirectEmployeeExpenses <= 0.0, NULL,IndirectEmployeeExpenses) AS IndirectEmployeeExpenses,
       IIF(InterestChargesLoanBank IS NULL OR InterestChargesLoanBank <= 0.0,NULL,InterestChargesLoanBank) AS InterestChargesLoanBank,
       IIF(OtherInsurancePremiumsCosts IS NULL OR OtherInsurancePremiumsCosts <= 0.0,NULL,OtherInsurancePremiumsCosts) AS OtherInsurancePremiumsCosts,
       IIF(PrivateFinanceInitiativeCharges IS NULL OR PrivateFinanceInitiativeCharges <= 0.0,NULL,PrivateFinanceInitiativeCharges) AS PrivateFinanceInitiativeCharges,
       IIF(RentRatesCosts IS NULL OR RentRatesCosts <= 0.0,NULL,RentRatesCosts) AS RentRatesCosts,
       IIF(SpecialFacilitiesCosts IS NULL OR SpecialFacilitiesCosts <= 0.0,NULL,SpecialFacilitiesCosts) AS SpecialFacilitiesCosts,
       IIF(StaffDevelopmentTrainingCosts IS NULL OR StaffDevelopmentTrainingCosts <= 0.0,NULL,StaffDevelopmentTrainingCosts) AS StaffDevelopmentTrainingCosts,
       IIF(StaffRelatedInsuranceCosts IS NULL OR StaffRelatedInsuranceCosts <= 0.0,NULL,StaffRelatedInsuranceCosts) AS StaffRelatedInsuranceCosts,
       IIF(SupplyTeacherInsurableCosts IS NULL OR SupplyTeacherInsurableCosts <= 0.0,NULL,SupplyTeacherInsurableCosts) AS SupplyTeacherInsurableCosts,
       IIF(CommunityFocusedSchoolStaff IS NULL OR CommunityFocusedSchoolStaff <= 0.0,NULL,CommunityFocusedSchoolStaff) AS CommunityFocusedSchoolStaff,
       IIF(CommunityFocusedSchoolCosts IS NULL OR CommunityFocusedSchoolCosts <= 0.0,NULL,CommunityFocusedSchoolCosts) AS CommunityFocusedSchoolCosts,
       IIF(AdministrativeSuppliesNonEducationalCosts IS NULL OR AdministrativeSuppliesNonEducationalCosts <= 0.0,NULL,AdministrativeSuppliesNonEducationalCosts) AS AdministrativeSuppliesNonEducationalCosts,
       IIF(TotalPremisesStaffServiceCosts IS NULL OR TotalPremisesStaffServiceCosts <= 0.0,NULL,TotalPremisesStaffServiceCosts) AS TotalPremisesStaffServiceCosts,
       IIF(CleaningCaretakingCosts IS NULL OR CleaningCaretakingCosts <= 0.0,NULL,CleaningCaretakingCosts) AS CleaningCaretakingCosts,
       IIF(MaintenancePremisesCosts IS NULL OR MaintenancePremisesCosts <= 0.0,NULL,MaintenancePremisesCosts) AS MaintenancePremisesCosts,
       IIF(OtherOccupationCosts IS NULL OR OtherOccupationCosts <= 0.0,NULL,OtherOccupationCosts) AS OtherOccupationCosts,
       IIF(PremisesStaffCosts IS NULL OR PremisesStaffCosts <= 0.0,NULL,PremisesStaffCosts) AS PremisesStaffCosts,
       IIF(TotalUtilitiesCosts IS NULL OR TotalUtilitiesCosts <= 0.0,NULL,TotalUtilitiesCosts) AS TotalUtilitiesCosts,
       IIF(EnergyCosts IS NULL OR EnergyCosts <= 0.0,NULL,EnergyCosts) AS EnergyCosts,
       IIF(WaterSewerageCosts IS NULL OR WaterSewerageCosts <= 0.0,NULL,WaterSewerageCosts) AS WaterSewerageCosts
FROM VW_ExpenditureSchoolPerUnit
WHERE RunType = 'default'
GO

DROP VIEW IF EXISTS VW_ExpenditureSchoolDefaultActual
GO

CREATE VIEW VW_ExpenditureSchoolDefaultActual AS
SELECT *
FROM VW_ExpenditureSchoolActual
WHERE RunType = 'default'
GO

DROP VIEW IF EXISTS VW_ExpenditureSchoolDefaultPercentExpenditure
GO

CREATE VIEW VW_ExpenditureSchoolDefaultPercentExpenditure AS
SELECT *
FROM VW_ExpenditureSchoolPercentExpenditure
WHERE RunType = 'default'
GO

DROP VIEW IF EXISTS VW_ExpenditureSchoolDefaultPercentIncome
GO

CREATE VIEW VW_ExpenditureSchoolDefaultPercentIncome AS
SELECT *
FROM VW_ExpenditureSchoolPercentIncome
WHERE RunType = 'default'
GO

DROP VIEW IF EXISTS VW_ExpenditureSchoolDefaultPerUnit
GO

CREATE VIEW VW_ExpenditureSchoolDefaultPerUnit AS
SELECT *
FROM VW_ExpenditureSchoolPerUnit
WHERE RunType = 'default'
GO

DROP VIEW IF EXISTS VW_ExpenditureSchoolDefaultCurrentActual
GO

CREATE VIEW VW_ExpenditureSchoolDefaultCurrentActual AS
SELECT c.*,
       s.SchoolName,
       s.SchoolType,
       s.LAName,
       s.TrustCompanyNumber,
       s.LaCode
FROM School s
         LEFT JOIN VW_ExpenditureSchoolDefaultActual c ON c.URN = s.URN
WHERE c.RunId = (SELECT Value FROM Parameters WHERE Name = 'CurrentYear')
GO

DROP VIEW IF EXISTS VW_ExpenditureSchoolDefaultCurrentPercentExpenditure
GO

CREATE VIEW VW_ExpenditureSchoolDefaultCurrentPercentExpenditure AS
SELECT c.*,
       s.SchoolName,
       s.SchoolType,
       s.LAName,
       s.TrustCompanyNumber,
       s.LaCode
FROM School s
         LEFT JOIN VW_ExpenditureSchoolDefaultPercentExpenditure c ON c.URN = s.URN
WHERE c.RunId = (SELECT Value FROM Parameters WHERE Name = 'CurrentYear')
GO

DROP VIEW IF EXISTS VW_ExpenditureSchoolDefaultCurrentPercentIncome
GO

CREATE VIEW VW_ExpenditureSchoolDefaultCurrentPercentIncome AS
SELECT c.*,
       s.SchoolName,
       s.SchoolType,
       s.LAName,
       s.TrustCompanyNumber,
       s.LaCode
FROM School s
         LEFT JOIN VW_ExpenditureSchoolDefaultPercentIncome c ON c.URN = s.URN
WHERE c.RunId = (SELECT Value FROM Parameters WHERE Name = 'CurrentYear')
GO

DROP VIEW IF EXISTS VW_ExpenditureSchoolDefaultCurrentPerUnit
GO

CREATE VIEW VW_ExpenditureSchoolDefaultCurrentPerUnit AS
SELECT c.*,
       s.SchoolName,
       s.SchoolType,
       s.LAName,
       s.TrustCompanyNumber,
       s.LaCode
FROM School s
         LEFT JOIN VW_ExpenditureSchoolDefaultPerUnit c ON c.URN = s.URN
WHERE c.RunId = (SELECT Value FROM Parameters WHERE Name = 'CurrentYear')
GO

DROP VIEW IF EXISTS VW_ExpenditureSchoolCustomActual
GO

CREATE VIEW VW_ExpenditureSchoolCustomActual AS
SELECT c.*,
       s.SchoolName,
       s.SchoolType,
       s.LAName
FROM School s
         LEFT JOIN VW_ExpenditureSchoolActual c ON c.URN = s.URN
WHERE c.RunType = 'custom'
GO

DROP VIEW IF EXISTS VW_ExpenditureSchoolCustomPercentExpenditure
GO

CREATE VIEW VW_ExpenditureSchoolCustomPercentExpenditure AS
SELECT c.*,
       s.SchoolName,
       s.SchoolType,
       s.LAName
FROM School s
         LEFT JOIN VW_ExpenditureSchoolPercentExpenditure c ON c.URN = s.URN
WHERE c.RunType = 'custom'
GO

DROP VIEW IF EXISTS VW_ExpenditureSchoolCustomPercentIncome
GO

CREATE VIEW VW_ExpenditureSchoolCustomPercentIncome AS
SELECT c.*,
       s.SchoolName,
       s.SchoolType,
       s.LAName
FROM School s
         LEFT JOIN VW_ExpenditureSchoolPercentIncome c ON c.URN = s.URN
WHERE c.RunType = 'custom'
GO

DROP VIEW IF EXISTS VW_ExpenditureSchoolCustomPerUnit
GO

CREATE VIEW VW_ExpenditureSchoolCustomPerUnit AS
SELECT c.*,
       s.SchoolName,
       s.SchoolType,
       s.LAName
FROM School s
         LEFT JOIN VW_ExpenditureSchoolPerUnit c ON c.URN = s.URN
WHERE c.RunType = 'custom'
GO