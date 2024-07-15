Feature: Benchmark financial plans Endpoint Testing
    
    Scenario: Getting financial plans successfully
        Given I have a valid financial plans get request for school id '990000' in year '2023' containing:
          | Key                | Value |
          | TargetContactRatio | 1.23  |
        When I submit the financial plans request
        Then the financial plans response for school id '990000' in year '2023' should contain:
          | Key                                 | Value |
          | IsComplete                          | false |
          | TargetContactRatio                  | 1.23  |
          | mixedAgeReceptionYear1              | false |
          | mixedAgeYear1Year2                  | false |
          | mixedAgeYear2Year3                  | false |
          | mixedAgeYear3Year4                  | false |
          | mixedAgeYear4Year5                  | false |
          | mixedAgeYear5Year6                  | false |
          | managementRoleHeadteacher           | false |
          | managementRoleDeputyHeadteacher     | false |
          | managementRoleNumeracyLead          | false |
          | managementRoleLiteracyLead          | false |
          | managementRoleHeadSmallCurriculum   | false |
          | managementRoleHeadKs1               | false |
          | managementRoleHeadKs2               | false |
          | managementRoleSenco                 | false |
          | managementRoleAssistantHeadteacher  | false |
          | managementRoleHeadLargeCurriculum   | false |
          | managementRolePastoralLeader        | false |
          | managementRoleOtherMembers          | false |
          | teachingPeriodsHeadteacher          | []    |
          | teachingPeriodsDeputyHeadteacher    | []    |
          | teachingPeriodsNumeracyLead         | []    |
          | teachingPeriodsLiteracyLead         | []    |
          | teachingPeriodsHeadSmallCurriculum  | []    |
          | teachingPeriodsHeadKs1              | []    |
          | teachingPeriodsHeadKs2              | []    |
          | teachingPeriodsSenco                | []    |
          | teachingPeriodsAssistantHeadteacher | []    |
          | teachingPeriodsHeadLargeCurriculum  | []    |
          | teachingPeriodsPastoralLeader       | []    |
          | teachingPeriodsOtherMembers         | []    |

    Scenario: Setting financial plans successfully
        Given I have a valid financial plans put request for school id '990000' in year '2023' containing:
          | Key                | Value |
          | TargetContactRatio | 1.23  |
        When I submit the financial plans request
        Then the financial plans response should return created or no content
        
    Scenario: Deleting financial plans successfully
        Given I have a valid financial plans delete request for school id '990000' in year '2023' containing:
          | Key                | Value |
          | TargetContactRatio | 1.23  |
        When I submit the financial plans request
        Then the financial plans response should return ok
        
    Scenario: Deleting financial plans unsuccessfully
        Given I have an invalid financial plans delete request for school id '990000' in year '2000'
        When I submit the financial plans request
        Then the financial plans response should return internal server error
        
    Scenario: Getting deployed financial plan unsuccessfully
        Given I do not have a deployed financial plan for school id '990000' in year '2023'
        When I submit the financial plan deployment request
        Then the financial plan deployment response should return not found
        
    Scenario: Getting all financial plans successfully
        Given I have a valid financial plans get request including school id '990000' in year '2023' but excluding:
          | Urn    | Year |
          | 990001 | 2023 |
          | 990002 | 2023 |
        When I submit the financial plans request
        Then the financial plans response should contain:
          | Urn    | Year |
          | 990000 | 2023 |
