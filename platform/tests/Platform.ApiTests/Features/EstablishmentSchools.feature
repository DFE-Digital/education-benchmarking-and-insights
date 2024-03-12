Feature: Establishment schools endpoints

    Scenario: Sending a valid school request
        Given a valid school request with id '119376'
        When I submit the schools request
        Then the school result should be ok
        
    Scenario: Sending an invalid school request should return not found
        Given a invalid school request
        When I submit the schools request
        Then the school result should be not found

    Scenario: Sending a valid query schools request for the first page
        Given a valid schools query request with page '1'
        When I submit the schools request
        Then the schools query result should be page '1' with '10' records
        
    Scenario: Sending a valid query schools request for the second page
        Given a valid schools query request with page '2'
        When I submit the schools request
        Then the schools query result should be page '2' with '10' records
        
    Scenario: Sending a valid query schools request for page size 5
        Given a valid schools query request with page size '5'
        When I submit the schools request
        Then the schools query result should be page '1' with '5' records

    Scenario: Sending a valid search schools request
        Given a valid schools search request
        When I submit the schools request
        Then the schools search result should be ok

    Scenario: Sending a valid suggest schools request
        Given a valid schools suggest request
        When I submit the schools request
        Then the schools suggest result should be:
          | Text                                                          | Name                                                        | Urn    |
          | Burscough Bridge St John's Church of England Primary *School* | Burscough Bridge St John's Church of England Primary School | 119376 |
          | Forest Row Church of England Primary School                   | Forest Row Church of England Primary School                 | 114504 |
          | Peasmarsh Church of England Primary *School*                  | Peasmarsh Church of England Primary School                  | 114518 |
          | *School* Lane                                                 | Brogdale CIC                                                | 148656 |
          | Barrow CofE Primary *School*                                  | Barrow CofE Primary School                                  | 111270 |
          | Luckington Community *School*                                 | Luckington Community School                                 | 126200 |
          | Middle Street Primary *School*                                | Middle Street Primary School                                | 114369 |
          | Oatlands *School*, St. Marys Road                             | Oatlands School                                             | 124994 |
          | Thomas Harding Junior *School*, Fullers Hill                  | Thomas Harding Junior School                                | 145069 |
          | Oak Hill Church of England Primary School                     | Oak Hill Church of England Primary School                   | 115670 |

    Scenario: Sending an invalid suggest schools request returns the validation results
        Given an invalid schools suggest request
        When I submit the schools request
        Then the schools suggest result should have the follow validation errors:
          | PropertyName  | ErrorMessage                                 |
          | SuggesterName | 'Suggester Name' must not be empty.          |
          | SearchText    | 'Search Text' must not be empty.             |
          | Size          | 'Size' must be greater than or equal to '5'. |