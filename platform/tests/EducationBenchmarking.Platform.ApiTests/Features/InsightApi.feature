Feature: API Endpoint Testing

    Scenario: Verify successful API request
        Given the insight Api is running 
        When I send the expenditure get request to the API
        Then the response status code should be 200
        And I should get a response body
