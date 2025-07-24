# Performance Test Plan: Local Authority High needs dashboard load tests (0008)

**Test run date:**

March 2025

## Goal

The goal of this test is to measure the performance of the High needs dashboard under load.
This page includes has a number of API calls which may benefit from an element of caching if performance begins to suffer

## Objective

- Establish a performance baseline under typical load conditions.
- Simulate real-world conditions to understand the services responsiveness during average traffic patterns.
- Ensure that the service remains stable without crashes or timeouts during normal usage.
- Confirm that the service deliver consistent performance over time and does not degrade with continued use.

## Procedure

**Determine Capacity:**

Calculate the number of `Virtual Users` required for Azure Load Testing based on a target throughput.

_NB: see previous test plans for calculation._

**Average Load Phase:**

For each endpoint, simulate the typical number of concurrent virtual users to mimic average load conditions over a period of 5 minutes.

**Peak Load Phase:**

For each endpoint, simulate the typical number of concurrent virtual users to mimic peak load conditions over a period of 5 minutes.
Manually scale core database to S2 tier to replicate production infrastucture for the duration of the peak load tests.

**Monitoring Performance:**

- Response Time
  - Definition: The duration between sending a request and receiving a response.
  - Measurement: This will be produced as an output of the test run.
- Error Percentage
  - Definition: The percentage of requests that result in an error response (5xx).
  - Measurement: This will be produced as an output of the test run.
- Throughput
  - Definition: The number of requests processed per second.
  - Measurement: This will be produced as an output of the test run.
- CPU and Memory utilisation
  - Definition: The average percentage utilisation.
  - Measurement: Monitor resource usage to ensure the infrastructure efficiently handles the load.
- Database performance
  - Definition: The average percentage of DTU utilisation.
  - Measurement: Monitor resource usage to ensure the infrastructure efficiently handles the load.

**Pages Under Test:**

| Page                                       | Target Throughput | Load Type | DB DTUs |
|--------------------------------------------|-------------------|-----------|---------|
| `/local-authority/{identifier}/high-needs` | 20 /s             | Average   | S1      |
| `/local-authority/{identifier}/high-needs` | 50 /s             | Peak      | S2      |

## Performance Success Criteria

- Response Time:
  - P75 below 500ms
  - P95 below 1000ms
  - P99 below 2000ms
- Error Rate: below 0.1%
- CPU and Memory utilisation: below 80%*
- Database performance: below 80%*

## Test Execution

- Load pattern: Linear
- Test duration (minutes): 6
- Ramp-up time (minutes): 1

## Test Output

<!-- take care with final separator line in piped table, as pandoc uses this for relative column widths -->
| Load Test Name | Initiated on         | Max VUs | Duration | Response time | Errors | Throughput | Result          |
|----------------|----------------------|---------|----------|---------------|--------|------------|-----------------|
| Average        | 21/03/2025, 12:31:36 | 4       | 5m 49s   | 209 ms        | 0.00 % | 21.49 /s   | [✅ Passed](https://portal.azure.com/#blade/Microsoft_Azure_CloudNativeTesting/NewReport/resourceId/%2Fsubscriptions%2Fa5c0a8d7-a54d-4a6d-ab79-4ca64a3b750f%2Fresourcegroups%2Fs198t01-ebis-perf-tests%2Fproviders%2Fmicrosoft.loadtestservice%2Floadtests%2Fs198t01-load-tests/testId/c4f39d49-7ae1-496b-b041-c30da019300e/testRunId/c4f39d49-7ae1-496b-b041-c30da0193032) |
| Peak           | 21/03/2025, 12:45:15 | 10      | 5m 56s   | 260 ms        | 0.00 % | 45.20 /s   | [✅ Passed](https://portal.azure.com/#blade/Microsoft_Azure_CloudNativeTesting/NewReport/resourceId/%2Fsubscriptions%2Fa5c0a8d7-a54d-4a6d-ab79-4ca64a3b750f%2Fresourcegroups%2Fs198t01-ebis-perf-tests%2Fproviders%2Fmicrosoft.loadtestservice%2Floadtests%2Fs198t01-load-tests/testId/00a24851-b775-455c-a32a-e4baa28fe03d/testRunId/00a24851-b775-455c-a32a-e4baa28fe074) |

**Findings and Recommendations:**

Performance was within the acceptable range and no errors were reported.

<!-- Leave the rest of this page blank -->
\newpage
