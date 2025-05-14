Feature: Insights expenditure endpoints

    Scenario: Sending a valid school expenditure request with category and dimension
        Given a school expenditure request with urn '990000', category 'TotalExpenditure' and dimension 'Actuals'
        When I submit the insights expenditure request
        Then the school expenditure result should be ok and contain:
          | Field                                           | Value                  |
          | URN                                             | 990000                 |
          | SchoolName                                      | Test school 176        |
          | SchoolType                                      | Voluntary aided school |
          | LAName                                          | Test Local Authority   |
          | TotalPupils                                     | 418.00                 |
          | TotalInternalFloorArea                          | 3021.00                |
          | TotalExpenditure                                | 2790504.00             |
          | TotalTeachingSupportStaffCosts                  |                        |
          | TeachingStaffCosts                              |                        |
          | SupplyTeachingStaffCosts                        |                        |
          | EducationalConsultancyCosts                     |                        |
          | EducationSupportStaffCosts                      |                        |
          | AgencySupplyTeachingStaffCosts                  |                        |
          | TotalNonEducationalSupportStaffCosts            |                        |
          | AdministrativeClericalStaffCosts                |                        |
          | OtherStaffCosts                                 |                        |
          | ProfessionalServicesNonCurriculumCosts          |                        |
          | TotalEducationalSuppliesCosts                   |                        |
          | ExaminationFeesCosts                            |                        |
          | LearningResourcesNonIctCosts                    |                        |
          | LearningResourcesIctCosts                       |                        |
          | TotalPremisesStaffServiceCosts                  |                        |
          | CleaningCaretakingCosts                         |                        |
          | MaintenancePremisesCosts                        |                        |
          | OtherOccupationCosts                            |                        |
          | PremisesStaffCosts                              |                        |
          | TotalUtilitiesCosts                             |                        |
          | EnergyCosts                                     |                        |
          | WaterSewerageCosts                              |                        |
          | AdministrativeSuppliesNonEducationalCosts       |                        |
          | TotalGrossCateringCosts                         |                        |
          | TotalNetCateringCosts                           |                        |
          | CateringStaffCosts                              |                        |
          | CateringSuppliesCosts                           |                        |
          | TotalOtherCosts                                 |                        |
          | DirectRevenueFinancingCosts                     |                        |
          | GroundsMaintenanceCosts                         |                        |
          | IndirectEmployeeExpenses                        |                        |
          | InterestChargesLoanBank                         |                        |
          | OtherInsurancePremiumsCosts                     |                        |
          | PrivateFinanceInitiativeCharges                 |                        |
          | RentRatesCosts                                  |                        |
          | SpecialFacilitiesCosts                          |                        |
          | StaffDevelopmentTrainingCosts                   |                        |
          | StaffRelatedInsuranceCosts                      |                        |
          | SupplyTeacherInsurableCosts                     |                        |
          | CommunityFocusedSchoolStaff                     |                        |
          | CommunityFocusedSchoolCosts                     |                        |

    Scenario: Sending a valid school expenditure request with dimension
        Given a school expenditure request with urn '990000', category '' and dimension 'Actuals'
        When I submit the insights expenditure request
        Then the school expenditure result should be ok and contain:
          | Field                                           | Value                  |
          | URN                                             | 990000                 |
          | SchoolName                                      | Test school 176        |
          | SchoolType                                      | Voluntary aided school |
          | LAName                                          | Test Local Authority   |
          | TotalPupils                                     | 418.00                 |
          | TotalInternalFloorArea                          | 3021.00                |
          | TotalExpenditure                                | 2790504.00             |
          | TotalTeachingSupportStaffCosts                  | 2044335.00             |
          | TeachingStaffCosts                              | 1437007.00             |
          | SupplyTeachingStaffCosts                        | 0.00                   |
          | EducationalConsultancyCosts                     | 208376.00              |
          | EducationSupportStaffCosts                      | 301653.00              |
          | AgencySupplyTeachingStaffCosts                  | 97299.00               |
          | TotalNonEducationalSupportStaffCosts            | 202341.00              |
          | AdministrativeClericalStaffCosts                | 157417.00              |
          | OtherStaffCosts                                 | 12915.00               |
          | ProfessionalServicesNonCurriculumCosts          | 32009.00               |
          | TotalEducationalSuppliesCosts                   | 70108.00               |
          | ExaminationFeesCosts                            | 0.00                   |
          | LearningResourcesNonIctCosts                    | 70108.00               |
          | LearningResourcesIctCosts                       | 24200.00               |
          | TotalPremisesStaffServiceCosts                  | 166932.00              |
          | CleaningCaretakingCosts                         | 47240.00               |
          | MaintenancePremisesCosts                        | 33774.00               |
          | OtherOccupationCosts                            | 33176.00               |
          | PremisesStaffCosts                              | 52742.00               |
          | TotalUtilitiesCosts                             | 31001.00               |
          | EnergyCosts                                     | 26060.00               |
          | WaterSewerageCosts                              | 4941.00                |
          | AdministrativeSuppliesNonEducationalCosts       | 28505.00               |
          | TotalGrossCateringCosts                         | 84186.00               |
          | TotalNetCateringCosts                           | 91847.00               |
          | CateringStaffCosts                              | 2170.00                |
          | CateringSuppliesCosts                           | 82016.00               |
          | TotalOtherCosts                                 | 141903.00              |
          | DirectRevenueFinancingCosts                     | 3006.00                |
          | GroundsMaintenanceCosts                         | 615.00                 |
          | IndirectEmployeeExpenses                        | 38374.00               |
          | InterestChargesLoanBank                         | 0.00                   |
          | OtherInsurancePremiumsCosts                     | 11862.00               |
          | PrivateFinanceInitiativeCharges                 | 0.00                   |
          | RentRatesCosts                                  | 73416.00               |
          | SpecialFacilitiesCosts                          | 0.00                   |
          | StaffDevelopmentTrainingCosts                   | 9115.00                |
          | StaffRelatedInsuranceCosts                      | 2735.00                |
          | SupplyTeacherInsurableCosts                     | 2780.00                |
          | CommunityFocusedSchoolStaff                     | 0.00                   |
          | CommunityFocusedSchoolCosts                     | 0.00                   |

    Scenario: Sending a valid school expenditure request with bad URN
        Given a school expenditure request with urn '0000000', category '' and dimension 'Actuals'
        When I submit the insights expenditure request
        Then the school expenditure result should be not found

    Scenario: Sending an invalid school expenditure request
        Given a school expenditure request with urn '990000', category 'Invalid' and dimension ''
        When I submit the insights expenditure request
        Then the school expenditure result should be bad request

    Scenario: Sending a valid school expenditure history request
        Given a valid school expenditure history request with urn '990000'
        When I submit the insights expenditure request
        Then the school expenditure history result should be ok and contain:
          | Year | TotalExpenditure | TotalTeachingSupportStaffCosts | TeachingStaffCosts | SupplyTeachingStaffCosts | EducationalConsultancyCosts | EducationSupportStaffCosts | AgencySupplyTeachingStaffCosts | TotalNonEducationalSupportStaffCosts | AdministrativeClericalStaffCosts | OtherStaffCosts | ProfessionalServicesNonCurriculumCosts | TotalEducationalSuppliesCosts | ExaminationFeesCosts | LearningResourcesNonIctCosts | LearningResourcesIctCosts | TotalPremisesStaffServiceCosts | CleaningCaretakingCosts | MaintenancePremisesCosts | OtherOccupationCosts | PremisesStaffCosts | TotalUtilitiesCosts | EnergyCosts | WaterSewerageCosts | AdministrativeSuppliesNonEducationalCosts | TotalGrossCateringCosts | TotalNetCateringCosts | CateringStaffCosts | CateringSuppliesCosts | TotalOtherCosts | DirectRevenueFinancingCosts | GroundsMaintenanceCosts | IndirectEmployeeExpenses | InterestChargesLoanBank | OtherInsurancePremiumsCosts | PrivateFinanceInitiativeCharges | RentRatesCosts | SpecialFacilitiesCosts | StaffDevelopmentTrainingCosts | StaffRelatedInsuranceCosts | SupplyTeacherInsurableCosts | CommunityFocusedSchoolStaff | CommunityFocusedSchoolCosts |
          | 2021 | 1352665.00       | 947543.00                      | 695259.00          | 0.00                     | 14407.00                    | 134473.00                  | 103404.00                      | 151028.00                            | 98073.00                         | 9316.00         | 43639.00                               | 10737.00                      | 0.00                 | 10737.00                     | 1236.00                   | 83002.00                       | 31170.00                | 44765.00                 | 1089.00              | 5978.00            | 46713.00            | 43198.00    | 3515.00            | 7677.00                                   | 67912.00                | 69690.00              | 0.00               | 67912.00              | 36817.00        | 0.00                        | 8211.00                 | 1405.00                  | 0.00                    | 2865.00                     | 0.00                            | 15763.00       | 0.00                   | 8573.00                       | 0.00                       | 0.00                        | 0.00                        | 0.00                        |
          | 2022 | 2790504.00       | 2044335.00                     | 1437007.00         | 0.00                     | 208376.00                   | 301653.00                  | 97299.00                       | 202341.00                            | 157417.00                        | 12915.00        | 32009.00                               | 70108.00                      | 0.00                 | 70108.00                     | 24200.00                  | 166932.00                      | 47240.00                | 33774.00                 | 33176.00             | 52742.00           | 31001.00            | 26060.00    | 4941.00            | 28505.00                                  | 84186.00                | 91847.00              | 2170.00            | 82016.00              | 141903.00       | 3006.00                     | 615.00                  | 38374.00                 | 0.00                    | 11862.00                    | 0.00                            | 73416.00       | 0.00                   | 9115.00                       | 2735.00                    | 2780.00                     | 0.00                        | 0.00                        |

    Scenario: Sending a valid school expenditure query request with URNs
        Given a valid school expenditure query request with urns:
          | Urn    |
          | 990000 |
          | 990001 |
          | 990002 |
        When I submit the insights expenditure request
        Then the school expenditure query result should be ok and contain:
          | SchoolName      | SchoolType                     | LAName               | URN    | SchoolName      | SchoolType                     | LAName        | TotalPupils | TotalInternalFloorArea | TotalExpenditure | TotalTeachingSupportStaffCosts | TeachingStaffCosts | SupplyTeachingStaffCosts | EducationalConsultancyCosts | EducationSupportStaffCosts | AgencySupplyTeachingStaffCosts | TotalNonEducationalSupportStaffCosts | AdministrativeClericalStaffCosts | OtherStaffCosts | ProfessionalServicesNonCurriculumCosts | TotalEducationalSuppliesCosts | ExaminationFeesCosts | LearningResourcesNonIctCosts | LearningResourcesIctCosts | TotalPremisesStaffServiceCosts | CleaningCaretakingCosts | MaintenancePremisesCosts | OtherOccupationCosts | PremisesStaffCosts | TotalUtilitiesCosts | EnergyCosts | WaterSewerageCosts | AdministrativeSuppliesNonEducationalCosts | TotalGrossCateringCosts | TotalNetCateringCosts | CateringStaffCosts | CateringSuppliesCosts | TotalOtherCosts | DirectRevenueFinancingCosts | GroundsMaintenanceCosts | IndirectEmployeeExpenses | InterestChargesLoanBank | OtherInsurancePremiumsCosts | PrivateFinanceInitiativeCharges | RentRatesCosts | SpecialFacilitiesCosts | StaffDevelopmentTrainingCosts | StaffRelatedInsuranceCosts | SupplyTeacherInsurableCosts | CommunityFocusedSchoolStaff | CommunityFocusedSchoolCosts |
          | Test school 176 | Voluntary aided school         | Test Local Authority | 990000 | Test school 176 | Voluntary aided school         | Bedford       | 418.00      | 3021.00                | 2790504.00       | 2044335.00                     | 1437007.00         | 0.00                     | 208376.00                   | 301653.00                  | 97299.00                       | 202341.00                            | 157417.00                        | 12915.00        | 32009.00                               | 70108.00                      | 0.00                 | 70108.00                     | 24200.00                  | 166932.00                      | 47240.00                | 33774.00                 | 33176.00             | 52742.00           | 31001.00            | 26060.00    | 4941.00            | 28505.00                                  | 84186.00                | 91847.00              | 2170.00            | 82016.00              | 141903.00       | 3006.00                     | 615.00                  | 38374.00                 | 0.00                    | 11862.00                    | 0.00                            | 73416.00       | 0.00                   | 9115.00                       | 2735.00                    | 2780.00                     | 0.00                        | 0.00                        |
          | Test school 241 | Voluntary aided school         | Islington            | 990001 | Test school 241 | Voluntary aided school         | Islington     | 303.00      | 2476.00                | 2054918.00       | 1521798.00                     | 997855.00          | 0.00                     | 220708.00                   | 278722.00                  | 24513.00                       | 160770.00                            | 92011.00                         | 28511.00        | 40248.00                               | 46521.00                      | 0.00                 | 46521.00                     | 10779.00                  | 143345.00                      | 4254.00                 | 38484.00                 | 8972.00              | 91635.00           | 23799.00            | 21324.00    | 2475.00            | 21815.00                                  | 71734.00                | 90089.00              | 0.00               | 71734.00              | 54357.00        | 0.00                        | 407.00                  | 6586.00                  | 0.00                    | 10274.00                    | 0.00                            | 0.00           | 2695.00                | 1648.00                       | 8736.00                    | 24011.00                    | 0.00                        | 0.00                        |
          | Test school 224 | Local authority nursery school | Isle of Wight        | 990002 | Test school 224 | Local authority nursery school | Isle of Wight | 191.00      | 2552.00                | 1719060.00       | 1189016.00                     | 733333.00          | 0.00                     | 151630.00                   | 161948.00                  | 142105.00                      | 137246.00                            | 103006.00                        | 13488.00        | 20752.00                               | 24992.00                      | 0.00                 | 24992.00                     | 14462.00                  | 110661.00                      | 35684.00                | 22248.00                 | 6416.00              | 46313.00           | 42196.00            | 37236.00    | 4960.00            | 17131.00                                  | 48949.00                | 57188.00              | 0.00               | 48949.00              | 134408.00       | 0.00                        | 0.00                    | 26539.00                 | 0.00                    | 6125.00                     | 0.00                            | 22580.00       | 1287.00                | 3568.00                       | 7587.00                    | 18030.00                    | 47010.00                    | 1682.00                     |

    Scenario: Sending a valid school expenditure query request with company number and phase
        Given a valid school expenditure query request with company number '08104190' and phase 'Secondary'
        When I submit the insights expenditure request
        Then the school expenditure query result should be ok and contain:
          | SchoolName             | SchoolType        | LAName                 | URN    | SchoolName             | SchoolType        | LAName                 | TotalPupils | TotalInternalFloorArea | TotalExpenditure | TotalTeachingSupportStaffCosts | TeachingStaffCosts | SupplyTeachingStaffCosts | EducationalConsultancyCosts | EducationSupportStaffCosts | AgencySupplyTeachingStaffCosts | TotalNonEducationalSupportStaffCosts | AdministrativeClericalStaffCosts | OtherStaffCosts | ProfessionalServicesNonCurriculumCosts | TotalEducationalSuppliesCosts | ExaminationFeesCosts | LearningResourcesNonIctCosts | LearningResourcesIctCosts | TotalPremisesStaffServiceCosts | CleaningCaretakingCosts | MaintenancePremisesCosts | OtherOccupationCosts | PremisesStaffCosts | TotalUtilitiesCosts | EnergyCosts | WaterSewerageCosts | AdministrativeSuppliesNonEducationalCosts | TotalGrossCateringCosts | TotalNetCateringCosts | CateringStaffCosts | CateringSuppliesCosts | TotalOtherCosts | DirectRevenueFinancingCosts | GroundsMaintenanceCosts | IndirectEmployeeExpenses | InterestChargesLoanBank | OtherInsurancePremiumsCosts | PrivateFinanceInitiativeCharges | RentRatesCosts | SpecialFacilitiesCosts | StaffDevelopmentTrainingCosts | StaffRelatedInsuranceCosts | SupplyTeacherInsurableCosts | CommunityFocusedSchoolStaff | CommunityFocusedSchoolCosts |
          | Test academy school 87 | Academy converter | City of London         | 777051 | Test academy school 87 | Academy converter | City of London         | 335.00      | 2951.00                | 2597481.00       | 1863218.00                     | 1157123.00         | 0.00                     | 358226.00                   | 323874.00                  | 23995.00                       | 175298.00                            | 96346.00                         | 45730.00        | 33222.00                               | 37281.00                      | 0.00                 | 37281.00                     | 21124.00                  | 218922.00                      | 37501.00                | 46134.00                 | 9861.00              | 125426.00          | 29951.00            | 24869.00    | 5082.00            | 25879.00                                  | 83265.00                | 95658.00              | 0.00               | 83265.00              | 145676.00       | 3133.00                     | 2904.00                 | 42756.00                 | 0.00                    | 9563.00                     | 0.00                            | 61712.00       | 7762.00                | 12700.00                      | 2873.00                    | 2273.00                     | 0.00                        | 0.00                        |
          | Test academy school 90 | Academy converter | Camden                 | 777052 | Test academy school 90 | Academy converter | Camden                 | 191.00      | 2552.00                | 1719060.00       | 1189016.00                     | 733333.00          | 0.00                     | 151630.00                   | 161948.00                  | 142105.00                      | 137246.00                            | 103006.00                        | 13488.00        | 20752.00                               | 24992.00                      | 0.00                 | 24992.00                     | 14462.00                  | 110661.00                      | 35684.00                | 22248.00                 | 6416.00              | 46313.00           | 42196.00            | 37236.00    | 4960.00            | 17131.00                                  | 48949.00                | 57188.00              | 0.00               | 48949.00              | 134408.00       | 0.00                        | 0.00                    | 26539.00                 | 0.00                    | 6125.00                     | 0.00                            | 22580.00       | 1287.00                | 3568.00                       | 7587.00                    | 18030.00                    | 47010.00                    | 1682.00                     |
          | Test academy school 91 | Academy converter | Greenwich              | 777053 | Test academy school 91 | Academy converter | Greenwich              | 230.00      | 1922.00                | 1674539.00       | 1215196.00                     | 800314.00          | 0.00                     | 94840.00                    | 318684.00                  | 1358.00                        | 106876.00                            | 87184.00                         | 0.00            | 19692.00                               | 30299.00                      | 0.00                 | 30299.00                     | 22358.00                  | 106281.00                      | 26771.00                | 32641.00                 | 7371.00              | 39498.00           | 27664.00            | 17402.00    | 10262.00           | 8607.00                                   | 50043.00                | 57329.00              | 0.00               | 50043.00              | 107214.00       | 0.00                        | 7997.00                 | 29424.00                 | 0.00                    | 5984.00                     | 0.00                            | 39955.00       | 88.00                  | 5478.00                       | 1919.00                    | 16369.00                    | 0.00                        | 0.00                        |
          | Test academy school 92 | Free school       | Hackney                | 777054 | Test academy school 92 | Free school       | Hackney                | 216.00      | 2135.00                | 1680594.00       | 1115052.00                     | 703564.00          | 0.00                     | 79648.00                    | 299829.00                  | 32011.00                       | 180507.00                            | 82949.00                         | 80178.00        | 17380.00                               | 41535.00                      | 0.00                 | 41535.00                     | 5431.00                   | 92731.00                       | 26025.00                | 23171.00                 | 9483.00              | 34052.00           | 21147.00            | 20654.00    | 493.00             | 8179.00                                   | 52373.00                | 63191.00              | 0.00               | 52373.00              | 163640.00       | 0.00                        | 328.00                  | 31603.00                 | 0.00                    | 6225.00                     | 0.00                            | 92036.00       | 6525.00                | 1236.00                       | 10488.00                   | 15199.00                    | 0.00                        | 0.00                        |
          | Test academy school 93 | Academy converter | Hammersmith and Fulham | 777055 | Test academy school 93 | Academy converter | Hammersmith and Fulham | 399.00      | 5754.00                | 3424906.00       | 2467694.00                     | 1575657.00         | 0.00                     | 262691.00                   | 589290.00                  | 40056.00                       | 385697.00                            | 183765.00                        | 133034.00       | 68898.00                               | 60244.00                      | 0.00                 | 60244.00                     | 9824.00                   | 228507.00                      | 72597.00                | 87146.00                 | 15500.00             | 53264.00           | 61813.00            | 53027.00    | 8786.00            | 26172.00                                  | 95978.00                | 109578.00             | 0.00               | 95978.00              | 88978.00        | 0.00                        | 0.00                    | 1493.00                  | 0.00                    | 10579.00                    | 0.00                            | 44954.00       | 11754.00               | 15389.00                      | 2311.00                    | 2498.00                     | 0.00                        | 0.00                        |
          | Test academy school 94 | Academy converter | Islington              | 777056 | Test academy school 94 | Academy converter | Islington              | 339.00      | 2120.00                | 2480660.00       | 1823492.00                     | 1259123.00         | 10292.00                 | 118624.00                   | 377589.00                  | 57864.00                       | 225264.00                            | 104273.00                        | 51652.00        | 69339.00                               | 51100.00                      | 0.00                 | 51100.00                     | 6303.00                   | 98730.00                       | 59421.00                | 28214.00                 | 11095.00             | 0.00               | 30179.00            | 23499.00    | 6680.00            | 23384.00                                  | 54988.00                | 69488.00              | 0.00               | 54988.00              | 167220.00       | 0.00                        | 4991.00                 | 51131.00                 | 0.00                    | 9348.00                     | 0.00                            | 58656.00       | 9900.00                | 1424.00                       | 2688.00                    | 29082.00                    | 0.00                        | 0.00                        |

    Scenario: Sending a valid school expenditure query request with LA code and phase
        Given a valid school expenditure query request with LA code '205' and phase 'Secondary'
        When I submit the insights expenditure request
        Then the school expenditure query result should be ok and contain:
          | SchoolName             | SchoolType        | LAName                 | URN    | SchoolName             | SchoolType        | LAName                 | TotalPupils | TotalInternalFloorArea | TotalExpenditure | TotalTeachingSupportStaffCosts | TeachingStaffCosts | SupplyTeachingStaffCosts | EducationalConsultancyCosts | EducationSupportStaffCosts | AgencySupplyTeachingStaffCosts | TotalNonEducationalSupportStaffCosts | AdministrativeClericalStaffCosts | OtherStaffCosts | ProfessionalServicesNonCurriculumCosts | TotalEducationalSuppliesCosts | ExaminationFeesCosts | LearningResourcesNonIctCosts | LearningResourcesIctCosts | TotalPremisesStaffServiceCosts | CleaningCaretakingCosts | MaintenancePremisesCosts | OtherOccupationCosts | PremisesStaffCosts | TotalUtilitiesCosts | EnergyCosts | WaterSewerageCosts | AdministrativeSuppliesNonEducationalCosts | TotalGrossCateringCosts | TotalNetCateringCosts | CateringStaffCosts | CateringSuppliesCosts | TotalOtherCosts | DirectRevenueFinancingCosts | GroundsMaintenanceCosts | IndirectEmployeeExpenses | InterestChargesLoanBank | OtherInsurancePremiumsCosts | PrivateFinanceInitiativeCharges | RentRatesCosts | SpecialFacilitiesCosts | StaffDevelopmentTrainingCosts | StaffRelatedInsuranceCosts | SupplyTeacherInsurableCosts | CommunityFocusedSchoolStaff | CommunityFocusedSchoolCosts |
          | Test academy school 93 | Academy converter | Hammersmith and Fulham | 777055 | Test academy school 93 | Academy converter | Hammersmith and Fulham | 399.00      | 5754.00                | 3424906.00       | 2467694.00                     | 1575657.00         | 0.00                     | 262691.00                   | 589290.00                  | 40056.00                       | 385697.00                            | 183765.00                        | 133034.00       | 68898.00                               | 60244.00                      | 0.00                 | 60244.00                     | 9824.00                   | 228507.00                      | 72597.00                | 87146.00                 | 15500.00             | 53264.00           | 61813.00            | 53027.00    | 8786.00            | 26172.00                                  | 95978.00                | 109578.00             | 0.00               | 95978.00              | 88978.00        | 0.00                        | 0.00                    | 1493.00                  | 0.00                    | 10579.00                    | 0.00                            | 44954.00       | 11754.00               | 15389.00                      | 2311.00                    | 2498.00                     | 0.00                        | 0.00                        |

    Scenario: Sending a valid trust expenditure request with category and dimension
        Given a trust expenditure request with company number '10192252', category 'TotalExpenditure' and dimension 'Actuals'
        When I submit the insights expenditure request
        Then the trust expenditure result should be ok and contain:
          | Field                                           | Value      |
          | CompanyNumber                                   | 10192252   |
          | SchoolTotalExpenditure                          | 5084583.00 |
          | SchoolTotalTeachingSupportStaffCosts            |            |
          | SchoolTeachingStaffCosts                        |            |
          | SchoolSupplyTeachingStaffCosts                  |            |
          | SchoolEducationalConsultancyCosts               |            |
          | SchoolEducationSupportStaffCosts                |            |
          | SchoolAgencySupplyTeachingStaffCosts            |            |
          | SchoolTotalNonEducationalSupportStaffCosts      |            |
          | SchoolAdministrativeClericalStaffCosts          |            |
          | SchoolOtherStaffCosts                           |            |
          | SchoolProfessionalServicesNonCurriculumCosts    |            |
          | SchoolTotalEducationalSuppliesCosts             |            |
          | SchoolExaminationFeesCosts                      |            |
          | SchoolLearningResourcesNonIctCosts              |            |
          | SchoolLearningResourcesIctCosts                 |            |
          | SchoolTotalPremisesStaffServiceCosts            |            |
          | SchoolCleaningCaretakingCosts                   |            |
          | SchoolMaintenancePremisesCosts                  |            |
          | SchoolOtherOccupationCosts                      |            |
          | SchoolPremisesStaffCosts                        |            |
          | SchoolTotalUtilitiesCosts                       |            |
          | SchoolEnergyCosts                               |            |
          | SchoolWaterSewerageCosts                        |            |
          | SchoolAdministrativeSuppliesNonEducationalCosts |            |
          | SchoolTotalGrossCateringCosts                   |            |
          | SchoolTotalNetCateringCosts                     |            |
          | SchoolCateringStaffCosts                        |            |
          | SchoolCateringSuppliesCosts                     |            |
          | SchoolTotalOtherCosts                           |            |
          | SchoolDirectRevenueFinancingCosts               |            |
          | SchoolGroundsMaintenanceCosts                   |            |
          | SchoolIndirectEmployeeExpenses                  |            |
          | SchoolInterestChargesLoanBank                   |            |
          | SchoolOtherInsurancePremiumsCosts               |            |
          | SchoolPrivateFinanceInitiativeCharges           |            |
          | SchoolRentRatesCosts                            |            |
          | SchoolSpecialFacilitiesCosts                    |            |
          | SchoolStaffDevelopmentTrainingCosts             |            |
          | SchoolStaffRelatedInsuranceCosts                |            |
          | SchoolSupplyTeacherInsurableCosts               |            |
          | TotalExpenditure                                | 5084583.00 |
          | TotalTeachingSupportStaffCosts                  |            |
          | TeachingStaffCosts                              |            |
          | SupplyTeachingStaffCosts                        |            |
          | EducationalConsultancyCosts                     |            |
          | EducationSupportStaffCosts                      |            |
          | AgencySupplyTeachingStaffCosts                  |            |
          | TotalNonEducationalSupportStaffCosts            |            |
          | AdministrativeClericalStaffCosts                |            |
          | OtherStaffCosts                                 |            |
          | ProfessionalServicesNonCurriculumCosts          |            |
          | TotalEducationalSuppliesCosts                   |            |
          | ExaminationFeesCosts                            |            |
          | LearningResourcesNonIctCosts                    |            |
          | LearningResourcesIctCosts                       |            |
          | TotalPremisesStaffServiceCosts                  |            |
          | CleaningCaretakingCosts                         |            |
          | MaintenancePremisesCosts                        |            |
          | OtherOccupationCosts                            |            |
          | PremisesStaffCosts                              |            |
          | TotalUtilitiesCosts                             |            |
          | EnergyCosts                                     |            |
          | WaterSewerageCosts                              |            |
          | AdministrativeSuppliesNonEducationalCosts       |            |
          | TotalGrossCateringCosts                         |            |
          | TotalNetCateringCosts                           |            |
          | CateringStaffCosts                              |            |
          | CateringSuppliesCosts                           |            |
          | TotalOtherCosts                                 |            |
          | DirectRevenueFinancingCosts                     |            |
          | GroundsMaintenanceCosts                         |            |
          | IndirectEmployeeExpenses                        |            |
          | InterestChargesLoanBank                         |            |
          | OtherInsurancePremiumsCosts                     |            |
          | PrivateFinanceInitiativeCharges                 |            |
          | RentRatesCosts                                  |            |
          | SpecialFacilitiesCosts                          |            |
          | StaffDevelopmentTrainingCosts                   |            |
          | StaffRelatedInsuranceCosts                      |            |
          | SupplyTeacherInsurableCosts                     |            |
          | CommunityFocusedSchoolStaff                     |            |
          | CommunityFocusedSchoolCosts                     |            |
        
    Scenario: Sending a valid trust expenditure request with category Utilities and dimension PerUnit  
        Given a trust expenditure request with company number '10192252', category 'Utilities' and dimension 'PerUnit'
        When I submit the insights expenditure request
        Then the trust expenditure result should be ok and contain:
          | Field                     | Value                    |
          | CompanyNumber             | 10192252                 |
          | SchoolTotalExpenditure    | 8214.1890145395799676898 |
          | SchoolTotalUtilitiesCosts | 11.3341269841269841269   |
          | SchoolEnergyCosts         | 9.6154761904761904761    |
          | SchoolWaterSewerageCosts  | 1.7186507936507936507    |
          | TotalExpenditure          | 8214.1890145395799676898 |
          | TotalUtilitiesCosts       | 11.3341269841269841269   |
          | EnergyCosts               | 9.6154761904761904761    |
          | WaterSewerageCosts        | 1.7186507936507936507    |
          
    Scenario: Sending a valid trust expenditure request with category Utilities and dimension PercentExpenditure  
        Given a trust expenditure request with company number '10192252', category 'Utilities' and dimension 'PercentExpenditure'
        When I submit the insights expenditure request
        Then the trust expenditure result should be ok and contain:
          | Field                     | Value                  |
          | CompanyNumber             | 10192252               |
          | SchoolTotalExpenditure    | 100.00000000000000000  |
          | SchoolTotalUtilitiesCosts | 1.68521194363431573    |
          | SchoolEnergyCosts         | 1.42967476388919209    |
          | SchoolWaterSewerageCosts  | 0.25553717974512364    |
          | TotalExpenditure          | 100.000000000000000000 |
          | TotalUtilitiesCosts       | 1.685211943634315730   |
          | EnergyCosts               | 1.429674763889192090   |
          | WaterSewerageCosts        | 0.255537179745123640   |
          
    Scenario: Sending a valid trust expenditure request with category Utilities and dimension PercentIncome     
        Given a trust expenditure request with company number '10192252', category 'Utilities' and dimension 'PercentIncome'
        When I submit the insights expenditure request
        Then the trust expenditure result should be ok and contain:
          | Field                     | Value                 |
          | CompanyNumber             | 10192252              |
          | SchoolTotalExpenditure    | 93.30316436521394785  |
          | SchoolTotalUtilitiesCosts | 1.57235606967134224   |
          | SchoolEnergyCosts         | 1.33393179483951732   |
          | SchoolWaterSewerageCosts  | 0.23842427483182492   |
          | TotalExpenditure          | 93.303164365213947840 |
          | TotalUtilitiesCosts       | 1.572356069671342230  |
          | EnergyCosts               | 1.333931794839517320  |
          | WaterSewerageCosts        | 0.238424274831824910  |

    Scenario: Sending a valid trust expenditure request with dimension
        Given a trust expenditure request with company number '10192252', category '' and dimension 'Actuals'
        When I submit the insights expenditure request
        Then the trust expenditure result should be ok and contain:
          | Field                                           | Value      |
          | CompanyNumber                                   | 10192252   |
          | SchoolTotalExpenditure                          | 5084583.00 |
          | SchoolTotalTeachingSupportStaffCosts            | 3639070.00 |
          | SchoolTeachingStaffCosts                        | 2440538.00 |
          | SchoolSupplyTeachingStaffCosts                  | 0.00       |
          | SchoolEducationalConsultancyCosts               | 262691.00  |
          | SchoolEducationSupportStaffCosts                | 895785.00  |
          | SchoolAgencySupplyTeachingStaffCosts            | 40056.00   |
          | SchoolTotalNonEducationalSupportStaffCosts      | 578205.00  |
          | SchoolAdministrativeClericalStaffCosts          | 327747.00  |
          | SchoolOtherStaffCosts                           | 133034.00  |
          | SchoolProfessionalServicesNonCurriculumCosts    | 117424.00  |
          | SchoolTotalEducationalSuppliesCosts             | 92972.00   |
          | SchoolExaminationFeesCosts                      | 0.00       |
          | SchoolLearningResourcesNonIctCosts              | 92972.00   |
          | SchoolLearningResourcesIctCosts                 | 0.00       |
          | SchoolTotalPremisesStaffServiceCosts            | 331257.00  |
          | SchoolCleaningCaretakingCosts                   | 103202.00  |
          | SchoolMaintenancePremisesCosts                  | 113676.00  |
          | SchoolOtherOccupationCosts                      | 21466.00   |
          | SchoolPremisesStaffCosts                        | 92913.00   |
          | SchoolTotalUtilitiesCosts                       | 85686.00   |
          | SchoolEnergyCosts                               | 72693.00   |
          | SchoolWaterSewerageCosts                        | 12993.00   |
          | SchoolAdministrativeSuppliesNonEducationalCosts | 37149.00   |
          | SchoolTotalGrossCateringCosts                   | 162358.00  |
          | SchoolTotalNetCateringCosts                     | 189153.00  |
          | SchoolCateringStaffCosts                        | 0.00       |
          | SchoolCateringSuppliesCosts                     | 162358.00  |
          | SchoolTotalOtherCosts                           | 144358.00  |
          | SchoolDirectRevenueFinancingCosts               | 0.00       |
          | SchoolGroundsMaintenanceCosts                   | 4690.00    |
          | SchoolIndirectEmployeeExpenses                  | 6560.00    |
          | SchoolInterestChargesLoanBank                   | 0.00       |
          | SchoolOtherInsurancePremiumsCosts               | 13729.00   |
          | SchoolPrivateFinanceInitiativeCharges           | 0.00       |
          | SchoolRentRatesCosts                            | 83524.00   |
          | SchoolSpecialFacilitiesCosts                    | 11754.00   |
          | SchoolStaffDevelopmentTrainingCosts             | 19292.00   |
          | SchoolStaffRelatedInsuranceCosts                | 2311.00    |
          | SchoolSupplyTeacherInsurableCosts               | 2498.00    |
          | TotalExpenditure                                | 5084583.00 |
          | TotalTeachingSupportStaffCosts                  | 3639070.00 |
          | TeachingStaffCosts                              | 2440538.00 |
          | SupplyTeachingStaffCosts                        | 0.00       |
          | EducationalConsultancyCosts                     | 262691.00  |
          | EducationSupportStaffCosts                      | 895785.00  |
          | AgencySupplyTeachingStaffCosts                  | 40056.00   |
          | TotalNonEducationalSupportStaffCosts            | 578205.00  |
          | AdministrativeClericalStaffCosts                | 327747.00  |
          | OtherStaffCosts                                 | 133034.00  |
          | ProfessionalServicesNonCurriculumCosts          | 117424.00  |
          | TotalEducationalSuppliesCosts                   | 92972.00   |
          | ExaminationFeesCosts                            | 0.00       |
          | LearningResourcesNonIctCosts                    | 92972.00   |
          | LearningResourcesIctCosts                       | 13529.00   |
          | TotalPremisesStaffServiceCosts                  | 331257.00  |
          | CleaningCaretakingCosts                         | 103202.00  |
          | MaintenancePremisesCosts                        | 113676.00  |
          | OtherOccupationCosts                            | 21466.00   |
          | PremisesStaffCosts                              | 92913.00   |
          | TotalUtilitiesCosts                             | 85686.00   |
          | EnergyCosts                                     | 72693.00   |
          | WaterSewerageCosts                              | 12993.00   |
          | AdministrativeSuppliesNonEducationalCosts       | 37149.00   |
          | TotalGrossCateringCosts                         | 162358.00  |
          | TotalNetCateringCosts                           | 189153.00  |
          | CateringStaffCosts                              | 0.00       |
          | CateringSuppliesCosts                           | 162358.00  |
          | TotalOtherCosts                                 | 144358.00  |
          | DirectRevenueFinancingCosts                     | 0.00       |
          | GroundsMaintenanceCosts                         | 4690.00    |
          | IndirectEmployeeExpenses                        | 6560.00    |
          | InterestChargesLoanBank                         | 0.00       |
          | OtherInsurancePremiumsCosts                     | 13729.00   |
          | PrivateFinanceInitiativeCharges                 | 0.00       |
          | RentRatesCosts                                  | 83524.00   |
          | SpecialFacilitiesCosts                          | 11754.00   |
          | StaffDevelopmentTrainingCosts                   | 19292.00   |
          | StaffRelatedInsuranceCosts                      | 2311.00    |
          | SupplyTeacherInsurableCosts                     | 2498.00    |
          | CommunityFocusedSchoolStaff                     |            |
          | CommunityFocusedSchoolCosts                     |            |

    Scenario: Sending a trust expenditure request with bad company number
        Given a trust expenditure request with company number '10000000', category '' and dimension 'Actuals'
        When I submit the insights expenditure request
        Then the trust expenditure result should be not found

    Scenario: Sending an invalid trust expenditure request
        Given a trust expenditure request with company number '10192252', category 'Invalid' and dimension ''
        When I submit the insights expenditure request
        Then the trust expenditure result should be bad request

    Scenario: Sending a valid trust expenditure history request
        Given a valid trust expenditure history request with company number '10192252'
        When I submit the insights expenditure request
        Then the trust expenditure history result should be ok and contain:
          | Year | TotalExpenditure | TotalTeachingSupportStaffCosts | TeachingStaffCosts | SupplyTeachingStaffCosts | EducationalConsultancyCosts | EducationSupportStaffCosts | AgencySupplyTeachingStaffCosts | TotalNonEducationalSupportStaffCosts | AdministrativeClericalStaffCosts | OtherStaffCosts | ProfessionalServicesNonCurriculumCosts | TotalEducationalSuppliesCosts | ExaminationFeesCosts | LearningResourcesNonIctCosts | LearningResourcesIctCosts | TotalPremisesStaffServiceCosts | CleaningCaretakingCosts | MaintenancePremisesCosts | OtherOccupationCosts | PremisesStaffCosts | TotalUtilitiesCosts | EnergyCosts | WaterSewerageCosts | AdministrativeSuppliesNonEducationalCosts | TotalGrossCateringCosts | TotalNetCateringCosts | CateringStaffCosts | CateringSuppliesCosts | TotalOtherCosts | DirectRevenueFinancingCosts | GroundsMaintenanceCosts | IndirectEmployeeExpenses | InterestChargesLoanBank | OtherInsurancePremiumsCosts | PrivateFinanceInitiativeCharges | RentRatesCosts | SpecialFacilitiesCosts | StaffDevelopmentTrainingCosts | StaffRelatedInsuranceCosts | SupplyTeacherInsurableCosts | CommunityFocusedSchoolStaff | CommunityFocusedSchoolCosts |
          | 2022 | 5084583.00       | 3639070.00                     | 2440538.00         | 0.00                     | 262691.00                   | 895785.00                  | 40056.00                       | 578205.00                            | 327747.00                        | 133034.00       | 117424.00                              | 92972.00                      | 0.00                 | 92972.00                     | 13529.00                  | 331257.00                      | 103202.00               | 113676.00                | 21466.00             | 92913.00           | 85686.00            | 72693.00    | 12993.00           | 37149.00                                  | 162358.00               | 189153.00             | 0.00               | 162358.00             | 144358.00       | 0.00                        | 4690.00                 | 6560.00                  | 0.00                    | 13729.00                    | 0.00                            | 83524.00       | 11754.00               | 19292.00                      | 2311.00                    | 2498.00                     |                             |                             |

    Scenario: Sending a valid trust expenditure query request
        Given a valid trust expenditure query request with company numbers:
          | CompanyNumber |
          | 10249712      |
          | 10259334      |
          | 10264735      |
        When I submit the insights expenditure request
        Then the trust expenditure query result should be ok and contain:
          | CompanyNumber | SchoolTotalExpenditure | SchoolTotalTeachingSupportStaffCosts | SchoolTeachingStaffCosts | SchoolSupplyTeachingStaffCosts | SchoolEducationalConsultancyCosts | SchoolEducationSupportStaffCosts | SchoolAgencySupplyTeachingStaffCosts | SchoolTotalNonEducationalSupportStaffCosts | SchoolAdministrativeClericalStaffCosts | SchoolOtherStaffCosts | SchoolProfessionalServicesNonCurriculumCosts | SchoolTotalEducationalSuppliesCosts | SchoolExaminationFeesCosts | SchoolLearningResourcesNonIctCosts | SchoolLearningResourcesIctCosts | SchoolTotalPremisesStaffServiceCosts | SchoolCleaningCaretakingCosts | SchoolMaintenancePremisesCosts | SchoolOtherOccupationCosts | SchoolPremisesStaffCosts | SchoolTotalUtilitiesCosts | SchoolEnergyCosts | SchoolWaterSewerageCosts | SchoolAdministrativeSuppliesNonEducationalCosts | SchoolTotalGrossCateringCosts | SchoolTotalNetCateringCosts | SchoolCateringStaffCosts | SchoolCateringSuppliesCosts | SchoolTotalOtherCosts | SchoolDirectRevenueFinancingCosts | SchoolGroundsMaintenanceCosts | SchoolIndirectEmployeeExpenses | SchoolInterestChargesLoanBank | SchoolOtherInsurancePremiumsCosts | SchoolPrivateFinanceInitiativeCharges | SchoolRentRatesCosts | SchoolSpecialFacilitiesCosts | SchoolStaffDevelopmentTrainingCosts | SchoolStaffRelatedInsuranceCosts | SchoolSupplyTeacherInsurableCosts | TotalExpenditure | TotalTeachingSupportStaffCosts | TeachingStaffCosts | SupplyTeachingStaffCosts | EducationalConsultancyCosts | EducationSupportStaffCosts | AgencySupplyTeachingStaffCosts | TotalNonEducationalSupportStaffCosts | AdministrativeClericalStaffCosts | OtherStaffCosts | ProfessionalServicesNonCurriculumCosts | TotalEducationalSuppliesCosts | ExaminationFeesCosts | LearningResourcesNonIctCosts | LearningResourcesIctCosts | TotalPremisesStaffServiceCosts | CleaningCaretakingCosts | MaintenancePremisesCosts | OtherOccupationCosts | PremisesStaffCosts | TotalUtilitiesCosts | EnergyCosts | WaterSewerageCosts | AdministrativeSuppliesNonEducationalCosts | TotalGrossCateringCosts | TotalNetCateringCosts | CateringStaffCosts | CateringSuppliesCosts | TotalOtherCosts | DirectRevenueFinancingCosts | GroundsMaintenanceCosts | IndirectEmployeeExpenses | InterestChargesLoanBank | OtherInsurancePremiumsCosts | PrivateFinanceInitiativeCharges | RentRatesCosts | SpecialFacilitiesCosts | StaffDevelopmentTrainingCosts | StaffRelatedInsuranceCosts | SupplyTeacherInsurableCosts | CommunityFocusedSchoolStaff | CommunityFocusedSchoolCosts |
| 10249712      | 5072668.00             | 3668096.00                           | 1377868.00               | 0.00                           | 236950.00                         | 1001271.00                       | 1052007.00                           | 500711.00                                  | 285658.00                              | 28104.00              | 186949.00                                    | 139350.00                           | 5344.00                    | 134006.00                          | 0.00                            | 344765.00                            | 39980.00                      | 152722.00                      | 4426.00                    | 147637.00                | 52487.00                  | 32052.00          | 20435.00                 | 100195.00                                       | 84190.00                      | 95553.00                    | 0.00                     | 84190.00                    | 107706.00             | 0.00                              | 3983.00                       | 39047.00                       | 0.00                          | 11762.00                          | 0.00                                  | 0.00                 | 4520.00                      | 28135.00                            | 7364.00                          | 12895.00                          | 5072668.00       | 3668096.00                     | 1377868.00         | 0.00                     | 236950.00                   | 1001271.00                 | 1052007.00                     | 500711.00                            | 285658.00                        | 28104.00        | 186949.00                              | 139350.00                     | 5344.00              | 134006.00                    | 75168.00                  | 344765.00                      | 39980.00                | 152722.00                | 4426.00              | 147637.00          | 52487.00            | 32052.00    | 20435.00           | 100195.00                                 | 84190.00                | 95553.00              | 0.00               | 84190.00              | 107706.00       | 0.00                        | 3983.00                 | 39047.00                 | 0.00                    | 11762.00                    | 0.00                            | 0.00           | 4520.00                | 28135.00                      | 7364.00                    | 12895.00                    |                             |                             |
 | 10259334      | 14665108.00            | 10240640.00                          | 8321911.00               | 20771.00                       | 537911.00                         | 1176654.00                       | 183393.00                            | 1299564.00                                 | 901007.00                              | 30378.00              | 368179.00                                    | 470172.00                           | 77694.00                   | 392478.00                          | 0.00                            | 836523.00                            | 147297.00                     | 237861.00                      | 32663.00                   | 418702.00                | 188224.00                 | 176642.00         | 11582.00                 | 364710.00                                       | 474250.00                     | 529524.00                   | 132993.00                | 341257.00                   | 505378.00             | 0.00                              | 0.00                          | 168947.00                      | 0.00                          | 73076.00                          | 0.00                                  | 25110.00             | 166721.00                    | 26592.00                            | 5068.00                          | 39864.00                          | 14665108.00      | 10240640.00                    | 8321911.00         | 20771.00                 | 537911.00                   | 1176654.00                 | 183393.00                      | 1299564.00                           | 901007.00                        | 30378.00        | 368179.00                              | 470172.00                     | 77694.00             | 392478.00                    | 285646.00                 | 836523.00                      | 147297.00               | 237861.00                | 32663.00             | 418702.00          | 188224.00           | 176642.00   | 11582.00           | 364710.00                                 | 474250.00               | 529524.00             | 132993.00          | 341257.00             | 505378.00       | 0.00                        | 0.00                    | 168947.00                | 0.00                    | 73076.00                    | 0.00                            | 25110.00       | 166721.00              | 26592.00                      | 5068.00                    | 39864.00                    |                             |                             |
 | 10264735      | 4260179.00             | 2973757.00                           | 1440362.00               | 0.00                           | 103151.00                         | 1394931.00                       | 35313.00                             | 447930.00                                  | 215178.00                              | 114615.00             | 118137.00                                    | 94732.00                            | 0.00                       | 94732.00                           | 0.00                            | 278898.00                            | 26116.00                      | 189034.00                      | 18543.00                   | 45205.00                 | 50543.00                  | 42238.00          | 8305.00                  | 44722.00                                        | 193648.00                     | 201810.00                   | 69353.00                 | 124295.00                   | 129832.00             | 0.00                              | 3929.00                       | 4918.00                        | 0.00                          | 10845.00                          | 0.00                                  | 62164.00             | 13804.00                     | 4893.00                             | 14051.00                         | 15228.00                          | 4260179.00       | 2973757.00                     | 1440362.00         | 0.00                     | 103151.00                   | 1394931.00                 | 35313.00                       | 447930.00                            | 215178.00                        | 114615.00       | 118137.00                              | 94732.00                      | 0.00                 | 94732.00                     | 46112.00                  | 278898.00                      | 26116.00                | 189034.00                | 18543.00             | 45205.00           | 50543.00            | 42238.00    | 8305.00            | 44722.00                                  | 193648.00               | 201810.00             | 69353.00           | 124295.00             | 129832.00       | 0.00                        | 3929.00                 | 4918.00                  | 0.00                    | 10845.00                    | 0.00                            | 62164.00       | 13804.00               | 4893.00                       | 14051.00                   | 15228.00                    |                             |                             |

    Scenario: Sending a valid school average across comparator set expenditure history request with dimension Actuals
        Given a school average across comparator set expenditure history request with urn '990000' and dimension 'Actuals'
        When I submit the insights expenditure request
        Then the school expenditure history result should be ok and contain:
          | Year | TotalExpenditure | TotalTeachingSupportStaffCosts | TeachingStaffCosts | SupplyTeachingStaffCosts | EducationalConsultancyCosts | EducationSupportStaffCosts | AgencySupplyTeachingStaffCosts | TotalNonEducationalSupportStaffCosts | AdministrativeClericalStaffCosts | OtherStaffCosts | ProfessionalServicesNonCurriculumCosts | TotalEducationalSuppliesCosts | ExaminationFeesCosts | LearningResourcesNonIctCosts | LearningResourcesIctCosts | TotalPremisesStaffServiceCosts | CleaningCaretakingCosts | MaintenancePremisesCosts | OtherOccupationCosts | PremisesStaffCosts | TotalUtilitiesCosts | EnergyCosts | WaterSewerageCosts | AdministrativeSuppliesNonEducationalCosts | TotalGrossCateringCosts | TotalNetCateringCosts | CateringStaffCosts | CateringSuppliesCosts | TotalOtherCosts | DirectRevenueFinancingCosts | GroundsMaintenanceCosts | IndirectEmployeeExpenses | OtherInsurancePremiumsCosts | RentRatesCosts | SpecialFacilitiesCosts | StaffDevelopmentTrainingCosts | StaffRelatedInsuranceCosts | SupplyTeacherInsurableCosts |
          | 2022 | 3222372.8        | 2317101.233333                 | 1628552.5          | 10292                    | 156853.4                    | 459429.966666              | 71922.3                        | 284207.166666                        | 188622.833333                    | 59343.318181    | 52065.9                                | 90801.5                       | 82380.8              | 77071.366666                 | 38662.366666              | 198889.8                       | 63845.428571            | 59272.4                  | 16960.75             | 80247.875          | 43712.75            | 36940       | 7585.48            | 29847.766666                              | 74437                   | 82849.766666          | 2170               | 74364.666666          | 150318.2        | 22488.5                     | 4365.655172             | 48109.833333             | 12396                       | 108581.071428  | 6065.111111            | 9663.1                        | 6206.448275                | 13522.357142                |

    Scenario: Sending a valid school average across comparator set expenditure history request with dimension PerUnit
        Given a school average across comparator set expenditure history request with urn '990000' and dimension 'PerUnit'
        When I submit the insights expenditure request
        Then the school expenditure history result should be ok and contain:
          | Year | TotalExpenditure          | TotalTeachingSupportStaffCosts | TeachingStaffCosts       | SupplyTeachingStaffCosts | EducationalConsultancyCosts | EducationSupportStaffCosts | AgencySupplyTeachingStaffCosts | TotalNonEducationalSupportStaffCosts | AdministrativeClericalStaffCosts | OtherStaffCosts         | ProfessionalServicesNonCurriculumCosts | TotalEducationalSuppliesCosts | ExaminationFeesCosts   | LearningResourcesNonIctCosts | LearningResourcesIctCosts | TotalPremisesStaffServiceCosts | CleaningCaretakingCosts | MaintenancePremisesCosts | OtherOccupationCosts  | PremisesStaffCosts     | TotalUtilitiesCosts    | EnergyCosts           | WaterSewerageCosts    | AdministrativeSuppliesNonEducationalCosts | TotalGrossCateringCosts | TotalNetCateringCosts   | CateringStaffCosts    | CateringSuppliesCosts   | TotalOtherCosts         | DirectRevenueFinancingCosts | GroundsMaintenanceCosts | IndirectEmployeeExpenses | OtherInsurancePremiumsCosts | RentRatesCosts          | SpecialFacilitiesCosts | StaffDevelopmentTrainingCosts | StaffRelatedInsuranceCosts | SupplyTeacherInsurableCosts |
          | 2022 | 11359.8090094430902266004 | 8490.5379145541410516932       | 5665.6053578322446012308 | 30.3598820058997050147   | 503.9763766196622418660     | 2139.0805999223783679636   | 180.8635841129925171320        | 974.7166481467850138011              | 541.7900250059146429097          | 330.3692531904499494480 | 190.6558374678737412961                | 239.1963225636258182662       | 79.0466643339945074988 | 226.0218785079600670163      | 100.6222760422114892725   | 55.1737576349836982831         | 17.0267412012855107206  | 16.0019809189030388497   | 4.9747470895744400960 | 23.2963178890975483388 | 11.0197855269639050988 | 8.6534736721758784327 | 2.6502692773625898660 | 89.4083453146734790215                    | 219.5258321136751276859 | 250.2590228737802783486 | 5.1913875598086124401 | 219.3527858616815072712 | 450.0981442212121004242 | 31.2864157644668673467      | 13.5963496355898072243  | 192.4571179823647904371  | 29.5278001847090519462      | 182.6825454970443042999 | 17.0513538005303566098 | 50.5292013692875394591        | 22.1137234933371702227     | 46.5108196921644665326      |

    Scenario: Sending a valid school average across comparator set expenditure history request with dimension PercentIncome
        Given a school average across comparator set expenditure history request with urn '990000' and dimension 'PercentIncome'
        When I submit the insights expenditure request
        Then the school expenditure history result should be ok and contain:
          | Year | TotalExpenditure      | TotalTeachingSupportStaffCosts | TeachingStaffCosts    | SupplyTeachingStaffCosts | EducationalConsultancyCosts | EducationSupportStaffCosts | AgencySupplyTeachingStaffCosts | TotalNonEducationalSupportStaffCosts | AdministrativeClericalStaffCosts | OtherStaffCosts      | ProfessionalServicesNonCurriculumCosts | TotalEducationalSuppliesCosts | ExaminationFeesCosts | LearningResourcesNonIctCosts | LearningResourcesIctCosts | TotalPremisesStaffServiceCosts | CleaningCaretakingCosts | MaintenancePremisesCosts | OtherOccupationCosts | PremisesStaffCosts   | TotalUtilitiesCosts  | EnergyCosts          | WaterSewerageCosts   | AdministrativeSuppliesNonEducationalCosts | TotalGrossCateringCosts | TotalNetCateringCosts | CateringStaffCosts   | CateringSuppliesCosts | TotalOtherCosts      | DirectRevenueFinancingCosts | GroundsMaintenanceCosts | IndirectEmployeeExpenses | OtherInsurancePremiumsCosts | RentRatesCosts       | SpecialFacilitiesCosts | StaffDevelopmentTrainingCosts | StaffRelatedInsuranceCosts | SupplyTeacherInsurableCosts |
          | 2022 | 98.995221742929042702 | 70.565108455664846790          | 46.758988468780213601 | 0.425128515457998310     | 5.797469898809304109        | 15.753488576890265343      | 2.240990560669797110           | 8.632609096632581231                 | 5.733195498142186092             | 1.852242470249516817 | 1.541102453640749464                   | 2.718365957488926634          | 0.955549966641254944 | 2.559107629715384142         | 1.214878439289032772      | 5.746869272472989132           | 1.783513694915195465    | 1.724198396038374639     | 0.506163469544749017 | 2.357048570339999535 | 1.177279432199800502 | 0.944565562269669733 | 0.260639534321746457 | 0.844529696262088799                      | 2.617768303613511290    | 3.009026542018567352  | 0.074510761311128430 | 2.615284611569807009  | 4.505382401686166325 | 0.378813515531051160        | 0.149608373855236579    | 1.545369410076941621     | 0.375188567189085604        | 2.304046256282603153 | 0.220898150139898785   | 0.390109954773032451          | 0.265001148424797042       | 0.573917485381907018        |

    Scenario: Sending a valid school average across comparator set expenditure history request with dimension PercentExpenditure
        Given a school average across comparator set expenditure history request with urn '990000' and dimension 'PercentExpenditure'
        When I submit the insights expenditure request
        Then the school expenditure history result should be ok and contain:
          | Year | TotalExpenditure       | TotalTeachingSupportStaffCosts | TeachingStaffCosts    | SupplyTeachingStaffCosts | EducationalConsultancyCosts | EducationSupportStaffCosts | AgencySupplyTeachingStaffCosts | TotalNonEducationalSupportStaffCosts | AdministrativeClericalStaffCosts | OtherStaffCosts      | ProfessionalServicesNonCurriculumCosts | TotalEducationalSuppliesCosts | ExaminationFeesCosts | LearningResourcesNonIctCosts | LearningResourcesIctCosts | TotalPremisesStaffServiceCosts | CleaningCaretakingCosts | MaintenancePremisesCosts | OtherOccupationCosts | PremisesStaffCosts   | TotalUtilitiesCosts  | EnergyCosts          | WaterSewerageCosts   | AdministrativeSuppliesNonEducationalCosts | TotalGrossCateringCosts | TotalNetCateringCosts | CateringStaffCosts   | CateringSuppliesCosts | TotalOtherCosts      | DirectRevenueFinancingCosts | GroundsMaintenanceCosts | IndirectEmployeeExpenses | OtherInsurancePremiumsCosts | RentRatesCosts       | SpecialFacilitiesCosts | StaffDevelopmentTrainingCosts | StaffRelatedInsuranceCosts | SupplyTeacherInsurableCosts |
          | 2022 | 100.000000000000000000 | 71.302064712541259609          | 47.297083017906512286 | 0.414889585836027500     | 5.840514188680241870        | 15.892129186062924490      | 2.258508667030380029           | 8.729809633157371926                 | 5.789175769193441405             | 1.871486378972294512 | 1.568210519384247870                   | 2.747757615582123735          | 0.990147633749085128 | 2.582733009957276212         | 1.227736677368743364      | 5.808636606234489134           | 1.800776825724163150    | 1.737955049829240169     | 0.513907932579167177 | 2.387886394152675807 | 1.190324842905228983 | 0.954263908582836529 | 0.264388246441079544 | 0.856240798609601638                      | 2.632869986706074397    | 3.024821926581822610  | 0.077763730136204780 | 2.630277862368200904  | 4.532398403688079380 | 0.392975033600374232        | 0.149393315666057057    | 1.562455145784085350     | 0.377749866339498632        | 2.309881569170330060 | 0.221865246598191884   | 0.393252833790803774          | 0.267954601797139124       | 0.570046806615160240        |

    Scenario: Sending an invalid school average across comparator set expenditure history request
        Given a school average across comparator set expenditure history request with urn '990000' and dimension 'invalid'
        When I submit the insights expenditure request
        Then the school expenditure result should be bad request

    Scenario: Sending a valid school national average expenditure history request with dimension Actuals, phase Primary, financeType Maintained
        Given a school national average expenditure history request with dimension 'Actuals', phase 'Primary', financeType 'Maintained'
        When I submit the insights expenditure request
        Then the school expenditure history result should be ok and contain:
          | Year | TotalExpenditure | TotalTeachingSupportStaffCosts | TeachingStaffCosts | SupplyTeachingStaffCosts | EducationalConsultancyCosts | EducationSupportStaffCosts | AgencySupplyTeachingStaffCosts | TotalNonEducationalSupportStaffCosts | AdministrativeClericalStaffCosts | OtherStaffCosts | ProfessionalServicesNonCurriculumCosts | TotalEducationalSuppliesCosts | ExaminationFeesCosts | LearningResourcesNonIctCosts | LearningResourcesIctCosts | TotalPremisesStaffServiceCosts | CleaningCaretakingCosts | MaintenancePremisesCosts | OtherOccupationCosts | PremisesStaffCosts | TotalUtilitiesCosts | EnergyCosts  | WaterSewerageCosts | AdministrativeSuppliesNonEducationalCosts | TotalGrossCateringCosts | TotalNetCateringCosts | CateringStaffCosts | CateringSuppliesCosts | TotalOtherCosts | DirectRevenueFinancingCosts | GroundsMaintenanceCosts | IndirectEmployeeExpenses | OtherInsurancePremiumsCosts | RentRatesCosts | SpecialFacilitiesCosts | StaffDevelopmentTrainingCosts | StaffRelatedInsuranceCosts | SupplyTeacherInsurableCosts |
          | 2021 | 3128083.326732   | 2267007.712871                 | 1548551.212871     | 51041.969696             | 87074.719387                | 568897.267326              | 65497.160000                   | 268613.762376                        | 160641.805970                    | 54886.849462    | 58517.527363                           | 75583.861386                  | 73458.172413         | 65037.886138                 | 38159.572139              | 185390.410891                  | 48696.115000            | 53314.688442             | 18392.462311         | 72369.091397       | 45458.495049        | 38547.509900 | 7267.112820        | 30234.722772                              | 91369.559405            | 105243.702970         | 127048.741935      | 72590.700000          | 139740.500000   | 65455.341463                | 6283.825000             | 12481.286458             | 10734.763819                | 52428.352601   | 10870.585714           | 10169.450000                  | 5195.715789                | 6324.507575                 |
          | 2022 | 3586858.804020   | 2546623.809045                 | 1846618.492462     | 10181.833333             | 148858.105527               | 485498.643216              | 68152.338624                   | 312768.522613                        | 212438.738693                    | 47497.843373    | 60708.467336                           | 105919.557788                 | 75307.400000         | 85105.954773                 | 48733.010362              | 209901.402010                  | 53657.198979            | 65201.356783             | 18018.124352         | 81773.480662       | 44952.296296        | 39068.169312 | 6766.770114        | 38466.959798                              | 81002.291457            | 93458.864321          | 122895.576923      | 64945.582914          | 209367.683417   | 34696.463414                | 4584.464516             | 54031.763819             | 13330.678391                | 162539.051948  | 5625.759689            | 8606.633165                   | 7196.952631                | 12043.101694                |

    Scenario: Sending a valid school national average expenditure history request with dimension PerUnit, phase Primary, financeType Maintained
        Given a school national average expenditure history request with dimension 'PerUnit', phase 'Primary', financeType 'Maintained'
        When I submit the insights expenditure request
        Then the school expenditure history result should be ok and contain:
          | Year | TotalExpenditure          | TotalTeachingSupportStaffCosts | TeachingStaffCosts       | SupplyTeachingStaffCosts | EducationalConsultancyCosts | EducationSupportStaffCosts | AgencySupplyTeachingStaffCosts | TotalNonEducationalSupportStaffCosts | AdministrativeClericalStaffCosts | OtherStaffCosts         | ProfessionalServicesNonCurriculumCosts | TotalEducationalSuppliesCosts | ExaminationFeesCosts   | LearningResourcesNonIctCosts | LearningResourcesIctCosts | TotalPremisesStaffServiceCosts | CleaningCaretakingCosts | MaintenancePremisesCosts | OtherOccupationCosts  | PremisesStaffCosts     | TotalUtilitiesCosts    | EnergyCosts           | WaterSewerageCosts    | AdministrativeSuppliesNonEducationalCosts | TotalGrossCateringCosts | TotalNetCateringCosts   | CateringStaffCosts      | CateringSuppliesCosts   | TotalOtherCosts         | DirectRevenueFinancingCosts | GroundsMaintenanceCosts | IndirectEmployeeExpenses | OtherInsurancePremiumsCosts | RentRatesCosts          | SpecialFacilitiesCosts | StaffDevelopmentTrainingCosts | StaffRelatedInsuranceCosts | SupplyTeacherInsurableCosts |
          | 2021 | 10704.2119586775638608101 | 7911.2048034012867013348       | 4574.6446405680496856894 | 688.5632882684160807990  | 374.8151521904963529406     | 2693.3870521952846997380   | 192.8180312069531800913        | 994.0891866432290066278              | 610.1219252535007086302          | 174.9344911439100524532 | 227.0333003642355579493                | 209.3765152589851793528       | 88.1669327498054001431 | 196.7188862998547011144      | 101.8630922496117280098   | 52.8148781287138761047         | 13.0653263659310373620  | 13.8905267595859613720   | 5.6352399511786053617 | 22.4856138194570434604 | 12.0230963874087704451 | 9.9444735846491239569 | 2.1930676010307277615 | 102.1852063482416869253                   | 286.0580880389503652214 | 321.1060129099418151020 | 500.3931909104874321644 | 211.3577243282143168882 | 363.6176473429551317763 | 170.0161894349888526624     | 21.2586884896587717473  | 40.6018788173930295090   | 34.1423884333251271847      | 104.9206849108246580645 | 61.0839161709051678993 | 46.7432641843145896003        | 16.5389563298938839733     | 25.2382409072384521442      |
          | 2022 | 11017.8854632454975149712 | 8101.2565999182545286806       | 5345.9766973953768399298 | 20.6170923917056471188   | 527.6688729436489112063     | 2042.7012098807526937068   | 192.7298754335769261138        | 938.2320988257683433382              | 567.1663422377784635763          | 226.7111446527049974409 | 181.9499273802058115447                | 249.0994465292719272669       | 80.5700621283320839507 | 226.8313389058635623559      | 106.3441427436523678311   | 64.7261409797345818810         | 15.1955495352989160443  | 20.3128301016119352787   | 8.2697576189386790384 | 23.5572261622798567663 | 10.9458258268157915720 | 9.1750746473539197224 | 2.0927403187674748863 | 101.9885599659769157580                   | 233.1390966402454650949 | 269.8953479812824490174 | 212.4453670596004211415 | 205.3824155168805859508 | 500.7615440824887265204 | 98.3387290216898272478      | 13.3376438419547414731  | 182.5397609940592229609  | 29.1128932206165978660      | 218.8987321089056612245 | 30.8978224943783229482 | 38.0812659509126772509        | 26.3110059151211537142     | 46.7050387594170469195      |

    Scenario: Sending a valid school national average expenditure history request with dimension PercentIncome, phase Primary, financeType Maintained
        Given a school national average expenditure history request with dimension 'PercentIncome', phase 'Primary', financeType 'Maintained'
        When I submit the insights expenditure request
        Then the school expenditure history result should be ok and contain:
          | Year | TotalExpenditure      | TotalTeachingSupportStaffCosts | TeachingStaffCosts    | SupplyTeachingStaffCosts | EducationalConsultancyCosts | EducationSupportStaffCosts | AgencySupplyTeachingStaffCosts | TotalNonEducationalSupportStaffCosts | AdministrativeClericalStaffCosts | OtherStaffCosts      | ProfessionalServicesNonCurriculumCosts | TotalEducationalSuppliesCosts | ExaminationFeesCosts | LearningResourcesNonIctCosts | LearningResourcesIctCosts | TotalPremisesStaffServiceCosts | CleaningCaretakingCosts | MaintenancePremisesCosts | OtherOccupationCosts | PremisesStaffCosts   | TotalUtilitiesCosts  | EnergyCosts          | WaterSewerageCosts   | AdministrativeSuppliesNonEducationalCosts | TotalGrossCateringCosts | TotalNetCateringCosts | CateringStaffCosts   | CateringSuppliesCosts | TotalOtherCosts      | DirectRevenueFinancingCosts | GroundsMaintenanceCosts | IndirectEmployeeExpenses | OtherInsurancePremiumsCosts | RentRatesCosts       | SpecialFacilitiesCosts | StaffDevelopmentTrainingCosts | StaffRelatedInsuranceCosts | SupplyTeacherInsurableCosts |
          | 2021 | 98.683224839375800770 | 71.535895566793988139          | 47.474677927815156001 | 1.597610846990668596     | 3.005625400716150963        | 19.085730513833809566      | 2.076228869775992984           | 9.315266685437214097                 | 5.249390221841115497             | 2.058027483540699390 | 2.207777730993447482                   | 2.051887029467536724          | 0.736013014132248658 | 1.946221794765382212         | 1.103105068494307012      | 5.988062688412718146           | 1.568744972977636203    | 1.555717292256553455     | 0.622085308001958818 | 2.492558401510335087 | 1.416627208444464587 | 1.175485313034172045 | 0.254876887631737872 | 0.965381310866419900                      | 3.011208064734561788    | 3.520550937759482163  | 2.556865583328686100 | 2.645005979965961060  | 3.539687997677436817 | 1.174351421676172181        | 0.237814595232035513    | 0.359155460851473363     | 0.383459545245950708        | 1.361325366060635194 | 0.402730756230272506   | 0.301203149543341344          | 0.176139088211475534       | 0.288611468418512541        |
          | 2022 | 99.483974671514908949 | 70.136301465441491368          | 46.660123360732175389 | 0.245104012544934536     | 5.207952369244560810        | 16.131470200911546392      | 2.226468143652273657           | 8.761342944547734417                 | 5.811201033762698985             | 1.553170501009401727 | 1.654532347128951571                   | 2.798799112096094509          | 0.896024242304377234 | 2.551154221006945022         | 1.227417990626322601      | 6.874125327808514259           | 1.651073296907960288    | 2.208567798808938485     | 0.778879070540863036 | 2.511115588190987809 | 1.258022848339639187 | 1.060571659552231876 | 0.230145082544061116 | 1.029788859991614575                      | 2.648279828979503694    | 3.123967031565880491  | 2.314310316200528089 | 2.345907626862851782  | 5.112892176467928981 | 1.275413855806177653        | 0.150325185292738981    | 1.545990201268648817     | 0.365998484534667578        | 2.656885450636727159 | 0.228811850228271102   | 0.299958624920170505          | 0.330684847564988407       | 0.595830063734340319        |

    Scenario: Sending a valid school national average expenditure history request with dimension PercentExpenditure, phase Primary, financeType Maintained
        Given a school national average expenditure history request with dimension 'PercentExpenditure', phase 'Primary', financeType 'Maintained'
        When I submit the insights expenditure request
        Then the school expenditure history result should be ok and contain:
          | Year | TotalExpenditure       | TotalTeachingSupportStaffCosts | TeachingStaffCosts    | SupplyTeachingStaffCosts | EducationalConsultancyCosts | EducationSupportStaffCosts | AgencySupplyTeachingStaffCosts | TotalNonEducationalSupportStaffCosts | AdministrativeClericalStaffCosts | OtherStaffCosts      | ProfessionalServicesNonCurriculumCosts | TotalEducationalSuppliesCosts | ExaminationFeesCosts | LearningResourcesNonIctCosts | LearningResourcesIctCosts | TotalPremisesStaffServiceCosts | CleaningCaretakingCosts | MaintenancePremisesCosts | OtherOccupationCosts | PremisesStaffCosts   | TotalUtilitiesCosts  | EnergyCosts          | WaterSewerageCosts   | AdministrativeSuppliesNonEducationalCosts | TotalGrossCateringCosts | TotalNetCateringCosts | CateringStaffCosts   | CateringSuppliesCosts | TotalOtherCosts      | DirectRevenueFinancingCosts | GroundsMaintenanceCosts | IndirectEmployeeExpenses | OtherInsurancePremiumsCosts | RentRatesCosts       | SpecialFacilitiesCosts | StaffDevelopmentTrainingCosts | StaffRelatedInsuranceCosts | SupplyTeacherInsurableCosts |
          | 2021 | 100.000000000000000000 | 72.512320874731527948          | 48.216905212546694085 | 1.665783469488526156     | 3.056660935001801667        | 19.235431875863366343      | 2.103790600964156058           | 9.407155115906779183                 | 5.294877121316616541             | 2.072560759763362823 | 2.241187714987781010                   | 2.088105311932321359          | 0.761081996535528331 | 1.978841064904943528         | 1.122201060699670065      | 6.074307392897383744           | 1.585660799105455127    | 1.584087457301481712     | 0.632296418276196604 | 2.527165509713322895 | 1.436746323311583573 | 1.193580724033579664 | 0.257265425257574297 | 0.975226429587645293                      | 3.049336469594728387    | 3.561937058275016603  | 2.599486259372008171 | 2.676909464088014404  | 3.580594029974684922 | 1.184228439913505767        | 0.240931972358074013    | 0.366149978281210991     | 0.389375382019989189        | 1.379313855933648736 | 0.407050797435654808   | 0.306741302470707740          | 0.176062977243803615       | 0.291754622850232187        |
          | 2022 | 100.000000000000000000 | 70.566807058842532720          | 47.032419290668263836 | 0.243376775431168091     | 5.222534380003310593        | 16.172331630490835971      | 2.229545226563932871           | 8.827145906925371921                 | 5.840744518697999261             | 1.576271868422837132 | 1.671521337181186901                   | 2.804289093159550625          | 0.911921985003105625 | 2.552250353585827963         | 1.230987844634265057      | 6.862955777250017317           | 1.655705337173290201    | 2.195836786859914228     | 0.769026240517042278 | 2.518322478352908688 | 1.269761248879548491 | 1.069057248157455599 | 0.232487369925466103 | 1.037194795689012994                      | 2.649081520209655151    | 3.122346564175992627  | 2.273042482962038927 | 2.352101095300042024  | 5.106857564765831221 | 1.233612694181337233        | 0.150785573086876154    | 1.546941016867814956     | 0.367481308514821130        | 2.676854974217225985 | 0.229316149832206997   | 0.302825620719785553          | 0.330814690807608155       | 0.591898689705248804        |

    Scenario: Sending a valid school national average expenditure history request with dimension Actuals, phase Secondary, financeType Academy
        Given a school national average expenditure history request with dimension 'Actuals', phase 'Secondary', financeType 'Academy'
        When I submit the insights expenditure request
        Then the school expenditure history result should be ok and contain:
          | Year | TotalExpenditure | TotalTeachingSupportStaffCosts | TeachingStaffCosts | SupplyTeachingStaffCosts | EducationalConsultancyCosts | EducationSupportStaffCosts | AgencySupplyTeachingStaffCosts | TotalNonEducationalSupportStaffCosts | AdministrativeClericalStaffCosts | OtherStaffCosts | ProfessionalServicesNonCurriculumCosts | TotalEducationalSuppliesCosts | ExaminationFeesCosts | LearningResourcesNonIctCosts | LearningResourcesIctCosts | TotalPremisesStaffServiceCosts | CleaningCaretakingCosts | MaintenancePremisesCosts | OtherOccupationCosts | PremisesStaffCosts | TotalUtilitiesCosts | EnergyCosts  | WaterSewerageCosts | AdministrativeSuppliesNonEducationalCosts | TotalGrossCateringCosts | TotalNetCateringCosts | CateringStaffCosts | CateringSuppliesCosts | TotalOtherCosts | DirectRevenueFinancingCosts | GroundsMaintenanceCosts | IndirectEmployeeExpenses | OtherInsurancePremiumsCosts | RentRatesCosts | SpecialFacilitiesCosts | StaffDevelopmentTrainingCosts | StaffRelatedInsuranceCosts | SupplyTeacherInsurableCosts |
          | 2021 | 2689000.040000   | 1903414.330000                 | 1229596.680000     | 6215.600000              | 108396.747474               | 491054.550000              | 79605.063829                   | 257664.690000                        | 142780.580000                    | 64388.852272    | 58221.920000                           | 56345.585858                  | 20304.333333         | 53884.454545                 | 36064.870000              | 158942.848484                  | 38656.395348            | 70783.457446             | 12857.923913         | 60652.986842       | 30492.406250        | 25836.623655 | 6122.288888        | 27845.760000                              | 89003.291666            | 95320.333333          | 64295.769230       | 81146.505263          | 141844.370000   | 26039.850000                | 5152.823529             | 12161.708333             | 12875.438775                | 43938.878378   | 6965.382352            | 7131.989690                   | 11600.803921               | 19261.103448                |
          | 2022 | 3147198.220000   | 2240705.160000                 | 1563749.030000     | 11493.571428             | 144943.760000               | 477532.340000              | 57715.569892                   | 271290.180000                        | 178067.480000                    | 52282.662650    | 49828.090000                           | 85572.960000                  | 68049.368421         | 72643.580000                 | 39240.649484              | 189658.890000                  | 46901.090909            | 58798.170000             | 18087.142857         | 72503.521739       | 39748.468750        | 34193.260416 | 6762.287356        | 32420.660000                              | 76898.280000            | 89107.390000          | 105663.600000      | 66331.920000          | 181943.630000   | 37567.350000                | 4119.632911             | 47832.320000             | 11459.020000                | 118940.304347  | 6196.492307            | 8393.140000                   | 6687.693877                | 13322.123595                |

    Scenario: Sending a valid school national average expenditure history request with dimension PerUnit, phase Special, financeType Academy
        Given a school national average expenditure history request with dimension 'PerUnit', phase 'Special', financeType 'Academy'
        When I submit the insights expenditure request
        Then the school expenditure history result should be ok and contain:
          | Year | TotalExpenditure          | TotalTeachingSupportStaffCosts | TeachingStaffCosts       | SupplyTeachingStaffCosts | EducationalConsultancyCosts | EducationSupportStaffCosts | AgencySupplyTeachingStaffCosts | TotalNonEducationalSupportStaffCosts | AdministrativeClericalStaffCosts | OtherStaffCosts         | ProfessionalServicesNonCurriculumCosts | TotalEducationalSuppliesCosts | ExaminationFeesCosts   | LearningResourcesNonIctCosts | LearningResourcesIctCosts | TotalPremisesStaffServiceCosts | CleaningCaretakingCosts | MaintenancePremisesCosts | OtherOccupationCosts  | PremisesStaffCosts     | TotalUtilitiesCosts    | EnergyCosts           | WaterSewerageCosts    | AdministrativeSuppliesNonEducationalCosts | TotalGrossCateringCosts | TotalNetCateringCosts   | CateringStaffCosts      | CateringSuppliesCosts   | TotalOtherCosts         | DirectRevenueFinancingCosts | GroundsMaintenanceCosts | IndirectEmployeeExpenses | OtherInsurancePremiumsCosts | RentRatesCosts          | SpecialFacilitiesCosts | StaffDevelopmentTrainingCosts | StaffRelatedInsuranceCosts | SupplyTeacherInsurableCosts |
          | 2021 | 9507.3345531008488370636  | 6917.8058094397848448186       | 4326.5773568542722090162 | 28.0048706158590404897   | 292.1732292579793987652     | 1922.1893570707012889468   | 422.8078481499801205576        | 889.8740570526284469048              | 498.8010934564743474239          | 173.4054149711753714049 | 255.3643779665385914248                | 211.1690183158324242113       | 81.9162652919971422959 | 193.3611345567026106687      | 99.5861487960619368897    | 58.4780074253344054186         | 14.9862124480093415547  | 15.0232631670042700999   | 5.8218816616113232049 | 26.4409829278212634038 | 11.3307571886675929195 | 9.5144931751551326661 | 1.9760530850315603265 | 90.0510336443318637128                    | 232.8412360696742653860 | 266.3381453917799187615 | 132.3036772031794942329 | 222.7746519346497386509 | 401.2884629588685472706 | 87.0889918938207161720      | 12.9870123547637844701  | 90.2930161964928560028   | 34.1264128986843297132      | 120.7926489255277191298 | 59.6563092958546873421 | 31.7959454735584340588        | 23.0901771276055800480     | 47.4955490957584486421      |
          | 2022 | 11090.7024119633114768134 | 8194.4654251003698648582       | 5429.7111729788648088002 | 22.3530425611044113591   | 526.9628456349504775235     | 2066.0220793419535830706   | 179.6611848726377830794        | 930.6941467536611557033              | 558.3196191843666173320          | 242.6270745347930763412 | 173.6268175780278694533                | 241.7972064235090705153       | 81.8255604475575740282 | 220.9055739688135196995      | 102.9172891135928742875   | 62.5266343047775750005         | 14.8237171972420321381  | 19.9063712998847816798   | 7.0090057586377451414 | 23.1381337102369625048 | 10.7532716495966438912 | 9.0653646170912499595 | 2.0685209284803758856 | 100.7718872681833046120                   | 230.6956100419110060519 | 267.3863125309488749118 | 179.8345557319325916046 | 207.7380071825153560598 | 525.5629266298694909913 | 109.7291892314517625818     | 12.9208765237647963882  | 181.6628221794796629410  | 28.4999131287377486149      | 216.6678305461607018693 | 32.3615728871599794776 | 39.9059757668599517576        | 24.4266359578405954739     | 46.1764709787916782016      |

    Scenario: Sending an invalid school national average expenditure history request with invalid dimension, phase Primary, financeType Maintained
        Given a school national average expenditure history request with dimension 'invalid', phase 'Primary', financeType 'Maintained'
        When I submit the insights expenditure request
        Then the school expenditure result should be bad request

    Scenario: Sending an invalid school national average expenditure history request with dimension Actuals, invalid phase, financeType Maintained
        Given a school national average expenditure history request with dimension 'Actuals', phase 'invalid', financeType 'Maintained'
        When I submit the insights expenditure request
        Then the school expenditure result should be bad request

    Scenario: Sending an invalid school national average expenditure history request with dimension Actuals, phase Primary, invalid financeType
        Given a school national average expenditure history request with dimension 'Actuals', phase 'Primary', financeType 'invalid'
        When I submit the insights expenditure request
        Then the school expenditure result should be bad request