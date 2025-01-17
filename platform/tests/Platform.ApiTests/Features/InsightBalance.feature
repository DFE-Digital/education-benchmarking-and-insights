Feature: Insights balance endpoints

    Scenario: Sending a valid school balance request with dimension
        Given a valid school balance request with urn '990000', dimension 'Actuals'
        When I submit the insights balance request
        Then the school balance result should be ok and contain:
          | Field               | Value                  |
          | URN                 | 990000                 |
          | SchoolName          | Test school 176        |
          | SchoolType          | Voluntary aided school |
          | LAName              | Test Local Authority   |
          | InYearBalance       | 121827.00              |
          | RevenueReserve      | 441008.00              |

    Scenario: Sending an invalid school balance request
        Given an invalid school balance request with urn '000000'
        When I submit the insights balance request
        Then the school balance result should be not found

    Scenario: Sending a valid school balance history request
        Given a valid school balance history request with urn '990000'
        When I submit the insights balance request
        Then the school balance history result should be ok and contain:
          | Year |  InYearBalance | RevenueReserve |
          | 2021 |  100522.00     | 79032.00       |
          | 2022 |  121827.00     | 441008.00      |

    Scenario: Sending a valid trust balance request with dimension
        Given a valid trust balance request with company number '10192252', dimension 'Actuals'
        When I submit the insights balance request
        Then the trust balance result should be ok and contain:
          | Field               | Value                  |
          | CompanyNumber       | 10192252               |
          | TrustName           | Test Company/Trust  31 |
          | InYearBalance       | 364946.00              |
          | RevenueReserve      | 871095.00              |

    Scenario: Sending an invalid trust balance request
        Given an invalid trust balance request with company number '10000000'
        When I submit the insights balance request
        Then the trust balance result should be not found

    Scenario: Sending a valid trust balance history request
        Given a valid trust balance history request with company number '10192252'
        When I submit the insights balance request
        Then the trust balance history result should be ok and contain:
          | Year | InYearBalance | RevenueReserve |
          | 2022 | 364946.00     | 871095.00      |

    Scenario: Sending a valid trust balance query request
        Given a valid trust balance query request with company numbers:
          | CompanyNumber |
          | 10249712      |
          | 10259334      |
          | 10264735      |
        When I submit the insights balance request
        Then the trust balance query result should be ok and contain:
          | CompanyNumber | TrustName               | SchoolInYearBalance | InYearBalance | RevenueReserve |
          | 10249712      | Test Company/Trust  229 | -381267.00          | -381267.00    | 49358.00       |
          | 10259334      | Test Company/Trust  157 | 598442.00           | 598442.00     | 1301815.00     |
          | 10264735      | Test Company/Trust  262 | -220633.00          | -220633.00    | -297118.00     |