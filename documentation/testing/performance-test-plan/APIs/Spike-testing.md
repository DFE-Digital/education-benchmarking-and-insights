# API Spike Testing Plan

## Description
This test simulates a sudden and significant increase in the number of users to evaluate the system's performance under unexpected peak loads. The goal is to ensure that the API endpoints can handle the load without significant degradation in performance metrics.
## Objective
- Assess system stability under sudden and extreme load increases.
- Identify performance bottlenecks when load spikes occur.
- Ensure system resilience and graceful recovery after spikes.
- Understand performance limits and capacity thresholds.
- Improve system design based on testing insights.
- Maintain acceptable user experience during high-load scenarios.
## Procedure

### Determine Capacity
Calculate the total number of requests that each API endpoint can handle simultaneously. For spike test we will do 1/3 the number of users in 30 minutes duration.

### Spike Phase
For each endpoint, abruptly increase the number of concurrent users to simulate a spike load.

### Monitoring Performance

#### Response Time
- **Definition**: The duration between sending a request to the API and receiving a response.
- **Measurement**: Review application insights during the test run and once the test is completed.

#### Error Rate
- **Definition**: The percentage of API requests that result in an error response (e.g., HTTP 4xx or 5xx status codes) compared to the total number of requests.
- **Measurement**: This will be produced as an output of the test run.

#### CPU Utilisation
- **Definition**: The percentage of CPU resources used by the system while handling API requests.
- **Measurement**: Use application insights or an operational dashboard to measure CPU utilisation during the test run.

#### Throughput
- **Definition**: The number of API requests processed per second.
- **Measurement**: Use application insights or the output of the tests to measure throughput during the test run.

## Success Criteria

### Performance

- **Response Time Distribution**: Verify that 95% of responses are within 600 ms.
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
For spike we will take 50% of peak load.

| Endpoints                  | Average Estimate Requests |
|----------------------------|---------------------------|
| `GET /school/{identifier}` | 70000                     |
| `GET /schools`             | 1250                      |
| `POST /schools/suggest`    | 80000                     |


### Total Average Calls to Each Benchmark API Endpoint

| Endpoints                                                       | Average Estimate Requests |
|-----------------------------------------------------------------|---------------------------|
| `GET /user-data`                                                | 40000                     |
| `GET /financial-plan/{urn}/{year}`                              | 14000                     |
| `PUT /financial-plan/{urn}/{year}`                              | 14000                     |
| `DELETE /financial-plan/{urn}/{year}`                           | 3500                      |
| `GET /financial-plan/{urn}/{year}/deployment`                   | 7000                      |
| `GET /financial-plans`                                          | 4125                      |
| `GET /custom-data/school/{urn}/{identifier}`                    | 5000                      |
| `PUT /custom-data/school/{urn}/{identifier}`                    | 5000                      |
| `DELETE /custom-data/school/{urn}/{identifier}`                 | 4200                      |
| `GET /comparator-set/school/{urn}/default`                      | 30000                     |
| `GET /comparator-set/school/{urn}/custom/{identifier}`          | 3000                      |
| `GET /comparator-set/school/{urn}/user-defined/{identifier}`    | 3000                      |
| `PUT /comparator-set/school/{urn}/user-defined/{identifier}`    | 3000                      |
| `DELETE /comparator-set/school/{urn}/user-defined/{identifier}` | 3000                      |
| `POST /comparators/schools`                                     |                           |

### Total Average Calls to Each Insight API Endpoint

| Endpoints                                           | Average Estimate Requests |
|-----------------------------------------------------|---------------------------|
| `GET /current-return-years`                         | 40000                     |
| `GET /school/{urn}/characteristics`                 | 7000                      |
| `GET /schools/characteristics`                      | 7000                      |
| `GET /metric-rag/default`                           | 20000                     |
| `GET /income/categories`                            | 10000                     |
| `GET /income/school/{urn}/history`                  | 10000                     |
| `GET /income/schools`                               | 10000                     |
| `GET /expenditure/categories`                       | 20000                     |
| `GET /expenditure/dimensions`                       | 20000                     |
| `GET /expenditure/school/{urn}`                     | 20000                     |
| `GET /expenditure/school/{urn}/custom/{identifier}` | 20000                     |
| `GET /expenditure/school/{urn}/history`             | 10000                     |
| `GET /expenditure/schools`                          | 10000                     |
| `GET /census/categories`                            | 10000                     |
| `GET /census/dimensions`                            | 10000                     |
| `GET /census/{urn}`                                 | 10000                     |
| `GET /census/{urn}/custom/{identifier}`             | 10000                     |
| `GET /census/{urn}/history`                         | 10000                     |
| `GET /census`                                       | 10000                     |
| `GET /budget-forecast/{companyNumber}`              | 10000                     |
| `GET /budget-forecast/{companyNumber}/metrics`      | 10000                     |
| `GET /budget-forecast/{companyNumber}/current-year` | 10000                     |
| `GET /balance/dimensions`                           | 10000                     |
| `GET /balance/school/{urn}`                         | 10000                     |
| `GET /balance/school/{urn}/history`                 | 10000                     |
| `GET /balance/schools`                              | 10000                     |


For each API endpoint, run the tests to simulate average load conditions.

## Test Execution

For each API endpoint we will spike the requests to the total number identified above. Starting with 50000
requests, we will increase the load every 2 seconds until we reach the specified number. The system will then run for 5
minutes under that load before gradually decreasing the load in the same manner it was increased.


## Test Output

### Summary Report
A summary of test report will be shared.
### Findings and recommendations
Findings from the test run and recommendations will be shared with the wider team.
