Feature: Establishment trusts endpoints

    Scenario: Sending a valid trust request
        Given a valid trust request with id '10647453'
        When I submit the trust request
        Then the trust result should be ok and have the following values:
          | Field         | Value                   |
          | CompanyNumber | 10647453                |
          | TrustName     | Test Company/Trust  227 |
        And the trust result should contain the following schools:
          | URN    | SchoolName              | OverallPhase |
          | 990639 | Test academy school 453 | 16 plus      |

    Scenario: Sending an invalid trust request should return not found
        Given an invalid trust request with id '99999999'
        When I submit the trust request
        Then the trust result should be not found

    Scenario: Sending a valid suggest trust request with exact code
        Given a valid trust suggest request with searchText '7539918'
        When I submit the trust request
        Then the trust suggest result should be ok and have the following values:
          | Field         | Value                 |
          | Text          | *7539918*             |
          | CompanyNumber | 7539918               |
          | TrustName     | Test Company/Trust  1 |

    Scenario: Sending a valid suggest trust request with partial name
        Given a valid trust suggest request with searchText 'test'
        When I submit the trust request
        Then the trust suggest result should be ok and have the following multiple values:
          | Text                      | TrustName               | CompanyNumber |
          | *Test* Company/Trust  334 | Test Company/Trust  334 | 10038640      |
          | *Test* Company/Trust  262 | Test Company/Trust  262 | 10264735      |
          | *Test* Company/Trust  301 | Test Company/Trust  301 | 4464331       |
          | *Test* Company/Trust  56  | Test Company/Trust  56  | 6897239       |
          | *Test* Company/Trust  286 | Test Company/Trust  286 | 7465701       |

    Scenario: Sending a valid suggest trust request
        Given a valid trust suggest request with searchText 'willNotBeFound'
        When I submit the trust request
        Then the trust suggest result should be empty

    Scenario: Sending an invalid suggest trust request returns the validation results
        Given an invalid trust suggest request with '<SearchText>' and '<Size>'
        When I submit the trust request
        Then the trust suggest result should be bad request and have the following validation errors:
          | PropertyName | ErrorMessage             |
          | SearchText   | <SearchTextErrorMessage> |
          | Size         | <SizeErrorMessage>       |

    Examples:
      | SuggesterName | SearchText                                                                                                    | Size | SuggesterNameErrorMessage | SearchTextErrorMessage                                                                   | SizeErrorMessage |
      | suggester     | te                                                                                                            | 5    |                           | The length of 'Search Text' must be at least 3 characters. You entered 2 characters.     |                  |
      | suggester     | 0123456789_0123456789_0123456789_0123456789_0123456789_0123456789_0123456789_0123456789_0123456789_0123456789 | 5    |                           | The length of 'Search Text' must be 100 characters or fewer. You entered 109 characters. |                  |

    Scenario: Sending a valid search trusts request with company number
        Given a valid trusts search request with searchText '10074054' page '1' size '5'
        When I submit the trust request
        Then the search trusts response should be ok and have the following values:
          | TotalResults | Page | PageSize | PageCount |
          | 1            | 1    | 5        | 1         |
        And the results should include the following trusts:
          | CompanyNumber | TrustName               | TotalPupils | SchoolsInTrust |
          | 10074054      | Test Company/Trust  108 | 1854        | 2              |

    Scenario: Sending a valid search trusts request with search text
        Given a valid trusts search request with searchText 'Test' page '1' size '5'
        When I submit the trust request
        Then the search trusts response should be ok and have the following values:
          | TotalResults | Page | PageSize | PageCount |
          | 368          | 1    | 5        | 74        |
        And the results should include the following trusts:
          | CompanyNumber | TrustName               | TotalPupils | SchoolsInTrust |
          | 10038640      | Test Company/Trust  334 | 1207        | 1              |
          | 10264735      | Test Company/Trust  262 | 468         | 1              |
          | 4464331       | Test Company/Trust  301 | 448         | 1              |
          | 6897239       | Test Company/Trust  56  | 518         | 1              |
          | 7465701       | Test Company/Trust  286 | 207         | 1              |

    Scenario: Sending a valid search trusts request with order by ascending
        Given a valid trusts search request with searchText 'Test' page '1' size '5' orderByField 'TrustNameSortable' orderByValue 'asc'
        When I submit the trust request
        Then the search trusts response should be ok and have the following values:
          | TotalResults | Page | PageSize | PageCount |
          | 368          | 1    | 5        | 74        |
        And the results should include the following trusts:
          | CompanyNumber | TrustName               | TotalPupils | SchoolsInTrust |
          | 7539918       | Test Company/Trust  1   |             |                |
          | 7353824       | Test Company/Trust  10  | 791         | 1              |
          | 8599329       | Test Company/Trust  102 | 330         | 1              |
          | 9187505       | Test Company/Trust  103 | 318         | 1              |
          | 8341194       | Test Company/Trust  104 | 424         | 1              |

    Scenario: Sending a valid search trusts request with order by descending
        Given a valid trusts search request with searchText 'Test' page '1' size '5' orderByField 'TrustNameSortable' orderByValue 'desc'
        When I submit the trust request
        Then the search trusts response should be ok and have the following values:
          | TotalResults | Page | PageSize | PageCount |
          | 368          | 1    | 5        | 74        |
        And the results should include the following trusts:
          | CompanyNumber | TrustName              | TotalPupils | SchoolsInTrust |
          | 8833418       | Test Company/Trust  99 |             |                |
          | 9662303       | Test Company/Trust  97 | 588         | 1              |
          | 8028084       | Test Company/Trust  96 |             |                |
          | 8010464       | Test Company/Trust  95 | 651         | 1              |
          | 9918358       | Test Company/Trust  94 | 482         | 1              |

    Scenario: Sending a valid search trusts request
        Given a valid trusts search request with searchText 'willNotBeFound' page '1' size '5'
        When I submit the trust request
        Then the trusts search result should be empty

    Scenario: Sending an invalid search trusts request
        Given an invalid trusts search request
        When I submit the trust request
        Then the search trusts response should be bad request containing validation errors