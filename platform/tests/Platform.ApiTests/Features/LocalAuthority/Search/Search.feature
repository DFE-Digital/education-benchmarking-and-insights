Feature: Local Authority Search

    Scenario Outline: Sending a valid suggest local authorities request returns ok
        Given a valid local authorities suggest request with searchText '<SearchText>'
        When I submit the search request
        Then the local authorities suggest result should be ok and match the expected output in '<Result>'

    Examples:
      | SearchText     | Result                                      |
      | 201            | LaSuggestExactCode.json                     |
      | and            | LaSuggestPartialName.json                   |
      | willNotBeFound | LaSuggestNoResults.json                     |

    Scenario Outline: Sending an invalid suggest local authorities request returns bad request
        Given an invalid local authorities suggest request with searchText '<SearchText>' and size <Size>
        When I submit the search request
        Then the local authorities suggest result should be bad request and match the expected output in '<Result>'

    Examples:
      | SearchText                                                                                            | Size | Result                                      |
      | te                                                                                                    | 5    | LaSuggestInvalidSearchTextTooShort.json     |
      | 0123456789_0123456789_0123456789_0123456789_0123456789_0123456789_0123456789_0123456789_0123456789_01 | 5    | LaSuggestInvalidSearchTextTooLong.json      |
      | test                                                                                                  | 4    | LaSuggestInvalidSize.json                   |

    Scenario Outline: Sending a valid search local authorities request returns ok
        Given a valid local authorities search request with searchText '<SearchText>' page <Page> size <Size> orderByField '<OrderByField>' orderByValue '<OrderByValue>'
        When I submit the search request
        Then the search local authorities response should be ok and match the expected output in '<Result>'

    Examples:
      | SearchText     | Page | Size | OrderByField               | OrderByValue | Result                              |
      | 201            | 1    | 5    |                            |              | LaSearchExactCode.json              |
      | and            | 1    | 5    |                            |              | LaSearchPartialName.json            |
      | and            | 1    | 5    | LocalAuthorityNameSortable | asc          | LaSearchAscending.json              |
      | and            | 1    | 5    | LocalAuthorityNameSortable | desc         | LaSearchDescending.json             |
      | willNotBeFound | 1    | 5    |                            |              | LaSearchNoResults.json              |

    Scenario Outline: Sending an invalid search local authorities request returns bad request
        Given an invalid local authorities search request with searchText '<SearchText>' page 1 size 5 orderByField '<OrderByField>' orderByValue '<OrderByValue>'
        When I submit the search request
        Then the search local authorities response should be bad request and match the expected output in '<Result>'

    Examples:
      | SearchText                                                                                            | OrderByField               | OrderByValue | Result                                     |
      | te                                                                                                    | LocalAuthorityNameSortable | asc          | LaSearchInvalidSearchTextTooShort.json     |
      | 0123456789_0123456789_0123456789_0123456789_0123456789_0123456789_0123456789_0123456789_0123456789_01 | LocalAuthorityNameSortable | asc          | LaSearchInvalidSearchTextTooLong.json      |
      | test                                                                                                  | InvalidField               | asc          | LaSearchInvalidOrderByField.json           |
      | test                                                                                                  | LocalAuthorityNameSortable | invalid      | LaSearchInvalidOrderByValue.json           |
