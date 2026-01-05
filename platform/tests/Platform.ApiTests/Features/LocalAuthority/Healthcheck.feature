Feature: Local Authority Healthcheck

    Scenario: Sending a valid healthcheck request
        Given a valid request
        When I submit the request
        Then the result should be healthy