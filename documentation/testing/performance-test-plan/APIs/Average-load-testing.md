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

- Response Time Distribution
- Error Rate
- CPU Utilisation
- Throughput

## APIs and Endpoints

| Category              | API Endpoint                                                             |
|-----------------------|--------------------------------------------------------------------------|
| **API Establishment** |                                                                          |
| Health Check          | `GET /health`                                                            |
| Trusts                | `GET /trust/{identifier}`                                                |
|                       | `POST /trusts/suggest`                                                   |
| Schools               | `GET /school/{identifier}`                                               |
|                       | `GET /schools`                                                           |
|                       | `POST /schools/suggest`                                                  |
| Local Authorities     | `GET /local-authority/{identifier}`                                      |
|                       | `POST /local-authorities/suggest`                                        |
| **API Benchmark**     |                                                                          |
| Health Check          | `GET /health`                                                            |
| User Data             | `GET /user-data`                                                         |
| Financial Plans       | `GET /financial-plan/{urn}/{year}`                                       |
|                       | `PUT /financial-plan/{urn}/{year}`                                       |
|                       | `DELETE /financial-plan/{urn}/{year}`                                    |
|                       | `GET /financial-plan/{urn}/{year}/deployment`                            |
|                       | `GET /financial-plans`                                                   |
| Custom Data           | `GET /custom-data/school/{urn}/{identifier}`                             |
|                       | `PUT /custom-data/school/{urn}/{identifier}`                             |
|                       | `DELETE /custom-data/school/{urn}/{identifier}`                          |
| Comparator Sets       | `GET /comparator-set/school/{urn}/default`                               |
|                       | `GET /comparator-set/school/{urn}/custom/{identifier}`                   |
|                       | `GET /comparator-set/school/{urn}/user-defined/{identifier}`             |
|                       | `PUT /comparator-set/school/{urn}/user-defined/{identifier}`             |
|                       | `DELETE /comparator-set/school/{urn}/user-defined/{identifier}`          |
|                       | `GET /comparator-set/trust/{companyNumber}/user-defined/{identifier}`    |
|                       | `PUT /comparator-set/trust/{companyNumber}/user-defined/{identifier}`    |
|                       | `DELETE /comparator-set/trust/{companyNumber}/user-defined/{identifier}` |
| Comparators           | `POST /comparators/schools`                                              |
|                       | `POST /comparators/trusts`                                               |
| **API Insight**       |                                                                          |
| Common                | `GET /current-return-years`                                              |
| Health Check          | `GET /health`                                                            |
| Trust                 | `GET /trusts/characteristics`                                            |
| Schools               | `GET /school/{urn}/characteristics`                                      |
|                       | `GET /schools/characteristics`                                           |
| Metric RAG Ratings    | `GET /metric-rag/{identifier}`                                           |
|                       | `GET /metric-rag/default`                                                |
| Income                | `GET /income/categories`                                                 |
|                       | `GET /income/dimensions`                                                 |
|                       | `GET /income/school/{urn}`                                               |
|                       | `GET /income/trust/{companyNumber}`                                      |
|                       | `GET /income/school/{urn}/history`                                       |
|                       | `GET /income/trust/{companyNumber}/history`                              |
|                       | `GET /income/schools`                                                    |
|                       | `GET /income/trusts`                                                     |
| Expenditure           | `GET /expenditure/categories`                                            |
|                       | `GET /expenditure/dimensions`                                            |
|                       | `GET /expenditure/school/{urn}`                                          |
|                       | `GET /expenditure/school/{urn}/custom/{identifier}`                      |
|                       | `GET /expenditure/trust/{companyNumber}`                                 |
|                       | `GET /expenditure/school/{urn}/history`                                  |
|                       | `GET /expenditure/trust/{companyNumber}/history`                         |
|                       | `GET /expenditure/schools`                                               |
|                       | `GET /expenditure/trusts`                                                |
| Census                | `GET /census/categories`                                                 |
|                       | `GET /census/dimensions`                                                 |
|                       | `GET /census/{urn}`                                                      |
|                       | `GET /census/{urn}/custom/{identifier}`                                  |
|                       | `GET /census/{urn}/history`                                              |
|                       | `GET /census`                                                            |
| Budget Forecast       | `GET /budget-forecast/{companyNumber}`                                   |
|                       | `GET /budget-forecast/{companyNumber}/metrics`                           |
|                       | `GET /budget-forecast/{companyNumber}/current-year`                      |
| Balance               | `GET /balance/dimensions`                                                |
|                       | `GET /balance/school/{urn}`                                              |
|                       | `GET /balance/trust/{companyNumber}`                                     |
|                       | `GET /balance/school/{urn}/history`                                      |
|                       | `GET /balance/trust/{companyNumber}/history`                             |
|                       | `GET /balance/schools`                                                   |
|                       | `GET /balance/trusts`                                                    |

For this test, we will focus exclusively on the Schools endpoints, as the Trusts and Local Authority endpoints are
identical to school. Additionally, we will not be testing the healthcheck endpoint.

### Total Average Calls to Each Establishment API Endpoint

| Endpoints                  | Average Estimate Requests | Comments                                |
|----------------------------|---------------------------|-----------------------------------------|
| `GET /school/{identifier}` | 49000                     | 7 requests x 7000 users at a given time |
| `GET /schools`             | 650                       | total trusts 2500/4                     |
| `POST /schools/suggest`    | 56000                     | 8 requests per user x 7000 users        |

### Total Average Calls to Each Benchmark API Endpoint

| Endpoints                                                       | Average Estimate Requests | Comments                                     |
|-----------------------------------------------------------------|---------------------------|----------------------------------------------|
| `GET /user-data`                                                | 28000                     | 4 x 7000                                     |
| `GET /financial-plan/{urn}/{year}`                              | 2800                      | 2 x1400(5% of 28000)                         |
| `PUT /financial-plan/{urn}/{year}`                              | 2800                      | 10 x1400(5% of 28000)                        |
| `DELETE /financial-plan/{urn}/{year}`                           | 280                       | 10% of 2800(users who created the plan       |
| `GET /financial-plan/{urn}/{year}/deployment`                   | 1400                      | 5% of 28000                                  |
| `GET /financial-plans`                                          | 1650                      | 1400(5% of 28000) + 250 (10% of 2500 trusts) |
| `GET /custom-data/school/{urn}/{identifier}`                    | 2800                      | 40% of 7000                                  |
| `PUT /custom-data/school/{urn}/{identifier}`                    | 2100                      | 30% of 7000                                  |
| `DELETE /custom-data/school/{urn}/{identifier}`                 | 2100                      | 30% of 7000                                  |
| `GET /comparator-set/school/{urn}/default`                      | 21000                     | 3x7000                                       |
| `GET /comparator-set/school/{urn}/custom/{identifier}`          | 2100                      | 30% of 7000                                  |
| `GET /comparator-set/school/{urn}/user-defined/{identifier}`    | 2100                      | 30% of 7000                                  |
| `PUT /comparator-set/school/{urn}/user-defined/{identifier}`    | 2100                      | 30% of 7000                                  |
| `DELETE /comparator-set/school/{urn}/user-defined/{identifier}` | 2100                      | 30% of 7000                                  |                                              |
| `POST /comparators/schools`                                     |                           |

### Total Average Calls to Each Insight API Endpoint

| Endpoints                                           | Average Estimate Requests | Comments    |
|-----------------------------------------------------|---------------------------|-------------|
| `GET /current-return-years`                         | 28000                     | 4 x 7000    |
| `GET /school/{urn}/characteristics`                 | 1400                      | 20% of 7000 |
| `GET /schools/characteristics`                      | 1400                      | 20% of 7000 |
| `GET /metric-rag/default`                           | 14000                     | 2 x 7000    |
| `GET /income/categories`                            | 7000                      |             |
| `GET /income/school/{urn}/history`                  | 7000                      |             |
| `GET /income/schools`                               | 7000                      |             |
| `GET /expenditure/categories`                       | 14000                     | 2x7000      |
| `GET /expenditure/dimensions`                       | 14000                     | 2x7000      |
| `GET /expenditure/school/{urn}`                     | 14000                     | 2x7000      |
| `GET /expenditure/school/{urn}/custom/{identifier}` | 14000                     | 2x7000      |
| `GET /expenditure/school/{urn}/history`             | 7000                      |             |
| `GET /expenditure/schools`                          | 7000                      |             |
| `GET /census/categories`                            | 7000                      |             |
| `GET /census/dimensions`                            | 7000                      |             |
| `GET /census/{urn}`                                 | 7000                      |             |
| `GET /census/{urn}/custom/{identifier}`             | 7000                      |             |
| `GET /census/{urn}/history`                         | 7000                      |             |
| `GET /census`                                       | 7000                      |             |
| `GET /budget-forecast/{companyNumber}`              | 1000                      |             |
| `GET /budget-forecast/{companyNumber}/metrics`      | 1000                      |             |
| `GET /budget-forecast/{companyNumber}/current-year` | 1000                      |             |
| `GET /balance/dimensions`                           | 7000                      |             |
| `GET /balance/school/{urn}`                         | 7000                      |             |
| `GET /balance/school/{urn}/history`                 | 7000                      |             |
| `GET /balance/schools`                              | 7000                      |             |

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
