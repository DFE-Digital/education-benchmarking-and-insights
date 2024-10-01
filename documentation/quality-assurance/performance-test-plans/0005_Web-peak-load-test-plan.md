# Performance Test Plan - 0005 - Web App Peak Load

**Test run date:**

September 2024

## Goal

The goal of this test is to measure how well the service performs under peak load conditions. Focus will be on verifying the critical pages.

## Objective

- Ensure that the services remains stable without crashes or timeouts during peak usage.
- Confirm that the service does not degrade significantly, and maintain performance, during expected high-traffic periods.
- Validate capacity during anticipated peak load.

## Procedure

**Determine Capacity:**

Calculate the number of `Virtual Users` required for Azure Load Testing based on a target throughput.

_NB: see previous test plans for calculation._

**Average Load Phase:**

For each page, simulate the typical number of concurrent virtual users to mimic peak load conditions over a period of 5 minutes.

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

| Page                                           | Target Throughput |
|------------------------------------------------|-------------------|
| `/school/{identifier}`                         | 83 /s             |
| `/school/{identifier}/spending-and-costs`      | 83 /s             |
| `/school/{identifier}/comparators`             | 83 /s             |
| `/school/{identifier}/find-ways-to-spend-less` | 83 /s             |
| `/trust/{identifier}`                          | 37 /s             |
| `/trust/{identifier}/spending-and-costs`       | 37 /s             |

*the target throughput is based on the assumed daily user numbers (of which 45% are academy trust users).

## Performance Success Criteria

- Response Time:
  - P75 below 500ms
  - P95 below 1000ms
  - P99 below 2000ms
- Error Rate: below 0.1%
- CPU and Memory utilisation: below 80%*
- Database performance: below 80%*

*based on production tiers P1v3 for app service & S1 (50DTU) for database.

## Test Execution

- Load pattern: Linear
- Test duration (minutes): 6
- Ramp-up time (minutes): 1

## Test Output

**Summary Report:**

<!-- take care with final separator line in piped table, as pandoc uses this for relative column widths -->
| Load Test Name             | Initiated on         | Max VUs | Duration | Response time | Errors | Throughput | Result      |
|----------------------------|----------------------|---------|----------|---------------|--------|------------|-------------|
| School Home                | 11/09/2024, 17:10:12 | 20      | 6m 0s    | 371 ms        | 0 %    | 60.85 /s   | [✅ Passed](https://portal.azure.com/#blade/Microsoft_Azure_CloudNativeTesting/NewReport/resourceId/%2Fsubscriptions%2Fa5c0a8d7-a54d-4a6d-ab79-4ca64a3b750f%2Fresourcegroups%2Fs198t01-ebis-perf-tests%2Fproviders%2Fmicrosoft.loadtestservice%2Floadtests%2Fs198t01-load-tests/testId/c661f194-eb06-4513-82fc-a3234deca16e/testRunId/c661f194-eb06-4513-82fc-a3234deca5ad)     |
| School Spending Priorities | 12/09/2024, 08:43:28 | 30      | 6m 1s    | 550 ms        | 0 %    | 66.03 /s   | [✅ Passed](https://portal.azure.com/#blade/Microsoft_Azure_CloudNativeTesting/NewReport/resourceId/%2Fsubscriptions%2Fa5c0a8d7-a54d-4a6d-ab79-4ca64a3b750f%2Fresourcegroups%2Fs198t01-ebis-perf-tests%2Fproviders%2Fmicrosoft.loadtestservice%2Floadtests%2Fs198t01-load-tests/testId/c661f194-eb06-4513-82fc-a3234deca4ae/testRunId/b96b13a1-fc89-4134-be13-f9ae4011361c)     |
| School Comparators         | 12/09/2024, 09:47:09 | 17      | 6m 6s    | 205 ms        | 0 %    | 89.17 /s   | [✅ Passed](https://portal.azure.com/#blade/Microsoft_Azure_CloudNativeTesting/NewReport/resourceId/%2Fsubscriptions%2Fa5c0a8d7-a54d-4a6d-ab79-4ca64a3b750f%2Fresourcegroups%2Fs198t01-ebis-perf-tests%2Fproviders%2Fmicrosoft.loadtestservice%2Floadtests%2Fs198t01-load-tests/testId/c661f194-eb06-4513-82fc-a3234deca344/testRunId/d550e583-5f7a-442c-a078-fc75f69be637)     |
| School Comparators         | 01/10/2024, 06:11:47 | 17      | 6m 4s    | 314 ms        | 0 %    | 59.12 /s   | [✅ Passed](https://portal.azure.com/#blade/Microsoft_Azure_CloudNativeTesting/NewReport/resourceId/%2Fsubscriptions%2Fa5c0a8d7-a54d-4a6d-ab79-4ca64a3b750f%2Fresourcegroups%2Fs198t01-ebis-perf-tests%2Fproviders%2Fmicrosoft.loadtestservice%2Floadtests%2Fs198t01-load-tests/testId/c661f194-eb06-4513-82fc-a3234deca344/testRunId/c2fd3225-1cba-4ef0-8fb3-13761161c06d) 1️⃣ |
| School Resources           | 12/09/2024, 10:28:27 | 15      | 6m 4s    | 151 ms        | 0.02 % | 97.71 /s   | [✅ Passed](https://portal.azure.com/#blade/Microsoft_Azure_CloudNativeTesting/NewReport/resourceId/%2Fsubscriptions%2Fa5c0a8d7-a54d-4a6d-ab79-4ca64a3b750f%2Fresourcegroups%2Fs198t01-ebis-perf-tests%2Fproviders%2Fmicrosoft.loadtestservice%2Floadtests%2Fs198t01-load-tests/testId/c661f194-eb06-4513-82fc-a3234deca3fe/testRunId/e815efe0-0aa3-4d21-b65b-c1d9fe79d060)     |
| Trust Home                 | 12/09/2024, 11:11:33 | 10      | 5m 55s   | 320 ms        | 0 %    | 44.47 /s   | [✅ Passed](https://portal.azure.com/#blade/Microsoft_Azure_CloudNativeTesting/NewReport/resourceId/%2Fsubscriptions%2Fa5c0a8d7-a54d-4a6d-ab79-4ca64a3b750f%2Fresourcegroups%2Fs198t01-ebis-perf-tests%2Fproviders%2Fmicrosoft.loadtestservice%2Floadtests%2Fs198t01-load-tests/testId/c661f194-eb06-4513-82fc-a3234deca6d0/testRunId/e815efe0-0aa3-4d21-b65b-c1d9fe79d4a9)     |
| Trust Home                 | 30/09/2024, 14:05:21 | 10      | 6m 1s    | 216 ms        | 0.01 % | 48.66 /s   | [✅ Passed](https://portal.azure.com/#blade/Microsoft_Azure_CloudNativeTesting/NewReport/resourceId/%2Fsubscriptions%2Fa5c0a8d7-a54d-4a6d-ab79-4ca64a3b750f%2Fresourcegroups%2Fs198t01-ebis-perf-tests%2Fproviders%2Fmicrosoft.loadtestservice%2Floadtests%2Fs198t01-load-tests/testId/c661f194-eb06-4513-82fc-a3234deca6d0/testRunId/6c5dabf3-8941-44e3-bf4e-0ad905a4c09c)     |
| Trust Spending Priorities  | 12/09/2024, 11:50:05 | 8       | 5m 55s   | 205 ms        | 0 %    | 44.68 /s   | [✅ Passed](https://portal.azure.com/#blade/Microsoft_Azure_CloudNativeTesting/NewReport/resourceId/%2Fsubscriptions%2Fa5c0a8d7-a54d-4a6d-ab79-4ca64a3b750f%2Fresourcegroups%2Fs198t01-ebis-perf-tests%2Fproviders%2Fmicrosoft.loadtestservice%2Floadtests%2Fs198t01-load-tests/testId/c661f194-eb06-4513-82fc-a3234deca6fd/testRunId/e815efe0-0aa3-4d21-b65b-c1d9fe79dddf)     |

**Findings and Recommendations:**

All test runs passed 💯

1️⃣ Comparator endpoints were refactored before this additional run took place.

<!-- Leave the rest of this page blank -->
\newpage
