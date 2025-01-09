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

    Scenario: Sending a valid school income request with category
        Given a valid school income request with urn '990000'
        When I submit the insights income request
        Then the school income result should be ok and contain:
          | Field                                | Value                  |
          | URN                                  | 990000                 |
          | SchoolName                           | Test school 176        |
          | SchoolType                           | Voluntary aided school |
          | LAName                               | Test Local Authority   |
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

    Scenario: Sending a valid school expenditure request with bad URN
        Given a valid school income request with urn '0000000'
        When I submit the insights income request
        Then the school income result should be not found

    Scenario: Sending a valid school income history request
        Given a valid school income history request with urn '990000'
        When I submit the insights income request
        Then the school income history result should be ok and contain:
          | Year |  TotalIncome | TotalGrantFunding | TotalSelfGeneratedFunding | DirectRevenueFinancing | DirectGrants | PrePost16Funding | OtherDfeGrants | OtherIncomeGrants | CommunityGrants | IncomeFacilitiesServices | IncomeCatering | DonationsVoluntaryFunds | ReceiptsSupplyTeacherInsuranceClaims |       
          | 2021 | 1453187.00  | 1435070.00        | 8707.00                   | 0.00                   | 1316142.00   | 1134288.00       | 730.00         | 181124.00         | 80102.00        | 6929.00                  | 1778.00        | 0.00                    | 0.00                                 | 
          | 2022 | 2912331.00  | 2851071.00        | 26520.00                  | 3006.00                | 2420249.00   | 2397709.00       | 0.00           | 22540.00          | 57641.00        | 1673.00                  | 7661.00        | 17187.00                | 0.00                                 | 

    Scenario: Sending a valid trust income history request
        Given a valid trust income history request with company number '10192252'
        When I submit the insights income request
        Then the trust income history result should be ok and contain:
          | Year | TotalIncome | TotalGrantFunding | TotalSelfGeneratedFunding | DirectRevenueFinancing | DirectGrants | PrePost16Funding | OtherDfeGrants | OtherIncomeGrants | CommunityGrants | IncomeFacilitiesServices | IncomeCatering | DonationsVoluntaryFunds | ReceiptsSupplyTeacherInsuranceClaims |
          | 2022 | 5449529.00  | 5233583.00        | 159560.00                 |                        | 4149093.00   | 4144178.00       | 0.00           | 4915.00           | 90066.00        | 119013.00                | 26795.00       | 14957.00                | 0.00                                 |

