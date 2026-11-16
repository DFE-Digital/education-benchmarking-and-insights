---
title: Data & Pipeline Reference
layout: sub-navigation
sectionKey: Reference
includeInBreadcrumbs: true
eleventyNavigation:
  key: Data & Pipeline
  parent: Reference
  order: 1
---

Precise technical specifications, schema definitions, data models, and operational parameters for data ingestion, transformation, and storage.

## Core Data Specifications & Schemas

* **[Data Sources Overview](/reference/data-and-pipeline/sources/):** Comprehensive catalogue of primary and ancillary input files, publication cycles, and storage locations.
* **[Database Data Models & ERD](/reference/data-and-pipeline/models/):** Entity-relationship diagrams and table schemas across the FBIT relational database.
* **[Cost Categories Taxonomy](/reference/data-and-pipeline/cost-categories/):** Standardised hierarchy and expenditure definitions for CFR, AAR, and BFR financial metrics.
* **[Rounding Rules in Service](/reference/data-and-pipeline/rounding-off-rules-in-service/):** Mathematical rounding standards applied across currency, pupil headcounts, and percentage ratios.
* **[Validation Rules](/reference/data-and-pipeline/validation/):** Integrity constraints, null-handling specifications, and boundary assertions applied during ingestion.

## Pipeline Orchestration & Execution

* **[Pipeline Orchestrator](/reference/data-and-pipeline/orchestrator/):** Container Apps job configuration, runtime environment parameters, and container specifications.
* **[Pipeline Trigger Message Schemas](/reference/data-and-pipeline/pipeline-trigger-message/):** Exhaustive JSON schema definitions, parameters (`runId`, `runUntil`, `deriveLaaRiskScores`), and queue payload examples.
* **[Data Processing Timeline](/reference/data-and-pipeline/timeline/):** Chronological multi-year release dependencies and source data availability dates.

## Source File Technical Specifications

Specifications, column types, and acquisition procedures for primary and ancillary input feeds:

* **[Key Stage 2 Performance Data](/reference/data-and-pipeline/key-stage-2/):** Primary school attainment data feeds and download steps.
* **[Key Stage 4 Progress & Attainment](/reference/data-and-pipeline/key-stage-4/):** Secondary school performance data and Progress 8 measures.
* **[School Pupil Census](/reference/data-and-pipeline/pupil-census/):** Headcounts, free school meals (FSM) eligibility, and pupil demographics.
* **[Section 251 Database Extract](/reference/data-and-pipeline/s251-db-extract/):** Local authority planned budget and outturn data extraction specifications.
* **[School Workforce Census](/reference/data-and-pipeline/school-workforce/):** Teacher headcount, FTE allocations, and staff ratios.
* **[Special Educational Needs (SEN2)](/reference/data-and-pipeline/special-educational-needs/):** Education, Health and Care (EHC) plans and caseload data.
