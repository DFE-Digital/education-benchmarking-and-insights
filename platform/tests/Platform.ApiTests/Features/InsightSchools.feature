Feature: Insights schools endpoints

    Scenario: Sending a valid school characteristics request
        Given a valid school characteristics request with urn '990000'
        When I submit the insight schools request
        Then the school characteristics result should be ok and contain:
          | Field                        | Value                |
          | URN                          | 990000               |
          | SchoolName                   | Test school 176      |
          | AddressTown                  | London               |
          | AddressPostcode              | ABC208               |
          | FinanceType                  | Maintained           |
          | OverallPhase                 | Primary              |
          | LAName                       | Test Local Authority |
          | TotalPupils                  | 337.00               |
          | PercentFreeSchoolMeals       | 11.00                |
          | PercentSpecialEducationNeeds | 0.00                 |
          | LondonWeighting              | Outer                |
          | BuildingAverageAge           | 1945.00              |
          | TotalInternalFloorArea       | 2549.00              |
          | OfstedDescription            | Good                 |
          | SchoolsInTrust               | 269                  |
          | IsPFISchool                  | false                |
          | TotalPupilsSixthForm         | 0.0                  |
          | KS2Progress                  | 1.0                  |
          | KS4Progress                  | 32.0                 |
          | PercentWithVI                | 4.0                  |
          | PercentWithSPLD              | 3.0                  |
          | PercentWithSLD               | 1.0                  |
          | PercentWithSLCN              | 14.0                 |
          | PercentWithSEMH              | 0.0                  |
          | PercentWithPMLD              | 4.0                  |
          | PercentWithPD                | 2.0                  |
          | PercentWithOTH               | 2.0                  |
          | PercentWithMSI               | 14.0                 |
          | PercentWithMLD               | 0.0                  |
          | PercentWithHI                | 94.0                 |
          | PercentWithASD               | 26.0                 |
          | SchoolPosition               | Deficit              |
          | Address                      | London, ABC208       |

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
          | URN    | SchoolName      | AddressTown | AddressPostcode | FinanceType | OverallPhase | LAName               | TotalPupils | PercentFreeSchoolMeals | PercentSpecialEducationNeeds | LondonWeighting | BuildingAverageAge | TotalInternalFloorArea | OfstedDescription | SchoolsInTrust | IsPFISchool | TotalPupilsSixthForm | KS2Progress | KS4Progress | PercentWithVI | PercentWithSPLD | PercentWithSLD | PercentWithSLCN | PercentWithSEMH | PercentWithPMLD | PercentWithPD | PercentWithOTH | PercentWithMSI | PercentWithMLD | PercentWithHI | PercentWithASD | SchoolPosition | Address        |
          | 990000 | Test school 176 | London      | ABC208          | Maintained  | Primary      | Test Local Authority | 337         | 11                     | 0                            | Outer           | 1945               | 2549                   | Good              | 269            | False       | 0                    | 1           | 32          | 4             | 3               | 1              | 14              | 0               | 4               | 2             | 2              | 14             | 0              | 94            | 26             | Deficit        | London, ABC208 |
          | 990001 | Test school 241 | London      | ABC281          | Maintained  | Primary      | Islington            | 164         | 11                     | 0                            | Inner           | 1945               | 2549                   | Good              | 269            | False       | 0                    | 1           | 49          | 9             | 6               | 1              | 34              | 0               | 6               | 1             | 5              | 23             | 0              | 100           | 51             | Deficit        | London, ABC281 |
          | 990002 | Test school 224 | London      | ABC262          | Maintained  | Nursery      | Isle of Wight        | 86          | 14                     | 0                            | Inner           | 1985               | 352                    | Outstanding       | 269            | False       | 0                    | 1           | 41          | 8             | 7               | 1              | 54              | 0               | 2               | 0             | 5              | 14             | 0              | 100           | 0              | Deficit        | London, ABC262 |