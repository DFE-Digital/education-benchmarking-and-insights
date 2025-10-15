Feature: Insights metric rag ratings endpoints

    Scenario: Sending a valid user defined metric rag rating request with default options
        Given a valid user defined metric rag rating with runId 'some-user-defined-id', useCustomData ''
        When I submit the metric rag rating request
        Then the metric rag rating result should be OK and match the expected output in 'MetricRagUserDefined.json'

    Scenario: Sending a valid default metric rag rating request with URNs and default options
        Given a valid default metric rag rating with categories '' and statuses '' for urns:
          | Urn    |
          | 777042 |
          | 777043 |
        When I submit the metric rag rating request
        Then the metric rag rating result should be OK and match the expected output in 'MetricRagDefaultUrns.json'

    Scenario: Sending a valid default metric rag rating with company number and phase and default options
        Given a valid default metric rag rating with categories '' and statuses '' with company number '08104190'
        When I submit the metric rag rating request
        Then the metric rag rating result should be OK and match the expected output in 'MetricRagDefaultCompanyNumber.json'

    Scenario: Sending a valid default metric rag rating with LA code and phase and default options
        Given a valid default metric rag rating with categories '' and statuses '' with LA code '205' and phase 'Primary'
        When I submit the metric rag rating request
        Then the metric rag rating result should be bad request and match the expected output in 'MetricRagDefaultMissingUrnsOrCompanyNumber.json'

    Scenario: Sending a valid default metric rag rating request with categories
        Given a valid default metric rag rating with categories 'Administrative supplies,Catering staff and supplies' and statuses '' for urns:
          | Urn    |
          | 777042 |
          | 777043 |
        When I submit the metric rag rating request
        Then the metric rag rating result should be OK and match the expected output in 'MetricRagDefaultCategoryAndUrns.json'

    Scenario: Sending a valid default metric rag rating request with status
        Given a valid default metric rag rating with categories '' and statuses 'amber' for urns:
          | Urn    |
          | 777042 |
          | 777043 |
        When I submit the metric rag rating request
        Then the metric rag rating result should be OK and match the expected output in 'MetricRagDefaultStatusAndUrns.json'
        
    Scenario: Sending an valid default metric rag rating summary request with URNs
        Given a default metric rag rating summary request with urns:
          | Urn    |
          | 777042 |
          | 777049 |
        When I submit the metric rag rating request
        Then the metric rag rating result should be OK and match the expected output in 'MetricRagSummaryUrns.json'
        
    Scenario: Sending an valid default metric rag rating summary request with company number and no overall phase
        Given a default metric rag rating summary request with companyNumber '08104190', LA code '', and overall phase ''
        When I submit the metric rag rating request
        Then the metric rag rating result should be OK and match the expected output in 'MetricRagSummaryCompanyNumber.json'    
        
    Scenario: Sending an valid default metric rag rating summary request with LA code and no overall phase
        Given a default metric rag rating summary request with companyNumber '', LA code '869', and overall phase ''
        When I submit the metric rag rating request
        Then the metric rag rating result should be OK and match the expected output in 'MetricRagSummaryLaCode.json'   
        
    Scenario: Sending an valid default metric rag rating summary request with LA code and Primary overall phase
        Given a default metric rag rating summary request with companyNumber '', LA code '869', and overall phase 'Primary'
        When I submit the metric rag rating request
        Then the metric rag rating result should be OK and match the expected output in 'MetricRagSummaryLaCodePrimary.json'    
         
    Scenario: Sending an valid default metric rag rating summary request with LA code and All-through overall phase
        Given a default metric rag rating summary request with companyNumber '', LA code '869', and overall phase 'All-through'
        When I submit the metric rag rating request
        Then the metric rag rating result should be OK and match the expected output in 'MetricRagSummaryLaCodeAllThrough.json'
                
    Scenario: Sending an invalid default metric rag rating summary request without URNs, company number, or LA code
        Given a default metric rag rating summary request with companyNumber '', LA code '', and overall phase ''
        When I submit the metric rag rating request
        Then the metric rag rating result should be bad request and match the expected output in 'MetricRagSummaryMissingUrnCompanyNumberLaCode.json'
                
    Scenario: Sending an invalid default metric rag rating summary request with LA code and an invalid overall phase
        Given a default metric rag rating summary request with companyNumber '', LA code '869', and overall phase 'invalid'
        When I submit the metric rag rating request
        Then the metric rag rating result should be bad request and match the expected output in 'MetricRagSummaryInvalidPhase.json'
   