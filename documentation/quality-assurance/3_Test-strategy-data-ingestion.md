# Test Strategy: Data Ingestion

## Purpose

This document outlines the strategy for testing **data ingestion** within the FBIT platform.
Each data ingestion cycle involves receiving structured datasets from upstream providers, processing them through the ingestion pipeline, storing the results in the database, and exposing the data to the service layer for end users.

The goal of this strategy is to ensure that every ingestion is **accurate, complete, and non-disruptive to existing data**.
This document defines **how ingestion testing is approached**, including the scope, objectives, responsibilities, and validation methods.

## Scope

**In Scope:**

- Validation of all data ingestion across environments
- Verification of:

  - File format, schema, and year/context accuracy
  - Successful pipeline processing and transformation
  - Correct database storage and mapping
  - Accurate reflection of data in the service layer
- Regression checks to ensure no adverse impact on existing data

**Out of Scope:**

- Functional testing of the ingestion pipeline code (covered by CI/CD)
- Verification of raw source data accuracy (owned by upstream providers)

## Goals

- Ensure each ingestion cycle is processed correctly and completely
- Detect:

  - Schema drift or format changes from upstream sources
  - Misalignment or overwriting of existing data
  - Mapping and transformation errors
- Maintain data integrity, traceability, and stakeholder confidence across cycles

## Responsibilities

| Role           | Responsibility                                                               |
|----------------|------------------------------------------------------------------------------|
| Data Engineer  | Prepares input files, executes pipeline, monitors ingestion logs             |
| QA             | Validates file → pipeline → DB → service flow and performs regression checks |
| Product owner  | Confirms data integrity and mapping outcomes meet business rules             |
| Delivery Lead  | Confirms sign-off before Production release                                  |

## Environments

| Environment | Purpose                    | Testing Activity                                         |
|-------------|----------------------------|----------------------------------------------------------|
| local       | Early validation           | Schema checks, small sample runs                         |
| Test        | Functional testing         | End-to-end validation of ingestion with sample/full data |
| Pre Prod    | Pre-production validation  | Regression checks, stakeholder review                    |
| Prod        | Live system                | Smoke checks and final verification                      |

## Test Strategy

**Step 1 – Schema and File Validation:**

- Confirm file formats, columns, and metadata are correct
- Validate year or version context in filenames and headers

**Step 2 – Pipeline Validation:**

- Monitor logs for successful stage completion
- Validate that no rows are dropped or duplicated
- Confirm transformation and aggregation steps are applied correctly

**Step 3 – Database Validation:**

- Verify that data is stored in the correct tables with correct mappings
- Compare row counts and key metrics against input files
- Run regression scripts to ensure historical data is unaffected

**Step 4 – Service/UI Validation:**

- Verify new data appears as expected in the service layer
- Check key metrics, filters, and year switching functionality
- Perform stakeholder review on representative data sets

## Data Releases

The service performs four data releases each year: S251, BFR, CFR and AAR. From the 2025-2026 cycle the Local Authority Risk Analysis (LAA) risk indicators are refreshed alongside CFR (see [decision 0023](../architecture/decisions/0023-laa-risk-indicators-data-architecture.md)).

These releases all follow the ingestion validation model above, but differ in their primary files, business logic and ancillary datasets. The table below describes the **types** of files involved rather than exact filenames, as filenames can change from cycle to cycle. The current source and ancillary files for each release are maintained in [`data/02_Sources.md`](../data/02_Sources.md), and the data engineer confirms the exact files in scope in the individual release plan at kickoff. Release timings, sourcing and the pipeline trigger are in [`data/05_Releases.md`](../data/05_Releases.md); acronyms are in the [glossary](../glossary.md).

| Aspect                     | BFR                                  | CFR                                                                              | S251                                             | AAR                                                                           |
|----------------------------|--------------------------------------|----------------------------------------------------------------------------------|--------------------------------------------------|-------------------------------------------------------------------------------|
| **Primary files**          | SOFA and three year forecast returns | Maintained schools master list                                                   | Budget and outturn files                         | Academies accounts return and central services extracts                       |
| **Ancillary datasets**     | None                                 | GIAS, Census (pupils and workforce), SEN, CDC, KS2/4, ILR, PRU, Hospital Schools | EHCP caseload, Statistical Neighbours            | GIAS, Census (pupils and workforce), SEN, CDC, KS2/4, ILR, CFO, High Exec Pay |
| **Unique business logic**  | Three year forecast aggregation      | Schema and reconciliation focused                                                | Budget and outturn integration, LA level mapping | Trust CS fund apportionment (pupil ratio, part year, new academies)           |
| **Transparency file**      | No                                   | Yes                                                                              | No                                               | Yes                                                                           |
| **LAA risk indicators**    | No                                   | Yes (from 2025-2026)                                                             | No                                               | No                                                                            |
| **Completeness reporting** | Not required                         | Required                                                                         | Required                                         | Required                                                                      |

How QA works through a release is described in the [Data Release Test Approach](./10_Data-Release-Test-Approach.md), and the reusable template for an individual release plan is in the [Data Release Test Plan](./11_Data-Release-Test-Plan.md). The individual dated plans live in [`data-release-test-plans/`](./data-release-test-plans/).

### Assuring the LAA Risk Indicators (CFR)

From the 2025-2026 cycle the CFR release also refreshes the LAA risk indicators (see [decision 0023](../architecture/decisions/0023-laa-risk-indicators-data-architecture.md)). Because the raw non-financial inputs are not stored in the database, these figures cannot be assured through database queries; the validation model for them is file based instead:

- LAA processing runs after the CFR refresh, and the parquet file saved at calculation time is the assurance artefact.
- The calculated figures are verified against the input files and the risk indicator webpage data download, since the non-financial datapoints cannot be queried in the database.
- The headline and breakdown tables are confirmed to populate the risk indicator pages, and existing view latency is confirmed to be unaffected.

The specific checks are captured in the LAA block of the [Data Release Test Plan](./11_Data-Release-Test-Plan.md).

## Risk Mitigation

| Risk                                      | Mitigation                                                |
|-------------------------------------------|-----------------------------------------------------------|
| Upstream schema or format change          | Validate files pre-ingestion and update mapping if needed |
| Pipeline or job failure                   | Test run locally followed by in test                      |
| Regression in existing data               | Execute 1-2 year regression script                        |
| LAA raw non-financial inputs not stored in the database   | Assure figures via input files, the parquet saved at calculation time, and the risk indicator webpage data download |

## Supporting Documents

- Data Sources: [`documentation/data/02_Sources.md`](../data/02_Sources.md)
- Releases: [`documentation/data/05_Releases.md`](../data/05_Releases.md)
- Validation Scripts – for schema, mapping, and regression checks *(links to be added later)*

<!-- Leave the rest of this page blank -->
\newpage
