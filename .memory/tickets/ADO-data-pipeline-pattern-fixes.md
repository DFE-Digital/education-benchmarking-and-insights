# Azure DevOps Tickets: Data Pipeline Code Review Findings (1 to 4)

## Ticket 1: Fix Queue Message Deletion on Failure, Comparator Error Swallowing, and Leaked Staging Tables

* **Title**: Fix data-pipeline queue retry deletion, swallowed comparator errors, and leaked staging tables
* **Work Item Type**: Bug
* **Area Path**: Data Pipeline

### Description

#### Context
A code review of `data-pipeline` ([code-review-report.md](file:///Users/murdomoyse/Documents/repos/education-benchmarking-and-insights/.memory/reports/code-review-report.md)) identified three failure-handling defects across the queue worker, comparator sets orchestrator, and SQL Server persistence helpers:
1. In [`handle_msg`](file:///Users/murdomoyse/Documents/repos/education-benchmarking-and-insights/data-pipeline/src/pipeline/main.py#L135-L146), the `finally` block deletes the message from `worker_queue` even when an unhandled `Exception` sets `msg_payload["success"] = False`. Because the message is deleted on its first failure, its `dequeue_count` never increments and the dead-letter retry check in [`_check_msg_dequeue`](file:///Users/murdomoyse/Documents/repos/education-benchmarking-and-insights/data-pipeline/src/pipeline/main.py#L150-L185) is never triggered after a processing error.
2. In [`run_default_comparator_sets_pipeline`](file:///Users/murdomoyse/Documents/repos/education-benchmarking-and-insights/data-pipeline/src/pipeline/comparator_sets/orchestrator.py#L95-L97), exceptions during comparator set calculation for a `school_type` are logged and ignored with `continue`, allowing the pipeline to proceed to the RAG stage with missing comparator sets.
3. In [`_write_data`](file:///Users/murdomoyse/Documents/repos/education-benchmarking-and-insights/data-pipeline/src/pipeline/utils/database.py#L187-L229) and [`upsert`](file:///Users/murdomoyse/Documents/repos/education-benchmarking-and-insights/data-pipeline/src/pipeline/utils/database.py#L267-L274), physical staging tables (`_{table}_{run_id}_{epoch}_temp`) are created outside a `try...finally` block. If `df.to_sql` or the `INSERT`/`MERGE` statement fails, the staging table remains in the database.

#### Requirements (EARS)
* **R1.1 (Unwanted behaviour)**: If an unhandled exception occurs in [`handle_msg`](file:///Users/murdomoyse/Documents/repos/education-benchmarking-and-insights/data-pipeline/src/pipeline/main.py#L58-L147), then the system shall retain the message in `worker_queue` so that [`_check_msg_dequeue`](file:///Users/murdomoyse/Documents/repos/education-benchmarking-and-insights/data-pipeline/src/pipeline/main.py#L150-L185) can enforce `dequeue_limit`.
* **R1.2 (Unwanted behaviour)**: If comparator set generation fails for a `school_type` in [`run_default_comparator_sets_pipeline`](file:///Users/murdomoyse/Documents/repos/education-benchmarking-and-insights/data-pipeline/src/pipeline/comparator_sets/orchestrator.py#L50-L116), then the system shall log the error and re-raise the exception.
* **R1.3 (Unwanted behaviour)**: If `df.to_sql` or the target table transaction fails in [`_write_data`](file:///Users/murdomoyse/Documents/repos/education-benchmarking-and-insights/data-pipeline/src/pipeline/utils/database.py#L161-L233) or [`upsert`](file:///Users/murdomoyse/Documents/repos/education-benchmarking-and-insights/data-pipeline/src/pipeline/utils/database.py#L235-L274), then the system shall execute `DROP TABLE IF EXISTS` for the temporary staging table inside a `finally` block.

#### Technical Notes
* Guard `worker_queue.delete_message(msg)` with `if msg_payload.get("success"):` in [data-pipeline/src/pipeline/main.py](file:///Users/murdomoyse/Documents/repos/education-benchmarking-and-insights/data-pipeline/src/pipeline/main.py#L141-L144).
* Replace `continue` with `raise` in [data-pipeline/src/pipeline/comparator_sets/orchestrator.py](file:///Users/murdomoyse/Documents/repos/education-benchmarking-and-insights/data-pipeline/src/pipeline/comparator_sets/orchestrator.py#L95-L97) to match [data-pipeline/src/pipeline/rag/orchestrator.py](file:///Users/murdomoyse/Documents/repos/education-benchmarking-and-insights/data-pipeline/src/pipeline/rag/orchestrator.py#L68-L70).
* Wrap staging table population and transaction execution in `try...finally` in [data-pipeline/src/pipeline/utils/database.py](file:///Users/murdomoyse/Documents/repos/education-benchmarking-and-insights/data-pipeline/src/pipeline/utils/database.py#L187-L274).
* Update unit tests in [data-pipeline/tests/unit/test_main.py](file:///Users/murdomoyse/Documents/repos/education-benchmarking-and-insights/data-pipeline/tests/unit/test_main.py) and [data-pipeline/tests/unit/utils/test_database.py](file:///Users/murdomoyse/Documents/repos/education-benchmarking-and-insights/data-pipeline/tests/unit/utils/test_database.py).

### Acceptance Criteria
- [ ] **AC1.1**: Given a valid message in `worker_queue`, when an unhandled exception is raised during [`handle_msg`](file:///Users/murdomoyse/Documents/repos/education-benchmarking-and-insights/data-pipeline/src/pipeline/main.py#L58-L147), then `msg_payload["success"]` is `False`, `complete_queue.send_message` is called with the failure payload, and `worker_queue.delete_message` is not called.
- [ ] **AC1.2**: Given a valid message in `worker_queue`, when [`handle_msg`](file:///Users/murdomoyse/Documents/repos/education-benchmarking-and-insights/data-pipeline/src/pipeline/main.py#L58-L147) completes normally or raises [`PipelineEarlyExit`](file:///Users/murdomoyse/Documents/repos/education-benchmarking-and-insights/data-pipeline/src/pipeline/utils/stats.py#L8-L12), then `msg_payload["success"]` is `True` and `worker_queue.delete_message` is called once.
- [ ] **AC1.3**: Given a `Default` comparator sets run, when processing `academies` or `maintained_schools` raises an exception in [`run_default_comparator_sets_pipeline`](file:///Users/murdomoyse/Documents/repos/education-benchmarking-and-insights/data-pipeline/src/pipeline/comparator_sets/orchestrator.py#L50-L116), then the error is logged and the exception is re-raised.
- [ ] **AC1.4**: Given a call to [`_write_data`](file:///Users/murdomoyse/Documents/repos/education-benchmarking-and-insights/data-pipeline/src/pipeline/utils/database.py#L161-L233) or [`upsert`](file:///Users/murdomoyse/Documents/repos/education-benchmarking-and-insights/data-pipeline/src/pipeline/utils/database.py#L235-L274), when `df.to_sql` or the `DELETE`/`INSERT`/`MERGE` query raises an exception, then `DROP TABLE IF EXISTS` is executed for the staging table before the exception propagates.

## Ticket 2: Deduplicate Comparator Set URN Selection and Remove `iterrows()`

* **Title**: Fix duplicate URN selection and remove iterrows in ComparatorCalculator
* **Work Item Type**: Bug
* **Area Path**: Data Pipeline

### Description

#### Context
In [`ComparatorCalculator._select_top_urns`](file:///Users/murdomoyse/Documents/repos/education-benchmarking-and-insights/data-pipeline/src/pipeline/comparator_sets/calculations.py#L145-L169), a candidate school that matches more than one priority filter (`target_pfi`, `target_boarding == "Boarding"`, `same_region`) is appended to `urns` multiple times. In addition, line 166 (`np.delete(urns_by_distance, same_region_indices)`) only excludes `same_region_indices` and does not exclude PFI or Boarding schools when filling the remaining slots up to `FINAL_SET_SIZE` (`30`), resulting in duplicate URNs in the comparator set. Furthermore, [`ComparatorCalculator._process_phase_group`](file:///Users/murdomoyse/Documents/repos/education-benchmarking-and-insights/data-pipeline/src/pipeline/comparator_sets/calculations.py#L199-L219) iterates over `group.iterrows()`, which constructs a `pd.Series` per school and violates the Pandas vectorisation standard in [data-pipeline/README.md](file:///Users/murdomoyse/Documents/repos/education-benchmarking-and-insights/data-pipeline/README.md#L48).

#### Requirements (EARS)
* **R2.1 (Event-driven)**: When [`ComparatorCalculator._select_top_urns`](file:///Users/murdomoyse/Documents/repos/education-benchmarking-and-insights/data-pipeline/src/pipeline/comparator_sets/calculations.py#L112-L169) selects comparator schools, the system shall return unique URNs ordered by selection priority (`target_urn`, `PFI`, `Boarding`, `Region`, and remaining closest schools by distance up to `FINAL_SET_SIZE = 30`).
* **R2.2 (Ubiquitous)**: The system shall iterate over NumPy boolean arrays (`_GeneratePupilSet` and `_GenerateBuildingSet`) instead of calling `group.iterrows()` in [`ComparatorCalculator._process_phase_group`](file:///Users/murdomoyse/Documents/repos/education-benchmarking-and-insights/data-pipeline/src/pipeline/comparator_sets/calculations.py#L171-L222).

#### Technical Notes
* In [data-pipeline/src/pipeline/comparator_sets/calculations.py](file:///Users/murdomoyse/Documents/repos/education-benchmarking-and-insights/data-pipeline/src/pipeline/comparator_sets/calculations.py#L145-L169), deduplicate `urns` while preserving first-seen order (`_, unique_indices = np.unique(urns, return_index=True); urns = urns[np.sort(unique_indices)]`) and compute remaining candidates with `urns_by_distance[~np.isin(urns_by_distance, urns)]`.
* In [data-pipeline/src/pipeline/comparator_sets/calculations.py](file:///Users/murdomoyse/Documents/repos/education-benchmarking-and-insights/data-pipeline/src/pipeline/comparator_sets/calculations.py#L199-L219), extract `generate_pupil_mask = group["_GeneratePupilSet"].to_numpy()` and `generate_building_mask = group["_GenerateBuildingSet"].to_numpy()` before the loop.
* Add regression tests in [data-pipeline/tests/unit/comparator_sets/test_calculations.py](file:///Users/murdomoyse/Documents/repos/education-benchmarking-and-insights/data-pipeline/tests/unit/comparator_sets/test_calculations.py).

### Acceptance Criteria
- [ ] **AC2.1**: Given a target school with `Is PFI = True` and `Boarders = "Boarding"`, when candidate schools in `urns_by_distance` match multiple priority rules (`PFI`, `Boarding`, `Region`), then [`_select_top_urns`](file:///Users/murdomoyse/Documents/repos/education-benchmarking-and-insights/data-pipeline/src/pipeline/comparator_sets/calculations.py#L112-L169) returns at most 30 URNs with no duplicate URN values (`len(urns) == len(np.unique(urns))`).
- [ ] **AC2.2**: Given a target school whose priority matches (`target_urn`, `PFI`, `Boarding`, `Region`) contain fewer than 30 unique schools, when [`_select_top_urns`](file:///Users/murdomoyse/Documents/repos/education-benchmarking-and-insights/data-pipeline/src/pipeline/comparator_sets/calculations.py#L112-L169) fills the remaining slots from `urns_by_distance`, then no school already selected via `PFI`, `Boarding`, or `Region` is added a second time.
- [ ] **AC2.3**: Given a phase group DataFrame, when [`_process_phase_group`](file:///Users/murdomoyse/Documents/repos/education-benchmarking-and-insights/data-pipeline/src/pipeline/comparator_sets/calculations.py#L171-L222) computes `Pupil` and `Building` comparator sets, then `DataFrame.iterrows()` is not called and all existing comparator calculation unit tests pass.

## Ticket 3: Cache SQLAlchemy Engine Creation and Guard Logger StreamHandler Registration

* **Title**: Cache SQLAlchemy Engine in get_engine and deduplicate handlers in setup_logger
* **Work Item Type**: Task
* **Area Path**: Data Pipeline

### Description

#### Context
Two resource initialisation helpers in `data-pipeline/src/pipeline/utils/` lack idempotency guards:
1. In [`get_engine`](file:///Users/murdomoyse/Documents/repos/education-benchmarking-and-insights/data-pipeline/src/pipeline/utils/database.py#L19-L53), a new SQLAlchemy `Engine` and connection pool are created on every call (up to 11 calls per default pipeline run) without caching or disposal, unlike [`get_blob_service_client`](file:///Users/murdomoyse/Documents/repos/education-benchmarking-and-insights/data-pipeline/src/pipeline/utils/storage.py#L28-L41) which uses `@lru_cache(maxsize=1)`.
2. In [`setup_logger`](file:///Users/murdomoyse/Documents/repos/education-benchmarking-and-insights/data-pipeline/src/pipeline/utils/log.py#L7-L16), a new `logging.StreamHandler()` is added to `logging.getLogger(name)` on every invocation, producing duplicate JSON log lines when `setup_logger` is called more than once for the same logger name.

#### Requirements (EARS)
* **R3.1 (Ubiquitous)**: The system shall cache the SQLAlchemy `Engine` instance returned by [`get_engine`](file:///Users/murdomoyse/Documents/repos/education-benchmarking-and-insights/data-pipeline/src/pipeline/utils/database.py#L19-L53) using `@lru_cache(maxsize=1)`.
* **R3.2 (Ubiquitous)**: The system shall attach a `logging.StreamHandler` in [`setup_logger`](file:///Users/murdomoyse/Documents/repos/education-benchmarking-and-insights/data-pipeline/src/pipeline/utils/log.py#L7-L16) only when `logger.handlers` is empty, and shall set `logger.propagate = False`.

#### Technical Notes
* Decorate [`get_engine`](file:///Users/murdomoyse/Documents/repos/education-benchmarking-and-insights/data-pipeline/src/pipeline/utils/database.py#L19-L53) with `@lru_cache(maxsize=1)` from `functools`.
* Update [`setup_logger`](file:///Users/murdomoyse/Documents/repos/education-benchmarking-and-insights/data-pipeline/src/pipeline/utils/log.py#L7-L16) to check `if not logger.handlers:` before adding the `StreamHandler`.
* Add unit tests in [data-pipeline/tests/unit/utils/test_database.py](file:///Users/murdomoyse/Documents/repos/education-benchmarking-and-insights/data-pipeline/tests/unit/utils/test_database.py).

### Acceptance Criteria
- [ ] **AC3.1**: Given multiple calls to [`get_engine()`](file:///Users/murdomoyse/Documents/repos/education-benchmarking-and-insights/data-pipeline/src/pipeline/utils/database.py#L19-L53) within the same process, when the returned objects are compared, then `get_engine()` returns the same cached `Engine` instance and `sqlalchemy.create_engine` is invoked once.
- [ ] **AC3.2**: Given multiple calls to [`setup_logger("pipeline.test")`](file:///Users/murdomoyse/Documents/repos/education-benchmarking-and-insights/data-pipeline/src/pipeline/utils/log.py#L7-L16) with the same logger name, when `logger.handlers` is inspected, then it contains exactly one `StreamHandler` and `logger.propagate` is `False`.

## Ticket 4: Remove Duplicate CFR Expression Evaluation and Fix Custom Net Catering Cost Recalculation

* **Title**: Remove duplicate CFR category evaluation and recalculate custom net catering costs
* **Work Item Type**: Bug
* **Area Path**: Data Pipeline

### Description

#### Context
In [`build_maintained_school_data`](file:///Users/murdomoyse/Documents/repos/education-benchmarking-and-insights/data-pipeline/src/pipeline/pre_processing/cfr/maintained_schools.py#L74-L90), `maintained_schools_column_eval` lookup and [`eval_cost_income_categories`](file:///Users/murdomoyse/Documents/repos/education-benchmarking-and-insights/data-pipeline/src/pipeline/pre_processing/cfr/calculations.py#L46-L61) are executed twice in consecutive identical blocks (lines 74-81 and lines 83-90). In addition, [`_post_process_custom`](file:///Users/murdomoyse/Documents/repos/education-benchmarking-and-insights/data-pipeline/src/pipeline/pre_processing/ancillary/custom_data.py#L145-L160) copies the pre-custom `Catering staff and supplies_Net Costs` column, sets it to `0.0` while recalculating category totals and per-unit costs via [`map_cost_series`](file:///Users/murdomoyse/Documents/repos/education-benchmarking-and-insights/data-pipeline/src/pipeline/pre_processing/common/mappings.py#L197-L215), and then overwrites the column with the stale pre-custom value at line 159 instead of recalculating `Catering staff and supplies_Total - Income_Catering services`.

#### Requirements (EARS)
* **R4.1 (Event-driven)**: When [`build_maintained_school_data`](file:///Users/murdomoyse/Documents/repos/education-benchmarking-and-insights/data-pipeline/src/pipeline/pre_processing/cfr/maintained_schools.py#L29-L113) processes CFR school data, the system shall execute [`eval_cost_income_categories`](file:///Users/murdomoyse/Documents/repos/education-benchmarking-and-insights/data-pipeline/src/pipeline/pre_processing/cfr/calculations.py#L46-L61) once.
* **R4.2 (Event-driven)**: When [`_post_process_custom`](file:///Users/murdomoyse/Documents/repos/education-benchmarking-and-insights/data-pipeline/src/pipeline/pre_processing/ancillary/custom_data.py#L120-L161) recalculates custom school data, the system shall recalculate `Catering staff and supplies_Net Costs` using [`calc_catering_net_costs`](file:///Users/murdomoyse/Documents/repos/education-benchmarking-and-insights/data-pipeline/src/pipeline/pre_processing/cfr/calculations.py#L64-L72).

#### Technical Notes
* Remove the duplicate block at lines 83-90 of [data-pipeline/src/pipeline/pre_processing/cfr/maintained_schools.py](file:///Users/murdomoyse/Documents/repos/education-benchmarking-and-insights/data-pipeline/src/pipeline/pre_processing/cfr/maintained_schools.py#L83-L90).
* In [data-pipeline/src/pipeline/pre_processing/ancillary/custom_data.py](file:///Users/murdomoyse/Documents/repos/education-benchmarking-and-insights/data-pipeline/src/pipeline/pre_processing/ancillary/custom_data.py#L145-L160), replace the stale `catering_net_costs` restoration at line 159 with `target_data = cfr_calculations.calc_catering_net_costs(target_data)`.
* Update unit tests in [data-pipeline/tests/unit/pre_processing/test_custom_data.py](file:///Users/murdomoyse/Documents/repos/education-benchmarking-and-insights/data-pipeline/tests/unit/pre_processing/test_custom_data.py).

### Acceptance Criteria
- [ ] **AC4.1**: Given maintained school input data for any supported CFR year, when [`build_maintained_school_data`](file:///Users/murdomoyse/Documents/repos/education-benchmarking-and-insights/data-pipeline/src/pipeline/pre_processing/cfr/maintained_schools.py#L29-L113) runs, then [`eval_cost_income_categories`](file:///Users/murdomoyse/Documents/repos/education-benchmarking-and-insights/data-pipeline/src/pipeline/pre_processing/cfr/calculations.py#L46-L61) is called once.
- [ ] **AC4.2**: Given custom data that changes `Catering staff and supplies_Catering staff`, `Catering staff and supplies_Catering supplies`, or `Income_Catering services`, when [`merge_custom_data`](file:///Users/murdomoyse/Documents/repos/education-benchmarking-and-insights/data-pipeline/src/pipeline/pre_processing/ancillary/custom_data.py#L14-L116) and [`_post_process_custom`](file:///Users/murdomoyse/Documents/repos/education-benchmarking-and-insights/data-pipeline/src/pipeline/pre_processing/ancillary/custom_data.py#L120-L161) execute, then `Catering staff and supplies_Net Costs` equals the recalculated `Catering staff and supplies_Total - Income_Catering services`.
