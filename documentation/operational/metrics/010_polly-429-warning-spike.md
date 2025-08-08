# Metric: Polly 429 Warning Spike

**Category:**  
Operational

**Data Source:**  
Polly warnings logs

**Purpose:**  
Detect increased throttling responses (HTTP 429) from Polly retry policies, indicating the service or dependencies are rate-limited. This helps tune retry logic and avoid cascading failures or degraded user experience.

**Actionability:**  
Investigate throttling causes and adjust retry or rate limiting strategies. When HTTP 429 (Too Many Requests) warnings increase:

1. Check if rate limits are being exceeded for the target service.
2. Adjust request throttling or batching logic.
3. Contact the service provider to confirm rate limit policies.
4. Implement exponential backoff to reduce retry pressure.
5. Monitor after adjustments to confirm reduced warnings.

**Target / Threshold:**  
Polly 429 warnings remain at 0 or minimal levels

**Reporting Frequency:**  
Real-time alerting

**Notes / Links:**  

<!-- Leave the rest of this page blank -->
\newpage
