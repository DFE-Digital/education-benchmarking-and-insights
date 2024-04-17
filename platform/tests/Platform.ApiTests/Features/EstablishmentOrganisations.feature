@ignore
Feature: Establishment organisations endpoints

    Scenario: Sending a valid suggest organisations request
        Given a valid organisations suggest request
        When I submit the organisations request
        Then the organisations suggest result should be:
          | Text                                                          | Identifier | Kind   |
          | Forest Row Church of England Primary School                   | 114504     | school |
          | *School* Lane                                                 | 148656     | school |
          | Burscough Bridge St John's Church of England Primary *School* | 119376     | school |
          | Thomas Harding Junior *School*, Fullers Hill                  | 145069     | school |
          | Peasmarsh Church of England Primary *School*                  | 114518     | school |
          | Oak Hill Church of England Primary School                     | 115670     | school |
          | Green Dragon Primary *School*                                 | 132266     | school |
          | Middle Street Primary *School*                                | 114369     | school |
          | Barrow CofE Primary *School*                                  | 111270     | school |
          | Luckington Community *School*                                 | 126200     | school |

    Scenario: Sending an invalid suggest organisations request returns the validation results
        Given an invalid organisations suggest request
        When I submit the organisations request
        Then the organisations suggest result should have the follow validation errors:
          | PropertyName  | ErrorMessage                                 |
          | SuggesterName | 'Suggester Name' must not be empty.          |
          | SearchText    | 'Search Text' must not be empty.             |
          | Size          | 'Size' must be greater than or equal to '5'. |