# Release Test Plan - 2025.05.2

**Release Date:** 20/05/2025
**Release Label:** 2025.05.2

## Introduction

This plan defines the approach for testing release `2025.05.2`, covering the necessary smoke testing. This release primarily focuses on implementing changes required for SFB decommissioning.

## Scope

**In-scope:**

- Updates and Enhancements
  - Deployment of holding website, ensuring that all relevant routes are correctly redirected.

**Out-of-Scope:**

- Tech Debt Items
- Any new functionality not targeted for this release
- Dependency updates

## Test Strategy

- Smoke Testing: Execute smoke tests to validate the basic functionality of the application post-deployment.

## Entry and Exit Criteria

**Entry Criteria:**

- All code changes for the release are completed and deployed to the pre-production environment.

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

## Test Deliverables

- Test plan document
- Test cases (Smoke)
- Test summary report outlining test results, pass/fail rates, and any outstanding issues

## Approval

- Stakeholders
- Project lead
- QA Lead
- Technical lead

## Notes

**Azure DevOps tickets:**

- [254608 - Route parameter validation](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/254608)
- [260643 - Prevent 500 errors from 499 API responses from throwing exceptions and logging to App Insights](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/260643)
- [260832 - Remove redundant code changes made to set the redirects)](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/260832)


<!-- Leave the rest of this page blank -->
\newpage
