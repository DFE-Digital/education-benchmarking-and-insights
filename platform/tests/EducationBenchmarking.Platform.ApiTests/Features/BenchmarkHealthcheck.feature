Feature: Benchmark healthcheck endpoint

    Scenario: Get healthcheck
        Given a valid benchmark health check request
        When I submit the benchmark health check request
        Then the benchmark health check result should be healthy