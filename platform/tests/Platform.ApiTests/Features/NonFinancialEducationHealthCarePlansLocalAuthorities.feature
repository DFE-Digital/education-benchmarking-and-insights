Feature: Non Financial education health care plans local authorities history endpoint

    @HighNeedsFlagEnabled
    Scenario: Sending a valid education health care plans history request with Actuals dimension returns the correct values
        Given an education health care plans history request with dimension 'Actuals' and LA codes:
          | Code |
          | 201  |
        When I submit the education health care plans request
        Then the education health care plans history result should be ok and match the expected output of 'EchpHistoryActuals.json'

    @HighNeedsFlagEnabled
    Scenario: Sending a valid education health care plans history request with Per1000 dimension returns the correct values
        Given an education health care plans history request with dimension 'Per1000' and LA codes:
          | Code |
          | 201  |
        When I submit the education health care plans request
        Then the education health care plans history result should be ok and match the expected output of 'EchpHistoryPer1000.json'
        
    @HighNeedsFlagEnabled
    Scenario: Sending a valid education health care plans history request with Per1000Pupil dimension returns the correct values
        Given an education health care plans history request with dimension 'Per1000Pupil' and LA codes:
          | Code |
          | 201  |
        When I submit the education health care plans request
        Then the education health care plans history result should be ok and match the expected output of 'EchpHistoryPer1000Pupil.json'
        
    @HighNeedsFlagEnabled
    Scenario: Sending an invalid education health care plans history request with no la codes returns bad request
        Given an education health care plans history request with no codes
        When I submit the education health care plans request
        Then the education health care plans history result should be bad request

    @HighNeedsFlagEnabled
    Scenario: Sending an invalid education health care plans history request with >10 la codes returns bad request
        Given an education health care plans history request with dimension 'Actuals' and LA codes:
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
        Then the education health care plans result should be ok and match the expected output of 'EchpActuals.json'

    @HighNeedsFlagEnabled
    Scenario: Sending a valid education health care plans request with Per1000 dimension returns the correct values
        Given an education health care plans request with dimension 'Per1000' and LA codes:
          | Code |
          | 201  |
          | 202  |
          | 203  |
        When I submit the education health care plans request
        Then the education health care plans result should be ok and match the expected output of 'EchpPer1000.json'

    @HighNeedsFlagEnabled
    Scenario: Sending a valid education health care plans request with Per1000Pupil dimension returns the correct values
        Given an education health care plans request with dimension 'Per1000Pupil' and LA codes:
          | Code |
          | 201  |
          | 202  |
          | 203  |
        When I submit the education health care plans request
        Then the education health care plans result should be ok and match the expected output of 'EchpPer1000Pupil.json'
        
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