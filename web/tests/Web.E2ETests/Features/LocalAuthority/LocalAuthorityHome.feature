Feature: Local Authority homepage

    Scenario: Go to compare your costs page
        Given I am on local authority homepage for local authority with code '204'
        When I click on compare your costs
        Then the compare your costs page is displayed