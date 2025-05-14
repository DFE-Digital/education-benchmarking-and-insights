Feature: Insights metric rag ratings endpoints

    Scenario: Sending a valid user defined metric rag rating request with default options
        Given a valid user defined metric rag rating with runId 'some-user-defined-id', useCustomData ''
        When I submit the metric rag rating request
        Then the metric rag rating result should be ok and contain:
          | URN | Category | SubCategory | Value | Median | DiffMedian | PercentDiff | Percentile | Decile | RAG |

    Scenario: Sending a valid default metric rag rating request with URNs and default options
        Given a valid default metric rag rating with categories '' and statuses '' for urns:
          | Urn    |
          | 777042 |
          | 777043 |
        When I submit the metric rag rating request
        Then the metric rag rating result should be ok and contain:
          | URN    | Category                                   | SubCategory | Value   | Median  | DiffMedian | PercentDiff | Percentile | Decile | RAG   |
          | 777042 | Administrative supplies                    | Total       | 429.07  | 47.78   | 381.28     | 88.86       | 99.00      | 9.00   | red   |
          | 777042 | Catering staff and supplies                | Total       | 362.59  | 173.19  | 189.40     | 52.23       | 92.33      | 9.00   | red   |
          | 777042 | Educational ICT                            | Total       | 37.59   | 67.38   | -29.79     | -79.26      | 22.33      | 2.00   | amber |
          | 777042 | Educational supplies                       | Total       | 407.06  | 124.94  | 282.12     | 69.31       | 99.00      | 9.00   | red   |
          | 777042 | Non-educational support staff and services | Total       | 844.54  | 493.42  | 351.12     | 41.58       | 95.67      | 9.00   | red   |
          | 777042 | Other costs                                | Total       | 374.24  | 225.45  | 148.78     | 39.76       | 89.00      | 8.00   | red   |
          | 777042 | Premises staff and services                | Total       | 92.75   | 42.42   | 50.33      | 54.26       | 99.00      | 9.00   | red   |
          | 777042 | Teaching and Teaching support staff        | Total       | 6314.59 | 3540.90 | 2773.68    | 43.93       | 99.00      | 9.00   | red   |
          | 777042 | Utilities                                  | Total       | 9.08    | 10.45   | -1.37      | -15.12      | 39.00      | 3.00   | amber |

    Scenario: Sending a valid default metric rag rating with company number and phase and default options
        Given a valid default metric rag rating with categories '' and statuses '' with company number '08104190'
        When I submit the metric rag rating request
        Then the metric rag rating result should be ok and contain:
          | URN    | Category                                   | SubCategory | Value    | Median  | DiffMedian | PercentDiff | Percentile | Decile | RAG   |
          | 777051 | Administrative supplies                    | Total       | 60.15    | 116.30  | -56.16     | -48.28      | 26.67      | 2.00   | green |
          | 777051 | Catering staff and supplies                | Total       | 1080.40  | 426.62  | 653.78     | 153.24      | 96.67      | 9.00   | red   |
          | 777051 | Educational ICT                            | Total       | 0.00     | 45.74   | -45.74     | -100.00     | 10.00      | 1.00   | amber |
          | 777051 | Educational supplies                       | Total       | 73.65    | 118.68  | -45.03     | -37.94      | 30.00      | 3.00   | green |
          | 777051 | Non-educational support staff and services | Total       | 1594.56  | 1585.13 | 9.43       | 0.59        | 53.33      | 5.00   | amber |
          | 777051 | Other costs                                | Total       | 784.94   | 437.39  | 347.55     | 79.46       | 86.67      | 8.00   | red   |
          | 777051 | Premises staff and services                | Total       | 138.42   | 57.88   | 80.54      | 139.13      | 100.00     | 10.00  | red   |
          | 777051 | Teaching and Teaching support staff        | Total       | 11199.60 | 8855.38 | 2344.22    | 26.47       | 76.67      | 7.00   | amber |
          | 777051 | Utilities                                  | Total       | 0.00     | 12.26   | -12.26     | -100.00     | 3.33       | 0.00   | green |
          | 777049 | Administrative supplies                    | Total       | 365.43   | 51.61   | 313.82     | 608.08      | 100.00     | 10.00  | red   |
          | 777049 | Catering staff and supplies                | Total       | 373.66   | 261.96  | 111.71     | 42.64       | 86.67      | 8.00   | red   |
          | 777049 | Educational ICT                            | Total       | 1.05     | 65.67   | -64.62     | -98.39      | 10.00      | 1.00   | amber |
          | 777049 | Educational supplies                       | Total       | 344.54   | 211.04  | 133.50     | 63.26       | 96.67      | 9.00   | red   |
          | 777049 | Non-educational support staff and services | Total       | 823.25   | 506.43  | 316.81     | 62.56       | 96.67      | 9.00   | red   |
          | 777049 | Other costs                                | Total       | 358.80   | 256.77  | 102.03     | 39.74       | 83.33      | 8.00   | red   |
          | 777049 | Premises staff and services                | Total       | 62.80    | 43.12   | 19.68      | 45.62       | 83.33      | 8.00   | red   |
          | 777049 | Teaching and Teaching support staff        | Total       | 6465.78  | 4075.20 | 2390.58    | 58.66       | 100.00     | 10.00  | red   |
          | 777049 | Utilities                                  | Total       | 14.17    | 12.17   | 2.00       | 16.43       | 73.33      | 7.00   | amber |

    Scenario: Sending a valid default metric rag rating with LA code and phase and default options
        Given a valid default metric rag rating with categories '' and statuses '' with LA code '205' and phase 'Primary'
        When I submit the metric rag rating request
        Then the metric rag rating result should be ok and contain:
          | URN    | Category                                   | SubCategory | Value   | Median  | DiffMedian | PercentDiff | Percentile | Decile | RAG   |
          | 777042 | Administrative supplies                    | Total       | 429.07  | 47.78   | 381.28     | 88.86       | 99.00      | 9.00   | red   |
          | 777042 | Catering staff and supplies                | Total       | 362.59  | 173.19  | 189.40     | 52.23       | 92.33      | 9.00   | red   |
          | 777042 | Educational ICT                            | Total       | 37.59   | 67.38   | -29.79     | -79.26      | 22.33      | 2.00   | amber |
          | 777042 | Educational supplies                       | Total       | 407.06  | 124.94  | 282.12     | 69.31       | 99.00      | 9.00   | red   |
          | 777042 | Non-educational support staff and services | Total       | 844.54  | 493.42  | 351.12     | 41.58       | 95.67      | 9.00   | red   |
          | 777042 | Other costs                                | Total       | 374.24  | 225.45  | 148.78     | 39.76       | 89.00      | 8.00   | red   |
          | 777042 | Premises staff and services                | Total       | 92.75   | 42.42   | 50.33      | 54.26       | 99.00      | 9.00   | red   |
          | 777042 | Teaching and Teaching support staff        | Total       | 6314.59 | 3540.90 | 2773.68    | 43.93       | 99.00      | 9.00   | red   |
          | 777042 | Utilities                                  | Total       | 9.08    | 10.45   | -1.37      | -15.12      | 39.00      | 3.00   | amber |
          | 990183 | Catering staff and supplies                | Total       | 480.00  | 260.00  | 219.00     | 46.00       | 96.00      | 9.00   | red   |

    Scenario: Sending a valid default metric rag rating request with categories
        Given a valid default metric rag rating with categories 'Administrative supplies,Catering staff and supplies' and statuses '' for urns:
          | Urn    |
          | 777042 |
          | 777043 |
        When I submit the metric rag rating request
        Then the metric rag rating result should be ok and contain:
          | URN    | Category                    | SubCategory | Value  | Median | DiffMedian | PercentDiff | Percentile | Decile | RAG |
          | 777042 | Administrative supplies     | Total       | 429.07 | 47.78  | 381.28     | 88.86       | 99.00      | 9.00   | red |
          | 777042 | Catering staff and supplies | Total       | 362.59 | 173.19 | 189.40     | 52.23       | 92.33      | 9.00   | red |

    Scenario: Sending a valid default metric rag rating request with status
        Given a valid default metric rag rating with categories '' and statuses 'amber' for urns:
          | Urn    |
          | 777042 |
          | 777043 |
        When I submit the metric rag rating request
        Then the metric rag rating result should be ok and contain:
          | URN    | Category        | SubCategory | Value | Median | DiffMedian | PercentDiff | Percentile | Decile | RAG   |
          | 777042 | Educational ICT | Total       | 37.59 | 67.38  | -29.79     | -79.26      | 22.33      | 2.00   | amber |
          | 777042 | Utilities       | Total       | 9.08  | 10.45  | -1.37      | -15.12      | 39.00      | 3.00   | amber |

    Scenario: Sending an invalid default metric rag rating request
        Given a valid default metric rag rating with categories '' and statuses '' with LA code '205' and phase 'Invalid'
        When I submit the metric rag rating request
        Then the metric rag rating result should be bad request