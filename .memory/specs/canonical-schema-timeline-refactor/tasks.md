## 1. PR 1: Core Timeline Primitives, Step-Function Lookup, and Quick-Win Bug Fixes (`data-pipeline`)

- [ ] 1.1 Implement `resolve_year_config`, `ColumnSpec`, and `DatasetTimeline` (built on `evolve_schema`) in `data-pipeline/src/pipeline/input_schemas/utils.py`, ensuring `resolve_year_config` resolves the largest integer key `<= year` (falling back to `"default"` or raising `KeyError` if neither exists).
- [ ] 1.2 Normalise inverted `"default"` schemas in `data-pipeline/src/pipeline/input_schemas/cfo.py` and `data-pipeline/src/pipeline/input_schemas/hospital_schools.py` so `"default"` represents the `2021` baseline schema and `2024` / `2025` step forward chronologically.
- [ ] 1.3 Replace `.get(year, ...)` schema lookups across `data-pipeline/src/pipeline/pre_processing/` with `resolve_year_config` and delete redundant unchanged year keys (`2025` in `aar_academies.py` and `aar_central_services.py`; `2026` in `census_pupils.py`, `census_workforce.py`, `gias.py`, `bfr.py`, and `maintained_schools_master_list.py`).
- [ ] 1.4 Remove the duplicate `eval_cost_income_categories` call in `data-pipeline/src/pipeline/pre_processing/cfr/maintained_schools.py` (lines 83 to 90) and deduplicate `nonaggregated_cost_category_map["academies"]` and `["central_services"]` in `data-pipeline/src/pipeline/config/__init__.py`.
- [ ] 1.5 Consult `test-cases.md` (`TC-01`, `TC-02`, `TC-06`, `TC-07`, `TC-08`) and use the `write-tests` skill to add unit tests in `data-pipeline/tests/unit/input_schemas/test_utils.py` and `data-pipeline/tests/unit/pre_processing/` verifying step-function year resolution, `DatasetTimeline` rewiring/backfill, and normalised `cfo` / `hospital_schools` ingestion across `2021` to `2026`.

## 2. PR 2: Centralised Database Projections and Declarative Default Padding (`data-pipeline`)

- [ ] 2.1 Create `data-pipeline/src/pipeline/config/projections.py` containing `apply_db_projection(df, projections, defaults=None)` and all SQL table projection dictionaries moved from `data-pipeline/src/pipeline/utils/database.py` (`schools_db_projections`, `la_ref_db_projections`, `trust_ref_db_projections`, `non_financial_db_projections`, `financial_db_projections`, `bfr_metrics_db_projections`, `bfr_db_projections`) alongside re-exports of `trust_db_projections` and `la_db_*_projections`.
- [ ] 2.2 Refactor `insert_schools_and_local_authorities`, `insert_trusts`, `insert_non_financial_data`, `insert_financial_data`, `insert_trust_financial_data`, `insert_bfr_metrics`, `insert_bfr`, `insert_la_financial`, `insert_la_non_financial`, and `insert_la_statistical_neighbours` in `data-pipeline/src/pipeline/utils/database.py` to use `apply_db_projection`.
- [ ] 2.3 Consult `test-cases.md` (`TC-05`) and use the `write-tests` skill to add unit tests in `data-pipeline/tests/unit/utils/test_database.py` verifying that `apply_db_projection` populates missing optional/historical columns with `np.nan` and restricts output columns strictly to the target SQL schema.

## 3. PR 3: Migrate Ancillary Tabular Datasets to `DatasetTimeline` (`data-pipeline`)

- [ ] 3.1 Migrate `ks4.py`, `census_pupils.py`, `census_workforce.py`, `gias.py`, `cfo.py`, `hospital_schools.py`, `high_exec_pay.py`, `ilr.py`, and `pru.py` in `data-pipeline/src/pipeline/input_schemas/` to use `DatasetTimeline` (or `evolve_schema` with declarative defaults), co-locating raw dtypes, canonical column renames, and missing-column defaults.
- [ ] 3.2 Update corresponding ancillary loaders in `data-pipeline/src/pipeline/pre_processing/ancillary/` to use the timeline normalisation helpers and remove `ensure_leadership_non_teacher_column_is_present` from `census.py` and `ensure_la_ks4_columns_are_present` from `s251/local_authority.py`.
- [ ] 3.3 Consult `test-cases.md` (`TC-02`, `TC-05`, `TC-08`) and use the `write-tests` skill to run and update ancillary unit tests in `data-pipeline/tests/unit/pre_processing/ancillary/` confirming identical canonical DataFrame outputs across `2021` to `2026`.

## 4. PR 4: Migrate Financial Datasets (`CFR`, `BFR`, `AAR`) and Collapse Multi-Hop AAR Renames (`data-pipeline`)

- [ ] 4.1 Update `maintained_schools_master_list.py` and `bfr.py` in `data-pipeline/src/pipeline/input_schemas/` to declare historical default backfill (`np.nan`) for 2024+ IT spend breakdown columns (`E20A` to `E20G`), and remove `ensure_it_spend_breakdown_columns_are_present` from `data-pipeline/src/pipeline/pre_processing/cfr/calculations.py`.
- [ ] 4.2 Refactor `aar_academies.py` and `aar_central_services.py` in `data-pipeline/src/pipeline/input_schemas/` and `academies.py` / `central_services.py` in `data-pipeline/src/pipeline/pre_processing/aar/` so both pre-2024 (`BNCH*`) and 2024+ (`BAE*`/`BAI*`) raw columns map directly to canonical category names at ingestion, eliminating the intermediate `BNCH*` rename hop.
- [ ] 4.3 Consult `test-cases.md` (`TC-03`, `TC-04`) and use the `write-tests` skill to add/update unit tests in `data-pipeline/tests/unit/pre_processing/cfr/` and `data-pipeline/tests/unit/pre_processing/aar/` verifying that pre-2024, 2024, and 2025+ runs produce identical canonical financial columns and totals.

## 5. PR 5: Refactor S251, SEN2, DSG, and High Needs Places Configs (`data-pipeline`)

- [ ] 5.1 Refactor `data-pipeline/src/pipeline/input_schemas/local_authority.py` to derive `2024` and `2025` dictionaries (`la_budget_column_mappings`, `la_outturn_column_mappings`, `la_budget_column_eval`, `la_outturn_column_eval`, `la_section_251_column_mappings`) from `"default"` using `evolve_schema`, removing ~500 lines of duplicated entries.
- [ ] 5.2 Refactor `high_needs_places.py` and `dsg.py` in `data-pipeline/src/pipeline/input_schemas/` and `pre_processing/s251/local_authority.py` to resolve year columns via `resolve_year_config` and remove `ensure_dsg_columns_are_present`.
- [ ] 5.3 Consult `test-cases.md` (`TC-09`) and use the `write-tests` skill to verify all S251 Local Authority and SEN2 unit tests in `data-pipeline/tests/unit/pre_processing/` across `2021` to `2026`, then run full `poetry run pytest`, `ruff check`, `black --check`, and `isort --check`.
