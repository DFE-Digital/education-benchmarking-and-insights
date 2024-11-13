Feature: Trust homepage

    Scenario: Go to compare your costs page
        Given I am on trust homepage for trust with company number '10074054'
        When I click on compare your costs
        Then the compare your costs page is displayed

    Scenario: Go to spending and costs page
        Given I am on trust homepage for trust with company number '8104190'
        When I click on view all spending priorities for this trust
        Then the spending and costs page is displayed

    Scenario: View RAG ratings for cost categories in trust
        Given I am on trust homepage for trust with company number '8104190'
        Then I can see the following RAG ratings for cost categories in the trust:
          | Name                                       | Status                                                                               |
          | Catering staff and supplies                | 2 high, 0 medium and 0 low priorities for Catering staff and supplies                |
          | Premises staff and services                | 2 high, 0 medium and 0 low priorities for Premises staff and services                |
          | Non-educational support staff and services | 1 high, 1 medium and 0 low priorities for Non-educational support staff and services |
          | Teaching and Teaching support staff        | 1 high, 1 medium and 0 low priorities for Teaching and Teaching support staff        |
          | Administrative supplies                    | 1 high, 0 medium and 1 low priorities for Administrative supplies                    |
          | Educational supplies                       | 1 high, 0 medium and 1 low priorities for Educational supplies                       |
          | Educational ICT                            | 0 high, 2 medium and 0 low priorities for Educational ICT                            |
          | Utilities                                  | 0 high, 1 medium and 1 low priorities for Utilities                                  |

    Scenario: View RAG ratings for schools in trust
        Given I am on trust homepage for trust with company number '8104190'
        Then I can see the following RAG ratings for schools in the trust:
          | Name                    | Status                                                            |
          | Test academy school 319 | 6 high, 2 medium and 0 low priorities for Test academy school 319 |
          | Test academy school 87  | 2 high, 3 medium and 3 low priorities for Test academy school 87  |
          | Test academy school 90  | Status unavailable                                                |
          | Test academy school 91  | Status unavailable                                                |
          | Test academy school 92  | Status unavailable                                                |
          | Test academy school 93  | Status unavailable                                                |
          | Test academy school 94  | Status unavailable                                                |