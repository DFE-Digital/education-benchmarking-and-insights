---
id: FEAT-002
title: "Pivot SEN2 Caseload Data"
status: Approved
req: "REQ-001, REQ-002, REQ-003, REQ-004, REQ-005, REQ-006, REQ-007, REQ-008"
created: 2026-11-05
---

# Feature Specification: Pivot SEN2 Caseload Data

**Created**: 2026-11-05  
**Updated**: 2026-11-05  
**Status**: Approved  
**Input**: User description: "as of 2025, the sen2 data source on the fbit data pipeline changed format. it needed a pivot to change it into teh normal sen2_estab_caseload file. The code to do the pivot is in sen2_more. I want to integrate that code into the data pipeline so that in 2025 and 2026 s251 runs, the data pipeline takes a file caseload.csv instead opf sen2_estab_caseload, and runs the pivot on the data before passing it to the existing logic. it needs to be configured as part of config. There is a small schema difference between the 2025 and 2026 caseload file, which is that in 2025 it has 'special_academy_free': 'Special - Academy/free', and in 2026 the category got split to 2 rows. also the time_period will change depending on the s251 year. presume the caseload.csv file isin the usual area that the data pipeline finds files in. explore the problem space and spec out the feature. ask questions if you need to."

## 1. Overview

This feature integrates the SEN2 caseload format transformation (`sen2_more`) directly into the Financial Benchmarking and Insights Tool (FBIT) data pipeline for the 2025 and 2026 Section 251 (`s251`) reporting cycles:
1. **Year-Aware SEN2 Source Ingestion**: For 2025 and 2026 Section 251 runs, the pipeline reads `caseload.csv` from the standard raw input location (`default/{s251_year}/caseload.csv`) instead of `sen2_estab_caseload.csv`. Historical runs (2024 and earlier) continue to read `sen2_estab_caseload.csv`.
2. **Configurable Pre-Processing Transformation**: For 2025 and 2026 runs, the pipeline filters, normalises, and unpivots `caseload.csv` into the canonical `sen2_estab_caseload` structure before passing the transformed dataset to the existing SEN2 calculation and Local Authority joining logic.
3. **Configuration-Driven Year Differences**: All year-specific variations between 2025 and 2026 (including the input filename, the academic-to-reporting `time_period` mapping, and the split of the 2025 `special_academy_free` column into `special_academy` and `special_free_school` in 2026) are defined in pipeline configuration.
4. **Documentation Signposting**: The Docs-as-Code Reference (`docs/content/reference/sources.md`) and Data Release How-to guides (`docs/content/how-to/data-release-guide/s251.md`, `cfr.md`, and `aar.md`) are updated to signpost that 2025 and 2026 runs ingest `caseload.csv` directly and pivot it automatically within the pipeline.

## 2. Problem Statement

Local Authority non-financial profiles in FBIT rely on Department for Education (DfE) SEN2 statistics to report Education, Health and Care Plan (EHCP) caseload counts across establishment categories (Total, Mainstream, Resourced/SEN Unit, Special, Independent, Hospital/AP/PRU, Post-16, and Other).

From the 2025 reporting cycle onwards, DfE changed the published SEN2 file format from the long-format `sen2_estab_caseload.csv` file to a wide-format `caseload.csv` file. Previously, transforming `caseload.csv` into `sen2_estab_caseload.csv` required running an external standalone script (`sen2_more.py`) manually prior to pipeline execution. In addition, the 2025 and 2026 `caseload.csv` files differ in both their academic year `time_period` values (`202425` for 2025 versus `202526` for 2026) and their Special Academy/Free School columns (`special_academy_free` in 2025 versus separate `special_academy` and `special_free_school` columns in 2026).

Without integrating this transformation and its year-specific schema configuration into the data pipeline, operators must manually pre-process SEN2 files outside the pipeline, violating the single-source-of-truth workflow and risking data mismatches between 2025 and 2026 runs.

## 3. User Personas

- **Data Pipeline Operators**: Trigger annual and historical Section 251 pipeline runs using raw DfE source files uploaded to the standard raw storage container without manual file conversion steps.
- **Local Authority Financial and Educational Analysts**: Compare Local Authority Section 251 High Needs expenditure alongside accurate EHCP caseload breakdowns across mainstream, special, independent, and post-16 provisions.
- **Data Engineers and Maintainers**: Configure annual SEN2 schema updates declaratively in the pipeline configuration module without duplicating or rewriting transformation logic.

### Domain Entities

- **Raw SEN2 Caseload Dataset (`caseload.csv`)**: The wide-format DfE SEN2 statistical dataset used from 2025 onwards, containing Local Authority identifiers, academic year `time_period` codes, breakdown topics, percentage columns, and per-establishment-type caseload columns.
- **Canonical SEN2 Establishment Caseload Dataset (`sen2_estab_caseload`)**: The normalised long-format dataset containing `time_period`, Local Authority identifiers (`new_la_code`, `old_la_code`), `establishment_group`, `establishment_type`, `ehcp_or_statement`, and `num_caseload`.
- **SEN2 Year Configuration**: Declarative per-year configuration specifying the raw SEN2 filename, columns to drop, column-to-establishment-type mappings, establishment group assignment rules, academic-to-reporting `time_period` replacements, and EHCP category evaluation expressions.
- **Local Authority Non-Financial Profile**: The output Local Authority record persisted by the pipeline containing derived EHCP caseload metrics (`EHCPTotal`, `EHCPMainstream`, `EHCPResourced`, `EHCPSpecial`, `EHCPIndependent`, `EHCPHospital`, `EHCPPost16`, and `EHCPOther`).

### User Scenarios

#### Scenario 1: 2025 Section 251 Pipeline Run with `caseload.csv`
1. A Data Pipeline Operator places the 2025 `caseload.csv` file into the standard raw storage folder (`default/2025/caseload.csv`) and triggers a pipeline run with `s251` year set to `2025`.
2. The system selects `caseload.csv` based on the 2025 SEN2 configuration.
3. The system filters rows to `"All EHC plans"`, replaces `breakdown` with `"Total"`, normalises `time_period` from `202425` to `2025`, maps `special_academy_free` to `"Special - Academy/free"`, unpivots the establishment columns, and derives `establishment_group`.
4. The system passes the transformed data to the existing SEN2 processor, calculates the Local Authority EHCP category totals, and persists them in the Local Authority Non-Financial Profile.

#### Scenario 2: 2026 Section 251 Pipeline Run with Split Special Academy and Free School Categories
1. A Data Pipeline Operator places the 2026 `caseload.csv` file into the standard raw storage folder (`default/2026/caseload.csv`) and triggers a pipeline run with `s251` year set to `2026`.
2. The system selects `caseload.csv` and applies the 2026 SEN2 configuration.
3. The system normalises the 2026 academic `time_period` (`202526`) to `2026`, combines and sums the split `special_academy` and `special_free_school` columns into a single `"Special - Academy/free"` category per Local Authority, and passes the transformed data in memory to the existing SEN2 processor.
4. The system completes the existing SEN2 calculation using the 2025/2026 EHCP category formulas and persists the 2026 Local Authority EHCP metrics.

#### Scenario 3: Historical Section 251 Pipeline Run (Pre-2025)
1. A Data Pipeline Operator triggers a pipeline run with `s251` year set to `2024` (or an earlier supported year).
2. The system selects `default/{s251_year}/sen2_estab_caseload.csv` based on the historical configuration and skips the `caseload.csv` unpivot step.
3. The system processes `sen2_estab_caseload.csv` using the existing SEN2 logic without regression.

#### Scenario 4: Operator Consults Documentation for Data Release Preparation
1. A Data Pipeline Operator or Developer consults the Data Sources Reference (`docs/content/reference/sources.md`) or the Data Release How-to Guides (`s251.md`, `cfr.md`, `aar.md`) when preparing a 2025 or 2026 release.
2. The documentation clearly signposts that `caseload.csv` is the expected raw file from 2025 onwards (while `sen2_estab_caseload.csv` applies to 2024 and earlier) and that the pipeline automatically performs the pivot without manual pre-processing.

## 4. Testing Strategy & Objectives *(mandatory)*

Verification for this feature focuses on configuration-driven transformation accuracy across 2025 and 2026 schemas, seamless integration with existing SEN2 calculations, and backward compatibility for pre-2025 runs:
- **2025 Caseload Transformation Verification**: Confirm that a 2025 `caseload.csv` input (containing `time_period` `202425` and `special_academy_free`) is transformed into the expected `sen2_estab_caseload` structure and produces accurate EHCP category totals (`EHCPTotal`, `EHCPSpecial`, etc.).
- **2026 Caseload Transformation Verification**: Confirm that a 2026 `caseload.csv` input (containing `time_period` `202526` and split `special_academy` and `special_free_school` columns) transforms and pivots cleanly without duplicate index errors and accurately sums both special school categories into `EHCPSpecial`.
- **Schema Validation & Missing Column Detection**: Confirm that if a required column in the configured rename mapping is absent from `caseload.csv`, the pipeline raises an immediate, descriptive error.
- **Pre-2025 Regression Verification**: Confirm that pre-2025 runs continue to load `sen2_estab_caseload.csv` and produce identical EHCP metrics.
- **Documentation Integrity & Diátaxis Alignment**: Confirm that `docs/content/reference/sources.md` and `docs/content/how-to/data-release-guide/` (`s251.md`, `cfr.md`, `aar.md`) accurately signpost `caseload.csv` for 2025/2026 runs and pass markdown linting and documentation build checks.

### Edge Cases

- What happens when `caseload.csv` in 2026 has both `special_academy` and `special_free_school` columns mapped to `"Special - Academy/free"`? The system combines and sums the numeric caseload values of `special_academy` and `special_free_school` into a single `"Special - Academy/free"` record per Local Authority so the downstream SEN2 pivot receives unique `(new_la_code, old_la_code, establishment_group, establishment_type)` keys and the downstream `EHCPSpecial` evaluation formula remains unchanged.
- What happens when `num_caseload` values in `caseload.csv` are empty, missing, or suppressed (such as `"x"` or `"z"`)? The system fills missing unpivoted `num_caseload` values with `"x"` during the caseload transformation, and the downstream SEN2 logic treats `"x"` and `"z"` as missing (`NA`) values.
- What happens when legacy establishment types (`"Sixth form college"`, `"Other FE"`, `"Hospital School"`) are absent from `caseload.csv`? The transformation populates these columns with `"x"` (suppressed/missing) so the canonical structure remains consistent.
- What happens when an expected column configured for renaming is missing from `caseload.csv`? The system fails fast with a clear error listing the missing columns before unpivoting.
- What happens when rows in `caseload.csv` have a `breakdown` value other than `"All EHC plans"` or belong to non-matching `time_period` years? Only rows with `breakdown == "All EHC plans"` and the configured `time_period` for the target `s251` year are included in the final Local Authority calculations.

## 5. Sub-features & Specifications *(mandatory)*

- [SEN2 Caseload Ingestion and Pivot Specification](sen2-caseload-ingestion-and-pivot-spec.md): Functional requirements and acceptance criteria for year-aware file selection, filtering, unpivoting `caseload.csv`, and passing the result in memory into the existing SEN2 pipeline logic.
- [SEN2 Yearly Schema and Category Configuration Specification](sen2-yearly-schema-and-category-config-spec.md): Functional requirements and acceptance criteria for configuring 2025 and 2026 column mappings, academic `time_period` normalisation, and the 2025 vs 2026 Special Academy/Free School schema change.
- [SEN2 Documentation Signposting Specification](sen2-documentation-signposting-spec.md): Functional requirements and acceptance criteria for updating the Reference and How-to documentation to signpost `caseload.csv` from 2025 onwards.
- [Value Assessment](value-assessment.md): Evaluation of the feature against the FBIT Organisational Value Rule Set ([ASSUME-004](../../assumptions/ASSUME-004-sen2-caseload-format-from-2025.md)).

## Success Criteria *(mandatory)*

### Measurable Outcomes

- **SC-001**: 100% of 2025 and 2026 Section 251 pipeline runs ingest `default/{s251_year}/caseload.csv` directly without requiring manual pre-processing scripts.
- **SC-002**: 100% of Local Authorities with valid records in the 2025 and 2026 `caseload.csv` datasets have all eight derived EHCP metrics (`EHCPTotal`, `EHCPMainstream`, `EHCPResourced`, `EHCPSpecial`, `EHCPIndependent`, `EHCPHospital`, `EHCPPost16`, `EHCPOther`) populated in the Local Authority Non-Financial Profile.
- **SC-003**: 100% of historical Section 251 pipeline runs (`2024` and earlier) continue to ingest `sen2_estab_caseload.csv` and complete with zero regressions.
- **SC-004**: 100% of year-specific SEN2 rules (filenames, `time_period` values, column mappings, and category formulas) are defined in pipeline configuration with zero hardcoded year values in the transformation function.
- **SC-005**: All SEN2 source file checklists and reference tables in `docs/content/` accurately signpost `caseload.csv` for 2025 onwards and `sen2_estab_caseload.csv` for pre-2025 runs.

## 6. Decisions & Clarifications

The following functional clarifications were reviewed and confirmed with the user on 2026-11-05:

| ID | Topic | Question | Confirmed Decision |
| :--- | :--- | :--- | :--- |
| **CL-001** | 2026 Split `special_academy` and `special_free_school` Handling | In 2026, `caseload.csv` splits `special_academy_free` into `special_academy` and `special_free_school`. Should 2026 map them to distinct establishment types or combine/sum them into `"Special - Academy/free"`? | Combine and sum `special_academy` and `special_free_school` into `"Special - Academy/free"` so the downstream `EHCPSpecial` evaluation formula remains unchanged and no duplicate pivot keys occur. |
| **CL-002** | 2026 EHCP Category Formulas (`EHCPHospital` and `EHCPPost16`) | Should `2026` use the same `EHCPHospital` and `EHCPPost16` category evaluation formulas as `2025`? | Yes: `2026` uses the same EHCP category evaluation formulas (`la_sen2_eval`) as `2025`. |
| **CL-003** | Intermediate `sen2_estab_caseload` Output | Should the transformed `caseload.csv` data be passed purely in memory to `prepare_sen2_data` or also written out as an intermediate file? | Do not write an intermediate file: pass the transformed data purely in memory to the existing SEN2 logic. |
