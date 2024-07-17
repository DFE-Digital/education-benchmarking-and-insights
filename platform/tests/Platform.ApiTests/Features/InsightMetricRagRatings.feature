Feature: Insights metric rag ratings endpoints
    
    Scenario: Sending a valid user defined metric rag rating request with default options
        Given a valid user defined metric rag rating with runId 'some-user-defined-id', useCustomData '' and setType ''
        When I submit the metric rag rating request
        Then the metric rag rating result should be ok and contain:
          | URN | Category | SubCategory | Value | Mean | DiffMean | PercentDiff | Percentile | Decile | RAG |
          
    Scenario: Sending a valid default metric rag rating request with default options
        Given a valid default metric rag rating with categories '' and statuses '' for urns:
          | Urn    |
          | 777042 |
          | 777043 |
        When I submit the metric rag rating request
        Then the metric rag rating result should be ok and contain:
          | URN    | Category                                   | SubCategory | Value   | Mean    | DiffMean | PercentDiff | Percentile | Decile | RAG   |
          | 777042 | Administrative supplies                    | Total       | 429.07  | 47.78   | 381.28   | 88.86       | 99.00      | 9.00   | red   |
          | 777042 | Catering staff and supplies                | Total       | 362.59  | 173.19  | 189.40   | 52.23       | 92.33      | 9.00   | red   |
          | 777042 | Educational ICT                            | Total       | 37.59   | 67.38   | -29.79   | -79.26      | 22.33      | 2.00   | amber |
          | 777042 | Educational supplies                       | Total       | 407.06  | 124.94  | 282.12   | 69.31       | 99.00      | 9.00   | red   |
          | 777042 | Non-educational support staff and services | Total       | 844.54  | 493.42  | 351.12   | 41.58       | 95.67      | 9.00   | red   |
          | 777042 | Other costs                                | Total       | 374.24  | 225.45  | 148.78   | 39.76       | 89.00      | 8.00   | red   |
          | 777042 | Premises staff and services                | Total       | 92.75   | 42.42   | 50.33    | 54.26       | 99.00      | 9.00   | red   |
          | 777042 | Teaching and Teaching support staff        | Total       | 6314.59 | 3540.90 | 2773.68  | 43.93       | 99.00      | 9.00   | red   |
          | 777042 | Utilities                                  | Total       | 9.08    | 10.45   | -1.37    | -15.12      | 39.00      | 3.00   | amber |

    Scenario: Sending a valid default metric rag rating request with categories
        Given a valid default metric rag rating with categories 'Administrative supplies,Catering staff and supplies' and statuses '' for urns:
          | Urn    |
          | 777042 |
          | 777043 |
        When I submit the metric rag rating request
        Then the metric rag rating result should be ok and contain:
          | URN    | Category                    | SubCategory | Value  | Mean   | DiffMean | PercentDiff | Percentile | Decile | RAG |
          | 777042 | Administrative supplies     | Total       | 429.07 | 47.78  | 381.28   | 88.86       | 99.00      | 9.00   | red |
          | 777042 | Catering staff and supplies | Total       | 362.59 | 173.19 | 189.40   | 52.23       | 92.33      | 9.00   | red |
          
    Scenario: Sending a valid default metric rag rating request with status
        Given a valid default metric rag rating with categories '' and statuses 'amber' for urns:
          | Urn    |
          | 777042 |
          | 777043 |
        When I submit the metric rag rating request
        Then the metric rag rating result should be ok and contain:
          | URN    | Category        | SubCategory | Value | Mean  | DiffMean | PercentDiff | Percentile | Decile | RAG   |
          | 777042 | Educational ICT | Total       | 37.59 | 67.38 | -29.79   | -79.26      | 22.33      | 2.00   | amber |
          | 777042 | Utilities       | Total       | 9.08  | 10.45 | -1.37    | -15.12      | 39.00      | 3.00   | amber |
