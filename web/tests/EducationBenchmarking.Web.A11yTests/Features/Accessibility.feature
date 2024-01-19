Feature: Accessibility Audit

    Scenario: Check the accessibility of Service Landing Page
        Given I am on the Service Landing Page
        When I check the accessibility of the page
        Then there are no accessibility issues

    Scenario: Check the accessibility of Choose your School Page
        Given I am on the Choose your School Page
        When I check the accessibility of the page
        Then there are no accessibility issues

    Scenario: Check the accessibility of School Homepage
        Given I am on the school "139696" Home Page
        When I check the accessibility of the page
        Then there are no accessibility issues

    Scenario: Check the accessibility of compare your costs page in chart view
        Given I am on compare your costs page for school '139696'
        When I check the accessibility of the page
        Then there are no accessibility issues
        
    Scenario: Check the accessibility of compare your costs page in table view
        Given I am on compare your costs page for school '139696'
        And I click on view as table
        When I check the accessibility of the page
        Then there are no accessibility issues

    Scenario: Check the accessibility of school workforce page in chart view
        Given I am on school workforce page for school '139696'
        When I check the accessibility of the page
        Then there are no accessibility issues
        
    Scenario: Check the accessibility of school workforce page in table view
        Given I am on school workforce page for school '139696'
        And I click on view as table
        When I check the accessibility of the page
        Then there are no accessibility issues