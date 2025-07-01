Feature: Content banners endpoints

    Scenario: Getting the current active banner for a given existing target when multiple active
        Given a banners request for the target 'target-1'
        When I submit the banners request
        Then the result should be ok and equal:
          | Field   | Value     |
          | Title   | Title 4   |
          | Heading | Heading 4 |
          | Body    | Body 4    |

    Scenario: Getting the current active banner for an outdated but existing target
        Given a banners request for the target '<target>'
        When I submit the banners request
        Then the result should be not found

    Examples:
      | target   |
      | target-2 |
      | target-3 |

    Scenario: Getting the current active banner for a given existing target in array
        Given a banners request for the target '<target>'
        When I submit the banners request
        Then the result should be ok and equal:
          | Field   | Value     |
          | Title   | Title 7   |
          | Heading | Heading 7 |
          | Body    | Body 7    |

    Examples:
      | target   |
      | target-4 |
      | target-5 |
      | target-6 |

    Scenario: Getting the current active banner for a given non-existent target
        Given a banners request for the target 'target-99'
        When I submit the banners request
        Then the result should be not found

    Scenario: Sending a valid banners request for unsupported API version
        Given a banners request with API version 'invalid' for the target 'target-1'
        When I submit the banners request
        Then the result should be bad request