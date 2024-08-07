# API Peak Load Testing Plan

## Description

This test simulates a gradual increase in the number of users to evaluate the system's performance at its expected peak
load. The goal is to ensure that the API endpoints can handle the load without significant degradation in performance
metrics.

## Procedure

### Determine Capacity

Calculate the total number of requests that each API endpoint can handle simultaneously.

### Ramp-Up Phase

For each endpoint, gradually increase the number of concurrent users over a ramp-up period of 5-10 seconds.

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

### Performance

- **Response Time Distribution**: Verify that 95% of responses are within 300 milliseconds.
- **Error Rate**: Maintain an error rate below 1%.
- **CPU Utilisation**: Ensure CPU usage remains below 80%.
- **Throughput**: get insights from average load test to compute the throughput.

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

| Endpoints                  | Average Estimate Requests | Comments                                 |
|----------------------------|---------------------------|------------------------------------------|
| `GET /school/{identifier}` | 140000                    | 7 requests x 20000 users at a given time |
| `GET /schools`             | 2500                      | 2500                                     |
| `POST /schools/suggest`    | 160000                    | 8 requests x 20000 users                 |

### Total Average Calls to Each Benchmark API Endpoint

| Endpoints                                                       | Average Estimate Requests | Comments                                       |
|-----------------------------------------------------------------|---------------------------|------------------------------------------------|
| `GET /user-data`                                                | 80000                     | 4 x 20000                                      |
| `GET /financial-plan/{urn}/{year}`                              | 28000                     | 2 x14000(50% of 28000)                         |
| `PUT /financial-plan/{urn}/{year}`                              | 28000                     | 10 x14000(50% of 28000)                        |
| `DELETE /financial-plan/{urn}/{year}`                           | 7000                      | 50% of 14000(users who created the plan)       |
| `GET /financial-plan/{urn}/{year}/deployment`                   | 14000                     | 50% of 28000                                   |
| `GET /financial-plans`                                          | 8250                      | 7000(50% of 14000) + 1250 (50% of 2500 trusts) |
| `GET /custom-data/school/{urn}/{identifier}`                    | 10000                     | 50% of 20000                                   |
| `PUT /custom-data/school/{urn}/{identifier}`                    | 10000                     | 50% of 20000                                   |
| `DELETE /custom-data/school/{urn}/{identifier}`                 | 8400                      | 30% of 28000                                   |
| `GET /comparator-set/school/{urn}/default`                      | 60000                     | 3x20000                                        |
| `GET /comparator-set/school/{urn}/custom/{identifier}`          | 6000                      | 30% of 20000                                   |
| `GET /comparator-set/school/{urn}/user-defined/{identifier}`    | 6000                      | 30% of 20000                                   |
| `PUT /comparator-set/school/{urn}/user-defined/{identifier}`    | 6000                      | 30% of 20000                                   |
| `DELETE /comparator-set/school/{urn}/user-defined/{identifier}` | 6000                      | 30% of 20000                                   |                                              |
| `POST /comparators/schools`                                     |                           |

### Total Average Calls to Each Insight API Endpoint

| Endpoints                                           | Average Estimate Requests | Comments     |
|-----------------------------------------------------|---------------------------|--------------|
| `GET /current-return-years`                         | 80000                     | 4 x 20000    |
| `GET /school/{urn}/characteristics`                 | 14000                     | 50% of 28000 |
| `GET /schools/characteristics`                      | 14000                     | 50% of 28000 |
| `GET /metric-rag/default`                           | 40000                     | 2 x 20000    |
| `GET /income/categories`                            | 20000                     |              |
| `GET /income/school/{urn}/history`                  | 20000                     |              |
| `GET /income/schools`                               | 20000                     |              |
| `GET /expenditure/categories`                       | 40000                     | 2x20000      |
| `GET /expenditure/dimensions`                       | 40000                     | 2x20000      |
| `GET /expenditure/school/{urn}`                     | 40000                     | 2x20000      |
| `GET /expenditure/school/{urn}/custom/{identifier}` | 40000                     | 2x20000      |
| `GET /expenditure/school/{urn}/history`             | 20000                     |              |
| `GET /expenditure/schools`                          | 20000                     |              |
| `GET /census/categories`                            | 20000                     |              |
| `GET /census/dimensions`                            | 20000                     |              |
| `GET /census/{urn}`                                 | 20000                     |              |
| `GET /census/{urn}/custom/{identifier}`             | 20000                     |              |
| `GET /census/{urn}/history`                         | 20000                     |              |
| `GET /census`                                       | 20000                     |              |
| `GET /budget-forecast/{companyNumber}`              | 20000                     |              |
| `GET /budget-forecast/{companyNumber}/metrics`      | 20000                     |              |
| `GET /budget-forecast/{companyNumber}/current-year` | 20000                     |              |
| `GET /balance/dimensions`                           | 20000                     |              |
| `GET /balance/school/{urn}`                         | 20000                     |              |
| `GET /balance/school/{urn}/history`                 | 20000                     |              |
| `GET /balance/schools`                              | 20000                     |              |

For each API endpoint, run the tests to simulate average load conditions.

## Test Execution

For each API endpoint we will increase the requests to the total number identified earlier gradually. Starting with 5000
requests, we will increase the load every 5 seconds until we reach the specified number. The system will then run for 5
minutes under that load before gradually decreasing the load in the same manner it was increased.

## Test Output

### Summary Report

A summary of test report will be shared.

### Findings and recommendations

Findings from the test run and recommendations will be shared with the wider team. 
