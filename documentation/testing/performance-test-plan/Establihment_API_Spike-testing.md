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
- **Success Criteria**: Ensure CPU usage remains below 80%.
- **Measurement**: Use application insights or an operational dashboard to measure CPU utilisation during the test run.

#### Throughput
- **Definition**: The number of API requests processed per second.
- **Measurement**: Use application insights or the output of the tests to measure throughput during the test run.

## Success Criteria

### Performance
- **Response Time Distribution**: Verify that 95% of responses are within 1 second.
- **Error Rate**: Maintain an error rate below 1%.
- **CPU Utilisation**: Ensure CPU usage remains below 80%.
- **Throughput**: Achieve a throughput of at least {X} requests per second.

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

### Total Peak Calls to Each Endpoint
- `GET /school/{identifier}`
- `GET /schools`
- `POST /schools/suggest`
- `GET /trust/{identifier}`
- `POST /trusts/suggest`
- `GET /local-authority/{identifier}`
- `POST /local-authority/suggest`

For each API endpoint, run the tests.

## Test Output

### Summary Report
A summary of test report will be shared.
### Findings and recommendations
Findings from the test run and recommendations will be shared with the wider team.
