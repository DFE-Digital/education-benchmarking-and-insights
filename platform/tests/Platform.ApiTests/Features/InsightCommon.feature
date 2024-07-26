Feature: Insights common endpoints

    Scenario: Sending a valid finance year request
        Given a current return years request
        When I submit the request
        Then the current return years result should be ok