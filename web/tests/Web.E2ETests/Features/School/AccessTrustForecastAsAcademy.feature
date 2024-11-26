Feature: School access to parent's Trust Forecast
       
    Background: 
        Given I am on the service home
        And I have signed in with organisation '01: FBIT TEST - Community School (Open)'
    
    @ignore    
    Scenario: Go to trust forecast for this school's parent trust
        Given I am on trust homepage for trust with company number '8104190'
        When I click on trust forecast
        Then the trust forecast page is displayed
        
    @ignore
    Scenario: Go to trust forecast for a trust that is not this school's parent
        Given I am on trust homepage for trust with company number '8104190'
        When I click on trust forecast
        Then the forbidden page is displayed