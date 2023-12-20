Feature: BenchmarkApi
API Endpoint Testing

	Scenario: Retrieve School Comparator Set Successfully
		Given the School Comparator Set API is running
		When I send a POST request to get comparators with the following data:
		  | requestJson | {"includeSet": true, "size": 7} |
		  | requestJson | {"includeSet": true, "size": 7} |
		Then the response status code for benchmark api should be 200
		And the response body should contain a valid School Comparator Set
		And the response size should be '7'

