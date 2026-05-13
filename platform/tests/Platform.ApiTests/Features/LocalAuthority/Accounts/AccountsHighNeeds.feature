Feature: Local Authority Accounts - High Needs

    Scenario Outline: High needs history request with dimension '<Dimension>' returns 200 OK and expected data
        Given a valid history request with dimension '<Dimension>' and LA codes:
          | Code |
          |  201 |
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

    Scenario: High needs history request for an invalid local authority returns 404 Not Found
        Given a valid history request with dimension 'Actuals' and LA codes:
          | Code |
          |  000 |
        When I submit the request
        Then the history result should be not found
        
    Scenario Outline: High needs request with dimension '<Dimension>' and type '<Type>' returns 200 OK and expected data
        Given a valid request to endpoint version '<Endpoint Version>' with dimension '<Dimension>' type '<Type>' and LA codes:
          | Code |
          |  201 |
          |  202 |
          |  203 |
        When I submit the request
        Then the result should be ok and match the expected output of '<Result>'

    Examples:
      | Result                                 | Type    | Dimension       | Endpoint Version |
      | LaHighNeedsPerPupil.json               |         | PerPupil        | 1.0              |
      | LaHighNeedsActuals.json                |         | Actuals         | 1.0              |
      | LaHighNeedsPerHead.json                |         | PerHead         | 1.0              |
      | LaHighNeedsBudgetPerPupil.json         | Budget  | PerPupil        | 2.0              |
      | LaHighNeedsBudgetPerEhcp.json          | Budget  | PerEhcp         | 2.0              |
      | LaHighNeedsBudgetPerSenSupport.json    | Budget  | PerSenSupport   | 2.0              |
      | LaHighNeedsBudgetPerTotalSupport.json  | Budget  | PerTotalSupport | 2.0              |
      | LaHighNeedsOutturnPerPupil.json        | Outturn | PerPupil        | 2.0              |
      | LaHighNeedsOutturnPerEhcp.json         | Outturn | PerEhcp         | 2.0              |
      | LaHighNeedsOutturnPerSenSupport.json   | Outturn | PerSenSupport   | 2.0              |
      | LaHighNeedsOutturnPerTotalSupport.json | Outturn | PerTotalSupport | 2.0              |

    Scenario Outline: High needs request with invalid parameters '<Issue>' returns 400 Bad Request
        Given an invalid request with '<Issue>' to endpoint version '<Endpoint Version>'
        When I submit the request
        Then the result should be bad request and match the expected output in '<Result>'

    Examples:
      | Issue              | Result                             | Endpoint Version |
      | no codes           | LaHighNeedsEmptyCodes.json         | 1.0              |
      | more than 30 codes | LaHighNeedsTooManyCodes.json       | 1.0              |
      | invalid dimension  | LaHighNeedsInvalidDimensionV1.json | 1.0              |
      | no codes           | LaHighNeedsEmptyCodes.json         | 2.0              |
      | more than 30 codes | LaHighNeedsTooManyCodes.json       | 2.0              |
      | invalid dimension  | LaHighNeedsInvalidDimensionV2.json | 2.0              |

    Scenario: High needs request for an invalid local authority returns 200 OK and empty array
        Given a valid request to endpoint version '<Endpoint Version>' with dimension 'PerPupil' type '' and LA codes:
          | Code |
          |  000 |
        When I submit the request
        Then the result should match '<Response>'
    
    Examples:
      | Endpoint Version | Response  |
      | 1.0              | OK        |
      | 2.0              | Not Found |