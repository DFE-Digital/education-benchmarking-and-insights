---
title: "Intent: Canonical Schema Timeline and Database Projection Refactor"
feature: "canonical-schema-timeline-refactor"
created: 2026-10-30
updated: 2026-10-30
---

# Intent: Canonical Schema Timeline and Database Projection Refactor

## Objective

Establish a single, maintainable pattern for multi-year schema evolution, canonical column normalisation, and SQL table projection in `data-pipeline`, ensuring that annual DfE dataset rollovers require configuration changes only for columns that actually changed and that pre-processing logic operates on year-agnostic canonical DataFrames.

## Boundaries & Scope Constraints

- **Untouchable:**
  - `core-infrastructure/` (SQL database schemas, migrations, and views must not be altered).
  - `platform/`, `web/`, and `front-end-components/`.
  - Public function signatures and return DataFrame schemas of `pre_process_*` entrypoints in `data-pipeline/src/pipeline/pre_processing/main.py`.
- **Scope Limits:**
  - Restrict all modifications to `data-pipeline/src/pipeline/input_schemas/`, `data-pipeline/src/pipeline/config/`, `data-pipeline/src/pipeline/pre_processing/`, `data-pipeline/src/pipeline/utils/database.py`, and `data-pipeline/tests/`.

## Technical Constraints

- **Dependencies:** Use only Python 3.13 standard library (`dataclasses`, `typing`, `collections.abc`) and existing `pandas` / `numpy` packages in `data-pipeline/pyproject.toml`. Zero new third-party packages.
- **Simplicity:** Keep `pipeline/input_schemas/utils.py` (`evolve_schema`, `resolve_year_config`, `ColumnSpec`, `DatasetTimeline`) under 150 lines of standard, fully typed Python so contractors can understand it in a single reading.
- **Backwards Compatibility:** During the phased rollout, `DatasetTimeline` must expose backwards-compatible dictionary views (or work seamlessly with `resolve_year_config`) so datasets can be migrated incrementally across PRs 1 through 5 without breaking unmigrated callers.
- **Data Integrity:** Generated Parquet outputs and SQL `INSERT` payloads across all years (`2021` to `2026`) must be bit-for-bit identical in column names, types, and values before and after the refactor.

## Data Definitions & Schemas

### Database Schema Changes

None. All target SQL tables (`School`, `Trust`, `LocalAuthority`, `NonFinancial`, `Financial`, `TrustFinancial`, `BudgetForecastReturn`, `BudgetForecastReturnMetric`, `LocalAuthorityFinancial`, `LocalAuthorityNonFinancial`, `LocalAuthorityStatisticalNeighbours`) remain unchanged.

### Code Data Structures

- **Step-Function Year Lookup (`data-pipeline/src/pipeline/input_schemas/utils.py`):**
  ```python
  def resolve_year_config[T](config: Mapping[int | str, T], year: int) -> T:
      """
      Resolve configuration for `year` using step-function (floor-key) semantics:
      returns the value for the largest integer key <= `year`, falling back to
      `config["default"]` if no integer key is <= `year`.
      """
  ```
- **Canonical Dataset Timeline (`data-pipeline/src/pipeline/input_schemas/utils.py`):**
  ```python
  @dataclass(frozen=True)
  class ColumnSpec:
      raw_name: str
      dtype: str
      default: Any = np.nan
      fillna: Any = None

  class DatasetTimeline:
      """
      Declarative timeline mapping raw dataset columns by year to a stable
      canonical output schema.
      """
      def evolve(
          self,
          year: int,
          *,
          add: Mapping[str, ColumnSpec] | None = None,
          rewire: Mapping[str, str | ColumnSpec] | None = None,
          remove: Iterable[str] = (),
          column_eval: Mapping[str, str] | None = None,
      ) -> Self: ...

      def resolve_raw_dtypes(self, year: int) -> dict[str, str]: ...
      def resolve_column_mappings(self, year: int) -> dict[str, str]: ...
      def normalise(self, df: pd.DataFrame, year: int) -> pd.DataFrame: ...
  ```
- **Unified Database Projection Helper (`data-pipeline/src/pipeline/config/projections.py`):**
  ```python
  def apply_db_projection(
      df: pd.DataFrame,
      projections: Mapping[str, str],
      defaults: Mapping[str, Any] | None = None,
  ) -> pd.DataFrame:
      """
      Ensure all source columns in `projections` exist on `df` (populating any
      missing columns from `defaults` or `np.nan`), rename them to target SQL
      column names, and select only the projected target columns.
      """
  ```

## API & Integration Contracts

No HTTP API or queue message contracts change. The SQL database projection contract (`apply_db_projection` feeding `_write_data` in `pipeline/utils/database.py`) guarantees that every projected DataFrame contains the exact target columns expected by the SQL temporary table insertion query.

## Validation Rules

### Test Sets (Input/Output Rulesets)
- **Test Set 1 (Step-Function Inheritance):** Given a config `{ "default": {"A": "int"}, 2024: {"A": "int", "B": "float"} }`, calling `resolve_year_config(config, 2023)` returns `{"A": "int"}`, calling `resolve_year_config(config, 2024)` returns `{"A": "int", "B": "float"}`, and calling `resolve_year_config(config, 2025)` or `2026` returns `{"A": "int", "B": "float"}` without requiring `2025` or `2026` keys.
- **Test Set 2 (Canonical Column Rewiring):** Given a `DatasetTimeline` where canonical column `"AverageAttainment"` maps from raw column `"ACTYRGRP"` in `"default"` and is rewired to `"ATT8SCR"` in `2023`, normalising a `2022` DataFrame with `"ACTYRGRP"` and a `2025` DataFrame with `"ATT8SCR"` both produce a DataFrame containing `"AverageAttainment"` with identical float values.
- **Test Set 3 (Automatic Historical Backfill):** Given a `DatasetTimeline` where `"E20A  Connectivity"` is added in `2025` with `default=np.nan`, normalising a `2024` DataFrame that lacks `"E20A  Connectivity"` automatically populates `"E20A  Connectivity"` with `np.nan` without calling an imperative `ensure_*` helper.
- **Test Set 4 (Database Projection Backfill):** Given `apply_db_projection(df, projections={"LocalAuthorityAvgAttainment8": "LocalAuthorityAvgAttainment8"})` on a DataFrame missing `"LocalAuthorityAvgAttainment8"`, the returned DataFrame contains `"LocalAuthorityAvgAttainment8"` filled with `np.nan`.

### Properties (Universally Quantifiable Predicates)
- **Property 1 (Year-Invariance of Canonical Output Columns):** For every dataset managed by `DatasetTimeline` and for any two years $y_1, y_2 \in [2021, 2026]$, the set of canonical columns produced by `timeline.normalise(df, y_1)` equals the set of canonical columns produced by `timeline.normalise(df, y_2)`.
- **Property 2 (Monotonic Forward Step Resolution):** For any config dictionary passed to `resolve_year_config(config, year)` where `year >= max(int_keys)`, the resolved config always equals `config[max(int_keys)]` and never falls back to `config["default"]`.

### Contracts (Preconditions, Invariants, Postconditions)
- **Contract 1:** Precondition: Raw input file for year $Y$ contains all columns in `timeline.resolve_raw_dtypes(Y).keys()`. Postcondition: `timeline.normalise(raw_df, Y)` returns a DataFrame with all canonical columns present, renamed, dtype-coerced, and indexed by the dataset's `index_col`.

## Measurable Success Criteria

- 100% of existing and new unit tests in `data-pipeline/tests/unit` pass (`poetry run pytest tests/unit`).
- Zero duplicate unchanged year keys remain across `data-pipeline/src/pipeline/input_schemas/`.
- All four `ensure_*_columns_are_present` functions in `pre_processing/` and the duplicate `eval_cost_income_categories` call in `maintained_schools.py` are removed.
- Net reduction of at least 600 lines of duplicated schema/mapping dictionary definitions across `input_schemas/` and `config/`.
- `poetry run ruff check .`, `poetry run black --check .`, and `poetry run isort --check .` pass with zero errors.

## Implementation Directives

- Execute and verify each PR phase independently; run `poetry run pytest tests/unit`, `poetry run ruff check .`, `poetry run black --check .`, and `poetry run isort --check .` after every task group.
