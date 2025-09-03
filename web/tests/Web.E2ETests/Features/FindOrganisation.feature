Feature: Find organisation

    Scenario: Go to school search page
        Given I am on find organisation page
        And 'school' organisation type is selected
        When I click Continue to school search
        Then the school search page is displayed