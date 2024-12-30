Feature: School access to parent's Trust Forecast

    Background:
        Given I am on the service home
        And I am not logged in

    Scenario: Go to trust forecast for this school's parent trust
        Given I have signed in with organisation '32: FBIT TEST - Special Post 16 Institution (Open)'
        And I am on trust homepage for trust with company number '00000001'
        When I click on trust forecast
        Then the trust forecast page is displayed

    Scenario: Go to trust forecast for a trust that is not this school's parent
        Given I have signed in with organisation '01: FBIT TEST - Community School (Open)'
        And I am on trust homepage for trust with company number '00000001'
        When I click on trust forecast
        Then the forbidden page is displayed