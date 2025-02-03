# Release Test Plan - 2025.02.1

_*Release version incremented to critical issues identified during testing of 2025.01.1._

**Release Date:** 03/02/2025  
**Release Label:** 2025.02.1

## Introduction

This plan defines the approach for testing release `2025.02.1`, covering all testing necessary.
Ensure the new feature, data release,  enhancements, and bug fixes in `2025.02.1` are functioning as expected without adversely impacting existing functionality.

The release encompasses the CFR 2024 data release, the AAR 2024 data release, and the standard code release, which incorporates all planned features and enhancements.

## Scope

**In-scope:**

- New Features:
  - Historic pages updated to show historical averages nationally.

- Enhancements:
  - Removed negative and zero cost category value for schools for better analysis.
  - Improved service reliability and maintainability:
    - Handled of varying input schemas for pupil and workforce census to accept varying schema.
    - Support for optional Ofsted data points in GIAS datasets to enhance flexibility.
  - Additional features for exporting and sharing charts for offline use:
    - Download multiple chart images, copy to clipboard
    - Download this page data.
    - Added cost codes to all charts and downloaded charts for improved clarity.
    - Extended "Save as Image" functionality on spending priorities and historic data page.
  - CFR 2024 and AAR 2024 data release

- Bug Fixes:
  - Data Accuracy and Presentation:
    - Corrected RAG value calculations to show aggregated data for federations.
    - Updated/fixed inconsistencies with pupil numbers.
    - Fixed typos on historic data cost category and dimensions on spending comparison.
  - UI and Usability Fixes:
    - Ensured multi-selection of LAs working as intended when doing user defined comparator set.
    - Added hover-over information for line charts for better accessibility.

**Out-of-Scope:**

- Any new functionality or enhancements not explicitly targeted for this release.
- Non-critical visual or content updates not related to user experience or data correctness.
- Testing scenarios outside the scope of existing modules, including datasets not included in this release.

## Test Strategy

- Functional Testing:
  - Features: Test new and updated features for correct functionality.
- Smoke Testing: Execute smoke tests to validate the basic functionality of the application post-deployment.
- Sanity Testing: Perform sanity checks on bug fixes, enhancements to confirm the updates.
- User Acceptance Testing: Coordinate with business stakeholders to validate functionality aligns with business needs.
- Exploratory Testing: Explore data on the service without predefined scripts, to uncover issues and assess quality.

## Entry and Exit Criteria

**Entry Criteria:**

- All code changes for the release are completed and successfully deployed to the pre-production environment.
- Pipeline run is completed successfully, incorporating updated files (e.g., pupil and workforce census files, and other ancillary files).
- CFR Data Release - data release is prepared and pipeline run is completed successfully in the pre-production environment.
- AAR Data Release - data release is prepared and pipeline run is completed successfully in the pre-production environment.

**Exit Criteria:**

- All high-priority test cases are executed and passed in pre-production.
- No critical defects remain open.
- UAT completed.
- Signed off by stakeholders.

## Roles and Responsibilities

- **QA lead:** Coordinate testing activities, manage test cases and defect triage.
- **Engineer(s):** Execute test cases, report and retest defects.
- **Stakeholders:** Participate in user acceptance testing and provide final approval.
- **Technical lead:** Oversee release planning.
- **Project lead:** Go/no-go decisions.

## Risk Analysis

- **Risk:** Bugs & Defects in Production. Unexpected software defects can cause system crashes, data corruption, or functional failures.
  - **Mitigation:** Conduct thorough testing (unit, integration, regression, and user acceptance testing). Implement automated testing to catch issues early.

- **Risk:** Performance Issues. The service may slow down or become unavailable due to unexpected load or inefficiencies in the new release.
  - **Mitigation:** Perform load testing before deployment. Monitor system health with real-time performance monitoring tools.

- **Risk:** Poor User Adoption. Users may struggle with new features or frustration.
  - **Mitigation:** Gather post-release feedback and quickly address usability concerns.

- **Risk:** Data Migration/Release Failures. Data may be lost, corrupted, or improperly migrated.
  - **Mitigation:** Perform data backups before migration. Conduct a dry run of the migration process in a pre-production environment.

## Test Deliverables

- Test plan document
- Test cases (Functional, Regression, Non-Functional)
- Test charters
- Test summary report outlining test results, pass/fail rates, and any outstanding issues

## Approval

- Stakeholders
- Project lead
- QA Lead
- Technical lead

## Notes

**Release Overview**

A typographical errors were identified during pre-production testing, which needed to be fixed. As a result, we abandoned the old release and included the fixes in the new release.
- **Original Planned Release:** 2025.01.1
- **New Release Version:** 2025.02.1
- **Hotfixes Included:** Corrected a typo in the 'Utilities' cost category dimension on the spending comparison page, and fixed another typo on the historic data page.
- **Current Release (2025.02.1):** contains the necessary hotfix and another typo correction. 
- **Testing Impact** The release testing plan is not impacted as the hotfixes are only content updates. 

**[Azure Test Plan](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_testPlans/execute?planId=247423&suiteId=247424)**

**[Exploratory Tests Charters](https://educationgovuk.sharepoint.com/sites/DfEFinancialBenchmarking/Shared%20Documents/Forms/AllItems.aspx?csf=1&web=1&e=jfyXWO&ovuser=fad277c9%2Dc60a%2D4da1%2Db5f3%2Db3b8b34a82f9%2CFaizan%2EAHMAD%40EDUCATION%2EGOV%2EUK&OR=Teams%2DHL&CT=1701167049235&clickparams=eyJBcHBOYW1lIjoiVGVhbXMtRGVza3RvcCIsIkFwcFZlcnNpb24iOiIyNy8yMzA5MjkxMTIwOCIsIkhhc0ZlZGVyYXRlZFVzZXIiOmZhbHNlfQ%3D%3D&cid=f4847989%2Dbc54%2D427c%2D8740%2Df6b118ede2af&FolderCTID=0x012000B007B75DE8F91C4B82D20FE8B354FCBD&id=%2Fsites%2FDfEFinancialBenchmarking%2FShared%20Documents%2FGeneral%2FTechnical%20Team%2FQA%20Testing%2FExploratory%20Tests%20Charters%2FRelease%202025%2E1%2E1&viewid=7afed90f%2D9f2f%2D431a%2D93ce%2D48075c0e93d8)**

**Azure DevOps tickets:**

- [242989 - Handle varying input schemas](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/242989)
- [237633 - Benchmarking historical trends](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/237633)
- [235748 - Suppressing negative and zero values](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/235748)
- [241716 - Federation per unit RAG values incorrect](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/241716)
- [241759 - Missing pupil numbers](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/241759)
- [241954 - Handle custom-data/part-year RAG](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/241954)
- [236233 - BFR Spinner when no data submitted](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/236233)
- [225945 - A02: Hover over information for line charts](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/225945)
- [241700 - Change Cost and Income codes in aar](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/241700)
- [241720 - Multi-selection of LA's not working](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/241720)
- [243796 - Extend Log Analytics Workspace data retention period](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/243796)
- [243084 - Dependencies - January 2025](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/243084)
- [231743 - Break pre-processing module into a smaller package/modules](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/231743)
- [231273 - ADO pipeline formatting check for data pipeline build](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/231273)
- [244563 - Configuration-driven data source download page](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/244563)
- [241881 - Ofsted data points optional in GIAS dataset](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/241881)
- [242105 - User defined comparator/custom data improvements](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/242105)
- [239257 - Add index to UserData table](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/239257)
- [245362 - Replace recharts-to-png with html-to-image](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/245362)
- [245267 - Add titles on all graphs](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/245267)
- [244349 - Add Save as Image to additional pages](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/244349)
- [242101 - Copy chart to clipboard](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/242101)
- [248237 - Benchmarking spending - utilities costs incorrect dimension name](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/248237)
- [248236 - historic data - spending tab typo](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/248236)

## Appendix

### Test Summary Report

**Summary of results:**

| Test Category     | Total Tests | Passed | Failed | Pass Rate |
|-------------------|:-----------:|:------:|:------:|:---------:|
| Functional Tests  |      2      |   2    |   0    |   100%    |
| Smoke Tests       |      1      |   1    |   0    |   100%    |
| Sanity Tests      |      9      |   9    |   0    |   100%    |
| Exploratory Tests |      2      |   1    |   0    |    WIP    |
| Total             |     14      |   12   |   0    |    WIP    |

<!-- Leave the rest of this page blank -->
\newpage
