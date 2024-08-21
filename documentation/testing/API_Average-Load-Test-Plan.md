# API Average Load Testing Plan

## Description

This test simulates typical user activity to evaluate the system's performance under expected normal loads. The goal is
to ensure that the API endpoints perform efficiently and reliably during everyday usage scenarios.

## Goal

Ensure that the system consistently performs well under typical operating conditions, with a particular focus on
verifying that the most used and critical endpoints are reliable, responsive, and functioning as expected.

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

- Response Time - less than ~500ms
- Error Rate - below 1%
- CPU Utilisation - below 35%
- Throughput - minimum of 45 requests per second

## APIs and Endpoints

For this test, we will focus exclusively on the Schools endpoints, as the Trusts and Local Authority endpoints are
identical to school. Additionally, we will not be testing the healthcheck endpoint.

### Total Average Calls to Critical Establishment API Endpoint

The average requests on this API is 4500 requests per minute.

| Endpoints               | Average Estimate Requests | Comments                                                                                |
|-------------------------|---------------------------|-----------------------------------------------------------------------------------------|
| `POST /schools/suggest` | 4000                      | This endpoint is most used as everytime a user search for school this is hit few times. |

### Total Average Calls to Critical Benchmark API Endpoint
The average requests on this API is 4500 requests per minute.

| Endpoints                                                    | Average Estimate Requests | Comments                                                                                                                                                                                      |
|--------------------------------------------------------------|---------------------------|-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `GET /financial-plan/{urn}/{year}`                           | 500                       | Used on financial plan journey. This goes hand in hand with Put. Estimated traffic on this 500 requests                                                                                       |
| `PUT /financial-plan/{urn}/{year}`                           | 500                       | Used on financial plan journey. This goes hand in hand with Get. Estimated traffic on this 500 requests                                                                                       |
| `GET /custom-data/school/{urn}/{identifier}`                 | 500                       | Used when viewing the custom data. Estimating 500 requests on this endpoint.                                                                                                                  |
| `GET /comparator-set/school/{urn}/default`                   | 1500                      | used when getting the comparator set on benchmarking pages. once the user has created a comparator set there could be multiple visits on the page. Estimating 1500 requests on this endpoint. |
| `GET /comparator-set/school/{urn}/custom/{identifier}`       | 500                       | Used in custom data set journey if the user has changed data which has affected the comparator set. Estimating 500 request on this endpoint.                                                  |
| `GET /comparator-set/school/{urn}/user-defined/{identifier}` | 1000                      | Used in user defined comparator set. Estimating 1000 requests on this endpoint.                                                                                                               |

### Total Average Calls to Critical Insight API Endpoint

The average requests on this API is 6300 requests per minute.

| Endpoints                               | Average Estimate Requests | Comments                                                                                                                                                                              |
|-----------------------------------------|---------------------------|---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `GET /current-return-years`             | 1000                      | Used on every page where data is displayed. This will be most used endpoint so estimating 1000 requests on this endpoint.                                                             |
| `GET /metric-rag/default`               | 1500                      | Used on spending prioritise, school homepage and trust journey. Homepage is mostly used so estimating 1500 requests on this endpoint                                                  |
| `GET /expenditure/categories`           | 200                       | Used on comparison page which is a popular page.                                                                                                                                      |
| `GET /expenditure/dimensions`           | 200                       | Used on comparison and history page to get the dimensions.                                                                                                                            |
| `GET /expenditure/school/{urn}`         | 2000                      | Used on comparison page and history page. To get the expenditure of a given school. This endpoint is called 10 times on comparision page.  Estimating 2000 requests on this endpoint. |
| `GET /expenditure/school/{urn}/history` | 500                       | Used on history page which also is a popular page looking at the logs. Estimating 500 requests.                                                                                       |

For each API endpoint, run the tests to simulate average load conditions.

## Test Execution

For each API endpoint we will increase the requests to the total number identified earlier gradually. Starting with 105
requests, we will increase the load every second until we reach the specified number. The system will then run for 5
minutes under that load before gradually decreasing the load in the same manner it was increased.

## Test Output

### Summary Report

We will provide a summary of the test results, including a detailed report on the system's performance against the
specified metrics.

### Findings and Recommendations

We will share the findings from the test along with recommendations with the wider team.