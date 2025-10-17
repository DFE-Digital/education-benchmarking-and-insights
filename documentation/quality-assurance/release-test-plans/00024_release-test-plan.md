# Release Test Plan: 2025.10.1

**Release Date:** TBC  
**Release Label:** 2025.10.1

## Introduction

This plan defines the approach for testing release `2025.10.0`, covering smoke, sanity, and data testing activities required for the platform and data pipeline.  
This release delivers a combination of new features, enhancements, refactoring, and maintenance updates to improve performance, maintainability, and user experience across the platform.

## Scope

**In-scope:**

- **New Features**
  - Introduction of *BFR Benchmark IT Spending page for Trusts*.
  - Creation of a *FBIS for Trust page* to enhance financial insights.

- **Enhancements**
  - Aggregated workforce metrics to improve reporting and comparability.
  - Implemented changes to High Needs section following the Red Line Review.
  - Files and images storage updated to be served from Blob storage

**Out-of-Scope:**

- Refactored comparator set and RAG logic for maintainability and performance.
- Nightly monitoring of commercial resource links to ensure reliability.
- Updated copy for revenue reserve on the *Historic data → Balance* tab.
- Improved clarity in total expenditure with updated MAT central spend apportionment logic.
- Refreshed LA homepage (top and bottom sections).
- Removed Direct Revenue Financing from Other cost category.
- Removed redundant feature flags and legacy code for FilteredSearch and HistoricalTrends.
- High-level DfE Databricks slides added for internal use.
- Login initiation tracking with improved action context.
- September 2025 dependency updates reviewed and merged.
- Implementation of *News pages* for improved content management.
- Excluded false positives from WAF configurations.

## Test Strategy

- **Sanity Testing:** Validate that the application and pipeline deploy successfully and operate as expected with new and refactored components.
- **Smoke Testing:** Execute smoke tests to confirm platform stability and availability post-deployment.
- **Data Smoke Testing:** Validate that BFR IT spending data, workforce data, and related metrics are correctly processed and reflected in downstream outputs.
- **UAT Testing:** User acceptance testing on the features and enhancements.

## Entry and Exit Criteria

**Entry Criteria:**

- All code changes for this release are completed and deployed to pre-production.
- pipeline run has been successfully executed.

**Exit Criteria:**

- All smoke, sanity, and data validation checks pass successfully.
- UAT completed and any findings have been 
- No critical or high-severity defects remain open.
- Stakeholder approval obtained for release progression.
- Files moved to new storage account as per updates in [278870](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/278870)

## Roles and Responsibilities

- **QA Lead:** Coordinate smoke, sanity, and data validation, and manage overall sign-off.
- **Engineer(s):** Execute validation, defect investigation, and retesting.
- **Stakeholders:** Conduct UAT and provide acceptance sign-off.
- **Technical Lead:** Oversee refactoring work and technical quality.
- **Project Lead:** Own go/no-go decision.

## Risk Analysis

- **Risk:** Refactoring of comparator and RAG logic may introduce regression in comparator generation.
  - **Mitigation:** Validated it in the previous pipeline environments.

## Test Deliverables

- Test plan document
- Test cases
- Data validation results
- Test summary report with results and sign-off status

## Approval

- Stakeholders
- Project lead
- QA Lead
- Technical lead

## Notes

**Release Overview:**

This release introduces several user-facing enhancements and backend refactoring to improve reliability, maintainability, and overall data accuracy.  
New trust-level pages and improved comparator logic have been validated in test environments with production datasets.  
Nightly link monitoring and performance improvements have been included as part of ongoing maintenance.

**[Azure Release Test Plan](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_testPlans/define?planId=285868&suiteId=285869)**

**Azure DevOps tickets included in this release:**

- [264127 - News pages](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/264127)
- [265001 - Review & merge September '25 dependency updates](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/265001)
- [266041 - Create BFR Benchmark IT spending page for Trusts](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/266041)
- [266550 - Refactor comparator set logic](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/266550)
- [266551 - Refactor rag logic](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/266551)
- [266568 - Nightly Monitoring of Commercial Resource Links](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/266568)
- [268157 - Changes to copy for Revenue reserve on Historic data page Balance tab](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/268157)
- [269145 - Track Login Initiation with action context](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/269145)
- [269582 - Improve consistency in Total Expenditure by clarifying MAT central spend apportionment](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/269582)
- [270539 - Remove Legacy Code for FilteredSearch](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/270539)
- [270542 - Remove redundant feature flags](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/270542)
- [270556 - Remove Legacy Code for HistoricalTrends](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/270556)
- [270713 - Create a FBIS for Trust page](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/270713)
- [278071 - High level DfE Databricks slides](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/278071)
- [278246 - Workforce Metrics Aggregation](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/278246)
- [278482 - Remove Direct Revenue Financing from "Other" Cost Category](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/278482)
- [280563 - Create Comparator Set for BFR IT Spend](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/280563)
- [281524 - LA Homepage - Top and bottom sections](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/281524)
- [283119 - WAF False Positive Exclusions](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/283119)
- [283922 - High Needs - Changes following the Red Line Review](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/283922)
- [278870 - Serve images and files from Blob Storage through Front Door using path-based routing.](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/278870)

## Appendix

### Test Summary Report

**Summary of results (to be completed post-testing):**

| Test Category           | Total Tests | Passed | Failed | Pass Rate |  
|-------------------------|:-----------:|:------:|:------:|:---------:|  
| Smoke Tests - Prod      |     TBC     |   -    |   -    |     -     |  
| Sanity Tests - Pre Prod |     TBC     |   -    |   -    |     -     |  
| Data Smoke Tests        |     TBC     |   -    |   -    |     -     |  
| Total                   |     TBC     |   -    |   -    |     -     |  

<!-- Leave the rest of this page blank -->
\newpage
