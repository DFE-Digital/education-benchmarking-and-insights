Feature: Content news endpoints

    Scenario: Getting the published news article for a given existing slug when multiple active
        Given a news article request for the slug '<slug>'
        When I submit the news request
        Then the response should be ok, contain a JSON object and match the expected output of '<output>'

    Examples:
      | slug             | output                                |
      | published        | ContentNewsArticlePublished.json      |
      | not-yet-archived | ContentNewsArticleNotYetArchived.json |

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

    Scenario: Getting the published news articles
        Given a news request
        When I submit the news request
        Then the results should be ok and equal:
          | Title            | Slug             | Published                         |
          | Not yet archived | not-yet-archived | 2025-08-28T14:46:10.3266667+00:00 |
          | Published        | published        | 2025-08-28T14:45:27.5866667+00:00 |

    Scenario: Sending a valid news request for unsupported API version
        Given a news request with API version 'invalid'
        When I submit the news request
        Then the result should be bad request