# Metric: LA Homepage School Breakdown Table Elements Usage

**Category:**  
User Insight

**Data Source:**  
Custom code-level application tracking

**Purpose:**  
Test the theory that the change local authority button on the LA Homepage isn't used.

**Actionability:**  
If the button isn't used, remove it.

**Target / Threshold:**  
If the ratio of page views to click throughs to change local authority is below 2%, remove it.

**Reporting Frequency:**  
Report after 1 month from release

**Notes / Links:**
At least 1,000 page visits will give better statistical significance.

**Log Analytics Query:**

```kql
let requestCount = toscalar(GetEstablishmentRequests
    | where Establishment == "local-authority" 
    | where Feature == "home"
    | where ResultCode == 200
    | summarize count());
let clickCount = toscalar(GetTrackedLinks
    | where Name == "change-organisation"
    | where Source contains "/local-authority/"
    | summarize count());
union
    (print Metric = "RequestCount", Value = requestCount * 1.0),
    (print Metric = "ClickCount", Value = clickCount * 1.0),
    (print Metric = "Ratio_ClickToRequest", Value = clickCount * 1.0 / requestCount)
```

_Sample response:_

| Metric               | Value               |
|----------------------|---------------------|
| RequestCount         | 66                  |
| ClickCount           | 12                  |
| Ratio_ClickToRequest | 0.18181818181818182 |

<!-- Leave the rest of this page blank -->
\newpage
