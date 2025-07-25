Feature: Insights IT Spend endpoints
                
    Scenario: Sending a schools query request with dimension 'Actuals'
        Given a schools query request with dimension 'Actuals' and with URNs:
        | Urn    |
        | 990000 |
        | 990001 |
        | 990002 |
        When I submit the IT spend schools request
        Then the schools IT spend query result should be ok and match the expected output of 'ItSpendSchoolsActual.json'
        
    Scenario: Sending a schools query request with dimension 'PerUnit'
        Given a schools query request with dimension 'PerUnit' and with URNs:
          | Urn    |
          | 990000 |
          | 990001 |
          | 990002 |
        When I submit the IT spend schools request
        Then the schools IT spend query result should be ok and match the expected output of 'ItSpendSchoolsPerUnit.json'
        
    Scenario: Sending a schools query request with dimension 'PercentExpenditure'
        Given a schools query request with dimension 'PercentExpenditure' and with URNs:
          | Urn    |
          | 990000 |
          | 990001 |
          | 990002 |
        When I submit the IT spend schools request
        Then the schools IT spend query result should be ok and match the expected output of 'ItSpendSchoolsPercentExpenditure.json'
        
    Scenario: Sending a schools query request with dimension 'PercentIncome'
        Given a schools query request with dimension 'PercentIncome' and with URNs:
          | Urn    |
          | 990000 |
          | 990001 |
          | 990002 |
        When I submit the IT spend schools request
        Then the schools IT spend query result should be ok and match the expected output of 'ItSpendSchoolsPercentIncome.json'
    
    Scenario: Sending an schools query request with an invalid dimension
        Given a schools query request with dimension 'invalid' and with URNs:
          | Urn    |
          | 990000 |
          | 990001 |
          | 990002 |
        When I submit the IT spend schools request
        Then the schools IT spend query result should be bad request