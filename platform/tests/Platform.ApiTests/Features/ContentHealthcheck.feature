Feature: Content healthcheck endpoint

    Scenario: Sending a valid healthcheck request
        Given a valid content health check request
        When I submit the content health check request
        Then the content health check result should be healthy