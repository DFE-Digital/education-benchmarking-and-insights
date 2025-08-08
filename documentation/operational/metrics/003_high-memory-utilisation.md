# Metric: High Memory Utilisation

**Category:**  
Operational

**Data Source:**  
Server monitoring / metrics platform

**Purpose:**  
Identify scenarios where memory consumption is unusually high and sustained, which could lead to performance degradation, application crashes, or out-of-memory errors. Monitoring helps prevent instability and supports capacity planning and troubleshooting.

**Actionability:**  
Investigate memory leaks or heavy usage and consider scaling or restarting services. If memory usage exceeds 85% for more than 5 minutes:

1. Identify high-memory processes using system monitoring tools.
2. Restart the affected process or service if it is safe to do so.
3. Review recent deployments for changes to memory usage patterns.
4. Investigate memory leaks via heap dumps if this is recurring.
5. Plan capacity increases if sustained usage is due to legitimate load growth.

**Target / Threshold:**  
Memory utilisation stays below 85% for sustained periods

**Reporting Frequency:**  
Real-time monitoring with alerting on breach

**Notes / Links:**  

<!-- Leave the rest of this page blank -->
\newpage
