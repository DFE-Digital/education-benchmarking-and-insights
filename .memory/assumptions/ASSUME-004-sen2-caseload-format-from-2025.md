---
id: ASSUME-004
title: "SEN2 Local Authority Caseload Source Format from 2025 Onwards"
component: "data-pipeline/pre_processing/ancillary/sen2"
domain: "data-pipeline"
tags: ["sen2", "s251", "caseload", "schema-evolution", "local-authority"]
status: "validated"
created: 2026-11-05
updated: 2026-11-05
resolved: 2026-11-05
---

## Assumption

From the 2025 Section 251 (`s251`) reporting cycle onwards (`2025` and `2026`), the Department for Education (DfE) SEN2 Local Authority caseload dataset is supplied in the standard raw input storage path (`default/{s251_year}/caseload.csv`) in a wide column format (`breakdown == "All EHC plans"` and 6-digit academic year `time_period` values such as `202425` and `202526`), whereas historical Section 251 runs (`2024` and earlier) continue to supply the pre-pivoted long-format `default/{s251_year}/sen2_estab_caseload.csv` file.

## Context

Prior to 2025, the pipeline ingested `sen2_estab_caseload.csv` directly and pivoted `establishment_group` and `establishment_type` rows into Local Authority Education, Health and Care Plan (EHCP) placement totals (`EHCPTotal`, `EHCPMainstream`, `EHCPResourced`, `EHCPSpecial`, `EHCPIndependent`, `EHCPHospital`, `EHCPPost16`, and `EHCPOther`). Starting in 2025, the upstream DfE SEN2 release changed format to `caseload.csv`, requiring a pre-ingestion unpivot (melt) and category normalisation step before executing the existing SEN2 calculation logic.

## Resolution

Validated by implementing `pivot_sen2_caseload_data` and year-keyed SEN2 caseload configuration (`2025` and `2026`) in `data-pipeline/src/pipeline/input_schemas/local_authority.py` and `data-pipeline/src/pipeline/pre_processing/ancillary/sen2.py`, verified by automated unit tests across `2024`, `2025`, and `2026` in `test_la_sen2.py`.
