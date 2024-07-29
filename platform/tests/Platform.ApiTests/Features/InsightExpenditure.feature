Feature: Insights expenditure endpoints
    
    Scenario: Sending a valid school expenditure dimension request
        Given a valid school expenditure dimension request 
        When I submit the insights expenditure request
        Then the expenditure dimensions result should be ok and contain:
          | Dimension          |
          | Actuals            |
          | PerUnit            |
          | PercentIncome      |
          | PercentExpenditure |
        
    Scenario: Sending a valid school expenditure category request
        Given a valid school expenditure category request 
        When I submit the insights expenditure request
        Then the expenditure categories result should be ok and contain:
          | Category                     |
          | TotalExpenditure             |
          | TeachingTeachingSupportStaff |
          | NonEducationalSupportStaff   |
          | EducationalSupplies          |
          | EducationalIct               |
          | PremisesStaffServices        |
          | Utilities                    |
          | AdministrationSupplies       |
          | CateringStaffServices        |
          | Other                        |

    Scenario: Sending a valid school expenditure request with category, dimension and exclude central services
        Given a valid school expenditure request with urn '990000', category 'TotalExpenditure', dimension 'Actuals' and exclude central services = 'true'
        When I submit the insights expenditure request
        Then the school expenditure result should be ok and contain:
          | Field                                        | Value                  |
          | URN                                          | 990000                 |
          | SchoolName                                   | Test school 176        |
          | SchoolType                                   | Voluntary aided school |
          | LAName                                       | Bedford                |
          | TotalPupils                                  | 418.00                 |
          | TotalInternalFloorArea                       | 3021.00                |
          | SchoolTotalExpenditure                       |                        |
          | SchoolTotalTeachingSupportStaffCosts         |                        |
          | SchoolTeachingStaffCosts                     |                        |
          | SchoolSupplyTeachingStaffCosts               |                        |
          | SchoolEducationalConsultancyCosts            |                        |
          | SchoolEducationSupportStaffCosts             |                        |
          | SchoolAgencySupplyTeachingStaffCosts         |                        |
          | SchoolTotalNonEducationalSupportStaffCosts   |                        |
          | SchoolAdministrativeClericalStaffCosts       |                        |
          | SchoolOtherStaffCosts                        |                        |
          | SchoolProfessionalServicesNonCurriculumCosts |                        |
          | SchoolTotalEducationalSuppliesCosts          |                        |
          | SchoolExaminationFeesCosts                   |                        |
          | SchoolLearningResourcesNonIctCosts           |                        |
          | SchoolLearningResourcesIctCosts              |                        |
          | SchoolTotalPremisesStaffServiceCosts         |                        |
          | SchoolCleaningCaretakingCosts                |                        |
          | SchoolMaintenancePremisesCosts               |                        |
          | SchoolOtherOccupationCosts                   |                        |
          | SchoolPremisesStaffCosts                     |                        |
          | SchoolTotalUtilitiesCosts                    |                        |
          | SchoolEnergyCosts                            |                        |
          | SchoolWaterSewerageCosts                     |                        |
          | SchoolAdministrativeSuppliesCosts            |                        |
          | SchoolTotalGrossCateringCosts                |                        |
          | SchoolCateringStaffCosts                     |                        |
          | SchoolCateringSuppliesCosts                  |                        |
          | SchoolTotalOtherCosts                        |                        |
          | SchoolDirectRevenueFinancingCosts            |                        |
          | SchoolGroundsMaintenanceCosts                |                        |
          | SchoolIndirectEmployeeExpenses               |                        |
          | SchoolInterestChargesLoanBank                |                        |
          | SchoolOtherInsurancePremiumsCosts            |                        |
          | SchoolPrivateFinanceInitiativeCharges        |                        |
          | SchoolRentRatesCosts                         |                        |
          | SchoolSpecialFacilitiesCosts                 |                        |
          | SchoolStaffDevelopmentTrainingCosts          |                        |
          | SchoolStaffRelatedInsuranceCosts             |                        |
          | SchoolSupplyTeacherInsurableCosts            |                        |
          | SchoolCommunityFocusedSchoolStaff            |                        |
          | SchoolCommunityFocusedSchoolCosts            |                        |
          | TotalExpenditure                             | 2790504.00             |
          | TotalTeachingSupportStaffCosts               |                        |
          | TeachingStaffCosts                           |                        |
          | SupplyTeachingStaffCosts                     |                        |
          | EducationalConsultancyCosts                  |                        |
          | EducationSupportStaffCosts                   |                        |
          | AgencySupplyTeachingStaffCosts               |                        |
          | TotalNonEducationalSupportStaffCosts         |                        |
          | AdministrativeClericalStaffCosts             |                        |
          | OtherStaffCosts                              |                        |
          | ProfessionalServicesNonCurriculumCosts       |                        |
          | TotalEducationalSuppliesCosts                |                        |
          | ExaminationFeesCosts                         |                        |
          | LearningResourcesNonIctCosts                 |                        |
          | LearningResourcesIctCosts                    |                        |
          | TotalPremisesStaffServiceCosts               |                        |
          | CleaningCaretakingCosts                      |                        |
          | MaintenancePremisesCosts                     |                        |
          | OtherOccupationCosts                         |                        |
          | PremisesStaffCosts                           |                        |
          | TotalUtilitiesCosts                          |                        |
          | EnergyCosts                                  |                        |
          | WaterSewerageCosts                           |                        |
          | AdministrativeSuppliesCosts                  |                        |
          | TotalGrossCateringCosts                      |                        |
          | CateringStaffCosts                           |                        |
          | CateringSuppliesCosts                        |                        |
          | TotalOtherCosts                              |                        |
          | DirectRevenueFinancingCosts                  |                        |
          | GroundsMaintenanceCosts                      |                        |
          | IndirectEmployeeExpenses                     |                        |
          | InterestChargesLoanBank                      |                        |
          | OtherInsurancePremiumsCosts                  |                        |
          | PrivateFinanceInitiativeCharges              |                        |
          | RentRatesCosts                               |                        |
          | SpecialFacilitiesCosts                       |                        |
          | StaffDevelopmentTrainingCosts                |                        |
          | StaffRelatedInsuranceCosts                   |                        |
          | SupplyTeacherInsurableCosts                  |                        |
          | CommunityFocusedSchoolStaff                  |                        |
          | CommunityFocusedSchoolCosts                  |                        |
          
    Scenario: Sending a valid school expenditure request with category and dimension
        Given a valid school expenditure request with urn '990000', category 'TotalExpenditure', dimension 'Actuals' and exclude central services = ''
        When I submit the insights expenditure request
        Then the school expenditure result should be ok and contain:
          | Field                                        | Value                  |
          | URN                                          | 990000                 |
          | SchoolName                                   | Test school 176        |
          | SchoolType                                   | Voluntary aided school |
          | LAName                                       | Bedford                |
          | TotalPupils                                  | 418.00                 |
          | TotalInternalFloorArea                       | 3021.00                |
          | SchoolTotalExpenditure                       | 2790504.00             |
          | SchoolTotalTeachingSupportStaffCosts         |                        |
          | SchoolTeachingStaffCosts                     |                        |
          | SchoolSupplyTeachingStaffCosts               |                        |
          | SchoolEducationalConsultancyCosts            |                        |
          | SchoolEducationSupportStaffCosts             |                        |
          | SchoolAgencySupplyTeachingStaffCosts         |                        |
          | SchoolTotalNonEducationalSupportStaffCosts   |                        |
          | SchoolAdministrativeClericalStaffCosts       |                        |
          | SchoolOtherStaffCosts                        |                        |
          | SchoolProfessionalServicesNonCurriculumCosts |                        |
          | SchoolTotalEducationalSuppliesCosts          |                        |
          | SchoolExaminationFeesCosts                   |                        |
          | SchoolLearningResourcesNonIctCosts           |                        |
          | SchoolLearningResourcesIctCosts              |                        |
          | SchoolTotalPremisesStaffServiceCosts         |                        |
          | SchoolCleaningCaretakingCosts                |                        |
          | SchoolMaintenancePremisesCosts               |                        |
          | SchoolOtherOccupationCosts                   |                        |
          | SchoolPremisesStaffCosts                     |                        |
          | SchoolTotalUtilitiesCosts                    |                        |
          | SchoolEnergyCosts                            |                        |
          | SchoolWaterSewerageCosts                     |                        |
          | SchoolAdministrativeSuppliesCosts            |                        |
          | SchoolTotalGrossCateringCosts                |                        |
          | SchoolCateringStaffCosts                     |                        |
          | SchoolCateringSuppliesCosts                  |                        |
          | SchoolTotalOtherCosts                        |                        |
          | SchoolDirectRevenueFinancingCosts            |                        |
          | SchoolGroundsMaintenanceCosts                |                        |
          | SchoolIndirectEmployeeExpenses               |                        |
          | SchoolInterestChargesLoanBank                |                        |
          | SchoolOtherInsurancePremiumsCosts            |                        |
          | SchoolPrivateFinanceInitiativeCharges        |                        |
          | SchoolRentRatesCosts                         |                        |
          | SchoolSpecialFacilitiesCosts                 |                        |
          | SchoolStaffDevelopmentTrainingCosts          |                        |
          | SchoolStaffRelatedInsuranceCosts             |                        |
          | SchoolSupplyTeacherInsurableCosts            |                        |
          | SchoolCommunityFocusedSchoolStaff            |                        |
          | SchoolCommunityFocusedSchoolCosts            |                        |
          | TotalExpenditure                             | 2790504.00             |
          | TotalTeachingSupportStaffCosts               |                        |
          | TeachingStaffCosts                           |                        |
          | SupplyTeachingStaffCosts                     |                        |
          | EducationalConsultancyCosts                  |                        |
          | EducationSupportStaffCosts                   |                        |
          | AgencySupplyTeachingStaffCosts               |                        |
          | TotalNonEducationalSupportStaffCosts         |                        |
          | AdministrativeClericalStaffCosts             |                        |
          | OtherStaffCosts                              |                        |
          | ProfessionalServicesNonCurriculumCosts       |                        |
          | TotalEducationalSuppliesCosts                |                        |
          | ExaminationFeesCosts                         |                        |
          | LearningResourcesNonIctCosts                 |                        |
          | LearningResourcesIctCosts                    |                        |
          | TotalPremisesStaffServiceCosts               |                        |
          | CleaningCaretakingCosts                      |                        |
          | MaintenancePremisesCosts                     |                        |
          | OtherOccupationCosts                         |                        |
          | PremisesStaffCosts                           |                        |
          | TotalUtilitiesCosts                          |                        |
          | EnergyCosts                                  |                        |
          | WaterSewerageCosts                           |                        |
          | AdministrativeSuppliesCosts                  |                        |
          | TotalGrossCateringCosts                      |                        |
          | CateringStaffCosts                           |                        |
          | CateringSuppliesCosts                        |                        |
          | TotalOtherCosts                              |                        |
          | DirectRevenueFinancingCosts                  |                        |
          | GroundsMaintenanceCosts                      |                        |
          | IndirectEmployeeExpenses                     |                        |
          | InterestChargesLoanBank                      |                        |
          | OtherInsurancePremiumsCosts                  |                        |
          | PrivateFinanceInitiativeCharges              |                        |
          | RentRatesCosts                               |                        |
          | SpecialFacilitiesCosts                       |                        |
          | StaffDevelopmentTrainingCosts                |                        |
          | StaffRelatedInsuranceCosts                   |                        |
          | SupplyTeacherInsurableCosts                  |                        |
          | CommunityFocusedSchoolStaff                  |                        |
          | CommunityFocusedSchoolCosts                  |                        |

    Scenario: Sending a valid school expenditure request with dimension
        Given a valid school expenditure request with urn '990000', category '', dimension 'Actuals' and exclude central services = ''
        When I submit the insights expenditure request
        Then the school expenditure result should be ok and contain:
          | Field                                        | Value                  |
          | URN                                          | 990000                 |
          | SchoolName                                   | Test school 176        |
          | SchoolType                                   | Voluntary aided school |
          | LAName                                       | Bedford                |
          | TotalPupils                                  | 418.00                 |
          | TotalInternalFloorArea                       | 3021.00                |
          | SchoolTotalExpenditure                       | 2790504.00             |
          | SchoolTotalTeachingSupportStaffCosts         | 2044335.00             |
          | SchoolTeachingStaffCosts                     | 1437007.00             |
          | SchoolSupplyTeachingStaffCosts               | 0.00                   |
          | SchoolEducationalConsultancyCosts            | 208376.00              |
          | SchoolEducationSupportStaffCosts             | 301653.00              |
          | SchoolAgencySupplyTeachingStaffCosts         | 97299.00               |
          | SchoolTotalNonEducationalSupportStaffCosts   | 202341.00              |
          | SchoolAdministrativeClericalStaffCosts       | 157417.00              |
          | SchoolOtherStaffCosts                        | 12915.00               |
          | SchoolProfessionalServicesNonCurriculumCosts | 32009.00               |
          | SchoolTotalEducationalSuppliesCosts          | 70108.00               |
          | SchoolExaminationFeesCosts                   | 0.00                   |
          | SchoolLearningResourcesNonIctCosts           | 70108.00               |
          | SchoolLearningResourcesIctCosts              | 24200.00               |
          | SchoolTotalPremisesStaffServiceCosts         | 166932.00              |
          | SchoolCleaningCaretakingCosts                | 47240.00               |
          | SchoolMaintenancePremisesCosts               | 33774.00               |
          | SchoolOtherOccupationCosts                   | 33176.00               |
          | SchoolPremisesStaffCosts                     | 52742.00               |
          | SchoolTotalUtilitiesCosts                    | 31001.00               |
          | SchoolEnergyCosts                            | 26060.00               |
          | SchoolWaterSewerageCosts                     | 4941.00                |
          | SchoolAdministrativeSuppliesCosts            | 28505.00               |
          | SchoolTotalGrossCateringCosts                | 84186.00               |
          | SchoolCateringStaffCosts                     | 2170.00                |
          | SchoolCateringSuppliesCosts                  | 82016.00               |
          | SchoolTotalOtherCosts                        | 141903.00              |
          | SchoolDirectRevenueFinancingCosts            | 3006.00                |
          | SchoolGroundsMaintenanceCosts                | 615.00                 |
          | SchoolIndirectEmployeeExpenses               | 38374.00               |
          | SchoolInterestChargesLoanBank                | 0.00                   |
          | SchoolOtherInsurancePremiumsCosts            | 11862.00               |
          | SchoolPrivateFinanceInitiativeCharges        | 0.00                   |
          | SchoolRentRatesCosts                         | 73416.00               |
          | SchoolSpecialFacilitiesCosts                 | 0.00                   |
          | SchoolStaffDevelopmentTrainingCosts          | 9115.00                |
          | SchoolStaffRelatedInsuranceCosts             | 2735.00                |
          | SchoolSupplyTeacherInsurableCosts            | 2780.00                |
          | SchoolCommunityFocusedSchoolStaff            | 0.00                   |
          | SchoolCommunityFocusedSchoolCosts            | 0.00                   |
          | TotalExpenditure                             | 2790504.00             |
          | TotalTeachingSupportStaffCosts               | 2044335.00             |
          | TeachingStaffCosts                           | 1437007.00             |
          | SupplyTeachingStaffCosts                     | 0.00                   |
          | EducationalConsultancyCosts                  | 208376.00              |
          | EducationSupportStaffCosts                   | 301653.00              |
          | AgencySupplyTeachingStaffCosts               | 97299.00               |
          | TotalNonEducationalSupportStaffCosts         | 202341.00              |
          | AdministrativeClericalStaffCosts             | 157417.00              |
          | OtherStaffCosts                              | 12915.00               |
          | ProfessionalServicesNonCurriculumCosts       | 32009.00               |
          | TotalEducationalSuppliesCosts                | 70108.00               |
          | ExaminationFeesCosts                         | 0.00                   |
          | LearningResourcesNonIctCosts                 | 70108.00               |
          | LearningResourcesIctCosts                    | 24200.00               |
          | TotalPremisesStaffServiceCosts               | 166932.00              |
          | CleaningCaretakingCosts                      | 47240.00               |
          | MaintenancePremisesCosts                     | 33774.00               |
          | OtherOccupationCosts                         | 33176.00               |
          | PremisesStaffCosts                           | 52742.00               |
          | TotalUtilitiesCosts                          | 31001.00               |
          | EnergyCosts                                  | 26060.00               |
          | WaterSewerageCosts                           | 4941.00                |
          | AdministrativeSuppliesCosts                  | 28505.00               |
          | TotalGrossCateringCosts                      | 84186.00               |
          | CateringStaffCosts                           | 2170.00                |
          | CateringSuppliesCosts                        | 82016.00               |
          | TotalOtherCosts                              | 141903.00              |
          | DirectRevenueFinancingCosts                  | 3006.00                |
          | GroundsMaintenanceCosts                      | 615.00                 |
          | IndirectEmployeeExpenses                     | 38374.00               |
          | InterestChargesLoanBank                      | 0.00                   |
          | OtherInsurancePremiumsCosts                  | 11862.00               |
          | PrivateFinanceInitiativeCharges              | 0.00                   |
          | RentRatesCosts                               | 73416.00               |
          | SpecialFacilitiesCosts                       | 0.00                   |
          | StaffDevelopmentTrainingCosts                | 9115.00                |
          | StaffRelatedInsuranceCosts                   | 2735.00                |
          | SupplyTeacherInsurableCosts                  | 2780.00                |
          | CommunityFocusedSchoolStaff                  | 0.00                   |
          | CommunityFocusedSchoolCosts                  | 0.00                   |

    Scenario: Sending an invalid school expenditure request
        Given an invalid school expenditure request with urn '000000' 
        When I submit the insights expenditure request
        Then the school expenditure result should be not found
        
    Scenario: Sending a valid school expenditure history request
        Given a valid school expenditure history request with urn '990000' 
        When I submit the insights expenditure request
        Then the school expenditure history result should be ok and contain:
         | Year | Term         | URN    | SchoolTotalExpenditure | SchoolTotalTeachingSupportStaffCosts | SchoolTeachingStaffCosts | SchoolSupplyTeachingStaffCosts | SchoolEducationalConsultancyCosts | SchoolEducationSupportStaffCosts | SchoolAgencySupplyTeachingStaffCosts | SchoolTotalNonEducationalSupportStaffCosts | SchoolAdministrativeClericalStaffCosts | SchoolOtherStaffCosts | SchoolProfessionalServicesNonCurriculumCosts | SchoolTotalEducationalSuppliesCosts | SchoolExaminationFeesCosts | SchoolLearningResourcesNonIctCosts | SchoolLearningResourcesIctCosts | SchoolTotalPremisesStaffServiceCosts | SchoolCleaningCaretakingCosts | SchoolMaintenancePremisesCosts | SchoolOtherOccupationCosts | SchoolPremisesStaffCosts | SchoolTotalUtilitiesCosts | SchoolEnergyCosts | SchoolWaterSewerageCosts | SchoolAdministrativeSuppliesCosts | SchoolTotalGrossCateringCosts | SchoolCateringStaffCosts | SchoolCateringSuppliesCosts | SchoolTotalOtherCosts | SchoolDirectRevenueFinancingCosts | SchoolGroundsMaintenanceCosts | SchoolIndirectEmployeeExpenses | SchoolInterestChargesLoanBank | SchoolOtherInsurancePremiumsCosts | SchoolPrivateFinanceInitiativeCharges | SchoolRentRatesCosts | SchoolSpecialFacilitiesCosts | SchoolStaffDevelopmentTrainingCosts | SchoolStaffRelatedInsuranceCosts | SchoolSupplyTeacherInsurableCosts | SchoolCommunityFocusedSchoolStaff | SchoolCommunityFocusedSchoolCosts | TotalExpenditure | TotalTeachingSupportStaffCosts | TeachingStaffCosts | SupplyTeachingStaffCosts | EducationalConsultancyCosts | EducationSupportStaffCosts | AgencySupplyTeachingStaffCosts | TotalNonEducationalSupportStaffCosts | AdministrativeClericalStaffCosts | OtherStaffCosts | ProfessionalServicesNonCurriculumCosts | TotalEducationalSuppliesCosts | ExaminationFeesCosts | LearningResourcesNonIctCosts | LearningResourcesIctCosts | TotalPremisesStaffServiceCosts | CleaningCaretakingCosts | MaintenancePremisesCosts | OtherOccupationCosts | PremisesStaffCosts | TotalUtilitiesCosts | EnergyCosts | WaterSewerageCosts | AdministrativeSuppliesCosts | TotalGrossCateringCosts | CateringStaffCosts | CateringSuppliesCosts | TotalOtherCosts | DirectRevenueFinancingCosts | GroundsMaintenanceCosts | IndirectEmployeeExpenses | InterestChargesLoanBank | OtherInsurancePremiumsCosts | PrivateFinanceInitiativeCharges | RentRatesCosts | SpecialFacilitiesCosts | StaffDevelopmentTrainingCosts | StaffRelatedInsuranceCosts | SupplyTeacherInsurableCosts | CommunityFocusedSchoolStaff | CommunityFocusedSchoolCosts |
         | 2018 | 2017 to 2018 | 990000 |                        |                                      |                          |                                |                                   |                                  |                                      |                                            |                                        |                       |                                              |                                     |                            |                                    |                                 |                                      |                               |                                |                            |                          |                           |                   |                          |                                   |                               |                          |                             |                       |                                   |                               |                                |                               |                                   |                                       |                      |                              |                                     |                                  |                                   |                                   |                                   |                  |                                |                    |                          |                             |                            |                                |                                      |                                  |                 |                                        |                               |                      |                              |                           |                                |                         |                          |                      |                    |                     |             |                    |                             |                         |                    |                       |                 |                             |                         |                          |                         |                             |                                 |                |                        |                               |                            |                             |                             |                             |
         | 2019 | 2018 to 2019 | 990000 |                        |                                      |                          |                                |                                   |                                  |                                      |                                            |                                        |                       |                                              |                                     |                            |                                    |                                 |                                      |                               |                                |                            |                          |                           |                   |                          |                                   |                               |                          |                             |                       |                                   |                               |                                |                               |                                   |                                       |                      |                              |                                     |                                  |                                   |                                   |                                   |                  |                                |                    |                          |                             |                            |                                |                                      |                                  |                 |                                        |                               |                      |                              |                           |                                |                         |                          |                      |                    |                     |             |                    |                             |                         |                    |                       |                 |                             |                         |                          |                         |                             |                                 |                |                        |                               |                            |                             |                             |                             |
         | 2020 | 2019 to 2020 | 990000 |                        |                                      |                          |                                |                                   |                                  |                                      |                                            |                                        |                       |                                              |                                     |                            |                                    |                                 |                                      |                               |                                |                            |                          |                           |                   |                          |                                   |                               |                          |                             |                       |                                   |                               |                                |                               |                                   |                                       |                      |                              |                                     |                                  |                                   |                                   |                                   |                  |                                |                    |                          |                             |                            |                                |                                      |                                  |                 |                                        |                               |                      |                              |                           |                                |                         |                          |                      |                    |                     |             |                    |                             |                         |                    |                       |                 |                             |                         |                          |                         |                             |                                 |                |                        |                               |                            |                             |                             |                             |
         | 2021 | 2020 to 2021 | 990000 | 1352665.00             | 947543.00                            | 695259.00                | 0.00                           | 14407.00                          | 134473.00                        | 103404.00                            | 151028.00                                  | 98073.00                               | 9316.00               | 43639.00                                     | 10737.00                            | 0.00                       | 10737.00                           | 1236.00                         | 83002.00                             | 31170.00                      | 44765.00                       | 1089.00                    | 5978.00                  | 46713.00                  | 43198.00          | 3515.00                  | 7677.00                           | 67912.00                      | 0.00                     | 67912.00                    | 36817.00              | 0.00                              | 8211.00                       | 1405.00                        | 0.00                          | 2865.00                           | 0.00                                  | 15763.00             | 0.00                         | 8573.00                             | 0.00                             | 0.00                              | 0.00                              | 0.00                              | 1352665.00       | 947543.00                      | 695259.00          | 0.00                     | 14407.00                    | 134473.00                  | 103404.00                      | 151028.00                            | 98073.00                         | 9316.00         | 43639.00                               | 10737.00                      | 0.00                 | 10737.00                     | 1236.00                   | 83002.00                       | 31170.00                | 44765.00                 | 1089.00              | 5978.00            | 46713.00            | 43198.00    | 3515.00            | 7677.00                     | 67912.00                | 0.00               | 67912.00              | 36817.00        | 0.00                        | 8211.00                 | 1405.00                  | 0.00                    | 2865.00                     | 0.00                            | 15763.00       | 0.00                   | 8573.00                       | 0.00                       | 0.00                        | 0.00                        | 0.00                        |
         | 2022 | 2021 to 2022 | 990000 | 2790504.00             | 2044335.00                           | 1437007.00               | 0.00                           | 208376.00                         | 301653.00                        | 97299.00                             | 202341.00                                  | 157417.00                              | 12915.00              | 32009.00                                     | 70108.00                            | 0.00                       | 70108.00                           | 24200.00                        | 166932.00                            | 47240.00                      | 33774.00                       | 33176.00                   | 52742.00                 | 31001.00                  | 26060.00          | 4941.00                  | 28505.00                          | 84186.00                      | 2170.00                  | 82016.00                    | 141903.00             | 3006.00                           | 615.00                        | 38374.00                       | 0.00                          | 11862.00                          | 0.00                                  | 73416.00             | 0.00                         | 9115.00                             | 2735.00                          | 2780.00                           | 0.00                              | 0.00                              | 2790504.00       | 2044335.00                     | 1437007.00         | 0.00                     | 208376.00                   | 301653.00                  | 97299.00                       | 202341.00                            | 157417.00                        | 12915.00        | 32009.00                               | 70108.00                      | 0.00                 | 70108.00                     | 24200.00                  | 166932.00                      | 47240.00                | 33774.00                 | 33176.00             | 52742.00           | 31001.00            | 26060.00    | 4941.00            | 28505.00                    | 84186.00                | 2170.00            | 82016.00              | 141903.00       | 3006.00                     | 615.00                  | 38374.00                 | 0.00                    | 11862.00                    | 0.00                            | 73416.00       | 0.00                   | 9115.00                       | 2735.00                    | 2780.00                     | 0.00                        | 0.00                        |

    Scenario: Sending a valid school expenditure query request
        Given a valid school expenditure query request with urns:
          | Urn    |
          | 990000 |
          | 990001 |
          | 990002 |
        When I submit the insights expenditure request
        Then the school expenditure query result should be ok and contain:
         | SchoolName      | SchoolType                     | LAName        | URN    | SchoolName      | SchoolType                     | LAName        | TotalPupils | TotalInternalFloorArea | SchoolTotalExpenditure | SchoolTotalTeachingSupportStaffCosts | SchoolTeachingStaffCosts | SchoolSupplyTeachingStaffCosts | SchoolEducationalConsultancyCosts | SchoolEducationSupportStaffCosts | SchoolAgencySupplyTeachingStaffCosts | SchoolTotalNonEducationalSupportStaffCosts | SchoolAdministrativeClericalStaffCosts | SchoolOtherStaffCosts | SchoolProfessionalServicesNonCurriculumCosts | SchoolTotalEducationalSuppliesCosts | SchoolExaminationFeesCosts | SchoolLearningResourcesNonIctCosts | SchoolLearningResourcesIctCosts | SchoolTotalPremisesStaffServiceCosts | SchoolCleaningCaretakingCosts | SchoolMaintenancePremisesCosts | SchoolOtherOccupationCosts | SchoolPremisesStaffCosts | SchoolTotalUtilitiesCosts | SchoolEnergyCosts | SchoolWaterSewerageCosts | SchoolAdministrativeSuppliesCosts | SchoolTotalGrossCateringCosts | SchoolCateringStaffCosts | SchoolCateringSuppliesCosts | SchoolTotalOtherCosts | SchoolDirectRevenueFinancingCosts | SchoolGroundsMaintenanceCosts | SchoolIndirectEmployeeExpenses | SchoolInterestChargesLoanBank | SchoolOtherInsurancePremiumsCosts | SchoolPrivateFinanceInitiativeCharges | SchoolRentRatesCosts | SchoolSpecialFacilitiesCosts | SchoolStaffDevelopmentTrainingCosts | SchoolStaffRelatedInsuranceCosts | SchoolSupplyTeacherInsurableCosts | SchoolCommunityFocusedSchoolStaff | SchoolCommunityFocusedSchoolCosts | TotalExpenditure | TotalTeachingSupportStaffCosts | TeachingStaffCosts | SupplyTeachingStaffCosts | EducationalConsultancyCosts | EducationSupportStaffCosts | AgencySupplyTeachingStaffCosts | TotalNonEducationalSupportStaffCosts | AdministrativeClericalStaffCosts | OtherStaffCosts | ProfessionalServicesNonCurriculumCosts | TotalEducationalSuppliesCosts | ExaminationFeesCosts | LearningResourcesNonIctCosts | LearningResourcesIctCosts | TotalPremisesStaffServiceCosts | CleaningCaretakingCosts | MaintenancePremisesCosts | OtherOccupationCosts | PremisesStaffCosts | TotalUtilitiesCosts | EnergyCosts | WaterSewerageCosts | AdministrativeSuppliesCosts | TotalGrossCateringCosts | CateringStaffCosts | CateringSuppliesCosts | TotalOtherCosts | DirectRevenueFinancingCosts | GroundsMaintenanceCosts | IndirectEmployeeExpenses | InterestChargesLoanBank | OtherInsurancePremiumsCosts | PrivateFinanceInitiativeCharges | RentRatesCosts | SpecialFacilitiesCosts | StaffDevelopmentTrainingCosts | StaffRelatedInsuranceCosts | SupplyTeacherInsurableCosts | CommunityFocusedSchoolStaff | CommunityFocusedSchoolCosts |
         | Test school 176 | Voluntary aided school         | Bedford       | 990000 | Test school 176 | Voluntary aided school         | Bedford       | 418.00      | 3021.00                | 2790504.00             | 2044335.00                           | 1437007.00               | 0.00                           | 208376.00                         | 301653.00                        | 97299.00                             | 202341.00                                  | 157417.00                              | 12915.00              | 32009.00                                     | 70108.00                            | 0.00                       | 70108.00                           | 24200.00                        | 166932.00                            | 47240.00                      | 33774.00                       | 33176.00                   | 52742.00                 | 31001.00                  | 26060.00          | 4941.00                  | 28505.00                          | 84186.00                      | 2170.00                  | 82016.00                    | 141903.00             | 3006.00                           | 615.00                        | 38374.00                       | 0.00                          | 11862.00                          | 0.00                                  | 73416.00             | 0.00                         | 9115.00                             | 2735.00                          | 2780.00                           | 0.00                              | 0.00                              | 2790504.00       | 2044335.00                     | 1437007.00         | 0.00                     | 208376.00                   | 301653.00                  | 97299.00                       | 202341.00                            | 157417.00                        | 12915.00        | 32009.00                               | 70108.00                      | 0.00                 | 70108.00                     | 24200.00                  | 166932.00                      | 47240.00                | 33774.00                 | 33176.00             | 52742.00           | 31001.00            | 26060.00    | 4941.00            | 28505.00                    | 84186.00                | 2170.00            | 82016.00              | 141903.00       | 3006.00                     | 615.00                  | 38374.00                 | 0.00                    | 11862.00                    | 0.00                            | 73416.00       | 0.00                   | 9115.00                       | 2735.00                    | 2780.00                     | 0.00                        | 0.00                        |
         | Test school 241 | Voluntary aided school         | Islington     | 990001 | Test school 241 | Voluntary aided school         | Islington     | 303.00      | 2476.00                | 2054918.00             | 1521798.00                           | 997855.00                | 0.00                           | 220708.00                         | 278722.00                        | 24513.00                             | 160770.00                                  | 92011.00                               | 28511.00              | 40248.00                                     | 46521.00                            | 0.00                       | 46521.00                           | 10779.00                        | 143345.00                            | 4254.00                       | 38484.00                       | 8972.00                    | 91635.00                 | 23799.00                  | 21324.00          | 2475.00                  | 21815.00                          | 71734.00                      | 0.00                     | 71734.00                    | 54357.00              | 0.00                              | 407.00                        | 6586.00                        | 0.00                          | 10274.00                          | 0.00                                  | 0.00                 | 2695.00                      | 1648.00                             | 8736.00                          | 24011.00                          | 0.00                              | 0.00                              | 2054918.00       | 1521798.00                     | 997855.00          | 0.00                     | 220708.00                   | 278722.00                  | 24513.00                       | 160770.00                            | 92011.00                         | 28511.00        | 40248.00                               | 46521.00                      | 0.00                 | 46521.00                     | 10779.00                  | 143345.00                      | 4254.00                 | 38484.00                 | 8972.00              | 91635.00           | 23799.00            | 21324.00    | 2475.00            | 21815.00                    | 71734.00                | 0.00               | 71734.00              | 54357.00        | 0.00                        | 407.00                  | 6586.00                  | 0.00                    | 10274.00                    | 0.00                            | 0.00           | 2695.00                | 1648.00                       | 8736.00                    | 24011.00                    | 0.00                        | 0.00                        |
         | Test school 224 | Local authority nursery school | Isle of Wight | 990002 | Test school 224 | Local authority nursery school | Isle of Wight | 191.00      | 2552.00                | 1719060.00             | 1189016.00                           | 733333.00                | 0.00                           | 151630.00                         | 161948.00                        | 142105.00                            | 137246.00                                  | 103006.00                              | 13488.00              | 20752.00                                     | 24992.00                            | 0.00                       | 24992.00                           | 14462.00                        | 110661.00                            | 35684.00                      | 22248.00                       | 6416.00                    | 46313.00                 | 42196.00                  | 37236.00          | 4960.00                  | 17131.00                          | 48949.00                      | 0.00                     | 48949.00                    | 134408.00             | 0.00                              | 0.00                          | 26539.00                       | 0.00                          | 6125.00                           | 0.00                                  | 22580.00             | 1287.00                      | 3568.00                             | 7587.00                          | 18030.00                          | 47010.00                          | 1682.00                           | 1719060.00       | 1189016.00                     | 733333.00          | 0.00                     | 151630.00                   | 161948.00                  | 142105.00                      | 137246.00                            | 103006.00                        | 13488.00        | 20752.00                               | 24992.00                      | 0.00                 | 24992.00                     | 14462.00                  | 110661.00                      | 35684.00                | 22248.00                 | 6416.00              | 46313.00           | 42196.00            | 37236.00    | 4960.00            | 17131.00                    | 48949.00                | 0.00               | 48949.00              | 134408.00       | 0.00                        | 0.00                    | 26539.00                 | 0.00                    | 6125.00                     | 0.00                            | 22580.00       | 1287.00                | 3568.00                       | 7587.00                    | 18030.00                    | 47010.00                    | 1682.00                     |

    Scenario: Sending a valid trust expenditure request with category, dimension and exclude central services
        Given a valid trust expenditure request with company number '10192252', category 'TotalExpenditure', dimension 'Actuals' and exclude central services = 'true'
        When I submit the insights expenditure request
        Then the trust expenditure result should be ok and contain:
          | Field                                        | Value                  |
          | CompanyNumber                                | 10192252               |
          | SchoolTotalExpenditure                       |                        |
          | SchoolTotalTeachingSupportStaffCosts         |                        |
          | SchoolTeachingStaffCosts                     |                        |
          | SchoolSupplyTeachingStaffCosts               |                        |
          | SchoolEducationalConsultancyCosts            |                        |
          | SchoolEducationSupportStaffCosts             |                        |
          | SchoolAgencySupplyTeachingStaffCosts         |                        |
          | SchoolTotalNonEducationalSupportStaffCosts   |                        |
          | SchoolAdministrativeClericalStaffCosts       |                        |
          | SchoolOtherStaffCosts                        |                        |
          | SchoolProfessionalServicesNonCurriculumCosts |                        |
          | SchoolTotalEducationalSuppliesCosts          |                        |
          | SchoolExaminationFeesCosts                   |                        |
          | SchoolLearningResourcesNonIctCosts           |                        |
          | SchoolLearningResourcesIctCosts              |                        |
          | SchoolTotalPremisesStaffServiceCosts         |                        |
          | SchoolCleaningCaretakingCosts                |                        |
          | SchoolMaintenancePremisesCosts               |                        |
          | SchoolOtherOccupationCosts                   |                        |
          | SchoolPremisesStaffCosts                     |                        |
          | SchoolTotalUtilitiesCosts                    |                        |
          | SchoolEnergyCosts                            |                        |
          | SchoolWaterSewerageCosts                     |                        |
          | SchoolAdministrativeSuppliesCosts            |                        |
          | SchoolTotalGrossCateringCosts                |                        |
          | SchoolCateringStaffCosts                     |                        |
          | SchoolCateringSuppliesCosts                  |                        |
          | SchoolTotalOtherCosts                        |                        |
          | SchoolDirectRevenueFinancingCosts            |                        |
          | SchoolGroundsMaintenanceCosts                |                        |
          | SchoolIndirectEmployeeExpenses               |                        |
          | SchoolInterestChargesLoanBank                |                        |
          | SchoolOtherInsurancePremiumsCosts            |                        |
          | SchoolPrivateFinanceInitiativeCharges        |                        |
          | SchoolRentRatesCosts                         |                        |
          | SchoolSpecialFacilitiesCosts                 |                        |
          | SchoolStaffDevelopmentTrainingCosts          |                        |
          | SchoolStaffRelatedInsuranceCosts             |                        |
          | SchoolSupplyTeacherInsurableCosts            |                        |
          | SchoolCommunityFocusedSchoolStaff            |                        |
          | SchoolCommunityFocusedSchoolCosts            |                        |
          | TotalExpenditure                             | 3424906.00             |
          | TotalTeachingSupportStaffCosts               |                        |
          | TeachingStaffCosts                           |                        |
          | SupplyTeachingStaffCosts                     |                        |
          | EducationalConsultancyCosts                  |                        |
          | EducationSupportStaffCosts                   |                        |
          | AgencySupplyTeachingStaffCosts               |                        |
          | TotalNonEducationalSupportStaffCosts         |                        |
          | AdministrativeClericalStaffCosts             |                        |
          | OtherStaffCosts                              |                        |
          | ProfessionalServicesNonCurriculumCosts       |                        |
          | TotalEducationalSuppliesCosts                |                        |
          | ExaminationFeesCosts                         |                        |
          | LearningResourcesNonIctCosts                 |                        |
          | LearningResourcesIctCosts                    |                        |
          | TotalPremisesStaffServiceCosts               |                        |
          | CleaningCaretakingCosts                      |                        |
          | MaintenancePremisesCosts                     |                        |
          | OtherOccupationCosts                         |                        |
          | PremisesStaffCosts                           |                        |
          | TotalUtilitiesCosts                          |                        |
          | EnergyCosts                                  |                        |
          | WaterSewerageCosts                           |                        |
          | AdministrativeSuppliesCosts                  |                        |
          | TotalGrossCateringCosts                      |                        |
          | CateringStaffCosts                           |                        |
          | CateringSuppliesCosts                        |                        |
          | TotalOtherCosts                              |                        |
          | DirectRevenueFinancingCosts                  |                        |
          | GroundsMaintenanceCosts                      |                        |
          | IndirectEmployeeExpenses                     |                        |
          | InterestChargesLoanBank                      |                        |
          | OtherInsurancePremiumsCosts                  |                        |
          | PrivateFinanceInitiativeCharges              |                        |
          | RentRatesCosts                               |                        |
          | SpecialFacilitiesCosts                       |                        |
          | StaffDevelopmentTrainingCosts                |                        |
          | StaffRelatedInsuranceCosts                   |                        |
          | SupplyTeacherInsurableCosts                  |                        |
          | CommunityFocusedSchoolStaff                  |                        |
          | CommunityFocusedSchoolCosts                  |                        |
          
    Scenario: Sending a valid trust expenditure request with category and dimension
        Given a valid trust expenditure request with company number '10192252', category 'TotalExpenditure', dimension 'Actuals' and exclude central services = ''
        When I submit the insights expenditure request
        Then the trust expenditure result should be ok and contain:
          | Field                                        | Value                  |
          | CompanyNumber                                | 10192252               |
          | SchoolTotalExpenditure                       | 3424906.00             |
          | SchoolTotalTeachingSupportStaffCosts         |                        |
          | SchoolTeachingStaffCosts                     |                        |
          | SchoolSupplyTeachingStaffCosts               |                        |
          | SchoolEducationalConsultancyCosts            |                        |
          | SchoolEducationSupportStaffCosts             |                        |
          | SchoolAgencySupplyTeachingStaffCosts         |                        |
          | SchoolTotalNonEducationalSupportStaffCosts   |                        |
          | SchoolAdministrativeClericalStaffCosts       |                        |
          | SchoolOtherStaffCosts                        |                        |
          | SchoolProfessionalServicesNonCurriculumCosts |                        |
          | SchoolTotalEducationalSuppliesCosts          |                        |
          | SchoolExaminationFeesCosts                   |                        |
          | SchoolLearningResourcesNonIctCosts           |                        |
          | SchoolLearningResourcesIctCosts              |                        |
          | SchoolTotalPremisesStaffServiceCosts         |                        |
          | SchoolCleaningCaretakingCosts                |                        |
          | SchoolMaintenancePremisesCosts               |                        |
          | SchoolOtherOccupationCosts                   |                        |
          | SchoolPremisesStaffCosts                     |                        |
          | SchoolTotalUtilitiesCosts                    |                        |
          | SchoolEnergyCosts                            |                        |
          | SchoolWaterSewerageCosts                     |                        |
          | SchoolAdministrativeSuppliesCosts            |                        |
          | SchoolTotalGrossCateringCosts                |                        |
          | SchoolCateringStaffCosts                     |                        |
          | SchoolCateringSuppliesCosts                  |                        |
          | SchoolTotalOtherCosts                        |                        |
          | SchoolDirectRevenueFinancingCosts            |                        |
          | SchoolGroundsMaintenanceCosts                |                        |
          | SchoolIndirectEmployeeExpenses               |                        |
          | SchoolInterestChargesLoanBank                |                        |
          | SchoolOtherInsurancePremiumsCosts            |                        |
          | SchoolPrivateFinanceInitiativeCharges        |                        |
          | SchoolRentRatesCosts                         |                        |
          | SchoolSpecialFacilitiesCosts                 |                        |
          | SchoolStaffDevelopmentTrainingCosts          |                        |
          | SchoolStaffRelatedInsuranceCosts             |                        |
          | SchoolSupplyTeacherInsurableCosts            |                        |
          | SchoolCommunityFocusedSchoolStaff            |                        |
          | SchoolCommunityFocusedSchoolCosts            |                        |
          | TotalExpenditure                             | 3424906.00             |
          | TotalTeachingSupportStaffCosts               |                        |
          | TeachingStaffCosts                           |                        |
          | SupplyTeachingStaffCosts                     |                        |
          | EducationalConsultancyCosts                  |                        |
          | EducationSupportStaffCosts                   |                        |
          | AgencySupplyTeachingStaffCosts               |                        |
          | TotalNonEducationalSupportStaffCosts         |                        |
          | AdministrativeClericalStaffCosts             |                        |
          | OtherStaffCosts                              |                        |
          | ProfessionalServicesNonCurriculumCosts       |                        |
          | TotalEducationalSuppliesCosts                |                        |
          | ExaminationFeesCosts                         |                        |
          | LearningResourcesNonIctCosts                 |                        |
          | LearningResourcesIctCosts                    |                        |
          | TotalPremisesStaffServiceCosts               |                        |
          | CleaningCaretakingCosts                      |                        |
          | MaintenancePremisesCosts                     |                        |
          | OtherOccupationCosts                         |                        |
          | PremisesStaffCosts                           |                        |
          | TotalUtilitiesCosts                          |                        |
          | EnergyCosts                                  |                        |
          | WaterSewerageCosts                           |                        |
          | AdministrativeSuppliesCosts                  |                        |
          | TotalGrossCateringCosts                      |                        |
          | CateringStaffCosts                           |                        |
          | CateringSuppliesCosts                        |                        |
          | TotalOtherCosts                              |                        |
          | DirectRevenueFinancingCosts                  |                        |
          | GroundsMaintenanceCosts                      |                        |
          | IndirectEmployeeExpenses                     |                        |
          | InterestChargesLoanBank                      |                        |
          | OtherInsurancePremiumsCosts                  |                        |
          | PrivateFinanceInitiativeCharges              |                        |
          | RentRatesCosts                               |                        |
          | SpecialFacilitiesCosts                       |                        |
          | StaffDevelopmentTrainingCosts                |                        |
          | StaffRelatedInsuranceCosts                   |                        |
          | SupplyTeacherInsurableCosts                  |                        |
          | CommunityFocusedSchoolStaff                  |                        |
          | CommunityFocusedSchoolCosts                  |                        |

    Scenario: Sending a valid trust expenditure request with dimension
        Given a valid trust expenditure request with company number '10192252', category '', dimension 'Actuals' and exclude central services = ''
        When I submit the insights expenditure request
        Then the trust expenditure result should be ok and contain:
          | Field                                        | Value      |
          | CompanyNumber                                | 10192252   |
          | SchoolTotalExpenditure                       | 3424906.00 |
          | SchoolTotalTeachingSupportStaffCosts         | 2467694.00 |
          | SchoolTeachingStaffCosts                     | 1575657.00 |
          | SchoolSupplyTeachingStaffCosts               | 0.00       |
          | SchoolEducationalConsultancyCosts            | 262691.00  |
          | SchoolEducationSupportStaffCosts             | 589290.00  |
          | SchoolAgencySupplyTeachingStaffCosts         | 40056.00   |
          | SchoolTotalNonEducationalSupportStaffCosts   | 385697.00  |
          | SchoolAdministrativeClericalStaffCosts       | 183765.00  |
          | SchoolOtherStaffCosts                        | 133034.00  |
          | SchoolProfessionalServicesNonCurriculumCosts | 68898.00   |
          | SchoolTotalEducationalSuppliesCosts          | 60244.00   |
          | SchoolExaminationFeesCosts                   | 0.00       |
          | SchoolLearningResourcesNonIctCosts           | 60244.00   |
          | SchoolLearningResourcesIctCosts              | 9824.00    |
          | SchoolTotalPremisesStaffServiceCosts         | 228507.00  |
          | SchoolCleaningCaretakingCosts                | 72597.00   |
          | SchoolMaintenancePremisesCosts               | 87146.00   |
          | SchoolOtherOccupationCosts                   | 15500.00   |
          | SchoolPremisesStaffCosts                     | 53264.00   |
          | SchoolTotalUtilitiesCosts                    | 61813.00   |
          | SchoolEnergyCosts                            | 53027.00   |
          | SchoolWaterSewerageCosts                     | 8786.00    |
          | SchoolAdministrativeSuppliesCosts            | 26172.00   |
          | SchoolTotalGrossCateringCosts                | 95978.00   |
          | SchoolCateringStaffCosts                     | 0.00       |
          | SchoolCateringSuppliesCosts                  | 95978.00   |
          | SchoolTotalOtherCosts                        | 88978.00   |
          | SchoolDirectRevenueFinancingCosts            | 0.00       |
          | SchoolGroundsMaintenanceCosts                | 0.00       |
          | SchoolIndirectEmployeeExpenses               | 1493.00    |
          | SchoolInterestChargesLoanBank                | 0.00       |
          | SchoolOtherInsurancePremiumsCosts            | 10579.00   |
          | SchoolPrivateFinanceInitiativeCharges        | 0.00       |
          | SchoolRentRatesCosts                         | 44954.00   |
          | SchoolSpecialFacilitiesCosts                 | 11754.00   |
          | SchoolStaffDevelopmentTrainingCosts          | 15389.00   |
          | SchoolStaffRelatedInsuranceCosts             | 2311.00    |
          | SchoolSupplyTeacherInsurableCosts            | 2498.00    |
          | SchoolCommunityFocusedSchoolStaff            | 0.00       |
          | SchoolCommunityFocusedSchoolCosts            | 0.00       |
          | TotalExpenditure                             | 3424906.00 |
          | TotalTeachingSupportStaffCosts               | 2467694.00 |
          | TeachingStaffCosts                           | 1575657.00 |
          | SupplyTeachingStaffCosts                     | 0.00       |
          | EducationalConsultancyCosts                  | 262691.00  |
          | EducationSupportStaffCosts                   | 589290.00  |
          | AgencySupplyTeachingStaffCosts               | 40056.00   |
          | TotalNonEducationalSupportStaffCosts         | 385697.00  |
          | AdministrativeClericalStaffCosts             | 183765.00  |
          | OtherStaffCosts                              | 133034.00  |
          | ProfessionalServicesNonCurriculumCosts       | 68898.00   |
          | TotalEducationalSuppliesCosts                | 60244.00   |
          | ExaminationFeesCosts                         | 0.00       |
          | LearningResourcesNonIctCosts                 | 60244.00   |
          | LearningResourcesIctCosts                    | 9824.00    |
          | TotalPremisesStaffServiceCosts               | 228507.00  |
          | CleaningCaretakingCosts                      | 72597.00   |
          | MaintenancePremisesCosts                     | 87146.00   |
          | OtherOccupationCosts                         | 15500.00   |
          | PremisesStaffCosts                           | 53264.00   |
          | TotalUtilitiesCosts                          | 61813.00   |
          | EnergyCosts                                  | 53027.00   |
          | WaterSewerageCosts                           | 8786.00    |
          | AdministrativeSuppliesCosts                  | 26172.00   |
          | TotalGrossCateringCosts                      | 95978.00   |
          | CateringStaffCosts                           | 0.00       |
          | CateringSuppliesCosts                        | 95978.00   |
          | TotalOtherCosts                              | 88978.00   |
          | DirectRevenueFinancingCosts                  | 0.00       |
          | GroundsMaintenanceCosts                      | 0.00       |
          | IndirectEmployeeExpenses                     | 1493.00    |
          | InterestChargesLoanBank                      | 0.00       |
          | OtherInsurancePremiumsCosts                  | 10579.00   |
          | PrivateFinanceInitiativeCharges              | 0.00       |
          | RentRatesCosts                               | 44954.00   |
          | SpecialFacilitiesCosts                       | 11754.00   |
          | StaffDevelopmentTrainingCosts                | 15389.00   |
          | StaffRelatedInsuranceCosts                   | 2311.00    |
          | SupplyTeacherInsurableCosts                  | 2498.00    |
          | CommunityFocusedSchoolStaff                  | 0.00       |
          | CommunityFocusedSchoolCosts                  | 0.00       |

    Scenario: Sending an invalid trust expenditure request
        Given an invalid trust expenditure request with company number '10000000' 
        When I submit the insights expenditure request
        Then the trust expenditure result should be not found
        
    Scenario: Sending a valid trust expenditure history request
        Given a valid trust expenditure history request with company number '10192252' 
        When I submit the insights expenditure request
        Then the trust expenditure history result should be ok and contain:
         | Year | Term         | CompanyNumber | SchoolTotalExpenditure | SchoolTotalTeachingSupportStaffCosts | SchoolTeachingStaffCosts | SchoolSupplyTeachingStaffCosts | SchoolEducationalConsultancyCosts | SchoolEducationSupportStaffCosts | SchoolAgencySupplyTeachingStaffCosts | SchoolTotalNonEducationalSupportStaffCosts | SchoolAdministrativeClericalStaffCosts | SchoolOtherStaffCosts | SchoolProfessionalServicesNonCurriculumCosts | SchoolTotalEducationalSuppliesCosts | SchoolExaminationFeesCosts | SchoolLearningResourcesNonIctCosts | SchoolLearningResourcesIctCosts | SchoolTotalPremisesStaffServiceCosts | SchoolCleaningCaretakingCosts | SchoolMaintenancePremisesCosts | SchoolOtherOccupationCosts | SchoolPremisesStaffCosts | SchoolTotalUtilitiesCosts | SchoolEnergyCosts | SchoolWaterSewerageCosts | SchoolAdministrativeSuppliesCosts | SchoolTotalGrossCateringCosts | SchoolCateringStaffCosts | SchoolCateringSuppliesCosts | SchoolTotalOtherCosts | SchoolDirectRevenueFinancingCosts | SchoolGroundsMaintenanceCosts | SchoolIndirectEmployeeExpenses | SchoolInterestChargesLoanBank | SchoolOtherInsurancePremiumsCosts | SchoolPrivateFinanceInitiativeCharges | SchoolRentRatesCosts | SchoolSpecialFacilitiesCosts | SchoolStaffDevelopmentTrainingCosts | SchoolStaffRelatedInsuranceCosts | SchoolSupplyTeacherInsurableCosts | SchoolCommunityFocusedSchoolStaff | SchoolCommunityFocusedSchoolCosts | TotalExpenditure | TotalTeachingSupportStaffCosts | TeachingStaffCosts | SupplyTeachingStaffCosts | EducationalConsultancyCosts | EducationSupportStaffCosts | AgencySupplyTeachingStaffCosts | TotalNonEducationalSupportStaffCosts | AdministrativeClericalStaffCosts | OtherStaffCosts | ProfessionalServicesNonCurriculumCosts | TotalEducationalSuppliesCosts | ExaminationFeesCosts | LearningResourcesNonIctCosts | LearningResourcesIctCosts | TotalPremisesStaffServiceCosts | CleaningCaretakingCosts | MaintenancePremisesCosts | OtherOccupationCosts | PremisesStaffCosts | TotalUtilitiesCosts | EnergyCosts | WaterSewerageCosts | AdministrativeSuppliesCosts | TotalGrossCateringCosts | CateringStaffCosts | CateringSuppliesCosts | TotalOtherCosts | DirectRevenueFinancingCosts | GroundsMaintenanceCosts | IndirectEmployeeExpenses | InterestChargesLoanBank | OtherInsurancePremiumsCosts | PrivateFinanceInitiativeCharges | RentRatesCosts | SpecialFacilitiesCosts | StaffDevelopmentTrainingCosts | StaffRelatedInsuranceCosts | SupplyTeacherInsurableCosts | CommunityFocusedSchoolStaff | CommunityFocusedSchoolCosts |
         | 2018 | 2017 to 2018 | 10192252      |                        |                                      |                          |                                |                                   |                                  |                                      |                                            |                                        |                       |                                              |                                     |                            |                                    |                                 |                                      |                               |                                |                            |                          |                           |                   |                          |                                   |                               |                          |                             |                       |                                   |                               |                                |                               |                                   |                                       |                      |                              |                                     |                                  |                                   |                                   |                                   |                  |                                |                    |                          |                             |                            |                                |                                      |                                  |                 |                                        |                               |                      |                              |                           |                                |                         |                          |                      |                    |                     |             |                    |                             |                         |                    |                       |                 |                             |                         |                          |                         |                             |                                 |                |                        |                               |                            |                             |                             |                             |
         | 2019 | 2018 to 2019 | 10192252      |                        |                                      |                          |                                |                                   |                                  |                                      |                                            |                                        |                       |                                              |                                     |                            |                                    |                                 |                                      |                               |                                |                            |                          |                           |                   |                          |                                   |                               |                          |                             |                       |                                   |                               |                                |                               |                                   |                                       |                      |                              |                                     |                                  |                                   |                                   |                                   |                  |                                |                    |                          |                             |                            |                                |                                      |                                  |                 |                                        |                               |                      |                              |                           |                                |                         |                          |                      |                    |                     |             |                    |                             |                         |                    |                       |                 |                             |                         |                          |                         |                             |                                 |                |                        |                               |                            |                             |                             |                             |
         | 2020 | 2019 to 2020 | 10192252      |                        |                                      |                          |                                |                                   |                                  |                                      |                                            |                                        |                       |                                              |                                     |                            |                                    |                                 |                                      |                               |                                |                            |                          |                           |                   |                          |                                   |                               |                          |                             |                       |                                   |                               |                                |                               |                                   |                                       |                      |                              |                                     |                                  |                                   |                                   |                                   |                  |                                |                    |                          |                             |                            |                                |                                      |                                  |                 |                                        |                               |                      |                              |                           |                                |                         |                          |                      |                    |                     |             |                    |                             |                         |                    |                       |                 |                             |                         |                          |                         |                             |                                 |                |                        |                               |                            |                             |                             |                             |
         | 2021 | 2020 to 2021 | 10192252      | 1659677.00             | 1171376.00                           | 864881.00                | 0.00                           | 0.00                              | 306495.00                        | 0.00                                 | 192508.00                                  | 143982.00                              | 0.00                  | 48526.00                                     | 32728.00                            | 0.00                       | 32728.00                           | 3705.00                         | 102750.00                            | 30605.00                      | 26530.00                       | 5966.00                    | 39649.00                 | 23873.00                  | 19666.00          | 4207.00                  | 10977.00                          | 66380.00                      | 0.00                     | 66380.00                    | 55380.00              | 0.00                              | 4690.00                       | 5067.00                        | 0.00                          | 3150.00                           | 0.00                                  | 38570.00             | 0.00                         | 3903.00                             | 0.00                             | 0.00                              | 0.00                              | 0.00                              | 1659677.00       | 1171376.00                     | 864881.00          | 0.00                     | 0.00                        | 306495.00                  | 0.00                           | 192508.00                            | 143982.00                        | 0.00            | 48526.00                               | 32728.00                      | 0.00                 | 32728.00                     | 3705.00                   | 102750.00                      | 30605.00                | 26530.00                 | 5966.00              | 39649.00           | 23873.00            | 19666.00    | 4207.00            | 10977.00                    | 66380.00                | 0.00               | 66380.00              | 55380.00        | 0.00                        | 4690.00                 | 5067.00                  | 0.00                    | 3150.00                     | 0.00                            | 38570.00       | 0.00                   | 3903.00                       | 0.00                       | 0.00                        | 0.00                        | 0.00                        |
         | 2022 | 2021 to 2022 | 10192252      | 3424906.00             | 2467694.00                           | 1575657.00               | 0.00                           | 262691.00                         | 589290.00                        | 40056.00                             | 385697.00                                  | 183765.00                              | 133034.00             | 68898.00                                     | 60244.00                            | 0.00                       | 60244.00                           | 9824.00                         | 228507.00                            | 72597.00                      | 87146.00                       | 15500.00                   | 53264.00                 | 61813.00                  | 53027.00          | 8786.00                  | 26172.00                          | 95978.00                      | 0.00                     | 95978.00                    | 88978.00              | 0.00                              | 0.00                          | 1493.00                        | 0.00                          | 10579.00                          | 0.00                                  | 44954.00             | 11754.00                     | 15389.00                            | 2311.00                          | 2498.00                           | 0.00                              | 0.00                              | 3424906.00       | 2467694.00                     | 1575657.00         | 0.00                     | 262691.00                   | 589290.00                  | 40056.00                       | 385697.00                            | 183765.00                        | 133034.00       | 68898.00                               | 60244.00                      | 0.00                 | 60244.00                     | 9824.00                   | 228507.00                      | 72597.00                | 87146.00                 | 15500.00             | 53264.00           | 61813.00            | 53027.00    | 8786.00            | 26172.00                    | 95978.00                | 0.00               | 95978.00              | 88978.00        | 0.00                        | 0.00                    | 1493.00                  | 0.00                    | 10579.00                    | 0.00                            | 44954.00       | 11754.00               | 15389.00                      | 2311.00                    | 2498.00                     | 0.00                        | 0.00                        |

    Scenario: Sending a valid trust expenditure query request
        Given a valid trust expenditure query request with company numbers:
         | CompanyNumber |
         | 10249712      |
         | 10259334      |
         | 10264735      |
        When I submit the insights expenditure request
        Then the trust expenditure query result should be ok and contain:
         | CompanyNumber | SchoolTotalExpenditure | SchoolTotalTeachingSupportStaffCosts | SchoolTeachingStaffCosts | SchoolSupplyTeachingStaffCosts | SchoolEducationalConsultancyCosts | SchoolEducationSupportStaffCosts | SchoolAgencySupplyTeachingStaffCosts | SchoolTotalNonEducationalSupportStaffCosts | SchoolAdministrativeClericalStaffCosts | SchoolOtherStaffCosts | SchoolProfessionalServicesNonCurriculumCosts | SchoolTotalEducationalSuppliesCosts | SchoolExaminationFeesCosts | SchoolLearningResourcesNonIctCosts | SchoolLearningResourcesIctCosts | SchoolTotalPremisesStaffServiceCosts | SchoolCleaningCaretakingCosts | SchoolMaintenancePremisesCosts | SchoolOtherOccupationCosts | SchoolPremisesStaffCosts | SchoolTotalUtilitiesCosts | SchoolEnergyCosts | SchoolWaterSewerageCosts | SchoolAdministrativeSuppliesCosts | SchoolTotalGrossCateringCosts | SchoolCateringStaffCosts | SchoolCateringSuppliesCosts | SchoolTotalOtherCosts | SchoolDirectRevenueFinancingCosts | SchoolGroundsMaintenanceCosts | SchoolIndirectEmployeeExpenses | SchoolInterestChargesLoanBank | SchoolOtherInsurancePremiumsCosts | SchoolPrivateFinanceInitiativeCharges | SchoolRentRatesCosts | SchoolSpecialFacilitiesCosts | SchoolStaffDevelopmentTrainingCosts | SchoolStaffRelatedInsuranceCosts | SchoolSupplyTeacherInsurableCosts | SchoolCommunityFocusedSchoolStaff | SchoolCommunityFocusedSchoolCosts | TotalExpenditure | TotalTeachingSupportStaffCosts | TeachingStaffCosts | SupplyTeachingStaffCosts | EducationalConsultancyCosts | EducationSupportStaffCosts | AgencySupplyTeachingStaffCosts | TotalNonEducationalSupportStaffCosts | AdministrativeClericalStaffCosts | OtherStaffCosts | ProfessionalServicesNonCurriculumCosts | TotalEducationalSuppliesCosts | ExaminationFeesCosts | LearningResourcesNonIctCosts | LearningResourcesIctCosts | TotalPremisesStaffServiceCosts | CleaningCaretakingCosts | MaintenancePremisesCosts | OtherOccupationCosts | PremisesStaffCosts | TotalUtilitiesCosts | EnergyCosts | WaterSewerageCosts | AdministrativeSuppliesCosts | TotalGrossCateringCosts | CateringStaffCosts | CateringSuppliesCosts | TotalOtherCosts | DirectRevenueFinancingCosts | GroundsMaintenanceCosts | IndirectEmployeeExpenses | InterestChargesLoanBank | OtherInsurancePremiumsCosts | PrivateFinanceInitiativeCharges | RentRatesCosts | SpecialFacilitiesCosts | StaffDevelopmentTrainingCosts | StaffRelatedInsuranceCosts | SupplyTeacherInsurableCosts | CommunityFocusedSchoolStaff | CommunityFocusedSchoolCosts |
         | 10249712      | 1736238.00             | 1159085.00                           | 661989.00                | 0.00                           | 229715.00                         | 235975.00                        | 31406.00                             | 115242.00                                  | 104652.00                              | 0.00                  | 10590.00                                     | 90342.00                            | 0.00                       | 90342.00                           | 31955.00                        | 167788.00                            | 39980.00                      | 75405.00                       | 4426.00                    | 47977.00                 | 14361.00                  | 11100.00          | 3261.00                  | 23848.00                          | 53005.00                      | 0.00                     | 53005.00                    | 80611.00              | 0.00                              | 3983.00                       | 29469.00                       | 0.00                          | 8292.00                           | 0.00                                  | 0.00                 | 4127.00                      | 14481.00                            | 7364.00                          | 12895.00                          | 0.00                              | 0.00                              | 1736238.00       | 1159085.00                     | 661989.00          | 0.00                     | 229715.00                   | 235975.00                  | 31406.00                       | 115242.00                            | 104652.00                        | 0.00            | 10590.00                               | 90342.00                      | 0.00                 | 90342.00                     | 31955.00                  | 167788.00                      | 39980.00                | 75405.00                 | 4426.00              | 47977.00           | 14361.00            | 11100.00    | 3261.00            | 23848.00                    | 53005.00                | 0.00               | 53005.00              | 80611.00        | 0.00                        | 3983.00                 | 29469.00                 | 0.00                    | 8292.00                     | 0.00                            | 0.00           | 4127.00                | 14481.00                      | 7364.00                    | 12895.00                    | 0.00                        | 0.00                        |
         | 10259334      | 6721461.00             | 5031792.00                           | 3942005.00               | 17555.00                       | 413074.00                         | 610345.00                        | 48813.00                             | 584231.00                                  | 443094.00                              | 9982.00               | 131155.00                                    | 226509.00                           | 25684.00                   | 200825.00                          | 46261.00                        | 341365.00                            | 129967.00                     | 51592.00                       | 10263.00                   | 149543.00                | 78302.00                  | 73809.00          | 4493.00                  | 37135.00                          | 225194.00                     | 132993.00                | 92201.00                    | 150672.00             | 0.00                              | 0.00                          | 103031.00                      | 0.00                          | 20645.00                          | 0.00                                  | 0.00                 | 4700.00                      | 12711.00                            | 5068.00                          | 4517.00                           | 0.00                              | 0.00                              | 6721461.00       | 5031792.00                     | 3942005.00         | 17555.00                 | 413074.00                   | 610345.00                  | 48813.00                       | 584231.00                            | 443094.00                        | 9982.00         | 131155.00                              | 226509.00                     | 25684.00             | 200825.00                    | 46261.00                  | 341365.00                      | 129967.00               | 51592.00                 | 10263.00             | 149543.00          | 78302.00            | 73809.00    | 4493.00            | 37135.00                    | 225194.00               | 132993.00          | 92201.00              | 150672.00       | 0.00                        | 0.00                    | 103031.00                | 0.00                    | 20645.00                    | 0.00                            | 0.00           | 4700.00                | 12711.00                      | 5068.00                    | 4517.00                     | 0.00                        | 0.00                        |
         | 10264735      | 1587223.00             | 1153185.00                           | 635145.00                | 0.00                           | 56343.00                          | 449776.00                        | 11921.00                             | 129506.00                                  | 106906.00                              | 0.00                  | 22600.00                                     | 65963.00                            | 0.00                       | 65963.00                           | 27564.00                        | 84130.00                             | 26116.00                      | 42852.00                       | 7356.00                    | 7806.00                  | 18165.00                  | 16358.00          | 1807.00                  | 14891.00                          | 51070.00                      | 0.00                     | 51070.00                    | 42747.00              | 0.00                              | 3907.00                       | 2041.00                        | 0.00                          | 5665.00                           | 0.00                                  | 0.00                 | 0.00                         | 1855.00                             | 14051.00                         | 15228.00                          | 0.00                              | 0.00                              | 1587223.00       | 1153185.00                     | 635145.00          | 0.00                     | 56343.00                    | 449776.00                  | 11921.00                       | 129506.00                            | 106906.00                        | 0.00            | 22600.00                               | 65963.00                      | 0.00                 | 65963.00                     | 27564.00                  | 84130.00                       | 26116.00                | 42852.00                 | 7356.00              | 7806.00            | 18165.00            | 16358.00    | 1807.00            | 14891.00                    | 51070.00                | 0.00               | 51070.00              | 42747.00        | 0.00                        | 3907.00                 | 2041.00                  | 0.00                    | 5665.00                     | 0.00                            | 0.00           | 0.00                   | 1855.00                       | 14051.00                   | 15228.00                    | 0.00                        | 0.00                        |
