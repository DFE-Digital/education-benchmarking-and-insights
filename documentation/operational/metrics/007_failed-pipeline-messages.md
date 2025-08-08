# Metric: Failed Pipeline Messages

**Category:**  
Operational

**Data Source:**  
Pipeline monitoring tools

**Purpose:**  
Ensure data processing pipelines function correctly by tracking message failures. Pipeline failures can cause data loss, delays, or incorrect processing, affecting downstream analytics, reporting, or operational decisions.

**Actionability:**  
Investigate pipeline failures and resolve errors to maintain data flow. When failed pipeline messages exceed 1:

1. Review error logs for pipeline components.
2. Retry failed messages if retry-safe.
3. Check for connectivity issues with upstream/downstream systems.
4. Patch or fix the specific processing stage causing failure.
5. Monitor pipeline throughput after remediation.

**Target / Threshold:**  
Zero failed pipeline messages during processing

**Reporting Frequency:**  
Continuous monitoring with alerting

**Notes / Links:**  

<!-- Leave the rest of this page blank -->
\newpage
