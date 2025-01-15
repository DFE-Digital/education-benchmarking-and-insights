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
          | Utilities                           | Medium priority |
          | Educational ICT                     | Medium priority |

    Scenario: Categories have the correct RAG commentary
        Given I am on spending and costs page for school with URN '777042'
        Then the RAG commentary for each category is
          | Name                                | Commentary                                         |
          | Teaching and Teaching support staff | Spending is higher than 99% of similar schools.    |
          | Non-educational support staff       | Spending is higher than 95.7% of similar schools. |
          | Administrative supplies             | Spending is higher than 99% of similar schools.    |
          | Educational supplies                | Spending is higher than 99% of similar schools.    |
          | Catering staff and supplies         | Spending is higher than 92.3% of similar schools. |
          | Premises staff and services         | Spending is higher than 99% of similar schools.    |
          | Utilities                           | Spending is higher than 39% of similar schools.    |
          | Educational ICT                     | Spending is less than 77.7% of similar schools.   |

    Scenario: Categories have the correct commercial resources
        Given I am on spending and costs page for school with URN '777042'
        Then the commercial resources for each category are
          | Name                                | Resource                                                                                                  |
          | Teaching and Teaching support staff | Supply teachers and temporary staffing (STaTS)                                                            |
          | Teaching and Teaching support staff | Temporary and permanent staffing                                                                          |
          | Non-educational support staff       | Audit services                                                                                            |
          | Non-educational support staff       | Specialist professional services                                                                          |
          | Non-educational support staff       | Legal services                                                                                            |
          | Non-educational support staff       | HR, payroll and employee screening services                                                               |
          | Administrative supplies             | Communications solutions                                                                                  |
          | Administrative supplies             | Communications solutions and associated telephony services                                                |
          | Administrative supplies             | Corporate software and related products and services                                                      |
          | Administrative supplies             | Cyber security services 3                                                                                 |
          | Administrative supplies             | Digital marketplace (G-Cloud 13)                                                                          |
          | Administrative supplies             | Education management systems                                                                              |
          | Administrative supplies             | Everything ICT                                                                                            |
          | Administrative supplies             | ICT networking and storage solutions                                                                      |
          | Administrative supplies             | IT hardware                                                                                               |
          | Administrative supplies             | Multi-functional devices and digital solutions                                                            |
          | Administrative supplies             | Multifunctional devices and digital transformation solutions                                              |
          | Administrative supplies             | Multifunctional devices, print and digital workflow software services and managed print service provision |
          | Administrative supplies             | Outsourced ICT                                                                                            |
          | Administrative supplies             | Print marketplace                                                                                         |
          | Administrative supplies             | Software licences and associated services for academies and schools                                       |
          | Administrative supplies             | Technology products & associated services 2                                                               |
          | Educational supplies                | Books for schools                                                                                         |
          | Catering staff and supplies         | Catering services                                                                                         |
          | Catering staff and supplies         | Catering services                                                                                         |
          | Catering staff and supplies         | Commercial catering equipment                                                                             |
          | Catering staff and supplies         | Grocery, fresh, chilled and frozen foods                                                                  |
          | Catering staff and supplies         | Outsourced catering services                                                                              |
          | Catering staff and supplies         | Facilities management and workplace services                                                              |
          | Premises staff and services         | Building cleaning                                                                                         |
          | Premises staff and services         | Cleaning services                                                                                         |
          | Premises staff and services         | Total cleaning service solutions                                                                          |
          | Premises staff and services         | Facilities management and workplace services                                                              |
          | Premises staff and services         | Internal fit-out and maintenance                                                                          |
          | Premises staff and services         | Building in use - support services                                                                        |
          | Utilities                           | Debt resolution services                                                                                  |
          | Utilities                           | Electricity (for supply during 2020 - 2028)                                                               |
          | Utilities                           | Energy cost recovery services                                                                             |
          | Utilities                           | Flexible electricity                                                                                      |
          | Utilities                           | Flexible gas                                                                                              |
          | Utilities                           | Liquid fuels                                                                                              |
          | Utilities                           | Liquid fuels                                                                                              |
          | Utilities                           | Liquified petroleum gas and other liquified fuels                                                         |
          | Utilities                           | Mains gas 2023                                                                                            |
          | Utilities                           | Supply of energy 2                                                                                        |
          | Utilities                           | Water, wastewater and ancillary services                                                                  |
          | Utilities                           | Water, wastewater and ancillary services                                                                  |

    Scenario: Categories have the correct category commentary
        Given I am on spending and costs page for school with URN '777042'
        Then the category commentary is
          | Name                                | Commentary                                                                                                                                                                                  |
          | Teaching and Teaching support staff | View all teaching and teaching support staff costs. This includes teaching staff, supply teaching staff, agency supply teaching staff, education support staff and educational consultancy. |
          | Non-educational support staff       | View all non-educational support staff and services costs. This includes administrative and clerical staff, auditor costs, other staff and professional services (non-curriculum).          |
          | Administrative supplies             | View all administrative supplies costs. This includes administrative supplies (non-educational).                                                                                            |
          | Educational supplies                | View all educational supplies costs. This includes examination fees and learning resources (not ICT equipment).                                                                             |
          | Catering staff and supplies         | View all catering staff and supplies costs. This includes catering staff and catering supplies.                                                                                             |
          | Premises staff and services         | View all premises staff and services costs. This includes cleaning and caretaking, maintenance of premises, other occupation costs and premises staff.                                      |
          | Utilities                           | View all utilities costs. This includes energy and water and sewerage.                                                                                                                      |
          | Educational ICT                     | View all educational ICT costs. This includes ICT learning resources.                                                                                                                       |

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
          | Utilities                           |
          | Educational ICT                     |

    Scenario Outline: Click on View more details on cost categories link
        Given I am on spending and costs page for school with URN '777042'
        When I click on View more details on cost categories link
        Then I am directed to cost categories guidance page

    Scenario: School with zero for a cost category in comparator set are not consumed when computing
        Given I am on spending and costs page for school with URN '777042'
        Then the 'Educational ICT' category should display:
          | Description           | Value                     |
          | This school spends    | £130 per pupil            |
          | Similar schools spend | £67 per pupil, on average |
          | This school spends    | £30(44.2%) less per pupil |
        And the message stating reason for less schools is visible for 'Educational ICT'