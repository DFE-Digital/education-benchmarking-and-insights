---
title: "Pipeline Trigger Message Schema"
layout: sub-navigation
sectionKey: "Reference"
includeInBreadcrumbs: true
eleventyNavigation:
  key: "Pipeline Trigger Message"
  parent: "Data Reference"
---

## Pipeline Trigger Message Schema

The Financial Benchmarking and Insights Tool (FBIT) data-processing pipeline relies on queue-triggered messages in `data-pipeline-job-pending` to execute data processing runs. These payloads coordinate official baseline data releases as well as interactive user-defined calculations.

For an in-depth discussion on the architectural and conceptual decisions behind these parameter designs (such as isolation levels, decoupling, and mismatched timelines), see the [Pipeline Trigger Message Concepts](../../../explanation/data/pipeline-trigger-message.md) explanation page.

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

## 2. Payload Examples

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
