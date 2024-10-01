# Performance Test Plan - 0001 - API Average Load

**Test run date:**

September 2024

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
| Establishment | `POST /school/suggest`                     | 75 /s             |
| Establishment | `GET /school/{identifier}`                 | 75 /s             |
| Establishment | `POST /school/{identifier}/comparators`    | 75 /s             |
| Benchmark     | `GET /user-data`                           | 75 /s             |
| Benchmark     | `GET /comparator-set/school/{urn}/default` | 75 /s             |
| Insights      | `GET /school/{urn}/characteristics`        | 105 /s            |
| Insights      | `GET /expenditure/school/{urn}`            | 105 /s            |
| Insights      | `GET /metric-rag/default`                  | 105 /s            |

## Performance Success Criteria

- Response Time:
  - P50 below 100ms
  - P95 below 150ms
  - P99 below 500ms
- Error Rate:  below 0.1%

## Test Execution

- Load pattern: Linear
- Test duration (minutes): 6
- Ramp-up time (minutes): 1

## Test Output

<!-- take care with final separator line in piped table, as pandoc uses this for relative column widths -->
| Load Test Name                             | Initiated on         | Max VUs | Duration | Response time | Errors | Throughput | Result      |
|--------------------------------------------|----------------------|---------|----------|---------------|--------|------------|-------------|
| Establishment API - Suggest Schools        | 30/08/2024, 13:52:13 | 2       | 5m 36s   | 42 ms         | 0 %    | 62.57 /s   | [✅ Passed](https://portal.azure.com/#blade/Microsoft_Azure_CloudNativeTesting/NewReport/resourceId/%2Fsubscriptions%2Fa5c0a8d7-a54d-4a6d-ab79-4ca64a3b750f%2Fresourcegroups%2Fs198t01-ebis-perf-tests%2Fproviders%2Fmicrosoft.loadtestservice%2Floadtests%2Fs198t01-load-tests/testId/c5e6f0a9-6512-4d8b-b0ca-8c6ac86a2050/testRunId/0bff3c63-0302-4d60-bfeb-e882a2ea2060)     |
| Establishment API - Get School             | 30/08/2024, 13:51:23 | 2       | 5m 32s   | 28 ms         | 0 %    | 82.35 /s   | [✅ Passed](https://portal.azure.com/#blade/Microsoft_Azure_CloudNativeTesting/NewReport/resourceId/%2Fsubscriptions%2Fa5c0a8d7-a54d-4a6d-ab79-4ca64a3b750f%2Fresourcegroups%2Fs198t01-ebis-perf-tests%2Fproviders%2Fmicrosoft.loadtestservice%2Floadtests%2Fs198t01-load-tests/testId/d7f8fc2f-870c-4166-8958-3633a07e4007/testRunId/0bff3c63-0302-4d60-bfeb-e882a2ea204a)     |
| Benchmark API - Get User Data              | 30/08/2024, 14:25:47 | 2       | 5m 30s   | 30 ms         | 0 %    | 77.63 /s   | [✅ Passed](https://portal.azure.com/#blade/Microsoft_Azure_CloudNativeTesting/NewReport/resourceId/%2Fsubscriptions%2Fa5c0a8d7-a54d-4a6d-ab79-4ca64a3b750f%2Fresourcegroups%2Fs198t01-ebis-perf-tests%2Fproviders%2Fmicrosoft.loadtestservice%2Floadtests%2Fs198t01-load-tests/testId/2136802a-1ecf-4277-bf8f-d72ff72ab0e7/testRunId/2136802a-1ecf-4277-bf8f-d72ff72ab114)     |
| Benchmark API - Post Comparators           | 30/08/2024, 21:07:24 | 6       | 5m 52s   | 110 ms        | 0 %    | 69.61 /s   | [✅ Passed](https://portal.azure.com/#blade/Microsoft_Azure_CloudNativeTesting/NewReport/resourceId/%2Fsubscriptions%2Fa5c0a8d7-a54d-4a6d-ab79-4ca64a3b750f%2Fresourcegroups%2Fs198t01-ebis-perf-tests%2Fproviders%2Fmicrosoft.loadtestservice%2Floadtests%2Fs198t01-load-tests/testId/8daea33b-ffae-41d5-8790-ae77a36f1033/testRunId/ba8041df-c0e5-4007-bcad-51c53c579054)     |
| Establishment API - Post Comparators       | 30/09/2024, 15:28:43 | 6       | 5m 50s   | 111 ms        | 0 %    | 68.60 /s   | [✅ Passed](https://portal.azure.com/#blade/Microsoft_Azure_CloudNativeTesting/NewReport/resourceId/%2Fsubscriptions%2Fa5c0a8d7-a54d-4a6d-ab79-4ca64a3b750f%2FresourceGroups%2Fs198t01-ebis-perf-tests%2Fproviders%2FMicrosoft.LoadTestService%2Floadtests%2Fs198t01-load-tests/testId/8daea33b-ffae-41d5-8790-ae77a36f1033/testRunId/555c0673-d7f6-4d88-bff3-43ae26a8a273) 3️⃣ |
| Benchmark API - Get Default Comparator Set | 30/08/2024, 20:59:11 | 2       | 5m 33s   | 30 ms         | 0 %    | 76.35 /s   | [✅ Passed](https://portal.azure.com/#blade/Microsoft_Azure_CloudNativeTesting/NewReport/resourceId/%2Fsubscriptions%2Fa5c0a8d7-a54d-4a6d-ab79-4ca64a3b750f%2Fresourcegroups%2Fs198t01-ebis-perf-tests%2Fproviders%2Fmicrosoft.loadtestservice%2Floadtests%2Fs198t01-load-tests/testId/49627d01-559a-4e73-9313-649e2602906e/testRunId/13617169-5b76-4f40-8eff-f8467b6c306e)     |
| Insight API - Get School Characteristics   | 02/09/2024, 10:32:41 | 3       | 5m 46s   | 31 ms         | 0 %    | 120.86 /s  | [✅ Passed](https://portal.azure.com/#blade/Microsoft_Azure_CloudNativeTesting/NewReport/resourceId/%2Fsubscriptions%2Fa5c0a8d7-a54d-4a6d-ab79-4ca64a3b750f%2Fresourcegroups%2Fs198t01-ebis-perf-tests%2Fproviders%2Fmicrosoft.loadtestservice%2Floadtests%2Fs198t01-load-tests/testId/e21fdb84-7dbb-406c-9fb8-cc2f44144008/testRunId/bd85629a-1327-4ea6-b566-fff5cc988066)     |
| Insight API - Get School Expenditure       | 02/09/2024, 10:54:38 | 3       | 5m 48s   | 30 ms         | 0 %    | 119.87 /s  | [✅ Passed](https://portal.azure.com/#blade/Microsoft_Azure_CloudNativeTesting/NewReport/resourceId/%2Fsubscriptions%2Fa5c0a8d7-a54d-4a6d-ab79-4ca64a3b750f%2Fresourcegroups%2Fs198t01-ebis-perf-tests%2Fproviders%2Fmicrosoft.loadtestservice%2Floadtests%2Fs198t01-load-tests/testId/bd85629a-1327-4ea6-b566-fff5cc9882b7/testRunId/bd85629a-1327-4ea6-b566-fff5cc9882d5)     |
| Insight API - Get Default Metric RAG       | 02/09/2024, 11:35:13 | 3       | 5m 41s   | 32 ms         | 0 %    | 114.57 /s  | [✅ Passed](https://portal.azure.com/#blade/Microsoft_Azure_CloudNativeTesting/NewReport/resourceId/%2Fsubscriptions%2Fa5c0a8d7-a54d-4a6d-ab79-4ca64a3b750f%2Fresourcegroups%2Fs198t01-ebis-perf-tests%2Fproviders%2Fmicrosoft.loadtestservice%2Floadtests%2Fs198t01-load-tests/testId/4b81ce6f-94bc-4476-b0b3-4d6246943037/testRunId/5eb66cee-ac1a-4210-860e-2593420ee010)     |

**Findings and Recommendations:**

All test runs passed 💯

3️⃣ Search (including comparator) endpoints were refactored before this additional run took place.

<!-- Leave the rest of this page blank -->
\newpage
