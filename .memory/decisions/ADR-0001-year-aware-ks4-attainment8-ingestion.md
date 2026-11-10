---
id: ADR-0001
title: "Year-Aware KS4 Input Schema and Conditional S251 Attainment 8 Extraction"
component: "data-pipeline/pre_processing"
domain: "data-pipeline"
tags: ["ks4", "attainment8", "s251", "local-authority", "input-schemas"]
status: "proposed"
created: 2026-10-23
updated: 2026-10-27
---

# Decision Record: 0001. Year-Aware KS4 Input Schema and Conditional S251 Attainment 8 Extraction

**Feature:** Populate Attainment 8 Metrics (`populate-attainment8-metrics`)  
**Date:** 2026-10-27  
**Deciders:** Engineering Team  
**Status:** Proposed

## Context

The Financial Benchmarking and Insights Tool (FBIT) must persist Key Stage 4 (KS4) Attainment 8 metrics to the SQL database columns added in migrations `141` through `146`:
1. School-level `Attainment8` in `dbo.NonFinancial` (added in `141-AlterNonFinancialAddAttainment8.sql`), populated from the `AverageAttainment` (`ATT8SCR`) column across all pipeline runs where KS4 data exists.
2. Local Authority `LocalAuthorityAvgAttainment8` and `NationalAvgAttainment8` in `dbo.LocalAuthorityNonFinancial` (added in `142-AlterLocalAuthorityNonFinancialAddAttainment8.sql`), populated from KS4 aggregate records (`RECTYPE == 4` and `RECTYPE == 5`) starting from the `2026` pipeline run (`s251_year >= 2026`).

Adding `RECTYPE` and `LEA` directly to a flat `input_schemas.ks4` dictionary causes `KeyError` failures when the pipeline processes historical KS4 datasets or unit test fixtures that only contain `["URN", "ATT8SCR", "P8MEA", "P8_BANDING"]` (see [ASSUME-001](../assumptions/ASSUME-001-historical-ks4-schema-compatibility.md)). Additionally, `insert_la_non_financial` requires `LocalAuthorityAvgAttainment8` and `NationalAvgAttainment8` columns to exist on the `local_authorities` DataFrame across all run years so that SQL projection mappings do not fail on pre-2026 runs.

## Options Considered

* **Option 1:** Enforce a single flat `ks4` schema requiring `RECTYPE` and `LEA` across all pipeline years and backfill all historical files and test fixtures.
* **Option 2:** Convert `input_schemas.ks4` into a year-keyed dictionary (`"default"` and `2026`) evolved via `evolve_schema`, keep school-level `prepare_ks4_data` compatible with the default schema, and extract Local Authority and National Average Attainment 8 via a dedicated S251 ancillary function for `s251_year >= 2026` with `np.nan` fallback columns for earlier runs.

## Evaluation

### Option 1: Single Flat Schema Across All Years
* **Pros:**
    * No year lookup is required when reading `ks4.xlsx`.
* **Cons:**
    * Breaks backwards compatibility with historical KS4 workbooks and existing unit test fixtures (`ks4_data`).
    * Violates the established repository convention for evolving schemas (`maintained_schools_master_list.py`, `census_pupils.py`, `local_authority.py`).

### Option 2: Year-Keyed KS4 Schema Using `evolve_schema` with Conditional S251 Extraction and Fallback Columns
* **Pros:**
    * Matches the established year-keyed schema pattern in `data-pipeline/src/pipeline/input_schemas/` using `evolve_schema` from `pipeline.input_schemas.utils`.
    * Preserves full backwards compatibility for 2023, 2024, and 2025 runs and existing school-level unit tests.
    * Isolates Local Authority (`RECTYPE == 4`) and National (`RECTYPE == 5`) extraction to the S251 ancillary workflow for `s251_year >= 2026`.
    * Guarantees that `LocalAuthorityAvgAttainment8` and `NationalAvgAttainment8` columns exist on the `local_authorities` DataFrame for all years (`np.nan` before 2026), matching the `ensure_dsg_recoupment_columns_are_present` pattern in `pre_processing/s251/local_authority.py`.
* **Cons:**
    * Requires passing or resolving the schema year when loading KS4 data for Local Authority benchmarking.

## Decision

We will implement **Option 2**:
1. Define `ks4` in `data-pipeline/src/pipeline/input_schemas/ks4.py` (and re-export in `input_schemas/__init__.py`) as a year-keyed dictionary with `"default"` (`URN`, `ATT8SCR`, `P8MEA`, `P8_BANDING`) and `2026` derived via `evolve_schema(_ks4_default, additions={"RECTYPE": "Int64", "LEA": "Int64"})`, along with matching column mappings.
2. Keep `prepare_ks4_data` in `data-pipeline/src/pipeline/pre_processing/ancillary/ks4.py` focused on school-level KS4 data (dropping rows where `URN` is `NA`) and map `"AverageAttainment": "Attainment8"` in `insert_non_financial_data`.
3. Add a dedicated Local Authority KS4 preparation function (`prepare_la_ks4_data`) in `data-pipeline/src/pipeline/pre_processing/ancillary/ks4.py` and invoke it from `get_s251_ancillary_data(run_id, s251_year)` when `s251_year >= 2026`.
4. Merge `LocalAuthorityAvgAttainment8` (by `old_la_code` / `LEA`) and assign `NationalAvgAttainment8` inside `build_local_authorities`, using an `ensure_attainment8_columns_are_present` helper so pre-2026 runs and missing-file scenarios populate `np.nan` (`NULL` in SQL).

## Consequences

### Positive
* Historical pipeline runs (`< 2026`) and existing test suites continue to pass without modification to legacy data files.
* Schema evolution is declarative and DRY through `evolve_schema`.
* Database projection dictionaries (`insert_non_financial_data` and `la_db_non_financial_projections`) remain static and deterministic across all years and align directly with `141-AlterNonFinancialAddAttainment8.sql` and `142-AlterLocalAuthorityNonFinancialAddAttainment8.sql`.

### Negative
* Slightly increases branching logic in `get_s251_ancillary_data` to handle `s251_year >= 2026`.
