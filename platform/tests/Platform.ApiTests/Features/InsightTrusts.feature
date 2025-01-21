Feature: Insights trusts endpoints

    Scenario: Sending valid trust characteristics requests
        Given a valid trust characteristics request with company numbers:
          | CompanyNumber |
          | 10020683      |
          | 10038640      |
          | 10050238      |
          | 10749662      |
          | 10499174      |
        When I submit the insight trusts request
        Then the trust characteristics results should be ok and contain:
          | CompanyNumber | TrustName               | TotalIncome | TotalPupils | SchoolsInTrust | OpenDate              | PercentFreeSchoolMeals | PercentSpecialEducationNeeds | TotalInternalFloorArea |
     | 10020683      | Test Company/Trust  244 | 6480833.00  | 128         | 1              | 6/1/2016 12:00:00 AM | 6                      | 0                            | 3710                   |
     | 10038640      | Test Company/Trust  334 | 10011989.00 | 292         | 1              | 9/1/2017 12:00:00 AM | 14                     | 0                            | 25572                  |
     | 10050238      | Test Company/Trust  388 | 11762969.00 | 139         | 1              | 12/1/2018 12:00:00 AM | 7                      | 0                            | 17322                  |
     | 10499174      | Heart of Mercia         | 20238740.00 | 747         | 3              | 3/1/2017 12:00:00 AM | 9.666666               | 0                            | 7255                   |
     | 10749662      | Hamwic Education Trust  | 20181319.00 | 694         | 3              | 3/1/2014 12:00:00 AM | 10.333333              | 0                            | 6686                   |
And the phases should contain:
          | CompanyNumber | Phases                             |
          | 10020683      | Special: 1                         |
          | 10038640      | Pupil referral unit: 1             |
          | 10050238      | All-through: 1                     |
          | 10499174      | 16 plus: 3                         |
          | 10749662      | Primary: 2, Pupil referral unit: 1 |