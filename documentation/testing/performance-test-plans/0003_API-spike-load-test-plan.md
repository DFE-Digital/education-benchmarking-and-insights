# Performance Test Plan - 0003 - API Spike Load

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
| Benchmark     | `GET /user-data`                           | 2   |
| Benchmark     | `POST /comparators/schools`                | 6   |
| Benchmark     | `GET /comparator-set/school/{urn}/default` | 2   |
| Insights      | `GET /school/{urn}/characteristics`        | 3   |
| Insights      | `GET /expenditure/school/{urn}`            | 3   |
| Insights      | `GET /metric-rag/default`                  | 3   |

## Performance Success Criteria

- Response Time:
    - P75 below 200ms
    - P95 below 300ms
    - P99 below 500ms
- Error Rate:  below 1%

## Test Execution

- Load pattern: Spike
- Test duration (minutes): 5
- Spike hold time (minutes): 1
- Spike load multiplier: 10

## Test Output

**Summary Report:**

//TODO: Add summary post test run

**Findings and Recommendations:**

//TODO: Add finding and recommendation post test run