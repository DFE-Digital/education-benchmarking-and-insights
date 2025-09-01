Feature: Content news endpoints

    Scenario: Getting the published news article for a given existing slug when multiple active
        Given a news article request for the slug '<slug>'
        When I submit the news request
        Then the result should be ok and equal:
          | Field | Value   |
          | Title | <title> |
          | Slug  | <slug>  |
          | Body  | <body>  |

    Examples:
      | slug             | title            | body                    |
      | published        | Published        | # Published body        |
      | not-yet-archived | Not yet archived | # Not yet archived body |

    Scenario: Getting the published news article for an outdated but existing slug
        Given a news article request for the slug '<slug>'
        When I submit the news request
        Then the result should be not found

    Examples:
      | slug              |
      | archived          |
      | not-yet-published |
      | unpublished       |

    Scenario: Getting the published news article for a given non-existent slug
        Given a news article request for the slug 'invalid'
        When I submit the news request
        Then the result should be not found

    Scenario: Sending a valid news article request for unsupported API version
        Given a news article request with API version 'invalid' for the slug 'published'
        When I submit the news request
        Then the result should be bad request