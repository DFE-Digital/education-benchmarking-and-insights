Feature: School historic data

    Scenario: Change dimension of total spending and costs
        Given I am on 'spending' history page for school with URN '101241'
        When I change 'spending' dimension to '£ per pupil'
        Then the 'spending' dimension is '£ per pupil'

    Scenario: Show all should expand all sections
        Given I am on 'spending' history page for school with URN '101241'
        When I click on show all sections on 'spending' tab
        Then all sections on 'spending' tab are expanded
        And the show all text changes to hide all sections on 'spending' tab
        And all 'spending' sub categories are displayed on the page

    Scenario Outline: Change all charts to table view
        Given I am on '<tab>' history page for school with URN '101241'
        And all sections are shown on '<tab>' tab
        When I click on view as table on '<tab>' tab
        Then all sections on '<tab>' tab are expanded
        And are showing table view on '<tab>' tab

        Examples:
          | tab      |
          | spending |
          | income   |

    Scenario: Hide single section
        Given I am on 'spending' history page for school with URN '101241'
        And all sections are shown on 'spending' tab
        When I click section link for 'non educational support staff'
        Then the section 'non educational support staff' is hidden