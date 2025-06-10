Feature: Content commercial resource endpoints

    Scenario: Submit a valid request
        Given a valid request
        When I submit the request
        Then the result should be ok and match the expected output