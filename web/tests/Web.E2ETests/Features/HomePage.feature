Feature: Home page

    Scenario: Go to find organisation
        Given I am on home page
        When I click Start now button
        Then the find organisation page is disabled

    Scenario: Service banner is displayed
        Given I am on home page
        Then the service banner displays the title 'Home page', heading 'Banner on home page' and body 'This banner has been configured on the automated test environment for the home page only'