Feature: Local Authority Education Health Care Plans

    Scenario Outline: Sending a valid education health care plans history request returns the correct values
        Given a history request with dimension '<dimension>' and LA codes:
          | Code |
          | 201  |
        When I submit the request
        Then the history result should be ok and match the expected output of '<result>'
        
        Examples:
          | dimension      | result                        |
          | Actuals        | EchpHistoryActuals.json        |
          | Per1000        | EchpHistoryPer1000.json        |
          | Per1000Pupil   | EchpHistoryPer1000Pupil.json   |

    Scenario Outline: Sending a valid education health care plans request returns the correct values
        Given a request with dimension '<dimension>' and LA codes:
          | Code |
          | 201  |
          | 202  |
          | 203  |
        When I submit the request
        Then the result should be ok and match the expected output of '<result>'
        
        Examples:
          | dimension      | result              |
          | Actuals        | EchpActuals.json     |
          | Per1000        | EchpPer1000.json     |
          | Per1000Pupil   | EchpPer1000Pupil.json |

    Scenario Outline: Sending an invalid education health care plans history request returns bad request
        Given a history request with <issue>
        When I submit the request
        Then the history result should be bad request and match the expected output of '<result>'
        
        Examples:
          | issue              | result                            |
          | no codes           | EchpHistoryEmptyCodes.json         |
          | more than 30 codes | EchpHistoryTooManyCodes.json       |
          | invalid dimension  | EchpHistoryInvalidDimension.json   |

    Scenario Outline: Sending an invalid education health care plans request returns bad request
        Given a request with <issue>
        When I submit the request
        Then the result should be bad request and match the expected output of '<result>'
        
        Examples:
          | issue              | result                      |
          | no codes           | EchpEmptyCodes.json          |
          | more than 30 codes | EchpTooManyCodes.json        |
          | invalid dimension  | EchpInvalidDimension.json    |

    Scenario: Sending an education health care plans history request for an invalid local authority returns not found
        Given a history request with dimension 'Actuals' and LA codes:
          | Code           |
          | willNotBeFound |
        When I submit the request
        Then the history result should be not found

    Scenario: Sending an education health care plans request for an invalid local authority returns empty
        Given a request with dimension 'Actuals' and LA codes:
          | Code           |
          | willNotBeFound |
        When I submit the request
        Then the result should be ok and match the expected output of 'EchpInvalidLa.json'

