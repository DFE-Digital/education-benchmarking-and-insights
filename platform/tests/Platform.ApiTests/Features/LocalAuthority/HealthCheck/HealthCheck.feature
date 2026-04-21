Feature: Local Authority Health Check

    @healthcheck
    Scenario Outline: Sending a valid healthcheck request
        Given a valid request
        When I submit the request
        Then the result should be '<Status>'

    Examples:
      | Status  |
      | Healthy |
