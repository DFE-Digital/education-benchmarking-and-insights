# Performance Test Plan - 0009 - Shutter app service load tests

**Test run date:**

July 2025

## Goal

The goal of this test is to measure the performance of the shutter app service under load in order to help determine to
which SKU the app service plan should be set.

## Objective

- Establish a performance baseline under typical load conditions.
- Simulate real-world conditions to understand the services responsiveness during average traffic patterns.
- Ensure that the service remains stable without crashes or timeouts during normal and peak usage.
- Confirm that the service does not degrade significantly, and maintain performance, during expected high-traffic periods.
- Validate capacity during anticipated peak load.

## Procedure

**Determine Capacity:**

Calculate the number of `Virtual Users` required for Azure Load Testing based on a target throughput.

_NB: see previous test plans for calculation._

**Load Phase:**

For the Shutter page and frequent static assets, simulate the typical number of concurrent virtual users to mimic
average and peak load conditions over a period of 5 minutes.

**Monitoring Performance:**

- Response Time
  - Definition: The duration between sending a request and receiving a response.
  - Measurement: This will be produced as an output of the test run.
- Error Percentage
  - Definition: The percentage of requests that result in an error response (5xx).
  - Measurement: This will be produced as an output of the test run.

**URLs Under Test:**

| Pages                                                                                                                     | Target Throughput | Load Type |
|---------------------------------------------------------------------------------------------------------------------------|-------------------|-----------|
| `/`, `/assets/styles/govuk-frontend.min.css`, `/assets/images/govuk-crest.svg`, `/assets/fonts/light-94a07e06a1-v2.woff2` | 40 /s             | Average   |
| `/`, `/assets/styles/govuk-frontend.min.css`, `/assets/images/govuk-crest.svg`, `/assets/fonts/light-94a07e06a1-v2.woff2` | 150 /s            | Peak      |

## Performance Success Criteria

- Response Time:
  - P75 below 100ms
  - P95 below 150ms
  - P99 below 500ms
- Error Rate: below 0.1%

## Test Execution

- Load pattern: Linear
- Test duration (minutes): 6
- Ramp-up time (minutes): 1

## Test Output

<!-- take care with final separator line in piped table, as pandoc uses this for relative column widths -->
| Load Test Name | Initiated on         | ASP  | Max VUs | Duration | Response time | Errors | Throughput | Result                                                                                                                                                                                                                                                                                                                                                                    |
|----------------|----------------------|------|---------|----------|---------------|--------|------------|------------|
| Average        | 15/07/2025, 08:48:40 | B1   | 4       | 5m 45s   | 205 ms        | 0.00 % | 25.12 /s   | [❌ Failed](https://portal.azure.com#blade/Microsoft_Azure_CloudNativeTesting/NewReport/resourceId/%2Fsubscriptions%2Fa5c0a8d7-a54d-4a6d-ab79-4ca64a3b750f%2FresourceGroups%2Fs198t01-ebis-perf-tests%2Fproviders%2FMicrosoft.LoadTestService%2Floadtests%2Fs198t01-load-tests/testId/62981246-7af5-4866-8ea5-f2f98408b051/testRunId/ca7b554e-6ba2-41ef-ad21-c68e37ffe044) |
| Average        | 15/07/2025, 09:21:14 | B3   | 4       | 5m 45s   | 114 ms        | 0.00 % | 66.97 /s   | [❌ Failed](https://portal.azure.com#blade/Microsoft_Azure_CloudNativeTesting/NewReport/resourceId/%2Fsubscriptions%2Fa5c0a8d7-a54d-4a6d-ab79-4ca64a3b750f%2Fresourcegroups%2Fs198t01-ebis-perf-tests%2Fproviders%2Fmicrosoft.loadtestservice%2Floadtests%2Fs198t01-load-tests/testId/62981246-7af5-4866-8ea5-f2f98408b051/testRunId/ca7b554e-6ba2-41ef-ad21-c68e37ffe504) |
| Average        | 15/07/2025, 08:33:51 | P0V3 | 4       | 5m 53s   | 82 ms         | 0.01 % | 46.13 /s   | [✅ Passed](https://portal.azure.com#blade/Microsoft_Azure_CloudNativeTesting/NewReport/resourceId/%2Fsubscriptions%2Fa5c0a8d7-a54d-4a6d-ab79-4ca64a3b750f%2Fresourcegroups%2Fs198t01-ebis-perf-tests%2Fproviders%2Fmicrosoft.loadtestservice%2Floadtests%2Fs198t01-load-tests/testId/62981246-7af5-4866-8ea5-f2f98408b051/testRunId/410414d0-7b7e-4dbe-bf1c-19092c876140) |
| Peak           | 15/07/2025, 08:58:29 | B1   | 10      | 6m 0s    | 262 ms        | 0.01 % | 61.00 /s   | [❌ Failed](https://portal.azure.com#blade/Microsoft_Azure_CloudNativeTesting/NewReport/resourceId/%2Fsubscriptions%2Fa5c0a8d7-a54d-4a6d-ab79-4ca64a3b750f%2Fresourcegroups%2Fs198t01-ebis-perf-tests%2Fproviders%2Fmicrosoft.loadtestservice%2Floadtests%2Fs198t01-load-tests/testId/490c15e8-1900-44f9-a9f2-3f58b0c0319d/testRunId/ca7b554e-6ba2-41ef-ad21-c68e37ffe1a9) |
| Peak           | 15/07/2025, 09:09:24 | B3   | 10      | 5m 54s   | 94 ms         | 0.00 % | 154.19 /s  | [✅ Passed](https://portal.azure.com#blade/Microsoft_Azure_CloudNativeTesting/NewReport/resourceId/%2Fsubscriptions%2Fa5c0a8d7-a54d-4a6d-ab79-4ca64a3b750f%2Fresourcegroups%2Fs198t01-ebis-perf-tests%2Fproviders%2Fmicrosoft.loadtestservice%2Floadtests%2Fs198t01-load-tests/testId/490c15e8-1900-44f9-a9f2-3f58b0c0319d/testRunId/ca7b554e-6ba2-41ef-ad21-c68e37ffe320) |
| Peak           | 15/07/2025, 08:25:49 | P0V3 | 10      | 5m 56s   | 80 ms         | 0.00 % | 183.93 /s  | [✅ Passed](https://portal.azure.com#blade/Microsoft_Azure_CloudNativeTesting/NewReport/resourceId/%2Fsubscriptions%2Fa5c0a8d7-a54d-4a6d-ab79-4ca64a3b750f%2Fresourcegroups%2Fs198t01-ebis-perf-tests%2Fproviders%2Fmicrosoft.loadtestservice%2Floadtests%2Fs198t01-load-tests/testId/490c15e8-1900-44f9-a9f2-3f58b0c0319d/testRunId/490c15e8-1900-44f9-a9f2-3f58b0c03399) |

**Findings and Recommendations:**

Test criteria at least partially failed when using Basic app service plans, so `P0V3` is the recommended SKU for the Shutter service.

<!-- Leave the rest of this page blank -->
\newpage
