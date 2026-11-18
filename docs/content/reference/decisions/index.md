---
title: Decisions
layout: sub-navigation
sectionKey: Reference
includeInBreadcrumbs: true
eleventyNavigation:
  key: Architecture Decisions
  parent: Reference
---

The decision-making process follows the flow shown below:

![Decision Process](/assets/images/architecture/Decision-Process.png)

The "standards" hard constraint includes typical implementations within the wider DfE landscape, as well as GDS compliance. For example, the use of Azure is not a standard, but a hard constraint as the preferred cloud platform for the department.

We also record architectural decision records using [ADRs](https://cognitect.com/blog/2011/11/15/documenting-architecture-decisions).

## Architecture Decision Records

* [ADR 0001: Data Storage](/reference/decisions/0001-data-storage/)
* [ADR 0002: Development Data](/reference/decisions/0002-development-data/)
* [ADR 0003: Front-end framework](/reference/decisions/0003-front-end-framework/)
* [ADR 0004: Pipeline Processing Development Language](/reference/decisions/0004-pipeline-processing-development-language/)
* [ADR 0005: API Performance Testing with Azure Load Tests](/reference/decisions/0005-api-performance-testing-with-azure-load-tests/)
* [ADR 0006: Rendering of Charts](/reference/decisions/0006-chart-rendering/)
* [ADR 0007: Deployment Options for SVG Service for Rendering Charts](/reference/decisions/0007-deployment-options-rendering-charts-service/)
* [ADR 0008: Resolving Intermittent 502, 503, and 429 Errors on Azure Function App](/reference/decisions/0008-resolving-intermittent-errors-function-app/)
* [ADR 0009: JavaScript Library Selection for ASP.NET Core with GOV.UK Standards](/reference/decisions/0009-js-library/)
* [ADR 0010: Decommissioning Schools financial benchmarking service](/reference/decisions/0010-sfb-decomm/)
* [ADR 0011: Azure Front Door Upgrade](/reference/decisions/0011-azure-front-door-upgrade/)
* [ADR 0012: Use of Connection Strings/Keys vs Managed Identity in Azure Authentication](/reference/decisions/0012-uage-managed-identity/)
* [ADR 0013: Data pipeline Databricks conversion](/reference/decisions/0013-dfe-databricks/)
* [ADR 0014: DfE Databricks data out](/reference/decisions/0014-dfe-databricks-data-out/)
* [ADR 0015: Chart Rendering API Performance and Scalability Improvements](/reference/decisions/0015-chart-svg-generation/)
* [ADR 0016: Evaluation of Custom WAF Pre-Filter Rules for Valid Application Routes](/reference/decisions/0016-waf-custom-rule/)
* [ADR 0017: Identity Provider Strategy: DfE Sign-in (DSI) vs Azure AD](/reference/decisions/0017-identity-provider-strategy/)
* [ADR 0018: Transition from Population-based to Pupil-based Normalisation/Standardisation](/reference/decisions/0018-transition-population-based-to-pupil-based/)
* [ADR 0019: Recommendation for Sourcing Local Authority Pupil Counts](/reference/decisions/0019-pupil-data-la/)
* [ADR 0020: Recommendation for workforce data senior leadership values](/reference/decisions/0020-workforce-data/)
* [ADR 0021: Recommendation for academy place funding values to enable DSG/s251 outturn alignment](/reference/decisions/0021-dsg-s251-alignment/)
* [ADR 0022: Build Logic vs. Pipeline Orchestration](/reference/decisions/0022-build-logic-vs-pipeline-orchestration/)
* [ADR 0023: Local Authority Risk Analysis tool data integration](/reference/decisions/0023-laa-risk-indicators-data-architecture/)
* [ADR 0024: Management Information Pipeline](/reference/decisions/0024-management-information-pipeline/)
* [ADR 0025: Lagging Datasets](/reference/decisions/0025-laa-lagging-datasets/)
* [ADR 0026: Attainment-8 feature flag](/reference/decisions/0026-add-featureflag-attainment8/)
