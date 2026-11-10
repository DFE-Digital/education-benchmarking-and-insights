---
title: "Populate Attainment 8 Metrics"
component: "data-pipeline/pre_processing"
domain: "data-pipeline"
tags: ["ks4", "attainment8", "s251", "local-authority", "input-schemas"]
created: 2026-10-23
updated: 2026-10-27
---

# Implementation Proposal: Populate Attainment 8 Metrics

## Intent

Persist Key Stage 4 (KS4) Attainment 8 performance metrics into the Financial Benchmarking and Insights Tool (FBIT) operational SQL database columns (`dbo.NonFinancial.Attainment8`, `dbo.LocalAuthorityNonFinancial.LocalAuthorityAvgAttainment8`, and `dbo.LocalAuthorityNonFinancial.NationalAvgAttainment8`, which were added in `Core.Database` migrations `141` through `146`):
1. Populate `Attainment8` in `dbo.NonFinancial` from the school-level `AverageAttainment` (`ATT8SCR`) column across all pipeline runs where KS4 data exists.
2. Populate `LocalAuthorityAvgAttainment8` and `NationalAvgAttainment8` in `dbo.LocalAuthorityNonFinancial` from KS4 aggregate records (`RECTYPE == 4` and `RECTYPE == 5`) starting from the `2026` pipeline run (`s251_year >= 2026`), while preserving backwards compatibility (`NULL`) on earlier runs.

## Scope

### In-Scope

- Create `data-pipeline/src/pipeline/input_schemas/ks4.py` (and re-export in `input_schemas/__init__.py`) defining `ks4` and `ks4_column_mappings` as year-keyed dictionaries (`"default"` and `2026`) using `evolve_schema` from `pipeline.input_schemas.utils` so historical KS4 datasets and test fixtures without `RECTYPE` and `LEA` continue to load cleanly.
- Update `insert_non_financial_data` in `data-pipeline/src/pipeline/utils/database.py` to project `"AverageAttainment": "Attainment8"` into `dbo.NonFinancial`.
- Add Local Authority and National Average Attainment 8 extraction in `data-pipeline/src/pipeline/pre_processing/ancillary/ks4.py` (`RECTYPE == 4` by `LEA` mapped to `LocalAuthorityAvgAttainment8` and `RECTYPE == 5` scalar average).
- Integrate `ks4` into `get_s251_ancillary_data` (`data-pipeline/src/pipeline/pre_processing/main.py`) for `s251_year >= 2026` and merge/assign `LocalAuthorityAvgAttainment8` and `NationalAvgAttainment8` in `build_local_authorities` (`data-pipeline/src/pipeline/pre_processing/s251/local_authority.py`) with `np.nan` fallback columns for earlier runs or missing files.
- Add `"LocalAuthorityAvgAttainment8": "LocalAuthorityAvgAttainment8"` and `"NationalAvgAttainment8": "NationalAvgAttainment8"` to `la_db_non_financial_projections` in `data-pipeline/src/pipeline/config/local_authority.py`.
- Add comprehensive unit tests for school KS4 preparation, Local Authority KS4 extraction, S251 Local Authority aggregation across `2024`/`2025` and `2026` runs, and SQL projection mappings.

### Out-of-Scope

- Creating or modifying `Core.Database` SQL migration scripts or views (already completed in scripts `141` through `146`).
- Modifying Platform .NET API endpoints or Web UI components (explicitly excluded per `CL-004`).
- Backfilling `LocalAuthorityAvgAttainment8` or `NationalAvgAttainment8` on historical runs prior to `2026` (explicitly excluded per `CL-002`).
- Changing the existing `.fillna(0)` coercion behaviour for suppressed strings (`NP`, `NE`, `SUPP`, `LOWCOV`) on rows present in the KS4 workbook (confirmed per `CL-001`).

## Knowledge Context

### Assumptions

- [ASSUME-001: Historical KS4 datasets do not require Local Authority or National Attainment 8 extraction](../../assumptions/ASSUME-001-historical-ks4-schema-compatibility.md) (`validated`)
- [ASSUME-002: Attainment 8 metrics improve financial and educational outcome benchmarking](../../assumptions/ASSUME-002-attainment8-benchmarking-value.md) (`unresolved`)

### Architectural Decisions (ADRs)

- [ADR-0001: Year-Aware KS4 Input Schema and Conditional S251 Attainment 8 Extraction](../../decisions/ADR-0001-year-aware-ks4-attainment8-ingestion.md) (`proposed`)

## Approach

1. **Year-Keyed KS4 Input Schema Using `evolve_schema` (`data-pipeline/src/pipeline/input_schemas/`)**:
   Create `data-pipeline/src/pipeline/input_schemas/ks4.py` (and re-export in `input_schemas/__init__.py`) defining `ks4` and `ks4_column_mappings` keyed by `"default"` and `2026` using `evolve_schema`:
   - `_ks4_default`: `{"URN": "Int64", "ATT8SCR": "float", "P8MEA": "float", "P8_BANDING": "string"}`
   - `_ks4_2026`: `evolve_schema(_ks4_default, additions={"RECTYPE": "Int64", "LEA": "Int64"})`
   - `ks4_column_mappings`: `"default"` mapping `ATT8SCR`, `P8MEA`, and `P8_BANDING`, with `2026` evolved via `evolve_schema(_ks4_column_mappings_default)`.

2. **School-Level Attainment 8 Persistence (`ks4.py` & `database.py`)**:
   - Keep `prepare_ks4_data(ks4_path)` backward-compatible so existing callers (`prepare_ks4_data(ks4_file)` in `ancillary/main.py` and unit tests) use `"default"`, drop missing `URN` rows (`dropna(subset=["URN"])`), and return `AverageAttainment`, `Progress8Measure`, and `Progress8Banding` indexed by `URN`.
   - Add `"AverageAttainment": "Attainment8"` to the `projections` dictionary in `insert_non_financial_data` (`data-pipeline/src/pipeline/utils/database.py`).

3. **Local Authority & National Attainment 8 Extraction and Persistence (`ks4.py`, `ancillary/main.py`, `main.py`, `local_authority.py`, `config/local_authority.py`)**:
   - Add `prepare_la_ks4_data(ks4_path, year: int = 2026) -> tuple[pd.DataFrame, float]` in `data-pipeline/src/pipeline/pre_processing/ancillary/ks4.py` using `input_schemas.ks4.get(year, input_schemas.ks4[2026])` to extract:
     - `la_avg_attainment`: rows where `RECTYPE == 4`, renamed `AverageAttainment` to `LocalAuthorityAvgAttainment8`, deduplicated and indexed by `LEA` (`old_la_code`).
     - `national_avg_attainment`: scalar `float` extracted from the row where `RECTYPE == 5` (`AverageAttainment.iloc[0]`), or `np.nan` if no `RECTYPE == 5` row exists.
   - Update `get_s251_ancillary_data(run_id: str, s251_year: int)` in `data-pipeline/src/pipeline/pre_processing/main.py` to load `default/{s251_year}/ks4.xlsx` via `pre_process_la_ks4` when `s251_year >= 2026` (returning `None` for earlier years or when the blob does not exist).
   - Update `build_local_authorities` in `data-pipeline/src/pipeline/pre_processing/s251/local_authority.py` to accept `ks4: tuple[pd.DataFrame, float] | None = None`, merge `LocalAuthorityAvgAttainment8` onto `local_authority_data` (`left_on="old_la_code", right_index=True, how="left"`), assign `NationalAvgAttainment8`, and call `ensure_attainment8_columns_are_present` so both columns always exist on the returned DataFrame.
   - Add `"LocalAuthorityAvgAttainment8": "LocalAuthorityAvgAttainment8"` and `"NationalAvgAttainment8": "NationalAvgAttainment8"` to `la_db_non_financial_projections` in `data-pipeline/src/pipeline/config/local_authority.py`.
