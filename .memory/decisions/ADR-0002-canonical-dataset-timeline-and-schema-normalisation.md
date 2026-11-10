---
id: ADR-0002
title: "Target-First Canonical Dataset Timeline and Step-Function Schema Normalisation"
component: "data-pipeline/input_schemas"
domain: "data-pipeline"
tags: ["input-schemas", "schema-evolution", "canonical-data-model", "database-projections", "maintainability"]
status: "proposed"
created: 2026-10-30
updated: 2026-10-30
---

# Decision Record: 0002. Target-First Canonical Dataset Timeline and Step-Function Schema Normalisation

**Feature:** Canonical Schema Timeline Refactor (`canonical-schema-timeline-refactor`)  
**Date:** 2026-10-30  
**Deciders:** Engineering Team  
**Status:** Proposed

## Context

The Python `data-pipeline` ingests 18 external Department for Education (DfE) datasets across multiple reporting cycles (`2021` through `2026`). Raw file schemas, column names, granular category breakdowns, and file formats change from year to year. The guiding architectural principle for the pipeline is to isolate year-dependent variation in declarative configuration and keep processing logic independent of dates and years. Because contractors maintain the codebase, configurations must have low cognitive load and show clear year-on-year diffs.

An architectural audit identified four systemic problems with the current approach:
1. **Exact-Year Lookup Traps (`schema.get(year, schema["default"])`)**: Resolving schemas with `.get(year, ...)` requires developers to duplicate unchanged year keys across every dictionary (`2024` and `2025` are identical in `aar_academies.py` and `aar_central_services.py`; `2025` and `2026` are duplicated in `census_pupils.py`, `census_workforce.py`, `gias.py`, and `maintained_schools_master_list.py`). If a new year key is omitted during annual rollover, `.get()` silently falls back to `"default"` (the oldest legacy schema) instead of the latest configured year. Furthermore, `"default"` means the oldest schema in most files, but means the newest schema in `cfo.py` and `hospital_schools.py`.
2. **Disconnected Parallel Dictionaries**: Each dataset splits its yearly configuration across separate `dtype` dictionaries, `*_column_mappings` dictionaries, and `*_column_eval` dictionaries. Although `evolve_schema` in `pipeline.input_schemas.utils` reduces duplication within a single dictionary, renaming a raw column in a new year still requires updating multiple parallel structures manually.
3. **Leaky Ingestion Boundaries and `ensure_*_columns_are_present` Proliferation**: When a later year adds new columns (such as 2024 Workforce Leadership Non-Teachers, 2025 CFR IT spend `E20A` to `E20G`, or 2026 KS4 Local Authority Attainment 8), pre-processing modules require bespoke imperative helper functions (`ensure_*_columns_are_present`) to pad `np.nan` values so historical runs do not fail during calculations or SQL projections.
4. **Multi-Hop Renaming and Scattered Database Projections**: Columns pass through up to four naming layers from raw file to SQL table (for example, `BAE010-T` to `BNCH21101 (Teaching staff)` to `Teaching and Teaching support staff_Teaching staff` to `TeachingStaffCosts`). Database projection dictionaries are split between `pipeline/config/` and local variables inside `pipeline/utils/database.py`.

## Options Considered

* **Option 1 (Incremental Dictionary Consolidation):** Keep the separate `dtype`, `*_column_mappings`, and `*_column_eval` dictionaries, add a `resolve_year_config(config, year)` floor-key lookup helper (`<= year`), convert remaining copy-pasted dictionaries to use `evolve_schema`, and centralise database projections with default missing-column padding.
* **Option 2 (Recommended: Target-First Canonical `DatasetTimeline` Contracts and Unified Database Projections):** Replace disconnected per-year dictionaries with a cohesive `DatasetTimeline` specification at the ingestion boundary. Map raw columns directly to canonical column names, evolve schemas via step-function temporal deltas (`<= year`), automatically backfill missing historical columns with configured defaults (`np.nan` or `0.0`) at read time, and centralise all SQL table projections in `pipeline/config/projections.py`.
* **Option 3 (Third-Party Schema Framework with External YAML):** Replace Python schema modules with Pandera `DataFrameSchema` classes and external YAML configuration files.
* **Option 4 (Minimal Cleanup Only):** Keep exact-year `.get(year, schema["default"])` lookups and existing dictionary structures, and only apply `evolve_schema` to the remaining copy-pasted files.

## Evaluation

### Option 1: Incremental Dictionary Consolidation (`resolve_year_config` + `evolve_schema`)
* **Pros:**
    * Small implementation effort (2 to 3 days) with minimal changes to `pre_processing/` call sites.
    * Eliminates duplicate year keys (`2025` in AAR, `2026` in Census and GIAS) through floor-key (`<= year`) resolution.
    * Uses the existing `evolve_schema` function without introducing new classes.
* **Cons:**
    * Does not solve the disconnected dictionary problem: raw column types, rename mappings, and missing-column defaults remain split across separate dictionaries that can drift out of sync.
    * Leaves raw or legacy intermediate column names (such as `BNCH*` codes) leaking into `pre_processing/`.

### Option 2 (Recommended): Target-First Canonical `DatasetTimeline` Contracts and Unified Database Projections
* **Pros:**
    * **Solves the Root Problem (Anti-Corruption Layer at Ingestion)**: Year-awareness stops at the ingestion boundary. Downstream pre-processing functions receive a DataFrame with guaranteed canonical column names and types regardless of the run year.
    * **Single Source of Truth per Dataset**: Co-locates raw column names, data types, canonical target names, `na_values`, and historical default backfill values (`np.nan` or `0.0`) in one timeline declaration.
    * **Step-Function Rollover Safety**: Resolves deltas chronologically up to `<= year` ([ASSUME-003](../assumptions/ASSUME-003-step-function-schema-inheritance-across-years.md)). Unchanged years require zero configuration changes, and new years automatically inherit the latest schema rather than reverting to legacy `"default"`.
    * **Eliminates `ensure_*_columns_are_present` and Multi-Hop Renames**: Columns added in later years are declared once in the timeline with a backfill default and automatically populated with `np.nan` (or `0.0`) when loading earlier years. Raw columns map directly to canonical names, removing intermediate legacy renames.
* **Cons / Drawbacks:**
    * **Bespoke Internal Abstraction**: Introduces a custom ~100-line `DatasetTimeline` / `ColumnSpec` abstraction in `pipeline.input_schemas.utils` that new contractors must learn during onboarding.
    * **Higher Migration Effort**: Requires updating both `input_schemas/` definitions and the `pd.read_csv` / `pd.read_excel` ingestion call sites across `pre_processing/` (5 to 7 days across phased pull requests).
    * **Imperfect Fit for Non-Tabular / Pivoted Sources**: Datasets with multi-row headers or pre-rename pivots (specifically Section 251 in `local_authority.py`, SEN2 in `sen2.py`, and multi-header ODS sheets in `dsg.py`) cannot use a simple 1:1 flat column reader without pre-pivot hook support or retaining step-function dictionary resolution for their pivot stages.
    * **Test Fixture Coupling**: Existing unit tests that construct synthetic raw DataFrames or inspect intermediate pre-rename column names will need updating as multi-hop renames are collapsed.

### Option 3: Third-Party Schema Framework (Pandera / Pydantic) + External YAML
* **Pros:**
    * Provides built-in runtime value and range validation.
* **Cons:**
    * Adds a heavy external dependency (`pandera`).
    * Class inheritance handles column additions well, but expressing column removals and raw-to-canonical renames across years is more verbose than `evolve_schema` or `DatasetTimeline`.
    * Moving schemas to YAML loses Python type safety, IDE navigation, and delta composition.

### Option 4: Minimal Cleanup Only (`evolve_schema` Rollout Without Lookup Changes)
* **Pros:**
    * Lowest immediate effort (1 day).
* **Cons:**
    * Retains the dangerous `.get(year, schema["default"])` exact-key lookup trap for future annual rollovers.
    * Leaves all `ensure_*_columns_are_present` helpers, duplicate `eval_cost_income_categories` calls, and 4-layer renames in place.

## Decision

We will implement **Option 2** via a phased, backwards-compatible rollout across `data-pipeline`:

1. **Core Resolution and Timeline Primitives (`pipeline/input_schemas/utils.py`)**:
   * Add `resolve_year_config(config, year)` implementing step-function (floor-key `<= year`) lookup for any dictionary-based configuration, and standardise `"default"` (or baseline year keys) so integer years always progress forward chronologically.
   * Implement `ColumnSpec` (or `Col`) and `DatasetTimeline` on top of `evolve_schema` and `resolve_year_config` to bind raw column names, dtypes, canonical target names, and backfill defaults (`default_if_missing=np.nan`) in a single declarative chain.
2. **Centralised Database Projections (`pipeline/config/projections.py` & `pipeline/utils/database.py`)**:
   * Move all hardcoded SQL `projections` dictionaries out of `pipeline/utils/database.py` into `pipeline/config/projections.py`.
   * Introduce `apply_db_projection(df, projections, defaults=...)` to project canonical columns to SQL column names and backfill optional/historical columns declaratively.
3. **Phased Dataset Migration (`pipeline/input_schemas/` & `pipeline/pre_processing/`)**:
   * Migrate tabular datasets (`ks4`, `maintained_schools_master_list`, `census_pupils`, `census_workforce`, `gias`, `cfo`, `hospital_schools`, `high_exec_pay`, `ilr`, `pru`, `bfr`, `aar_academies`, `aar_central_services`) to `DatasetTimeline`.
   * Collapse intermediate `BNCH*` renames in AAR (`academies.py` and `central_services.py`) so raw columns map directly to canonical category names, and deduplicate `nonaggregated_cost_category_map` in `pipeline/config/__init__.py`.
   * Remove redundant unchanged year keys (`2025` in AAR, `2026` in Census, GIAS, BFR, and Master List) and remove bespoke `ensure_*_columns_are_present` functions once covered by timeline and projection defaults.
   * Fix the duplicate `eval_cost_income_categories` call in `pre_processing/cfr/maintained_schools.py`.

## Consequences

### Positive
* Annual dataset rollovers only require configuration changes when DfE modifies a file's structure; unchanged datasets automatically carry forward their latest schema.
* Year-on-year schema changes are expressed as compact, self-documenting deltas (`rewire`, `add`, `remove`) in a single file per dataset.
* Pre-processing modules operate on stable canonical columns without `if year >= ...` checks or `ensure_*_columns_are_present` patching functions.
* All database table column mappings live in `pipeline/config/projections.py` rather than inside database I/O functions.

### Negative
* Contractors must understand the `DatasetTimeline` and `resolve_year_config` primitives in `pipeline/input_schemas/utils.py` (mitigated by keeping the implementation under 120 lines with docstrings and unit tests).
* Pivoted datasets (`local_authority.py` S251 and `sen2.py`) still require a two-stage config (raw read + post-pivot canonical evaluation), which will use `resolve_year_config` and `evolve_schema` rather than flat 1:1 column mapping.
