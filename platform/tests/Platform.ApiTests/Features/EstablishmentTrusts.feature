Feature: Establishment trusts endpoints

    Scenario: Sending a valid trust request
        Given a valid trust request with id '7539918'
        When I submit the trust request
        Then the trust result should be correct

    Scenario: Sending an invalid trust request should return not found
        Given an invalid trust request with id '99999999'
        When I submit the trust request
        Then the trust result should be not found

    Scenario: Sending a valid suggest trust request with exact code
        Given a valid trust suggest request with searchText '7539918'
        When I submit the trust request
        Then the trust suggest result should be correct
        
    Scenario: Sending a valid suggest trust request with partial name
        Given a valid trust suggest request with searchText 'test'
        When I submit the trust request
        Then the trust suggest result should be:
          | Text                         | TrustName                  | CompanyNumber |    
          | *Test* Company/Trust  334    | Test Company/Trust  334    | 10038640      |
          | *Test* Company/Trust  262    | Test Company/Trust  262    | 10264735      |
          | *Test* Company/Trust  301    | Test Company/Trust  301    | 4464331       |
          | *Test* Company/Trust  56     | Test Company/Trust  56     | 6897239       |
          | *Test* Company/Trust  286    | Test Company/Trust  286    | 7465701       |
          
    Scenario: Sending a valid suggest trust request
        Given a valid trust suggest request with searchText 'willNotBeFound'
        When I submit the trust request
        Then the trust suggest result should be empty
    
    Scenario: Sending an invalid suggest trust request returns the validation results
        Given an invalid trust suggest request
        When I submit the trust request
        Then the trust suggest result should have the follow validation errors:
          | PropertyName  | ErrorMessage                                 |
          | SuggesterName | 'Suggester Name' must not be empty.          |
          | SearchText    | 'Search Text' must not be empty.             |
          | Size          | 'Size' must be greater than or equal to '5'. |