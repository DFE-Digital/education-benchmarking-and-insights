---
title: Explanation
layout: sub-navigation
sectionKey: Explanation
includeInBreadcrumbs: true
eleventyNavigation:
  key: Explanation
---

Explanation documentation is **understanding-oriented** discussion, explaining why things are structured the way they are across the Financial Benchmarking and Insights Tool (FBIT). It provides design rationale, architectural context, accounting methodologies, and engineering trade-offs.

---

## [Architecture](/explanation/architecture/)

Understand the architectural principles, container views, boundary protections, and deployment topology of the FBIT platform:

* **[System Introduction & Overview](/explanation/architecture/introduction/):** High-level purpose of the service, core components, and external system integrations.
* **[Application Architecture](/explanation/architecture/application-architecture/):** Container architecture across front-end web apps, backend APIs, SQL databases, and Front Door CDN caching.
* **[Logical Architecture](/explanation/architecture/logical-architecture/):** Domain boundaries, modular separation of concerns, and component relationships.
* **[Data Architecture](/explanation/architecture/data-architecture/):** Relational storage design, blob storage containers, and persistence tier topology.
* **[Deployment Architecture](/explanation/architecture/deployment-architecture/):** Multi-environment topology (dev, test, preprod, prod), Container Apps infrastructure, and release promotion gates.
* **[Information Architecture](/explanation/architecture/information-architecture/):** Navigation tree, page hierarchies, and user journey mappings across the public service.
* **[Authentication & Authorisation](/explanation/architecture/auth-authz/):** Public unauthenticated access versus authenticated roles managed via DfE Sign-in.
* **[Security Architecture](/explanation/architecture/security-architecture/):** Layered defence, network boundary protection, encrypted communications, and security controls.
* **[Constraints & Principles](/explanation/architecture/constraints-and-principles/):** Guiding technical constraints, DfE engineering standards, and cloud policies.

---

## [Domain & Methodology](/explanation/domain-and-methodology/)

Explore the statutory financial frameworks, educational accounting returns, and statistical methodologies underpinning FBIT benchmarking:

* **[Business Context](/explanation/domain-and-methodology/business-context/):** Legislative background, departmental objectives, and user needs driving school financial benchmarking.
* **[Domain Concepts](/explanation/domain-and-methodology/domain/):** Core domain entities including maintained schools, academies, multi-academy trusts (MATs), and local authorities.
* **[Budget Forecast Return (BFR)](/explanation/domain-and-methodology/bfr/):** Multi-year financial forecasting, 3Y projections, Statement of Financial Activities (SOFA), and line-number mappings.
* **[Dedicated Schools Grant (DSG) Funding](/explanation/domain-and-methodology/dsg-funding/):** Local authority high needs blocks, outturn reconciliation, and DSG allocation mechanics.
* **[Academy Central Services Apportionments](/explanation/domain-and-methodology/academy-apportionments/):** Methodology for allocating trust-level central service expenditure across constituent academy establishments.
* **[Local Authority School Financial Risk Indicators (LAA)](/explanation/domain-and-methodology/laa-risk-indicators/):** Multi-factor risk calculation framework detecting deficit drift and financial vulnerability in maintained schools.
* **[Key Stage 4 Progress 8 Indicator](/explanation/domain-and-methodology/ks4-progress-indicator/):** Integration of secondary educational attainment and pupil progress metrics into financial comparisons.
* **[User-Generated Calculations](/explanation/domain-and-methodology/user-generated-calculations/):** Calculation pipelines for bespoke user-defined comparator sets and ad-hoc financial comparisons.

---

## [Data Pipeline](/explanation/data-pipeline/)

Learn how the data processing engine ingests, transforms, assures, and loads school financial returns:

* **[Pipeline Ingestion & Transformation Processing](/explanation/data-pipeline/processing/):** Complete lifecycle of a pipeline run, spanning pre-processing, master list compilation, comparator set calculations, and metric RAG derivations.
* **[Pipeline Trigger Message Concepts](/explanation/data-pipeline/pipeline-trigger-message/):** Conceptual model of decoupled `runId` vs. data `year`, stage control (`runUntil`), transparency file generation switches, and isolated user calculation queues.
* **[Pipeline Orchestrator Deployment & Secret Resolution](/explanation/data-pipeline/pipeline-orchestrator-deployment-and-secrets/):** Infrastructure patterns for orchestrating Container Apps jobs, resolving runtime database credentials via Azure Key Vault, and managing ephemeral workers.

---

## [Engineering Practices](/explanation/engineering-practices/)

Discover the technical practices, accessibility standards, user research approaches, and testing strategies applied across the service:

* **[Progressive Enhancement](/explanation/engineering-practices/progressive-enhancement/):** Rationale for server-first rendering (SSR) of charts and forms, ensuring full usability without client-side JavaScript.
* **[Task Cancellation & Resilience](/explanation/engineering-practices/task-cancellation/):** Asynchronous task cancellation tokens and thread termination safety patterns across ASP.NET Core endpoints.
* **[Volumetrics & Capacity Demand](/explanation/engineering-practices/volumetrics-demand/):** User traffic modeling, peak concurrency estimation, and compute scaling limits.
* **[AI-Assisted Engineering](/explanation/engineering-practices/ai-assisted-engineering/):** Guardrails and development practices for integrating AI assistants safely into software delivery workflows.
* **[User Testing & Prototyping](/explanation/engineering-practices/user-testing/):** Integrating continuous user research, interaction feedback, and iterative design cycles into sprint delivery.
* **[Microsoft Clarity & Session Analytics](/explanation/engineering-practices/microsoft-clarity/):** Privacy-compliant behavioural telemetry, heatmaps, and session replay instrumentation for digital service optimization.
* **[Service Test Approach](/explanation/engineering-practices/test-approach/):** Overall test engineering approach, quadrant test pyramid, and defect lifecycle in Azure DevOps.
* **[Data Releases Test Approach](/explanation/engineering-practices/data-release-test-approach/):** Tactical QA embedding model and verification discipline across annual data ingestion releases.
* **[Test Strategy: Service Web & API](/explanation/engineering-practices/test-strategy-service/):** End-to-end testing strategy across ASP.NET Core web services, API controllers, and integration gates.
* **[Test Strategy: Data Ingestion](/explanation/engineering-practices/test-strategy-data-ingestion/):** Ingestion test strategy, dataset variation matrix, and schema assurance models for the data pipeline.
* **[Test Strategy: Dynamic Content](/explanation/engineering-practices/test-strategy-dynamic-content/):** Assurance strategy for database-driven CMS banners, maintenance messaging, and dynamic service announcements.
