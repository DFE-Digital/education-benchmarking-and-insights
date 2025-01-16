Feature: Trust create comparator set

    Background:
        Given I am on the service home
        And I have signed in with organisation '010: FBIT TEST - Multi-Academy Trust (Open)'
        And I am on compare by page for trust with company number '00000001'

    Scenario: Can view create comparator set page
        Then the create comparators by page is displayed

    Scenario: Can move to create comparator set by characteristic page
        When I select the option By Characteristic and click continue
        Then the create comparators by characteristic page is displayed

    Scenario: Can move to create comparator set by name page
        When I select the option By Name and click continue
        Then the create comparators by name page is displayed

    Scenario: Can create comparator set by name
        When I select the option By Name and click continue
        And I select the trust with urn '10074054' from suggester
        And I click the choose trust button
        And I click the create set button
        Then the trust benchmark spending page is displayed