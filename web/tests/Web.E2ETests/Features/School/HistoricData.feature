Feature: School historic data

    Scenario Outline: Change dimension of history data
        Given I am on '<tab>' history page for school with URN '777042'
        When I change '<tab>' dimension to '<dimension>'
        Then the '<tab>' dimension is '<dimension>'
        
    Examples:
      | tab      | dimension   |
      | spending | £ per pupil |
      | income   | £ per pupil |
      | balance  | £ per pupil |
      | census   | headcount per FTE |

    Scenario Outline: Show all should expand all sections
        Given I am on '<tab>' history page for school with URN '777042'
        When I click on show all sections on '<tab>'
        Then all sections on '<tab>' tab are expanded
        And the show all text changes to hide all sections on '<tab>'
        And all '<tab>' sub categories are displayed on the page
        
    Examples:
      | tab      |
      | spending |
      | income   |

    Scenario Outline: Change all charts to table view
        Given I am on '<tab>' history page for school with URN '777042'
        And all sections are shown on '<tab>'
        When I click on view as table on '<tab>' tab
        Then are showing table view on '<tab>' tab

        Examples:
          | tab      |
          | spending |
          | income   |
          | balance  |
          | census   |
       

    Scenario: Hide single section
        Given I am on 'spending' history page for school with URN '777042'
        And all sections are shown on 'spending'
        When I click section link for 'non educational support staff'
        Then the section 'non educational support staff' is hidden