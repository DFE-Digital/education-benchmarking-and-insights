# Release Test Plan - 2025.04.1

_*Release version incremented to 2025.04.1 after 2025.04.0 encountered an error during production deployment. The issue was related to SFB decommissioning changes._

**Release Date:** 30/04/2025
**Release Label:** 2025.04.1

## Introduction

This plan defines the approach for testing release `2025.04.1`, covering all functional, non-functional, and regression testing necessary.
Ensure that all new features, enhancements, and bug fixes in `2025.04.1` are functioning as expected without adversely impacting existing functionality.

This release encompasses CFO details update/refresh, as well as the High Needs data ingestion.

## Scope

**In-scope:**

- New features
  - High Needs Benchmarking feature consisting of the dashboard page, benchmarking, national view, and historic data.
- Enhancements
  - CFO contact details in the service have been updated with the latest data.
  - The "-T" suffix has been removed from cost codes in the service.
  - Custom comparators will now exclude schools that have not submitted their returns.
- Bug fixes
  - The Nursery School ICFP journey now correctly displays the applicable years.

**Out-of-Scope:**

- Any new functionality not targeted for this release
- Dependency updates

## Test Strategy

- Functional Testing: Check the high needs feature is functionality as expected.
- Exploratory Testing: Coordinate with business stakeholders to do an exploratory testing of the release features.
- Sanity Testing: Perform sanity checks on bug fixes to confirm their resolution.
- Smoke Testing: Execute smoke tests to validate the basic functionality of the application post-deployment.

## Entry and Exit Criteria

**Entry Criteria:**

- All code changes for the release are completed and deployed to the pre-production environment.
- The pre-production environment is set up with the required data. As part of the release, the following files have been added or updated:
  - `High-needs-local-authority-benchmarking-tool.xlsm`
  - `sen2_estab_caseload.csv`
  - `2018 SNPP Population persons.csv`
  - `s251_alleducation_la_regional_national.csv`
  - `plannedexpenditure_schools_other_education_la_unrounded_data.csv`
  - `CFO.xlsx`
- High Needs feature is enabled.
- Pipeline run is completed.

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

- **Risk:** Bugs & Defects in Production. Unexpected software defects can cause system crashes, data corruption, or functional failures.
  - **Mitigation:** Conduct thorough testing (unit, integration, regression testing). Implement automated testing to catch issues early.

- **Risk:** Poor User Adoption. Users may struggle with new features or frustration.
  - **Mitigation:** Gather post-release feedback and quickly address usability concerns.

- **Risk:** High needs feature might fail in the UAT.
  - **Mitigation:** Turn off the High needs feature and proceed with the release of other updates.

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

**Release Overview:**

A deployment issue in production with version 2025.04.0 caused by SFB decommissioning changes that could only be validated in the live environment—required us to increment the release version. The original release was abandoned, and the fix was included in the new version.

- **Original Planned Release:** 2025.04.0
- **New Release Version:** 2025.04.1
- **Hotfixes Included:** Fix for a Terraform deployment error encountered during the production deployment.
- **Current Release (2025.04.1):** Contains the necessary hotfix.
- **Testing Impact:** No impact to the release testing plan.

**[Azure Test Plan](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_testPlans/define?planId=259145&suiteId=259146)**

**Azure DevOps tickets:**

- [249535 - Local authority homepage - access point](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/249535)
- [249538 - Dashboard page - high needs](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/249538)
- [249539 - Snapshot of national rankings](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/249539)
- [249540 - Snapshot of historic data](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/249540)
- [249541 - Help/support section](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/249541)
- [249546 - National ranking page - view all](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/249546)
- [249550 - Historic data - view all](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/249550)
- [249552 - Benchmarking journey - selection of comparators](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/249552)
- [249557 - Benchmarking page - display data](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/249557)
- [249575 - Headline figures](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/249575)
- [250438 - Ingest Section 251 - Outturn data](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/250438)
- [250505 - Ingest Statistical Neighbours - dataset](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/250505)
- [250667 - Ingest Section 251 - Budget data](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/250667)
- [250819 - Ingest ONS data - population dataset](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/250819)
- [251853 - Ingest SEN2 data](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/251853)
- [252392 - LA financial data projection/storage](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/252392)
- [253282 - LA non-financial data projection/storage](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/253282)
- [253286 - LA Statistical Neighbours data projection/storage](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/253286)
- [253802 - High needs snagging / UR prep](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/253802)
- [254433 - Nursery schools try to use this feature, it asks for pupils from Year 7 to Year 11 (Bug)](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/254433)
- [254434 - Custom comparators include schools that didn't submit a return](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/254434)
- [254814 - CFO contact detail - refresh](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/254814)
- [255674 - Choose local authorities to benchmark against - MVP iteration](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/255674)
- [255679 - Dashboard Page - MVP Iteration](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/255679)
- [255683 - General content review - MVP iteration](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/255683)
- [256320 - Benchmark Spending Page - MVP Iteration](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/256320)
- [257399 - March 2025 dependencies](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/257399)
- [257474 - Incorrect SEN2 values (Bug)](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/257474)
- [258226 - Removal of the "-T" suffix from cost codes displayed for academies](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/258226)
- [258233 - High Needs ONS population data ingestion year fix (Bug)](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/258233)
- [258519 - Display S251 line codes on HN benchmarking charts](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/258519)
- [258520 - Display 2 to 18 population figures on HN benchmarking tables](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/258520)

**Azure DevOps tickets included however feature disabled and not tested:**

- [250281 - School Journey - Filtered search](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/250281)
- [250287 - Trust Journey - Filtered search](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/250287)
- [250286 - Local Authority Journey - Filtered search](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/250286)

## Appendix

### Test Summary Report

**Summary of results:**

| Test Category     | Total Tests | Passed | Failed | Pass Rate |
|-------------------|:-----------:|:------:|:------:|:---------:|
| Smoke Tests       |      1      |   1    |   0    |   100%    |
| Functional Tests  |      7      |   7    |   0    |   100%    |
| Sanity Tests      |      1      |   1    |   0    |   100%    |
| Exploratory Tests |     10      |   8    |   2    |    80%    |
| Total             |     19      |   17   |   2    |   89.4%   |

<!-- Leave the rest of this page blank -->
\newpage
