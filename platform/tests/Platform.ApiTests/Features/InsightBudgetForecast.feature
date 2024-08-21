﻿Feature: Insights budget forecast endpoints
    
    Scenario: Sending a valid budget forecast request with default options
        Given a valid budget forecast request for company number '10192252' with runType 'default', category 'Revenue reserve' and runId '2022'
        When I submit the budget forecast request
        Then the budget forecast result should be ok and contain:
          | Year | Forecast | Actual | ForecastTotalPupils | ActualTotalPupils | Variance | PercentVariance | VarianceStatus |
        
    Scenario: Sending a valid budget forecast metrics request
        Given a valid budget forecast metrics request for company number '10192252'
        When I submit the budget forecast request
        Then the budget forecast metrics result should be ok and contain:
          | RunType | RunId | Year | CompanyNumber | Metric | Value |