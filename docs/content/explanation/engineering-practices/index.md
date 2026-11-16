---
title: Engineering Practices
layout: sub-navigation
sectionKey: Explanation
includeInBreadcrumbs: true
eleventyNavigation:
  key: Engineering Practices
  parent: Explanation
  order: 4
---

Engineering practices across the FBIT project reflect a commitment to web accessibility, system resilience, user-centred evidence, and rigorous automated quality assurance.

This section explains the technical practices, architectural resilience patterns, and quality assurance strategies adopted across the engineering team:

## Software Architecture & UX Practices

* **[Progressive Enhancement](/explanation/engineering-practices/progressive-enhancement/):** Rationale for server-first rendering (SSR) of charts and forms, ensuring full usability without client-side JavaScript.
* **[Task Cancellation & Resilience](/explanation/engineering-practices/task-cancellation/):** Asynchronous task cancellation tokens and thread termination safety patterns across ASP.NET Core endpoints.
* **[Volumetrics & Capacity Demand](/explanation/engineering-practices/volumetrics-demand/):** User traffic modeling, peak concurrency estimation, and compute scaling limits.
* **[AI-Assisted Engineering](/explanation/engineering-practices/ai-assisted-engineering/):** Guardrails and development practices for integrating AI assistants safely into software delivery workflows.

## User Research & Observability

* **[User Testing & Prototyping](/explanation/engineering-practices/user-testing/):** Integrating continuous user research, interaction feedback, and iterative design cycles into sprint delivery.
* **[Microsoft Clarity & Session Analytics](/explanation/engineering-practices/microsoft-clarity/):** Privacy-compliant behavioural telemetry, heatmaps, and session replay instrumentation for digital service optimization.

## Quality Assurance & Testing Strategies

* **[Service Test Approach](/explanation/engineering-practices/test-approach/):** Overall test engineering approach, quadrant test pyramid, and defect lifecycle in Azure DevOps.
* **[Data Releases Test Approach](/explanation/engineering-practices/data-release-test-approach/):** Tactical QA embedding model and verification discipline across annual data ingestion releases.
* **[Test Strategy: Service Web & API](/explanation/engineering-practices/test-strategy-service/):** End-to-end testing strategy across ASP.NET Core web services, API controllers, and integration gates.
* **[Test Strategy: Data Ingestion](/explanation/engineering-practices/test-strategy-data-ingestion/):** Ingestion test strategy, dataset variation matrix, and schema assurance models for the data pipeline.
* **[Test Strategy: Dynamic Content](/explanation/engineering-practices/test-strategy-dynamic-content/):** Assurance strategy for database-driven CMS banners, maintenance messaging, and dynamic service announcements.
