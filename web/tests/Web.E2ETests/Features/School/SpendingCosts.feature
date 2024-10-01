Feature: School spending and costs

    Scenario: Categories in the correct priority order
        Given I am on spending and costs page for school with URN '777042'
        Then the priority order of charts is
          | Name                                | Priority        |
          | Teaching and Teaching support staff | High priority   |
          | Non-educational support staff       | High priority   |
          | Administrative supplies             | High priority   |
          | Educational supplies                | High priority   |
          | Catering staff and supplies         | High priority   |
          | Premises staff and services         | High priority   |
          | Other costs                         | High priority   |
          | Utilities                           | Medium priority |
          | Educational ICT                     | Medium priority |

    Scenario: Categories have the correct RAG commentary
        Given I am on spending and costs page for school with URN '777042'
        Then the RAG commentary for each category is
          | Name                                | Commentary                                         |
          | Teaching and Teaching support staff | Spending is lower than 1% of similar schools.      |
          | Non-educational support staff       | Spending is higher than 95.67% of similar schools. |
          | Administrative supplies             | Spending is higher than 99% of similar schools.    |
          | Educational supplies                | Spending is lower than 1% of similar schools.      |
          | Catering staff and supplies         | Spending is higher than 92.33% of similar schools. |
          | Premises staff and services         | Spending is higher than 99% of similar schools.    |
          | Other costs                         | Spending is higher than 89% of similar schools.    |
          | Utilities                           | Spending is higher than 39% of similar schools.    |
          | Educational ICT                     | Spending is higher than 22.33% of similar schools. |

    Scenario Outline: Click on view all links for each chart
        Given I am on spending and costs page for school with URN '777042'
        When I click on view all '<CostCategory>' link
        Then I am directed to compare your costs page
        And the accordion '<CostCategory>'is expanded

        Examples:
          | CostCategory                        |
          | Teaching and Teaching support staff |
          | Non-educational support staff       |
          | Administrative supplies             |
          | Educational supplies                |
          | Catering staff and supplies         |
          | Premises staff and services         |
          | Other costs                         |
          | Utilities                           |
          | Educational ICT                     |