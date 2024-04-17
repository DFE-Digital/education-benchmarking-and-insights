Feature: Find organisation

    Scenario: Goto school homepage
        Given I am on find organisation page
        And 'school' organisation type is selected
        When I select the school with urn '118167' from suggester
        Then the school homepage is displayed