Feature: Trust spending and costs

    Scenario: Cost categories are in the correct priority
        Given I am on spending and costs for trust with company number '08104190'
        Then the priority categories are:
          | Priority        | Category                                   | Commentary                                      |
          | High priority   | Catering staff and supplies                | 2 out of 7 schools in the high priority range   |
          | High priority   | Other costs                                | 2 out of 7 schools in the high priority range   |
          | High priority   | Premises staff and services                | 2 out of 7 schools in the high priority range   |
          | High priority   | Administrative supplies                    | 1 out of 7 schools in the high priority range   |
          | High priority   | Educational supplies                       | 1 out of 7 schools in the high priority range   |
          | High priority   | Non-educational support staff and services | 1 out of 7 schools in the high priority range   |
          | High priority   | Teaching and Teaching support staff        | 1 out of 7 schools in the high priority range   |
          | Medium priority | Educational ICT                            | 2 out of 7 schools in the medium priority range |
          | Medium priority | Utilities                                  | 1 out of 7 schools in the medium priority range |
          | Medium priority | Non-educational support staff and services | 1 out of 7 schools in the medium priority range |
          | Medium priority | Teaching and Teaching support staff        | 1 out of 7 schools in the medium priority range |
          | Low priority    | Administrative supplies                    | 1 out of 7 schools in the low priority range    |
          | Low priority    | Educational supplies                       | 1 out of 7 schools in the low priority range    |
          | Low priority    | Utilities                                  | 1 out of 7 schools in the low priority range    |