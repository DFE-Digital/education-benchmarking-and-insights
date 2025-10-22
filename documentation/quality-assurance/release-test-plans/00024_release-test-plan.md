# Release Test Plan: 2025.10.3

_*Release version updated to 2025.10.3 following resolution of deployment failures caused by WAF configuration updates. The initial 2025.10.1 release encountered production deployment issues, which were partially addressed in 2025.10.2, and fully resolved in 2025.10.3 after successful pre-production smoke testing._*

**Release Date:** 2025/10/22  
**Release Label:** 2025.10.3

## Introduction

This plan defines the approach for testing release `2025.10.3`, covering smoke, sanity, and data testing activities required for the platform and data pipeline.  
This release delivers a combination of new features, enhancements, refactoring, and maintenance updates to improve performance, maintainability, and user experience across the platform.

## Scope

**In-scope:**

- **New Features**
  - Added BFR Benchmark IT Spending page for Trusts providing a benchmarking view and allowing Trusts to compare IT spending across peers, improving financial transparency and efficiency tracking.
  - Added a dedicated Financial Benchmarking Insights Summary page for Trusts, providing high-level financial overviews and key performance indicators.

- **Enhancements**
  - Aggregated workforce metrics for schools in federations in schools to improve reporting and comparability.
  - Refined the LA homepage to add High Needs section as per red line review.
  - Files and images storage updated to be served from Blob storage for improved platform performance, scalability, and reliability.

**Out-of-Scope:**

- Simplified and refactored comparator set and RAG backend logic for better maintainability and consistent performance.
- Introduced automated nightly checks to validate that all commercial resource links remain active.
- Updated copy for revenue reserve on the Historic data → Balance tab.
- Updated total expenditure computation logic for MAT schools.
- Removed Direct Revenue Financing from Other cost category.
- Cleaned up unused flags and removes deprecated logic for maintainability.
- High-level DfE Databricks slides added for internal use.
- Added richer tracking of user login activity for analytics and behavioural insights.
- September 2025 dependency updates reviewed and merged.
- Implemented News pages for improved content management and news visibility.
- Excluded false positives from WAF configurations.
- Refreshed LA homepage layout (WIP and won't be included in this release).

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
- UAT completed and any findings have been satisfied.
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
- **Risk:** We might encounter errors during production deployment due to WAF configuration changes, which cannot be validated in earlier environments.
  - **Mitigation:** Put in a fix and do another release.

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

**Release (First Update):**

The initial release encountered issues due to the WAF configuration updates. These issues were only reproducible during the production deployment stage and prevented the release from completing successfully.

- **Original Planned Release:** 2025.10.1
- **Hotfix Release Version:** 2025.10.2
- **Issue Identified:** WAF configuration updates caused unexpected failures during deployment to production.
- **Fix Implemented:** Adjusted WAF settings.
- **Testing Impact:** The failure occurred post-deployment; the fix was verified internally and incorporated into the subsequent release (2025.10.2) for validation.

**Release (Second Update):**

A follow-up release was initiated with the WAF fix included. However, the 2025.10.2 release also failed during deployment, requiring further validation before proceeding.

- **Release Version:** 2025.10.2
- **Hotfix Release Version:** 2025.10.3
- **Changes Included:** Applied WAF configuration fixes and stability updates following the 2025.10.2 deployment failure.
- **Testing Impact:** A smoke test in pre-production is required to confirm platform stability and ensure all functionalities are working as expected before reattempting production deployment.

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

**Summary of results:**

| Test Category           | Total Tests | Passed | Failed |  Pass Rate  |  
|-------------------------|:-----------:|:------:|:------:|:-----------:|  
| Smoke Tests - Prod      |      1      |   1    |   0    |    100%     |  
| Smoke Tests - Pre Prod  |      2      |   2    |   0    |    100%     |
| Sanity Tests - Pre Prod |     16      |   16   |   0    |    100%     |  
| Data Smoke Tests        |      2      |   2    |   0    |    100%     |  
| Total                   |     21      |   21   |   0    |    100%     |  

<!-- Leave the rest of this page blank -->
\newpage
