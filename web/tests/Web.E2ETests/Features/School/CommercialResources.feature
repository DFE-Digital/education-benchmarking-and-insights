Feature: School find ways to spend less

    Scenario: View all resources
        Given I am on 'recommended' resources page for school with URN '101241'
        And the following priority categories are shown on the page
          | Name                                | Priority        |
          | Catering staff and services         | High priority   |
          | Utilities                           | High priority   |
          | Premises and services               | High priority   |
          | Educational ICT                     | Medium priority |
          | Teaching and teaching support staff | Medium priority |
          | Other                               | Medium priority |
          | Administrative supplies             | Medium priority |
        When I click on all resources
        Then all resources tab is displayed

    Scenario: Show all should expand all sections
        Given I am on 'all' resources page for school with URN '101241'
        When I click on show all sections
        Then all sections on the page are expanded
        And the show all text changes to hide all sections
        And all resources sub categories are displayed on the page
        And all sub categories have correct link