Feature: School find ways to spend less

    Scenario: View all resources
        Given I am on 'recommended' resources page for school with URN '777042'
        And the following priority categories are shown on the page
          | Name                                       | Priority        |
          | Teaching and Teaching support staff        | High priority   |
          | Non-educational support staff and services | High priority   |
          | Administrative supplies                    | High priority   |
          | Educational supplies                       | High priority   |
          | Catering staff and supplies                | High priority   |
          | Premises staff and services                | High priority   |
          | Other costs                                | High priority   |
          | Utilities                                  | Medium priority |
          | Educational ICT                            | Medium priority |
        When I click on all resources
        Then all resources tab is displayed

    Scenario: Show all should expand all sections
        Given I am on 'all' resources page for school with URN '777042'
        When I click on show all sections
        Then all sections on the page are expanded
        And the show all text changes to hide all sections
        And all resources sub categories are displayed on the page
        And all sub categories have correct link