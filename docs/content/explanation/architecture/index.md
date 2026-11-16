---
title: Architecture
layout: sub-navigation
sectionKey: Explanation
includeInBreadcrumbs: true
eleventyNavigation:
  key: Architecture
  parent: Explanation
  order: 1
---

The architecture of the Financial Benchmarking and Insights Tool (FBIT) balances scalability, security, and maintainability across web interfaces, backend APIs, and distributed data processing pipelines.

This section explains the design rationale, system boundaries, and architectural viewpoints that underpin the platform:

## System Context & Concepts

* **[System Introduction & Context](/explanation/architecture/introduction/):** High-level overview of the FBIT service, core components, and external actor interactions.
* **[Constraints & Principles](/explanation/architecture/constraints-and-principles/):** Foundational architecture principles, DfE engineering guidelines, and cloud constraints.

## Structural Viewpoints

* **[Application Architecture](/explanation/architecture/application-architecture/):** Container view detailing Azure App Service, containerised front ends, backend APIs, and front-door caching.
* **[Logical Architecture](/explanation/architecture/logical-architecture/):** Conceptual structure showing domain modules, separation of concerns, and inter-service dependencies.
* **[Data Architecture](/explanation/architecture/data-architecture/):** Data storage topology, persistence layers (Azure SQL, Blob Storage), and ingestion flow.
* **[Deployment Architecture](/explanation/architecture/deployment-architecture/):** Environment topology across dev, test, preprod, and prod, container infrastructure, and CI/CD promotion.
* **[Information Architecture](/explanation/architecture/information-architecture/):** User journey structure, navigation hierarchies, and content organisation across the public-facing service.

## Security & Identity

* **[Authentication and Authorisation](/explanation/architecture/auth-authz/):** Access control models, public vs. authenticated role segregation, and DfE Sign-in integration.
* **[Security Architecture](/explanation/architecture/security-architecture/):** Threat modeling, boundary protection, encrypted communications, and defensive measures across platform services.
