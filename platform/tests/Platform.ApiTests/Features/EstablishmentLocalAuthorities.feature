Feature: Establishment local authorities endpoints

    Scenario: Sending a valid local authority request
        Given a valid local authority request with id '201'
        When I submit the local authorities request
        Then the local authority result should be correct

    Scenario: Sending an invalid local authority request should return not found
        Given an invalid local authority request with id '999'
        When I submit the local authorities request
        Then the local authority result should be not found

    Scenario: Sending a valid suggest local authorities request with exact code
        Given a valid local authorities suggest request with searchText '201'
        When I submit the local authorities request
        Then the local authorities suggest result should be correct
        
    Scenario: Sending a valid suggest local authorities request with partial name
        Given a valid local authorities suggest request with searchText 'of'
        When I submit the local authorities request
        Then the local authorities suggest result should be:
          | Text                          | Name                        | Code |    
          | East Riding *of* Yorkshire    | East Riding of Yorkshire    | 811  |
          | Isles *of* Scilly             | Isles of Scilly             | 420  |
          | City *of* London              | City of London              | 201  |
          | Isle *of* Wight               | Isle of Wight               | 921  |
          
    Scenario: Sending a valid suggest local authorities request
        Given a valid local authorities suggest request with searchText 'willNotBeFound'
        When I submit the local authorities request
        Then the local authorities suggest result should be empty
    
    Scenario: Sending an invalid suggest local authorities request returns the validation results
        Given an invalid local authorities suggest request
        When I submit the local authorities request
        Then the local authorities suggest result should have the follow validation errors:
          | PropertyName  | ErrorMessage                                 |
          | SuggesterName | 'Suggester Name' must not be empty.          |
          | SearchText    | 'Search Text' must not be empty.             |
          | Size          | 'Size' must be greater than or equal to '5'. |