Feature: School search page

    Scenario: Go to school homepage using mouse
        Given I am on school search page
        When I type '990095' into the search bar
        And I click Search after selecting a result
        Then the school homepage is displayed

    Scenario: Go to school homepage using keyboard
        Given I am on school search page
        When I type '990095' into the search bar
        And I press the enter key after selecting a result
        Then the school homepage is displayed

    Scenario: Displaying relevant search suggestions when entering a keyword
        Given I am on school search page
        When I type '<keyword>' into the search bar
        Then each suggester result contains '<keyword>'

    Examples:
      | keyword            |
      | Test               |
      | 9905               |
      | address            |
      | brixton            |
      | greenwich          |
      | London             |
      | Greater Manchester |
      | ABC                |

    Scenario: Display full search results if suggestion not selected using mouse
        Given I am on school search page
        When I type 'test' into the search bar
        And I click Search without selecting a result
        Then the school search results page is displayed

    Scenario: Display full search results if suggestion not selected using keyboard
        Given I am on school search page
        When I type 'test' into the search bar
        And I press the enter key without selecting a result
        Then the school search results page is displayed