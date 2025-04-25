Feature: Home page

    Scenario: Go to find organisation
        Given I am on home page
        When I click Start now button
        Then the find organisation page is disabled

    Scenario: Canonical link is in response headers
        Given I am on home page
        Then the canonical link should be present in headers