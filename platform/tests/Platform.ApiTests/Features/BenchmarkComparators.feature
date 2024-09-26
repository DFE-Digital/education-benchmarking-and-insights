Feature: Benchmark Comparators Endpoint Testing

    Scenario: Sending a valid comparator schools request
        Given a valid comparator schools request
        When I submit the comparator schools request
        Then the comparator schools should return 410 Gone

    Scenario: Sending a valid comparator trusts request
        Given a valid comparator trusts request
        When I submit the comparator trusts request
        Then the comparator trusts should return 410 Gone