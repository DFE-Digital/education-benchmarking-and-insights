Feature: Census Senior Leadership
    
    Scenario: Sending a valid senior leadership request
        Given a senior leadership request with query parameters:
          | ParameterName | ParameterValue |
          | Dimension     | <Dimension>    |
          | Urns          | <Urns>         |
          
        When I submit the request
        Then the result should be ok and match the expected output of '<Result>'

    Examples: 
    | Dimension        | Urns                  | Result                                |
    | Total            | 990000,990001,990002  | SeniorLeadershipTotal.json            |
    | PercentWorkforce | 990000, 990001,990002 | SeniorLeadershipPercentWorkforce.json |
    
    Scenario: Sending an invalid senior leadership request
        Given a senior leadership request with query parameters:
          | ParameterName | ParameterValue |
          | Dimension     | <Dimension>    |
          | Urn1          | <Urn1>         |
          | Urn2          | <Urn2>         |
          | Urn3          | <Urn3>         |
        When I submit the request
        Then the result should be bad request and match the expected output of '<Result>'
        
    Examples: 
      | Dimension | Urn1                 | Result                                |
      | Invalid   | 990000,990001,990002 | SeniorLeadershipInvalidDimension.json |