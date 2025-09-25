Feature: Trust benchmark IT spending

    Background:
        Given I am on the service home
        And I have signed in with organisation '010: FBIT TEST - Multi-Academy Trust (Open)'
        And I have no previous comparators selected for company number '10074054'
        And I am on IT spend for trust with company number '10074054'
        When I select the option By Name and click continue
        And I select the trust with company number '00000001' from suggester
        And I click the choose trust button
        And I select the trust with company number '08076374' from suggester
        And I click the choose trust button
        And I click the create set button

    Scenario: Can view IT spending
        Then the IT spend for trust page for company number '10074054' is displayed
        
    Scenario: Can view comparator trusts
        When I click on view comparator set
        Then the comparator set page is displayed