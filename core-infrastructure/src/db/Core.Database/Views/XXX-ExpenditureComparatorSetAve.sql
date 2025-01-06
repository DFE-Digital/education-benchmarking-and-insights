DROP VIEW IF EXISTS VW_SchoolExpenditureComparatorSetAvgActual
GO

CREATE VIEW VW_SchoolExpenditureComparatorSetAvgActual AS
WITH pupilSet AS (SELECT RunId
                       , URN
                       , Comparator.value AS ComparatorURN
                  FROM ComparatorSet
                           CROSS APPLY Openjson(Pupil) Comparator
                  WHERE RunType = 'default'),
     buildingSet AS (SELECT RunId
                          , URN
                          , Comparator.value AS ComparatorURN
                     FROM ComparatorSet
                              CROSS APPLY Openjson(Building) Comparator
                     WHERE RunType = 'default'),
     pupilAvg AS (SELECT s.URN
                       , s.RunId
                       , Avg(f.TotalExpenditure)                          AS TotalExpenditure
                       , Avg(f.TotalTeachingSupportStaffCosts)            AS TotalTeachingSupportStaffCosts
                       , Avg(f.TeachingStaffCosts)                        AS TeachingStaffCosts
                       , Avg(f.SupplyTeachingStaffCosts)                  AS SupplyTeachingStaffCosts
                       , Avg(f.EducationalConsultancyCosts)               AS EducationalConsultancyCosts
                       , Avg(f.EducationSupportStaffCosts)                AS EducationSupportStaffCosts
                       , Avg(f.AgencySupplyTeachingStaffCosts)            AS AgencySupplyTeachingStaffCosts
                       , Avg(f.TotalNonEducationalSupportStaffCosts)      AS TotalNonEducationalSupportStaffCosts
                       , Avg(f.AdministrativeClericalStaffCosts)          AS AdministrativeClericalStaffCosts
                       , Avg(f.AuditorsCosts)                             AS AuditorsCosts
                       , Avg(f.OtherStaffCosts)                           AS OtherStaffCosts
                       , Avg(f.ProfessionalServicesNonCurriculumCosts)    AS ProfessionalServicesNonCurriculumCosts
                       , Avg(f.TotalEducationalSuppliesCosts)             AS TotalEducationalSuppliesCosts
                       , Avg(f.ExaminationFeesCosts)                      AS ExaminationFeesCosts
                       , Avg(f.LearningResourcesNonIctCosts)              AS LearningResourcesNonIctCosts
                       , Avg(f.LearningResourcesIctCosts)                 AS LearningResourcesIctCosts
                       , Avg(f.TotalGrossCateringCosts)                   AS TotalGrossCateringCosts
                       , Avg(f.TotalNetCateringCosts)                     AS TotalNetCateringCosts
                       , Avg(f.CateringStaffCosts)                        AS CateringStaffCosts
                       , Avg(f.CateringSuppliesCosts)                     AS CateringSuppliesCosts
                       , Avg(f.TotalOtherCosts)                           AS TotalOtherCosts
                       , Avg(f.DirectRevenueFinancingCosts)               AS DirectRevenueFinancingCosts
                       , Avg(f.GroundsMaintenanceCosts)                   AS GroundsMaintenanceCosts
                       , Avg(f.IndirectEmployeeExpenses)                  AS IndirectEmployeeExpenses
                       , Avg(f.InterestChargesLoanBank)                   AS InterestChargesLoanBank
                       , Avg(f.OtherInsurancePremiumsCosts)               AS OtherInsurancePremiumsCosts
                       , Avg(f.PrivateFinanceInitiativeCharges)           AS PrivateFinanceInitiativeCharges
                       , Avg(f.RentRatesCosts)                            AS RentRatesCosts
                       , Avg(f.SpecialFacilitiesCosts)                    AS SpecialFacilitiesCosts
                       , Avg(f.StaffDevelopmentTrainingCosts)             AS StaffDevelopmentTrainingCosts
                       , Avg(f.StaffRelatedInsuranceCosts)                AS StaffRelatedInsuranceCosts
                       , Avg(f.SupplyTeacherInsurableCosts)               AS SupplyTeacherInsurableCosts
                       , Avg(f.CommunityFocusedSchoolStaff)               AS CommunityFocusedSchoolStaff
                       , Avg(f.CommunityFocusedSchoolCosts)               AS CommunityFocusedSchoolCosts
                       , Avg(f.AdministrativeSuppliesNonEducationalCosts) AS AdministrativeSuppliesNonEducationalCosts
                  FROM pupilSet s
                           INNER JOIN VW_FinancialDefaultNormalisedActual f
                                      ON (
                                          s.ComparatorURN = f.URN
                                              AND s.RunId = f.RunId
                                          )
                  GROUP BY s.URN
                         , s.RunId),
     buildingAvg AS (SELECT s.URN
                          , s.RunId
                          , Avg(f.TotalPremisesStaffServiceCosts) AS TotalPremisesStaffServiceCosts
                          , Avg(f.CleaningCaretakingCosts)        AS CleaningCaretakingCosts
                          , Avg(f.MaintenancePremisesCosts)       AS MaintenancePremisesCosts
                          , Avg(f.OtherOccupationCosts)           AS OtherOccupationCosts
                          , Avg(f.PremisesStaffCosts)             AS PremisesStaffCosts
                          , Avg(f.TotalUtilitiesCosts)            AS TotalUtilitiesCosts
                          , Avg(f.EnergyCosts)                    AS EnergyCosts
                          , Avg(f.WaterSewerageCosts)             AS WaterSewerageCosts
                     FROM buildingSet s
                              INNER JOIN VW_FinancialDefaultNormalisedActual f
                                         ON (
                                             s.ComparatorURN = f.URN
                                                 AND s.RunId = f.RunId
                                             )
                     GROUP BY s.URN
                            , s.RunId)
SELECT p.URN
     , p.RunId
     , TotalExpenditure
     , TotalTeachingSupportStaffCosts
     , TeachingStaffCosts
     , SupplyTeachingStaffCosts
     , EducationalConsultancyCosts
     , EducationSupportStaffCosts
     , AgencySupplyTeachingStaffCosts
     , TotalNonEducationalSupportStaffCosts
     , AdministrativeClericalStaffCosts
     , AuditorsCosts
     , OtherStaffCosts
     , ProfessionalServicesNonCurriculumCosts
     , TotalEducationalSuppliesCosts
     , ExaminationFeesCosts
     , LearningResourcesNonIctCosts
     , LearningResourcesIctCosts
     , TotalGrossCateringCosts
     , TotalNetCateringCosts
     , CateringStaffCosts
     , CateringSuppliesCosts
     , TotalOtherCosts
     , DirectRevenueFinancingCosts
     , GroundsMaintenanceCosts
     , IndirectEmployeeExpenses
     , InterestChargesLoanBank
     , OtherInsurancePremiumsCosts
     , PrivateFinanceInitiativeCharges
     , RentRatesCosts
     , SpecialFacilitiesCosts
     , StaffDevelopmentTrainingCosts
     , StaffRelatedInsuranceCosts
     , SupplyTeacherInsurableCosts
     , CommunityFocusedSchoolStaff
     , CommunityFocusedSchoolCosts
     , AdministrativeSuppliesNonEducationalCosts
     , TotalPremisesStaffServiceCosts
     , CleaningCaretakingCosts
     , MaintenancePremisesCosts
     , OtherOccupationCosts
     , PremisesStaffCosts
     , TotalUtilitiesCosts
     , EnergyCosts
     , WaterSewerageCosts
FROM pupilAvg p
         FULL JOIN buildingAvg b
                   ON (
                       p.URN = b.URN
                           AND p.RunId = b.RunId
                       )
GO

DROP VIEW IF EXISTS VW_SchoolExpenditureComparatorSetAvgPercentExpenditure
GO

CREATE VIEW VW_SchoolExpenditureComparatorSetAvgPercentExpenditure AS
WITH pupilSet AS (SELECT RunId
                       , URN
                       , Comparator.value AS ComparatorURN
                  FROM ComparatorSet
                           CROSS APPLY Openjson(Pupil) Comparator
                  WHERE RunType = 'default'),
     buildingSet AS (SELECT RunId
                          , URN
                          , Comparator.value AS ComparatorURN
                     FROM ComparatorSet
                              CROSS APPLY Openjson(Building) Comparator
                     WHERE RunType = 'default'),
     pupilAvg AS (SELECT s.URN
                       , s.RunId
                       , Avg(f.TotalExpenditure)                          AS TotalExpenditure
                       , Avg(f.TotalTeachingSupportStaffCosts)            AS TotalTeachingSupportStaffCosts
                       , Avg(f.TeachingStaffCosts)                        AS TeachingStaffCosts
                       , Avg(f.SupplyTeachingStaffCosts)                  AS SupplyTeachingStaffCosts
                       , Avg(f.EducationalConsultancyCosts)               AS EducationalConsultancyCosts
                       , Avg(f.EducationSupportStaffCosts)                AS EducationSupportStaffCosts
                       , Avg(f.AgencySupplyTeachingStaffCosts)            AS AgencySupplyTeachingStaffCosts
                       , Avg(f.TotalNonEducationalSupportStaffCosts)      AS TotalNonEducationalSupportStaffCosts
                       , Avg(f.AdministrativeClericalStaffCosts)          AS AdministrativeClericalStaffCosts
                       , Avg(f.AuditorsCosts)                             AS AuditorsCosts
                       , Avg(f.OtherStaffCosts)                           AS OtherStaffCosts
                       , Avg(f.ProfessionalServicesNonCurriculumCosts)    AS ProfessionalServicesNonCurriculumCosts
                       , Avg(f.TotalEducationalSuppliesCosts)             AS TotalEducationalSuppliesCosts
                       , Avg(f.ExaminationFeesCosts)                      AS ExaminationFeesCosts
                       , Avg(f.LearningResourcesNonIctCosts)              AS LearningResourcesNonIctCosts
                       , Avg(f.LearningResourcesIctCosts)                 AS LearningResourcesIctCosts
                       , Avg(f.TotalGrossCateringCosts)                   AS TotalGrossCateringCosts
                       , Avg(f.TotalNetCateringCosts)                     AS TotalNetCateringCosts
                       , Avg(f.CateringStaffCosts)                        AS CateringStaffCosts
                       , Avg(f.CateringSuppliesCosts)                     AS CateringSuppliesCosts
                       , Avg(f.TotalOtherCosts)                           AS TotalOtherCosts
                       , Avg(f.DirectRevenueFinancingCosts)               AS DirectRevenueFinancingCosts
                       , Avg(f.GroundsMaintenanceCosts)                   AS GroundsMaintenanceCosts
                       , Avg(f.IndirectEmployeeExpenses)                  AS IndirectEmployeeExpenses
                       , Avg(f.InterestChargesLoanBank)                   AS InterestChargesLoanBank
                       , Avg(f.OtherInsurancePremiumsCosts)               AS OtherInsurancePremiumsCosts
                       , Avg(f.PrivateFinanceInitiativeCharges)           AS PrivateFinanceInitiativeCharges
                       , Avg(f.RentRatesCosts)                            AS RentRatesCosts
                       , Avg(f.SpecialFacilitiesCosts)                    AS SpecialFacilitiesCosts
                       , Avg(f.StaffDevelopmentTrainingCosts)             AS StaffDevelopmentTrainingCosts
                       , Avg(f.StaffRelatedInsuranceCosts)                AS StaffRelatedInsuranceCosts
                       , Avg(f.SupplyTeacherInsurableCosts)               AS SupplyTeacherInsurableCosts
                       , Avg(f.CommunityFocusedSchoolStaff)               AS CommunityFocusedSchoolStaff
                       , Avg(f.CommunityFocusedSchoolCosts)               AS CommunityFocusedSchoolCosts
                       , Avg(f.AdministrativeSuppliesNonEducationalCosts) AS AdministrativeSuppliesNonEducationalCosts
                  FROM pupilSet s
                           INNER JOIN VW_FinancialDefaultNormalisedPercentExpenditure f
                                      ON (
                                          s.ComparatorURN = f.URN
                                              AND s.RunId = f.RunId
                                          )
                  GROUP BY s.URN
                         , s.RunId),
     buildingAvg AS (SELECT s.URN
                          , s.RunId
                          , Avg(f.TotalPremisesStaffServiceCosts) AS TotalPremisesStaffServiceCosts
                          , Avg(f.CleaningCaretakingCosts)        AS CleaningCaretakingCosts
                          , Avg(f.MaintenancePremisesCosts)       AS MaintenancePremisesCosts
                          , Avg(f.OtherOccupationCosts)           AS OtherOccupationCosts
                          , Avg(f.PremisesStaffCosts)             AS PremisesStaffCosts
                          , Avg(f.TotalUtilitiesCosts)            AS TotalUtilitiesCosts
                          , Avg(f.EnergyCosts)                    AS EnergyCosts
                          , Avg(f.WaterSewerageCosts)             AS WaterSewerageCosts
                     FROM buildingSet s
                              INNER JOIN VW_FinancialDefaultNormalisedPercentExpenditure f
                                         ON (
                                             s.ComparatorURN = f.URN
                                                 AND s.RunId = f.RunId
                                             )
                     GROUP BY s.URN
                            , s.RunId)
SELECT p.URN
     , p.RunId
     , TotalExpenditure
     , TotalTeachingSupportStaffCosts
     , TeachingStaffCosts
     , SupplyTeachingStaffCosts
     , EducationalConsultancyCosts
     , EducationSupportStaffCosts
     , AgencySupplyTeachingStaffCosts
     , TotalNonEducationalSupportStaffCosts
     , AdministrativeClericalStaffCosts
     , AuditorsCosts
     , OtherStaffCosts
     , ProfessionalServicesNonCurriculumCosts
     , TotalEducationalSuppliesCosts
     , ExaminationFeesCosts
     , LearningResourcesNonIctCosts
     , LearningResourcesIctCosts
     , TotalGrossCateringCosts
     , TotalNetCateringCosts
     , CateringStaffCosts
     , CateringSuppliesCosts
     , TotalOtherCosts
     , DirectRevenueFinancingCosts
     , GroundsMaintenanceCosts
     , IndirectEmployeeExpenses
     , InterestChargesLoanBank
     , OtherInsurancePremiumsCosts
     , PrivateFinanceInitiativeCharges
     , RentRatesCosts
     , SpecialFacilitiesCosts
     , StaffDevelopmentTrainingCosts
     , StaffRelatedInsuranceCosts
     , SupplyTeacherInsurableCosts
     , CommunityFocusedSchoolStaff
     , CommunityFocusedSchoolCosts
     , AdministrativeSuppliesNonEducationalCosts
     , TotalPremisesStaffServiceCosts
     , CleaningCaretakingCosts
     , MaintenancePremisesCosts
     , OtherOccupationCosts
     , PremisesStaffCosts
     , TotalUtilitiesCosts
     , EnergyCosts
     , WaterSewerageCosts
FROM pupilAvg p
         FULL JOIN buildingAvg b
                   ON (
                       p.URN = b.URN
                           AND p.RunId = b.RunId
                       )
GO

DROP VIEW IF EXISTS VW_SchoolExpenditureComparatorSetAvgPercentIncome
GO

CREATE VIEW VW_SchoolExpenditureComparatorSetAvgPercentIncome AS
WITH pupilSet AS (SELECT RunId
                       , URN
                       , Comparator.value AS ComparatorURN
                  FROM ComparatorSet
                           CROSS APPLY Openjson(Pupil) Comparator
                  WHERE RunType = 'default'),
     buildingSet AS (SELECT RunId
                          , URN
                          , Comparator.value AS ComparatorURN
                     FROM ComparatorSet
                              CROSS APPLY Openjson(Building) Comparator
                     WHERE RunType = 'default'),
     pupilAvg AS (SELECT s.URN
                       , s.RunId
                       , Avg(f.TotalExpenditure)                          AS TotalExpenditure
                       , Avg(f.TotalTeachingSupportStaffCosts)            AS TotalTeachingSupportStaffCosts
                       , Avg(f.TeachingStaffCosts)                        AS TeachingStaffCosts
                       , Avg(f.SupplyTeachingStaffCosts)                  AS SupplyTeachingStaffCosts
                       , Avg(f.EducationalConsultancyCosts)               AS EducationalConsultancyCosts
                       , Avg(f.EducationSupportStaffCosts)                AS EducationSupportStaffCosts
                       , Avg(f.AgencySupplyTeachingStaffCosts)            AS AgencySupplyTeachingStaffCosts
                       , Avg(f.TotalNonEducationalSupportStaffCosts)      AS TotalNonEducationalSupportStaffCosts
                       , Avg(f.AdministrativeClericalStaffCosts)          AS AdministrativeClericalStaffCosts
                       , Avg(f.AuditorsCosts)                             AS AuditorsCosts
                       , Avg(f.OtherStaffCosts)                           AS OtherStaffCosts
                       , Avg(f.ProfessionalServicesNonCurriculumCosts)    AS ProfessionalServicesNonCurriculumCosts
                       , Avg(f.TotalEducationalSuppliesCosts)             AS TotalEducationalSuppliesCosts
                       , Avg(f.ExaminationFeesCosts)                      AS ExaminationFeesCosts
                       , Avg(f.LearningResourcesNonIctCosts)              AS LearningResourcesNonIctCosts
                       , Avg(f.LearningResourcesIctCosts)                 AS LearningResourcesIctCosts
                       , Avg(f.TotalGrossCateringCosts)                   AS TotalGrossCateringCosts
                       , Avg(f.TotalNetCateringCosts)                     AS TotalNetCateringCosts
                       , Avg(f.CateringStaffCosts)                        AS CateringStaffCosts
                       , Avg(f.CateringSuppliesCosts)                     AS CateringSuppliesCosts
                       , Avg(f.TotalOtherCosts)                           AS TotalOtherCosts
                       , Avg(f.DirectRevenueFinancingCosts)               AS DirectRevenueFinancingCosts
                       , Avg(f.GroundsMaintenanceCosts)                   AS GroundsMaintenanceCosts
                       , Avg(f.IndirectEmployeeExpenses)                  AS IndirectEmployeeExpenses
                       , Avg(f.InterestChargesLoanBank)                   AS InterestChargesLoanBank
                       , Avg(f.OtherInsurancePremiumsCosts)               AS OtherInsurancePremiumsCosts
                       , Avg(f.PrivateFinanceInitiativeCharges)           AS PrivateFinanceInitiativeCharges
                       , Avg(f.RentRatesCosts)                            AS RentRatesCosts
                       , Avg(f.SpecialFacilitiesCosts)                    AS SpecialFacilitiesCosts
                       , Avg(f.StaffDevelopmentTrainingCosts)             AS StaffDevelopmentTrainingCosts
                       , Avg(f.StaffRelatedInsuranceCosts)                AS StaffRelatedInsuranceCosts
                       , Avg(f.SupplyTeacherInsurableCosts)               AS SupplyTeacherInsurableCosts
                       , Avg(f.CommunityFocusedSchoolStaff)               AS CommunityFocusedSchoolStaff
                       , Avg(f.CommunityFocusedSchoolCosts)               AS CommunityFocusedSchoolCosts
                       , Avg(f.AdministrativeSuppliesNonEducationalCosts) AS AdministrativeSuppliesNonEducationalCosts
                  FROM pupilSet s
                           INNER JOIN VW_FinancialDefaultNormalisedPercentIncome f
                                      ON (
                                          s.ComparatorURN = f.URN
                                              AND s.RunId = f.RunId
                                          )
                  GROUP BY s.URN
                         , s.RunId),
     buildingAvg AS (SELECT s.URN
                          , s.RunId
                          , Avg(f.TotalPremisesStaffServiceCosts) AS TotalPremisesStaffServiceCosts
                          , Avg(f.CleaningCaretakingCosts)        AS CleaningCaretakingCosts
                          , Avg(f.MaintenancePremisesCosts)       AS MaintenancePremisesCosts
                          , Avg(f.OtherOccupationCosts)           AS OtherOccupationCosts
                          , Avg(f.PremisesStaffCosts)             AS PremisesStaffCosts
                          , Avg(f.TotalUtilitiesCosts)            AS TotalUtilitiesCosts
                          , Avg(f.EnergyCosts)                    AS EnergyCosts
                          , Avg(f.WaterSewerageCosts)             AS WaterSewerageCosts
                     FROM buildingSet s
                              INNER JOIN VW_FinancialDefaultNormalisedPercentIncome f
                                         ON (
                                             s.ComparatorURN = f.URN
                                                 AND s.RunId = f.RunId
                                             )
                     GROUP BY s.URN
                            , s.RunId)
SELECT p.URN
     , p.RunId
     , TotalExpenditure
     , TotalTeachingSupportStaffCosts
     , TeachingStaffCosts
     , SupplyTeachingStaffCosts
     , EducationalConsultancyCosts
     , EducationSupportStaffCosts
     , AgencySupplyTeachingStaffCosts
     , TotalNonEducationalSupportStaffCosts
     , AdministrativeClericalStaffCosts
     , AuditorsCosts
     , OtherStaffCosts
     , ProfessionalServicesNonCurriculumCosts
     , TotalEducationalSuppliesCosts
     , ExaminationFeesCosts
     , LearningResourcesNonIctCosts
     , LearningResourcesIctCosts
     , TotalGrossCateringCosts
     , TotalNetCateringCosts
     , CateringStaffCosts
     , CateringSuppliesCosts
     , TotalOtherCosts
     , DirectRevenueFinancingCosts
     , GroundsMaintenanceCosts
     , IndirectEmployeeExpenses
     , InterestChargesLoanBank
     , OtherInsurancePremiumsCosts
     , PrivateFinanceInitiativeCharges
     , RentRatesCosts
     , SpecialFacilitiesCosts
     , StaffDevelopmentTrainingCosts
     , StaffRelatedInsuranceCosts
     , SupplyTeacherInsurableCosts
     , CommunityFocusedSchoolStaff
     , CommunityFocusedSchoolCosts
     , AdministrativeSuppliesNonEducationalCosts
     , TotalPremisesStaffServiceCosts
     , CleaningCaretakingCosts
     , MaintenancePremisesCosts
     , OtherOccupationCosts
     , PremisesStaffCosts
     , TotalUtilitiesCosts
     , EnergyCosts
     , WaterSewerageCosts
FROM pupilAvg p
         FULL JOIN buildingAvg b
                   ON (
                       p.URN = b.URN
                           AND p.RunId = b.RunId
                       )
GO

DROP VIEW IF EXISTS VW_SchoolExpenditureComparatorSetAvgPerUnit
GO

CREATE VIEW VW_SchoolExpenditureComparatorSetAvgPerUnit AS
WITH pupilSet AS (SELECT RunId
                       , URN
                       , Comparator.value AS ComparatorURN
                  FROM ComparatorSet
                           CROSS APPLY Openjson(Pupil) Comparator
                  WHERE RunType = 'default'),
     buildingSet AS (SELECT RunId
                          , URN
                          , Comparator.value AS ComparatorURN
                     FROM ComparatorSet
                              CROSS APPLY Openjson(Building) Comparator
                     WHERE RunType = 'default'),
     pupilAvg AS (SELECT s.URN
                       , s.RunId
                       , Avg(f.TotalExpenditure)                          AS TotalExpenditure
                       , Avg(f.TotalTeachingSupportStaffCosts)            AS TotalTeachingSupportStaffCosts
                       , Avg(f.TeachingStaffCosts)                        AS TeachingStaffCosts
                       , Avg(f.SupplyTeachingStaffCosts)                  AS SupplyTeachingStaffCosts
                       , Avg(f.EducationalConsultancyCosts)               AS EducationalConsultancyCosts
                       , Avg(f.EducationSupportStaffCosts)                AS EducationSupportStaffCosts
                       , Avg(f.AgencySupplyTeachingStaffCosts)            AS AgencySupplyTeachingStaffCosts
                       , Avg(f.TotalNonEducationalSupportStaffCosts)      AS TotalNonEducationalSupportStaffCosts
                       , Avg(f.AdministrativeClericalStaffCosts)          AS AdministrativeClericalStaffCosts
                       , Avg(f.AuditorsCosts)                             AS AuditorsCosts
                       , Avg(f.OtherStaffCosts)                           AS OtherStaffCosts
                       , Avg(f.ProfessionalServicesNonCurriculumCosts)    AS ProfessionalServicesNonCurriculumCosts
                       , Avg(f.TotalEducationalSuppliesCosts)             AS TotalEducationalSuppliesCosts
                       , Avg(f.ExaminationFeesCosts)                      AS ExaminationFeesCosts
                       , Avg(f.LearningResourcesNonIctCosts)              AS LearningResourcesNonIctCosts
                       , Avg(f.LearningResourcesIctCosts)                 AS LearningResourcesIctCosts
                       , Avg(f.TotalGrossCateringCosts)                   AS TotalGrossCateringCosts
                       , Avg(f.TotalNetCateringCosts)                     AS TotalNetCateringCosts
                       , Avg(f.CateringStaffCosts)                        AS CateringStaffCosts
                       , Avg(f.CateringSuppliesCosts)                     AS CateringSuppliesCosts
                       , Avg(f.TotalOtherCosts)                           AS TotalOtherCosts
                       , Avg(f.DirectRevenueFinancingCosts)               AS DirectRevenueFinancingCosts
                       , Avg(f.GroundsMaintenanceCosts)                   AS GroundsMaintenanceCosts
                       , Avg(f.IndirectEmployeeExpenses)                  AS IndirectEmployeeExpenses
                       , Avg(f.InterestChargesLoanBank)                   AS InterestChargesLoanBank
                       , Avg(f.OtherInsurancePremiumsCosts)               AS OtherInsurancePremiumsCosts
                       , Avg(f.PrivateFinanceInitiativeCharges)           AS PrivateFinanceInitiativeCharges
                       , Avg(f.RentRatesCosts)                            AS RentRatesCosts
                       , Avg(f.SpecialFacilitiesCosts)                    AS SpecialFacilitiesCosts
                       , Avg(f.StaffDevelopmentTrainingCosts)             AS StaffDevelopmentTrainingCosts
                       , Avg(f.StaffRelatedInsuranceCosts)                AS StaffRelatedInsuranceCosts
                       , Avg(f.SupplyTeacherInsurableCosts)               AS SupplyTeacherInsurableCosts
                       , Avg(f.CommunityFocusedSchoolStaff)               AS CommunityFocusedSchoolStaff
                       , Avg(f.CommunityFocusedSchoolCosts)               AS CommunityFocusedSchoolCosts
                       , Avg(f.AdministrativeSuppliesNonEducationalCosts) AS AdministrativeSuppliesNonEducationalCosts
                  FROM pupilSet s
                           INNER JOIN VW_FinancialDefaultNormalisedPerUnit f
                                      ON (
                                          s.ComparatorURN = f.URN
                                              AND s.RunId = f.RunId
                                          )
                  GROUP BY s.URN
                         , s.RunId),
     buildingAvg AS (SELECT s.URN
                          , s.RunId
                          , Avg(f.TotalPremisesStaffServiceCosts) AS TotalPremisesStaffServiceCosts
                          , Avg(f.CleaningCaretakingCosts)        AS CleaningCaretakingCosts
                          , Avg(f.MaintenancePremisesCosts)       AS MaintenancePremisesCosts
                          , Avg(f.OtherOccupationCosts)           AS OtherOccupationCosts
                          , Avg(f.PremisesStaffCosts)             AS PremisesStaffCosts
                          , Avg(f.TotalUtilitiesCosts)            AS TotalUtilitiesCosts
                          , Avg(f.EnergyCosts)                    AS EnergyCosts
                          , Avg(f.WaterSewerageCosts)             AS WaterSewerageCosts
                     FROM buildingSet s
                              INNER JOIN VW_FinancialDefaultNormalisedPerUnit f
                                         ON (
                                             s.ComparatorURN = f.URN
                                                 AND s.RunId = f.RunId
                                             )
                     GROUP BY s.URN
                            , s.RunId)
SELECT p.URN
     , p.RunId
     , TotalExpenditure
     , TotalTeachingSupportStaffCosts
     , TeachingStaffCosts
     , SupplyTeachingStaffCosts
     , EducationalConsultancyCosts
     , EducationSupportStaffCosts
     , AgencySupplyTeachingStaffCosts
     , TotalNonEducationalSupportStaffCosts
     , AdministrativeClericalStaffCosts
     , AuditorsCosts
     , OtherStaffCosts
     , ProfessionalServicesNonCurriculumCosts
     , TotalEducationalSuppliesCosts
     , ExaminationFeesCosts
     , LearningResourcesNonIctCosts
     , LearningResourcesIctCosts
     , TotalGrossCateringCosts
     , TotalNetCateringCosts
     , CateringStaffCosts
     , CateringSuppliesCosts
     , TotalOtherCosts
     , DirectRevenueFinancingCosts
     , GroundsMaintenanceCosts
     , IndirectEmployeeExpenses
     , InterestChargesLoanBank
     , OtherInsurancePremiumsCosts
     , PrivateFinanceInitiativeCharges
     , RentRatesCosts
     , SpecialFacilitiesCosts
     , StaffDevelopmentTrainingCosts
     , StaffRelatedInsuranceCosts
     , SupplyTeacherInsurableCosts
     , CommunityFocusedSchoolStaff
     , CommunityFocusedSchoolCosts
     , AdministrativeSuppliesNonEducationalCosts
     , TotalPremisesStaffServiceCosts
     , CleaningCaretakingCosts
     , MaintenancePremisesCosts
     , OtherOccupationCosts
     , PremisesStaffCosts
     , TotalUtilitiesCosts
     , EnergyCosts
     , WaterSewerageCosts
FROM pupilAvg p
         FULL JOIN buildingAvg b
                   ON (
                       p.URN = b.URN
                           AND p.RunId = b.RunId
                       )
GO