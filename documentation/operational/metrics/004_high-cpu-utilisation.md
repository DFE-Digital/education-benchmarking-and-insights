# Metric: High CPU Utilisation

**Category:**  
Operational

**Data Source:**  
Server monitoring / metrics platform

**Purpose:**  
Detect periods of excessive CPU load that may degrade application responsiveness and throughput. High CPU utilisation can indicate inefficient code, resource bottlenecks, or scaling issues that need attention to maintain performance.

**Actionability:**  
Identify CPU-intensive processes and optimise or scale resources. If CPU usage exceeds 85% for more than 5 minutes:

1. Identify processes consuming the most CPU via monitoring tools.
2. Scale out infrastructure or redistribute workloads if load-related.
3. Investigate inefficient code or runaway processes if abnormal.
4. Review historical trends to determine if this is part of normal usage patterns.
5. Apply optimisations or resource adjustments as needed.

**Target / Threshold:**  
CPU utilisation below 85% sustained

**Reporting Frequency:**  
Real-time with alerting

**Notes / Links:**

<!-- Leave the rest of this page blank -->
\newpage
