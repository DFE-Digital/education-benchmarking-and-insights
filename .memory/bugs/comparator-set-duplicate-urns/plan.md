---
title: "Duplicate School URNs in Data Pipeline Comparator Sets"
component: "data-pipeline/comparator_sets"
domain: "benchmarking"
status: "fixed"
tags: ["bug", "data-pipeline", "comparator-sets", "deduplication"]
created: 2026-11-26
updated: 2026-11-26
---

## Bug Summary

In `ComparatorCalculator._select_top_urns` (`data-pipeline/src/pipeline/comparator_sets/calculations.py`), the pipeline could add the same school URN two or three times to a Pupil or Building comparator set when the target school is a PFI school or a boarding school.

## Reproduction

1. **Overlapping priority criteria**: With 26 valid schools where the target school has `Is PFI = True`, `Boarders (name) = "Boarding"`, and `GOR (name) = "Region A"`, `_select_top_urns` returned 30 entries containing only 16 unique URNs.
2. **Incomplete fallback exclusion**: With 40 valid schools where `URN0` (`Region A`) and `URN1` (`Region B`) have `Is PFI = True`, `_select_top_urns` returned `['URN0', 'URN1', 'URN1', 'URN2', ...]`, re-adding `URN1` during the distance top-up step.

## Root Cause Analysis (5 Whys)

1. **Why did comparator sets contain duplicate school URNs?** `_select_top_urns` appended URNs across PFI, Boarding, Region, and fallback passes without checking prior selections.
2. **Why did priority passes select the same school multiple times?** Each pass filtered `sorted_indices` independently instead of excluding candidates selected by earlier passes.
3. **Why did the fallback pass re-add PFI and Boarding peers?** `np.delete(urns_by_distance, same_region_indices)` excluded only same-region candidates and omitted PFI and Boarding matches from other regions.
4. **Why did unit tests miss the defect?** Existing tests asserted `len(result) == FINAL_SET_SIZE` (30) on a 26-school fixture without asserting URN uniqueness.
5. **Why did persistence not reject duplicates?** `insert_comparator_set` serialises NumPy arrays directly to JSON without uniqueness checks.

## Horizontal Impact Analysis

* **RAG calculations (`rag/calculations.py`)**: `processed_data.index.isin(set_urns)` collapsed duplicates, leaving fewer than 30 unique peers and shifting RAG medians and deciles.
* **SQL average views (`Core.Database/Views/`)**: `CROSS APPLY Openjson(...)` without `DISTINCT` weighted duplicated schools multiple times in historical comparator-set averages.
* **Platform search (`ComparatorsService.cs`)**: Not affected (single-pass Azure AI Search scoring).

## Resolution

1. Added regression and unit tests in `data-pipeline/tests/unit/comparator_sets/test_calculations.py` (`# regression-test: bug comparator-set-duplicate-urns`).
2. Deduplicated input index URNs in `ComparatorCalculator.__init__`, tracked selected candidates with `selected_mask` in `_select_top_urns`, replaced `group.iterrows()` with `_build_sets_for_phase`, and scoped `calculate_comparator_sets(target_urn=...)` to the target school's phase and row.
