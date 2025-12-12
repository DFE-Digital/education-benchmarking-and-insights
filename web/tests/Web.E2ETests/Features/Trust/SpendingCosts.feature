Feature: Trust spending and costs

    Scenario: Cost categories are in the correct priority
        Given I am on spending and costs for trust with company number '08104190'
        Then the priority categories are:
          | Priority | Category                                   | Commentary                                        |
          | High     | Catering staff and supplies                | 2 out of 7 schools in the high RAG rating range   |
          | High     | Other costs                                | 2 out of 7 schools in the high RAG rating range   |
          | High     | Premises staff and services                | 2 out of 7 schools in the high RAG rating range   |
          | High     | Administrative supplies                    | 1 out of 7 schools in the high RAG rating range   |
          | High     | Educational supplies                       | 1 out of 7 schools in the high RAG rating range   |
          | High     | Non-educational support staff and services | 1 out of 7 schools in the high RAG rating range   |
          | High     | Teaching and Teaching support staff        | 1 out of 7 schools in the high RAG rating range   |
          | Medium   | Educational ICT                            | 2 out of 7 schools in the medium RAG rating range |
          | Medium   | Utilities                                  | 1 out of 7 schools in the medium RAG rating range |
          | Medium   | Non-educational support staff and services | 1 out of 7 schools in the medium RAG rating range |
          | Medium   | Teaching and Teaching support staff        | 1 out of 7 schools in the medium RAG rating range |
          | Low      | Administrative supplies                    | 1 out of 7 schools in the low RAG rating range    |
          | Low      | Educational supplies                       | 1 out of 7 schools in the low RAG rating range    |
          | Low      | Utilities                                  | 1 out of 7 schools in the low RAG rating range    |