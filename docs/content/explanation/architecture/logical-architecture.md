---
title: Logical Architecture
layout: sub-navigation
sectionKey: Explanation
includeInBreadcrumbs: true
eleventyNavigation:
  key: Logical Architecture
  parent: Architecture Explanation
  order: 7
---

## Logical Viewpoint

```mermaid
graph TD
    Client[Web Browser] --> WebApp[Web Application]
    WebApp --> EstAPI[Establishment API]
    WebApp --> BenchAPI[Benchmark API]
    WebApp --> InsightAPI[Insight API]
    WebApp --> LAAPI[LocalAuthorityFinances API]
    WebApp -.-> AppInsights[Azure Application Insights]
    WebApp -.-> DfESignIn[DfE Sign-In]
```

| Component | Description |
|:---|:---|
| Web Application | This application is the front end for the service and provides all the functionality |
| Establishment API | Handles searching and details of establishments |
| Benchmark API | Handles benchmark calculations and comparisons |
| Insight API | Handles insights and recommendations |
| LocalAuthorityFinances API | Handles local authority financial data |

## Cross-cutting concerns

| Component | Description |
|:---|:---|
| Logging and Analytics | Azure Application Insights |
| Authentication and Authorisation | DfE Sign-In |
