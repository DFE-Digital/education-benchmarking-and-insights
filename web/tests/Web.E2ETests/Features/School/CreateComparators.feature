Feature: School create comparator set

    Background:
        Given I am on create comparators page for school with URN '777042'       
        And I have signed in with organisation '01: FBIT TEST - Community School (Open)'

    Scenario: Can view create comparator set page
        When I click continue
        Then the create comparators by page is displayed

    Scenario: Can move to create comparator set by characteristic page
        When I click continue
        And I select the option By Characteristic and click continue
        Then the create comparators by characteristic page is displayed

    Scenario: Can move to create comparator set by name page
        When I click continue
        And I select the option By Name and click continue
        Then the create comparators by name page is displayed

    Scenario: Can create comparator set by name
        When I click continue
        And I select the option By Name and click continue
        And I select the school with urn '777045' from suggester
        And I click the choose school button
        And I click the create set button
        Then the school home page is displayed