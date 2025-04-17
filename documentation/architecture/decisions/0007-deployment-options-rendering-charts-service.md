# Decision - 0007 - Deployment Options for SVG Service for Rendering Charts

Date: 2025-01-29

## Status

Accepted

## Context

The project requires an SVG rendering service for generating charts dynamically.
The service must handle chart creation efficiently, ensuring scalability and cost-effectiveness.
Several deployment options leveraging Azure services have been considered.

### Options Considered

1. Azure App Service with Express and Node.js
2. Azure App Service with Vite
3. Azure Function App with Native Charting Libraries

### Key Considerations

- Performance: The service must handle requests for chart rendering with minimal latency.
- Scalability: The solution should scale effectively with increasing workloads.
- Cost: Operational costs should be minimised without sacrificing performance.
- Maintainability: The solution must support straightforward deployment and updates.
- Bundle Size: Smaller deployment sizes improve startup time and reduce resource usage.
- Cold Starts: The **Consumption Plan** is the most cost-effective option but introduces cold start delays. The **Elastic Premium Plan** eliminates cold starts by keeping instances warm and is a fallback option if performance issues arise.

## Cost and Bundle Size Estimates

The following are cost and estimated bundle size estimates for each option:

1. Azure App Service (Node.js / Express / Vite):
    - Pricing Tier: Standard S1 (Production-ready)
        - £55.32/month for 1 instance (1 core, 1.75GB RAM).
        - Scales horizontally with additional instances (e.g., 2 instances = £110.64/month).
        - Includes 50GB of storage.
    - Bundle Size:
        - Node.js: ~15–25MB.
        - Express: ~25–40MB (middleware and routing overhead).
        - Vite: ~40–60MB (bundled assets).

2. Azure Function App (Native Charting Libraries) with Consumption Plan or Elastic Premium Plan:
    - Consumption Plan Costs:
        - £0.000016/GB-s and £0.000014/req.
        - No cost for idle time; scales automatically.
    - Elastic Premium Plan Costs (if required):
        - EP1 (1 vCPU, 3.5GB RAM): £93.41/month (1 warm instance).
        - Additional warm instances charged at £93.41/month.
    - Bundle Size:
        - Native Charting Libraries (e.g., Chart.js, D3.js): ~10–20MB.

## Decision

After evaluating the options, the preferred initial option is Azure Function App with Native Charting
Libraries on the Consumption Plan, which offers the most cost-effective and scalable solution.
If performance monitoring identifies issues with cold start latency, the Elastic Premium Plan will be
adopted as a fallback to ensure consistent performance.

### Option 1: Azure App Service with Express and Node.js

- Advantages:
  - Greater flexibility for routing and middleware.
  - Easy integration with charting libraries for dynamic SVG generation.
- Disadvantages:
  - Higher cost and maintenance overhead compared to serverless options.
  - Bundle size (~25–40MB) increases deployment time.
- Nominal Cost: Starts at £55.32/month for 1 instance.

### Option 2: Azure App Service with Vite

- Advantages:
  - Faster build times and optimised runtime for modern JavaScript frameworks.
- Disadvantages:
  - Overkill for a back-end-only service.
  - Largest bundle size (~40–60MB).
- Nominal Cost: Starts at £55.32/month for 1 instance.

### Option 3: Azure Function App with Native Charting Libraries (Preferred Option)

#### Initial Plan: Consumption Plan

- Advantages:
  - Extremely cost-effective, with pay-as-you-go pricing.
  - Scales automatically to handle variable workloads.
  - Minimal bundle size (~10–20MB).
- Disadvantages:
  - Cold starts may introduce latency during low activity periods.
- Nominal Cost: Based on usage: £0.000016/GB-s and £0.000014/request.

#### Fallback Plan: Elastic Premium Plan

- Advantages:
  - Eliminates cold starts by keeping instances warm.
  - Ensures consistent performance for high-demand workloads.
- Disadvantages:
  - Higher base cost compared to the Consumption Plan.
- Nominal Cost: Starts at £93.41/month for 1 warm instance.

## Recommendation

### Preferred Option: Azure Function App with Native Charting Libraries

- Start with the **Consumption Plan** to minimise costs and scale efficiently for expected workloads.
- Monitor application performance and evaluate cold start latency. If cold start latency becomes a concern, transition to the **Elastic Premium Plan** to eliminate cold start delays.

## Consequences

- Initially minimises costs with pay-as-you-go pricing.
- Elastic Premium Plan guarantees consistent performance if cold starts are an issue.

## Notes

### Proof of concept outcome

Multiple implementation and deployment combinations leveraging Node.js Azure Function Apps have been considered and validated via a proof-of-concept.

**Options validated:**

1. Node.js Azure Function App with d3
2. Node.js Azure Function App with Chart.js
3. Node.js Azure Function App with Consumption Plan
4. Node.js Azure Function App with Elastic Premium Plan

**Key Considerations:**

- Performance: The chart server side rendering (SSR) service must handle requests for chart rendering with minimal latency.
- Scalability: The solution should scale effectively with increasing workloads.
- Cost: Operational costs should be minimised without sacrificing performance.
- Cold Starts: The **Consumption Plan** is the most cost-effective option but introduces cold start delays. The **Elastic Premium Plan** eliminates cold starts by keeping instances warm and scaling to a defined maximum number of instances based on load.

**Performance:**

Full details of the load tests summarised below may be found [here](../../quality-assurance/performance-test-plans/0007_API-Web-ssr-charts-load-test-plan.md).

**Spending and Priorities page:**

This page was chosen for the proof-of-concept as it contains multiple, but relatively simple vertical bar charts.
Load testing against a page as per a real user, including downloading of embedded resources, was executed in Average and Peak traffic scenarios both with and without SSR charts.

| Run           | Load    | API Infrastructure        | Response time | Throughput |
|---------------|---------|---------------------------|---------------|------------|
| Baseline      | Average |                           | 359 ms        | 92.53 /s   |
| SSR d3 charts | Average | `EP1` / 10 burst / 1 warm | 494 ms        | 74.49 /s   |
| SSR d3 charts | Average | `Y1`                      | 441 ms        | 86.85 /s   |
| Baseline      | Peak    |                           | 679 ms        | 126.74 /s  |
| SSR d3 charts | Peak    | `EP1` / 10 burst / 1 warm | 691 ms        | 115.30 /s  |
| SSR d3 charts | Peak    | `Y1`                      | 839 ms        | 95.42 /s   |

**Chart Rendering API:**

Average, Peak, Spike and Soak tests were also executed against the underlying SSR service directly in order to monitor and validate performance of the various options defined above. Although the API is much more user friendly, performance using [Chart.js](https://www.chartjs.org/) was poor compared to [d3](https://d3js.org/) and has not been documented below. Similarly, using [xmldom](https://www.npmjs.com/package/@xmldom/xmldom) instead of [jsdom](https://www.npmjs.com/package/jsdom) for the virtual DOM required by d3 was the most performant combination, along with [worker_threads](https://nodejs.org/api/worker_threads.html).

| Load Test Name | API Infrastructure        | Response time | Throughput |
|----------------|---------------------------|---------------|------------|
| Average API    | `EP1` / 10 burst / 1 warm | 77 ms         | 82.88 /s   |
| Average API    | `Y1`                      | 73 ms         | 88.08 /s   |
| Peak API       | `EP1` / 10 burst / 1 warm | 81 ms         | 173.65 /s  |
| Peak API       | `Y1`                      | 118 ms        | 171.79 /s  |
| Soak API       | `EP1` / 10 burst / 1 warm | 72 ms         | 236.75 /s  |
| Soak API       | `Y1`                      | 118 ms        | 141.80 /s  |
| Spike API      | `EP1` / 10 burst / 1 warm | 81 ms         | 210.29 /s  |
| Spike API      | `Y1`                      | 145 ms        | 110.49 /s  |

> **NOTE:** Linux consumption plans do not support performance metrics ([[1]](https://learn.microsoft.com/en-us/azure/azure-functions/monitor-functions-reference?tabs=consumption-plan#supported-metrics-for-microsoftwebsites), [[2]](https://learn.microsoft.com/en-us/azure/azure-functions/monitor-functions?tabs=portal#azure-monitor-platform-metrics)) so monitoring the service at `Y1` is limited. `EP1` includes these metrics, and so alerting may therefore also be configured as required, and as implemented in the proof-of-concept.

**Decision:**

1. Chart rendering service should be implemented using d3
2. Elastic Premium plan should be used by the service

**Next Steps:**

[User story](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/258039) to be resolved that completes the implementation of server side charts fully on one page within FBIT. Progressive enhancement may then be used to add additional functionality to the page to enable (for example) copy and saving of chart images. The dependency on `front-end-components` may then be removed completely for this particular page.

<!-- Leave the rest of this page blank -->
\newpage
