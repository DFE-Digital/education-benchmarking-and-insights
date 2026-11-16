---
title: Reference
layout: sub-navigation
sectionKey: Reference
includeInBreadcrumbs: true
eleventyNavigation:
  key: Reference
---

Reference documentation is **information-oriented** description, describing the technical machinery, schemas, contracts, configurations, and verification records across the Financial Benchmarking and Insights Tool (FBIT).

---

## [Data & Pipeline Reference](/reference/data-and-pipeline/)

Precise technical specifications, schema definitions, data models, and operational parameters for data ingestion, transformation, and storage:

* **[Data Sources Overview](/reference/data-and-pipeline/sources/):** Catalogue of primary and ancillary input files, publication schedules, and storage locations.
* **[Database Data Models & ERD](/reference/data-and-pipeline/models/):** Entity-relationship diagrams and table schemas across the FBIT relational database.
* **[Cost Categories Taxonomy](/reference/data-and-pipeline/cost-categories/):** Standardised hierarchy and expenditure definitions for CFR, AAR, and BFR financial metrics.
* **[Rounding Rules in Service](/reference/data-and-pipeline/rounding-off-rules-in-service/):** Mathematical rounding standards applied across currency, pupil headcounts, and percentage ratios.
* **[Validation Rules](/reference/data-and-pipeline/validation/):** Integrity assertions, null handling, and type constraints enforced during ingestion.
* **[Pipeline Orchestrator Specifications](/reference/data-and-pipeline/orchestrator/):** Container Apps job configuration, runtime environment parameters, and container specifications.
* **[Pipeline Trigger Message Schemas](/reference/data-and-pipeline/pipeline-trigger-message/):** Exhaustive JSON schema definitions, parameters (`runId`, `runUntil`, `deriveLaaRiskScores`), and queue payload examples.
* **[Data Processing Timeline](/reference/data-and-pipeline/timeline/):** Chronological multi-year release dependencies and source data availability dates.
* **Source File Feeds:** Column definitions and procurement instructions for [Key Stage 2](/reference/data-and-pipeline/key-stage-2/), [Key Stage 4](/reference/data-and-pipeline/key-stage-4/), [Pupil Census](/reference/data-and-pipeline/pupil-census/), [Section 251 Extracts](/reference/data-and-pipeline/s251-db-extract/), [School Workforce](/reference/data-and-pipeline/school-workforce/), and [Special Educational Needs](/reference/data-and-pipeline/special-educational-needs/).

---

## [Platform & Web Reference](/reference/platform-and-web/)

Technical contracts, API endpoint definitions, UI components, and state management rules for the web application and backend microservices:

* **[Platform APIs Specifications](/reference/platform-and-web/platform-apis/):** Controller endpoint contracts, query parameters, response structures, and secret configuration.
* **[Chart Rendering API](/reference/platform-and-web/chart-rendering-api/):** Server-side SVG rendering contracts, payload parameters, and typography styling for chart images.
* **[Line Chart Rendering](/reference/platform-and-web/line-chart/):** SVG line chart dimensions, axis scaling, trend points, and progressive enhancement layers.
* **[Branding & Design System](/reference/platform-and-web/branding/):** DfE Design System integration, GOV.UK Frontend macro styles, and typography tokens.
* **[Progressive Enhancements](/reference/platform-and-web/progressive-enhancements/):** Client-side enhancements for server-rendered HTML (dynamic sorting, chart tooltips, auto-complete).
* **[Cookies Policy & Storage](/reference/platform-and-web/cookies/):** Cookie classifications, consent lifecycle, and persistence lifetimes.
* **[Service Banners](/reference/platform-and-web/service-banners/):** Database-driven banner models, severity classifications, and dynamic display mechanisms.
* **[Service Conditions & Terms](/reference/platform-and-web/service-conditions/):** Terms of use, data privacy policies, and statutory compliance messaging.
* **[Session State Management](/reference/platform-and-web/session-state/):** Redis-backed session data schemas, TTL configurations, and user state handling.
* **[Integration with DfE Sign-in (DSI)](/reference/platform-and-web/integration-with-dfe-signin/):** OpenID Connect (OIDC) authentication endpoints, claims mappings, and token validation.
* **[DSI Test Organisations](/reference/platform-and-web/dsi-test-organisations/):** Test organisation IDs, user personas, and synthetic test accounts in non-production environments.

---

## [Infrastructure & Operations](/reference/infrastructure-and-operations/)

Cloud edge configuration, non-functional service requirements, and active monitoring metrics:

* **[Azure Front Door Specifications](/reference/infrastructure-and-operations/azure-front-door/):** CDN routing rules, SSL offloading, caching policies, and Web Application Firewall (WAF) rule configurations.
* **[Non-Functional Requirements (NFRs)](/reference/infrastructure-and-operations/non-functional-requirements/):** Service level objectives (SLOs), availability targets (99.9%), response time thresholds, and concurrency capacities.
* **[Commercial Resources](/reference/infrastructure-and-operations/commercial-resources/):** Procurement links, licensed tooling, and third-party SaaS dependency configurations.
* **[Service Metrics and Measures](/reference/infrastructure-and-operations/metrics/):** Comprehensive catalogue of active operational alerts (CPU, memory, latency, 5xx errors) and user insight metrics with Application Insights query definitions.

---

## [Architecture Decisions (ADRs)](/reference/decisions/)

Review Architecture Decision Records (ADRs) documenting technical choices, architectural evaluations, and rationale across infrastructure, platform components, and data processing:

* **[ADR Index & Process](/reference/decisions/):** Overview of the ADR lifecycle, decision criteria, and DfE architectural standards.
* Browse 26 recorded decisions covering data storage, front-end frameworks, chart rendering architecture, identity provider strategy, pupil population metrics, and pipeline orchestration.

---

## [Quality & Assurance](/reference/quality-and-assurance/)

Centralised archive of quality assurance records, release sign-offs, performance test plans, API test coverage matrices, and automated security vulnerability scans:

* **[Release Test Plans](/reference/quality-and-assurance/release-test-plans/):** Chronological archive of 38+ production deployment test plans, regression scopes, and deployment verification sign-offs.
* **[Data Release Test Plans](/reference/quality-and-assurance/data-release-test-plans/):** Dedicated UAT plans, data assurance matrices, and verification checklists for annual data drops (AAR, CFR, BFR, S251).
* **[Performance Test Plans](/reference/quality-and-assurance/performance-test-plans/):** Load, peak, and spike test run plans with Virtual User (VU) configurations, thresholds, and latency benchmarks.
* **[API Test Plans](/reference/quality-and-assurance/api-test-plans/):** Technical verification scenarios, validation rules, and endpoint coverage definitions for platform microservices.
* **[Security Scan Reports](/reference/quality-and-assurance/security-scan-reports/):** Automated OWASP Zed Attack Proxy (ZAP) baseline security vulnerability audit reports.
* **[Performance Testing Overview](/reference/quality-and-assurance/performance-testing/):** Virtual user allocation rules, test execution parameters, and load profiles for API and web testing.
* **[Security Testing Overview](/reference/quality-and-assurance/security-testing/):** Multi-phase external IT health check standards, penetration test parameters, and continuous CI security scans.
