Feature: Insights IT Spend endpoints
                
    Scenario: Sending a valid schools IT spend request with dimension 'Actuals'
        Given a schools IT spend request with dimension 'Actuals' and URNs:
        | Urn    |
        | 990000 |
        | 990001 |
        | 990002 |
        When I submit the IT spend request
        Then the schools result should be ok and match the expected output of 'ItSpendSchoolsActual.json'
        
    Scenario: Sending a valid schools IT spend request with dimension 'PerUnit'
        Given a schools IT spend request with dimension 'PerUnit' and URNs:
          | Urn    |
          | 990000 |
          | 990001 |
          | 990002 |
        When I submit the IT spend request
        Then the schools result should be ok and match the expected output of 'ItSpendSchoolsPerUnit.json'
        
    Scenario: Sending a valid schools IT spend request with dimension 'PercentExpenditure'
        Given a schools IT spend request with dimension 'PercentExpenditure' and URNs:
          | Urn    |
          | 990000 |
          | 990001 |
          | 990002 |
        When I submit the IT spend request
        Then the schools result should be ok and match the expected output of 'ItSpendSchoolsPercentExpenditure.json'
        
    Scenario: Sending a valid schools IT spend request with dimension 'PercentIncome'
        Given a schools IT spend request with dimension 'PercentIncome' and URNs:
          | Urn    |
          | 990000 |
          | 990001 |
          | 990002 |
        When I submit the IT spend request
        Then the schools result should be ok and match the expected output of 'ItSpendSchoolsPercentIncome.json'
    
    Scenario: Sending an invalid schools IT spend request with an invalid dimension
        Given a schools IT spend request with dimension 'invalid' and URNs:
          | Urn    |
          | 990000 |
          | 990001 |
          | 990002 |
        When I submit the IT spend request
        Then the schools IT spend result should be bad request
        
    Scenario: Sending a valid trusts IT spend request
        Given a trusts IT spend request with company numbers:
          | CompanyNumbers |
          | 10192252       |
          | 10259334       |
          | 10264735       |
        When I submit the IT spend request
        Then the trusts result should be ok and match the expected output of 'ItSpendTrusts.json'
        
    Scenario: Sending an invalid trusts IT spend request with no URNs
        Given a trusts IT spend request with no company numbers
        When I submit the IT spend request
        Then the trusts IT spend result should be bad request
        
    Scenario: Sending a valid trust forecast IT spend request
        Given a trust forecast IT spend request with company number '10192252' and year '2022'
        When I submit the IT spend request
        Then the trust forecast result should be ok and match the expected output of 'ItSpendTrustForecast.json'
        
    Scenario: Sending an invalid trust forecast IT spend request with no URN
        Given a trust forecast IT spend request with company number '' and year '2022'
        When I submit the IT spend request
        Then the trust forecast IT spend result should be bad request
        
    Scenario: Sending an invalid trust forecast IT spend request with no year
        Given a trust forecast IT spend request with company number '10192252' and year ''
        When I submit the IT spend request
        Then the trust forecast IT spend result should be bad request