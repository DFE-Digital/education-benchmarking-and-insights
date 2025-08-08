# Metric: Sustained Memory Growth

**Category:**  
Operational

**Data Source:**  
Monitoring system / metrics platform

**Purpose:**  
Identify memory leaks or inefficient resource use by tracking long-term upward trends in memory usage. Sustained growth may not trigger immediate alarms but can cause future outages if left unchecked.

**Actionability:**  
Investigate causes of memory growth and remediate leaks or inefficiencies. If memory usage trends upward over time:

1. Analyse memory profiling data to identify potential leaks.
2. Review code changes for unbounded caching or retained objects.
3. Restart processes if short-term relief is required.
4. Optimise memory usage in code where possible.
5. Increase capacity only after root cause analysis confirms it is needed.

**Target / Threshold:**  
Memory usage stable or decreasing over time

**Reporting Frequency:**  
Periodic monitoring with alerts on trend changes

**Notes / Links:**  

<!-- Leave the rest of this page blank -->
\newpage
