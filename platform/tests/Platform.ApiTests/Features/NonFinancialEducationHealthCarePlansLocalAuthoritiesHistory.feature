Feature: Non Financial education health care plans local authorities history endpoint

# TODO: these tests use stubbed data see EducationHealthCarePlansStubService
# replace with real data once implemented

    Scenario: Sending a valid education health care plans history request returns the correct values
        Given an education health care plans history request with codes '201'
        When I submit the education health care plans history request
        Then the education health care plans history result should be ok and have the following values:
          | StartYear | EndYear |
          | 2020      | 2024    |

        And the education health care plans history result should be ok and have the following plan for '2020':
          | Code | Name                | Total | Mainstream | Resourced | Special | Independent | Hospital | Post16 | Other |
          | 201  | Local authority 201 | 52    | 21         | 1         | 2       | 1           | 21       | 1      | 5     |

        And the education health care plans history result should be ok and have the following plan for '2021':
          | Code | Name                | Total | Mainstream | Resourced | Special | Independent | Hospital | Post16 | Other |
          | 201  | Local authority 201 | 59    | 22         | 2         | 3       | 2           | 22       | 2      | 6     |

        And the education health care plans history result should be ok and have the following plan for '2022':
          | Code | Name                | Total | Mainstream | Resourced | Special | Independent | Hospital | Post16 | Other |
          | 201  | Local authority 201 | 66    | 23         | 3         | 4       | 3           | 23       | 3      | 7     |

        And the education health care plans history result should be ok and have the following plan for '2023':
          | Code | Name                | Total | Mainstream | Resourced | Special | Independent | Hospital | Post16 | Other |
          | 201  | Local authority 201 | 65    | 24         | 4         | 5       | 4           | 24       | 4      | 0     |

        And the education health care plans history result should be ok and have the following plan for '2024':
          | Code | Name                | Total | Mainstream | Resourced | Special | Independent | Hospital | Post16 | Other |
          | 201  | Local authority 201 | 47    | 25         | 5         | 6       | 5           | 0        | 5      | 1     |

    Scenario: Sending an invalid education health care plans history request with no la codes returns a validation error
        Given an education health care plans history request with no codes
        When I submit the education health care plans history request
        Then the education health care plans history result should be bad request

    Scenario: Sending an invalid education health care plans history request with >10 la codes returns a validation error
        Given an education health care plans history request with codes '101,102,103,104,105,106,107,108,109,110,201'
        When I submit the education health care plans history request
        Then the education health care plans history result should be bad request