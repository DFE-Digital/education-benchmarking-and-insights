Feature: Trust search page

    Scenario: Go to trust homepage using mouse
        Given I am on trust search page
        When I type '10038640' into the search bar
        And I click Search after selecting a result
        Then the trust homepage is displayed

    Scenario: Go to trust homepage using keyboard
        Given I am on trust search page
        When I type '10038640' into the search bar
        And I press the enter key after selecting a result
        Then the trust homepage is displayed

    Scenario: Displaying relevant search suggestions when entering a keyword
        Given I am on trust search page
        When I type '<keyword>' into the search bar
        Then each suggester result contains '<keyword>'

    Examples:
      | keyword |
      | Test    |
      | 1003    |
      | company |

    Scenario: Display full search results if suggestion not selected using mouse
        Given I am on trust search page
        When I type 'test' into the search bar
        And I click Search without selecting a result
        Then the trust search results page is displayed

    Scenario: Display full search results if suggestion not selected using keyboard
        Given I am on trust search page
        When I type 'test' into the search bar
        And I press the enter key without selecting a result
        Then the trust search results page is displayed