# Test Plan: CFR Data Release – 2024-2025

## Purpose

This plan defines the QA strategy to validate the Data Release for the **Consistent Financial Reporting (CFR) covering the 2024-2025 period**.
The primary focus is ensuring the integrity of the ingestion and transformation pipeline for the `maintained_schools_master_list.csv` into the FBIT platform, the accurate integration of ancillary datasets, and the verification of data availability and accuracy within the service.

## Scope

### In Scope

- **Schema & Structural Validation:** Strict contract checking for the primary CFR file (`maintained_schools_master_list.csv`) and all listed ancillary datasets (GIAS, Census, SEN, CDC, KS2/4, ILR) within this Data Release.
- **End-to-End (E2E) Pipeline:** Monitoring of the data release process from raw file landing to database persistence, ensuring no silent drops or transformation failures.
- **Data Reconciliation:** Ensuring database records, row counts, and key joins (school identifiers) match source file totals and counts.
- **Service/UI Validation:** Functional verification of the FBIT front-end for the 2025 reporting year for maintained schools.
- **Regression Testing:** Verification that historical CFR data (2024 and prior) remains unchanged during this data release.
- **CFR 2024-2025 Transparency File Integration:** Validation of the transparency dataset generated against the input files.

### Out of Scope

- Validation of the accuracy of data within the raw source files (upstream responsibility of Data Analysts).

## Test Data Profile

| Category | Files / Sources                                                 |
| :--- |:----------------------------------------------------------------|
| **Primary CFR** | maintained_schools_master_list.csv                              |
| **Organisational** | gias.csv, gias_links.csv                                        |
| **Census Data** | census_pupils.csv, census_workforce.xlsx, sen.csv               |
| **Educational/Financial** | cdc.csv, ks2.xlsx, ks4.xlsx, ILR R06 cut with FSM and EHCP.xlsx |
| **Transparency** | CFR 2024-2025 Transparency File                                 |

## Test Activities & Methodologies

### Schema & File Integrity Validation

**Goal:** Ensure structural integrity to prevent pipeline failures and schema-level data loss during the data release.

- **Constraint Checking:** Validate headers, data types (String, Decimal, Date), and mandatory/non-nullable fields (URNs, numeric fields).
- **Identifier Consistency:** Verify URNs follow the standard format and that value formats are valid across all primary and ancillary files.
- **Uniqueness:** Confirm no duplicate records within the primary CFR file.

### Ancillary Data & Completeness Reporting

**Goal:** Quantify "Data Readiness" and identify gaps before final processing.

- **Cross-Reference Validation:** Ensure ancillary data (e.g., KS2/KS4, SEN) correctly maps to the URNs present in the primary CFR master list.
- **Metrics Generation:**
  - **Volume Check:** Record count comparison and identification of schools missing specific ancillary data pieces.
  - **Orphan Reporting:** Capture counts of missing data by ancillary type (e.g., number of schools without `ks2.xlsx` data).
  - **Summary Table:** Produce a completeness table indicating how many schools have missing ancillary data.

### Database & Pipeline Validation

**Goal:** Ensure data persistence and relational integrity.

- **Transformation Monitoring:** Trigger CFR ingestion and monitor logs for processing successes, silent drops, or duplicates.
- **Relational Joins:** Query the database to validate that mappings derived from ancillary sources are present, consistent, and correctly joined to school identifiers.
- **Regression Guardrails:** Run checksums and regression queries on historical CFR data to ensure no accidental overwrites occurred.

### Service/UI Validation

**Goal:** Final user acceptance of the data presentation.

- **Year Selection:** Access the service and verify the 2025 toggle loads the correct maintained schools dataset.
- **Data Accuracy:** Spot-check high-value metrics and filters for a representative sample of schools against raw source files.
- **Fallback States:** Ensure UI handles schools with missing ancillary data gracefully (documenting any visible impact).

### CFR 2024-2025 Transparency File Integration

**Goal:** Ensure the transparency file is correctly produced and matches the source.

- **Validation:** Compare the generated transparency file against the input ingestion files to ensure 1:1 mapping and data accuracy.

## Responsibilities & Environment

### Responsibilities

| Role | Responsibility |
| :--- | :--- |
| **Data Analyst(s)** | Produce and review source CFR and ancillary files for accuracy before the data release. |
| **Data Engineer** | Execute pipeline runs, provide logs, and support technical testing activities. |
| **QA Lead** | Prepare test plans/scripts; manage overall execution of schema validation and completeness reporting. |
| **Engineer(s)** | Assist in test execution, running validation scripts, and performing regression checks under QA guidance. |
| **Technical Lead** | Oversee the technical quality and architectural integrity of the CFR ingestion pipeline. |
| **Stakeholders** | Conduct User Acceptance Testing (UAT) and provide formal sign-off on data integrity. |
| **Project Lead** | Final Go/No-Go decision for the Data Release. |

### Environments

- **Test:** Sandbox for initial ingestion, schema validation, and logic debugging.
- **Pre-Prod:** Full-scale dress rehearsal with complete ancillary data for final stakeholder sign-off.

## Exit Criteria (Sign-off Requirements)

- [x] All primary and ancillary schemas pass validation
- [x] Completeness report generated and reviewed by Stakeholders
- [x] Pipeline completes E2E with no High or Critical errors
- [x] Database reflects accurate 2024-2025 CFR data with expected mappings
- [x] Regression tests confirm historical data integrity
- [x] UI displays CFR 2025 data accurately across all metrics and filters
- [x] CFR 2024-2025 Transparency file successfully verified
