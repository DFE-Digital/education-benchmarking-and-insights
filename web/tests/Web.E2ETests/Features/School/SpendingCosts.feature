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
          | Teaching and Teaching support staff | Spending is higher than 99% of similar schools.    |
          | Non-educational support staff       | Spending is higher than 95.67% of similar schools. |
          | Administrative supplies             | Spending is higher than 99% of similar schools.    |
          | Educational supplies                | Spending is higher than 99% of similar schools.    |
          | Catering staff and supplies         | Spending is higher than 92.33% of similar schools. |
          | Premises staff and services         | Spending is higher than 99% of similar schools.    |
          | Other costs                         | Spending is higher than 89% of similar schools.    |
          | Utilities                           | Spending is higher than 39% of similar schools.    |
          | Educational ICT                     | Spending is less than 77.67% of similar schools.   |

    Scenario: Categories have the correct commercial resources
        Given I am on spending and costs page for school with URN '777042'
        Then the commercial resources for each category are
          | Name                                | Resource                                                            |
          | Teaching and Teaching support staff | Hiring supply teachers and agency workers                           |
          | Teaching and Teaching support staff | Specialist professional services                                    |
          | Teaching and Teaching support staff | Guidance for CFP                                                    |
          | Teaching and Teaching support staff | Teaching vacancies                                                  |
          | Non-educational support staff       |                                                                     |
          | Administrative supplies             | Digital Marketplace (G-Cloud 12)                                    |
          | Administrative supplies             | DFE Furniture                                                       |
          | Administrative supplies             | Software licenses and associated services for academies and schools |
          | Educational supplies                | Books and educational resources buying guidance                     |
          | Educational supplies                | Print Marketplace                                                   |
          | Educational supplies                | Books and educational resources                                     |
          | Catering staff and supplies         | Building in use                                                     |
          | Premises staff and services         | Building in use - support services                                  |
          | Premises staff and services         | Good estate management for schools                                  |
          | Premises staff and services         | Internal fit-out and maintenance                                    |
          | Other costs                         | National professional qualification (NPQ) framework                 |
          | Other costs                         | Staff absence protection and reimbursement                          |
          | Other costs                         | Risk protection arrangement                                         |
          | Utilities                           | Electricity                                                         |
          | Utilities                           | Water, wastewater and ancillary services 2                          |
          | Educational ICT                     | Network connectivity and telecommunication solutions                |
          | Educational ICT                     | Print market place                                                  |

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