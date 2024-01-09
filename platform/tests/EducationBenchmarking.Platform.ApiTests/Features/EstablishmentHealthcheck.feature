Feature: Establishment healthcheck endpoint

    Scenario: Get healthcheck
        Given a valid establishment health check request
        When I submit the establishment health check request
        Then the establishment health check result should be healthy