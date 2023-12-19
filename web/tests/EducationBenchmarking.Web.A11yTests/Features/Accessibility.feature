Feature: Accessibility Audit

    Scenario: Check the accessibility of Service Landing Page
        Given I am on the Service Landing Page
        When I check the accessibility of the page
        Then there are no accessibility issues

    Scenario: Check the accessibility of Choose your School Page
        Given I am on the Choose your School Page
        When I check the accessibility of the page
        Then there are no accessibility issues

    Scenario: Check the accessibility of School Homepage and compare your school
        Given I am on the school "142205" Home Page  
        When I check the accessibility of the page
        Then there are no accessibility issues