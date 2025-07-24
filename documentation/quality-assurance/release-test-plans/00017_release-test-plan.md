# Release Test Plan: 2025.03.0

**Release Date:** 10/03/2025  
**Release Label:** 2025.03.0

## Introduction

This plan defines the approach for testing release `2025.03.0`, covering all testing necessary.  
Ensure that the enhancement, and critical bug fix in `2025.3.0` are functioning as expected without adversely impacting existing
functionality.

## Scope

**In-scope:**

- Enhancements
  - Improved logging and alerting for retry and circuit breaker policies
  - Utilised GIAS `Predecessor` link to derive census and CDC data
- Bug fixes
  - Ensure cache is purged following a data pipeline run

**Out-of-Scope:**

- General improvements related to service reliability, maintainability, and performance optimisations.
- Infrastructure changes, dependency updates, and logging enhancements.
- Local authority High needs feature
- Any new functionality not targeted for this release.

## Test Strategy

- **Smoke Testing:** Execute smoke tests to validate the basic functionality of the application post-deployment to production.
- **Sanity Testing:** Perform sanity checks on bug fixes, enhancements to confirm the updates.

## Entry and Exit Criteria

**Entry Criteria:**

- All code changes for the release are completed and successfully deployed to the pre-production environment.
- No high-priority open defects.

**Exit Criteria:**

- All high-priority test cases are executed and passed in pre-production.
- No critical defects remain open.
- Signed off by stakeholders.

## Roles and Responsibilities

- **QA lead:** Coordinate testing activities, manage test cases, and defect triage.
- **Engineer(s):** Execute test cases, report, and retest defects.
- **Stakeholders:** Participate in user acceptance testing and provide final approval.
- **Technical lead:** Oversee release planning.
- **Project lead:** Go/no-go decisions.

## Risk Analysis

- **Risk:** Bugs & Defects in Production. Unexpected software defects can cause system crashes, data corruption, or functional failures.
  - **Mitigation:** Conduct thorough testing (unit, integration, regression testing). Implement automated testing to catch issues early.

- **Risk:** Data Migration/Release Failures. Data may be lost, corrupted, or improperly migrated.
  - **Mitigation:** Perform data backups before migration. Conduct a dry run of the migration process in a pre-production environment.

## Test Deliverables

- Test plan document
- Test cases (Functional)
- Test summary report outlining test results, pass/fail rates, and any outstanding issues

## Approval

- Stakeholders
- Project lead
- QA Lead
- Technical lead

## Notes

**[Azure Test Plan](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_testPlans/execute?planId=252764&suiteId=252765)**

**Azure DevOps tickets:**

- [249246 - Extend Polly structured logs and add alerting on `429`s](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/249246)
- [249433 - Remove `UserId` (email) from requests to Benchmark `/api/user-data`](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/249433)
- [250420 - Derive missing data via GIAS-links](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/250420)
- [250568 - ClearCacheTrigger fails when called from orchestrator after successful default pipeline run](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/250568)
- [251353 - March 2025 depdendencies](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/251353)

**Azure DevOps tickets included however feature disabled and not tested:**

- [249535 - local authority homepage - access point](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/249535)
- [249538 - Dashboard page - high needs](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/249538)
- [249539 - Snapshot of national rankings](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/249539)
- [249540 - snapshot of historic data](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/249540)
- [249546 - National ranking page - view all](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/249546)
- [249550 - Historic data - view all](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/249550)

## Appendix

### Test Summary Report

**Summary of results:**

| Test Category | Total Tests | Passed | Failed | Pass Rate |
|---------------|:-----------:|:------:|:------:|:---------:|
| Smoke Tests   |      1      |   1    |   0    |   100%    |
| Sanity Tests  |      1      |   1    |   0    |   100%    |
| Total         |      2      |   2    |   0    |   100%    |

<!-- Leave the rest of this page blank -->  
\newpage  
