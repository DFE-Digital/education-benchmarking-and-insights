# Release Test Plan - 2025.02.5

_*Release version incremented to 2025.02.5 after 2025.02.4 was canceled due to a requested update. The increment also addresses an error encountered during the deployment of 2025.02.3 to pre-production._

**Release Date:** 19/02/2025  
**Release Label:** 2025.02.5

## Introduction

This plan defines the approach for testing release `2025.02.5`, covering all testing necessary.  
Ensure the enhancements, and bug fixes in `2025.02.5` are functioning as expected without adversely impacting existing functionality.
The release encompasses the standard code release, which incorporates all enhancements, and bug fixes.

## Scope

**In-scope:**

- **Enhancements:**
  - Additional features for exporting and sharing charts for offline use:
    - Ability to save all chart images, allowing users to easily download and reference visual data.
    - Context data, including cost codes, now appears on chart images for better clarity when exporting.
    - Users can download selected images instead of all, providing more flexibility.
    - Offline images have been updated to display additional information for improved usability.

- **Bug Fixes:**
  - Data Accuracy and Presentation:
    - Corrected an issue where establishments with the same name were displayed incorrectly in y-axis ticks.
    - Resolved inconsistent rounding issues on comparison page charts.

**Out-of-Scope:**

- General improvements related to service reliability, maintainability, and performance optimisations.
- Infrastructure changes, dependency updates, and logging enhancements.
- Automated processes, background data pipeline updates, and retry mechanisms.
- UI styling and front-end framework updates.
- Testing of datasets, system configurations, or features not included in this release.

## Test Strategy

- **Functional Testing:**
  - Enhancements: Test enhanced features for correct functionality.
- **Smoke Testing:** Execute smoke tests to validate the basic functionality of the application post-deployment to production.
- **Sanity Testing:** Perform sanity checks on bug fixes, enhancements to confirm the updates.

## Entry and Exit Criteria

**Entry Criteria:**

- All code changes for the release are completed and successfully deployed to the pre-production environment.
- No high-priority open defects.

**Exit Criteria:**

- All high-priority test cases are executed and passed in pre-production.
- No critical defects remain open.
- Signed off by stakeholders.

## Roles and Responsibilities

- **QA lead:** Coordinate testing activities, manage test cases, and defect triage.
- **Engineer(s):** Execute test cases, report, and retest defects.
- **Stakeholders:** Participate in user acceptance testing and provide final approval.
- **Technical lead:** Oversee release planning.
- **Project lead:** Go/no-go decisions.

## Risk Analysis

- **Risk:** Bugs & Defects in Production. Unexpected software defects can cause system crashes, data corruption, or functional failures.
  - **Mitigation:** Conduct thorough testing (unit, integration, regression testing). Implement automated testing to catch issues early.

- **Risk:** Poor User Adoption. Users may struggle with new features or frustration.
  - **Mitigation:** Gather post-release feedback and quickly address usability concerns.

## Test Deliverables

- Test plan document
- Test cases (Functional)
- Test summary report outlining test results, pass/fail rates, and any outstanding issues

## Approval

- Stakeholders
- Project lead
- QA Lead
- Technical lead

## Notes

**Release Overview:**

This release focuses on improvements in data pipeline, enhanced chart export functionality, key bug fixes, and February dependencies.

- **Current Release Version:** 2025.02.5

**[Azure Test Plan](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_testPlans/define?planId=250580&suiteId=250581)**

**Azure DevOps tickets:**

- [239369 - SQL server & database alerting](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/239369)
- [240689 - Replace MERGE for data-pipeline jobs](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/240689)
- [242097 - Download this page data](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/242097)
- [242099 - Save all chart images](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/242099)
- [242100 - Context data on chart images (cost codes)](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/242100)
- [245257 - Download Selected Images](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/245257)
- [245786 - Inconsistent rounding off on charts - comparison page](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/245786)
- [246376 - Change workforce data issue - custom data](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/246376)
- [246758 - Migrate from Application Insights instrumentation keys to connection strings](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/246758)
- [247363 - Update offline images to show additional information](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/247363)
- [247409 - Consolidate front end stylesheets](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/247409)
- [248372 - February 2025 dependencies](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/248372)
- [248424 - Allow Polly to retry API requests that fail with 429](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/248424)
- [248427 - Add additional Function App logging for self, and dependencies](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/248427)
- [249104 - New default pipeline run completion should mark affected UserData as Active = 0](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/249104)
- [249204 - Incorrect dimension on trust benchmarking](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/249204)
- [249206 - Dimension changes both contexts for both balance charts](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/249206)
- [249227 - Water Sewerage Costs missing](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/249227)
- [249236 - School percent income values displaying incorrectly](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/249236)
- [249969 - Benchmarking charts display establishments with the same name incorrectly in y-axis ticks](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/249969)

## Appendix

### Test Summary Report

| Test Category     | Total Tests | Passed | Failed | Pass Rate |  
|-------------------|:-----------:|:------:|:------:|:---------:|  
| Functional Tests  |     12      |   12   |   0    |   100%    |  
| Smoke Tests       |      X      |   X    |   X    |    X%     |  
| Sanity Tests      |      2      |   2    |   0    |   100%    |  
| Total             |      X      |   X    |   X    |    X%     |  

<!-- Leave the rest of this page blank -->  
\newpage  
