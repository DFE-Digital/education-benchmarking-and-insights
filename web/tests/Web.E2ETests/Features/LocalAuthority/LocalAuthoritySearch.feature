﻿Feature: Local authority search page

    @FilteredSearchEnabled
    Scenario: Go to local authority homepage using mouse
        Given I am on local authority search page
        When I type '201' into the search bar
        And I click Search after selecting a result
        Then the local authority homepage is displayed

    @FilteredSearchEnabled
    Scenario: Go to local authority homepage using keyboard
        Given I am on local authority search page
        When I type '201' into the search bar
        And I press the enter key after selecting a result
        Then the local authority homepage is displayed

    @FilteredSearchEnabled
    Scenario: Displaying relevant search suggestions when entering a keyword
        Given I am on local authority search page
        When I type '<keyword>' into the search bar
        Then each suggester result contains '<keyword>'

    Examples:
      | keyword |
      | Leeds   |
      | 383     |

    @FilteredSearchEnabled
    Scenario: Display full search results if suggestion not selected using mouse
        Given I am on local authority search page
        When I type 'test' into the search bar
        And I click Search without selecting a result
        Then the local authority search results page is displayed

    @FilteredSearchEnabled
    Scenario: Display full search results if suggestion not selected using keyboard
        Given I am on local authority search page
        When I type 'test' into the search bar
        And I press the enter key without selecting a result
        Then the local authority search results page is displayed