Feature: Local Authority Accounts High Needs

    Scenario: valid high needs history request returns the expected budget and outturn values
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

    Scenario: Sending an invalid high needs history request
        Given an invalid history request
        When I submit the request
        Then the history result should be bad request

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

    Scenario: Sending an invalid high needs request
        Given an invalid request
        When I submit the request
        Then the result should be bad request