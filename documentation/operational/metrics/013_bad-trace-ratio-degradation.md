# Metric: Bad Trace Ratio Degradation

**Category:**  
Operational

**Data Source:**  
Tracing system

**Purpose:**  
Measure the health of system tracing by monitoring the proportion of traces logged as Warning, Error, or Fatal. An increasing ratio indicates growing instability or errors, guiding root cause analysis and quality improvement.

**Actionability:**  
Investigate causes of bad traces and implement stability improvements. If the percentage of bad traces rises:

1. Investigate high-severity trace entries for root causes.
2. Correlate issues with recent changes or deployments.
3. Address code or infrastructure issues causing errors.
4. Improve logging context to better capture error details.
5. Validate improvement by tracking the bad trace ratio after fixes.

**Target / Threshold:**  
Bad trace percentage remains stable or improves over time

**Reporting Frequency:**  
Continuous monitoring with alerts on degradation

**Notes / Links:**  

<!-- Leave the rest of this page blank -->
\newpage
