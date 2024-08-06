# API Average Load Testing Plan

## Description
This test simulates typical user activity to evaluate the system's performance under expected normal loads. The goal is to ensure that the API endpoints perform efficiently and reliably during everyday usage scenarios.
## Objective
- Assess system stability under typical load conditions.
- Identify performance bottlenecks during normal usage.
- Ensure system reliability and responsiveness during average load.
- Understand performance metrics and capacity thresholds under regular load.
- Improve system design based on testing insights.
- Maintain an optimal user experience during average load scenarios.
## Procedure

### Determine Capacity
Calculate the average number of requests that each API endpoint typically handles simultaneously. For the average load test, simulate the number of users equivalent to the expected daily usage pattern.

### Average Load Phase
For each endpoint, simulate the typical number of concurrent users to mimic average load conditions over a period of 5 minutes. 

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

In the context of average load testing, we aim to monitor and document the system's behavior under normal user load conditions. This will help in understanding the system's performance and identifying potential areas for improvement. The following metrics will be observed. 
### Performance
- Response Time Distribution
- Error Rate
- CPU Utilisation
- Throughput

## APIs and Endpoints

### Api.Establishment

#### Health Check
- `GET /health`

### Schools
- `GET /school/{identifier}`
- `GET /schools`
- `POST /schools/suggest`

### Trusts
- `GET /trust/{identifier}`
- `POST /trusts/suggest`

### Local Authorities
- `GET /local-authority/{identifier}`
- `POST /local-authority/suggest`

For this test, we will focus exclusively on the Schools endpoints, as the Trusts and Local Authority endpoints are identical to school. Additionally, we will not be testing the healthcheck endpoint.
### Total Average Calls to Each Endpoint
- `GET /school/{identifier}` 
- `GET /schools`
- `POST /schools/suggest`

For each API endpoint, run the tests to simulate average load conditions.

## Test Execution
For each API endpoint we will increase the requests to the total number identified earlier gradually. Starting with 20 requests, we will increase the load every 5 seconds until we reach the specified number. The system will then run for 5 minutes under that load before gradually decreasing the load in the same manner it was increased.


## Test Output

### Summary Report
We will provide a summary of the test results, including a detailed report on the system's performance against the specified metrics.

### Findings and Recommendations
We will share the findings from the test along with recommendations with the wider team.
