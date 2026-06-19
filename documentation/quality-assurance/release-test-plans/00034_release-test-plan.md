# Release Test Plan: 2026.06.0

Release Date: 19/09/2026
Release Label: 2026.06.0

## Introduction

This document outlines the approach of testing release `2026.06.0` covering the necessary testing activities.
This release introduces filtering to school benchmarking page, refactors comparator selection logic, includes dependency updates, fixes historic trust data visualisation issues and fix to data on trust to trust benchmarking page.

## Scope

**In-scope:**

- **Enhancements**
  - Improved the school benchmarking page by adding a filter menu to enhance usability and navigation.

- **Bug Fixes**
  - Corrected dimension data issues on the trust historic page to ensure income and balance charts display accurately.
  - Corrected the trust-to-trust benchmarking page to display accurate percentages for all dimensions.

- **Maintenance**
  - Refactored the local authority comparator selection logic to improve code maintainability and simplify view models.
  - Reviewed and merged critical May and June 2026 platform and package dependency updates.

**Out-of-Scope:**

- CFR transparency file generation using the pipeline has been implemented but is excluded from testing for this release.

## Test Strategy

- **Smoke Testing (Pre-Prod):** Verify that all core functionalities behind login are accessible and working as expected in the pre-production environment.
- **Sanity Testing (Production):** Validate that newly released features are functioning correctly in the production environment.
- **Smoke Testing (Production):** Perform basic post-deployment checks to confirm system stability and availability.

## Entry and Exit Criteria

- Entry Criteria:
  - Code deployed successfully to the target environment.
  - Environment is accessible and stable for testing.
  - feature flag is turned on

- Exit Criteria:
  - All planned smoke and sanity tests are executed.
  - No critical or high-severity defects remain open.
  - Stakeholders confirm readiness for release.

## Roles and Responsibilities

- **QA Lead:** Coordinate smoke, sanity testing and manage overall sign-off.
- **Engineer(s):** Execute validation, defect investigation, and retesting.
- **Stakeholders:** Provide acceptance sign-off where required.
- **Technical Lead:** Oversee the overall release and technical quality.
- **Project Lead:** Own go/no-go decision.

## Risk Analysis

- **Risk:** The feature flag must be manually toggled and supplemented with post-release code changes to guarantee it stays in the toggled state, incorrectly applying these changes could introduce a risk.
  - **Mitigation:** Immediate post-deployment validation after feature flag is toggled.
- **Risk:** May '26 dependency updates could introduce regressions or dependency conflicts across unrelated platform paths.
  - **Mitigation:** Run extended smoke checks on critical user journeys.

## Test Deliverables

- Test plan document
- Test execution results
- Defect reports
- Test summary report

## Approval

- **Stakeholders**
- **Project Lead**
- **QA Lead**
- **Technical Lead**

## Notes

**Release Overview:**

Release commpleted successfully and all smoke and sanity tests passed. Filters have been implemented on the school benchmarking page, local authority comparator selection logic has been refactored, and critical dependency updates have been merged. Bug fixes for trust historic data visualisation and trust-to-trust benchmarking percentages have been applied.

**Azure DevOps tickets included in this release:**

- [298070 - School Benchmark Spending - Implement filters](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/298070)
- [300765 - Refactor LA Comparator selection Controller Flow and Simplify View Models](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/300765)
- [313835 - Review and merge additional May '26 dependency updates](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/313835)
- [316452 - Trust Historic data for Income/Balance charts view graph as options](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/316452)
- [313865 - Convert CFR file generation SQL to python](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/313865)
- [316607 - Fix incorrect percentages in trust-to-trust benchmarking](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/316607)
- [316634 - Review and merge additional June '26 dependency updates](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/316634)

## Appendix

### Test Summary Report

**Summary of results:**

| Test Category           | Total Tests | Passed | Failed | Pass Rate |
|-------------------------|:-----------:|:------:|:------:|:---------:|
| Smoke Tests - Prod      |      1      |   1    |   0    |   100%    |
| Smoke Tests - Pre Prod  |      1      |   1    |   0    |   100%    |
| Sanity Tests - Pre Prod |      1      |   1    |   0    |   100%    |
| Total                   |      3      |   3    |   0    |   100%    |

\newpagepage
