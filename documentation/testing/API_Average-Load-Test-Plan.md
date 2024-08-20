# API Average Load Testing Plan

## Description

This test simulates typical user activity to evaluate the system's performance under expected normal loads. The goal is
to ensure that the API endpoints perform efficiently and reliably during everyday usage scenarios.

## Objective

- Assess system stability under typical load conditions.
- Identify performance bottlenecks during normal usage.
- Ensure system reliability and responsiveness during average load.
- Understand performance metrics and capacity thresholds under regular load.
- Improve system design based on testing insights.
- Maintain an optimal user experience during average load scenarios.

## Procedure

### Determine Capacity

Calculate the average number of requests that each API endpoint typically handles simultaneously. For the average load
test, simulate the number of users equivalent to the expected daily usage pattern.

### Average Load Phase

For each endpoint, simulate the typical number of concurrent users to mimic average load conditions over a period of 5
minutes.

### Monitoring Performance

#### Response Time

- **Definition**: The duration between sending a request to the API and receiving a response.
- **Measurement**: Review application insights during the test run and once the test is completed.

#### Error Rate

- **Definition**: The percentage of API requests that result in an error response (e.g., HTTP 4xx or 5xx status codes)
  compared to the total number of requests.
- **Measurement**: This will be produced as an output of the test run.

#### CPU Utilisation

- **Definition**: The percentage of CPU resources used by the system while handling API requests.
- **Measurement**: Use application insights or an operational dashboard to measure CPU utilisation during the test run.

#### Throughput

- **Definition**: The number of API requests processed per second.
- **Measurement**: Use application insights or the output of the tests to measure throughput during the test run.

## Success Criteria

In the context of average load testing, we aim to monitor and document the system's behavior under normal user load
conditions. This will help in understanding the system's performance and identifying potential areas for improvement.
The following metrics will be observed.

### Performance

- Response Time
- Error Rate
- CPU Utilisation
- Throughput

## APIs and Endpoints

For this test, we will focus exclusively on the Schools endpoints, as the Trusts and Local Authority endpoints are
identical to school. Additionally, we will not be testing the healthcheck endpoint.

### Total Average Calls to Each Establishment API Endpoint

| Endpoints                  | Average Estimate Requests | Comments                                                                                                                                             |
|----------------------------|---------------------------|------------------------------------------------------------------------------------------------------------------------------------------------------|
| `GET /school/{identifier}` | 49000                     | This endpoint is almost called everytime the user interacts with the service. if a users visits 7 pages then 7 requests x 7000 users at a given time |
| `GET /schools`             | 650                       | Called on trust journeys to get the schools. total trusts ~2500/4                                                                                    |
| `POST /schools/suggest`    | 56000                     | Used when a user is searching for school. Assuming 8 requests per user x 7000 users                                                                  |

### Total Average Calls to Each Benchmark API Endpoint

| Endpoints                                                       | Average Estimate Requests | Comments                                                                                                                           |
|-----------------------------------------------------------------|---------------------------|------------------------------------------------------------------------------------------------------------------------------------|
| `GET /user-data`                                                | 28000                     | Used mainly on benchmarking pages. on average there will be 4 requests  x 7000 users                                               |
| `GET /financial-plan/{urn}/{year}`                              | 14000                     | Used on financial plan pages. Estimating 10 requests x 1400(5% of 28000 users will create the financial plan)                      |
| `PUT /financial-plan/{urn}/{year}`                              | 14000                     | Used on financial plan pages. Estimating 10 pages are visited x 1400 users(5% of 28000)                                            |
| `DELETE /financial-plan/{urn}/{year}`                           | 280                       | Used when financial plan is deleted. Estimating 10% of 1400(users who created the plan) will delete the created plan.              |
| `GET /financial-plan/{urn}/{year}/deployment`                   | 1400                      | used when viewing the deployment plan. Estimating 5% of 28000 will view the deployment plan.                                       |
| `GET /financial-plans`                                          | 1650                      | Used when viewing the financial plans. Estimating 1400 users(5% of 28000) + 250 (10% of 2500 trusts) will view the financial plans |
| `GET /custom-data/school/{urn}/{identifier}`                    | 2800                      | Used when viewing the custom data. Estimating 40% of 7000 users use custom data.                                                   |
| `PUT /custom-data/school/{urn}/{identifier}`                    | 2800                      | Used during the custom data journey. Estimating 40% of 7000 users use custom data.                                                 |
| `DELETE /custom-data/school/{urn}/{identifier}`                 | 2100                      | Used when deleting the custom data. Estimating 30% of 7000 users will delete the custom data                                       |
| `GET /comparator-set/school/{urn}/default`                      | 28000                     | used when getting the comparator set on benchmarking pages. Estimating 4 requests  x 7000 users.                                   |
| `GET /comparator-set/school/{urn}/custom/{identifier}`          | 2100                      | Used in custom comparator set journey. Estimating 30% of 7000 will create a custom comparator.                                     |
| `GET /comparator-set/school/{urn}/user-defined/{identifier}`    | 2100                      | Used in user defined comparator set. Estimating 30% of 7000 will use user-defined comparator set.                                  |
| `PUT /comparator-set/school/{urn}/user-defined/{identifier}`    | 2100                      | Used in user defined comparator set. Estimating 30% of 7000 will use user-defined comparator set.                                  |
| `DELETE /comparator-set/school/{urn}/user-defined/{identifier}` | 2100                      | Used in user defined comparator set. Estimating 30% of 7000 will use user-defined comparator set.                                  |
| `POST /comparators/schools`                                     | x                         | x                                                                                                                                  |

### Total Average Calls to Each Insight API Endpoint

| Endpoints                                           | Average Estimate Requests | Comments                                                                                                             |
|-----------------------------------------------------|---------------------------|----------------------------------------------------------------------------------------------------------------------|
| `GET /current-return-years`                         | 28000                     | Used on every page where data is displayed. estimating  4 pages x 7000 users.                                        |
| `GET /school/{urn}/characteristics`                 | 2800                      | Used on custom data and user defined comparator set. Estimating 40% of average users 7000 will use this.             |
| `GET /schools/characteristics`                      | 2800                      | Used on trust benchmarking and view comparator page. Estimating 40% of average users 7000 will use this.             |
| `GET /metric-rag/default`                           | 14000                     | Used on spending prioritise, school homepage and trust journey. Estimating 2 hits x 7000 users.                      |
| `GET /income/categories`                            | 7000                      | Used on History page. Estimating 7000 users will use it.                                                             |
| `GET /income/school/{urn}/history`                  | 7000                      | Used on History page. Estimating 7000 users will use it.                                                             |
| `GET /income/schools`                               | 7000                      | Used on History page. Estimating 7000 users will use it.                                                             |
| `GET /expenditure/categories`                       | 7000                      | Used on comparison page. Estimating 7000  users.                                                                     |
| `GET /expenditure/dimensions`                       | 14000                     | Used on comparison and history page. Estimating 2 x 7000  users.                                                     |
| `GET /expenditure/school/{urn}`                     | 126000                    | Used on comparison page and history page. For each of the cost category 9 x 2(comparision and history) x 7000 users. |
| `GET /expenditure/school/{urn}/custom/{identifier}` | 18900                     | Used in custom data pages. there are 9 cost categories x 2100(30 % users who will use custom journey)                |
| `GET /expenditure/school/{urn}/history`             | 7000                      | Used on history page. Estimating 7000 users.                                                                         |
| `GET /expenditure/schools`                          | 7000                      | Estimating 7000 users will hit this endpoint.                                                                        |
| `GET /census/categories`                            | 7000                      | Used on census comparison page. Estimating 7000 users will use it.                                                   |
| `GET /census/dimensions`                            | 7000                      | Used on census comparison page. Estimating 7000 users will use it.                                                   |
| `GET /census/{urn}`                                 | 7000                      | Used on census comparison page. Estimating 7000 users will use it.                                                   |
| `GET /census/{urn}/custom/{identifier}`             | 2100                      | Used on custom data journey. Estimating 2100 users (30% of 7000) will use it.                                        |
| `GET /census/{urn}/history`                         | 7000                      | Used on history tab.  Estimating 7000 users will use it.                                                             |
| `GET /census`                                       | 56000                     | Used on census page. There are 8 categories. Estimating 8  x 7000 users.                                             |
| `GET /budget-forecast/{companyNumber}`              | 7000                      | Used on budget forecast. Estimating 7000 users will use it.                                                          |
| `GET /budget-forecast/{companyNumber}/metrics`      | 7000                      | Used on budget forecast. Estimating 7000 users will use it.                                                          |
| `GET /budget-forecast/{companyNumber}/current-year` | 7000                      | Used on budget forecast. Estimating 7000 users will use it.                                                          |
| `GET /balance/dimensions`                           | 7000                      | Used on History page.  Estimating 7000 users will use it.                                                            |
| `GET /balance/school/{urn}`                         | 7000                      | Estimating 7000 users will use it.                                                                                   |
| `GET /balance/school/{urn}/history`                 | 7000                      | used on history page. Estimating 7000 users will use it.                                                             |
| `GET /balance/schools`                              | 7000                      | Estimating 7000 users will use it.                                                                                   |

For each API endpoint, run the tests to simulate average load conditions.

## Test Execution

For each API endpoint we will increase the requests to the total number identified earlier gradually. Starting with 5000
requests, we will increase the load every 5 seconds until we reach the specified number. The system will then run for 5
minutes under that load before gradually decreasing the load in the same manner it was increased.

## Test Output

### Summary Report

We will provide a summary of the test results, including a detailed report on the system's performance against the
specified metrics.

### Findings and Recommendations

We will share the findings from the test along with recommendations with the wider team.