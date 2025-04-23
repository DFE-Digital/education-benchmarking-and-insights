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
          | Code | Name           | Population2To18 | Total | Mainstream | Resourced | Special | Independent | Hospital | Post16 | Other |
          | 201  | City of London | 1756            | 19    | 10         | 10        | 3       | 2           | 0        |        |       |
          | 202  | Camden         | 52239           | 1440  | 590        | 791       | 323     | 95          | 17       |        |       |
          | 203  | Greenwich      | 65643           | 2180  | 840        | 1088      | 557     | 110         | 57       |        |       |

    @HighNeedsFlagEnabled
    Scenario: Sending a valid education health care plans request with Per1000 dimension returns the correct values
        Given an education health care plans request with dimension 'Per1000' and LA codes:
          | Code |
          | 201  |
          | 202  |
          | 203  |
        When I submit the education health care plans request
        Then the education health care plans result should be ok and contain the following values:
          | Code | Name           | Population2To18 | Total             | Mainstream        | Resourced         | Special          | Independent      | Hospital         | Post16 | Other |
          | 201  | City of London | 1756            | 10.82004555808656 | 5.69476082004555  | 5.69476082004555  | 1.70842824601366 | 1.13895216400911 | 0.00000000000000 |        |       |
          | 202  | Camden         | 52239           | 27.56561189915580 | 11.29424376423744 | 15.14194375849461 | 6.18311989126897 | 1.81856467390263 | 0.32542736269836 |        |       |
          | 203  | Greenwich      | 65643           | 33.20993860731532 | 12.79649010557104 | 16.57450147007297 | 8.48529165333698 | 1.67573084715811 | 0.86833325716374 |        |       |

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