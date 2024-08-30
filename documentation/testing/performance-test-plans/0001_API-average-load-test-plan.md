# Performance Test Plan - 0001 - API Average Load

**Test run date:**

September 2024

## Goal

The goal of this test is to measure how well an APIs perform under typical load conditions. Focus will be on verifying the high demand and critical endpoints.

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

| API           | Endpoint                                   | Target Throughput<br/>(requests per second) |
|---------------|--------------------------------------------|---------------------------------------------|
| Establishment | `POST /school/suggest`                     | 75                                          |
| Establishment | `GET /school/{identifier}`                 | 75                                          |
| Benchmark     | `GET /user-data`                           | 75                                          |
| Benchmark     | `POST /comparators/schools`                | 75                                          |
| Benchmark     | `GET /comparator-set/school/{urn}/default` | 75                                          |
| Insights      | `GET /school/{urn}/characteristics`        | 105                                         |
| Insights      | `GET /expenditure/school/{urn}`            | 105                                         |
| Insights      | `GET /metric-rag/default`                  | 105                                         |

## Performance Success Criteria

- Response Time:
  - P50 below 100ms
  - P95 below 150ms
  - P99 below 500ms
- Error Rate:  below 0.1%

## Test Execution

- Load pattern: Linear
- Test duration (minutes): 5
- Ramp-up time (minutes): 1

## Test Output

| Load Test Name                             | Endpoint                                          | Initiated on         | Virtual Users<br/>(maximum) | Duration | Response time<br/>(90th %ile) | Errors | Throughput | Result                                                                                                                                                                                                                                                                                                                                                                     |
|--------------------------------------------|---------------------------------------------------|----------------------|-----------------------------|----------|-------------------------------|--------|------------|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| Establishment API - Suggest Schools        | `POST /school/suggest`                            | 30/08/2024, 13:52:13 | 2                           | 5m 36s   | 42 ms                         | 0 %    | 62.57 /s   | [✅ Passed](https://portal.azure.com/#blade/Microsoft_Azure_CloudNativeTesting/NewReport/resourceId/%2Fsubscriptions%2Fa5c0a8d7-a54d-4a6d-ab79-4ca64a3b750f%2Fresourcegroups%2Fs198t01-ebis-perf-tests%2Fproviders%2Fmicrosoft.loadtestservice%2Floadtests%2Fs198t01-load-tests/testId/c5e6f0a9-6512-4d8b-b0ca-8c6ac86a2050/testRunId/0bff3c63-0302-4d60-bfeb-e882a2ea2060) |
| Establishment API - Get School             | `GET /school/{identifier}`                        | 30/08/2024, 13:51:23 | 2                           | 5m 32s   | 28 ms                         | 0 %    | 82.35 /s   | [✅ Passed](https://portal.azure.com/#blade/Microsoft_Azure_CloudNativeTesting/NewReport/resourceId/%2Fsubscriptions%2Fa5c0a8d7-a54d-4a6d-ab79-4ca64a3b750f%2Fresourcegroups%2Fs198t01-ebis-perf-tests%2Fproviders%2Fmicrosoft.loadtestservice%2Floadtests%2Fs198t01-load-tests/testId/d7f8fc2f-870c-4166-8958-3633a07e4007/testRunId/0bff3c63-0302-4d60-bfeb-e882a2ea204a) |
| Benchmark API - Get User Data              | `GET /user-data`                                  | 30/08/2024, 14:25:47 | 2                           | 5m 30s   | 30 ms                         | 0 %    | 77.63 /s   | [✅ Passed](https://portal.azure.com/#blade/Microsoft_Azure_CloudNativeTesting/NewReport/resourceId/%2Fsubscriptions%2Fa5c0a8d7-a54d-4a6d-ab79-4ca64a3b750f%2Fresourcegroups%2Fs198t01-ebis-perf-tests%2Fproviders%2Fmicrosoft.loadtestservice%2Floadtests%2Fs198t01-load-tests/testId/2136802a-1ecf-4277-bf8f-d72ff72ab0e7/testRunId/2136802a-1ecf-4277-bf8f-d72ff72ab114) |
| Benchmark API - Post Comparators           | `POST /comparators/schools`                       | 30/08/2024, 21:07:24 | 6                           | 5m 52s   | 110 ms                        | 0 %    | 69.61 /s   | [✅ Passed](https://portal.azure.com/#blade/Microsoft_Azure_CloudNativeTesting/NewReport/resourceId/%2Fsubscriptions%2Fa5c0a8d7-a54d-4a6d-ab79-4ca64a3b750f%2Fresourcegroups%2Fs198t01-ebis-perf-tests%2Fproviders%2Fmicrosoft.loadtestservice%2Floadtests%2Fs198t01-load-tests/testId/8daea33b-ffae-41d5-8790-ae77a36f1033/testRunId/ba8041df-c0e5-4007-bcad-51c53c579054) |
| Benchmark API - Get Default Comparator Set | `GET /comparator-set/school/{identifier}/default` | 30/08/2024, 20:59:11 | 2                           | 5m 33s   | 30 ms                         | 0 %    | 76.35 /s   | [✅ Passed](https://portal.azure.com/#blade/Microsoft_Azure_CloudNativeTesting/NewReport/resourceId/%2Fsubscriptions%2Fa5c0a8d7-a54d-4a6d-ab79-4ca64a3b750f%2Fresourcegroups%2Fs198t01-ebis-perf-tests%2Fproviders%2Fmicrosoft.loadtestservice%2Floadtests%2Fs198t01-load-tests/testId/49627d01-559a-4e73-9313-649e2602906e/testRunId/13617169-5b76-4f40-8eff-f8467b6c306e) |

**Findings and Recommendations:**

//TODO: Add finding and recommendation post test run

## Azure Load Testing Nuances

The items below are not exhaustive, nor terminal. They are just some things to be aware of when authoring and executing the load tests.

### Sometimes parameters are cleared

When editing an existing Load Test, ensure that the Parameters page of the settings does not get cleared unexpectedly.

### CSV files

Apostrophes in source CSV files sometimes cause POST-ed JSON to be malformed, possibly leading to `400` or `500` responses from the API.
Additionally, hyphens do not seem to be supported when defining CSV column names.

### Editing tests takes time

It takes a short period of time for Azure Portal to process changes to tests.
Errors will be raised when making additional changes while previous ones are being processed.
Running a new test too early may lead to the wrong version of the test configuration being used.

### Running tests takes time

It takes around 5 minutes to provision the load test run, during which time there is no feedback in Azure Portal.
It takes around 5 minutes to deprovision the load test run, during which time sometimes the test run is marked as 'Done' even though the results are not yet available.

### Limited usefulness of Debug mode

Test runs may be exeucted in 'Debug' mode which adds more detail to the logs available once the wrong run is complete via the 'Copy artifacts' button.
However, the logs do not include POST-ed data, but do include Secret parameters.

> **💡 Tip:** To view the artifacts, copy the SAS URL from the 'Copy artifacts' request and paste into Azure Storage Explorer > Connect > Blob container > SAS URL

### App components

Linking server side components to each load test does provide useful analytics after a short delay, however for those that consume Azure Search components nothing is shown. This appears to be a configuration issue with Azure Search rather than Azure Load Testing, however (even though the correct Diagnostic Settings appear to be pesent).

### Traffic source

It is possible to configure requests to be sourced from different locations, but due to the relative low numbers of requests for average load testing only a single engine is required, and locations are set per-engine. The location `West Europe` has therefore been configured everywhere.

### Buggy UI

Azure Portal is a little clunky when trying to navigate between tests and test runs, often erroneously taking you right back to the Portal landing page.
