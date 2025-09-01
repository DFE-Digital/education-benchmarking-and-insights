# Release Test Plan: 2025.09.0

_*Release version updated to 2025.09.0 after fixes for issues identified in UAT. The original 2025.08.0 release had an issue with the user-defined comparator set, which was patched in 2025.08.1._

**Release Date:** TBC  
**Release Label:** 2025.09.0

## Introduction

This plan defines the approach for testing release `2025.09.0`, covering smoke, sanity, data ingestion, and UAT testing activities required for the data pipeline.  
This release delivers enhancements to the data pipeline to support the CFR 2025 data drop and modularisation for maintainability.

## Scope

**In-scope:**

- Data Drop

  - CFR 2025 data drop

- Enhancements

  - Data pipeline enhancements to ingest IT spending breakdown costs to support CFR 2025 data drop
  - WAF updates to provide access to service outside the UK

- Bug Fixes

  - Leadership total now include total of non-teaching leadership staff. 
  - Income label for Other DFE/EFS revenue grants has been updated.
  - Figures on pages are now rounded off as a whole £.

**Out-of-Scope:**

- Modularisation of the data pipeline for maintainability
- Benchmarking IT spending charts (behind feature flag, not enabled in this release)
- Enhancements to trust benchmarking for Education ICT
- Content updates to the BAE code
- Regression fixes within the user-defined comparators journey

## Test Strategy

- **Sanity Testing:** Validate that the application and data pipeline deploy successfully and are operational with 2025 CFR data load.
- **Smoke Testing:** Execute smoke tests to validate the basic functionality of the application post-deployment.
- **Data Ingestion Testing:** Validate ingestion of the CFR 2025 dataset into the pipeline, ensuring correct mapping, processing, and availability in downstream outputs.
- **User Acceptance Testing (UAT):** Stakeholders to verify new CFR 2025 data availability, accuracy, and usability in the platform.

## Entry and Exit Criteria

**Entry Criteria:**

- All code changes for the release are completed and deployed to the pre-production environment.
- Pipeline run is completed with 2025 CFR data

**Exit Criteria:**

- All smoke, sanity, and data ingestion checks complete successfully.
- UAT completed and signed off by stakeholders.
- No critical defects remain open.
- Sign-off obtained from stakeholders.

## Roles and Responsibilities

- **QA lead:** Coordinate smoke, sanity, data ingestion validation and oversee sign-off.
- **Engineer(s):** Execute validation steps, investigate and retest defects.
- **Stakeholders:** Perform UAT and provide acceptance sign-off.
- **Technical lead:** Oversee pipeline modularisation and build fixes.
- **Project lead:** Own go/no-go decision.

## Risk Analysis

- **Risk:** Ingestion of new IT spending breakdown could cause data inconsistencies with CFR 2025 data.
  - **Mitigation:** Validate data integrity and data pipeline run in previous environments
- **Risk:** Modularisation may break existing dependencies.
  - **Mitigation:** Execute regression-style sanity pipeline run in earlier environments.
- **Risk:** UAT feedback may identify data accuracy issues close to release.
  - **Mitigation:** Engage stakeholders early and validate data quality in pre-production.

## Test Deliverables

- Test plan document
- Test cases
- Data ingestion test cases and results
- UAT feedback summary
- Test summary report with results and sign-off status

## Approval

- Stakeholders
- Project lead
- QA Lead
- Technical lead

## Notes

**Release Overview:**

This release is user-facing with the 2025 CFR data drop and new IT spending cost breakdown preparation.  
Sanity and data ingestion testing will be performed in pre-production.  
Benchmarking IT spending charts will remain behind a feature flag (off by default).

**Previous Fix:**

An issue was identified with the user-defined comparator during pre-prod testing for which a hotfix was added.

- **Original Planned Release:** 2025.08.0
- **Hotfix Release Version:** 2025.08.1
- **Hotfixes Included:** Fixed user-defined comparator creation.
- **Testing Impact:** The fix was successfully tested in an earlier environment. Additionally, a few other completed tickets progressed to the pre-production stage and were validated in the same environment. No additional testing is needed in pre prod.

**Current Release:**

Following UAT, additional issues were identified that required fixes.

- **Current Release Version:** 2025.09.0
- **Changes Included:** Fixes for issues identified during UAT.
- **Testing Impact:** A sanity check will be carried out in pre-prod to validate the updated changes, ensuring that the content is refreshed and data pipeline changes are applied to the correct tables/columns as expected.  

**[Azure CFR release Test Plan](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_testPlans/execute?planId=275364&suiteId=275365)**

**[Azure Test Plan](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_testPlans/define?planId=277517&suiteId=277518)**

**Azure DevOps tickets included in this release:**

- [266033 – CFR changes to IT spend breakdown](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/266033)
- [266552 – Modularise data pipeline](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/266552)
- [270078 – Benchmarking IT spending charts](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/270078)
- [273597 – Tweak data pipeline stats collector to display file date](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/273597)
- [275406 – Data pipeline docker build failure](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/275406)
- [268637 - Education ICT Incorrect-Trust benchmarking](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_sprints/taskboard/FBIT/s198-DfE-Benchmarking-service/Sprint%2047?workitem=268637)
- [269577 - Incorrect BAE Code Displayed for Ground Maintenance Costs](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_sprints/taskboard/FBIT/s198-DfE-Benchmarking-service/Sprint%2047?workitem=269577)
- [276528 - User defined rag generation failure](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/276528)
- [270080 -Benchmarking IT spending charts filtering](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_sprints/taskboard/FBIT/s198-DfE-Benchmarking-service/Sprint%2047?workitem=270080)
- [270083 - Benchmarking IT spending charts tooltip](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_sprints/taskboard/FBIT/s198-DfE-Benchmarking-service/Sprint%2047?workitem=270083)
- [272139 - Part year annotation for SSR charts for IT Spend](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_sprints/taskboard/FBIT/s198-DfE-Benchmarking-service/Sprint%2047?workitem=272139)
- [270092 - Download Benchmark IT spending page data](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_sprints/taskboard/FBIT/s198-DfE-Benchmarking-service/Sprint%2047?workitem=270092)
- [270093 - Save Benchmark IT spending chart images](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_sprints/taskboard/FBIT/s198-DfE-Benchmarking-service/Sprint%2047?workitem=270093)
- [276746 - Leadership total doesn't include non-teaching leadership](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/276746)
- [276747 -Update income label for "Other DFE/EFS revenue grants](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/276747)
- [276748 - Figures to be shown as whole £](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/276748)
- [266319 - Associate new and remove orphaned WAF policies](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/266319)
- [259190 - Update deprecated azurerm_storage_account block in Terraform](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/259190)

## Appendix

### Test Summary Report

**Summary of results (to be completed post-testing):**

| Test Category           | Total Tests | Passed | Failed | Pass Rate |  
|-------------------------|:-----------:|:------:|:------:|:---------:|  
| Smoke Tests - Prod      |     TBC     |  TBC   |  TBC   |    TBC    |  
| Sanity Tests - Pre Prod |     TBC     |   1    |   0    |   100%    |  
| Data Ingestion Tests    |      6      |   6    |   0    |   100%    |  
| Total                   |     TBC     |  TBC   |  TBC   |    TBC    |  

<!-- Leave the rest of this page blank -->
\newpage
