# Release Test Plan: 2025.11.1

**Release Date:** TBC  
**Release Label:** 2025.11.1

## Introduction

This plan defines the approach for testing release `2025.11.1`, covering smoke and sanity testing activities.
This release delivers a combination of enhancements, custom data generation fix and maintenance updates to improve performance, maintainability, and user experience across the platform.

## Scope

**In-scope:**

- **Enhancements**
  - Local authority home page updated to show pupil, workforce, financial and priority schools RAG ratings.
  - Progress 8 score added on benchmark spending page giving users a combined view of spend vs outcomes. 
  
- **Bug Fixes**
  - Issues with custom data generation has also been fixed.

**Out-of-Scope:**

- Completed November ’25 dependency updates to ensure platform compatibility, stability, and security compliance.

## Test Strategy

- **Sanity Testing:** Validate that the application and pipeline deploy successfully and operate as expected with new and refactored components.
- **Smoke Testing:** Execute smoke tests to confirm platform stability and availability post-deployment.
- **UAT Testing:** UAT on enhancements on benchmark spending page to show progress 8 data.

## Entry and Exit Criteria

**Entry Criteria:**

- All code changes have been deployed to pre-production.
- Pre production pipeline run is successfully completed.

**Exit Criteria:**

- All smoke, sanity checks pass successfully.
- No critical or high-severity defects remain open.
- Stakeholder approval obtained for release progression.

## Roles and Responsibilities

- **QA Lead:** Coordinate smoke, sanity, and data validation, and manage overall sign-off.
- **Engineer(s):** Execute validation, defect investigation, and retesting.
- **Stakeholders:** Conduct UAT and provide acceptance sign-off.
- **Technical Lead:** Oversee the overall release and technical quality.
- **Project Lead:** Own go/no-go decision.

## Risk Analysis

- **Risk:** Dependency updates may cause unexpected regressions in unrelated platform components.
  - **Mitigation:** Run extended smoke tests on critical user journeys after deployment.
- **Risk:** We might encounter errors during production deployment due to WAF configuration changes, which cannot be validated in earlier environments.
  - **Mitigation:** Start release early in the day giving enough time to put in a fix and do another release.

## Test Deliverables

- Test plan document
- Test cases (smoke, sanity testing)
- Test execution results and defect logs
- Test summary report with final release recommendation

## Approval

- **Stakeholders**
- **Project Lead**
- **QA Lead**
- **Technical Lead**

## Notes

**[Azure Release Test Plan](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_testPlans/define?planId=288905&suiteId=288906)**

**Azure DevOps tickets included in this release:**

- [281520 - LA homepage - Pupil and workforce data tab](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/281520)
- [281521 - LA Homepage - Priority schools by RAG ratings](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/281521)
- [286933 - Unable to generate custom data](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/286933)
- [281519 - LA Homepage - Financial data tab](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/281519)
- [265003 - Review & merge November '25 dependency updates](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/265003)
- [283987 - Benchmark spending - Surface Progress 8 Data for KS4 schools](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/283987)

## Appendix

### Test Summary Report

**Summary of results:**

| Test Category           | Total Tests | Passed | Failed |  Pass Rate  |  
|-------------------------|:-----------:|:------:|:------:|:-----------:|  
| Smoke Tests - Prod      |             |        |        |             |  
| Sanity Tests - Pre Prod |             |        |        |             |  
| UAT - Pre Prod          |             |        |        |             |  
| Total                   |             |        |        |             |  

<!-- Leave the rest of this page blank -->
\newpage
