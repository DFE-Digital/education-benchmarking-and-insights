# Decision - 0007 - Deployment Options for SVG Service for Rendering Charts

Date: 2025-01-29

## Status

Proposed

## Context

The project requires an SVG rendering service for generating charts dynamically.
The service must handle chart creation efficiently, ensuring scalability and cost-effectiveness.
Several deployment options leveraging Azure services have been considered.

### Options Considered

1. Azure App Service with Express and Node.js
2. Azure App Service with Vite
3. Azure Function App with Native Charting Libraries

### Key Considerations

- Performance:** The service must handle requests for chart rendering with minimal latency.
- Scalability:** The solution should scale effectively with increasing workloads.
- Cost: Operational costs should be minimised without sacrificing performance.
- Maintainability: The solution must support straightforward deployment and updates.
- Bundle Size: Smaller deployment sizes improve startup time and reduce resource usage.
- Cold Starts: The **Consumption Plan** is the most cost-effective option but introduces cold start delays. The **Elastic Premium Plan** eliminates cold starts by keeping instances warm and is a fallback option if performance issues arise.

## Cost and Bundle Size Estimates

The following are cost and estimated bundle size estimates for each option:

1. Azure App Service (Node.js / Express / Vite):
    - Pricing Tier: Standard S1 (Production-ready)
        - £55.32/month** for 1 instance (1 core, 1.75GB RAM).
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

**Next Steps:**

1. Implement a proof of concept using Azure Function App with Native Charting Libraries on the **Consumption Plan** to validate performance and cost.
2. Monitor performance metrics and cold start latency during low activity periods.
3. Transition to the **Elastic Premium Plan** if necessary to maintain consistent performance.

<!-- Leave the rest of this page blank -->
\newpage
