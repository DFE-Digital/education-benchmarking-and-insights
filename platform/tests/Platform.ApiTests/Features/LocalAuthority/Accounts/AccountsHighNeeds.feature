Feature: Local Authority Accounts - High Needs

    Scenario Outline: High needs history request with dimension '<Dimension>' returns 200 OK and expected data
        Given a valid history request with dimension '<Dimension>' and LA codes:
          | Code |
          | 201  |
        When I submit the request
        Then the history result should be ok and match the expected output of '<Result>'

    Examples:
      | Result                          | Dimension |
      | LaHighNeedsHistoryActuals.json  | Actuals   |
      | LaHighNeedsHistoryPerHead.json  | PerHead   |
      | LaHighNeedsHistoryPerPupil.json | PerPupil  |

    Scenario Outline: High needs history request with invalid parameters '<Issue>' returns 400 Bad Request
        Given an invalid history request with '<Issue>'
        When I submit the request
        Then the history result should be bad request and match the expected output in '<Result>'

    Examples:
      | Issue              | Result                                  |
      | no codes           | LaHighNeedsHistoryEmptyCodes.json       |
      | more than 30 codes | LaHighNeedsHistoryTooManyCodes.json     |
      | invalid dimension  | LaHighNeedsHistoryInvalidDimension.json |

    Scenario: Sending a high needs history request for an invalid local authority returns not found
        Given a valid history request with dimension 'Actuals' and LA codes:
          | Code |
          | 000  |
        When I submit the request
        Then the history result should be not found

    Scenario: Sending a valid high needs request returns the expected budget and outturn values
        Given a valid request with dimension '<Dimension>' and LA codes:
          | Code |
          | 201  |
          | 202  |
          | 203  |
        When I submit the request
        Then the result should be ok and match the expected output of '<Result>'

    Examples:
      | Result                   | Dimension |
      | LaHighNeedsActuals.json  | Actuals   |
      | LaHighNeedsPerHead.json  | PerHead   |
      | LaHighNeedsPerPupil.json | PerPupil  |

    Scenario Outline: High needs request with invalid parameters '<Issue>' returns 400 Bad Request
        Given an invalid request with '<Issue>'
        When I submit the request
        Then the result should be bad request and match the expected output in '<Result>'

    Examples:
      | Issue              | Result                           |
      | no codes           | LaHighNeedsEmptyCodes.json       |
      | more than 30 codes | LaHighNeedsTooManyCodes.json     |
      | invalid dimension  | LaHighNeedsInvalidDimension.json |

    Scenario: High needs request for an invalid local authority returns 200 OK and empty array
        Given a valid request with dimension 'Actuals' and LA codes:
          | Code |
          | 000  |
        When I submit the request
        Then the result should be ok and match the expected output of 'LaHighNeedsEmptyArray.json'