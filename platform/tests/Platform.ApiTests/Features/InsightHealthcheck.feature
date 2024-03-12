Feature: Insight healthcheck endpoint

    Scenario: Sending a valid healthcheck request
        Given a valid insight health check request
        When I submit the insight health check request
        Then the insight health check result should be healthy