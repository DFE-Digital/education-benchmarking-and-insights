# Release Test Plan - 2024.12.1

**Release Date:** 09/12/2024

**Release Label:** 2024.12.1

## Introduction

This plan defines the approach for testing release `2024.12.1`.

Ensure that the enhancement, and critical bug fix in `2024.12.1` are functioning as expected without adversely impacting existing
functionality.

## Scope

**In-scope:**

- Enhancements
  - Release of Financial Benchmarking and Insights Summary feature
  - Remove Ofsted rating from front end
  - Monthly security patches
- Bug fixes
  - Validation errors from School/Trust/LA suggesters
  - Percentage precision inconsistencies in front end
  - Typo in CFP journey on mixed classes

**Out-of-Scope:**

- Any new functionality not targeted for this release.

## Test Strategy

- Sanity Testing: Perform sanity checks on bug fixes to confirm their resolution.
- Smoke Testing: Execute smoke tests to validate the basic functionality of the application post-deployment.

## Entry and Exit Criteria

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

N/A

## Test Deliverables

- Test plan document.
- Test summary report outlining test results, pass/fail rates, and any outstanding issues.

## Approval

- Stakeholders
- Project lead
- QA Lead
- Technical lead

## Notes

**[Azure Test Plan](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_testPlans/define?planId=241361&suiteId=241362)**

**Azure DevOps tickets:**

- [222988 : Financial Benchmarking and Insights](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/222988)
- [232831 : Remove Ofsted rating from service (front end)](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/239852)
- [240744 : Validation error from School/Trust/LA suggesters](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/240744)
- [217223 : Percentages being shown to 2 decimals places](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/217223)
- [216444 : CFP Journey - Typo/ spelling error](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/216444)
- [240701 : Dependabot - December 2024](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/240701)

## Appendix

### Test Summary Report

**Summary of results:**

| Test Category | Total Tests | Passed | Failed | Pass Rate |
|---------------|:-----------:|:------:|:------:|:---------:|
| Sanity Tests  |      6      |   6    |   0    |   100%    |
| Smoke Tests   |      1      |   1    |   0    |   100%    |
| Total         |      7      |   7    |   0    |   100%    |

<!-- Leave the rest of this page blank -->
\newpage
