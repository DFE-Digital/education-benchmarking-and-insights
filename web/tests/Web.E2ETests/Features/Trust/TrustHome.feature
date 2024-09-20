Feature: Trust homepage

    Scenario: Go to compare your costs page
        Given I am on trust homepage for trust with company number '10074054'
        When I click on compare your costs
        Then the compare your costs page is displayed