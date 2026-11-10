---
title: "Code Review Report - Data Pipeline Architecture and Implementation Patterns"
date: 2026-11-04
severity: HIGH
confidence: HIGH
tags: [code, review, engineering]
---

# Code Review Report

## Executive Summary
The `data-pipeline` module contains five pattern defects across queue failure handling, comparator set calculation, database and logger lifecycle management, duplicate cost evaluation logic, and year-keyed ancillary schema fallbacks. Note: Both `MEDIUM` findings from the previous audit (`2026-10-27`) in `data-pipeline/src/pipeline/pre_processing/ancillary/ks4.py` remain unfixed and are consolidated into Finding 5.

## Findings

### [HIGH] Failed pipeline runs delete queue messages, swallow comparator errors, and leak database staging tables
**File**: `data-pipeline/src/pipeline/main.py` (Lines 135-146), `data-pipeline/src/pipeline/comparator_sets/orchestrator.py` (Lines 95-97), `data-pipeline/src/pipeline/utils/database.py` (Lines 187-229)

**Issue**:
1. In `main.py:141-143`, the `finally` block deletes the message from `worker_queue` even when an unhandled `Exception` sets `msg_payload["success"] = False`. This prevents Azure Queue retry delivery and makes the dead-letter check in `_check_msg_dequeue` (`main.py:150-185`) unreachable after a processing failure.
2. In `comparator_sets/orchestrator.py:95-97`, the `except Exception` block logs and continues when a `school_type` fails, allowing the pipeline to proceed to the RAG stage with missing comparator sets.
3. In `utils/database.py:187-229` (`_write_data`) and `267-273` (`upsert`), `_get_temp_table` creates a persistent physical table (`_{table}_{run_id}_{epoch}_temp`) outside a `try...finally` block. If `df.to_sql` or the `INSERT`/`MERGE` query fails, the staging table is never dropped.

**Recommendation**:
Only delete the message from `worker_queue` when processing succeeds, re-raise exceptions in `comparator_sets/orchestrator.py`, and drop physical staging tables in a `finally` block.
```python
    except Exception as error:
        logger.exception(
            f"An exception occurred: {type(error).__name__}", exc_info=error
        )
        msg_payload["success"] = False
        msg_payload["error"] = str(error)
    finally:
        if msg_payload.get("success"):
            with suppress(ResourceNotFoundError):
                worker_queue.delete_message(msg)

        complete_queue.send_message(json.dumps(msg_payload), time_to_live=300)
```
```python
        except Exception as e:
            logger.error(f"Failed to process {school_type}. Error: {e}", exc_info=True)
            raise
```
```python
    temp_table = _get_temp_table(
        table=table,
        run_id=run_id,
        engine=engine,
    )
    try:
        logger.debug(f"Writing to temp. table: {temp_table}.")
        start = time.time()
        df.to_sql(
            name=temp_table.name,
            con=engine,
            if_exists="append",
            index=df.index.name is not None,
            chunksize=int(os.getenv("SQL_CHUNKSIZE", 10_000)),
        )
        logger.debug(
            f"Wrote {len(df.index):,} rows to {temp_table} in {int(time.time() - start):,} seconds."
        )

        sql = textwrap.dedent(f"""
        BEGIN TRANSACTION;

        DELETE
          FROM {table}
         WHERE RunId = :run_id
        ;

        INSERT INTO {table} (
            {'\n      , '.join(temp_table.columns)}
        )
        SELECT {'\n         , '.join(temp_table.columns)}
          FROM {temp_table}
        ;

        COMMIT;
        """).strip()

        logger.debug(f"Writing to {table} ({run_id}).")
        start = time.time()
        with engine.begin() as cnx:
            cnx.execute(sqlalchemy.text(sql), parameters={"run_id": run_id})
    finally:
        with engine.begin() as cnx:
            cnx.execute(sqlalchemy.text(f"DROP TABLE IF EXISTS {temp_table};"))
```

### [HIGH] Comparator set selection appends duplicate URNs and iterates over DataFrame rows with `iterrows()`
**File**: `data-pipeline/src/pipeline/comparator_sets/calculations.py` (Lines 145-169, 199-219)

**Issue**:
1. In `_select_top_urns` (`calculations.py:145-169`), schools that match more than one priority filter (`target_pfi`, `target_boarding == "Boarding"`, `same_region`) are appended to `urns` multiple times. At line 166, `np.delete(urns_by_distance, same_region_indices)` only removes `same_region_indices` and does not exclude PFI or Boarding schools, producing duplicate URNs in the comparator set.
2. In `_process_phase_group` (`calculations.py:199-219`), `group.iterrows()` boxes every row into a `pd.Series` inside the inner loop, violating the module standard against `iterrows()`.

**Recommendation**:
Deduplicate `urns` in selection order, filter `urns_by_distance` with `~np.isin(urns_by_distance, urns)`, and iterate over NumPy boolean arrays instead of `group.iterrows()`.
```python
        if target_pfi:
            top_pfi = pfi_without_target[sorted_indices]
            same_pfi = np.argwhere(top_pfi).flatten()
            urns = np.append(urns, urns_by_distance[same_pfi])

        if target_boarding == "Boarding":
            top_boarding = boarding_without_target[sorted_indices]
            same_boarding = np.argwhere(top_boarding == target_boarding).flatten()
            urns = np.append(urns, urns_by_distance[same_boarding])

        top_regions = regions_without_target[sorted_indices]
        same_region_indices = np.argwhere(top_regions == target_region).flatten()
        urns = np.append(urns, urns_by_distance[same_region_indices])

        _, unique_indices = np.unique(urns, return_index=True)
        urns = urns[np.sort(unique_indices)]

        if len(urns) >= FINAL_SET_SIZE:
            return urns[:FINAL_SET_SIZE]

        fill_count = FINAL_SET_SIZE - len(urns)
        remaining_urns = urns_by_distance[~np.isin(urns_by_distance, urns)]
        urns_to_add = remaining_urns[:fill_count]
        urns = np.append(urns, urns_to_add)

        return urns
```
```python
        generate_pupil_mask = group["_GeneratePupilSet"].to_numpy()
        generate_building_mask = group["_GenerateBuildingSet"].to_numpy()

        for i in range(len(group)):
            if generate_pupil_mask[i]:
                pupil_urns = self._select_top_urns(
                    i, base_arrays, pupil_distances[i], pupil_include_mask
                )
                pupil_sets.append(pupil_urns if len(pupil_urns) > 1 else np.array([]))
            else:
                pupil_sets.append(np.array([]))

            if generate_building_mask[i]:
                building_urns = self._select_top_urns(
                    i, base_arrays, building_distances[i], building_include_mask
                )
                building_sets.append(
                    building_urns if len(building_urns) > 1 else np.array([])
                )
            else:
                building_sets.append(np.array([]))
```

### [MEDIUM] Uncached SQLAlchemy `Engine` creation and unguarded `StreamHandler` registration leak pools and duplicate logs
**File**: `data-pipeline/src/pipeline/utils/database.py` (Lines 19-53), `data-pipeline/src/pipeline/utils/log.py` (Lines 7-16)

**Issue**:
1. In `utils/database.py:19-53`, `get_engine()` creates a new SQLAlchemy `Engine` and connection pool on every call without caching or disposal (up to 11 times per default run), whereas `get_blob_service_client()` in `utils/storage.py:28` uses `@lru_cache(maxsize=1)`.
2. In `utils/log.py:7-16`, `setup_logger(name)` unconditionally adds a new `logging.StreamHandler()` to `logging.getLogger(name)`, causing duplicate JSON log output whenever `setup_logger` is called more than once for the same logger name.

**Recommendation**:
Cache `get_engine()` with `@lru_cache(maxsize=1)` and guard handler attachment in `setup_logger(name)`.
```python
from functools import lru_cache


@lru_cache(maxsize=1)
def get_engine() -> sqlalchemy.engine.Engine:
```
```python
def setup_logger(name: str):
    logger = logging.getLogger(name)
    logger.setLevel(os.getenv("LOG_LEVEL", logging.INFO))

    if not logger.handlers:
        handler = logging.StreamHandler()
        formatter = JsonFormatter(fmt="%(asctime)s %(levelname)s %(name)s %(message)s")
        handler.setFormatter(formatter)
        logger.addHandler(handler)
        logger.propagate = False

    return logger
```

### [MEDIUM] Duplicate category expression evaluation and stale custom net catering cost calculation
**File**: `data-pipeline/src/pipeline/pre_processing/cfr/maintained_schools.py` (Lines 74-90), `data-pipeline/src/pipeline/pre_processing/ancillary/custom_data.py` (Lines 145-160)

**Issue**:
1. In `maintained_schools.py:74-90`, `maintained_schools_column_eval` lookup and `eval_cost_income_categories(maintained_schools, maintained_schools_column_eval)` are executed twice in consecutive identical blocks.
2. In `custom_data.py:145-160`, `_post_process_custom` saves the pre-custom `Catering staff and supplies_Net Costs`, zeroes it out during `map_cost_series`, and restores the stale pre-custom value at line 159 instead of recalculating `Catering staff and supplies_Total - Income_Catering services`.

**Recommendation**:
Remove the duplicate block in `maintained_schools.py:83-90` and recalculate `Catering staff and supplies_Net Costs` using `cfr_calculations.calc_catering_net_costs` in `custom_data.py`.
```python
    maintained_schools_column_eval = (
        input_schemas.maintained_schools_master_list_column_eval.get(
            year, input_schemas.maintained_schools_master_list_column_eval["default"]
        )
    )
    maintained_schools = eval_cost_income_categories(
        maintained_schools, maintained_schools_column_eval
    )

    maintained_schools = join_federations(maintained_schools, year)
```
```python
    target_data["Catering staff and supplies_Net Costs"] = 0.0

    for category, settings in config.rag_category_settings.items():
        basis_data = target_data[
            (
                "Number of pupils"
                if settings["type"] == "Pupil"
                else "Total Internal Floor Area"
            )
        ]
        target_data = mappings.map_cost_series(category, target_data, basis_data)

    target_data = cfr_calculations.calc_catering_net_costs(target_data)

    return target_data
```

### [MEDIUM] Missing columns in `ks4["default"]` schema fallback and premature `.fillna(0)` on national Attainment 8
**File**: `data-pipeline/src/pipeline/pre_processing/ancillary/ks4.py` (Lines 59-89)

**Issue**:
1. In `ks4.py:59`, `input_schemas.ks4.get(year, input_schemas.ks4["default"])` falls back to `input_schemas.ks4["default"]`, which omits `"RECTYPE"` and `"LEA"`. For any year not explicitly listed in `input_schemas.ks4`, line 79 raises `KeyError: 'RECTYPE'`.
2. In `ks4.py:74-76`, `.fillna(0)` is applied to `ks4["AverageAttainment"]` before extracting `national_rows` at line 86, converting a suppressed or missing national Attainment 8 score into `0.0` instead of `np.nan`.

**Recommendation**:
Use `2024` as the fallback schema key in `preprocess_la_ks4` and apply `.fillna(0)` only to the Local Authority subset after extracting `national_avg_attainment`.
```python
    schema = input_schemas.ks4.get(year, input_schemas.ks4[2024])
    column_mappings = input_schemas.ks4_column_mappings.get(
        year, input_schemas.ks4_column_mappings["default"]
    )

    ks4 = pd.read_excel(
        ks4_path,
        dtype=schema,
        usecols=schema.keys(),
        na_values=["NP", "NE", "SUPP", "LOWCOV"],
        engine="calamine",
    )
    logger.info(f"LA KS4 Data raw shape: {ks4.shape}")

    ks4.rename(columns=column_mappings, inplace=True)
    ks4["AverageAttainment"] = pd.to_numeric(
        ks4["AverageAttainment"], errors="coerce"
    )

    national_rows = ks4[ks4["RECTYPE"] == 5]["AverageAttainment"].dropna()
    national_avg_attainment = (
        float(national_rows.iloc[0]) if not national_rows.empty else np.nan
    )

    la_avg_attainment = (
        ks4[ks4["RECTYPE"] == 4][["LEA", "AverageAttainment"]]
        .dropna(subset=["LEA"])
        .drop_duplicates(subset=["LEA"])
        .assign(AverageAttainment=lambda df: df["AverageAttainment"].fillna(0))
        .rename(columns={"AverageAttainment": "LocalAuthorityAvgAttainment8"})
        .set_index("LEA")
    )

    return la_avg_attainment, national_avg_attainment
```
