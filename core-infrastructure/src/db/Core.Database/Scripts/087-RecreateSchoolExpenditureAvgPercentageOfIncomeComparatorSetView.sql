DROP VIEW IF EXISTS SchoolExpenditureAvgPercentageOfIncomeComparatorSet
GO

CREATE VIEW SchoolExpenditureAvgPercentageOfIncomeComparatorSet AS
  WITH pupilComparator AS (
    SELECT RunId
         , URN
         , Comparator.value AS PupilComparatorURN
      FROM ComparatorSet
     CROSS APPLY Openjson(Pupil) Comparator
     WHERE RunType = 'default'
  ), buildingComparator AS (
    SELECT RunId
         , URN
         , Comparator.value AS BuildingComparatorURN
      FROM ComparatorSet
     CROSS APPLY Openjson(Building) Comparator
     WHERE RunType = 'default'
  ), pupilExpenditureAvgOfPercentageOfIncomeComparatorSet AS (
    SELECT pupilComparator.URN
         , pupilComparator.RunId
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
      FROM pupilComparator
     INNER
      JOIN SchoolExpenditurePercentageOfIncomeHistoric
        ON (
                 pupilComparator.PupilComparatorURN = SchoolExpenditurePercentageOfIncomeHistoric.URN
             AND pupilComparator.RunId = SchoolExpenditurePercentageOfIncomeHistoric.Year
           )
     GROUP
        BY pupilComparator.URN
         , pupilComparator.RunId
   ), buildingExpenditureAvgOfPercentageOfIncomeComparatorSet AS (
    SELECT buildingComparator.URN
         , buildingComparator.RunId
         , Avg(TotalPremisesStaffServiceCosts) AS TotalPremisesStaffServiceCosts
         , Avg(CleaningCaretakingCosts)        AS CleaningCaretakingCosts
         , Avg(MaintenancePremisesCosts)       AS MaintenancePremisesCosts
         , Avg(OtherOccupationCosts)           AS OtherOccupationCosts
         , Avg(PremisesStaffCosts)             AS PremisesStaffCosts
         , Avg(TotalUtilitiesCosts)            AS TotalUtilitiesCosts
         , Avg(EnergyCosts)                    AS EnergyCosts
         , Avg(WaterSewerageCosts)             AS WaterSewerageCosts
      FROM buildingComparator
     INNER
      JOIN SchoolExpenditurePercentageOfIncomeHistoric
        ON (
                 buildingComparator.BuildingComparatorURN = SchoolExpenditurePercentageOfIncomeHistoric.URN
             AND buildingComparator.RunId = SchoolExpenditurePercentageOfIncomeHistoric.Year
           )
     GROUP
        BY buildingComparator.URN
         , buildingComparator.RunId
    )
  SELECT pupilExpenditureAvgOfPercentageOfIncomeComparatorSet.URN
       , pupilExpenditureAvgOfPercentageOfIncomeComparatorSet.RunId AS Year
       , TotalExpenditure
       , TotalIncome
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
    FROM pupilExpenditureAvgOfPercentageOfIncomeComparatorSet
    FULL
    JOIN buildingExpenditureAvgOfPercentageOfIncomeComparatorSet
      ON (
               pupilExpenditureAvgOfPercentageOfIncomeComparatorSet.URN = buildingExpenditureAvgOfPercentageOfIncomeComparatorSet.URN
           AND pupilExpenditureAvgOfPercentageOfIncomeComparatorSet.RunId = buildingExpenditureAvgOfPercentageOfIncomeComparatorSet.RunId
         )
GO