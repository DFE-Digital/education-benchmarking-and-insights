## ADDED Requirements

### Requirement: REQ-004 Local Authority Attainment 8 Extraction and Persistence (2026 Run)
When executing the 2026 reporting run, the system shall extract the Local Authority Average Attainment 8 score for each Local Authority from the KS4 performance dataset and persist it in the Local Authority Non-Financial Profile.

#### Acceptance Criteria
- Given a 2026 reporting run and a KS4 dataset containing Local Authority aggregate records (identified by record type 4 and Local Authority code), when Local Authority non-financial data is processed, then each Local Authority record is matched to its corresponding Local Authority Attainment 8 score.
- When the Local Authority Non-Financial Profile is saved for the 2026 run, then the Local Authority Attainment 8 score is persisted to two decimal places for each matched Local Authority.
- If a Local Authority has no matching Local Authority aggregate record in the 2026 KS4 dataset, then the system stores an empty (null) value for Local Authority Attainment 8 for that Local Authority.

### Requirement: REQ-005 National Average Attainment 8 Extraction and Persistence (2026 Run)
When executing the 2026 reporting run, the system shall extract the National Average Attainment 8 score from the KS4 performance dataset and persist it in the Local Authority Non-Financial Profile for each Local Authority.

#### Acceptance Criteria
- Given a 2026 reporting run and a KS4 dataset containing a National aggregate record (identified by record type 5), when Local Authority non-financial data is processed, then the National Average Attainment 8 score is associated with each Local Authority record in that run.
- When the Local Authority Non-Financial Profile is saved for the 2026 run, then the National Average Attainment 8 score is persisted to two decimal places alongside the Local Authority Attainment 8 score.

### Requirement: REQ-006 Backward Compatibility for Pre-2026 Local Authority Runs
If a reporting run is executed for a reporting cycle prior to 2026, then the system shall complete Local Authority processing without requiring Local Authority or National Average Attainment 8 values.

#### Acceptance Criteria
- Given a reporting run for a year earlier than 2026 (such as 2024 or 2025), when the pipeline processes Local Authority non-financial data, then the system does not fail if Local Authority or National Attainment 8 breakdowns are absent.
- When the Local Authority Non-Financial Profile is saved for a pre-2026 run, then the Local Authority Attainment 8 and National Average Attainment 8 metrics are stored as empty (null) values.
