Feature: Local Authority access to School curriculum and financial planning for its schools.

    Background:
        Given I am on the service home
        And I have signed in with organisation '002: FBIT TEST - Local Authority'

    Scenario: Go to curriculum and financial planning page for a school in this local authority
        Given I am on school homepage for school with urn '990033'
        When I click on curriculum and financial planning
        Then the curriculum and financial planning page is displayed

    Scenario: Go to curriculum and financial planning page for a school not in this local authority
        Given I am on school homepage for school with urn '777046'
        When I click on curriculum and financial planning
        Then the forbidden page is displayed