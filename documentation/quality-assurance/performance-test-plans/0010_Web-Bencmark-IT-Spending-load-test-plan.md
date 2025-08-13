# Performance Test Plan: Benchmark IT Spending page SSR Web load test (0010)

**Test run date:**

August 2025

## Goal

The goal of this test is to measure the performance of the Benchmark IT spending page (`/school/{identifier}/benchmark-it-spending`) under load, following the implementation of server-side rendering (SSR).

This page uses horizontal bar charts, which have similar build times to the SSR vertical bar charts used in the Spending priorities page. However, the payload size is significantly larger, which may impact over-the-wire timings and full page load performance.

## Objective

- Establish baseline performance under average and peak load conditions for the Benchmark IT spending page.
- Measure the impact of larger payload size on end-to-end page load times.
- Ensure the page remains stable under load without errors or degradation.
- Provide recommendations on optimisations if payload size materially affects performance.

## Procedure

**Determine Capacity:**

Calculate the number of `Virtual Users` required for Azure Load Testing based on a target throughput.

_NB: see previous test plans for calculation._

**Average Load Phase:**

Simulate typical concurrent virtual users for the page over a period of 5 minutes.

**Peak Load Phase:**

Simulate higher concurrent virtual users to mimic peak load conditions over a period of 5 minutes.

Manually scale core database to S2 tier to replicate production infrastructure for the duration of the peak load tests.

**Monitoring Performance:**

- Response Time
  - Definition: Time between request initiation and full response received.
  - Measurement: Azure Load Testing metrics.
- Error Percentage
  - Definition: % of requests returning error (5xx).
  - Measurement: Azure Load Testing metrics.
- Throughput
  - Definition: Requests processed per second.
  - Measurement: Azure Load Testing metrics.
- CPU & Memory utilisation
  - Definition: Average percentage utilisation on app service during load.
  - Measurement: Azure Monitor.
- Database performance
  - Definition: Average % DTU utilisation.
  - Measurement: Azure Monitor.

**Page Under Test:**

| Page                                         | Target Throughput | Load Type | DB DTUs |
|----------------------------------------------|-------------------|-----------|---------|
| `/school/{identifier}/benchmark-it-spending` | 50 /s             | Average   | S2      |
| `/school/{identifier}/benchmark-it-spending` | 83 /s             | Peak      | S2      |

## Performance Success Criteria

Response Time:

- P75 below 500ms
- P95 below 1000ms
- P99 below 2000ms
- Error Rate: below 0.1%
- CPU and Memory utilisation: below 80%*
- Database performance: below 80%*

*based on production tiers P1v3 for app service & S2 (50DTU) for database.

## Test Execution

- Load pattern: Linear
- Test duration (minutes): 6
- Ramp-up time (minutes): 1

## Test Output

| Load Test Name                             | Initiated on         | Max VUs | Duration | Response time (P75) | Response time (P95) | Response time (P99) | Errors (%) | Throughput (req/s) | Result                                                                                                                                                                                                                                                                                                                                                                                                                                |
|--------------------------------------------|----------------------|---------|----------|---------------------|---------------------|---------------------|------------|--------------------|---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| Average Web                                | 8/12/2025_1:27:45 PM | 12      | 6m       | 390ms               | 1903ms              | 3450ms              | 0.0%       | 20.91/s            | [❌ Failed](https://portal.azure.com/?feature.msaljs=true#view/Microsoft_Azure_CloudNativeTesting/NewReport/resourceId/%2Fsubscriptions%2Fa5c0a8d7-a54d-4a6d-ab79-4ca64a3b750f%2Fresourcegroups%2Fs198t01-ebis-perf-tests%2Fproviders%2Fmicrosoft.loadtestservice%2Floadtests%2Fs198t01-load-tests/testRunId/8f1521b3-20bd-4f4f-b03a-fdfebc8e0055/testId/72e37301-bb08-4ddd-a6d5-15a93d16b25b/openingFromBlade~/true) 1️⃣              |
| Peak Web                                   | 8/12/2025_4:28:18 PM | 18      | 6m 2s    | 369ms               | 1980ms              | 3191ms              | 0.0%       | 29.96/s            | [❌ Failed](https://portal.azure.com/?feature.msaljs=true#view/Microsoft_Azure_CloudNativeTesting/NewReport/resourceId/%2Fsubscriptions%2Fa5c0a8d7-a54d-4a6d-ab79-4ca64a3b750f%2Fresourcegroups%2Fs198t01-ebis-perf-tests%2Fproviders%2Fmicrosoft.loadtestservice%2Floadtests%2Fs198t01-load-tests/testRunId/47200ac7-2189-44d7-899f-1a072903d0ed/testId/5acd6f35-8572-4bdf-8f0a-e50156b21198/openingFromBlade~/true)  1️⃣             |
| Average Web (Functions App tiers upgraded) | 8/13/2025_9:37:20 AM | 12      | 5m 59s   | 1928ms              | 7305ms              | 11530ms             | 0.0%       | 6.03/s             | [❌ Failed and Degraded](https://portal.azure.com/?feature.msaljs=true#view/Microsoft_Azure_CloudNativeTesting/NewReport/resourceId/%2Fsubscriptions%2Fa5c0a8d7-a54d-4a6d-ab79-4ca64a3b750f%2Fresourcegroups%2Fs198t01-ebis-perf-tests%2Fproviders%2Fmicrosoft.loadtestservice%2Floadtests%2Fs198t01-load-tests/testRunId/47200ac7-2189-44d7-899f-1a072903d192/testId/72e37301-bb08-4ddd-a6d5-15a93d16b25b/openingFromBlade~/true) 2️⃣ |

**Findings and Recommendations:**

First pass of the test failed with the set criteria with app service set to P1V3 and database set to S2.

The Functions app tier was upgraded but the performance degraded. 

<!-- Leave the rest of this page blank -->
\newpage
