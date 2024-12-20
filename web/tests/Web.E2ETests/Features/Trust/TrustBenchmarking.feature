Feature: Trust Benchmarking

    Background:
        Given I am on trust homepage for trust with company number '00000001'
        And I have signed in with organisation '010: FBIT TEST - Multi-Academy Trust (Open)'
        And I am on create comparators page for trust with company number '00000001'

    Scenario: Can create comparator set by name
        When I select the option By Name and continue
        And I select the trust with company number '10074054' from suggester
        And I click the choose trust button
        And I click the create set button
        Then the trust benchmark spending page is displayed
        
    

   