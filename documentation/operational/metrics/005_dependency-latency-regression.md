# Metric: Dependency Latency Regression

**Category:**  
Operational

**Data Source:**  
Service tracing / monitoring tools

**Purpose:**  
Track if downstream service calls are becoming slower than expected, as this can cascade to impact overall system responsiveness. Early detection allows teams to engage with dependency owners or adjust timeouts and retries before user experience degrades.

**Actionability:**  
Investigate and resolve causes of increased latency in dependent services. If dependency calls slow down significantly:

1. Verify latency data in service tracing tools.
2. Contact dependency owners to confirm whether they are experiencing issues.
3. Adjust timeouts or circuit breakers to mitigate cascading failures.
4. Implement retries with exponential backoff if safe.
5. Monitor after changes to ensure latency returns to baseline.

**Target / Threshold:**  
Maintain dependency call latency within acceptable historical range

**Reporting Frequency:**  
Continuous monitoring with alerts on deviation

**Notes / Links:**  

<!-- Leave the rest of this page blank -->
\newpage
