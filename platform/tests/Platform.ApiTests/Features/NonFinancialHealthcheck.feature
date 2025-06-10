Feature: Non financial healthcheck endpoint

    Scenario: Sending a valid healthcheck request
        Given a valid non financial health check request
        When I submit the non financial health check request
        Then the non financial health check result should be healthy