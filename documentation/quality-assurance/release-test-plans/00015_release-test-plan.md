# Release Test Plan - [TBC]

**Release Date:** TBC  
**Release Label:** [TBC]

## Introduction

## Introduction
>
>[!NOTE]
>Update the below to reflect the purpose/objective for this plan, so that it is specific to the release.

This plan defines the approach for testing release `[release label]`, covering all functional, non-functional, and regression testing necessary.
Ensure that all new features, enhancements, and bug fixes in `[release label]` are functioning as expected without adversely impacting existing functionality.

## Scope
>
>[!NOTE]
>In-scope list should include a link to the ADO ticket.
>Ticket title may be adequate to describe the feature/enhancement/bug fix,
>however if it doesn't than a meaning title should be added here.
>
>Out-of-scope list any other areas that are explicitly out of scope.

## Scope

**In-scope:**

- New Features:
  - Historic pages updated to show historical averages nationally.

- Enhancements:
  - Removed negative and zero cost category value for schools for better analysis.
  - Added cost codes to all charts and downloaded charts for improved clarity.
  - Extended "Save as Image" functionality on spending priorities and historic data page.
  - Improved service reliability and maintainability:
    - handled of varying input schemas for pupil and workforce census to accept varying schema.
    - Handled updated Cost and Income codes in AAR for 2024
    - Support for optional Ofsted data points in GIAS datasets to enhance flexibility.
  - Additional features for exporting and sharing charts for offline use:
    - Download multiple chart images, copy to clipboard
    - download this page data.

- Bug Fixes:
  - Data Accuracy and Presentation:
    - Corrected RAG value calculations to show aggregated data for federations.
    - Updated/fixed inconsistencies with pupil numbers.
  - UI and Usability Fixes:
    - Ensured multi-selection of LAs working as intended when doing user defined comparator set.
    - Added hover-over information for line charts for better accessibility.
    - Added appropriate message when no data is submitted in BFR.

**Out-of-Scope:**

- Any new functionality or enhancements not explicitly targeted for this release.
- Non-critical visual or content updates not related to user experience or data correctness.
- Testing scenarios outside the scope of existing modules, including datasets not included in this release.

## Test Strategy
>
>[!NOTE]
>Add/remove/update where necessary to reflect the types of testings for this release.

- Functional Testing:
  - Features: Test new and updated features for correct functionality.
  - Regression: Verify that existing functionality remains intact with new changes.
- Non-Functional Testing:
  - Performance: Load testing on peak usage scenarios.
  - Security: Test for SQL injection, XSS, and other vulnerabilities.
- Exploratory Testing: Explore features and functionality without predefined scripts, to uncover issues and assess quality.
- User Acceptance Testing: Coordinate with business stakeholders to validate functionality aligns with business needs.
- Smoke Testing: Execute smoke tests to validate the basic functionality of the application post-deployment.
- Sanity Testing: Perform sanity checks on bug fixes to confirm their resolution.

## Entry and Exit Criteria
>
>[!NOTE]
>Add/remove/update where necessary to reflect the criteria for this release.

**Entry Criteria:**

- All code changes for release are completed and deployed to the pre-production environment.
- Pre-production environment is set up with required data.

**Exit Criteria:**

- All high-priority test cases pass.
- No critical defects remain open.
- Signed off by stakeholders.


## Roles and Responsibilities

- **QA lead:** Coordinate testing activities, manage test cases and defect triage.
- **Engineer(s):** Execute test cases, report and retest defects.
- **Stakeholders:** Participate in user acceptance testing and provide final approval.
- **Technical lead:** Oversee release planning.
- **Project lead:** Go/no-go decisions.

## Risk Analysis
>
>[!NOTE]
>Add risks (with mitigation) for this release.

- **Risk:**
  - **Mitigation:**

## Test Deliverables
>
>[!NOTE]
>Add/remove/update where necessary to reflect the deliverables for this release.

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

**Azure DevOps tickets:**

- [242989 Handle varying input schemas](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/242989)
- [237633 - Benchmarking historical trends](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/237633)
- [235748 - Suppressing negative and zero values](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/235748)
- [241716 - Federation per unit RAG values incorrect](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/241716)
- [241759 - Missing pupil numbers](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/241759)
- [241954 - Handle custom-data/part-year RAG](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/241954)
- [236233 - BFR Spinner when no data submitted](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/236233)
- [225945 - A02: Hover over information for line charts](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/225945)
- WIP [241700 - Change Cost and Income codes in aar](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/241700)
- [241720 - Multi-selection of LA's not working](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/241720)
- [243796 - Extend Log Analytics Workspace data retention period](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/243796)
- [243084 - Dependencies - January 2025](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/243084)
- [231743 - Break pre-processing module into a smaller package/modules](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/231743)
- [231273 - ADO pipeline formatting check for data pipeline build](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/231273)
- WIP [244563 - FE - Download Transparency File](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/244563)
- WIP [241881 - Ofsted data points optional in GIAS dataset](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/241881)
- [242105 - User defined comparator/custom data improvements](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/242105)
- WIP [239257 - Add index to UserData table](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/239257)
- [245362 - Replace recharts-to-png with html-to-image](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/245362)
- [245267 - Add titles on all graphs](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/245267)
- WIP [244349 - Add Save as Image to additional pages](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/244349)
- WIP [242099 - Save all chart images](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/242099)
- WIP [242101 - Copy chart to clipboard](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/242101)
- WIP [242100 - Context data on chart images (cost codes)](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/242100)
- WIP [245257 - Download Selected Images](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/245257)
- WIP [242097 - Download this page data](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/242097)
