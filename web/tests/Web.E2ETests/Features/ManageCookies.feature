Feature: Manage cookies

    Scenario: Cookie banner is displayed on home page
        Given I am on home page
        When I have no cookies in context
        Then the cookie banner is displayed

    Scenario: Cookies may be accepted on home page
        Given I am on home page
        When I have no cookies in context
        And I click to 'accept' cookies
        Then the cookie banner is dismissed with the 'accept' message
        
    Scenario: Cookies may be rejected on home page
        Given I am on home page
        When I have no cookies in context
        When I click to 'reject' cookies
        Then the cookie banner is dismissed with the 'reject' message

    Scenario: Cookie banner is displayed on cookies page
        Given I am on cookies page
        When I have no cookies in context
        Then the cookie banner is displayed
        
    Scenario: Cookies may be accepted on cookies page using the banner
        Given I am on cookies page
        When I have no cookies in context
        And I click to 'accept' cookies
        Then the cookies saved banner is displayed
        
    Scenario: Cookies may be rejected on cookies page using the banner
        Given I am on cookies page
        When I have no cookies in context
        And I click to 'reject' cookies
        Then the cookies saved banner is displayed
        
    Scenario: Cookies may be accepted on cookies page using the form
        Given I am on cookies page
        When I have no cookies in context
        And I click to 'accept' cookies using the form
        Then the cookies saved banner is displayed
        
    Scenario: Cookies may be rejected on cookies page using the form
        Given I am on cookies page
        When I have no cookies in context
        And I click to 'reject' cookies using the form
        Then the cookies saved banner is displayed