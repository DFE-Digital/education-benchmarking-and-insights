Feature: API Endpoint Testing

   Scenario: Verify successful API request
        Given the Api is running
        When I send a GET request to the "api/schools/expenditure" endpoint
        Then the response status code should be 200