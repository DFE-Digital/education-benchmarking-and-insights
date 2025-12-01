# Release Test Plan: 2025.11.1

**Release Date:** 2025/12/01  
**Release Label:** 2025.11.1

## Introduction

This plan defines the approach for testing release `2025.11.1`, covering smoke and sanity testing activities.  
This release delivers a combination of enhancements of Progress 8 data, updating survey link, bug fixes, and content updates on benchmarking pages.

## Scope

**In-scope:**

- **Enhancements**
  - KS4 schools homepage updated to surface Progress 8 banding and score.
  - Benchmark pupil and workforce data updated to surface Progress 8 Data giving combine comparision view.
  - Content updates on benchmark Spending and pupil and workforce data page to align with pattern and provide clarity in different scenarios.
  - Feedback banner updated with new survey and relevant changes on contact us page.

- **Bug Fixes**
  - Revenue reserve as percentage of income in trust historic data has now been fixed which was incorrectly computed previously.
  - High needs Benchmarking page charts legends are now showing as expected now.

## Test Strategy

- **Sanity Testing:** Validate that the application is deployed successfully and operate as expected with new enhancements and refactored components.
- **Smoke Testing:** Execute smoke tests to confirm platform stability and availability post-deployment.
- **UAT Testing:** UAT on enhancements for Progress 8 data surfaced across school homepage, benchmark pupil/workforce data, and benchmark spending pages.

## Entry and Exit Criteria

**Entry Criteria:**

- All code changes have been deployed to pre-production.

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

- **Risk:** During UAT, critical issues may be identified in the Progress 8 functionality.
  - **Mitigation:** Start the pre prod checks as early as possible giving us time to do another release before the release to public date (3rd December) if anything is spotted.
- **Risk:** WAF code changes might fail when deploying changes to production.
  - **Mitigation:** Do a test deployment to prod like environment to validate impact before hand.

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

**Release Overview:**

The release was completed successfully with no issues and progress 8 feature turned off.

**[Azure Release Test Plan](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_testPlans/define?planId=292376&suiteId=292377)**

**Azure DevOps tickets included in this release:**

- [285442 - Trust Historic data revenue reserve dimension issue](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/285442)
- [285516 - School Homepage - Surface Progress 8 Data for KS4 schools](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/285516)
- [285570 - Benchmark pupil and workforce data - Surface Progress 8 Data for KS4 schools](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/285570)
- [285897 - Feedback banner - survey replacement](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/285897)
- [290366 - Benchmark Spending - Display no comparator set content](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/290366)
- [290680 - Benchmark Spending/pupil and workforce data (Default) - content updates](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/290680)
- [290780 - Benchmark Spending/pupil and workforce data (Custom Data Set Not In Use) - Content Updates](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/290780)
- [290853 - Highneed Benchmarking page not showing chart legend](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/290853)
- [291735 - Resolve positioning of dimension drop down and code codes with vertically stacked chart actions](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/291735)

## Appendix

### Test Summary Report

**Summary of results:**

| Test Category           | Total Tests | Passed | Failed | Pass Rate |  
|-------------------------|:-----------:|:------:|:------:|:---------:|  
| Smoke Tests - Prod      |      1      |   1    |   0    |   100%    |  
| Sanity Tests - Pre Prod |      6      |   6    |   0    |   100%    |  
| UAT - Pre Prod          |      1      |   1    |   0    |   100%    |  
| Total                   |      8      |   8    |   0    |   100%    |  

<!-- Leave the rest of this page blank -->
\newpage
