Feature: Insights income endpoints

    Scenario: Sending a valid school income dimension request
        Given a valid school income dimension request
        When I submit the insights income request
        Then the income dimensions result should be ok and contain:
          | Dimension          |
          | Actuals            |
          | PerUnit            |
          | PercentIncome      |
          | PercentExpenditure |

    Scenario: Sending a valid school income category request
        Given a valid school income category request
        When I submit the insights income request
        Then the income categories result should be ok and contain:
          | Category               |
          | GrantFunding           |
          | SelfGenerated          |
          | DirectRevenueFinancing |

    Scenario: Sending a valid school income request with category, dimension and exclude central services
        Given a valid school income request with urn '990000', category 'GrantFunding', dimension 'Actuals' and exclude central services = 'true'
        When I submit the insights income request
        Then the school income result should be ok and contain:
          | Field                                | Value                  |
          | URN                                  | 990000                 |
          | SchoolName                           | Test school 176        |
          | SchoolType                           | Voluntary aided school |
          | LAName                               | Bedford                |
          | TotalPupils                          | 418.00                 |
          | TotalIncome                          | 2912331.00             |
          | TotalGrantFunding                    | 2851071.00             |
          | TotalSelfGeneratedFunding            |                        |
          | DirectRevenueFinancing               |                        |
          | DirectGrants                         | 2420249.00             |
          | PrePost16Funding                     | 2397709.00             |
          | OtherDfeGrants                       | 0.00                   |
          | OtherIncomeGrants                    | 22540.00               |
          | CommunityGrants                      | 57641.00               |
          | IncomeFacilitiesServices             |                        |
          | IncomeCatering                       |                        |
          | DonationsVoluntaryFunds              |                        |
          | ReceiptsSupplyTeacherInsuranceClaims |                        |
          | SchoolTotalIncome                    |                        |
          | SchoolTotalGrantFunding              |                        |
          | SchoolTotalSelfGeneratedFunding      |                        |
          | SchoolDirectRevenueFinancing         |                        |
          | SchoolDirectGrants                   |                        |
          | SchoolPrePost16Funding               |                        |
          | SchoolOtherDfeGrants                 |                        |
          | SchoolOtherIncomeGrants              |                        |
          | SchoolCommunityGrants                |                        |
          | SchoolIncomeFacilitiesServices       |                        |
          | SchoolIncomeCatering                 |                        |
          | SchoolDonationsVoluntaryFunds        |                        |

    Scenario: Sending a valid school income request with category and dimension
        Given a valid school income request with urn '990000', category 'GrantFunding', dimension 'Actuals' and exclude central services = ''
        When I submit the insights income request
        Then the school income result should be ok and contain:
          | Field                                | Value                  |
          | URN                                  | 990000                 |
          | SchoolName                           | Test school 176        |
          | SchoolType                           | Voluntary aided school |
          | LAName                               | Bedford                |
          | TotalPupils                          | 418.00                 |
          | TotalIncome                          | 2912331.00             |
          | TotalGrantFunding                    | 2851071.00             |
          | TotalSelfGeneratedFunding            |                        |
          | DirectRevenueFinancing               |                        |
          | DirectGrants                         | 2420249.00             |
          | PrePost16Funding                     | 2397709.00             |
          | OtherDfeGrants                       | 0.00                   |
          | OtherIncomeGrants                    | 22540.00               |
          | CommunityGrants                      | 57641.00               |
          | IncomeFacilitiesServices             |                        |
          | IncomeCatering                       |                        |
          | DonationsVoluntaryFunds              |                        |
          | ReceiptsSupplyTeacherInsuranceClaims |                        |
          | SchoolTotalIncome                    | 2912331.00             |
          | SchoolTotalGrantFunding              | 2851071.00             |
          | SchoolTotalSelfGeneratedFunding      |                        |
          | SchoolDirectRevenueFinancing         |                        |
          | SchoolDirectGrants                   | 2420249.00             |
          | SchoolPrePost16Funding               | 2397709.00             |
          | SchoolOtherDfeGrants                 | 0.00                   |
          | SchoolOtherIncomeGrants              | 22540.00               |
          | SchoolCommunityGrants                | 57641.00               |
          | SchoolIncomeFacilitiesServices       |                        |
          | SchoolIncomeCatering                 |                        |
          | SchoolDonationsVoluntaryFunds        |                        |

    Scenario: Sending a valid school income request with dimension
        Given a valid school income request with urn '990000', category '', dimension 'Actuals' and exclude central services = ''
        When I submit the insights income request
        Then the school income result should be ok and contain:
          | Field                                | Value                  |
          | URN                                  | 990000                 |
          | SchoolName                           | Test school 176        |
          | SchoolType                           | Voluntary aided school |
          | LAName                               | Bedford                |
          | TotalPupils                          | 418.00                 |
          | TotalIncome                          | 2912331.00             |
          | TotalGrantFunding                    | 2851071.00             |
          | TotalSelfGeneratedFunding            | 26520.00               |
          | DirectRevenueFinancing               | 3006.00                |
          | DirectGrants                         | 2420249.00             |
          | PrePost16Funding                     | 2397709.00             |
          | OtherDfeGrants                       | 0.00                   |
          | OtherIncomeGrants                    | 22540.00               |
          | CommunityGrants                      | 57641.00               |
          | IncomeFacilitiesServices             | 1673.00                |
          | IncomeCatering                       | 7661.00                |
          | DonationsVoluntaryFunds              | 17187.00               |
          | ReceiptsSupplyTeacherInsuranceClaims | 0.00                   |
          | SchoolTotalIncome                    | 2912331.00             |
          | SchoolTotalGrantFunding              | 2851071.00             |
          | SchoolTotalSelfGeneratedFunding      | 26520.00               |
          | SchoolDirectRevenueFinancing         | 3006.00                |
          | SchoolDirectGrants                   | 2420249.00             |
          | SchoolPrePost16Funding               | 2397709.00             |
          | SchoolOtherDfeGrants                 | 0.00                   |
          | SchoolOtherIncomeGrants              | 22540.00               |
          | SchoolCommunityGrants                | 57641.00               |
          | SchoolIncomeFacilitiesServices       | 1673.00                |
          | SchoolIncomeCatering                 | 7661.00                |
          | SchoolDonationsVoluntaryFunds        | 17187.00               |

    Scenario: Sending an invalid school income request
        Given an invalid school income request with urn '000000'
        When I submit the insights income request
        Then the school income result should be not found

    Scenario: Sending a valid school income history request
        Given a valid school income history request with urn '990000'
        When I submit the insights income request
        Then the school income history result should be ok and contain:
          | Year | Term         | URN    | TotalIncome | TotalGrantFunding | TotalSelfGeneratedFunding | DirectRevenueFinancing | DirectGrants | PrePost16Funding | OtherDfeGrants | OtherIncomeGrants | CommunityGrants | IncomeFacilitiesServices | IncomeCatering | DonationsVoluntaryFunds | ReceiptsSupplyTeacherInsuranceClaims | SchoolTotalIncome | SchoolTotalGrantFunding | SchoolTotalSelfGeneratedFunding | SchoolDirectRevenueFinancing | SchoolDirectGrants | SchoolPrePost16Funding | SchoolOtherDfeGrants | SchoolOtherIncomeGrants | SchoolCommunityGrants | SchoolIncomeFacilitiesServices | SchoolIncomeCatering | SchoolDonationsVoluntaryFunds |
          | 2018 | 2017 to 2018 | 990000 |             |                   |                           |                        |              |                  |                |                   |                 |                          |                |                         |                                      |                   |                         |                                 |                              |                    |                        |                      |                         |                       |                                |                      |                               |
          | 2019 | 2018 to 2019 | 990000 |             |                   |                           |                        |              |                  |                |                   |                 |                          |                |                         |                                      |                   |                         |                                 |                              |                    |                        |                      |                         |                       |                                |                      |                               |
          | 2020 | 2019 to 2020 | 990000 |             |                   |                           |                        |              |                  |                |                   |                 |                          |                |                         |                                      |                   |                         |                                 |                              |                    |                        |                      |                         |                       |                                |                      |                               |
          | 2021 | 2020 to 2021 | 990000 | 1453187.00  | 1435070.00        | 8707.00                   | 0.00                   | 1316142.00   | 1134288.00       | 730.00         | 181124.00         | 80102.00        | 6929.00                  | 1778.00        | 0.00                    | 0.00                                 | 1453187.00        | 1435070.00              | 8707.00                         | 0.00                         | 1316142.00         | 1134288.00             | 730.00               | 181124.00               | 80102.00              | 6929.00                        | 1778.00              | 0.00                          |
          | 2022 | 2021 to 2022 | 990000 | 2912331.00  | 2851071.00        | 26520.00                  | 3006.00                | 2420249.00   | 2397709.00       | 0.00           | 22540.00          | 57641.00        | 1673.00                  | 7661.00        | 17187.00                | 0.00                                 | 2912331.00        | 2851071.00              | 26520.00                        | 3006.00                      | 2420249.00         | 2397709.00             | 0.00                 | 22540.00                | 57641.00              | 1673.00                        | 7661.00              | 17187.00                      |

    Scenario: Sending a valid school income query request
        Given a valid school income query request with urns:
          | Urn    |
          | 990000 |
          | 990001 |
          | 990002 |
        When I submit the insights income request
        Then the school income query result should be ok and contain:
          | URN    | TotalIncome | TotalGrantFunding | TotalSelfGeneratedFunding | DirectRevenueFinancing | DirectGrants | PrePost16Funding | OtherDfeGrants | OtherIncomeGrants | CommunityGrants | IncomeFacilitiesServices | IncomeCatering | DonationsVoluntaryFunds | ReceiptsSupplyTeacherInsuranceClaims | SchoolTotalIncome | SchoolTotalGrantFunding | SchoolTotalSelfGeneratedFunding | SchoolDirectRevenueFinancing | SchoolDirectGrants | SchoolPrePost16Funding | SchoolOtherDfeGrants | SchoolOtherIncomeGrants | SchoolCommunityGrants | SchoolIncomeFacilitiesServices | SchoolIncomeCatering | SchoolDonationsVoluntaryFunds |
          | 990000 | 2912331.00  | 2851071.00        | 26520.00                  | 3006.00                | 2420249.00   | 2397709.00       | 0.00           | 22540.00          | 57641.00        | 1673.00                  | 7661.00        | 17187.00                | 0.00                                 | 2912331.00        | 2851071.00              | 26520.00                        | 3006.00                      | 2420249.00         | 2397709.00             | 0.00                 | 22540.00                | 57641.00              | 1673.00                        | 7661.00              | 17187.00                      |
          | 990001 | 2039283.00  | 1969878.00        | 56754.00                  | 0.00                   | 1762409.00   | 1760909.00       | 1500.00        | 0.00              | 48852.00        | 25842.00                 | 18355.00       | 4080.00                 | 8120.00                              | 2039283.00        | 1969878.00              | 56754.00                        | 0.00                         | 1762409.00         | 1760909.00             | 1500.00              | 0.00                    | 48852.00              | 25842.00                       | 18355.00             | 4080.00                       |
          | 990002 | 1674148.00  | 1599525.00        | 66873.00                  | 0.00                   | 1342622.00   | 1306852.00       | 0.00           | 35770.00          | 73936.00        | 9358.00                  | 8239.00        | 10546.00                | 37300.00                             | 1674148.00        | 1599525.00              | 66873.00                        | 0.00                         | 1342622.00         | 1306852.00             | 0.00                 | 35770.00                | 73936.00              | 9358.00                        | 8239.00              | 10546.00                      |

    Scenario: Sending a valid trust income request with category, dimension and exclude central services
        Given a valid trust income request with company number '10192252', category 'GrantFunding', dimension 'Actuals' and exclude central services = 'true'
        When I submit the insights income request
        Then the trust income result should be ok and contain:
          | Field                                | Value                  |
          | CompanyNumber                        | 10192252               |
          | TrustName                            | Test Company/Trust  31 |
          | TotalIncome                          | 5449529.00             |
          | TotalGrantFunding                    | 5233583.00             |
          | TotalSelfGeneratedFunding            |                        |
          | DirectRevenueFinancing               |                        |
          | DirectGrants                         | 4149093.00             |
          | PrePost16Funding                     | 4144178.00             |
          | OtherDfeGrants                       | 0.00                   |
          | OtherIncomeGrants                    | 4915.00                |
          | CommunityGrants                      | 90066.00               |
          | IncomeFacilitiesServices             |                        |
          | IncomeCatering                       |                        |
          | DonationsVoluntaryFunds              |                        |
          | ReceiptsSupplyTeacherInsuranceClaims |                        |

    Scenario: Sending a valid trust income request with category and dimension
        Given a valid trust income request with company number '10192252', category 'GrantFunding', dimension 'Actuals' and exclude central services = ''
        When I submit the insights income request
        Then the trust income result should be ok and contain:
          | Field                                | Value                  |
          | CompanyNumber                        | 10192252               |
          | TrustName                            | Test Company/Trust  31 |
          | TotalIncome                          | 5449529.00             |
          | TotalGrantFunding                    | 5233583.00             |
          | TotalSelfGeneratedFunding            |                        |
          | DirectRevenueFinancing               |                        |
          | DirectGrants                         | 4149093.00             |
          | PrePost16Funding                     | 4144178.00             |
          | OtherDfeGrants                       | 0.00                   |
          | OtherIncomeGrants                    | 4915.00                |
          | CommunityGrants                      | 90066.00               |
          | IncomeFacilitiesServices             |                        |
          | IncomeCatering                       |                        |
          | DonationsVoluntaryFunds              |                        |
          | ReceiptsSupplyTeacherInsuranceClaims |                        |

    Scenario: Sending a valid trust income request with dimension
        Given a valid trust income request with company number '10192252', category '', dimension 'Actuals' and exclude central services = ''
        When I submit the insights income request
        Then the trust income result should be ok and contain:
          | Field                                | Value                  |
          | CompanyNumber                        | 10192252               |
          | TrustName                            | Test Company/Trust  31 |
          | TotalIncome                          | 5449529.00             |
          | TotalGrantFunding                    | 5233583.00             |
          | TotalSelfGeneratedFunding            | 159560.00              |
          | DirectRevenueFinancing               |                        |
          | DirectGrants                         | 4149093.00             |
          | PrePost16Funding                     | 4144178.00             |
          | OtherDfeGrants                       | 0.00                   |
          | OtherIncomeGrants                    | 4915.00                |
          | CommunityGrants                      | 90066.00               |
          | IncomeFacilitiesServices             | 119013.00              |
          | IncomeCatering                       | 26795.00               |
          | DonationsVoluntaryFunds              | 14957.00               |
          | ReceiptsSupplyTeacherInsuranceClaims | 0.00                   |

    Scenario: Sending an invalid trust income request
        Given an invalid trust income request with company number '10000000'
        When I submit the insights income request
        Then the trust income result should be not found

    Scenario: Sending a valid trust income history request
        Given a valid trust income history request with company number '10192252'
        When I submit the insights income request
        Then the trust income history result should be ok and contain:
          | Year | Term         | CompanyNumber | TotalIncome | TotalGrantFunding | TotalSelfGeneratedFunding | DirectRevenueFinancing | DirectGrants | PrePost16Funding | OtherDfeGrants | OtherIncomeGrants | CommunityGrants | IncomeFacilitiesServices | IncomeCatering | DonationsVoluntaryFunds | ReceiptsSupplyTeacherInsuranceClaims |
          | 2022 | 2021 to 2022 | 10192252      | 5449529.00  | 5233583.00        | 159560.00                 |                        | 4149093.00   | 4144178.00       | 0.00           | 4915.00           | 90066.00        | 119013.00                | 26795.00       | 14957.00                | 0.00                                 |

    Scenario: Sending a valid trust income query request
        Given a valid trust income query request with company numbers:
          | CompanyNumber |
          | 10249712      |
          | 10259334      |
          | 10264735      |
        When I submit the insights income request
        Then the trust income query result should be ok and contain:
          | CompanyNumber | TrustName               | TotalIncome | TotalGrantFunding | TotalSelfGeneratedFunding | DirectRevenueFinancing | DirectGrants | PrePost16Funding | OtherDfeGrants | OtherIncomeGrants | CommunityGrants | IncomeFacilitiesServices | IncomeCatering | DonationsVoluntaryFunds | ReceiptsSupplyTeacherInsuranceClaims |
          | 10249712      | Test Company/Trust  229 | 4691401.00  | 4591956.00        | 31979.00                  |                        | 2113541.00   | 2115383.00       | 0.00           | -1842.00          | 41819.00        | 496.00                   | 11363.00       | 9537.00                 | 3600.00                              |
          | 10259334      | Test Company/Trust  157 | 15263550.00 | 14660243.00       | 507735.00                 |                        | 13254849.00  | 13169887.00      | 45450.00       | 39512.00          | 6300.00         | 360623.00                | 55274.00       | 20336.00                | 7630.00                              |
          | 10264735      | Test Company/Trust  262 | 4039546.00  | 3904192.00        | 74882.00                  |                        | 3194834.00   | 3179149.00       | 11915.00       | 3770.00           | 78577.00        | 25992.00                 | 8162.00        | 29065.00                | 0.00                                 |