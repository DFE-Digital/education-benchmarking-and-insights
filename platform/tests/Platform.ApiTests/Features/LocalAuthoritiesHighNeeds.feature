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
          | Field                 | Value          |
          | Code                  | 201            |
          | Name                  | City of London |
          | Population2To18       | 1756           |
          | CarriedForwardBalance | -1358598       |
        And the high needs result should have the following values for '202':
          | Field                 | Value     |
          | Code                  | 202       |
          | Name                  | Camden    |
          | Population2To18       | 52239     |
          | CarriedForwardBalance | -13202762 |
        And the high needs result should have the following values for '203':
          | Field                 | Value     |
          | Code                  | 203       |
          | Name                  | Greenwich |
          | Population2To18       | 65643     |
          | CarriedForwardBalance | -4048211  |
        And the high needs result should contain the following outturn values for '201':
          | Field | Value  |
          | Total | 476694 |
        And the high needs result should contain the following outturn values for '202':
          | Field | Value    |
          | Total | 38882018 |
        And the high needs result should contain the following outturn values for '203':
          | Field | Value    |
          | Total | 51215869 |
        And the high needs result should contain the following outturn high needs amount values for '201':
          | Field                        | Value  |
          | TotalPlaceFunding            | 0      |
          | TopUpFundingMaintained       | 59325  |
          | TopUpFundingNonMaintained    | 274823 |
          | SenServices                  | 130891 |
          | AlternativeProvisionServices | 0      |
          | HospitalServices             | 0      |
          | OtherHealthServices          | 11655  |
        And the high needs result should contain the following outturn maintained values for '201':
          | Field                | Value |
          | EarlyYears           | 0     |
          | Primary              | 59325 |
          | Secondary            | 0     |
          | Special              | 0     |
          | AlternativeProvision | 0     |
          | PostSchool           | 0     |
          | Income               | 0     |
        And the high needs result should contain the following outturn non maintained values for '201':
          | Field                | Value  |
          | EarlyYears           | 0      |
          | Primary              | 0      |
          | Secondary            | 98600  |
          | Special              | 139827 |
          | AlternativeProvision | 0      |
          | PostSchool           | 36397  |
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
          | Field                 | Value          |
          | Code                  | 201            |
          | Name                  | City of London |
          | Population2To18       | 1756           |
          | CarriedForwardBalance | -1358598       |
        And the high needs result should have the following values for '202':
          | Field                 | Value     |
          | Code                  | 202       |
          | Name                  | Camden    |
          | Population2To18       | 52239     |
          | CarriedForwardBalance | -13202762 |
        And the high needs result should have the following values for '203':
          | Field                 | Value     |
          | Code                  | 203       |
          | Name                  | Greenwich |
          | Population2To18       | 65643     |
          | CarriedForwardBalance | -4048211  |
        And the high needs result should contain the following budget values for '201':
          | Field | Value  |
          | Total | 641000 |
        And the high needs result should contain the following budget values for '202':
          | Field | Value    |
          | Total | 39897962 |
        And the high needs result should contain the following budget values for '203':
          | Field | Value    |
          | Total | 56539657 |
        And the high needs result should contain the following budget high needs amount values for '201':
          | Field                        | Value  |
          | TotalPlaceFunding            | 0      |
          | TopUpFundingMaintained       | 317000 |
          | TopUpFundingNonMaintained    | 194000 |
          | SenServices                  | 130000 |
          | AlternativeProvisionServices | 0      |
          | HospitalServices             | 0      |
          | OtherHealthServices          | 0      |
        And the high needs result should contain the following budget maintained values for '201':
          | Field                | Value  |
          | EarlyYears           | 0      |
          | Primary              | 69000  |
          | Secondary            | 54000  |
          | Special              | 194000 |
          | AlternativeProvision | 0      |
          | PostSchool           | 0      |
          | Income               | 0      |
        And the high needs result should contain the following budget non maintained values for '201':
          | Field                | Value  |
          | EarlyYears           | 0      |
          | Primary              | 0      |
          | Secondary            | 0      |
          | Special              | 194000 |
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
          | Field                 | Value          |
          | Code                  | 201            |
          | Name                  | City of London |
          | Population2To18       | 1756           |
          | CarriedForwardBalance | -1358598       |
        And the high needs result should have the following values for '202':
          | Field                 | Value     |
          | Code                  | 202       |
          | Name                  | Camden    |
          | Population2To18       | 52239     |
          | CarriedForwardBalance | -13202762 |
        And the high needs result should have the following values for '203':
          | Field                 | Value     |
          | Code                  | 203       |
          | Name                  | Greenwich |
          | Population2To18       | 65643     |
          | CarriedForwardBalance | -4048211  |
        And the high needs result should contain the following outturn values for '201':
          | Field | Value                   |
          | Total | 271.4658314350797266514 |
        And the high needs result should contain the following outturn values for '202':
          | Field | Value                   |
          | Total | 744.3101514194375849461 |
        And the high needs result should contain the following outturn values for '203':
          | Field | Value                   |
          | Total | 780.2182867937175327148 |
        And the high needs result should contain the following outturn high needs amount values for '201':
          | Field                        | Value                   |
          | TotalPlaceFunding            | 0.0000000000000000000   |
          | TopUpFundingMaintained       | 33.7841685649202733485  |
          | TopUpFundingNonMaintained    | 156.5051252847380410022 |
          | SenServices                  | 74.5392938496583143507  |
          | AlternativeProvisionServices | 0.0000000000000000000   |
          | HospitalServices             | 0.0000000000000000000   |
          | OtherHealthServices          | 6.6372437357630979498   |
        And the high needs result should contain the following outturn maintained values for '201':
          | Field                | Value                  |
          | EarlyYears           | 0.0000000000000000000  |
          | Primary              | 33.7841685649202733485 |
          | Secondary            | 0.0000000000000000000  |
          | Special              | 0.0000000000000000000  |
          | AlternativeProvision | 0.0000000000000000000  |
          | PostSchool           | 0.0000000000000000000  |
          | Income               | 0.0000000000000000000  |
        And the high needs result should contain the following outturn non maintained values for '201':
          | Field                | Value                  |
          | EarlyYears           | 0.0000000000000000000  |
          | Primary              | 0.0000000000000000000  |
          | Secondary            | 56.1503416856492027334 |
          | Special              | 79.6281321184510250569 |
          | AlternativeProvision | 0.0000000000000000000  |
          | PostSchool           | 20.7272209567198177676 |
          | Income               | 0.0000000000000000000  |
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
          | Field                 | Value          |
          | Code                  | 201            |
          | Name                  | City of London |
          | Population2To18       | 1756           |
          | CarriedForwardBalance | -1358598       |
        And the high needs result should have the following values for '202':
          | Field                 | Value     |
          | Code                  | 202       |
          | Name                  | Camden    |
          | Population2To18       | 52239     |
          | CarriedForwardBalance | -13202762 |
        And the high needs result should have the following values for '203':
          | Field                 | Value     |
          | Code                  | 203       |
          | Name                  | Greenwich |
          | Population2To18       | 65643     |
          | CarriedForwardBalance | -4048211  |
        And the high needs result should contain the following budget values for '201':
          | Field | Value                   |
          | Total | 365.0341685649202733485 |
        And the high needs result should contain the following budget values for '202':
          | Field | Value                   |
          | Total | 763.7581500411569899883 |
        And the high needs result should contain the following budget values for '203':
          | Field | Value                   |
          | Total | 861.3204302058102158646 |
        And the high needs result should contain the following budget high needs amount values for '201':
          | Field                        | Value                   |
          | TotalPlaceFunding            | 0.0000000000000000000   |
          | TopUpFundingMaintained       | 180.5239179954441913439 |
          | TopUpFundingNonMaintained    | 110.4783599088838268792 |
          | SenServices                  | 74.0318906605922551252  |
          | AlternativeProvisionServices | 0.0000000000000000000   |
          | HospitalServices             | 0.0000000000000000000   |
          | OtherHealthServices          | 0.0000000000000000000   |
        And the high needs result should contain the following budget maintained values for '201':
          | Field                | Value                   |
          | EarlyYears           | 0.0000000000000000000   |
          | Primary              | 39.2938496583143507972  |
          | Secondary            | 30.7517084282460136674  |
          | Special              | 110.4783599088838268792 |
          | AlternativeProvision | 0.0000000000000000000   |
          | PostSchool           | 0.0000000000000000000   |
          | Income               | 0.0000000000000000000   |
        And the high needs result should contain the following budget non maintained values for '201':
          | Field                | Value                   |
          | EarlyYears           | 0.0000000000000000000   |
          | Primary              | 0.0000000000000000000   |
          | Secondary            | 0.0000000000000000000   |
          | Special              | 110.4783599088838268792 |
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