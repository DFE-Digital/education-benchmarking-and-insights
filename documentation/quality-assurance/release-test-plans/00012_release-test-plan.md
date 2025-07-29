# Release Test Plan: 2024.11.3

**Release Date:** 20/11/2024

**Release Label:** 2024.11.3

## Introduction

This plan defines the approach for testing release `2024.11.3`.

Ensure that the enhancement, and critical bug fix in `2024.11.3` are functioning as expected without adversely impacting existing
functionality.

## Scope

**In-scope:**

- Enhancements
  - Data pipeline orchestrator triggers search indexers on completion
- Bug fixes
  - Schools without building comparator set unable to view spending priorities

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

**[Azure Test Plan](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_testPlans/define?planId=238516&suiteId=238517)**

**Azure DevOps tickets:**

- [232831 : Trigger the search indexers post pipeline run](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/232831)
- [237597 : Unable to view Spending Priorities when no building comparator set](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/237597)

## Appendix

### Test Summary Report

**Summary of results:**

| Test Category | Total Tests | Passed | Failed | Pass Rate |
|---------------|:-----------:|:------:|:------:|:---------:|
| Sanity Tests  |      2      |   2    |   0    |   100%    |
| Smoke Tests   |     20      |   20   |   0    |   100%    |
| Total         |     22      |   22   |   0    |   100%    |

<!-- Leave the rest of this page blank -->
\newpage
