# Performance Test Plan - 0005 - Web App Peak Load

**Test run date:**

TBC

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

*the target throughput is based on the assumed daily user numbers, and 45% of being academy trust users.

## Performance Success Criteria

- Response Time:
  - P50 below 100ms
  - P95 below 150ms
  - P99 below 500ms
- Error Rate:  below 0.1%
- CPU and Memory utilisation : below 80%*
- Database performance : below 80%*

*based on production tiers P1v3 for app service & S1 (50DTU) for database. 

## Test Execution

- Load pattern: Linear
- Test duration (minutes): 5
- Ramp-up time (minutes): 1

## Test Output

**Summary Report:**

//TODO: Add summary post test run

**Findings and Recommendations:**

//TODO: Add finding and recommendation post test run
<!-- Leave the rest of this page blank -->
\newpage
