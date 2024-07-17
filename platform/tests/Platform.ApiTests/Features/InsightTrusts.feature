Feature: Insights trusts endpoints
        
    Scenario: Sending valid trust characteristics requests
        Given a valid trust characteristics request with company numbers:
           | CompanyNumber |
           | 10020683      |
           | 10038640      |
           | 10050238      |
        When I submit the insight trusts request
        Then the trust characteristics results should be ok and contain:
          | CompanyNumber | TrustName               | TotalIncome | TotalPupils | SchoolsInTrust | OpenDate             | PercentFreeSchoolMeals | PercentSpecialEducationNeeds | TotalInternalFloorArea | 
          | 10020683      | Test Company/Trust  244 |             | 128.0       | 1.0            | 9/1/2021 12:00:00 AM | 6.0                    | 0.0                          | 3710.0                 |        
          | 10038640      | Test Company/Trust  334 |             | 292.0       | 1.0            | 6/1/2016 12:00:00 AM | 14.0                   | 0.0                          | 25572.0                |        
          | 10050238      | Test Company/Trust  388 |             | 139.0       | 1.0            | 9/1/2017 12:00:00 AM | 7.0                    | 0.0                          | 17322.0                |         
