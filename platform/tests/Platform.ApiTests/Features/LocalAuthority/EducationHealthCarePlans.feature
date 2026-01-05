Feature: Local Authority Education Health Care Plans

    Scenario: Sending a valid education health care plans history request with Actuals dimension returns the correct values
        Given a history request with dimension 'Actuals' and LA codes:
          | Code |
          | 201  |
        When I submit the request
        Then the history result should be ok and match the expected output of 'EchpHistoryActuals.json'

    Scenario: Sending a valid education health care plans history request with Per1000 dimension returns the correct values
        Given a history request with dimension 'Per1000' and LA codes:
          | Code |
          | 201  |
        When I submit the request
        Then the history result should be ok and match the expected output of 'EchpHistoryPer1000.json'
        
    Scenario: Sending a valid education health care plans history request with Per1000Pupil dimension returns the correct values
        Given a history request with dimension 'Per1000Pupil' and LA codes:
          | Code |
          | 201  |
        When I submit the request
        Then the history result should be ok and match the expected output of 'EchpHistoryPer1000Pupil.json'
        
    Scenario: Sending an invalid education health care plans history request with no la codes returns bad request
        Given a history request with no codes
        When I submit the request
        Then the history result should be bad request

    Scenario: Sending an invalid education health care plans history request with >10 la codes returns bad request
        Given a history request with dimension 'Actuals' and LA codes:
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
        When I submit the request
        Then the history result should be bad request

    Scenario: Sending a valid education health care plans request with Actuals dimension returns the correct values
        Given a request with dimension 'Actuals' and LA codes:
          | Code |
          | 201  |
          | 202  |
          | 203  |
        When I submit the request
        Then the result should be ok and match the expected output of 'EchpActuals.json'

    Scenario: Sending a valid education health care plans request with Per1000 dimension returns the correct values
        Given a request with dimension 'Per1000' and LA codes:
          | Code |
          | 201  |
          | 202  |
          | 203  |
        When I submit the request
        Then the result should be ok and match the expected output of 'EchpPer1000.json'

    Scenario: Sending a valid education health care plans request with Per1000Pupil dimension returns the correct values
        Given a request with dimension 'Per1000Pupil' and LA codes:
          | Code |
          | 201  |
          | 202  |
          | 203  |
        When I submit the request
        Then the result should be ok and match the expected output of 'EchpPer1000Pupil.json'
        
    Scenario: Sending an invalid education health care plans request with no la codes returns a validation error
        Given a request with no codes
        When I submit the request
        Then the result should be bad request

    Scenario: Sending an invalid education health care plans request with default dimension and >10 la codes returns a validation error
        Given a request with dimension '' and LA codes:
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
        When I submit the request
        Then the result should be bad request