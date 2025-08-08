# Metric: Failed Request/Dependency Spike

**Category:**  
Operational

**Data Source:**  
Monitoring systems / logs

**Purpose:**  
Detect abnormal rises in failed external or internal service calls, indicating possible network issues, service outages, or capacity problems. Early alerts enable prompt diagnosis and mitigation, preserving system integrity.

**Actionability:**  
Investigate failures and mitigate causes to restore service health. If failed requests or dependency calls increase sharply:

1. Identify which service(s) or endpoint(s) are failing.
2. Cross-check with availability and latency metrics for the same period.
3. Retry failed requests if this is a transient spike.
4. Notify dependency owners if the issue is external.
5. Implement mitigation steps such as load shedding or failover routing.

**Target / Threshold:**  
Failure rates remain within historical norms

**Reporting Frequency:**  
Real-time alerting

**Notes / Links:**  

<!-- Leave the rest of this page blank -->
\newpage
