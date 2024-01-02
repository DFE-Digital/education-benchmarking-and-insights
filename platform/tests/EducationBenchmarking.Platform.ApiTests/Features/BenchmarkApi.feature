Feature: BenchmarkApi API Endpoint Testing
        Scenario: Create a comparator set successfully
            Given I have a valid comparator set request of size set to '7'
            When I submit the request
            Then a valid comparator set of size '7' should be returned
            And the response status code api is 200

    Scenario: Get error for Invalid School Comparator Set Request
        Given I have a invalid comparator set request of size set to 'invalid'
        When I submit the request
        Then the response status code api is 500
