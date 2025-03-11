Feature: Local authorities high needs endpoints
# todo: replace stubbed values with real

    Scenario: Sending a valid high needs history request returns the expected outturn values
        Given a valid high needs history request with LA codes:
          | Code |
          | 201  |
        When I submit the high needs request
        Then the high needs history result should be ok and have the following values:
          | Start year | End year |
          | 2021       | 2024     |
        And the high needs history result should contain the following outturn values for '2021':
          | Field | Value   |
          | Year  | 2021    |
          | Code  | 201     |
          | Total | 1014042 |
        And the high needs history result should contain the following outturn values for '2022':
          | Field | Value   |
          | Year  | 2022    |
          | Code  | 201     |
          | Total | 1014044 |
        And the high needs history result should contain the following outturn values for '2023':
          | Field | Value   |
          | Year  | 2023    |
          | Code  | 201     |
          | Total | 1014046 |
        And the high needs history result should contain the following outturn values for '2024':
          | Field | Value   |
          | Year  | 2024    |
          | Code  | 201     |
          | Total | 1014048 |
        And the high needs history result should contain the following outturn high needs amount values for '2021':
          | Field                        | Value   |
          | TotalPlaceFunding            | 1002022 |
          | TopUpFundingMaintained       | 1002023 |
          | TopUpFundingNonMaintained    | 1002024 |
          | SenServices                  | 1002025 |
          | AlternativeProvisionServices | 1002026 |
          | HospitalServices             | 1002027 |
          | OtherHealthServices          | 1002028 |
        And the high needs history result should contain the following outturn maintained values for '2021':
          | Field                | Value   |
          | EarlyYears           | 1002029 |
          | Primary              | 1002030 |
          | Secondary            | 1002031 |
          | Special              | 1002032 |
          | AlternativeProvision | 1002033 |
          | PostSchool           | 1002034 |
          | Income               | 1002035 |
        And the high needs history result should contain the following outturn non maintained values for '2021':
          | Field                | Value   |
          | EarlyYears           | 1002036 |
          | Primary              | 1002037 |
          | Secondary            | 1002038 |
          | Special              | 1002039 |
          | AlternativeProvision | 1002040 |
          | PostSchool           | 1002041 |
          | Income               | 1002042 |
        And the high needs history result should contain the following outturn place funding values for '2021':
          | Field                | Value   |
          | Primary              | 1002043 |
          | Secondary            | 1002044 |
          | Special              | 1002045 |
          | AlternativeProvision | 1002046 |

    Scenario: Sending a valid high needs history request returns the expected budget values
        Given a valid high needs history request with LA codes:
          | Code |
          | 201  |
        When I submit the high needs request
        Then the high needs history result should be ok and have the following values:
          | Start year | End year |
          | 2021       | 2024     |
        And the high needs history result should contain the following budget values for '2021':
          | Field | Value   |
          | Year  | 2021    |
          | Code  | 201     |
          | Total | 1110001 |
        And the high needs history result should contain the following budget values for '2022':
          | Field | Value   |
          | Year  | 2022    |
          | Code  | 201     |
          | Total | 1110000 |
        And the high needs history result should contain the following budget values for '2023':
          | Field | Value   |
          | Year  | 2023    |
          | Code  | 201     |
          | Total | 1110001 |
        And the high needs history result should contain the following budget values for '2024':
          | Field | Value   |
          | Year  | 2024    |
          | Code  | 201     |
          | Total | 1110000 |
        And the high needs history result should contain the following budget high needs amount values for '2021':
          | Field                        | Value   |
          | TotalPlaceFunding            | 1102022 |
          | TopUpFundingMaintained       | 1102023 |
          | TopUpFundingNonMaintained    | 1102024 |
          | SenServices                  | 1102025 |
          | AlternativeProvisionServices | 1102026 |
          | HospitalServices             | 1102027 |
          | OtherHealthServices          | 1102028 |
        And the high needs history result should contain the following budget maintained values for '2021':
          | Field                | Value   |
          | EarlyYears           | 1102029 |
          | Primary              | 1102030 |
          | Secondary            | 1102031 |
          | Special              | 1102032 |
          | AlternativeProvision | 1102033 |
          | PostSchool           | 1102034 |
          | Income               | 1102035 |
        And the high needs history result should contain the following budget non maintained values for '2021':
          | Field                | Value   |
          | EarlyYears           | 1102036 |
          | Primary              | 1102037 |
          | Secondary            | 1102038 |
          | Special              | 1102039 |
          | AlternativeProvision | 1102040 |
          | PostSchool           | 1102041 |
          | Income               | 1102042 |
        And the high needs history result should contain the following budget place funding values for '2021':
          | Field                | Value   |
          | Primary              | 1102043 |
          | Secondary            | 1102044 |
          | Special              | 1102045 |
          | AlternativeProvision | 1102046 |

    Scenario: Sending an invalid high needs history request
        Given an invalid high needs history request
        When I submit the high needs request
        Then the high needs history result should be bad request

    Scenario: Sending a valid high needs request with Actuals dimension returns the expected outturn values
        Given a valid high needs request with dimension 'Actuals' and LA codes:
          | Code |
          | 201  |
          | 202  |
          | 203  |
        When I submit the high needs request
        Then the high needs result should be ok and have the following values for '201':
          | Field | Value               |
          | Code  | 201                 |
          | Name  | Local authority 201 |
        And the high needs result should have the following values for '202':
          | Field | Value               |
          | Code  | 202                 |
          | Name  | Local authority 202 |
        And the high needs result should have the following values for '203':
          | Field | Value               |
          | Code  | 203                 |
          | Name  | Local authority 203 |
        And the high needs result should contain the following outturn values for '201':
          | Field | Value   |
          | Total | 1010001 |
        And the high needs result should contain the following outturn values for '202':
          | Field | Value   |
          | Total | 1010000 |
        And the high needs result should contain the following outturn values for '203':
          | Field | Value   |
          | Total | 1010001 |
        And the high needs result should contain the following outturn high needs amount values for '201':
          | Field                        | Value   |
          | TotalPlaceFunding            | 1002226 |
          | TopUpFundingMaintained       | 1002227 |
          | TopUpFundingNonMaintained    | 1002228 |
          | SenServices                  | 1002229 |
          | AlternativeProvisionServices | 1002230 |
          | HospitalServices             | 1002231 |
          | OtherHealthServices          | 1002232 |
        And the high needs result should contain the following outturn maintained values for '201':
          | Field                | Value   |
          | EarlyYears           | 1002233 |
          | Primary              | 1002234 |
          | Secondary            | 1002235 |
          | Special              | 1002236 |
          | AlternativeProvision | 1002237 |
          | PostSchool           | 1002238 |
          | Income               | 1002239 |
        And the high needs result should contain the following outturn non maintained values for '201':
          | Field                | Value   |
          | EarlyYears           | 1002240 |
          | Primary              | 1002241 |
          | Secondary            | 1002242 |
          | Special              | 1002243 |
          | AlternativeProvision | 1002244 |
          | PostSchool           | 1002245 |
          | Income               | 1002246 |
        And the high needs result should contain the following outturn place funding values for '201':
          | Field                | Value   |
          | Primary              | 1002247 |
          | Secondary            | 1002248 |
          | Special              | 1002249 |
          | AlternativeProvision | 1002250 |

    Scenario: Sending a valid high needs request with Actuals dimension returns the expected budget values
        Given a valid high needs request with dimension 'Actuals' and LA codes:
          | Code |
          | 201  |
          | 202  |
          | 203  |
        When I submit the high needs request
        Then the high needs result should be ok and have the following values for '201':
          | Field | Value               |
          | Code  | 201                 |
          | Name  | Local authority 201 |
        And the high needs result should have the following values for '202':
          | Field | Value               |
          | Code  | 202                 |
          | Name  | Local authority 202 |
        And the high needs result should have the following values for '203':
          | Field | Value               |
          | Code  | 203                 |
          | Name  | Local authority 203 |
        And the high needs result should contain the following budget values for '201':
          | Field | Value   |
          | Total | 1110001 |
        And the high needs result should contain the following budget values for '202':
          | Field | Value   |
          | Total | 1110000 |
        And the high needs result should contain the following budget values for '203':
          | Field | Value   |
          | Total | 1110001 |
        And the high needs result should contain the following budget high needs amount values for '201':
          | Field                        | Value   |
          | TotalPlaceFunding            | 1102226 |
          | TopUpFundingMaintained       | 1102227 |
          | TopUpFundingNonMaintained    | 1102228 |
          | SenServices                  | 1102229 |
          | AlternativeProvisionServices | 1102230 |
          | HospitalServices             | 1102231 |
          | OtherHealthServices          | 1102232 |
        And the high needs result should contain the following budget maintained values for '201':
          | Field                | Value   |
          | EarlyYears           | 1102233 |
          | Primary              | 1102234 |
          | Secondary            | 1102235 |
          | Special              | 1102236 |
          | AlternativeProvision | 1102237 |
          | PostSchool           | 1102238 |
          | Income               | 1102239 |
        And the high needs result should contain the following budget non maintained values for '201':
          | Field                | Value   |
          | EarlyYears           | 1102240 |
          | Primary              | 1102241 |
          | Secondary            | 1102242 |
          | Special              | 1102243 |
          | AlternativeProvision | 1102244 |
          | PostSchool           | 1102245 |
          | Income               | 1102246 |
        And the high needs result should contain the following budget place funding values for '201':
          | Field                | Value   |
          | Primary              | 1102247 |
          | Secondary            | 1102248 |
          | Special              | 1102249 |
          | AlternativeProvision | 1102250 |

    Scenario: Sending a valid high needs request with PerHead dimension returns the expected outturn values
        Given a valid high needs request with dimension 'PerHead' and LA codes:
          | Code |
          | 201  |
          | 202  |
          | 203  |
        When I submit the high needs request
        Then the high needs result should be ok and have the following values for '201':
          | Field | Value               |
          | Code  | 201                 |
          | Name  | Local authority 201 |
        And the high needs result should have the following values for '202':
          | Field | Value               |
          | Code  | 202                 |
          | Name  | Local authority 202 |
        And the high needs result should have the following values for '203':
          | Field | Value               |
          | Code  | 203                 |
          | Name  | Local authority 203 |
        And the high needs result should contain the following outturn values for '201':
          | Field | Value |
          | Total | 1011  |
        And the high needs result should contain the following outturn values for '202':
          | Field | Value |
          | Total | 1010  |
        And the high needs result should contain the following outturn values for '203':
          | Field | Value |
          | Total | 1011  |
        And the high needs result should contain the following outturn high needs amount values for '201':
          | Field                        | Value |
          | TotalPlaceFunding            | 3226  |
          | TopUpFundingMaintained       | 3227  |
          | TopUpFundingNonMaintained    | 3228  |
          | SenServices                  | 3229  |
          | AlternativeProvisionServices | 3230  |
          | HospitalServices             | 3231  |
          | OtherHealthServices          | 3232  |
        And the high needs result should contain the following outturn maintained values for '201':
          | Field                | Value |
          | EarlyYears           | 3233  |
          | Primary              | 3234  |
          | Secondary            | 3235  |
          | Special              | 3236  |
          | AlternativeProvision | 3237  |
          | PostSchool           | 3238  |
          | Income               | 3239  |
        And the high needs result should contain the following outturn non maintained values for '201':
          | Field                | Value |
          | EarlyYears           | 3240  |
          | Primary              | 3241  |
          | Secondary            | 3242  |
          | Special              | 3243  |
          | AlternativeProvision | 3244  |
          | PostSchool           | 3245  |
          | Income               | 3246  |
        And the high needs result should contain the following outturn place funding values for '201':
          | Field                | Value |
          | Primary              | 3247  |
          | Secondary            | 3248  |
          | Special              | 3249  |
          | AlternativeProvision | 3250  |

    Scenario: Sending a valid high needs request with PerHead dimension returns the expected budget values
        Given a valid high needs request with dimension 'PerHead' and LA codes:
          | Code |
          | 201  |
          | 202  |
          | 203  |
        When I submit the high needs request
        Then the high needs result should be ok and have the following values for '201':
          | Field | Value               |
          | Code  | 201                 |
          | Name  | Local authority 201 |
        And the high needs result should have the following values for '202':
          | Field | Value               |
          | Code  | 202                 |
          | Name  | Local authority 202 |
        And the high needs result should have the following values for '203':
          | Field | Value               |
          | Code  | 203                 |
          | Name  | Local authority 203 |
        And the high needs result should contain the following budget values for '201':
          | Field | Value |
          | Total | 1111  |
        And the high needs result should contain the following budget values for '202':
          | Field | Value |
          | Total | 1110  |
        And the high needs result should contain the following budget values for '203':
          | Field | Value |
          | Total | 1111  |
        And the high needs result should contain the following budget high needs amount values for '201':
          | Field                        | Value |
          | TotalPlaceFunding            | 3326  |
          | TopUpFundingMaintained       | 3327  |
          | TopUpFundingNonMaintained    | 3328  |
          | SenServices                  | 3329  |
          | AlternativeProvisionServices | 3330  |
          | HospitalServices             | 3331  |
          | OtherHealthServices          | 3332  |
        And the high needs result should contain the following budget maintained values for '201':
          | Field                | Value |
          | EarlyYears           | 3333  |
          | Primary              | 3334  |
          | Secondary            | 3335  |
          | Special              | 3336  |
          | AlternativeProvision | 3337  |
          | PostSchool           | 3338  |
          | Income               | 3339  |
        And the high needs result should contain the following budget non maintained values for '201':
          | Field                | Value |
          | EarlyYears           | 3340  |
          | Primary              | 3341  |
          | Secondary            | 3342  |
          | Special              | 3343  |
          | AlternativeProvision | 3344  |
          | PostSchool           | 3345  |
          | Income               | 3346  |
        And the high needs result should contain the following budget place funding values for '201':
          | Field                | Value |
          | Primary              | 3347  |
          | Secondary            | 3348  |
          | Special              | 3349  |
          | AlternativeProvision | 3350  |

    Scenario: Sending an invalid high needs request
        Given an invalid high needs request
        When I submit the high needs request
        Then the high needs result should be bad request