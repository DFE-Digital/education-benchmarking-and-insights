---
title: "Intent: Populate Attainment 8 Metrics"
feature: "populate-attainment8-metrics"
created: 2026-10-23
updated: 2026-10-27
---

# Intent: Populate Attainment 8 Metrics

## Objective

Enable the FBIT Python data pipeline to persist school-level `Attainment8` into `dbo.NonFinancial` across all pipeline runs and Local Authority / National Average Attainment 8 benchmarks (`LocalAuthorityAvgAttainment8`, `NationalAvgAttainment8`) into `dbo.LocalAuthorityNonFinancial` for the `2026` run onwards, without breaking historical runs (`< 2026`).

## Boundaries & Scope Constraints

- **Untouchable:**
  - `core-infrastructure/src/db/Core.Database/Scripts/` (all required database tables, columns, views, and parameters were already added in scripts `141` through `146`).
  - `platform/` (.NET APIs), `web/` (ASP.NET MVC portal), and `front-end-components/`.
- **Scope Limits:**
  - Modify only `data-pipeline/src/pipeline/` modules (`input_schemas/ks4.py`, `input_schemas/__init__.py`, `pre_processing/ancillary/ks4.py`, `pre_processing/ancillary/main.py`, `pre_processing/main.py`, `pre_processing/s251/local_authority.py`, `config/local_authority.py`, `utils/database.py`) and `data-pipeline/tests/`.

## Technical Constraints

- **Dependencies:** Must use existing Python 3.13, `pandas`, `numpy`, and `sqlalchemy` packages in `data-pipeline/pyproject.toml`. Do not add new third-party dependencies.
- **Schema Evolution:** Must use `evolve_schema` from `pipeline.input_schemas.utils` to derive the `2026` KS4 schema and column mappings from the `"default"` KS4 definitions.
- **Performance:** Extracting LA and National Attainment 8 from `ks4.xlsx` must operate in-memory on the single KS4 DataFrame without increasing S251 pre-processing execution time by more than 5%.
- **Compatibility:** Must maintain backwards compatibility with `2023`, `2024`, and `2025` pipeline runs and existing unit test fixtures where `ks4.xlsx` only contains `["URN", "ATT8SCR", "P8MEA", "P8_BANDING"]`.

## Data Definitions & Schemas

### Pre-Existing Database Schema (`Core.Database`)

The target database columns were added in a separate PR and must not be recreated:
- `141-AlterNonFinancialAddAttainment8.sql`: Added `Attainment8 DECIMAL(18,2) NULL` to `dbo.[NonFinancial]`.
- `142-AlterLocalAuthorityNonFinancialAddAttainment8.sql`: Added `LocalAuthorityAvgAttainment8 DECIMAL(18,2) NULL` and `NationalAvgAttainment8 DECIMAL(18,2) NULL` to `dbo.[LocalAuthorityNonFinancial]`.
- `143` through `146`: Refreshed views (`VW_CurrentDefaultNonFinancial`, `VW_CurrentDefaultLocalAuthorityNonFinancial`, `VW_SchoolCharacteristics`) and inserted the `Attainment8` parameter.

### Code Data Structures

- **Input Schemas (`data-pipeline/src/pipeline/input_schemas/ks4.py`):**
  ```python
  from .utils import evolve_schema

  ks4_index_col = "URN"

  _ks4_default = {
      "URN": "Int64",
      "ATT8SCR": "float",
      "P8MEA": "float",
      "P8_BANDING": "string",
  }

  _ks4_2026 = evolve_schema(
      _ks4_default,
      additions={
          "RECTYPE": "Int64",
          "LEA": "Int64",
      },
  )

  ks4 = {
      "default": _ks4_default,
      2026: _ks4_2026,
  }

  _ks4_column_mappings_default = {
      "ATT8SCR": "AverageAttainment",
      "P8MEA": "Progress8Measure",
      "P8_BANDING": "Progress8Banding",
  }

  ks4_column_mappings = {
      "default": _ks4_column_mappings_default,
      2026: evolve_schema(_ks4_column_mappings_default),
  }
  ```
- **Internal Data Flows & State Objects:**
  - `prepare_ks4_data(ks4_path) -> pd.DataFrame`: Reads `ks4_path` using `input_schemas.ks4["default"]` and `input_schemas.ks4_column_mappings["default"]`, drops rows where `URN` is `NA` (in case aggregate rows exist in the workbook), coerces `AverageAttainment` and `Progress8Measure` with `.fillna(0)`, and sets index to `URN`.
  - `prepare_la_ks4_data(ks4_path, year: int = 2026) -> tuple[pd.DataFrame, float]`: Reads `ks4_path` using `input_schemas.ks4.get(year, input_schemas.ks4[2026])` and `input_schemas.ks4_column_mappings.get(year, input_schemas.ks4_column_mappings[2026])`, coerces `AverageAttainment` (`pd.to_numeric(..., errors="coerce").fillna(0)`), extracts `la_avg_attainment` for `RECTYPE == 4` (indexed by `LEA` with column `LocalAuthorityAvgAttainment8`), and extracts `national_avg_attainment` scalar `float` for `RECTYPE == 5` (or `np.nan` if no `RECTYPE == 5` row exists).
  - `build_local_authorities(..., ks4: tuple[pd.DataFrame, float] | None = None) -> pd.DataFrame`: Merges `la_avg_attainment` on `left_on="old_la_code", right_index=True, how="left"`, assigns `NationalAvgAttainment8 = national_avg_attainment`, and calls `ensure_attainment8_columns_are_present` to guarantee `LocalAuthorityAvgAttainment8` and `NationalAvgAttainment8` exist (defaulting to `np.nan`).
- **Database Projection Mappings:**
  - `data-pipeline/src/pipeline/utils/database.py` (`insert_non_financial_data`): Add `"AverageAttainment": "Attainment8"` to `projections`.
  - `data-pipeline/src/pipeline/config/local_authority.py` (`la_db_non_financial_projections`): Add `"LocalAuthorityAvgAttainment8": "LocalAuthorityAvgAttainment8"` and `"NationalAvgAttainment8": "NationalAvgAttainment8"`.

## API & Integration Contracts

No REST, RPC, GraphQL, or webhook endpoints are added or modified by this feature. The integration boundary is the Azure SQL Database schema (`dbo.NonFinancial` and `dbo.LocalAuthorityNonFinancial`).

## Validation Rules

### Test Sets (Input/Output Rulesets)
- **Test Set 1 (School KS4 Persistence):** Given a school with `URN = 100101` and `ATT8SCR = "46.5"` in `ks4.xlsx`, `prepare_ks4_data` produces `AverageAttainment = 46.5`, and `insert_non_financial_data` projects `Attainment8 = 46.5` into `dbo.NonFinancial`.
- **Test Set 2 (School Suppressed KS4 Value):** Given a school with `URN = 100102` and `ATT8SCR = "SUPP"` in `ks4.xlsx`, `prepare_ks4_data` produces `AverageAttainment = 0.0`.
- **Test Set 3 (2026 Local Authority & National Attainment 8):** Given `s251_year = 2026` and a `ks4.xlsx` workbook containing `(RECTYPE=4, LEA=201, ATT8SCR=50.25)` and `(RECTYPE=5, ATT8SCR=45.80)`, `build_local_authorities` sets `LocalAuthorityAvgAttainment8 = 50.25` and `NationalAvgAttainment8 = 45.80` for Local Authority `201`.
- **Test Set 4 (Pre-2026 Local Authority Run):** Given `s251_year = 2024` or `2025` (`ks4=None`), `build_local_authorities` sets `LocalAuthorityAvgAttainment8 = np.nan` and `NationalAvgAttainment8 = np.nan` for all Local Authorities.

### Properties (Universally Quantifiable Predicates)
- **Property 1:** For every DataFrame returned by `build_local_authorities(...)` regardless of `s251_year` or whether `ks4` is `None`, `"LocalAuthorityAvgAttainment8" in df.columns` and `"NationalAvgAttainment8" in df.columns` must both be `True`.
- **Property 2:** For every school row in `prepare_ks4_data(...)`, the index `URN` contains no `NA` values (`df.index.isna().sum() == 0`), ensuring Local Authority (`RECTYPE == 4`) and National (`RECTYPE == 5`) rows without a school `URN` never pollute school-level merges.

### Contracts (Preconditions, Invariants, Postconditions)
- **Contract 1:** Precondition: `ks4.xlsx` for `s251_year >= 2026` conforms to `input_schemas.ks4[2026]`. Postcondition: `insert_la_non_financial` executes parameterised SQL `INSERT INTO LocalAuthorityNonFinancial` including `LocalAuthorityAvgAttainment8` and `NationalAvgAttainment8` with `np.nan` converted to `None` (`NULL`).

## Measurable Success Criteria

- `pytest tests/unit` in `data-pipeline/` passes with 100% pass rate, including all new unit tests for `ks4`, `local_authority`, and `database` projections.
- `ruff check`, `black --check`, and `isort --check` pass in `data-pipeline/` with zero warnings or errors.

## Implementation Directives

- Run `poetry run pytest tests/unit` and `poetry run ruff check .` inside `data-pipeline/` after completing each Python task group.
