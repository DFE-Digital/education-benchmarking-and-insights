Feature: Local authorities high needs endpoints
# todo: replace stubbed values with real

    Scenario: Sending a valid high needs request returns the expected outturn values
        Given a valid high needs request with LA codes '201'
        When I submit the high needs request
        Then the high needs result should be ok and have the following values:
          | Start year | End year |
          | 2021       | 2024     |
        And the high needs result should contain the following outturn values for '2021':
          | Field | Value   |
          | Year  | 2021    |
          | Code  | 201     |
          | Total | 1012021 |
        And the high needs result should contain the following outturn values for '2022':
          | Field | Value   |
          | Year  | 2022    |
          | Code  | 201     |
          | Total | 1012022 |
        And the high needs result should contain the following outturn values for '2023':
          | Field | Value   |
          | Year  | 2023    |
          | Code  | 201     |
          | Total | 1012023 |
        And the high needs result should contain the following outturn values for '2024':
          | Field | Value   |
          | Year  | 2024    |
          | Code  | 201     |
          | Total | 1012024 |
        And the high needs result should contain the following outturn high needs amount values for '2021':
          | Field                        | Value   |
          | TotalPlaceFunding            | 1002022 |
          | TopUpFundingMaintained       | 1002023 |
          | TopUpFundingNonMaintained    | 1002024 |
          | SenServices                  | 1002025 |
          | AlternativeProvisionServices | 1002026 |
          | HospitalServices             | 1002027 |
          | OtherHealthServices          | 1002028 |
        And the high needs result should contain the following outturn maintained values for '2021':
          | Field                | Value   |
          | EarlyYears           | 1002029 |
          | Primary              | 1002030 |
          | Secondary            | 1002031 |
          | Special              | 1002032 |
          | AlternativeProvision | 1002033 |
          | PostSchool           | 1002034 |
          | Income               | 1002035 |
        And the high needs result should contain the following outturn non maintained values for '2021':
          | Field                | Value   |
          | EarlyYears           | 1002036 |
          | Primary              | 1002037 |
          | Secondary            | 1002038 |
          | Special              | 1002039 |
          | AlternativeProvision | 1002040 |
          | PostSchool           | 1002041 |
          | Income               | 1002042 |
        And the high needs result should contain the following outturn place funding values for '2021':
          | Field                | Value   |
          | Primary              | 1002043 |
          | Secondary            | 1002044 |
          | Special              | 1002045 |
          | AlternativeProvision | 1002046 |

    Scenario: Sending a valid high needs request returns the expected budget values
        Given a valid high needs request with LA codes '201'
        When I submit the high needs request
        Then the high needs result should be ok and have the following values:
          | Start year | End year |
          | 2021       | 2024     |
        And the high needs result should contain the following budget values for '2021':
          | Field | Value   |
          | Year  | 2021    |
          | Code  | 201     |
          | Total | 1112021 |
        And the high needs result should contain the following budget values for '2022':
          | Field | Value   |
          | Year  | 2022    |
          | Code  | 201     |
          | Total | 1112022 |
        And the high needs result should contain the following budget values for '2023':
          | Field | Value   |
          | Year  | 2023    |
          | Code  | 201     |
          | Total | 1112023 |
        And the high needs result should contain the following budget values for '2024':
          | Field | Value   |
          | Year  | 2024    |
          | Code  | 201     |
          | Total | 1112024 |
        And the high needs result should contain the following budget high needs amount values for '2021':
          | Field                        | Value   |
          | TotalPlaceFunding            | 1102022 |
          | TopUpFundingMaintained       | 1102023 |
          | TopUpFundingNonMaintained    | 1102024 |
          | SenServices                  | 1102025 |
          | AlternativeProvisionServices | 1102026 |
          | HospitalServices             | 1102027 |
          | OtherHealthServices          | 1102028 |
        And the high needs result should contain the following budget maintained values for '2021':
          | Field                | Value   |
          | EarlyYears           | 1102029 |
          | Primary              | 1102030 |
          | Secondary            | 1102031 |
          | Special              | 1102032 |
          | AlternativeProvision | 1102033 |
          | PostSchool           | 1102034 |
          | Income               | 1102035 |
        And the high needs result should contain the following budget non maintained values for '2021':
          | Field                | Value   |
          | EarlyYears           | 1102036 |
          | Primary              | 1102037 |
          | Secondary            | 1102038 |
          | Special              | 1102039 |
          | AlternativeProvision | 1102040 |
          | PostSchool           | 1102041 |
          | Income               | 1102042 |
        And the high needs result should contain the following budget place funding values for '2021':
          | Field                | Value   |
          | Primary              | 1102043 |
          | Secondary            | 1102044 |
          | Special              | 1102045 |
          | AlternativeProvision | 1102046 |

    Scenario: Sending an invalid high needs request
        Given an invalid high needs request
        When I submit the high needs request
        Then the high needs result should be bad request