Feature: Find organisation

    @FindOrganisationEnabled
    Scenario: Goto school homepage
        Given I am on find organisation page
        And 'school' organisation type is selected
        When I select the school with urn '990095' from suggester
        When I click Continue
        Then the school homepage is displayed

    @FindOrganisationEnabled
    Scenario: Displaying relevant search suggestions when entering a keyword
        Given I am on find organisation page
        And 'school' organisation type is selected
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

    @FacetedSearchEnabled
    Scenario: Go to school search page
        Given I am on find organisation page
        And 'school' organisation type is selected
        When I click Continue to school search
        Then the school search page is displayed