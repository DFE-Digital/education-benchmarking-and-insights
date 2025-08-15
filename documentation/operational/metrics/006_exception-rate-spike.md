# Metric: Exception Rate Spike

**Category:**  
Operational

**Data Source:**  
Application logs / error tracking systems

**Purpose:**  
Monitor sudden increases in unhandled exceptions or errors within the application, which often precede outages or major bugs. Identifying spikes quickly enables targeted fixes before problems escalate.

**Actionability:**  
Investigate root causes and fix bugs causing the exceptions. When exception rates spike:

1. Check logs to identify the exception type(s) and frequency.
2. Correlate with recent code deployments or configuration changes.
3. Roll back or patch faulty code if the cause is confirmed.
4. Add additional error handling where appropriate.
5. Document findings in incident reports for learning and prevention.

**Target / Threshold:**  
Exception rate remains within normal baseline ranges

**Reporting Frequency:**  
Real-time alerting

**Notes / Links:**  

<!-- Leave the rest of this page blank -->
\newpage
