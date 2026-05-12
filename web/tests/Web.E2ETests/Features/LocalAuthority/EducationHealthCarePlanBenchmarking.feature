@HighNeedsBenchmarkingFlagEnabled
Feature: Local Authority Education Health Care Plan Benchmarking

    Background:
        Given I am on education health care plan comparators selection page for local authority with code '211'
        When I click the Start Benchmarking button

    Scenario: Page displays all education health care plan charts
        Given I am on education health care plan benchmarking page for local authority with code '211'
        Then I should see all the education health care plan charts displayed:
            | Chart name                                                                        |
            | Total pupils with EHC plans                                                       |
            | Placement of pupils with EHC plans in mainstream schools or academies             |
            | Placement of pupils with EHC plans resourced provision or SEN units               |
            | Placement of pupils with EHC plans maintained special school or special academies |
            | Placement of pupils with EHC plans NMSS or independent schools                    |
            | Placement of pupils with EHC plans in hospital schools or alternative provisions  |
            | Placement of pupils with EHC plans in post 16                                     |
            | Placement of pupils with EHC plans in other types of provisions                   |

    Scenario: Total pupils with EHC plans table displays correct data
        Given I am on education health care plan benchmarking page for local authority with code '211'
        When I change view to table
        Then the following is shown for 'Total EHC plans'
          | Local authority        | Number of pupils | EHC plans per 1,000 pupils |
          | Hackney                | 57,444           | 53.3                       |
          | Greenwich              | 63,850           | 34.14                      |
          | City of London         | 1,650            | 11.52                      |
          | Hammersmith and Fulham | 18,500           | 0                          |
          | Tower Hamlets          | 28               | No data submitted          |

