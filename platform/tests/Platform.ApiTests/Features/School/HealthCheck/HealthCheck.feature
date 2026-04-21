Feature: School HealthCheck

    @healthcheck
    Scenario: Request health status successfully returns the health status
        Given a valid request
        When I submit the request
        Then the result should be 'Healthy'