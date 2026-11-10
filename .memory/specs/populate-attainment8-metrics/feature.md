---
id: FEAT-001
title: "Populate Attainment 8 Metrics"
status: Approved
req: "REQ-001, REQ-002, REQ-003, REQ-004, REQ-005, REQ-006"
created: 2026-10-23
---

# Feature Specification: Populate Attainment 8 Metrics

**Created**: 2026-10-23  
**Updated**: 2026-10-27  
**Status**: Approved  
**Input**: User description: "This feature populates some columns in Feature Details. Functionality: Added nullable columns for Attainment 8 metrics, enabling the data pipeline and views to reference these fields. Implementation: NonFinancial adds Attainment8; LocalAuthorityNonFinancial adds LocalAuthorityAvgAttainment8 and NationalAvgAttainment8. These columns are populated from KS4 data. We will only need the LA level breakdown and National average on the 2026 run but earlier runs still need to work."

## 1. Overview

This feature extends the Financial Benchmarking and Insights Tool (FBIT) to capture, process, and persist Key Stage 4 (KS4) Attainment 8 performance metrics across two organisational levels:
1. **School Level**: Persist the average Attainment 8 score for each school (both maintained schools and academies) as part of its non-financial performance profile.
2. **Local Authority and National Level**: Extract and persist the Local Authority Average Attainment 8 score and the National Average Attainment 8 score from official KS4 data as part of the Local Authority non-financial profile for the 2026 reporting cycle.

The feature maintains full backward compatibility with historical reporting runs prior to 2026. Earlier runs continue to execute without requiring Local Authority or National Average Attainment 8 breakdowns.

## 2. Problem Statement

Schools, academy trusts, and Local Authorities use FBIT to evaluate financial expenditure alongside pupil educational outcomes. Previously, the service extracted school-level Attainment 8 scores during KS4 pre-processing, but did not persist those scores into the final school non-financial records. In addition, Local Authority non-financial records lacked Local Authority Average Attainment 8 and National Average Attainment 8 benchmarks.

Without persisted Attainment 8 metrics at the school, Local Authority, and national levels, downstream views and users cannot compare school or Local Authority resource use against KS4 academic attainment for the 2026 reporting cycle.

## 3. User Personas

- **School Business Professionals and Trust Leaders**: Compare an individual school's spending and pupil characteristics against its KS4 Attainment 8 performance and comparator schools.
- **Local Authority Financial and Educational Analysts**: Evaluate Local Authority educational outcomes relative to the National Average Attainment 8 score alongside Section 251 and High Needs financial data.
- **Governors and Trustees**: Review school and Local Authority educational attainment context to support strategic financial oversight and accountability.
- **Data Pipeline Operators**: Execute annual and historical data pipeline runs reliably across the 2026 reporting cycle and prior reporting cycles (2025 and earlier).

### Domain Entities

- **KS4 Performance Dataset**: Official Department for Education (DfE) Key Stage 4 statistical release containing school-level performance records, Local Authority aggregate records, and National aggregate records.
- **School Non-Financial Profile**: The collection of demographic, workforce, Special Educational Needs (SEN), and academic outcome metrics (including KS2 Progress, KS4 Progress 8, and KS4 Attainment 8) associated with an individual school for a specific reporting run.
- **Local Authority Non-Financial Profile**: The collection of population, pupil count, SEN support, Education, Health and Care Plan (EHCP), and educational attainment metrics (Local Authority Attainment 8 and National Average Attainment 8) associated with a Local Authority for a specific reporting run.
- **Reporting Cycle Run**: A versioned execution of the data processing workflow for a specific reporting year (such as the 2026 annual run or a historical pre-2026 run).

### User Scenarios

#### Scenario 1: School Attainment 8 Ingestion and Persistence
1. A Data Pipeline Operator triggers a reporting cycle run for maintained schools and academies.
2. The system ingests the KS4 Performance Dataset for the relevant reporting period.
3. The system extracts the school-level average Attainment 8 score alongside Progress 8 metrics for each school.
4. The system persists the school-level Attainment 8 score in the School Non-Financial Profile so downstream views can display the metric.

#### Scenario 2: 2026 Local Authority and National Attainment 8 Benchmarking
1. A Data Pipeline Operator triggers the 2026 reporting cycle run.
2. The system ingests the 2026 KS4 Performance Dataset and identifies Local Authority aggregate records and the National aggregate record.
3. The system associates each Local Authority with its corresponding Local Authority Attainment 8 score and the National Average Attainment 8 score.
4. The system persists both metrics in the Local Authority Non-Financial Profile for the 2026 run.

#### Scenario 3: Historical Reporting Run Compatibility (Pre-2026)
1. A Data Pipeline Operator triggers a reporting cycle run for a year prior to 2026 (for example, 2024 or 2025).
2. The system processes school and Local Authority datasets without requiring Local Authority or National Average Attainment 8 breakdowns.
3. The historical run completes without error, recording empty (unpopulated) values for Local Authority and National Average Attainment 8 in the Local Authority Non-Financial Profile.

## 4. Testing Strategy & Objectives *(mandatory)*

Verification for this feature focuses on data accuracy, completeness of persisted non-financial metrics, and backward compatibility across reporting years:
- **School Metric Persistence Verification**: Confirm that school-level Attainment 8 values from KS4 source data are accurately retained through pre-processing and stored in the School Non-Financial Profile for both maintained schools and academies.
- **2026 Local Authority and National Benchmark Verification**: Confirm that for a 2026 run, the Local Authority Attainment 8 score and National Average Attainment 8 score are accurately extracted from the KS4 source data and stored for each Local Authority.
- **Historical Run Regression Verification**: Confirm that executing a pre-2026 run succeeds when Local Authority and National Attainment 8 breakdowns are not required or when historical KS4 source files omit Local Authority record type columns.

### Edge Cases

- What happens when a school or Local Authority has a suppressed or non-numeric Attainment 8 score (such as `NP`, `NE`, `SUPP`, or `LOWCOV`) in the KS4 dataset? The system converts suppressed values in the KS4 dataset to `0.00` during KS4 pre-processing, consistent with existing Progress 8 handling, while records absent from the KS4 dataset remain empty (null).
- What happens when a school (for example, a Primary school) has no record in the KS4 dataset? The system must store an empty (unpopulated) Attainment 8 value for that school.
- What happens when the KS4 source file is absent for a given run or year? The system must complete the run without error and store empty (unpopulated) Attainment 8 values.
- What happens when a Local Authority in the Section 251 dataset does not have a matching Local Authority record in the 2026 KS4 dataset? The system must store an empty Local Authority Attainment 8 value while still populating the National Average Attainment 8 value if available.
- How does the system handle pre-2026 runs where KS4 files do not include Local Authority or record type fields? The system must process school-level KS4 data without failing on missing Local Authority breakdown fields ([ASSUME-001](../../assumptions/ASSUME-001-historical-ks4-schema-compatibility.md)).

## 5. Sub-features & Specifications *(mandatory)*

- [School Attainment 8 Metrics Specification](school-attainment8-metrics-spec.md): Functional requirements and acceptance criteria for capturing and persisting school-level Attainment 8 scores.
- [Local Authority and National Attainment 8 Benchmarks Specification](la-attainment8-benchmarks-spec.md): Functional requirements and acceptance criteria for capturing and persisting Local Authority and National Average Attainment 8 metrics on the 2026 run and maintaining historical compatibility.
- [Value Assessment](value-assessment.md): Evaluation of the feature against the FBIT Organisational Value Rule Set.

## Success Criteria *(mandatory)*

### Measurable Outcomes

- **SC-001**: 100% of schools with valid KS4 Attainment 8 data in the input dataset have their Attainment 8 score persisted in their School Non-Financial Profile upon completion of a pipeline run.
- **SC-002**: 100% of Local Authorities processed in the 2026 reporting cycle have their matching Local Authority Attainment 8 score and the National Average Attainment 8 score persisted in their Local Authority Non-Financial Profile.
- **SC-003**: 100% of historical pipeline runs (for reporting years prior to 2026) complete with zero errors and require no manual changes to historical KS4 source files.
- **SC-004**: All persisted Attainment 8 metrics maintain numeric precision to two decimal places, matching official DfE KS4 statistical publications.

## 6. Decisions & Clarifications

The following functional clarifications were reviewed and confirmed with the user on 2026-10-23:

| ID | Topic | Question | Confirmed Decision |
| :--- | :--- | :--- | :--- |
| **CL-001** | Suppressed or Missing KS4 Scores | When a school or Local Authority has a suppressed or unavailable Attainment 8 score (`NP`, `NE`, `SUPP`, `LOWCOV`) in the KS4 file, should the persisted value be left empty (null) or defaulted to `0.00`? | Retain existing `.fillna(0)` behaviour for rows present in the KS4 dataset, while entities without a matching KS4 record (or when the KS4 file is absent) receive `NULL`. |
| **CL-002** | School Attainment 8 in Pre-2026 Runs | Should school-level Attainment 8 scores be persisted for all reporting runs (including pre-2026 runs where KS4 school data exists), with only the Local Authority and National Average Attainment 8 restricted to the 2026 run onwards? | Persist school-level Attainment 8 for all runs where KS4 data is present, and restrict Local Authority and National Average Attainment 8 extraction and persistence to the 2026 run onwards ([ASSUME-001](../../assumptions/ASSUME-001-historical-ks4-schema-compatibility.md)). |
| **CL-003** | Authoritative KS4 Year for 2026 LA Benchmarks | For the 2026 run, which reporting year parameter should determine the KS4 dataset used for Local Authority and National Average Attainment 8? | Source the 2026 Local Authority and National Average Attainment 8 metrics from the KS4 dataset corresponding to the Local Authority (`s251`) reporting year (`2026`). |
| **CL-004** | Scope of Database and Downstream Views | Are database schema migrations or updates to downstream database views or API/UI views included in the scope of this feature? | Database schema migrations and views (`141` through `146` in `Core.Database`) were added in a separate PR; this feature is scoped solely to `data-pipeline` ingestion, schema evolution (`evolve_schema`), and database persistence (`NonFinancial.Attainment8`, `LocalAuthorityNonFinancial.LocalAuthorityAvgAttainment8`, and `LocalAuthorityNonFinancial.NationalAvgAttainment8`). |
