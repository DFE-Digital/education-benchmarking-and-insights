Feature: Non Financial education health care plans local authorities history endpoint

    @HighNeedsFlagEnabled
    Scenario: Sending a valid education health care plans history request returns the correct values
        Given an education health care plans history request with LA codes:
          | Code |
          | 201  |
        When I submit the education health care plans request
        Then the education health care plans history result should be ok and have the following values:
          | StartYear | EndYear |
          | 2020      | 2024    |

        And the education health care plans history result should not have a plan for '2020'

        And the education health care plans history result should have the following plan for '2021':
          | Code | Name | Total | Mainstream | Resourced | Special | Independent | Hospital | Post16 | Other |
          | 201  |      | 20    | 14         | 14        | 1       | 3           | 0        |        |       |

        And the education health care plans history result should have the following plan for '2022':
          | Code | Name | Total | Mainstream | Resourced | Special | Independent | Hospital | Post16 | Other |
          | 201  |      | 19    | 10         | 10        | 3       | 2           | 0        |        |       |

        And the education health care plans history result should have the following plan for '2023':
          | Code | Name | Total | Mainstream | Resourced | Special | Independent | Hospital | Post16 | Other |
          | 201  |      | 21    | 13         | 14        | 2       | 3           | 0        | 2      | 8     |

        And the education health care plans history result should have the following plan for '2024':
          | Code | Name | Total | Mainstream | Resourced | Special | Independent | Hospital | Post16 | Other |
          | 201  |      | 24    | 13         | 14        | 4       | 3           | 0        | 1      | 13    |

    @HighNeedsFlagEnabled
    Scenario: Sending an invalid education health care plans history request with no la codes returns a validation error
        Given an education health care plans history request with no codes
        When I submit the education health care plans request
        Then the education health care plans history result should be bad request

    @HighNeedsFlagEnabled
    Scenario: Sending an invalid education health care plans history request with >10 la codes returns a validation error
        Given an education health care plans history request with LA codes:
          | Code |
          | 101  |
          | 102  |
          | 103  |
          | 104  |
          | 105  |
          | 106  |
          | 107  |
          | 108  |
          | 109  |
          | 110  |
          | 201  |
        When I submit the education health care plans request
        Then the education health care plans history result should be bad request

    @HighNeedsFlagEnabled
    Scenario: Sending a valid education health care plans request with Actuals dimension returns the correct values
        Given an education health care plans request with dimension 'Actuals' and LA codes:
          | Code |
          | 201  |
          | 202  |
          | 203  |
        When I submit the education health care plans request
        Then the education health care plans result should be ok and contain the following values:
          | Code | Name           | Total | Mainstream | Resourced | Special | Independent | Hospital | Post16 | Other |
          | 201  | City of London | 24    | 13         | 14        | 4       | 3           | 0        | 1      | 13    |
          | 202  | Camden         | 1532  | 605        | 860       | 321     | 117         | 14       | 165    | 982   |
          | 203  | Greenwich      | 2738  | 1134       | 1456      | 582     | 140         | 65       | 341    | 1758  |

    @HighNeedsFlagEnabled
    Scenario: Sending a valid education health care plans request with Per1000 dimension returns the correct values
        Given an education health care plans request with dimension 'Per1000' and LA codes:
          | Code |
          | 201  |
          | 202  |
          | 203  |
        When I submit the education health care plans request
        Then the education health care plans result should be ok and contain the following values:
          | Code | Name           | Total             | Mainstream        | Resourced         | Special          | Independent      | Hospital         | Post16           | Other             |
          | 201  | City of London | 13.00108342361863 | 7.04225352112676  | 7.58396533044420  | 2.16684723726977 | 1.62513542795232 | 0.00000000000000 | 0.54171180931744 | 7.04225352112676  |
          | 202  | Camden         | 28.87896096062131 | 11.40455050990593 | 16.21142717110595 | 6.05100944409885 | 2.20550811513883 | 0.26390695394823 | 3.11033195724707 | 18.51118776979773 |
          | 203  | Greenwich      | 41.41269000983135 | 17.15193223928004 | 22.02223398623610 | 8.80284353021250 | 2.11752249867654 | 0.98313544581411 | 5.15767980034787 | 26.59003251909551 |

    @HighNeedsFlagEnabled
    Scenario: Sending an invalid education health care plans request with no la codes returns a validation error
        Given an education health care plans request with no codes
        When I submit the education health care plans request
        Then the education health care plans result should be bad request

    @HighNeedsFlagEnabled
    Scenario: Sending an invalid education health care plans request with default dimension and >10 la codes returns a validation error
        Given an education health care plans request with dimension '' and LA codes:
          | Code |
          | 101  |
          | 102  |
          | 103  |
          | 104  |
          | 105  |
          | 106  |
          | 107  |
          | 108  |
          | 109  |
          | 110  |
          | 201  |
        When I submit the education health care plans request
        Then the education health care plans result should be bad request