# API Average Load Testing Plan

## Goal

Ensure that the APIs consistently perform well under typical operating conditions, with a particular focus on verifying that the most used and critical endpoints are robust, responsive, and functioning as expected. The goal is to confirm that response time, error rates, and throughput meets the required performance standards. 

## Objective

- Assess APIs stability under typical load conditions.
- Ensure APIs responsiveness during average load.
- Understand performance metrics and capacity thresholds under regular load.

## Procedure

### Determine Capacity

Calculate the average number of requests that each API handles simultaneously. We also need to determine number of concurrent users, test duration and ramp up period. 

### Average Load Phase

For each endpoint, simulate the typical number of concurrent virtual users to mimic average load conditions over a period of 5 minutes.

### Monitoring Performance

#### Response Time

- **Definition**: The duration between sending a request to the API and receiving a response.
- **Measurement**: This will be produced as an output of the test run.

#### Error Percentage

- **Definition**: The percentage of API requests that result in an error response. We will focus on 5xx errors. 
  compared to the total number of requests.
- **Measurement**: This will be produced as an output of the test run.

#### Throughput

- **Definition**: The number of API requests processed per second.
- **Measurement**: This will be produced as an output of the test run.

## Performance Success Criteria

- Response Time - less than ~500ms
- Error Rate - below 1%
- Throughput - defined below for each API

## APIs and Endpoints

For this test, we will focus exclusively on the critical and high demand endpoints. 

### Establishment API

The average requests on this API is 4500 requests per minute.
#### Endpoints
1. `Post /school/suggest`
2. `Get /school/{identifier}`

### Benchmark API
The average requests on this API is 4500 requests per minute.

#### Endpoints 
1. `Get /user-data`
2. `Post /comparators/schools`

### Insights API
The average requests on this API is 6300 requests per minute.
#### Endpoints

1. `Get /school/{urn}/characteristics`


## Test Execution

For each API endpoint we will increase the requests to the total number identified earlier gradually. Starting with 250
concurrent users, and variable engine size to meet the requests count with a ramp up period of 2 minutes. The test will then run for 5 minutes. 

## Test Output

### Summary Report

We will provide a summary of the test results, including a detailed report on the APIs performance against the
specified metrics.

### Findings and Recommendations

We will share the findings from the test along with recommendations with the wider team.