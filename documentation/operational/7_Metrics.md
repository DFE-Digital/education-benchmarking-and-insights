# Service Metrics & Measures

This document provides a quick reference for all tracked metrics and measures for the service. Each metric is quantitive (a number) and drives a decision or action. Each metric should have an associated defininition (eg IaC code, a SQL query). If a metric is used to drive a decision, record the decision output and deactivate the metric if needed.

Some things to think about when defining a metric are:

* What is the metric definition?
* What is the decision or action from the metric?
* Who monitors the metric?
* (If relevant) How do we measure the success of the decision/action?

Each metric has its own detailed document in the `/metrics` folder.

## Categories

* **Operational** – Service health, reliability, infrastructure performance.
  * Example: If there is a spike in the HTTP error count (metric) the dev team need to be aware so they can fix it (so set an alert on the metric), otherwise the service agreement  might be breached.
* **User Insight** – User behaviour, engagement, product outcomes.
  * Example: Measure the click through rate (metric) for the new IT spend breakdown page for LA schools. If the click through rate is very low over a month the team might consider removing it or moving the content.

## Operational Metrics

| Metric Name                     | Purpose                                                                                                                   | Active | Code Definition                              | Detail Link                                          |
|---------------------------------|---------------------------------------------------------------------------------------------------------------------------|--------|---------------------------------------------|------------------------------------------------------|
| Service Availability            | Alert if availability is below 99.9%                                                                                      | ✅     | [IaC alert](../../support-analytics/terraform/alerts.tf)        | [View](metrics/001_service-availability.md)          |
| HTTP Error Spike                | Alert if HTTP 5xx error count exceeds 1                                                                                   | ✅     | [IaC alert](../../support-analytics/terraform/alerts.tf)            | [View](metrics/002_http-error-spike.md)              |
| High Memory Utilisation         | Alert if memory utilisation exceeds 85% for more than 5 minutes                                                           | ✅     | [IaC alert](../../support-analytics/terraform/alerts.tf)               | [View](metrics/003_high-memory-utilisation.md)       |
| High CPU Utilisation            | Alert if CPU utilisation exceeds 85% for more than 5 minutes                                                              | ✅     | [IaC alert](../../support-analytics/terraform/alerts.tf)                  | [View](metrics/004_high-cpu-utilisation.md)          |
| Dependency Latency Regression   | Alerts if a dependency call has started responding to requests more slowly than it used to                                | ✅     | [IaC alert](../../support-analytics/terraform/alerts.tf)   | [View](metrics/005_dependency-latency-regression.md) |
| Exception Rate Spike            | Alerts if exhibiting an abnormal rise in the number of exceptions                                                         | ✅     | [IaC alert](../../support-analytics/terraform/alerts.tf)           | [View](metrics/006_exception-rate-spike.md)          |
| Failed Pipeline Messages        | Alert if number of failed finished pipeline messages exceeds 1                                                            | ✅     | [IaC alert](../../support-analytics/terraform/alerts.tf)     | [View](metrics/007_failed-pipeline-messages.md)      |
| Failed Request/Dependency Spike | Detects if experiencing an abnormal rise in the rate in failed HTTP requests or dependency calls                          | ✅     | [IaC alert](../../support-analytics/terraform/alerts.tf)     | [View](metrics/008_failed-request-spike.md)          |
| Sustained Memory Growth         | Alerts on a consistent increase in memory consumption over a long period of time                                          | ✅     | [IaC alert](../../support-analytics/terraform/alerts.tf)        | [View](metrics/009_sustained-memory-growth.md)       |
| Polly 429 Warning Spike         | Alert if number of Polly warnings with status code 429 exceeds 1                                                          | ✅     | [IaC alert](../../support-analytics/terraform/alerts.tf)            | [View](metrics/010_polly-429-warning-spike.md)       |
| Polly Warning Volume Spike      | Alert if number of Polly warnings exceeds 10                                                                              | ✅     | [IaC alert](../../support-analytics/terraform/alerts.tf)       | [View](metrics/011_polly-warning-volume-spike.md)    |
| Response Latency Regression     | Alerts if responses to requests appear more slowly than they used to                                                      | ✅     | [IaC alert](../../support-analytics/terraform/alerts.tf)     | [View](metrics/012_response-latency-regression.md)   |
| Bad Trace Ratio Degradation     | Alerts if the percentage of "bad" traces (logged with a level of Warning, Error, or Fatal) out of all traces is degrading | ✅     | [IaC alert](../../support-analytics/terraform/alerts.tf)          | [View](metrics/013_bad-trace-ratio-degradation.md)   |

## User Insight Metrics

| Metric Name                     | Purpose                                                                                                                   | Active | Code Definition                              | Detail Link                                          |
|---------------------------------|---------------------------------------------------------------------------------------------------------------------------|--------|---------------------------------------------|------------------------------------------------------|
| LA Homepage: High needs comparators | How easy is it to get to the high needs comparators page? | ✅ | TBC | link |
| LA Homepage: School breakdown table usage | Are the new components on the LA homepage being used? | ✅ | TBC | link |
| LA Homepage: School journeys started | Are the new components on the LA homepage being used? | ✅ | TBC | link |
| LA Homepage: Change local authority button usage | Theory: this button isn't used. | ✅ | TBC | link |

<!-- Leave the rest of this page blank -->
\newpage
