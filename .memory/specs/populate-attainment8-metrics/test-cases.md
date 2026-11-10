# Test Cases: Populate Attainment 8 Metrics

## Overview

This document defines the automated unit and integration test scenarios for the `populate-attainment8-metrics` feature, aligned with the project test strategy in [`.memory/testing.md`](../../testing.md) and functional requirements `REQ-001` through `REQ-006`.

## Happy Path Scenarios

### TC-001: School-Level KS4 Data Preparation (`prepare_ks4_data`)
- **Requirement:** `REQ-001`, `REQ-002`
- **Preconditions:** An in-memory Excel buffer (`ks4_data` fixture) containing `URN`, `ATT8SCR`, `P8MEA`, and `P8_BANDING` columns with valid numeric scores (for example, `URN=100101`, `ATT8SCR=20.0`).
- **Steps:**
  1. Call `prepare_ks4_data(ks4_data)`.
  2. Inspect the resulting DataFrame columns, index, and values.
- **Expected Result:**
  - The DataFrame is indexed by `URN`.
  - Column `AverageAttainment` is present with dtype `float64` and value `20.0` for `URN=100101`.

### TC-002: School-Level NonFinancial Database Projection (`insert_non_financial_data`)
- **Requirement:** `REQ-001`, `REQ-002`
- **Preconditions:** A mocked SQLAlchemy connection and a school DataFrame containing `AverageAttainment = 47.25`.
- **Steps:**
  1. Inspect or invoke `insert_non_financial_data` in `data-pipeline/src/pipeline/utils/database.py`.
- **Expected Result:**
  - `AverageAttainment` is mapped to SQL column `Attainment8` in `dbo.NonFinancial`.

### TC-003: 2026 Local Authority and National Average KS4 Extraction (`prepare_la_ks4_data`)
- **Requirement:** `REQ-004`, `REQ-005`
- **Preconditions:** An in-memory Excel buffer conforming to `input_schemas.ks4[2026]` (`RECTYPE`, `LEA`, `URN`, `ATT8SCR`, `P8MEA`, `P8_BANDING`) containing:
  - School rows (`RECTYPE == 1`, `URN = 100101`, `LEA = 201`, `ATT8SCR = 48.0`)
  - Local Authority rows (`RECTYPE == 4`, `LEA = 201`, `URN = NA`, `ATT8SCR = 50.25`; `RECTYPE == 4`, `LEA = 202`, `URN = NA`, `ATT8SCR = 44.10`)
  - National row (`RECTYPE == 5`, `LEA = NA`, `URN = NA`, `ATT8SCR = 46.50`)
- **Steps:**
  1. Call `prepare_la_ks4_data(ks4_2026_data, year=2026)`.
- **Expected Result:**
  - Returns `(la_avg_attainment, national_avg_attainment)`.
  - `la_avg_attainment` is indexed by `LEA` (`201`, `202`) and has column `LocalAuthorityAvgAttainment8` with values `50.25` and `44.10`.
  - `national_avg_attainment` equals `46.50`.

### TC-004: 2026 S251 Local Authority Aggregation with KS4 Benchmarks (`build_local_authorities`)
- **Requirement:** `REQ-004`, `REQ-005`
- **Preconditions:** Valid S251 inputs for `s251_year = 2026` (`old_la_code` values `201`, `202`, `203`) and `ks4` tuple from `prepare_la_ks4_data`.
- **Steps:**
  1. Call `build_local_authorities(..., s251_year=2026, ks4=ks4_tuple)`.
- **Expected Result:**
  - `LocalAuthorityAvgAttainment8` is populated with matched LA scores (`50.25` for `201`, `44.10` for `202`).
  - `NationalAvgAttainment8` equals `46.50` across all Local Authority rows.
  - `la_db_non_financial_projections` maps both columns directly to `LocalAuthorityNonFinancial`.

## Negative Scenarios

### TC-005: Pre-2026 S251 Local Authority Run (`s251_year = 2024` and `2025`)
- **Requirement:** `REQ-006`
- **Preconditions:** S251 inputs for `s251_year = 2024` or `2025` where `get_s251_ancillary_data` does not extract LA/National KS4 benchmarks (`ks4 = None`).
- **Steps:**
  1. Call `build_local_authorities(..., s251_year=2024, ks4=None)`.
- **Expected Result:**
  - The pipeline succeeds without raising a `KeyError`.
  - Both `LocalAuthorityAvgAttainment8` and `NationalAvgAttainment8` exist on the returned DataFrame and contain `np.nan` for every Local Authority row.

### TC-006: Missing KS4 File on 2026 Run
- **Requirement:** `REQ-003`, `REQ-004`, `REQ-005`
- **Preconditions:** `get_blob` returns `None` for `default/2026/ks4.xlsx`.
- **Steps:**
  1. Execute `pre_process_ks4` and `pre_process_la_ks4` (or `build_local_authorities` with `ks4=None`).
- **Expected Result:**
  - School `AverageAttainment` is `np.nan` (`NULL` in `dbo.NonFinancial`).
  - Local Authority `LocalAuthorityAvgAttainment8` and `NationalAvgAttainment8` are `np.nan` (`NULL` in `dbo.LocalAuthorityNonFinancial`).

## Edge Case Scenarios

### TC-007: Suppressed Strings (`NP`, `NE`, `SUPP`, `LOWCOV`) in KS4 Data
- **Requirement:** `REQ-003`, `CL-001`
- **Preconditions:** KS4 input workbook contains `"SUPP"` or `"LOWCOV"` in `ATT8SCR` for a present school (`RECTYPE == 1`) or Local Authority (`RECTYPE == 4`).
- **Steps:**
  1. Call `prepare_ks4_data` and `prepare_la_ks4_data`.
- **Expected Result:**
  - Suppressed strings on present rows are coerced via `pd.to_numeric(..., errors="coerce").fillna(0)` to `0.0`.

### TC-008: Unmatched Local Authority in 2026 KS4 Data
- **Requirement:** `REQ-004`
- **Preconditions:** Local Authority `old_la_code = 203` exists in `s251` data, but `LEA = 203` has no `RECTYPE == 4` row in `ks4.xlsx`.
- **Steps:**
  1. Call `build_local_authorities(..., s251_year=2026, ks4=ks4_tuple)`.
- **Expected Result:**
  - `LocalAuthorityAvgAttainment8` for `old_la_code = 203` is `np.nan` (persisted as SQL `NULL`), while `NationalAvgAttainment8` still receives the national value (`46.50`).

### TC-009: Missing `RECTYPE == 5` Row in 2026 KS4 Data
- **Requirement:** `REQ-005`
- **Preconditions:** A 2026 `ks4.xlsx` buffer contains `RECTYPE == 4` rows but zero `RECTYPE == 5` rows.
- **Steps:**
  1. Call `prepare_la_ks4_data(ks4_buffer, year=2026)`.
- **Expected Result:**
  - `national_avg_attainment` defaults to `np.nan` without raising an `IndexError`.

### TC-010: School KS4 Preparation Ignores Aggregate Rows Without `URN`
- **Requirement:** `REQ-002`
- **Preconditions:** A 2026 `ks4.xlsx` buffer contains school rows (`URN` populated) plus LA/National aggregate rows (`URN` is `NA`).
- **Steps:**
  1. Call `prepare_ks4_data(ks4_2026_buffer)`.
- **Expected Result:**
  - Rows with `URN = NA` are dropped before setting the `URN` index so that school DataFrames only merge valid `URN` keys.
