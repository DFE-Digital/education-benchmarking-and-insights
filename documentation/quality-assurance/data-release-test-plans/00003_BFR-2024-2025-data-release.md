# Test Plan: BFR Data Release – 2024-2025

## Purpose

This plan defines the QA strategy to validate the Data Release for the **Budget Forecast Return (BFR) covering the 2024-2025 period**.

The primary focus is ensuring the integrity of the ingestion and transformation pipeline for `BFR_SOFA_raw.csv` and `BFR_3Y_raw.csv` into the FBIT platform, verifying the accuracy of the Statement of Financial Activities (SOFA) and three-year forecast data, and ensuring correct data presentation within the service.

## Scope

### In Scope

- **Schema & Structural Validation:** Strict contract checking for the primary BFR files (`BFR_SOFA_raw.csv` and `BFR_3Y_raw.csv`) to ensure header and data type integrity.
- **End-to-End (E2E) Pipeline:** Monitoring of the data release process from raw file landing to database persistence, ensuring successful transformation and aggregation.
- **Data Reconciliation:** Ensuring database records and row counts match the expected record counts from the BFR source files.
- **Service/UI Validation:** Functional verification of the FBIT front-end for the 2025 reporting year, confirming visibility for trusts and schools.
- **Regression Testing:** Verification that historical BFR data (2024 and prior) remains unchanged during this data release.

### Out of Scope

- Validation of the accuracy of data within the raw source files (upstream responsibility of Data Analysts).

## Test Data Profile

| Category | Files / Sources |
| :--- | :--- |
| **Primary BFR** | `BFR_SOFA_raw.csv` (SOFA), `BFR_3Y_raw.csv` (Three-year forecast) |

## Test Activities & Methodologies

### Schema & File Integrity Validation

**Goal:** Ensure structural integrity to prevent pipeline failures and schema-level data loss during the data release.

- **Constraint Checking:** Validate headers, data types (String, Decimal, Date), and mandatory/non-nullable fields.
- **Numeric Validation:** Verify that all financial and forecast fields contain valid numeric data without corruption.
- **Format Verification:** Check date fields and identifiers for standard compliance as per the ingestion contract.

### Pipeline Validation

**Goal:** Monitor the automated flow of data through the FBIT ingestion engine.

- **Transformation Monitoring:** Review ingestion logs in the Test environment for silent drops, warnings, or critical errors.
- **Logic Verification:** Review any specific transformation or aggregation logic applied to the three-year forecast data to ensure it aligns with business rules.

### Database Validation

**Goal:** Ensure data persistence and relational integrity.

- **Data Reconciliation:** Query the database for 2025 BFR data and confirm row counts match source file totals.
- **Completeness Check:** Verify that all trusts and schools present in the input files are correctly represented in the database tables.
- **Field Accuracy:** Validate key calculated or migrated fields against the raw source data for precision.

### Service/UI Validation

**Goal:** Final user acceptance of the data presentation.

- **Data Visibility:** Navigate the service to confirm BFR data is visible and correctly associated with the expected entities.
- **Metric Accuracy:** Cross-check high-level summary metrics in the UI against values confirmed during database validation.

### Regression Testing

**Goal:** Prevent degradation of historical datasets.

- **Historical Integrity:** Run regression scripts to confirm previous years’ BFR data remains unchanged.
- **Comparative Analysis:** Compare historical row counts and sample records before and after the 2025 ingestion cycle.

## Responsibilities & Environment

### Responsibilities

| Role | Responsibility |
| :--- | :--- |
| **Data Analyst(s)** | Produce and review source BFR files for accuracy before the data release. |
| **Data Engineer** | Load source files, execute pipeline runs, and monitor ingestion logs for technical errors. |
| **QA Lead** | Prepare test plans/scripts; manage overall execution of schema, database, and UI validation. |
| **Engineer(s)** | Assist in test execution, running regression scripts, and recording results under QA guidance. |
| **Technical Lead** | Oversee the technical quality and architectural integrity of the BFR ingestion pipeline. |
| **Stakeholders** | Conduct User Acceptance Testing (UAT) and provide formal sign-off on data validation outcomes. |
| **Project Lead** | Final Go/No-Go decision for the BFR Data Release. |

### Environments

- **Test:** Sandbox for initial ingestion, schema validation, E2E pipeline testing, and stakeholder review.
- **Pre-Prod:** Final User Acceptance Testing (UAT) and smoke checks before production.

## Exit Criteria (Sign-off Requirements)

- [x] All BFR primary schemas pass validation
- [x] Pipeline completes E2E with no High or Critical errors in logs
- [x] Database record counts match source `BFR_SOFA` and `BFR_3Y` files
- [x] Regression tests confirm historical BFR data integrity (2024 and prior)
- [x] UI/Service displays 2025 BFR data accurately with correct metrics
- [x] Product Owner/BA sign-off received
