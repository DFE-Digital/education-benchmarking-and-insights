## ADDED Requirements

### Requirement: REQ-004 Configurable Time Period Normalisation per S251 Year
When transforming `caseload.csv` for a given `s251` reporting year, the system shall normalise the raw academic year `time_period` value (such as `202425` for 2025 and `202526` for 2026) to the 4-digit `s251` reporting year using year-aware configuration.

#### Acceptance Criteria
- Given a 2025 `s251` run where `caseload.csv` records the 2024/25 academic year as `202425`, when the caseload transformation runs, then `time_period` `202425` is normalised to `2025` so that downstream SEN2 filtering on `str(year)` matches the 2025 records.
- Given a 2026 `s251` run where `caseload.csv` records the 2025/26 academic year as `202526`, when the caseload transformation runs, then `time_period` `202526` is normalised to `2026` so that downstream SEN2 filtering on `str(year)` matches the 2026 records.

### Requirement: REQ-005 Year-Aware 2025 and 2026 Establishment Category Schema Configuration
When transforming `caseload.csv`, the system shall apply year-specific column rename mappings and category evaluation rules from configuration to account for the 2025 combined `special_academy_free` column and the 2026 split `special_academy` and `special_free_school` columns.

#### Acceptance Criteria
- Given a 2025 `s251` run, when the caseload transformation validates and renames columns using the 2025 configuration, then `special_academy_free` is mapped to `"Special - Academy/free"` under the `"Special school"` establishment group.
- Given a 2026 `s251` run, when the caseload transformation validates and renames columns using the 2026 configuration, then `special_academy` and `special_free_school` are both mapped to `"Special - Academy/free"` and combined (summed) into a single `"Special - Academy/free"` value per Local Authority so that no duplicate-key error occurs during the downstream SEN2 pivot and the downstream `EHCPSpecial` formula remains unchanged.
- Given a 2025 or 2026 `s251` run, when the downstream SEN2 processor evaluates EHCP category totals, then both 2025 and 2026 use the 2025/2026 `la_sen2_eval` category formulas (where `EHCPHospital` excludes `Hospital School` and `EHCPPost16` excludes `Other FE` and `Sixth form college`).
- If any column specified in the active year's rename configuration is missing from `caseload.csv`, then the system raises a `KeyError` identifying the missing columns before unpivoting.

### Requirement: REQ-006 Missing, Suppressed, and Legacy Category Value Handling
When transforming `caseload.csv` for a 2025 or 2026 `s251` run, the system shall populate legacy establishment columns (`"Sixth form college"`, `"Other FE"`, and `"Hospital School"`) and any empty `num_caseload` entries with the suppression marker `"x"` so that downstream SEN2 type conversion and `na_values` handling operate consistently.

#### Acceptance Criteria
- Given a 2025 or 2026 `caseload.csv` dataset, when the caseload transformation runs, then `"Sixth form college"`, `"Other FE"`, and `"Hospital School"` are initialised to `"x"` prior to unpivoting, and any null values in `num_caseload` after unpivoting are replaced with `"x"`.
- When the transformed dataset is ingested by the existing SEN2 processor, then `"x"` and `"z"` values in `num_caseload` are parsed as `NA` (`Int64` nullable integer) without raising type conversion errors.
