Feature: Establishment healthcheck endpoint

    Scenario: Sending a valid healthcheck request
        Given a valid establishment health check request
        When I submit the establishment health check request
        Then the establishment health check result should be healthy