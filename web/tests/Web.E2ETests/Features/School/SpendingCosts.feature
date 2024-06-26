Feature: School spending and costs


    Scenario Outline: Click on view all links for each chart
        Given I am on spending and costs page for school with URN '777042'
        And the priority order of charts is
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
        When I click on view all '<CostCategory>' link
        Then I am directed to compare your costs page
        And the accordion '<CostCategory>'is expanded

        Examples:
          | CostCategory                               |
          |Teaching and Teaching support staff         |
          | Non-educational support staff and services |
          | Administrative supplies                    |
          | Educational supplies                       |
          | Catering staff and supplies                |
          | Premises staff and services                |
          | Other costs                                |
          | Utilities                                  |
          | Educational ICT                            |