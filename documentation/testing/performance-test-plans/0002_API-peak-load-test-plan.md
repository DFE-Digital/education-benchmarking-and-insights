# Performance Test Plan - 0002 - API Peak Load

## Goal

The goal of this test is to measure how well an APIs perform under peak load conditions. Focus will be on verifying the
high demand and critical endpoints.

## Objective

- Ensure that the APIs remains stable without crashes or timeouts during peak usage.
- Ensure that the APIs do not degrade significantly under peak load usage.
- Ensure that the API can handle the maximum anticipated load without failure, confirming its ability to scale
  appropriately under peak conditions.

## Procedure

**Determine Capacity:**

To calculate the number of `Virtual Users` required for Azure Load Testing based on a target throughput, use the
following formula:

Number of Virtual Users = Target Throughput (requests per second) / Requests per User per Second

- `Target Throughput` is the desired number of requests per second (RPS) you want the system to handle.
- `Requests per User per Second` is the number of requests that each virtual user can make per second.

To calculate the `Requests per User per Second`, use the following formula:

Requests per User per Second = 1 / (Response Time + Think Time)

- `Response Time` is average response time in seconds
- `Think Time` is the user wait time in second between requests

**Average Load Phase:**

For each endpoint, simulate the typical number of concurrent virtual users to mimic average load conditions over a
period of 5 minutes.

**Monitoring Performance:**

- Response Time
    - Definition: The duration between sending a request to the API and receiving a response.
    - Measurement: This will be produced as an output of the test run.
- Error Percentage
    - Definition: The percentage of API requests that result in an error response (5xx).
    - Measurement: This will be produced as an output of the test run.
- Throughput
    - Definition: The number of API requests processed per second.
    - Measurement: This will be produced as an output of the test run.

**APIs & Endpoints Under Test:**

| API           | Endpoint                                   | Target Throughput (requests per second) |
|---------------|--------------------------------------------|-----------------------------------------|
| Establishment | `POST /school/suggest`                     | 113                                     |
| Establishment | `GET /school/{identifier}`                 | 113                                     |
| Benchmark     | `GET /user-data`                           | 113                                     |
| Benchmark     | `POST /comparators/schools`                | 113                                     |
| Benchmark     | `GET /comparator-set/school/{urn}/default` | 113                                     |
| Insights      | `GET /school/{urn}/characteristics`        | 158                                     |
| Insights      | `GET /expenditure/school/{urn}`            | 158                                     |
| Insights      | `GET /metric-rag/default`                  | 158                                     |

## Performance Success Criteria

- Response Time:
    - P75 below 200ms
    - P95 below 300ms
    - P99 below 500ms
- Error Rate:  below 1%

## Test Execution

- Load pattern: Linear
- Test duration (minutes): 2
- Ramp-up time (minutes): 1

## Test Output

**Summary Report:**

//TODO: Add summary post test run

**Findings and Recommendations:**

//TODO: Add finding and recommendation post test run