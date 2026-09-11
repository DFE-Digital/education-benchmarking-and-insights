---
title: "Pipeline Trigger Message Concepts"
layout: sub-navigation
sectionKey: "Explanation"
includeInBreadcrumbs: true
eleventyNavigation:
  key: "Pipeline Trigger Message Concepts"
  parent: "Data Explanation"
---

## Pipeline Trigger Message Concepts

This document explains the background logic, design choices, and architectural principles behind the Financial Benchmarking and Insights Tool (FBIT) pipeline trigger message parameters.

For the factual parameters list and JSON schema specifications, see the [Pipeline Trigger Message Reference](../../reference/data/pipeline-trigger-message/).

---

## 1. Decoupling of `runId` and `year`

While both parameters represent timeline-related data, they are architecturally decoupled in the pipeline processing layer:

* **`runId`**: Serves as the database partition/composite key to group and query outputs in SQL tables and Parquet files.
* **`year`**: Serves as the directory reference in Azure Blob storage to locate and ingest the correct raw input files.

This separation prevents hardcoding data ingestion structures to output schemas, offering flexibility when naming system-wide database releases.

---

## 2. Mismatched DfE Timelines

Department for Education (DfE) raw datasets (AAR, CFR, BFR, S251) are released on independent annual cycles. Consequently, the latest official "baseline" system state at any given point (e.g., `RunId = 2026`) requires pulling from mismatched years across raw datasets.

For example, a valid default baseline compiler run might need:

* `cfr` for **2026**
* `aar` for **2025**
* `bfr` for **2025**
* `s251` for **2025**

Grouping these source years under a nested `"year"` dictionary enables the default pipeline run to load files from the correct respective raw container directories while storing the entire standardized result set under a unified `RunId` database partition.

---

## 3. User-Calculation & Run Isolation

To isolate and protect official system-wide calculations from ad-hoc user interactions, FBIT enforces strict isolation rules:

* **Default Runs**: Used by system administrators for baseline datasets. They use an integer year (e.g., `2026`) for `runId` and `"default"` for `runType`.
* **Custom/User-Defined Runs**: Triggered when a frontend user creates a custom comparator group or submits custom financial data. These runs use a temporary, unique UUID string (e.g., `"c321ef6a-3b1c-4ce2-8e32-0d0167bf2fa7"`) for `runId` and `"custom"` for `runType`.

This design partitions custom rows safely in both blob storage containers and relational database tables, allowing simple, isolated data cleanup or expiration without impacting the core default baseline datasets.

---

## 4. Comparison Anchoring

In user-defined RAG and custom-data runs, the complex nested `"year"` dictionary is flattened into a single integer (e.g., `year: 2025`).

This integer serves as the **"anchor year"**. It defines the baseline dataset (`RunId = <year>` and `RunType = 'default'`) against which the custom school or comparator group should be evaluated. This allows the system to compare a user's simulated or custom metrics with official, pre-calculated peer school distributions of that specific baseline year.

---

## 5. Automated `jobId` injection

The backend orchestrator automatically appends a transactional `jobId` (UUID) to every message in transit. This ID acts as a correlation token used to track, monitor, and troubleshoot the lifecycle of background task orchestration. It is strictly runtime-managed and omitted from manual triggering payloads.
