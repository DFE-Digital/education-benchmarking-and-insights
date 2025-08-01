﻿# Performance Test Plan: API School History Load Tests (0006)

**Test run date:**

December 2024 - January 2025

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
| Insights | `GET /expenditure/school/{urn}/history`                        | Average | 15 /s             | S1      |
| Insights | `GET /expenditure/school/history/national-average`             | Average | 2 /s              | S1      |
| Insights | `GET /expenditure/school/{urn}/history/comparator-set-average` | Average | 15 /s             | S1      |
| Insights | `GET /expenditure/school/{urn}/history`                        | Peak    | 30 /s             | S2      |
| Insights | `GET /expenditure/school/history/national-average`             | Peak    | 30 /s             | S2      |
| Insights | `GET /expenditure/school/{urn}/history/comparator-set-average` | Peak    | 30 /s             | S2      |
| Insights | `GET /census/{urn}/history`                                    | Average | 15 /s             | S1      |
| Insights | `GET /census/history/national-average`                         | Average | 2  /s             | S1      |
| Insights | `GET /census/{urn}/history/comparator-set-average`             | Average | 15 /s             | S1      |
| Insights | `GET /census/{urn}/history`                                    | Peak    | 30 /s             | S2      |
| Insights | `GET /census/history/national-average`                         | Peak    | 30 /s             | S2      |
| Insights | `GET /census/{urn}/history/comparator-set-average`             | Peak    | 30 /s             | S2      |

## Performance Success Criteria

- Average Response Time:
  - P50 below 100ms
  - P95 below 2000ms
  - P99 below 3000ms
- Peak Response Time:
  - P75 below 400ms
  - P95 below 6000ms
  - P99 below 10000ms
- Error Rate: below 0.1% *

## Test Execution

- Load pattern: Linear
- Test duration (minutes): 6
- Ramp-up time (minutes): 1

> **NOTE:** Once the distributed cache was added, it was flushed before each test run

## Test Output

<!-- take care with final separator line in piped table, as pandoc uses this for relative column widths -->
| Load Test Name                                                        | Initiated on         | Max VUs | Duration | Response time | Errors   | Throughput | Result | Diff |
|-----------------------------------------------------------------------|----------------------|---------|----------|---------------|----------|------------|--------|------|
| Insight API - Get School Expenditure History (school)                 | 13/12/2024, 17:12:08 | 1       | 5m 3s    | 52 ms         | 0 %      | 26.53 /s   | [✅ Passed](https://portal.azure.com/#blade/Microsoft_Azure_CloudNativeTesting/NewReport/resourceId/%2Fsubscriptions%2Fa5c0a8d7-a54d-4a6d-ab79-4ca64a3b750f%2Fresourcegroups%2Fs198t01-ebis-perf-tests%2Fproviders%2Fmicrosoft.loadtestservice%2Floadtests%2Fs198t01-load-tests/testId/033810ae-2d25-4a4e-a7f7-0cb9e7312008/testRunId/033810ae-2d25-4a4e-a7f7-0cb9e7312028)     |      |
|                                                                       | 09/01/2025, 14:38:14 | 1       | 5m 2s    | 40 ms         | 0 %      | 33.28 /s   | [✅ Passed](https://portal.azure.com/#blade/Microsoft_Azure_CloudNativeTesting/NewReport/resourceId/%2Fsubscriptions%2Fa5c0a8d7-a54d-4a6d-ab79-4ca64a3b750f%2FresourceGroups%2Fs198t01-ebis-perf-tests%2Fproviders%2FMicrosoft.LoadTestService%2Floadtests%2Fs198t01-load-tests/testId/033810ae-2d25-4a4e-a7f7-0cb9e7312008/testRunId/f7668ffe-e9e3-49a9-9f0c-062ab28df386)     | 🟢   |
| Insight API - Get School Expenditure History (national average)       | 16/12/2024, 06:09:59 | 1       | 4m 58s   | 5.86 s        | 61 %     | 0.40 /s    | [❌ Failed](https://portal.azure.com/#blade/Microsoft_Azure_CloudNativeTesting/NewReport/resourceId/%2Fsubscriptions%2Fa5c0a8d7-a54d-4a6d-ab79-4ca64a3b750f%2Fresourcegroups%2Fs198t01-ebis-perf-tests%2Fproviders%2Fmicrosoft.loadtestservice%2Floadtests%2Fs198t01-load-tests/testId/9650ad0b-cf99-40e8-b7fc-d96e84526008/testRunId/39f67675-b857-4bcb-a84b-140dd1b2a053) 1️⃣ |      |
|                                                                       | 17/12/2024, 09:51:49 | 1       | 5m 1s    | 2.21 s        | 0 %      | 1.71 /s    | [✅ Passed](https://portal.azure.com/#blade/Microsoft_Azure_CloudNativeTesting/NewReport/resourceId/%2Fsubscriptions%2Fa5c0a8d7-a54d-4a6d-ab79-4ca64a3b750f%2Fresourcegroups%2Fs198t01-ebis-perf-tests%2Fproviders%2Fmicrosoft.loadtestservice%2Floadtests%2Fs198t01-load-tests/testId/9650ad0b-cf99-40e8-b7fc-d96e84526008/testRunId/225793b5-c48f-409d-b5a9-5da2b0fed0cd)     | 🟢   |
|                                                                       | 09/01/2025, 14:32:46 | 1       | 5m       | 723 ms        | 22.2 % * | 3.78 /s    | [✅ Passed](https://portal.azure.com/#blade/Microsoft_Azure_CloudNativeTesting/NewReport/resourceId/%2Fsubscriptions%2Fa5c0a8d7-a54d-4a6d-ab79-4ca64a3b750f%2FresourceGroups%2Fs198t01-ebis-perf-tests%2Fproviders%2FMicrosoft.LoadTestService%2Floadtests%2Fs198t01-load-tests/testId/9650ad0b-cf99-40e8-b7fc-d96e84526008/testRunId/f7668ffe-e9e3-49a9-9f0c-062ab28df2d6)     | 🟢   |
|                                                                       | 13/01/2025, 12:28:39 | 1       | 5m 2s    | 42 ms         | 22.2 % * | 28.91 /s   | [✅ Passed](https://portal.azure.com/#blade/Microsoft_Azure_CloudNativeTesting/NewReport/resourceId/%2Fsubscriptions%2Fa5c0a8d7-a54d-4a6d-ab79-4ca64a3b750f%2Fresourcegroups%2Fs198t01-ebis-perf-tests%2Fproviders%2Fmicrosoft.loadtestservice%2Floadtests%2Fs198t01-load-tests/testId/9650ad0b-cf99-40e8-b7fc-d96e84526008/testRunId/236c67c6-87a2-47ed-a520-49f08906a018)     | 🟢   |
|                                                                       | 14/01/2025, 08:50:07 | 1       | 5m 5s    | 35 ms         | 0 %      | 30.88 /s   | [✅ Passed](https://portal.azure.com/#blade/Microsoft_Azure_CloudNativeTesting/NewReport/resourceId/%2Fsubscriptions%2Fa5c0a8d7-a54d-4a6d-ab79-4ca64a3b750f%2FresourceGroups%2Fs198t01-ebis-perf-tests%2Fproviders%2FMicrosoft.LoadTestService%2Floadtests%2Fs198t01-load-tests/testId/9650ad0b-cf99-40e8-b7fc-d96e84526008/testRunId/58394ba7-fa1d-40e4-aa73-994417d37023)     | 🟢   |
| Insight API - Get School Expenditure History (comparator set average) | 16/12/2024, 06:30:13 | 1       | 5m 3s    | 78 ms         | 0 %      | 15.52 /s   | [✅ Passed](https://portal.azure.com/#blade/Microsoft_Azure_CloudNativeTesting/NewReport/resourceId/%2Fsubscriptions%2Fa5c0a8d7-a54d-4a6d-ab79-4ca64a3b750f%2Fresourcegroups%2Fs198t01-ebis-perf-tests%2Fproviders%2Fmicrosoft.loadtestservice%2Floadtests%2Fs198t01-load-tests/testId/7935fa61-109e-4ce8-8860-59165b9e4026/testRunId/39f67675-b857-4bcb-a84b-140dd1b2a13c)     |      |
|                                                                       | 09/01/2025, 15:48:20 | 1       | 5m 8s    | 52 ms         | 0 %      | 22.21 /s   | [✅ Passed](https://portal.azure.com/#blade/Microsoft_Azure_CloudNativeTesting/NewReport/resourceId/%2Fsubscriptions%2Fa5c0a8d7-a54d-4a6d-ab79-4ca64a3b750f%2Fresourcegroups%2Fs198t01-ebis-perf-tests%2Fproviders%2Fmicrosoft.loadtestservice%2Floadtests%2Fs198t01-load-tests/testId/7935fa61-109e-4ce8-8860-59165b9e4026/testRunId/f7668ffe-e9e3-49a9-9f0c-062ab28dfa45)     | 🟢   |
| Insight API - Get School Expenditure History (school)                 | 16/12/2024, 07:45:49 | 3       | 5m 45s   | 30 ms         | 0 %      | 120.29 /s  | [✅ Passed](https://portal.azure.com/#blade/Microsoft_Azure_CloudNativeTesting/NewReport/resourceId/%2Fsubscriptions%2Fa5c0a8d7-a54d-4a6d-ab79-4ca64a3b750f%2Fresourcegroups%2Fs198t01-ebis-perf-tests%2Fproviders%2Fmicrosoft.loadtestservice%2Floadtests%2Fs198t01-load-tests/testId/39f67675-b857-4bcb-a84b-140dd1b2a529/testRunId/39f67675-b857-4bcb-a84b-140dd1b2a685)     |      |
|                                                                       | 09/01/2025, 16:45:07 | 3       | 5m 48s   | 32 ms         | 0 %      | 105.92 /s  | [✅ Passed](https://portal.azure.com/#blade/Microsoft_Azure_CloudNativeTesting/NewReport/resourceId/%2Fsubscriptions%2Fa5c0a8d7-a54d-4a6d-ab79-4ca64a3b750f%2Fresourcegroups%2Fs198t01-ebis-perf-tests%2Fproviders%2Fmicrosoft.loadtestservice%2Floadtests%2Fs198t01-load-tests/testId/39f67675-b857-4bcb-a84b-140dd1b2a529/testRunId/0d4d7c93-9eda-4c78-a60b-b27cc47af0ca)     | 🔴   |
| Insight API - Get School Expenditure History (national average)       | 16/12/2024, 08:36:42 | 3       | 5m 27s   | 1.25 s        | 64 %     | 1.10 /s    | [❌ Failed](https://portal.azure.com/#blade/Microsoft_Azure_CloudNativeTesting/NewReport/resourceId/%2Fsubscriptions%2Fa5c0a8d7-a54d-4a6d-ab79-4ca64a3b750f%2Fresourcegroups%2Fs198t01-ebis-perf-tests%2Fproviders%2Fmicrosoft.loadtestservice%2Floadtests%2Fs198t01-load-tests/testId/39f67675-b857-4bcb-a84b-140dd1b2a545/testRunId/39f67675-b857-4bcb-a84b-140dd1b2adb4) 1️⃣ |      |
|                                                                       | 17/12/2024, 10:58:16 | 3       | 5m 48s   | 2.72 s        | 0 %      | 3.47 /s    | [✅ Passed](https://portal.azure.com/#blade/Microsoft_Azure_CloudNativeTesting/NewReport/resourceId/%2Fsubscriptions%2Fa5c0a8d7-a54d-4a6d-ab79-4ca64a3b750f%2Fresourcegroups%2Fs198t01-ebis-perf-tests%2Fproviders%2Fmicrosoft.loadtestservice%2Floadtests%2Fs198t01-load-tests/testId/39f67675-b857-4bcb-a84b-140dd1b2a545/testRunId/37cff4e5-a955-4589-acf8-93658c235143)     | 🔴   |
|                                                                       | 09/01/2025, 16:16:22 | 3       | 5m 39s   | 4.42 s        | 22.1 % * | 2.17 /s    | [❌ Failed](https://portal.azure.com/#blade/Microsoft_Azure_CloudNativeTesting/NewReport/resourceId/%2Fsubscriptions%2Fa5c0a8d7-a54d-4a6d-ab79-4ca64a3b750f%2Fresourcegroups%2Fs198t01-ebis-perf-tests%2Fproviders%2Fmicrosoft.loadtestservice%2Floadtests%2Fs198t01-load-tests/testId/39f67675-b857-4bcb-a84b-140dd1b2a545/testRunId/f7668ffe-e9e3-49a9-9f0c-062ab28dfe4b) 4️⃣ | 🔴   |
|                                                                       | 13/01/2025, 12:51:14 | 3       | 5m 47s   | 38 ms         | 22.2 % * | 85.45 /s   | [✅ Passed](https://portal.azure.com/#blade/Microsoft_Azure_CloudNativeTesting/NewReport/resourceId/%2Fsubscriptions%2Fa5c0a8d7-a54d-4a6d-ab79-4ca64a3b750f%2Fresourcegroups%2Fs198t01-ebis-perf-tests%2Fproviders%2Fmicrosoft.loadtestservice%2Floadtests%2Fs198t01-load-tests/testId/39f67675-b857-4bcb-a84b-140dd1b2a545/testRunId/fc1fcf8a-751e-4d98-93b2-65db30b8b017)     | 🟢   |
|                                                                       | 14/01/2025, 09:46:30 | 3       | 5m 48s   | 33 ms         | 0 %      | 104.03 /s  | [✅ Passed](https://portal.azure.com/#blade/Microsoft_Azure_CloudNativeTesting/NewReport/resourceId/%2Fsubscriptions%2Fa5c0a8d7-a54d-4a6d-ab79-4ca64a3b750f%2Fresourcegroups%2Fs198t01-ebis-perf-tests%2Fproviders%2Fmicrosoft.loadtestservice%2Floadtests%2Fs198t01-load-tests/testId/39f67675-b857-4bcb-a84b-140dd1b2a545/testRunId/c82e058b-66c2-4daf-a4d7-2c9c232ec093)     | 🟢   |
| Insight API - Get School Expenditure History (comparator set average) | 16/12/2024, 07:54:12 | 3       | 5m 49s   | 111 ms        | 0 %      | 33.94 /s   | [✅ Passed](https://portal.azure.com/#blade/Microsoft_Azure_CloudNativeTesting/NewReport/resourceId/%2Fsubscriptions%2Fa5c0a8d7-a54d-4a6d-ab79-4ca64a3b750f%2Fresourcegroups%2Fs198t01-ebis-perf-tests%2Fproviders%2Fmicrosoft.loadtestservice%2Floadtests%2Fs198t01-load-tests/testId/39f67675-b857-4bcb-a84b-140dd1b2a4fe/testRunId/39f67675-b857-4bcb-a84b-140dd1b2a837) 2️⃣ |      |
|                                                                       | 09/01/2025, 17:06:29 | 3       | 5m 49s   | 74 ms         | 0 %      | 53.28 /s   | [✅ Passed](https://portal.azure.com/#blade/Microsoft_Azure_CloudNativeTesting/NewReport/resourceId/%2Fsubscriptions%2Fa5c0a8d7-a54d-4a6d-ab79-4ca64a3b750f%2Fresourcegroups%2Fs198t01-ebis-perf-tests%2Fproviders%2Fmicrosoft.loadtestservice%2Floadtests%2Fs198t01-load-tests/testId/39f67675-b857-4bcb-a84b-140dd1b2a4fe/testRunId/94fc6eda-f0c0-4f14-8a77-5742be57b027)     | 🟢   |
| Insight API - Get School Census History (comparator set average)      | 16/12/2024, 11:10:06 | 1       | 5m 4s    | 36 ms         | 0 %      | 31.06 /s   | [✅ Passed](https://portal.azure.com/#blade/Microsoft_Azure_CloudNativeTesting/NewReport/resourceId/%2Fsubscriptions%2Fa5c0a8d7-a54d-4a6d-ab79-4ca64a3b750f%2Fresourcegroups%2Fs198t01-ebis-perf-tests%2Fproviders%2Fmicrosoft.loadtestservice%2Floadtests%2Fs198t01-load-tests/testId/87d8dd2a-7c3f-4a70-85ae-9aa3c5c2e039/testRunId/87d8dd2a-7c3f-4a70-85ae-9aa3c5c2e2ab)     |      |
|                                                                       | 09/01/2025, 15:55:35 | 1       | 5m       | 39 ms         | 0 %      | 31.72 /s   | [✅ Passed](https://portal.azure.com/#blade/Microsoft_Azure_CloudNativeTesting/NewReport/resourceId/%2Fsubscriptions%2Fa5c0a8d7-a54d-4a6d-ab79-4ca64a3b750f%2Fresourcegroups%2Fs198t01-ebis-perf-tests%2Fproviders%2Fmicrosoft.loadtestservice%2Floadtests%2Fs198t01-load-tests/testId/87d8dd2a-7c3f-4a70-85ae-9aa3c5c2e039/testRunId/f7668ffe-e9e3-49a9-9f0c-062ab28dfb11)     | 🔴   |
| Insight API - Get School Census History (school)                      | 16/12/2024, 11:44:53 | 1       | 5m 1s    | 52 ms         | 0 %      | 28.56 /s   | [✅ Passed](https://portal.azure.com/#blade/Microsoft_Azure_CloudNativeTesting/NewReport/resourceId/%2Fsubscriptions%2Fa5c0a8d7-a54d-4a6d-ab79-4ca64a3b750f%2Fresourcegroups%2Fs198t01-ebis-perf-tests%2Fproviders%2Fmicrosoft.loadtestservice%2Floadtests%2Fs198t01-load-tests/testId/87d8dd2a-7c3f-4a70-85ae-9aa3c5c2e151/testRunId/87d8dd2a-7c3f-4a70-85ae-9aa3c5c2e782)     |      |
|                                                                       | 09/01/2025, 16:01:27 | 1       | 5m 6s    | 36 ms         | 0 %      | 33.71 /s   | [✅ Passed](https://portal.azure.com/#blade/Microsoft_Azure_CloudNativeTesting/NewReport/resourceId/%2Fsubscriptions%2Fa5c0a8d7-a54d-4a6d-ab79-4ca64a3b750f%2FresourceGroups%2Fs198t01-ebis-perf-tests%2Fproviders%2FMicrosoft.LoadTestService%2Floadtests%2Fs198t01-load-tests/testId/87d8dd2a-7c3f-4a70-85ae-9aa3c5c2e151/testRunId/f7668ffe-e9e3-49a9-9f0c-062ab28dfc19)     | 🟢   |
| Insight API - Get School Census History (national average)            | 16/12/2024, 11:52:27 | 1       | 5m 9s    | 700 ms        | 56 %     | 4.11 /s    | [❌ Failed](https://portal.azure.com/#blade/Microsoft_Azure_CloudNativeTesting/NewReport/resourceId/%2Fsubscriptions%2Fa5c0a8d7-a54d-4a6d-ab79-4ca64a3b750f%2Fresourcegroups%2Fs198t01-ebis-perf-tests%2Fproviders%2Fmicrosoft.loadtestservice%2Floadtests%2Fs198t01-load-tests/testId/87d8dd2a-7c3f-4a70-85ae-9aa3c5c2e1aa/testRunId/87d8dd2a-7c3f-4a70-85ae-9aa3c5c2e975) 3️⃣ |      |
|                                                                       | 17/12/2024, 10:18:35 | 1       | 5m 4s    | 830 ms        | 0 %      | 2.93 /s    | [❌ Failed](https://portal.azure.com/#blade/Microsoft_Azure_CloudNativeTesting/NewReport/resourceId/%2Fsubscriptions%2Fa5c0a8d7-a54d-4a6d-ab79-4ca64a3b750f%2Fresourcegroups%2Fs198t01-ebis-perf-tests%2Fproviders%2Fmicrosoft.loadtestservice%2Floadtests%2Fs198t01-load-tests/testId/87d8dd2a-7c3f-4a70-85ae-9aa3c5c2e1aa/testRunId/225793b5-c48f-409d-b5a9-5da2b0fed2fe)     | 🔴   |
|                                                                       | 09/01/2025, 14:45:37 | 1       | 5m 7s    | 97 ms         | 22.2 % * | 15.44 /s   | [✅ Passed](https://portal.azure.com/#blade/Microsoft_Azure_CloudNativeTesting/NewReport/resourceId/%2Fsubscriptions%2Fa5c0a8d7-a54d-4a6d-ab79-4ca64a3b750f%2Fresourcegroups%2Fs198t01-ebis-perf-tests%2Fproviders%2Fmicrosoft.loadtestservice%2Floadtests%2Fs198t01-load-tests/testId/87d8dd2a-7c3f-4a70-85ae-9aa3c5c2e1aa/testRunId/f7668ffe-e9e3-49a9-9f0c-062ab28df6fd)     | 🟢   |
|                                                                       | 13/01/2025, 12:40:17 | 1       | 5m 8s    | 41 ms         | 22.2 % * | 30.48 /s   | [✅ Passed](https://portal.azure.com/#blade/Microsoft_Azure_CloudNativeTesting/NewReport/resourceId/%2Fsubscriptions%2Fa5c0a8d7-a54d-4a6d-ab79-4ca64a3b750f%2FresourceGroups%2Fs198t01-ebis-perf-tests%2Fproviders%2FMicrosoft.LoadTestService%2Floadtests%2Fs198t01-load-tests/testId/87d8dd2a-7c3f-4a70-85ae-9aa3c5c2e1aa/testRunId/236c67c6-87a2-47ed-a520-49f08906a2b6)     | 🟢   |
|                                                                       | 14/01/2025, 09:22:56 | 1       | 5m 9s    | 35 ms         | 0 %      | 33.89 /s   | [✅ Passed](https://portal.azure.com/#blade/Microsoft_Azure_CloudNativeTesting/NewReport/resourceId/%2Fsubscriptions%2Fa5c0a8d7-a54d-4a6d-ab79-4ca64a3b750f%2FresourceGroups%2Fs198t01-ebis-perf-tests%2Fproviders%2FMicrosoft.LoadTestService%2Floadtests%2Fs198t01-load-tests/testId/87d8dd2a-7c3f-4a70-85ae-9aa3c5c2e1aa/testRunId/7c0d53e4-3d2d-40a2-8b3a-93430104422b)     | 🟢   |
| Insight API - Get School Census History (comparator set average)      | 16/12/2024, 12:18:21 | 3       | 5m 47s   | 44 ms         | 0 %      | 82.93 /s   | [✅ Passed](https://portal.azure.com/#blade/Microsoft_Azure_CloudNativeTesting/NewReport/resourceId/%2Fsubscriptions%2Fa5c0a8d7-a54d-4a6d-ab79-4ca64a3b750f%2Fresourcegroups%2Fs198t01-ebis-perf-tests%2Fproviders%2Fmicrosoft.loadtestservice%2Floadtests%2Fs198t01-load-tests/testId/87d8dd2a-7c3f-4a70-85ae-9aa3c5c2ecca/testRunId/87d8dd2a-7c3f-4a70-85ae-9aa3c5c2ece8)     |      |
|                                                                       | 09/01/2025, 16:54:18 | 3       | 5m 44s   | 35 ms         | 0 %      | 97.28 /s   | [✅ Passed](https://portal.azure.com/#blade/Microsoft_Azure_CloudNativeTesting/NewReport/resourceId/%2Fsubscriptions%2Fa5c0a8d7-a54d-4a6d-ab79-4ca64a3b750f%2Fresourcegroups%2Fs198t01-ebis-perf-tests%2Fproviders%2Fmicrosoft.loadtestservice%2Floadtests%2Fs198t01-load-tests/testId/87d8dd2a-7c3f-4a70-85ae-9aa3c5c2ecca/testRunId/0d4d7c93-9eda-4c78-a60b-b27cc47af1e1)     | 🟢   |
| Insight API - Get School Census History (school)                      | 16/12/2024, 12:42:21 | 3       | 5m 45s   | 29 ms         | 0 %      | 118.39 /s  | [✅ Passed](https://portal.azure.com/#blade/Microsoft_Azure_CloudNativeTesting/NewReport/resourceId/%2Fsubscriptions%2Fa5c0a8d7-a54d-4a6d-ab79-4ca64a3b750f%2Fresourcegroups%2Fs198t01-ebis-perf-tests%2Fproviders%2Fmicrosoft.loadtestservice%2Floadtests%2Fs198t01-load-tests/testId/87d8dd2a-7c3f-4a70-85ae-9aa3c5c2ed0e/testRunId/7496ab17-5a94-479e-b5a8-74d88968a188)     |      |
|                                                                       | 09/01/2025, 16:36:31 | 3       | 5m 45s   | 31 ms         | 0 %      | 109.22 /s  | [✅ Passed](https://portal.azure.com/#blade/Microsoft_Azure_CloudNativeTesting/NewReport/resourceId/%2Fsubscriptions%2Fa5c0a8d7-a54d-4a6d-ab79-4ca64a3b750f%2Fresourcegroups%2Fs198t01-ebis-perf-tests%2Fproviders%2Fmicrosoft.loadtestservice%2Floadtests%2Fs198t01-load-tests/testId/87d8dd2a-7c3f-4a70-85ae-9aa3c5c2ed0e/testRunId/0d4d7c93-9eda-4c78-a60b-b27cc47af017)     | 🔴   |
| Insight API - Get School Census History (national average)            | 16/12/2024, 12:35:11 | 3       | 5m 45s   | 1.04 s        | 56 %     | 8.53 /s    | [❌ Failed](https://portal.azure.com/#blade/Microsoft_Azure_CloudNativeTesting/NewReport/resourceId/%2Fsubscriptions%2Fa5c0a8d7-a54d-4a6d-ab79-4ca64a3b750f%2Fresourcegroups%2Fs198t01-ebis-perf-tests%2Fproviders%2Fmicrosoft.loadtestservice%2Floadtests%2Fs198t01-load-tests/testId/87d8dd2a-7c3f-4a70-85ae-9aa3c5c2ed38/testRunId/7496ab17-5a94-479e-b5a8-74d88968a073) 3️⃣ |      |
|                                                                       | 17/12/2024, 10:51:14 | 3       | 5m 46s   | 1.17 s        | 0 %      | 5.21 /s    | [❌ Failed](https://portal.azure.com/#blade/Microsoft_Azure_CloudNativeTesting/NewReport/resourceId/%2Fsubscriptions%2Fa5c0a8d7-a54d-4a6d-ab79-4ca64a3b750f%2Fresourcegroups%2Fs198t01-ebis-perf-tests%2Fproviders%2Fmicrosoft.loadtestservice%2Floadtests%2Fs198t01-load-tests/testId/87d8dd2a-7c3f-4a70-85ae-9aa3c5c2ed38/testRunId/37cff4e5-a955-4589-acf8-93658c235037)     | 🔴   |
|                                                                       | 09/01/2025, 16:22:51 | 3       | 5m 39s   | 771 ms        | 22.1 % * | 9.32 /s    | [❌ Failed](https://portal.azure.com/#blade/Microsoft_Azure_CloudNativeTesting/NewReport/resourceId/%2Fsubscriptions%2Fa5c0a8d7-a54d-4a6d-ab79-4ca64a3b750f%2Fresourcegroups%2Fs198t01-ebis-perf-tests%2Fproviders%2Fmicrosoft.loadtestservice%2Floadtests%2Fs198t01-load-tests/testId/87d8dd2a-7c3f-4a70-85ae-9aa3c5c2ed38/testRunId/f7668ffe-e9e3-49a9-9f0c-062ab28dfec3)     | 🟢   |
|                                                                       | 13/01/2025, 13:00:24 | 3       | 5m 41s   | 35 ms         | 22.3 % * | 106.26 /s  | [✅ Passed](https://portal.azure.com/#blade/Microsoft_Azure_CloudNativeTesting/NewReport/resourceId/%2Fsubscriptions%2Fa5c0a8d7-a54d-4a6d-ab79-4ca64a3b750f%2Fresourcegroups%2Fs198t01-ebis-perf-tests%2Fproviders%2Fmicrosoft.loadtestservice%2Floadtests%2Fs198t01-load-tests/testId/87d8dd2a-7c3f-4a70-85ae-9aa3c5c2ed38/testRunId/fc1fcf8a-751e-4d98-93b2-65db30b8b115)     | 🟢   |
|                                                                       | 14/01/2025, 09:38:05 | 3       | 5m 49s   | 33 ms         | 0 %      | 104.52 /s  | [✅ Passed](https://portal.azure.com/#blade/Microsoft_Azure_CloudNativeTesting/NewReport/resourceId/%2Fsubscriptions%2Fa5c0a8d7-a54d-4a6d-ab79-4ca64a3b750f%2Fresourcegroups%2Fs198t01-ebis-perf-tests%2Fproviders%2Fmicrosoft.loadtestservice%2Floadtests%2Fs198t01-load-tests/testId/87d8dd2a-7c3f-4a70-85ae-9aa3c5c2ed38/testRunId/7c0d53e4-3d2d-40a2-8b3a-93430104447b)     | 🟢   |

\* Higher acceptable error rate permitted for National Average endpoints due to an acceptance that some combinations of `OverallPhase` and `FinanceType` will return a `404` with the SQL view refactor work completed in [#1751](https://github.com/DFE-Digital/education-benchmarking-and-insights/pull/1751). This was later [reverted](https://github.com/DFE-Digital/education-benchmarking-and-insights/commit/6f5e88b5d05e0a47aabfeb158edd49c6a7e57b67).

**Findings and Recommendations:**

High error rates on initial runs were due to misconfigured input parameters that returned `400` validation errors.

1️⃣ Very poor performance of National Average School Expenditure History endpoint.

2️⃣ Poor performance of Comparator Set Average School Expenditure History endpoint, but within the variance defined for the test run.

See [#1681](https://github.com/DFE-Digital/education-benchmarking-and-insights/pull/1681) for remediation commentary for both of the above.

3️⃣ Poor performance of National Average School Census History endpoint.

See [#1751](https://github.com/DFE-Digital/education-benchmarking-and-insights/pull/1751) for remediation commentary for all of the above.

4️⃣ Poor performance of National Average School Expenditure History endpoint under peak load, before caching implemented over refactored SQL views.

<!-- Leave the rest of this page blank -->
\newpage
