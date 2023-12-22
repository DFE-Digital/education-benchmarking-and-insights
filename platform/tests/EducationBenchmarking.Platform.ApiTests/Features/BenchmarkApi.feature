Feature: BenchmarkApi
API Endpoint Testing

    Scenario: Create School Comparator Set Successfully
        Given I want to create a comparator set
        When I send a request to get school comparators with includeset set to true and size set to '7'
        Then a valid school comparator set of size '7' should be returned
        And the response status code api is 200

    Scenario: Receive 500 Bad Request for Invalid School Comparator Set Request
        Given I want to create a comparator set
        When I send a request to get school comparators with includeset set to true and size set to 'invalid-size'
        Then the response status code api is 500

    Scenario: Create Trusts Comparator Set Successfully

        Given I want to create a comparator set
        When I send a request to get trust comparators with includeset set to true and size set to '8'
        Then a valid trust comparator set of size '8' should be returned
        And the response status code api is 200

    Scenario: Create 500 Bad Request for Invalid Trusts Comparator Set Request
        Given I want to create a comparator set
        When I send a request to get trust comparators with includeset set to true and size set to 'invalid'
        Then the response status code api is 500