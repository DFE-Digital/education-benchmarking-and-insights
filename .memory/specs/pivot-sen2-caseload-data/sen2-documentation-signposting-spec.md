## ADDED Requirements

### Requirement: REQ-007 Reference Documentation Update for SEN2 Caseload Source File
When the SEN2 `caseload.csv` ingestion and pivot capability is introduced, the system documentation in the Reference quadrant (`docs/content/reference/sources.md`) shall signpost that `caseload.csv` is the required SEN2 raw input file from 2025 onwards, while `sen2_estab_caseload.csv` remains the input for pre-2025 historical runs.

#### Acceptance Criteria
- Given the Non-Financial Data Files table in `docs/content/reference/sources.md`, when updated for this feature, then the SEN2 entry clearly documents `caseload.csv` (for 2025 and 2026 S251 runs) alongside `sen2_estab_caseload.csv` (for 2024 and earlier runs), states that the pipeline automatically pivots `caseload.csv` in memory, and notes the 2025 (`special_academy_free`) versus 2026 (`special_academy` and `special_free_school`) schema variation.
- When the documentation is reviewed against project standards, then it adheres to UK English spelling, avoids em dashes and horizontal rules, and aligns with the Reference quadrant of the Diátaxis framework.

### Requirement: REQ-008 Data Release How-To Guides Signposting for `caseload.csv`
When the SEN2 `caseload.csv` ingestion and pivot capability is introduced, the task-oriented Data Release How-to guides (`docs/content/how-to/data-release-guide/s251.md`, `docs/content/how-to/data-release-guide/cfr.md`, and `docs/content/how-to/data-release-guide/aar.md`) shall signpost that operators supply `caseload.csv` for 2025 and 2026 runs without running a manual external SEN2 pivot script.

#### Acceptance Criteria
- Given the expected source file checklists in `docs/content/how-to/data-release-guide/s251.md`, `docs/content/how-to/data-release-guide/cfr.md`, and `docs/content/how-to/data-release-guide/aar.md`, when updated for this feature, then each checklist signposts `caseload.csv` for 2025 onwards (and `sen2_estab_caseload.csv` for pre-2025 runs).
- Given the 2025 format change note in `docs/content/how-to/data-release-guide/s251.md`, when updated for this feature, then it explains that the SEN2 `caseload.csv` pivot is executed automatically by the data pipeline for 2025 and 2026 runs and no longer requires manual pre-conversion into `sen2_estab_caseload.csv`.
