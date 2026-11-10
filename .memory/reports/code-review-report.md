---
title: "Code Review Report - Pivot SEN2 Caseload Data (333315/333807)"
date: 2026-11-10
severity: MEDIUM
confidence: HIGH
tags: [code, review, engineering]
---

# Code Review Report

**Date:** 2026-11-10 | **Change Intent:** Integrate in-memory pivoting and year-aware schema configuration for the 2025 and 2026 SEN2 `caseload.csv` Local Authority dataset and update data release documentation | **Status:** WARN

## 1. Executive Summary

| Severity | Count | Summary |
|---|---|---|
| 🔴 CRITICAL | 0 | Immediate action required |
| 🟠 HIGH | 0 | Serious logic/performance risk |
| 🟡 MEDIUM | 1 | Improvement/Maintenance risk |
| 🔵 LOW | 2 | Minor/Refinement opportunities |
| ⚪ INFO | 0 | Observation/Best practice |

## 2. Architectural Assessment

- **Alignment:** SOUND
- **Boundaries & Cohesion:** Blob storage retrieval remains isolated in `pre_process_sen2` (`data-pipeline/src/pipeline/pre_processing/ancillary/main.py`), while `pivot_sen2_caseload_data` and `prepare_sen2_data` (`data-pipeline/src/pipeline/pre_processing/ancillary/sen2.py`) operate purely on file buffers and in-memory DataFrames. Year-specific column mappings and category expressions are declaratively defined in `data-pipeline/src/pipeline/input_schemas/local_authority.py`.
- **Precedent & Standards:** The 2026 schema delta (`special_academy_free` split into `special_academy` and `special_free_school`) uses `evolve_schema` from `pipeline.input_schemas.utils`, and unit tests in `test_la_sen2.py` use in-memory `io.StringIO` buffers and mocks in accordance with `data-pipeline/README.md` and `data-pipeline/AGENTS.md`. Documentation updates in `docs/content/reference/sources.md` and `docs/content/how-to/data-release-guide/` align with the Reference and How-to quadrants of the Diátaxis framework.
- **Trade-offs & Potential Improvements:**
  - **Adopt step-function year resolution (`<= year` floor lookup) across SEN2 config dictionaries (Open-Closed Principle / Rollover Safety):** `pre_process_sen2` and `prepare_sen2_data` currently rely on exact year-key membership (`year in input_schemas.la_sen2_caseload_column_mappings` and `.get(year, ...["default"])`), where `"default"` points to the pre-2025 `sen2_estab_caseload.csv` format. Resolving year keys via a chronological floor lookup (`<= year`, as proposed in `ADR-0002`) would allow 2027 and future S251 runs to inherit the 2026 `caseload.csv` configuration automatically without duplicating year keys across `la_sen2_filenames`, `la_sen2_caseload_column_mappings`, and `la_sen2_eval`.
  - **Normalise wide `caseload.csv` directly to canonical evaluation columns once pre-2025 runs are retired (Data Locality / Reduced Intermediate Coupling):** `pivot_sen2_caseload_data` currently melts wide `caseload.csv` data into the legacy 14-column long format (`sen2_estab_caseload`) and populates synthetic `legacy_na_columns` (`Sixth form college`, `Other FE`, `Hospital School`), only for `prepare_sen2_data` to immediately drop 7 metadata columns and pivot the remaining rows back to wide format. Retaining this two-stage adapter is pragmatic while pre-2025 format compatibility is required, but mapping wide `caseload.csv` columns directly to canonical category inputs would eliminate the unpivot-repivot round-trip.

## 3. Findings

### Logic & Data Integrity - MEDIUM

- **Location:** `data-pipeline/src/pipeline/pre_processing/ancillary/sen2.py:39-68, 82`
- **Risk:** `pd.read_csv` in `pivot_sen2_caseload_data` omits explicit `dtype` definitions, which leaves `time_period`, `old_la_code`, and establishment caseload columns subject to implicit pandas type inference. If `time_period` is inferred as a string (for example, when non-numeric identifiers exist in the raw CSV), `df["time_period"] == academic_year` compares strings to an `int` and filters out all rows; if any caseload column is inferred as `object`, `.astype(schema)` at line 82 raises `TypeError` when casting string values to `"Int64"`. In addition, lines 46-52 mutate the boolean-filtered DataFrame slice without `.copy()`, and line 62 passes duplicate `"Special - Academy/free"` column names in `value_vars` for 2026.
- **Recommendation:** Pass explicit `dtype` mappings (`"string"` for `id_vars` and `"Int64"` for `old_la_code` and `column_mappings`) to `pd.read_csv`, compare `time_period` as a string, call `.copy()` after row filtering, initialise `legacy_na_columns` with `"Int64"` dtype, and deduplicate `value_vars` with `dict.fromkeys`.
```python
    raw_dtypes = (
        {col: "string" for col in id_vars}
        | {"old_la_code": "Int64"}
        | {col: "Int64" for col in column_mappings}
    )

    df = pd.read_csv(
        caseload_filepath_or_buffer,
        usecols=usecols,
        dtype=raw_dtypes,
        na_values={col: na_values for col in [*column_mappings, "old_la_code"]},
    )

    academic_year = f"{year - 1}{year % 100:02d}"
    df = df[
        (df["time_period"] == academic_year)
        & (df["breakdown"] == "All EHC plans")
    ].copy()
    df["breakdown"] = "Total"
    df["time_period"] = str(year)
    for col in legacy_na_columns:
        df[col] = pd.Series(pd.NA, index=df.index, dtype="Int64")
    df = df.rename(columns=column_mappings)

    # Combine split establishment columns that map to the same target name
    # (e.g. 2026 special academy and special free school columns).
    for col in df.columns[df.columns.duplicated()].unique():
        combined = df[col].sum(axis=1, min_count=1).astype("Int64")
        df = df.drop(columns=[col])
        df[col] = combined

    value_vars = list(dict.fromkeys(column_mappings.values())) + legacy_na_columns
    df = df.melt(
        id_vars=id_vars,
        value_vars=value_vars,
        var_name="establishment_type",
        value_name="num_caseload",
    )
```

### Maintainability - LOW

- **Location:** `data-pipeline/src/pipeline/input_schemas/__init__.py:82`, `data-pipeline/src/pipeline/input_schemas/local_authority.py:588-619`, `data-pipeline/README.md:107-116`, `docs/content/how-to/data-release-guide/s251.md:99`
- **Risk:** `la_sen2_caseload_drop_columns` in `local_authority.py` and `__init__.py` is unused dead configuration after `pivot_sen2_caseload_data` switched to `usecols`. In `docs/content/how-to/data-release-guide/s251.md:99`, the `Gotchas` section still states that operators must generate the SEN2 file manually, contradicting line 25, and the uncommitted diff in `data-pipeline/README.md:107-116` replaces generic queue message placeholders with hardcoded `2025` values.
- **Recommendation:** Remove `la_sen2_caseload_drop_columns` from `local_authority.py` and `input_schemas/__init__.py`, update or remove the stale SEN2 manual generation bullet in `docs/content/how-to/data-release-guide/s251.md:99`, and revert the uncommitted placeholder edits in `data-pipeline/README.md`.
```python
la_sen2_index_column = ["new_la_code", "old_la_code"]
la_sen2_filenames = {
    "default": "sen2_estab_caseload.csv",
    2025: "caseload.csv",
    2026: "caseload.csv",
}
la_sen2_caseload_na_columns = {
    "default": [
        "Sixth form college",
        "Other FE",
        "Hospital School",
    ],
}
```

### Test Quality - LOW

- **Location:** `data-pipeline/tests/unit/pre_processing/local_authorities/conftest.py:491`, `data-pipeline/tests/unit/pre_processing/local_authorities/test_la_sen2.py:43-56`
- **Risk:** All rows in `la_sen2_caseload_2025_raw` and `la_sen2_caseload_2026_raw` set `breakdown` to `"All EHC plans"`, so unit tests do not verify that rows with other `breakdown` values are excluded. In addition, `test_sen2_2026_caseload_combines_split_special_academy_and_free_school` omits assertions for `EHCPMainstream`, `EHCPResourced`, and `EHCPIndependent`.
- **Recommendation:** Include a non-`"All EHC plans"` row in the caseload test fixtures (or add a filter assertion in `test_la_sen2.py`) and assert all eight derived EHCP columns in `test_sen2_2026_caseload_combines_split_special_academy_and_free_school`.
```python
def test_sen2_2026_caseload_combines_split_special_academy_and_free_school(
    la_sen2_caseload_2026_raw: pd.DataFrame,
):
    result = prepare_sen2_data(
        io.StringIO(la_sen2_caseload_2026_raw.to_csv(index=False)), 2026
    )

    assert len(result.index) == 1
    row = result.loc[("E10000000", 101)]
    assert row["EHCPTotal"] == 100
    assert row["EHCPMainstream"] == 17
    assert row["EHCPResourced"] == 6
    assert row["EHCPSpecial"] == 35
    assert row["EHCPIndependent"] == 9
    assert row["EHCPHospital"] == 6
    assert row["EHCPPost16"] == 12
    assert row["EHCPOther"] == 15
```

## 4. Required Remediation

- [ ] MEDIUM Pass explicit `dtype` mappings to `pd.read_csv`, filter `time_period` as a string, call `.copy()` on the filtered slice, and deduplicate `value_vars` in `data-pipeline/src/pipeline/pre_processing/ancillary/sen2.py`.
- [ ] LOW Remove unused `la_sen2_caseload_drop_columns` from `data-pipeline/src/pipeline/input_schemas/local_authority.py` and `data-pipeline/src/pipeline/input_schemas/__init__.py`, remove the stale manual SEN2 generation note in `docs/content/how-to/data-release-guide/s251.md`, and revert uncommitted hardcoded values in `data-pipeline/README.md`.
- [ ] LOW Add test coverage for non-`"All EHC plans"` `breakdown` filtering and assert all eight derived EHCP columns in `data-pipeline/tests/unit/pre_processing/local_authorities/test_la_sen2.py`.
