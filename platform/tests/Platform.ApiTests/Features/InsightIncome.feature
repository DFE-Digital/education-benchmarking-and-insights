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
          | TotalIncome                          | 3685110.00             | 
          | TotalGrantFunding                    | 3595411.00             | 
          | TotalSelfGeneratedFunding            |                        | 
          | DirectRevenueFinancing               |                        | 
          | DirectGrants                         | 2741553.00             | 
          | PrePost16Funding                     | 2736638.00             | 
          | OtherDfeGrants                       | 0.00                   | 
          | OtherIncomeGrants                    | 4915.00                | 
          | CommunityGrants                      | 46914.00               | 
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
          | TotalIncome                          | 3685110.00             | 
          | TotalGrantFunding                    | 3595411.00             | 
          | TotalSelfGeneratedFunding            |                        | 
          | DirectRevenueFinancing               |                        | 
          | DirectGrants                         | 2741553.00             | 
          | PrePost16Funding                     | 2736638.00             | 
          | OtherDfeGrants                       | 0.00                   | 
          | OtherIncomeGrants                    | 4915.00                | 
          | CommunityGrants                      | 46914.00               | 
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
          | TotalIncome                          | 3685110.00             | 
          | TotalGrantFunding                    | 3595411.00             | 
          | TotalSelfGeneratedFunding            | 68353.00               | 
          | DirectRevenueFinancing               | 0.00                   | 
          | DirectGrants                         | 2741553.00             | 
          | PrePost16Funding                     | 2736638.00             | 
          | OtherDfeGrants                       | 0.00                   | 
          | OtherIncomeGrants                    | 4915.00                | 
          | CommunityGrants                      | 46914.00               | 
          | IncomeFacilitiesServices             | 41001.00               | 
          | IncomeCatering                       | 13600.00               | 
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
         | Year | Term         |  CompanyNumber | TotalIncome |  TotalGrantFunding |  TotalSelfGeneratedFunding |  DirectRevenueFinancing |  DirectGrants |  PrePost16Funding |  OtherDfeGrants |  OtherIncomeGrants |  CommunityGrants |  IncomeFacilitiesServices |  IncomeCatering |  DonationsVoluntaryFunds |    ReceiptsSupplyTeacherInsuranceClaims |
         | 2018 | 2017 to 2018 | 10192252       |             |                    |                            |                         |               |                   |                 |                    |                  |                           |                 |                          |                                         |
         | 2019 | 2018 to 2019 | 10192252       |             |                    |                            |                         |               |                   |                 |                    |                  |                           |                 |                          |                                         |
         | 2020 | 2019 to 2020 | 10192252       |             |                    |                            |                         |               |                   |                 |                    |                  |                           |                 |                          |                                         |
         | 2021 | 2020 to 2021 | 10192252       | 1764419.00  | 1638172.00         | 91207.00                   | 0.00                    | 1407540.00    | 1407540.00        | 0.00            | 0.00               | 43152.00         | 78012.00                  | 13195.00        | 0.00                     | 0.00                                    |
         | 2022 | 2021 to 2022 | 10192252       | 3685110.00  | 3595411.00         | 68353.00                   | 0.00                    | 2741553.00    | 2736638.00        | 0.00            | 4915.00            | 46914.00         | 41001.00                  | 13600.00        | 14957.00                 | 0.00                                    |

    Scenario: Sending a valid trust income query request
        Given a valid trust income query request with company numbers:
         | CompanyNumber |
         | 10249712      |
         | 10259334      |
         | 10264735      |
        When I submit the insights income request
        Then the trust income query result should be ok and contain:
          |  CompanyNumber |    TrustName            | TotalIncome |  TotalGrantFunding |  TotalSelfGeneratedFunding |  DirectRevenueFinancing |  DirectGrants |  PrePost16Funding |  OtherDfeGrants |  OtherIncomeGrants |  CommunityGrants |  IncomeFacilitiesServices |  IncomeCatering |  DonationsVoluntaryFunds |    ReceiptsSupplyTeacherInsuranceClaims |
          | 10249712       | Test Company/Trust  229 | 1718135.00  | 1681207.00         | 27548.00                   | 0.00                    | 1371311.00    | 1371311.00        | 0.00            | 0.00               | 41819.00         | 103.00                    | 10280.00        | 8337.00                  | 3600.00                                 |
          | 10259334       | Test Company/Trust  157 | 6866710.00  | 6544582.00         | 288258.00                  | 0.00                    | 5999283.00    | 5967483.00        | 15000.00        | 16800.00           | 0.00             | 164208.00                 | 55274.00        | 5575.00                  | 0.00                                    |
          | 10264735       | Test Company/Trust  262 | 1614419.00  | 1541886.00         | 50961.00                   | 0.00                    | 1274798.00    | 1262883.00        | 11915.00        | 0.00               | 45045.00         | 2071.00                   | 8162.00         | 29065.00                 | 0.00                                    |
