Feature: BenchmarkApi
API Endpoint Testing

    Scenario: Retrieve School Comparator Set Successfully
        Given the Benchmark API is running
        When I send a POST request to get comparators with the following payload:
          | Type        | Body                            |
          | requestJson | {"includeSet": true, "size": 7} |
        Then the response status code for benchmark api should be 200
        And the response body should contain a valid School Comparator Set
        And the response size should be '7'
        
    Scenario: Receive 500 Bad Request for Invalid School Comparator Set Request
        Given the Benchmark API is running
        When I send a POST request to get comparators with the following payload:
          | Type        | Body                                         |
          | requestJson | {"includeSet": true, "size": "invalid-size"} |
        Then the response status code for benchmark api should be 500
        
    Scenario: Retrieve Trusts Comparator Set Successfully
        Given the Benchmark API is running
        When I send a POST request to get trusts comparators with the following payload:
          | Type        | Body                            |
          | requestJson | {"includeSet": true, "size": 8} |
        Then the response status code for benchmark api should be 200
        And the response body should contain a valid trusts Comparator Set
        And the response size should be '8'
           
    Scenario: Receive 500 Bad Request for Invalid Trusts Comparator Set Request
        Given the Benchmark API is running
        When I send a POST request to get trusts comparators with the following payload:
          | Type        | Body                                         |
          | requestJson | {"includeSet": true, "size": "invalid-size"} |
        Then the response status code for benchmark api should be 500
  