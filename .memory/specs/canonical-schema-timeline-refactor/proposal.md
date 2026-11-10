---
title: "Canonical Schema Timeline and Database Projection Refactor"
component: "data-pipeline/input_schemas"
domain: "data-pipeline"
tags: ["input-schemas", "schema-evolution", "canonical-data-model", "database-projections", "refactor"]
created: 2026-10-30
updated: 2026-10-30
---

# Implementation Proposal: Canonical Schema Timeline and Database Projection Refactor

## Intent

Refactor how the Python `data-pipeline` defines and resolves annual input schemas, column mappings, derived column evaluations, and SQL database projections so that:
1. Year-dependent variation is isolated at the ingestion boundary via a declarative `DatasetTimeline` and step-function (`<= year`) lookup (`resolve_year_config`), eliminating copy-pasted unchanged year keys and silent fallbacks to legacy `"default"` schemas.
2. Raw file columns map directly to canonical internal column names at ingestion time, eliminating disconnected parallel dictionaries (`dtype` vs `*_column_mappings`), multi-hop legacy renames (`BNCH*`), and bespoke `ensure_*_columns_are_present` padding functions.
3. Database column projections are centralised in `pipeline/config/projections.py` with declarative missing-column backfill (`np.nan`).

## Scope

### In-Scope

- Implement `resolve_year_config`, `ColumnSpec`, and `DatasetTimeline` (built on `evolve_schema`) in `data-pipeline/src/pipeline/input_schemas/utils.py`.
- Fix the duplicate `eval_cost_income_categories` invocation in `data-pipeline/src/pipeline/pre_processing/cfr/maintained_schools.py` and deduplicate `nonaggregated_cost_category_map` in `data-pipeline/src/pipeline/config/__init__.py`.
- Normalise inverted `"default"` schemas in `cfo.py` and `hospital_schools.py` so all schemas progress chronologically from a baseline year (`<= year`).
- Centralise all SQL table projection dictionaries from `data-pipeline/src/pipeline/utils/database.py` and `data-pipeline/src/pipeline/config/` into `data-pipeline/src/pipeline/config/projections.py` with a declarative `apply_db_projection` helper.
- Migrate tabular datasets (`ks4`, `maintained_schools_master_list`, `census_pupils`, `census_workforce`, `gias`, `cfo`, `hospital_schools`, `high_exec_pay`, `ilr`, `pru`, `bfr`, `aar_academies`, `aar_central_services`) to `DatasetTimeline` (or `resolve_year_config` + `evolve_schema`), removing redundant unchanged year keys (`2025` in AAR, `2026` in Census, GIAS, BFR, and Master List).
- Update pivoted and multi-header datasets (`local_authority.py` S251, `sen2.py`, `dsg.py`, `high_needs_places.py`) to use `resolve_year_config` and `evolve_schema`, removing hardcoded year-string duplication.
- Remove bespoke `ensure_*_columns_are_present` functions in `census.py`, `cfr/calculations.py`, and `s251/local_authority.py` once replaced by declarative timeline and projection defaults.

### Out-of-Scope

- Modifying `Core.Database` SQL table schemas, views, or migrations.
- Modifying `.NET` Platform APIs (`platform/`), Web Portal (`web/`), or Front-End Components (`front-end-components/`).
- Introducing third-party schema validation dependencies (such as `pandera` or `pydantic`) or external YAML/JSON schema files.
- Changing business calculation formulas (such as RAG rating thresholds, federation aggregation logic, or BFR metric formulas).

## Knowledge Context

### Assumptions

- [ASSUME-001: Historical KS4 datasets do not require Local Authority or National Attainment 8 extraction](../../assumptions/ASSUME-001-historical-ks4-schema-compatibility.md) (`validated`)
- [ASSUME-003: DfE dataset schemas evolve as a forward step-function across reporting years](../../assumptions/ASSUME-003-step-function-schema-inheritance-across-years.md) (`unresolved`)

### Architectural Decisions (ADRs)

- [ADR-0001: Year-Aware KS4 Input Schema and Conditional S251 Attainment 8 Extraction](../../decisions/ADR-0001-year-aware-ks4-attainment8-ingestion.md) (`proposed`)
- [ADR-0002: Target-First Canonical Dataset Timeline and Step-Function Schema Normalisation](../../decisions/ADR-0002-canonical-dataset-timeline-and-schema-normalisation.md) (`proposed`)

## Approach

To ensure safe, incremental delivery with zero regressions, the rollout is divided into five atomic, deployable Pull Request (PR) phases:

1. **PR 1: Core Primitives, Quick-Win Bug Fixes, and Step-Function Lookup (`input_schemas/utils.py`, `cfo.py`, `hospital_schools.py`, `maintained_schools.py`)**:
   - Add `resolve_year_config(config, year)` and `DatasetTimeline` / `ColumnSpec` to `pipeline/input_schemas/utils.py`.
   - Invert `cfo.py` and `hospital_schools.py` so `"default"` represents the baseline (`2021`) schema and later years step forward.
   - Replace `.get(year, ...)` lookups across `pre_processing/` with `resolve_year_config` and delete redundant unchanged year keys (`2025` in AAR, `2026` in Census, GIAS, BFR, and Master List).
   - Remove the duplicate `eval_cost_income_categories` call in `pre_processing/cfr/maintained_schools.py` and deduplicate `nonaggregated_cost_category_map` in `config/__init__.py`.

2. **PR 2: Centralised Database Projections and Declarative Default Padding (`config/projections.py` & `utils/database.py`)**:
   - Extract all inline `projections` dictionaries from `pipeline/utils/database.py` into `pipeline/config/projections.py` alongside `trust_db_projections` and `la_db_*_projections`.
   - Implement `apply_db_projection(df, projections, defaults=...)` to project DataFrames to SQL column names while automatically populating missing historical columns with `np.nan` (or configured defaults).

3. **PR 3: Migrate Ancillary Tabular Datasets to `DatasetTimeline` (`ks4`, `census_pupils`, `census_workforce`, `gias`, `cfo`, `hospital_schools`, `high_exec_pay`, `ilr`, `pru`)**:
   - Convert ancillary dataset definitions in `input_schemas/` to `DatasetTimeline` specifications that bind raw columns, dtypes, canonical names, `na_values`, and missing-column defaults in one structure.
   - Remove `ensure_leadership_non_teacher_column_is_present` from `pre_processing/ancillary/census.py` and `ensure_la_ks4_columns_are_present` from `pre_processing/s251/local_authority.py`.

4. **PR 4: Migrate Financial Datasets (`maintained_schools_master_list`, `bfr`, `aar_academies`, `aar_central_services`) and Collapse Multi-Hop AAR Renames**:
   - Convert `maintained_schools_master_list.py` and `bfr.py` to `DatasetTimeline` / `evolve_schema` with declarative defaults, removing `ensure_it_spend_breakdown_columns_are_present` from `pre_processing/cfr/calculations.py`.
   - Refactor `aar_academies.py` and `aar_central_services.py` so both pre-2024 (`BNCH*`) and 2024+ (`BAE*`/`BAI*`) raw columns map directly to canonical category names at ingestion, eliminating the intermediate `BNCH*` rename hop.

5. **PR 5: Refactor S251, SEN2, DSG, and High Needs Places Configs (`local_authority.py`, `sen2.py`, `dsg.py`, `high_needs_places.py`)**:
   - Refactor `input_schemas/local_authority.py` (`la_budget_column_mappings`, `la_outturn_column_mappings`, `la_budget_column_eval`, `la_outturn_column_eval`, `la_section_251_column_mappings`) to use `evolve_schema` and `resolve_year_config`, eliminating ~500 lines of duplicated dictionary entries across `default`, `2024`, and `2025`.
   - Unify `high_needs_places.py` and `dsg.py` year column resolution and remove `ensure_dsg_columns_are_present` from `pre_processing/s251/local_authority.py`.
