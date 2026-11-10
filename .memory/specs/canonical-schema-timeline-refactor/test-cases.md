# Test Cases: Canonical Schema Timeline and Database Projection Refactor

## 🟢 Happy Path Scenarios

### TC-01: Step-Function Year Resolution Inherits Most Recent Prior Year
- **Pre-conditions**: A year-keyed configuration defines `"default"` and `2024` entries, with no explicit keys for `2025` or `2026`.
- **Steps**:
  1. Call `resolve_year_config(config, 2023)`.
  2. Call `resolve_year_config(config, 2024)`.
  3. Call `resolve_year_config(config, 2026)`.
- **Expected Result**: Step 1 returns the `"default"` entry; Steps 2 and 3 both return the `2024` entry without falling back to `"default"`.

### TC-02: `DatasetTimeline` Normalises Rewired Raw Columns to Identical Canonical Columns Across Years
- **Pre-conditions**: A `DatasetTimeline` defines a canonical column `"AverageAttainment"` mapped from `"ACTYRGRP"` in `"default"` and rewired to `"ATT8SCR"` from `2023` onwards.
- **Steps**:
  1. Pass a raw `2022` DataFrame containing `{"URN": [1001], "ACTYRGRP": [45.2]}` to `timeline.normalise(df_2022, year=2022)`.
  2. Pass a raw `2026` DataFrame containing `{"URN": [1001], "ATT8SCR": [45.2], "RECTYPE": [1], "LEA": [201]}` to `timeline.normalise(df_2026, year=2026)`.
- **Expected Result**: Both normalised DataFrames contain the canonical `"AverageAttainment"` column with value `45.2` and share the exact same canonical column set.

### TC-03: Automatic Backfill of Future-Year Columns During Historical Pipeline Runs
- **Pre-conditions**: Maintained schools CFR data is processed for `year=2024` (where raw files do not contain `E20A` through `E20G` IT spend breakdown columns) and for `year=2025` (where `E20A` through `E20G` exist).
- **Steps**:
  1. Execute `build_maintained_school_data` for `year=2024`.
  2. Execute `build_maintained_school_data` for `year=2025`.
- **Expected Result**: The `2024` output DataFrame contains all canonical IT spend breakdown columns populated with `np.nan`, and the `2025` output DataFrame contains the parsed numeric values from `E20A` through `E20G`, without requiring a separate `ensure_it_spend_breakdown_columns_are_present` helper.

### TC-04: Direct Canonical Mapping of Pre-2024 (`BNCH*`) and 2024+ (`BAE*`/`BAI*`) AAR Data
- **Pre-conditions**: Academy financial return raw data is provided in `2023` format (`BNCH21101 (Teaching staff)`) and in `2025` format (`BAE010-T`).
- **Steps**:
  1. Execute `prepare_aar_data` for `year=2023` and `year=2025`.
- **Expected Result**: Both runs produce identical canonical cost and income columns (`Teaching and Teaching support staff_Teaching staff`, `Total Expenditure`, `Total Income`, `In year balance`) with matching calculated totals.

### TC-05: Declarative Database Projection with Missing Optional Columns
- **Pre-conditions**: A pre-processed DataFrame is passed to `apply_db_projection` (or `insert_la_non_financial`) for a historical year (`2024`) where `LocalAuthorityAvgAttainment8` and `NationalAvgAttainment8` were not extracted.
- **Steps**:
  1. Call `apply_db_projection(df, la_db_non_financial_projections)`.
- **Expected Result**: The projected DataFrame contains all target SQL columns in `la_db_non_financial_projections`, with `LocalAuthorityAvgAttainment8` and `NationalAvgAttainment8` automatically populated as `np.nan` (persisted as SQL `NULL`).

## 🔴 Negative Scenarios

### TC-06: Unconfigured Dataset Without `"default"` or Prior Year Raises Explicit `KeyError`
- **Pre-conditions**: A configuration dictionary contains only `{2023: {"A": "int"}}` (no `"default"` key) and is queried for `year=2021`.
- **Steps**:
  1. Call `resolve_year_config(config, 2021)`.
- **Expected Result**: Raises a descriptive `KeyError` stating that no configuration exists for `year=2021` (rather than returning `None` and causing a downstream `AttributeError` or `TypeError` inside `pd.read_csv`).

### TC-07: `DatasetTimeline` Rejects Rewiring or Removing an Unknown Canonical Column
- **Pre-conditions**: A developer defines `.evolve(2025, rewire={"NonExistentCanonicalCol": "RAW_COL"})` or `.evolve(2025, remove=["NonExistentCanonicalCol"])`.
- **Steps**:
  1. Instantiate or resolve the `DatasetTimeline`.
- **Expected Result**: Raises a `KeyError` immediately at schema definition/resolution time identifying `"NonExistentCanonicalCol"`.

## 🟡 Edge Cases

### TC-08: Historical Override Schemas (`cfo` and `hospital_schools`) Resolve Identically Across `2021` to `2026`
- **Pre-conditions**: `cfo` and `hospital_schools` schemas are normalised so `"default"` represents the `2021` baseline and `2024` / `2025` represent forward steps.
- **Steps**:
  1. Run `prepare_cfo_data` and `prepare_hospital_schools_data` across years `2021`, `2022`, `2023`, `2024`, `2025`, and `2026`.
- **Expected Result**: Years `2021` through `2023` resolve the historical column names (`"Direct Revenue Financing (Revenue contributions to capital)"` for Hospital Schools), and `2024` through `2026` resolve the updated column names (`"Direct revenue financing (Capital expenditure from revenue)"`).

### TC-09: S251 Pivoted Budget and Outturn Evaluations Across `2023`, `2024`, and `2025`+
- **Pre-conditions**: S251 Local Authority raw data is pivoted into `<category>__<metric>` columns for `2023` (`"default"`), `2024`, and `2026` (inheriting `2025`).
- **Steps**:
  1. Execute `prepare_local_authority_data` for `2023`, `2024`, and `2026`.
- **Expected Result**: All three years resolve the appropriate S251 category rollups and produce identical canonical `Budget*` and `Outturn*` columns matching `la_db_financial_projections`.
