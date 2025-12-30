# Test Plan: AAR Data Release – 2025

## Purpose

This plan defines the QA strategy to validate the data release, specifically the ingestion and transformation of the AAR 2025 dataset into the FBIT platform. The primary focus is ensuring the integrity of the pipeline for `aar.csv` and `aar_cs.csv`, the accurate integration of 12 ancillary datasets, and the correct execution of trust-level Central Service (CS) fund allocation logic.

## Scope

### In Scope

- **Schema & Structural Validation:** Strict contract checking for primary AAR files and all listed ancillary datasets (GIAS, Pupil/Workforce Census, SEN, CDC, KS2/4, CFO, ILR, High Exec Pay) within this Data Release.
- **Trust CS Fund Allocation Logic:** Validation of apportionment based on pupil counts, part-year membership, and new academy joiners.
- **End-to-End (E2E) Pipeline:** Monitoring of the **Data Release** process from raw file landing to database persistence.
- **Data Reconciliation:** Ensuring database records match source file totals and counts.
- **Service/UI Validation:** Functional verification of the FBIT front-end for the 2025 reporting year.
- **Regression Testing:** Verification that historical AAR data (2024 and prior) remains unchanged during this data release.
- **AAR 2025 Transparency File Integration:** Addition of the transparency dataset into the service.

### Out of Scope

- Validation of the accuracy of data within the raw source files (upstream responsibility of Data Analysts).

## Test Data Profile

| Category | Files / Sources |
|---------|-----------------|
| Primary AAR | `aar.csv` (School-level), `aar_cs.csv` (Trust Central Services) |
| Organisational | gias.csv, gias_links.csv |
| Census Data | census_pupils.csv, census_workforce.xlsx, Workforce_2010_2024_fte_hc_nat_reg_la_sch.csv |
| Educational/Financial | sen.csv, cdc.csv, ks2.xlsx, ks4.xlsx, cfo.xlsx, ILR R06 cut with FSM and EHCP.xlsx, HExP.csv |
| Transparency | AAR 2025 Transparency File |

## Test Activities & Methodologies

### Schema & File Integrity Validation

**Goal:** Ensure structural integrity to prevent pipeline failures and schema-level data loss during the data release.

- Constraint Checking: Validate headers, data types (String, Decimal, Date), and mandatory/non-nullable fields (URN, Trust UID).
- Identifier Consistency: Verify URNs and Trust company numbers follow the standard 6 and 8 digit formats respectively.
- Uniqueness: Confirm no duplicate primary keys across URN/Trust combinations.

### Ancillary Data & Completeness Reporting

**Goal:** Quantify "Data Readiness" before final processing.

- Cross-Reference Validation: Ensure ancillary data (e.g., KS2/KS4) correctly maps to the URNs present in the primary `aar.csv`.
- Metrics Generation:
  - Volume Check: Record count comparison against previous year's data drop.
  - Orphan Reporting: Identify academies present in AAR but missing from specific ancillary sets.
  - Summary Table: High-level report of "Completeness %" per dataset.

### Business Logic: Trust CS Fund Allocation

**Goal:** Verify the mathematical accuracy of the FBIT transformation engine.

- Proportional Apportionment: Use SQL/Python scripts to verify that `aar_cs.csv` funds are split among academies based on pupil headcount ratios.
- Temporal Logic (Part-Year):
  - Validate "Mid-year Joiner" logic: ensure allocation is weighted by days spent within the trust.
  - Check for prevention of double counting when academies move trusts mid-cycle.
- Newly Formed Academies: Validate correct initialization and allocation from date of formation.
- Rounding & Reconciliation: Ensure academy-level totals sum exactly to trust-level totals.

### Database & Pipeline Validation

**Goal:** Ensure data persistence and relational integrity.

- Transformation Monitoring: Review ingestion logs for silent drops or warnings during ETL.
- Relational Joins: Validate correct joining of AAR data with ancillary datasets (Workforce, SEN, KS2, etc.).
- Regression Guardrails: Run checksums on 2024 data to ensure no accidental overwrites occurred.

### Service/UI Validation

**Goal:** Final user acceptance of the data presentation.

- Year Selection: Verify the 2025 toggle loads the correct dataset.
- Data Accuracy: Spot-check high-value metrics against raw source files.
- Fallback States: Ensure UI handles missing data gracefully (e.g., N/A instead of errors).

### AAR 2025 Transparency File Integration

**Goal:** Ensure the transparency is correctly produced.

- Validation steps to be added here.

## Responsibilities & Environment

### Responsibilities

| Role                | Responsibility                                                                                     |
|:--------------------|:---------------------------------------------------------------------------------------------------|
| **Data Analyst(s)** | Produce and review source files for accuracy before the data release.                              |
| **Data Engineer**   | Execute pipeline runs, provide logs, and support technical testing activities.                     |
| **QA Lead**         | Prepare test plans/scripts; manage overall testing and delegate tasks across the engineering team. |
| **Engineer(s)**     | Assist in test execution and script verification under the guidance of QA.                         |
| **Technical Lead**  | Oversee the technical quality and architectural integrity of the overall data release.             |
| **Stakeholders**    | Conduct User Acceptance Testing (UAT) and provide formal sign-off.                                 |
| **Project Lead**    | Final Go/No-Go decision for the Data Release.                                                      |

### Environments

- **Test:** Sandbox for initial ingestion and logic debugging.
- **Pre-Prod:** Full-scale dress rehearsal with complete ancillary data for final sign-off.

## Exit Criteria (Sign-off Requirements)

- [ ] All primary and ancillary schemas pass validation
- [ ] Completeness report generated and reviewed
- [ ] CS fund allocation logic verified with 0% variance
- [ ] Pipeline completes E2E with no High or Critical errors
- [ ] Regression tests confirm historical data integrity
- [ ] UI displays AAR 2025 data accurately across all metrics and filters
- [ ] Transparency file successfully verified
