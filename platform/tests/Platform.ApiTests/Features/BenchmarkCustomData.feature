Feature: Benchmark Custom Data Endpoint Testing
    
    Scenario: Getting custom data successfully
        Given I have a valid custom data get request for school id '990000' containing:
          | Key                                       | Value  |
          | AdministrativeSuppliesNonEducationalCosts | 123.00 |
        When I submit the custom data request
        Then the custom data response should contain:
          | Key                                       | Value  |
          | AdministrativeSuppliesNonEducationalCosts | 123.00 |
          
    Scenario: Setting custom data successfully
        Given I have a valid custom data put request for school id '990000' containing:
          | Key                                       | Value  |
          | AdministrativeSuppliesNonEducationalCosts | 123.00 |
        When I submit the custom data request
        Then the custom data response should return accepted
        
    Scenario: Deleting custom data successfully
        Given I have a valid custom data delete request for school id '990000' containing:
          | Key                                       | Value  |
          | AdministrativeSuppliesNonEducationalCosts | 123.00 |
        When I submit the custom data request
        Then the custom data response should return ok
        
    Scenario: Deleting custom data unsuccessfully
        Given I have an invalid custom data delete request for school id '990000'
        When I submit the custom data request
        Then the custom data response should return not found