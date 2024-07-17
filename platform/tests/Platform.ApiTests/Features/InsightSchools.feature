Feature: Insights schools endpoints
          
    Scenario: Sending a valid school characteristics request
        Given a valid school characteristics request with urn '990000'
        When I submit the insight schools request
        Then the school characteristics result should be ok and contain:
          | Field                        | Value           |
          | URN                          | 990000          |
          | SchoolName                   | Test school 176 |
          | AddressTown                  | London          |
          | AddressPostcode              | ABC208          |
          | FinanceType                  | Maintained      |
          | OverallPhase                 | Primary         |
          | LAName                       | Bedford         |
          | TotalPupils                  | 337.00          |
          | PercentFreeSchoolMeals       | 11.00           |
          | PercentSpecialEducationNeeds | 0.00            |
          | LondonWeighting              | Outer           |
          | BuildingAverageAge           | 1945.00         |
          | TotalInternalFloorArea       | 2549.00         |
          | OfstedDescription            | Good            |
          | SchoolsInTrust               |                 |
          | IsPFISchool                  | false           |
          | TotalPupilsSixthForm         | 0.0             |
          | KS2Progress                  | 1.0             |
          | KS4Progress                  | 32.0            |
          | PercentWithVI                | 4.0             |
          | PercentWithSPLD              | 3.0             |
          | PercentWithSLD               | 1.0             |
          | PercentWithSLCN              | 14.0            |
          | PercentWithSEMH              | 0.0             |
          | PercentWithPMLD              | 4.0             |
          | PercentWithPD                | 2.0             |
          | PercentWithOTH               | 2.0             |
          | PercentWithMSI               | 14.0            |
          | PercentWithMLD               | 0.0             |
          | PercentWithHI                | 94.0            |
          | PercentWithASD               | 26.0            |
          | SchoolPosition               | Deficit         |
          | Address                      | London, ABC208  |

    Scenario: Sending an invalid school characteristics request
        Given an invalid school characteristics request with urn '1000000'
        When I submit the insight schools request
        Then the school characteristics result should be not found
        
    Scenario: Sending valid school characteristics requests
        Given a valid school characteristics request with urns:
           | Urn    |
           | 990000 |
           | 990001 |
           | 990002 |
        When I submit the insight schools request
        Then the school characteristics results should be ok and contain:
          | URN    | SchoolName      | AddressTown | AddressPostcode | FinanceType | OverallPhase | LAName        | TotalPupils | PercentFreeSchoolMeals | PercentSpecialEducationNeeds | LondonWeighting | BuildingAverageAge | TotalInternalFloorArea | OfstedDescription | SchoolsInTrust | IsPFISchool | TotalPupilsSixthForm | KS2Progress | KS4Progress | PercentWithVI | PercentWithSPLD | PercentWithSLD | PercentWithSLCN | PercentWithSEMH | PercentWithPMLD | PercentWithPD | PercentWithOTH | PercentWithMSI | PercentWithMLD | PercentWithHI | PercentWithASD | SchoolPosition | Address        |
          | 990000 | Test school 176 | London      | ABC208          | Maintained  | Primary      | Bedford       | 337.0       | 11.0                   | 0.0                          | Outer           | 1945.0             | 2549.0                 | Good              |                | False       | 0.0                  | 1.0         | 32.0        | 4.0           | 3.0             | 1.0            | 14.0            | 0.0             | 4.0             | 2.0           | 2.0            | 14.0           | 0.0            | 94.0          | 26.0           | Deficit        | London, ABC208 |
          | 990001 | Test school 241 | London      | ABC281          | Maintained  | Primary      | Islington     | 164.0       | 11.0                   | 0.0                          | Inner           | 1945.0             | 2549.0                 | Good              |                | False       | 0.0                  | 1.0         | 49.0        | 9.0           | 6.0             | 1.0            | 34.0            | 0.0             | 6.0             | 1.0           | 5.0            | 23.0           | 0.0            | 100.0         | 51.0           | Deficit        | London, ABC281 |
          | 990002 | Test school 224 | London      | ABC262          | Maintained  | Nursery      | Isle of Wight | 86.0        | 14.0                   | 0.0                          | Inner           | 1985.0             | 352.0                  | Outstanding       |                | False       | 0.0                  | 1.0         | 41.0        | 8.0           | 7.0             | 1.0            | 54.0            | 0.0             | 2.0             | 0.0           | 5.0            | 14.0           | 0.0            | 100.0         | 0.0            | Deficit        | London, ABC262 |
