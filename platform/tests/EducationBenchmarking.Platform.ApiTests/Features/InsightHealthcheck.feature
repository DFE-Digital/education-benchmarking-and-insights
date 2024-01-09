Feature: Insight healthcheck endpoint

    Scenario: Get healthcheck
        Given a valid insight health check request
        When I submit the insight health check request
        Then the insight health check result should be healthy