# Performance Test Plan - 0007 - Web School Spending and Priorities load tests for SSR charts

**Test run date:**

March 2025

## Goal

The goal of this test is to measure the impact of replacing client rendered charts with server side rendered ones.
This includes both URL tests and Web tests in order to include coverage over downloaded Web assets.

## Objective

- Establish performance baselines for:
  - typical load conditions using a URL based test
  - peak conditions using a URL based test
  - typical load conditions using a Web based test
  - peak conditions using a Web based test
- Compare all of the above once [SSR chart spike](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/248027) is complete

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

| Page                                      | Test Type | Load Type | DB DTUs |
|-------------------------------------------|-----------|-----------| ------- |
| `/school/{identifier}/spending-and-costs` | URL       | Average   | S1      |
| `/school/{identifier}/spending-and-costs` | URL       | Peak      | S2      |
| `/school/{identifier}/spending-and-costs` | Web       | Average   | S1      |
| `/school/{identifier}/spending-and-costs` | Web       | Peak      | S2      |

## Test Execution

- Load pattern: Linear
- Test duration (minutes): 6
- Ramp-up time (minutes): 1

## Test Output

<!-- take care with final separator line in piped table, as pandoc uses this for relative column widths -->
| Load Test Name                                                        | Initiated on         | Max VUs | Duration | Response time | Errors   | Throughput | Result |
|-----------------------------------------------------------------------|----------------------|---------|----------|---------------|----------|------------|--------|
| Average URL                                                           | 20/03/2025, 07:06:46 | 15      | 6m       | 320 ms        | 0.01 %   | 49.99 /s   | [✅ Passed](https://portal.azure.com/#blade/Microsoft_Azure_CloudNativeTesting/NewReport/resourceId/%2Fsubscriptions%2Fa5c0a8d7-a54d-4a6d-ab79-4ca64a3b750f%2Fresourcegroups%2Fs198t01-ebis-perf-tests%2Fproviders%2Fmicrosoft.loadtestservice%2Floadtests%2Fs198t01-load-tests/testId/64328db8-47e9-4214-9182-b5b7505ff027/testRunId/ffdcb805-e405-4f6b-a735-cbcb90ad7027) |
| Peak URL                                                              | 20/03/2025, 10:46:26 | 30      | 6m  5s   | 694 ms        | 0.00 %   | 67.49 /s   | [✅ Passed](https://portal.azure.com/#blade/Microsoft_Azure_CloudNativeTesting/NewReport/resourceId/%2Fsubscriptions%2Fa5c0a8d7-a54d-4a6d-ab79-4ca64a3b750f%2Fresourcegroups%2Fs198t01-ebis-perf-tests%2Fproviders%2Fmicrosoft.loadtestservice%2Floadtests%2Fs198t01-load-tests/testId/c661f194-eb06-4513-82fc-a3234deca4ae/testRunId/6a131fd9-7a2e-4b7e-8140-9a9ed9dd96d1) |
| Average Web                                                           | 20/03/2025, 11:10:07 | 15      | 5m 7s    | 350 ms        | 0.00 %   | 113.76 /s  | [✅ Passed](https://portal.azure.com/#blade/Microsoft_Azure_CloudNativeTesting/NewReport/resourceId/%2Fsubscriptions%2Fa5c0a8d7-a54d-4a6d-ab79-4ca64a3b750f%2Fresourcegroups%2Fs198t01-ebis-perf-tests%2Fproviders%2Fmicrosoft.loadtestservice%2Floadtests%2Fs198t01-load-tests/testId/6a131fd9-7a2e-4b7e-8140-9a9ed9dd9b0b/testRunId/6a131fd9-7a2e-4b7e-8140-9a9ed9dd9b3f) |
| Peak Web                                                              | 20/03/2025, 10:46:26 | 30      | 5m  4s   | 420 ms        | 0.00 %   | 68.72 /s   | [✅ Passed](https://portal.azure.com/#blade/Microsoft_Azure_CloudNativeTesting/NewReport/resourceId/%2Fsubscriptions%2Fa5c0a8d7-a54d-4a6d-ab79-4ca64a3b750f%2Fresourcegroups%2Fs198t01-ebis-perf-tests%2Fproviders%2Fmicrosoft.loadtestservice%2Floadtests%2Fs198t01-load-tests/testId/1f5453f4-9d9c-4cab-8911-b676df16842a/testRunId/6a131fd9-7a2e-4b7e-8140-9a9ed9dd98aa) |

**Findings and Recommendations:**

<!-- Leave the rest of this page blank -->
\newpage
