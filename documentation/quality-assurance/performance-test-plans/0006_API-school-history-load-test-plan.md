﻿# Performance Test Plan - 0006 - API School History Load Tests

**Test run date:**

December 2024

## Goal

The goal of this test is to measure how well the APIs perform under typical load conditions. Focus will be on verifying the high demand and critical endpoints.

## Objective

- Establish a performance baseline under typical load conditions.
- Simulate real-world conditions to understand the APIs responsiveness during average traffic patterns.
- Ensure that the APIs remains stable without crashes or timeouts  during normal usage.
- Confirm that the APIs deliver consistent performance over time and does not degrade with continued use.
- Ensure that the APIs can scale as expected under average load.

## Procedure

**Determine Capacity:**

To calculate the number of `Virtual Users` required for Azure Load Testing based on a target throughput, use the following formula:

Number of Virtual Users = Target Throughput (requests per second) / Requests per User per Second

- `Target Throughput` is the desired number of requests per second (RPS) you want the system to handle.
- `Requests per User per Second` is the number of requests that each virtual user can make per second.

To calculate the `Requests per User per Second`, use the following formula:

Requests per User per Second = 1 / (Response Time + Think Time)

- `Response Time` is average response time in seconds
- `Think Time` is the user wait time in second between requests

**Average Load Phase:**

For each endpoint, simulate the typical number of concurrent virtual users to mimic average load conditions over a period of 5 minutes.

**Peak Load Phase:**

For each endpoint, simulate the typical number of concurrent virtual users to mimic peak load conditions over a period of 5 minutes.
Manually scale core database to S2 tier to replicate production infrastucture for the duration of the peak load tests.

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

| API      | Endpoint                                                       | Type    | Target Throughput | DB DTUs |
|----------|----------------------------------------------------------------|---------|-------------------|---------|
| Insights | `GET /expenditure/school/{urn}/history`                        | Average | 20 /s             | S1      |
| Insights | `GET /expenditure/school/history/national-average`             | Average | 20 /s             | S1      |
| Insights | `GET /expenditure/school/{urn}/history/comparator-set-average` | Average | 20 /s             | S1      |
| Insights | `GET /expenditure/school/{urn}/history`                        | Peak    | 40 /s             | S2      |
| Insights | `GET /expenditure/school/history/national-average`             | Peak    | 40 /s             | S2      |
| Insights | `GET /expenditure/school/{urn}/history/comparator-set-average` | Peak    | 40 /s             | S2      |

## Performance Success Criteria

- Average Response Time:
  - P50 below 100ms
  - P95 below 150ms
  - P99 below 500ms
- Peak Response Time:
  - P75 below 200ms
  - P95 below 300ms
  - P99 below 1000ms
- Error Rate:  below 0.1%

## Test Execution

- Load pattern: Linear
- Test duration (minutes): 6
- Ramp-up time (minutes): 1

## Test Output

<!-- take care with final separator line in piped table, as pandoc uses this for relative column widths -->
| Load Test Name                                                        | Initiated on         | Max VUs | Duration | Response time | Errors | Throughput | Result |
|-----------------------------------------------------------------------|----------------------|---------|----------|---------------|--------|------------|--------|
| Insight API - Get School Expenditure History (school)                 | 13/12/2024, 17:12:08 | 1       | 5m 3s    | 52 ms         | 0 %    | 26.53 /s   | [✅ Passed](https://portal.azure.com/#blade/Microsoft_Azure_CloudNativeTesting/NewReport/resourceId/%2Fsubscriptions%2Fa5c0a8d7-a54d-4a6d-ab79-4ca64a3b750f%2Fresourcegroups%2Fs198t01-ebis-perf-tests%2Fproviders%2Fmicrosoft.loadtestservice%2Floadtests%2Fs198t01-load-tests/testId/033810ae-2d25-4a4e-a7f7-0cb9e7312008/testRunId/033810ae-2d25-4a4e-a7f7-0cb9e7312028)     |
| Insight API - Get School Expenditure History (national average)       | 16/12/2024, 06:09:59 | 1       | 4m 58s   | 5.86 s        | 61 %   | 0.40 /s    | [❌ Failed](https://portal.azure.com/#blade/Microsoft_Azure_CloudNativeTesting/NewReport/resourceId/%2Fsubscriptions%2Fa5c0a8d7-a54d-4a6d-ab79-4ca64a3b750f%2Fresourcegroups%2Fs198t01-ebis-perf-tests%2Fproviders%2Fmicrosoft.loadtestservice%2Floadtests%2Fs198t01-load-tests/testId/9650ad0b-cf99-40e8-b7fc-d96e84526008/testRunId/39f67675-b857-4bcb-a84b-140dd1b2a053) 1️⃣ |
| Insight API - Get School Expenditure History (comparator set average) | 16/12/2024, 06:30:13 | 1       | 5m 3s    | 78 ms         | 0 %    | 15.52 /s   | [✅ Passed](https://portal.azure.com/#blade/Microsoft_Azure_CloudNativeTesting/NewReport/resourceId/%2Fsubscriptions%2Fa5c0a8d7-a54d-4a6d-ab79-4ca64a3b750f%2Fresourcegroups%2Fs198t01-ebis-perf-tests%2Fproviders%2Fmicrosoft.loadtestservice%2Floadtests%2Fs198t01-load-tests/testId/7935fa61-109e-4ce8-8860-59165b9e4026/testRunId/39f67675-b857-4bcb-a84b-140dd1b2a13c)     |
| Insight API - Get School Expenditure History (school)                 | 16/12/2024, 07:45:49 | 3       | 5m 45s   | 30.00 ms      | 0 %    | 120.29 /s  | [✅ Passed](https://portal.azure.com/#blade/Microsoft_Azure_CloudNativeTesting/NewReport/resourceId/%2Fsubscriptions%2Fa5c0a8d7-a54d-4a6d-ab79-4ca64a3b750f%2Fresourcegroups%2Fs198t01-ebis-perf-tests%2Fproviders%2Fmicrosoft.loadtestservice%2Floadtests%2Fs198t01-load-tests/testId/39f67675-b857-4bcb-a84b-140dd1b2a529/testRunId/39f67675-b857-4bcb-a84b-140dd1b2a685)     |
| Insight API - Get School Expenditure History (national average)       | 16/12/2024, 08:36:42 | 3       | 5m 27s   | 1.25 s        | 64 %   | 1.10 /s    | [❌ Failed](https://portal.azure.com/#blade/Microsoft_Azure_CloudNativeTesting/NewReport/resourceId/%2Fsubscriptions%2Fa5c0a8d7-a54d-4a6d-ab79-4ca64a3b750f%2Fresourcegroups%2Fs198t01-ebis-perf-tests%2Fproviders%2Fmicrosoft.loadtestservice%2Floadtests%2Fs198t01-load-tests/testId/39f67675-b857-4bcb-a84b-140dd1b2a545/testRunId/39f67675-b857-4bcb-a84b-140dd1b2adb4) 1️⃣ |
| Insight API - Get School Expenditure History (comparator set average) | 16/12/2024, 07:54:12 | 3       | 5m 49s   | 111.00 ms     | 0 %    | 33.94 /s   | [✅ Passed](https://portal.azure.com/#blade/Microsoft_Azure_CloudNativeTesting/NewReport/resourceId/%2Fsubscriptions%2Fa5c0a8d7-a54d-4a6d-ab79-4ca64a3b750f%2Fresourcegroups%2Fs198t01-ebis-perf-tests%2Fproviders%2Fmicrosoft.loadtestservice%2Floadtests%2Fs198t01-load-tests/testId/39f67675-b857-4bcb-a84b-140dd1b2a4fe/testRunId/39f67675-b857-4bcb-a84b-140dd1b2a837)  2️⃣ |

**Findings and Recommendations:**

1️⃣ Very poor performance of National Average School Expenditure History endpoint.

2️⃣ Poor performance of Comparator Set Average School Expenditure History endpoint, but within the variance defined for the test run.

<!-- Leave the rest of this page blank -->
\newpage