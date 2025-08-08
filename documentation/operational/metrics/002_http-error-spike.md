# Metric: HTTP Error Spike

**Category:**  
Operational

**Data Source:**  
Web server logs / monitoring tools  

**Purpose:**  
Detect server-side failures that prevent successful request completion. Even a small number of 5xx errors can signal backend issues, causing user-facing errors and loss of trust. Monitoring this helps maintain service reliability and quickly addresses systemic faults.

**Actionability:**  
Investigate server errors promptly to minimise impact and prevent wider outages. When the 5xx error count exceeds the threshold:

1. Check logs to identify which endpoint(s) are failing.
2. Review application error tracking tools for matching stack traces.
3. Roll back recent changes if the issue coincides with a deployment.
4. If caused by a dependency, contact the relevant team for resolution.
5. Monitor closely after mitigation to confirm resolution.

**Target / Threshold:**  
No more than 1 HTTP 5xx error in the alerting window

**Reporting Frequency:**  
Continuous monitoring with immediate alerting

**Notes / Links:**  

<!-- Leave the rest of this page blank -->
\newpage
