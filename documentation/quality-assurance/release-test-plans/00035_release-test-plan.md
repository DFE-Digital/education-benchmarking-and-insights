# Release Test Plan: 2026.08.0

Release Date: TBC
Release Label: 2026.08.0

## Introduction

This document outlines the approach of testing release `2026.08.0` covering the necessary testing activities.
This release focuses on database security configuration changes (removing Azure service access from the database security configuration), alongside data pipeline enhancements, improved page referrer tracking and reporting, a fix to user-defined comparators, dependency updates and supporting documentation.

## Scope

**In-scope:**

- **Database & Security Configuration**
  - Removed Azure service access from the database security configuration.

- **Enhancements**
  - Improved page referrer tracking and reporting.
  - Enabled the data pipeline to run from the point of having the maintained master schools list file.
  - Added break points into the data pipeline.

- **Bug Fixes**
  - Corrected user-defined comparators so the selected dimension updates correctly.

- **Maintenance**
  - Updated `Platform.Cache.Tests` mocks to support the API changes in `Microsoft.Azure.StackExchangeRedis 3.3.1`.
  - Reviewed and merged July 2026 platform and package dependency updates.

**Out-of-Scope:**

- No items are out of scope for this release.

## Test Strategy

- **Smoke Testing (Pre-Prod):** Verify that all core functionalities behind login are accessible and working as expected in the pre-production environment, including database connectivity following the security configuration change.
- **Sanity Testing (Production):** Validate that newly released features are functioning correctly in the production environment.
- **Smoke Testing (Production):** Perform basic post-deployment checks to confirm system stability and availability.

## Entry and Exit Criteria

- Entry Criteria:
  - Code deployed successfully to the target environment.
  - Environment is accessible and stable for testing.

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

- **Risk:** Removing Azure service access from the database security configuration could affect database connectivity or permissions for dependent services.
  - **Mitigation:** Validate database connectivity and service access immediately after deployment as part of pre-production smoke testing before promoting to production.
- **Risk:** July '26 dependency updates could introduce regressions or dependency conflicts across unrelated platform paths.
  - **Mitigation:** Run extended smoke checks on critical user journeys.
- **Risk:** The first deployment of the release will fail as configuration changes are applied.
  - **Mitigation:** Execute a second deployment immediately after configuration application, which is expected to succeed (green deployment).

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

{To be completed after release.}

**Azure DevOps tickets included in this release:**

- [316772 - Remove azure service access from database security configuration](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/316772)
- [305821 - Update Platform.Cache.Tests mocks to support API changes in Microsoft.Azure.StackExchangeRedis 3.3.1](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/305821)
- [318282 - User defined comparators - dimension not updating](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/318282)
- [318286 - Improve page referrer tracking and reporting](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/318286)
- [319567 - Review and merge Jul '26 dependency updates](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/319567)
- [323597 - Enable pipeline to run from point of having the maintained master schools list file](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/323597)
- [323600 - Add in break points in to the data pipeline](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_workitems/edit/323600)

## Appendix

### Test Summary Report

**Summary of results:**

| Test Category           | Total Tests | Passed | Failed | Pass Rate |
|-------------------------|:-----------:|:------:|:------:|:---------:|
| Smoke Tests - Prod      |     TBD     |  TBD   |  TBD   |    TBD    |
| Smoke Tests - Pre Prod  |     TBD     |  TBD   |  TBD   |    TBD    |
| Sanity Tests - Pre Prod |     TBD     |  TBD   |  TBD   |    TBD    |
| Total                   |     TBD     |  TBD   |  TBD   |    TBD    |

\newpage
