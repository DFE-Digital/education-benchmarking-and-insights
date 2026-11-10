# Implementation Tasks: Pivot SEN2 Caseload Data (`FEAT-002`)

- [x] **Task 1: Configure SEN2 Caseload Schemas and Filenames (`REQ-001`, `REQ-004`, `REQ-005`, `REQ-006`)**
  - Add `la_sen2_filenames`, `la_sen2_caseload_drop_columns`, `la_sen2_caseload_time_period`, `la_sen2_caseload_na_columns`, `la_sen2_caseload_column_mappings` (using `evolve_schema` for 2025 and 2026), `la_sen2_caseload_id_vars`, `la_sen2_caseload_group_rules`, and `la_sen2_caseload_final_order` to `data-pipeline/src/pipeline/input_schemas/local_authority.py`.
  - Add the `2026` entry to `la_sen2_eval` in `data-pipeline/src/pipeline/input_schemas/local_authority.py`.
  - Export the new SEN2 configuration objects in `data-pipeline/src/pipeline/input_schemas/__init__.py`.
- [x] **Task 2: Integrate Caseload Pivot and Year-Aware File Selection (`REQ-001`, `REQ-002`, `REQ-003`, `REQ-005`, `REQ-006`)**
  - Update `pre_process_sen2` in `data-pipeline/src/pipeline/pre_processing/ancillary/main.py` to select the SEN2 filename from `la_sen2_filenames`.
  - Implement `pivot_sen2_caseload_data` and integrate it into `prepare_sen2_data` in `data-pipeline/src/pipeline/pre_processing/ancillary/sen2.py` so 2025 and 2026 runs transform `caseload.csv` in memory (including summing duplicate renamed columns such as 2026 `special_academy` and `special_free_school` into `"Special - Academy/free"`) before executing the existing SEN2 pivot and evaluation logic.
- [x] **Task 3: Implement Unit Tests (`REQ-001` to `REQ-006`)**
  - Add unit tests in `data-pipeline/tests/unit/pre_processing/local_authorities/test_la_sen2.py` and fixtures in `data-pipeline/tests/unit/pre_processing/local_authorities/conftest.py` covering 2025 `caseload.csv` processing, 2026 `caseload.csv` processing (with split `special_academy` and `special_free_school` columns summed into `"Special - Academy/free"`), missing required column error handling (`KeyError`), suppressed/missing values (`"x"`), year-aware filename selection in `pre_process_sen2`, and backward compatibility for pre-2025 `sen2_estab_caseload.csv` runs.
- [x] **Task 4: Update Documentation (`REQ-007`, `REQ-008`)**
  - Update `docs/content/reference/sources.md`, `docs/content/how-to/data-release-guide/s251.md`, `docs/content/how-to/data-release-guide/cfr.md`, and `docs/content/how-to/data-release-guide/aar.md` to signpost that `caseload.csv` is used from 2025 onwards and is pivoted automatically by the pipeline.
- [x] **Task 5: Verify Tests and Update Assumptions**
  - Run the unit test suite (`pytest`) and verify all tests pass.
  - Update `ASSUME-004` status and mark all tasks complete in `tasks.md`.
