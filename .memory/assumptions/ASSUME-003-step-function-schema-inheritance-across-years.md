---
id: ASSUME-003
title: "DfE dataset schemas evolve as a forward step-function across reporting years"
component: "data-pipeline/input_schemas"
domain: "data-pipeline"
tags: ["input-schemas", "schema-evolution", "rollover", "step-function"]
status: "unresolved"
created: 2026-10-30
updated: 2026-10-30
---

## Assumption

External Department for Education (DfE) datasets (`aar`, `bfr`, `census_pupils`, `census_workforce`, `cfo`, `cfr`, `dsg`, `gias`, `high_exec_pay`, `high_needs_places`, `hospital_schools`, `ilr`, `ks4`, `local_authority`, `maintained_schools_master_list`, `pru`) evolve chronologically as a forward step-function (`<= year`). When a pipeline run executes for year $Y$, the valid schema for any dataset without an explicit entry for year $Y$ is the schema from the largest configured year $\le Y$, rather than the oldest baseline (`"default"`) schema.

## Context

In the existing codebase, exact dictionary lookups (`schema.get(year, schema["default"])`) caused unconfigured years to fall back to `"default"` (typically pre-2023 schemas), forcing developers to copy-paste identical year keys (`2024` to `2025`, `2025` to `2026`) across all schema files. In two files (`cfo.py` and `hospital_schools.py`), `"default"` was inverted to represent the modern schema while explicit integer keys (`2021` to `2024`) represented historical overrides. Normalising all schemas to start from a baseline (`"default"`) and step forward via integer year keys allows `resolve_year_config` and `DatasetTimeline` ([ADR-0002](../decisions/ADR-0002-canonical-dataset-timeline-and-schema-normalisation.md)) to inherit the latest schema automatically on annual rollovers.

## Resolution

Pending validation during Phase 1 implementation of `canonical-schema-timeline-refactor` by verifying that all existing unit tests for historical (`2021` to `2025`) and current (`2026`) years pass under step-function resolution after normalising `cfo.py` and `hospital_schools.py`.
