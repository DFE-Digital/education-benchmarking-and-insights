# Value Assessment: Pivot SEN2 Caseload Data

**Date**: 2026-11-05
**Status**: Final

## 1. Context

- **Feature / Initiative**: Pivot SEN2 Caseload Data (`FEAT-002`)
- **Associated Vision**: [Product Vision](../../product-vision.md)
- **Feature Specification**: [Feature Specification](feature.md)

## 2. Organisational Value Definition

- **Primary Value Driver(s)**: Single Source of Truth, Historical Continuity, Benchmarking Completeness, and Pupil Outcome Alignment ([KB-0001](../../knowledge/KB-0001-fbit-value-rule-set.md)).
- **Strategic Goals Alignment**: Automates the ingestion and normalisation of official Department for Education (DfE) SEN2 Education, Health and Care Plan (EHCP) caseload statistics for 2025 and 2026 Section 251 runs, enabling Local Authorities to benchmark High Needs expenditure against pupil SEN placement demand without manual data manipulation.

## 3. Value Rule Set

| Rule / Criterion | Description | Minimum Threshold for Go |
| :--- | :--- | :--- |
| **Pupil Outcome Alignment** | Enables correlation between financial expenditure and official pupil educational outcomes and SEN demand. | Directly exposes or persists an official DfE metric for school or Local Authority benchmarking. |
| **Benchmarking Completeness** | Provides necessary comparative context for valid Local Authority comparisons. | Supplies complete Local Authority EHCP placement breakdowns across all establishment categories. |
| **Historical Continuity** | Maintains operational compatibility with prior financial and academic year pipeline runs. | Zero regressions on pre-2025 pipeline runs when supporting 2025 and 2026 SEN2 caseload files. |
| **Single Source of Truth** | Derives metrics directly from authoritative DfE input datasets without manual data manipulation. | 100% automated ingestion and persistence within the standard pipeline workflow. |

## 4. Feature Assessment

| Criterion | Expected Impact | Confidence Level (High/Med/Low) | Notes / Evidence |
| :--- | :--- | :--- | :--- |
| **Pupil Outcome Alignment** | High: Ensures official 2025 and 2026 SEN2 EHCP placement numbers are accurately derived for Local Authority High Needs benchmarking. | High | EHCP placement counts are essential non-financial context for Section 251 High Needs spending. |
| **Benchmarking Completeness** | High: Populates all eight EHCP category totals (`EHCPTotal`, `EHCPMainstream`, `EHCPResourced`, `EHCPSpecial`, `EHCPIndependent`, `EHCPHospital`, `EHCPPost16`, `EHCPOther`) for 2025 and 2026. | High | Accounts for the 2026 split of `special_academy_free` into `special_academy` and `special_free_school`. |
| **Historical Continuity** | High: Preserves `sen2_estab_caseload.csv` ingestion for pre-2025 runs while handling 2025 and 2026 `caseload.csv` schemas via year-keyed configuration. | High | Year-aware configuration isolates 2025 and 2026 rules from historical runs ([ASSUME-004](../../assumptions/ASSUME-004-sen2-caseload-format-from-2025.md)). |
| **Single Source of Truth** | High: Eliminates manual execution of the standalone `sen2_more.py` script before running the pipeline. | High | Raw `caseload.csv` files dropped into `default/{s251_year}/` are processed end-to-end by the pipeline. |

## 5. Assumptions

- [ASSUME-003: DfE dataset schemas evolve as a forward step-function across reporting years](../../assumptions/ASSUME-003-step-function-schema-inheritance-across-years.md)
- [ASSUME-004: SEN2 Local Authority Caseload Source Format from 2025 Onwards](../../assumptions/ASSUME-004-sen2-caseload-format-from-2025.md)

## 6. Conclusion & Recommendation

**Recommendation**: GO

**Rationale**:
This feature satisfies all criteria in the FBIT Value Rule Set with high confidence. Integrating the `caseload.csv` pivot directly into the pipeline's configuration-driven pre-processing stage eliminates manual pre-processing steps, prevents duplicate-key failures on the 2026 split special school categories, and preserves backward compatibility for pre-2025 Section 251 runs.

## 7. Knowledge Captured

- [KB-0001: FBIT Organisational Value Definition and Value Rule Set](../../knowledge/KB-0001-fbit-value-rule-set.md)
