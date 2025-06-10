Feature: Local authority finances healthcheck endpoint

    Scenario: Sending a valid healthcheck request
        Given a valid local authority finances health check request
        When I submit the local authority finances health check request
        Then the local authority finances health check result should be healthy