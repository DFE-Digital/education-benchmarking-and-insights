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
For each endpoint, simulate the typical number of concurrent users to mimic average load conditions over a period of 5 days.

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

{revisit the performance success criteria} 
### Performance
- **Response Time Distribution**: Verify that 95% of responses are within acceptable limits for normal usage, which is within 1 second.
- **Error Rate**: Maintain an error rate below 1%.
- **CPU Utilisation**: Ensure CPU usage remains within 45%.
- **Throughput**: Achieve a throughput that meets the typical demands of average user activity. 

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

### Total Average Calls to Each Endpoint
- `GET /school/{identifier}`
- `GET /schools`
- `POST /schools/suggest`
- `GET /trust/{identifier}`
- `POST /trusts/suggest`
- `GET /local-authority/{identifier}`
- `POST /local-authority/suggest`

For each API endpoint, run the tests to simulate average load conditions.

## Test Output

### Summary Report
A summary of the test report will be shared.
### Findings and Recommendations
Findings from the test run and recommendations will be shared with the wider team.
