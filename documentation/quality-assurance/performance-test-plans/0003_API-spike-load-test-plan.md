# Performance Test Plan - 0003 - API Spike Load

**Test run date:**

October 2024

## Goal

The goal of this test is to measure how the APIs perform under sudden surges in activity. Focus will be on verifying the
high demand and critical endpoints.

## Objective

- Evaluate the APIs ability to handle sudden, extreme surges in traffic and ensure it recovers without failure or significant performance degradation.
- Validate that the API remains stable, responsive, and available even during abrupt traffic spikes, minimizing the risk of crashes or downtime.
- Determine how quickly the API can return to normal performance levels after the spike load subsides, ensuring smooth service continuity.

## Procedure

**Average Load Phase:**

For each endpoint simulate the typical number of concurrent virtual users to mimic average load conditions over a
period of 5 minutes, with a spike held for 1 minute.

**Monitoring Performance:**

- Response Time
  - Definition: The duration between sending a request to the API and receiving a response.
  - Measurement: This will be produced as an output of the test run.
- Error Percentage
  - Definition: The percentage of API requests that result in an error response (5xx).
  - Measurement: This will be produced as an output of the test run.

**APIs & Endpoints Under Test:**

| API           | Endpoint                                   | VUs |
|---------------|--------------------------------------------|-----|
| Establishment | `POST /school/suggest`                     | 2   |
| Establishment | `GET /school/{identifier}`                 | 2   |
| Establishment | `POST /school/{identifier}/comparators`    | 2   |
| Benchmark     | `GET /user-data`                           | 2   |
| Benchmark     | `GET /comparator-set/school/{urn}/default` | 2   |
| Insights      | `GET /school/{urn}/characteristics`        | 3   |
| Insights      | `GET /expenditure/school/{urn}`            | 3   |
| Insights      | `GET /metric-rag/default`                  | 3   |

## Performance Success Criteria

- Response Time:
  - P75 below 200ms
  - P95 below 500ms
  - P99 below 2000ms
- Error Rate:  below 1%

## Test Execution

- Load pattern: Spike
- Test duration (minutes): 6
- Spike hold time (minutes): 1
- Spike load multiplier: 10

## Test Output

**Summary Report:**

<!-- take care with final separator line in piped table, as pandoc uses this for relative column widths -->
| Load Test Name                             | Initiated on         | Duration | Response time | Errors | Throughput | Result      |
|--------------------------------------------|----------------------|----------|---------------|--------|------------|-------------|
| Establishment API - Suggest Schools        | 08/10/2024, 07:58:44 | 6m 4s    | 114 ms        | 0 %    | 63.37  /s  | [✅ Passed](https://portal.azure.com/#blade/Microsoft_Azure_CloudNativeTesting/NewReport/resourceId/%2Fsubscriptions%2Fa5c0a8d7-a54d-4a6d-ab79-4ca64a3b750f%2FresourceGroups%2Fs198t01-ebis-perf-tests%2Fproviders%2FMicrosoft.LoadTestService%2Floadtests%2Fs198t01-load-tests/testId/ebe91c02-0073-4825-be19-2d7c6401c004/testRunId/d9ada663-f552-4793-952e-e757bbeef2ec)     |
| Establishment API - Get Schools            | 08/10/2024, 07:47:34 | 6m 5s    | 47 ms         | 0 %    | 170.68 /s  | [✅ Passed](https://portal.azure.com/#blade/Microsoft_Azure_CloudNativeTesting/NewReport/resourceId/%2Fsubscriptions%2Fa5c0a8d7-a54d-4a6d-ab79-4ca64a3b750f%2FresourceGroups%2Fs198t01-ebis-perf-tests%2Fproviders%2FMicrosoft.LoadTestService%2Floadtests%2Fs198t01-load-tests/testId/d9ada663-f552-4793-952e-e757bbeef008/testRunId/d9ada663-f552-4793-952e-e757bbeef045)     |
| Establishment API - Post Comparators       | 08/10/2024, 08:07:39 | 6m 4s    | 225 ms        | 0 %    | 41.51 /s   | [✅ Passed](https://portal.azure.com/#blade/Microsoft_Azure_CloudNativeTesting/NewReport/resourceId/%2Fsubscriptions%2Fa5c0a8d7-a54d-4a6d-ab79-4ca64a3b750f%2FresourceGroups%2Fs198t01-ebis-perf-tests%2Fproviders%2FMicrosoft.LoadTestService%2Floadtests%2Fs198t01-load-tests/testId/d9ada663-f552-4793-952e-e757bbeef31d/testRunId/9f7fc3f4-594d-4141-8bc4-f7064306e085)     |
| Benchmark API - Get User Data              | 08/10/2024, 08:16:42 | 6m 8s    | 49 ms         | 0 %    | 162.79 /s  | [✅ Passed](https://portal.azure.com/#blade/Microsoft_Azure_CloudNativeTesting/NewReport/resourceId/%2Fsubscriptions%2Fa5c0a8d7-a54d-4a6d-ab79-4ca64a3b750f%2Fresourcegroups%2Fs198t01-ebis-perf-tests%2Fproviders%2Fmicrosoft.loadtestservice%2Floadtests%2Fs198t01-load-tests/testId/ee1d96eb-dd60-4f98-bab8-b071e403306d/testRunId/ee1d96eb-dd60-4f98-bab8-b071e4033115)     |
| Benchmark API - Get Default Comparator Set | 08/10/2024, 08:26:21 | 6m 9s    | 51 ms         | 0 %    | 159.85 /s  | [✅ Passed](https://portal.azure.com/#blade/Microsoft_Azure_CloudNativeTesting/NewReport/resourceId/%2Fsubscriptions%2Fa5c0a8d7-a54d-4a6d-ab79-4ca64a3b750f%2Fresourcegroups%2Fs198t01-ebis-perf-tests%2Fproviders%2Fmicrosoft.loadtestservice%2Floadtests%2Fs198t01-load-tests/testId/ee1d96eb-dd60-4f98-bab8-b071e40331b0/testRunId/ee1d96eb-dd60-4f98-bab8-b071e4033269)     |
| Insight API - Get School Characteristics   | 08/10/2024, 08:42:24 | 6m 6s    | 71 ms         | 0 %    | 192.88 /s  | [✅ Passed](https://portal.azure.com/#blade/Microsoft_Azure_CloudNativeTesting/NewReport/resourceId/%2Fsubscriptions%2Fa5c0a8d7-a54d-4a6d-ab79-4ca64a3b750f%2Fresourcegroups%2Fs198t01-ebis-perf-tests%2Fproviders%2Fmicrosoft.loadtestservice%2Floadtests%2Fs198t01-load-tests/testId/ee1d96eb-dd60-4f98-bab8-b071e40332df/testRunId/ee1d96eb-dd60-4f98-bab8-b071e403381a)     |
| Insight API - Get School Expenditure       | 08/10/2024, 08:50:26 | 5m 59s   | 71 ms         | 0 %    | 202.95 /s  | [✅ Passed](https://portal.azure.com/#blade/Microsoft_Azure_CloudNativeTesting/NewReport/resourceId/%2Fsubscriptions%2Fa5c0a8d7-a54d-4a6d-ab79-4ca64a3b750f%2Fresourcegroups%2Fs198t01-ebis-perf-tests%2Fproviders%2Fmicrosoft.loadtestservice%2Floadtests%2Fs198t01-load-tests/testId/ee1d96eb-dd60-4f98-bab8-b071e4033477/testRunId/ee1d96eb-dd60-4f98-bab8-b071e40339b6)     |
| Insight API - Get Default Metric RAG       | 08/10/2024, 08:58:59 | 6m 1s    | 76 ms         | 0 %    | 184.00 /s  | [✅ Passed](https://portal.azure.com/#blade/Microsoft_Azure_CloudNativeTesting/NewReport/resourceId/%2Fsubscriptions%2Fa5c0a8d7-a54d-4a6d-ab79-4ca64a3b750f%2Fresourcegroups%2Fs198t01-ebis-perf-tests%2Fproviders%2Fmicrosoft.loadtestservice%2Floadtests%2Fs198t01-load-tests/testId/ee1d96eb-dd60-4f98-bab8-b071e4033690/testRunId/ee1d96eb-dd60-4f98-bab8-b071e4033ba2)     |

**Findings and Recommendations:**

All test runs passed 💯

<!-- Leave the rest of this page blank -->
\newpage
