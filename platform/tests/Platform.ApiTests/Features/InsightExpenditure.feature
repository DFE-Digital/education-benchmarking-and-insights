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
        Given a school expenditure request with urn '990000', category 'TotalExpenditure', dimension 'Actuals' and exclude central services = 'true'
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
          | SchoolTotalNetCateringCosts                  |                        |
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
          | TotalNetCateringCosts                        |                        |
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
        Given a school expenditure request with urn '990000', category 'TotalExpenditure', dimension 'Actuals' and exclude central services = ''
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
          | SchoolTotalNetCateringCosts                  |                        |
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
          | TotalNetCateringCosts                        |                        |
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
        Given a school expenditure request with urn '990000', category '', dimension 'Actuals' and exclude central services = ''
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
          | SchoolTotalNetCateringCosts                  | 91847.00               |
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
          | TotalNetCateringCosts                        | 91847.00               |
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

    Scenario: Sending an valid school expenditure request with bad URN
        Given a school expenditure request with urn '0000000', category '', dimension 'Actuals' and exclude central services = ''
        When I submit the insights expenditure request
        Then the school expenditure result should be not found

    Scenario: Sending an invalid school expenditure request
        Given a school expenditure request with urn '990000', category 'Invalid', dimension '' and exclude central services = ''
        When I submit the insights expenditure request
        Then the school expenditure result should be bad request

    Scenario: Sending a valid school expenditure history request
        Given a valid school expenditure history request with urn '990000'
        When I submit the insights expenditure request
        Then the school expenditure history result should be ok and contain:
          | Year | Term         | URN    | SchoolTotalExpenditure | SchoolTotalTeachingSupportStaffCosts | SchoolTeachingStaffCosts | SchoolSupplyTeachingStaffCosts | SchoolEducationalConsultancyCosts | SchoolEducationSupportStaffCosts | SchoolAgencySupplyTeachingStaffCosts | SchoolTotalNonEducationalSupportStaffCosts | SchoolAdministrativeClericalStaffCosts | SchoolOtherStaffCosts | SchoolProfessionalServicesNonCurriculumCosts | SchoolTotalEducationalSuppliesCosts | SchoolExaminationFeesCosts | SchoolLearningResourcesNonIctCosts | SchoolLearningResourcesIctCosts | SchoolTotalPremisesStaffServiceCosts | SchoolCleaningCaretakingCosts | SchoolMaintenancePremisesCosts | SchoolOtherOccupationCosts | SchoolPremisesStaffCosts | SchoolTotalUtilitiesCosts | SchoolEnergyCosts | SchoolWaterSewerageCosts | SchoolAdministrativeSuppliesCosts | SchoolTotalGrossCateringCosts | SchoolTotalNetCateringCosts | SchoolCateringStaffCosts | SchoolCateringSuppliesCosts | SchoolTotalOtherCosts | SchoolDirectRevenueFinancingCosts | SchoolGroundsMaintenanceCosts | SchoolIndirectEmployeeExpenses | SchoolInterestChargesLoanBank | SchoolOtherInsurancePremiumsCosts | SchoolPrivateFinanceInitiativeCharges | SchoolRentRatesCosts | SchoolSpecialFacilitiesCosts | SchoolStaffDevelopmentTrainingCosts | SchoolStaffRelatedInsuranceCosts | SchoolSupplyTeacherInsurableCosts | SchoolCommunityFocusedSchoolStaff | SchoolCommunityFocusedSchoolCosts | TotalExpenditure | TotalTeachingSupportStaffCosts | TeachingStaffCosts | SupplyTeachingStaffCosts | EducationalConsultancyCosts | EducationSupportStaffCosts | AgencySupplyTeachingStaffCosts | TotalNonEducationalSupportStaffCosts | AdministrativeClericalStaffCosts | OtherStaffCosts | ProfessionalServicesNonCurriculumCosts | TotalEducationalSuppliesCosts | ExaminationFeesCosts | LearningResourcesNonIctCosts | LearningResourcesIctCosts | TotalPremisesStaffServiceCosts | CleaningCaretakingCosts | MaintenancePremisesCosts | OtherOccupationCosts | PremisesStaffCosts | TotalUtilitiesCosts | EnergyCosts | WaterSewerageCosts | AdministrativeSuppliesCosts | TotalGrossCateringCosts | TotalNetCateringCosts | CateringStaffCosts | CateringSuppliesCosts | TotalOtherCosts | DirectRevenueFinancingCosts | GroundsMaintenanceCosts | IndirectEmployeeExpenses | InterestChargesLoanBank | OtherInsurancePremiumsCosts | PrivateFinanceInitiativeCharges | RentRatesCosts | SpecialFacilitiesCosts | StaffDevelopmentTrainingCosts | StaffRelatedInsuranceCosts | SupplyTeacherInsurableCosts | CommunityFocusedSchoolStaff | CommunityFocusedSchoolCosts |
          | 2018 | 2017 to 2018 | 990000 |                        |                                      |                          |                                |                                   |                                  |                                      |                                            |                                        |                       |                                              |                                     |                            |                                    |                                 |                                      |                               |                                |                            |                          |                           |                   |                          |                                   |                               |                             |                          |                             |                       |                                   |                               |                                |                               |                                   |                                       |                      |                              |                                     |                                  |                                   |                                   |                                   |                  |                                |                    |                          |                             |                            |                                |                                      |                                  |                 |                                        |                               |                      |                              |                           |                                |                         |                          |                      |                    |                     |             |                    |                             |                         |                       |                    |                       |                 |                             |                         |                          |                         |                             |                                 |                |                        |                               |                            |                             |                             |                             |
          | 2019 | 2018 to 2019 | 990000 |                        |                                      |                          |                                |                                   |                                  |                                      |                                            |                                        |                       |                                              |                                     |                            |                                    |                                 |                                      |                               |                                |                            |                          |                           |                   |                          |                                   |                               |                             |                          |                             |                       |                                   |                               |                                |                               |                                   |                                       |                      |                              |                                     |                                  |                                   |                                   |                                   |                  |                                |                    |                          |                             |                            |                                |                                      |                                  |                 |                                        |                               |                      |                              |                           |                                |                         |                          |                      |                    |                     |             |                    |                             |                         |                       |                    |                       |                 |                             |                         |                          |                         |                             |                                 |                |                        |                               |                            |                             |                             |                             |
          | 2020 | 2019 to 2020 | 990000 |                        |                                      |                          |                                |                                   |                                  |                                      |                                            |                                        |                       |                                              |                                     |                            |                                    |                                 |                                      |                               |                                |                            |                          |                           |                   |                          |                                   |                               |                             |                          |                             |                       |                                   |                               |                                |                               |                                   |                                       |                      |                              |                                     |                                  |                                   |                                   |                                   |                  |                                |                    |                          |                             |                            |                                |                                      |                                  |                 |                                        |                               |                      |                              |                           |                                |                         |                          |                      |                    |                     |             |                    |                             |                         |                       |                    |                       |                 |                             |                         |                          |                         |                             |                                 |                |                        |                               |                            |                             |                             |                             |
          | 2021 | 2020 to 2021 | 990000 | 1352665.00             | 947543.00                            | 695259.00                | 0.00                           | 14407.00                          | 134473.00                        | 103404.00                            | 151028.00                                  | 98073.00                               | 9316.00               | 43639.00                                     | 10737.00                            | 0.00                       | 10737.00                           | 1236.00                         | 83002.00                             | 31170.00                      | 44765.00                       | 1089.00                    | 5978.00                  | 46713.00                  | 43198.00          | 3515.00                  | 7677.00                           | 67912.00                      | 69690.00                    | 0.00                     | 67912.00                    | 36817.00              | 0.00                              | 8211.00                       | 1405.00                        | 0.00                          | 2865.00                           | 0.00                                  | 15763.00             | 0.00                         | 8573.00                             | 0.00                             | 0.00                              | 0.00                              | 0.00                              | 1352665.00       | 947543.00                      | 695259.00          | 0.00                     | 14407.00                    | 134473.00                  | 103404.00                      | 151028.00                            | 98073.00                         | 9316.00         | 43639.00                               | 10737.00                      | 0.00                 | 10737.00                     | 1236.00                   | 83002.00                       | 31170.00                | 44765.00                 | 1089.00              | 5978.00            | 46713.00            | 43198.00    | 3515.00            | 7677.00                     | 67912.00                | 69690.00              | 0.00               | 67912.00              | 36817.00        | 0.00                        | 8211.00                 | 1405.00                  | 0.00                    | 2865.00                     | 0.00                            | 15763.00       | 0.00                   | 8573.00                       | 0.00                       | 0.00                        | 0.00                        | 0.00                        |
          | 2022 | 2021 to 2022 | 990000 | 2790504.00             | 2044335.00                           | 1437007.00               | 0.00                           | 208376.00                         | 301653.00                        | 97299.00                             | 202341.00                                  | 157417.00                              | 12915.00              | 32009.00                                     | 70108.00                            | 0.00                       | 70108.00                           | 24200.00                        | 166932.00                            | 47240.00                      | 33774.00                       | 33176.00                   | 52742.00                 | 31001.00                  | 26060.00          | 4941.00                  | 28505.00                          | 84186.00                      | 91847.00                    | 2170.00                  | 82016.00                    | 141903.00             | 3006.00                           | 615.00                        | 38374.00                       | 0.00                          | 11862.00                          | 0.00                                  | 73416.00             | 0.00                         | 9115.00                             | 2735.00                          | 2780.00                           | 0.00                              | 0.00                              | 2790504.00       | 2044335.00                     | 1437007.00         | 0.00                     | 208376.00                   | 301653.00                  | 97299.00                       | 202341.00                            | 157417.00                        | 12915.00        | 32009.00                               | 70108.00                      | 0.00                 | 70108.00                     | 24200.00                  | 166932.00                      | 47240.00                | 33774.00                 | 33176.00             | 52742.00           | 31001.00            | 26060.00    | 4941.00            | 28505.00                    | 84186.00                | 91847.00              | 2170.00            | 82016.00              | 141903.00       | 3006.00                     | 615.00                  | 38374.00                 | 0.00                    | 11862.00                    | 0.00                            | 73416.00       | 0.00                   | 9115.00                       | 2735.00                    | 2780.00                     | 0.00                        | 0.00                        |

    Scenario: Sending a valid school expenditure query request with URNs
        Given a valid school expenditure query request with urns:
          | Urn    |
          | 990000 |
          | 990001 |
          | 990002 |
        When I submit the insights expenditure request
        Then the school expenditure query result should be ok and contain:
          | SchoolName      | SchoolType                     | LAName        | URN    | SchoolName      | SchoolType                     | LAName        | TotalPupils | TotalInternalFloorArea | SchoolTotalExpenditure | SchoolTotalTeachingSupportStaffCosts | SchoolTeachingStaffCosts | SchoolSupplyTeachingStaffCosts | SchoolEducationalConsultancyCosts | SchoolEducationSupportStaffCosts | SchoolAgencySupplyTeachingStaffCosts | SchoolTotalNonEducationalSupportStaffCosts | SchoolAdministrativeClericalStaffCosts | SchoolOtherStaffCosts | SchoolProfessionalServicesNonCurriculumCosts | SchoolTotalEducationalSuppliesCosts | SchoolExaminationFeesCosts | SchoolLearningResourcesNonIctCosts | SchoolLearningResourcesIctCosts | SchoolTotalPremisesStaffServiceCosts | SchoolCleaningCaretakingCosts | SchoolMaintenancePremisesCosts | SchoolOtherOccupationCosts | SchoolPremisesStaffCosts | SchoolTotalUtilitiesCosts | SchoolEnergyCosts | SchoolWaterSewerageCosts | SchoolAdministrativeSuppliesCosts | SchoolTotalGrossCateringCosts | SchoolTotalNetCateringCosts | SchoolCateringStaffCosts | SchoolCateringSuppliesCosts | SchoolTotalOtherCosts | SchoolDirectRevenueFinancingCosts | SchoolGroundsMaintenanceCosts | SchoolIndirectEmployeeExpenses | SchoolInterestChargesLoanBank | SchoolOtherInsurancePremiumsCosts | SchoolPrivateFinanceInitiativeCharges | SchoolRentRatesCosts | SchoolSpecialFacilitiesCosts | SchoolStaffDevelopmentTrainingCosts | SchoolStaffRelatedInsuranceCosts | SchoolSupplyTeacherInsurableCosts | SchoolCommunityFocusedSchoolStaff | SchoolCommunityFocusedSchoolCosts | TotalExpenditure | TotalTeachingSupportStaffCosts | TeachingStaffCosts | SupplyTeachingStaffCosts | EducationalConsultancyCosts | EducationSupportStaffCosts | AgencySupplyTeachingStaffCosts | TotalNonEducationalSupportStaffCosts | AdministrativeClericalStaffCosts | OtherStaffCosts | ProfessionalServicesNonCurriculumCosts | TotalEducationalSuppliesCosts | ExaminationFeesCosts | LearningResourcesNonIctCosts | LearningResourcesIctCosts | TotalPremisesStaffServiceCosts | CleaningCaretakingCosts | MaintenancePremisesCosts | OtherOccupationCosts | PremisesStaffCosts | TotalUtilitiesCosts | EnergyCosts | WaterSewerageCosts | AdministrativeSuppliesCosts | TotalGrossCateringCosts | TotalNetCateringCosts | CateringStaffCosts | CateringSuppliesCosts | TotalOtherCosts | DirectRevenueFinancingCosts | GroundsMaintenanceCosts | IndirectEmployeeExpenses | InterestChargesLoanBank | OtherInsurancePremiumsCosts | PrivateFinanceInitiativeCharges | RentRatesCosts | SpecialFacilitiesCosts | StaffDevelopmentTrainingCosts | StaffRelatedInsuranceCosts | SupplyTeacherInsurableCosts | CommunityFocusedSchoolStaff | CommunityFocusedSchoolCosts |
          | Test school 176 | Voluntary aided school         | Bedford       | 990000 | Test school 176 | Voluntary aided school         | Bedford       | 418.00      | 3021.00                | 2790504.00             | 2044335.00                           | 1437007.00               | 0.00                           | 208376.00                         | 301653.00                        | 97299.00                             | 202341.00                                  | 157417.00                              | 12915.00              | 32009.00                                     | 70108.00                            | 0.00                       | 70108.00                           | 24200.00                        | 166932.00                            | 47240.00                      | 33774.00                       | 33176.00                   | 52742.00                 | 31001.00                  | 26060.00          | 4941.00                  | 28505.00                          | 84186.00                      | 91847.00                    | 2170.00                  | 82016.00                    | 141903.00             | 3006.00                           | 615.00                        | 38374.00                       | 0.00                          | 11862.00                          | 0.00                                  | 73416.00             | 0.00                         | 9115.00                             | 2735.00                          | 2780.00                           | 0.00                              | 0.00                              | 2790504.00       | 2044335.00                     | 1437007.00         | 0.00                     | 208376.00                   | 301653.00                  | 97299.00                       | 202341.00                            | 157417.00                        | 12915.00        | 32009.00                               | 70108.00                      | 0.00                 | 70108.00                     | 24200.00                  | 166932.00                      | 47240.00                | 33774.00                 | 33176.00             | 52742.00           | 31001.00            | 26060.00    | 4941.00            | 28505.00                    | 84186.00                | 91847.00              | 2170.00            | 82016.00              | 141903.00       | 3006.00                     | 615.00                  | 38374.00                 | 0.00                    | 11862.00                    | 0.00                            | 73416.00       | 0.00                   | 9115.00                       | 2735.00                    | 2780.00                     | 0.00                        | 0.00                        |
          | Test school 241 | Voluntary aided school         | Islington     | 990001 | Test school 241 | Voluntary aided school         | Islington     | 303.00      | 2476.00                | 2054918.00             | 1521798.00                           | 997855.00                | 0.00                           | 220708.00                         | 278722.00                        | 24513.00                             | 160770.00                                  | 92011.00                               | 28511.00              | 40248.00                                     | 46521.00                            | 0.00                       | 46521.00                           | 10779.00                        | 143345.00                            | 4254.00                       | 38484.00                       | 8972.00                    | 91635.00                 | 23799.00                  | 21324.00          | 2475.00                  | 21815.00                          | 71734.00                      | 90089.00                    | 0.00                     | 71734.00                    | 54357.00              | 0.00                              | 407.00                        | 6586.00                        | 0.00                          | 10274.00                          | 0.00                                  | 0.00                 | 2695.00                      | 1648.00                             | 8736.00                          | 24011.00                          | 0.00                              | 0.00                              | 2054918.00       | 1521798.00                     | 997855.00          | 0.00                     | 220708.00                   | 278722.00                  | 24513.00                       | 160770.00                            | 92011.00                         | 28511.00        | 40248.00                               | 46521.00                      | 0.00                 | 46521.00                     | 10779.00                  | 143345.00                      | 4254.00                 | 38484.00                 | 8972.00              | 91635.00           | 23799.00            | 21324.00    | 2475.00            | 21815.00                    | 71734.00                | 90089.00              | 0.00               | 71734.00              | 54357.00        | 0.00                        | 407.00                  | 6586.00                  | 0.00                    | 10274.00                    | 0.00                            | 0.00           | 2695.00                | 1648.00                       | 8736.00                    | 24011.00                    | 0.00                        | 0.00                        |
          | Test school 224 | Local authority nursery school | Isle of Wight | 990002 | Test school 224 | Local authority nursery school | Isle of Wight | 191.00      | 2552.00                | 1719060.00             | 1189016.00                           | 733333.00                | 0.00                           | 151630.00                         | 161948.00                        | 142105.00                            | 137246.00                                  | 103006.00                              | 13488.00              | 20752.00                                     | 24992.00                            | 0.00                       | 24992.00                           | 14462.00                        | 110661.00                            | 35684.00                      | 22248.00                       | 6416.00                    | 46313.00                 | 42196.00                  | 37236.00          | 4960.00                  | 17131.00                          | 48949.00                      | 57188.00                    | 0.00                     | 48949.00                    | 134408.00             | 0.00                              | 0.00                          | 26539.00                       | 0.00                          | 6125.00                           | 0.00                                  | 22580.00             | 1287.00                      | 3568.00                             | 7587.00                          | 18030.00                          | 47010.00                          | 1682.00                           | 1719060.00       | 1189016.00                     | 733333.00          | 0.00                     | 151630.00                   | 161948.00                  | 142105.00                      | 137246.00                            | 103006.00                        | 13488.00        | 20752.00                               | 24992.00                      | 0.00                 | 24992.00                     | 14462.00                  | 110661.00                      | 35684.00                | 22248.00                 | 6416.00              | 46313.00           | 42196.00            | 37236.00    | 4960.00            | 17131.00                    | 48949.00                | 57188.00              | 0.00               | 48949.00              | 134408.00       | 0.00                        | 0.00                    | 26539.00                 | 0.00                    | 6125.00                     | 0.00                            | 22580.00       | 1287.00                | 3568.00                       | 7587.00                    | 18030.00                    | 47010.00                    | 1682.00                     |

    Scenario: Sending a valid school expenditure query request with company number and phase
        Given a valid school expenditure query request with company number '8104190' and phase 'Secondary':
        When I submit the insights expenditure request
        Then the school expenditure query result should be ok and contain:
          | SchoolName             | SchoolType        | LAName                 | URN    | SchoolName             | SchoolType        | LAName                 | TotalPupils | TotalInternalFloorArea | SchoolTotalExpenditure | SchoolTotalTeachingSupportStaffCosts | SchoolTeachingStaffCosts | SchoolSupplyTeachingStaffCosts | SchoolEducationalConsultancyCosts | SchoolEducationSupportStaffCosts | SchoolAgencySupplyTeachingStaffCosts | SchoolTotalNonEducationalSupportStaffCosts | SchoolAdministrativeClericalStaffCosts | SchoolOtherStaffCosts | SchoolProfessionalServicesNonCurriculumCosts | SchoolTotalEducationalSuppliesCosts | SchoolExaminationFeesCosts | SchoolLearningResourcesNonIctCosts | SchoolLearningResourcesIctCosts | SchoolTotalPremisesStaffServiceCosts | SchoolCleaningCaretakingCosts | SchoolMaintenancePremisesCosts | SchoolOtherOccupationCosts | SchoolPremisesStaffCosts | SchoolTotalUtilitiesCosts | SchoolEnergyCosts | SchoolWaterSewerageCosts | SchoolAdministrativeSuppliesCosts | SchoolTotalGrossCateringCosts | SchoolTotalNetCateringCosts | SchoolCateringStaffCosts | SchoolCateringSuppliesCosts | SchoolTotalOtherCosts | SchoolDirectRevenueFinancingCosts | SchoolGroundsMaintenanceCosts | SchoolIndirectEmployeeExpenses | SchoolInterestChargesLoanBank | SchoolOtherInsurancePremiumsCosts | SchoolPrivateFinanceInitiativeCharges | SchoolRentRatesCosts | SchoolSpecialFacilitiesCosts | SchoolStaffDevelopmentTrainingCosts | SchoolStaffRelatedInsuranceCosts | SchoolSupplyTeacherInsurableCosts | SchoolCommunityFocusedSchoolStaff | SchoolCommunityFocusedSchoolCosts | TotalExpenditure | TotalTeachingSupportStaffCosts | TeachingStaffCosts | SupplyTeachingStaffCosts | EducationalConsultancyCosts | EducationSupportStaffCosts | AgencySupplyTeachingStaffCosts | TotalNonEducationalSupportStaffCosts | AdministrativeClericalStaffCosts | OtherStaffCosts | ProfessionalServicesNonCurriculumCosts | TotalEducationalSuppliesCosts | ExaminationFeesCosts | LearningResourcesNonIctCosts | LearningResourcesIctCosts | TotalPremisesStaffServiceCosts | CleaningCaretakingCosts | MaintenancePremisesCosts | OtherOccupationCosts | PremisesStaffCosts | TotalUtilitiesCosts | EnergyCosts | WaterSewerageCosts | AdministrativeSuppliesCosts | TotalGrossCateringCosts | TotalNetCateringCosts | CateringStaffCosts | CateringSuppliesCosts | TotalOtherCosts | DirectRevenueFinancingCosts | GroundsMaintenanceCosts | IndirectEmployeeExpenses | InterestChargesLoanBank | OtherInsurancePremiumsCosts | PrivateFinanceInitiativeCharges | RentRatesCosts | SpecialFacilitiesCosts | StaffDevelopmentTrainingCosts | StaffRelatedInsuranceCosts | SupplyTeacherInsurableCosts | CommunityFocusedSchoolStaff | CommunityFocusedSchoolCosts |
          | Test academy school 87 | Academy converter | City of London         | 777051 | Test academy school 87 | Academy converter | City of London         | 335.00      | 2951.00                | 2597481.00             | 1863218.00                           | 1157123.00               | 0.00                           | 358226.00                         | 323874.00                        | 23995.00                             | 175298.00                                  | 96346.00                               | 45730.00              | 33222.00                                     | 37281.00                            | 0.00                       | 37281.00                           | 21124.00                        | 218922.00                            | 37501.00                      | 46134.00                       | 9861.00                    | 125426.00                | 29951.00                  | 24869.00          | 5082.00                  | 25879.00                          | 83265.00                      | 95658.00                    | 0.00                     | 83265.00                    | 145676.00             | 3133.00                           | 2904.00                       | 42756.00                       | 0.00                          | 9563.00                           | 0.00                                  | 61712.00             | 7762.00                      | 12700.00                            | 2873.00                          | 2273.00                           | 0.00                              | 0.00                              | 2597481.00       | 1863218.00                     | 1157123.00         | 0.00                     | 358226.00                   | 323874.00                  | 23995.00                       | 175298.00                            | 96346.00                         | 45730.00        | 33222.00                               | 37281.00                      | 0.00                 | 37281.00                     | 21124.00                  | 218922.00                      | 37501.00                | 46134.00                 | 9861.00              | 125426.00          | 29951.00            | 24869.00    | 5082.00            | 25879.00                    | 83265.00                | 95658.00              | 0.00               | 83265.00              | 145676.00       | 3133.00                     | 2904.00                 | 42756.00                 | 0.00                    | 9563.00                     | 0.00                            | 61712.00       | 7762.00                | 12700.00                      | 2873.00                    | 2273.00                     | 0.00                        | 0.00                        |
          | Test academy school 90 | Academy converter | Camden                 | 777052 | Test academy school 90 | Academy converter | Camden                 | 191.00      | 2552.00                | 1719060.00             | 1189016.00                           | 733333.00                | 0.00                           | 151630.00                         | 161948.00                        | 142105.00                            | 137246.00                                  | 103006.00                              | 13488.00              | 20752.00                                     | 24992.00                            | 0.00                       | 24992.00                           | 14462.00                        | 110661.00                            | 35684.00                      | 22248.00                       | 6416.00                    | 46313.00                 | 42196.00                  | 37236.00          | 4960.00                  | 17131.00                          | 48949.00                      | 57188.00                    | 0.00                     | 48949.00                    | 134408.00             | 0.00                              | 0.00                          | 26539.00                       | 0.00                          | 6125.00                           | 0.00                                  | 22580.00             | 1287.00                      | 3568.00                             | 7587.00                          | 18030.00                          | 47010.00                          | 1682.00                           | 1719060.00       | 1189016.00                     | 733333.00          | 0.00                     | 151630.00                   | 161948.00                  | 142105.00                      | 137246.00                            | 103006.00                        | 13488.00        | 20752.00                               | 24992.00                      | 0.00                 | 24992.00                     | 14462.00                  | 110661.00                      | 35684.00                | 22248.00                 | 6416.00              | 46313.00           | 42196.00            | 37236.00    | 4960.00            | 17131.00                    | 48949.00                | 57188.00              | 0.00               | 48949.00              | 134408.00       | 0.00                        | 0.00                    | 26539.00                 | 0.00                    | 6125.00                     | 0.00                            | 22580.00       | 1287.00                | 3568.00                       | 7587.00                    | 18030.00                    | 47010.00                    | 1682.00                     |
          | Test academy school 91 | Academy converter | Greenwich              | 777053 | Test academy school 91 | Academy converter | Greenwich              | 230.00      | 1922.00                | 1674539.00             | 1215196.00                           | 800314.00                | 0.00                           | 94840.00                          | 318684.00                        | 1358.00                              | 106876.00                                  | 87184.00                               | 0.00                  | 19692.00                                     | 30299.00                            | 0.00                       | 30299.00                           | 22358.00                        | 106281.00                            | 26771.00                      | 32641.00                       | 7371.00                    | 39498.00                 | 27664.00                  | 17402.00          | 10262.00                 | 8607.00                           | 50043.00                      | 57329.00                    | 0.00                     | 50043.00                    | 107214.00             | 0.00                              | 7997.00                       | 29424.00                       | 0.00                          | 5984.00                           | 0.00                                  | 39955.00             | 88.00                        | 5478.00                             | 1919.00                          | 16369.00                          | 0.00                              | 0.00                              | 1674539.00       | 1215196.00                     | 800314.00          | 0.00                     | 94840.00                    | 318684.00                  | 1358.00                        | 106876.00                            | 87184.00                         | 0.00            | 19692.00                               | 30299.00                      | 0.00                 | 30299.00                     | 22358.00                  | 106281.00                      | 26771.00                | 32641.00                 | 7371.00              | 39498.00           | 27664.00            | 17402.00    | 10262.00           | 8607.00                     | 50043.00                | 57329.00              | 0.00               | 50043.00              | 107214.00       | 0.00                        | 7997.00                 | 29424.00                 | 0.00                    | 5984.00                     | 0.00                            | 39955.00       | 88.00                  | 5478.00                       | 1919.00                    | 16369.00                    | 0.00                        | 0.00                        |
          | Test academy school 92 | Free school       | Hackney                | 777054 | Test academy school 92 | Free school       | Hackney                | 216.00      | 2135.00                | 1680594.00             | 1115052.00                           | 703564.00                | 0.00                           | 79648.00                          | 299829.00                        | 32011.00                             | 180507.00                                  | 82949.00                               | 80178.00              | 17380.00                                     | 41535.00                            | 0.00                       | 41535.00                           | 5431.00                         | 92731.00                             | 26025.00                      | 23171.00                       | 9483.00                    | 34052.00                 | 21147.00                  | 20654.00          | 493.00                   | 8179.00                           | 52373.00                      | 63191.00                    | 0.00                     | 52373.00                    | 163640.00             | 0.00                              | 328.00                        | 31603.00                       | 0.00                          | 6225.00                           | 0.00                                  | 92036.00             | 6525.00                      | 1236.00                             | 10488.00                         | 15199.00                          | 0.00                              | 0.00                              | 1680594.00       | 1115052.00                     | 703564.00          | 0.00                     | 79648.00                    | 299829.00                  | 32011.00                       | 180507.00                            | 82949.00                         | 80178.00        | 17380.00                               | 41535.00                      | 0.00                 | 41535.00                     | 5431.00                   | 92731.00                       | 26025.00                | 23171.00                 | 9483.00              | 34052.00           | 21147.00            | 20654.00    | 493.00             | 8179.00                     | 52373.00                | 63191.00              | 0.00               | 52373.00              | 163640.00       | 0.00                        | 328.00                  | 31603.00                 | 0.00                    | 6225.00                     | 0.00                            | 92036.00       | 6525.00                | 1236.00                       | 10488.00                   | 15199.00                    | 0.00                        | 0.00                        |
          | Test academy school 93 | Academy converter | Hammersmith and Fulham | 777055 | Test academy school 93 | Academy converter | Hammersmith and Fulham | 399.00      | 5754.00                | 3424906.00             | 2467694.00                           | 1575657.00               | 0.00                           | 262691.00                         | 589290.00                        | 40056.00                             | 385697.00                                  | 183765.00                              | 133034.00             | 68898.00                                     | 60244.00                            | 0.00                       | 60244.00                           | 9824.00                         | 228507.00                            | 72597.00                      | 87146.00                       | 15500.00                   | 53264.00                 | 61813.00                  | 53027.00          | 8786.00                  | 26172.00                          | 95978.00                      | 109578.00                   | 0.00                     | 95978.00                    | 88978.00              | 0.00                              | 0.00                          | 1493.00                        | 0.00                          | 10579.00                          | 0.00                                  | 44954.00             | 11754.00                     | 15389.00                            | 2311.00                          | 2498.00                           | 0.00                              | 0.00                              | 3424906.00       | 2467694.00                     | 1575657.00         | 0.00                     | 262691.00                   | 589290.00                  | 40056.00                       | 385697.00                            | 183765.00                        | 133034.00       | 68898.00                               | 60244.00                      | 0.00                 | 60244.00                     | 9824.00                   | 228507.00                      | 72597.00                | 87146.00                 | 15500.00             | 53264.00           | 61813.00            | 53027.00    | 8786.00            | 26172.00                    | 95978.00                | 109578.00             | 0.00               | 95978.00              | 88978.00        | 0.00                        | 0.00                    | 1493.00                  | 0.00                    | 10579.00                    | 0.00                            | 44954.00       | 11754.00               | 15389.00                      | 2311.00                    | 2498.00                     | 0.00                        | 0.00                        |
          | Test academy school 94 | Academy converter | Islington              | 777056 | Test academy school 94 | Academy converter | Islington              | 339.00      | 2120.00                | 2480660.00             | 1823492.00                           | 1259123.00               | 10292.00                       | 118624.00                         | 377589.00                        | 57864.00                             | 225264.00                                  | 104273.00                              | 51652.00              | 69339.00                                     | 51100.00                            | 0.00                       | 51100.00                           | 6303.00                         | 98730.00                             | 59421.00                      | 28214.00                       | 11095.00                   | 0.00                     | 30179.00                  | 23499.00          | 6680.00                  | 23384.00                          | 54988.00                      | 69488.00                    | 0.00                     | 54988.00                    | 167220.00             | 0.00                              | 4991.00                       | 51131.00                       | 0.00                          | 9348.00                           | 0.00                                  | 58656.00             | 9900.00                      | 1424.00                             | 2688.00                          | 29082.00                          | 0.00                              | 0.00                              | 2480660.00       | 1823492.00                     | 1259123.00         | 10292.00                 | 118624.00                   | 377589.00                  | 57864.00                       | 225264.00                            | 104273.00                        | 51652.00        | 69339.00                               | 51100.00                      | 0.00                 | 51100.00                     | 6303.00                   | 98730.00                       | 59421.00                | 28214.00                 | 11095.00             | 0.00               | 30179.00            | 23499.00    | 6680.00            | 23384.00                    | 54988.00                | 69488.00              | 0.00               | 54988.00              | 167220.00       | 0.00                        | 4991.00                 | 51131.00                 | 0.00                    | 9348.00                     | 0.00                            | 58656.00       | 9900.00                | 1424.00                       | 2688.00                    | 29082.00                    | 0.00                        | 0.00                        |

    Scenario: Sending a valid school expenditure query request with LA code and phase
        Given a valid school expenditure query request with LA code '205' and phase 'Secondary':
        When I submit the insights expenditure request
        Then the school expenditure query result should be ok and contain:
          | SchoolName             | SchoolType        | LAName                 | URN    | SchoolName             | SchoolType        | LAName                 | TotalPupils | TotalInternalFloorArea | SchoolTotalExpenditure | SchoolTotalTeachingSupportStaffCosts | SchoolTeachingStaffCosts | SchoolSupplyTeachingStaffCosts | SchoolEducationalConsultancyCosts | SchoolEducationSupportStaffCosts | SchoolAgencySupplyTeachingStaffCosts | SchoolTotalNonEducationalSupportStaffCosts | SchoolAdministrativeClericalStaffCosts | SchoolOtherStaffCosts | SchoolProfessionalServicesNonCurriculumCosts | SchoolTotalEducationalSuppliesCosts | SchoolExaminationFeesCosts | SchoolLearningResourcesNonIctCosts | SchoolLearningResourcesIctCosts | SchoolTotalPremisesStaffServiceCosts | SchoolCleaningCaretakingCosts | SchoolMaintenancePremisesCosts | SchoolOtherOccupationCosts | SchoolPremisesStaffCosts | SchoolTotalUtilitiesCosts | SchoolEnergyCosts | SchoolWaterSewerageCosts | SchoolAdministrativeSuppliesCosts | SchoolTotalGrossCateringCosts | SchoolTotalNetCateringCosts | SchoolCateringStaffCosts | SchoolCateringSuppliesCosts | SchoolTotalOtherCosts | SchoolDirectRevenueFinancingCosts | SchoolGroundsMaintenanceCosts | SchoolIndirectEmployeeExpenses | SchoolInterestChargesLoanBank | SchoolOtherInsurancePremiumsCosts | SchoolPrivateFinanceInitiativeCharges | SchoolRentRatesCosts | SchoolSpecialFacilitiesCosts | SchoolStaffDevelopmentTrainingCosts | SchoolStaffRelatedInsuranceCosts | SchoolSupplyTeacherInsurableCosts | SchoolCommunityFocusedSchoolStaff | SchoolCommunityFocusedSchoolCosts | TotalExpenditure | TotalTeachingSupportStaffCosts | TeachingStaffCosts | SupplyTeachingStaffCosts | EducationalConsultancyCosts | EducationSupportStaffCosts | AgencySupplyTeachingStaffCosts | TotalNonEducationalSupportStaffCosts | AdministrativeClericalStaffCosts | OtherStaffCosts | ProfessionalServicesNonCurriculumCosts | TotalEducationalSuppliesCosts | ExaminationFeesCosts | LearningResourcesNonIctCosts | LearningResourcesIctCosts | TotalPremisesStaffServiceCosts | CleaningCaretakingCosts | MaintenancePremisesCosts | OtherOccupationCosts | PremisesStaffCosts | TotalUtilitiesCosts | EnergyCosts | WaterSewerageCosts | AdministrativeSuppliesCosts | TotalGrossCateringCosts | TotalNetCateringCosts | CateringStaffCosts | CateringSuppliesCosts | TotalOtherCosts | DirectRevenueFinancingCosts | GroundsMaintenanceCosts | IndirectEmployeeExpenses | InterestChargesLoanBank | OtherInsurancePremiumsCosts | PrivateFinanceInitiativeCharges | RentRatesCosts | SpecialFacilitiesCosts | StaffDevelopmentTrainingCosts | StaffRelatedInsuranceCosts | SupplyTeacherInsurableCosts | CommunityFocusedSchoolStaff | CommunityFocusedSchoolCosts |
          | Test academy school 93 | Academy converter | Hammersmith and Fulham | 777055 | Test academy school 93 | Academy converter | Hammersmith and Fulham | 399.00      | 5754.00                | 3424906.00             | 2467694.00                           | 1575657.00               | 0.00                           | 262691.00                         | 589290.00                        | 40056.00                             | 385697.00                                  | 183765.00                              | 133034.00             | 68898.00                                     | 60244.00                            | 0.00                       | 60244.00                           | 9824.00                         | 228507.00                            | 72597.00                      | 87146.00                       | 15500.00                   | 53264.00                 | 61813.00                  | 53027.00          | 8786.00                  | 26172.00                          | 95978.00                      | 109578.00                   | 0.00                     | 95978.00                    | 88978.00              | 0.00                              | 0.00                          | 1493.00                        | 0.00                          | 10579.00                          | 0.00                                  | 44954.00             | 11754.00                     | 15389.00                            | 2311.00                          | 2498.00                           | 0.00                              | 0.00                              | 3424906.00       | 2467694.00                     | 1575657.00         | 0.00                     | 262691.00                   | 589290.00                  | 40056.00                       | 385697.00                            | 183765.00                        | 133034.00       | 68898.00                               | 60244.00                      | 0.00                 | 60244.00                     | 9824.00                   | 228507.00                      | 72597.00                | 87146.00                 | 15500.00             | 53264.00           | 61813.00            | 53027.00    | 8786.00            | 26172.00                    | 95978.00                | 109578.00             | 0.00               | 95978.00              | 88978.00        | 0.00                        | 0.00                    | 1493.00                  | 0.00                    | 10579.00                    | 0.00                            | 44954.00       | 11754.00               | 15389.00                      | 2311.00                    | 2498.00                     | 0.00                        | 0.00                        |

    Scenario: Sending a valid trust expenditure request with category, dimension and exclude central services
        Given a trust expenditure request with company number '10192252', category 'TotalExpenditure', dimension 'Actuals' and exclude central services = 'true'
        When I submit the insights expenditure request
        Then the trust expenditure result should be ok and contain:
          | Field                                        | Value      |
          | CompanyNumber                                | 10192252   |
          | SchoolTotalExpenditure                       |            |
          | SchoolTotalTeachingSupportStaffCosts         |            |
          | SchoolTeachingStaffCosts                     |            |
          | SchoolSupplyTeachingStaffCosts               |            |
          | SchoolEducationalConsultancyCosts            |            |
          | SchoolEducationSupportStaffCosts             |            |
          | SchoolAgencySupplyTeachingStaffCosts         |            |
          | SchoolTotalNonEducationalSupportStaffCosts   |            |
          | SchoolAdministrativeClericalStaffCosts       |            |
          | SchoolOtherStaffCosts                        |            |
          | SchoolProfessionalServicesNonCurriculumCosts |            |
          | SchoolTotalEducationalSuppliesCosts          |            |
          | SchoolExaminationFeesCosts                   |            |
          | SchoolLearningResourcesNonIctCosts           |            |
          | SchoolLearningResourcesIctCosts              |            |
          | SchoolTotalPremisesStaffServiceCosts         |            |
          | SchoolCleaningCaretakingCosts                |            |
          | SchoolMaintenancePremisesCosts               |            |
          | SchoolOtherOccupationCosts                   |            |
          | SchoolPremisesStaffCosts                     |            |
          | SchoolTotalUtilitiesCosts                    |            |
          | SchoolEnergyCosts                            |            |
          | SchoolWaterSewerageCosts                     |            |
          | SchoolAdministrativeSuppliesCosts            |            |
          | SchoolTotalGrossCateringCosts                |            |
          | SchoolTotalNetCateringCosts                  |            |
          | SchoolCateringStaffCosts                     |            |
          | SchoolCateringSuppliesCosts                  |            |
          | SchoolTotalOtherCosts                        |            |
          | SchoolDirectRevenueFinancingCosts            |            |
          | SchoolGroundsMaintenanceCosts                |            |
          | SchoolIndirectEmployeeExpenses               |            |
          | SchoolInterestChargesLoanBank                |            |
          | SchoolOtherInsurancePremiumsCosts            |            |
          | SchoolPrivateFinanceInitiativeCharges        |            |
          | SchoolRentRatesCosts                         |            |
          | SchoolSpecialFacilitiesCosts                 |            |
          | SchoolStaffDevelopmentTrainingCosts          |            |
          | SchoolStaffRelatedInsuranceCosts             |            |
          | SchoolSupplyTeacherInsurableCosts            |            |
          | SchoolCommunityFocusedSchoolStaff            |            |
          | SchoolCommunityFocusedSchoolCosts            |            |
          | TotalExpenditure                             | 5084583.00 |
          | TotalTeachingSupportStaffCosts               |            |
          | TeachingStaffCosts                           |            |
          | SupplyTeachingStaffCosts                     |            |
          | EducationalConsultancyCosts                  |            |
          | EducationSupportStaffCosts                   |            |
          | AgencySupplyTeachingStaffCosts               |            |
          | TotalNonEducationalSupportStaffCosts         |            |
          | AdministrativeClericalStaffCosts             |            |
          | OtherStaffCosts                              |            |
          | ProfessionalServicesNonCurriculumCosts       |            |
          | TotalEducationalSuppliesCosts                |            |
          | ExaminationFeesCosts                         |            |
          | LearningResourcesNonIctCosts                 |            |
          | LearningResourcesIctCosts                    |            |
          | TotalPremisesStaffServiceCosts               |            |
          | CleaningCaretakingCosts                      |            |
          | MaintenancePremisesCosts                     |            |
          | OtherOccupationCosts                         |            |
          | PremisesStaffCosts                           |            |
          | TotalUtilitiesCosts                          |            |
          | EnergyCosts                                  |            |
          | WaterSewerageCosts                           |            |
          | AdministrativeSuppliesCosts                  |            |
          | TotalGrossCateringCosts                      |            |
          | TotalNetCateringCosts                        |            |
          | CateringStaffCosts                           |            |
          | CateringSuppliesCosts                        |            |
          | TotalOtherCosts                              |            |
          | DirectRevenueFinancingCosts                  |            |
          | GroundsMaintenanceCosts                      |            |
          | IndirectEmployeeExpenses                     |            |
          | InterestChargesLoanBank                      |            |
          | OtherInsurancePremiumsCosts                  |            |
          | PrivateFinanceInitiativeCharges              |            |
          | RentRatesCosts                               |            |
          | SpecialFacilitiesCosts                       |            |
          | StaffDevelopmentTrainingCosts                |            |
          | StaffRelatedInsuranceCosts                   |            |
          | SupplyTeacherInsurableCosts                  |            |
          | CommunityFocusedSchoolStaff                  |            |
          | CommunityFocusedSchoolCosts                  |            |

    Scenario: Sending a valid trust expenditure request with category and dimension
        Given a trust expenditure request with company number '10192252', category 'TotalExpenditure', dimension 'Actuals' and exclude central services = ''
        When I submit the insights expenditure request
        Then the trust expenditure result should be ok and contain:
          | Field                                        | Value      |
          | CompanyNumber                                | 10192252   |
          | SchoolTotalExpenditure                       | 5084583.00 |
          | SchoolTotalTeachingSupportStaffCosts         |            |
          | SchoolTeachingStaffCosts                     |            |
          | SchoolSupplyTeachingStaffCosts               |            |
          | SchoolEducationalConsultancyCosts            |            |
          | SchoolEducationSupportStaffCosts             |            |
          | SchoolAgencySupplyTeachingStaffCosts         |            |
          | SchoolTotalNonEducationalSupportStaffCosts   |            |
          | SchoolAdministrativeClericalStaffCosts       |            |
          | SchoolOtherStaffCosts                        |            |
          | SchoolProfessionalServicesNonCurriculumCosts |            |
          | SchoolTotalEducationalSuppliesCosts          |            |
          | SchoolExaminationFeesCosts                   |            |
          | SchoolLearningResourcesNonIctCosts           |            |
          | SchoolLearningResourcesIctCosts              |            |
          | SchoolTotalPremisesStaffServiceCosts         |            |
          | SchoolCleaningCaretakingCosts                |            |
          | SchoolMaintenancePremisesCosts               |            |
          | SchoolOtherOccupationCosts                   |            |
          | SchoolPremisesStaffCosts                     |            |
          | SchoolTotalUtilitiesCosts                    |            |
          | SchoolEnergyCosts                            |            |
          | SchoolWaterSewerageCosts                     |            |
          | SchoolAdministrativeSuppliesCosts            |            |
          | SchoolTotalGrossCateringCosts                |            |
          | SchoolTotalNetCateringCosts                  |            |
          | SchoolCateringStaffCosts                     |            |
          | SchoolCateringSuppliesCosts                  |            |
          | SchoolTotalOtherCosts                        |            |
          | SchoolDirectRevenueFinancingCosts            |            |
          | SchoolGroundsMaintenanceCosts                |            |
          | SchoolIndirectEmployeeExpenses               |            |
          | SchoolInterestChargesLoanBank                |            |
          | SchoolOtherInsurancePremiumsCosts            |            |
          | SchoolPrivateFinanceInitiativeCharges        |            |
          | SchoolRentRatesCosts                         |            |
          | SchoolSpecialFacilitiesCosts                 |            |
          | SchoolStaffDevelopmentTrainingCosts          |            |
          | SchoolStaffRelatedInsuranceCosts             |            |
          | SchoolSupplyTeacherInsurableCosts            |            |
          | SchoolCommunityFocusedSchoolStaff            |            |
          | SchoolCommunityFocusedSchoolCosts            |            |
          | TotalExpenditure                             | 5084583.00 |
          | TotalTeachingSupportStaffCosts               |            |
          | TeachingStaffCosts                           |            |
          | SupplyTeachingStaffCosts                     |            |
          | EducationalConsultancyCosts                  |            |
          | EducationSupportStaffCosts                   |            |
          | AgencySupplyTeachingStaffCosts               |            |
          | TotalNonEducationalSupportStaffCosts         |            |
          | AdministrativeClericalStaffCosts             |            |
          | OtherStaffCosts                              |            |
          | ProfessionalServicesNonCurriculumCosts       |            |
          | TotalEducationalSuppliesCosts                |            |
          | ExaminationFeesCosts                         |            |
          | LearningResourcesNonIctCosts                 |            |
          | LearningResourcesIctCosts                    |            |
          | TotalPremisesStaffServiceCosts               |            |
          | CleaningCaretakingCosts                      |            |
          | MaintenancePremisesCosts                     |            |
          | OtherOccupationCosts                         |            |
          | PremisesStaffCosts                           |            |
          | TotalUtilitiesCosts                          |            |
          | EnergyCosts                                  |            |
          | WaterSewerageCosts                           |            |
          | AdministrativeSuppliesCosts                  |            |
          | TotalGrossCateringCosts                      |            |
          | TotalNetCateringCosts                        |            |
          | CateringStaffCosts                           |            |
          | CateringSuppliesCosts                        |            |
          | TotalOtherCosts                              |            |
          | DirectRevenueFinancingCosts                  |            |
          | GroundsMaintenanceCosts                      |            |
          | IndirectEmployeeExpenses                     |            |
          | InterestChargesLoanBank                      |            |
          | OtherInsurancePremiumsCosts                  |            |
          | PrivateFinanceInitiativeCharges              |            |
          | RentRatesCosts                               |            |
          | SpecialFacilitiesCosts                       |            |
          | StaffDevelopmentTrainingCosts                |            |
          | StaffRelatedInsuranceCosts                   |            |
          | SupplyTeacherInsurableCosts                  |            |
          | CommunityFocusedSchoolStaff                  |            |
          | CommunityFocusedSchoolCosts                  |            |

    Scenario: Sending a valid trust expenditure request with dimension
        Given a trust expenditure request with company number '10192252', category '', dimension 'Actuals' and exclude central services = ''
        When I submit the insights expenditure request
        Then the trust expenditure result should be ok and contain:
          | Field                                        | Value      |
          | CompanyNumber                                | 10192252   |
          | SchoolTotalExpenditure                       | 5084583.00 |
          | SchoolTotalTeachingSupportStaffCosts         | 3639070.00 |
          | SchoolTeachingStaffCosts                     | 2440538.00 |
          | SchoolSupplyTeachingStaffCosts               | 0.00       |
          | SchoolEducationalConsultancyCosts            | 262691.00  |
          | SchoolEducationSupportStaffCosts             | 895785.00  |
          | SchoolAgencySupplyTeachingStaffCosts         | 40056.00   |
          | SchoolTotalNonEducationalSupportStaffCosts   | 578205.00  |
          | SchoolAdministrativeClericalStaffCosts       | 327747.00  |
          | SchoolOtherStaffCosts                        | 133034.00  |
          | SchoolProfessionalServicesNonCurriculumCosts | 117424.00  |
          | SchoolTotalEducationalSuppliesCosts          | 92972.00   |
          | SchoolExaminationFeesCosts                   | 0.00       |
          | SchoolLearningResourcesNonIctCosts           | 92972.00   |
          | SchoolLearningResourcesIctCosts              | 13529.00   |
          | SchoolTotalPremisesStaffServiceCosts         | 331257.00  |
          | SchoolCleaningCaretakingCosts                | 103202.00  |
          | SchoolMaintenancePremisesCosts               | 113676.00  |
          | SchoolOtherOccupationCosts                   | 21466.00   |
          | SchoolPremisesStaffCosts                     | 92913.00   |
          | SchoolTotalUtilitiesCosts                    | 85686.00   |
          | SchoolEnergyCosts                            | 72693.00   |
          | SchoolWaterSewerageCosts                     | 12993.00   |
          | SchoolAdministrativeSuppliesCosts            | 37149.00   |
          | SchoolTotalGrossCateringCosts                | 162358.00  |
          | SchoolTotalNetCateringCosts                  |            |
          | SchoolCateringStaffCosts                     | 0.00       |
          | SchoolCateringSuppliesCosts                  | 162358.00  |
          | SchoolTotalOtherCosts                        | 144358.00  |
          | SchoolDirectRevenueFinancingCosts            | 0.00       |
          | SchoolGroundsMaintenanceCosts                | 4690.00    |
          | SchoolIndirectEmployeeExpenses               | 6560.00    |
          | SchoolInterestChargesLoanBank                | 0.00       |
          | SchoolOtherInsurancePremiumsCosts            | 13729.00   |
          | SchoolPrivateFinanceInitiativeCharges        | 0.00       |
          | SchoolRentRatesCosts                         | 83524.00   |
          | SchoolSpecialFacilitiesCosts                 | 11754.00   |
          | SchoolStaffDevelopmentTrainingCosts          | 19292.00   |
          | SchoolStaffRelatedInsuranceCosts             | 2311.00    |
          | SchoolSupplyTeacherInsurableCosts            | 2498.00    |
          | SchoolCommunityFocusedSchoolStaff            |            |
          | SchoolCommunityFocusedSchoolCosts            |            |
          | TotalExpenditure                             | 5084583.00 |
          | TotalTeachingSupportStaffCosts               | 3639070.00 |
          | TeachingStaffCosts                           | 2440538.00 |
          | SupplyTeachingStaffCosts                     | 0.00       |
          | EducationalConsultancyCosts                  | 262691.00  |
          | EducationSupportStaffCosts                   | 895785.00  |
          | AgencySupplyTeachingStaffCosts               | 40056.00   |
          | TotalNonEducationalSupportStaffCosts         | 578205.00  |
          | AdministrativeClericalStaffCosts             | 327747.00  |
          | OtherStaffCosts                              | 133034.00  |
          | ProfessionalServicesNonCurriculumCosts       | 117424.00  |
          | TotalEducationalSuppliesCosts                | 92972.00   |
          | ExaminationFeesCosts                         | 0.00       |
          | LearningResourcesNonIctCosts                 | 92972.00   |
          | LearningResourcesIctCosts                    | 13529.00   |
          | TotalPremisesStaffServiceCosts               | 331257.00  |
          | CleaningCaretakingCosts                      | 103202.00  |
          | MaintenancePremisesCosts                     | 113676.00  |
          | OtherOccupationCosts                         | 21466.00   |
          | PremisesStaffCosts                           | 92913.00   |
          | TotalUtilitiesCosts                          | 85686.00   |
          | EnergyCosts                                  | 72693.00   |
          | WaterSewerageCosts                           | 12993.00   |
          | AdministrativeSuppliesCosts                  | 37149.00   |
          | TotalGrossCateringCosts                      | 162358.00  |
          | TotalNetCateringCosts                        |            |
          | CateringStaffCosts                           | 0.00       |
          | CateringSuppliesCosts                        | 162358.00  |
          | TotalOtherCosts                              | 144358.00  |
          | DirectRevenueFinancingCosts                  | 0.00       |
          | GroundsMaintenanceCosts                      | 4690.00    |
          | IndirectEmployeeExpenses                     | 6560.00    |
          | InterestChargesLoanBank                      | 0.00       |
          | OtherInsurancePremiumsCosts                  | 13729.00   |
          | PrivateFinanceInitiativeCharges              | 0.00       |
          | RentRatesCosts                               | 83524.00   |
          | SpecialFacilitiesCosts                       | 11754.00   |
          | StaffDevelopmentTrainingCosts                | 19292.00   |
          | StaffRelatedInsuranceCosts                   | 2311.00    |
          | SupplyTeacherInsurableCosts                  | 2498.00    |
          | CommunityFocusedSchoolStaff                  |            |
          | CommunityFocusedSchoolCosts                  |            |

    Scenario: Sending a trust expenditure request with bad company number
        Given a trust expenditure request with company number '10000000', category '', dimension 'Actuals' and exclude central services = ''
        When I submit the insights expenditure request
        Then the trust expenditure result should be not found

    Scenario: Sending an invalid trust expenditure request
        Given a trust expenditure request with company number '10192252', category 'Invalid', dimension '' and exclude central services = ''
        When I submit the insights expenditure request
        Then the trust expenditure result should be bad request

    Scenario: Sending a valid trust expenditure history request
        Given a valid trust expenditure history request with company number '10192252'
        When I submit the insights expenditure request
        Then the trust expenditure history result should be ok and contain:
          | Year | Term         | CompanyNumber | SchoolTotalExpenditure | SchoolTotalTeachingSupportStaffCosts | SchoolTeachingStaffCosts | SchoolSupplyTeachingStaffCosts | SchoolEducationalConsultancyCosts | SchoolEducationSupportStaffCosts | SchoolAgencySupplyTeachingStaffCosts | SchoolTotalNonEducationalSupportStaffCosts | SchoolAdministrativeClericalStaffCosts | SchoolOtherStaffCosts | SchoolProfessionalServicesNonCurriculumCosts | SchoolTotalEducationalSuppliesCosts | SchoolExaminationFeesCosts | SchoolLearningResourcesNonIctCosts | SchoolLearningResourcesIctCosts | SchoolTotalPremisesStaffServiceCosts | SchoolCleaningCaretakingCosts | SchoolMaintenancePremisesCosts | SchoolOtherOccupationCosts | SchoolPremisesStaffCosts | SchoolTotalUtilitiesCosts | SchoolEnergyCosts | SchoolWaterSewerageCosts | SchoolAdministrativeSuppliesCosts | SchoolTotalGrossCateringCosts | SchoolTotalNetCateringCosts | SchoolCateringStaffCosts | SchoolCateringSuppliesCosts | SchoolTotalOtherCosts | SchoolDirectRevenueFinancingCosts | SchoolGroundsMaintenanceCosts | SchoolIndirectEmployeeExpenses | SchoolInterestChargesLoanBank | SchoolOtherInsurancePremiumsCosts | SchoolPrivateFinanceInitiativeCharges | SchoolRentRatesCosts | SchoolSpecialFacilitiesCosts | SchoolStaffDevelopmentTrainingCosts | SchoolStaffRelatedInsuranceCosts | SchoolSupplyTeacherInsurableCosts | SchoolCommunityFocusedSchoolStaff | SchoolCommunityFocusedSchoolCosts | TotalExpenditure | TotalTeachingSupportStaffCosts | TeachingStaffCosts | SupplyTeachingStaffCosts | EducationalConsultancyCosts | EducationSupportStaffCosts | AgencySupplyTeachingStaffCosts | TotalNonEducationalSupportStaffCosts | AdministrativeClericalStaffCosts | OtherStaffCosts | ProfessionalServicesNonCurriculumCosts | TotalEducationalSuppliesCosts | ExaminationFeesCosts | LearningResourcesNonIctCosts | LearningResourcesIctCosts | TotalPremisesStaffServiceCosts | CleaningCaretakingCosts | MaintenancePremisesCosts | OtherOccupationCosts | PremisesStaffCosts | TotalUtilitiesCosts | EnergyCosts | WaterSewerageCosts | AdministrativeSuppliesCosts | TotalGrossCateringCosts | TotalNetCateringCosts | CateringStaffCosts | CateringSuppliesCosts | TotalOtherCosts | DirectRevenueFinancingCosts | GroundsMaintenanceCosts | IndirectEmployeeExpenses | InterestChargesLoanBank | OtherInsurancePremiumsCosts | PrivateFinanceInitiativeCharges | RentRatesCosts | SpecialFacilitiesCosts | StaffDevelopmentTrainingCosts | StaffRelatedInsuranceCosts | SupplyTeacherInsurableCosts | CommunityFocusedSchoolStaff | CommunityFocusedSchoolCosts |
          | 2022 | 2021 to 2022 | 10192252      | 5084583.00             | 3639070.00                           | 2440538.00               | 0.00                           | 262691.00                         | 895785.00                        | 40056.00                             | 578205.00                                  | 327747.00                              | 133034.00             | 117424.00                                    | 92972.00                            | 0.00                       | 92972.00                           | 13529.00                        | 331257.00                            | 103202.00                     | 113676.00                      | 21466.00                   | 92913.00                 | 85686.00                  | 72693.00          | 12993.00                 | 37149.00                          | 162358.00                     |                             | 0.00                     | 162358.00                   | 144358.00             | 0.00                              | 4690.00                       | 6560.00                        | 0.00                          | 13729.00                          | 0.00                                  | 83524.00             | 11754.00                     | 19292.00                            | 2311.00                          | 2498.00                           |                                   |                                   | 5084583.00       | 3639070.00                     | 2440538.00         | 0.00                     | 262691.00                   | 895785.00                  | 40056.00                       | 578205.00                            | 327747.00                        | 133034.00       | 117424.00                              | 92972.00                      | 0.00                 | 92972.00                     | 13529.00                  | 331257.00                      | 103202.00               | 113676.00                | 21466.00             | 92913.00           | 85686.00            | 72693.00    | 12993.00           | 37149.00                    | 162358.00               |                       | 0.00               | 162358.00             | 144358.00       | 0.00                        | 4690.00                 | 6560.00                  | 0.00                    | 13729.00                    | 0.00                            | 83524.00       | 11754.00               | 19292.00                      | 2311.00                    | 2498.00                     |                             |                             |

    Scenario: Sending a valid trust expenditure query request
        Given a valid trust expenditure query request with company numbers:
          | CompanyNumber |
          | 10249712      |
          | 10259334      |
          | 10264735      |
        When I submit the insights expenditure request
        Then the trust expenditure query result should be ok and contain:
          | CompanyNumber | SchoolTotalExpenditure | SchoolTotalTeachingSupportStaffCosts | SchoolTeachingStaffCosts | SchoolSupplyTeachingStaffCosts | SchoolEducationalConsultancyCosts | SchoolEducationSupportStaffCosts | SchoolAgencySupplyTeachingStaffCosts | SchoolTotalNonEducationalSupportStaffCosts | SchoolAdministrativeClericalStaffCosts | SchoolOtherStaffCosts | SchoolProfessionalServicesNonCurriculumCosts | SchoolTotalEducationalSuppliesCosts | SchoolExaminationFeesCosts | SchoolLearningResourcesNonIctCosts | SchoolLearningResourcesIctCosts | SchoolTotalPremisesStaffServiceCosts | SchoolCleaningCaretakingCosts | SchoolMaintenancePremisesCosts | SchoolOtherOccupationCosts | SchoolPremisesStaffCosts | SchoolTotalUtilitiesCosts | SchoolEnergyCosts | SchoolWaterSewerageCosts | SchoolAdministrativeSuppliesCosts | SchoolTotalGrossCateringCosts | SchoolTotalNetCateringCosts | SchoolCateringStaffCosts | SchoolCateringSuppliesCosts | SchoolTotalOtherCosts | SchoolDirectRevenueFinancingCosts | SchoolGroundsMaintenanceCosts | SchoolIndirectEmployeeExpenses | SchoolInterestChargesLoanBank | SchoolOtherInsurancePremiumsCosts | SchoolPrivateFinanceInitiativeCharges | SchoolRentRatesCosts | SchoolSpecialFacilitiesCosts | SchoolStaffDevelopmentTrainingCosts | SchoolStaffRelatedInsuranceCosts | SchoolSupplyTeacherInsurableCosts | SchoolCommunityFocusedSchoolStaff | SchoolCommunityFocusedSchoolCosts | TotalExpenditure | TotalTeachingSupportStaffCosts | TeachingStaffCosts | SupplyTeachingStaffCosts | EducationalConsultancyCosts | EducationSupportStaffCosts | AgencySupplyTeachingStaffCosts | TotalNonEducationalSupportStaffCosts | AdministrativeClericalStaffCosts | OtherStaffCosts | ProfessionalServicesNonCurriculumCosts | TotalEducationalSuppliesCosts | ExaminationFeesCosts | LearningResourcesNonIctCosts | LearningResourcesIctCosts | TotalPremisesStaffServiceCosts | CleaningCaretakingCosts | MaintenancePremisesCosts | OtherOccupationCosts | PremisesStaffCosts | TotalUtilitiesCosts | EnergyCosts | WaterSewerageCosts | AdministrativeSuppliesCosts | TotalGrossCateringCosts | TotalNetCateringCosts | CateringStaffCosts | CateringSuppliesCosts | TotalOtherCosts | DirectRevenueFinancingCosts | GroundsMaintenanceCosts | IndirectEmployeeExpenses | InterestChargesLoanBank | OtherInsurancePremiumsCosts | PrivateFinanceInitiativeCharges | RentRatesCosts | SpecialFacilitiesCosts | StaffDevelopmentTrainingCosts | StaffRelatedInsuranceCosts | SupplyTeacherInsurableCosts | CommunityFocusedSchoolStaff | CommunityFocusedSchoolCosts |
          | 10249712      | 5072668.00             | 3668096.00                           | 1377868.00               | 0.00                           | 236950.00                         | 1001271.00                       | 1052007.00                           | 500711.00                                  | 285658.00                              | 28104.00              | 186949.00                                    | 139350.00                           | 5344.00                    | 134006.00                          | 75168.00                        | 344765.00                            | 39980.00                      | 152722.00                      | 4426.00                    | 147637.00                | 52487.00                  | 32052.00          | 20435.00                 | 100195.00                         | 84190.00                      |                             | 0.00                     | 84190.00                    | 107706.00             | 0.00                              | 3983.00                       | 39047.00                       | 0.00                          | 11762.00                          | 0.00                                  | 0.00                 | 4520.00                      | 28135.00                            | 7364.00                          | 12895.00                          |                                   |                                   | 5072668.00       | 3668096.00                     | 1377868.00         | 0.00                     | 236950.00                   | 1001271.00                 | 1052007.00                     | 500711.00                            | 285658.00                        | 28104.00        | 186949.00                              | 139350.00                     | 5344.00              | 134006.00                    | 75168.00                  | 344765.00                      | 39980.00                | 152722.00                | 4426.00              | 147637.00          | 52487.00            | 32052.00    | 20435.00           | 100195.00                   | 84190.00                |                       | 0.00               | 84190.00              | 107706.00       | 0.00                        | 3983.00                 | 39047.00                 | 0.00                    | 11762.00                    | 0.00                            | 0.00           | 4520.00                | 28135.00                      | 7364.00                    | 12895.00                    |                             |                             |
          | 10259334      | 14665108.00            | 10240640.00                          | 8321911.00               | 20771.00                       | 537911.00                         | 1176654.00                       | 183393.00                            | 1299564.00                                 | 901007.00                              | 30378.00              | 368179.00                                    | 470172.00                           | 77694.00                   | 392478.00                          | 285646.00                       | 836523.00                            | 147297.00                     | 237861.00                      | 32663.00                   | 418702.00                | 188224.00                 | 176642.00         | 11582.00                 | 364710.00                         | 474250.00                     |                             | 132993.00                | 341257.00                   | 505378.00             | 0.00                              | 0.00                          | 168947.00                      | 0.00                          | 73076.00                          | 0.00                                  | 25110.00             | 166721.00                    | 26592.00                            | 5068.00                          | 39864.00                          |                                   |                                   | 14665108.00      | 10240640.00                    | 8321911.00         | 20771.00                 | 537911.00                   | 1176654.00                 | 183393.00                      | 1299564.00                           | 901007.00                        | 30378.00        | 368179.00                              | 470172.00                     | 77694.00             | 392478.00                    | 285646.00                 | 836523.00                      | 147297.00               | 237861.00                | 32663.00             | 418702.00          | 188224.00           | 176642.00   | 11582.00           | 364710.00                   | 474250.00               |                       | 132993.00          | 341257.00             | 505378.00       | 0.00                        | 0.00                    | 168947.00                | 0.00                    | 73076.00                    | 0.00                            | 25110.00       | 166721.00              | 26592.00                      | 5068.00                    | 39864.00                    |                             |                             |
          | 10264735      | 4260179.00             | 2973757.00                           | 1440362.00               | 0.00                           | 103151.00                         | 1394931.00                       | 35313.00                             | 447930.00                                  | 215178.00                              | 114615.00             | 118137.00                                    | 94732.00                            | 0.00                       | 94732.00                           | 46112.00                        | 278898.00                            | 26116.00                      | 189034.00                      | 18543.00                   | 45205.00                 | 50543.00                  | 42238.00          | 8305.00                  | 44722.00                          | 193648.00                     |                             | 69353.00                 | 124295.00                   | 129832.00             | 0.00                              | 3929.00                       | 4918.00                        | 0.00                          | 10845.00                          | 0.00                                  | 62164.00             | 13804.00                     | 4893.00                             | 14051.00                         | 15228.00                          |                                   |                                   | 4260179.00       | 2973757.00                     | 1440362.00         | 0.00                     | 103151.00                   | 1394931.00                 | 35313.00                       | 447930.00                            | 215178.00                        | 114615.00       | 118137.00                              | 94732.00                      | 0.00                 | 94732.00                     | 46112.00                  | 278898.00                      | 26116.00                | 189034.00                | 18543.00             | 45205.00           | 50543.00            | 42238.00    | 8305.00            | 44722.00                    | 193648.00               |                       | 69353.00           | 124295.00             | 129832.00       | 0.00                        | 3929.00                 | 4918.00                  | 0.00                    | 10845.00                    | 0.00                            | 62164.00       | 13804.00               | 4893.00                       | 14051.00                   | 15228.00                    |                             |                             |