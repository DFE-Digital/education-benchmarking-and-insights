﻿Feature: Local authority search page

    Scenario: Go to local authority homepage using mouse
        Given I am on local authority search page
        When I type '201' into the search bar
        And I click Search after selecting a result
        Then the local authority home page is displayed

    Scenario: Go to local authority homepage using keyboard
        Given I am on local authority search page
        When I type '201' into the search bar
        And I press the enter key after selecting a result
        Then the local authority home page is displayed

    Scenario: Displaying relevant search suggestions when entering a keyword
        Given I am on local authority search page
        When I type '<keyword>' into the search bar
        Then each suggester result contains '<keyword>'

    Examples:
      | keyword |
      | Leeds   |
      | 383     |

    Scenario: Display full search results if suggestion not selected using mouse
        Given I am on local authority search page
        When I type 'test' into the search bar
        And I click Search without selecting a result
        Then the local authority search results page is displayed

    Scenario: Display full search results if suggestion not selected using keyboard
        Given I am on local authority search page
        When I type 'test' into the search bar
        And I press the enter key without selecting a result
        Then the local authority search results page is displayed