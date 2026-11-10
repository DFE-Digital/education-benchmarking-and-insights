# Value Assessment: Populate Attainment 8 Metrics

**Date**: 2026-10-23
**Status**: Final

## 1. Context

- **Feature / Initiative**: Populate Attainment 8 Metrics (`FEAT-001`)
- **Associated Vision**: [Product Vision](../../product-vision.md)
- **Feature Specification**: [Feature Specification](feature.md)

## 2. Organisational Value Definition

- **Primary Value Driver(s)**: Pupil Outcome Alignment, Benchmarking Completeness, Accountability and Oversight Support, and Historical Continuity ([KB-0001](../../knowledge/KB-0001-fbit-value-rule-set.md)).
- **Strategic Goals Alignment**: Directly supports the core FBIT objectives to support better financial planning for improved pupil outcomes and to provide Local Authorities, governors, and trustees with reliable comparative performance data.

## 3. Value Rule Set

| Rule / Criterion | Description | Minimum Threshold for Go |
| :--- | :--- | :--- |
| **Pupil Outcome Alignment** | Enables correlation between financial expenditure and official pupil educational outcomes. | Directly exposes or persists an official DfE outcome metric for school or Local Authority benchmarking. |
| **Benchmarking Completeness** | Provides necessary comparative context (such as Local Authority and National averages) for valid comparisons. | Supplies both organisation-level and comparator/national average metrics where applicable. |
| **Historical Continuity** | Maintains operational compatibility with prior financial and academic year pipeline runs. | Zero regressions on pre-2026 pipeline runs when introducing 2026 reporting metrics. |
| **Single Source of Truth** | Derives metrics directly from authoritative DfE input datasets without manual data manipulation. | 100% automated ingestion and persistence within the standard pipeline workflow. |

## 4. Feature Assessment

| Criterion | Expected Impact | Confidence Level (High/Med/Low) | Notes / Evidence |
| :--- | :--- | :--- | :--- |
| **Pupil Outcome Alignment** | High: Persists official KS4 Attainment 8 scores for schools and Local Authorities. | High | Attainment 8 is already partially ingested in KS4 pre-processing; persisting it completes the data lifecycle ([ASSUME-002](../../assumptions/ASSUME-002-attainment8-benchmarking-value.md)). |
| **Benchmarking Completeness** | High: Adds both Local Authority Average and National Average Attainment 8 for the 2026 cycle. | High | Record types 4 (Local Authority) and 5 (National) exist in the authoritative KS4 file for 2026. |
| **Historical Continuity** | High: Preserves pre-2026 pipeline compatibility via optional (nullable) metrics and year-aware extraction. | High | Scoped so that LA/National breakdowns are only required on the 2026 run ([ASSUME-001](../../assumptions/ASSUME-001-historical-ks4-schema-compatibility.md)). |
| **Single Source of Truth** | High: Fully automated extraction from the existing KS4 raw dataset during pipeline runs. | High | No manual data entry or external data sources are required beyond the standard KS4 input blob. |

## 5. Assumptions

- [ASSUME-001: Historical KS4 datasets do not require Local Authority or National Attainment 8 extraction](../../assumptions/ASSUME-001-historical-ks4-schema-compatibility.md)
- [ASSUME-002: Attainment 8 metrics improve financial and educational outcome benchmarking](../../assumptions/ASSUME-002-attainment8-benchmarking-value.md)

## 6. Conclusion & Recommendation

**Recommendation**: GO

**Rationale**: 
This feature meets all four criteria in the FBIT Value Rule Set with high confidence. It unlocks a critical Department for Education academic outcome metric (Attainment 8) at both the school and Local Authority levels for the 2026 reporting cycle while protecting historical pipeline runs from regression.

## 7. Knowledge Captured

- [KB-0001: FBIT Organisational Value Definition and Value Rule Set](../../knowledge/KB-0001-fbit-value-rule-set.md)
