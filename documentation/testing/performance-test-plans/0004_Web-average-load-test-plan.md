# Performance Test Plan - 0004 - Web App Average Load

**Test run date:**

TBC

## Goal

The goal of this test is to measure how well the service performs under typical load conditions. Focus will be on verifying the critical pages.

## Objective

- Establish a performance baseline under typical load conditions.
- Simulate real-world conditions to understand the services responsiveness during average traffic patterns.
- Ensure that the service remains stable without crashes or timeouts  during normal usage.
- Confirm that the service deliver consistent performance over time and does not degrade with continued use.

## Procedure

**Determine Capacity:**

Calculate the number of `Virtual Users` required for Azure Load Testing based on a target throughput.

_NB: see previous test plans for calculation._

**Average Load Phase:**

For each page, simulate the typical number of concurrent virtual users to mimic average load conditions over a period of 5 minutes.

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
| `/school/{identifier}`                         | 50 /s             |
| `/school/{identifier}/spending-and-costs`      | 50 /s             |
| `/school/{identifier}/comparators`             | 50 /s             |
| `/school/{identifier}/find-ways-to-spend-less` | 50 /s             |
| `/trust/{identifier}`                          | 23 /s             |
| `/trust/{identifier}/spending-and-costs`       | 23 /s             |

*the target throughput is based on the assumed daily user numbers, and 45% of being academy trust users.

## Performance Success Criteria

- Response Time:
  - P50 below 100ms
  - P95 below 150ms
  - P99 below 500ms
- Error Rate:  below 0.1%
- CPU and Memory utilisation : below 50%*
- Database performance : below 50%*

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
