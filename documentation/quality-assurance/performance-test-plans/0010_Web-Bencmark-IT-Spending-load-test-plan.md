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
| `/school/{identifier}/benchmark-it-spending` | 50 /s             | Average   | S1      |
| `/school/{identifier}/benchmark-it-spending` | 80 /s             | Peak      | S2      |

## Performance Success Criteria

Response Time (Full Page Load):
- P75 below 1500ms
- P95 below 2500ms
- P99 below 4000ms
- Error Rate: below 0.1%
- CPU and Memory utilisation: below 80%
- Database DTU utilisation: below 80%

## Test Execution

- Load pattern: Linear
- Test duration (minutes): 6
- Ramp-up time (minutes): 1

## Test Output

| Load Test Name                        | Initiated on         | Max VUs | Duration | Response time (P75) | Errors (%) | Throughput (req/s) | Result |
|---------------------------------------|----------------------|---------|----------|---------------------|-----------|--------------------|--------|
| Average Web – Benchmark IT spending   | 08/08/2025, 01:31:36 | 15      | 6m       | 1503ms              | 0.03%     | 21.87/s            | TBD    |
| Peak Web – Benchmark IT spending      | 08/08/2025, 02:25:43 | 30      | 6m       | 2004ms              | 0%         | 34.03/s            | TBD    |

**Findings and Recommendations:**

To be completed post-test. 


<!-- Leave the rest of this page blank -->
\newpage
