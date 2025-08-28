Feature: Insights budget forecast endpoints

    Scenario: Sending a valid budget forecast request with default options
        Given a valid budget forecast request for company number '00000001' with runType 'default', category 'Revenue reserve' and runId '2023'
        When I submit the budget forecast request
        Then the budget forecast result should be ok and contain:
          | Year | Forecast  | Actual    | ForecastTotalPupils | ActualTotalPupils | Variance | PercentVariance               | VarianceStatus                  |
          | 2020 | 0.00      |           | 0.00                |                   |          |                               |                                 |
          | 2021 | 763000.00 |           | 317.00              |                   |          |                               |                                 |
          | 2022 | 653000.00 | 733588.00 | 333.00              | 355.00            | 80588.00 | 10.98545777739003364286220603 | AR significantly above forecast |
          | 2023 | 608000.00 |           | 337.00              |                   |          |                               |                                 |
          | 2024 | 547000.00 |           | 336.00              |                   |          |                               |                                 |
          | 2025 | 490000.00 |           | 336.00              |                   |          |                               |                                 |

    Scenario: Sending a valid budget forecast metrics request
        Given a valid budget forecast metrics request for company number '00000001'
        When I submit the budget forecast request
        Then the budget forecast metrics result should be ok and contain:
          | RunType | RunId | Year | CompanyNumber | Metric                                        | Value |
          |         |       | 2023 |               | Expenditure as percentage of income           | 99.42 |
          |         |       | 2023 |               | Grant funding as percentage of income         | 95.78 |
          |         |       | 2023 |               | Revenue reserve as percentage of income       | 36.63 |
          |         |       | 2023 |               | Self generated income as percentage of income | 4.22  |
          |         |       | 2023 |               | Slope                                         | 1.20  |
          |         |       | 2023 |               | Slope flag                                    | 3.76  |
          |         |       | 2023 |               | Staff costs as percentage of income           | 82.48 |