# Performance Test Plan - 0002 - API Peak Load

## Goal

The goal of this test is to measure how well an APIs perform under peak load conditions. Focus will be on verifying the
high demand and critical endpoints.

## Objective
Tests system behavior during expected high-traffic periods, allowing us to validate capacity and maintain performance during peak demand.

- Ensure that the APIs remains stable without crashes or timeouts during peak usage.
- Confirm that the APIs do not degrade significantly, and maintain performance, during expected high-traffic periods.
- Validate capacity during anticipated peak load.

## Procedure

**Determine Capacity:**

Calculate the number of `Virtual Users` required for Azure Load Testing based on a target throughput.

_NB: see previous test plans for calculation._

**Average Load Phase:**

For each endpoint, simulate the typical number of concurrent virtual users to mimic peak load conditions over a
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

| API           | Endpoint                                   | Target Throughput |
|---------------|--------------------------------------------|-------------------|
| Establishment | `POST /school/suggest`                     | 113 /s            |
| Establishment | `GET /school/{identifier}`                 | 113 /s            |
| Benchmark     | `GET /user-data`                           | 113 /s            |
| Benchmark     | `POST /comparators/schools`                | 113 /s            |
| Benchmark     | `GET /comparator-set/school/{urn}/default` | 113 /s            |
| Insights      | `GET /school/{urn}/characteristics`        | 158 /s            |
| Insights      | `GET /expenditure/school/{urn}`            | 158 /s            |
| Insights      | `GET /metric-rag/default`                  | 158 /s            |

## Performance Success Criteria

- Response Time:
    - P75 below 200ms
    - P95 below 300ms
    - P99 below 500ms
- Error Rate:  below 1%

## Test Execution

- Load pattern: Linear
- Test duration (minutes): 5
- Ramp-up time (minutes): 1

## Test Output

**Summary Report:**

//TODO: Add summary post test run

**Findings and Recommendations:**

//TODO: Add finding and recommendation post test run