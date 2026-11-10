## ADDED Requirements

### Requirement: REQ-001 Year-Aware SEN2 Input Source Selection
When executing Local Authority Section 251 (`s251`) pre-processing, the system shall select the SEN2 raw input file from the standard raw storage directory (`default/{s251_year}/`) based on the configured filename for the `s251` reporting year (`caseload.csv` for 2025 and 2026 runs, and `sen2_estab_caseload.csv` for pre-2025 runs).

#### Acceptance Criteria
- Given an `s251` reporting run for year `2025` or `2026`, when the pipeline loads SEN2 ancillary data, then it reads `default/{s251_year}/caseload.csv` from the raw storage container.
- Given an `s251` reporting run for a year prior to `2025` (such as `2024`), when the pipeline loads SEN2 ancillary data, then it reads `default/{s251_year}/sen2_estab_caseload.csv` from the raw storage container and bypasses the `caseload.csv` transformation step.

### Requirement: REQ-002 Caseload Filtering and Unpivoting to Standard SEN2 Structure
When ingesting `caseload.csv` for a 2025 or 2026 `s251` run, the system shall filter, normalise, and unpivot (melt) the wide caseload dataset into the standard `sen2_estab_caseload` column structure before executing the existing SEN2 calculation logic.

#### Acceptance Criteria
- Given a raw `caseload.csv` dataset for 2025 or 2026, when the caseload transformation runs, then the system retains only rows where `breakdown` equals `"All EHC plans"`, drops the configured percentage and breakdown topic columns, and sets `breakdown` to `"Total"`.
- When the caseload columns are renamed and unpivoted using the configured identifier columns (`time_period`, `time_identifier`, `geographic_level`, `country_code`, `country_name`, `region_code`, `region_name`, `new_la_code`, `old_la_code`, `la_name`, `breakdown`), then each establishment column is melted into `establishment_type` and `num_caseload`, `establishment_group` is derived from the configured group matching rules (defaulting unmatched types to `"z"`), and `breakdown` is renamed to `ehcp_or_statement`.
- When the transformation completes, then the resulting dataset contains the exact ordered columns required by the existing SEN2 processor (`time_period`, `time_identifier`, `geographic_level`, `country_code`, `country_name`, `region_code`, `region_name`, `new_la_code`, `old_la_code`, `la_name`, `establishment_group`, `establishment_type`, `ehcp_or_statement`, `num_caseload`).

### Requirement: REQ-003 In-Memory Integration with Existing SEN2 Processing Logic
When the `caseload.csv` transformation produces the normalised SEN2 dataset for a 2025 or 2026 run, the system shall pass the transformed dataset directly in memory into the existing SEN2 processing logic without writing an intermediate CSV file to storage.

#### Acceptance Criteria
- Given a 2025 or 2026 `s251` run, when `caseload.csv` is transformed into the standard SEN2 structure, then the pipeline passes the transformed data in memory to the existing SEN2 processing stage and does not write an intermediate `sen2_estab_caseload.csv` file.
- Given the transformed SEN2 dataset for 2025 or 2026, when passed to the existing SEN2 processing stage, then the system filters out rows where `old_la_code` is empty, filters `time_period` to the target `s251` year, and filters `ehcp_or_statement` to `"Total"`.
- When the existing SEN2 processing stage pivots and evaluates the dataset, then it outputs a DataFrame indexed by `new_la_code` and `old_la_code` containing `EHCPTotal`, `EHCPMainstream`, `EHCPResourced`, `EHCPSpecial`, `EHCPIndependent`, `EHCPHospital`, `EHCPPost16`, and `EHCPOther`.
