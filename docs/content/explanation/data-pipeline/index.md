---
title: Data Pipeline
layout: sub-navigation
sectionKey: Explanation
includeInBreadcrumbs: true
eleventyNavigation:
  key: Data Pipeline
  parent: Explanation
  order: 3
---

The FBIT Data Pipeline is a high-throughput Python and Azure Container Apps data transformation engine responsible for ingesting, standardising, assuring, and loading school financial returns.

This section explains the processing lifecycles, triggering paradigms, and deployment secrets management for the pipeline:

## Ingestion Architecture & Execution

* **[Pipeline Ingestion & Transformation Processing](/explanation/data-pipeline/processing/):** Lifecycle stages of a pipeline run, spanning pre-processing, master list creation, comparator set generation, and metric RAG calculations.
* **[Pipeline Trigger Message Concepts](/explanation/data-pipeline/pipeline-trigger-message/):** Conceptual model of decoupled `runId` vs. data `year`, stage control (`runUntil`), transparency file generation switches, and isolated user calculation queues.
* **[Pipeline Orchestrator Deployment & Secret Resolution](/explanation/data-pipeline/pipeline-orchestrator-deployment-and-secrets/):** Infrastructure patterns for orchestrating Container Apps jobs, resolving runtime database credentials via Azure Key Vault, and managing ephemeral workers.
