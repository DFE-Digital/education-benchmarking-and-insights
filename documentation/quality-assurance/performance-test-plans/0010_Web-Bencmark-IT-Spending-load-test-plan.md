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

| Load Test Name                             | Initiated on         | Max VUs | Duration | Response time (P75) | Response time (P95) | Response time (P99) | Errors (%) | Throughput (req/s) | Result                                                                                                                                                                                                                                                                                                                                                                                                                          |
|--------------------------------------------|----------------------|---------|----------|---------------------|---------------------|---------------------|------------|--------------------|---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| Average Web                                | 8/12/2025_1:27:45 PM | 12      | 6m       | 390ms               | 1903ms              | 3450ms              | 0.0%       | 20.91/s            | [❌ Failed](https://portal.azure.com/?feature.msaljs=true#view/Microsoft_Azure_CloudNativeTesting/NewReport/resourceId/%2Fsubscriptions%2Fa5c0a8d7-a54d-4a6d-ab79-4ca64a3b750f%2Fresourcegroups%2Fs198t01-ebis-perf-tests%2Fproviders%2Fmicrosoft.loadtestservice%2Floadtests%2Fs198t01-load-tests/testRunId/8f1521b3-20bd-4f4f-b03a-fdfebc8e0055/testId/72e37301-bb08-4ddd-a6d5-15a93d16b25b/openingFromBlade~/true)            |
| Peak Web                                   | 8/12/2025_4:28:18 PM | 18      | 6m 2s    | 369ms               | 1980ms              | 3191ms              | 0.0%       | 29.96/s            | [❌ Failed](https://portal.azure.com/?feature.msaljs=true#view/Microsoft_Azure_CloudNativeTesting/NewReport/resourceId/%2Fsubscriptions%2Fa5c0a8d7-a54d-4a6d-ab79-4ca64a3b750f%2Fresourcegroups%2Fs198t01-ebis-perf-tests%2Fproviders%2Fmicrosoft.loadtestservice%2Floadtests%2Fs198t01-load-tests/testRunId/47200ac7-2189-44d7-899f-1a072903d0ed/testId/5acd6f35-8572-4bdf-8f0a-e50156b21198/openingFromBlade~/true)            |
| Average Web (Functions App tiers upgraded) | 8/13/2025_9:37:20 AM | 12      | 5m 59s   | 1928ms              | 7305ms              | 11530ms             | 0.0%       | 6.03/s             | [❌ Failed & Degraded](https://portal.azure.com/?feature.msaljs=true#view/Microsoft_Azure_CloudNativeTesting/NewReport/resourceId/%2Fsubscriptions%2Fa5c0a8d7-a54d-4a6d-ab79-4ca64a3b750f%2Fresourcegroups%2Fs198t01-ebis-perf-tests%2Fproviders%2Fmicrosoft.loadtestservice%2Floadtests%2Fs198t01-load-tests/testRunId/47200ac7-2189-44d7-899f-1a072903d192/testId/72e37301-bb08-4ddd-a6d5-15a93d16b25b/openingFromBlade~/true) |
| Average Web – Run 1 (Optimised API)        | 8/20/2025_8:34:49 AM | 14      | 5m 56s   | 310ms               | 581ms               | 968ms               | 0.0%       | 48.32/s            | [✅ Passed](https://portal.azure.com/?feature.msaljs=true#view/Microsoft_Azure_CloudNativeTesting/NewReport/resourceId/%2Fsubscriptions%2Fa5c0a8d7-a54d-4a6d-ab79-4ca64a3b750f%2Fresourcegroups%2Fs198t01-ebis-perf-tests%2Fproviders%2Fmicrosoft.loadtestservice%2Floadtests%2Fs198t01-load-tests/testRunId/15c0b53a-1092-463d-95f4-3ae5e0d8c1b7/testId/72e37301-bb08-4ddd-a6d5-15a93d16b25b/openingFromBlade~/true)            |
| Average Web – Runs 2–7 (Optimised API)     | 8/20/2025            | 14      | 6m       | ~300–320ms          | ~560–600ms          | ~900–1000ms         | 0.0%       | ~50/s              | ✅ Passed – consistent with Run 1                                                                                                                                                                                                                                                                                                                                                                                                |
| Peak Web – Run 1 (Optimised API)           | 8/20/2025_8:11:27 AM | 18      | 6m 6s    | 480ms               | 645ms               | 1137ms              | 0.0%       | 60.82/s            | [✅ Passed](https://portal.azure.com/?feature.msaljs=true#view/Microsoft_Azure_CloudNativeTesting/NewReport/resourceId/%2Fsubscriptions%2Fa5c0a8d7-a54d-4a6d-ab79-4ca64a3b750f%2Fresourcegroups%2Fs198t01-ebis-perf-tests%2Fproviders%2Fmicrosoft.loadtestservice%2Floadtests%2Fs198t01-load-tests/testRunId/7c2a73f1-ad97-4200-9ad0-50caedcc0312/testId/5acd6f35-8572-4bdf-8f0a-e50156b21198/openingFromBlade~/true)                                                                                                                                                                                                                                                                                                                                                                                                                    |
| Peak Web – Runs 2–8 (Optimised API)        | 8/20/2025            | 18      | 6m       | ~450–500ms          | ~560–700ms          | ~900–1300ms         | 0.0%       | ~65/s              | ✅ Passed – consistent with Run 1                                                                                                                                                                                                                                                                                                                                                                                                |

**Findings and Recommendations:**

The initial test runs with the App Service set to P1V3 and the database on S2 failed against the performance criteria. An attempted fix by upgrading the Functions App tier further degraded performance, with response times increasing significantly.

The root cause was traced to API endpoints where third-party code was generating SVGs at runtime. By refactoring this into templated SVGs under our control, CPU usage was reduced sharply and performance stabilised.

As a result, tests that were previously intermittent became consistently passing. Both average load and peak load scenarios ran successfully across multiple runs with no errors and response times well below thresholds, and the API is now considerably less resource-hungry.

<!-- Leave the rest of this page blank -->
\newpage
