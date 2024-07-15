Feature: Benchmark User Data Endpoint Testing
    
    Scenario: Getting user data successfully
        Given I have a valid user data get request for school id '990000' containing custom data:
          | Key                                       | Value  |
          | AdministrativeSuppliesNonEducationalCosts | 123.00 |
        When I submit the user data request
        Then the user data response should be ok
   