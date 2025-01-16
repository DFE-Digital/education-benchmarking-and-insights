Feature: Benchmark Custom Data Endpoint Testing

    Scenario: Getting custom data successfully
        Given I have a valid custom data get request for school id '990000' containing:
          | Key                                       | Value  |
          | AdministrativeSuppliesNonEducationalCosts | 123.00 |
        When I submit the custom data request
        Then new user data should be created for school id '990000'
        And the custom data response should contain:
          | Key                                       | Value  |
          | AdministrativeSuppliesNonEducationalCosts | 123.00 |

    Scenario: Setting custom data successfully
        Given I have a valid custom data post request for school id '990000' containing:
          | Key                                       | Value  |
          | AdministrativeSuppliesNonEducationalCosts | 123.00 |
        When I submit the custom data request
        Then new user data should be created for school id '990000'

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