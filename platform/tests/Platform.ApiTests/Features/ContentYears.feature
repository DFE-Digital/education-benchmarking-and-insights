Feature: Content years endpoints

    Scenario: Sending a valid finance year request
        Given a current return years request
        When I submit the request
        Then the current return years result should be:
          | Field | Value |
          | Aar   | 2022  |
          | Cfr   | 2022  |
          | S251  | 2024  |

    Scenario: Sending a valid request for API version 1.0
        Given a current return years request with API version '1.0'
        When I submit the request
        Then the current return years result should be:
          | Field | Value |
          | Aar   | 2022  |
          | Cfr   | 2022  |
          | S251  | 2024  |
          
    Scenario: Sending a valid request for unsupported API version
        Given a current return years request with API version 'version'
        When I submit the request
        Then the current return years result should be bad request