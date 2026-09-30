---
title: Service Metrics and Measures
layout: sub-navigation
sectionKey: Reference
includeInBreadcrumbs: true
eleventyNavigation:
  key: Service Metrics
  parent: Reference
---

This document provides a quick reference for all tracked metrics and measures for the service. Each metric is quantitive (a number) and drives a decision or action. Each metric should have an associated defininition (eg IaC code, a SQL query). If a metric is used to drive a decision, record the decision output and deactivate the metric if needed.

Some things to think about when defining a metric are:

* What is the metric definition?
* What is the decision or action from the metric?
* Who monitors the metric?
* (If relevant) How do we measure the success of the decision/action?

Each metric has its own detailed document in the `/metrics` folder.

## Categories

* **Operational**: Service health, reliability, infrastructure performance.
  * Example: If there is a spike in the HTTP error count (metric) the dev team need to be aware so they can fix it (so set an alert on the metric), otherwise the service agreement  might be breached.
* **User Insight**: User behaviour, engagement, product outcomes.
  * Example: Measure the click through rate (metric) for the new IT spend breakdown page for LA schools. If the click through rate is very low over a month the team might consider removing it or moving the content.

## Operational Metrics

| Metric Name                     | Purpose                                                                                                                   | Active | Code Definition                                          | Detail Link                                          |
|---------------------------------|---------------------------------------------------------------------------------------------------------------------------|--------|----------------------------------------------------------|------------------------------------------------------|
| Service Availability            | Alert if availability is below 99.9%                                                                                      | ✅      | [IaC alert](https://github.com/DFE-Digital/education-benchmarking-and-insights/blob/main/support-analytics/terraform/alerts.tf) | [View](/reference/metrics/service-availability/)          |
| HTTP Error Spike                | Alert if HTTP 5xx error count exceeds 1                                                                                   | ✅      | [IaC alert](https://github.com/DFE-Digital/education-benchmarking-and-insights/blob/main/support-analytics/terraform/alerts.tf) | [View](/reference/metrics/http-error-spike/)              |
| High Memory Utilisation         | Alert if memory utilisation exceeds 85% for more than 5 minutes                                                           | ✅      | [IaC alert](https://github.com/DFE-Digital/education-benchmarking-and-insights/blob/main/support-analytics/terraform/alerts.tf) | [View](/reference/metrics/high-memory-utilisation/)       |
| High CPU Utilisation            | Alert if CPU utilisation exceeds 85% for more than 5 minutes                                                              | ✅      | [IaC alert](https://github.com/DFE-Digital/education-benchmarking-and-insights/blob/main/support-analytics/terraform/alerts.tf) | [View](/reference/metrics/high-cpu-utilisation/)          |
| Dependency Latency Regression   | Alerts if a dependency call has started responding to requests more slowly than it used to                                | ✅      | [IaC alert](https://github.com/DFE-Digital/education-benchmarking-and-insights/blob/main/support-analytics/terraform/alerts.tf) | [View](/reference/metrics/dependency-latency-regression/) |
| Exception Rate Spike            | Alerts if exhibiting an abnormal rise in the number of exceptions                                                         | ✅      | [IaC alert](https://github.com/DFE-Digital/education-benchmarking-and-insights/blob/main/support-analytics/terraform/alerts.tf) | [View](/reference/metrics/exception-rate-spike/)          |
| Failed Pipeline Messages        | Alert if number of failed finished pipeline messages exceeds 1                                                            | ✅      | [IaC alert](https://github.com/DFE-Digital/education-benchmarking-and-insights/blob/main/support-analytics/terraform/alerts.tf) | [View](/reference/metrics/failed-pipeline-messages/)      |
| Failed Request/Dependency Spike | Detects if experiencing an abnormal rise in the rate in failed HTTP requests or dependency calls                          | ✅      | [IaC alert](https://github.com/DFE-Digital/education-benchmarking-and-insights/blob/main/support-analytics/terraform/alerts.tf) | [View](/reference/metrics/failed-request-spike/)          |
| Sustained Memory Growth         | Alerts on a consistent increase in memory consumption over a long period of time                                          | ✅      | [IaC alert](https://github.com/DFE-Digital/education-benchmarking-and-insights/blob/main/support-analytics/terraform/alerts.tf) | [View](/reference/metrics/sustained-memory-growth/)       |
| Polly 429 Warning Spike         | Alert if number of Polly warnings with status code 429 exceeds 1                                                          | ✅      | [IaC alert](https://github.com/DFE-Digital/education-benchmarking-and-insights/blob/main/support-analytics/terraform/alerts.tf) | [View](/reference/metrics/polly-429-warning-spike/)       |
| Polly Warning Volume Spike      | Alert if number of Polly warnings exceeds 10                                                                              | ✅      | [IaC alert](https://github.com/DFE-Digital/education-benchmarking-and-insights/blob/main/support-analytics/terraform/alerts.tf) | [View](/reference/metrics/polly-warning-volume-spike/)    |
| Response Latency Regression     | Alerts if responses to requests appear more slowly than they used to                                                      | ✅      | [IaC alert](https://github.com/DFE-Digital/education-benchmarking-and-insights/blob/main/support-analytics/terraform/alerts.tf) | [View](/reference/metrics/response-latency-regression/)   |
| Bad Trace Ratio Degradation     | Alerts if the percentage of "bad" traces (logged with a level of Warning, Error, or Fatal) out of all traces is degrading | ✅      | [IaC alert](https://github.com/DFE-Digital/education-benchmarking-and-insights/blob/main/support-analytics/terraform/alerts.tf) | [View](/reference/metrics/bad-trace-ratio-degradation/)   |

## User Insight Metrics

| Metric Name                                      | Purpose                                                   | Active | Code Definition | Detail Link                                                              |
|--------------------------------------------------|-----------------------------------------------------------|--------|-----------------|--------------------------------------------------------------------------|
| LA Homepage: High needs comparators              | How easy is it to get to the high needs comparators page? | ✅      | TBC             | [View](/reference/metrics/la-homepage-high-needs-comparators-clicks/)         |
| LA Homepage: School breakdown table usage        | Are the new components on the LA homepage being used?     | ✅      | TBC             | [View](/reference/metrics/la-homepage-school-breakdown-table-elements-usage/) |
| LA Homepage: School journeys started             | Are the new components on the LA homepage being used?     | ✅      | TBC             | [View](/reference/metrics/la-homepage-school-journeys-started/)               |
| LA Homepage: Change local authority button usage | Theory: this button isn't used.                           | ✅      | ✅               | [View](/reference/metrics/la-homepage-change-authority-button-usage/)         |
