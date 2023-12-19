Feature: Accessibility Audit

    Scenario: Check the accessibility of landing page
        Given I am on the LandingPage
        When I check the accessibility of the page
        Then there are no accessibility issues

    Scenario: Check the accessibility of choose school page
        Given I am on the ChooseSchoolPage
        When I check the accessibility of the page
        Then there are no accessibility issues

    Scenario: Check the accessibility of benchmarking page
        Given I am on the BenchmarkingPage
        When I check the accessibility of the page
        Then there are no accessibility issues