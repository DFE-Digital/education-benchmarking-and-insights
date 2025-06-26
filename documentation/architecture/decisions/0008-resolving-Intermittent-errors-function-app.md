# Decision - 0008 - Resolving Intermittent 502, 503, and 429 Errors on Azure Function App

Date: 2025-02-10

## Status

Accepted

## Context

We have observed intermittent 502, 503, and 429 errors occurring on an Azure Function App during cold starts,
either when scaling from zero instances or during auto-scaling events.
These errors do not occur under sustained load or during load tests.

### Technical Background

- 502 Bad Gateway & 503 Service Unavailable: Typically occur when the function instance is not ready to accept traffic.
- 429 Too Many Requests: Happens when requests exceed the throttling limits during scaling events.
- Cold start delays on the Consumption Plan can contribute to these errors.
- Current costs for three function apps total £1 per month.

## Decision

We recommend a combination of Option 1 (Fine-Tune Front-End Resilience) and
Option 2(More Granular Function Apps on Consumption Plan) as the preferred approach.

**Rationale:**

- Maintains the extremely low cost of £1 per month.
- Fine-tuning retry mechanisms helps reduce impact on users without increasing costs.
- Splitting into more granular function apps allows for better scaling control while remaining on the Consumption Plan.
- Option 3 (Elastic Premium) is not justified given the current cost constraints and workload characteristics.

### Option 1: Fine-Tune Front-End Resilience Mechanisms

The front-end web application already has retry mechanisms and a circuit breaker pattern to handle transient failures.
Fine-tune retry timing to minimize impact on users and adjust logic to better handle cold starts.
This approach relies on natural scaling behaviors without modifying the function architecture or hosting plan.

**Pros:**

- No additional cost.
- No infrastructure changes required.
- Already aligned with best practices for handling transient errors.
- Reduces end-user impact with improved retry logic.

**Cons:**

- Does not eliminate cold starts or reduce error occurrences.
- Potentially higher request failures during traffic spikes if retry logic is not optimized correctly.

### Option 2: More Granular Function Apps on Consumption Plan

Split the function app into multiple smaller, granular function apps.
This reduces the need for scaling each function app, thus minimizing cold start occurrences.
Functions can be grouped by similar execution characteristics (e.g., latency-sensitive vs. background processing).

**Pros:**

- Remains on low-cost Consumption Plan.
- Reduces cold start impact as scaling requirements are lower per function app.
- More predictable scaling behavior.

**Cons:**

- Increased operational complexity (managing multiple function apps).
- Deployment and monitoring become more distributed.
- Does not eliminate cold starts but reduces their frequency.

### Option 3: Move to Elastic Premium Plan

Switch to the Elastic Premium Plan, which supports pre-warmed instances to eliminate cold starts.
Allows for faster scaling beyond pre-warmed instances when needed.
Enables VNET integration and higher execution limits if required.

**Pros:**

- Eliminates cold starts, reducing transient failures.
- Supports more predictable scaling and performance.
- Provides better reliability for critical workloads.

**Cons:**

- Significantly higher cost compared to the Consumption Plan.
- May require careful tuning of pre-warmed instances to balance cost and performance.
- Unnecessary cost increase if existing traffic patterns do not justify premium instances.

## Consequences

- Maintains low cost, reduces cold starts, and minimizes user impact.
- Cold starts are reduced but not eliminated completely, requiring continued monitoring and potential adjustments.

**Next Steps:**

- Fine-tune retry and circuit breaker logic in the front-end application to handle cold starts more effectively.
- Evaluate function app grouping and identify logical separation points for breaking functions into smaller apps.
- Monitor Azure Scale Controller behavior and adjust configurations.
- Reassess after implementation to determine if further actions are needed.

<!-- Leave the rest of this page blank -->
\newpage
