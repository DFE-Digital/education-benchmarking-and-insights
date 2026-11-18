---
title: Reference
layout: sub-navigation
sectionKey: Reference
includeInBreadcrumbs: true
eleventyNavigation:
  key: Reference
---

Reference documentation is **information-oriented** description, describing the technical machinery, schemas, contracts, configurations, and verification records across the Financial Benchmarking and Insights Tool (FBIT).

## [Architecture Decisions](/reference/decisions/)

Review Architecture Decision Records (ADRs) documenting technical choices, architectural evaluations, and rationale across infrastructure, platform components, and data processing:

* **[Architecture Decisions](/reference/decisions/):** Overview of the ADR lifecycle, decision criteria, DfE architectural standards, and all 26 recorded decisions (`ADR 0001` to `ADR 0026`).

## [Data & Pipeline](/reference/data-and-pipeline/)

Precise technical specifications, schema definitions, data models, and operational parameters for data ingestion, transformation, and storage:

* **[Cost Categories](/reference/data-and-pipeline/cost-categories/):** Standardised hierarchy and expenditure definitions for CFR, AAR, and BFR financial metrics.
* **[Key Stage 2](/reference/data-and-pipeline/key-stage-2/):** Primary school attainment data feeds, column definitions, and download steps.
* **[Key Stage 4](/reference/data-and-pipeline/key-stage-4/):** Secondary school performance data, column definitions, and Progress 8 measures.
* **[Model Validation](/reference/data-and-pipeline/validation/):** Integrity assertions, null handling, and type constraints enforced during ingestion.
* **[Models](/reference/data-and-pipeline/models/):** Entity-relationship diagrams and table schemas across the FBIT relational database.
* **[Pipeline Orchestrator](/reference/data-and-pipeline/orchestrator/):** Container Apps job configuration, runtime environment parameters, and container specifications.
* **[Pipeline Trigger Message](/reference/data-and-pipeline/pipeline-trigger-message/):** Exhaustive JSON schema definitions, parameters (`runId`, `runUntil`, `deriveLaaRiskScores`), and queue payload examples.
* **[Pupil Census](/reference/data-and-pipeline/pupil-census/):** Headcounts, free school meals (FSM) eligibility, and pupil demographic input specifications.
* **[Rounding Off Rules In Service](/reference/data-and-pipeline/rounding-off-rules-in-service/):** Mathematical rounding standards applied across currency, pupil headcounts, and percentage ratios.
* **[S251 DB Extract](/reference/data-and-pipeline/s251-db-extract/):** Local authority planned budget and outturn data extraction specifications.
* **[School Workforce Source](/reference/data-and-pipeline/school-workforce/):** Teacher headcount, FTE allocations, and staff ratio input specifications.
* **[Sources](/reference/data-and-pipeline/sources/):** Catalogue of primary and ancillary input files, publication schedules, and storage locations.
* **[Special Educational Needs Source](/reference/data-and-pipeline/special-educational-needs/):** Education, Health and Care (EHC) plans and SEN2 caseload input specifications.
* **[Timeline](/reference/data-and-pipeline/timeline/):** Chronological multi-year release dependencies and source data availability dates.

## [Design](/reference/design/)

Reference information for designing the service and prototyping changes:

* **[Prototype kit structure](/reference/design/prototype-kit-structure/):** How the FBIT prototype kit is organised, with one index page per work item listing final designs and the concepts considered.

## [Infrastructure & Operations](/reference/infrastructure-and-operations/)

Cloud edge configuration, non-functional service requirements, and active monitoring metrics:

* **[Azure Front Door](/reference/infrastructure-and-operations/azure-front-door/):** CDN routing rules, SSL offloading, caching policies, and Web Application Firewall (WAF) rule configurations.
* **[Commercial Resources](/reference/infrastructure-and-operations/commercial-resources/):** Procurement links, licensed tooling, and third-party SaaS dependency configurations.
* **[Non-Functional Requirements](/reference/infrastructure-and-operations/non-functional-requirements/):** Service level objectives (SLOs), availability targets (99.9%), response time thresholds, and concurrency capacities.
* **[Service Metrics](/reference/infrastructure-and-operations/metrics/):** Comprehensive catalogue of active operational alerts (CPU, memory, latency, 5xx errors) and user insight metrics with Application Insights query definitions.

## [Platform & Web](/reference/platform-and-web/)

Technical contracts, API endpoint definitions, UI components, and state management rules for the web application and backend microservices:

* **[Branding and Design System](/reference/platform-and-web/branding/):** DfE Design System integration, GOV.UK Frontend macro styles, and typography tokens.
* **[Chart Rendering API](/reference/platform-and-web/chart-rendering-api/):** Server-side SVG rendering contracts, payload parameters, and typography styling for chart images.
* **[Cookies](/reference/platform-and-web/cookies/):** Cookie classifications, consent lifecycle, and persistence lifetimes.
* **[DSI Test Organisations](/reference/platform-and-web/dsi-test-organisations/):** Test organisation IDs, user personas, and synthetic test accounts in non-production environments.
* **[Integration with DfE Sign In (DSI)](/reference/platform-and-web/integration-with-dfe-signin/):** OpenID Connect (OIDC) authentication endpoints, claims mappings, and token validation.
* **[Line Chart](/reference/platform-and-web/line-chart/):** SVG line chart dimensions, axis scaling, trend points, and progressive enhancement layers.
* **[Platform APIs](/reference/platform-and-web/platform-apis/):** Controller endpoint contracts, query parameters, response structures, and secret configuration.
* **[Progressive Enhancements](/reference/platform-and-web/progressive-enhancements/):** Client-side enhancements for server-rendered HTML (dynamic sorting, chart tooltips, auto-complete).
* **[Service Banners](/reference/platform-and-web/service-banners/):** Database-driven banner models, severity classifications, and dynamic display mechanisms.
* **[Service Conditions](/reference/platform-and-web/service-conditions/):** Terms of use, data privacy policies, and statutory compliance messaging.
* **[Session State](/reference/platform-and-web/session-state/):** Redis-backed session data schemas, TTL configurations, and user state handling.
* **[Sortable HTML Tables](/reference/platform-and-web/sortable-tables/):** Accessible table sorting composition, MOJ design pattern, button semantics, and ARIA state specifications.

## [Quality & Assurance](/reference/quality-and-assurance/)

Centralised archive of quality assurance records, release sign-offs, performance test plans, API test coverage matrices, and automated security vulnerability scans:

* **[API Test Plans](/reference/quality-and-assurance/api-test-plans/):** Technical verification scenarios, validation rules, and endpoint coverage definitions for platform microservices.
* **[Data Release Test Plans](/reference/quality-and-assurance/data-release-test-plans/):** Dedicated UAT plans, data assurance matrices, and verification checklists for annual data drops (AAR, CFR, BFR, S251).
* **[Performance Test Plans](/reference/quality-and-assurance/performance-test-plans/):** Load, peak, and spike test run plans with Virtual User (VU) configurations, thresholds, and latency benchmarks.
* **[QA Performance Testing](/reference/quality-and-assurance/performance-testing/):** Virtual user allocation rules, test execution parameters, and load profiles for API and web testing.
* **[Release Test Plans](/reference/quality-and-assurance/release-test-plans/):** Chronological archive of 38+ production deployment test plans, regression scopes, and deployment verification sign-offs.
* **[Security Scan Reports](/reference/quality-and-assurance/security-scan-reports/):** Automated OWASP Zed Attack Proxy (ZAP) baseline security vulnerability audit reports.
* **[Security Testing](/reference/quality-and-assurance/security-testing/):** Multi-phase external IT health check standards, penetration test parameters, and continuous CI security scans.
