# Chart Rendering API Performance and Scalability Improvements (0015)

Date: 2025-08-21

## Status

Accepted

## Context

The Chart Rendering API (implemented as an Azure Function App) was extended with a new chart type. Following this addition, several performance and scalability concerns were raised:

- Inconsistent load test results for Average and Peak load profiles.
- High CPU usage during rendering operations.
- Azure Functions scale controller for elastic plans does not respond directly to CPU load, causing delays in scaling.
- Consideration was given to moving to a dedicated plan, where scaling rules could be tied to CPU metrics. However, delays would still exist since the workload is CPU-bound.

## Analysis

- The initial implementation used D3.js to generate SVGs, requiring ownership of the DOM. This introduced a bottleneck in rendering.
- High CPU consumption and memory usage were aggravated by worker thread pooling, dynamic imports, and broad module inclusion.

## Decision

To address these challenges, the following improvements were adopted:

1. SVG Templating Approach:
   - Moved away from D3-driven DOM manipulation.
   - Implemented a lightweight templating approach for SVG generation.
   - Removed the DOM ownership constraint, reducing CPU overhead.

2. Code & Import Optimizations
   - Eliminated worker thread pooling, which was adding overhead.
   - Ensured static imports are used.
   - Imported only required modules, minimizing memory footprint.

3. App Service Plan Adjustment
   - Following optimizations, the App Service Plan was reduced from EP1 (Elastic Premium) to Y1 (Consumption Plan).
   - This change resulted in significant cost savings with no loss in performance.

## Consequences

- Positive:
  - Lower CPU and memory usage.
  - Improved consistency in load test results.
  - Reduced risk of delayed scaling due to CPU-bound bottlenecks.
  - Significant cost savings by moving from EP1 to Y1 without performance degradation.
  - More efficient and predictable API performance.

- Negative / Trade-offs:
  - D3’s flexibility for complex SVG manipulation is no longer leveraged.

<!-- Leave the rest of this page blank -->
\newpage
