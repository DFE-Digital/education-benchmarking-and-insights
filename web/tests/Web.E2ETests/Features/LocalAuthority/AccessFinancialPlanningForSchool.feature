Feature: Local Authority access to School curriculum and financial planning for its schools.
    
    Background: 
        Given I am on the service home
        And I have signed in with organisation '01: FBIT TEST - Community School (Open)'
    
    @ignore    
    Scenario: Go to curriculum and financial planning page for a school in this local authority
        Given I am on school homepage for school with urn '990104'
        When I click on curriculum and financial planning
        Then the curriculum and financial planning page is displayed
    
    @ignore    
    Scenario: Go to curriculum and financial planning page for a school not in this local authority
        Given I am on school homepage for school with urn '990104'
        When I click on curriculum and financial planning
        Then the forbidden page is displayed