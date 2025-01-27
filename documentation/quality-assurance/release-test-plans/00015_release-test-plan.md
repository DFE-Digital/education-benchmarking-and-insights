# Release Test Plan - [TBC]

**Release Date:** TBC  
**Release Label:** [TBC]

## Introduction

## Introduction

This plan defines the approach for testing release `[release label]`, covering all testing necessary.
Ensure the new feature, data release,  enhancements, and bug fixes in `[release label]` are functioning as expected without adversely impacting existing functionality.

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

- Functional Testing:
  - Features: Test new and updated features for correct functionality.
- Smoke Testing: Execute smoke tests to validate the basic functionality of the application post-deployment.
- Sanity Testing: Perform sanity checks on bug fixes, enhancments to confirm the updates.
- User Acceptance Testing: Coordinate with business stakeholders to validate functionality aligns with business needs.

## Entry and Exit Criteria
>
>[!NOTE]
>Add/remove/update where necessary to reflect the criteria for this release.

**Entry Criteria:**

- All code changes for release are completed and successfully deployed to the pre-production environment.
- Pipeline run is completed successfully with the updated files including pupil and workforce census, AAR 2024 files in pre-production.

**Exit Criteria:**

- All high-priority test cases have passed.
- No critical defects remain open.
- UAT Testing completed.
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
