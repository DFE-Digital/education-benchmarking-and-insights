Feature: Content commercial resource endpoints

    Scenario: Submit a valid request
        Given a valid request
        When I submit the request
        Then the result should be ok and match the expected output
        
    Scenario: Submit a valid request for API version 1.0
        Given a valid request with API version '1.0'
        When I submit the request
        Then the result should be ok and match the expected output
        
    Scenario: Submit a valid request for unsupported API version
        Given a valid request with API version 'invalid'
        When I submit the request
        Then the result should be bad request