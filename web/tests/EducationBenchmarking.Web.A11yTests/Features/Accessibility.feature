Feature: Accessibility Audit

    Scenario: Check the accessibility of landing page
        Given I open the page with the url https://s198d01-education-benchmarking.azurewebsites.net/
        When I check the accessibility of the page
        Then there are no accessibility issues

    Scenario: Check the accessibility of choose school page
        Given I open the page with the url https://s198d01-education-benchmarking.azurewebsites.net/choose-school
        When I check the accessibility of the page
        Then there are no accessibility issues

    Scenario: Check the accessibility of benchmarking page
        Given I open the page with the url https://s198d01-education-benchmarking.azurewebsites.net/school/142205
        When I check the accessibility of the page
        Then there are no accessibility issues