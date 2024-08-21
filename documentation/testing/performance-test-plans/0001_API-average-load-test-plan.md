# Performance Test Plan - 0001 - API Average Load

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

| API           | Endpoint                            | Target Throughput (requests per second) |
|---------------|-------------------------------------|-----------------------------------------|
| Establishment | `POST /school/suggest`              | 75                                      |
| Establishment | `GET /school/{identifier}`          | 75                                      |
| Benchmark     | `GET /user-data`                    | 75                                      |
| Benchmark     | `POST /comparators/schools`         | 75                                      |
| Insights      | `GET /school/{urn}/characteristics` | 105                                     |


## Performance Success Criteria

- Response Time - `TBC`
- Error Rate - below 0.1%

## Test Execution

- Load pattern: Linear
- Test duration (minutes): 5
- Ramp-up time (minutes): 1

## Test Output

**Summary Report:**

//TODO: Add summary post test run

**Findings and Recommendations:**

//TODO: Add finding and recommendation post test run