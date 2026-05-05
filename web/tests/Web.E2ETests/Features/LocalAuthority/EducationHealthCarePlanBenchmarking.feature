@HighNeedsBenchmarkingFlagEnabled
Feature: Local Authority Education Health Care Plan Benchmarking

    Background:
        Given I am on education health care plan comparators selection page for local authority with code '202'
        When I click the Start Benchmarking button

    Scenario: Page displays all education health care plan charts
        Given I am on education health care plan benchmarking page for local authority with code '20'
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
        Given I am on education health care plan benchmarking page for local authority with code '202'
        When I change view to table
        Then the following is shown for 'Total pupils with EHC plans'
            | Local Authority        | Total pupils with EHC plans (per 1000 pupils) | Pupils |
            | Hackney                | 53.3                                          | 57,444 |
            | Greenwich              | 34.14                                         | 63,850 |
            | City of London         | 11.52                                         | 1,650  |
            | Hammersmith and Fulham | 0                                             | 18,500 |
            | Camden                 | No data submitted                             | 50,101 |
