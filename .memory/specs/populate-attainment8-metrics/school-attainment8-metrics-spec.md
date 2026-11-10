## ADDED Requirements

### Requirement: REQ-001 School-Level Attainment 8 Extraction and Persistence
When processing Key Stage 4 (KS4) performance data for a reporting run, the system shall extract the average Attainment 8 score for each school and persist it in the School Non-Financial Profile.

#### Acceptance Criteria
- Given a valid KS4 performance dataset containing school-level Attainment 8 scores, when the pipeline processes maintained schools and academies, then each school with a valid KS4 record has its Attainment 8 score persisted to two decimal places in the School Non-Financial Profile.
- Given a school that is present in the financial dataset (such as a Maintained School or Academy) and has a matching KS4 record, when non-financial data is saved, then the persisted Attainment 8 value matches the source `ATT8SCR` metric from the KS4 dataset.

### Requirement: REQ-002 Handling Unreported or Non-Applicable School Attainment 8 Data
If a school does not have an applicable or reported Attainment 8 score in the KS4 performance dataset, then the system shall record an empty (unpopulated) Attainment 8 value in the School Non-Financial Profile.

#### Acceptance Criteria
- Given a school with no corresponding entry in the KS4 dataset (for example, a Primary or Nursery school), when the School Non-Financial Profile is persisted, then the Attainment 8 metric is stored as empty (null).
- Given a reporting run where the KS4 source file is not provided, when the pipeline executes, then the pipeline completes without error and stores empty (null) Attainment 8 values for all schools.
- Given a school present in the KS4 dataset with a suppressed Attainment 8 value (`NP`, `NE`, `SUPP`, `LOWCOV`), when the KS4 dataset is pre-processed, then the suppressed value is converted to `0.00` in accordance with existing KS4 pre-processing behaviour.

### Requirement: REQ-003 School Attainment 8 Continuity Across Reporting Runs
While executing either a 2026 reporting run or an earlier historical reporting run, the system shall process and persist school-level Attainment 8 scores without requiring Local Authority breakdown columns to be present in historical KS4 files.

#### Acceptance Criteria
- Given a pre-2026 reporting run using a historical KS4 dataset, when the pipeline processes school-level KS4 metrics, then the run completes without error and persists school-level non-financial metrics ([ASSUME-001](../../assumptions/ASSUME-001-historical-ks4-schema-compatibility.md)).
