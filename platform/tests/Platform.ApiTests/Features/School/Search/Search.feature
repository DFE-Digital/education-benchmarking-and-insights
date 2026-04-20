Feature: School Search

Scenario Outline: Search returns 200 OK
  Given a valid school search request with '<Scenario>'
  When I submit the school search request
  Then the response status code is 200 OK
  And the school search response body matches the expected JSON

  Examples:
    | Scenario                   |
    | default                    |
    | pagination                 |
    | order-by                   |
    | filter-phase               |

Scenario Outline: Search returns 400 Bad Request for validation errors
  Given an invalid school search request with '<Scenario>'
  When I submit the school search request
  Then the response status code is 400 Bad Request
  And the school search response body matches the expected validation errors JSON

  Examples:
    | Scenario                    |
    | missing-search-text         |
    | short-search-text           |
    | long-search-text            |
    | invalid-order-by-field      |
    | invalid-order-by-value      |
    | invalid-filter-field        |
    | invalid-filter-value        |

Scenario: Search returns 400 Bad Request for unsupported API version
  Given a valid school search request with 'default'
  When I submit the school search request with an unsupported API version
  Then the response status code is 400 Bad Request
  And the response body is a standard problem details JSON

Scenario Outline: Suggest returns 200 OK
  Given a valid school suggest request with '<Scenario>'
  When I submit the school suggest request
  Then the response status code is 200 OK
  And the school suggest response body matches the expected JSON

  Examples:
    | Scenario                   |
    | default                    |
    | valid-size                 |
    | valid-exclude              |
    | exclude-missing-financial  |

Scenario Outline: Suggest returns 400 Bad Request for validation errors
  Given an invalid school suggest request with '<Scenario>'
  When I submit the school suggest request
  Then the response status code is 400 Bad Request
  And the school suggest response body matches the expected validation errors JSON

  Examples:
    | Scenario                    |
    | missing-search-text         |
    | short-search-text           |
    | long-search-text            |
    | invalid-size                |

Scenario: Suggest returns 400 Bad Request for unsupported API version
  Given a valid school suggest request with 'default'
  When I submit the school suggest request with an unsupported API version
  Then the response status code is 400 Bad Request
  And the response body is a standard problem details JSON
