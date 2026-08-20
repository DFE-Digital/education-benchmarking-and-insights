---
title: "Pipeline Trigger Message"
layout: sub-navigation
sectionKey: "Reference"
includeInBreadcrumbs: true
eleventyNavigation:
  key: "Pipeline Trigger Message"
  parent: "Data Reference"
---

## Pipeline Trigger Message

The Financial Benchmarking and Insights Tool (FBIT) data-processing pipeline relies on queue-triggered messages in `data-pipeline-job-pending` to execute data processing runs. These payloads coordinate official baseline data releases as well as interactive user-defined calculations.

---

## 1. Overall Schema & Definitions

All incoming pipeline messages adhere to a structured schema defined by the backend orchestration layer. The variables below control workflow routing, data boundaries, and source file locations:

| Variable | Payload Parameter | Type | Allowed Values | Definition & Nuance |
| :--- | :--- | :--- | :--- | :--- |
| **Job Type** | `"type"` | String | `"default"`, `"comparator-set"`, `"custom-data"` | Defines the high-level operational workflow (corresponds to `Pipeline.JobType` in C#). Dictates how the queue trigger parses and routes the message. |
| **Run Type** | `"runType"` | String | `"default"`, `"custom"` | Defines data classification and isolation (corresponds to `Pipeline.RunType` in C#). Dictates SQL table write targets (`RunType` column) and Parquet storage directories. |
| **Run ID** | `"runId"` | Integer or String | Integer Year (Default) or UUID String (Custom) | The database primary/composite key under which processed output is saved and queried by the frontend. |
| **Year** | `"year"` | Dictionary or Integer | Nested Dictionary (Default) or Integer Year (Custom) | Specifies either the directory paths of raw source files to read (for default runs) or the baseline year of comparison (for custom runs). |
| **URN** | `"urn"` | String | 6-digit URN string | Unique Reference Number identifying the "anchor" school. Only required for user-defined or custom runs. |
| **Run Until** | `"runUntil"` | String | `"transparency-file"`, `"pre-processing"`, `"comparators"` | Optional. Halts pipeline execution early after specified boundaries for development/debugging purposes. |
| **Generate Transparency Files** | `"generateTransparencyFilesAndPrecursorFiles"` | Boolean | `true`, `false` | Optional (defaults to `false`). Directs pre-processing to rebuild transparency spreadsheets from raw inputs rather than loading pre-existing master lists. |
| **Derive LAA Risk** | `"deriveLaaRiskScores"` | Boolean | `true`, `false` | Optional (defaults to `false`). Indicates whether Local Authority Risk Assessment scores should be derived during pipeline execution. |
| **Payload Data** | `"payload"` | Object | Schema-dependent object | Job-specific payload containing either the custom comparator set (`"ComparatorSetPayload"`) or custom financial metrics (`"CustomDataPayload"`). |

---

## 2. Examples

The three primary use cases in FBIT use the following distinct payload schemas:

### A. Default Run Payload

Used by systems administrators to run a full system-wide baseline compilation when the DfE publishes new datasets.

```json
{
  "type": "default",
  "runType": "default",
  "runId": 2026,
  "year": {
    "aar": 2025,
    "cfr": 2026,
    "bfr": 2025,
    "s251": 2025
  },
  "runUntil": "comparators",
  "generateTransparencyFilesAndPrecursorFiles": false,
  "deriveLaaRiskScores": true
}
```

### B. User-Defined Comparator Set Payload

Triggered when a frontend user creates a custom comparator group of schools.

```json
{
  "type": "comparator-set",
  "runType": "default",
  "runId": "a6db019c-d45f-44f3-8af0-71d82c1c6257",
  "year": 2025,
  "urn": "100449",
  "payload": {
    "_type": "ComparatorSetPipelinePayload",
    "kind": "ComparatorSetPayload",
    "set": [
      "100218",
      "100240",
      "102272",
      "132266",
      "100449"
    ]
  }
}
```

### C. Custom Data Payload

Triggered when a frontend user inputs revised hypothetical financial or characteristics data for a school to model changes.

```json
{
  "type": "custom-data",
  "runType": "custom",
  "runId": "c321ef6a-3b1c-4ce2-8e32-0d0167bf2fa7",
  "year": 2022,
  "urn": "142875",
  "payload": {
    "kind": "CustomDataPayload",
    "teachingStaffCosts": 140000.0,
    "totalIncome": 600000.0,
    "totalPupils": 250.0,
    "workforceFTE": 18.5
  }
}
```

---

## 3. Notes

* **jobId**:
  The orchestrator adds a `jobId` to each job's message, which is a unique transactional identifier used to track and monitor the status of background task orchestration. As it's auto-generated, don't include it in your trigger.
* **Decoupling of `runId` and `year`**:
  While both parameters represent timeline-related data, they are architecturally decoupled. `runId` serves as the database partition key to group and query outputs, while `year` serves as the directory reference to locate input source files.
* **Mismatched DfE Timelines**:
  DfE updates (AAR, CFR, BFR, S251) are released on independent annual cycles. Consequently, the latest official "baseline" system state at any given point (e.g. `RunId = 2026`) requires pulling from mismatched years across raw datasets (e.g. `cfr` 2026, but `aar` 2025). Grouping these source years under a nested `"year"` dictionary enables the default pipeline run to load files from correct raw container directories while storing everything under a unified `RunId` output.
* **User-Calculation Isolation**:
  To isolate and protect official system-wide calculations, any custom or user-defined run uses a temporary UUID string for `runId` and `"custom"` for `runType`. This partitions the custom rows safely in both blob storage and relational SQL tables, allowing simple, isolated cleanups without affecting default datasets.
* **Comparison Anchoring**:
  In user-defined RAG and custom-data runs, the `year` parameter is flattened to a single integer. This serves as the "anchor year", defining which default baseline dataset (`RunId = <year>` and `RunType = 'default'`) the custom school or comparator group should be evaluated against.
