Feature: School spending and costs

    Scenario: View how we choose similar school details
        Given I am on spending and costs page for school with URN '101241'
        And the priority order of charts is
          | Name                                | Priority        |
          | Catering staff and services         | High priority   |
          | Utilities                           | High priority   |
          | Premises and services               | High priority   |
          | Educational ICT                     | Medium priority |
          | Teaching and teaching support staff | Medium priority |
          | Other                               | Medium priority |
          | Administrative supplies             | Medium priority |
          | Educational supplies                | Low priority    |
          | Non-educational support staff       | Low priority    |
        When I click on how we choose similar schools
        Then the details section is expanded

    Scenario Outline: Click on view all links for each chart
        Given I am on spending and costs page for school with URN '103119'
        And the priority order of charts is
          | Name                                | Priority        |
          | Administrative supplies             | Medium priority |
          | Premises and services               | Medium priority |
          | Catering staff and services         | Medium priority |
          | Non-educational support staff       | Medium priority |
          | Other                               | Medium priority |
          | Utilities                           | Medium priority |
          | Educational supplies                | Medium priority |
          | Teaching and teaching support staff | Low priority    |
          | Educational ICT                     | Low priority    |
        When I click on view all '<CostCategory>' link
        Then I am directed to compare your costs page
        And the accordion '<CostCategory>'is expanded

        Examples:
          | CostCategory                        |
          | Administrative supplies             |
          | Premises and services               |
          | Catering staff and services         |
          | Non-educational support staff       |
          | Other                               |
          | Utilities                           |
          | Educational supplies                |
          | Teaching and teaching support staff |
          | Educational ICT                     |