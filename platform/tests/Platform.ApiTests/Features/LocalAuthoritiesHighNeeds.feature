Feature: Local authorities high needs endpoints

    @HighNeedsFlagEnabled
    Scenario: Sending a valid high needs history request returns the expected outturn values
        Given a valid high needs history request with LA codes:
          | Code |
          | 201  |
        When I submit the high needs request
        Then the high needs history result should be ok and have the following values:
          | Start year | End year |
          | 2020       | 2024     |
        And the high needs history result should not contain outturn values for '2020'
        And the high needs history result should contain the following outturn values for '2021':
          | Field | Value  |
          | Year  | 2021   |
          | Code  | 201    |
          | Total | 509730 |
        And the high needs history result should contain the following outturn values for '2022':
          | Field | Value  |
          | Year  | 2022   |
          | Code  | 201    |
          | Total | 476694 |
        And the high needs history result should contain the following outturn values for '2023':
          | Field | Value  |
          | Year  | 2023   |
          | Code  | 201    |
          | Total | 580413 |
        And the high needs history result should contain the following outturn values for '2024':
          | Field | Value  |
          | Year  | 2024   |
          | Code  | 201    |
          | Total | 726203 |
        And the high needs history result should contain the following outturn high needs amount values for '2024':
          | Field                        | Value  |
          | TotalPlaceFunding            | 0      |
          | TopUpFundingMaintained       | 270508 |
          | TopUpFundingNonMaintained    | 302012 |
          | SenServices                  | 143279 |
          | AlternativeProvisionServices | 0      |
          | HospitalServices             | 0      |
          | OtherHealthServices          | 10404  |
        And the high needs history result should contain the following outturn maintained values for '2024':
          | Field                | Value  |
          | EarlyYears           | 0      |
          | Primary              | 139146 |
          | Secondary            | 124528 |
          | Special              | 6834   |
          | AlternativeProvision | 0      |
          | PostSchool           | 0      |
          | Income               | 0      |
        And the high needs history result should contain the following outturn non maintained values for '2024':
          | Field                | Value  |
          | EarlyYears           | 0      |
          | Primary              | 13850  |
          | Secondary            | 0      |
          | Special              | 288161 |
          | AlternativeProvision | 0      |
          | PostSchool           | 0      |
          | Income               | 0      |
        And the high needs history result should contain the following outturn place funding values for '2024':
          | Field                | Value |
          | Primary              | 0     |
          | Secondary            | 0     |
          | Special              | 0     |
          | AlternativeProvision | 0     |

    @HighNeedsFlagEnabled
    Scenario: Sending a valid high needs history request returns the expected budget values
        Given a valid high needs history request with LA codes:
          | Code |
          | 201  |
        When I submit the high needs request
        Then the high needs history result should be ok and have the following values:
          | Start year | End year |
          | 2020       | 2024     |
        And the high needs history result should not contain budget values for '2020'
        And the high needs history result should contain the following budget values for '2021':
          | Field | Value |
          | Year  | 2021  |
          | Code  | 201   |
          | Total |       |
        And the high needs history result should contain the following budget values for '2022':
          | Field | Value  |
          | Year  | 2022   |
          | Code  | 201    |
          | Total | 641000 |
        And the high needs history result should contain the following budget values for '2023':
          | Field | Value  |
          | Year  | 2023   |
          | Code  | 201    |
          | Total | 570000 |
        And the high needs history result should contain the following budget values for '2024':
          | Field | Value  |
          | Year  | 2024   |
          | Code  | 201    |
          | Total | 489000 |
        And the high needs history result should contain the following budget high needs amount values for '2024':
          | Field                        | Value  |
          | TotalPlaceFunding            | 0      |
          | TopUpFundingMaintained       | 111000 |
          | TopUpFundingNonMaintained    | 241000 |
          | SenServices                  | 137000 |
          | AlternativeProvisionServices | 0      |
          | HospitalServices             | 0      |
          | OtherHealthServices          | 0      |
        And the high needs history result should contain the following budget maintained values for '2024':
          | Field                | Value  |
          | EarlyYears           | 0      |
          | Primary              | 0      |
          | Secondary            | 111000 |
          | Special              | 0      |
          | AlternativeProvision | 0      |
          | PostSchool           | 0      |
          | Income               | 0      |
        And the high needs history result should contain the following budget non maintained values for '2024':
          | Field                | Value  |
          | EarlyYears           | 0      |
          | Primary              | 0      |
          | Secondary            | 0      |
          | Special              | 241000 |
          | AlternativeProvision | 0      |
          | PostSchool           | 0      |
          | Income               | 0      |
        And the high needs history result should contain the following budget place funding values for '2024':
          | Field                | Value |
          | Primary              | 0     |
          | Secondary            | 0     |
          | Special              | 0     |
          | AlternativeProvision | 0     |

    @HighNeedsFlagEnabled
    Scenario: Sending an invalid high needs history request
        Given an invalid high needs history request
        When I submit the high needs request
        Then the high needs history result should be bad request

    @HighNeedsFlagEnabled
    Scenario: Sending a valid high needs request with Actuals dimension returns the expected outturn values
        Given a valid high needs request with dimension 'Actuals' and LA codes:
          | Code |
          | 201  |
          | 202  |
          | 203  |
        When I submit the high needs request
        Then the high needs result should be ok and have the following values for '201':
          | Field | Value          |
          | Code  | 201            |
          | Name  | City of London |
        And the high needs result should have the following values for '202':
          | Field | Value  |
          | Code  | 202    |
          | Name  | Camden |
        And the high needs result should have the following values for '203':
          | Field | Value     |
          | Code  | 203       |
          | Name  | Greenwich |
        And the high needs result should contain the following outturn values for '201':
          | Field | Value  |
          | Total | 726203 |
        And the high needs result should contain the following outturn values for '202':
          | Field | Value    |
          | Total | 50863090 |
        And the high needs result should contain the following outturn values for '203':
          | Field | Value    |
          | Total | 66487211 |
        And the high needs result should contain the following outturn high needs amount values for '201':
          | Field                        | Value  |
          | TotalPlaceFunding            | 0      |
          | TopUpFundingMaintained       | 270508 |
          | TopUpFundingNonMaintained    | 302012 |
          | SenServices                  | 143279 |
          | AlternativeProvisionServices | 0      |
          | HospitalServices             | 0      |
          | OtherHealthServices          | 10404  |
        And the high needs result should contain the following outturn maintained values for '201':
          | Field                | Value  |
          | EarlyYears           | 0      |
          | Primary              | 139146 |
          | Secondary            | 124528 |
          | Special              | 6834   |
          | AlternativeProvision | 0      |
          | PostSchool           | 0      |
          | Income               | 0      |
        And the high needs result should contain the following outturn non maintained values for '201':
          | Field                | Value  |
          | EarlyYears           | 0      |
          | Primary              | 13850  |
          | Secondary            | 0      |
          | Special              | 288161 |
          | AlternativeProvision | 0      |
          | PostSchool           | 0      |
          | Income               | 0      |
        And the high needs result should contain the following outturn place funding values for '201':
          | Field                | Value |
          | Primary              | 0     |
          | Secondary            | 0     |
          | Special              | 0     |
          | AlternativeProvision | 0     |

    @HighNeedsFlagEnabled
    Scenario: Sending a valid high needs request with Actuals dimension returns the expected budget values
        Given a valid high needs request with dimension 'Actuals' and LA codes:
          | Code |
          | 201  |
          | 202  |
          | 203  |
        When I submit the high needs request
        Then the high needs result should be ok and have the following values for '201':
          | Field | Value          |
          | Code  | 201            |
          | Name  | City of London |
        And the high needs result should have the following values for '202':
          | Field | Value  |
          | Code  | 202    |
          | Name  | Camden |
        And the high needs result should have the following values for '203':
          | Field | Value     |
          | Code  | 203       |
          | Name  | Greenwich |
        And the high needs result should contain the following budget values for '201':
          | Field | Value  |
          | Total | 489000 |
        And the high needs result should contain the following budget values for '202':
          | Field | Value    |
          | Total | 48342263 |
        And the high needs result should contain the following budget values for '203':
          | Field | Value    |
          | Total | 68565816 |
        And the high needs result should contain the following budget high needs amount values for '201':
          | Field                        | Value  |
          | TotalPlaceFunding            | 0      |
          | TopUpFundingMaintained       | 111000 |
          | TopUpFundingNonMaintained    | 241000 |
          | SenServices                  | 137000 |
          | AlternativeProvisionServices | 0      |
          | HospitalServices             | 0      |
          | OtherHealthServices          | 0      |
        And the high needs result should contain the following budget maintained values for '201':
          | Field                | Value  |
          | EarlyYears           | 0      |
          | Primary              | 0      |
          | Secondary            | 111000 |
          | Special              | 0      |
          | AlternativeProvision | 0      |
          | PostSchool           | 0      |
          | Income               | 0      |
        And the high needs result should contain the following budget non maintained values for '201':
          | Field                | Value  |
          | EarlyYears           | 0      |
          | Primary              | 0      |
          | Secondary            | 0      |
          | Special              | 241000 |
          | AlternativeProvision | 0      |
          | PostSchool           | 0      |
          | Income               | 0      |
        And the high needs result should contain the following budget place funding values for '201':
          | Field                | Value |
          | Primary              | 0     |
          | Secondary            | 0     |
          | Special              | 0     |
          | AlternativeProvision | 0     |

    @HighNeedsFlagEnabled
    Scenario: Sending a valid high needs request with PerHead dimension returns the expected outturn values
        Given a valid high needs request with dimension 'PerHead' and LA codes:
          | Code |
          | 201  |
          | 202  |
          | 203  |
        When I submit the high needs request
        Then the high needs result should be ok and have the following values for '201':
          | Field | Value          |
          | Code  | 201            |
          | Name  | City of London |
        And the high needs result should have the following values for '202':
          | Field | Value  |
          | Code  | 202    |
          | Name  | Camden |
        And the high needs result should have the following values for '203':
          | Field | Value     |
          | Code  | 203       |
          | Name  | Greenwich |
        And the high needs result should contain the following outturn values for '201':
          | Field | Value                   |
          | Total | 393.3927410617551462621 |
        And the high needs result should contain the following outturn values for '202':
          | Field | Value                   |
          | Total | 958.7945107353578766800 |
        And the high needs result should contain the following outturn values for '203':
          | Field | Value                    |
          | Total | 1005.6297511911064055055 |
        And the high needs result should contain the following outturn high needs amount values for '201':
          | Field                        | Value                   |
          | TotalPlaceFunding            | 0.0000000000000000000   |
          | TopUpFundingMaintained       | 146.5373781148429035752 |
          | TopUpFundingNonMaintained    | 163.6034669555796316359 |
          | SenServices                  | 77.6159263271939328277  |
          | AlternativeProvisionServices | 0.0000000000000000000   |
          | HospitalServices             | 0.0000000000000000000   |
          | OtherHealthServices          | 5.6359696641386782231   |
        And the high needs result should contain the following outturn maintained values for '201':
          | Field                | Value                  |
          | EarlyYears           | 0.0000000000000000000  |
          | Primary              | 75.3770314192849404117 |
          | Secondary            | 67.4582881906825568797 |
          | Special              | 3.7020585048754062838  |
          | AlternativeProvision | 0.0000000000000000000  |
          | PostSchool           | 0.0000000000000000000  |
          | Income               | 0.0000000000000000000  |
        And the high needs result should contain the following outturn non maintained values for '201':
          | Field                | Value                   |
          | EarlyYears           | 0.0000000000000000000   |
          | Primary              | 7.5027085590465872156   |
          | Secondary            | 0.0000000000000000000   |
          | Special              | 156.1002166847237269772 |
          | AlternativeProvision | 0.0000000000000000000   |
          | PostSchool           | 0.0000000000000000000   |
          | Income               | 0.0000000000000000000   |
        And the high needs result should contain the following outturn place funding values for '201':
          | Field                | Value                 |
          | Primary              | 0.0000000000000000000 |
          | Secondary            | 0.0000000000000000000 |
          | Special              | 0.0000000000000000000 |
          | AlternativeProvision | 0.0000000000000000000 |

    @HighNeedsFlagEnabled
    Scenario: Sending a valid high needs request with PerHead dimension returns the expected budget values
        Given a valid high needs request with dimension 'PerHead' and LA codes:
          | Code |
          | 201  |
          | 202  |
          | 203  |
        When I submit the high needs request
        Then the high needs result should be ok and have the following values for '201':
          | Field | Value          |
          | Code  | 201            |
          | Name  | City of London |
        And the high needs result should have the following values for '202':
          | Field | Value  |
          | Code  | 202    |
          | Name  | Camden |
        And the high needs result should have the following values for '203':
          | Field | Value     |
          | Code  | 203       |
          | Name  | Greenwich |
        And the high needs result should contain the following budget values for '201':
          | Field | Value                   |
          | Total | 264.8970747562296858071 |
        And the high needs result should contain the following budget values for '202':
          | Field | Value                   |
          | Total | 911.2756696638956436502 |
        And the high needs result should contain the following budget values for '203':
          | Field | Value                    |
          | Total | 1037.0689858579747409816 |
        And the high needs result should contain the following budget high needs amount values for '201':
          | Field                        | Value                   |
          | TotalPlaceFunding            | 0.0000000000000000000   |
          | TopUpFundingMaintained       | 60.1300108342361863488  |
          | TopUpFundingNonMaintained    | 130.5525460455037919826 |
          | SenServices                  | 74.2145178764897074756  |
          | AlternativeProvisionServices | 0.0000000000000000000   |
          | HospitalServices             | 0.0000000000000000000   |
          | OtherHealthServices          | 0.0000000000000000000   |
        And the high needs result should contain the following budget maintained values for '201':
          | Field                | Value                  |
          | EarlyYears           | 0.0000000000000000000  |
          | Primary              | 0.0000000000000000000  |
          | Secondary            | 60.1300108342361863488 |
          | Special              | 0.0000000000000000000  |
          | AlternativeProvision | 0.0000000000000000000  |
          | PostSchool           | 0.0000000000000000000  |
          | Income               | 0.0000000000000000000  |
        And the high needs result should contain the following budget non maintained values for '201':
          | Field                | Value                   |
          | EarlyYears           | 0.0000000000000000000   |
          | Primary              | 0.0000000000000000000   |
          | Secondary            | 0.0000000000000000000   |
          | Special              | 130.5525460455037919826 |
          | AlternativeProvision | 0.0000000000000000000   |
          | PostSchool           | 0.0000000000000000000   |
          | Income               | 0.0000000000000000000   |
        And the high needs result should contain the following budget place funding values for '201':
          | Field                | Value                 |
          | Primary              | 0.0000000000000000000 |
          | Secondary            | 0.0000000000000000000 |
          | Special              | 0.0000000000000000000 |
          | AlternativeProvision | 0.0000000000000000000 |

    @HighNeedsFlagEnabled
    Scenario: Sending an invalid high needs request
        Given an invalid high needs request
        When I submit the high needs request
        Then the high needs result should be bad request