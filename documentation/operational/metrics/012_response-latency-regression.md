# Metric: Response Latency Regression

**Category:**  
Operational

**Data Source:**  
Monitoring and tracing systems  

**Purpose:**  
Track increases in response times for user requests, which directly affect user satisfaction and perceived performance. Detecting regressions allows investigation of recent changes or load conditions causing slowness.

**Actionability:**  
Investigate performance regressions and implement optimisations. When request latency increases:

1. Compare latency before/after recent deployments.
2. Check for resource constraints (CPU, memory, network).
3. Identify slow endpoints from application performance monitoring.
4. Optimise queries, caching, or code paths causing delays.
5. Monitor after changes to confirm improvement.

**Target / Threshold:**  
Maintain response times within baseline ranges

**Reporting Frequency:**  
Real-time with alerting

**Notes / Links:**  

<!-- Leave the rest of this page blank -->
\newpage
