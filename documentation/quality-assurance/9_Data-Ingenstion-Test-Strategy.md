# 📊 Data Ingestion – Test Strategy

## Overview

This document outlines the strategy for testing the annual ingestion of data for BFR (Budget Forecast Return), CFR (Consistent Financial Reporting), and AAR (Academy Accounts Return). The ingestion process involves generating input files, processing them through a pipeline, storing results in a database, and exposing the data on the service.

CFR and AAR also require ancillary file updates.
> **Note:** This strategy document will be updated whenever any new data types or ingestion flows are added.

## Scope

- **Included:**
  - Data types: BFR, CFR, AAR
  - Ancillary files for CFR and AAR (e.g., GIAS, census)
  - Pipeline processing and transformation logic
  - Database storage validation
  - Service layer presentation testing

- **Excluded:**
  - Source data correctness from upstream providers
  - UI rendering bugs unrelated to data
  - External API integrations outside ingestion scope

## Objectives

- Validate schema and data quality of input files
- Confirm correct year-context processing
- Verify successful ingestion and transformation via pipeline
- Ensure correct and complete storage in database
- Ensure correct data is visible on the service for the new year
- Validate that ancillary files update and integrate as expected

## Test Phases

**Pre-Ingestion Checks:**

- Verify file structure/schema
- Run sampling check for data integrity (e.g. valid URNs, company numbers)

**Pipeline Execution Validation:**

- Trigger pipeline with test files in local environment
- Confirm processing completes with no errors
- Review loge

**Database Validation:**

- Verify that

  - Correct year tables are populated
  - Expected row counts per school/trust match
  - Correct mapping joins (e.g., URN to Trust, LA to School)
  - No duplicate or dropped rows

**Ancillary File Integration(if applicable):**

- Validate structure and row count of updated ancillary files
- Cross-check mappings are applied
- Regression spot-check: same school in 2023 vs. 2024 shows correct mapping updates

**Service/UI Validation:**

- Confirm new-year data appears for expected schools/trusts
- Spot-check key metrics (e.g., pupil count, expenditure) vs input file
- Confirm filtering and year-switching logic

## Tools & Environment

- **Pipeline environments**: `dev`, `test`, `pre prod`
- **Validation tools**: SQL client, Python (Pandas)
- **Monitoring/logging**: Application logs for pipeline stages

## Roles and Responsibilities

| Role          | Responsibility                                               |
|---------------|--------------------------------------------------------------|
| Data Engineer | File preparation, pipeline execution, Logs                   |
| QA            | Schema validation, DB checks, UI testing, Regression testing |
| Product/BA    | Review business rules, validate mapping and metrics logic    |

## Risks & Mitigation

| Risk                                           | Mitigation                                                                                                                                                 |
|------------------------------------------------|------------------------------------------------------------------------------------------------------------------------------------------------------------|
| **Upstream data format or schema has changed** | Validate incoming files against expected schema before ingestion. Engage with upstream data source early in the release cycle to confirm format stability. |

## Exit Criteria

- All test cases pass for each data type
- Ancillary files integrated with no issues
- New-year data visible and accurate in the service
- Prior-year data unchanged and accessible
- QA sign-off on data checks
- Logs and metrics show success

## Test Artifacts

- Test scripts
- Log captures of pipeline and DB output
- Test results document (manual/automated)
- Regression comparison reports for past years

## 12. Versioning

This test strategy is reviewed and updated yearly before ingestion begins.

| Year | Updated By | Notes |
|------|------------|-------|
| 2025 | QA Lead    | N/A   |

<!-- Leave the rest of this page blank -->
\newpage
