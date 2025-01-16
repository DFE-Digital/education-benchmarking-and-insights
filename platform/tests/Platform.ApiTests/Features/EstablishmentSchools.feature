Feature: Establishment schools endpoints

    Scenario: Sending a valid school request
        Given a valid school request with id '777042'
        When I submit the schools request
        Then the school result should be ok and have the following values:
          | Field      | Value           |
          | URN        | 777042          |
          | SchoolName | Test school 102 |

    Scenario: Sending an invalid school request should return not found
        Given an invalid school request with id '999999'
        When I submit the schools request
        Then the school result should be not found

    Scenario: Sending a valid suggest schools request with exact URN
        Given a valid schools suggest request with searchText '777042'
        When I submit the schools request
        Then the school suggest result should be ok and have the following values:
          | Field      | Value           |
          | Text       | *777042*        |
          | URN        | 777042          |
          | SchoolName | Test school 102 |

    Scenario: Sending a valid suggest schools request with partial name
        Given a valid schools suggest request with searchText 'test'
        When I submit the schools request
        Then the schools suggest result should be ok and have the following multiple values:
          | Text                     | SchoolName             | URN    |
          | *Test* academy school 92 | Test academy school 92 | 777054 |
          | *Test* school 249        | Test school 249        | 990007 |
          | *Test* school 68         | Test school 68         | 990014 |
          | *Test* school 219        | Test school 219        | 990018 |
          | *Test* school 26         | Test school 26         | 990031 |

    Scenario: Sending a valid suggest schools request
        Given a valid schools suggest request with searchText 'willNotBeFound'
        When I submit the schools request
        Then the schools suggest result should be empty

    Scenario: Sending an invalid suggest schools request returns the validation results
        Given an invalid schools suggest request with '<SearchText>' and '<Size>'
        When I submit the schools request
        Then the schools suggest result should be bad request and have the following validation errors:
          | PropertyName  | ErrorMessage                |
          | SearchText    | <SearchTextErrorMessage>    |
          | Size          | <SizeErrorMessage>          |

    Examples:
      | SuggesterName | SearchText                                                                                                    | Size | SuggesterNameErrorMessage           | SearchTextErrorMessage                                                                   | SizeErrorMessage                             |
      | suggester     | te                                                                                                            | 5    |                                     | The length of 'Search Text' must be at least 3 characters. You entered 2 characters.     |                                              |
      | suggester     | 0123456789_0123456789_0123456789_0123456789_0123456789_0123456789_0123456789_0123456789_0123456789_0123456789 | 5    |                                     | The length of 'Search Text' must be 100 characters or fewer. You entered 109 characters. |                                              |