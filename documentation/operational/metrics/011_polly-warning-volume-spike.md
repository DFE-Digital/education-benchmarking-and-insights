# Metric: Polly Warning Volume Spike

**Category:**  
Operational

**Data Source:**  
Polly warnings logs

**Purpose:**  
Monitor overall increase in Polly warnings, signalling retries, throttling, or transient failures that could affect service reliability. Prompt investigation can reduce impact and improve system robustness.

**Actionability:**  
Review Polly warnings and identify systemic issues causing volume spikes. When Polly warnings exceed the threshold:

1. Identify which calls are triggering Polly’s retry policies.
2. Check dependency health for errors or throttling.
3. Optimise retry logic to avoid overwhelming the dependency.
4. Apply circuit breaker patterns if the issue is sustained.
5. Verify resolution by tracking warning count reduction.

**Target / Threshold:**  
Polly warnings below 10 in the alerting window

**Reporting Frequency:**  
Continuous monitoring with alerting

**Notes / Links:**  

<!-- Leave the rest of this page blank -->
\newpage
