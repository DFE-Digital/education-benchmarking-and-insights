Feature: Local authorities high needs endpoints

    @HighNeedsFlagEnabled
    Scenario: Sending a valid high needs history request with Actuals dimension returns the expected budget and outturn values
        Given a valid high needs history request with dimension 'Actuals' and LA codes:
          | Code |
          | 201  |
        When I submit the high needs request
        Then the high needs history result should be ok and match the expected output of 'LaHighNeedsHistoryActuals.json'
        
    @HighNeedsFlagEnabled
    Scenario: Sending a valid high needs history request with PerHead dimension returns the expected budget and outturn values
        Given a valid high needs history request with dimension 'PerHead' and LA codes:
          | Code |
          | 201  |
        When I submit the high needs request
        Then the high needs history result should be ok and match the expected output of 'LaHighNeedsHistoryPerHead.json'

    @HighNeedsFlagEnabled
    Scenario: Sending a valid high needs history request with PerPupil dimension returns the expected budget and outturn values
        Given a valid high needs history request with dimension 'PerPupil' and LA codes:
          | Code |
          | 201  |
        When I submit the high needs request
        Then the high needs history result should be ok and match the expected output of 'LaHighNeedsHistoryPerPupil.json'
        
    @HighNeedsFlagEnabled
    Scenario: Sending an invalid high needs history request
        Given an invalid high needs history request
        When I submit the high needs request
        Then the high needs history result should be bad request

    @HighNeedsFlagEnabled
    Scenario: Sending a valid high needs request with Actuals dimension returns the expected budget and outturn values
        Given a valid high needs request with dimension 'Actuals' and LA codes:
          | Code |
          | 201  |
          | 202  |
          | 203  |
        When I submit the high needs request
        Then the high needs result should be ok and match the expected output of 'LaHighNeedsActuals.json'

    @HighNeedsFlagEnabled
    Scenario: Sending a valid high needs request with PerHead dimension returns the expected budget and outturn values
        Given a valid high needs request with dimension 'PerHead' and LA codes:
          | Code |
          | 201  |
          | 202  |
          | 203  |
        When I submit the high needs request
        Then the high needs result should be ok and match the expected output of 'LaHighNeedsPerHead.json'

    @HighNeedsFlagEnabled
    Scenario: Sending a valid high needs request with PerPupil dimension returns the expected budget and outturn values
        Given a valid high needs request with dimension 'PerPupil' and LA codes:
          | Code |
          | 201  |
          | 202  |
          | 203  |
        When I submit the high needs request
        Then the high needs result should be ok and match the expected output of 'LaHighNeedsPerPupil.json'
        
    @HighNeedsFlagEnabled
    Scenario: Sending an invalid high needs request
        Given an invalid high needs request
        When I submit the high needs request
        Then the high needs result should be bad request